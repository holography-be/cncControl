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
    partial class CNCMain
    {
        //Return true if char is a valid float digit, show eror message is not and return false
        private bool checkDigitFloat(char ch)
        {
            if ((ch != '.') & (ch != '0') & (ch != '1') & (ch != '2') & (ch != '3') & (ch != '4') & (ch != '5') & (ch != '6') & (ch != '7') & (ch != '8') & (ch != '9') & (Convert.ToByte(ch) != 8) & (Convert.ToByte(ch) != 13) & (Convert.ToByte(ch) != 27))  //is a 0-9 numbre or . decimal separator, backspace or enter
            {
                MessageBox.Show("Allowed chars are '0'-'9' and '.' as decimal separator", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }

    public static class Utils
    {
        static NumberStyles styles;

        public static string getLineWithContain(List<string> fullString, string subString)
        {
            foreach (string str in fullString)
            {
                if (str.Contains(subString))
                {
                    return str;
                }
            }
            return "";
        }

        public static string getStringValue(string fullString, string paramName)
        {
            return getValue(fullString, paramName).ToString();
        }

        public static double getValue(string fullString, string paramName)
        {
            styles = NumberStyles.Number;
            string strTemp = paramName;
            double dblTemp = 0;
            if (fullString.Contains(strTemp))
            {
                fullString += " "; // to be sure to extract last value
                strTemp = fullString.Remove(0, fullString.LastIndexOf(strTemp) + strTemp.Length);
                try
                {
                    dblTemp = Double.Parse(strTemp.Remove(strTemp.IndexOf(" ")), styles);
                }
                catch (Exception e) {
                    dblTemp = -1;
                }
            }
            return dblTemp;
        }
    }
}
