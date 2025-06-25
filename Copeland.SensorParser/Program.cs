using System.Text.Json;
using System.Text.Json.Nodes;
using Copeland.SensorParser.ApplicationServices;
using Copeland.SensorParser.DataNormalization;
using Copeland.SensorParser.DataNormalization.Foo1;
using Copeland.SensorParser.DataNormalization.Foo2;
using Microsoft.Extensions.DependencyInjection;

namespace Copeland.SensorParser
{


    public static class Program
    {
        static Program()
        {
            _services = InitializeServices();
        }

        public static string File1Path = "DeviceDataFoo1.json";
        public static string File2Path = "DeviceDataFoo2.json";

        private static ServiceProvider _services;

        private static ServiceProvider InitializeServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<IDataNormalizerService, Foo1DataNormalizer>()
                .AddTransient<IDataNormalizerService, Foo2DataNormalizer>()
                .AddTransient<IDataNormalizer, DataNormalizer>()
                .AddTransient<IDataLoader, FileDataLoader>()
                .AddTransient<IDataSaver, FileDataSaver>()
                .AddTransient<DataProcessor>();

            return services.BuildServiceProvider();
        }

        static void Main(string[] args)
        {
            // Get the processor.
            var processor = _services.GetRequiredService<DataProcessor>();

            // Process the data.
            processor.ProcessData();
        }

    }

}
