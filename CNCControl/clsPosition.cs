using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNCControl
{
partial class CNCMain {
    class clsPosition
    {
        private CNCMain _gui;
        private bool _enable;

        public clsPosition(CNCMain gui)
        {
            _gui = gui;
            _enable = false;
            _gui.Connected += Connected;
        }

        private void Connected()
        {

        }

        public void Shutdown() {
            Enable = false;
        }

        public bool Enable
        {
            get { return _enable; }
            set {
                _enable = value;
                if (value == true)
                {
                    _gui.serial.AddDelegation(showPosition);
                }
                else
                {
                    _gui.serial.DeleteDelegation(showPosition);
                }
            
            }
        }

        
        public void showPosition(String data)
        {
            String[] positions;
            String[] machinePositions;
            String[] workPositions;
            String laserTemp;
            if (data[1] == '\n') return;
            if (data.Contains("MPos:") == true)
            {
                _gui.txtResults.Text = data + _gui.txtResults.Text;
                data = data.Remove(data.Length - 3, 3);
                positions = data.Split(':');
                machinePositions = positions[1].Split(',');
                workPositions = positions[2].Split(',');
                laserTemp = positions[5].Split(',')[0];
                _gui.displayX.Value = workPositions[0];
                _gui.displayY.Value = workPositions[1];
                _gui.displayZ.Value = workPositions[2];
                _gui.txtLaserTemp.Value = laserTemp;

            }
        }
    }
}

}
