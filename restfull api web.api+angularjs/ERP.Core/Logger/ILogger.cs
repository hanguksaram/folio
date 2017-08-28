namespace ERP.Core.Logger
{
    using System;

    public interface ILogger
    {
        void Info(string message);

        void Warning(string message, Exception e = null);

        void Error(string message, Exception e = null);
    }
}