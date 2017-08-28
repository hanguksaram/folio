using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Logger
{
    using log4net;

    public class Log4NetLogger : ILogger
    {
        private ILog _logger;

        public Log4NetLogger(string logName = "default")
        {
            try
            {
                this._logger = LogManager.GetLogger(logName);
                log4net.Config.XmlConfigurator.Configure();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }

        public void Info(string message)
        {
            if (_logger == null)
            {
                Console.WriteLine(message);
                return;
            }
            this._logger.Info(message);
        }

        public void Warning(string message, Exception e = null)
        {
            if (_logger == null)
            {
                Console.WriteLine(message);
                return;
            }
            this._logger.Warn(message, e);
        }

        public void Error(string message, Exception e = null)
        {
            if (_logger == null)
            {
                Console.WriteLine(message);
                return;
            }
            this._logger.Error(message, e);
        }
    }
}
