namespace WaveFormRendererApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonLoadSoundFile = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBoxPeakCalculationStrategy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.upDownBlockSize = new System.Windows.Forms.NumericUpDown();
            this.buttonRefreshImage = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.upDownWidth = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.upDownTopHeight = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.upDownBottomHeight = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxDecibels = new System.Windows.Forms.CheckBox();
            this.labelRendering = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonTopColour = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonBottomColour = new System.Windows.Forms.Button();
            this.comboBoxRenderSettings = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonLoadImage = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.subTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbldB = new System.Windows.Forms.Label();
            this.volumeSlider1 = new NAudio.Gui.VolumeSlider();
            this.lblTag = new System.Windows.Forms.Label();
            this.lvMarktimes = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnNewMark = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnMark = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCut = new System.Windows.Forms.Button();
            this.btnArrangeAudio = new System.Windows.Forms.Button();
            this.btnToPre = new System.Windows.Forms.Button();
            this.btnToNext = new System.Windows.Forms.Button();
            this.waveformPainter2 = new NAudio.Gui.WaveformPainter();
            this.waveformPainter1 = new NAudio.Gui.WaveformPainter();
            this.volumeMeter2 = new NAudio.Gui.VolumeMeter();
            this.volumeMeter1 = new NAudio.Gui.VolumeMeter();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.btnPause = new System.Windows.Forms.Button();
            this.lblTimeMsg = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.chkPlayDirect = new System.Windows.Forms.CheckBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.lblSoundsInfo = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMarkAudio = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenAudioFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPast = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownBlockSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownTopHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownBottomHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLoadSoundFile
            // 
            this.buttonLoadSoundFile.Location = new System.Drawing.Point(13, 12);
            this.buttonLoadSoundFile.Name = "buttonLoadSoundFile";
            this.buttonLoadSoundFile.Size = new System.Drawing.Size(75, 21);
            this.buttonLoadSoundFile.TabIndex = 0;
            this.buttonLoadSoundFile.Text = "Load Audio";
            this.buttonLoadSoundFile.UseVisualStyleBackColor = true;
            this.buttonLoadSoundFile.Click += new System.EventHandler(this.OnLoadSoundFileClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(12, 170);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(816, 329);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.Resize += new System.EventHandler(this.pictureBox1_Resize);
            // 
            // comboBoxPeakCalculationStrategy
            // 
            this.comboBoxPeakCalculationStrategy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPeakCalculationStrategy.FormattingEnabled = true;
            this.comboBoxPeakCalculationStrategy.Location = new System.Drawing.Point(446, 71);
            this.comboBoxPeakCalculationStrategy.Name = "comboBoxPeakCalculationStrategy";
            this.comboBoxPeakCalculationStrategy.Size = new System.Drawing.Size(121, 20);
            this.comboBoxPeakCalculationStrategy.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(311, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Peak Calculation Strategy";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(649, 96);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(74, 21);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.OnButtonSaveClick);
            // 
            // upDownBlockSize
            // 
            this.upDownBlockSize.Location = new System.Drawing.Point(240, 40);
            this.upDownBlockSize.Maximum = new decimal(new int[] {
            44100,
            0,
            0,
            0});
            this.upDownBlockSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upDownBlockSize.Name = "upDownBlockSize";
            this.upDownBlockSize.Size = new System.Drawing.Size(60, 21);
            this.upDownBlockSize.TabIndex = 5;
            this.upDownBlockSize.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // buttonRefreshImage
            // 
            this.buttonRefreshImage.Location = new System.Drawing.Point(543, 96);
            this.buttonRefreshImage.Name = "buttonRefreshImage";
            this.buttonRefreshImage.Size = new System.Drawing.Size(100, 21);
            this.buttonRefreshImage.TabIndex = 6;
            this.buttonRefreshImage.Text = "Refresh Image";
            this.buttonRefreshImage.UseVisualStyleBackColor = true;
            this.buttonRefreshImage.Click += new System.EventHandler(this.OnRefreshImageClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Block Size";
            // 
            // upDownWidth
            // 
            this.upDownWidth.Location = new System.Drawing.Point(88, 42);
            this.upDownWidth.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.upDownWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upDownWidth.Name = "upDownWidth";
            this.upDownWidth.Size = new System.Drawing.Size(57, 21);
            this.upDownWidth.TabIndex = 5;
            this.upDownWidth.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Image Width";
            // 
            // upDownTopHeight
            // 
            this.upDownTopHeight.Location = new System.Drawing.Point(245, 71);
            this.upDownTopHeight.Maximum = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.upDownTopHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upDownTopHeight.Name = "upDownTopHeight";
            this.upDownTopHeight.Size = new System.Drawing.Size(56, 21);
            this.upDownTopHeight.TabIndex = 5;
            this.upDownTopHeight.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(160, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Top Height";
            // 
            // upDownBottomHeight
            // 
            this.upDownBottomHeight.Location = new System.Drawing.Point(245, 98);
            this.upDownBottomHeight.Maximum = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.upDownBottomHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upDownBottomHeight.Name = "upDownBottomHeight";
            this.upDownBottomHeight.Size = new System.Drawing.Size(56, 21);
            this.upDownBottomHeight.TabIndex = 5;
            this.upDownBottomHeight.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(160, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "Bottom Height";
            // 
            // checkBoxDecibels
            // 
            this.checkBoxDecibels.AutoSize = true;
            this.checkBoxDecibels.Location = new System.Drawing.Point(572, 74);
            this.checkBoxDecibels.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxDecibels.Name = "checkBoxDecibels";
            this.checkBoxDecibels.Size = new System.Drawing.Size(72, 16);
            this.checkBoxDecibels.TabIndex = 10;
            this.checkBoxDecibels.Text = "Decibels";
            this.checkBoxDecibels.UseVisualStyleBackColor = true;
            this.checkBoxDecibels.CheckedChanged += new System.EventHandler(this.OnDecibelsCheckedChanged);
            // 
            // labelRendering
            // 
            this.labelRendering.AutoSize = true;
            this.labelRendering.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRendering.ForeColor = System.Drawing.Color.Silver;
            this.labelRendering.Location = new System.Drawing.Point(15, 173);
            this.labelRendering.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRendering.Name = "labelRendering";
            this.labelRendering.Size = new System.Drawing.Size(286, 55);
            this.labelRendering.TabIndex = 11;
            this.labelRendering.Text = "Rendering...";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "Top Colour:";
            // 
            // buttonTopColour
            // 
            this.buttonTopColour.Location = new System.Drawing.Point(118, 68);
            this.buttonTopColour.Margin = new System.Windows.Forms.Padding(2);
            this.buttonTopColour.Name = "buttonTopColour";
            this.buttonTopColour.Size = new System.Drawing.Size(28, 21);
            this.buttonTopColour.TabIndex = 9;
            this.buttonTopColour.UseVisualStyleBackColor = true;
            this.buttonTopColour.Click += new System.EventHandler(this.OnColorButtonClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "Bottom Colour:";
            // 
            // buttonBottomColour
            // 
            this.buttonBottomColour.Location = new System.Drawing.Point(118, 95);
            this.buttonBottomColour.Margin = new System.Windows.Forms.Padding(2);
            this.buttonBottomColour.Name = "buttonBottomColour";
            this.buttonBottomColour.Size = new System.Drawing.Size(28, 21);
            this.buttonBottomColour.TabIndex = 9;
            this.buttonBottomColour.UseVisualStyleBackColor = true;
            this.buttonBottomColour.Click += new System.EventHandler(this.OnColorButtonClick);
            // 
            // comboBoxRenderSettings
            // 
            this.comboBoxRenderSettings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRenderSettings.DropDownWidth = 250;
            this.comboBoxRenderSettings.FormattingEnabled = true;
            this.comboBoxRenderSettings.Location = new System.Drawing.Point(446, 41);
            this.comboBoxRenderSettings.Name = "comboBoxRenderSettings";
            this.comboBoxRenderSettings.Size = new System.Drawing.Size(171, 20);
            this.comboBoxRenderSettings.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(331, 44);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "Rendering Style";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(318, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 12);
            this.label10.TabIndex = 3;
            this.label10.Text = "Background Image:";
            // 
            // buttonLoadImage
            // 
            this.buttonLoadImage.Location = new System.Drawing.Point(428, 95);
            this.buttonLoadImage.Name = "buttonLoadImage";
            this.buttonLoadImage.Size = new System.Drawing.Size(100, 21);
            this.buttonLoadImage.TabIndex = 15;
            this.buttonLoadImage.Text = "Load...";
            this.buttonLoadImage.UseVisualStyleBackColor = true;
            this.buttonLoadImage.Click += new System.EventHandler(this.OnButtonLoadImageClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lbldB);
            this.splitContainer1.Panel2.Controls.Add(this.volumeSlider1);
            this.splitContainer1.Panel2.Controls.Add(this.lblTag);
            this.splitContainer1.Panel2.Controls.Add(this.lvMarktimes);
            this.splitContainer1.Panel2.Controls.Add(this.btnNewMark);
            this.splitContainer1.Panel2.Controls.Add(this.btnPreview);
            this.splitContainer1.Panel2.Controls.Add(this.btnMark);
            this.splitContainer1.Panel2.Controls.Add(this.btnEnd);
            this.splitContainer1.Panel2.Controls.Add(this.btnStart);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel2.Controls.Add(this.label11);
            this.splitContainer1.Panel2.Controls.Add(this.btnCut);
            this.splitContainer1.Panel2.Controls.Add(this.btnArrangeAudio);
            this.splitContainer1.Panel2.Controls.Add(this.btnToPre);
            this.splitContainer1.Panel2.Controls.Add(this.btnToNext);
            this.splitContainer1.Panel2.Controls.Add(this.waveformPainter2);
            this.splitContainer1.Panel2.Controls.Add(this.waveformPainter1);
            this.splitContainer1.Panel2.Controls.Add(this.volumeMeter2);
            this.splitContainer1.Panel2.Controls.Add(this.volumeMeter1);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.trackBar1);
            this.splitContainer1.Panel2.Controls.Add(this.btnPause);
            this.splitContainer1.Panel2.Controls.Add(this.lblTimeMsg);
            this.splitContainer1.Panel2.Controls.Add(this.btnStop);
            this.splitContainer1.Panel2.Controls.Add(this.chkPlayDirect);
            this.splitContainer1.Panel2.Controls.Add(this.btnPlay);
            this.splitContainer1.Panel2.Controls.Add(this.lblSoundsInfo);
            this.splitContainer1.Panel2.Controls.Add(this.buttonLoadImage);
            this.splitContainer1.Panel2.Controls.Add(this.labelRendering);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.buttonLoadSoundFile);
            this.splitContainer1.Panel2.Controls.Add(this.comboBoxRenderSettings);
            this.splitContainer1.Panel2.Controls.Add(this.comboBoxPeakCalculationStrategy);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxDecibels);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.buttonBottomColour);
            this.splitContainer1.Panel2.Controls.Add(this.label10);
            this.splitContainer1.Panel2.Controls.Add(this.buttonTopColour);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.buttonSave);
            this.splitContainer1.Panel2.Controls.Add(this.upDownBottomHeight);
            this.splitContainer1.Panel2.Controls.Add(this.upDownBlockSize);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.buttonRefreshImage);
            this.splitContainer1.Panel2.Controls.Add(this.upDownTopHeight);
            this.splitContainer1.Panel2.Controls.Add(this.upDownWidth);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(1370, 631);
            this.splitContainer1.SplitterDistance = 533;
            this.splitContainer1.TabIndex = 16;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.subTag});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(533, 631);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 46;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "时间";
            this.columnHeader2.Width = 129;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "文件";
            this.columnHeader3.Width = 260;
            // 
            // subTag
            // 
            this.subTag.Text = "Tag";
            this.subTag.Width = 100;
            // 
            // lbldB
            // 
            this.lbldB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbldB.AutoSize = true;
            this.lbldB.Location = new System.Drawing.Point(472, 532);
            this.lbldB.Name = "lbldB";
            this.lbldB.Size = new System.Drawing.Size(47, 12);
            this.lbldB.TabIndex = 47;
            this.lbldB.Text = "分贝：0";
            // 
            // volumeSlider1
            // 
            this.volumeSlider1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.volumeSlider1.Location = new System.Drawing.Point(526, 596);
            this.volumeSlider1.Name = "volumeSlider1";
            this.volumeSlider1.Size = new System.Drawing.Size(155, 25);
            this.volumeSlider1.TabIndex = 32;
            // 
            // lblTag
            // 
            this.lblTag.AutoSize = true;
            this.lblTag.Location = new System.Drawing.Point(550, 146);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(47, 12);
            this.lblTag.TabIndex = 46;
            this.lblTag.Text = "TagInfo";
            // 
            // lvMarktimes
            // 
            this.lvMarktimes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lvMarktimes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader7,
            this.columnHeader5,
            this.columnHeader6});
            this.lvMarktimes.FullRowSelect = true;
            this.lvMarktimes.HideSelection = false;
            this.lvMarktimes.Location = new System.Drawing.Point(13, 505);
            this.lvMarktimes.Name = "lvMarktimes";
            this.lvMarktimes.Size = new System.Drawing.Size(445, 114);
            this.lvMarktimes.TabIndex = 39;
            this.lvMarktimes.UseCompatibleStateImageBehavior = false;
            this.lvMarktimes.View = System.Windows.Forms.View.Details;
            this.lvMarktimes.SelectedIndexChanged += new System.EventHandler(this.lvMarktimes_SelectedIndexChanged);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "NO";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Desc";
            this.columnHeader7.Width = 223;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "StartTime";
            this.columnHeader5.Width = 88;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "EndTime";
            this.columnHeader6.Width = 88;
            // 
            // btnNewMark
            // 
            this.btnNewMark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewMark.Location = new System.Drawing.Point(468, 505);
            this.btnNewMark.Name = "btnNewMark";
            this.btnNewMark.Size = new System.Drawing.Size(72, 23);
            this.btnNewMark.TabIndex = 45;
            this.btnNewMark.Text = "New Mark";
            this.btnNewMark.UseVisualStyleBackColor = true;
            this.btnNewMark.Click += new System.EventHandler(this.btnNewMark_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPreview.Location = new System.Drawing.Point(681, 505);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(57, 23);
            this.btnPreview.TabIndex = 44;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnMark
            // 
            this.btnMark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMark.Location = new System.Drawing.Point(747, 505);
            this.btnMark.Name = "btnMark";
            this.btnMark.Size = new System.Drawing.Size(57, 23);
            this.btnMark.TabIndex = 43;
            this.btnMark.Text = "Add";
            this.btnMark.UseVisualStyleBackColor = true;
            this.btnMark.Click += new System.EventHandler(this.btnMark_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEnd.Location = new System.Drawing.Point(615, 505);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(57, 23);
            this.btnEnd.TabIndex = 42;
            this.btnEnd.Text = "End";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStart.Location = new System.Drawing.Point(549, 505);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(57, 23);
            this.btnStart.TabIndex = 41;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 363);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 40;
            this.label11.Text = "Cut Audio List";
            this.label11.Visible = false;
            // 
            // btnCut
            // 
            this.btnCut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCut.Location = new System.Drawing.Point(696, 598);
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(113, 23);
            this.btnCut.TabIndex = 38;
            this.btnCut.Text = "Auto Cut Audio";
            this.btnCut.UseVisualStyleBackColor = true;
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // btnArrangeAudio
            // 
            this.btnArrangeAudio.Location = new System.Drawing.Point(118, 10);
            this.btnArrangeAudio.Name = "btnArrangeAudio";
            this.btnArrangeAudio.Size = new System.Drawing.Size(107, 23);
            this.btnArrangeAudio.TabIndex = 37;
            this.btnArrangeAudio.Text = "Arrange Audio";
            this.btnArrangeAudio.UseVisualStyleBackColor = true;
            this.btnArrangeAudio.Click += new System.EventHandler(this.btnArrangeAudio_Click);
            // 
            // btnToPre
            // 
            this.btnToPre.Location = new System.Drawing.Point(399, 141);
            this.btnToPre.Name = "btnToPre";
            this.btnToPre.Size = new System.Drawing.Size(26, 23);
            this.btnToPre.TabIndex = 36;
            this.btnToPre.Text = "<<";
            this.btnToPre.UseVisualStyleBackColor = true;
            this.btnToPre.Click += new System.EventHandler(this.btnToPre_Click);
            // 
            // btnToNext
            // 
            this.btnToNext.Location = new System.Drawing.Point(432, 141);
            this.btnToNext.Name = "btnToNext";
            this.btnToNext.Size = new System.Drawing.Size(26, 23);
            this.btnToNext.TabIndex = 35;
            this.btnToNext.Text = ">>";
            this.btnToNext.UseVisualStyleBackColor = true;
            this.btnToNext.Click += new System.EventHandler(this.btnToNext_Click);
            // 
            // waveformPainter2
            // 
            this.waveformPainter2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.waveformPainter2.Location = new System.Drawing.Point(526, 598);
            this.waveformPainter2.Name = "waveformPainter2";
            this.waveformPainter2.Size = new System.Drawing.Size(155, 23);
            this.waveformPainter2.TabIndex = 34;
            this.waveformPainter2.Text = "waveformPainter2";
            // 
            // waveformPainter1
            // 
            this.waveformPainter1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.waveformPainter1.Location = new System.Drawing.Point(526, 596);
            this.waveformPainter1.Name = "waveformPainter1";
            this.waveformPainter1.Size = new System.Drawing.Size(155, 23);
            this.waveformPainter1.TabIndex = 33;
            this.waveformPainter1.Text = "waveformPainter1";
            // 
            // volumeMeter2
            // 
            this.volumeMeter2.Amplitude = 1F;
            this.volumeMeter2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.volumeMeter2.Location = new System.Drawing.Point(505, 547);
            this.volumeMeter2.MaxDb = 0F;
            this.volumeMeter2.MinDb = -96F;
            this.volumeMeter2.Name = "volumeMeter2";
            this.volumeMeter2.Size = new System.Drawing.Size(15, 74);
            this.volumeMeter2.TabIndex = 31;
            this.volumeMeter2.Text = "volumeMeter2";
            // 
            // volumeMeter1
            // 
            this.volumeMeter1.Amplitude = 0F;
            this.volumeMeter1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.volumeMeter1.Location = new System.Drawing.Point(484, 547);
            this.volumeMeter1.MaxDb = 0F;
            this.volumeMeter1.MinDb = -96F;
            this.volumeMeter1.Name = "volumeMeter1";
            this.volumeMeter1.Size = new System.Drawing.Size(15, 74);
            this.volumeMeter1.TabIndex = 30;
            this.volumeMeter1.Text = "volumeMeter1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(579, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 29;
            this.label5.Text = "Volume:";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(631, 10);
            this.trackBar1.Maximum = 40;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(199, 45);
            this.trackBar1.TabIndex = 28;
            this.trackBar1.Value = 40;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(267, 141);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(57, 23);
            this.btnPause.TabIndex = 27;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblTimeMsg
            // 
            this.lblTimeMsg.AutoSize = true;
            this.lblTimeMsg.Location = new System.Drawing.Point(15, 152);
            this.lblTimeMsg.Name = "lblTimeMsg";
            this.lblTimeMsg.Size = new System.Drawing.Size(47, 12);
            this.lblTimeMsg.TabIndex = 26;
            this.lblTimeMsg.Text = "process";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(332, 141);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(57, 23);
            this.btnStop.TabIndex = 22;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // chkPlayDirect
            // 
            this.chkPlayDirect.AutoSize = true;
            this.chkPlayDirect.Checked = true;
            this.chkPlayDirect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPlayDirect.Location = new System.Drawing.Point(478, 144);
            this.chkPlayDirect.Name = "chkPlayDirect";
            this.chkPlayDirect.Size = new System.Drawing.Size(66, 16);
            this.chkPlayDirect.TabIndex = 21;
            this.chkPlayDirect.Text = "ToNewer";
            this.chkPlayDirect.UseVisualStyleBackColor = true;
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(200, 141);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(57, 23);
            this.btnPlay.TabIndex = 20;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // lblSoundsInfo
            // 
            this.lblSoundsInfo.AutoSize = true;
            this.lblSoundsInfo.Location = new System.Drawing.Point(11, 127);
            this.lblSoundsInfo.Name = "lblSoundsInfo";
            this.lblSoundsInfo.Size = new System.Drawing.Size(29, 12);
            this.lblSoundsInfo.TabIndex = 16;
            this.lblSoundsInfo.Text = "Info";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMarkAudio,
            this.tsmiOpenAudioFolder,
            this.tsmiCopy,
            this.tsmiPast});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(185, 92);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // tsmiMarkAudio
            // 
            this.tsmiMarkAudio.Name = "tsmiMarkAudio";
            this.tsmiMarkAudio.Size = new System.Drawing.Size(184, 22);
            this.tsmiMarkAudio.Text = "标记音频";
            this.tsmiMarkAudio.Click += new System.EventHandler(this.tsmiMarkAudio_Click);
            // 
            // tsmiOpenAudioFolder
            // 
            this.tsmiOpenAudioFolder.Name = "tsmiOpenAudioFolder";
            this.tsmiOpenAudioFolder.Size = new System.Drawing.Size(184, 22);
            this.tsmiOpenAudioFolder.Text = "打开音频所在文件夹";
            this.tsmiOpenAudioFolder.Click += new System.EventHandler(this.tsmiOpenAudioFolder_Click);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(184, 22);
            this.tsmiCopy.Text = "复制";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // tsmiPast
            // 
            this.tsmiPast.Name = "tsmiPast";
            this.tsmiPast.Size = new System.Drawing.Size(184, 22);
            this.tsmiPast.Text = "粘贴";
            this.tsmiPast.Click += new System.EventHandler(this.tsmiPast_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 631);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "Waveform Renderer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownBlockSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownTopHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownBottomHeight)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonLoadSoundFile;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBoxPeakCalculationStrategy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.NumericUpDown upDownBlockSize;
        private System.Windows.Forms.Button buttonRefreshImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown upDownWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown upDownTopHeight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown upDownBottomHeight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxDecibels;
        private System.Windows.Forms.Label labelRendering;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonTopColour;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonBottomColour;
        private System.Windows.Forms.ComboBox comboBoxRenderSettings;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonLoadImage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label lblSoundsInfo;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.CheckBox chkPlayDirect;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblTimeMsg;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label5;
        private NAudio.Gui.VolumeSlider volumeSlider1;
        private NAudio.Gui.VolumeMeter volumeMeter2;
        private NAudio.Gui.VolumeMeter volumeMeter1;
        private NAudio.Gui.WaveformPainter waveformPainter1;
        private NAudio.Gui.WaveformPainter waveformPainter2;
        private System.Windows.Forms.Button btnToPre;
        private System.Windows.Forms.Button btnToNext;
        private System.Windows.Forms.Button btnArrangeAudio;
        private System.Windows.Forms.Button btnCut;
        private System.Windows.Forms.ListView lvMarktimes;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiMarkAudio;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenAudioFolder;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnMark;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnNewMark;
        private System.Windows.Forms.ColumnHeader subTag;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.Label lbldB;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiPast;
    }
}

