using DJORGA.Application.DTOs;
using System.Threading.Tasks;

namespace DJORGA.Application.Interfaces.External
{
    /// <summary>
    /// Service zur Generierung und zum Caching von Wellenform-Daten (FrequencyPeaks).
    /// </summary>
    public interface IWaveformService
    {
        /// <summary>
        /// Extrahiert frequenz-spezifische Peak-Daten aus einer Audio-Datei.
        /// </summary>
        /// <param name="trackId">Eindeutige ID des Tracks für das Caching.</param>
        /// <param name="filePath">Pfad zur Audio-Datei.</param>
        /// <param name="peakCount">Anzahl der zu generierenden Peaks.</param>
        /// <returns>Ein Array von Frequenz-Peaks (Low, Mid, High).</returns>
        Task<FrequencyPeak[]> GetPeaksAsync(string trackId, string filePath, int peakCount = 2000);
    }
}
