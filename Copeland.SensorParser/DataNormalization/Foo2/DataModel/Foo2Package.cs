using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copeland.SensorParser.DataNormalization.Foo2.DataModel
{
    public class Foo2Package
    {
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public IEnumerable<Device> Devices { get; set; }
    }
}
