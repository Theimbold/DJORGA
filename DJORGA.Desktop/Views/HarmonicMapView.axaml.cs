using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using DJORGA.Desktop.ViewModels;
using SkiaSharp;
using System;
using System.Linq;

namespace DJORGA.Desktop.Views
{
    public partial class HarmonicMapView : UserControl
    {
        private float _zoom = 1.0f;
        private SKPoint _offset = new SKPoint(0, 0);
        private Point? _lastMousePos;

        public HarmonicMapView()
        {
            InitializeComponent();
            
            PointerPressed += OnPointerPressed;
            PointerMoved += OnPointerMoved;
            PointerReleased += OnPointerReleased;
            PointerWheelChanged += OnPointerWheelChanged;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);
            if (DataContext is HarmonicMapViewModel vm)
            {
                vm.LoadMapCommand.Execute(null);
                vm.PropertyChanged += (s, args) => { if (args.PropertyName == "Nodes") InvalidateVisual(); };
            }
            
            _offset = new SKPoint((float)Bounds.Width / 2, (float)Bounds.Height / 2);
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);
            if (DataContext is HarmonicMapViewModel vm)
            {
                context.Custom(new HarmonicMapDrawOperation(new Rect(0, 0, Bounds.Width, Bounds.Height), vm, _zoom, _offset));
            }
        }

        private void OnPointerWheelChanged(object? sender, PointerWheelEventArgs e)
        {
            float delta = (float)e.Delta.Y;
            _zoom *= (delta > 0 ? 1.1f : 0.9f);
            _zoom = Math.Clamp(_zoom, 0.1f, 10f);
            InvalidateVisual();
        }

        private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            _lastMousePos = e.GetPosition(this);

            if (DataContext is HarmonicMapViewModel vm)
            {
                // Weltkoordinaten berechnen (unter Berücksichtigung von Zoom und Offset)
                var pos = _lastMousePos.Value;
                float worldX = (float)(pos.X - _offset.X) / _zoom;
                float worldY = (float)(pos.Y - _offset.Y) / _zoom;

                // Prüfen, ob ein Knoten getroffen wurde
                var clickedNode = vm.Nodes.FirstOrDefault(n => 
                    Math.Sqrt(Math.Pow(n.X - worldX, 2) + Math.Pow(n.Y - worldY, 2)) <= n.Radius + 5);

                vm.SelectNode(clickedNode);
            }
        }

        private void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            if (_lastMousePos.HasValue)
            {
                var pos = e.GetPosition(this);
                var diff = pos - _lastMousePos.Value;
                _offset = new SKPoint(_offset.X + (float)diff.X, _offset.Y + (float)diff.Y);
                _lastMousePos = pos;
                InvalidateVisual();
            }
        }

        private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            _lastMousePos = null;
        }

        private void OnZoomIn(object? sender, RoutedEventArgs e) { _zoom *= 1.2f; InvalidateVisual(); }
        private void OnZoomOut(object? sender, RoutedEventArgs e) { _zoom /= 1.2f; InvalidateVisual(); }
        private void OnResetView(object? sender, RoutedEventArgs e)
        {
            _zoom = 1.0f;
            _offset = new SKPoint((float)Bounds.Width / 2, (float)Bounds.Height / 2);
            InvalidateVisual();
        }
    }

    public class HarmonicMapDrawOperation : ICustomDrawOperation
    {
        private readonly HarmonicMapViewModel _vm;
        private readonly float _zoom;
        private readonly SKPoint _offset;

        public HarmonicMapDrawOperation(Rect bounds, HarmonicMapViewModel vm, float zoom, SKPoint offset)
        {
            Bounds = bounds;
            _vm = vm;
            _zoom = zoom;
            _offset = offset;
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
                DrawMap(canvas);
            }
        }

        private void DrawMap(SKCanvas canvas)
        {
            if (_vm.Nodes == null) return;

            canvas.Save();
            canvas.Translate(_offset.X, _offset.Y);
            canvas.Scale(_zoom);

            // 1. Edges
            using var edgePaint = new SKPaint
            {
                Color = SKColors.White.WithAlpha(30),
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1 / _zoom,
                IsAntialias = true
            };

            foreach (var edge in _vm.Edges)
            {
                var source = _vm.Nodes.FirstOrDefault(n => n.TrackId == edge.SourceId);
                var target = _vm.Nodes.FirstOrDefault(n => n.TrackId == edge.TargetId);
                if (source != null && target != null)
                {
                    canvas.DrawLine((float)source.X, (float)source.Y, (float)target.X, (float)target.Y, edgePaint);
                }
            }

            // 2. Nodes
            foreach (var node in _vm.Nodes)
            {
                using var nodePaint = new SKPaint
                {
                    Color = SKColor.Parse(node.Color),
                    Style = SKPaintStyle.Fill,
                    IsAntialias = true
                };
                
                canvas.DrawCircle((float)node.X, (float)node.Y, node.Radius, nodePaint);
            }

            canvas.Restore();
        }
    }
}
