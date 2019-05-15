using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.UI.UITasks
{
    public interface IUITask<EArgs> where EArgs : EventArgs
    {
        void Trigger(object sender, EArgs e);

        void Cancel(object sender, EArgs e);
    }
}
