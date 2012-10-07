﻿using System;
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
            // Check reading right
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridRoom.Visible = false;
                return;
            }

            // Set page size
            gridRoom.SettingsPager.PageSize = ServiceFacade.SettingsHelper.PageSize;

            // Lay cot co chua cac nut thao tac
            var gridcolums = gridRoom.Columns["Operation"] as GridViewCommandColumn;

            // Neu khong co cot do thi khong can lam tiep
            if (gridcolums == null) return;

            // Gan visible cho nut new, edit bang cach kiem tra quyen
            gridcolums.EditButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode,
                                                                       OperationConstant.Update.Key, out _message);

            // Set hien thi/an cho nut new
            btnAdd.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode,
                                                        OperationConstant.Create.Key, out _message);

            // Set hien thi/an cho nut delete
            btnGeneralDelete.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(),
                                                                                  ScreenCode,
                                                                                  OperationConstant.Delete.Key,
                                                                                  out _message);

            // Get delete button
            GridViewCommandColumnCustomButton btnDelete =
                gridcolums.CustomButtons.Cast<GridViewCommandColumnCustomButton>().FirstOrDefault(
                    customButton => customButton.ID == "btnDelete");

            // Check delete right, if user has no right to delete => invisible delete button
            if (btnDelete != null && !btnGeneralDelete.Visible)
            {
                btnDelete.Visibility = GridViewCustomButtonVisibility.Invisible;
            }

            // Check visibility of button in button column
            // If there is no button is displayed => invisible column
            gridcolums.Visible = gridcolums.EditButton.Visible || gridcolums.NewButton.Visible || btnGeneralDelete.Visible;
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.ShowDialog(this, "System is error. Please contact Administrator.");
        }
    }
  
    protected void gridRoom_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        try
        {
            if (e.ButtonID != "btnDelete") return;
            #region Delete row
            // Check deleting right
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                return;
            }

            // Validate id
            Int32 id;
            if (Int32.TryParse(gridRoom.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
            {
                var room = DataRepository.RoomProvider.GetById(id);
                if (room == null || room.IsDisabled)
                {
                    WebCommon.AlertGridView(sender, "Cannot find room.");
                    return;
                }
                DataRepository.RoomProvider.DeepLoad(room);
                // Kiem tra xem co cho nao dang su dung room hay khong
                if (room.RosterCollection.Exists(x => !x.IsDisabled) || room.DoctorRoomCollection.Exists(x => !x.IsDisabled)
                    || room.AppointmentCollection.Exists(x => !x.IsDisabled))
                {
                    WebCommon.AlertGridView(sender, String.Format("Room {0} is using, you cannot delete it.", room.Title));
                    return;
                }
                room.IsDisabled = true;
                room.UpdateUser = WebCommon.GetAuthUsername();
                room.UpdateDate = DateTime.Now;
                DataRepository.RoomProvider.Update(room);
            }
            #endregion
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete room. Please contact Administrator");
        }
    }
   
    protected void gridRoom_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        try
        {
            // Validate user right for creating
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Validate empty field
            if (!WebCommon.ValidateEmpty("Service", e.NewValues["ServicesId"], out _message)
                || !WebCommon.ValidateEmpty("Title", e.NewValues["Title"], out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Validate integer value for service id
            int iIndex;
            if (!Int32.TryParse(e.NewValues["ServicesId"].ToString().Trim(), out iIndex))
            {
                WebCommon.AlertGridView(sender, "Service is invalid.");
                e.Cancel = true;
                return;
            }

            // Set pure value
            e.NewValues["Title"] = e.NewValues["Title"].ToString().Trim();
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;

            // Show message alert delete successfully
            WebCommon.AlertGridView(sender, "Room is created successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            e.Cancel = true;
            WebCommon.AlertGridView(sender, "Cannot create new room. Please contact Administrator");
        }
    }

    protected void gridRoom_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        try
        {
            // Validate user right for updating
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Update.Key, out _message))//Kiem tra xem user co quyen cap nhat hay khong
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Validate empty field
            if (!WebCommon.ValidateEmpty("Service", e.NewValues["ServicesId"], out _message)
                || !WebCommon.ValidateEmpty("Title", e.NewValues["Title"], out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Validate integer value for service id
            int iIndex;
            if (!Int32.TryParse(e.NewValues["ServicesId"].ToString().Trim(), out iIndex))
            {
                WebCommon.AlertGridView(sender, "Service is invalid.");
                e.Cancel = true;
                return;
            }

            // Set pure value
            e.NewValues["Title"] = e.NewValues["Title"].ToString().Trim();
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;

            // Show message alert delete successfully
            WebCommon.AlertGridView(sender, "Room is updated successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            e.Cancel = true;
            WebCommon.AlertGridView(sender, "Cannot update room. Please contact Administrator");
        }
    }
  
    /// <summary>
    /// Tao option All
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridRoom_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "ServicesId")
        {
            // Create option All for combobox Services
            var comboBox = e.Editor as ASPxComboBox;
            if (comboBox != null)
                comboBox.ClientSideEvents.Init = "function(s, e) {s.InsertItem('" + GeneralConstants.FilterAll.Key
                    + "', '" + GeneralConstants.FilterAll.Value + "', '');}";
        }
    }

    /// <summary>
    /// Xoa nhieu room
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridRoom_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
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
                    WebCommon.AlertGridView(sender, "Cannot find room.");
                    tm.Rollback();
                    return;
                }

                // Lay danh sach Id cua cac row duoc select
                var fieldNames = new[] { "Id" };
                List<object> columnValues = grid.GetSelectedFieldValues(fieldNames);

                // Doi trang thai cua cac roster
                foreach (object roomId in columnValues)
                {
                    int id;
                    if (!Int32.TryParse(roomId.ToString(), out id))
                    {
                        WebCommon.AlertGridView(sender, "Room is invalid. You cannot delete it.");
                        tm.Rollback();
                        return;
                    }

                    var room = DataRepository.RoomProvider.GetById(id);
                    if (room == null || room.IsDisabled)
                    {
                        WebCommon.AlertGridView(sender, "Room is not existed. You cannot delete it.");
                        tm.Rollback();
                        return;
                    }
                    DataRepository.RoomProvider.DeepLoad(room);

                    // Kiem tra xem co cho nao dang su dung room hay khong
                    if (room.RosterCollection.Exists(x => !x.IsDisabled) || room.DoctorRoomCollection.Exists(x => !x.IsDisabled)
                        || room.AppointmentCollection.Exists(x => !x.IsDisabled))
                    {
                        WebCommon.AlertGridView(sender, String.Format("Room {0} is using, you cannot delete it.", room.Title));
                        tm.Rollback();
                        return;
                    }
                    room.IsDisabled = true;
                    room.UpdateUser = WebCommon.GetAuthUsername();
                    room.UpdateDate = DateTime.Now;
                    DataRepository.RoomProvider.Update(tm, room);
                }
                tm.Commit();
                WebCommon.AlertGridView(sender, grid.Selection.Count > 1 ? "Deleted rooms." : "Deleted room.");

                // Set tam duration de lay duoc danh sach moi
                //int duration = RosterDataSource.CacheDuration;
                //RosterDataSource.CacheDuration = 0;
                //grid.DataBind();
                grid.Selection.UnselectAll();
                //RosterDataSource.CacheDuration = duration;
            }
        }
        catch (Exception ex)
        {
            tm.Rollback();
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete service. Please contact Administrator.");
        }
    }

    /// <summary>
    /// An cac row co IsDisabled bang true
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridRoom_OnHtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        try
        {
            if (e.RowType == GridViewRowType.Data)
            {
                e.Row.Visible = !Boolean.Parse(e.GetValue("IsDisabled").ToString());
            }
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
}
