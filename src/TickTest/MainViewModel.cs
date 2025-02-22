using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TickTest
{
    public partial class MainViewModel : BaseViewModel
    {
        private Rect? _bounds;

        [ObservableProperty]
        private double _ellipseRadius = 10.0;

        [ObservableProperty]
        private double _ellipseX;

        [ObservableProperty]
        private double _ellipseY;

        [ObservableProperty]
        private bool _showLines;

        [ObservableProperty]
        private Point? _horizontalStartPoint;

        [ObservableProperty]
        private Point? _horizontalEndPoint;

        [ObservableProperty]
        private Point? _verticalStartPoint;

        [ObservableProperty]
        private Point? _verticalEndPoint;

        public void Initialize(Rect bounds)
        {
            _bounds = bounds;

            HorizontalStartPoint = new Point(bounds.Left, bounds.Center.Y);
            HorizontalEndPoint = new Point(bounds.Right, bounds.Center.Y);
            VerticalStartPoint = new Point(bounds.Center.X, bounds.Top);
            VerticalEndPoint = new Point(bounds.Center.X, bounds.Bottom);

            EllipseX = _bounds.Value.Center.X - EllipseRadius;
            EllipseY = _bounds.Value.Center.Y - EllipseRadius;

            System.Diagnostics.Trace.WriteLine($"Initialized model - EllipseX: {EllipseX}, EllipseY: {EllipseY}");
        }

        public void Start()
        {
            StartEngine(1000);
        }

        public void Stop()
        {
            StopEngine();
        }
    }
}
