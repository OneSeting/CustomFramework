using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LittleHourglass.Logger
{
    public class LoggerHelper
    {
        private static readonly ILog logger;

        //Logger/log4net.config   这个文件的配置值研究

        static LoggerHelper()
        {
            if (logger == null)
            {
                // 创建一个 具有指定名称的存储库。
                var respository = LogManager.CreateRepository("LitteHourglassLogs");
                // 初始化作为文件路径的包装的 System.IO.FileInfo 类的新实例。
                var log4netConfig = new FileInfo("Logger/log4net.config");
                // 使用这个类来使用Xml树初始化log4net环境。
                XmlConfigurator.Configure(respository, log4netConfig);
                //存储库的名称
                logger = LogManager.GetLogger(respository.Name, "InfoLogger");
            }      
        }

        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message, Exception exception = null)
        {
            if (exception == null)
            {
                logger.Info(message);
            }
            else
            {
                logger.Info(message, exception);
            }
        
        }

        /// <summary>
        /// 告警日志
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(string message, Exception exception = null)
        {
            if (exception == null)
            {
                logger.Warn(message);
            }
            else
            {
                logger.Warn(message, exception);
            }

        }


        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message, Exception exception = null)
        {
            if (exception == null)
            {
                logger.Error(message);
            }
            else
            {
                logger.Error(message, exception);
            }

        }


    }
}
