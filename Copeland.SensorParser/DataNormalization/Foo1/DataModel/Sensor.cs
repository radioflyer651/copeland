namespace Copeland.SensorParser.DataNormalization.Foo1.DataModel
{
    public class Sensor
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public IEnumerable<Crumb> Crumbs { get; set; }
    }
}
