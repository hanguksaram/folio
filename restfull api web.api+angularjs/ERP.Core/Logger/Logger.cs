using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Logger
{
    public class Logger
    {
        private static ILogger _commonLogger;

        public static ILogger Current
        {
            get
            {
                return _commonLogger ?? (_commonLogger = new Log4NetLogger());
            }
        }
    }
}
