using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Threading;


namespace CNCControl
{
    public class clsSerial
    {
        public string[] comPorts ;
        private SerialPort _port;
        public String Port = "COM4";
        public int BaudRate = 115200;
        public int TimeOut = 5000;
        public bool Connected = false;
        private StringBuilder dataLine;
        private Byte[] _dataBuffer; 

        public delegate void serialDataEventHandler(String data);
        public event serialDataEventHandler serialDataEvent;

        public delegate void DataReceivedDelegated(String Data);
        private List<DataReceivedDelegated> Delegations;


        public clsSerial()
        {
            _port = new SerialPort();
            _port.ReceivedBytesThreshold = 2;
            dataLine = new StringBuilder();
            _dataBuffer = new Byte[256];
            serialDataEvent += serialDataReceive;
            Delegations = new List<DataReceivedDelegated>();
        }

        public bool Connect() {
            if (Connected == true) {
                return false;
            }
            try
            {
                _port.PortName = Port;
                _port.BaudRate = BaudRate;
                _port.ReadTimeout = TimeOut;
                _port.Open();
            }
            catch (System.IO.IOException e)
            {
                return false;
            }
            _port.DtrEnable = true;
            _port.DtrEnable = false;
            // wait 2 seconds
            Thread.Sleep(2000);
            Connected = true;
            readAsyncData();
            return true;
        }

        public bool Disconnect()
        {
            try
            {
                if (_port.IsOpen == true)
                {
                    _port.BaseStream.Close();
                }

            } catch(Exception e) {
                System.Windows.Forms.MessageBox.Show("Erreur fermeture connexion.");
                return false;
            }
            Connected = false;
            return true;
        }

        public bool SendData(String data)
        {
            Byte[] chars;
            if ((Connected == true) && _port.IsOpen)
            {
                if (data.Length == 1)
                {
                    chars = System.Text.Encoding.GetEncoding(1252).GetBytes(data);
                    try
                    {
                        _port.BaseStream.Write(chars,0,1);
                    } catch(Exception e) {
                        System.Windows.Forms.MessageBox.Show("Erreur de connexion");
                    }
                }
                else
                {
                    chars = System.Text.Encoding.GetEncoding(1252).GetBytes(data + '\n');
                    try
                    {
                        _port.BaseStream.Write(chars, 0, chars.Length);
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show("Erreur de connexion");
                    }
                }
                return true;
            }
            return false;
        }
        
        private void serialDataReceive(string data)
        {
            foreach (Char c in data)
            {
                dataLine.Append(c);
                if (c == '\n')
                {
                    // send DataLine à tous les delegate
                    foreach (DataReceivedDelegated delegation in Delegations) {
                        delegation.Invoke(dataLine.ToString());
                    }
                    dataLine.Clear();
                }
            }
        }

        private async void readAsyncData()
        {
            try
            {
                int dataLength = await _port.BaseStream.ReadAsync(_dataBuffer,0,200);
                Byte[] _data = new Byte[dataLength];
                Buffer.BlockCopy(_dataBuffer, 0, _data, 0, dataLength);
                // lance l'event de reception
                serialDataEvent(System.Text.ASCIIEncoding.ASCII.GetString(_data));
                // relance l'attente de réception
                readAsyncData();
            }
            catch (System.IO.IOException e)
            {

            }
            catch (TimeoutException e)
            {

            }
            catch (System.InvalidOperationException e)
            {

            }

        }

        public bool AddDelegation(DataReceivedDelegated delegation)
        {
            try
            {
                Delegations.Add(delegation);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeleteDelegation(DataReceivedDelegated delegation)
        {
            try
            {
                Delegations.Remove(delegation);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
