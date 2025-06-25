using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Copeland.SensorParser.DataNormalization;

namespace Copeland.SensorParser.ApplicationServices
{
    /// <summary>
    /// Saves NormalizedData sets to specialized media.
    /// </summary>
    public interface IDataSaver
    {
        /// <summary>
        /// Saves the data to its medium.
        /// </summary>
        /// <param name="data"></param>
        void Save(IEnumerable<NormalizedData> data);
    }
}
