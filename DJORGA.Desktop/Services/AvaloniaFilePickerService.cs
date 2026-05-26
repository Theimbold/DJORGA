using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using DJORGA.Application.Interfaces.UI;
using DJORGA.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJORGA.Desktop.Services
{
    /// <summary>
    /// Avalonia-spezifische Implementierung des Datei-Auswahl-Services.
    /// </summary>
    public class AvaloniaFilePickerService : IFilePickerService
    {
        private readonly Func<Window> _mainWindowFactory;

        public AvaloniaFilePickerService(Func<Window> mainWindowFactory)
        {
            _mainWindowFactory = mainWindowFactory;
        }

        public async Task<string?> OpenFileAsync(string title, string[] extensions)
        {
            var topLevel = TopLevel.GetTopLevel(_mainWindowFactory());
            if (topLevel == null) return null;

            var options = new FilePickerOpenOptions
            {
                Title = title,
                AllowMultiple = false,
                FileTypeFilter = new[]
                {
                    new FilePickerFileType("Rekordbox XML")
                    {
                        Patterns = extensions.Select(e => $"*.{e}").ToList()
                    }
                }
            };

            var result = await topLevel.StorageProvider.OpenFilePickerAsync(options);
            return result.Count > 0 ? result[0].Path.LocalPath : null;
        }

        public async Task<string?> SaveFileAsync(string title, string defaultExtension)
        {
            var topLevel = TopLevel.GetTopLevel(_mainWindowFactory());
            if (topLevel == null) return null;

            var files = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title = title,
                DefaultExtension = defaultExtension,
                FileTypeChoices = new[] { 
                    new FilePickerFileType($"{defaultExtension.ToUpper()} Files") { 
                        Patterns = new[] { $"*.{defaultExtension}" } 
                    } 
                }
            });

            return files?.Path.LocalPath;
        }
    }
}
