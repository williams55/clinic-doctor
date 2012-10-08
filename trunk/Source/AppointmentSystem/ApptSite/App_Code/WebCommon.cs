using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Appt.Common.Constants;
using Common;
using Common.Util;
using DevExpress.Web.ASPxGridView;
using Log.Controller;
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

    #region ASPxGridView
    /// <summary>
    /// Alert message for ASPxGridView
    /// </summary>
    /// <param name="sender">ASPxGridView object</param>
    /// <param name="message"></param>
    public static void AlertGridView(object sender, string message)
    {
        try
        {
            ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = message;
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }

    /// <summary>
    /// Validate empty string
    /// </summary>
    /// <param name="title"> </param>
    /// <param name="input"></param>
    /// <param name="message"></param>
    public static bool ValidateEmpty(string title, object input, out string message)
    {
        message = string.Empty;
        try
        {
            if (input == null || string.IsNullOrEmpty(input.ToString().Trim()))
            {
                message = String.Format("{0} cannot be empty.", title);
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            message = "System error. Please contact administrator.";
        }
        return false;
    }

    public static List<ConstantKeyValue> GetWeekday()
    {
        return JsonConvert.DeserializeObject<List<ConstantKeyValue>>(Constants.Weekdays);
    }

    /// <summary>
    /// Refresh grid moi khi delete item
    /// Phai thuc hien nhieu buoc la vi khi thuc hien ham bind o 1 trang > 0 thi bi loi
    /// </summary>
    /// <param name="grid"></param>
    public static void RefreshGrid(ASPxGridView grid)
    {
        int page = grid.PageIndex;
        grid.PageIndex = 0;
        grid.DataBind();
        grid.PageIndex = page;
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
    /// <param name="message"></param>
    /// <param name="data">List of data</param>
    /// <returns></returns>
    public static string BuildFailedResult(object message, object data)
    {
        return BuildResult(false, message, data);
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

    /// <summary>
    /// Build failed result to string for ajax with Json format
    /// </summary>
    /// <returns></returns>
    public static string BuildSuccessfulResult()
    {
        return BuildResult(true, string.Empty, new List<object>());
    }

    /// <summary>
    /// Build failed result to string for ajax with Json format
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static string BuildSuccessfulResult(string message)
    {
        return BuildResult(true, message, new List<object>());
    }
    #endregion
}
