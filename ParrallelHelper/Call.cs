using System;

namespace ParallelHelper
{
    public static class Call
    {
        public static void Once(OnceFlag flag,  Action action)
        {
            if (flag.CheckIfCalledAndSet)
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception e)
                {
                    flag.Reset();

                    throw;
                }
            }
        }

        public static void OnceSimple(OnceFlagSimple flag, Action action)
        {
            lock (flag)
            {
                try
                {
                    if (flag.CheckIfCalledAndSet)
                    {
                        action.Invoke();
                    }
                }
                catch (Exception)
                {
                    flag.Reset();

                    throw;
                }
            }
        }

    }
}
