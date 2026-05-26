# Task 086: Anbindung an PlayerViewModel

## Ziel
Integration des neuen `WaveformControl` in die `PlayerView.axaml` und Bindung der `Peaks` und `Position` (Progress) an das ViewModel.

## Details
- `ItemsControl` durch `WaveformControl` ersetzen.
- Namespace in XAML registrieren (`using:MyApp.Desktop.Controls`).
- `Peaks="{Binding WaveformPeaks}"`.
- `Progress="{Binding Position}"`.
- `Duration="{Binding Duration}"`.

## Fortschritt
- [x] Namespace in `PlayerView.axaml` hinzugefügt.
- [x] `WaveformControl` eingefügt.
- [x] Bindings gesetzt.
