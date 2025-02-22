using CommunityToolkit.Mvvm.ComponentModel;
using System;
using TickLib;

namespace TickTest
{
    public partial class BaseViewModel : ObservableObject, IDisposable
    {
        protected ITickEngine Engine { get; } = new TickEngine();
        protected ITickActionQueue Queue { get; } = new TickActionQueue();

        [ObservableProperty]
        public bool _isTimerRunning;

        protected BaseViewModel()
        {
            Engine.Register(Queue);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            Engine.Stop();
            Engine.Unregister(Queue);
        }

        protected void StartEngine(int intervalMs)
        {
            Engine.Start(intervalMs);
            IsTimerRunning = true;
        }

        protected void StopEngine()
        {
            IsTimerRunning = false;
            Engine.Stop();
        }
    }
}
