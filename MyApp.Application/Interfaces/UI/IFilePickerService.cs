using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.UI
{
    /// <summary>
    /// Service zum Öffnen von Datei-Auswahl-Dialogen.
    /// </summary>
    public interface IFilePickerService
    {
        /// <summary>
        /// Öffnet einen Dialog zur Auswahl einer einzelnen Datei.
        /// </summary>
        /// <param name="title">Titel des Dialogs.</param>
        /// <param name="extensions">Liste der erlaubten Dateiendungen (z.B. "xml").</param>
        /// <returns>Der vollständige Pfad zur Datei oder null, falls abgebrochen wurde.</returns>
        Task<string?> OpenFileAsync(string title, string[] extensions);
        /// <summary>
        /// Öffnet einen Dialog zum Speichern einer Datei.
        /// </summary>
        Task<string?> SaveFileAsync(string title, string defaultExtension);
    }
}
