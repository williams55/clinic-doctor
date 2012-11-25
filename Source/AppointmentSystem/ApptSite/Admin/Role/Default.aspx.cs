using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using AppointmentSystem.Data;
using AppointmentSystem.Settings.BusinessLayer;
using ApptSite;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using DevExpress.Web.ASPxEditors;
using Log.Controller;
using Common.Util;
using Appt.Common.Constants;
using AppointmentBusiness.Util;
public partial class Admin_Role_Default : Page
{
    private const string ScreenCode = "Role";
    static string _message;

    #region Main list
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Check reading right
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridRole.Visible = false;
                return;
            }

            // Set page size
            gridRole.SettingsPager.PageSize = ServiceFacade.SettingsHelper.PageSize;

            // Lay cot co chua cac nut thao tac
            var commandColumn = gridRole.Columns["Operation"] as GridViewCommandColumn;

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
            WebCommon.ShowDialog(this, "System is error. Please contact Administrator.");
        }
    }

    /// <summary>
    /// Xoa mot role
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridRole_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        try
        {
            if (e.ButtonID != "btnDelete") return;
            #region Delete row
            // Check deleting right
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                return;
            }

            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                WebCommon.AlertGridView(sender, "Cannot find role.");
                return;
            }

            // Validate id
            Int32 id;
            if (Int32.TryParse(grid.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
            {
                var role = DataRepository.RoleProvider.GetById(id);
                if (role == null || role.IsDisabled)
                {
                    WebCommon.AlertGridView(sender, "Role is not existed. You cannot delete it.");
                    return;
                }
                DataRepository.RoleProvider.DeepLoad(role);

                // Kiem tra xem co cho nao dang su dung room hay khong
                if (role.GroupRoleCollection.Exists(x => !x.IsDisabled) || role.UserRoleCollection.Exists(x => !x.IsDisabled)
                    || role.RoleDetailCollection.Exists(x => !x.IsDisabled))
                {
                    WebCommon.AlertGridView(sender, String.Format("Role {0} is using, you cannot delete it.", role.Title));
                    return;
                }
                role.IsDisabled = true;
                role.UpdateUser = AccountSession.Session;
                role.UpdateDate = DateTime.Now;
                DataRepository.RoleProvider.Update(role);

                // Show message alert delete successfully
                WebCommon.AlertGridView(sender, "Role is deleted successfully.");
            }
            #endregion
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete role. Please contact Administrator");
        }
    }

    /// <summary>
    /// Them mot role
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridRole_RowInserting(object sender, ASPxDataInsertingEventArgs e)
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

            // Validate empty field
            if (!WebCommon.ValidateEmpty("Title", e.NewValues["Title"], out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Set pure value
            e.NewValues["Title"] = e.NewValues["Title"].ToString().Trim();
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = AccountSession.Session;
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

    /// <summary>
    /// Cap nhat mot role
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridRole_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        try
        {
            // Validate user right for updating
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Update.Key, out _message))//Kiem tra xem user co quyen cap nhat hay khong
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
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = AccountSession.Session;
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

    /// <summary>
    /// Xoa nhieu role
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridRole_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
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
                    WebCommon.AlertGridView(sender, "Cannot find role.");
                    tm.Rollback();
                    return;
                }

                // Lay danh sach Id cua cac row duoc select
                var fieldNames = new[] { "Id" };
                List<object> columnValues = grid.GetSelectedFieldValues(fieldNames);

                // Doi trang thai cua cac roster
                foreach (object roleId in columnValues)
                {
                    int id;
                    if (!Int32.TryParse(roleId.ToString(), out id))
                    {
                        WebCommon.AlertGridView(sender, "Role is invalid. You cannot delete it.");
                        tm.Rollback();
                        return;
                    }

                    var role = DataRepository.RoleProvider.GetById(id);
                    if (role == null || role.IsDisabled)
                    {
                        WebCommon.AlertGridView(sender, "Role is not existed. You cannot delete it.");
                        tm.Rollback();
                        return;
                    }
                    DataRepository.RoleProvider.DeepLoad(role);

                    // Kiem tra xem co cho nao dang su dung room hay khong
                    if (role.GroupRoleCollection.Exists(x => !x.IsDisabled) ||
                        role.UserRoleCollection.Exists(x => !x.IsDisabled)
                        || role.RoleDetailCollection.Exists(x => !x.IsDisabled))
                    {
                        WebCommon.AlertGridView(sender, String.Format("Role {0} is using, you cannot delete it.", role.Title));
                        tm.Rollback();
                        return;
                    }
                    role.IsDisabled = true;
                    role.UpdateUser = AccountSession.Session;
                    role.UpdateDate = DateTime.Now;
                    DataRepository.RoleProvider.Update(tm, role);
                }
                tm.Commit();
                WebCommon.AlertGridView(sender, String.Format("{0} deleted successfully.",
                                      grid.Selection.Count > 1 ? "Roles are" : "Role is"));

                grid.Selection.UnselectAll();
            }
        }
        catch (Exception ex)
        {
            tm.Rollback();
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete role. Please contact Administrator.");
        }
    }
    #endregion

    #region Role detail
    protected void gridRoleDetail_Init(object sender, EventArgs e)
    {
        try
        {
            // Check reading right
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridRole.Visible = false;
                return;
            }

            // Check null for grid role detail
            var gridRoleDetail = sender as ASPxGridView;
            if (gridRoleDetail == null)
            {
                WebCommon.AlertGridView(sender, _message);
                return;
            }

            // Set page size
            gridRoleDetail.SettingsPager.PageSize = ServiceFacade.SettingsHelper.PageSize;

            // Lay cot co chua cac nut thao tac
            var commandColumn = gridRoleDetail.Columns["Operation"] as GridViewCommandColumn;

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

            int roleId = int.Parse(gridRoleDetail.GetMasterRowKeyValue().ToString());
            var param = RoleDetailDataSource.Parameters["WhereClause"];
            if (param == null)
            {
                RoleDetailDataSource.Parameters.Add("WhereClause"
                    , String.Format("IsDisabled = 'false' AND RoleId = {0}", roleId));
            }
            else
            {
                param.DefaultValue = String.Format("IsDisabled = 'false' AND RoleId = {0}", roleId);
            }
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(this, "System is error. Please contact Administrator.");
        }
    }
    protected void gridRoleDetail_OnCustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        try
        {
            if (e.ButtonID != "btnDelete") return;

            #region Delete row
            // Check deleting right
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                return;
            }

            // Get current grid role detail
            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                WebCommon.AlertGridView(sender, "Cannot find role detail.");
                return;
            }

            int id;
            // Kiem tra xem id cua roledetail co phai la so khong
            if (Int32.TryParse(grid.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
            {
                // Neu id la so thi tien hanh kiem tra xem role detail co duoc su dung khong
                var roleDetail = DataRepository.RoleDetailProvider.GetById(id);

                // Kiem tra xem roledetail co ton tai hay khong
                // neu khong ton tai thi bao loi
                if (roleDetail == null || roleDetail.IsDisabled)
                {
                    WebCommon.AlertGridView(sender, "Role detail is not existed. You cannot delete it.");
                    return;
                }
                roleDetail.IsDisabled = true;
                roleDetail.UpdateUser = AccountSession.Session;
                roleDetail.UpdateDate = DateTime.Now;
                DataRepository.RoleDetailProvider.Update(roleDetail);

                // Show message alert delete successfully
                WebCommon.AlertGridView(sender, "Role detail is deleted successfully.");
            }
            #endregion
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete role detail. Please contact Administrator");
        }

    }
    protected void gridRoleDetail_RowInserting(object sender, ASPxDataInsertingEventArgs e)
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

            var crud = GetStringCrud(sender, e);
            var roleId = grid.GetMasterRowKeyValue();

            // Validate empty field
            if (!WebCommon.ValidateEmpty("Role", roleId, out _message)
                || !WebCommon.ValidateEmpty("Screen", e.NewValues["ScreenCode"], out _message)
                || !WebCommon.ValidateEmpty("Right", crud, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }
            
            // Set pure value
            e.NewValues["ScreenCode"] = e.NewValues["ScreenCode"].ToString().Trim();
            e.NewValues["RoleId"] = roleId;
            e.NewValues["Crud"] = crud;
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = AccountSession.Session;
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;

            // Show message alert delete successfully
            WebCommon.AlertGridView(sender, "Role detail is created successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            e.Cancel = true;
            WebCommon.AlertGridView(sender, "Cannot create new role detail. Please contact Administrator");
        }
    }
    protected void gridRoleDetail_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        try
        {
            // Validate user right for updating
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Update.Key, out _message))//Kiem tra xem user co quyen cap nhat hay khong
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
                return;
            }

            var crud = GetStringCrudUpdate(sender, e);
            var roleId = grid.GetMasterRowKeyValue();

            // Validate empty field
            if (!WebCommon.ValidateEmpty("Role", roleId, out _message)
                || !WebCommon.ValidateEmpty("Screen", e.NewValues["ScreenCode"], out _message)
                || !WebCommon.ValidateEmpty("Right", crud, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Set pure value
            e.NewValues["ScreenCode"] = e.NewValues["ScreenCode"].ToString().Trim();
            e.NewValues["RoleId"] = roleId;
            e.NewValues["Crud"] = crud;
            e.NewValues["UpdateUser"] = AccountSession.Session;
            e.NewValues["UpdateDate"] = DateTime.Now;

            // Show message alert delete successfully
            WebCommon.AlertGridView(sender, "Role detail is updated successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            e.Cancel = true;
            WebCommon.AlertGridView(sender, "Cannot update role detail. Please contact Administrator");
        }
    }

    /// <summary>
    /// Xoa nhieu role detail
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridRoleDetail_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
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
                    WebCommon.AlertGridView(sender, "Cannot find role detail.");
                    tm.Rollback();
                    return;
                }

                // Lay danh sach Id cua cac row duoc select
                var fieldNames = new[] { "Id" };
                List<object> columnValues = grid.GetSelectedFieldValues(fieldNames);

                // Doi trang thai cua cac roster
                foreach (object roleId in columnValues)
                {
                    int id;
                    if (!Int32.TryParse(roleId.ToString(), out id))
                    {
                        WebCommon.AlertGridView(sender, "Role detail is invalid. You cannot delete it.");
                        tm.Rollback();
                        return;
                    }

                    var roleDetail = DataRepository.RoleDetailProvider.GetById(id);
                    if (roleDetail == null || roleDetail.IsDisabled)
                    {
                        WebCommon.AlertGridView(sender, "Role detail is not existed. You cannot delete it.");
                        tm.Rollback();
                        return;
                    }

                    roleDetail.IsDisabled = true;
                    roleDetail.UpdateUser = AccountSession.Session;
                    roleDetail.UpdateDate = DateTime.Now;
                    DataRepository.RoleDetailProvider.Update(tm, roleDetail);
                }
                tm.Commit();
                WebCommon.AlertGridView(sender, String.Format("{0} deleted successfully.",
                                                      grid.Selection.Count > 1 ? "Role details are" : "Role detail is"));

                // Set tam pageIndex de lay duoc danh sach moi
                grid.Selection.UnselectAll();
            }
        }
        catch (Exception ex)
        {
            tm.Rollback();
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete role detail. Please contact Administrator.");
        }
    }
    #endregion

    #region Checkbox in role detail
    protected string GetStringCrud(object sender, ASPxDataInsertingEventArgs e)//lay du lieu trong check box khi them moi
    {
        try
        {
            string crud = string.Empty;

            // Validate null
            var asPxGridView = sender as ASPxGridView;
            if (asPxGridView == null)
            {
                return crud;
            }

            var gridColumn = asPxGridView.Columns["Right"] as GridViewDataColumn;

            var chkr = (ASPxCheckBox)asPxGridView.FindEditRowCellTemplateControl(gridColumn, "ckcR");
            var chku = (ASPxCheckBox)asPxGridView.FindEditRowCellTemplateControl(gridColumn, "ckcU");
            var chkd = (ASPxCheckBox)asPxGridView.FindEditRowCellTemplateControl(gridColumn, "ckcD");
            var chkc = (ASPxCheckBox)asPxGridView.FindEditRowCellTemplateControl(gridColumn, "ckcC");
            if (chkc.Checked) crud += OperationConstant.Create.Key;
            if (chkr.Checked) crud += OperationConstant.Read.Key;
            if (chku.Checked) crud += OperationConstant.Update.Key;
            if (chkd.Checked) crud += OperationConstant.Delete.Key;
            return crud;
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return string.Empty;
        }
    }

    protected string GetStringCrudUpdate(object sender, ASPxDataUpdatingEventArgs e)
    {
        try
        {
            string crud = string.Empty;

            // Validate null
            var asPxGridView = sender as ASPxGridView;
            if (asPxGridView == null)
            {
                return crud;
            }

            var gridColumn = asPxGridView.Columns["Right"] as GridViewDataColumn;

            var chkr = (ASPxCheckBox)asPxGridView.FindEditRowCellTemplateControl(gridColumn, "ckcR");
            var chku = (ASPxCheckBox)asPxGridView.FindEditRowCellTemplateControl(gridColumn, "ckcU");
            var chkd = (ASPxCheckBox)asPxGridView.FindEditRowCellTemplateControl(gridColumn, "ckcD");
            var chkc = (ASPxCheckBox)asPxGridView.FindEditRowCellTemplateControl(gridColumn, "ckcC");
            if (chkc.Checked) crud += OperationConstant.Create.Key;
            if (chkr.Checked) crud += OperationConstant.Read.Key;
            if (chku.Checked) crud += OperationConstant.Update.Key;
            if (chkd.Checked) crud += OperationConstant.Delete.Key;
            return crud;
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return string.Empty;
        }
    }

    //Danh dau check box xem co quyen khong dua vao ky tu truyen vao
    public bool GetCheckbox(object crud, string chars)
    {
        return crud != null && crud.ToString().Contains(chars);
    }
    #endregion
}