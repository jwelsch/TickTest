namespace TickLib
{
    public interface ITickEngine
    {
        void Start(int periodMilliseconds);

        void Stop();

        void Register(ITickAction action);

        void Unregister(ITickAction action);
    }

    public class TickEngine : ITickEngine
    {
        private const int _periodMillisecondsMin = 50;

        private readonly Lock _timerLock = new();

        private System.Timers.Timer? _timer;
        private int _periodMilliseconds;
        private List<ITickAction> _actions = new();

        public void Start(int periodMilliseconds)
        {
            if (periodMilliseconds < _periodMillisecondsMin)
            {
                throw new ArgumentException($"The period cannot be less than {_periodMillisecondsMin} milliseconds.", nameof(periodMilliseconds));
            }

            lock (_timerLock)
            {
                if (_timer != null)
                {
                    return;
                }

                _periodMilliseconds = periodMilliseconds;
                _timer = new System.Timers.Timer(new TimeSpan(0, 0, 0, 0, _periodMilliseconds));
                _timer.AutoReset = true;
                _timer.Elapsed += TimerCallback;
                _timer.Start();
            }
        }

        public void Stop()
        {
            lock (_timerLock)
            {
                _timer?.Dispose();
                _timer = null;
            }
        }

        public void Register(ITickAction action)
        {
            lock (_timerLock)
            {
                for (var i = 0; i < _actions.Count; i++)
                {
                    if (_actions[i] == action)
                    {
                        return;
                    }
                }

                _actions.Add(action);
            }
        }

        public void Unregister(ITickAction action)
        {
            lock (_timerLock)
            {
                for (var i = 0; i < _actions.Count; i++)
                {
                    if (_actions[i] == action)
                    {
                        _actions.Remove(action);
                        return;
                    }
                }
            }
        }

        private void TimerCallback(object? sender, System.Timers.ElapsedEventArgs e)
        {
            // This will also prevent more than one callback executing at once.
            lock (_timerLock)
            {
                // If the timer has been stopped ignore any other callbacks.
                if (_timer == null)
                {
                    return;
                }

                var start = DateTime.Now;

                for (var i = 0; i < _actions.Count; i++)
                {
                    _actions[i].Do();
                }

                var span = DateTime.Now - start;
                System.Diagnostics.Trace.WriteLine($"Action list took {span.TotalMilliseconds} ms.");
            }
        }
    }
}
