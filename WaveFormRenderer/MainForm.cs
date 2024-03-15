using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaveFormRendererLib;

namespace WaveFormRendererApp
{
    public partial class MainForm : Form
    {
        private string selectedFile;
        private string imageFile;
        private readonly WaveFormRenderer waveFormRenderer;
        private readonly WaveFormRendererSettings standardSettings;
        private Image currentOrgImg = null;
        private List<FileInfo> files = null;
        private FileSystemWatcher fileSystemWatcher = null;
        private Action<float> setVolumeDelegate;
        private WaveOut waveOutDevice;
        private AudioFileReader audioFileReader;
        private ListViewItem currentPlayItem;
        bool autoPlayNext = false;
        //请设定需默认打开的工作目录。
        private string initDir = @"C:\";
        private static readonly object lockObj = new object();
        private static readonly object fileLockObj = new object();

        public MainForm()
        {
            InitializeComponent();
            if (File.Exists("def")) {
                using (StreamReader sr = File.OpenText("def")) {
                    var path = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(path) == false && Directory.Exists(path)) {
                        initDir = path;
                    }
                }
            }
            waveFormRenderer = new WaveFormRenderer();

            standardSettings = new StandardWaveFormRendererSettings() {Name = "Standard"};
            var soundcloudOriginalSettings = new SoundCloudOriginalSettings() {Name = "SoundCloud Original"};

            var soundCloudLightBlocks = new SoundCloudBlockWaveFormSettings(Color.FromArgb(102, 102, 102), Color.FromArgb(103, 103, 103), Color.FromArgb(179, 179, 179),
                Color.FromArgb(218, 218, 218)) {Name = "SoundCloud Light Blocks"};

            var soundCloudDarkBlocks = new SoundCloudBlockWaveFormSettings(Color.FromArgb(52, 52, 52), Color.FromArgb(55, 55, 55), Color.FromArgb(154, 154, 154),
                Color.FromArgb(204, 204, 204)) {Name = "SoundCloud Darker Blocks"};

            var soundCloudOrangeBlocks = new SoundCloudBlockWaveFormSettings(Color.FromArgb(255, 76, 0), Color.FromArgb(255, 52, 2), Color.FromArgb(255, 171, 141),
                Color.FromArgb(255, 213, 199)) {Name = "SoundCloud Orange Blocks"};

            var topSpacerColor = Color.FromArgb(64, 83, 22, 3);
            var soundCloudOrangeTransparentBlocks = new SoundCloudBlockWaveFormSettings(Color.FromArgb(196, 197, 53, 0), topSpacerColor, Color.FromArgb(196, 79, 26, 0),
                Color.FromArgb(64, 79, 79, 79)) 
                { 
                    Name = "SoundCloud Orange Transparent Blocks", 
                    PixelsPerPeak = 2, 
                    SpacerPixels = 1,
                    TopSpacerGradientStartColor = topSpacerColor,
                    BackgroundColor = Color.Transparent
                };

            var topSpacerColor2 = Color.FromArgb(64, 224, 224, 224);
            var soundCloudGrayTransparentBlocks = new SoundCloudBlockWaveFormSettings(Color.FromArgb(196, 224, 225, 224), topSpacerColor2, Color.FromArgb(196, 128, 128, 128),
                Color.FromArgb(64, 128, 128, 128))
            {
                Name = "SoundCloud Gray Transparent Blocks",
                PixelsPerPeak = 2,
                SpacerPixels = 1,
                TopSpacerGradientStartColor = topSpacerColor2,
                BackgroundColor = Color.Transparent
            };


            buttonBottomColour.BackColor = standardSettings.BottomPeakPen.Color;
            buttonTopColour.BackColor = standardSettings.TopPeakPen.Color;
            comboBoxPeakCalculationStrategy.Items.Add("Max Absolute Value");
            comboBoxPeakCalculationStrategy.Items.Add("Max Rms Value");
            comboBoxPeakCalculationStrategy.Items.Add("Sampled Peaks");
            comboBoxPeakCalculationStrategy.Items.Add("Scaled Average");
            comboBoxPeakCalculationStrategy.SelectedIndex = 0;
            comboBoxPeakCalculationStrategy.SelectedIndexChanged += (sender, args) => RenderWaveform();

            comboBoxRenderSettings.DisplayMember = "Name";

            comboBoxRenderSettings.DataSource = new[]
            {
                standardSettings, 
                soundcloudOriginalSettings, 
                soundCloudLightBlocks, 
                soundCloudDarkBlocks, 
                soundCloudOrangeBlocks, 
                soundCloudOrangeTransparentBlocks,
                soundCloudGrayTransparentBlocks
            };

            comboBoxRenderSettings.SelectedIndex = 0;
            comboBoxRenderSettings.SelectedIndexChanged += (sender, args) => RenderWaveform();

            labelRendering.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.LoadAndWatchDir(initDir);

        }
        #region 波形图部分
        private IPeakProvider GetPeakProvider()
        {
            switch (comboBoxPeakCalculationStrategy.SelectedIndex)
            {
                case 0:
                    return new MaxPeakProvider();
                case 1:
                    return new RmsPeakProvider((int)upDownBlockSize.Value);
                case 2:
                    return new SamplingPeakProvider((int)upDownBlockSize.Value);
                case 3:
                    return new AveragePeakProvider(4);
                default:
                    throw new InvalidOperationException("Unknown calculation strategy");
            }
        }

        private WaveFormRendererSettings GetRendererSettings()
        {
            var settings = (WaveFormRendererSettings) comboBoxRenderSettings.SelectedItem;
            settings.TopHeight = (int)upDownTopHeight.Value;
            settings.BottomHeight = (int)upDownBottomHeight.Value;
            settings.Width = (int)upDownWidth.Value;
            settings.DecibelScale = checkBoxDecibels.Checked;
            return settings;
        }

        private void RenderWaveform()
        {
            if (selectedFile == null) return;
            var settings = GetRendererSettings();
            if (imageFile != null)
            {
                settings.BackgroundImage = new Bitmap(imageFile);
            }
            pictureBox1.Image = null;
            labelRendering.Visible = true;
            Enabled = false;
            var peakProvider = GetPeakProvider();
            Task.Factory.StartNew(() => RenderThreadFunc(peakProvider, settings));
        }

        private void RenderThreadFunc(IPeakProvider peakProvider, WaveFormRendererSettings settings)
        {
            Image image = null;
            List<float> lineList = null;
            //List<PeakInfo> peakInfos = new List<PeakInfo>();
            try
            {
                image = waveFormRenderer.Render(selectedFile, peakProvider, settings, out lineList);
                lock(lockObj)
                {
                    currentOrgImg = image.Clone() as Image;
                }
                //peakInfos = waveFormRenderer.GetPeaks(peakProvider);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                //return;
            }
            BeginInvoke((Action)(() => FinishedRender(image, lineList)));
        }

        private void FinishedRender(Image image, List<float> lineList)
        {
            labelRendering.Visible = false;
            Enabled = true;
            if (image == null)
                return;
            if (this.pictureBox1.Height != image.Height)
                this.pictureBox1.Height = image.Height;
            pictureBox1.Image = image;
            this.lblSoundsInfo.Text = string.Format("{2}:max:{0};min{1}", lineList.Max(), lineList.Min(),
                Path.GetFileName(this.selectedFile));
        }


        private void OnButtonSaveClick(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "PNG files|*.png";
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                pictureBox1.Image.Save(sfd.FileName);
            }
        }

        private void OnRefreshImageClick(object sender, EventArgs e)
        {
            RenderWaveform();
        }

        private void OnColorButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var cd = new ColorDialog();
            cd.Color = button.BackColor;
            if (cd.ShowDialog(this) == DialogResult.OK)
            {
                button.BackColor = cd.Color;

                standardSettings.TopPeakPen = new Pen(buttonTopColour.BackColor);
                standardSettings.BottomPeakPen = new Pen(buttonBottomColour.BackColor);
                RenderWaveform();
            }
        }

        private void OnDecibelsCheckedChanged(object sender, EventArgs e)
        {
            RenderWaveform();
        }

        private void OnButtonLoadImageClick(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.bmp;*.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.imageFile = ofd.FileName;
                RenderWaveform();
            }
        }
        #endregion

        #region 音频播放部分
        private void Play()
        {
            if (selectedFile == null) 
                return;

            if(waveOutDevice != null)
            {
                if (waveOutDevice.PlaybackState == PlaybackState.Paused)
                {
                    waveOutDevice.Resume();
                    return;
                }
                waveOutDevice.Stop();
            }
            Thread th = new Thread(new ThreadStart(() =>
            {
                using (waveOutDevice = new WaveOut())
                {
                    try
                    {

                        waveOutDevice.PlaybackStopped += WaveOutDevice_PlaybackStopped;
                        audioFileReader = new AudioFileReader(selectedFile);
                        ISampleProvider provider = CreateAnalyzeProvider(audioFileReader);
                        waveOutDevice.Init(provider);
                        waveOutDevice.Play();
                        this.maxdB = 0.0f;
                        ThreadPool.QueueUserWorkItem(new WaitCallback((object param) =>
                        {
                            StartRefreshProgress(param as AudioFileReader);
                        }), audioFileReader);
                        while (waveOutDevice.PlaybackState != PlaybackState.Stopped)
                        {
                            Thread.Sleep(1000);
                        }
                        waveOutDevice.PlaybackStopped -= WaveOutDevice_PlaybackStopped;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }));
            th.IsBackground = true;
            th.Start();
        }

        private void StartRefreshProgress(AudioFileReader reader)
        {
            while (waveOutDevice != null && waveOutDevice.PlaybackState != PlaybackState.Stopped)
            {
                double position = reader.CurrentTime.TotalSeconds / reader.TotalTime.TotalSeconds;
                if (reader.CurrentTime == reader.TotalTime)
                    break;
                //获取图片
                if (this.currentOrgImg != null)
                {
                    Image backImg = null;
                    lock (lockObj)
                    {
                        backImg = this.currentOrgImg.Clone() as Image;
                    }
                    int width = backImg.Width;
                    int height = backImg.Height;
                    using (Graphics g = Graphics.FromImage(backImg))
                    {
                        int x = Math.Max((int)Math.Ceiling(width * position) - 2, 0);
                        g.DrawLine(Pens.Black, new Point(x, 0), new Point(x, height));
                    }
                    BeginInvoke((Action)(() => {
                        this.pictureBox1.Image = backImg;
                    }));
                }
                BeginInvoke((Action)(()=> {
                    onPlayingUpdateUI(reader);
                }));
                Thread.Sleep(1000 / 24);
                //reader.CurrentTime = TimeSpan.FromSeconds(reader.TotalTime.TotalSeconds * trackbarvalue / 100.0)
                //long index = waveOutDevice.GetPosition();
            }
        }

        private void onPlayingUpdateUI(AudioFileReader reader)
        {
            this.lblTimeMsg.Text = GetTimeDesc((long)reader.TotalTime.TotalSeconds) + " / " + GetTimeDesc((long)reader.CurrentTime.TotalSeconds);
        }

        private string GetTimeDesc(long seconds)
        {
            //int tHour = (int)(totalSeconds / 60 / 60);
            int hour = (int)(seconds / 60 / 60);
            //int tMinu = (int)(totalSeconds - tHour * 60 * 60);
            int minu = (int)(seconds - hour * 60 * 60) / 60;
            //int tSec = (int)(totalSeconds - tHour * 60 * 60 - tMinu * 60);
            int sec = (int)(seconds - hour * 60 * 60 - minu * 60);

            return string.Format("{0:d2}:{1:d2}:{2:d2}", hour, minu, sec);
            //if (hour > 0)
            //    return string.Format("{0}:{1:d2}:{2:d2}", hour, minu, sec);
            //if (minu > 0)
            //{
            //    if(tHour > 0)
            //    return string.Format("{0}:{1:d2}", minu, sec);
            //}
            //return string.Format("{0}", sec);
        }

        private void SetPosition(double trackbarvalue)
        {
            if (trackbarvalue > 1)
                return;
            audioFileReader.CurrentTime = TimeSpan.FromSeconds(audioFileReader.TotalTime.TotalSeconds * trackbarvalue);
        }
        private void WaveOutDevice_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if(autoPlayNext)
            {
                BeginInvoke((Action)(()=>{
                    this.btnToNext.PerformClick();
                }));
            }
                //this.PlayNext(this.chkPlayDirect.Checked);
            //throw new NotImplementedException();
        }

        #endregion
        #region 文件加载及监控部分
        private void OnLoadSoundFileClick(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.SelectedPath = initDir;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string path = fbd.SelectedPath;
                initDir = path;
                LoadAndWatchDir(path);
                using (StreamWriter fs = File.CreateText("def")) {
                    fs.WriteLine(initDir);
                }
            }
        }

        private void LoadAndWatchDir(string path)
        {
            MarkLib.InitDb(path);
            //自动过滤该目录下的所有文件。
            lock (fileLockObj)
            {
                files = new List<FileInfo>();
                DirectoryInfo di = new DirectoryInfo(path);
                FileInfo[] fileInfos = di.GetFiles("*.*", SearchOption.TopDirectoryOnly).Where(s => s.Name.EndsWith(".3gp") ||
                    s.Name.EndsWith(".mp3") || s.Name.EndsWith("._dw_3gp") || s.Name.EndsWith(".wav")).ToArray();
                files.AddRange(fileInfos);
                SortFileListByTime();
                this.FillList(files);
            }
            if (fileSystemWatcher != null)
            {
                fileSystemWatcher.EnableRaisingEvents = false;
                fileSystemWatcher.Created -= FileSystemWatcher_Created;
                fileSystemWatcher.Deleted -= FileSystemWatcher_Deleted;
                fileSystemWatcher.Dispose();
            }
            fileSystemWatcher = new FileSystemWatcher(path);
            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
            fileSystemWatcher.EnableRaisingEvents = true;

        }

        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath.EndsWith(".mp3") || e.FullPath.EndsWith("._dw_3gp") || e.FullPath.EndsWith(".3gp") || e.FullPath.EndsWith(".wav"))
            {
                FileInfo fileInfo = new FileInfo(e.FullPath);
                lock (fileLockObj)
                {
                    var f = from a in files where a.FullName == fileInfo.FullName select a;
                    if (f != null && f.Count() > 0)
                    {
                        int index = files.IndexOf(f.First());
                        //从文件集中删除
                        files.RemoveAt(index);
                        //从UI中删除
                        this.listView1.BeginUpdate();
                        this.listView1.Items.RemoveAt(index);
                        //更新序号
                        for (int i = index; i < this.listView1.Items.Count; i++)
                        {
                            this.listView1.Items[i].SubItems[0].Text = (i + 1) + "";
                        }
                        this.listView1.EndUpdate();
                    }
                }
            }
            //throw new NotImplementedException();
        }

        private void SortFileListByTime()
        {
            files.Sort((file1, file2) =>
            {
                string time1 = "", time2 = "";
                var a1 = file1.Name.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                int timeIndex = file1.Name.StartsWith("[record]") ? 2 : 3;
                if (a1 != null && a1.Length >= 3)
                {
                    time1 = a1[timeIndex];
                }
                var a2 = file2.Name.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                timeIndex = file2.Name.StartsWith("[record]") ? 2 : 3;
                if (a2 != null && a2.Length >= 3)
                {
                    time2 = a2[timeIndex];
                }
                return time2.CompareTo(time1);
            });
        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            if(e.FullPath.EndsWith(".mp3") || e.FullPath.EndsWith("._dw_3gp") || e.FullPath.EndsWith(".3gp") || e.FullPath.EndsWith(".wav"))
            {
                FileInfo fileInfo = new FileInfo(e.FullPath);
                lock (fileLockObj)
                {
                    if (this.files != null)
                    {
                        files.Add(fileInfo);
                        this.SortFileListByTime();
                        int insIndex = files.IndexOf(fileInfo);
                        //在第index行插入新的
                        BeginInvoke((Action)(() => {
                            this.listView1.BeginUpdate();
                            this.listView1.Items.Insert(insIndex, CreateListViewItem((insIndex + 1), fileInfo));
                            //后面所有的行的序号更新
                            for (int i = (insIndex + 1); i < this.listView1.Items.Count; i++)
                            {
                                this.listView1.Items[i].SubItems[0].Text = (i + 1) + "";
                            }
                            this.listView1.EndUpdate();
                        }));
                    }
                }
            }
        }

        private int GetItemIndex(ListViewItem item)
        {
            ListViewItem[] items = new ListViewItem[this.listView1.Items.Count];
            this.listView1.Items.CopyTo(items, 0);
            var i = from a in items where (a.Tag as FileInfo).FullName == (item.Tag as FileInfo).FullName select a;
            if (i != null && i.Count() > 0)
            {
                return this.listView1.Items.IndexOf(i.First());
            }
            return -1;
        }

        private void FillList(List<FileInfo> fileInfos)
        {
            if (fileInfos != null)
            {
                this.listView1.BeginUpdate();
                this.listView1.Items.Clear();
                int index = 1;
                foreach (FileInfo fi in fileInfos)
                {
                    //[record][05.12.19 15-51][1575532285655].dw_3gp
                    ListViewItem item = CreateListViewItem(index++, fi);
                    if(item != null)
                        this.listView1.Items.Add(item);
                }
                this.listView1.EndUpdate();
            }
        }

        private ListViewItem CreateListViewItem(int index, FileInfo fi)
        {
            var fa = fi.Name.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            if (fa != null && fa.Length >= 3)
            {
                int timeIndex = fi.Name.StartsWith("[record]") ? 2 : 3;
                string stime = fa[timeIndex];
                long time = Convert.ToInt64(stime);
                long beginTicks = new DateTime(1970, 1, 1, 8, 0, 0, DateTimeKind.Utc).Ticks;
                DateTime dateValue = new DateTime(beginTicks + time * 10000);
                ListViewItem item = new ListViewItem(index + "");
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dateValue.ToString("yyyy-MM-dd HH:mm:ss")));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, fi.Name));
                MarkFile mf = MarkLib.GetMarkFileByName(fi.Name);
                if (mf != null)
                {
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, mf.TagClass));
                    this.updateTagInfo(item, mf);
                }
                else
                {
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, ""));
                }
                item.Tag = fi;
                return item;
            }
            else
            {
                ListViewItem item = new ListViewItem(index + "");
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, fi.CreationTime.ToString("yyyy-MM-dd HH:mm:ss")));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, fi.Name));
                MarkFile mf = MarkLib.GetMarkFileByName(fi.Name);
                if (mf != null)
                {
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, mf.TagClass));
                    this.updateTagInfo(item, mf);
                }
                else
                {
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, ""));
                }
                item.Tag = fi;
                return item;
            }
        }

        #endregion

        #region 播放控制部分
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listView1.GetItemAt(e.X, e.Y) != null)
            {
                ListViewItem item = this.listView1.GetItemAt(e.X, e.Y);
                RenderItem(item);
                this.loadMarkInfo(item);
                if (this.currentPlayItem != null && waveOutDevice != null && waveOutDevice.PlaybackState != PlaybackState.Stopped)
                {
                    waveOutDevice.Stop();
                }
            }
        }

        private void RenderItem(ListViewItem item)
        {
            FileInfo fi = item.Tag as FileInfo;
            if (fi != null)
            {
                this.currentPlayItem = item;
                selectedFile = fi.FullName;//ofd.FileName;
                RenderWaveform();
            }
        }

        private void loadMarkInfo(ListViewItem item)
        {
            FileInfo fi = item.Tag as FileInfo;
            if (fi != null)
            {
                MarkFile mf = MarkLib.GetMarkFileByName(fi.Name);
                if (mf != null)
                {
                    //this.lblTag.Text = mf.TagClass + ":" + mf.MarkDesc;
                    ///TODO: 增加标签显示。
                    this.updateTagInfo(item, mf);
                }
                else {
                    this.lblTag.Text = "no tag info.";
                }
                List<MarkTime> marks = MarkLib.GetMarkTimesByFileName(fi.Name);
                this.lvMarktimes.BeginUpdate();
                this.lvMarktimes.Items.Clear();
                if (marks != null && marks.Count > 0)
                {
                    int index = 0;
                    foreach (var i in marks)
                    {
                        ListViewItem it = new ListViewItem(++index + "");
                        it.SubItems.Add(new ListViewItem.ListViewSubItem(it, i.Desc));
                        string startTime = (int)(TimeSpan.FromTicks(i.StartTime).TotalSeconds / 60L) + ":" + (int)(TimeSpan.FromTicks(i.StartTime).TotalSeconds % 60);
                        it.SubItems.Add(new ListViewItem.ListViewSubItem(it, startTime));
                        string endTime = (int)(TimeSpan.FromTicks(i.EndTime).TotalSeconds / 60L) + ":" + (int)(TimeSpan.FromTicks(i.EndTime).TotalSeconds % 60);
                        it.SubItems.Add(new ListViewItem.ListViewSubItem(it, endTime));
                        it.Tag = i;
                        this.lvMarktimes.Items.Add(it);
                    }
                }
                this.lvMarktimes.EndUpdate();
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            autoPlayNext = true;
            this.Play();
        }

        private void PlayNext(bool isNext = true)
        {
            if (currentPlayItem != null)
            {
                while (true)
                {
                    //int index = -1;
                    if (this.InvokeRequired)
                    {
                        bool invoked = false;
                        bool canPlay = false;
                        Invoke((Action)(() =>
                        {
                            int nextIndexOffset = (this.chkPlayDirect.Checked ? -1 : 1) * (isNext ? 1 : -1);
                            int index = this.GetItemIndex(currentPlayItem) + nextIndexOffset;
                            if (index >= 0 && index < this.listView1.Items.Count)
                            {
                                canPlay = true;
                                this.listView1.Items[index].Selected = true;
                                this.RenderItem(this.listView1.Items[index]);
                                this.Play();
                                this.loadMarkInfo(this.currentPlayItem);
                            }
                            //invoked = true;
                        }));
                        //while (invoked == false)
                        //{
                        //    Thread.Sleep(100);
                        //}
                        //如果可以播放，或者不自动播放到下一文件，则跳出循环。
                        if (canPlay || !autoPlayNext)
                        {
                            break;
                        }
                        //如果不符合要求，则等待
                        Thread.Sleep(1000 * 10);
                        continue;
                    }
                    else
                    {
                        int nextIndexOffset = (this.chkPlayDirect.Checked ? -1 : 1) * (isNext ? 1 : -1);
                        int index = this.GetItemIndex(currentPlayItem) + nextIndexOffset;
                        if (index >= 0 && index < this.listView1.Items.Count)
                        {
                            this.listView1.Items[index].Selected = true;
                            this.RenderItem(this.listView1.Items[index]);
                            this.Play();
                            this.loadMarkInfo(this.currentPlayItem);
                            break;
                        }
                        else
                        {
                            if (!autoPlayNext)
                                break;
                            Thread.Sleep(100);
                            Application.DoEvents();
                            continue;
                        }
                    }
                }
                maxdB = 0.0;
            }
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (waveOutDevice != null)
            {
                autoPlayNext = false;
                waveOutDevice.Stop();
            }
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int position = e.X;
            lock (lockObj)
            {
                if (this.currentOrgImg != null)
                {
                    this.SetPosition(((double)e.X / (double)this.currentOrgImg.Width));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.waveOutDevice != null && this.waveOutDevice.PlaybackState == PlaybackState.Playing)
            {
                this.waveOutDevice.Pause();
            }
            else if (this.waveOutDevice != null && this.waveOutDevice.PlaybackState == PlaybackState.Paused)
            {
                this.waveOutDevice.Resume();
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (this.waveOutDevice != null)
            {
                this.waveOutDevice.Volume = (float)this.trackBar1.Value / (float)this.trackBar1.Maximum;
            }
        }

        private void btnToNext_Click(object sender, EventArgs e)
        {
            this.btnStop.PerformClick();
            autoPlayNext = true;
            this.PlayNext(true);
        }

        private void btnToPre_Click(object sender, EventArgs e)
        {
            this.btnStop.PerformClick();
            autoPlayNext = true;
            this.PlayNext(false);
        }
        #endregion

        #region 音频分析部分

        private ISampleProvider CreateAnalyzeProvider(AudioFileReader audioFileReader)
        {
            ISampleProvider sampleProvider;
            try
            {
                sampleProvider = CreateInputStream(audioFileReader);
            }
            catch (Exception createException)
            {
                MessageBox.Show(String.Format("{0}", createException.Message), "Error Loading File");
                return null;
            }
            setVolumeDelegate(volumeSlider1.Volume);
            return sampleProvider;
        }

        private ISampleProvider CreateInputStream(AudioFileReader audioFileReader)
        {
            //audioFileReader = new AudioFileReader(fileName);

            var sampleChannel = new SampleChannel(audioFileReader, true);
            sampleChannel.PreVolumeMeter += OnPreVolumeMeter;
            setVolumeDelegate = vol => sampleChannel.Volume = vol;
            var postVolumeMeter = new MeteringSampleProvider(sampleChannel);
            postVolumeMeter.StreamVolume += OnPostVolumeMeter;

            return postVolumeMeter;
        }

        bool StartOREnd = false;
        List<int> CutInfo = new List<int>();
        double maxdB = 0;
        void OnPreVolumeMeter(object sender, StreamVolumeEventArgs e)
        {
            // we know it is stereo
            waveformPainter1.AddMax(e.MaxSampleValues[0]);
            waveformPainter2.AddMax(e.MaxSampleValues[1]);
            BeginInvoke((Action)(() => {
                var peak = Math.Abs(e.MaxSampleValues[0]);
                if (e.MaxSampleValues.Length > 1)
                {
                    peak = Math.Max(Math.Abs(e.MaxSampleValues[0]), Math.Abs(e.MaxSampleValues[1]));
                }
                float referenceValue = 20e-6f;
                var dB = 20.0f * Math.Log10(peak/referenceValue);
                if (peak > 0)
                {
                    maxdB = Math.Max(maxdB, dB);
                    this.lbldB.Text = "分贝：" + dB + "。当前最大分贝：" + maxdB;
                }
            }));
            //做循环播放。
            if (this.markTime != null && markTime.EndTime > this.markTime.StartTime)
            {
                try
                {
                    if (audioFileReader.CurrentTime >= TimeSpan.FromTicks(markTime.EndTime))
                    {
                        audioFileReader.CurrentTime = TimeSpan.FromTicks(markTime.StartTime);
                        maxdB = 0;
                    }
                }
                catch
                {
                    MessageBox.Show("Are you start paly this audio?");
                }
            }
            return;
            /*
            TimeSpan currentTime = (waveOutDevice.PlaybackState == PlaybackState.Stopped) ? TimeSpan.Zero : audioFileReader.CurrentTime;
            if (e.MaxSampleValues[0] > 0.01 && StartOREnd == false)
            {
                StartOREnd = true;
                DateTime starttime = DateTime.ParseExact(String.Format("{0:00}:{1:00}", (int)currentTime.TotalMinutes,
                    currentTime.Seconds), "mm:ss:fff", null);
                int StartPoint = starttime.Minute * 60 * 1000 + starttime.Second * 1000 + starttime.Millisecond;
                //this.richTextBox1.Text += "\r\n开始：" + StartPoint.ToString();
                CutInfo.Add(StartPoint);
            }
            if (e.MaxSampleValues[0] < 0.01 && StartOREnd == true)
            {
                StartOREnd = false;
                DateTime endtime = DateTime.ParseExact(String.Format("{0:00}:{1:00}", (int)currentTime.TotalMinutes,
                    currentTime.Seconds), "mm:ss:fff", null);
                int EndPoint = endtime.Minute * 60 * 1000 + endtime.Second * 1000 + endtime.Millisecond;
                //this.richTextBox1.Text += "\r\n结束：" + EndPoint.ToString();
                CutInfo.Add(EndPoint);
            }
            */
        }

        void OnPostVolumeMeter(object sender, StreamVolumeEventArgs e)
        {
            // we know it is stereo
            volumeMeter1.Amplitude = e.MaxSampleValues[0];
            volumeMeter2.Amplitude = e.MaxSampleValues[1];

        }


        private void btnCut_Click(object sender, EventArgs e)
        {

        }

        public static void TrimOGGFile(string inputPath, string outputPath, TimeSpan? begin, TimeSpan? end)
        {
            if (begin.HasValue && end.HasValue && begin > end)
                throw new ArgumentOutOfRangeException("end", "end should be greater than begin");

            //using (var reader = new AudioFileReader(inputPath))
            //using (var writer = File.Create(outputPath))
            //{
                
            //    Mp3Frame frame;
            //    while ((frame = reader.ReadNextFrame()) != null)
            //        if (reader.CurrentTime >= begin || !begin.HasValue)
            //        {
            //            if (reader.CurrentTime <= end || !end.HasValue)
            //                writer.Write(frame.RawData, 0, frame.RawData.Length);
            //            else break;
            //        }
            //}
        }

        public static void TrimMp3File(string inputPath, string outputPath, TimeSpan? begin, TimeSpan? end)
        {
            if (begin.HasValue && end.HasValue && begin > end)
                throw new ArgumentOutOfRangeException("end", "end should be greater than begin");

            using (var reader = new Mp3FileReader(inputPath))
            using (var writer = File.Create(outputPath))
            {
                Mp3Frame frame;
                while ((frame = reader.ReadNextFrame()) != null)
                    if (reader.CurrentTime >= begin || !begin.HasValue)
                    {
                        if (reader.CurrentTime <= end || !end.HasValue)
                            writer.Write(frame.RawData, 0, frame.RawData.Length);
                        else break;
                    }
            }
        }
        public static void TrimWavFile(string inPath, string outPath, TimeSpan cutFromStart, TimeSpan cutFromEnd)
        {
            using (WaveFileReader reader = new WaveFileReader(inPath))
            {
                using (WaveFileWriter writer = new WaveFileWriter(outPath, reader.WaveFormat))
                {
                    int bytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000;
                    int startPos = (int)cutFromStart.TotalMilliseconds * bytesPerMillisecond;
                    startPos = startPos - startPos % reader.WaveFormat.BlockAlign;
                    int endPos = (int)cutFromEnd.TotalMilliseconds * bytesPerMillisecond;
                    endPos = endPos - endPos % reader.WaveFormat.BlockAlign;
                    TrimWavFile(reader, writer, startPos, endPos);
                }
            }
        }
        private static void TrimWavFile(WaveFileReader reader, WaveFileWriter writer, int startPos, int endPos)
        {
            reader.Position = startPos;
            byte[] buffer = new byte[1024];
            while (reader.Position < endPos)
            {
                int bytesRequired = (int)(endPos - reader.Position);
                if (bytesRequired > 0)
                {
                    int bytesToRead = Math.Min(bytesRequired, buffer.Length);
                    int bytesRead = reader.Read(buffer, 0, bytesToRead);
                    if (bytesRead > 0)
                    {
                        writer.WriteData(buffer, 0, bytesRead);
                    }
                }
            }
        }
        #endregion

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        #region 文件整理
        private void btnArrangeAudio_Click(object sender, EventArgs e)
        {
            LoadAndWatchDir(this.initDir);
        }
        #endregion

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            this.upDownTopHeight.Value = this.pictureBox1.Height - this.upDownBottomHeight.Value;
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void tsmiMarkAudio_Click(object sender, EventArgs e)
        {
            FileInfo fi = (this.contextMenuStrip1.Tag as ListViewItem).Tag as FileInfo;
            if (fi != null) 
            {
                MarkFile mf = new MarkFile();
                AddMarkFileFrm frm = new AddMarkFileFrm(fi.Name, mf);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //(this.contextMenuStrip1.Tag as ListViewItem).SubItems[3].Text = mf.TagClass;
                    //this.lblTag.Text = mf.TagClass + ":" + mf.MarkDesc;
                    this.updateTagInfo(this.contextMenuStrip1.Tag as ListViewItem, mf);
                }
            }
        }

        private void updateTagInfo(ListViewItem item, MarkFile mf)
        {
            if (item != null && mf != null)
            {
                int count = MarkLib.GetMarkTimesByFileName(mf.FileName).Count;
                item.SubItems[3].Text = mf.TagClass + ":" + count;
                this.lblTag.Text = mf.TagClass + ":" + mf.MarkDesc;
            }
        }

        private void tsmiOpenAudioFolder_Click(object sender, EventArgs e)
        {
            FileInfo fi = (this.contextMenuStrip1.Tag as ListViewItem).Tag as FileInfo;
            if (fi != null)
            {
                string filePath = fi.FullName; // 替换为你想要打开的文件路径

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "explorer.exe";
                startInfo.Arguments = "/select," + filePath;

                try
                {
                    Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("无法打开文件所在的文件夹：" + ex.Message);
                }
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.tsmiPast.Enabled = true;
                CopyObj copyObj = ClipboardHelper.GetClipboardData();
                if (copyObj == null)
                    this.tsmiPast.Enabled = false;
                if (copyObj != null)
                {
                    FileInfo cfi = new FileInfo(copyObj.FileFullPath);
                    if (cfi.Directory.FullName.ToLower().Trim() == this.initDir.ToLower().Trim())
                    {
                        this.tsmiPast.Enabled = false;
                    }
                }

                if (this.listView1.GetItemAt(e.X, e.Y) != null)
                {
                    ListViewItem item = this.listView1.GetItemAt(e.X, e.Y);
                    FileInfo fi = item.Tag as FileInfo;
                    if (fi != null)
                    {
                        this.tsmiCopy.Visible = this.tsmiMarkAudio.Visible = this.tsmiOpenAudioFolder.Visible = true;
                        this.contextMenuStrip1.Tag = item;
                        this.contextMenuStrip1.Show(this.listView1, e.X, e.Y);
                    }
                }
                else if (this.tsmiPast.Enabled)
                {
                    this.tsmiCopy.Visible = this.tsmiMarkAudio.Visible = this.tsmiOpenAudioFolder.Visible = false;
                    this.contextMenuStrip1.Show(this.listView1, e.X, e.Y);
                }
            }
        }

        MarkTime markTime = null;
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (markTime != null)
            {
                markTime.StartTime = audioFileReader.CurrentTime.Ticks;
                var div = TimeSpan.FromTicks(markTime.StartTime).Seconds / audioFileReader.TotalTime.TotalSeconds;
                double startPt = this.pictureBox1.Width * div;
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (markTime != null)
                markTime.EndTime = audioFileReader.CurrentTime.Ticks;
        }

        private void btnNewMark_Click(object sender, EventArgs e)
        {
            if ((this.currentPlayItem.Tag as FileInfo) != null)
            {
                markTime = new MarkTime();
                markTime.FileName = (this.currentPlayItem.Tag as FileInfo).Name;
                this.btnMark.Text = "Add";
            }
            else
            {
                MessageBox.Show("Must select audio!");
            }
        }

        private void btnMark_Click(object sender, EventArgs e)
        {
            if (markTime != null)
            {
                AddMarkTimeFrm frm = new AddMarkTimeFrm(markTime.FileName, markTime.StartTime, markTime.EndTime, this.markTime.id > 0 ? this.markTime : null);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (this.markTime.id <= 0)
                    {
                        this.markTime = null;
                    }
                    this.loadMarkInfo(this.currentPlayItem);
                }
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (this.markTime != null && this.markTime.StartTime >= 0)
            {
                try
                {
                    if (waveOutDevice == null || waveOutDevice.PlaybackState != PlaybackState.Playing)
                    {
                        this.btnPlay.PerformClick();
                        Application.DoEvents();
                        Thread.Sleep(200);
                    }
                    audioFileReader.CurrentTime = TimeSpan.FromTicks(markTime.StartTime);
                    maxdB = 0;
                }
                catch
                {
                    MessageBox.Show("Are you start paly this audio?");
                }
            }
        }

        private void lvMarktimes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvMarktimes.SelectedItems.Count > 0)
            {
                this.markTime = this.lvMarktimes.SelectedItems[0].Tag as MarkTime;
                this.btnMark.Text = "Update";
            }
        }

        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            FileInfo fi = (this.contextMenuStrip1.Tag as ListViewItem).Tag as FileInfo;
            if (fi != null)
            {
                string filePath = fi.FullName; // 替换为你想要打开的文件路径
                if (!File.Exists(filePath))
                    return;
                CopyObj copyObj = new CopyObj();
                copyObj.FileFullPath = filePath;
                MarkFile mf = MarkLib.GetMarkFileByName(fi.Name);
                if (mf != null)
                {
                    copyObj.MarkFile = mf;
                    List<MarkTime> mts = MarkLib.GetMarkTimesByFileName(fi.Name);
                    if (mts != null && mts.Count() > 0)
                    {
                        copyObj.MarkTimes = mts;
                    }
                }
                ClipboardHelper.SetClipboardData(copyObj);
            }
        }

        private void tsmiPast_Click(object sender, EventArgs e)
        {
            CopyObj copyObj = ClipboardHelper.GetClipboardData();
            if (copyObj != null)
            {
                FileInfo fi = new FileInfo(copyObj.FileFullPath);
                if (fi.Directory.FullName.ToLower().Trim() == this.initDir.ToLower().Trim())
                {
                    MessageBox.Show("请不要在同一个目录中复制粘贴！");
                    return;
                }
                else 
                {
                    string destFile = Path.Combine(this.initDir, fi.Name);
                    if(File.Exists(destFile))
                    {
                        if (MessageBox.Show(destFile + System.Environment.NewLine + "文件已经存在，是否覆盖？", "文件已存在", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                            == DialogResult.Cancel)
                            return;
                        else
                            fi.CopyTo(destFile, true);
                    }
                    else
                        fi.CopyTo(destFile);
                    if (copyObj.MarkFile != null)
                    {
                        MarkLib.CleanMarkInfo(fi.Name);
                        MarkLib.AddFileMark(fi.Name, copyObj.MarkFile.TagClass, copyObj.MarkFile.MarkDesc);
                    }
                    if (copyObj.MarkTimes != null && copyObj.MarkTimes.Count() > 0)
                    {
                        foreach (MarkTime mt in copyObj.MarkTimes)
                        {
                            MarkLib.AddAudioMark(fi.Name, mt.StartTime, mt.EndTime, mt.Desc);
                        }
                    }
                }
            }
        }
    }
}
