﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentSystem.Settings.BusinessLayer;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxUploadControl;
using DevExpress.Web.ASPxEditors;
using AppointmentSystem.Entities;
using AppointmentSystem.Data;
using AppointmentBusiness.Util;
using Appt.Common.Constants;
using Log.Controller;
using Common.Util;
public partial class Admin_Users_EditUser : System.Web.UI.Page
{
    const string UploadDirectory = "~/Images/";
    string ThumbnailFileName = "tuan1.jpg";
    string ScreenCode = "User";
    static string _message;
    protected void Page_Load(object sender, EventArgs e)
    {      
        try
        {
            bool result = false;
            bindUser();
            result=RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key,
                                       out _message);
            if (!result)
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridUser.Visible = false;
            }
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
        ASPxGridView aspxgrid = sender as ASPxGridView;
        FileUpload fileupload = (FileUpload)aspxgrid.FindEditFormTemplateControl("Uploadimg");

        ASPxTextBox idb = (ASPxTextBox)(aspxgrid.FindEditFormTemplateControl("ASPxTextBox1"));
        string result = string.Empty;
        if (fileupload != null)
            result = SaveImage(fileupload);
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
            return string.Empty;
        }
        string filename = Server.MapPath(UploadDirectory + uploadedFile.FileName);
        uploadedFile.PostedFile.SaveAs(filename);
        return uploadedFile.FileName;
    }
}
