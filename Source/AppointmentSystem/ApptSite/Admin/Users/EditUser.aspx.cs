using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
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
            result=RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key,
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
    protected void UserRoleGrid_DataSelect(object sender, EventArgs e)
    {
        var parameter = UserRoleDatas.Parameters["whereClause"];
        string username= (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        if (parameter == null)
        {

            UserRoleDatas.Parameters.Add("WhereClause", String.Format("IsDisabled = 'false' AND Username = '{0}'",username));
        }
        else
        {
            parameter.DefaultValue = String.Format("IsDisabled = 'false' AND Username = '{0}'",
                                  (sender as ASPxGridView).GetMasterRowKeyValue());
        }
    }
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
            if (iusers!=null)
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "Field User name is exists";
                e.Cancel = true;
                return;
            
            }
            //check email is exists
            string squery = string.Format("Email='{0}' and IsDisabled='False'",e.NewValues["Email"]);
            int count=0;
            TList<Users> user = DataRepository.UsersProvider.GetPaged(squery,"",0, 0, out count);
            if (user.Count>0)
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
            string squery = string.Format("Email='{0}' and IsDisabled='False' and Username!='{1}'", e.NewValues["Email"],e.OldValues["Username"]);
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
    /// <summary>
    /// loc nhung role ma co the them cho user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void UserRoleGrid_OnInitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        int count;
        string username= (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        ASPxGridView userrole = (ASPxGridView)sender;
        ASPxComboBox cboroleid = (ASPxComboBox)userrole.FindEditFormTemplateControl("cboroleid");
        TList<Role> listrole = DataRepository.RoleProvider.GetPaged("IsDisabled='false'","",0,0,out count);
        TList<UserRole> userroleq = DataRepository.UserRoleProvider.GetPaged(string.Format("IsDisabled='false' and Username='{0}'",username), "", 0, 0, out count);
        var lstroleid = listrole.FindAll(role=> !userroleq.Exists(role2=>role.Id==role2.RoleId));
        cboroleid.DataSource = lstroleid;
        cboroleid.DataBindItems();
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
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage]
                        = String.Format("You do not have permission to delete", "Delete User");
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
    protected void UserRoleGrid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                return;
            }
            if (e.ButtonID != "btnDeleteRole") return;
            ASPxGridView girdUserRole = (ASPxGridView)(sender as ASPxGridView);//lay gia tri bang cua gridview
            Int64 id;
            if (Int64.TryParse(girdUserRole.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
            {
                var obj = DataRepository.UserRoleProvider.GetById(id);
                obj.IsDisabled = true;
                obj.UpdateUser = WebCommon.GetAuthUsername();
                obj.UpdateDate = DateTime.Now;
                DataRepository.UserRoleProvider.Update(obj);
            }
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    protected void UserRoleGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key,
                                       out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                return;
            }
            string Username = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
            if (e.NewValues["RoleId"]==null)
            {
                WebCommon.AlertGridView(sender,"You not select role");
                e.Cancel = true;
                return;
            }
            string RoleId = e.NewValues["RoleId"].ToString();
            e.NewValues["Username"] = Username;
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;

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
}
