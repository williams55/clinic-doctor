using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentSystem.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using Appt.Common.Constants;
using AppointmentBusiness.Util;
using Log.Controller;
using Common.Util;

public partial class Admin_Status_Default : System.Web.UI.Page
{
    string ScreenCode="Status";
    string  _message;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                grid.Visible = false;
                return;
            }
            var gridViewCommandColumn = grid.Columns["#"] as GridViewCommandColumn;
            if (gridViewCommandColumn == null) return;
            gridViewCommandColumn.EditButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(),
                                                                                  ScreenCode,
                                                                                  OperationConstant.Update.Key,
                                                                                  out _message);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    protected void grid_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        string priindex = e.NewValues["PriorityIndex"].ToString();
        int index;
        if (priindex.Trim().Length > 0)
        {
            try {
                index = int.Parse(priindex);   
            }
            catch
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "PriortyIndex was not in a correct format.";
                e.Cancel = true;
                return;
            }
        }
        e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
        e.NewValues["UpdateDate"] = DateTime.Now;
        e.NewValues["IsDisabled"] = true;
    }
    protected void grid_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {

    }
}
