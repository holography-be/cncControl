using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        Bitmap OriginalImage;
        Bitmap imageToPrint;
        Bitmap adjustedImage;
        List<string> gCodeFromPicture;
        float DPI;
        float DPI_Laser;
        float ratio;

        bool reload = false;    // flag pour reload image (empêcher resize si reload)
        NumberStyles floatStyles;

        Single oldSizeX;
        Single oldSizeY;

        byte[] pixelLookup = {255,254,253,252,251,250,249,248,247,246,245,244,243,242,241,240,239,238,237,236,235,234,233,232,231,230,229,228,227,226,225,224,223,222,221,220,219,218,217,216,215,214,213,212,211,210,209,208,207,206,205,204,203,202,201,200,199,198,197,196,195,194,193,192,191,190,189,188,187,186,185,184,183,182,181,180,179,178,177,176,175,174,173,172,171,170,169,168,167,166,165,164,163,162,161,160,159,158,157,156,155,154,153,152,151,150,149,148,147,146,145,144,143,142,141,140,139,138,137,136,135,134,133,132,131,130,129,128,127,126,125,124,123,122,121,120,119,118,117,116,115,114,113,112,111,110,109,108,107,106,105,104,103,102,101,100,99,98,97,96,95,94,93,92,91,90,89,88,87,86,85,84,83,82,81,80,79,78,77,76,75,74,73,72,71,70,69,68,67,66,65,64,63,62,61,60,59,58,57,56,55,54,53,52,51,50,49,48,47,46,45,44,43,42,41,40,39,38,37,36,35,34,33,32,31,30,29,28,27,26,25,24,23,22,21,20,19,18,17,16,15,14,13,12,11,10,9,8,7,6,5,4,3,2,1,0};

        


        public CNCMain()
        {
            InitializeComponent();
            
#region Define Delegate
            UpdatePositionAction = new UpdatePositionDelegate(UpdatePosition);
#endregion
            floatStyles = NumberStyles.Number;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
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
            cbStepSize.SelectedIndex = 1;

            cbScan.SelectedItem = "Horizontal";
            cbPowerDivisor.SelectedItem = "1";
            DPI = (float)(1.00 / (Single.Parse(txtRes.Text)) * 25.4);
            DPI_Laser = Single.Parse(txtRes.Text) / 10; // Pixel par cm
            txtDPI.Text = (1.00/(Single.Parse(txtRes.Text)) * 25.4).ToString("##0");
            groupNiveaux.Enabled = false;
            panelManipImage.Enabled = false;
            groupConvert.Enabled = false;
            cbCanal.SelectedIndex = 0;

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

                //displayX.Value = string.Format("{0:0.00}", wx);
                //displayY.Value = string.Format("{0:0.00}", wy);
                //displayZ.Value = string.Format("{0:0.00}", wz);
                //displayE.Value = string.Format("{0:0.00}", we);
                //txtLaserTemp.Value = string.Format("{0:0.00}", tempLaser);
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
                            col = pixelUtil[line][1];   // dernier pixel car on va de droite à gauche
                            endX = pixelUtil[line][0];  // 1er pixel
                            coordX = (float)(col + 1) * resolution;
                        }
                        // avance rapide au 1er pixel utile
                        strCoord = "X" + coordX.ToString("###0.00") + "S0";
                        lines.Add(strCoord);
                        lastPixel = pixelTable[line][col];  // 1er pixel (pour commencer séquence de pixels contigus)
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
                    adjustedImage = null;
                    panelManipImage.Enabled = true;
                    groupConvert.Enabled = true;
                    groupNiveaux.Enabled = true;
                    cbCanal.SelectedIndex = 0;
                    rbDithering.Checked = false;
                    rbNiveaux.Checked = false;
                    tbBrightness.Value = 0;
                    tbContrast.Value = 0;
                    tbGamma.Value = 100;
                    imageToPrint = new Bitmap(dlg.OpenFile());
                    OriginalImage = new Bitmap(imageToPrint);
                    pictureBox1.Image = new Bitmap(imageToPrint);
                    adjustedImage = new Bitmap(imageToPrint);
                    //imageToPrint = ResizeImage(imageToPrint,imageToPrint.Width,imageToPrint.Height);
                    getImageSize(imageToPrint);
                    tabControl2.Enabled = true;

                    //Refresh();
                    //userAdjust();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message,"Erreur",MessageBoxButtons.OK);
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
                    int feedRate = Const.DefaultFeedRate;
                    if (txtFeedRate.Text != "")
                    {
                        feedRate = int.Parse(txtFeedRate.Text);
                    }
                    int mode = Const.DefaultMode;
             
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
            userAdjust();
        }

        private void lblContrast_DoubleClick(object sender, EventArgs e)
        {
            tbContrast.Value = 0;
            userAdjust();
        }

        private void lblBrightness_DoubleClick(object sender, EventArgs e)
        {
            tbBrightness.Value = 0;
            userAdjust();
        }

        private void tbGamma_ValueChanged(object sender, EventArgs e)
        {
            lblGamma.Text = (tbGamma.Value/100.0f).ToString();
            if (adjustedImage == null) return;
            pictureBox1.Image = imgBalance(adjustedImage, tbBrightness.Value, tbContrast.Value, tbGamma.Value);
            Refresh();
            //userAdjust();
        }

        private void tbContrast_ValueChanged(object sender, EventArgs e)
        {
            lblContrast.Text = tbContrast.Value.ToString();
            if (adjustedImage == null) return;
            pictureBox1.Image = imgBalance(adjustedImage, tbBrightness.Value, tbContrast.Value, tbGamma.Value);
            Refresh();
            //userAdjust();
        }

        private void tbBrightness_ValueChanged(object sender, EventArgs e)
        {
            lblBrightness.Text = tbBrightness.Value.ToString();
            if (adjustedImage == null) return;
            pictureBox1.Image = imgBalance(adjustedImage, tbBrightness.Value, tbContrast.Value, tbGamma.Value);
            Refresh();
            //userAdjust();
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
                xSize = Convert.ToInt32(float.Parse(txtImgSizePixelX.Text, CultureInfo.InvariantCulture.NumberFormat) / float.Parse(txtRes.Text, CultureInfo.InvariantCulture.NumberFormat));
                ySize = Convert.ToInt32(float.Parse(txtImgSizePixelY.Text, CultureInfo.InvariantCulture.NumberFormat) / float.Parse(txtRes.Text, CultureInfo.InvariantCulture.NumberFormat));
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
        private void imgGrayscale(Bitmap original, int mode)
        {
            lblImageAction.Text = "Grayscaling...";
            Refresh();
            Bitmap newBitmap;
            PixelFormat pf = PixelFormat.Format32bppArgb;
            switch(mode) 
            {
                case 0:
//                    newBitmap = GrayScaleBitmap.grayScale(adjustedImage,pf,GrayScaleBitmap.GrayScaleMethod.AUTOMATIC);
////                    adjustedImage = newBitmap;
                    newBitmap = original;
                    break;
                case 1:
                    newBitmap = GrayScaleBitmap.grayScale(adjustedImage,pf,GrayScaleBitmap.GrayScaleMethod.RED);
                    break;
                case 2:
                    newBitmap = GrayScaleBitmap.grayScale(adjustedImage,pf,GrayScaleBitmap.GrayScaleMethod.GREEN);
                    break;
                case 3:
                    newBitmap = GrayScaleBitmap.grayScale(adjustedImage,pf,GrayScaleBitmap.GrayScaleMethod.BLUE);
                    break;
                case 4: // desaturation
                    newBitmap = GrayScaleBitmap.grayScale(adjustedImage,pf,GrayScaleBitmap.GrayScaleMethod.DESATURATION);
                    break;
                case 5: // decomp min
                    newBitmap = GrayScaleBitmap.grayScale(adjustedImage,pf,GrayScaleBitmap.GrayScaleMethod.DECOMPOSITION_MIN);
                    break;
                case 6: // decomp max
                    newBitmap = GrayScaleBitmap.grayScale(adjustedImage,pf,GrayScaleBitmap.GrayScaleMethod.DECOMPOSITION_MAX);
                    break;
                default:
                    newBitmap = GrayScaleBitmap.grayScale(adjustedImage,pf,GrayScaleBitmap.GrayScaleMethod.AUTOMATIC);
                    break;
            }
            pictureBox1.Image = newBitmap;

            lblImageAction.Text = "Done";
            Refresh();
            //return (newBitmap);
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

        ////Resize image to desired width/height for gcode generation
        //private Bitmap imgResize(Bitmap input, Int32 xSize, Int32 ySize)
        //{
        //    //Resize
        //    Bitmap output;
        //    lblImageAction.Text = "Resizing...";
        //    Refresh();
        //    output = new Bitmap(input, new Size(xSize, ySize));
        //    lblImageAction.Text = "Done";
        //    Refresh();
        //    return (output);
        //}

        private void AdjustImageSize()
        {
            int xSize = Convert.ToInt32(Single.Parse(txtImgSizeX.Text) / DPI_Laser);
            int ySize = Convert.ToInt32(Single.Parse(txtImgSizeY.Text) / DPI_Laser);
            adjustedImage = ResizeImage(xSize, ySize);
            getImageSize(adjustedImage);
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

        private byte maxRGB(byte red, byte green, byte blue)
        {
            return (Math.Max(red, Math.Max(green, blue)));
        }

        private byte minRGB(byte red, byte green, byte blue)
        {
            return (Math.Min(red, Math.Min(green, blue)));
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
            getImageSize(adjustedImage);
            Refresh();
        }

        private void button9_Click_2(object sender, EventArgs e)
        {
            adjustedImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            pictureBox1.Image = adjustedImage;
            getImageSize(adjustedImage);
            Refresh();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            adjustedImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pictureBox1.Image = adjustedImage;
            getImageSize(adjustedImage);
            Refresh();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            adjustedImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pictureBox1.Image = adjustedImage;
            getImageSize(adjustedImage);
            Refresh();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            adjustedImage = imgInvert(adjustedImage);
            pictureBox1.Image = adjustedImage;
            Refresh();
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

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            var resolution = Single.Parse(txtDPI.Text);
            destImage.SetResolution(resolution,resolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void getImageSize(Bitmap image)
        {
            Single X;
            Single Y;
            Single XX;
            Single YY;
            X = image.Width;
            Y = image.Height;
            XX = (X / DPI) * (Single)2.54;
            YY = (Y / DPI) * (Single)2.54;
            oldSizeX = XX;
            oldSizeY = YY;
            ratio = X / Y;
            txtImgSizePixelX.Text = X.ToString();
            txtImgSizePixelY.Text = Y.ToString();
            txtImgSizeX.Text = XX.ToString();
            txtImgSizeY.Text = YY.ToString();
            txtRatio.Text = ratio.ToString("##0.0");
        }

        private void txtImgSizeX_Enter(object sender, EventArgs e)
        {
            oldSizeX = Single.Parse(txtImgSizeX.Text);
            reload = false;
        }

        private void txtImgSizeY_Enter(object sender, EventArgs e)
        {

            oldSizeY = Single.Parse(txtImgSizeY.Text);
            reload = false;
        }


        private void txtImgSizeX_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Prevent any not allowed char
            if (!checkDigitFloat(e.KeyChar))
            {
                e.Handled = true;//Stop the character from being entered into the control since it is non-numerical.
                return;
            }

            if (e.KeyChar == Convert.ToChar(27))
            {
                txtImgSizeX.Text = oldSizeX.ToString("##0.00");
            }
            
            if (e.KeyChar == Convert.ToChar(13))
            {
                widthChangedCheck();
            }


        }

        private void txtImgSizeY_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Prevent any not allowed char
            if (!checkDigitFloat(e.KeyChar))
            {
                e.Handled = true;//Stop the character from being entered into the control since it is non-numerical.
                return;
            }
            if (e.KeyChar == Convert.ToChar(27))
            {
                txtImgSizeY.Text = oldSizeY.ToString("##0.00");
            }
            if (e.KeyChar == Convert.ToChar(13))
            {
                widthChangedCheck();
            }
        }

        //Check if a new image width has been confirmad by user, process it.
        private void widthChangedCheck()
        {
            try
            {
                if (adjustedImage == null) return;//if no image, do nothing
                float newValue = Single.Parse(txtImgSizeX.Text);//Get the user input value           
                if (newValue == oldSizeX) return;//if not is a new value do nothing     
                //lastValue = Single.Parse(txtImgSizeX.Text);
                if (cbRatio.Checked)
                {
                    txtImgSizeY.Text = (newValue / ratio).ToString("##0.00");
                }
                txtImgSizeX.Text = newValue.ToString("##0.00");

            }
            catch
            {
                MessageBox.Show("Check width value.", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Check if a new image height has been confirmad by user, process it.
        private void heightChangedCheck()
        {
            try
            {
                if (adjustedImage == null) return;//if no image, do nothing
                float newValue = Single.Parse(txtImgSizeY.Text);//Get the user input value   
                if (newValue == oldSizeY) return;//if not is a new value do nothing
                //lastValue = Convert.ToSingle(txtImgSizeY.Text, CultureInfo.InvariantCulture.NumberFormat);
                if (cbRatio.Checked)
                {
                    txtImgSizeX.Text = (newValue * ratio).ToString("##0.00");
                }
                txtImgSizeY.Text = newValue.ToString("##0.00");

            }
            catch
            {
                MessageBox.Show("Check height value.", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtImgSizeX_Leave(object sender, EventArgs e)
        {
                widthChangedCheck();
                AdjustImageSize();
                pictureBox1.Image = adjustedImage;


        }

        private void txtImgSizeY_Leave(object sender, EventArgs e)
        {
                heightChangedCheck();
                AdjustImageSize();
                pictureBox1.Image = adjustedImage;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            frmSize theForm = new frmSize();
            theForm.setParams(this,adjustedImage, Int16.Parse(txtDPI.Text),Single.Parse(txtRes.Text));
            theForm.ShowDialog(this);
        }

        public void getNewImage(Bitmap image) {
            adjustedImage = image;
            pictureBox1.Image = adjustedImage;
            getImageSize(adjustedImage);

        }

        private void tbBrightness_MouseUp(object sender, MouseEventArgs e)
        {
            userAdjust();
        }

        private void tbContrast_MouseUp(object sender, MouseEventArgs e)
        {
            userAdjust();
        }

        private void tbGamma_MouseUp(object sender, MouseEventArgs e)
        {
            userAdjust();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            reload = true;
            adjustedImage = null;
            tbBrightness.Value = 0;
            tbContrast.Value = 0;
            tbGamma.Value = 100;
            getImageSize(imageToPrint);
            panelManipImage.Enabled = true;
            cbCanal.SelectedIndex = 0;
            rbDithering.Checked = false;
            rbNiveaux.Checked = false;
            imageToPrint = OriginalImage;
            adjustedImage = OriginalImage;
            pictureBox1.Image = OriginalImage;
        }



        private void button28_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = adjustedImage;
            rbDithering.Checked = false;
            rbNiveaux.Checked = false;
            cbCanal.SelectedIndex = 0;
        }

        private void rbNiveaux_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNiveaux.Checked == true)
            {
                adjustedImage = imageToPrint;
                AdjustImageSize();
                imgGrayscale(adjustedImage, cbCanal.SelectedIndex);
                //pictureBox1.Image = adjustedImage;
            }
        }

        private void cbCanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbNiveaux.Checked == false) return;
            adjustedImage = imageToPrint;
            AdjustImageSize();
            imgGrayscale(adjustedImage, cbCanal.SelectedIndex);
            //pictureBox1.Image = adjustedImage;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public Bitmap ResizeImage(int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            var resolution = DPI;
            destImage.SetResolution(resolution, resolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(adjustedImage, destRect, 0, 0, adjustedImage.Width, adjustedImage.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            tbMatrice.Text = "byte pixelLookup[] = (";
            for (int x = 0; x < 255; x++)
            {
                tbMatrice.Text += (255 - x).ToString() + ",";
            }
            tbMatrice.Text += ");";
        }

        private void button32_Click(object sender, EventArgs e)
        {
            AdjustImageSize();
            pictureBox1.Image = adjustedImage;
        }

        private void button33_Click(object sender, EventArgs e)
        {
            txtImgSizeX.Text = oldSizeX.ToString("##0.00");
            txtImgSizeY.Text = oldSizeY.ToString("##0.00");
        }

        private void button30_Click(object sender, EventArgs e)
        {

        }

        private void button29_Click(object sender, EventArgs e)
        {

        }

        private void button27_Click(object sender, EventArgs e)
        {
            
        }

        private void button34_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(150, 10, PixelFormat.Format32bppArgb);
            LockBitmap LockImg = new LockBitmap(img);
            LockImg.LockBits();
            Color indexColor = Color.FromArgb(255,255,255,255);
            for (int x = 0; x < 150; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    LockImg.SetPixel(x, y, indexColor);
                }
            }
            indexColor = Color.FromArgb(255, 0, 0, 0);
            for (int x = 0; x < 150; x += 20)
            {
                for (int y = 0; y < 10; y++)
                {
                    LockImg.SetPixel(x, y, indexColor);
                }
            }
            LockImg.UnlockBits();
            pictureBox1.Image = img;
        }


    }




   
}
