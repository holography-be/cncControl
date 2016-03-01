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
    public partial class test : Form
    {

        public delegate void SetText(string str);
        public SetText SetTextDelegate;




        public test()
        {
            InitializeComponent();
            SetTextDelegate = new SetText(SetTextMethod);
        }

        public void SetTextMethod(string str)
        {
            label1.Text = str;
            ((frmCNCMain)(this.Owner)).CurrentMode = eMode.READEEPROM;
        }

        private void test_Load(object sender, EventArgs e)
        {

        }

    }
}
