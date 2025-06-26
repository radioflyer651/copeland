namespace Copeland.SensorParser.DataNormalization
{
    public class NormalizationServiceNotFoundException:Exception
    {
        public NormalizationServiceNotFoundException(): base("Normalization service not found for data object.") { }
    }
}
