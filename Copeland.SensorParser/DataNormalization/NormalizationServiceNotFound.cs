namespace Copeland.SensorParser.DataNormalization
{
    public class NormalizationServiceNotFound:Exception
    {
        public NormalizationServiceNotFound(): base("Normalization service not found for data object.") { }
    }
}
