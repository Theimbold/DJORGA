using MyApp.Application.DTOs;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.External
{
    /// <summary>
    /// Service zur Extraktion von Metadaten direkt aus Audio-Dateien.
    /// </summary>
    public interface IMetadataService
    {
        /// <summary>
        /// Extrahiert Metadaten (Tags, Cover) aus der angegebenen Datei.
        /// </summary>
        Task<TrackMetadata> ExtractMetadataAsync(string filePath);

        /// <summary>
        /// Aktualisiert die Metadaten in der physischen Audio-Datei.
        /// </summary>
        Task UpdateMetadataAsync(string filePath, TrackMetadata metadata);
    }
}
