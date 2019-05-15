using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskExtensions
{
    public static class TaskProvider
    {
        public static async Task DelayRelativeToLastDelay(DateTime lastTaskDelayStarted, DateTime lastTaskDelayCompleted, int targetMillisecondsDelay, CancellationToken token)
        {
            // Calculate the difference between last loop task delay start and completion and ..
            int lastCycleMsDelay = (int)(lastTaskDelayStarted - lastTaskDelayCompleted).TotalMilliseconds;

            // .. subtract retrived value from target milliseconds delay
            int relativeMillisecondsDelay = targetMillisecondsDelay - lastCycleMsDelay;

            // if relativeMillisecondsDelay is lower then 1, simply return.
            if (relativeMillisecondsDelay < 1) return;
            // otherwise delay task by relativeMillisecondsDelay
            else await Task.Delay(relativeMillisecondsDelay, token);
        }
    }
}
