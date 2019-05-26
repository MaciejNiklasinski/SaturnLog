using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.UI.UITasks
{
    public abstract class UITask<EArgs> : IUITask<EArgs> where EArgs : EventArgs
    {
        public abstract void Trigger(object sender, EArgs e);

        public abstract void Cancel(object sender, EArgs e);
    }
}
