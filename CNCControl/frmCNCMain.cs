using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace CNCControl
{
    public enum eMode { CONNECTED, DISCONNECTED, RUNNING, FEEDHOLD, CYCLESTART, FINISHED, ABORTED, WAITING, READY, LOADING, SOFTRESET, INACTIVE, READEEPROM, WRITEEEPROM };

    public partial class frmCNCMain : Form
    {

        bool isConnect = false;
        public delegate void SetText(string str);
        public SetText serialDelegate;
        Color defColor;
        bool bAlarmLaser = false;
        List<string> CommandHistory;
        int IndexCommandHistory;
        bool IgnoreKey;
        bool bWait;
        string serialString;
        bool bRunning;
        public delegate void UpdatePositionDelegate(string str);
        UpdatePositionDelegate UpdatePositionAction;
        public delegate void TransmitLEDDelegate();
        TransmitLEDDelegate TX_LED;
        public delegate void ReceiveLEDDelegate();
        ReceiveLEDDelegate RX_LED;
        private System.Timers.Timer TXLEDoff;
        private System.Timers.Timer RXLEDoff;
        Regex PositionRegex;
        public eMode CurrentMode;
        frmEEPROM fEEPROM;
        private Thread workThread;

        public frmCNCMain()
        {
            InitializeComponent();
            UpdatePositionAction = new UpdatePositionDelegate(UpdatePosition);

            serialDelegate = new SetText(SetTextMethod);
            serialString = "";
            bWait = false;
            bRunning = false;
            comPort.ReadBufferSize = 1024;
            comPort.WriteBufferSize = 1024;
            defColor = txtLaserTemp.BackColor;
            TXLEDoff = new System.Timers.Timer(10);
            TXLEDoff.Elapsed += TXLEDoffElapsed;
            RXLEDoff = new System.Timers.Timer(10);
            RXLEDoff.Elapsed += RXLEDoffElapsed; 
            // Regex pour status D[xxx;yyy;zzz;eee],C[xxx;yyy;zzz;eee],T[temp]
            PositionRegex = new Regex(
              "D\\[([-+]?[0-9]*[\\\\.,]?[0-9]*);([-+]?[0-9]*[\\\\.,]?[0" +
              "-9]*);([-+]?[0-9]*[\\\\.,]?[0-9]*);([-+]?[0-9]*[\\\\.,]?[0-9]*)\\],C\\[([-+]?[0-9]*[\\\\." +
              ",]?[0-9]*);([-+]?[0-9]*[\\\\.,]?[0-9]*);([-+]?[0-9]*[\\\\.,]" +
              "?[0-9]*);([-+]?[0-9]*[\\\\.,]?[0-9]*)\\],T\\[([-+]?[0-9]*[\\\\.,]?[0-9]*)\\].*",
              RegexOptions.CultureInvariant | RegexOptions.Compiled
            );     
        }

        public void UpdatePosition(string str)
        {
            double mx, wx;
            double my, wy;
            double mz, wz;
            double me, we;
            double tempLaser;

            try
            {
                MatchCollection matches = PositionRegex.Matches(str);
                GroupCollection groups = matches[0].Groups;

            //Debug.WriteLine(str + "\r\n");


                mx = double.Parse(groups[1].Value.ToString());
                my = double.Parse(groups[2].Value.ToString());
                mz = double.Parse(groups[3].Value.ToString());
                me = double.Parse(groups[4].Value.ToString());
                wx = double.Parse(groups[5].Value.ToString());
                wy = double.Parse(groups[6].Value.ToString());
                wz = double.Parse(groups[7].Value.ToString());
                we = double.Parse(groups[8].Value.ToString());
                tempLaser = double.Parse(groups[9].Value.ToString());

                displayX.Value = string.Format("{0:0.00}", wx);
                displayY.Value = string.Format("{0:0.00}", wy);
                displayZ.Value = string.Format("{0:0.00}", wz);
                displayE.Value = string.Format("{0:0.00}", we);
                txtLaserTemp.Value = string.Format("{0:0.00}", tempLaser);

                //Debug.WriteLine(string.Format("M X={0} Y={1} Z={2}", mx, my, mz));
                //Debug.WriteLine(string.Format("W X={0} Y={1} Z={2}", wx, wy, wz));

            }
            catch (Exception ex) { MessageBox.Show(str, ex.Message); }
        }

        public void SetTextMethod(string str)
        {
            bWait = true;
            NumberStyles styles;
            styles = NumberStyles.Number;
            string strTemp;
            if (txtResults.Text.Length > 20000) txtResults.Text = txtResults.Text.Substring(10000);
            str = str.Replace("\n", Environment.NewLine);
            txtResults.AppendText(DateTime.Now.ToString("HH:MM:ss") + Environment.NewLine + " " + str);
            //textBox1.AppendText(str);
            if (str.Contains("TL:"))
            {
                strTemp = str.Remove(0, str.LastIndexOf("TL:") + 3);
                txtLaserTemp.Text = Double.Parse(strTemp.Remove(strTemp.IndexOf(" ")),styles).ToString();
            }
            bWait = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripButton1.BackColor = Color.Red;
            toolStripButton1.ToolTipText = "";
            toolStripButton1.Text = "Connect";
            foreach (string s in SerialPort.GetPortNames())
            {
                toolStripComboBox1.Items.Add(s);
            }
            comPort.BaudRate = 250000;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (isConnect == false)
            {
                if (toolStripComboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Select a port,","No port selected");
                    return;
                }
                comPort.PortName = (string)toolStripComboBox1.SelectedItem;
                comPort.Open();
                toolStripButton1.BackColor = Color.LightGreen;
                isConnect = true;
                toolStripButton1.Text = "Disconnect";
                //timer2.Enabled = true;
                TimerStatusUpdate.Enabled = true;
                lblMode.BackColor = System.Drawing.Color.LightGreen;
                lblMode.Text = "CONNECTED";
            }
            else 
            {
                comPort.Close() ;
                //timer2.Enabled = false;
                TimerStatusUpdate.Enabled = false;
                toolStripButton1.BackColor = Color.Red;
                isConnect = false;
                toolStripButton1.Text = "Connect";
                lblMode.BackColor = System.Drawing.Color.Khaki;
                lblMode.Text = "OFFLINE";
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CommandHistory.Add(textBox2.Text);
            //IndexCommandHistory = CommandHistory.Count;
            //serialPort1.WriteLine(textBox2.Text);
            WriteSerial(txtCommand.Text);
            //textBox1.Text = sendCommand(textBox2.Text,100).Replace("\n",Environment.NewLine);     
        }

        public void WriteSerial(string cmd)
        {
            if(comPort.IsOpen) 
            {
                comPort.WriteLine(cmd);
            }
        }

        private void updateStatus_Tick(object sender, EventArgs e)
        {
            if (cbUpdate.Checked)
            {
                if (comPort.IsOpen)
                    {
                        WriteSerial("M114");
                    }
            }
        }
            //if (Double.Parse(txtLaserTemp.Text) > Double.Parse(txtMaxLaserTemp.Text))
            //{
            //    bAlarmLaser = true;
            //    this.Text = "ALARM - TEMP LASER";
            //    if (this.BackColor == Color.Red)
            //    {
            //        txtLaserTemp.BackColor = defColor;
            //    }
            //    else
            //    {
            //        txtLaserTemp.BackColor = Color.Red;
            //    }
            //    Console.Beep(5000, 100); ;
            //} else if (bAlarmLaser == true) {
            //    this.Text = "CNC Main Control";
            //    txtLaserTemp.BackColor = defColor;
            //    bAlarmLaser = false;
            //}


        private void timer2_Tick(object sender, EventArgs e)
        {
            if (bRunning)
            {
                bWait = true;
            }
            else getStatus();
        }

        private void getStatus()
        {
            string reply = sendCommand("M114",1000);
            txtResults.AppendText(reply + Environment.NewLine);
            //DEST X:0.00 DEST Y:0.00 DEST Z:0.00 DEST E:0.00 TL:24.90 CURRENT X: 0.00 CURRENT Y:0.00 CURRENT Z:0.00 CURRENT E:0.00
            //txtX.Text = Utils.getStringValue(reply, "CURRENT X:");
            //txtY.Text = Utils.getStringValue(reply, "CURRENT Y:");
            //txtZ.Text = Utils.getStringValue(reply, "CURRENT Z:");
            //txtE.Text = Utils.getStringValue(reply, "CURRENT E:");
            txtLaserTemp.Text = Utils.getStringValue(reply, "TL:");
            bWait = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sendCommand("M121",150);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtResults.Text = "";
        }

        public string sendCommand(string Command,int waitTime)
        {
            if (!comPort.IsOpen) return "";
            Application.DoEvents();
            string reply = "";
            comPort.WriteLine(Command);
            System.Threading.Thread.Sleep(waitTime);
            while (true)
            {
                while (comPort.BytesToRead > 0)
                {
                    reply += comPort.ReadExisting();
                }

                if(reply != String.Empty)
                {
                    //reply = reply.Replace("\n","");
                    reply = reply.Replace("ok", "");
                    return reply;
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (!comPort.IsOpen) return;
            TimerStatusUpdate.Enabled = false;
            fEEPROM = new frmEEPROM();
            fEEPROM.ShowDialog(this);
            TimerStatusUpdate.Enabled = true;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            long currMillis = DateTime.Now.Ticks;
            int currentIndex = 0;
            bRunning = true;
            timer2.Enabled = false;
            double y = 0;
            txtResults.Text = DateTime.Now.ToString();
            string strCommand;
            sendCommand("G0 X0 Y0 F9000",20);
            while (true) {
                
                    strCommand = "G1 X100 F1000";
                    //textBox1.AppendText(strCommand + Environment.NewLine);
                    sendCommand(strCommand,20);
                    currentIndex++;
                y += 0.1;
                strCommand = "G1 Y" + y.ToString().Trim() + " F1000";
                //textBox1.AppendText(strCommand + Environment.NewLine);
                sendCommand(strCommand,20);

                    strCommand = "G1 X0 F1000";
                    //textBox1.AppendText(strCommand + Environment.NewLine);
                    sendCommand(strCommand,20);

                y += 0.1;
                strCommand = "G1 Y" + y.ToString().Trim() + " F1000";
                //textBox1.AppendText(strCommand + Environment.NewLine);
                sendCommand(strCommand,20);

                if (DateTime.Now.Ticks - currMillis > 50000000)
                {
                    getStatus();
                    currMillis = DateTime.Now.Ticks;
                }

                if (y > 100) break;
            }
            txtResults.AppendText(DateTime.Now.ToString());
            bRunning = false;
            timer2.Enabled = true;
        }

        private void serialPort1_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            if (e.EventType == SerialError.TXFull)
            {
                txtResults.AppendText("Error TX" + Environment.NewLine);
            }
        }

        private void cbUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUpdate.Checked)
            {
                TimerStatusUpdate.Interval = 1000 / trackBar1.Value;
                TimerStatusUpdate.Enabled = true;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //if (TimerStatusUpdate.Enabled)
            {
                //TimerStatusUpdate.Enabled = false;
                TimerStatusUpdate.Interval = 1000 / trackBar1.Value;
            }
            txtInterval.Text = trackBar1.Value.ToString();
        }

        private void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
           string ACK = string.Empty;
           List<string> EEPROMConfig = new List<string>();
            lock (this)
            {
                while (comPort.BytesToRead > 0)
                {
                    if (comPort.IsOpen) ACK = comPort.ReadLine();
                    ACK = ACK.ToUpper().Trim();
                    // Read EEPROM
                    if (CurrentMode == eMode.READEEPROM)
                    {

                        EEPROMConfig.Add(ACK);
                        if (ACK == "END_EEPROM")
                        {
                            CurrentMode = eMode.READY;
                            Invoke(fEEPROM.ReadEEPROMValuesAction, EEPROMConfig);
                        }
                    }
                    else if (ACK == "OK")
                    {
                        //txtResults.Text = "OK";
                        //Invoke(RX_LED);
                    }
                    else if (ACK.StartsWith("D["))
                    {
                        Invoke(UpdatePositionAction, ACK);
                    }
                    else if (ACK != "") { }
                }
                Application.DoEvents();
            }
        }

        private void comPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            test frm = new test();
            frm.Show(this);
            Invoke(frm.SetTextDelegate, "Hello");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            test frm = new test();
            frm.Show(this);
            Invoke(frm.SetTextDelegate, "Hello");
        }

        private void TransmitLED()
        {            
            lblTX.BackColor = System.Drawing.Color.LightGreen;
            TXLEDoff.Enabled = true;
        }

        private void TXLEDoffElapsed(object sender, EventArgs e)
        {
            TXLEDoff.Enabled = false;
            lblTX.BackColor = System.Drawing.Color.DarkGray;
        }

        private void ReceiveLED()
        {
            RXLEDoff.Enabled = true;
            lblRX.BackColor = System.Drawing.Color.Khaki;
        }

        private void RXLEDoffElapsed(object sender, EventArgs e)
        {
            RXLEDoff.Enabled = false;
            lblRX.BackColor = System.Drawing.Color.DarkGray;
        }

    }
}
