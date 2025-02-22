using CommunityToolkit.Mvvm.ComponentModel;
using System;
using TickLib;

namespace TickTest
{
    public partial class BaseViewModel : ObservableObject, IDisposable
    {
        private readonly ITickEngine _engine = new TickEngine();
        private readonly ITickActionQueue _queue = new TickActionQueue();

        [ObservableProperty]
        public bool _isTimerRunning;

        protected BaseViewModel()
        {
            _engine.Register(_queue);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            _engine.Stop();
            _engine.Unregister(_queue);
        }

        protected void StartEngine(int intervalMs)
        {
            _engine.Start(intervalMs);
            IsTimerRunning = true;
        }

        protected void StopEngine()
        {
            IsTimerRunning = false;
            _engine.Stop();
        }

        protected void EnqueueAction(IQueuedAction action)
        {
            _queue.Enqueue(action);
        }
    }
}
