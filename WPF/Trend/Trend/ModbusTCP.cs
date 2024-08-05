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
        public float result { get; set; }

        public string ip { get; }
        public int port { get; }
        
        public ModbusTCP(string ip, int port) 
        { 
            this.client = new ModbusTcpClient();
            this.connected = false;
            this.result = 0.0f;

            this.ip = ip;
            this.port = port;

        }

    }
}
