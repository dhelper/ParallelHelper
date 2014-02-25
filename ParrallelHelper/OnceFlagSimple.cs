namespace ParallelHelper
{
    public class OnceFlagSimple
    {
        private bool _wasCalled = false;
        internal bool CheckIfCalledAndSet
        {
            get
            {
                var oldValue = _wasCalled;

                _wasCalled = true;

                return !oldValue;
            }
        }

        internal void Reset()
        {
            _wasCalled = false;
        }
    }
}