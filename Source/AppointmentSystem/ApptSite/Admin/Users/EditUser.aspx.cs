using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentSystem.Settings.BusinessLayer;
using DevExpress.Web.ASPxGridView;
using AppointmentSystem.Entities;
using AppointmentSystem.Data;
using AppointmentBusiness.Util;
using Appt.Common.Constants;
using Log.Controller;
using Common.Util;
public partial class Admin_Users_EditUser : System.Web.UI.Page
{
    string ScreenCode = "User";
    static string _message;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Validate user right for reading
            //if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key,
            //                           out _message))
            //{
            //    WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
            //    gridUser.Visible = false;
            //}
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
        
    }
    protected void UserRoleGrid_DataSelect(object sender, EventArgs e)
    {
        var parameter = UserRoleDatas.Parameters["whereClause"];
        if (parameter == null)
        {

            UserRoleDatas.Parameters.Add("WhereClause", String.Format("IsDisabled = 'false' AND UserId = '{0}'", (sender as ASPxGridView).GetMasterRowKeyValue()));
        }
        else
        {
            parameter.DefaultValue = String.Format("IsDisabled = 'false' AND UserId = '{0}'",
                                  (sender as ASPxGridView).GetMasterRowKeyValue());
        }
    }
    protected void UserGroupGrid_DataSelect(object sender, EventArgs e)
    {
        var parameter = UserRoleDatas.Parameters["whereClause"];
        if (parameter == null)
        {

            UserRoleDatas.Parameters.Add("WhereClause", String.Format("IsDisabled = 'false' AND UserId = '{0}'", (sender as ASPxGridView).GetMasterRowKeyValue()));
        }
        else
        {
            parameter.DefaultValue = String.Format("IsDisabled = 'false' AND UserId = '{0}'",
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
            if (e.NewValues["Username"].ToString().Trim().Length < 1)
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

    }
    protected void gridUser_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string perxe = ServiceFacade.SettingsHelper.UserPrefix;
        int count = 0;
        TList<Users> objuser = DataRepository.UsersProvider.GetPaged("Id like '" + perxe + "' + '%'", "Id desc", 0, 1, out count);
        if (count == 0)
        {
            e.NewValues["Id"] = perxe + "0001";
        }
        else
        {
            e.NewValues["Id"] = perxe + string.Format("{0:0000}", int.Parse(objuser[0].Id.Substring(objuser[0].Id.Length - 3)) + 1);
        }
    }
    protected bool Getcheckbox(string obj)
    {
        string str =obj.ToString();
        if (str == "true") return true;
        return false;
    }
    protected void gridUser_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
      
    }
    protected void gridUser_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        // Checks whether the generated row has the errors.
        bool hasError = e.GetValue("Username").ToString().Length <= 1;
        hasError = hasError || e.GetValue("DisplayName").ToString().Length <= 1;
        hasError = hasError || e.GetValue("UserGroupId") == null;
        // If the row has the error(s), its text color is set to red.
        if (hasError)
            e.Row.ForeColor = System.Drawing.Color.Red;

    }
    protected void gridUser_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage]
                        = String.Format("You do not have permission to delete","Delete User");
                return;
            }
            if (e.ButtonID != "btnDelete") return;
            string id=gridUser.GetRowValues(e.VisibleIndex, "Id").ToString();
            var obj = DataRepository.UsersProvider.GetById(id);
            if (obj == null || obj.IsDisabled) return;
            DataRepository.UsersProvider.DeepLoad(obj);
            if(obj.DoctorRoomCollection.Exists(x=>!x.IsDisabled)||obj.AppointmentCollection.Exists(x=>!x.IsDisabled)||obj.DoctorServiceCollection.Exists(x=>!x.IsDisabled)||obj.RosterCollection.Exists(x=>!x.IsDisabled))
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = String.Format("Dr. {0} is using, you cannot delete it.", obj.DisplayName);
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
            string query=string.Format("UserId='{0}' and RoleId={1} and IsDisabled='false'",UserId,RoleId);
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
}
