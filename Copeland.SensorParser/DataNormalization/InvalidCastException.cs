namespace Copeland.SensorParser.DataNormalization
{
    /// <summary>
    /// Exception thrown when an IDataNormalizer is unable to cast the passed JsonObject to the expected data type, though
    /// it had the expected properties of the type.
    /// </summary>
    public class InvalidCastException : Exception
    {
        public InvalidCastException(string typeName) : base($"Unable to case JsonObject to type ${typeName}.") { }
    }
}
