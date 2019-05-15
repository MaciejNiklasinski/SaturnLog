using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class TaskEventArgs : System.EventArgs
    {
        public Task Task { get; private set; }

        public TaskEventArgs(Task task)
        {
            this.Task = task;
        }
    }
}
