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
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key,
                                       out _message))
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
    protected void UserRoleGrid_DataSelect(object sender, EventArgs e)
    {
        var parameter = UserRoleDatas.Parameters["whereClause"];
        if (parameter == null)
        {

            UserRoleDatas.Parameters.Add("WhereClause", String.Format("IsDisabled = 'false' AND UserId = '{0}'", (sender as ASPxGridView).GetMasterRowKeyValue()));
        }
        else
        {
            parameter.DefaultValue = String.Format("IsDisabled = 'false' AND UserId = {0}",
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
            parameter.DefaultValue = String.Format("IsDisabled = 'false' AND UserId = {0}",
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
        string perxe = "USR";
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
        
        if (e.NewValues["Username"].ToString().Trim().Length < 1)
        {
            e.RowError = "Username can not be empty";
        }
        if (e.NewValues["DisplayName"].ToString().Trim().Length < 1)
        {
            e.RowError = "DisplayName can not be empty";
        }
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
    protected void gridUser_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        string id = e.NewValues["Id"].ToString();
        string userGroupId = e.NewValues["UserGroupId"].ToString();

        int count;
        TList<GroupRole> grouprole =
            DataRepository.GroupRoleProvider.GetPaged("GroupId='" + userGroupId + " ' and IsDisabled='false'",
                                                      "RoleId desc", 0, ServiceFacade.SettingsHelper.GetPagedLength,
                                                      out count);
        var luserrole = new TList<UserRole>();

        foreach (var groupRole in grouprole)
        {
            DataRepository.UserRoleProvider.Insert(new UserRole
            {
                UserId = id,
                RoleId = groupRole.RoleId,
                UpdateDate = DateTime.Now,
                UpdateUser = EntitiesUtilities.GetAuthName()
            });
        }
    }
    protected void gridUser_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        if (e.CallbackName == "ABC")
        {
            int index = ((ASPxGridView)sender).EditingRowVisibleIndex;
            string val = ((ASPxGridView)sender).GetRowValues(index, "Description").ToString();
        }
    }
}
