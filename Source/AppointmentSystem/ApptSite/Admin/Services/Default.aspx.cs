using System;
using System.Collections.Generic;
using System.Linq;
using AppointmentSystem.Data;
using AppointmentSystem.Settings.BusinessLayer;
using AppointmentSystem.Web.Data;
using DevExpress.Web.ASPxEditors;
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
            
            // Set page size
            gridServices.SettingsPager.PageSize = ServiceFacade.SettingsHelper.PageSize;

            // Lay cot co chua cac nut thao tac trong grid role
            var gridViewCommandColumn = gridServices.Columns["Operation"] as GridViewCommandColumn;

            // Neu khong co cot do thi khong can lam tiep
            if (gridViewCommandColumn == null) return;

            // Gan visible cho nut new, edit bang cach kiem tra quyen
            gridViewCommandColumn.EditButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(),
                                                                                  ScreenCode,
                                                                                  OperationConstant.Update.Key,
                                                                                  out _message);

            // Set hien thi/an cho nut new
            btnAdd.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode,
                                                        OperationConstant.Create.Key, out _message);

            // Set hien thi/an cho nut delete
            btnGeneralDelete.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(),
                                                                                  ScreenCode,
                                                                                  OperationConstant.Delete.Key,
                                                                                  out _message);

            // Set hien thi/an cho nut delete cua tung row
            GridViewCommandColumnCustomButton btnDelete =
                gridViewCommandColumn.CustomButtons.Cast<GridViewCommandColumnCustomButton>().FirstOrDefault(
                    customButton => customButton.ID == "btnDelete");
            if (btnDelete != null && !btnGeneralDelete.Visible)
            {
                btnDelete.Visibility = GridViewCustomButtonVisibility.Invisible;
            }

            // Set hien thi/an cho cot button
            gridViewCommandColumn.Visible = gridViewCommandColumn.EditButton.Visible
                || gridViewCommandColumn.NewButton.Visible
                || btnGeneralDelete.Visible;
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
            #region Validating
            // Validate user right for creating
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

            // Get PriorityIndex object
            var index = (ASPxSpinEdit)((ASPxGridView)sender).FindEditFormTemplateControl("index");
            
            // Validate empty field
            if (!WebCommon.ValidateEmpty("Title", e.NewValues["Title"], out _message)
                || !WebCommon.ValidateEmpty("Short Title", e.NewValues["ShortTitle"], out _message)
                || !WebCommon.ValidateEmpty("Index", index.Value, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Validate integer value for priority index
            int iIndex;
            if (!Int32.TryParse(index.Value.ToString().Trim(), out iIndex))
            {
                WebCommon.AlertGridView(sender, "Index value is invalid.");
                e.Cancel = true;
                return;
            }
            #endregion

            // Set value
            e.NewValues["Title"] = e.NewValues["Title"].ToString().Trim();
            e.NewValues["ShortTitle"] = e.NewValues["Title"].ToString().Trim();
            e.NewValues["PriorityIndex"] = index.Value;
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;

            // Show message alert delete successfully
            WebCommon.AlertGridView(sender, "Service is created successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            e.Cancel = true;
            WebCommon.AlertGridView(sender, "Cannot create new service. Please contact Administrator");
        }
    }
 
    protected void gridServices_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        try
        {
            // Neu truong hop khong phai la xoa mot item thi dung ket thuc
            // Phai kiem tra cai nay vi o day minh chi dung cai custom button de xoa, khong lam gi khac
            if (e.ButtonID != "btnDelete") return;

            // Validate user right for deleting
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                // Cau lenh nay se quang ra exception va DevExpress se nhan duoc exception de thong bao loi cho nguoi dung
                WebCommon.AlertGridView(sender, _message);
                return;
            }

            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                WebCommon.AlertGridView(sender, "Cannot find service.");
                return;
            }

            // Truong hop ma user co quyen thi tien hanh kiem tra xem service nay co thang con nao khong
            // neu co thi thong bao loi la service dang duoc su dung, khong the xoa
            // neu service hoan toan ko duoc su dung thi xoa no
            int id; // Khai bao bien id de parse gia tri id cua service tu chuoi sang so

            // Kiem tra xem id cua service co phai la so khong
            if (!Int32.TryParse(grid.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
            {
                WebCommon.AlertGridView(sender, "Cannot delete service. Service is invalid!");
            }

            // Lay thong tin cua service
            var services = DataRepository.ServicesProvider.GetById(id);

            // Kiem tra xem service co ton tai hay khong
            // neu khong ton tai thi bao loi
            if (services == null || services.IsDisabled)
            {
                // Set returned message, then DevExpress will get it and show for end-user
                WebCommon.AlertGridView(sender, "Service is not existed. You cannot delete it.");
                return;
            }

            // O day dung deepload de load toan bo du lieu lien quan
            DataRepository.ServicesProvider.DeepLoad(services);

            // Tien hanh kiem tra xem no co dang duoc su dung hay khong
            // luu y la cac thong tin lien ket voi no phai co gia tri IsDisabled la false thi moi tinh la ton tai
            // neu IsDisabled co gia tri True thi nghia la no khong con ton tai
            if (services.RoomCollection.Exists(x => !x.IsDisabled) // Kiem tra xem co phong nao dang co dich vu nay hay khong
                || services.UsersCollection.Exists(x => !x.IsDisabled) // Kiem tra xem co bac si nao duoc gan vao dich vu nay hay khong
                || services.AppointmentCollection.Exists(x => !x.IsDisabled) // Kiem tra xem co appointment nao da co dich vu nay hay khong
                )
            {
                // Set returned message, then DevExpress will get it and show for end-user
                WebCommon.AlertGridView(sender, String.Format("Service {0} is using, you cannot delete it.", services.Title));
                return;
            }
            // Neu service dang trong tinh trang hoan toan khong duoc su dung thi tien hanh xoa
            // viec xoa chi o day chi la doi gia tri cua IsDisabled thanh True
            services.IsDisabled = true;
            services.UpdateUser = WebCommon.GetAuthUsername();
            services.UpdateDate = DateTime.Now;
            DataRepository.ServicesProvider.Update(services);

            // Show message alert delete successfully
            WebCommon.AlertGridView(sender, "Service is deleted successfully.");
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
            #region Validating
            // Validate user right for updating
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Update.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Get PriorityIndex object
            var index = (ASPxSpinEdit)((ASPxGridView)sender).FindEditFormTemplateControl("index");

            // Validate empty field
            if (!WebCommon.ValidateEmpty("Title", e.NewValues["Title"], out _message)
                || !WebCommon.ValidateEmpty("Short Title", e.NewValues["ShortTitle"], out _message)
                || !WebCommon.ValidateEmpty("Index", index.Value, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Validate integer value for priority index
            int iIndex;
            if (!Int32.TryParse(index.Value.ToString().Trim(), out iIndex))
            {
                WebCommon.AlertGridView(sender, "Index value is invalid.");
                e.Cancel = true;
                return;
            }
            #endregion

            // Set value
            e.NewValues["Title"] = e.NewValues["Title"].ToString().Trim();
            e.NewValues["ShortTitle"] = e.NewValues["ShortTitle"].ToString().Trim();
            e.NewValues["PriorityIndex"] = index.Value;
            e.NewValues["UpdateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["UpdateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;

            // Show message alert update successfully
            WebCommon.AlertGridView(sender, "Service is updated successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            e.Cancel = true;
            WebCommon.AlertGridView(sender, "Cannot update service. Please contact Administrator");
        }
    }

    /// <summary>
    /// Xoa nhieu service
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridServices_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
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
                    WebCommon.AlertGridView(sender, _message);
                    tm.Rollback();
                    return;
                }

                var grid = sender as ASPxGridView;
                if (grid == null)
                {
                    WebCommon.AlertGridView(sender, "Cannot find service.");
                    tm.Rollback();
                    return;
                }

                // Lay danh sach Id cua cac row duoc select
                var fieldNames = new[] { "Id" };
                List<object> columnValues = grid.GetSelectedFieldValues(fieldNames);

                // Doi trang thai cua cac roster
                foreach (object serviceId in columnValues)
                {
                    int id;
                    if (!Int32.TryParse(serviceId.ToString(), out id))
                    {
                        WebCommon.AlertGridView(sender, "Service is invalid. You cannot delete it.");
                        tm.Rollback();
                        return;
                    }

                    var services = DataRepository.ServicesProvider.GetById(id);
                    if (services == null || services.IsDisabled)
                    {
                        WebCommon.AlertGridView(sender, "Service is not existed. You cannot delete it.");
                        tm.Rollback();
                        return;
                    }
                    DataRepository.ServicesProvider.DeepLoad(services);

                    // Kiem tra xem co cho nao dang su dung room hay khong
                    if (services.RoomCollection.Exists(x => !x.IsDisabled) // Kiem tra xem co phong nao dang co dich vu nay hay khong
                        || services.UsersCollection.Exists(x => !x.IsDisabled) // Kiem tra xem co bac si nao duoc gan vao dich vu nay hay khong
                        || services.AppointmentCollection.Exists(x => !x.IsDisabled) // Kiem tra xem co appointment nao da co dich vu nay hay khong
                        )
                    {
                        WebCommon.AlertGridView(sender, String.Format("Service {0} is using, you cannot delete it.", services.Title));
                        tm.Rollback();
                        return;
                    }
                    services.IsDisabled = true;
                    services.UpdateUser = WebCommon.GetAuthUsername();
                    services.UpdateDate = DateTime.Now;
                    DataRepository.ServicesProvider.Update(tm, services);
                }
                tm.Commit();
                WebCommon.AlertGridView(sender, String.Format("{0} deleted successfully.",
                                                      grid.Selection.Count > 1 ? "Services are" : "Service is"));

                // Set tam pageIndex de lay duoc danh sach moi
                grid.Selection.UnselectAll();
            }
        }
        catch (Exception ex)
        {
            if (tm.IsOpen)
            {
                tm.Rollback();
            }
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete service. Please contact Administrator.");
        }
    }
}
