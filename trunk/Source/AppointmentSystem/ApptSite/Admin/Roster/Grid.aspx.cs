using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentBusiness.Util;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using Appt.Common.Constants;
using Common;
using Common.Util;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using Log.Controller;
using Newtonsoft.Json;

public partial class Admin_Roster_Grid : System.Web.UI.Page
{
    private const string ScreenCode = "Roster";
    static string _message;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Neu la postback thi bo qua
            if (IsPostBack)
            {
                return;
            }

            // Check reading right
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
                gridRoster.Visible = false;
                return;
            }

            // Set page size
            gridRoster.SettingsPager.PageSize = ServiceFacade.SettingsHelper.PageSize;

            // Lay cot co chua cac nut thao tac
            var commandColumn = gridRoster.Columns["Operation"] as GridViewCommandColumn;

            // Neu khong co cot do thi khong can lam tiep
            if (commandColumn == null) return;

            // Gan visible cho nut new, edit bang cach kiem tra quyen
            commandColumn.EditButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Update.Key, out _message);
            commandColumn.NewButton.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key, out _message);

            // Get delete button
            GridViewCommandColumnCustomButton btnDelete =
                commandColumn.CustomButtons.Cast<GridViewCommandColumnCustomButton>().FirstOrDefault(
                    customButton => customButton.ID == "btnDelete");

            // Check delete right, if user has no right to delete => invisible delete button
            if (btnDelete != null && !RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                btnDelete.Visibility = GridViewCustomButtonVisibility.Invisible;
            }

            // Check visibility of button in button column
            // If there is no button is displayed => invisible column
            commandColumn.Visible = commandColumn.EditButton.Visible || commandColumn.NewButton.Visible ||
                                 (btnDelete != null && btnDelete.Visibility != GridViewCustomButtonVisibility.Invisible);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.ShowDialog(this, "System is error. Please contact Administrator.");
        }
    }

    protected void gridRoster_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
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

            // Lay roster muon xoa
            var id = gridRoster.GetRowValues(e.VisibleIndex, "Id").ToString();
            Roster roster = DataRepository.RosterProvider.GetById(id);
            if (roster == null || roster.IsDisabled)
            {
                WebCommon.AlertGridView(sender, "There is no roster to delete.");
                return;
            }
            if (roster.StartTime <= DateTime.Now)
            {
                WebCommon.AlertGridView(sender, "Cannot delete roster. Roster is expired.");
                return;
            }

            roster.IsDisabled = true;
            roster.UpdateUser = WebCommon.GetAuthUsername();
            roster.UpdateDate = DateTime.Now;
            DataRepository.RosterProvider.Update(roster);
            #endregion
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot delete roster. Please contact Administrator");
        }
    }

    protected void gridRoster_RowInserting(object sender, ASPxDataInsertingEventArgs e)
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

            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                WebCommon.AlertGridView(sender, "Cannot find roster.");
                e.Cancel = true;
                return;
            }

            var fromTime = grid.FindEditFormTemplateControl("fromTime") as ASPxTimeEdit;
            var fromDate = grid.FindEditFormTemplateControl("fromDate") as ASPxDateEdit;
            var endTime = grid.FindEditFormTemplateControl("endTime") as ASPxTimeEdit;
            var endDate = grid.FindEditFormTemplateControl("endDate") as ASPxDateEdit;

            if (fromTime == null || fromDate == null || endTime == null || endDate == null)
            {
                WebCommon.AlertGridView(sender, "New form is error.");
                e.Cancel = true;
                return;
            }

            var chkIsRepeat = grid.FindEditFormTemplateControl("chkIsRepeat") as ASPxCheckBox;
            var chkWeekday = grid.FindEditFormTemplateControl("chkWeekday") as CheckBoxList;

            // Validate empty field
            if (!WebCommon.ValidateEmpty("Doctor", e.NewValues["Username"], out _message)
                || !WebCommon.ValidateEmpty("Roster Type", e.NewValues["RosterTypeId"], out _message)
                || !WebCommon.ValidateEmpty("From Time", fromTime.Text, out _message)
                || !WebCommon.ValidateEmpty("From Date", fromDate.Text, out _message)
                || !WebCommon.ValidateEmpty("End Time", endTime.Text, out _message)
                || !WebCommon.ValidateEmpty("End Date", endDate.Text, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Get date time
            DateTime dtStart = (DateTime)fromDate.Value, dtEnd = (DateTime)endDate.Value;
            DateTime timeStart = (DateTime)fromTime.Value, timeEnd = (DateTime)endTime.Value;
            dtStart = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, timeStart.Hour, timeStart.Minute, 0);
            dtEnd = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, timeEnd.Hour, timeEnd.Minute, 0);

            // Declare list of roster in case repeat roster
            var lstRoster = new TList<Roster>();

            // Lay du lieu duoc nhap vao
            var username = e.NewValues["Username"].ToString();
            int intRosterTypeId;
            var note = e.NewValues["Note"].ToString();

            if (!Int32.TryParse(e.NewValues["RosterTypeId"].ToString(), out intRosterTypeId))
            {
                WebCommon.AlertGridView(sender, "Roster Type is invalid.");
                e.Cancel = true;
                return;
            }

            if (chkIsRepeat != null && chkIsRepeat.Checked && chkWeekday != null)
            {
                #region Validate Time
                if (dtStart >= dtEnd)
                {
                    WebCommon.AlertGridView(sender, "To time must be greater than from date.");
                    e.Cancel = true;
                    return;
                }

                // If roster is created in a passed or current day
                DateTime dtNow = DateTime.Now;
                if (new DateTime(dtNow.Year, dtNow.Month, dtNow.Day) >= new DateTime(dtStart.Year, dtStart.Month, dtStart.Day))
                {
                    WebCommon.AlertGridView(sender, "You can not change roster to passed or current date.");
                    e.Cancel = true;
                    return;
                }
                #endregion

                // Get auto increasement id
                string perfix = ServiceFacade.SettingsHelper.RosterPrefix + DateTime.Now.ToString("yyMMdd");
                int count;
                TList<Roster> objPo = DataRepository.RosterProvider.GetPaged("Id like '" + perfix + "' + '%'", "Id desc", 0, 1, out count);
                string number = count == 0 ? "001" : String.Format("{0:000}", int.Parse(objPo[0].Id.Substring(objPo[0].Id.Length - 3)) + 1);

                // Variable for error message if there is conflict roster
                string errorMessage = string.Empty;
                var repeatRoster = Guid.NewGuid();

                // Set gia tri cho ngay dau tien
                // Se khong cho gia tri ngay dau tien vao vong lap
                e.NewValues["Id"] = perfix + number;
                e.NewValues["StartTime"] = dtStart;
                e.NewValues["EndTime"] = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, dtEnd.Hour, dtEnd.Minute, 0); ;
                e.NewValues["RepeatId"] = repeatRoster;
                e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
                e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;

                // Lay danh sach ngay duoc lap lai
                var weekday = (from ListItem item in chkWeekday.Items where item.Selected select item.Value).ToList();

                while (dtStart <= dtEnd)
                {
                    dtStart = dtStart.AddDays(1);
                    if (weekday.Exists(x => x == dtStart.DayOfWeek.ToString()))
                    {
                        var dtTmpStart = dtStart;
                        var dtTmpEnd = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, dtEnd.Hour, dtEnd.Minute, 0);

                        // Check existed rosters
                        string query = string.Format("Username = '{0}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'"
                            , username, dtTmpEnd, dtTmpStart);
                        DataRepository.RosterProvider.GetPaged(query, "Id desc", 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);
                        // If there is no roster -> insert new roster
                        if (count > 0)
                        {
                            errorMessage += dtTmpStart.DayOfWeek.ToString() + " " + dtTmpStart.ToString("dd MMM yyyy") + ", ";
                            continue;
                        }

                        number = String.Format("{0:000}", int.Parse(number) + 1);
                        lstRoster.Add(new Roster
                        {
                            Id = perfix + number,
                            Username = username,
                            RosterTypeId = intRosterTypeId,
                            StartTime = dtTmpStart,
                            EndTime = dtTmpEnd,
                            RepeatId = repeatRoster,
                            Note = note,
                            CreateUser = username,
                            UpdateUser = username
                        });
                    }
                }

                if (errorMessage.Length > 0)
                {
                    WebCommon.AlertGridView(sender, String.Format("There are some rosters conflicted: {0}", errorMessage.Substring(0, errorMessage.Length - 1)));
                    e.Cancel = true;
                    return;
                }

                // Insert new rosters
                DataRepository.RosterProvider.Insert(lstRoster);
            }
            else
            {
                #region Validate Time
                if (dtStart >= dtEnd)
                {
                    WebCommon.AlertGridView(sender, "To time must be greater than from date.");
                    e.Cancel = true;
                    return;
                }
                // If roster is created in a passed or current day
                DateTime dtNow = DateTime.Now;
                if (new DateTime(dtNow.Year, dtNow.Month, dtNow.Day) >= new DateTime(dtStart.Year, dtStart.Month, dtStart.Day))
                {
                    WebCommon.AlertGridView(sender, "You can not change roster to passed or current date.");
                    e.Cancel = true;
                    return;
                }
                #endregion

                var newObj = new Roster();

                string perfix = ServiceFacade.SettingsHelper.RosterPrefix + DateTime.Now.ToString("yyMMdd");
                int count;
                TList<Roster> objPo = DataRepository.RosterProvider.GetPaged("Id like '" + perfix + "' + '%'", "Id desc", 0, 1, out count);
                string id;
                if (count == 0)
                    id = perfix + "001";
                else
                {
                    id = perfix + String.Format("{0:000}", int.Parse(objPo[0].Id.Substring(objPo[0].Id.Length - 3)) + 1);
                }

                // Check existed rosters
                string query = string.Format("Username = '{0}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'"
                    , username, dtStart, dtEnd);
                DataRepository.RosterProvider.GetPaged(query, "Id desc", 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);
                // If there is no roster -> insert new roster
                if (count > 0)
                {
                    WebCommon.AlertGridView(sender, String.Format("There is roster conflicted: From {0} {1} to {2} {3}"
                        , newObj.StartTime.DayOfWeek, newObj.StartTime.ToString("dd MMM yyyy HH:mm")
                        , newObj.EndTime.DayOfWeek, newObj.EndTime.ToString("dd MMM yyyy HH:mm")));
                    e.Cancel = true;
                    return;
                }

                // Set pure value
                e.NewValues["Id"] = id;
                e.NewValues["StartTime"] = dtStart;
                e.NewValues["EndTime"] = dtEnd;
                e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
                e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
            }

            // Show message alert delete successfully
            WebCommon.AlertGridView(sender, "Roster is created successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            e.Cancel = true;
            WebCommon.AlertGridView(sender, "Cannot create new Roster. Please contact Administrator");
        }
    }

    protected void gridRoster_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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

            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                WebCommon.AlertGridView(sender, "Cannot find roster.");
                e.Cancel = true;
                return;
            }

            var fromTime = grid.FindEditFormTemplateControl("fromTime") as ASPxTimeEdit;
            var fromDate = grid.FindEditFormTemplateControl("fromDate") as ASPxDateEdit;
            var endTime = grid.FindEditFormTemplateControl("endTime") as ASPxTimeEdit;
            var endDate = grid.FindEditFormTemplateControl("endDate") as ASPxDateEdit;
            var txtId = grid.FindEditFormTemplateControl("txtId") as ASPxTextBox;
            var lbChoosen = grid.FindEditFormTemplateControl("lbChoosen") as ASPxListBox;

            if (fromTime == null || fromDate == null || endTime == null || endDate == null || txtId == null || lbChoosen == null)
            {
                WebCommon.AlertGridView(sender, "Edit form is error.");
                e.Cancel = true;
                return;
            }

            var chkSimilar = grid.FindEditFormTemplateControl("chkSimilar") as ASPxCheckBox;

            // Validate empty field
            if (!WebCommon.ValidateEmpty("Doctor", e.NewValues["Username"], out _message)
                || !WebCommon.ValidateEmpty("Roster Type", e.NewValues["RosterTypeId"], out _message)
                || !WebCommon.ValidateEmpty("From Time", fromTime.Text, out _message)
                || !WebCommon.ValidateEmpty("From Date", fromDate.Text, out _message)
                || !WebCommon.ValidateEmpty("End Time", endTime.Text, out _message)
                || !WebCommon.ValidateEmpty("End Date", endDate.Text, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                return;
            }

            // Get date time
            DateTime dtStart = (DateTime)fromDate.Value, dtEnd = (DateTime)endDate.Value;
            DateTime timeStart = (DateTime)fromTime.Value, timeEnd = (DateTime)endTime.Value;
            dtStart = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, timeStart.Hour, timeStart.Minute, 0);
            dtEnd = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, timeEnd.Hour, timeEnd.Minute, 0);

            // Lay du lieu duoc nhap vao
            var rosterId = txtId.Text;
            var username = e.NewValues["Username"].ToString();
            int intRosterTypeId;
            var note = e.NewValues["Note"].ToString();

            if (!Int32.TryParse(e.NewValues["RosterTypeId"].ToString(), out intRosterTypeId))
            {
                WebCommon.AlertGridView(sender, "Roster Type is invalid.");
                e.Cancel = true;
                return;
            }

            if (chkSimilar != null && chkSimilar.Checked)
            {
                #region Validate Time
                if (dtStart >= dtEnd)
                {
                    WebCommon.AlertGridView(sender, "To time must be greater than from date.");
                    e.Cancel = true;
                    return;
                }

                // If roster is created in a passed or current day
                DateTime dtNow = DateTime.Now;
                if (new DateTime(dtNow.Year, dtNow.Month, dtNow.Day) >= new DateTime(dtStart.Year, dtStart.Month, dtStart.Day))
                {
                    WebCommon.AlertGridView(sender, "You can not change roster to passed or current date.");
                    e.Cancel = true;
                    return;
                }
                #endregion

                // Get roster by id
                Roster rosterItem = DataRepository.RosterProvider.GetById(rosterId);
            }
            else
            {
                
            }
            // Set pure value
            e.NewValues["Title"] = e.NewValues["Title"].ToString().Trim();
            e.NewValues["CreateUser"] = e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["CreateDate"] = e.NewValues["UpdateDate"] = DateTime.Now;
            e.Cancel = true;//tmp
            // Show message alert delete successfully
            WebCommon.AlertGridView(sender, "Role is updated successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            e.Cancel = true;
            WebCommon.AlertGridView(sender, "Cannot update roster. Please contact Administrator");
        }
    }

    // Lay khoang thoi gian phu hop voi moi buoc nhay
    protected DateTime getDate(object obj, bool isEndTime)
    {
        var currentTime = DateTime.Now;
        try
        {
            if (obj == null)
            {
                currentTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, currentTime.Hour, 0, 0)
                    .
                    AddMinutes(ServiceFacade.SettingsHelper.RosterMinuteStep * (isEndTime ? 2 : 1));
            }
            else
            {
                currentTime = DateTime.Parse(obj.ToString());
            }
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
        return currentTime;
    }

    protected void gridRoster_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
    {
        try
        {
            // Lay danh sach weekday va bind vao checkbox list
            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                return;
            }
            var chkWeekday = grid.FindEditFormTemplateControl("chkWeekday") as CheckBoxList;
            var lstWeekday = JsonConvert.DeserializeObject<List<ConstantKeyValue>>(Constants.Weekdays);

            if (chkWeekday == null)
            {
                return;
            }

            chkWeekday.DataSource = lstWeekday;
            chkWeekday.DataTextField = "key";
            chkWeekday.DataValueField = "key";
            chkWeekday.DataBind();
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    protected void gridRoster_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
    {
        try
        {
            //var grid = sender as ASPxGridView;
            //if (grid == null)
            //{
            //    WebCommon.AlertGridView(sender, "Cannot find roster.");
            //    e.Cancel = true;
            //    return;
            //}

            //// Lay RepeatId de tim nhung roster co cung dang
            //var hdfRepeatId = grid.FindEditFormTemplateControl("hdfRepeatId") as HiddenField;
            //var lbAvailable = grid.FindEditFormTemplateControl("lbAvailable") as ASPxListBox;
            //if (lbAvailable == null || hdfRepeatId == null)
            //{
            //    WebCommon.AlertGridView(sender, "Cannot find roster.");
            //    e.Cancel = true;
            //    return;
            //}

            //int count;
            //var lst =
            //    DataRepository.RosterProvider.GetPaged(
            //        String.Format("RepeatId = '{0}' AND IsDisabled = 'False'",
            //                      hdfRepeatId.Value),
            //        "Id ASC", 0, ServiceFacade.SettingsHelper.GetPagedLength,
            //        out count);
            
            //// Bind du lieu vao list
            //lbAvailable.DataSource = lst;
            //lbAvailable.TextField = "Id";
            //lbAvailable.ValueField = "Id";
            //lbAvailable.DataBind();
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            WebCommon.AlertGridView(sender, "Cannot update roster. Please contact Administrator");
        }
    }
}
