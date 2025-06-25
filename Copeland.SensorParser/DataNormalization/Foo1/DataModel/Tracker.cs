namespace Copeland.SensorParser.DataNormalization.Foo1.DataModel
{
    public class Tracker
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string ShipmentStartDtm { get; set; }
        public IEnumerable<Sensor> Sensors { get; set; }

    }
}
