using System;

namespace ParallelHelper
{
    public static class Call
    {
        public static void Once(OnceFlag flag,  Action action)
        {
            if (flag.CheckIfCalledAndSet)
            {
                action.Invoke();
            }
        }
    }
}
