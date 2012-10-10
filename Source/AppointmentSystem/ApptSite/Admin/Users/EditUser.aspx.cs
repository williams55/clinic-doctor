using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using AppointmentBusiness.Util;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using Appt.Common.Constants;
using Common.Util;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxUploadControl;
using DevExpress.Web.Data;
using Log.Controller;
using System.Collections;
using DevExpress.Web.ASPxEditors;
public partial class Admin_Users_EditUser : System.Web.UI.Page
{
    string ScreenCode = "User";
    static string _message;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            bool result = false;
            //bindUser();
            result = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key,
                                       out _message);
            if (!result)
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridUser.Visible = false;
            }

            // Set page size
            gridUser.SettingsPager.PageSize = ServiceFacade.SettingsHelper.PageSize;
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }

    }
    protected void bindUser()
    {
        var listUser = DataRepository.UsersProvider.GetAll();
        gridUser.DataSource = listUser;
        gridUser.DataBind();
    }
    const string UploadDirectory = "~/Admin/Images/";
    const string ThumbnailFileName = "ThumbnailImage.jpg";
    protected void uplImage_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
    }
    public static Bitmap ResizeImage(System.Drawing.Image original, int newWidth, int newHeight)
    {
        if (original.Width == 0 || original.Height == 0 || newWidth == 0 || newHeight == 0) return null;
        if (original.Width < newWidth) newWidth = original.Width;
        if (original.Height < newHeight) newHeight = original.Height;
        Bitmap newBitmap = new Bitmap(newWidth, newHeight);
        Graphics g = Graphics.FromImage(newBitmap);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.DrawImage(original, 0, 0, newWidth, newHeight);
        return newBitmap;
    }
    //protected void UserRoleGrid_DataSelect(object sender, EventArgs e)
    //{
    //    var parameter = UserRoleDatas.Parameters["whereClause"];
    //    string username= (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
    //    if (parameter == null)
    //    {

    //        UserRoleDatas.Parameters.Add("WhereClause", String.Format("IsDisabled = 'false' AND Username = '{0}'",username));
    //    }
    //    else
    //    {
    //        parameter.DefaultValue = String.Format("IsDisabled = 'false' AND Username = '{0}'",
    //                              (sender as ASPxGridView).GetMasterRowKeyValue());
    //    }
    //}
    protected void UserGroupGrid_DataSelect(object sender, EventArgs e)
    {
        var parameter = UserRoleDatas.Parameters["whereClause"];
        if (parameter == null)
        {

            UserRoleDatas.Parameters.Add("WhereClause", String.Format("IsDisabled = 'false' AND Username = '{0}'", (sender as ASPxGridView).GetMasterRowKeyValue()));
        }
        else
        {
            parameter.DefaultValue = String.Format("IsDisabled = 'false' AND Username = '{0}'",
                                  (sender as ASPxGridView).GetMasterRowKeyValue());
        }
    }
    protected void gridUser_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key,
                                       out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }
            // Validate empty field
            if (!WebCommon.ValidateEmpty("Username", e.NewValues["Username"], out _message)
                || !WebCommon.ValidateEmpty("First name", e.NewValues["Firstname"], out _message)
                || !WebCommon.ValidateEmpty("Last name", e.NewValues["Lastname"], out _message)
                || !WebCommon.ValidateEmpty("Display name", e.NewValues["DisplayName"], out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }
            //check username is exists
            Users iusers = (Users)DataRepository.UsersProvider.GetByUsername(e.NewValues["Username"].ToString());
            if (iusers != null)
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "Field User name is exists";
                e.Cancel = true;
                return;

            }
            //check email is exists
            string squery = string.Format("Email='{0}' and IsDisabled='False'", e.NewValues["Email"]);
            int count = 0;
            TList<Users> user = DataRepository.UsersProvider.GetPaged(squery, "", 0, 0, out count);
            if (user.Count > 0)
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "Field Email is exists";
                e.Cancel = true;
                return;
            }
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
            WebCommon.AlertGridView(sender, "User is created successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot insert role Please contact Administrator");
        }
    }
    protected void gridUser_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key,
                                       out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }
            // Validate empty field
            if (!WebCommon.ValidateEmpty("First name", e.NewValues["Firstname"], out _message)
                || !WebCommon.ValidateEmpty("Last name", e.NewValues["Lastname"], out _message)
                || !WebCommon.ValidateEmpty("Display name", e.NewValues["DisplayName"], out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }
            //check email is exists
            string squery = string.Format("Email='{0}' and IsDisabled='False' and Username!='{1}'", e.NewValues["Email"], e.OldValues["Username"]);
            int count = 0;
            TList<Users> user = DataRepository.UsersProvider.GetPaged(squery, "", 0, 0, out count);
            if (user.Count > 0)
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "Field Email is exists";
                e.Cancel = true;
                return;
            }
            e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["UpdateDate"] = DateTime.Now;
            WebCommon.AlertGridView(sender, "User is update successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    protected void gridUser_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        //string perxe = ServiceFacade.SettingsHelper.UserPrefix;
        //int count = 0;
        //TList<Users> objuser = DataRepository.UsersProvider.GetPaged("Id like '" + perxe + "' + '%'", "Id desc", 0, 1, out count);
        //if (count == 0)
        //{
        //    e.NewValues["Id"] = perxe + "0001";
        //}
        //else
        //{
        //    e.NewValues["Id"] = perxe + string.Format("{0:0000}", int.Parse(objuser[0].Id.Substring(objuser[0].Id.Length - 3)) + 1);
        //}
    }

    protected bool Getcheckbox(string obj)
    {
        string str = obj;
        if (str == "true") return true;
        return false;
    }
    protected void gridUser_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {

    }
    protected void gridUser_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //// Checks whether the generated row has the errors.
        //bool hasError = e.GetValue("Username").ToString().Length <= 1;
        //hasError = hasError || e.GetValue("DisplayName").ToString().Length <= 1;
        //hasError = hasError || e.GetValue("UserGroupId") == null;
        //// If the row has the error(s), its text color is set to red.
        //if (hasError)
        //    e.Row.ForeColor = System.Drawing.Color.Red;

    }
    protected void gridUser_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                return;
            }
            if (e.ButtonID != "btnDelete") return;
            string username = gridUser.GetRowValues(e.VisibleIndex, "Username").ToString();
            var obj = DataRepository.UsersProvider.GetByUsername(username);
            if (obj == null || obj.IsDisabled) return;
            DataRepository.UsersProvider.DeepLoad(obj);
            if (obj.DoctorRoomCollection.Exists(x => !x.IsDisabled) || obj.AppointmentCollection.Exists(x => !x.IsDisabled)
                || (obj.ServicesId != null && !obj.ServicesIdSource.IsDisabled) || obj.RosterCollection.Exists(x => !x.IsDisabled))
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = String.Format("{0} is using, you cannot delete it.", obj.DisplayName);
                return;
            }
            obj.IsDisabled = true;
            obj.UpdateUser = WebCommon.GetAuthUsername();
            obj.UpdateDate = DateTime.Now;
            DataRepository.UsersProvider.Update(obj);
            WebCommon.AlertGridView(sender, "User is deleted successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }

    }
    public string SaveImage(FileUpload uploadedFile)
    {
        if (!uploadedFile.HasFile)
        {
            return "you have not select image";
        }
        string directoryimage = Server.MapPath("../Images");
        return string.Empty;
    }

    #region UserRole
    protected void gridUserRole_OnInit(object sender, EventArgs e)
    {
        try
        {
            // Check null for grid user role
            var gridUserRole = sender as ASPxGridView;
            if (gridUserRole == null)
            {
                WebCommon.AlertGridView(sender, _message);
                return;
            }

            // Check reading right
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridUserRole.Visible = false;
                return;
            }

            // Set page size
            gridUserRole.SettingsPager.PageSize = ServiceFacade.SettingsHelper.PageSize;

            // Lay cot co chua cac nut thao tac
            var commandColumn = gridUserRole.Columns["Operation"] as GridViewCommandColumn;

            // Neu khong co cot do thi khong can lam tiep
            if (commandColumn == null) return;

            // Khong cho sua
            commandColumn.EditButton.Visible = false;

            // Set hien thi/an cho nut new
            btnAdd.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode,
                                                        OperationConstant.Create.Key, out _message);

            // Set hien thi/an cho nut delete
            btnGeneralDelete.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(),
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

            string username = gridUserRole.GetMasterRowKeyValue().ToString();
            var param = UserRoleDatas.Parameters["WhereClause"];
            if (param == null)
            {
                UserRoleDatas.Parameters.Add("WhereClause"
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
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
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
                userRole.IsDisabled = true;
                userRole.UpdateUser = WebCommon.GetAuthUsername();
                userRole.UpdateDate = DateTime.Now;
                DataRepository.UserRoleProvider.Update(userRole);

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
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key, out _message))
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
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
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
                if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Delete.Key, out _message))
                {
                    WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
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
                    userRole.UpdateUser = WebCommon.GetAuthUsername();
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
