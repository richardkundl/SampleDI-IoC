using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Cache
{
    public class NoCacheStorage: ICacheStorage
    {
        public void Remove(string key)
        {
        }

        public void Set(string key, object value)
        {
        }

        public void Set(string key, object data, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
        }

        public T Get<T>(string key)
        {
            return default(T);
        }
    }
}
