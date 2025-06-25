using Copeland.SensorParser.DataNormalization.DataModel;

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
