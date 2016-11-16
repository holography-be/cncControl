using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNCControl
{
    public partial class ImageEditor : Form
    {
        private Bitmap _img;

        public ImageEditor()
        {
            InitializeComponent();
        }

        public void loadImage(Bitmap img)
        {
            _img = img;
            pictureBox1.BackgroundImage = _img;
        }


    }
}
