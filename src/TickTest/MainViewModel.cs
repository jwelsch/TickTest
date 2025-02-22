using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using TickLib;

namespace TickTest
{
    public partial class MainViewModel : BaseViewModel
    {
        private const int _maxTicks = 50;

        private Rect _bounds = new();
        private Velocity? _velocity;
        private PositionCalculator _positionCalculator = new();

        [ObservableProperty]
        private int _tickCount;

        [ObservableProperty]
        private double _ellipseRadius = 10.0;

        [ObservableProperty]
        private Point _ellipsePosition = new();

        [ObservableProperty]
        private bool _showLines;

        [ObservableProperty]
        private Point _horizontalStartPoint = new();

        [ObservableProperty]
        private Point _horizontalEndPoint = new();

        [ObservableProperty]
        private Point _verticalStartPoint = new();

        [ObservableProperty]
        private Point _verticalEndPoint = new();

        public void Initialize(Rect bounds)
        {
            _bounds = bounds;

            HorizontalStartPoint = new Point(_bounds.Left, _bounds.Center.Y);
            HorizontalEndPoint = new Point(_bounds.Right, _bounds.Center.Y);
            VerticalStartPoint = new Point(_bounds.Center.X, _bounds.Top);
            VerticalEndPoint = new Point(_bounds.Center.X, _bounds.Bottom);

            EllipsePosition = new Point(_bounds.Center.X - EllipseRadius, _bounds.Center.Y - EllipseRadius);
        }

        public void Start()
        {
            _velocity = new Velocity(45.0, 10.0);
            TickCount = 0;

            EnqueueAction(new QueuedAction(TickCallback));

            StartEngine(250);
        }

        public void Stop()
        {
            StopEngine();
        }

        private void TickCallback()
        {
            if (_velocity == null)
            {
                return;
            }

            TickCount++;

            //System.Diagnostics.Trace.WriteLine($"TickCallback - Calculating new position");

            EllipsePosition = _positionCalculator.NewPosition(EllipsePosition, _velocity, _bounds);

            //System.Diagnostics.Trace.WriteLine($"TickCallback - Calculated new position: {EllipsePosition}");

            if (TickCount >= _maxTicks)
            {
                Stop();
            }
            else
            {
                EnqueueAction(new QueuedWaitAction(TickCallback));
            }
        }
    }
}
