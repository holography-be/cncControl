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
        Color defColor;
        bool bRunning;

#region Declare Delegate
        public delegate void UpdatePositionDelegate(string str);
        UpdatePositionDelegate UpdatePositionAction;
        public delegate void UpdateComReceiveTextDelegate(string str);
        UpdateComReceiveTextDelegate UpdateComReceiveTextAction; 
#endregion

        Regex PositionRegex;
        public eMode CurrentMode;
        frmEEPROM formEEPROM;
        private Thread CommandThread;
        List<string> gCodeCommands;
        bool bCancel;
        bool WaitingACK;

        double curX;
        double curY;
        double curZ;
        double curE;


        public frmCNCMain()
        {
            InitializeComponent();
            
#region Define Delegate
            UpdatePositionAction = new UpdatePositionDelegate(UpdatePosition);
            UpdateComReceiveTextAction = new UpdateComReceiveTextDelegate(UpdateComReceiveText); 
#endregion

            comPort.ReadBufferSize = 1024;
            comPort.WriteBufferSize = 1024;
            defColor = txtLaserTemp.BackColor;
            // Regex pour status D[xxx;yyy;zzz;eee],C[xxx;yyy;zzz;eee],T[temp]
            PositionRegex = new Regex(
              "D\\[([-+]?[0-9]*[\\\\.,]?[0-9]*);([-+]?[0-9]*[\\\\.,]?[0" +
              "-9]*);([-+]?[0-9]*[\\\\.,]?[0-9]*);([-+]?[0-9]*[\\\\.,]?[0-9]*)\\],C\\[([-+]?[0-9]*[\\\\." +
              ",]?[0-9]*);([-+]?[0-9]*[\\\\.,]?[0-9]*);([-+]?[0-9]*[\\\\.,]" +
              "?[0-9]*);([-+]?[0-9]*[\\\\.,]?[0-9]*)\\],T\\[([-+]?[0-9]*[\\\\.,]?[0-9]*)\\].*",
              RegexOptions.CultureInvariant | RegexOptions.Compiled
            );
            bCancel = true;
            bRunning = false;
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
                TimerStatusUpdate.Enabled = false;
                comPort.Close() ;               
                toolStripButton1.BackColor = Color.Red;
                isConnect = false;
                toolStripButton1.Text = "Connect";
                lblMode.BackColor = System.Drawing.Color.Khaki;
                lblMode.Text = "OFFLINE";
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WriteSerial(txtCommand.Text);
        }

        public void WriteSerial(string cmd)
        {
            if(comPort.IsOpen) 
            {
                comPort.WriteLine(cmd.ToUpper());
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

        private void button2_Click(object sender, EventArgs e)
        {
            WriteSerial("M121");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtResults.Text = "";
            txtComReceive.Text = "";
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (!comPort.IsOpen) return;
            TimerStatusUpdate.Enabled = false;
            formEEPROM = new frmEEPROM();
            formEEPROM.ShowDialog(this);
            TimerStatusUpdate.Enabled = true;
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
            {
                if (trackBar1.Value < 0)
                {
                    TimerStatusUpdate.Interval = 1000 / (-(trackBar1.Value-1));
                    txtInterval.Text = ((double)(TimerStatusUpdate.Interval)/1000).ToString();
                    TimerStatusUpdate.Enabled = true;
                }
                if (trackBar1.Value == 0)
                {
                    txtInterval.Text = "0";
                    TimerStatusUpdate.Enabled = false;
                }
                if (trackBar1.Value > 0)
                {
                    TimerStatusUpdate.Interval = trackBar1.Value * 1000;
                    txtInterval.Text = trackBar1.Value.ToString();
                    TimerStatusUpdate.Enabled = true;
                }
            }
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
                            Invoke(formEEPROM.ReadEEPROMValuesAction, EEPROMConfig);
                        }
                    }
                    else if (ACK.StartsWith("D["))
                    {
                        Invoke(UpdatePositionAction, ACK);
                    }
                    else if (ACK == "OK") 
                    {
                        // Accusé de reception de commande
                        WaitingACK = false;
                    }
                    else
                    {
                        Invoke(UpdateComReceiveTextAction, ACK);
                    }
                }
                Application.DoEvents();
            }
        }

        private void comPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            Invoke(UpdateComReceiveTextAction, e.ToString());
        }

        public void UpdateComReceiveText(string str)
        {
            txtComReceive.AppendText(str + Environment.NewLine);
        }

        #region Joggle Control
        private void button12_Click(object sender, EventArgs e)
        {
            WriteSerial("G92 X0 Y0 Z0");
            curX = curY = curZ = 0;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            curX += 1;
            WriteSerial("G0 X" + curX);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            curX += 10;
            WriteSerial("G0 X" + curX);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            curX -= 1;
            WriteSerial("G0 X" + curX);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            curX -= 10;
            WriteSerial("G0 X" + curX);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            gCodeCommands = new List<string>();
            int l = 0;
            Random rnd = new Random();
            for (double x = 0; x < 100; x += 0.09)
            {
               


                gCodeCommands.Add("G1 X" + x.ToString()+ " L" + rnd.Next(0,256)   + " F2000");
            }
            //foreach (string line in txtResults.Lines)  + " L" + rnd.Next(0,256) 
            //{
            //    gCodeCommands.Add(line);
            //}
        }

        private void button15_Click(object sender, EventArgs e)
        {
            WriteSerial("M18");
            bCancel = true;
            bRunning = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            curY -= 1;
            WriteSerial("G0 Y" + curY);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            curY -= 10;
            WriteSerial("G0 Y" + curY);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            curY += 1;
            WriteSerial("G0 Y" + curY);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            curY += 10;
            WriteSerial("G0 Y" + curY);
        } 
        #endregion


        private void button14_Click(object sender, EventArgs e)
        {
            WriteSerial("M17");
            bCancel = false;
            CommandThread = new Thread(gCodeThread);
            CommandThread.Start();
        }

        private void gCodeThread()
        {
            bRunning = true;
            Console.WriteLine("Start: " + DateTime.Now);
            foreach (string line in gCodeCommands)
            {
                if (bCancel) break;
                try
                {
                    WriteSerial(line);
                    while(WaitingACK == true) {  // on attend accusé de réception
                	    Application.DoEvents();
                        //Thread.Sleep(5);
                    }
                    if(bCancel == true) break;                
               	    WaitingACK = true;
                    Invoke(UpdateComReceiveTextAction, line);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
                    bCancel = true;
                    break;
                }
            }
            bRunning = false;
            Console.WriteLine("End: " + DateTime.Now);
        }



    }
}
