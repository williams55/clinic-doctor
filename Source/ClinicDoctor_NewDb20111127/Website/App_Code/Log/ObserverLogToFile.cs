using System.IO;

namespace LogUtil
{
    /// <summary>
    /// Writes log events to a local file.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Observer.
    /// The Observer Design Pattern allows this class to attach itself to an
    /// the logger and 'listen' to certain events and be notified of the event. 
    /// </remarks>
    public class ObserverLogToFile : ILog
    {
        private string _fileName;

        /// <summary>
        /// Constructor of ObserverLogToFile.
        /// </summary>
        /// <param name="fileName">File log to.</param>
        public ObserverLogToFile(string fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// Constructor of ObserverLogToFile.
        /// </summary>
        /// <param name="fileName">File log to.</param>
        public ObserverLogToFile()
        {
            
        }

        /// <summary>
        /// Write a log request to a file.
        /// </summary>
        /// <param name="sender">Sender of the log request.</param>
        /// <param name="e">Parameters of the log request.</param>
        public void Log(object sender, LogEventArgs e)
        {
            string message = "[" + e.Date.ToString() + "] " +
                e.SeverityString + ": " + e.Message;
            if (e.Exception != null)
            {
                LogController.WriteLog(e.Exception);
            }
            else
            {
                LogController.WriteLog(e.Message);
            }
        }
    }
}
