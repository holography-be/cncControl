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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Open Image";
            dlg.Filter = "All files (*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    Bitmap imageToPrint = new Bitmap(dlg.OpenFile());
                    pictureBox1.Image = imageToPrint;

                    //Refresh();
                    //userAdjust();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Erreur", MessageBoxButtons.OK);
                }
            }
            dlg.Dispose();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Bitmap bit = new Bitmap(pictureBox1.Image);
            Bitmap nw = bit.toBlackAndWhite(BitmapExtension.GrayScaleMethod.AUTOMATIC);
            pictureBox1.Image = nw;
        }
    }
}
