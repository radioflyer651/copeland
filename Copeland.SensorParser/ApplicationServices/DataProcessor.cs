using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Copeland.SensorParser.DataNormalization;

namespace Copeland.SensorParser.ApplicationServices
{
    /// <summary>
    /// Loads data from the configured source, normalizes and combines it,
    /// and saves it back to the configured output.
    /// </summary>
    public class DataProcessor
    {
        private readonly IDataLoader _loader;
        private readonly IDataSaver _saver;
        private readonly IDataNormalizer _normalizer;

        public DataProcessor(IDataLoader loader, IDataSaver saver, IDataNormalizer normalizer)
        {
            _loader = loader ?? throw new ArgumentNullException(nameof(loader));
            _saver = saver ?? throw new ArgumentNullException(nameof(saver));
            _normalizer = normalizer ?? throw new ArgumentNullException(nameof(normalizer));
        }

        public void ProcessData()
        {
            // Load the data.
            var data = _loader.LoadData();

            // Normalize it.
            var normalizeData = data.Select(d => _normalizer.NormalizeObject(d)).SelectMany(d => d);

            // Save the data.
            _saver.Save(normalizeData);
        }
    }
}
