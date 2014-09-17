using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleDI.Context
{
    public class ThreadContextService : IContextService
    {
        public string GetContextulFullFilePath(string fileName)
        {
            var dir = Directory.GetCurrentDirectory();
            var fileInfo = new FileInfo(Path.Combine(dir, fileName));
            return fileInfo.FullName;
        }

        public string GetUserName()
        {
            var userName = "null";
            try
            {
                if (Thread.CurrentPrincipal != null)
                {
                    userName = (Thread.CurrentPrincipal.Identity.IsAuthenticated
                                   ? Thread.CurrentPrincipal.Identity.Name
                                   : "null");
                }
            }
            catch
            {
            }
            return userName;
        }

        public ContextProperties GetContextProperties()
        {
            return new ContextProperties();
        }
    }
}
