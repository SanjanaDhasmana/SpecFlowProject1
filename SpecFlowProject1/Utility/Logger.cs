using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject1.Utility
{
    public class Logger
    {
        private static readonly ILogger logger;
        static Logger()
        {
            LogManager.Configuration = new XmlLoggingConfiguration("D:\\VisualStudio\\SpecFlowProject1\\SpecFlowProject1\\NLog.config");
            logger = LogManager.GetCurrentClassLogger();
        }
        public static void Info(string message) => logger.Info(message);
        public static void Debug(string message) => logger.Debug(message);
        public static void Error(string message) => logger.Error(message);
        public static void Warn(string message) => logger.Warn(message);
        public static void Fatal(string message) => logger.Fatal(message);
    }

}
