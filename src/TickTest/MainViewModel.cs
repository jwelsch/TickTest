using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using TickLib;

namespace TickTest
{
    public partial class MainViewModel : ObservableObject, ITickAction, IDisposable
    {
        private readonly ITickEngine _engine = new TickEngine();

        [ObservableProperty]
        private string? _logText;

        [ObservableProperty]
        public bool _isTimerRunning;

        [RelayCommand]
        public void Start()
        {
            IsTimerRunning = true;

            _engine.Register(this);
            _engine.Start(1000);
        }

        [RelayCommand]
        public void Stop()
        {
            System.Diagnostics.Trace.WriteLine($"Stop command");

            _engine.Stop();
            _engine.Unregister(this);

            IsTimerRunning = false;
        }

        public void Do()
        {
            Dispatcher.UIThread.Invoke(() =>
            {
                LogText += "Tick\n";
            });
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            _engine.Stop();
            _engine.Unregister(this);
        }
    }
}
