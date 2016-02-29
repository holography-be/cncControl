using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace CNCControl
{
    public partial class frmEEPROM : Form
    {
        /*
            echo:Stored settings retrieved
            echo:Steps per unit:
            echo:CONFIG M92 X134.74 Y134.74 Z4266.66 E200.00
            echo:Maximum feedrates (mm/s):
            echo:CONFIG M203 X160.00 Y160.00 Z10.00 E10000.00
            echo:Maximum Acceleration (mm/s2):
            echo:CONFIG M201 X9000 Y9000 Z100 E10000
            echo:Acceleration: S=acceleration, T=retract acceleration
            echo:CONFIG M204 S6000.00 T6000.00
            echo:Advanced variables: S=Min feedrate (mm/s), T=Min travel feedrate (mm/s), B=minimum segment time (ms), X=maximum XY jerk (mm/s),  Z=maximum Z jerk (mm/s),  E=maximum E jerk (mm/s)
            echo:CONFIG M205 S0.00 T0.00 B20000 X10.00 Z0.50 E20.00
            echo:Home offset (mm):
            echo:CONFIG M206 X0.00 Y0.00 Z0.00 
         */

        List<string> strConfig;
        public frmCNCMain frmBase;
        NumberStyles styles;
        bool nonNumberEntered;
        int waitTime;

        public frmEEPROM()
        {
            InitializeComponent();            
            styles = NumberStyles.Number;
            waitTime = 150;
        }

        private void EEPROM_Load(object sender, EventArgs e)
        {
            strConfig = new List<string>();
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        }

        private string extractString(string from, string subString)
        {
            string strReturn = "";


            return strReturn;
        }

        private string extractString(List<string> from, string subString)
        {
            string strReturn = "";


            return strReturn;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to save in EEPROM ?", "Confirmation", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                string strCommand = "";
                strCommand = "M92 X" + txt_X_92.Text + " Y" + txt_Y_92.Text + " Z" + txt_Z_92.Text + " E" + txt_E_92.Text ;
                frmBase.sendCommand(strCommand,waitTime);
                strCommand = "M203 X" + txt_X_203.Text + " Y" + txt_Y_203.Text + " Z" + txt_Z_203.Text + " E" + txt_E_203.Text;
                frmBase.sendCommand(strCommand, waitTime);
                strCommand = "M201 X" + txt_X_201.Text + " Y" + txt_Y_201.Text + " Z" + txt_Z_201.Text + " E" + txt_E_201.Text;
                frmBase.sendCommand(strCommand, waitTime);
                strCommand = "M204 S" + txt_S_204.Text;
                frmBase.sendCommand(strCommand, waitTime);
                strCommand = "M205 X" + txt_X_205.Text + " Z" + txt_Z_205.Text + " E" + txt_E_205.Text + " S" + txt_S_205.Text + " T" + txt_T_205.Text + " B" + txt_B_205.Text;
                frmBase.sendCommand(strCommand, waitTime);
                strCommand = "M206 X" + txt_X_206.Text + " Y" + txt_Y_206.Text + " Z" + txt_Z_206.Text;
                frmBase.sendCommand(strCommand, waitTime);
                strCommand = "M210 L" + txt_MIN_210.Text + " O" + txt_OPERATION_210.Text + " H" + txt_MAX_210.Text;
                frmBase.sendCommand(strCommand, waitTime);
                // Save in EEPROM
                frmBase.sendCommand("M500",waitTime);
                System.Threading.Thread.Sleep(250);
                // reload values from EEPROM
                frmBase.sendCommand("M501",waitTime);
                System.Threading.Thread.Sleep(250);
                button1_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            strConfig.Clear();
            string strTemp = frmBase.sendCommand("M503",waitTime);
            foreach (string str in strTemp.Split('\n'))
            {
                strConfig.Add(str);
            }
            ReadConfig();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to restore default values ?", "Confirmation", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                frmBase.sendCommand("M502",waitTime);
                System.Threading.Thread.Sleep(250);
                button1_Click(sender, e);
            }
        }

        void ReadConfig() {
            string strParams = "";
            // Get M92
            strParams = Utils.getLineWithContain(strConfig, "M92");
            if (strParams != "")
            {
                txt_X_92.Text = Utils.getStringValue(strParams, "X");
                txt_Y_92.Text = Utils.getStringValue(strParams, "Y");
                txt_Z_92.Text = Utils.getStringValue(strParams, "Z");
                txt_E_92.Text = Utils.getStringValue(strParams, "E");
            }
            // Get M203
            strParams = Utils.getLineWithContain(strConfig, "M203");
            if (strParams != "")
            {
                txt_X_203.Text = Utils.getStringValue(strParams, "X");
                txt_Y_203.Text = Utils.getStringValue(strParams, "Y");
                txt_Z_203.Text = Utils.getStringValue(strParams, "Z");
                txt_E_203.Text = Utils.getStringValue(strParams, "E");
            }
            // Get M201
            strParams = Utils.getLineWithContain(strConfig, "M201");
            if (strParams != "")
            {
                txt_X_201.Text = Utils.getStringValue(strParams, "X");
                txt_Y_201.Text = Utils.getStringValue(strParams, "Y");
                txt_Z_201.Text = Utils.getStringValue(strParams, "Z");
                txt_E_201.Text = Utils.getStringValue(strParams, "E");
            }
            // Get M204
            strParams = Utils.getLineWithContain(strConfig, "M204");
            if (strParams != "")
            {
                txt_S_204.Text = Utils.getStringValue(strParams, "S");
            }
            // Get M205
            strParams = Utils.getLineWithContain(strConfig, "M205");
            if (strParams != "")
            {
                txt_S_205.Text = Utils.getStringValue(strParams, "S");
                txt_T_205.Text = Utils.getStringValue(strParams, "T");
                txt_B_205.Text = Utils.getStringValue(strParams, "B");
                txt_X_205.Text = Utils.getStringValue(strParams, "X");
                txt_Z_205.Text = Utils.getStringValue(strParams, "Z");
                txt_E_205.Text = Utils.getStringValue(strParams, "E");
            }
            // Get M206
            strParams = Utils.getLineWithContain(strConfig, "M206");
            if (strParams != "")
            {
                txt_X_206.Text = Utils.getStringValue(strParams, "X");
                txt_Y_206.Text = Utils.getStringValue(strParams, "Y");
                txt_Z_206.Text = Utils.getStringValue(strParams, "Z");
            }
            // Get M210 (Laser Temp)
            strParams = Utils.getLineWithContain(strConfig, "M210");
            if (strParams != "")
            {
                txt_MIN_210.Text = Utils.getStringValue(strParams, "L");
                txt_OPERATION_210.Text = Utils.getStringValue(strParams, "O");
                txt_MAX_210.Text = Utils.getStringValue(strParams, "H");
            }
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            // Initialize the flag to false.
            nonNumberEntered = false;
            // Determine whether the keystroke is a number from the top of the keyboard.
            if ((e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9) && (e.KeyValue != 190)&&(e.KeyValue != 188))
            {
                // Determine whether the keystroke is a number from the keypad.
                if ((e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9) && e.KeyValue != 190 && e.KeyValue != 188)
                {
                    // Determine whether the keystroke is a backspace.
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed.
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumberEntered = true;
                    }
                }
            }
            //If shift key was pressed, it's not a number.
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }
            // If already a decimal point 
            if ((e.KeyValue == 190 || e.KeyValue == 188) && ((TextBox)sender).Text.Contains(".")) nonNumberEntered = true;
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',') e.KeyChar = '.';
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Read values in EEPROM
            strConfig.Clear();
            string strTemp = frmBase.sendCommand("M501",waitTime);
            foreach (string str in strTemp.Split('\n'))
            {
                strConfig.Add(str);
            }
            ReadConfig();
        }




    }
}
