using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copeland.SensorParser.DataNormalization.Foo2.DataModel
{
    public class Device
    {
        public int DeviceID { get; set; }
        public string Name { get; set; }
        public string StartDateTime { get; set; }
        public IEnumerable<SensorData> SensorData { get; set; }
    }
}
