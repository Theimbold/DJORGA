using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Persistence.EntityFramework
{
    /// <summary>
    /// Stellt sicher, dass die Datenbank initialisiert und bereit ist.
    /// </summary>
    public static class DbInitializer
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            // Erstellt die Datenbank und Tabellen, falls sie nicht existieren.
            // Hinweis: Für spätere Versionen mit Schema-Änderungen wird auf Migrationen umgestellt.
            await context.Database.EnsureCreatedAsync();
        }
    }
}
