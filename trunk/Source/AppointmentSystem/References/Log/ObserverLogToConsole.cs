
namespace Log
{
    /// <summary>
    /// Writes log events to the diagnostic trace.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Observer.
    /// The Observer Design Pattern allows this class to attach itself to an
    /// the logger and 'listen' to certain events and be notified of the event. 
    /// </remarks>
    public class ObserverLogToConsole : ILog
    {
        /// <summary>
        /// Writes a log request to the diagnostic trace on the page.
        /// </summary>
        /// <param name="sender">Sender of the log request.</param>
        /// <param name="e">Parameters of the log request.</param>
        public void Log(object sender, LogEventArgs e)
        {
            if (e.Exception != null)
            {
                LogController.WriteLog(e.Exception);
            }
        }
    }
}
