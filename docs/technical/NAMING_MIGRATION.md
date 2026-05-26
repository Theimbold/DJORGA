# Naming Migration Plan: MyApp -> DJORGA

Status: **Abgeschlossen** ✓  
Abgeschlossen am: 26. Mai 2026 (E-021)

## Goal
Standardize all namespaces, project names, and folders from the generic `MyApp` prefix to the product name `DJORGA`.

## Target Structure
- `DJORGA.Domain`
- `DJORGA.Application`
- `DJORGA.Infrastructure`
- `DJORGA.Desktop`
- `DJORGA.Api`
- `DJORGA.Tests`

## Migration Progress

### Phase 1: Namespace and Reference Update
- [x] Update `DJORGA.sln` to reflect the new project names.
- [x] Update root `README.md` and `PROJECT_CONTEXT.md`.
- [x] Global search and replace `MyApp` with `DJORGA` in all source files.
    - [x] `DJORGA.Api`
    - [x] `DJORGA.Application`
    - [x] `DJORGA.Domain`
    - [x] `DJORGA.Infrastructure`
    - [x] `DJORGA.Desktop` (ViewModels, Views, Services, Controls, Converters)
    - [x] `DJORGA.Tests`

### Phase 2: File and Folder Renaming
- [x] Rename `MyApp.*.csproj` to `DJORGA.*.csproj`.
- [x] Rename `MyApp.*` directories to `DJORGA.*`.
- [x] Update `.sln` with new paths.

### Phase 3: Validation
- [x] Alle `ProjectReference`-Pfade in `.csproj`-Dateien auf `DJORGA.*` korrigiert.
- [x] Nullprüfung: kein `MyApp`-Vorkommen in `*.cs`, `*.axaml`, `*.csproj`, `*.sln`.
- [ ] `dotnet restore` — **auf Entwicklerrechner auszuführen**
- [ ] `dotnet build`  — **auf Entwicklerrechner auszuführen**
- [ ] `dotnet test`   — **auf Entwicklerrechner auszuführen**

## Validierungsbefehle (lokal ausführen)
```bash
cd <Projektordner>
dotnet restore
dotnet build --configuration Release
dotnet test
```

## Ergebnis
Alle Namespaces, `using`-Direktiven, XAML-Namespace-Deklarationen und
Projekt-Referenzen verwenden einheitlich `DJORGA.*`. Kein `MyApp`-Vorkommen
mehr in der Codebasis.
