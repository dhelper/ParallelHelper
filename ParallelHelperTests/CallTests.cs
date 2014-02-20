using ParallelHelper;
using Xunit;

namespace ParallelHelperTests
{
    public class CallTests
    {
        [Fact]
        public void Once_RunActionOnce_ActionPerformed()
        {
            var flag = new OnceFlag();
            var wasCalled = false;
            Call.Once(flag, () => wasCalled = true);

            Assert.True(wasCalled);
        }

        [Fact]
        public void Once_RunActionMAnyTimes_ActionPerformedOnlyOnce()
        {
            var flag = new OnceFlag();
            var timesCalled = 0;
            Call.Once(flag, () => timesCalled++);

            Assert.Equal(1, timesCalled);
        }
    }
}
