using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentBusiness.Util;
using Appt.Common.Constants;
using ApptSite;
using DevExpress.Web.ASPxGridView;
using Log.Controller;
using Common.Util;

public partial class Admin_Screen_Default : System.Web.UI.Page
{
    string ScreenCode = "Screen";
    static string _message;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridScreen.Visible = false;
                return;
            }
            var gridcolums = gridScreen.Columns["btnCommand"] as GridViewCommandColumn;
            if (gridcolums == null) return;
            gridcolums.EditButton.Visible = RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Update.Key, out _message);
            gridcolums.NewButton.Visible = RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Create.Key, out _message);
            GridViewCommandColumnCustomButton btnDelete = null;
            foreach (GridViewCommandColumnCustomButton customButton in gridcolums.CustomButtons)
            {
                if (customButton.ID == "btnDelete")
                {
                    btnDelete = customButton;
                    break;
                }
            }
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                btnDelete.Visibility = GridViewCustomButtonVisibility.Invisible;
            }

            gridcolums.Visible = gridcolums.EditButton.Visible || gridcolums.NewButton.Visible || (btnDelete.Visibility != GridViewCustomButtonVisibility.Invisible);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    protected void gridScreen_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
    {

    }
    protected void gridScreen_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

        if ( e.NewValues["ScreenCode"]==null||e.NewValues["ScreenCode"].ToString().Trim().Length < 1)
        {
            ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "Field ScreenCode can not be empty";
            e.Cancel = true;
            return;

        }
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = AccountSession.Session;
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
    }
    protected void gridScreen_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (e.NewValues["ScreenCode"] == null || e.NewValues["ScreenCode"].ToString().Trim().Length < 1)
        {
            ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "Field Last name can not be empty";
            e.Cancel = true;
            return;

        }
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = AccountSession.Session;
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
    }
}
