using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for EntitiesUtilities
/// </summary>
public static class EntitiesUtilities
{
    // Seperate username and domain
    public static string GetAuthName(string userName)
    {
        try
        {
            string authUserName;
            authUserName = userName.Split('\\')[1];
            return authUserName;
        }
        catch
        {
            return string.Empty;
        }
    }
}
