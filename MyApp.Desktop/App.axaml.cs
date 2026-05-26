using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces.External;
using MyApp.Application.Interfaces.Persistence;
using MyApp.Application.Interfaces.UI;
using MyApp.Application.Interfaces.Services;
using MyApp.Application.Services;
using MyApp.Application.UseCases.Rekordbox;
using MyApp.Application.UseCases.Library;
using MyApp.Infrastructure.External.Rekordbox;
using MyApp.Infrastructure.External.Metadata;
using MyApp.Infrastructure.External.Audio;
using MyApp.Infrastructure.Persistence.EntityFramework;
using MyApp.Infrastructure.Persistence.Repositories;
using MyApp.Desktop.ViewModels;
using MyApp.Desktop.Services;
using System;
using System.Threading.Tasks;

namespace MyApp.Desktop
{
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

            var dbContext = Services.GetRequiredService<AppDbContext>();
            // Task 095: Asynchrone Initialisierung ohne den UI-Thread zu blockieren
            _ = Task.Run(async () => 
            {
                await DbInitializer.InitializeAsync(dbContext);
            });

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
            services.AddScoped<ISmartCollectionRepository, SqliteSmartCollectionRepository>();

            // 2. Infrastructure: External & UI Services
            var rekordboxService = new RekordboxXmlService();
            services.AddSingleton<IRekordboxService>(rekordboxService);
            services.AddSingleton<IRekordboxExportService>(rekordboxService);
            services.AddSingleton<IRekordboxPathService, WindowsRekordboxPathService>();
            
            services.AddScoped<IMetadataService, TagLibMetadataService>();
            services.AddScoped<ICoverCacheService, LocalCoverCacheService>();
            services.AddScoped<IWaveformService, NAudioWaveformService>();
            services.AddSingleton<IAudioPlayerService, NAudioPlayerService>();
            
            services.AddSingleton<IAppStateService, AppStateService>();
            services.AddSingleton<INavigationService, DesktopNavigationService>();
            services.AddSingleton<IFilePickerService>(x => new AvaloniaFilePickerService(() => 
                ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current!.ApplicationLifetime!).MainWindow!));
            services.AddSingleton<Func<MainViewModel>>(x => () => x.GetRequiredService<MainViewModel>());

            // 2.1 AI Playlist Builder Services
            services.AddScoped<IHarmonicScorer, HarmonicScoringService>();
            services.AddScoped<IAiPlaylistBuilder, AiPlaylistBuilderService>();
            services.AddSingleton<IBackgroundAnalysisService, BackgroundAnalysisService>();
            services.AddScoped<RuleEvaluatorService>();
            services.AddScoped<HarmonicGraphService>();

            // 3. Application: Use Cases
            services.AddTransient<ImportRekordboxXmlUseCase>();
            services.AddTransient<UpdateTrackMetadataUseCase>();
            services.AddTransient<DeleteTrackUseCase>();
            services.AddTransient<ExportSmartCollectionUseCase>();

            // 4. UI: ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<SidebarViewModel>();
            services.AddSingleton<PlayerViewModel>();
            services.AddTransient<OnboardingViewModel>();
            services.AddTransient<ImportWizardViewModel>();
            services.AddTransient<LibraryViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<AIBuilderViewModel>();
            services.AddTransient<HarmonicMapViewModel>();
            services.AddTransient<SettingsViewModel>();
        }

        public static new App? Current => (App?)Avalonia.Application.Current;
    }
}
