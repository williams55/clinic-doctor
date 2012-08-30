using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentBusiness.Util;
using AppointmentSystem.Data;
using AppointmentSystem.Settings.BusinessLayer;
using Appt.Common.Constants;
using Common.Util;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using Log.Controller;

public partial class Admin_Roster_Grid : System.Web.UI.Page
{
    private const string ScreenCode = "Roster";
    static string _message;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Neu la postback thi bo qua
            if (IsPostBack)
            {
                return;
            }

            // Check reading right
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridRoster.Visible = false;
                return;
            }

            // Set page size
            gridRoster.SettingsPager.PageSize = ServiceFacade.SettingsHelper.PageSize;

            // Lay cot co chua cac nut thao tac
            var commandColumn = gridRoster.Columns["Operation"] as GridViewCommandColumn;

            // Neu khong co cot do thi khong can lam tiep
            if (commandColumn == null) return;

            // Gan visible cho nut new, edit bang cach kiem tra quyen
            commandColumn.EditButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Update.Key, out _message);
            commandColumn.NewButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key, out _message);

            // Get delete button
            GridViewCommandColumnCustomButton btnDelete =
                commandColumn.CustomButtons.Cast<GridViewCommandColumnCustomButton>().FirstOrDefault(
                    customButton => customButton.ID == "btnDelete");

            // Check delete right, if user has no right to delete => invisible delete button
            if (btnDelete != null && !RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                btnDelete.Visibility = GridViewCustomButtonVisibility.Invisible;
            }

            // Check visibility of button in button column
            // If there is no button is displayed => invisible column
            commandColumn.Visible = commandColumn.EditButton.Visible || commandColumn.NewButton.Visible ||
                                 (btnDelete != null && btnDelete.Visibility != GridViewCustomButtonVisibility.Invisible);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.ShowDialog(this, "System is error. Please contact Administrator.");
        }
    }

    // Delete role
    protected void gridRole_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        try
        {
            if (e.ButtonID != "btnDelete") return;
            #region Delete row
            // Check deleting right
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                return;
            }

            // Validate id
            Int32 id;
            if (Int32.TryParse(gridRoster.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
            {
                var obj = DataRepository.RoleProvider.GetById(id);
                if (obj == null || obj.IsDisabled)
                {
                    WebCommon.AlertGridView(sender, "Role is not existed. You cannot delete it.");
                    return;
                }
                DataRepository.RoleProvider.DeepLoad(obj);

                // Kiem tra xem co cho nao dang su dung room hay khong
                if (obj.GroupRoleCollection.Exists(x => !x.IsDisabled) || obj.UserRoleCollection.Exists(x => !x.IsDisabled)
                    || obj.RoleDetailCollection.Exists(x => !x.IsDisabled))
                {
                    WebCommon.AlertGridView(sender, String.Format("Role {0} is used, you cannot delete it.", obj.Title));
                    return;
                }
                obj.IsDisabled = true;
                obj.UpdateUser = WebCommon.GetAuthUsername();
                obj.UpdateDate = DateTime.Now;
                DataRepository.RoleProvider.Update(obj);
            }
            #endregion
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete role. Please contact Administrator");
        }
    }

    protected void gridRole_RowInserting(object sender, ASPxDataInsertingEventArgs e)
    {
        try
        {
            // Validate user right for creating
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Validate empty field
            if (!WebCommon.ValidateEmpty("Title", e.NewValues["Title"], out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Set pure value
            e.NewValues["Title"] = e.NewValues["Title"].ToString().Trim();
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;

            // Show message alert delete successfully
            WebCommon.AlertGridView(sender, "Role is created successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            e.Cancel = true;
            WebCommon.AlertGridView(sender, "Cannot create new role. Please contact Administrator");
        }
    }

    protected void gridRole_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        try
        {
            // Validate user right for updating
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Update.Key, out _message))//Kiem tra xem user co quyen cap nhat hay khong
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Validate empty field
            if (!WebCommon.ValidateEmpty("Title", e.NewValues["Title"], out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Set pure value
            e.NewValues["Title"] = e.NewValues["Title"].ToString().Trim();
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;

            // Show message alert delete successfully
            WebCommon.AlertGridView(sender, "Role is updated successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            e.Cancel = true;
            WebCommon.AlertGridView(sender, "Cannot update role. Please contact Administrator");
        }
    }
}
