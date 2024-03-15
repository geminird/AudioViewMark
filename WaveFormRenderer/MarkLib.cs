using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace WaveFormRendererApp
{
    /// <summary>
    /// 使用Sqlite记录文件附加信息，包括文件分类，内容描述等。
    /// </summary>
    public class MarkLib
    {
        public static readonly string dbName = "market.db";
        private static string dbFile;

        public static List<string> Tags = new List<string>();

        public static void InitDb(String folder)
        {
            string databaseFileName = Path.Combine( folder, dbName);
            dbFile = "Data source=" + databaseFileName;
            if (!File.Exists(databaseFileName))
            {
                SQLiteConnection.CreateFile(databaseFileName);
                InitTables();
            }
            Tags = getAllTags();
        }

        public static bool InitTables()
        {
            try
            {
                using (var con = new SQLiteConnection(dbFile))
                {
                    con.Open();
                    using (var cmd = new SQLiteCommand(con))
                    {
                        cmd.CommandText = "DROP TABLE IF EXISTS MarkFiles";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = @"CREATE TABLE MarkFiles(MarkId INTEGER PRIMARY KEY AUTOINCREMENT, TagClass TEXT, FileName TEXT,FileDesc TEXT,MarkDate DATETIME)";
                        cmd.ExecuteNonQuery();


                        cmd.CommandText = "DROP TABLE IF EXISTS MarkTime";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = @"CREATE TABLE MarkTime(id INTEGER PRIMARY KEY AUTOINCREMENT, FileName TEXT, StartTime INTEGER, EndTime INTEGER, Desc TEXT,MarkDate DATETIME)";
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static List<string> getAllTags()
        {
            var cmd = getSqliteCmd();
            List<string> listData = new List<string>();
            using (cmd.Connection)
            {
                using (cmd)
                {
                    cmd.CommandText = "select distinct TagClass from MarkFiles";
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())

                        {
                            listData.Add(rdr.GetString(0));
                        }
                        return listData;
                    }
                }
            }
        }

        private static SQLiteCommand getSqliteCmd()
        {
            var con = new SQLiteConnection(dbFile);
            con.Open();
            var cmd = new SQLiteCommand(con);
            return cmd;
        }

        public static bool AddFileMark(string fileName, string tag, string desc)
        {
            var cmd = getSqliteCmd();
            using (cmd.Connection) 
            {
                using (cmd)
                {
                    cmd.CommandText = "INSERT INTO MarkFiles(TagClass,FileName, FileDesc,MarkDate) VALUES(@class, @name,@desc,@dt)";
                    cmd.Parameters.AddWithValue("@class", tag);
                    cmd.Parameters.AddWithValue("@name", fileName);
                    cmd.Parameters.AddWithValue("@desc", desc);
                    cmd.Parameters.AddWithValue("@dt", DateTime.Now);
                    cmd.Prepare();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        if (!Tags.Contains(tag))
                        {
                            Tags.Add(tag);
                        }
                        return true;
                    }
                    return false;
                }
            }
        }

        public static bool UpdateAudioMark(int id, string fileName, long start, long end, string desc)
        {
            var cmd = getSqliteCmd();
            using (cmd.Connection)
            {
                using (cmd)
                {
                    cmd.CommandText = "update MarkTime set FileName = @name,StartTime = @st, EndTime = @et, Desc = @desc where id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", fileName);
                    cmd.Parameters.AddWithValue("@st", start);
                    cmd.Parameters.AddWithValue("@et", end);
                    cmd.Parameters.AddWithValue("@desc", desc);
                    cmd.Prepare();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static bool AddAudioMark(string fileName, long start, long end, string desc)
        {
            var cmd = getSqliteCmd();
            using (cmd.Connection)
            {
                using (cmd)
                {
                    cmd.CommandText = "INSERT INTO MarkTime(FileName,StartTime, EndTime, Desc,MarkDate) VALUES(@name, @st, @et, @desc, @dt)";
                    cmd.Parameters.AddWithValue("@name", fileName);
                    cmd.Parameters.AddWithValue("@st", start);
                    cmd.Parameters.AddWithValue("@et", end);
                    cmd.Parameters.AddWithValue("@desc", desc);
                    cmd.Parameters.AddWithValue("@dt", DateTime.Now);
                    cmd.Prepare();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static MarkFile GetMarkFileByName(string fileName)
        {
            var cmd = getSqliteCmd();
            using (cmd.Connection)
            {
                using (cmd)
                {
                    cmd.CommandText = "select MarkId, TagClass,FileName, FileDesc,MarkDate from MarkFiles where FileName=@fn order by MarkId asc";
                    cmd.Parameters.AddWithValue("@fn", fileName.Trim());
                    cmd.Prepare();
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        if(rdr.Read())
                        {
                            MarkFile mf = new MarkFile() { 
                                MarkId = rdr.GetInt32(0),
                                TagClass = rdr.GetString(1),
                                FileName = rdr.GetString(2),
                                MarkDesc = rdr.GetString(3),
                                MarkDate = rdr.GetDateTime(4)
                            };
                            return mf;
                        }
                    }
                }
            }
            return null;
        }

        public static List<MarkFile> GetMarkFiles()
        {
            var cmd = getSqliteCmd();
            List<MarkFile> listData = new List<MarkFile>();
            using (cmd.Connection)
            {
                using (cmd)
                {
                    cmd.CommandText = "select MarkId, TagClass,FileName, FileDesc,MarkDate from MarkFiles";
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {

                        // print column headers with the data from a database table.

                        //Console.WriteLine($"{rdr.GetName(0),-3} {rdr.GetName(1),-8} {rdr.GetName(2),8}");

                        //listData.Add($"{rdr.GetName(0)}\t {rdr.GetName(1)}\t\t\t {rdr.GetName(2)}\t {rdr.GetName(3)} \t {rdr.GetName(4)}");
                        while (rdr.Read())

                        {

                            //Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetInt32(2)} {rdr.GetString(3)}");

                            //listData.Add($"{rdr.GetInt32(0)} \t{rdr.GetString(1)}\t {rdr.GetInt32(2)}\t{rdr.GetString(3)} \t{rdr.GetDateTime(4)}");

                            listData.Add(new MarkFile() { MarkId = rdr.GetInt32(0), TagClass = rdr.GetString(1),
                                FileName = rdr.GetString(2), MarkDesc = rdr.GetString(3), MarkDate = rdr.GetDateTime(4)});
                        }
                        return listData;
                    }
                }
            }
        }

        public static List<MarkTime> GetMarkTimes(int markId)
        {
            var cmd = getSqliteCmd();
            List<MarkTime> listData = new List<MarkTime>();
            using (cmd.Connection)
            {
                using (cmd)
                {
                    cmd.CommandText = "select id, FileName, StartTime, EndTime, Desc,MarkDate from MarkTime  where MarkId = @id";
                    cmd.Parameters.AddWithValue("@id", markId);
                    cmd.Prepare();
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {

                        // print column headers with the data from a database table.

                        //Console.WriteLine($"{rdr.GetName(0),-3} {rdr.GetName(1),-8} {rdr.GetName(2),8}");

                        //listData.Add($"{rdr.GetName(0)}\t {rdr.GetName(1)}\t\t\t {rdr.GetName(2)}\t {rdr.GetName(3)} \t {rdr.GetName(4)}");
                        while (rdr.Read())

                        {

                            //Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetInt32(2)} {rdr.GetString(3)}");

                            //listData.Add($"{rdr.GetInt32(0)} \t{rdr.GetString(1)}\t {rdr.GetInt32(2)}\t{rdr.GetString(3)} \t{rdr.GetDateTime(4)}");

                            listData.Add(new MarkTime()
                            {
                                id = rdr.GetInt32(0),
                                FileName = rdr.GetString(1),
                                StartTime = rdr.GetInt64(2),
                                EndTime = rdr.GetInt64(3),
                                Desc = rdr.GetString(4),
                                MarkDate = rdr.GetDateTime(5)
                            });
                        }
                        return listData;
                    }
                }
            }
        }

        public static void CleanMarkInfo(string fileName)
        {
            var cmd = getSqliteCmd();
            using (cmd.Connection)
            {
                using (cmd)
                {
                    cmd.CommandText = "delete from MarkFiles where FileName=@fn;delete from MarkTime  where FileName = @fn";
                    cmd.Parameters.AddWithValue("@fn", fileName);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static List<MarkTime> GetMarkTimesByFileName(string fileName)
        {
            var cmd = getSqliteCmd();
            List<MarkTime> listData = new List<MarkTime>();
            using (cmd.Connection)
            {
                using (cmd)
                {
                    cmd.CommandText = "select id, FileName, StartTime, EndTime, Desc,MarkDate from MarkTime  where FileName = @fileName";
                    cmd.Parameters.AddWithValue("@fileName", fileName);
                    cmd.Prepare();
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            listData.Add(new MarkTime()
                            {
                                id = rdr.GetInt32(0),
                                FileName = rdr.GetString(1),
                                StartTime = rdr.GetInt64(2),
                                EndTime = rdr.GetInt64(3),
                                Desc = rdr.GetString(4),
                                MarkDate = rdr.GetDateTime(5)
                            });
                        }
                        return listData;
                    }
                }
            }
        }
    }

    [Serializable]
    public class MarkFile
    {
        public int MarkId { get; set; }

        public string FileName { get; set; }
        public string TagClass { get; set; }
        public string MarkDesc { get; set; }
        public DateTime MarkDate { get; set; }
    }

    [Serializable]
    public class MarkTime
    {
        public int id { get; set; }
        public string FileName { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public string Desc { get; set; }
        public DateTime MarkDate { get; set; }
    }

    [Serializable]
    public class CopyObj
    { 
        public string FileFullPath { get; set; }
        public MarkFile MarkFile { get; set; }
        public List<MarkTime> MarkTimes { get; set; }
    }

    public static class ClipboardHelper
    {
        public static void SetClipboardData(CopyObj copyObj)
        {
            if (copyObj != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, copyObj);
                    Clipboard.SetDataObject(stream, true);
                }
            }
        }

        public static CopyObj GetClipboardData()
        {
            IDataObject dataObject = Clipboard.GetDataObject();
            if (dataObject != null && dataObject.GetDataPresent(typeof(MemoryStream)))
            {
                using (MemoryStream stream = (MemoryStream)dataObject.GetData(typeof(MemoryStream)))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (CopyObj)formatter.Deserialize(stream);
                }
            }

            return null;
        }
    }
}
