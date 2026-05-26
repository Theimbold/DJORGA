using MyApp.Application.Interfaces.External;
using System;
using System.Collections.Generic;
using System.IO;

namespace MyApp.Infrastructure.External.Rekordbox
{
    /// <summary>
    /// Windows-spezifische Implementierung zur Pfad-Erkennung von Rekordbox.
    /// </summary>
    public class WindowsRekordboxPathService : IRekordboxPathService
    {
        public IEnumerable<string> GetDetectedXmlPaths()
        {
            var detectedPaths = new List<string>();

            // 1. Standard-Dokumente Ordner
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string pioneerDocs = Path.Combine(documentsPath, "Pioneer", "rekordbox");
            
            // Suche nach allen XML Dateien im Pioneer Dokumente Ordner
            if (Directory.Exists(pioneerDocs))
            {
                var files = Directory.GetFiles(pioneerDocs, "*.xml");
                detectedPaths.AddRange(files);
            }

            // 2. AppData Pfad (Einstellungen)
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string pioneerAppData = Path.Combine(appData, "Pioneer", "rekordbox");
            if (Directory.Exists(pioneerAppData))
            {
                // Hier liegen oft Konfigurationsdateien, falls der User dort exportiert hat
                var files = Directory.GetFiles(pioneerAppData, "rekordbox.xml");
                detectedPaths.AddRange(files);
            }

            return detectedPaths;
        }

        public string GetDefaultExportFileName() => "rekordbox_export.xml";
    }
}
