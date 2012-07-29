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
                // Set returned message, then DevExpress will get it and show for end-user
                WebCommon.AlertGridView(sender, _message);

                // Cancel operation
                e.Cancel = true;

                // Stop
                return;
            }

            // Get ColorEditor object
            var color = (ASPxColorEdit)((ASPxGridView)sender).FindEditFormTemplateControl("ColorEditHeaderBackColor");

            // Get PriorityIndex object
            var index = (ASPxSpinEdit)((ASPxGridView)sender).FindEditFormTemplateControl("index");

            #region Validating
            // Check field Title if empty, then stop
            if (e.NewValues["Title"] == null || string.IsNullOrEmpty(e.NewValues["Title"].ToString().Trim()))
            {
                WebCommon.AlertGridView(sender, "Title cannot be empty");
                e.Cancel = true;
                return;
            }

            // Check field Text if empty, then stop
            if (string.IsNullOrEmpty(color.Text))
            {
                WebCommon.AlertGridView(sender, "Title cannot be empty");
                e.Cancel = true;
                return;
            }

            // Validate integer value for priority index
            int iIndex;
            if (!Int32.TryParse(index.Value.ToString(), out iIndex))
            {
                // Set returned message, then DevExpress will get it and show for end-user
                WebCommon.AlertGridView(sender, "Index value is invalid.");

                // Cancel operation
                e.Cancel = true;

                // Stop
                return;
            }
            #endregion

            e.NewValues["Title"] = e.NewValues["Title"].ToString().Trim();
            e.NewValues["PriorityIndex"] = index.Value;
            e.NewValues["ColorCode"] = color.Text.Trim();
            e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["UpdateDate"] = DateTime.Now;
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            e.Cancel = true;
        }
    }
}
