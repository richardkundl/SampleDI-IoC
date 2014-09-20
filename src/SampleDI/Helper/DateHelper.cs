using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Helper
{
    public static class DateHelper
    {
        public static long ToUnix(this DateTime time) {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0);
            var timeSpan = time.ToUniversalTime() - epoch;
            return Convert.ToInt64(timeSpan.TotalMilliseconds);
        }
    }
}
