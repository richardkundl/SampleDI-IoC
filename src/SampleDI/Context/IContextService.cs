using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Context
{
    public interface IContextService
    {
        string GetContextulFullFilePath(string fileName);

        string GetUserName();

        ContextProperties GetContextProperties();
    }
}
