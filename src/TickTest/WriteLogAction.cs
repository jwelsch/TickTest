using System;
using TickLib;

namespace TickTest
{
    public class WriteLogAction : ITickAction
    {
        private Action<string> _logger;

        public string Message { get; }

        public WriteLogAction(Action<string> logger, string message)
        {
            _logger = logger;
            Message = message;
        }

        public void Do()
        {
            _logger.Invoke(Message);
        }
    }
}
