using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Log;

namespace AutoSendSms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // Code that runs on application startup
            // Read and assign application wide logging severity
            string severity = ConfigurationManager.AppSettings.Get("LogSeverity");
            SingletonLogger.Instance.Severity = (LogSeverity)Enum.Parse(typeof(LogSeverity), severity, true);

            // Send log messages to debugger console (output window). 
            // Btw: the attach operation is the Observer pattern.
            ILog log = new ObserverLogToConsole();
            SingletonLogger.Instance.Attach(log);

            // Send log messages to a file
            log = new ObserverLogToFile("filename");
            SingletonLogger.Instance.Attach(log);
            
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new SmsService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
