using System.Collections.Generic;

namespace MyApp.Application.Interfaces.External
{
    /// <summary>
    /// Service zur automatischen Erkennung von Rekordbox-Installationspfaden und Datenbanken.
    /// </summary>
    public interface IRekordboxPathService
    {
        /// <summary>
        /// Sucht nach bekannten Speicherorten der Rekordbox XML-Exportdatei.
        /// </summary>
        /// <returns>Eine Liste mit gefundenen Pfaden.</returns>
        IEnumerable<string> GetDetectedXmlPaths();

        /// <summary>
        /// Gibt den Standard-Exportnamen zurück.
        /// </summary>
        string GetDefaultExportFileName();
    }
}
