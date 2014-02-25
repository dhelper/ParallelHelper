using System;
using System.Threading;
using ParallelHelper;
using Xunit;

namespace ParallelHelperTests
{
    public class CallSimpleTests
    {
        [Fact]
        public void Once_RunActionOnce_ActionPerformed()
        {
            var flag = new OnceFlagSimple();
            var wasCalled = false;
            Call.OnceSimple(flag, () => wasCalled = true);

            Assert.True(wasCalled);
        }

        [Fact]
        public void Once_RunActionMAnyTimes_ActionPerformedOnlyOnce()
        {
            var flag = new OnceFlagSimple();
            var timesCalled = 0;
            Call.OnceSimple(flag, () => timesCalled++);
            Call.OnceSimple(flag, () => timesCalled++);
            Call.OnceSimple(flag, () => timesCalled++);
            Call.OnceSimple(flag, () => timesCalled++);

            Assert.Equal(1, timesCalled);
        }

        [Fact]
        public void Once_RunActionTwiceTheFirstThrowAnException_ExceptionPropagedToCallerAndTheSecondExecuted()
        {
            var flag = new OnceFlagSimple();

            var wasCalled = false;

            Assert.Throws<ApplicationException>(() =>
                Call.OnceSimple(flag, () =>
                {
                    throw new ApplicationException("BOOM!");
                }));

            Call.OnceSimple(flag, () => wasCalled = true);

            Assert.True(wasCalled);
        }

        [Fact(Timeout = 30000)]
        public void Once_RunActionTwiceTheFirstThrowAnExceptionAfter2ndExcuted_ExceptionPropagedToCallerAndTheSecondExecuted()
        {
            var flag = new OnceFlagSimple();

            var wasCalled = false;
            var insideThread1 = new AutoResetEvent(false);
            var insideThread2 = new AutoResetEvent(false);
            var t1 = new Thread(() =>
            {
                try
                {
                    Call.OnceSimple(flag, () =>
                    {
                        insideThread1.Set();
                        insideThread2.WaitOne();
                        throw new ApplicationException("BOOM!");
                    });
                }
                catch (Exception){}
            });
            
            t1.Start();
            insideThread1.WaitOne();

            var t2 = new Thread(() =>
            {
                insideThread2.Set();

                Call.OnceSimple(flag, () => wasCalled = true);
            });

            t2.Start();

            t1.Join();
            t2.Join();

            Assert.True(wasCalled);
        }
    }
}