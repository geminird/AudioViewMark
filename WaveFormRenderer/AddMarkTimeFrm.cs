using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WaveFormRendererApp
{
    public partial class AddMarkTimeFrm : Form
    {
        String fileName;
        long start, end;
        MarkTime updateObj = null;
        public AddMarkTimeFrm(string fileName, long start, long end, MarkTime updateMark = null)
        {
            InitializeComponent();
            this.fileName = fileName;
            this.lblFile.Text = fileName;
            this.start = start;
            this.end = end;
            updateObj = updateMark;
            if (updateObj != null)
            {
                this.fileName = updateObj.FileName;
                this.start = updateObj.StartTime;
                this.end = updateObj.EndTime;
                this.txtDesc.Text = updateObj.Desc;
            }
        }

        private void AddMarkFileFrm_Load(object sender, EventArgs e)
        {
        }

        private void btnComit_Click(object sender, EventArgs e)
        {
            if (this.fileName.Trim() != "" && end > start && start >= 0 && this.txtDesc.Text.Trim() != "")
            {
                if (this.updateObj == null && MarkLib.AddAudioMark(fileName, start, end, this.txtDesc.Text))
                { 
                this.DialogResult = DialogResult.OK;
                }
                else if (this.updateObj != null && MarkLib.UpdateAudioMark(updateObj.id, fileName, start, end, this.txtDesc.Text))
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show(" Desc must not null and start,end must is right！");
            }
        }
    }
}
