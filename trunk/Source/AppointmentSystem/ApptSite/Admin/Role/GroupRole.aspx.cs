using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApptSite;
using DevExpress.Web.ASPxGridView;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentBusiness.Util;
using Appt.Common.Constants;
using Common.Util;
using Logger.Controller;

public partial class Admin_Role_GroupRole : System.Web.UI.Page
{
    string _message;
    string ScreenCode = "GroupRole";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            bool chas = RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Read.Key, out _message);
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridGroupRole.Visible = false;
                return;
            }
            var gridcolums = gridGroupRole.Columns["btnCommand"] as GridViewCommandColumn;
            if (gridcolums == null) return;
            gridcolums.EditButton.Visible = RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Update.Key, out _message);
            gridcolums.NewButton.Visible = RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Create.Key, out _message);
            //var btnDelete = gridcolums.CustomButtons.Find(x => x.ID == "btnDelete");
            GridViewCommandColumnCustomButton btnDelete = null;
            foreach (GridViewCommandColumnCustomButton customButton in gridcolums.CustomButtons)
            {
                if (customButton.ID == "btnDelete")
                {
                    btnDelete = customButton;
                    break;
                }
            }
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode,OperationConstant.Delete.Key,out _message))
            {
                btnDelete.Visibility = GridViewCustomButtonVisibility.Invisible;
            }

            gridcolums.Visible = gridcolums.EditButton.Visible|| gridcolums.NewButton.Visible|| (btnDelete.Visibility != GridViewCustomButtonVisibility.Invisible);
        }
        catch (Exception ex)
        {
            LoggerController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
  
    protected void Gridrole_OnRowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = AccountSession.Session;
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
    }
    protected void Gridrole_OnRowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["GroupId"] =(sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = AccountSession.Session;
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
        int Id = int.Parse(e.NewValues["RoleId"].ToString());
        string sql=string.Format("RoleId={0} and GroupId='{1}' and IsDisabled='false'",Id,e.NewValues["GroupId"]);
        int count;
        var lisid = DataRepository.GroupRoleProvider.GetPaged(sql,"",0,10,out count);
        if (lisid.Count > 0) { 
             lblmessage.Text = "role is exist";
             lblmessage.Visible = true;
            e.Cancel = true;
           
        }
        
    }
    protected void Gridrole_OnCustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        try
        {
            if (e.ButtonID != "btnDeleteRoleGroup") return;
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                return;
            }
            ASPxGridView girdrolegroup = (ASPxGridView)(sender as ASPxGridView);//lay gia tri bang cua gridview
            int id;
            if (Int32.TryParse(girdrolegroup.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
            {
                var obj = DataRepository.GroupRoleProvider.GetById(id);
                if (obj == null || obj.IsDisabled)
                {
                    ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "Role group is not existed. You cannot delete it.";
                    return;
                }
                obj.IsDisabled = true;
                obj.UpdateUser = AccountSession.Session;
                obj.UpdateDate = DateTime.Now;
                DataRepository.GroupRoleProvider.Update(obj);
            }
        }
        catch (Exception ex)
        {
            LoggerController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "There is system error. Please contact Administrator.";
        }
    }
    protected void Gridrolegroup_OnBeforePerformDataSelect(object sender, EventArgs e)
    {

        ASPxGridView girdroledetail = (ASPxGridView)(sender as ASPxGridView);
        var gridColunms = girdroledetail.Columns["buttonCommand"] as GridViewCommandColumn;
        if (gridColunms == null) return;

        // Gan visible cho nut new, edit bang cach kiem tra quyen
        gridColunms.EditButton.Visible = RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Update.Key, out _message);
        gridColunms.NewButton.Visible = RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Create.Key, out _message);
        // Rieng nut delete thi kiem tra khac boi vi no su dung custom button
       //var btnDelete = gridColunms.CustomButtons.Find(x => x.ID == "btnDeleteRolegroup");
        GridViewCommandColumnCustomButton btnDelete = null;
        foreach (GridViewCommandColumnCustomButton customButton in gridColunms.CustomButtons)
        {
            if (customButton.ID == "btnDelete")
            {
                btnDelete = customButton;
                break;
            }
        }
        if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Delete.Key, out _message))
        {
            btnDelete.Visibility = GridViewCustomButtonVisibility.Invisible;
        }

        // Tat ca cot neu 3 thao tac deu khong hien thi
        gridColunms.Visible = gridColunms.EditButton.Visible
            || gridColunms.NewButton.Visible
            || (btnDelete.Visibility != GridViewCustomButtonVisibility.Invisible);
        var param = GroupRoleDatas.Parameters["WhereClause"];
        if (param == null)
        {
            GroupRoleDatas.Parameters.Add("WhereClause"
                , String.Format("IsDisabled = 'false' AND GroupId = '{0}'", (sender as ASPxGridView).GetMasterRowKeyValue()));
        }
        else
        {
            param.DefaultValue = String.Format("IsDisabled = 'false' AND GroupId = '{0}'",
                                  (sender as ASPxGridView).GetMasterRowKeyValue());
        }

    }
    protected void gridGroupRole_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = AccountSession.Session;
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
    }
    protected void gridGroupRole_OnRowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = AccountSession.Session;
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
    }
    protected void gridGroupRole_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        try
        {
            if (e.ButtonID != "btnDelete") return;
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                return;
            }                    
            string id = gridGroupRole.GetRowValues(e.VisibleIndex, "Id").ToString();              
            var obj = DataRepository.UserGroupProvider.GetById(id);
            if (obj == null || obj.IsDisabled)
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "User Group is not existed. You cannot delete it.";
                return;
            }

            DataRepository.UserGroupProvider.DeepLoad(obj);
            if (obj.UsersCollection.Exists(x => !x.IsDisabled) )
            {
                ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = String.Format("Group user {0} is using, you cannot delete it.", obj.Title);
                return;
            }
            obj.IsDisabled = true;
            obj.UpdateUser = AccountSession.Session;
            obj.UpdateDate = DateTime.Now;
            DataRepository.UserGroupProvider.Update(obj);
            
        }
        catch (Exception ex)
        {
            LoggerController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "There is system error. Please contact Administrator.";
        }
    }
    protected void gridGroupRole_OnRowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (e.NewValues["Id"]==null ||e.NewValues["Id"].ToString()=="")
        {
            e.RowError = "Id can not be empty";
        }
    }
    
}
