// File: LogHelper.cs

using System;
using NLog;

namespace WCFDocServiceV2.DAL
{
    public static class LogHelper
    {
        private static Logger logger;

        static LogHelper()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public static void LogInfo(string Message)
        {
            logger.Info(Message);
        }

        public static void LogError(string Message, SystemException ex)
        {
            logger.Error(Message, ex);
        }
    }
}
