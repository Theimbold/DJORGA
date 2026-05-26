using Avalonia.Data.Converters;
using Avalonia.Media;
using MyApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MyApp.Desktop.Converters
{
    public class MoodToColorConverter : IValueConverter
    {
        private static readonly Dictionary<TrackMood, Color> MoodColors = new()
        {
            { TrackMood.Melancholic, Color.Parse("#4A90E2") },
            { TrackMood.Hypnotic, Color.Parse("#00BFA5") },
            { TrackMood.Energetic, Color.Parse("#FFAB00") },
            { TrackMood.Aggressive, Color.Parse("#D50000") },
            { TrackMood.Uplifting, Color.Parse("#FF4081") },
            { TrackMood.DarkSinister, Color.Parse("#311B92") },
            { TrackMood.MinimalStripped, Color.Parse("#9E9E9E") },
            { TrackMood.OrganicWarm, Color.Parse("#7CB342") }
        };

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is TrackMood mood && MoodColors.TryGetValue(mood, out var color))
            {
                return new SolidColorBrush(color);
            }

            return Brushes.Transparent;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
