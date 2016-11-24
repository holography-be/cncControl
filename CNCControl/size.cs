using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNCControl
{
    public partial class frmSize : Form
    {

        private Bitmap adjustedImage;
        private Single resolution;
        private Int16 DPI;
        public CNCMain frmParent;

        public frmSize()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                panel2.Enabled = false;
                numericUpDown1.Enabled = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                panel2.Enabled = true;
                numericUpDown1.Enabled = false;

            }
        }

        private void frmSize_Load(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
        }

        public void setParams(CNCMain parent, Bitmap image, Int16 iDPI, Single sResolution)
        {
            frmParent = parent;
            DPI = iDPI;
            resolution = sResolution;
            adjustedImage = image;
            txtResolution.Text = (1.00f / sResolution).ToString("#0.0");
            getImageSize();
        }

        public Bitmap retImage()
        {
            return adjustedImage;
        }

        private void getImageSize()
        {
            Single X;
            Single Y;
            Single XX;
            Single YY;
            X = adjustedImage.Width;
            Y = adjustedImage.Height;
            XX = (X / DPI) * (Single)2.54;
            YY = (Y / DPI) * (Single)2.54;
            txtWidthX.Text = X.ToString();
            txtHeightY.Text = Y.ToString();
            txtWidth.Text = XX.ToString();
            txtHeight.Text = YY.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = 100;
            getImageSize();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmParent.getNewImage(ResizeImage(Int32.Parse(txtWidthX.Text), Int32.Parse(txtHeightY.Text)));
            this.Close();
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
    }
}
