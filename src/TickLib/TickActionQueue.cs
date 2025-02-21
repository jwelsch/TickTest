namespace TickLib
{
    public interface ITickActionQueue : ITickAction
    {
        int Count { get; }

        void Enqueue(ITickAction action);
    }

    public class TickActionQueue : ITickActionQueue
    {
        private readonly Lock _sync = new();

        private Queue<ITickAction> _queue = new();

        public int Count => _queue.Count;

        public void Enqueue(ITickAction action)
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
                ITickAction? action = null;

                while (_queue.TryDequeue(out action))
                {
                    action.Do();
                }
            }
        }
    }
}
