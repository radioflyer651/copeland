﻿using System.Text.Json.Nodes;
using Copeland.SensorParser.DataNormalization.DataModel;

namespace Copeland.SensorParser.DataNormalization
{
    /// <summary>
    /// Given a set of IDataNormalizerService objects, takes any data from sensors
    /// finds the right service, and returns the norlaized data.
    /// </summary>
    public class DataNormalizer: IDataNormalizer
    {
        private readonly IEnumerable<IDataNormalizerService> _services;

        public DataNormalizer(IEnumerable<IDataNormalizerService> services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        protected internal IDataNormalizerService? GetService(JsonObject element)
        {
            return _services.FirstOrDefault(s => s.CanHandle(element));
        }

        public IEnumerable<NormalizedData> NormalizeObject(JsonObject element)
        {
            // Get the service.
            var service = GetService(element);

            // If not found, then we can't do much.
            if (service == null)
            {
                throw new NormalizationServiceNotFoundException();
            }

            // Return the normalized data.
            return service.NormalizeData(element);
        }
    }
}