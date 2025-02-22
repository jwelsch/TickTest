namespace TickLib
{
    public interface IQueuedAction
    {
        void Execute();
    }

    public interface IQueuedWaitAction : IQueuedAction
    {
        bool CanExecute();
    }

    public class QueuedAction : IQueuedAction
    {
        private readonly Action _action;

        public QueuedAction(Action action)
        {
            _action = action;
        }

        public void Execute() => _action.Invoke();
    }

    public class QueuedWaitAction : QueuedAction, IQueuedWaitAction
    {
        private readonly int _waitCount;
        private int _waitsEncountered;

        public QueuedWaitAction(Action action, int waitCount = 1)
            : base(action)
        {
            _waitCount = waitCount;
        }

        public bool CanExecute()
        {
            return _waitsEncountered++ >= _waitCount;

            //if (_waitsEncountered < _waitCount)
            //{
            //    _waitsEncountered++;
            //    return false;
            //}

            //return true;
        }
    }
}
