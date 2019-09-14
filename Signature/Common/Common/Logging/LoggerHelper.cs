using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Common.Logging
{
    public enum LoggerLevel
    {
        Fatal,
        Error,
        Warning,
        Info,
        Debug
    }
    public static class LoggerHelper
    {
        static LoggerHelper()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void Log(Type t, LoggerLevel level, params object[] messages)
        {
            ILog log = LogManager.GetLogger(t);
            string message = string.Empty;
            foreach (object m in messages) message += ((m == null) ? "" : m.ToString()) + " ";
            switch (level)
            {
                case LoggerLevel.Fatal: log.Fatal(message); break;
                case LoggerLevel.Warning: log.Warn(message); break;
                case LoggerLevel.Error: log.Error(message); break;
                case LoggerLevel.Info: log.Info(message); break;
                case LoggerLevel.Debug: log.Debug(message); break;
            }
        }
        public static void Log(this object obj, LoggerLevel level, params object[] messages)
        {
            Log(obj.GetType(), level, messages);
        }

        public static void Log(this object obj, System.Exception ex)
        {
            ILog log = LogManager.GetLogger(obj.GetType());
            log.Fatal(ex.Message, ex);
        }

    }
}
