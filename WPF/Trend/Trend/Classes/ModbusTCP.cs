using FluentModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trend
{
    public class ModbusTCP
    {
        public ModbusTcpClient client { get; }
        public bool connected { get; set; }
        public float[] mbFloat { get; set; }
        public int[] mbInt { get; set; }
        public bool[] mbBool {  set; get; }
        public string ip { get; }
        public int port { get; }
        
        public ModbusTCP(string ip, int port, int floatArrLength = 0, int intArrLength = 0, int boolArrLength = 0) 
        { 
            this.client = new ModbusTcpClient();
            this.connected = false;
            this.mbFloat = new float[floatArrLength];
            this.mbInt = new int[intArrLength];
            this.mbBool = new bool[boolArrLength];

            this.ip = ip;
            this.port = port;

        }

    }
}
