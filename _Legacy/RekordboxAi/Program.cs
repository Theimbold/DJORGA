using System;
using Avalonia;
using Avalonia.ReactiveUI;

namespace RekordboxAi
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia types before AppBuilder is created.
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .With(new AvaloniaNative.ToolkitOptions { UseGpu = false })
                .LogToTrace()
                .UseReactiveUI();
    }
}
