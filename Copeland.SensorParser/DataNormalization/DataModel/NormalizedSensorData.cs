namespace Copeland.SensorParser.DataNormalization.DataModel
{
    public enum NormalizedSensorDataTypes
    {
        Temperature,
        Humidity
    }

    public class NormalizedSensorData
    {
        public NormalizedSensorDataTypes SensorType { get; set; }
        public float Value { get; set; }
        public DateTime DateTime { get; set; }
    }
}
