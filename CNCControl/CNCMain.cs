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
using System.Runtime.InteropServices;


namespace CNCControl
{
    public enum eMode { CONNECTED, DISCONNECTED, RUNNING, FEEDHOLD, CYCLESTART, FINISHED, ABORTED, WAITING, READY, LOADING, SOFTRESET, INACTIVE, READEEPROM, WRITEEEPROM };
    public partial class CNCMain : Form
    {

        bool isConnect = false;
        Color defColor;
        bool bRunning;
        bool ignoreOpenError;

#region Declare Delegate
        public delegate void UpdatePositionDelegate(string str);
        UpdatePositionDelegate UpdatePositionAction;
        public delegate void UpdateComReceiveTextDelegate(string str);
        UpdateComReceiveTextDelegate UpdateComReceiveTextAction; 
#endregion

        public clsSerial serial;
        private clsPosition grblPositions;

        public delegate void ConnectHandler();
        public event ConnectHandler Connected;

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
        Bitmap adjustedImage;
        List<string> gCodeFromPicture;

        NumberStyles floatStyles;

        public CNCMain()
        {
            InitializeComponent();
            
#region Define Delegate
            UpdatePositionAction = new UpdatePositionDelegate(UpdatePosition);
#endregion
            floatStyles = NumberStyles.Number;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            defColor = txtLaserTemp.BackColor;
            // Regex pour status D[xxx;yyy;zzz;eee],C[xxx;yyy;zzz;eee],T[temp]
            PositionRegex = new Regex(
              "D\\[([-+]?[0-9]*[\\\\.,]?[0-9]*);([-+]?[0-9]*[\\\\.,]?[0" +
              "-9]*);([-+]?[0-9]*[\\\\.,]?[0-9]*);([-+]?[0-9]*[\\\\.,]?[0-9]*)\\],C\\[([-+]?[0-9]*[\\\\." +
              ",]?[0-9]*);([-+]?[0-9]*[\\\\.,]?[0-9]*);([-+]?[0-9]*[\\\\.,]" +
              "?[0-9]*);([-+]?[0-9]*[\\\\.,]?[0-9]*)\\],T\\[([-+]?[0-9]*[\\\\.,]?[0-9]*)\\],M\\[([-+]?[0-9]*[\\\\.,]?[0-9]*)\\].*",
              RegexOptions.CultureInvariant | RegexOptions.Compiled
            );
            bCancel = true;
            bRunning = false;
            cbDPI.SelectedIndex = 1;
            cbStepSize.SelectedIndex = 1;
            cbMode.SelectedIndex = 0;

            cbScan.SelectedItem = "Horizontal";
            cbPowerDivisor.SelectedItem = "1";
        }

        public void UpdatePosition(string str)
        {
            double mx, wx;
            double my, wy;
            double mz, wz;
            double me, we;
            double tempLaser;
            double memoryFree;

            if (cbUpdate.Checked != true) return;
            
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
                memoryFree = double.Parse(groups[10].Value.ToString());

                displayX.Value = string.Format("{0:0.00}", wx);
                displayY.Value = string.Format("{0:0.00}", wy);
                displayZ.Value = string.Format("{0:0.00}", wz);
                displayE.Value = string.Format("{0:0.00}", we);
                txtLaserTemp.Value = string.Format("{0:0.00}", tempLaser);
                txtMemoryFree.Text = string.Format("{0:0000}", memoryFree);

                //Debug.WriteLine(string.Format("M X={0} Y={1} Z={2}", mx, my, mz));
                //Debug.WriteLine(string.Format("W X={0} Y={1} Z={2}", wx, wy, wz));

            }
            catch (Exception ex) {
                if (!ignoreOpenError)
                {
                    MessageBox.Show(str, ex.Message);
                    ignoreOpenError = false;
                }
            }
            ignoreOpenError = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set user preferences/defaults
            Application.EnableVisualStyles();
            toolStripButton1.BackColor = Color.Red;
            toolStripButton1.ToolTipText = "";
            toolStripButton1.Text = "Connect";
            foreach (string s in SerialPort.GetPortNames())
            {
                cbPort.Items.Add(s);
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            //
            lblBrightness.Text = "0";
            lblContrast.Text = "0";
            lblGamma.Text = "1";
            serial = new clsSerial();
            grblPositions = new clsPosition(this);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (isConnect == false)
            {
                if (cbPort.SelectedItem == null)
                {
                    MessageBox.Show("Select a port,","No port selected");
                    return;
                }
                ignoreOpenError = true;
                serial.Port = cbPort.SelectedItem.ToString();
                serial.BaudRate = 115200;
                serial.TimeOut = 5000;
                serial.Connect();
                grblPositions.Enable = true;
                isConnect = true;
                toolStripButton1.Text = "Disconnect";
                TimerStatusUpdate.Enabled = true;
                return;
            }
            else 
            {
                
                TimerStatusUpdate.Enabled = false;
                grblPositions.Enable = false;
                serial.Disconnect();               
                toolStripButton1.BackColor = Color.Red;
                isConnect = false;
                toolStripButton1.Text = "Connect";
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //////////if (txtCommand.Text != "")
            //////////{
            //////////    gCodeCommands = new List<string>();
            //////////    Cursor.Current = Cursors.WaitCursor;
            //////////    txtGCodePreview.Visible = true;
            //////////    Application.DoEvents();
            //////////    pgBar.Value = 0;
            //////////    pgBar.Style = ProgressBarStyle.Marquee;
            //////////    int idx = txtCommand.Lines.Count();
            //////////    int curLine = 0;
            //////////    foreach (string str in txtCommand.Lines)
            //////////    {
            //////////        gCodeCommands.Add(str);
            //////////        pgBar.Value = 100 / (idx / ++curLine);
            //////////        Application.DoEvents();
            //////////    }
            //////////}
            //////////Cursor.Current = Cursors.Default;
            if (txtCommand.Text != "")
            {
                serial.SendData(txtCommand.Text);
            }
        }

        public void SerialWrite(string cmd)
        {
            if (isConnect == true)
            {
                serial.SendData(cmd.ToUpper());
            }
        }

        public void SerialWriteLine(string cmd)
        {
            if(isConnect == true) 
            {
                serial.SendData(cmd.ToUpper());
            }
        }

        private void updateStatus_Tick(object sender, EventArgs e)
        {
            if (cbUpdate.Checked)
            {
                if (isConnect == true)
                {
                    serial.SendData("?");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serial.SendData("$X");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtResults.Text = "";
            txtComReceive.Text = "";
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            TimerStatusUpdate.Enabled = false;
            formEEPROM = new frmEEPROM();
            formEEPROM.ShowDialog(this);
            TimerStatusUpdate.Enabled = true;
        }

        private void cbUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (!(cbUpdate.Checked))
            {
                TimerStatusUpdate.Enabled = false;
            }
            else
            {
                TimerStatusUpdate.Enabled = true;
            }
        }


        #region Joggle Control
        private void jcZeroAxes_Click(object sender, EventArgs e)
        {
            serial.SendData("G92 X0 Y0 Z0");
            curX = curY = curZ = 0;
        }

        private void jcXPlus_Click(object sender, EventArgs e)
        {
            curX += float.Parse(cbStepSize.SelectedItem.ToString(), floatStyles);
            serial.SendData("G0 X" + curX);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            curX += 10;
            serial.SendData("G0 X" + curX);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            curX -= float.Parse(cbStepSize.SelectedItem.ToString(),floatStyles);
            serial.SendData("G0 X" + curX);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            curX -= 10;
            serial.SendData("G0 X" + curX);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (txtGCodePreview.Text != "")
            {
                //SerialWriteLine("M18");
                bCancel = true;
                bRunning = false;
                string strTemp = "";
                char high;
                char low;
                var val = 0 ;
                int x;
                gCodeCommands = new List<string>();
                Cursor.Current = Cursors.WaitCursor;
                txtGCodePreview.Visible = true;
                Application.DoEvents();
                pgBar.Maximum = 100;
                pgBar.Value = 0;
                pgBar.ForeColor = Color.Blue;
                pgBar.Style = ProgressBarStyle.Marquee;
                Application.DoEvents();
                int idx = txtGCodePreview.Lines.Count();
                //pgBar.Maximum = idx;
                int curLine = 0;
                //gCodeCommands.Add("M121");
                gCodeCommands.Add("G92 X0 Y0");
                foreach (string str in txtGCodePreview.Lines)
                {                    
                    //////////x = str.IndexOf("P") + 1;
                    //////////if (str.StartsWith("A"))
                    //////////{
                    //////////    strTemp = str.Substring(0, x);
                    //////////    // convert Hexa to Binary
                    //////////    while (x < str.Length)
                    //////////    {
                    //////////        high = str.ElementAt(x++);
                    //////////        low = str.ElementAt(x++);
                    //////////        val = ((high > '9' ? high - 55 : high - 48) << 4) + (low > '9' ? low - 55 : low - 48);
                    //////////        strTemp += (char)val;
                    //////////    }
                    //////////    val = x;
                    //////////    // trail
                    //////////    strTemp += new string('\0', Const.MaxPixelPerCommandLine);
                    //////////    strTemp = strTemp.Substring(0, 197);
                    //////////}
                    //////////else
                    {
                        strTemp = str;
                    }

                    if (str != "") gCodeCommands.Add(strTemp);
                    pgBar.Value = 100/(idx / ++curLine);
                    Application.DoEvents();
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
            SerialWriteLine("M1");
            bCancel = true;
            bRunning = false;
            //CommandThread.Suspend();
            //Console.WriteLine("End: " + DateTime.Now);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            curY -= 1;
            SerialWriteLine("G0 Y" + curY);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            curY -= 10;
            SerialWriteLine("G0 Y" + curY);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            curY += 1;
            SerialWriteLine("G0 Y" + curY);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            curY += 10;
            SerialWriteLine("G0 Y" + curY);
        } 
        #endregion


        private void button14_Click(object sender, EventArgs e)
        {
            //WriteSerial("M17");
            pgBar.Value = 0;
            Application.DoEvents();
            bCancel = false;
            //Console.WriteLine("Start: " + DateTime.Now);
            CommandThread = new Thread(gCodeThread);
            CommandThread.Start();
        }

        private void gCodeThread()
        {
            bRunning = true;
            foreach (string line in gCodeCommands)
            {
                if (bCancel) break;
                try
                {
                    // si pixelSegment, on évite les CR/LF
                    //////////if (line.StartsWith("A"))
                    //////////{
                    //////////    SerialWrite(line);
                    //////////}
                    //////////else
                    {
                        SerialWriteLine(line);
                    }
                    WaitingACK = true;
                    //Console.WriteLine(DateTime.Now + " Tx: " + line);
                    while(WaitingACK == true) {  // on attend accusé de réception
                       // Console.WriteLine(DateTime.Now + " WAIT ACK");
                	    Application.DoEvents();
                        //Thread.Sleep(1);
                    }
                    if(bCancel == true) break;
                    //Invoke(UpdateComReceiveTextAction, "OK");
                    //Invoke(UpdateComReceiveTextAction, line);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
                    bCancel = true;
                    break;
                }
            }
            bRunning = false;
            //Console.WriteLine("End: " + DateTime.Now);
        }

        //////////private string gCodeFromBitMap(Bitmap bitmapPicture, double orgX=0.0, double orgY=0.0, int FeedRate=2500, int DPI = 72, int Mode = 1)
        //////////{
        //////////    string str = "";
        //////////    int x;
        //////////    int y;
        //////////    int remainPixel;
        //////////    double realX;
        //////////    double realY;
        //////////    double pixSize = 1 / (DPI / 25.4);
        //////////    int direction;
        //////////    int stepX;
        //////////    char high;
        //////////    char low;
        //////////    int currentPixel;
        //////////    Image8Bit image = new Image8Bit(bitmapPicture);
        //////////    int pixelSize = (int)(Math.Round(1 / (DPI / 25.4), 2) * 100);
        //////////    byte[][] pixelTable;
        //////////    realY = orgY;
        //////////    realX = orgX;
        //////////    pixelTable = new byte[image.Height][];
        //////////    pgBar.Minimum = 0;
        //////////    pgBar.Maximum = image.Height * image.Width;
        //////////    pgBar.ForeColor = Color.Red;
        //////////    for (y = 0; y < image.Height; y++)
        //////////    {
        //////////        pixelTable[y] = new byte[image.Width];
        //////////        for (x = 0; x < image.Width; x++)
        //////////        {
        //////////            pixelTable[y][x] = (image.GetPixel(x, y).B);
        //////////        }
        //////////        pgBar.Value = (y + 1) * x;
        //////////        Application.DoEvents();
        //////////    }
        //////////    image.Dispose();
        //////////    Application.DoEvents();
        //////////    pgBar.Value = 0;
        //////////    pgBar.Maximum = pixelTable.Length;
        //////////    pgBar.ForeColor = Color.Green;
        //////////    str = "";
        //////////    realY = orgY;
        //////////    realX = orgX;
        //////////    StreamWriter outputFile = new StreamWriter(Application.LocalUserAppDataPath + Convert.ToString("\\tmpgCode.txt"));
        //////////    for (y = 0; y < (pixelTable.Length); y++)
        //////////    {
        //////////        direction = ((y % 2) == 0 ? 1 : -1);
        //////////        realX = (direction == -1 ? realX : orgX);
        //////////        outputFile.WriteLine("G0 Y" + realY.ToString("##0.00") + " X" + realX.ToString("##0.00") + " F" + FeedRate.ToString().Trim());
        //////////        remainPixel = pixelTable[y].Length;
        //////////        x = 0;
        //////////        str = "";
        //////////        stepX = 0;
        //////////        while (remainPixel > 0)
        //////////        {
        //////////            currentPixel = pixelTable[y][(direction == 1 ? x : (pixelTable[y].Length - 1) - x)];
        //////////            high = (char)((currentPixel >> 4) + 65);
        //////////            low = (char)((currentPixel & 15) + 65);
        //////////            //Console.WriteLine(val.ToString() + " : " + high + low);
        //////////            str += high.ToString() + low.ToString();
        //////////            stepX++;
        //////////            x++;
        //////////            realX += pixSize * direction;
        //////////            if (stepX == Const.MaxPixelPerCommandLine)
        //////////            {
        //////////                outputFile.WriteLine("G5 X" + realX.ToString("##0.00").Trim() + " F" + FeedRate.ToString().Trim() + " S" + pixSize.ToString("#0.00") + " P" + str);
        //////////                stepX = 0;
        //////////                str = "";
        //////////            }
        //////////            remainPixel--;
        //////////        }
        //////////        if (str != "")
        //////////        {
        //////////            outputFile.WriteLine("A X" + realX.ToString("##0.00").Trim() + " F" + FeedRate.ToString().Trim() + " P" + str);
        //////////        }
        //////////        pgBar.Value = y + 1;
        //////////        Application.DoEvents();
        //////////        realY += ((double)pixelSize) / 100;
        //////////    }
        //////////    Application.DoEvents();
        //////////    outputFile.Close();
        //////////    outputFile.Dispose();
        //////////    Cursor.Current = Cursors.Default;









        //////////    //////////// open tempFile
        //////////    //////////StreamWriter outputFile = new StreamWriter(Application.LocalUserAppDataPath + Convert.ToString("\\tmpgCode.txt"));
        //////////    //////////string strReturn = "";
        //////////    //////////string strPixelLevel = "";
        //////////    //////////byte[] pixelLevel = new byte[150];
        //////////    //////////int currentPixelColor;
        //////////    //////////int previousPixelColor;
        //////////    //////////int firstPixel;
        //////////    //////////int totalPixel;
        //////////    //////////double currentX = 0.0;
        //////////    //////////double currentY = orgY;
        //////////    //////////double incX = Math.Round(1 / (DPI / 25.4), 2);  // convert en mm, déterminer taille d'un pixel, arrondi au 1/100 de mm
        //////////    //////////double targetX;   // pour le 1er pixel
        //////////    //////////int direction;  // optimize move, éviter un retour de ligne "vide"
        //////////    //////////Image8Bit img = new Image8Bit(bitmapPicture);
        //////////    //////////var draw = pictureBox1.CreateGraphics();
        //////////    //////////var pen = new Pen(Color.LightGreen, 1.0F);
        //////////    //////////if (Mode == 1)
        //////////    //////////{
        //////////    //////////    //outputFile.WriteLine("G0 X" + orgX.ToString("##0.00").Trim() + " Y" + (orgY).ToString("##0.00").Trim() + " F" + FeedRate.ToString().Trim());
        //////////    //////////    for (int y = 0; y < img.Height; y++)
        //////////    //////////    {
        //////////    //////////        direction = y % 2;  // 0 = 0->max, 1 = Max -> 0;
        //////////    //////////        strPixelLevel = "";
        //////////    //////////        totalPixel = 0;
        //////////    //////////        currentY = orgY + ((double)y * incX);
        //////////    //////////        currentX = ( direction == 0 ? orgX : ((double)img.Width * incX) + orgX);
        //////////    //////////        draw.DrawLine(pen, 0, y, pictureBox1.Width, y);
        //////////    //////////        int x = (direction == 0 ? 0 : img.Width - 1);
        //////////    //////////        int remainPixel = img.Width;
        //////////    //////////        outputFile.WriteLine("G0 Y" + (currentY).ToString("##0.00").Trim() + " F" + FeedRate.ToString().Trim());
        //////////    //////////        //outputFile.WriteLine("G0 X" + orgX.ToString("##0.00").Trim() + " F" + FeedRate.ToString().Trim());
        //////////    //////////        while (remainPixel > 0)
        //////////    //////////        {                        
        //////////    //////////            currentPixelColor = img.GetPixel(x, y).B;
        //////////    //////////            x = (direction == 0 ? x+1 : x-1 );
        //////////    //////////            totalPixel++;
        //////////    //////////            remainPixel--;
        //////////    //////////            strPixelLevel += currentPixelColor.ToString("X2");
        //////////    //////////            if (totalPixel == Const.MaxPixelPerCommandLine || remainPixel == 0)
        //////////    //////////            {
        //////////    //////////                // Nouveau segment
        //////////    //////////                outputFile.WriteLine("A0"  + " X" + (currentX).ToString("##0.00").Trim() + " F" + FeedRate.ToString().Trim() + " S" + incX.ToString("#0.00") + " P" + strPixelLevel);
        //////////    //////////                totalPixel = 0;
        //////////    //////////                strPixelLevel = "";
        //////////    //////////            } 
        //////////    //////////            currentX = (direction == 0 ? currentX + incX : currentX - incX);                       
        //////////    //////////        }
        //////////    //////////        pictureBox1.Invalidate();
        //////////    //////////        Application.DoEvents();
        //////////    //////////        ////outputFile.WriteLine("G0 Y" + (currentY).ToString("##0.00").Trim() + " F" + FeedRate.ToString().Trim());
        //////////    //////////        ////outputFile.WriteLine("G0 X" + orgX.ToString("##0.00").Trim() + " F" + FeedRate.ToString().Trim());
        //////////    //////////    }
        //////////    //////////}
        //////////    //////////else if (Mode == 2)
        //////////    //////////{
        //////////    //////////    for (int y = 0; y < img.Height; y++)
        //////////    //////////    {
        //////////    //////////        draw.DrawLine(pen, 0, y, pictureBox1.Width, y);
        //////////    //////////        previousPixelColor = firstPixel = img.GetPixel(0, y).B;  // 1er pixel
        //////////    //////////        currentY = orgY + ((double)y * incX);
        //////////    //////////        outputFile.WriteLine("G0 X" + orgX.ToString("##0.00").Trim() + " Y" + currentY.ToString("##0.00").Trim() + " F" + FeedRate.ToString().Trim());
        //////////    //////////        //strReturn += "G0 X" + orgX.ToString("###.##").Trim() + " Y" + currentY.ToString("###.##").Trim() + (" F" + FeedRate).Trim();
        //////////    //////////        targetX = 0.0 - incX;   // pour le 1er pixel             
        //////////    //////////        //
        //////////    //////////        // TODO : Optimiser le sens d'impression
        //////////    //////////        for (int x = 0; x < img.Width; x++)
        //////////    //////////        {
        //////////    //////////            currentPixelColor = img.GetPixel(x, y).B;
        //////////    //////////            if (currentPixelColor == previousPixelColor)
        //////////    //////////            {
        //////////    //////////                targetX = orgX + ((double)x * incX);
        //////////    //////////            }
        //////////    //////////            else
        //////////    //////////            {
        //////////    //////////                // nouvelle couleur, on clôture le mouvement.
        //////////    //////////                outputFile.WriteLine("G1 X" + targetX.ToString("##0.00").Trim() + " L" + previousPixelColor.ToString().Trim() + " F" + FeedRate.ToString().Trim());
        //////////    //////////                //strReturn += "G1 X" + targetX.ToString("###.##").Trim() + " L" + previousPixelColor.ToString().Trim() + (" F" + FeedRate).Trim();
        //////////    //////////                previousPixelColor = currentPixelColor;
        //////////    //////////                firstPixel = -1; // il y a au moins 2 couleur par ligne
        //////////    //////////                targetX = orgX + ((double)x * incX);
        //////////    //////////            }
        //////////    //////////        }
        //////////    //////////        if (firstPixel != -1)
        //////////    //////////        { // on n'a pas détecter d'autre couleur pour la ligne en cours
        //////////    //////////            outputFile.WriteLine("G1 X" + targetX.ToString("##0.00").Trim() + " L" + previousPixelColor.ToString().Trim() + " F" + (FeedRate).ToString().Trim());
        //////////    //////////            //strReturn += "G1 X" + targetX.ToString("###.##").Trim() + " L" + previousPixelColor.ToString().Trim() + (" F" + FeedRate).Trim();
        //////////    //////////        }
        //////////    //////////        //currentY = y;  // on considère qu'un pixel est carré
        //////////    //////////        //outputFile.WriteLine("G0 X" + orgX.ToString("##0.00").Trim() + " Y" + currentY.ToString("##0.00").Trim() + " F" + (FeedRate).ToString().Trim());
        //////////    //////////        //strReturn += "G0 X" + orgX.ToString("###.##").Trim() + " Y" + currentY.ToString("###.##").Trim() + (" F" + FeedRate).Trim();
        //////////    //////////        pictureBox1.Invalidate();
        //////////    //////////        Application.DoEvents();
        //////////    //////////    }
        //////////    //////////}
        //////////    //////////else
        //////////    //////////{
        //////////    //////////    // unknown Mode
        //////////    //////////}
        //////////    //////////outputFile.Close();
        //////////    //////////outputFile.Dispose();
        //////////    //////////img.Dispose();
        //////////    //////////img = null;

        //////////    return "";




        //////////    //////////outputFile.Close();
        //////////    //////////outputFile.Dispose();
        //////////    //////////img.Dispose();
        //////////    //////////img = null;
        //////////    //////////return strReturn;

        //////////    ////////// open tempFile
        //////////    ////////StreamWriter outputFile = new StreamWriter(Application.LocalUserAppDataPath + Convert.ToString("\\tmpgCode.txt"));
        //////////    ////////string strReturn = "";
        //////////    ////////int pixelLevel;
        //////////    ////////double pixelSize = Math.Round(1/(DPI/25.4),2);  // convert en mm, déterminer taille d'un pixel, arrondi au 1/100 de mm
        //////////    ////////Image8Bit img = new Image8Bit(bitmapPicture);
        //////////    ////////var draw = pictureBox1.CreateGraphics();
        //////////    ////////var pen = new Pen(Color.LightGreen, 1.0F);
        //////////    ////////for (int y = 0; y < img.Height; y++)
        //////////    ////////{
        //////////    ////////    draw.DrawLine(pen, 0, y, pictureBox1.Width, y);
        //////////    ////////    for (int x = 0; x < img.Width; x++)
        //////////    ////////    {
        //////////    ////////        // get pixel
        //////////    ////////        pixelLevel = img.GetPixel(x, y).B;
        //////////    ////////        strReturn += " " + pixelLevel.ToString();
        //////////    ////////    }
        //////////    ////////    //outputFile.WriteLine("G1 X" + targetX.ToString("##0.00").Trim() + " L" + previousPixelColor.ToString().Trim() + " F" + (FeedRate).ToString().Trim());
        //////////    ////////    outputFile.WriteLine(strReturn);
        //////////    ////////    strReturn = "";
        //////////    ////////    Application.DoEvents();
        //////////    ////////    pictureBox1.Invalidate();
        //////////    ////////}
        //////////    ////////outputFile.Close();
        //////////    ////////outputFile.Dispose();
        //////////    ////////img.Dispose();
        //////////    ////////img = null;
        //////////    ////////return strReturn;
        //////////}

        private int interpolate(int pixel, int min, int max) {
            pixel = 255 - pixel;
            Int32 dif=max-min;
            return (min + ((pixel * dif) / 255));
        }

        float lastX;
        float lastY;
        int lastPixel;
        float currentX;
        float currentY;
        int currentPixel;

        private string generateLine() {
            //Generate Gcode line
            string line = "";
            if (currentX != lastX)//Add X coord to line if is diferent from previous             
            {
                line += 'X' + string.Format(CultureInfo.InvariantCulture.NumberFormat, "{0:0.###}", currentX);
            }
            if (currentX != lastY)//Add Y coord to line if is diferent from previous
            {
                line += 'Y' +  string.Format(CultureInfo.InvariantCulture.NumberFormat, "{0:0.###}", currentY);
            }
            if (currentPixel != lastPixel)//Add power value to line if is diferent from previous
            {
                line += "S" + Convert.ToString(currentPixel);
            }
            return line;
        }

        private string gCodeFromBitMap(Bitmap bitmap, double orgX = 0.0, double orgY = 0.0, int FeedRate = 2500, int DPI = 72, int Mode = 1)
        {
            // TODO : optimiser lignes : trouver le 1er pixel non "blanc" (0) pour positioner rapidement le laser à la position du 1er pixel utile.
            int col = 0;
            int line = 0;
            int currentPixel = 0;
            int firstPixel = -1;
            int lastPixel = -1;
            int countPixel;
            int direction = 0;  // 0 = left to right, 1 = right to left
            List<String> lines = new List<string>();
            int maxPixel = Int32.Parse(txtMaxPixel.Text);
            int minPixel = Int32.Parse(txtMinPixel.Text);
            float coordX = -1;
            float coordY = -1;
            float resolution = Convert.ToSingle(txtRes.Text);
            string strCoord;
            int startX;
            int endX;
            int incX;

            // transfert pixels
            byte[][] pixelTable;
            int[][] pixelUtil;  // pour chaque ligne, 1er et dernier pixel utile (non blanc)
            pixelTable = new byte[bitmap.Height][];
            pixelUtil = new int[bitmap.Height][];
            pgBar.Minimum = 0;
            pgBar.Maximum = bitmap.Height * bitmap.Width;
            pgBar.ForeColor = Color.Red;
            try
            {
                for (line = 0; line < bitmap.Height; line++)
                {
                    pixelTable[line] = new byte[bitmap.Width];
                    pixelUtil[line] = new int[2];
                    pixelUtil[line][0] = pixelUtil[line][1] = -1;
                    lastPixel = -1;
                    firstPixel = -1;
                    for (col = 0; col < bitmap.Width; col++)
                    {
                        currentPixel = interpolate(bitmap.GetPixel(col, line).B, minPixel, maxPixel);
                        if (currentPixel != 0) {
                            if (firstPixel == -1) { // on a trouvé le 1er pixel                                
                                firstPixel = col;
                            }
                            lastPixel = col;    // on assume que c'est le dernier pixel
                        }
                        pixelTable[line][col] = (byte)currentPixel;
                    }
                    pixelUtil[line][0] = firstPixel;
                    pixelUtil[line][1] = lastPixel;
                    pgBar.Value = (line + 1) * col;
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }


            lines.Add("$28=" + cbPowerDivisor.SelectedItem.ToString().Trim());
            lines.Add("G0 X0 Y0 Z0");
            lines.Add("F" + txtFeedRate.Text);
            lines.Add("M8");
            lines.Add("M3");



            if (cbScan.SelectedItem.ToString() == "Horizontal")
            {
                pgBar.Minimum = 0;
                pgBar.Maximum = bitmap.Height;
                pgBar.ForeColor = Color.Green;
                for (line = 0; line < bitmap.Height; line++)
                {
                    coordY = (float)line * resolution;
                    strCoord = "Y" + coordY.ToString("###0.00") + "S0";
                    lines.Add(strCoord);
                    if ((pixelUtil[line][0] != -1) && (pixelUtil[line][1] != -1)) // on a au moins trouvé un pixel utile, sinon la ligne est remplie de pixels blancs
                    {
                        if (direction == 0)
                        {
                            incX = 1;
                            col = pixelUtil[line][0];   // 1er pixel
                            endX = pixelUtil[line][1] + 1;  // dernier pixel (+1 car le pixel a une superficie) 
                            coordX = (float)(col) * resolution;
                        }
                        else
                        {
                            incX = -1;
                            col = pixelUtil[line][1];   // dernier pixel mais on va de droite à gauche
                            endX = pixelUtil[line][0];  // 1er pixel
                            coordX = (float)(col + 1) * resolution;
                        }
                        // avance rapide au 1er pixel utile
                        strCoord = "X" + coordX.ToString("###0.00") + "S0";
                        lines.Add(strCoord);
                        //// Premier pixel utile (autre que 0)
                        //lastPixel = -1;
                        //int i;
                        //for (i = col;i != endX; i = i + incX ) {
                        //    lastPixel = interpolate(bitmap.GetPixel(i, line).R,minPixel,maxPixel);
                        //    if (lastPixel != 0)
                        //    {
                        //        coordX = (float)i * resolution;
                        //        strCoord = "X" + coordX.ToString("###0.00") + "S0";
                        //        lines.Add(strCoord);
                        //        break;
                        //    }
                        //}
                        //if (lastPixel != 0)
                        //{
                            //col = i + incX;
                            //lastPixel = interpolate(bitmapPicture.GetPixel(col, line).R,minPixel,maxPixel);
                        lastPixel = pixelTable[line][col];  // 1er pixel (pour commencer séquence de pixel contigu)
                            countPixel = 0;
                            col += incX;  // prochain pixel
                            for (; ; )
                            {
                                //currentPixel = interpolate(bitmap.GetPixel(col, line).R, minPixel, maxPixel);
                                currentPixel = pixelTable[line][col];
                                if (lastPixel != currentPixel)
                                {
                                    if (direction == 0)
                                    {
                                        coordX = (float)col * resolution;
                                    }
                                    else
                                    {
                                        coordX = (float)(col + 1) * resolution;
                                    }

                                    strCoord = "X" + coordX.ToString("###0.00") + "S" + lastPixel.ToString("##0");
                                    if ((countPixel > 5) && (lastPixel == 0))
                                    {
                                        strCoord += "F1000"; // +txtFeedRate.Text;
                                    }
                                    lastPixel = currentPixel;
                                    countPixel = 0;
                                    lines.Add(strCoord);
                                }
                                else
                                {
                                    countPixel++;
                                }
                                col += incX;
                                if (col == endX)
                                {
                                    coordX = (float)col * resolution;
                                    strCoord = "X" + coordX.ToString("###0.00") + "S" + lastPixel.ToString("##0");
                                    if ((countPixel > 5) && (lastPixel == 0))
                                    {
                                        strCoord += "F1000"; // +txtFeedRate.Text;
                                    }
                                    lines.Add(strCoord);
                                    break;
                                }
                            }
                        //}
                        direction = (direction == 0) ? 1 : 0;
                    }
                    pgBar.Value = (line + 1);
                    Application.DoEvents();
                }

            }
            else
            {
                // diagonal

            }

            StreamWriter outputFile = new StreamWriter(Application.LocalUserAppDataPath + Convert.ToString("\\tmpgCode.txt"),false);
            foreach (string str in lines)
            {
                outputFile.WriteLine(str);
            }
            outputFile.Close();
            outputFile.Dispose();
            Application.DoEvents();



            return "";
            //string str = "";
            //int x;
            //int y;
            //int remainPixel;
            //double realX;
            //double realY;
            //double oldX;
            //double pixSize = 25.4 / DPI;
            //int direction;
            //int stepX;
            //char high;
            //char low;
            //int currentPixel;
            //int oldPixel;
            //Bitmap image = new Bitmap(bitmapPicture);
            //int pixelSize;
            //pixelSize = (int)(Math.Round(1 / (DPI / 25.4), 2) * 100);
            //byte[][] pixelTable;
            //realY = orgY;
            //realX = orgX;
            //pixelTable = new byte[image.Height][];
            //pgBar.Minimum = 0;
            //pgBar.Maximum = image.Height * image.Width;
            //pgBar.ForeColor = Color.Red;
            //for (y = 0; y < image.Height; y++)
            //{
            //    pixelTable[y] = new byte[image.Width];
            //    for (x = 0; x < image.Width; x++)
            //    {
            //        pixelTable[y][x] = (image.GetPixel(x, y).B);
            //    }
            //    pgBar.Value = (y + 1) * x;
            //    Application.DoEvents();
            //}
            //image.Dispose();
            //Application.DoEvents();
            //pgBar.Value = 0;
            //pgBar.Maximum = pixelTable.Length;
            //pgBar.ForeColor = Color.Green;
            //str = "";
            //realY = orgY;
            //realX = orgX;
            //StreamWriter outputFile = new StreamWriter(Application.LocalUserAppDataPath + Convert.ToString("\\tmpgCode.txt"));
            ////for (realX = 0; realX < 100; realX+=0.18)
            ////{
            ////    outputFile.WriteLine("X" + realX.ToString("##0.00")); // + " F" + FeedRate.ToString().Trim());
            ////}
            //outputFile.WriteLine("G21");
            //outputFile.WriteLine("F" + FeedRate.ToString("#0").Trim());
            //outputFile.WriteLine("S0");
            //outputFile.WriteLine("G0 X0 Y0 Z0");
            //outputFile.WriteLine("M8");
            //outputFile.WriteLine("M3");
            //outputFile.WriteLine("G1");
            //direction = 1;
            //int col=0;
            //for (int line = 0; line < bitmapPicture.Height; line++) {
            //    col = (direction == 1) ? 0 : bitmapPicture.Width;
            //    for (x=0;x<bitmapPicture.Width;x++) {
            //        currentPixel = interpolate(255 - pixelTable[line][col],0,255);
            //        col--;
            //    }
            //    direction *= -1;
            //}





            //for (x = 0; x < (pixelTable.Length); x++)
            //{
            //    direction = ((x % 2) == 0 ? 1 : -1);
            //    realX = (direction == -1 ? realX : orgX);
            //    outputFile.WriteLine("G0 Y" + realY.ToString("##0.00") + " S0");
            //    remainPixel = pixelTable[x].Length;
            //    x = 0;
            //    str = "";
            //    stepX = 0;
            //    oldPixel = -1;
            //    oldX = realX;
            //    currentPixel = -1;
            //    while (remainPixel > 0)
            //    {
            //        currentPixel = pixelTable[x][(direction == 1 ? y : (pixelTable[x].Length - 1) - y)];
            //        //if (oldPixel == -1)
            //        //{
            //        //    oldPixel = currentPixel;
            //        //}
            //        //high = (char)((currentPixel >> 4) + 65);
            //        //low = (char)((currentPixel & 15) + 65);
            //        //Console.WriteLine(val.ToString() + " : " + high + low);
            //        //str += high.ToString() + low.ToString();
            //        stepX++;
            //        x++;
            //        realX += pixSize * direction;
            //        outputFile.WriteLine("X" + realX.ToString("##0.00").Trim() + " S" + (255 - currentPixel).ToString("#0").Trim());
            //        //outputFile.WriteLine("X" + realX.ToString("##0.00").Trim() + " S" + ((remainPixel % 2)*255).ToString("#0").Trim());
            //        //if (oldPixel != currentPixel)
            //        //{
            //        //    // new x coordonates
            //        //    outputFile.WriteLine("G0 X" + realX.ToString("##0.00").Trim()); // + " S" + oldPixel.ToString("#0").Trim());
            //        //    oldPixel = currentPixel;
            //        //    oldX = realX;
            //        //}
            //        remainPixel--;
            //    }
            //    // derniers pixels
            //    //outputFile.WriteLine("X" + realX.ToString("##0.00").Trim() + " S" + (255 - currentPixel).ToString("#0").Trim());
            //    pgBar.Value = y + 1;
            //    Application.DoEvents();
            //    realY += ((double)pixelSize) / 100;
            //}
            //Application.DoEvents();
            //outputFile.Close();
            //outputFile.Dispose();
            //Cursor.Current = Cursors.Default;

            //return "";


        }

        private void button16_Click(object sender, EventArgs e)
        {
            //cbRender.SelectedIndex = 0;
            txtGCodePreview.Visible = false;
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Open Image";
            dlg.Filter = "All files (*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    
                    imageToPrint = new Bitmap(dlg.OpenFile());
                    setSizeOfImage();
                    tbBrightness.Value = 0;
                    tbContrast.Value = 0;
                    tbGamma.Value = 100;
                    pictureBox1.Image = new Bitmap(imageToPrint);
                    adjustedImage = new Bitmap(imageToPrint);
                    //Refresh();
                    //userAdjust();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Image invalide","Erreur",MessageBoxButtons.OK);
                }
            }
            dlg.Dispose();
        }

        private void button17_Click(object sender, EventArgs e)
        {                    
            string temp = "";
            txtGCodePreview.Visible = false;
            if (adjustedImage != null)
            {
                Application.UseWaitCursor = true;
                Cursor.Current = Cursors.Default;
                Application.DoEvents();
                try
                {
                    int DPI = Const.DefaultDPI;
                    if (cbDPI.SelectedItem.ToString() != "")
                    {
                        DPI = int.Parse(cbDPI.SelectedItem.ToString());
                    }
                    int feedRate = Const.DefaultFeedRate;
                    if (txtFeedRate.Text != "")
                    {
                        feedRate = int.Parse(txtFeedRate.Text);
                    }
                    int mode = Const.DefaultMode;
                    if (cbMode.SelectedIndex == 1)
                    {
                        mode = 2;
                    }                   
                    temp = gCodeFromBitMap(adjustedImage,FeedRate:feedRate,DPI:DPI,Mode:mode);
                }
                catch (Exception ex)
                {
                    Application.UseWaitCursor = false;
                    Cursor = Cursors.Default;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                }
                Application.UseWaitCursor = false;
                Cursor.Current = Cursors.Default;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
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
            txtGCodePreview.Visible = true;
        }

        private void txtMaxLaserTemp_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbDPI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (imageToPrint != null) {
                setSizeOfImage();
            }

        }

        private void setSizeOfImage()
        {
            double DPI = double.Parse(cbDPI.SelectedItem.ToString());
            txtImgSizePixelX.Text = imageToPrint.Width.ToString();
            txtImgSizePixelY.Text = imageToPrint.Height.ToString();
            txtImgSizeX.Text = ((double)(imageToPrint.Width / DPI) * 2.54).ToString("0.0#"); //.ToString("0.###");
            txtImgSizeY.Text = ((double)(imageToPrint.Height / DPI) * 2.54).ToString("0.0#"); //.ToString("0.###");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            cbPort.Items.Clear();
            foreach (string s in SerialPort.GetPortNames())
            {
                cbPort.Items.Add(s);
            }
        }

        private void txtResults_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SerialWriteLine("M0");
        }

        private void tbUpdate_Scroll(object sender, EventArgs e)
        {
            txtMemoryFree.Text = tbUpdate.Value.ToString();
        }

        private void tbUpdate_ValueChanged(object sender, EventArgs e)
        {
            TimerStatusUpdate.Interval = tbUpdate.Value;
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            serial.SendData("?");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            ImageEditor frm = new ImageEditor();
            frm.loadImage(imageToPrint);
            frm.Show(this);


        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            serial.SendData("$X");
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            serial.SendData("?");
        }

        private void lblGamma_DoubleClick(object sender, EventArgs e)
        {
            tbGamma.Value = 100;
        }

        private void lblContrast_DoubleClick(object sender, EventArgs e)
        {
            tbContrast.Value = 0;
        }

        private void lblBrightness_DoubleClick(object sender, EventArgs e)
        {
            tbBrightness.Value = 0;
        }

        private void tbGamma_ValueChanged(object sender, EventArgs e)
        {
            lblGamma.Text = (tbGamma.Value/100.0f).ToString();
            Refresh();
            userAdjust();
        }

        private void tbContrast_ValueChanged(object sender, EventArgs e)
        {
            lblContrast.Text = tbContrast.Value.ToString();
            Refresh();
            userAdjust();
        }

        private void tbBrightness_ValueChanged(object sender, EventArgs e)
        {
            lblBrightness.Text = tbBrightness.Value.ToString();
            Refresh();
            userAdjust();
        }

        //Invoked when the user input any value for image adjust
        private void userAdjust()
        {
            try
            {
                if (adjustedImage == null) return;//if no image, do nothing
                //adjustedImage = imageToPrint;
                //Apply resize to original image
                Int32 xSize;//Total X pixels of resulting image for GCode generation
                Int32 ySize;//Total Y pixels of resulting image for GCode generation
                //xSize = Convert.ToInt32(float.Parse(txtImgSizePixelX.Text, CultureInfo.InvariantCulture.NumberFormat) / float.Parse(txtRes.Text, CultureInfo.InvariantCulture.NumberFormat));
                //ySize = Convert.ToInt32(float.Parse(txtImgSizePixelY.Text, CultureInfo.InvariantCulture.NumberFormat) / float.Parse(txtRes.Text, CultureInfo.InvariantCulture.NumberFormat));
                //adjustedImage = imgResize(imageToPrint, xSize, ySize);
                //Apply balance to adjusted (resized) image
                adjustedImage = imgBalance(adjustedImage, tbBrightness.Value, tbContrast.Value, tbGamma.Value);
                //Reset dirthering to adjusted (resized and balanced) image
                //cbDirthering.Text = "GrayScale 8 bit";
                //Display image
                pictureBox1.Image = adjustedImage;
                //Set preview
                //autoZoomToolStripMenuItem_Click(this, null);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error resizing/balancing image: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Adjust brightness contrast and gamma of an image      
        private Bitmap imgBalance(Bitmap img, int brigh, int cont, int gam)
        {
            lblImageAction.Text = "Balancing...";
            Refresh();
            ImageAttributes imageAttributes;
            float brightness = (brigh / 100.0f) + 1.0f;
            float contrast = (cont / 100.0f) + 1.0f;
            float gamma = 1 / (gam / 100.0f);
            float adjustedBrightness = brightness - 1.0f;
            Bitmap output;
            // create matrix that will brighten and contrast the image
            float[][] ptsArray ={
            new float[] {contrast, 0, 0, 0, 0}, // scale red
            new float[] {0, contrast, 0, 0, 0}, // scale green
            new float[] {0, 0, contrast, 0, 0}, // scale blue
            new float[] {0, 0, 0, 1.0f, 0}, // don't scale alpha
            new float[] {adjustedBrightness, adjustedBrightness, adjustedBrightness, 0, 1}};

            output = new Bitmap(img);
            imageAttributes = new ImageAttributes();
            imageAttributes.ClearColorMatrix();
            imageAttributes.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            imageAttributes.SetGamma(gamma, ColorAdjustType.Bitmap);
            Graphics g = Graphics.FromImage(output);
            g.DrawImage(output, new Rectangle(0, 0, output.Width, output.Height)
            , 0, 0, output.Width, output.Height,
            GraphicsUnit.Pixel, imageAttributes);
            lblImageAction.Text = "Done";
            Refresh();
            return (output);
        }
        //Return a grayscale version of an image
        private Bitmap imgGrayscale(Bitmap original)
        {
            lblImageAction.Text = "Grayscaling...";
            Refresh();
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);//create a blank bitmap the same size as original
            Graphics g = Graphics.FromImage(newBitmap);//get a graphics object from the new image
            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][] 
                {
                    new float[] {.299f, .299f, .299f, 0, 0},
                    new float[] {.587f, .587f, .587f, 0, 0},
                    new float[] {.114f, .114f, .114f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });
            ImageAttributes attributes = new ImageAttributes();//create some image attributes
            attributes.SetColorMatrix(colorMatrix);//set the color matrix attribute

            //draw the original image on the new image using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            g.Dispose();//dispose the Graphics object
            lblImageAction.Text = "Done";
            Refresh();
            return (newBitmap);
        }
        //Return a inverted colors version of a image
        private Bitmap imgInvert(Bitmap original)
        {
            lblImageAction.Text = "Inverting...";
            Refresh();
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);//create a blank bitmap the same size as original
            Graphics g = Graphics.FromImage(newBitmap);//get a graphics object from the new image
            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][] 
                {
                    new float[] {-1, 0, 0, 0, 0},
                    new float[] {0, -1, 0, 0, 0},
                    new float[] {0, 0, -1, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {1, 1, 1, 0, 1}
                });
            ImageAttributes attributes = new ImageAttributes();//create some image attributes
            attributes.SetColorMatrix(colorMatrix);//set the color matrix attribute

            //draw the original image on the new image using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            g.Dispose();//dispose the Graphics object
            lblImageAction.Text = "Done";
            Refresh();
            return (newBitmap);
        }

        //Resize image to desired width/height for gcode generation
        private Bitmap imgResize(Bitmap input, Int32 xSize, Int32 ySize)
        {
            //Resize
            Bitmap output;
            lblImageAction.Text = "Resizing...";
            Refresh();
            output = new Bitmap(input, new Size(xSize, ySize));
            lblImageAction.Text = "Done";
            Refresh();
            return (output);
        }

        //Apply dirthering to an image (Convert to 1 bit)
        private Bitmap imgDirther(Bitmap input)
        {
            lblImageAction.Text = "Dirthering...";
            Refresh();
            var masks = new byte[] { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };
            var output = new Bitmap(input.Width, input.Height, PixelFormat.Format1bppIndexed);
            var data = new sbyte[input.Width, input.Height];
            var inputData = input.LockBits(new Rectangle(0, 0, input.Width, input.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            try
            {
                var scanLine = inputData.Scan0;
                var line = new byte[inputData.Stride];
                for (var y = 0; y < inputData.Height; y++, scanLine += inputData.Stride)
                {
                    Marshal.Copy(scanLine, line, 0, line.Length);
                    for (var x = 0; x < input.Width; x++)
                    {
                        data[x, y] = (sbyte)(64 * (GetGreyLevel(line[x * 3 + 2], line[x * 3 + 1], line[x * 3 + 0]) - 0.5));
                    }
                }
            }
            finally
            {
                input.UnlockBits(inputData);
            }
            var outputData = output.LockBits(new Rectangle(0, 0, output.Width, output.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);
            try
            {
                var scanLine = outputData.Scan0;
                for (var y = 0; y < outputData.Height; y++, scanLine += outputData.Stride)
                {
                    var line = new byte[outputData.Stride];
                    for (var x = 0; x < input.Width; x++)
                    {
                        var j = data[x, y] > 0;
                        if (j) line[x / 8] |= masks[x % 8];
                        var error = (sbyte)(data[x, y] - (j ? 32 : -32));
                        if (x < input.Width - 1) data[x + 1, y] += (sbyte)(7 * error / 16);
                        if (y < input.Height - 1)
                        {
                            if (x > 0) data[x - 1, y + 1] += (sbyte)(3 * error / 16);
                            data[x, y + 1] += (sbyte)(5 * error / 16);
                            if (x < input.Width - 1) data[x + 1, y + 1] += (sbyte)(1 * error / 16);
                        }
                    }
                    Marshal.Copy(line, 0, scanLine, outputData.Stride);
                }
            }
            finally
            {
                output.UnlockBits(outputData);
            }
            lblImageAction.Text = "Done";
            Refresh();
            return (output);
        }

        private static double GetGreyLevel(byte r, byte g, byte b)//aux for dirthering
        {
            return (r * 0.299 + g * 0.587 + b * 0.114) / 255;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = new Bitmap(imageToPrint);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = new Bitmap(adjustedImage);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            adjustedImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox1.Image = adjustedImage;
            Refresh();
        }

        private void button9_Click_2(object sender, EventArgs e)
        {
            adjustedImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            pictureBox1.Image = adjustedImage;
            Refresh();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            adjustedImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pictureBox1.Image = adjustedImage;
            Refresh();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            adjustedImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pictureBox1.Image = adjustedImage;
            Refresh();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            adjustedImage = imgInvert(adjustedImage);
            pictureBox1.Image = adjustedImage;
            Refresh();
        }

        private void cbRender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (adjustedImage == null) return;
            if (cbRender.SelectedIndex == 1)
            {
                lblImageAction.Text = "Dirtering...";
                adjustedImage = imgDirther(adjustedImage);
                pictureBox1.Image = adjustedImage;
                lblImageAction.Text = "Done";
                Refresh();
            }
            else if(cbRender.SelectedIndex == 0)
            {
                lblImageAction.Text = "Gray scale...";
                adjustedImage = imgGrayscale(adjustedImage);
                lblImageAction.Text = "Done";
                Refresh();
                userAdjust();
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            var mate = new string[10,15];

            for (int y = 0; y < 15; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    mate[x, y] = "[" + x.ToString("00") + "," + y.ToString("00") + "]";
                }
            }
            for (int y = 0; y < 15; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    tbMatrice.Text += mate[x, y];
                }
                tbMatrice.Text += Environment.NewLine + Environment.NewLine;
            }
            Application.DoEvents();
            int col = 0;
            int line = 0;
            int maxX = 10;
            int maxY = 15;
            int curX = 0;
            int curY = 0;
            int toX = 0;
            int toY = 1;
            int dirX = 1;
            int dirY = 1;
            int lastX = 0;
            int lastY = 0;
            tbMatrice.Text = "";



        }

        private void button22_Click(object sender, EventArgs e)
        {
            // reset image
            adjustedImage = new Bitmap(imageToPrint);
            pictureBox1.Image = new Bitmap(imageToPrint);
            Refresh();
        }


    }




   
}
