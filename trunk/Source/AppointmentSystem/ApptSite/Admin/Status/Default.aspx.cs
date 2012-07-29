using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentSystem.Data;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using Appt.Common.Constants;
using AppointmentBusiness.Util;
using Log.Controller;
using Common.Util;

public partial class Admin_Status_Default : System.Web.UI.Page
{
    private const string ScreenCode = "Status";
    string _message;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Check reading right of user
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                grid.Visible = false;
                return;
            }

            // Get command column
            var gridViewCommandColumn = grid.Columns["#"] as GridViewCommandColumn;

            // If command column is not existed, stop
            if (gridViewCommandColumn == null) return;

            // Set visible for edit button
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
        try
        {
            // Check updating right of user
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key,
                                       out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Get ColorEditor object
            var color = (ASPxColorEdit)((ASPxGridView)sender).FindEditFormTemplateControl("ColorEditHeaderBackColor");

            // Get PriorityIndex object
            var index = (ASPxSpinEdit)((ASPxGridView)sender).FindEditFormTemplateControl("index");

            #region Validating
            // Validate empty field
            if (!WebCommon.ValidateEmpty("Title", e.NewValues["Title"], out _message)
                || !WebCommon.ValidateEmpty("Color Code", color.Text, out _message)
                || !WebCommon.ValidateEmpty("Index", index.Value, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }
            
            // Validate integer value for priority index
            int iIndex;
            if (!Int32.TryParse(index.Value.ToString().Trim(), out iIndex))
            {
                WebCommon.AlertGridView(sender, "Index value is invalid.");
                e.Cancel = true;
                return;
            }
            #endregion

            // Set value
            e.NewValues["Title"] = e.NewValues["Title"].ToString().Trim();
            e.NewValues["PriorityIndex"] = index.Value;
            e.NewValues["ColorCode"] = color.Text.Trim();
            e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["UpdateDate"] = DateTime.Now;

            // Show message alert delete successfully
            WebCommon.AlertGridView(sender, "Status is updated successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            e.Cancel = true;
            WebCommon.AlertGridView(sender, "Cannot update service. Please contact Administrator");
        }
    }
}
