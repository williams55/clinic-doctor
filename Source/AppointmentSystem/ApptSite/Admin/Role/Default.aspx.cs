using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
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
            // Neu la postback thi bo qua
            if (IsPostBack)
            {
                return;
            }
            
            // Check reading right
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key, out _message))
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
            if (Int32.TryParse(gridRole.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
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
    #endregion

    #region Role detail
    protected void gridRoleDetail_Init(object sender, EventArgs e)
    {
        try
        {
            // Check reading right
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key, out _message))
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
            var commandColumn = gridRole.Columns["Operation"] as GridViewCommandColumn;

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

            int roleId = int.Parse(gridRoleDetail.GetMasterRowKeyValue().ToString());
            var param = RoleDetailDataSource.Parameters["WhereClause"];
            if (param == null)
            {
                RoleDetailDataSource.Parameters.Add("WhereClause"
                    , String.Format("IsDisabled = 'false' AND RoleId = {0}", roleId));
            }
            else
            {
                param.DefaultValue = String.Format("IsDisabled = 'false' AND RoleId = {0}",
                                      (sender as ASPxGridView).GetMasterRowKeyValue());
            }
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.ShowDialog(this, "System is error. Please contact Administrator.");
        }
    }
    protected void gridRoleDetail_OnCustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
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

            // Get current grid role detail
            var gridRoleDetail = sender as ASPxGridView;
            if (gridRoleDetail == null)
            {
                WebCommon.AlertGridView(sender, "Cannot find role detail.");
                return;
            }

            int id;

            // Kiem tra xem id cua roledetail co phai la so khong
            if (Int32.TryParse(gridRoleDetail.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
            {
                // Neu id la so thi tien hanh kiem tra xem service co duoc su dung khong
                var obj = DataRepository.RoleDetailProvider.GetById(id);

                // Kiem tra xem roledetail co ton tai hay khong
                // neu khong ton tai thi bao loi
                if (obj == null || obj.IsDisabled)
                {
                    WebCommon.AlertGridView(sender, "Role detail is not existed. You cannot delete it.");
                    return;
                }
                DataRepository.RoleDetailProvider.DeepLoad(obj);

                obj.IsDisabled = true;
                obj.UpdateUser = WebCommon.GetAuthUsername();
                obj.UpdateDate = DateTime.Now;
                DataRepository.RoleDetailProvider.Update(obj);
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
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Check null for grid role detail
            var gridRoleDetail = sender as ASPxGridView;
            if (gridRoleDetail == null)
            {
                WebCommon.AlertGridView(sender, _message);
                return;
            }

            var crud = GetStringCrud(sender, e);
            var roleId = gridRoleDetail.GetMasterRowKeyValue();

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
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
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
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Update.Key, out _message))//Kiem tra xem user co quyen cap nhat hay khong
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Check null for grid role detail
            var gridRoleDetail = sender as ASPxGridView;
            if (gridRoleDetail == null)
            {
                WebCommon.AlertGridView(sender, _message);
                return;
            }

            var crud = GetStringCrudUpdate(sender, e);
            var roleId = gridRoleDetail.GetMasterRowKeyValue();

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
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;

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