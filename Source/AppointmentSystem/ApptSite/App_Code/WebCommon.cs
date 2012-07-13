using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json;

/// <summary>
/// Summary description for WebCommon
/// </summary>
public class WebCommon
{
    public static string GetAuthUsername()
    {
        return HttpContext.Current.User.Identity.Name.Split('\\')[1];
    }

    /// <summary>
    /// Get Screen code of current page
    /// </summary>
    /// <returns></returns>
    public static string GetScreenCode()
    {
        string sPagePath = HttpContext.Current.Request.Url.AbsolutePath;
        return HttpContext.Current.User.Identity.Name.Split('\\')[1];
    }

    /// <summary>
    /// Get Screen code of current page
    /// </summary>
    /// <returns></returns>
    public static string GetHomepageUrl(Page page)
    {
        return String.Format("http://{0}{1}", HttpContext.Current.Request.Url.Authority, page.ResolveUrl("~/"));
    }

    #region ShowDialog
    /// <summary>
    /// Generate to js code to show dialog by jQuery ui
    /// </summary>
    /// <param name="page">Current page will show dialog</param>
    /// <param name="objectId">Id of object will be focused</param>
    /// <param name="title">Title of dialog</param>
    /// <param name="message">Notice message</param>
    /// <param name="returnUrl">Url will be redirect after close dialog</param>
    public static void ShowDialog(Page page, string objectId, string title, string message, string returnUrl)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "dialog"
            , String.Format("ShowDialog('{0}','{1}','{2}','{3}')", objectId, title, message, returnUrl), true);
    }

    /// <summary>
    /// Generate to js code to show dialog by jQuery ui
    /// </summary>
    /// <param name="page">Current page will show dialog</param>
    /// <param name="message">Notice message</param>
    public static void ShowDialog(Page page, string message)
    {
        ShowDialog(page, string.Empty, "Message", message, string.Empty);
    }

    /// <summary>
    /// Generate to js code to show dialog by jQuery ui
    /// </summary>
    /// <param name="page">Current page will show dialog</param>
    /// <param name="message">Notice message</param>
    /// <param name="returnUrl">Url will be redirect after close dialog</param>
    public static void ShowDialog(Page page, string message, string returnUrl)
    {
        ShowDialog(page, string.Empty, "Message", message, returnUrl);
    }
    #endregion

    #region Build Result for Ajax call with Json format
    /// <summary>
    /// Build to string result for ajax with Json format
    /// </summary>
    /// <param name="isSuccess">Result: True, False</param>
    /// <param name="message"></param>
    /// <param name="data">List of data</param>
    /// <returns></returns>
    public static string BuildResult(object isSuccess, object message, object data)
    {
        return JsonConvert.SerializeObject(new
        {
            result = isSuccess.ToString().ToLower(),
            message,
            data
        });
    }

    /// <summary>
    /// Build failed result to string for ajax with Json format
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static string BuildFailedResult(object message)
    {
        return BuildResult(false, message, new List<object>());
    }

    /// <summary>
    /// Build failed result to string for ajax with Json format
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string BuildSuccessfulResult(object obj)
    {
        return BuildResult(true, string.Empty, obj);
    }
    #endregion
}
