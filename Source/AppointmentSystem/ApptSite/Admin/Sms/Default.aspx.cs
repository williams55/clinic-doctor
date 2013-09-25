using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentBusiness.BO;
using AppointmentBusiness.Util;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using Appt.Common.Constants;
using ApptSite;
using Common.Util;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using Logger.Controller;

public partial class Admin_Sms_Default : System.Web.UI.Page
{
    private const string ScreenCode = "Sms";
    static string _message;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Check reading right
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridSms.Visible = false;
                return;
            }

            // Set page size
            gridSms.SettingsPager.PageSize = ServiceFacade.SettingsHelper.PageSize;

            // Lay cot co chua cac nut thao tac
            var gridcolums = gridSms.Columns["Operation"] as GridViewCommandColumn;

            // Neu khong co cot do thi khong can lam tiep
            if (gridcolums == null) return;

            // Gan visible cho nut new, edit bang cach kiem tra quyen
            //gridcolums.EditButton.Visible = RightAccess.CheckUserRight(AccountSession.Session, ScreenCode,
            //                                                           OperationConstant.Update.Key, out _message);

            // Set hien thi/an cho nut new
            btnAdd.Visible = RightAccess.CheckUserRight(AccountSession.Session, ScreenCode,
                                                        OperationConstant.Create.Key, out _message);

            // Set hien thi/an cho nut delete
            btnGeneralDelete.Visible = RightAccess.CheckUserRight(AccountSession.Session,
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

            #region Set datasource cho column
            var column = (gridSms.Columns["SmsType"] as GridViewDataComboBoxColumn);
            if (column != null)
            {
                column.PropertiesComboBox.DataSource = SmsType.GetAll();
                column.PropertiesComboBox.ValueField = "Key";
                column.PropertiesComboBox.TextField = "Value";
            }

            #endregion
        }
        catch (Exception ex)
        {
            LoggerController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.ShowDialog(this, "System is error. Please contact Administrator.");
        }
    }

    protected void gridSms_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        try
        {
            if (e.ButtonID != "btnDelete") return;
            #region Delete row
            // Check deleting right
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                return;
            }

            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                WebCommon.AlertGridView(sender, "Cannot find room.");
                return;
            }

            // Validate id
            Int32 id;
            if (Int32.TryParse(grid.GetRowValues(e.VisibleIndex, "Id").ToString(), out id))
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
                room.UpdateUser = AccountSession.Session;
                room.UpdateDate = DateTime.Now;
                DataRepository.RoomProvider.Update(room);
                WebCommon.AlertGridView(sender, "Room is deleted successfully.");
            }
            #endregion
        }
        catch (Exception ex)
        {
            LoggerController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete room. Please contact Administrator");
        }
    }

    protected void gridSms_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        try
        {
            // Validate user right for creating
            if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Create.Key, out _message))
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
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = AccountSession.Session;
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;

            // Show message alert delete successfully
            WebCommon.AlertGridView(sender, "Room is created successfully.");
        }
        catch (Exception ex)
        {
            LoggerController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            e.Cancel = true;
            WebCommon.AlertGridView(sender, "Cannot create new room. Please contact Administrator");
        }
    }

    /// <summary>
    /// Tao option All
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridSms_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "SmsType")
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
    protected void gridSms_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        tm.BeginTransaction();
        try
        {
            ASPxGridView grid;
            switch (e.Parameters)
            {
                case "Delete":
                    // Check deleting right
                    if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Delete.Key, out _message))
                    {
                        WebCommon.AlertGridView(sender, _message);
                        tm.Rollback();
                        return;
                    }

                    grid = sender as ASPxGridView;
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
                        room.UpdateUser = AccountSession.Session;
                        room.UpdateDate = DateTime.Now;
                        DataRepository.RoomProvider.Update(tm, room);
                    }
                    tm.Commit();
                    WebCommon.AlertGridView(sender, String.Format("{0} deleted successfully.",
                                                          grid.Selection.Count > 1 ? "Rooms are" : "Room is"));

                    // Set tam duration de lay duoc danh sach moi
                    grid.Selection.UnselectAll();
                    break;
                case "Save":
                    // Check deleting right
                    if (!RightAccess.CheckUserRight(AccountSession.Session, ScreenCode, OperationConstant.Create.Key, out _message))
                    {
                        WebCommon.AlertGridView(sender, _message);
                        tm.Rollback();
                        return;
                    }

                    grid = sender as ASPxGridView;
                    if (grid == null)
                    {
                        WebCommon.AlertGridView(sender, "Form error.");
                        tm.Rollback();
                        return;
                    }

                    #region Validation
                    var objMessage = grid.FindEditFormTemplateControl("txtMessage") as ASPxMemo;
                    var objSendTime = grid.FindEditFormTemplateControl("dtSendTime") as ASPxTimeEdit;
                    var objSendDate = grid.FindEditFormTemplateControl("dtSendDate") as ASPxDateEdit;
                    var objIsSendNow = grid.FindEditFormTemplateControl("chkIsSendNow") as ASPxCheckBox;
                    var objPhones = grid.FindEditFormTemplateControl("txtPhones") as ASPxMemo;

                    if (objMessage == null || objSendTime == null || objSendDate == null
                        || objIsSendNow == null || objPhones == null)
                    {
                        WebCommon.AlertGridView(sender, "Form error. Please contact Administrator.");
                        tm.Rollback();
                        return;
                    }

                    if (string.IsNullOrEmpty(objMessage.Text.Trim()) || objSendTime.Value == null
                        || objSendDate.Value == null || string.IsNullOrEmpty(objPhones.Text.Trim()))
                    {
                        WebCommon.AlertGridView(sender, "Please input value before save.");
                        tm.Rollback();
                        return;
                    }
                    #endregion

                    var sendTime = (DateTime)objSendTime.Value;
                    var sendDate = (DateTime)objSendDate.Value;
                    var sms = new Sms
                                     {
                                         Message = objMessage.Text.Trim(),
                                         SmsType = SmsType.Other,
                                         SendTime = new DateTime(sendDate.Year, sendDate.Month, sendDate.Day, sendTime.Hour, sendTime.Minute, 0),
                                         IsSendNow = objIsSendNow.Checked,
                                         IsSent = false,
                                         SendingTimes = 1,
                                     };

                    string messageCode;
                    WebCommon.AlertGridView(sender,
                                            BoFactory.SmsBO.AddSms(sms, objPhones.Text, out messageCode)
                                                ? "Your SMS has been created successfully"
                                                : BoFactory.MessageConfigBO.GetMessage(messageCode));
                    grid.CancelEdit();
                    break;
            }
        }
        catch (Exception ex)
        {
            tm.Rollback();
            LoggerController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete service. Please contact Administrator.");
        }
    }
}