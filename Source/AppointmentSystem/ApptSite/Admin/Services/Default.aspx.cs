using System;
using System.Linq;
using AppointmentSystem.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using Log.Controller;
using Common.Util;
using Appt.Common.Constants;
using AppointmentBusiness.Util;

public partial class Admin_Services_Default : System.Web.UI.Page
{
    private const string ScreenCode = "Services";
    static string _message;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Validate user right for reading
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridServices.Visible = false;
                return;
            }

            // Lay cot co chua cac nut thao tac trong grid role
            var gridViewCommandColumn = gridServices.Columns["#"] as GridViewCommandColumn;

            // Neu khong co cot do thi khong can lam tiep
            if (gridViewCommandColumn == null) return;

            // Gan visible cho nut new, edit bang cach kiem tra quyen
            gridViewCommandColumn.EditButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(),
                                                                                  ScreenCode,
                                                                                  OperationConstant.Update.Key,
                                                                                  out _message);
            gridViewCommandColumn.NewButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(),
                                                                                  ScreenCode,
                                                                                  OperationConstant.Create.Key,
                                                                                  out _message);

            // Rieng nut delete thi kiem tra khac boi vi no su dung custom button
            GridViewCommandColumnCustomButton btnDelete =
                gridViewCommandColumn.CustomButtons.Cast<GridViewCommandColumnCustomButton>().FirstOrDefault(
                    customButton => customButton.ID == "btnDelete");
            if (btnDelete != null && !RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(),
                                                                                  ScreenCode,
                                                                                  OperationConstant.Delete.Key,
                                                                                  out _message))
            {
                btnDelete.Visibility = GridViewCustomButtonVisibility.Invisible;
            }

            // Tat ca cot neu 3 thao tac deu khong hien thi
            gridViewCommandColumn.Visible = gridViewCommandColumn.EditButton.Visible
                || gridViewCommandColumn.NewButton.Visible
                || (btnDelete != null && btnDelete.Visibility != GridViewCustomButtonVisibility.Invisible);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.ShowDialog(this, "System is error. Please contact Administrator.");
        }
    }

    protected void gridServices_RowInserting(object sender, ASPxDataInsertingEventArgs e)
    {
        try
        {
            // Kiem tra xem user co quyen them hay khong
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key,
                                       out _message))
            {
                // Cau lenh nay se quang ra exception va DevExpress se nhan duoc exception de thong bao loi cho nguoi dung
                WebCommon.AlertGridView(sender, _message);

                // Cancel operation
                e.Cancel = true;

                // Stop
                return;
            }

            // Gan cac gia tri cho dong duoc them
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            e.Cancel = true;
            WebCommon.AlertGridView(sender, "Cannot insert new service. Please contact Administrator");
        }
    }
    protected void gridServices_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
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
            if (Int32.TryParse(gridServices.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
            {
                // Neu id la so thi tien hanh kiem tra xem service co duoc su dung khong

                // Lay thong tin cua service
                var obj = DataRepository.ServicesProvider.GetById(id);

                // Kiem tra xem service co ton tai hay khong
                // neu khong ton tai thi bao loi
                if (obj == null || obj.IsDisabled)
                {
                    // Set returned message, then DevExpress will get it and show for end-user
                    WebCommon.AlertGridView(sender, "Service is not existed. You cannot delete it.");
                    return;
                }

                // O day dung deepload de load toan bo du lieu lien quan
                DataRepository.ServicesProvider.DeepLoad(obj);

                // Tien hanh kiem tra xem no co dang duoc su dung hay khong
                // luu y la cac thong tin lien ket voi no phai co gia tri IsDisabled la false thi moi tinh la ton tai
                // neu IsDisabled co gia tri True thi nghia la no khong con ton tai
                if (obj.RoomCollection.Exists(x => !x.IsDisabled) // Kiem tra xem co phong nao dang co dich vu nay hay khong
                    || obj.DoctorServiceCollection.Exists(x => !x.IsDisabled) // Kiem tra xem co bac si nao duoc gan vao dich vu nay hay khong
                    || obj.AppointmentCollection.Exists(x => !x.IsDisabled) // Kiem tra xem co appointment nao da co dich vu nay hay khong
                    )
                {
                    // Set returned message, then DevExpress will get it and show for end-user
                    WebCommon.AlertGridView(sender, String.Format("Service {0} is using, you cannot delete it.", obj.Title));
                    return;
                }
                // Neu service dang trong tinh trang hoan toan khong duoc su dung thi tien hanh xoa
                // viec xoa chi o day chi la doi gia tri cua IsDisabled thanh True
                obj.IsDisabled = true;
                obj.UpdateUser = WebCommon.GetAuthUsername();
                obj.UpdateDate = DateTime.Now;
                DataRepository.ServicesProvider.Update(obj);
            }
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete service. Please contact Administrator");
        }
    }
    protected void gridServices_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Update.Key, out _message))//Kiem tra xem user co quyen cap nhat hay khong
            {
                // Set returned message, then DevExpress will get it and show for end-user
                WebCommon.AlertGridView(sender, _message);
                
                // Cancel operation
                e.Cancel = true;

                // Stop
                return;
            }
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            e.Cancel = true;
            WebCommon.AlertGridView(sender, "Cannot update service. Please contact Administrator");
        }
    }
    protected void gridServices_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        // Check field Title if empty, then stop
        if (e.NewValues["Title"] == null || string.IsNullOrEmpty(e.NewValues["Title"].ToString().Trim()))
        {
            ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] =
              "Title cannot be empty";
        }
        
    }
}
