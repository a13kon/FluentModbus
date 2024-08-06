using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trend.Classes
{
    public class Trends
    {
        public List<double> Xdate = new List<double>();
        public List<double> Ypreassure = new List<double>();
        public List<double> Ytemperature = new List<double>();

        public bool follow = true;
        public bool start = true;
        public Trends() { }
    }
}
