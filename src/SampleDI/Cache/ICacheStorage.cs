using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Cache
{
    public interface ICacheStorage
    {
        void Remove(string key);

        void Set(string key, object value);

        void Set(string key, object data, DateTime absoluteExpiration, TimeSpan slidingExpiration);

        T Get<T>(string key);
    }
}
