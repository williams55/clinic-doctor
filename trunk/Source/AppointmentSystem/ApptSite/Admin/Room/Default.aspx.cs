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
using AppointmentBusiness.Util;
using Appt.Common.Constants;
using Log.Controller;
using Common.Util;

public partial class Admin_Room_edit : System.Web.UI.Page
{
    private const string ScreenCode = "Room";
    static string _message;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridRoom.Visible = false;
                return;
            }

            // Set page size
            gridRoom.SettingsPager.PageSize = ServiceFacade.SettingsHelper.PageSize;
            
            var gridcolums = gridRoom.Columns["btnCommand"] as GridViewCommandColumn;
            if (gridcolums == null) return;
            gridcolums.EditButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Update.Key, out _message);
            gridcolums.NewButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key, out _message);

            // Get delete button
            GridViewCommandColumnCustomButton btnDelete =
                gridcolums.CustomButtons.Cast<GridViewCommandColumnCustomButton>().FirstOrDefault(
                    customButton => customButton.ID == "btnDelete");

            // Check delete right, if user has no right to delete => invisible delete button
            if (btnDelete != null && !RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                btnDelete.Visibility = GridViewCustomButtonVisibility.Invisible;
            }

            // Check visibility of button in button column
            // If there is no button is displayed => invisible column
            gridcolums.Visible = gridcolums.EditButton.Visible || gridcolums.NewButton.Visible ||
                                 (btnDelete != null && btnDelete.Visibility != GridViewCustomButtonVisibility.Invisible);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    protected void gridRoom_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                return;  
            }
            if (e.ButtonID != "btnDelete") return;
            Int64 id;
            if (Int64.TryParse(gridRoom.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
            {
                var obj = DataRepository.RoomProvider.GetById(int.Parse(id.ToString()));
                if (obj == null||obj.IsDisabled)
                {
                    ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = "Room is Using. You cannot delete it.";
                    return;
                }
                DataRepository.RoomProvider.DeepLoad(obj);
                if (obj.RosterCollection.Exists(x => !x.IsDisabled) || obj.DoctorRoomCollection.Exists(x => !x.IsDisabled) )
                {
                    ((ASPxGridView)sender).JSProperties[GeneralConstants.ApptMessage] = String.Format("Room {0} is using, you cannot delete it.", obj.Title);
                    return;
                }
                obj.IsDisabled = true;
                obj.UpdateUser = WebCommon.GetAuthUsername();
                obj.UpdateDate = DateTime.Now;
                DataRepository.RoomProvider.Update(obj);
            }
        }
        catch( Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    protected void gridRoom_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        try {
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
        }
        catch( Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    protected void gridRoom_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Update.Key, out _message))//Kiem tra xem user co quyen cap nhat hay khong
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
}
