using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace MyApp.Desktop.Converters
{
    public class WaveformScaleConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is float f)
            {
                // Skaliert 0.0-1.0 auf Pixel. 
                // Parameter kann die maximale Höhe definieren (Standard 20px).
                double max = 20.0;
                if (parameter != null && double.TryParse(parameter.ToString(), out double p))
                    max = p;

                return Math.Max(1, f * max);
            }
            return 1.0;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
