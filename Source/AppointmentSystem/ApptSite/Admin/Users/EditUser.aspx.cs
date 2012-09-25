using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web.UI.WebControls;
using AppointmentBusiness.Util;
using AppointmentSystem.Data;
using AppointmentSystem.Settings.BusinessLayer;
using Appt.Common.Constants;
using Common.Util;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxUploadControl;
using Log.Controller;
using Image = System.Drawing.Image;


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
        e.CallbackData = SavePostedFile(e.UploadedFile);
    }

    string SavePostedFile(UploadedFile uploadedFile)
    {
        if (!uploadedFile.IsValid)
            return string.Empty;
        string fileName = Path.Combine(MapPath(UploadDirectory), ThumbnailFileName);
        using (Image original = Image.FromStream(uploadedFile.FileContent))
        using (Image thumbnail = PhotoUtils.Inscribe(original, 100))
        {
            PhotoUtils.SaveToJpeg(thumbnail, fileName);
        }
        return ThumbnailFileName;
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
            if (e.NewValues["Username"].ToString().Trim().Length < 1||e.NewValues["Username"]==null)
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "Field User name can not be empty";
                e.Cancel = true;
            }
            if (e.NewValues["DisplayName"].ToString().Trim().Length < 1)
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "Field Display name can not be empty";
                e.Cancel = true;
                return;
            }
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    protected void gridUser_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (Session["imagedata"] != null)
        {
            e.NewValues["Avatar"] = Session["imagedata"];
            Session.Remove("imagedata");
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
            //if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Delete.Key, out _message))
            //{
            //    WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
            //    return;
            //}
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

            string UserId = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
            string RoleId = e.NewValues["RoleId"].ToString();
            int count;
            string query = string.Format("UserId='{0}' and RoleId={1} and IsDisabled='false'", UserId, RoleId);
            var obj = DataRepository.UserRoleProvider.GetPaged(query, string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);
            if (count > 0)
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = String.Format("Role {0} is using, you cannot delete it.", RoleId);
                e.Cancel = true;
                return;
            }
            e.NewValues["UserId"] = UserId;
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
