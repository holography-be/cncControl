﻿using System;
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


namespace CNCControl
{
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

        public frmCNCMain()
        {
            InitializeComponent();
            serialDelegate = new SetText(SetTextMethod);
            serialString = "";
            bWait = false;
            bRunning = false;
            serialPort1.ReadBufferSize = 1024;
            serialPort1.WriteBufferSize = 1024;
            defColor = txtLaserTemp.BackColor;
            CommandHistory = new List<string>();
            IndexCommandHistory = 5;
            CommandHistory.Add("1");
            CommandHistory.Add("2");
            CommandHistory.Add("3");
            CommandHistory.Add("4");
            CommandHistory.Add("5");
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

        }

        public void SetTextMethod(string str)
        {
            bWait = true;
            NumberStyles styles;
            styles = NumberStyles.Number;
            string strTemp;
            if (textBox1.Text.Length > 20000) textBox1.Text = textBox1.Text.Substring(10000);
            str = str.Replace("\n", Environment.NewLine);
            textBox1.AppendText(DateTime.Now.ToString("HH:MM:ss") + Environment.NewLine + " " + str);
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
            serialPort1.BaudRate = 250000;
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
                serialPort1.PortName = (string)toolStripComboBox1.SelectedItem;
                serialPort1.Open();
                toolStripButton1.BackColor = Color.LightGreen;
                isConnect = true;
                toolStripButton1.Text = "Disconnect";
                timer2.Enabled = true;
                timer1.Enabled = true;
                serialPort1.WriteLine("M114");
                System.Threading.Thread.Sleep(50);
                while (serialPort1.BytesToRead > 0) { 
                    serialString += serialPort1.ReadExisting(); 
                }
                serialString = "";
            }
            else 
            {
                serialPort1.Close() ;
                timer2.Enabled = false;
                timer1.Enabled = false;
                toolStripButton1.BackColor = Color.Red;
                isConnect = false;
                toolStripButton1.Text = "Connect";        
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CommandHistory.Add(textBox2.Text);
            IndexCommandHistory = CommandHistory.Count;
            //serialPort1.WriteLine(textBox2.Text);
            textBox1.Text = sendCommand(textBox2.Text,100).Replace("\n",Environment.NewLine);     
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Double.Parse(txtLaserTemp.Text) > Double.Parse(txtMaxLaserTemp.Text))
            {
                bAlarmLaser = true;
                this.Text = "ALARM - TEMP LASER";
                if (this.BackColor == Color.Red)
                {
                    txtLaserTemp.BackColor = defColor;
                }
                else
                {
                    txtLaserTemp.BackColor = Color.Red;
                }
                Console.Beep(5000, 100); ;
            } else if (bAlarmLaser == true) {
                this.Text = "CNC Main Control";
                txtLaserTemp.BackColor = defColor;
                bAlarmLaser = false;
            }
        }

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
            textBox1.AppendText(reply + Environment.NewLine);
            //DEST X:0.00 DEST Y:0.00 DEST Z:0.00 DEST E:0.00 TL:24.90 CURRENT X: 0.00 CURRENT Y:0.00 CURRENT Z:0.00 CURRENT E:0.00
            txtX.Text = Utils.getStringValue(reply, "CURRENT X:");
            txtY.Text = Utils.getStringValue(reply, "CURRENT Y:");
            txtZ.Text = Utils.getStringValue(reply, "CURRENT Z:");
            txtE.Text = Utils.getStringValue(reply, "CURRENT E:");
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
            textBox1.Text = "";
        }

        public string sendCommand(string Command,int waitTime)
        {
            if (!serialPort1.IsOpen) return "";
            Application.DoEvents();
            string reply = "";
            serialPort1.WriteLine(Command);
            System.Threading.Thread.Sleep(waitTime);
            while (true)
            {
                while (serialPort1.BytesToRead > 0)
                {
                    reply += serialPort1.ReadExisting();
                }

                if(reply != String.Empty)
                {
                    //reply = reply.Replace("\n","");
                    reply = reply.Replace("ok", "");
                    return reply;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmEEPROM frmEEPROM = new frmEEPROM();
            frmEEPROM.frmBase = this;
            frmEEPROM.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen) return;
            timer2.Enabled = false;
            frmEEPROM frmEEPROM = new frmEEPROM();
            frmEEPROM.frmBase = this;
            frmEEPROM.ShowDialog(this);
            timer2.Enabled = true;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            long currMillis = DateTime.Now.Ticks;
            int currentIndex = 0;
            bRunning = true;
            timer2.Enabled = false;
            double y = 0;
            textBox1.Text = DateTime.Now.ToString();
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
            textBox1.AppendText(DateTime.Now.ToString());
            bRunning = false;
            timer2.Enabled = true;
        }

        private void serialPort1_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            if (e.EventType == SerialError.TXFull)
            {
                textBox1.AppendText("Error TX" + Environment.NewLine);
            }
        }
    }
}
