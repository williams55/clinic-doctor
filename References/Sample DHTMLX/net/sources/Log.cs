using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Provides public interface to logging functionality
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Gets or Sets enabled state for logging system
        /// </summary>
        public static bool Enabled
        {
            get;
            set;
        }

        /// <summary>
        /// Gets reference to collection of trace listeners
        /// </summary>
        public static TraceListenerCollection Listeners
        {
            get
            {
                return Trace.Listeners;
            }
        }

        /// <summary>
        /// Writes message to log
        /// </summary>
        /// <param name="context">Object associated with this message</param>
        /// <param name="Message">Message to log</param>
        public static void WriteLine(object context, string Message)
        {
            if (Enabled)
            {
                Trace.Write(DateTime.Now.ToLongTimeString());
                Trace.Write(" ");
                Trace.Write(context.GetType().Name);
                Trace.Write(":\t");
                Trace.WriteLine(Message);
            }
        }
    }
}
