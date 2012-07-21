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
public partial class Admin_Role_Default : System.Web.UI.Page
{
    private const string ScreenCode = "Role";
    static string _message;
    #region //loadpage
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Validate user right for reading
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridRole.Visible = false;
                return;
            }

            // Lay cot co chua cac nut thao tac
            var gridViewCommandColumn = gridRole.Columns["#"] as GridViewCommandColumn;

            // Neu khong co cot do thi khong can lam tiep
            if (gridViewCommandColumn == null) return;

            // Gan visible cho nut new, edit bang cach kiem tra quyen
            gridViewCommandColumn.EditButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(),                                                                                ScreenCode,
                                                                                  OperationConstant.Update.Key,
                                                                                  out _message);
            gridViewCommandColumn.NewButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(),
                                                                                  ScreenCode,
                                                                                  OperationConstant.Create.Key,
                                                                                  out _message);
            // Rieng nut delete thi kiem tra khac boi vi no su dung custom button
            var btnDelete = gridViewCommandColumn.CustomButtons.Find(x => x.ID == "btnDelete");
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(),
                                                                                  ScreenCode,
                                                                                  OperationConstant.Delete.Key,
                                                                                  out _message))
            {
                btnDelete.Visibility = GridViewCustomButtonVisibility.Invisible;
            }

            // Tat ca cot neu 3 thao tac deu khong hien thi
            gridViewCommandColumn.Visible = gridViewCommandColumn.EditButton.Visible
                || gridViewCommandColumn.NewButton.Visible
                || (btnDelete.Visibility != GridViewCustomButtonVisibility.Invisible);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    protected void Gridroledetail_OnBeforePerformDataSelect(object sender, EventArgs e)
    {
        var param = RoledetailDataS.Parameters["WhereClause"];
        string ss = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        ASPxGridView girdroledetail = (ASPxGridView)(sender as ASPxGridView);
        var gridViewCommandColumn = girdroledetail.Columns["Controlcommand"] as GridViewCommandColumn;
        if (gridViewCommandColumn == null) return;

        // Gan visible cho nut new, edit bang cach kiem tra quyen
        gridViewCommandColumn.EditButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode,OperationConstant.Update.Key,out _message);
        gridViewCommandColumn.NewButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(),ScreenCode,OperationConstant.Create.Key,out _message);
        // Rieng nut delete thi kiem tra khac boi vi no su dung custom button
        var btnDelete = gridViewCommandColumn.CustomButtons.Find(x => x.ID == "btnDelete");
        if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(),ScreenCode,OperationConstant.Delete.Key,out _message))
        {
            btnDelete.Visibility = GridViewCustomButtonVisibility.Invisible;
        }

        // Tat ca cot neu 3 thao tac deu khong hien thi
        gridViewCommandColumn.Visible = gridViewCommandColumn.EditButton.Visible
            || gridViewCommandColumn.NewButton.Visible
            || (btnDelete.Visibility != GridViewCustomButtonVisibility.Invisible);
        if (param == null)
        {
            RoledetailDataS.Parameters.Add("WhereClause"
                , String.Format("IsDisabled = 'false' AND RoleId = {0}", (sender as ASPxGridView).GetMasterRowKeyValue()));
        }
        else
        {
            param.DefaultValue = String.Format("IsDisabled = 'false' AND RoleId = {0}",
                                  (sender as ASPxGridView).GetMasterRowKeyValue());
        }

    }
    #endregion
    #region // edit role
    protected void gridRole_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
    {

        try
        {
            // Neu truong hop khong phai la xoa mot item thi dung ket thuc
            // Phai kiem tra cai nay vi o day minh chi dung cai custom button de xoa, khong lam gi khac
            if (e.ButtonID != "btnDelete") return;

            // Kiem tra xem user co quyen xoa mot service hay khong
            // Neu khong co quyen thi quang ra loi va cancel cai thao tac dang thuc thi
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                // Cau lenh nay se quang ra exception va DevExpress se nhan duoc exception de thong bao loi cho nguoi dung
                WebCommon.AlertGridView(sender, _message);
                return;
            }

            // Truong hop ma user co quyen thi tien hanh kiem tra xem service nay co thang con nao khong
            // neu co thi thong bao loi la service dang duoc su dung, khong the xoa
            // neu service hoan toan ko duoc su dung thi xoa no
            int id; // Khai bao bien id de parse gia tri id cua service tu chuoi sang so

            // Kiem tra xem id cua service co phai la so khong
            if (Int32.TryParse(gridRole.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
            {
                // Neu id la so thi tien hanh kiem tra xem service co duoc su dung khong

                // Lay thong tin cua service
               // var obj = DataRepository.ServicesProvider.GetById(id);
                var obj = DataRepository.RoleProvider.GetById(id);

                // Kiem tra xem service co ton tai hay khong
                // neu khong ton tai thi bao loi
                if (obj == null || obj.IsDisabled)
                {
                    ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "Service is not existed. You cannot delete it.";
                    return;
                }

                // O day dung deepload de load toan bo du lieu lien quan
                //DataRepository.ServicesProvider.DeepLoad(obj);
                DataRepository.RoleProvider.DeepLoad(obj);

                // Tien hanh kiem tra xem no co dang duoc su dung hay khong
                // luu y la cac thong tin lien ket voi no phai co gia tri IsDisabled la false thi moi tinh la ton tai
                // neu IsDisabled co gia tri True thi nghia la no khong con ton tai
                if(obj.GroupRoleCollection.Exists(x=>!x.IsDisabled)&& obj.UserRoleCollection.Exists(x=>!x.IsDisabled))
                {
                    ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = String.Format("Role {0} is using, you cannot delete it.", obj.Title);
                    
                   return;
                }
                // Neu service dang trong tinh trang hoan toan khong duoc su dung thi tien hanh xoa
                // viec xoa chi o day chi la doi gia tri cua IsDisabled thanh True
                obj.IsDisabled = true;
                obj.UpdateUser = WebCommon.GetAuthUsername();
                obj.UpdateDate = DateTime.Now;
                DataRepository.RoleProvider.Update(obj);
                //khi xoa dc role trong ta se xoa tung roledetail
                var listroledetail = DataRepository.RoleDetailProvider.GetByRoleId(id);
                for (int i = 0; i < listroledetail.Count; i++)
                {
                    listroledetail[i].IsDisabled = true;
                    listroledetail[i].UpdateUser = WebCommon.GetAuthUsername();
                    listroledetail[i].UpdateDate = DateTime.Now;
                    DataRepository.RoleDetailProvider.Update(listroledetail[i]);
                }
            }
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "There is system error. Please contact Administrator.";
        }
   
    }
    protected void Gridroledetail_OnCustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
    {

        try
        {
            // Neu truong hop khong phai la xoa mot item thi dung ket thuc
            // Phai kiem tra cai nay vi o day minh chi dung cai custom button de xoa, khong lam gi khac
            if (e.ButtonID != "btnDelete") return;
           
            // Kiem tra xem user co quyen xoa mot role  hay khong
            // Neu khong co quyen thi quang ra loi va cancel cai thao tac dang thuc thi
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                // Cau lenh nay se quang ra exception va DevExpress se nhan duoc exception de thong bao loi cho nguoi dung
                WebCommon.AlertGridView(sender, _message);
                return;
            }

            // Truong hop ma user co quyen thi tien hanh kiem tra xem service nay co thang con nao khong
            // neu co thi thong bao loi la service dang duoc su dung, khong the xoa
            // neu service hoan toan ko duoc su dung thi xoa no
            ASPxGridView girdroledetail = (ASPxGridView)(sender as ASPxGridView);//lay gia tri bang cua gridview
            int id; // Khai bao bien id de parse gia tri id cua service tu chuoi sang so

            // Kiem tra xem id cua roledetail co phai la so khong
            if (Int32.TryParse(girdroledetail.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
            {
                // Neu id la so thi tien hanh kiem tra xem service co duoc su dung khong

                // Lay thong tin cua service             
                var obj = DataRepository.RoleDetailProvider.GetById(id);

                // Kiem tra xem roledetail co ton tai hay khong
                // neu khong ton tai thi bao loi
                if (obj == null || obj.IsDisabled)
                {
                    ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "Service is not existed. You cannot delete it.";
                    return;
                }

                
                // Vi roledetail  la con cua roled khong co bang nao tham chieu toi no len ta co the xoa ma khong can kiem tra cac bang khac
                
                obj.IsDisabled = true;
                obj.UpdateUser = WebCommon.GetAuthUsername();
                obj.UpdateDate = DateTime.Now;
                DataRepository.RoleDetailProvider.Update(obj);
            }
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "There is system error. Please contact Administrator.";
        }

    }
    protected void gridRole_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;

    }
    protected void Gridroledetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        int id = int.Parse((sender as ASPxGridView).GetMasterRowKeyValue().ToString());

        string crud = Getstringcrud(sender, e);
        e.NewValues["RoleId"] = id;
        e.NewValues["Crud"] = crud;
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
    }
    protected void Gridroledetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Crud"] = GetStringCrudUpdate(sender, e);
        e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
        e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
    }
    protected void Gridroledetail_OnRowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        if (e.NewValues["ScreenCode"]==null)//check field Title if empty, then stop
        {
            e.RowError = "Field ScreenCode not be empty";
        }

    }
    #endregion
    #region //get data to checkbox
    protected string Getstringcrud(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)//lay du lieu trong check box khi them moi
    {
        string crud = string.Empty;
        ASPxCheckBox chkr = (ASPxCheckBox)(sender as ASPxGridView).FindEditFormTemplateControl("ckcR");
        ASPxCheckBox chku = (ASPxCheckBox)(sender as ASPxGridView).FindEditFormTemplateControl("ckcU");
        ASPxCheckBox chkd = (ASPxCheckBox)(sender as ASPxGridView).FindEditFormTemplateControl("ckcD");
        ASPxCheckBox chkc = (ASPxCheckBox)(sender as ASPxGridView).FindEditFormTemplateControl("ckcC");
        if (chkc.Checked == true) crud += "C";
        if (chkr.Checked == true) crud += "R";
        if (chku.Checked == true) crud += "U";
        if (chkd.Checked == true) crud += "D";
        return crud;
    }
    protected string GetStringCrudUpdate(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string crud = string.Empty;
        ASPxCheckBox chkr = (ASPxCheckBox)(sender as ASPxGridView).FindEditFormTemplateControl("ckcR");
        ASPxCheckBox chku = (ASPxCheckBox)(sender as ASPxGridView).FindEditFormTemplateControl("ckcU");
        ASPxCheckBox chkd = (ASPxCheckBox)(sender as ASPxGridView).FindEditFormTemplateControl("ckcD");
        ASPxCheckBox chkc = (ASPxCheckBox)(sender as ASPxGridView).FindEditFormTemplateControl("ckcC");
        if (chkc.Checked == true) crud += "C";
        if (chkr.Checked == true) crud += "R";
        if (chku.Checked == true) crud += "U";
        if (chkd.Checked == true) crud += "D";
        return crud;
    }//lay du lieu trong checkbox khi cap nhat
    public bool Getcheckbox(string crud,string chars)//danh dau check box xem co quyen khong dua vao ky tu truyen vao 
    {
        if (crud != null)
        {
            int size = crud.Length;
            for (int i = 0; i < size; i++)
            {
                if (crud[i] == chars[0]) return true;
            }

        }
        return false;
    }
 #endregion
}