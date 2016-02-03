using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TickTick.Enums;
using WindowsUniversalLogger.Interfaces;
using WindowsUniversalLogger.Logging;
using WindowsUniversalLogger.Logging.Sessions;

namespace TickTick.Helper
{
    public static class LoggerHelper
    {

        //private static ILoggingSession _logInstance;

        //public static ILoggingSession LogInstance
        //{
        //    get
        //    {
        //        if (_logInstance == null)
        //        {
        //            lock ("create")
        //            {
        //                if (_logInstance == null)
        //                {
        //                    _logInstance = LoggingSession.Instance;
        //                }
        //            }
        //        }
        //        return _logInstance;
        //    }
        //}
        private static DateTime? LastRecordTime { get; set; }
        public static bool IS_LOG_ENABLED = false;//!Constants.RELEASE;

        public static async Task LogToAllChannels(LogLevel? logLevel, string logMsg)
        {
            LogLevel logLevelNotNull = logLevel ?? LogLevel.DEBUG;
            await LogToAllChannels(logLevelNotNull, logMsg, DateTime.Now);
        }
        public static async Task LogToAllChannels(LogLevel logLevel, string logMsg, DateTime dateTime)
        {
            await LogToAllChannels(logLevel, logMsg, dateTime, false);
        }
        public static async Task LogToAllChannels(LogLevel logLevel, string logMsg, DateTime dateTime, bool isNeedMethodName)
        {
            await LogToAllChannels(logLevel, logMsg, dateTime, isNeedMethodName, "类型未知");
        }
        public static async Task LogToAllChannels(LogLevel logLevel, string logMsg, DateTime dateTime, bool isNeedMethodName, string type)
        {
            //var str =System.Reflection.MethodAttributes..GetMethodFromHandle(.GetCurrentMethod().DeclaringType.Namespace;
            //var nameSpace = MethodBase.GetCurrentMethod.DeclaringType.NameSpace;

            // TODO 暂时放在这里，以后有用，当知道如何获取方法名之后。。。
            var deltaTime = DateTime.Now - (LastRecordTime ?? DateTime.Now);
            string nameSpace = string.Empty;
            string className = string.Empty;
            string methodName = string.Empty;

            await LoggingSession.Instance.LogToAllChannels(
             new LogEntry(
                 logLevel,
                 string.Format("日志信息：【{0}】，记录时间：{1}，与上次记录时间差：{2} \r\n "
                             + "当前所在方法名：{3}，类名：{4}，程序集：{5} \r\n"
                             + "当前类型“{6}；\r\n", logMsg, dateTime, deltaTime, nameSpace, className, methodName, type)));
            LastRecordTime = DateTime.Now;
        }
    }
}
