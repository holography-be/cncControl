using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNCControl
{
    class Settings
    {
        #region LASER
            public readonly Single DEFAULT_LASER_RESOLUTION = 0.2f;
            public readonly UInt16 DEFAULT_LASER_MIN = 0;
            public readonly UInt16 DEFAULT_LASER_MAX = 255;
            public readonly UInt16 DEFAULT_LASER_POWER_DIVISOR = 1;       
        #endregion

        #region GCODE
            public readonly UInt16 DEFAULT_FEEDRATE = 2500;
        #endregion



    }
}
