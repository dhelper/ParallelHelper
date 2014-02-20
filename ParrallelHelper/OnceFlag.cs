using System.Threading;

namespace ParallelHelper
{
    public class OnceFlag
    {
        private const int NotCalled = 0;
        private const int Called = 1;
        private int _state = NotCalled;

        public bool CheckIfCalledAndSet
        {
            get
            {
                var oldValue = Interlocked.Exchange(ref _state, Called);

                return oldValue == NotCalled;
            }
        }
    }
}