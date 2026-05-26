using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using MyApp.Application.DTOs;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApp.Desktop.Controls
{
    public class WaveformControl : Control
    {
        public static readonly StyledProperty<IEnumerable<FrequencyPeak>?> PeaksProperty =
            AvaloniaProperty.Register<WaveformControl, IEnumerable<FrequencyPeak>?>(nameof(Peaks));

        public static readonly StyledProperty<double> ProgressProperty =
            AvaloniaProperty.Register<WaveformControl, double>(nameof(Progress));

        public static readonly StyledProperty<double> DurationProperty =
            AvaloniaProperty.Register<WaveformControl, double>(nameof(Duration));

        public IEnumerable<FrequencyPeak>? Peaks
        {
            get => GetValue(PeaksProperty);
            set => SetValue(PeaksProperty, value);
        }

        public double Progress
        {
            get => GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        public double Duration
        {
            get => GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        static WaveformControl()
        {
            AffectsRender<WaveformControl>(PeaksProperty, ProgressProperty, DurationProperty);
        }

        public override void Render(DrawingContext context)
        {
            if (Peaks == null || !Peaks.Any()) return;

            context.Custom(new WaveformDrawOperation(new Rect(0, 0, Bounds.Width, Bounds.Height), Peaks, Progress, Duration));
        }
    }

    public class WaveformDrawOperation : ICustomDrawOperation
    {
        private readonly FrequencyPeak[] _peaks;
        private readonly double _progress;
        private readonly double _duration;

        public WaveformDrawOperation(Rect bounds, IEnumerable<FrequencyPeak> peaks, double progress, double duration)
        {
            Bounds = bounds;
            _peaks = peaks.ToArray();
            _progress = progress;
            _duration = duration;
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
                DrawWaveform(canvas);
            }
        }

        private void DrawWaveform(SKCanvas canvas)
        {
            if (_peaks.Length == 0) return;

            float width = (float)Bounds.Width;
            float height = (float)Bounds.Height;
            float centerY = height / 2;
            float peakWidth = width / _peaks.Length;

            // Colors (Rekordbox Style)
            // Low: Red (#ff0000)
            // Mid: Green (#00ff00)
            // High: Blue (#007aff)
            using var lowPaint = new SKPaint { Color = SKColor.Parse("#E81123"), Style = SKPaintStyle.Fill, IsAntialias = true };
            using var midPaint = new SKPaint { Color = SKColor.Parse("#107C10").WithAlpha(200), Style = SKPaintStyle.Fill, IsAntialias = true };
            using var highPaint = new SKPaint { Color = SKColor.Parse("#0078D7").WithAlpha(150), Style = SKPaintStyle.Fill, IsAntialias = true };
            
            // Background
            canvas.Clear(SKColors.Transparent);

            for (int i = 0; i < _peaks.Length; i++)
            {
                var peak = _peaks[i];
                float x = i * peakWidth;

                // Simple smoothing (average with neighbors)
                float low = peak.Low;
                float mid = peak.Mid;
                float high = peak.High;

                if (i > 0 && i < _peaks.Length - 1)
                {
                    low = (low + _peaks[i - 1].Low + _peaks[i + 1].Low) / 3;
                    mid = (mid + _peaks[i - 1].Mid + _peaks[i + 1].Mid) / 3;
                    high = (high + _peaks[i - 1].High + _peaks[i + 1].High) / 3;
                }

                // Scale amplitudes
                float lowH = low * centerY * 0.95f;
                float midH = mid * centerY * 0.95f;
                float highH = high * centerY * 0.95f;

                // Draw Layered (Mirrored)
                // We draw them on top of each other with alpha blending
                float rectW = peakWidth > 1 ? peakWidth - 0.5f : peakWidth;

                // 1. Low
                canvas.DrawRect(x, centerY - lowH, rectW, lowH * 2, lowPaint);
                // 2. Mid
                canvas.DrawRect(x, centerY - midH, rectW, midH * 2, midPaint);
                // 3. High
                canvas.DrawRect(x, centerY - highH, rectW, highH * 2, highPaint);
            }

            // Draw Playhead
            float progressX = (float)(_duration > 0 ? (_progress / _duration) * width : 0);
            using var playheadPaint = new SKPaint 
            { 
                Color = SKColors.White, 
                StrokeWidth = 2,
                IsAntialias = true,
                ImageFilter = SKImageFilter.CreateBlur(1, 0)
            };
            canvas.DrawLine(progressX, 0, progressX, height, playheadPaint);
            
            // Playhead "Glow"
            using var glowPaint = new SKPaint
            {
                Color = SKColors.White.WithAlpha(50),
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 4,
                IsAntialias = true
            };
            canvas.DrawLine(progressX, 0, progressX, height, glowPaint);
        }
    }
}
