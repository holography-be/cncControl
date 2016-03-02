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
using ImageProcessing;
using System.Drawing.Imaging;
using System.IO;

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

        Bitmap imageToPrint;
        List<string> gCodeFromPicture;

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
            cbDPI.SelectedIndex = 0;
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
                if (int.Parse(txtInterval.Text) > 0) TimerStatusUpdate.Interval = 1000 * int.Parse(txtInterval.Text); 
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
            Cursor.Current = Cursors.WaitCursor;
            txtGCodePreview.Visible = true;
            Application.DoEvents();
            if (txtGCodePreview.Text != "")
            {
                foreach (string str in txtGCodePreview.Lines)
                {
                    gCodeCommands.Add(str);
                }
            }
            Cursor.Current = Cursors.Default;


            //gCodeCommands = new List<string>();
            //int l = 0;
            //Random rnd = new Random();
            //for (double x = 0; x < 100; x += 0.09)
            //{
               


            //    gCodeCommands.Add("G1 X" + x.ToString()+ " L" + rnd.Next(0,256)   + " F2000");
            //}
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

        private string gCodeFromBitMap(Bitmap bitmapPicture, double orgX=0.0, double orgY=0.0, int FeedRate=4500, int DPI = 72)
        {
            // open tempFile
            StreamWriter outputFile = new StreamWriter(Application.LocalUserAppDataPath + Convert.ToString("\\tmpgCode.txt"));
            string strReturn = "";
            int currentPixelColor;
            int previousPixelColor;
            int firstPixel;
            double currentX = 0.0;
            double currentY = orgY;
            double incX = Math.Round(1/(DPI/25.4),2);  // convert en mm, déterminer taille d'un pixel, arrondi au 1/100 de mm
            double targetX;   // pour le 1er pixel
            Image8Bit img = new Image8Bit(bitmapPicture);
            var draw = pictureBox1.CreateGraphics();
            var pen = new Pen(Color.LightGreen, 1.0F);
            for(int y=0; y < img.Height; y++)
            {
                draw.DrawLine(pen, 0, y, pictureBox1.Width, y);
                previousPixelColor = firstPixel = img.GetPixel(0, y).B;  // 1er pixel
                currentY = orgY + ((double)y * incX);
                outputFile.WriteLine("G0 X" + orgX.ToString("##0.00").Trim() + " Y" + currentY.ToString("##0.00").Trim() + " F" + FeedRate.ToString().Trim());
                //strReturn += "G0 X" + orgX.ToString("###.##").Trim() + " Y" + currentY.ToString("###.##").Trim() + (" F" + FeedRate).Trim();
                targetX = 0.0 - incX;   // pour le 1er pixel             
                //
                // TODO : Optimiser le sens d'impression
                for(int x=0; x< img.Width; x++)
                {
                    currentPixelColor = img.GetPixel(x, y).B;
                    if (currentPixelColor == previousPixelColor)
                    {
                        targetX = orgX + ((double)x * incX);
                    }
                    else
                    {
                        // nouvelle couleur, on clôture le mouvement.
                        outputFile.WriteLine("G1 X" + targetX.ToString("##0.00").Trim() + " L" + previousPixelColor.ToString().Trim() + " F" + FeedRate.ToString().Trim());
                        //strReturn += "G1 X" + targetX.ToString("###.##").Trim() + " L" + previousPixelColor.ToString().Trim() + (" F" + FeedRate).Trim();
                        previousPixelColor = currentPixelColor;
                        firstPixel = -1; // il y a au moins 2 couleur par ligne
                        targetX = orgX + ((double)x * incX);
                    }
                }
                if (firstPixel != -1) { // on n'a pas détecter d'autre couleur pour la ligne en cours
                    outputFile.WriteLine("G1 X" + targetX.ToString("##0.00").Trim() + " L" + previousPixelColor.ToString().Trim() + " F" + (FeedRate).ToString().Trim());
                    //strReturn += "G1 X" + targetX.ToString("###.##").Trim() + " L" + previousPixelColor.ToString().Trim() + (" F" + FeedRate).Trim();
                }
                currentY = y ;  // on considère qu'un pixel est carré
                //outputFile.WriteLine("G0 X" + orgX.ToString("##0.00").Trim() + " Y" + currentY.ToString("##0.00").Trim() + " F" + (FeedRate).ToString().Trim());
                //strReturn += "G0 X" + orgX.ToString("###.##").Trim() + " Y" + currentY.ToString("###.##").Trim() + (" F" + FeedRate).Trim();
                pictureBox1.Invalidate();
                Application.DoEvents();
            }
            outputFile.Close();
            outputFile.Dispose();
            img.Dispose();
            img = null;
            return strReturn;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            txtGCodePreview.Visible = false;
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Open Image";
            dlg.Filter = "All files (*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackgroundImage = new Bitmap(dlg.OpenFile());
                imageToPrint = new Bitmap(dlg.OpenFile());
            }

            dlg.Dispose();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            txtGCodePreview.Visible = false;
            if (pictureBox1.BackgroundImage != null)
            {
                Application.UseWaitCursor = true;
                Cursor.Current = Cursors.Default;
                Application.DoEvents();
                try
                {
                    int DPI = 72;
                    if (cbDPI.SelectedItem.ToString() != "")
                    {
                        DPI = int.Parse(cbDPI.SelectedItem.ToString());
                    }
                    string temp = "";
                    gCodeFromPicture = new List<string>();
                    temp = gCodeFromBitMap(imageToPrint,FeedRate:9000,DPI:DPI);
                    //foreach (string line in )
                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                }
                try
                {
                    StreamReader inputFile = new StreamReader(Application.LocalUserAppDataPath + Convert.ToString("\\tmpgCode.txt"));
                    txtGCodePreview.Text = inputFile.ReadToEnd();
                    inputFile.Close();
                    inputFile.Dispose();
                    Cursor.Current = Cursors.Default;
                }
                catch (Exception exc)
                {

                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            float xx = (float)pictureBox1.Width;
            float yy = (float)pictureBox1.Height;
            using (var draw = pictureBox1.CreateGraphics())
            {
                for (int y = 0; y <= pictureBox1.Height; y++)
                {
                    using (var pen = new Pen(Color.LightGreen, 1.0F))
                    {
                        draw.DrawLine(pen, 0, y,xx,y);
                    }
                    pictureBox1.Invalidate();
                }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            txtGCodePreview.Visible = true;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
            Application.DoEvents();
            Cursor.Position = Cursor.Position;
        }

    }
}
