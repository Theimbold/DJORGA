# Build Notes & Troubleshooting

## Prerequisites
- .NET 8 SDK
- Visual Studio 2022 (optional, but recommended for XAML preview) or VS Code with C# Dev Kit.

## Standard Build Commands
```bash
dotnet restore
dotnet build
dotnet test
```

## Known Issues and Fixes

### 1. Missing Dependency Injection Abstractions
If you see errors related to `IServiceScopeFactory` or `Microsoft.Extensions.DependencyInjection` in `MyApp.Application`, ensure the following package is added:
```bash
dotnet add MyApp.Application\MyApp.Application.csproj package Microsoft.Extensions.DependencyInjection.Abstractions
```
*(Fixed in baseline setup)*

### 2. Avalonia Namespace Errors (AVLN2000)
If you see `Unable to resolve type namespace alias` in `.axaml` files, check the `xmlns` definitions at the top of the file. 
Example: Ensure `xmlns:dto="using:MyApp.Application.DTOs"` is present if using `ScoredTrack`.
*(Fixed in baseline setup for AIBuilderView.axaml)*

### 3. Build Error CS1022 (Extra Brace)
A known issue in `BackgroundAnalysisService.cs` where an extra closing brace at the end of the file caused build failures.
*(Fixed in baseline setup)*

## Project Structure
- `DJORGA.sln`: Main solution file.
- `MyApp.Desktop`: Startup project.
- `djorga.db`: Local SQLite database (Auto-created on first run).

## Environment
- **OS:** Windows (Primary development target).
- **Runtime:** .NET 8.0.
