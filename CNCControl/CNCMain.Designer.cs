namespace CNCControl
{
    partial class CNCMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CNCMain));
            this.txtResults = new System.Windows.Forms.TextBox();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TimerStatusUpdate = new System.Windows.Forms.Timer(this.components);
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbUpdate = new System.Windows.Forms.TrackBar();
            this.txtMemoryFree = new System.Windows.Forms.TextBox();
            this.displayE = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.displayZ = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.label9 = new System.Windows.Forms.Label();
            this.displayY = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.label8 = new System.Windows.Forms.Label();
            this.displayX = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.txtLaserTemp = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.label7 = new System.Windows.Forms.Label();
            this.cbUpdate = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cbPort = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btRefreshPort = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.jcZeroAxes = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.txtComReceive = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpImage = new System.Windows.Forms.TabPage();
            this.txtRes = new System.Windows.Forms.TextBox();
            this.lblImageAction = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblBrightness = new System.Windows.Forms.Label();
            this.lblContrast = new System.Windows.Forms.Label();
            this.lblGamma = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbBrightness = new System.Windows.Forms.TrackBar();
            this.tbGamma = new System.Windows.Forms.TrackBar();
            this.tbContrast = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFeedRate = new System.Windows.Forms.TextBox();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtImgSizeY = new System.Windows.Forms.TextBox();
            this.txtImgSizeX = new System.Windows.Forms.TextBox();
            this.txtImgSizePixelY = new System.Windows.Forms.TextBox();
            this.txtImgSizePixelX = new System.Windows.Forms.TextBox();
            this.txtGCodePreview = new System.Windows.Forms.TextBox();
            this.cbDPI = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button18 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.tpInterface = new System.Windows.Forms.TabPage();
            this.dgvGcode = new System.Windows.Forms.DataGridView();
            this.stat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lineNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button3 = new System.Windows.Forms.Button();
            this.fpJog = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.cbStepSize = new System.Windows.Forms.ComboBox();
            this.tpFile = new System.Windows.Forms.TabPage();
            this.tpSetting = new System.Windows.Forms.TabPage();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.button2 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbUpdate)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpImage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGamma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tpInterface.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGcode)).BeginInit();
            this.fpJog.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtResults
            // 
            this.txtResults.Location = new System.Drawing.Point(493, 134);
            this.txtResults.MaxLength = 0;
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResults.Size = new System.Drawing.Size(177, 71);
            this.txtResults.TabIndex = 2;
            this.txtResults.WordWrap = false;
            this.txtResults.TextChanged += new System.EventHandler(this.txtResults_TextChanged);
            // 
            // txtCommand
            // 
            this.txtCommand.Location = new System.Drawing.Point(3, 557);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(484, 20);
            this.txtCommand.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(497, 557);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(52, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 29);
            this.label2.TabIndex = 6;
            this.label2.Text = "Laser Temp.";
            // 
            // TimerStatusUpdate
            // 
            this.TimerStatusUpdate.Interval = 500;
            this.TimerStatusUpdate.Tick += new System.EventHandler(this.updateStatus_Tick);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(493, 64);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 23;
            this.button6.Text = "clear";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.tbUpdate);
            this.groupBox1.Controls.Add(this.txtMemoryFree);
            this.groupBox1.Controls.Add(this.displayE);
            this.groupBox1.Controls.Add(this.displayZ);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.displayY);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.displayX);
            this.groupBox1.Controls.Add(this.txtLaserTemp);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbUpdate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(926, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 396);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Position";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // tbUpdate
            // 
            this.tbUpdate.LargeChange = 1000;
            this.tbUpdate.Location = new System.Drawing.Point(50, 323);
            this.tbUpdate.Maximum = 10000;
            this.tbUpdate.Minimum = 200;
            this.tbUpdate.Name = "tbUpdate";
            this.tbUpdate.Size = new System.Drawing.Size(155, 45);
            this.tbUpdate.SmallChange = 500;
            this.tbUpdate.TabIndex = 36;
            this.tbUpdate.TickFrequency = 500;
            this.tbUpdate.Value = 200;
            this.tbUpdate.Scroll += new System.EventHandler(this.tbUpdate_Scroll);
            this.tbUpdate.ValueChanged += new System.EventHandler(this.tbUpdate_ValueChanged);
            // 
            // txtMemoryFree
            // 
            this.txtMemoryFree.Location = new System.Drawing.Point(49, 265);
            this.txtMemoryFree.Name = "txtMemoryFree";
            this.txtMemoryFree.Size = new System.Drawing.Size(155, 29);
            this.txtMemoryFree.TabIndex = 35;
            // 
            // displayE
            // 
            this.displayE.ArrayCount = 6;
            this.displayE.ColorBackground = System.Drawing.Color.Black;
            this.displayE.ColorDark = System.Drawing.Color.Green;
            this.displayE.ColorLight = System.Drawing.Color.Lime;
            this.displayE.DecimalShow = true;
            this.displayE.ElementPadding = new System.Windows.Forms.Padding(4);
            this.displayE.ElementWidth = 10;
            this.displayE.ItalicFactor = 0F;
            this.displayE.Location = new System.Drawing.Point(49, 157);
            this.displayE.Name = "displayE";
            this.displayE.Size = new System.Drawing.Size(156, 36);
            this.displayE.TabIndex = 3;
            this.displayE.TabStop = false;
            this.displayE.Value = "0000.00";
            // 
            // displayZ
            // 
            this.displayZ.ArrayCount = 6;
            this.displayZ.ColorBackground = System.Drawing.Color.Black;
            this.displayZ.ColorDark = System.Drawing.Color.Green;
            this.displayZ.ColorLight = System.Drawing.Color.Lime;
            this.displayZ.DecimalShow = true;
            this.displayZ.ElementPadding = new System.Windows.Forms.Padding(4);
            this.displayZ.ElementWidth = 10;
            this.displayZ.ItalicFactor = 0F;
            this.displayZ.Location = new System.Drawing.Point(49, 114);
            this.displayZ.Name = "displayZ";
            this.displayZ.Size = new System.Drawing.Size(156, 36);
            this.displayZ.TabIndex = 2;
            this.displayZ.TabStop = false;
            this.displayZ.Value = "0000.00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 169);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 24);
            this.label9.TabIndex = 19;
            this.label9.Text = "E";
            // 
            // displayY
            // 
            this.displayY.ArrayCount = 6;
            this.displayY.ColorBackground = System.Drawing.Color.Black;
            this.displayY.ColorDark = System.Drawing.Color.Green;
            this.displayY.ColorLight = System.Drawing.Color.Lime;
            this.displayY.DecimalShow = true;
            this.displayY.ElementPadding = new System.Windows.Forms.Padding(4);
            this.displayY.ElementWidth = 10;
            this.displayY.ItalicFactor = 0F;
            this.displayY.Location = new System.Drawing.Point(49, 71);
            this.displayY.Name = "displayY";
            this.displayY.Size = new System.Drawing.Size(156, 36);
            this.displayY.TabIndex = 1;
            this.displayY.TabStop = false;
            this.displayY.Value = "0000.00";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 24);
            this.label8.TabIndex = 18;
            this.label8.Text = "Z";
            // 
            // displayX
            // 
            this.displayX.ArrayCount = 6;
            this.displayX.ColorBackground = System.Drawing.Color.Black;
            this.displayX.ColorDark = System.Drawing.Color.Green;
            this.displayX.ColorLight = System.Drawing.Color.Lime;
            this.displayX.DecimalShow = true;
            this.displayX.ElementPadding = new System.Windows.Forms.Padding(4);
            this.displayX.ElementWidth = 10;
            this.displayX.ItalicFactor = 0F;
            this.displayX.Location = new System.Drawing.Point(49, 28);
            this.displayX.Name = "displayX";
            this.displayX.Size = new System.Drawing.Size(156, 36);
            this.displayX.TabIndex = 0;
            this.displayX.TabStop = false;
            this.displayX.Value = "0000.00";
            // 
            // txtLaserTemp
            // 
            this.txtLaserTemp.ArrayCount = 5;
            this.txtLaserTemp.ColorBackground = System.Drawing.Color.Black;
            this.txtLaserTemp.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtLaserTemp.ColorLight = System.Drawing.Color.Red;
            this.txtLaserTemp.DecimalShow = true;
            this.txtLaserTemp.ElementPadding = new System.Windows.Forms.Padding(4);
            this.txtLaserTemp.ElementWidth = 10;
            this.txtLaserTemp.ItalicFactor = 0F;
            this.txtLaserTemp.Location = new System.Drawing.Point(49, 228);
            this.txtLaserTemp.Name = "txtLaserTemp";
            this.txtLaserTemp.Size = new System.Drawing.Size(109, 31);
            this.txtLaserTemp.TabIndex = 34;
            this.txtLaserTemp.TabStop = false;
            this.txtLaserTemp.Value = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 24);
            this.label7.TabIndex = 17;
            this.label7.Text = "Y";
            // 
            // cbUpdate
            // 
            this.cbUpdate.AutoSize = true;
            this.cbUpdate.Checked = true;
            this.cbUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUpdate.Location = new System.Drawing.Point(49, 300);
            this.cbUpdate.Name = "cbUpdate";
            this.cbUpdate.Size = new System.Drawing.Size(94, 17);
            this.cbUpdate.TabIndex = 29;
            this.cbUpdate.Text = "Status Update";
            this.cbUpdate.UseVisualStyleBackColor = true;
            this.cbUpdate.CheckedChanged += new System.EventHandler(this.cbUpdate_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 24);
            this.label1.TabIndex = 16;
            this.label1.Text = "X";
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Location = new System.Drawing.Point(0, 826);
            this.statusStrip1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1184, 35);
            this.statusStrip1.TabIndex = 33;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // cbPort
            // 
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(56, 22);
            this.toolStripButton1.Text = "Connect";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(57, 22);
            this.toolStripButton2.Text = "EEPROM";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbPort,
            this.btRefreshPort,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1184, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btRefreshPort
            // 
            this.btRefreshPort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btRefreshPort.Image = ((System.Drawing.Image)(resources.GetObject("btRefreshPort.Image")));
            this.btRefreshPort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRefreshPort.Name = "btRefreshPort";
            this.btRefreshPort.Size = new System.Drawing.Size(50, 22);
            this.btRefreshPort.Text = "Refresh";
            this.btRefreshPort.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btRefreshPort.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(96, 22);
            this.toolStripButton3.Text = "Disable EndStop";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click_1);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(51, 22);
            this.toolStripButton4.Text = "Get Pos";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(88, 100);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 112);
            this.button4.TabIndex = 36;
            this.button4.Text = "X-";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(170, 56);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(138, 40);
            this.button5.TabIndex = 37;
            this.button5.Text = "Y-";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(170, 211);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(138, 39);
            this.button7.TabIndex = 38;
            this.button7.Text = "Y+";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(314, 100);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 112);
            this.button8.TabIndex = 39;
            this.button8.Text = "X+";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.jcXPlus_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(170, 13);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(138, 37);
            this.button10.TabIndex = 41;
            this.button10.Text = "button10";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(170, 257);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(138, 37);
            this.button11.TabIndex = 42;
            this.button11.Text = "button11";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // jcZeroAxes
            // 
            this.jcZeroAxes.Location = new System.Drawing.Point(189, 113);
            this.jcZeroAxes.Name = "jcZeroAxes";
            this.jcZeroAxes.Size = new System.Drawing.Size(97, 83);
            this.jcZeroAxes.TabIndex = 43;
            this.jcZeroAxes.Text = "Zero all axes";
            this.jcZeroAxes.UseVisualStyleBackColor = true;
            this.jcZeroAxes.Click += new System.EventHandler(this.jcZeroAxes_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(574, 64);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(75, 23);
            this.button13.TabIndex = 44;
            this.button13.Text = "Send Lines";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(655, 64);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(75, 23);
            this.button14.TabIndex = 45;
            this.button14.Text = "RUN";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(736, 64);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(75, 23);
            this.button15.TabIndex = 46;
            this.button15.Text = "CANCEL";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // txtComReceive
            // 
            this.txtComReceive.Location = new System.Drawing.Point(493, 92);
            this.txtComReceive.MaxLength = 0;
            this.txtComReceive.Multiline = true;
            this.txtComReceive.Name = "txtComReceive";
            this.txtComReceive.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtComReceive.Size = new System.Drawing.Size(395, 22);
            this.txtComReceive.TabIndex = 47;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpImage);
            this.tabControl1.Controls.Add(this.tpInterface);
            this.tabControl1.Controls.Add(this.fpJog);
            this.tabControl1.Controls.Add(this.tpFile);
            this.tabControl1.Controls.Add(this.tpSetting);
            this.tabControl1.Location = new System.Drawing.Point(10, 37);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1160, 783);
            this.tabControl1.TabIndex = 48;
            // 
            // tpImage
            // 
            this.tpImage.Controls.Add(this.button20);
            this.tpImage.Controls.Add(this.button19);
            this.tpImage.Controls.Add(this.button12);
            this.tpImage.Controls.Add(this.button9);
            this.tpImage.Controls.Add(this.button2);
            this.tpImage.Controls.Add(this.txtRes);
            this.tpImage.Controls.Add(this.lblImageAction);
            this.tpImage.Controls.Add(this.groupBox2);
            this.tpImage.Controls.Add(this.label6);
            this.tpImage.Controls.Add(this.txtFeedRate);
            this.tpImage.Controls.Add(this.cbMode);
            this.tpImage.Controls.Add(this.label5);
            this.tpImage.Controls.Add(this.txtImgSizeY);
            this.tpImage.Controls.Add(this.txtImgSizeX);
            this.tpImage.Controls.Add(this.txtImgSizePixelY);
            this.tpImage.Controls.Add(this.txtImgSizePixelX);
            this.tpImage.Controls.Add(this.txtGCodePreview);
            this.tpImage.Controls.Add(this.cbDPI);
            this.tpImage.Controls.Add(this.pictureBox1);
            this.tpImage.Controls.Add(this.button18);
            this.tpImage.Controls.Add(this.button17);
            this.tpImage.Controls.Add(this.button16);
            this.tpImage.Location = new System.Drawing.Point(4, 22);
            this.tpImage.Name = "tpImage";
            this.tpImage.Padding = new System.Windows.Forms.Padding(3);
            this.tpImage.Size = new System.Drawing.Size(1152, 757);
            this.tpImage.TabIndex = 1;
            this.tpImage.UseVisualStyleBackColor = true;
            // 
            // txtRes
            // 
            this.txtRes.Location = new System.Drawing.Point(851, 514);
            this.txtRes.Name = "txtRes";
            this.txtRes.Size = new System.Drawing.Size(100, 20);
            this.txtRes.TabIndex = 17;
            this.txtRes.Text = "0.17";
            // 
            // lblImageAction
            // 
            this.lblImageAction.AutoSize = true;
            this.lblImageAction.Location = new System.Drawing.Point(786, 11);
            this.lblImageAction.Name = "lblImageAction";
            this.lblImageAction.Size = new System.Drawing.Size(0, 13);
            this.lblImageAction.TabIndex = 16;
            this.lblImageAction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblBrightness);
            this.groupBox2.Controls.Add(this.lblContrast);
            this.groupBox2.Controls.Add(this.lblGamma);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.tbBrightness);
            this.groupBox2.Controls.Add(this.tbGamma);
            this.groupBox2.Controls.Add(this.tbContrast);
            this.groupBox2.Location = new System.Drawing.Point(804, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(342, 220);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            // 
            // lblBrightness
            // 
            this.lblBrightness.AutoSize = true;
            this.lblBrightness.Location = new System.Drawing.Point(151, 16);
            this.lblBrightness.MinimumSize = new System.Drawing.Size(31, 13);
            this.lblBrightness.Name = "lblBrightness";
            this.lblBrightness.Size = new System.Drawing.Size(31, 13);
            this.lblBrightness.TabIndex = 8;
            this.lblBrightness.Text = "0000";
            this.lblBrightness.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblBrightness.DoubleClick += new System.EventHandler(this.lblBrightness_DoubleClick);
            // 
            // lblContrast
            // 
            this.lblContrast.AutoSize = true;
            this.lblContrast.Location = new System.Drawing.Point(151, 80);
            this.lblContrast.MinimumSize = new System.Drawing.Size(31, 13);
            this.lblContrast.Name = "lblContrast";
            this.lblContrast.Size = new System.Drawing.Size(31, 13);
            this.lblContrast.TabIndex = 7;
            this.lblContrast.Text = "0000";
            this.lblContrast.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblContrast.DoubleClick += new System.EventHandler(this.lblContrast_DoubleClick);
            // 
            // lblGamma
            // 
            this.lblGamma.AutoSize = true;
            this.lblGamma.Location = new System.Drawing.Point(151, 148);
            this.lblGamma.MinimumSize = new System.Drawing.Size(31, 13);
            this.lblGamma.Name = "lblGamma";
            this.lblGamma.Size = new System.Drawing.Size(31, 13);
            this.lblGamma.TabIndex = 6;
            this.lblGamma.Text = "1";
            this.lblGamma.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblGamma.DoubleClick += new System.EventHandler(this.lblGamma_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Gamma";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Contrast";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Brightness";
            // 
            // tbBrightness
            // 
            this.tbBrightness.Location = new System.Drawing.Point(6, 32);
            this.tbBrightness.Maximum = 127;
            this.tbBrightness.Minimum = -127;
            this.tbBrightness.Name = "tbBrightness";
            this.tbBrightness.Size = new System.Drawing.Size(330, 45);
            this.tbBrightness.TabIndex = 0;
            this.tbBrightness.TickFrequency = 10;
            this.tbBrightness.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbBrightness.ValueChanged += new System.EventHandler(this.tbBrightness_ValueChanged);
            // 
            // tbGamma
            // 
            this.tbGamma.Location = new System.Drawing.Point(6, 164);
            this.tbGamma.Maximum = 500;
            this.tbGamma.Name = "tbGamma";
            this.tbGamma.Size = new System.Drawing.Size(330, 45);
            this.tbGamma.TabIndex = 2;
            this.tbGamma.TickFrequency = 22;
            this.tbGamma.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbGamma.Value = 100;
            this.tbGamma.ValueChanged += new System.EventHandler(this.tbGamma_ValueChanged);
            // 
            // tbContrast
            // 
            this.tbContrast.Location = new System.Drawing.Point(6, 96);
            this.tbContrast.Maximum = 127;
            this.tbContrast.Minimum = -127;
            this.tbContrast.Name = "tbContrast";
            this.tbContrast.Size = new System.Drawing.Size(330, 45);
            this.tbContrast.TabIndex = 1;
            this.tbContrast.TickFrequency = 10;
            this.tbContrast.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbContrast.ValueChanged += new System.EventHandler(this.tbContrast_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(958, 611);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Feedrate";
            // 
            // txtFeedRate
            // 
            this.txtFeedRate.Location = new System.Drawing.Point(958, 630);
            this.txtFeedRate.Name = "txtFeedRate";
            this.txtFeedRate.Size = new System.Drawing.Size(123, 20);
            this.txtFeedRate.TabIndex = 13;
            this.txtFeedRate.Text = "2500";
            // 
            // cbMode
            // 
            this.cbMode.FormattingEnabled = true;
            this.cbMode.Items.AddRange(new object[] {
            "Mode 1",
            "Mode 2"});
            this.cbMode.Location = new System.Drawing.Point(957, 587);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(124, 21);
            this.cbMode.TabIndex = 12;
            this.cbMode.Text = "Mode 1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(949, 436);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "DPI";
            // 
            // txtImgSizeY
            // 
            this.txtImgSizeY.Location = new System.Drawing.Point(915, 487);
            this.txtImgSizeY.Name = "txtImgSizeY";
            this.txtImgSizeY.Size = new System.Drawing.Size(59, 20);
            this.txtImgSizeY.TabIndex = 10;
            // 
            // txtImgSizeX
            // 
            this.txtImgSizeX.Location = new System.Drawing.Point(850, 487);
            this.txtImgSizeX.Name = "txtImgSizeX";
            this.txtImgSizeX.Size = new System.Drawing.Size(59, 20);
            this.txtImgSizeX.TabIndex = 9;
            // 
            // txtImgSizePixelY
            // 
            this.txtImgSizePixelY.Location = new System.Drawing.Point(915, 460);
            this.txtImgSizePixelY.Name = "txtImgSizePixelY";
            this.txtImgSizePixelY.Size = new System.Drawing.Size(59, 20);
            this.txtImgSizePixelY.TabIndex = 8;
            // 
            // txtImgSizePixelX
            // 
            this.txtImgSizePixelX.Location = new System.Drawing.Point(850, 460);
            this.txtImgSizePixelX.Name = "txtImgSizePixelX";
            this.txtImgSizePixelX.Size = new System.Drawing.Size(59, 20);
            this.txtImgSizePixelX.TabIndex = 7;
            // 
            // txtGCodePreview
            // 
            this.txtGCodePreview.Location = new System.Drawing.Point(980, 404);
            this.txtGCodePreview.MaxLength = 0;
            this.txtGCodePreview.Multiline = true;
            this.txtGCodePreview.Name = "txtGCodePreview";
            this.txtGCodePreview.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtGCodePreview.Size = new System.Drawing.Size(101, 157);
            this.txtGCodePreview.TabIndex = 6;
            this.txtGCodePreview.Visible = false;
            // 
            // cbDPI
            // 
            this.cbDPI.FormattingEnabled = true;
            this.cbDPI.Items.AddRange(new object[] {
            "72",
            "150",
            "300"});
            this.cbDPI.Location = new System.Drawing.Point(851, 433);
            this.cbDPI.Name = "cbDPI";
            this.cbDPI.Size = new System.Drawing.Size(92, 21);
            this.cbDPI.TabIndex = 5;
            this.cbDPI.Text = "150";
            this.cbDPI.SelectedIndexChanged += new System.EventHandler(this.cbDPI_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(6, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(780, 720);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(957, 698);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(124, 23);
            this.button18.TabIndex = 2;
            this.button18.Text = "gCode preview";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(957, 669);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(124, 23);
            this.button17.TabIndex = 1;
            this.button17.Text = "Generate gCode";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(6, 6);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(123, 23);
            this.button16.TabIndex = 0;
            this.button16.Text = "Load image";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // tpInterface
            // 
            this.tpInterface.Controls.Add(this.dgvGcode);
            this.tpInterface.Controls.Add(this.button15);
            this.tpInterface.Controls.Add(this.button3);
            this.tpInterface.Controls.Add(this.txtCommand);
            this.tpInterface.Controls.Add(this.button1);
            this.tpInterface.Controls.Add(this.txtResults);
            this.tpInterface.Controls.Add(this.txtComReceive);
            this.tpInterface.Controls.Add(this.button6);
            this.tpInterface.Controls.Add(this.groupBox1);
            this.tpInterface.Controls.Add(this.button13);
            this.tpInterface.Controls.Add(this.button14);
            this.tpInterface.Location = new System.Drawing.Point(4, 22);
            this.tpInterface.Name = "tpInterface";
            this.tpInterface.Size = new System.Drawing.Size(1152, 757);
            this.tpInterface.TabIndex = 4;
            this.tpInterface.Text = "Interface";
            this.tpInterface.UseVisualStyleBackColor = true;
            // 
            // dgvGcode
            // 
            this.dgvGcode.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvGcode.AllowUserToAddRows = false;
            this.dgvGcode.AllowUserToDeleteRows = false;
            this.dgvGcode.AllowUserToResizeRows = false;
            this.dgvGcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvGcode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGcode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stat,
            this.lineNum,
            this.data});
            this.dgvGcode.Location = new System.Drawing.Point(3, 64);
            this.dgvGcode.MultiSelect = false;
            this.dgvGcode.Name = "dgvGcode";
            this.dgvGcode.ReadOnly = true;
            this.dgvGcode.RowHeadersVisible = false;
            this.dgvGcode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvGcode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGcode.Size = new System.Drawing.Size(484, 255);
            this.dgvGcode.StandardTab = true;
            this.dgvGcode.TabIndex = 53;
            this.dgvGcode.VirtualMode = true;
            // 
            // stat
            // 
            this.stat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.stat.Frozen = true;
            this.stat.HeaderText = "Sts";
            this.stat.Name = "stat";
            this.stat.ReadOnly = true;
            this.stat.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lineNum
            // 
            this.lineNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.lineNum.HeaderText = "Line";
            this.lineNum.Name = "lineNum";
            this.lineNum.ReadOnly = true;
            this.lineNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // data
            // 
            this.data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.data.HeaderText = "GCode";
            this.data.Name = "data";
            this.data.ReadOnly = true;
            this.data.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.data.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Red;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.Yellow;
            this.button3.Location = new System.Drawing.Point(966, 402);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(183, 71);
            this.button3.TabIndex = 51;
            this.button3.Text = "EMERGENCY STOP";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // fpJog
            // 
            this.fpJog.Controls.Add(this.label4);
            this.fpJog.Controls.Add(this.cbStepSize);
            this.fpJog.Controls.Add(this.button4);
            this.fpJog.Controls.Add(this.button5);
            this.fpJog.Controls.Add(this.button7);
            this.fpJog.Controls.Add(this.button8);
            this.fpJog.Controls.Add(this.jcZeroAxes);
            this.fpJog.Controls.Add(this.button11);
            this.fpJog.Controls.Add(this.button10);
            this.fpJog.Location = new System.Drawing.Point(4, 22);
            this.fpJog.Name = "fpJog";
            this.fpJog.Padding = new System.Windows.Forms.Padding(3);
            this.fpJog.Size = new System.Drawing.Size(1152, 757);
            this.fpJog.TabIndex = 0;
            this.fpJog.Text = "Jog Control";
            this.fpJog.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(328, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 45;
            this.label4.Text = "Step size";
            // 
            // cbStepSize
            // 
            this.cbStepSize.FormattingEnabled = true;
            this.cbStepSize.Items.AddRange(new object[] {
            "0.01",
            "0.1",
            "1",
            "10"});
            this.cbStepSize.Location = new System.Drawing.Point(328, 56);
            this.cbStepSize.Name = "cbStepSize";
            this.cbStepSize.Size = new System.Drawing.Size(50, 21);
            this.cbStepSize.TabIndex = 44;
            // 
            // tpFile
            // 
            this.tpFile.Location = new System.Drawing.Point(4, 22);
            this.tpFile.Name = "tpFile";
            this.tpFile.Padding = new System.Windows.Forms.Padding(3);
            this.tpFile.Size = new System.Drawing.Size(1152, 757);
            this.tpFile.TabIndex = 2;
            this.tpFile.Text = "File to print";
            this.tpFile.UseVisualStyleBackColor = true;
            // 
            // tpSetting
            // 
            this.tpSetting.Location = new System.Drawing.Point(4, 22);
            this.tpSetting.Name = "tpSetting";
            this.tpSetting.Size = new System.Drawing.Size(1152, 757);
            this.tpSetting.TabIndex = 3;
            this.tpSetting.Text = "Setting";
            this.tpSetting.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pgBar
            // 
            this.pgBar.BackColor = System.Drawing.SystemColors.Control;
            this.pgBar.ForeColor = System.Drawing.Color.Red;
            this.pgBar.Location = new System.Drawing.Point(12, 826);
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(1158, 23);
            this.pgBar.Step = 1;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 50;
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Location = new System.Drawing.Point(804, 232);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 30);
            this.button2.TabIndex = 18;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button9.BackgroundImage")));
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button9.Location = new System.Drawing.Point(840, 232);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(30, 30);
            this.button9.TabIndex = 19;
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button12.BackgroundImage")));
            this.button12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button12.Location = new System.Drawing.Point(876, 232);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(30, 30);
            this.button12.TabIndex = 20;
            this.button12.UseVisualStyleBackColor = true;
            // 
            // button19
            // 
            this.button19.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button19.BackgroundImage")));
            this.button19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button19.Location = new System.Drawing.Point(912, 232);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(30, 30);
            this.button19.TabIndex = 21;
            this.button19.UseVisualStyleBackColor = true;
            // 
            // button20
            // 
            this.button20.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button20.BackgroundImage")));
            this.button20.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button20.Location = new System.Drawing.Point(948, 232);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(30, 30);
            this.button20.TabIndex = 22;
            this.button20.UseVisualStyleBackColor = true;
            // 
            // CNCMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1184, 861);
            this.Controls.Add(this.pgBar);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MinimumSize = new System.Drawing.Size(1200, 900);
            this.Name = "CNCMain";
            this.Text = "Main CNC";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbUpdate)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpImage.ResumeLayout(false);
            this.tpImage.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGamma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tpInterface.ResumeLayout(false);
            this.tpInterface.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGcode)).EndInit();
            this.fpJog.ResumeLayout(false);
            this.fpJog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer TimerStatusUpdate;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbUpdate;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripComboBox cbPort;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button jcZeroAxes;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.TextBox txtComReceive;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpImage;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.TabPage tpFile;
        private System.Windows.Forms.TabPage fpJog;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbDPI;
        private System.Windows.Forms.TextBox txtGCodePreview;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbStepSize;
        private System.Windows.Forms.TextBox txtImgSizePixelY;
        private System.Windows.Forms.TextBox txtImgSizePixelX;
        private System.Windows.Forms.TextBox txtImgSizeY;
        private System.Windows.Forms.TextBox txtImgSizeX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbMode;
        private System.Windows.Forms.TextBox txtFeedRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.ToolStripButton btRefreshPort;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TrackBar tbUpdate;
        internal DmitryBrant.CustomControls.SevenSegmentArray displayE;
        internal DmitryBrant.CustomControls.SevenSegmentArray displayZ;
        internal DmitryBrant.CustomControls.SevenSegmentArray displayY;
        internal DmitryBrant.CustomControls.SevenSegmentArray txtLaserTemp;
        internal DmitryBrant.CustomControls.SevenSegmentArray displayX;
        private System.Windows.Forms.TextBox txtMemoryFree;
        private System.Windows.Forms.TabPage tpSetting;
        private System.Windows.Forms.TabPage tpInterface;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblBrightness;
        private System.Windows.Forms.Label lblContrast;
        private System.Windows.Forms.Label lblGamma;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar tbBrightness;
        private System.Windows.Forms.TrackBar tbGamma;
        private System.Windows.Forms.TrackBar tbContrast;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        internal System.Windows.Forms.DataGridView dgvGcode;
        internal System.Windows.Forms.DataGridViewTextBoxColumn stat;
        internal System.Windows.Forms.DataGridViewTextBoxColumn lineNum;
        internal System.Windows.Forms.DataGridViewTextBoxColumn data;
        private System.Windows.Forms.Label lblImageAction;
        private System.Windows.Forms.TextBox txtRes;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button2;
    }
}

