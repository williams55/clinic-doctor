using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentBusiness.Util;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using Appt.Common.Constants;
using ApptSite;
using Common.Util;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using Log.Controller;

public partial class Admin_Users_UserGroup : System.Web.UI.Page
{
    private const string ScreenCode = "Group";
    static string _message;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Check reading right
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridGroup.Visible = false;
                return;
            }

            // Set page size
            gridGroup.SettingsPager.PageSize = ServiceFacade.SettingsHelper.PageSize;

            // Lay cot co chua cac nut thao tac
            var commandColumn = gridGroup.Columns["Operation"] as GridViewCommandColumn;

            // Neu khong co cot do thi khong can lam tiep
            if (commandColumn == null) return;

            // Gan visible cho nut new, edit bang cach kiem tra quyen
            commandColumn.EditButton.Visible = RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Update.Key, out _message);

            // Set hien thi/an cho nut new
            btnAdd.Visible = RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Create.Key, out _message);

            // Set hien thi/an cho nut delete
            btnGeneralDelete.Visible = RightAccess.CheckUserRight(AccountSession.Session,
                                                                                  ScreenCode,
                                                                                  OperationConstant.Delete.Key,
                                                                                  out _message);

            // Get delete button
            GridViewCommandColumnCustomButton btnDelete =
                commandColumn.CustomButtons.Cast<GridViewCommandColumnCustomButton>().FirstOrDefault(
                    customButton => customButton.ID == "btnDelete");

            // Check delete right, if user has no right to delete => invisible delete button
            if (btnDelete != null && !btnGeneralDelete.Visible)
            {
                btnDelete.Visibility = GridViewCustomButtonVisibility.Invisible;
            }

            // Check visibility of button in button column
            // If there is no button is displayed => invisible column
            commandColumn.Visible = commandColumn.EditButton.Visible || commandColumn.NewButton.Visible || btnGeneralDelete.Visible;
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }

    #region Group
    protected void gridGroup_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        try
        {
            if (e.ButtonID != "btnDelete") return;

            // Check deleting right
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                return;
            }

            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                WebCommon.AlertGridView(sender, "Cannot find group.");
                return;
            }

            string groupId = grid.GetRowValues(e.VisibleIndex, "Id").ToString();
            var userGroup = DataRepository.UserGroupProvider.GetById(groupId);
            if (userGroup == null || userGroup.IsDisabled)
            {
                WebCommon.AlertGridView(sender, "Group is not existed. You cannot delete it.");
                return;
            }
            DataRepository.UserGroupProvider.DeepLoad(userGroup);

            // Kiem tra xem user co duoc su dung chua
            if (userGroup.GroupRoleCollection.Exists(x => !x.IsDisabled) || userGroup.UsersCollection.Exists(x => !x.IsDisabled))
            {
                WebCommon.AlertGridView(sender, String.Format("{0} is using, you cannot delete.", userGroup.Title));
                return;
            }
            userGroup.IsDisabled = true;
            userGroup.UpdateUser = AccountSession.Session;
            userGroup.UpdateDate = DateTime.Now;
            DataRepository.UserGroupProvider.Update(userGroup);

            // Show message alert delete successfully
            WebCommon.AlertGridView(sender, "Group is deleted successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete group. Please contact Administrator.");
        }
    }

    protected void gridGroup_OnCustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        tm.BeginTransaction();
        try
        {
            if (e.Parameters == "Delete")
            {
                // Check deleting right
                if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Delete.Key, out _message))
                {
                    WebCommon.AlertGridView(sender, _message);
                    tm.Rollback();
                    return;
                }

                var grid = sender as ASPxGridView;
                if (grid == null)
                {
                    WebCommon.AlertGridView(sender, "Cannot find group.");
                    tm.Rollback();
                    return;
                }

                // Lay danh sach Id cua cac row duoc select
                var fieldNames = new[] { "Id" };
                List<object> columnValues = grid.GetSelectedFieldValues(fieldNames);

                // Doi trang thai cua cac roster
                foreach (object groupId in columnValues)
                {
                    var userGroup = DataRepository.UserGroupProvider.GetById(groupId.ToString());
                    if (userGroup == null || userGroup.IsDisabled)
                    {
                        WebCommon.AlertGridView(sender, "Group is not existed. You cannot delete it.");
                        tm.Rollback();
                        return;
                    }
                    DataRepository.UserGroupProvider.DeepLoad(userGroup);

                    // Kiem tra xem user co duoc su dung chua
                    if (userGroup.GroupRoleCollection.Exists(x => !x.IsDisabled) || userGroup.UsersCollection.Exists(x => !x.IsDisabled))
                    {
                        WebCommon.AlertGridView(sender, String.Format("{0} is using, you cannot delete.", userGroup.Title));
                        tm.Rollback();
                        return;
                    }
                    userGroup.IsDisabled = true;
                    userGroup.UpdateUser = AccountSession.Session;
                    userGroup.UpdateDate = DateTime.Now;
                    DataRepository.UserGroupProvider.Update(tm, userGroup);
                }
                tm.Commit();
                WebCommon.AlertGridView(sender, String.Format("{0} deleted successfully.",
                                      grid.Selection.Count > 1 ? "Groups are" : "Group is"));

                grid.Selection.UnselectAll();
            }
        }
        catch (Exception ex)
        {
            tm.Rollback();
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete group. Please contact Administrator.");
        }
    }
    
    protected void gridGroup_RowInserting(object sender, ASPxDataInsertingEventArgs e)
    {
        try
        {
            // Validate user right for creating
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Create.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Get grid
            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                WebCommon.AlertGridView(sender, "Add form is error.");
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

            e.NewValues["Id"] = e.NewValues["Title"].ToString().Trim().Replace(" ", "");
            e.NewValues["Title"] = e.NewValues["Title"].ToString().Trim();
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = AccountSession.Session;
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
            WebCommon.AlertGridView(sender, "Group is created successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot insert group. Please contact Administrator");
            e.Cancel = true;
        }
    }
    protected void gridGroup_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Create.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Get grid
            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                WebCommon.AlertGridView(sender, "Edit form is error.");
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

            e.NewValues["Username"] = e.NewValues["Title"].ToString().Trim();
            e.NewValues["UpdateUser"] = AccountSession.Session;
            e.NewValues["UpdateDate"] = DateTime.Now;
            WebCommon.AlertGridView(sender, "Group is update successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot update group. Please contact Administrator");
            e.Cancel = true;
        }
    }
    #endregion

    #region Group Role
    protected void gridGroupRole_OnInit(object sender, EventArgs e)
    {
        try
        {
            // Check null for grid user role
            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                WebCommon.AlertGridView(sender, _message);
                return;
            }

            // Check reading right
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                grid.Visible = false;
                return;
            }

            // Set page size
            grid.SettingsPager.PageSize = ServiceFacade.SettingsHelper.PageSize;

            // Lay cot co chua cac nut thao tac
            var commandColumn = grid.Columns["Operation"] as GridViewCommandColumn;

            // Neu khong co cot do thi khong can lam tiep
            if (commandColumn == null) return;

            // Khong cho sua
            commandColumn.EditButton.Visible = false;

            // Set hien thi/an cho nut new
            btnAdd.Visible = RightAccess.CheckUserRight(AccountSession.Session, ScreenCode,
                                                        OperationConstant.Create.Key, out _message);

            // Set hien thi/an cho nut delete
            btnGeneralDelete.Visible = RightAccess.CheckUserRight(AccountSession.Session,
                                                                                  ScreenCode,
                                                                                  OperationConstant.Delete.Key,
                                                                                  out _message);

            // Get delete button
            GridViewCommandColumnCustomButton btnDelete =
                commandColumn.CustomButtons.Cast<GridViewCommandColumnCustomButton>().FirstOrDefault(
                    customButton => customButton.ID == "btnDelete");

            // Check delete right, if user has no right to delete => invisible delete button
            if (btnDelete != null && !btnGeneralDelete.Visible)
            {
                btnDelete.Visibility = GridViewCustomButtonVisibility.Invisible;
            }

            // Check visibility of button in button column
            // If there is no button is displayed => invisible column
            commandColumn.Visible = commandColumn.EditButton.Visible || commandColumn.NewButton.Visible || btnGeneralDelete.Visible;

            string groupId = grid.GetMasterRowKeyValue().ToString();
            var param = GroupRoleDataSource.Parameters["WhereClause"];
            if (param == null)
            {
                GroupRoleDataSource.Parameters.Add("WhereClause"
                    , String.Format("IsDisabled = 'false' AND GroupId='{0}'", groupId));
            }
            else
            {
                param.DefaultValue = String.Format("IsDisabled = 'false' AND GroupId='{0}'", groupId);
            }
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(this, "System is error. Please contact Administrator.");
        }
    }

    protected void gridGroupRole_OnCustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        try
        {
            if (e.ButtonID != "btnDelete") return;

            // Check deleting right
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                return;
            }

            // Get current grid user role
            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                WebCommon.AlertGridView(sender, "Cannot find group role.");
                return;
            }

            long id;
            if (Int64.TryParse(grid.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
            {
                // Neu id la so thi tien hanh kiem tra xem user role co duoc su dung khong
                var groupRole = DataRepository.GroupRoleProvider.GetById(id);

                // Neu khong ton tai thi bao loi
                if (groupRole == null || groupRole.IsDisabled)
                {
                    WebCommon.AlertGridView(sender, "User role is not existed. You cannot delete it.");
                    return;
                }
                groupRole.IsDisabled = true;
                groupRole.UpdateUser = AccountSession.Session;
                groupRole.UpdateDate = DateTime.Now;
                DataRepository.GroupRoleProvider.Update(groupRole);

                // Show message alert delete successfully
                WebCommon.AlertGridView(sender, "Group role is deleted successfully.");
            }
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete group role. Please contact Administrator.");
        }
    }

    protected void gridGroupRole_OnCustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        tm.BeginTransaction();
        try
        {
            if (e.Parameters == "Delete")
            {
                // Check deleting right
                if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Delete.Key, out _message))
                {
                    WebCommon.AlertGridView(sender, _message);
                    return;
                }

                var grid = sender as ASPxGridView;
                if (grid == null)
                {
                    WebCommon.AlertGridView(sender, "Cannot find group role.");
                    tm.Rollback();
                    return;
                }

                // Lay danh sach Id cua cac row duoc select
                var fieldNames = new[] { "Id" };
                List<object> columnValues = grid.GetSelectedFieldValues(fieldNames);

                // Doi trang thai cua cac roster
                foreach (object roleId in columnValues)
                {
                    long id;
                    if (!Int64.TryParse(roleId.ToString(), out id))
                    {
                        WebCommon.AlertGridView(sender, "Group role is invalid. You cannot delete it.");
                        tm.Rollback();
                        return;
                    }

                    var groupRole = DataRepository.GroupRoleProvider.GetById(id);
                    if (groupRole == null || groupRole.IsDisabled)
                    {
                        WebCommon.AlertGridView(sender, "Group role is not existed. You cannot delete it.");
                        tm.Rollback();
                        return;
                    }

                    groupRole.IsDisabled = true;
                    groupRole.UpdateUser = AccountSession.Session;
                    groupRole.UpdateDate = DateTime.Now;
                    DataRepository.GroupRoleProvider.Update(tm, groupRole);
                }
                tm.Commit();
                WebCommon.AlertGridView(sender, String.Format("{0} deleted successfully.",
                                                      grid.Selection.Count > 1 ? "Group role are" : "Group role is"));

                // Set tam pageIndex de lay duoc danh sach moi
                grid.Selection.UnselectAll();
            }
        }
        catch (Exception ex)
        {
            tm.Rollback();
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete user role. Please contact Administrator.");
        }
    }

    protected void gridGroupRole_OnRowInserting(object sender, ASPxDataInsertingEventArgs e)
    {
        try
        {
            // Validate user right for creating
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Create.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Check null for grid role detail
            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            string groupId = grid.GetMasterRowKeyValue().ToString();
            if (!WebCommon.ValidateEmpty("Role", e.NewValues["RoleId"], out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            e.NewValues["GroupId"] = groupId;
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = AccountSession.Session;
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;

            WebCommon.AlertGridView(sender, "Group role is created successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot create group role. Please contact Administrator");
            e.Cancel = true;
        }
    }

    protected void gridGroupRole_OnInitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
    {
        try
        {
            // Get current grid user role
            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                WebCommon.AlertGridView(sender, "Cannot find group role.");
                return;
            }

            int count;
            string groupId = grid.GetMasterRowKeyValue().ToString();

            // Lay cot co chua id
            var commandColumn = grid.Columns["Role"] as GridViewDataComboBoxColumn;

            // Neu khong co cot do thi khong can lam tiep
            if (commandColumn == null) return;

            var cboRole = (ASPxComboBox)grid.FindEditFormTemplateControl("cboGroupRole");
            TList<Role> roles = DataRepository.RoleProvider.GetPaged("IsDisabled='false'", "", 0, 0, out count);
            TList<GroupRole> userRoles = DataRepository.GroupRoleProvider.GetPaged(
                    string.Format("IsDisabled='false' and GroupId='{0}'", groupId), "", 0, 0, out count);
            var availableRoles = roles.FindAll(role => !userRoles.Exists(role2 => role.Id == role2.RoleId));
            cboRole.DataSource = availableRoles;
            cboRole.DataBindItems();
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(this, "System is error. Please contact Administrator.");
        }
    }
    #endregion
}
