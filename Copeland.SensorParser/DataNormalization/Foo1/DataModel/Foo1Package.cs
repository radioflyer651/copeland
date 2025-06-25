using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copeland.SensorParser.DataNormalization.Foo1.DataModel
{
    public class Foo1Package
    {
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public IEnumerable<Tracker> Trackers { get; set; }
    }
}
