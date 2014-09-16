using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Logging
{
    public class NullLoggingService: ILoggingService
    {
        public void LogInfo(object logSource, string message, Exception exception = null)
        {
        }

        public void LogWarning(object logSource, string message, Exception exception = null)
        {
        }

        public void LogError(object logSource, string message, Exception exception = null)
        {
        }

        public void LogFatal(object logSource, string message, Exception exception = null)
        {
        }
    }
}
