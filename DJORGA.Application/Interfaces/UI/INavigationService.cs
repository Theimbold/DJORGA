using System;

namespace DJORGA.Application.Interfaces.UI
{
    /// <summary>
    /// Service zur Navigation zwischen verschiedenen Ansichten.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Navigiert zum angegebenen ViewModel-Typ.
        /// </summary>
        void NavigateTo<TViewModel>() where TViewModel : class;
    }
}
