namespace CNCControl
{
    partial class frmCNCMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCNCMain));
            this.comPort = new System.IO.Ports.SerialPort(this.components);
            this.txtResults = new System.Windows.Forms.TextBox();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TimerStatusUpdate = new System.Windows.Forms.Timer(this.components);
            this.txtMaxLaserTemp = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbUpdate = new System.Windows.Forms.CheckBox();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblTX = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRX = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.txtComReceive = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtGCodePreview = new System.Windows.Forms.TextBox();
            this.cbDPI = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button18 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.cbStepSize = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button20 = new System.Windows.Forms.Button();
            this.txtImgSizePixelX = new System.Windows.Forms.TextBox();
            this.txtImgSizePixelY = new System.Windows.Forms.TextBox();
            this.txtImgSizeX = new System.Windows.Forms.TextBox();
            this.txtImgSizeY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.displayE = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.displayZ = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.displayY = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.displayX = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.txtLaserTemp = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comPort
            // 
            this.comPort.BaudRate = 250000;
            this.comPort.DiscardNull = true;
            this.comPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.comPort_DataReceived);
            // 
            // txtResults
            // 
            this.txtResults.Location = new System.Drawing.Point(12, 531);
            this.txtResults.MaxLength = 0;
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResults.Size = new System.Drawing.Size(786, 174);
            this.txtResults.TabIndex = 2;
            this.txtResults.WordWrap = false;
            // 
            // txtCommand
            // 
            this.txtCommand.Location = new System.Drawing.Point(12, 502);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(786, 20);
            this.txtCommand.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(804, 500);
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
            this.TimerStatusUpdate.Enabled = true;
            this.TimerStatusUpdate.Interval = 3000;
            this.TimerStatusUpdate.Tick += new System.EventHandler(this.updateStatus_Tick);
            // 
            // txtMaxLaserTemp
            // 
            this.txtMaxLaserTemp.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxLaserTemp.Location = new System.Drawing.Point(56, 284);
            this.txtMaxLaserTemp.Name = "txtMaxLaserTemp";
            this.txtMaxLaserTemp.Size = new System.Drawing.Size(148, 44);
            this.txtMaxLaserTemp.TabIndex = 17;
            this.txtMaxLaserTemp.Text = "25.00";
            this.txtMaxLaserTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMaxLaserTemp.TextChanged += new System.EventHandler(this.txtMaxLaserTemp_TextChanged);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(12, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(149, 27);
            this.button2.TabIndex = 18;
            this.button2.Text = "!!! Disable EndStops !!!";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(881, 500);
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
            this.groupBox1.Controls.Add(this.displayE);
            this.groupBox1.Controls.Add(this.displayZ);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.displayY);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.displayX);
            this.groupBox1.Controls.Add(this.txtLaserTemp);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Controls.Add(this.txtInterval);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbUpdate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMaxLaserTemp);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(948, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 469);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Position";
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
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 24);
            this.label8.TabIndex = 18;
            this.label8.Text = "Z";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 24);
            this.label1.TabIndex = 16;
            this.label1.Text = "X";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 388);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 17);
            this.label3.TabIndex = 28;
            this.label3.Text = "Update interval (Sec)";
            // 
            // cbUpdate
            // 
            this.cbUpdate.AutoSize = true;
            this.cbUpdate.Checked = true;
            this.cbUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUpdate.Location = new System.Drawing.Point(34, 424);
            this.cbUpdate.Name = "cbUpdate";
            this.cbUpdate.Size = new System.Drawing.Size(94, 17);
            this.cbUpdate.TabIndex = 29;
            this.cbUpdate.Text = "Status Update";
            this.cbUpdate.UseVisualStyleBackColor = true;
            this.cbUpdate.CheckedChanged += new System.EventHandler(this.cbUpdate_CheckedChanged);
            // 
            // txtInterval
            // 
            this.txtInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInterval.Location = new System.Drawing.Point(147, 385);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(61, 20);
            this.txtInterval.TabIndex = 30;
            this.txtInterval.Text = "3";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(49, 334);
            this.trackBar1.Maximum = 5;
            this.trackBar1.Minimum = -5;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(155, 45);
            this.trackBar1.TabIndex = 31;
            this.trackBar1.Value = 3;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTX,
            this.lblRX,
            this.lblMode,
            this.toolStripDropDownButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 826);
            this.statusStrip1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1184, 35);
            this.statusStrip1.TabIndex = 33;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblTX
            // 
            this.lblTX.AutoSize = false;
            this.lblTX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblTX.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblTX.Name = "lblTX";
            this.lblTX.Size = new System.Drawing.Size(32, 30);
            this.lblTX.Text = "TX";
            // 
            // lblRX
            // 
            this.lblRX.AutoSize = false;
            this.lblRX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblRX.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblRX.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblRX.Name = "lblRX";
            this.lblRX.Size = new System.Drawing.Size(32, 30);
            this.lblRX.Text = "RX";
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = false;
            this.lblMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblMode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(80, 30);
            this.lblMode.Text = "OFFLINE";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(164, 33);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
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
            this.toolStripComboBox1,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1184, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 100);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 112);
            this.button3.TabIndex = 35;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(88, 100);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 112);
            this.button4.TabIndex = 36;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(170, 56);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(138, 40);
            this.button5.TabIndex = 37;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(170, 211);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(138, 39);
            this.button7.TabIndex = 38;
            this.button7.Text = "button7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(314, 100);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 112);
            this.button8.TabIndex = 39;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(396, 100);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 112);
            this.button9.TabIndex = 40;
            this.button9.Text = "button9";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
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
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(189, 113);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(97, 83);
            this.button12.TabIndex = 43;
            this.button12.Text = "Zero all axes";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(962, 499);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(75, 23);
            this.button13.TabIndex = 44;
            this.button13.Text = "Send Lines";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(1043, 499);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(75, 23);
            this.button14.TabIndex = 45;
            this.button14.Text = "RUN";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(805, 711);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(75, 23);
            this.button15.TabIndex = 46;
            this.button15.Text = "CANCEL";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // txtComReceive
            // 
            this.txtComReceive.Location = new System.Drawing.Point(805, 530);
            this.txtComReceive.MaxLength = 0;
            this.txtComReceive.Multiline = true;
            this.txtComReceive.Name = "txtComReceive";
            this.txtComReceive.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtComReceive.Size = new System.Drawing.Size(366, 175);
            this.txtComReceive.TabIndex = 47;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 83);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(930, 410);
            this.tabControl1.TabIndex = 48;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cbMode);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtImgSizeY);
            this.tabPage2.Controls.Add(this.txtImgSizeX);
            this.tabPage2.Controls.Add(this.txtImgSizePixelY);
            this.tabPage2.Controls.Add(this.txtImgSizePixelX);
            this.tabPage2.Controls.Add(this.txtGCodePreview);
            this.tabPage2.Controls.Add(this.cbDPI);
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Controls.Add(this.button18);
            this.tabPage2.Controls.Add(this.button17);
            this.tabPage2.Controls.Add(this.button16);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(922, 384);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Image to print";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtGCodePreview
            // 
            this.txtGCodePreview.Location = new System.Drawing.Point(592, 6);
            this.txtGCodePreview.MaxLength = 0;
            this.txtGCodePreview.Multiline = true;
            this.txtGCodePreview.Name = "txtGCodePreview";
            this.txtGCodePreview.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtGCodePreview.Size = new System.Drawing.Size(393, 372);
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
            this.cbDPI.Location = new System.Drawing.Point(359, 93);
            this.cbDPI.Name = "cbDPI";
            this.cbDPI.Size = new System.Drawing.Size(58, 21);
            this.cbDPI.TabIndex = 5;
            this.cbDPI.SelectedIndexChanged += new System.EventHandler(this.cbDPI_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(7, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(345, 342);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(359, 64);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(113, 23);
            this.button18.TabIndex = 2;
            this.button18.Text = "gCode preview";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(359, 35);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(113, 23);
            this.button17.TabIndex = 1;
            this.button17.Text = "Generate gCode";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(359, 6);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(113, 23);
            this.button16.TabIndex = 0;
            this.button16.Text = "Load image";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(922, 384);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "File to print";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.cbStepSize);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Controls.Add(this.button7);
            this.tabPage1.Controls.Add(this.button8);
            this.tabPage1.Controls.Add(this.button12);
            this.tabPage1.Controls.Add(this.button9);
            this.tabPage1.Controls.Add(this.button11);
            this.tabPage1.Controls.Add(this.button10);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(922, 384);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Jog Control";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            "0.1",
            "0.01",
            "1",
            "10"});
            this.cbStepSize.Location = new System.Drawing.Point(328, 56);
            this.cbStepSize.Name = "cbStepSize";
            this.cbStepSize.Size = new System.Drawing.Size(50, 21);
            this.cbStepSize.TabIndex = 44;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button20
            // 
            this.button20.Location = new System.Drawing.Point(375, 40);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(75, 23);
            this.button20.TabIndex = 49;
            this.button20.Text = "button20";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // txtImgSizePixelX
            // 
            this.txtImgSizePixelX.Location = new System.Drawing.Point(358, 120);
            this.txtImgSizePixelX.Name = "txtImgSizePixelX";
            this.txtImgSizePixelX.Size = new System.Drawing.Size(59, 20);
            this.txtImgSizePixelX.TabIndex = 7;
            // 
            // txtImgSizePixelY
            // 
            this.txtImgSizePixelY.Location = new System.Drawing.Point(423, 120);
            this.txtImgSizePixelY.Name = "txtImgSizePixelY";
            this.txtImgSizePixelY.Size = new System.Drawing.Size(59, 20);
            this.txtImgSizePixelY.TabIndex = 8;
            // 
            // txtImgSizeX
            // 
            this.txtImgSizeX.Location = new System.Drawing.Point(358, 147);
            this.txtImgSizeX.Name = "txtImgSizeX";
            this.txtImgSizeX.Size = new System.Drawing.Size(59, 20);
            this.txtImgSizeX.TabIndex = 9;
            // 
            // txtImgSizeY
            // 
            this.txtImgSizeY.Location = new System.Drawing.Point(423, 147);
            this.txtImgSizeY.Name = "txtImgSizeY";
            this.txtImgSizeY.Size = new System.Drawing.Size(59, 20);
            this.txtImgSizeY.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(423, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "DPI";
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
            this.txtLaserTemp.ArrayCount = 4;
            this.txtLaserTemp.ColorBackground = System.Drawing.Color.Black;
            this.txtLaserTemp.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtLaserTemp.ColorLight = System.Drawing.Color.Red;
            this.txtLaserTemp.DecimalShow = true;
            this.txtLaserTemp.ElementPadding = new System.Windows.Forms.Padding(4);
            this.txtLaserTemp.ElementWidth = 10;
            this.txtLaserTemp.ItalicFactor = 0F;
            this.txtLaserTemp.Location = new System.Drawing.Point(51, 228);
            this.txtLaserTemp.Name = "txtLaserTemp";
            this.txtLaserTemp.Size = new System.Drawing.Size(153, 50);
            this.txtLaserTemp.TabIndex = 34;
            this.txtLaserTemp.TabStop = false;
            this.txtLaserTemp.Value = "";
            // 
            // cbMode
            // 
            this.cbMode.FormattingEnabled = true;
            this.cbMode.Items.AddRange(new object[] {
            "Mode 1",
            "Mode 2"});
            this.cbMode.Location = new System.Drawing.Point(478, 38);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(108, 21);
            this.cbMode.TabIndex = 12;
            // 
            // frmCNCMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1184, 861);
            this.Controls.Add(this.button20);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtComReceive);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.toolStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MinimumSize = new System.Drawing.Size(1200, 900);
            this.Name = "frmCNCMain";
            this.Text = "Main CNC";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort comPort;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer TimerStatusUpdate;
        private System.Windows.Forms.TextBox txtMaxLaserTemp;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private DmitryBrant.CustomControls.SevenSegmentArray displayX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbUpdate;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.TrackBar trackBar1;
        private DmitryBrant.CustomControls.SevenSegmentArray displayE;
        private DmitryBrant.CustomControls.SevenSegmentArray displayZ;
        private DmitryBrant.CustomControls.SevenSegmentArray displayY;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTX;
        private System.Windows.Forms.ToolStripStatusLabel lblRX;
        private System.Windows.Forms.ToolStripStatusLabel lblMode;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private DmitryBrant.CustomControls.SevenSegmentArray txtLaserTemp;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.TextBox txtComReceive;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbDPI;
        private System.Windows.Forms.TextBox txtGCodePreview;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbStepSize;
        private System.Windows.Forms.TextBox txtImgSizePixelY;
        private System.Windows.Forms.TextBox txtImgSizePixelX;
        private System.Windows.Forms.TextBox txtImgSizeY;
        private System.Windows.Forms.TextBox txtImgSizeX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbMode;
    }
}

