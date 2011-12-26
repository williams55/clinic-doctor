<%@ Application Language="C#" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="LogUtil" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        InitializeLogger();
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
    void Application_BeginRequest(object sender, EventArgs e)
    {
        var customCulture = new CultureInfo("en-Us");
        var DFI = DateTimeFormatInfo.CurrentInfo.Clone() as DateTimeFormatInfo;
        DFI.DateSeparator = "-";
        DFI.ShortDatePattern = "dd-MMM-yyyy";
        DFI.ShortTimePattern = "HH:mm:ss";
        DFI.LongTimePattern = "";
        customCulture.DateTimeFormat = DFI;
        Thread.CurrentThread.CurrentCulture = customCulture;
        Thread.CurrentThread.CurrentUICulture = customCulture;
    }

    /// <summary>
    /// Initializes logging facility with severity level and observer(s).
    /// Private helper method.
    /// </summary>
    private void InitializeLogger()
    {
        // Read and assign application wide logging severity
        string severity = ConfigurationManager.AppSettings.Get("LogSeverity");
        SingletonLogger.Instance.Severity = (LogSeverity)Enum.Parse(typeof(LogSeverity), severity, true);

        // Send log messages to debugger console (output window). 
        // Btw: the attach operation is the Observer pattern.
        ILog log = new ObserverLogToConsole();
        SingletonLogger.Instance.Attach(log);

        // Send log messages to email (observer pattern)
        //string from = "notification@yourcompany.com";
        //string to = "webmaster@yourcompany.com";
        //string subject = "Webmaster: please review";
        //string body = "email text";
        //var smtpClient = new SmtpClient("mail.yourcompany.com");
        //log = new ObserverLogToEmail(from, to, subject, body, smtpClient);
        //SingletonLogger.Instance.Attach(log);

        // Other log output options

        //// Send log messages to a file
        log = new ObserverLogToFile();
        SingletonLogger.Instance.Attach(log);

        //// Send log message to event log
        //log = new ObserverLogToEventlog();
        //SingletonLogger.Instance.Attach(log);

        //// Send log messages to database (observer pattern)
        //log = new ObserverLogToDatabase();
        //SingletonLogger.Instance.Attach(log);
    }

</script>

