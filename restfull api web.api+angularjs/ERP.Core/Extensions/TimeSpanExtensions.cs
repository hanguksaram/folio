using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string ToFormatedTime(this TimeSpan ts)
        {
            return string.Format("{0}h {1}m", Math.Floor(ts.TotalHours).ToString("00"), ts.Minutes.ToString("00"));
        }
    }
}
