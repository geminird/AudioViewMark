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
    public partial class AddMarkFileFrm : Form
    {
        String fileName;

        private MarkFile mf = null;
        
        public AddMarkFileFrm(string fileName, MarkFile mf)
        {
            InitializeComponent();
            this.fileName = fileName;
            this.mf = mf;
        }

        private void AddMarkFileFrm_Load(object sender, EventArgs e)
        {
            this.cbTag.Items.AddRange(MarkLib.Tags.ToArray());
            MarkFile mfile = MarkLib.GetMarkFileByName(this.fileName);
            if (mfile != null)
            {
                this.cbTag.Text = mfile.TagClass;
                this.txtDesc.Text = mfile.MarkDesc;
            }
        }

        private void btnComit_Click(object sender, EventArgs e)
        {
            if (this.cbTag.Text.Trim() != "" && this.txtDesc.Text.Trim() != "")
            {
                if(MarkLib.AddFileMark(fileName, this.cbTag.Text, this.txtDesc.Text))
                {

                    mf.MarkDesc = this.txtDesc.Text;
                    mf.TagClass = this.cbTag.Text;
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show("Class & Desc must not null！");
            }
        }
    }
}
