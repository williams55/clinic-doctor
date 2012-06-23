using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

/// <summary>
/// Summary description for WebCommon
/// </summary>
public class WebCommon
{
    public static string GetAuthUsername()
    {
        return HttpContext.Current.User.Identity.Name.Split('\\')[1];
    }
}
