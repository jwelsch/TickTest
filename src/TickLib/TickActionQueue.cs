namespace TickLib
{
    public interface ITickActionQueue : ITickAction
    {
        int Count { get; }

        void Enqueue(IQueuedAction action);
    }

    public class TickActionQueue : ITickActionQueue
    {
        private readonly Lock _sync = new();
        private readonly Queue<IQueuedAction> _queue = new();

        public int Count => _queue.Count;

        public void Enqueue(IQueuedAction action)
        {
            lock (_sync)
            {
                _queue.Enqueue(action);
            }
        }

        public void Do()
        {
            lock (_sync)
            {
                IQueuedAction? action;
                var waitingActions = new List<IQueuedWaitAction>();

                while (_queue.TryDequeue(out action))
                {
                    if (action is IQueuedWaitAction waitAction
                        && !waitAction.CanExecute())
                    {
                        waitingActions.Add(waitAction);
                        continue;
                    }

                    action.Execute();
                }

                for (var i = 0; i < waitingActions.Count; i++)
                {
                    _queue.Enqueue(waitingActions[i]);
                }
            }
        }
    }
}
