using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copeland.SensorParser.DataNormalization
{
    public class NormalizationServiceNotFound:Exception
    {
        public NormalizationServiceNotFound(): base("Normalization service not found for data object.") { }
    }
}
