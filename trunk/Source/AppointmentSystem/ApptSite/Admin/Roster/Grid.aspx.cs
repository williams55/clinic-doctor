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
using DevExpress.Utils;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using Log.Controller;
using Newtonsoft.Json;

public partial class Admin_Roster_Grid : Page
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
            btnAdd.Visible = RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Create.Key, out _message);

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
            var doctor = grid.FindEditFormTemplateControl("choDoctor") as ASPxComboBox;
            var rosterType = grid.FindEditFormTemplateControl("cboRosterType") as ASPxComboBox;

            if (fromTime == null || fromDate == null || endTime == null || endDate == null || doctor == null || rosterType == null)
            {
                WebCommon.AlertGridView(sender, "New form is error.");
                e.Cancel = true;
                return;
            }

            var chkIsRepeat = grid.FindEditFormTemplateControl("chkIsRepeat") as ASPxCheckBox;
            var chkWeekday = grid.FindEditFormTemplateControl("chkWeekday") as CheckBoxList;

            // Validate empty field
            if (!WebCommon.ValidateEmpty("Doctor", doctor.SelectedItem.Value, out _message)
                || !WebCommon.ValidateEmpty("Roster Type", rosterType.SelectedItem.Value, out _message)
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
            var username = doctor.SelectedItem.Value.ToString();
            int intRosterTypeId;
            var note = e.NewValues["Note"].ToString();

            if (!Int32.TryParse(rosterType.SelectedItem.Value.ToString(), out intRosterTypeId))
            {
                WebCommon.AlertGridView(sender, "Roster Type is invalid.");
                e.Cancel = true;
                return;
            }

            if (chkIsRepeat != null && chkIsRepeat.Checked && chkWeekday != null)
            {
                #region Validate Time
                // Set lai ngay cho endtime de so sanh
                timeEnd = new DateTime(timeStart.Year, timeStart.Month, timeStart.Day, timeEnd.Hour, timeEnd.Minute, 0);

                if (timeStart >= timeEnd)
                {
                    WebCommon.AlertGridView(sender, "End time must be greater than start time.");
                    e.Cancel = true;
                    return;
                }
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
                e.NewValues["Username"] = username;
                e.NewValues["RosterTypeId"] = intRosterTypeId;
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
                            CreateUser = WebCommon.GetAuthUsername(),
                            UpdateUser = WebCommon.GetAuthUsername()
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
                e.NewValues["Username"] = username;
                e.NewValues["RosterTypeId"] = intRosterTypeId;
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
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            tm.BeginTransaction();

            // Validate user right for updating
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Update.Key, out _message))//Kiem tra xem user co quyen cap nhat hay khong
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                tm.Rollback();
                return;
            }

            var grid = sender as ASPxGridView;
            if (grid == null)
            {
                WebCommon.AlertGridView(sender, "Cannot find roster.");
                e.Cancel = true;
                tm.Rollback();
                return;
            }

            var objStartTime = grid.FindEditFormTemplateControl("fromTimeEdit") as ASPxTimeEdit;
            var objStartDate = grid.FindEditFormTemplateControl("fromDateEdit") as ASPxDateEdit;
            var objEndTime = grid.FindEditFormTemplateControl("endTimeEdit") as ASPxTimeEdit;
            var objEndDate = grid.FindEditFormTemplateControl("endDateEdit") as ASPxDateEdit;
            var txtId = grid.FindEditFormTemplateControl("txtId") as ASPxTextBox;
            var radAllSimilar = grid.FindEditFormTemplateControl("radAllSimilar") as ASPxRadioButton;
            var radEnabledSimilar = grid.FindEditFormTemplateControl("radEnabledSimilar") as ASPxRadioButton;
            var doctor = grid.FindEditFormTemplateControl("choDoctorEdit") as ASPxComboBox;
            var rosterType = grid.FindEditFormTemplateControl("cboRosterTypeEdit") as ASPxComboBox;

            if (objStartTime == null || objStartDate == null || objEndTime == null || objEndDate == null
                || txtId == null || radAllSimilar == null || radEnabledSimilar == null || doctor == null || rosterType == null)
            {
                WebCommon.AlertGridView(sender, "Edit form is error.");
                e.Cancel = true;
                tm.Rollback();
                return;
            }

            // Validate empty field
            if (!WebCommon.ValidateEmpty("Doctor", doctor.SelectedItem.Value, out _message)
                || !WebCommon.ValidateEmpty("Roster Type", rosterType.SelectedItem.Value, out _message)
                || !WebCommon.ValidateEmpty("From Time", objStartTime.Text, out _message)
                || !WebCommon.ValidateEmpty("From Date", objStartDate.Text, out _message)
                || !WebCommon.ValidateEmpty("End Time", objEndTime.Text, out _message)
                || !WebCommon.ValidateEmpty("End Date", objEndDate.Text, out _message))
            {
                WebCommon.AlertGridView(sender, _message);
                e.Cancel = true;
                tm.Rollback();
                return;
            }

            // Lay du lieu duoc nhap vao
            var rosterId = txtId.Text;
            var username = doctor.SelectedItem.Value.ToString();
            int intRosterTypeId;
            var note = e.NewValues["Note"].ToString().Trim();
            var startTime = (DateTime)objStartTime.Value;
            var startDate = (DateTime)objStartDate.Value;
            var endTime = (DateTime)objEndTime.Value;
            var endDate = (DateTime)objEndDate.Value;
            string errorMessage = string.Empty;

            if (!Int32.TryParse(rosterType.SelectedItem.Value.ToString(), out intRosterTypeId))
            {
                WebCommon.AlertGridView(sender, "Roster Type is invalid.");
                e.Cancel = true;
                tm.Rollback();
                return;
            }

            #region Validate Time
            // Set lai ngay cho endtime de so sanh
            endTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, endTime.Hour, endTime.Minute, 0);

            if (startTime >= endTime)
            {
                WebCommon.AlertGridView(sender, "End time must be greater than start time.");
                e.Cancel = true;
                tm.Rollback();
                return;
            }

            // Tao thoi gian moi de kiem tra
            var newStartTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hour, startTime.Minute, 0);
            var newEndTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hour, endTime.Minute, 0);
            #endregion

            #region Kiem tra roster hien tai
            // Lay thong tin roster hien tai
            var currentRoster = DataRepository.RosterProvider.GetById(rosterId);

            // Kiem tra roster co ton tai hay khong
            if (currentRoster == null)
            {
                WebCommon.AlertGridView(sender, String.Format("Roster {0} is not existed.", txtId.Text));
                e.Cancel = true;
                tm.Rollback();
                return;
            }

            // Kiem tra xem roster co phai da qua khong
            if (currentRoster.StartTime < DateTime.Now)
            {
                WebCommon.AlertGridView(sender, String.Format("Roster {0} is passed.", txtId.Text));
                e.Cancel = true;
                tm.Rollback();
                return;
            }

            // Kiem tra appointment, chua lam
            DataRepository.RosterProvider.DeepLoad(currentRoster);
            if (currentRoster.AppointmentCollection.Any(appointment => appointment.StartTime < newStartTime || appointment.EndTime > newEndTime))
            {
                errorMessage += String.Format("<br />Cannot change roster {0} because there are some appointments.", currentRoster.Id);
            }
            #endregion

            // Neu similar roster duoc chon
            if ((radAllSimilar.Checked || radEnabledSimilar.Checked) && currentRoster.RepeatId != null)
            {
                int count;
                var lstSimilar = DataRepository.RosterProvider.GetPaged(
                    String.Format("IsDisabled = 'False' AND RepeatId = '{0}' AND Id <> '{1}'", currentRoster.RepeatId, currentRoster.Id)
                    , "Id desc", 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);
                DataRepository.RosterProvider.DeepLoad(lstSimilar);
                foreach (var roster in lstSimilar)
                {
                    // Kiem tra xem roster co phai da qua khong, dong thoi neu loai update la all, neu la enabled thi ko can bao loi
                    if (roster.StartTime < DateTime.Now)
                    {
                        if (radAllSimilar.Checked)
                        {
                            errorMessage += String.Format("<br />Roster {0} is passed.", roster.Id);
                        }
                        continue;
                    }

                    // Neu roster da co appointment thi bao loi
                    // Cho nay chua them column roster trong appointment nen chua kiem tra duoc
                    if (roster.AppointmentCollection.Any(appointment => appointment.StartTime < newStartTime || appointment.EndTime > newEndTime))
                    {
                        errorMessage += String.Format("<br />Cannot change roster {0} because there are some appointments.", currentRoster.Id);
                    }

                    roster.StartTime = new DateTime(roster.StartTime.Year, roster.StartTime.Month, roster.StartTime.Day
                        , startTime.Hour, startTime.Minute, 0);
                    roster.EndTime = new DateTime(roster.EndTime.Year, roster.EndTime.Month, roster.EndTime.Day
                        , endTime.Hour, endTime.Minute, 0);
                    roster.Note = note;
                    roster.RosterTypeId = intRosterTypeId;
                    roster.Username = username;
                    roster.UpdateUser = WebCommon.GetAuthUsername();
                    roster.UpdateDate = DateTime.Now;
                    DataRepository.RosterProvider.Save(tm, roster);
                }
            }

            if (errorMessage.Length > 0)
            {
                WebCommon.AlertGridView(sender, String.Format("There are some rosters error: {0}", errorMessage.Substring(0, errorMessage.Length - 1)));
                e.Cancel = true;
                tm.Rollback();
                return;
            }

            // Set pure value
            e.NewValues["Username"] = username;
            e.NewValues["RosterTypeId"] = intRosterTypeId;
            e.NewValues["StartTime"] = newStartTime;
            e.NewValues["EndTime"] = newEndTime;
            e.NewValues["UpdateUser"] = WebCommon.GetAuthUsername();
            e.NewValues["UpdateDate"] = DateTime.Now;

            // Show message alert delete successfully
            tm.Commit();
            WebCommon.AlertGridView(sender, "Roster is updated successfully.");
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            e.Cancel = true;
            tm.Rollback();
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

    /// <summary>
    /// Tat mo nut edit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridRoster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        try
        {
            if (e.VisibleIndex == -1) return;

            // Neu la button edit thi kiem tra dieu kien de hien thi
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                var roster = gridRoster.GetRow(e.VisibleIndex) as Roster;
                e.Visible = roster != null && roster.StartTime > DateTime.Now;
            }
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    protected void gridRoster_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        try
        {
            // Neu la button delete thi kiem tra dieu kien de hien thi
            if (e.CellType == GridViewTableCommandCellType.Data && e.ButtonID == "btnDelete")
            {
                var grid = (ASPxGridView)sender;
                var roster = grid.GetRow(e.VisibleIndex) as Roster;
                if (roster != null && roster.StartTime > DateTime.Now)
                {
                    e.Visible = DefaultBoolean.True;
                }
                else
                {
                    e.Visible = DefaultBoolean.False;
                }
            }
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
}
