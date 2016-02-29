using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CNCControl
{
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
