using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class ExceptionEventArgs : ExceptionEventArgs<Exception>
    {
        public ExceptionEventArgs(Exception exception) : base(exception) 
        {

        }
    }


    public class ExceptionEventArgs<Ex> : System.EventArgs where Ex : Exception
    {
        public Ex Exception { get; private set; }

        public ExceptionEventArgs(Ex exception)
        {
            this.Exception = exception;
        }
    }

    public class ExceptionsEventArgs : ExceptionsEventArgs<Exception>
    {
        public ExceptionsEventArgs(IList<Exception> exceptions) : base(exceptions)
        {

        }
    }

    public class ExceptionsEventArgs<Ex> : System.EventArgs where Ex : Exception
    {
        public IList<Ex> Exceptions { get; private set; }

        public ExceptionsEventArgs(IList<Ex> exceptions)
        {

            this.Exceptions = exceptions;
        }
    }
}
