using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using Appt.Common.Constants;
using AppointmentBusiness.Util;
using Log.Controller;
using Common.Util;

public partial class Admin_Users_Default : System.Web.UI.Page
{
    private const string ScreenCode = "User";
    static string _message;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Validate user right for reading
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key,
                                       out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridUser.Visible = false;
            }
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    protected void gridUser_OnInitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string perxe = "user" + DateTime.Now.ToString("ddMMyy");
        int count = 0;
        TList<Users> objuser = DataRepository.UsersProvider.GetPaged("Id like '" + perxe + "' + '%'", "Id desc", 0, 1, out count);
        if (count == 0)
        {
            e.NewValues["Id"] = perxe + "001";
        }
        else
        {
            e.NewValues["Id"] = perxe + string.Format("{0:000}", int.Parse(objuser[0].Id.Substring(objuser[0].Id.Length - 3)) + 1);
        }
    }
    protected void gridUser_OnRowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.Cancel = true;
    }
}
