using System;
using System.Collections.Generic;
using System.Linq;
using AppointmentBusiness.Util;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using Appt.Common.Constants;
using ApptSite;
using Common.Util;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using Log.Controller;
using DevExpress.Web.ASPxEditors;
public partial class Admin_Users_EditUser : System.Web.UI.Page
{
    private const string ScreenCode = "User";
    static string _message;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Check reading right
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridUser.Visible = false;
                return;
            }

            // Set page size
            gridUser.SettingsPager.PageSize = ServiceFacade.SettingsHelper.PageSize;

            // Lay cot co chua cac nut thao tac
            var commandColumn = gridUser.Columns["Operation"] as GridViewCommandColumn;

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

    #region User
    /// <summary>
    /// Xoa mot user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridUser_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
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
                WebCommon.AlertGridView(sender, "Cannot find user.");
                return;
            }

            string username = grid.GetRowValues(e.VisibleIndex, "Username").ToString();
            var user = DataRepository.UsersProvider.GetByUsername(username);
            if (user == null || user.IsDisabled)
            {
                WebCommon.AlertGridView(sender, "User is not existed. You cannot delete it.");
                return;
            }
            DataRepository.UsersProvider.DeepLoad(user);

            // Kiem tra xem user co duoc su dung chua
            if (user.DoctorRoomCollection.Exists(x => !x.IsDisabled) || user.AppointmentCollection.Exists(x => !x.IsDisabled)
                || user.RosterCollection.Exists(x => !x.IsDisabled))
            {
                WebCommon.AlertGridView(sender, String.Format("{0} is using, you cannot delete.", user.DisplayName));
                return;
            }
            user.IsDisabled = true;
            user.UpdateUser = AccountSession.Session;
            user.UpdateDate = DateTime.Now;
            DataRepository.UsersProvider.Update(user);

            // Show message alert delete successfully
            WebCommon.AlertGridView(sender, "User is deleted successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete user. Please contact Administrator.");
        }
    }

    /// <summary>
    /// Xoa nhieu user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridUser_OnCustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
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
                    WebCommon.AlertGridView(sender, "Cannot find user.");
                    tm.Rollback();
                    return;
                }

                // Lay danh sach Id cua cac row duoc select
                var fieldNames = new[] { "Username" };
                List<object> columnValues = grid.GetSelectedFieldValues(fieldNames);

                // Doi trang thai cua cac roster
                foreach (object username in columnValues)
                {
                    var user = DataRepository.UsersProvider.GetByUsername(username.ToString());
                    if (user == null || user.IsDisabled)
                    {
                        WebCommon.AlertGridView(sender, "User is not existed. You cannot delete it.");
                        tm.Rollback();
                        return;
                    }
                    DataRepository.UsersProvider.DeepLoad(user);

                    // Kiem tra xem user co duoc su dung chua
                    if (user.DoctorRoomCollection.Exists(x => !x.IsDisabled) || user.AppointmentCollection.Exists(x => !x.IsDisabled)
                        || user.RosterCollection.Exists(x => !x.IsDisabled))
                    {
                        WebCommon.AlertGridView(sender, String.Format("{0} is using, you cannot delete.", user.DisplayName));
                        tm.Rollback();
                        return;
                    }
                    user.IsDisabled = true;
                    user.UpdateUser = AccountSession.Session;
                    user.UpdateDate = DateTime.Now;
                    DataRepository.UsersProvider.Update(tm, user);
                }
                tm.Commit();
                WebCommon.AlertGridView(sender, String.Format("{0} deleted successfully.",
                                      grid.Selection.Count > 1 ? "Users are" : "User is"));

                grid.Selection.UnselectAll();
            }
        }
        catch (Exception ex)
        {
            tm.Rollback();
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete user. Please contact Administrator.");
        }
    }

    protected void gridUser_RowInserting(object sender, ASPxDataInsertingEventArgs e)
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

            // Get control and validate
            var radMale = grid.FindEditFormTemplateControl("radMale") as ASPxRadioButton;
            var radFemale = grid.FindEditFormTemplateControl("radFemale") as ASPxRadioButton;
            var txtPsw = grid.FindEditFormTemplateControl("txtPsw") as ASPxTextBox;

            if (radMale == null || radFemale == null || txtPsw == null)
            {
                WebCommon.AlertGridView(sender, "Add form is error.");
                e.Cancel = true;
                return;
            }

            // Validate empty field
            if (!WebCommon.ValidateEmpty("Username", e.NewValues["Username"], out _message)
                || !WebCommon.ValidateEmpty("Password", txtPsw.Text, out _message)
                || !WebCommon.ValidateEmpty("Service", e.NewValues["ServicesId"], out _message)
                || !WebCommon.ValidateEmpty("Group", e.NewValues["UserGroupId"], out _message)
                || !WebCommon.ValidateEmpty("First Name", e.NewValues["Firstname"], out _message)
                || !WebCommon.ValidateEmpty("Last Name", e.NewValues["Lastname"], out _message)
                || !WebCommon.ValidateEmpty("Display Name", e.NewValues["DisplayName"], out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Check username is exists
            var user = DataRepository.UsersProvider.GetByUsername(e.NewValues["Username"].ToString());
            if (user != null)
            {
                WebCommon.AlertGridView(sender, "Username is existed.");
                e.Cancel = true;
                return;

            }

            // Validate service field
            int serviceId;
            if (!Int32.TryParse(e.NewValues["ServicesId"].ToString(), out serviceId))
            {
                WebCommon.AlertGridView(sender, "Service is invalid.");
                e.Cancel = true;
                return;
            }
            var service = DataRepository.ServicesProvider.GetById(serviceId);
            if (service == null || service.IsDisabled)
            {
                WebCommon.AlertGridView(sender, "Service is not existed.");
                e.Cancel = true;
                return;
            }

            // Validate group field
            var userGroup = DataRepository.UserGroupProvider.GetById(e.NewValues["UserGroupId"].ToString());
            if (userGroup == null || userGroup.IsDisabled)
            {
                WebCommon.AlertGridView(sender, "Group is not existed.");
                e.Cancel = true;
                return;
            }

            // Check email is exists
            string squery = string.Format("Email='{0}' and IsDisabled='False'", e.NewValues["Email"]);
            int count;
            if (DataRepository.UsersProvider.GetPaged(squery, "", 0, 0, out count).Any())
            {
                WebCommon.AlertGridView(sender, "Email is existed.");
                e.Cancel = true;
                return;
            }

            e.NewValues["Username"] = e.NewValues["Username"].ToString().Trim();
            e.NewValues["Password"] = Encrypt.EncryptPassword(txtPsw.Text);
            e.NewValues["Firstname"] = FString.ToTitleCase(e.NewValues["Firstname"].ToString().Trim());
            e.NewValues["Lastname"] = FString.ToTitleCase(e.NewValues["Lastname"].ToString().Trim());
            e.NewValues["DisplayName"] = FString.ToTitleCase(e.NewValues["DisplayName"].ToString().Trim());
            e.NewValues["CellPhone"] = e.NewValues["CellPhone"] == null ? string.Empty : e.NewValues["CellPhone"].ToString().Trim();
            e.NewValues["Email"] = e.NewValues["Email"] == null ? string.Empty : e.NewValues["Email"].ToString().Trim();
            e.NewValues["Note"] = e.NewValues["Note"] == null ? string.Empty : e.NewValues["Note"].ToString().Trim();
            e.NewValues["UserGroupId"] = e.NewValues["UserGroupId"].ToString().Trim();
            e.NewValues["ServicesId"] = serviceId;
            e.NewValues["IsFemale"] = radFemale.Checked;
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = AccountSession.Session;
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
            WebCommon.AlertGridView(sender, "User is created successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot insert user. Please contact Administrator");
            e.Cancel = true;
        }
    }

    protected void gridUser_OnRowInserted(object sender, ASPxDataInsertedEventArgs e)
    {
        //TransactionManager tm = DataRepository.Provider.CreateTransaction();
        //tm.BeginTransaction();
        //try
        //{
        //    // Validate group field
        //    var userGroup = DataRepository.UserGroupProvider.GetById(e.NewValues["UserGroupId"].ToString());
        //    if (userGroup == null || userGroup.IsDisabled)
        //    {
        //        WebCommon.AlertGridView(sender, "Group is not existed.");
        //        tm.Rollback();
        //        return;
        //    }
        //    DataRepository.UserGroupProvider.DeepLoad(userGroup);

        //    foreach (var groupRole in userGroup.GroupRoleCollection)
        //    {
        //        DataRepository.UserRoleProvider.Insert(tm, new UserRole
        //            {
        //                Username = e.NewValues["Username"].ToString(),
        //                RoleId = groupRole.RoleId,
        //                CreateDate = DateTime.Now,
        //                CreateUser = AccountSession.Session,
        //                UpdateDate = DateTime.Now,
        //                UpdateUser = AccountSession.Session,
        //            });
        //    }
        //    tm.Commit();
        //}
        //catch (Exception ex)
        //{
        //    tm.Rollback();
        //    LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        //    WebCommon.AlertGridView(sender, "Cannot insert user role. Please contact Administrator");
        //}
    }

    protected void gridUser_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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
            if (!WebCommon.ValidateEmpty("Service", e.NewValues["ServicesId"], out _message)
                || !WebCommon.ValidateEmpty("Group", e.NewValues["UserGroupId"], out _message)
                || !WebCommon.ValidateEmpty("First Name", e.NewValues["Firstname"], out _message)
                || !WebCommon.ValidateEmpty("Last Name", e.NewValues["Lastname"], out _message)
                || !WebCommon.ValidateEmpty("Display Name", e.NewValues["DisplayName"], out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Get control and validate
            var radMale = grid.FindEditFormTemplateControl("radMale") as ASPxRadioButton;
            var radFemale = grid.FindEditFormTemplateControl("radFemale") as ASPxRadioButton;
            var txtCPsw = grid.FindEditFormTemplateControl("txtCPsw") as ASPxTextBox;

            if (radMale == null || radFemale == null || txtCPsw == null)
            {
                WebCommon.AlertGridView(sender, "Add form is error.");
                e.Cancel = true;
                return;
            }

            // Validate service field
            int serviceId;
            if (!Int32.TryParse(e.NewValues["ServicesId"].ToString(), out serviceId))
            {
                WebCommon.AlertGridView(sender, "Service is invalid.");
                e.Cancel = true;
                return;
            }
            var service = DataRepository.ServicesProvider.GetById(serviceId);
            if (service == null || service.IsDisabled)
            {
                WebCommon.AlertGridView(sender, "Service is not existed.");
                e.Cancel = true;
                return;
            }

            // Validate group field
            var userGroup = DataRepository.UserGroupProvider.GetById(e.NewValues["UserGroupId"].ToString());
            if (userGroup == null || userGroup.IsDisabled)
            {
                WebCommon.AlertGridView(sender, "Group is not existed.");
                e.Cancel = true;
                return;
            }

            //// Check email is exists
            //string squery = string.Format("Email='{0}' and IsDisabled='False' and Username!='{1}'", e.NewValues["Email"], e.OldValues["Username"]);
            //int count;
            //if (DataRepository.UsersProvider.GetPaged(squery, "", 0, 0, out count).Any())
            //{
            //    WebCommon.AlertGridView(sender, "Email is existed.");
            //    e.Cancel = true;
            //    return;
            //}

            if (!string.IsNullOrEmpty(txtCPsw.Text))
            {
                e.NewValues["Password"] = Encrypt.EncryptPassword(txtCPsw.Text);
            }
            e.NewValues["Firstname"] = e.NewValues["Firstname"].ToString().Trim();
            e.NewValues["Lastname"] = e.NewValues["Lastname"].ToString().Trim();
            e.NewValues["DisplayName"] = e.NewValues["DisplayName"].ToString().Trim();
            e.NewValues["CellPhone"] = e.NewValues["CellPhone"] == null ? string.Empty : e.NewValues["CellPhone"].ToString().Trim();
            e.NewValues["Email"] = e.NewValues["Email"] == null ? string.Empty : e.NewValues["Email"].ToString().Trim();
            e.NewValues["Note"] = e.NewValues["Note"] == null ? string.Empty : e.NewValues["Note"].ToString().Trim();
            e.NewValues["UserGroupId"] = e.NewValues["UserGroupId"].ToString().Trim();
            e.NewValues["ServicesId"] = serviceId;
            e.NewValues["IsFemale"] = radFemale.Checked;
            e.NewValues["UpdateUser"] = AccountSession.Session;
            e.NewValues["UpdateDate"] = DateTime.Now;
            WebCommon.AlertGridView(sender, "User is update successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot update user. Please contact Administrator");
            e.Cancel = true;
        }
    }
    #endregion

    #region UserRole
    protected void gridUserRole_OnInit(object sender, EventArgs e)
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

            string username = grid.GetMasterRowKeyValue().ToString();
            var param = UserRoleDataSource.Parameters["WhereClause"];
            if (param == null)
            {
                UserRoleDataSource.Parameters.Add("WhereClause"
                    , String.Format("IsDisabled = 'false' AND Username='{0}'", username));
            }
            else
            {
                param.DefaultValue = String.Format("IsDisabled = 'false' AND Username='{0}'", username);
            }
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(this, "System is error. Please contact Administrator.");
        }
    }

    protected void gridUserRole_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
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
                WebCommon.AlertGridView(sender, "Cannot find user role.");
                return;
            }

            long id;
            if (Int64.TryParse(grid.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
            {
                // Neu id la so thi tien hanh kiem tra xem user role co duoc su dung khong
                var userRole = DataRepository.UserRoleProvider.GetById(id);

                // Kiem tra xem roledetail co ton tai hay khong
                // neu khong ton tai thi bao loi
                if (userRole == null || userRole.IsDisabled)
                {
                    WebCommon.AlertGridView(sender, "User role is not existed. You cannot delete it.");
                    return;
                }
                //Change disable to delete
                //userRole.IsDisabled = true;
                //userRole.UpdateUser = AccountSession.Session;
                //userRole.UpdateDate = DateTime.Now;
                //DataRepository.UserRoleProvider.Update(userRole);
                DataRepository.UserRoleProvider.Delete(userRole);

                // Show message alert delete successfully
                WebCommon.AlertGridView(sender, "User role is deleted successfully.");
            }
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete user role. Please contact Administrator.");
        }
    }

    protected void gridUserRole_RowInserting(object sender, ASPxDataInsertingEventArgs e)
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

            string username = grid.GetMasterRowKeyValue().ToString();
            if (!WebCommon.ValidateEmpty("Role", e.NewValues["RoleId"], out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            e.NewValues["Username"] = username;
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = AccountSession.Session;
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;

            WebCommon.AlertGridView(sender, "User role is created successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot create user role. Please contact Administrator");
            e.Cancel = true;
        }
    }

    /// <summary>
    /// Xoa nhieu user role
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridUserRole_OnCustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
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
                    WebCommon.AlertGridView(sender, "Cannot find user role.");
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
                        WebCommon.AlertGridView(sender, "User role is invalid. You cannot delete it.");
                        tm.Rollback();
                        return;
                    }

                    var userRole = DataRepository.UserRoleProvider.GetById(id);
                    if (userRole == null || userRole.IsDisabled)
                    {
                        WebCommon.AlertGridView(sender, "User role is not existed. You cannot delete it.");
                        tm.Rollback();
                        return;
                    }

                    userRole.IsDisabled = true;
                    userRole.UpdateUser = AccountSession.Session;
                    userRole.UpdateDate = DateTime.Now;
                    DataRepository.UserRoleProvider.Update(tm, userRole);
                }
                tm.Commit();
                WebCommon.AlertGridView(sender, String.Format("{0} deleted successfully.",
                                                      grid.Selection.Count > 1 ? "User role are" : "User role is"));

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

    /// <summary>
    /// Loc bot cac role da co cua user khi tao moi 1 role
    /// Tam bo, ko xai
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridUserRole_OnInitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
    {
        try
        {
            // Get current grid user role
            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                WebCommon.AlertGridView(sender, "Cannot find user role.");
                return;
            }

            int count;
            string username = grid.GetMasterRowKeyValue().ToString();

            // Lay cot co chua id
            var commandColumn = grid.Columns["Role"] as GridViewDataComboBoxColumn;

            // Neu khong co cot do thi khong can lam tiep
            if (commandColumn == null) return;

            var cboRole = (ASPxComboBox)grid.FindEditFormTemplateControl("cboUserRole");
            TList<Role> roles = DataRepository.RoleProvider.GetPaged("IsDisabled='false'", "", 0, 0, out count);
            TList<UserRole> userRoles = DataRepository.UserRoleProvider.GetPaged(string.Format("IsDisabled='false' and Username='{0}'", username), "", 0, 0, out count);
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
