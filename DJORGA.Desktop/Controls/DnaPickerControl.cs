using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using DJORGA.Domain.ValueObjects;
using SkiaSharp;
using System;
using System.Collections.Generic;

namespace DJORGA.Desktop.Controls
{
    public class DnaPickerControl : Control
    {
        public static readonly StyledProperty<TrackMood> SelectedMoodProperty =
            AvaloniaProperty.Register<DnaPickerControl, TrackMood>(nameof(SelectedMood));

        public static readonly StyledProperty<TrackTimeContext> SelectedTimeContextProperty =
            AvaloniaProperty.Register<DnaPickerControl, TrackTimeContext>(nameof(SelectedTimeContext));

        public TrackMood SelectedMood
        {
            get => GetValue(SelectedMoodProperty);
            set => SetValue(SelectedMoodProperty, value);
        }

        public TrackTimeContext SelectedTimeContext
        {
            get => GetValue(SelectedTimeContextProperty);
            set => SetValue(SelectedTimeContextProperty, value);
        }

        static DnaPickerControl()
        {
            AffectsRender<DnaPickerControl>(SelectedMoodProperty, SelectedTimeContextProperty);
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);
            var pos = e.GetPosition(this);
            UpdateSelectionFromPoint(pos);
        }

        private void UpdateSelectionFromPoint(Point p)
        {
            double cellWidth = Bounds.Width / 8;
            double cellHeight = Bounds.Height / 8;

            int col = (int)(p.X / cellWidth);
            int row = (int)(p.Y / cellHeight);

            if (col >= 0 && col < 8 && row >= 0 && row < 8)
            {
                // Mood is Rows (1-8)
                // TimeContext is Cols (1-8)
                SelectedMood = (TrackMood)(row + 1);
                SelectedTimeContext = (TrackTimeContext)(col + 1);
            }
        }

        public override void Render(DrawingContext context)
        {
            context.Custom(new DnaPickerDrawOperation(new Rect(0, 0, Bounds.Width, Bounds.Height), SelectedMood, SelectedTimeContext));
        }
    }

    public class DnaPickerDrawOperation : ICustomDrawOperation
    {
        private readonly TrackMood _selectedMood;
        private readonly TrackTimeContext _selectedTimeContext;

        private static readonly Dictionary<TrackMood, SKColor> MoodColors = new()
        {
            { TrackMood.Melancholic, SKColor.Parse("#4A90E2") },     // Blue
            { TrackMood.Hypnotic, SKColor.Parse("#00BFA5") },        // Teal
            { TrackMood.Energetic, SKColor.Parse("#FFAB00") },       // Amber
            { TrackMood.Aggressive, SKColor.Parse("#D50000") },      // Red
            { TrackMood.Uplifting, SKColor.Parse("#FF4081") },       // Pink
            { TrackMood.DarkSinister, SKColor.Parse("#311B92") },    // Deep Purple
            { TrackMood.MinimalStripped, SKColor.Parse("#9E9E9E") }, // Grey
            { TrackMood.OrganicWarm, SKColor.Parse("#7CB342") }      // Light Green
        };

        public DnaPickerDrawOperation(Rect bounds, TrackMood selectedMood, TrackTimeContext selectedTimeContext)
        {
            Bounds = bounds;
            _selectedMood = selectedMood;
            _selectedTimeContext = selectedTimeContext;
        }

        public Rect Bounds { get; }

        public void Dispose() { }

        public bool Equals(ICustomDrawOperation? other) => false;

        public bool HitTest(Point p) => false;

        public void Render(ImmediateDrawingContext context)
        {
            var leaseFeature = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            if (leaseFeature == null) return;

            using (var lease = leaseFeature.Lease())
            {
                var canvas = lease.SkCanvas;
                DrawPicker(canvas);
            }
        }

        private void DrawPicker(SKCanvas canvas)
        {
            float width = (float)Bounds.Width;
            float height = (float)Bounds.Height;
            float cellW = width / 8;
            float cellH = height / 8;

            canvas.Clear(SKColors.Transparent);

            for (int row = 0; row < 8; row++)
            {
                var mood = (TrackMood)(row + 1);
                if (!MoodColors.TryGetValue(mood, out var baseColor)) baseColor = SKColors.Gray;

                for (int col = 0; col < 8; col++)
                {
                    var time = (TrackTimeContext)(col + 1);
                    
                    // Adjust brightness based on TimeContext (col)
                    // Sunrise (1) -> Peak (6) -> LateNight (7) -> Afterhour (8)
                    float brightnessFactor = 0.5f + (col * 0.1f); 
                    if (col > 5) brightnessFactor = 1.0f - ((col - 5) * 0.2f); // After Peak it gets darker

                    var cellColor = AdjustBrightness(baseColor, brightnessFactor);
                    
                    float x = col * cellW;
                    float y = row * cellH;
                    float padding = 2f;

                    bool isSelected = (mood == _selectedMood && time == _selectedTimeContext);

                    using var paint = new SKPaint
                    {
                        Color = cellColor,
                        Style = SKPaintStyle.Fill,
                        IsAntialias = true
                    };

                    var rect = new SKRect(x + padding, y + padding, x + cellW - padding, y + cellH - padding);
                    float cornerRadius = 4f;
                    canvas.DrawRoundRect(rect, cornerRadius, cornerRadius, paint);

                    if (isSelected)
                    {
                        using var borderPaint = new SKPaint
                        {
                            Color = SKColors.White,
                            Style = SKPaintStyle.Stroke,
                            StrokeWidth = 2f,
                            IsAntialias = true
                        };
                        canvas.DrawRoundRect(rect, cornerRadius, cornerRadius, borderPaint);

                        // Selection Glow
                        using var glowPaint = new SKPaint
                        {
                            Color = SKColors.White.WithAlpha(100),
                            Style = SKPaintStyle.Stroke,
                            StrokeWidth = 4f,
                            IsAntialias = true,
                            ImageFilter = SKImageFilter.CreateBlur(2, 2)
                        };
                        canvas.DrawRoundRect(rect, cornerRadius, cornerRadius, glowPaint);
                    }
                }
            }
        }

        private SKColor AdjustBrightness(SKColor color, float factor)
        {
            float h, s, l;
            color.ToHsl(out h, out s, out l);
            l = Math.Max(0, Math.Min(100, l * factor));
            return SKColor.FromHsl(h, s, l);
        }
    }
}
