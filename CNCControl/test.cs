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
            //label1.Text = str;
            ((CNCMain)(this.Owner)).CurrentMode = eMode.READEEPROM;
        }

        private void test_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] Bytes = new byte[768];
            Random rnd = new Random();            
            for (int i = 0; i < 768; i++)
            {
                //Bytes[i] = (byte)rnd.Next(0, 256);
                Bytes[i] = (byte)(i % 0x100);
                textBox2.Text += Bytes[i].ToString("X2") + " ";

            }
            // convert to string
            textBox1.Text = Convert.ToBase64String(Bytes,Base64FormattingOptions.None);
            textBox2.Text = textBox2.Text.Trim();

            string hexValues = "48 65 6C 6C 15 6F 20 57 6F 72 6C 64 21";
            string[] hexValuesSplit = hexValues.Split(' ');
            foreach (String hex in hexValuesSplit)
            {
                // Convert the number expressed in base-16 to an integer.
                int value = Convert.ToInt32(hex, 16);
                // Get the character corresponding to the integral value.
                string stringValue = Char.ConvertFromUtf32(value);
                char charValue = (char)value;
                Console.WriteLine("hexadecimal value = {0}, int value = {1}, char value = {2} or {3}",
                                    hex, value, stringValue, charValue);
            }
        }

    }
}
