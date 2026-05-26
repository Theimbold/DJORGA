using System;
using MyApp.Domain.Entities;

namespace MyApp.Application.Interfaces.UI
{
    /// <summary>
    /// Definiert den globalen Zustand der Anwendung.
    /// </summary>
    public interface IAppStateService
    {
        bool IsLibraryEmpty { get; set; }
        bool IsPlayerVisible { get; set; }
        Track? CurrentTrack { get; set; }
        event Action? StateChanged;
    }
}
