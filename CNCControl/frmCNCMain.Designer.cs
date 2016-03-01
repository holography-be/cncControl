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
            this.displayE = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.displayZ = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.label9 = new System.Windows.Forms.Label();
            this.displayY = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.label8 = new System.Windows.Forms.Label();
            this.displayX = new DmitryBrant.CustomControls.SevenSegmentArray();
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
            this.txtLaserTemp = new DmitryBrant.CustomControls.SevenSegmentArray();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.txtComReceive = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.label2.Location = new System.Drawing.Point(1069, 225);
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
            this.txtMaxLaserTemp.Location = new System.Drawing.Point(1074, 316);
            this.txtMaxLaserTemp.Name = "txtMaxLaserTemp";
            this.txtMaxLaserTemp.Size = new System.Drawing.Size(148, 44);
            this.txtMaxLaserTemp.TabIndex = 17;
            this.txtMaxLaserTemp.Text = "25.00";
            this.txtMaxLaserTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.groupBox1.Controls.Add(this.displayE);
            this.groupBox1.Controls.Add(this.displayZ);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.displayY);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.displayX);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(1017, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 210);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Position";
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
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 770);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Update interval (Sec)";
            // 
            // cbUpdate
            // 
            this.cbUpdate.AutoSize = true;
            this.cbUpdate.Checked = true;
            this.cbUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUpdate.Location = new System.Drawing.Point(18, 797);
            this.cbUpdate.Name = "cbUpdate";
            this.cbUpdate.Size = new System.Drawing.Size(94, 17);
            this.cbUpdate.TabIndex = 29;
            this.cbUpdate.Text = "Status Update";
            this.cbUpdate.UseVisualStyleBackColor = true;
            this.cbUpdate.CheckedChanged += new System.EventHandler(this.cbUpdate_CheckedChanged);
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(128, 767);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(61, 20);
            this.txtInterval.TabIndex = 30;
            this.txtInterval.Text = "3";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(18, 716);
            this.trackBar1.Maximum = 5;
            this.trackBar1.Minimum = -5;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(167, 45);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 823);
            this.statusStrip1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1253, 35);
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
            this.toolStrip1.Size = new System.Drawing.Size(1253, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(36, 225);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 112);
            this.button3.TabIndex = 35;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(118, 225);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 112);
            this.button4.TabIndex = 36;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(200, 181);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(138, 40);
            this.button5.TabIndex = 37;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(200, 336);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(138, 39);
            this.button7.TabIndex = 38;
            this.button7.Text = "button7";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(344, 225);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 112);
            this.button8.TabIndex = 39;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(426, 225);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 112);
            this.button9.TabIndex = 40;
            this.button9.Text = "button9";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(200, 138);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(138, 37);
            this.button10.TabIndex = 41;
            this.button10.Text = "button10";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(200, 382);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(138, 37);
            this.button11.TabIndex = 42;
            this.button11.Text = "button11";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(219, 238);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(97, 83);
            this.button12.TabIndex = 43;
            this.button12.Text = "Zero";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(980, 499);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(75, 23);
            this.button13.TabIndex = 44;
            this.button13.Text = "Send Lines";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
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
            this.txtLaserTemp.Location = new System.Drawing.Point(1069, 260);
            this.txtLaserTemp.Name = "txtLaserTemp";
            this.txtLaserTemp.Size = new System.Drawing.Size(153, 50);
            this.txtLaserTemp.TabIndex = 34;
            this.txtLaserTemp.TabStop = false;
            this.txtLaserTemp.Value = "";
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(1061, 499);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(75, 23);
            this.button14.TabIndex = 45;
            this.button14.Text = "RUN";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(1142, 499);
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
            this.txtComReceive.Size = new System.Drawing.Size(412, 175);
            this.txtComReceive.TabIndex = 47;
            // 
            // frmCNCMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1253, 858);
            this.Controls.Add(this.txtComReceive);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtLaserTemp);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.cbUpdate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtMaxLaserTemp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.toolStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
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
    }
}

