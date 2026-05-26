using System.Threading.Tasks;

namespace DJORGA.Application.Interfaces.External
{
    /// <summary>
    /// Service zum Verwalten und Cachen von Cover-Art Bildern.
    /// </summary>
    public interface ICoverCacheService
    {
        /// <summary>
        /// Speichert rohe Bilddaten in einem optimierten Cache-Format.
        /// </summary>
        /// <param name="trackId">ID des Tracks für den Dateinamen.</param>
        /// <param name="imageData">Rohe Bilddaten (Byte-Array).</param>
        /// <returns>Pfad zum gespeicherten Cache-Bild.</returns>
        Task<string> CacheCoverAsync(string trackId, byte[] imageData);
    }
}
