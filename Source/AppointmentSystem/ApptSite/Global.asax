<%@ Application Language="C#" %>
<%@ Import Namespace="Log" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
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
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
