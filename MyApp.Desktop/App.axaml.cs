using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces.External;
using MyApp.Application.Interfaces.Persistence;
using MyApp.Application.UseCases.Rekordbox;
using MyApp.Infrastructure.External.Rekordbox;
using MyApp.Infrastructure.Persistence.EntityFramework;
using MyApp.Infrastructure.Persistence.Repositories;
using MyApp.Desktop.ViewModels;
using System;

namespace MyApp.Desktop
{
    // Explizite Angabe der Basisklasse, um Konflikt mit MyApp.Application Namespace zu lösen
    public partial class App : Avalonia.Application
    {
        public IServiceProvider? Services { get; private set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            Services = serviceCollection.BuildServiceProvider();

            // Datenbank initialisieren
            var dbContext = Services.GetRequiredService<AppDbContext>();
            DbInitializer.InitializeAsync(dbContext).Wait();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainVm = Services.GetRequiredService<MainViewModel>();
                desktop.MainWindow = new Views.MainWindow
                {
                    DataContext = mainVm
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // 1. Infrastructure: Persistence
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlite("Data Source=djorga.db"));
            
            services.AddScoped<ITrackRepository, SqliteTrackRepository>();
            services.AddScoped<IPlaylistRepository, SqlitePlaylistRepository>();

            // 2. Infrastructure: External Services
            services.AddScoped<IRekordboxService, RekordboxXmlService>();

            // 3. Application: Use Cases
            services.AddTransient<ImportRekordboxXmlUseCase>();

            // 4. UI: ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<LibraryViewModel>();
        }

        public static new App? Current => (App?)Avalonia.Application.Current;
    }
}
