﻿using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using TickLib;

namespace TickTest
{
    public partial class MainViewModel : ObservableObject, IDisposable
    {
        private readonly ITickEngine _engine = new TickEngine();
        private readonly ITickActionQueue _queue = new TickActionQueue();

        [ObservableProperty]
        private int _number = 0;

        [ObservableProperty]
        private string? _logText;

        [ObservableProperty]
        public bool _isTimerRunning;

        [RelayCommand]
        public void Start()
        {
            IsTimerRunning = true;

            _engine.Register(_queue);
            _engine.Start(1000);
        }

        [RelayCommand]
        public void Stop()
        {
            System.Diagnostics.Trace.WriteLine($"Stop command");

            _engine.Stop();
            _engine.Unregister(_queue);

            IsTimerRunning = false;
        }

        [RelayCommand]
        public void Enqueue()
        {
            _queue.Enqueue(new WriteLogAction(WriteLog, Number.ToString()));
            Number++;
        }

        private void WriteLog(string message)
        {
            LogText += $"{message}\n";
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            _engine.Stop();
            _engine.Unregister(_queue);
        }
    }
}
