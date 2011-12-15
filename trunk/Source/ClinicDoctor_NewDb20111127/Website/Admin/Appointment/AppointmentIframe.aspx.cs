using System;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web;
using ClinicDoctor.Data;
using ClinicDoctor.Entities;
using ClinicDoctor.Settings.BusinessLayer;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_Appointment_AppointmentIframe : System.Web.UI.Page
{
    static string strSeperateStaff = " | ";
    static char chrSeperateStaff = '|';

    #region "Roster"
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string SaveEvent(string DoctorUsername, string RosterTypeId, string RosterTitle, string StartTime, string EndTime,
        string StartDate, string EndDate, string Note, string RepeatRoster, string Weekday, string Month)
    {
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data:  [{
         *              Id: id of roster,
         *              DoctorUserName: Doctor UserName,
         *              DoctorShortName: Doctor ShortName,
         *              RosterTypeId: Roster Type Id,
         *              RosterTypeTitle: Roster Type Title,
         *              start_date: start date,
         *              end_date: end date,
         *              note: note,
         *              text: roster type + note,
         *              color: color of event [get from status],
         *              isnew: true
         *          }, {}, {}]
         *  }
         */

        string result = string.Empty;

        // Check StaffId
        if (DoctorUsername == null)
        {
            result = @"[{ 'result': 'false', 'message': 'You must choose staff.', 'data': [] }]";
            return result;
        }

        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            tm.BeginTransaction();

            string username = EntitiesUtilities.GetAuthName();
            Staff obj = DataRepository.StaffProvider.GetByUserNameIsDisabled(tm, DoctorUsername, false);
            if (obj == null)
            {
                result = @"[{ 'result': 'false', 'message': 'Staff is not exist.', 'data': '[]' }]";
                goto StepResult;
            }

            // Get Roster Type
            RosterType objRt = DataRepository.RosterTypeProvider.GetByIdIsDisabled(Convert.ToInt64(RosterTypeId), false);
            if (objRt == null)
            {
                result = @"[{ 'result': 'false', 'message': 'Roster Type is not exist.', 'data': '[]' }]";
                goto StepResult;
            }

            // Get start time and end time
            int intStartHour = Convert.ToInt32(StartTime.Split(':')[0]);
            int intStartMinute = Convert.ToInt32(StartTime.Split(':')[1]);
            int intEndHour = Convert.ToInt32(EndTime.Split(':')[0]);
            int intEndMinute = Convert.ToInt32(EndTime.Split(':')[1]);

            // Repeat roster
            if (RepeatRoster == "true")
            {
                // Convert Month to right format
                DateTime month = Convert.ToDateTime(Month);
                DateTime item = new DateTime(month.Year, month.Month, 1);

                // Get auto increasement id
                string Perfix = "R" + ServiceFacade.SettingsHelper.RosterPrefix + DateTime.Now.ToString("yyMMdd");
                int Count = 0;
                string strNumber = "000";
                TList<DoctorRoster> objPo = DataRepository.DoctorRosterProvider.GetPaged(tm, "Id like '" + Perfix + "' + '%'", "Id desc", 0, 1, out Count);
                if (Count == 0)
                    strNumber = "000";
                else
                {
                    strNumber = String.Format("{0:000}", int.Parse(objPo[0].Id.Substring(objPo[0].Id.Length - 3)));
                }

                // Variable for result
                result = string.Empty;

                // Variable for error message if there is conflict roster
                string errorMessage = string.Empty;

                while (month.Month == item.Month)
                {
                    if (Weekday.Contains(item.DayOfWeek.ToString()))
                    {
                        DoctorRoster newObj = new DoctorRoster();
                        strNumber = String.Format("{0:000}", int.Parse(strNumber) + 1);

                        newObj.Id = Perfix + strNumber;

                        newObj.DoctorUserName = obj.UserName;
                        newObj.DoctorShortName = obj.ShortName;
                        newObj.RosterTypeId = Convert.ToInt64(RosterTypeId);
                        newObj.RosterTypeTitle = RosterTitle;
                        newObj.StartTime = new DateTime(item.Year, item.Month, item.Day, intStartHour, intStartMinute, 0);
                        newObj.EndTime = new DateTime(item.Year, item.Month, item.Day, intEndHour, intEndMinute, 0);
                        newObj.Note = Note;
                        newObj.ColorCode = (objRt.ColorCode != "" ? objRt.ColorCode : ServiceFacade.SettingsHelper.UncompleteColor);
                        newObj.CreateUser = username;
                        newObj.UpdateUser = username;

                        DataRepository.DoctorRosterProvider.Insert(tm, newObj);

                        // Check if start time >= end time
                        if (newObj.StartTime >= newObj.EndTime)
                        {
                            tm.Rollback();
                            result = @"[{ 'result': 'false', 'message': 'To date must be greater than From date.', 'data': [] }]";
                            goto StepResult;
                        }

                        // If roster is created in a passed or current day
                        DateTime dtNow = DateTime.Now;
                        if (new DateTime(dtNow.Year, dtNow.Month, dtNow.Day) >= new DateTime(newObj.StartTime.Year, newObj.StartTime.Month, newObj.StartTime.Day))
                        {
                            tm.Rollback();
                            result = @"[{ 'result': 'false', 'message': 'You can not change roster to passed or current date.', 'data': [] }]";
                            goto StepResult;
                        }

                        // Check roster before insert new roster
                        string query = string.Format("DoctorUserName = '{0}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'", obj.UserName, newObj.EndTime, newObj.StartTime);
                        TList<DoctorRoster> lstRoster = DataRepository.DoctorRosterProvider.GetPaged(tm, query, "Id desc", 0, 1, out Count);
                        // If there is no roster -> insert new roster
                        if (Count > 1)
                        {
                            errorMessage += newObj.StartTime.DayOfWeek.ToString() + " " + newObj.StartTime.ToString("dd MMM yyyy") + ", ";
                        }

                        result += @"{id: '" + newObj.Id + @"',"
                            + @"start_date: '" + newObj.StartTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                            + @"end_date: '" + newObj.EndTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                            + @"section_id: '" + newObj.DoctorUserName + @"',"
                            + @"text: '" + newObj.RosterTypeTitle + @"<br />Doctor: " + newObj.DoctorUserName + @"<br />";

                        if (!string.IsNullOrEmpty(newObj.Note))
                            result += newObj.Note;

                        result += @"',DoctorUserName: '" + newObj.DoctorUserName + @"',"
                           + @"DoctorShortName: '" + newObj.DoctorShortName + @"',"
                           + @"RosterTypeId: '" + newObj.RosterTypeId + @"',"
                           + @"RosterTypeTitle: '" + newObj.RosterTypeTitle + @"',"
                           + @"note: '" + newObj.Note + @"',"
                           + @"color: '" + newObj.ColorCode + @"',"
                           + @"isnew: 'false'"
                           + @"},";
                    }
                    item = item.AddDays(1);
                }

                if (errorMessage.Length > 0)
                {
                    tm.Rollback();
                    result = @"[{ 'result': 'false', 'message': 'There are some rosters conflicted: " + errorMessage.Substring(0, errorMessage.Length - 1) + "', 'data': [] }]";
                    goto StepResult;
                }
                else
                {
                    result = @"[{ 'result': 'true', 'message': '', 'data': [" + result.Substring(0, result.Length - 1) + @"] }]";
                }
            }
            else
            {
                DoctorRoster newObj = new DoctorRoster();

                // Get start date and end date
                DateTime dtStart = Convert.ToDateTime(StartDate);
                DateTime dtEnd = Convert.ToDateTime(EndDate);

                string Perfix = "R" + ServiceFacade.SettingsHelper.RosterPrefix + DateTime.Now.ToString("yyMMdd");
                int Count = 0;
                TList<DoctorRoster> objPo = DataRepository.DoctorRosterProvider.GetPaged(tm, "Id like '" + Perfix + "' + '%'", "Id desc", 0, 1, out Count);
                if (Count == 0)
                    newObj.Id = Perfix + "001";
                else
                {
                    newObj.Id = Perfix + String.Format("{0:000}", int.Parse(objPo[0].Id.Substring(objPo[0].Id.Length - 3)) + 1);
                }

                newObj.DoctorUserName = obj.UserName;
                newObj.DoctorShortName = obj.ShortName;
                newObj.RosterTypeId = Convert.ToInt64(RosterTypeId);
                newObj.RosterTypeTitle = RosterTitle;
                newObj.StartTime = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, intStartHour, intStartMinute, 0);
                newObj.EndTime = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, intEndHour, intEndMinute, 0);
                newObj.Note = Note;
                newObj.ColorCode = (objRt.ColorCode != "" ? objRt.ColorCode : ServiceFacade.SettingsHelper.UncompleteColor);
                newObj.CreateUser = username;
                newObj.UpdateUser = username;

                DataRepository.DoctorRosterProvider.Insert(tm, newObj);

                // Check if start time >= end time
                if (newObj.StartTime >= newObj.EndTime)
                {
                    tm.Rollback();
                    result = @"[{ 'result': 'false', 'message': 'To date must be greater than From date.', 'data': [] }]";
                    goto StepResult;
                }

                // If roster is created in a passed or current day
                DateTime dtNow = DateTime.Now;
                if (new DateTime(dtNow.Year, dtNow.Month, dtNow.Day) >= new DateTime(newObj.StartTime.Year, newObj.StartTime.Month, newObj.StartTime.Day))
                {
                    tm.Rollback();
                    result = @"[{ 'result': 'false', 'message': 'You can not change roster to passed or current date.', 'data': [] }]";
                    goto StepResult;
                }

                // Check roster before insert new roster
                string query = string.Format("DoctorUserName = '{0}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'", obj.UserName, newObj.EndTime, newObj.StartTime);
                TList<DoctorRoster> lstRoster = DataRepository.DoctorRosterProvider.GetPaged(tm, query, "Id desc", 0, 1, out Count);
                // If there is no roster -> insert new roster
                if (Count > 1)
                {
                    tm.Rollback();
                    result = @"[{ 'result': 'false', 'message': 'There is roster conflicted: From "
                        + newObj.StartTime.DayOfWeek.ToString() + " " + newObj.StartTime.ToString("dd MMM yyyy HH:mm") + " to  "
                        + newObj.EndTime.DayOfWeek.ToString() + " " + newObj.EndTime.ToString("dd MMM yyyy HH:mm") + "', 'data': [] }]";
                    goto StepResult;
                }

                result = @"[{ 'result': 'true', 'message': '', 'data': ["
                    + @"{id: '" + newObj.Id + @"',"
                    + @"start_date: '" + newObj.StartTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"end_date: '" + newObj.EndTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"section_id: '" + newObj.DoctorUserName + @"',"
                    + @"text: '" + newObj.RosterTypeTitle + @"<br />Doctor: " + newObj.DoctorUserName + @"<br />";

                if (!string.IsNullOrEmpty(newObj.Note))
                    result += newObj.Note;

                result += @"',DoctorUserName: '" + newObj.DoctorUserName + @"',"
                    + @"DoctorShortName: '" + newObj.DoctorShortName + @"',"
                    + @"RosterTypeId: '" + newObj.RosterTypeId + @"',"
                    + @"RosterTypeTitle: '" + newObj.RosterTypeTitle + @"',"
                    + @"note: '" + newObj.Note + @"',"
                    + @"color: '" + newObj.ColorCode + @"',"
                    + @"isnew: 'false'"
                    + @"}"
                   + @"] }]";
            }
            tm.Commit();

        }
        catch (Exception ex)
        {
            //Write log cho nay
            tm.Rollback();

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            goto StepResult;
        }

    StepResult:
        return result;
    }

    #region "Update Roster"
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string UpdateEventSave(string Id, string DoctorUsername, string RosterTypeId, string RosterTitle, string StartTime, string EndTime,
        string StartDate, string EndDate, string Note)
    {
        try
        {
            // Get start time and end time
            int intStartHour = Convert.ToInt32(StartTime.Split(':')[0]);
            int intStartMinute = Convert.ToInt32(StartTime.Split(':')[1]);
            int intEndHour = Convert.ToInt32(EndTime.Split(':')[0]);
            int intEndMinute = Convert.ToInt32(EndTime.Split(':')[1]);

            // Get start date and end date
            DateTime dtStart = Convert.ToDateTime(StartDate);
            DateTime dtEnd = Convert.ToDateTime(EndDate);

            dtStart = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, intStartHour, intStartMinute, 0);
            dtEnd = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, intEndHour, intEndMinute, 0);

            return UpdateEvent(Id, DoctorUsername, RosterTypeId, RosterTitle, dtStart, dtEnd, Note);
        }
        catch (Exception ex)
        {
            //Write log cho nay

            return @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string UpdateEventMove(string Id, string DoctorUsername, string RosterTypeId, string RosterTitle, string StartTime, string EndTime, string Note)
    {
        // Get start date and end date
        DateTime dtStart = Convert.ToDateTime(StartTime);
        DateTime dtEnd = Convert.ToDateTime(EndTime);

        return UpdateEvent(Id, DoctorUsername, RosterTypeId, RosterTitle, dtStart, dtEnd, Note);
    }

    // Update roster
    private static string UpdateEvent(string Id, string DoctorUsername, string RosterTypeId, string RosterTitle, DateTime StartTime, DateTime EndTime, string Note)
    {
        string result = string.Empty;

        // Check StaffId
        if (DoctorUsername == null)
        {
            result = @"[{ 'result': 'false', 'message': 'You must choose staff.', 'data': [] }]";
            return result;
        }

        int Count = 0;
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            tm.BeginTransaction();

            string username = EntitiesUtilities.GetAuthName();
            Staff obj = DataRepository.StaffProvider.GetByUserNameIsDisabled(tm, DoctorUsername, false);
            if (obj == null)
            {
                result = @"[{ 'result': 'false', 'message': 'Staff is not exist.', 'data': [] }]";
                goto StepResult;
            }

            DoctorRoster dr = DataRepository.DoctorRosterProvider.GetByIdIsCompleteIsDisabled(Id, false, false);
            if (dr == null)
            {
                result = @"[{ 'result': 'false', 'message': 'There is no roster to update or the roster is expired.', 'data': [] }]";
                goto StepResult;
            }

            // Get Roster Type
            RosterType objRt = DataRepository.RosterTypeProvider.GetByIdIsDisabled(Convert.ToInt64(RosterTypeId), false);
            if (objRt == null)
            {
                result = @"[{ 'result': 'false', 'message': 'Roster Type is not exist.', 'data': '[]' }]";
                goto StepResult;
            }

            dr.DoctorUserName = obj.UserName;
            dr.DoctorShortName = obj.ShortName;
            dr.RosterTypeId = Convert.ToInt64(RosterTypeId);
            dr.RosterTypeTitle = RosterTitle;
            dr.StartTime = StartTime;
            dr.EndTime = EndTime;
            dr.Note = Note;
            dr.ColorCode = (objRt.ColorCode != "" ? objRt.ColorCode : ServiceFacade.SettingsHelper.UncompleteColor);
            dr.UpdateUser = username;
            dr.UpdateDate = DateTime.Now;

            DataRepository.DoctorRosterProvider.Save(tm, dr);

            // Check if start time >= end time
            if (dr.StartTime >= dr.EndTime)
            {
                tm.Rollback();
                result = @"[{ 'result': 'false', 'message': 'To date must be greater than From date.', 'data': [] }]";
                goto StepResult;
            }

            // If roster is created in a passed or current day
            if (Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd")) >= Convert.ToDateTime(dr.StartTime.ToString("yyyy/MM/dd")))
            {
                tm.Rollback();
                result = @"[{ 'result': 'false', 'message': 'You can not change roster to passed or current date.', 'data': [] }]";
                goto StepResult;
            }

            // Check roster before insert new roster
            string query = string.Format("DoctorUserName = '{0}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'", obj.UserName, dr.EndTime, dr.StartTime);
            TList<DoctorRoster> lstRoster = DataRepository.DoctorRosterProvider.GetPaged(tm, query, "Id desc", 0, 1, out Count);
            // If there is no roster -> insert new roster
            if (Count > 1)
            {
                tm.Rollback();
                result = @"[{ 'result': 'false', 'message': 'There is roster conflicted: From "
                    + dr.StartTime.DayOfWeek.ToString() + " " + dr.StartTime.ToString("dd MMM yyyy HH:mm") + " to  "
                    + dr.EndTime.DayOfWeek.ToString() + " " + dr.EndTime.ToString("dd MMM yyyy HH:mm") + "', 'data': [] }]";
                goto StepResult;
            }

            result = @"[{ 'result': 'true', 'message': '', 'data': ["
                + @"{id: '" + dr.Id + @"',"
                + @"start_date: '" + dr.StartTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                + @"end_date: '" + dr.EndTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                + @"section_id: '" + dr.DoctorUserName + @"',"
                + @"text: '" + dr.RosterTypeTitle + @"<br />Doctor: " + dr.DoctorUserName + @"<br />";

            if (!string.IsNullOrEmpty(dr.Note))
                result += dr.Note;

            result += @"',DoctorUserName: '" + dr.DoctorUserName + @"',"
                + @"DoctorShortName: '" + dr.DoctorShortName + @"',"
                + @"RosterTypeId: '" + dr.RosterTypeId + @"',"
                + @"RosterTypeTitle: '" + dr.RosterTypeTitle + @"',"
                + @"note: '" + dr.Note + @"',"
                + @"color: '" + dr.ColorCode + @"',"
                + @"isnew: 'false'"
                + @"}"
                + @"] }]";
            tm.Commit();
        }
        catch (Exception ex)
        {
            //Write log cho nay
            tm.Rollback();

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            return result;
        }

    StepResult:
        return result;
    }
    #endregion

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string LoadRoster(string Mode, string CurrentDateView)
    {
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data:  [{
         *              Id: id of roster,
         *              DoctorUserName: Doctor UserName,
         *              DoctorShortName: Doctor ShortName,
         *              RosterTypeId: Roster Type Id,
         *              RosterTypeTitle: Roster Type Title,
         *              start_date: start date,
         *              end_date: end date,
         *              note: note,
         *              text: roster type + note,
         *              color: color of event [get from status],
         *              isnew: true
         *          }, {}, {}]
         *  }
         */

        string result = string.Empty;
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            tm.BeginTransaction();

            string username = EntitiesUtilities.GetAuthName();
            Staff obj = DataRepository.StaffProvider.GetByUserNameIsDisabled(username, false);
            if (obj == null)
            {
                result = @"[{ 'result': 'false', 'message': 'Doctor is not exist.', 'data': '[]' }]";
                goto StepResult;
            }

            TList<DoctorRoster> lstObj = DataRepository.DoctorRosterProvider.GetByIsDisabled(tm, false);

            string color = string.Empty;
            foreach (DoctorRoster item in lstObj)
            {
                result += @"{id: '" + item.Id + @"',"
                    + @"start_date: '" + item.StartTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"end_date: '" + item.EndTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"section_id: '" + item.DoctorUserName + @"',"
                    + @"text: '" + item.RosterTypeTitle + @"<br />Doctor: " + item.DoctorUserName + @"<br />";

                if (!string.IsNullOrEmpty(item.Note))
                    result += item.Note;

                result += @"',DoctorUserName: '" + item.DoctorUserName + @"',"
                    + @"DoctorShortName: '" + item.DoctorShortName + @"',"
                    + @"RosterTypeId: '" + item.RosterTypeId + @"',"
                    + @"RosterTypeTitle: '" + item.RosterTypeTitle + @"',"
                    + @"RosterTypeId: '" + item.RosterTypeId + @"',"
                    + @"note: '" + item.Note + @"',";

                // If roster is passed, set color code and is completed
                if (item.StartTime <= DateTime.Now)
                {
                    item.ColorCode = ServiceFacade.SettingsHelper.CompleteColor;
                    item.IsComplete = true;
                    DataRepository.DoctorRosterProvider.Save(tm, item);

                    result += @"readonly: true,";
                }
                result += @"color: '" + item.ColorCode + @"',";
                result += @"isnew: 'false'},";
            }
            if (result.Length > 1)
            {
                result = @"[{ 'result': 'true', 'message': '', 'data': "
                    + "[" + result.Substring(0, result.Length - 1) + "]"
                    + @" }]";
            }
            else
            {
                result = @"[{ 'result': 'true', 'message': '' }]";
            }

            tm.Commit();
        }
        catch (Exception ex)
        {
            //Write log cho nay
            tm.Rollback();

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': '[]' }]";
            goto StepResult;
        }

    StepResult:
        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string DeleteEvent(string Id)
    {
        string result = string.Empty;

        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            tm.BeginTransaction();

            string username = EntitiesUtilities.GetAuthName();

            DoctorRoster dr = DataRepository.DoctorRosterProvider.GetByIdIsCompleteIsDisabled(Id, false, false);
            if (dr == null)
            {
                result = @"[{ 'result': 'false', 'message': 'There is no roster to delete or roster is expired.', 'data': [] }]";
                return result;
            }

            dr.IsDisabled = true;
            dr.UpdateUser = username;
            dr.UpdateDate = DateTime.Now;

            DataRepository.DoctorRosterProvider.Save(tm, dr);

            result = @"[{ 'result': 'true', 'message': '', 'data': [] }]";
            tm.Commit();
        }
        catch (Exception ex)
        {
            //Write log cho nay
            tm.Rollback();

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            return result;
        }

    StepResult:
        return result;
    }
    #endregion

    #region "Function"
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetPatient()
    {
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data:  [{
         *          key: name of roles,
         *          label: name of roles,
         *          open: true, [priority for Doctor]
         *          children:  [{
         *                      key: Staff Id,
         *                      label: Staff Name,
         *                      }, {}, {}]
         *          }, {}, {}]
         *  }]
         */
        string result = string.Empty;

        try
        {
            
            TList<Customer> lst = DataRepository.CustomerProvider.GetByIsDisabled(false);

            // If there is no functionality, return empty data
            if (lst.Count == 0)
            {
                result = @"[{ 'result': 'true', 'message': 'There is no group.', 'patientName': [], 'patientId': [] }]";
                goto StepResult;
            }

            result = string.Empty;
            lst.Sort("FirstName ASC");
            foreach (Customer item in lst)
            {
                result += @"{'key' : '" + item.Id + @"'" + "," + @"'label' : '" + String.Format("{3} | {2}. {0} {1}", item.FirstName, item.LastName, item.Title, item.Id) + @"'" + "},";
            }
            result = result.Substring(0, result.Length - 1);

            result = @"[{ 'result': 'true', 'message': '', 'data':[" + result + @"] }]";
            goto StepResult;
        }
        catch (Exception ex)
        {
            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            goto StepResult;
        }
    StepResult:
        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetTime()
    {
        int step = ServiceFacade.SettingsHelper.MinuteStep;
        int minute = ServiceFacade.SettingsHelper.MaxMinute;
        int hour = ServiceFacade.SettingsHelper.MaxHour;

        TList<RosterType> lst = DataRepository.RosterTypeProvider.GetByIsDisabled(false);

        string result = string.Empty;
        int m = 0;
        string key = string.Empty;
        string label = string.Empty;

        for (int h = 0; h < hour; h++)
        {
            m = 0;
            while (m < minute)
            {
                key = h.ToString("00") + ":" + m.ToString("00") + ":00";
                label = h.ToString("00") + ":" + m.ToString("00");
                result += @"{'key' : '" + key + @"'" + "," + @"'label' : '" + label + @"'" + "},";
                m += step;
            }
        }
        if (result.Length > 1)
            result = "[" + result.Substring(0, result.Length - 1) + "]";

        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetDoctorTree()
    {
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data:  [{
         *          key: name of roles,
         *          label: name of roles,
         *          open: true, [priority for Doctor]
         *          children:  [{
         *                      key: Staff Id,
         *                      label: Staff Name
         *                      }, {}, {}]
         *          }, {}, {}]
         *  }]
         */
        string result = string.Empty;

        try
        {
            // Get all functionalities
            TList<Functionality> lstFunc = DataRepository.FunctionalityProvider.GetByIsDisabled(false);

            // If there is no functionality, return empty data
            if (lstFunc.Count == 0)
            {
                result = @"[{ 'result': 'false', 'message': 'There is no group. Please contact Administrator.', 'data': [] }]";
                goto StepResult;
            }

            // Get all available staffs
            TList<DoctorFunc> lstDoctorFunc = DataRepository.DoctorFuncProvider.GetByIsDisabled(false);
            bool hasItem = false;
            lstDoctorFunc.Sort("DoctorShortName ASC");

            foreach (Functionality objFunc in lstFunc)
            {
                result += @"{'key' : '" + objFunc.Id + @"'" + "," + @"'label' : '" + objFunc.Title + @"', open: false, children: [";
                hasItem = false;

                // Get staff by functionality then remove it from list
                for (int j = 0; j < lstDoctorFunc.Count; j++)
                {
                    if (lstDoctorFunc[j].FuncId == objFunc.Id)
                    {
                        result += @"{'key' : '" + lstDoctorFunc[j].DoctorUserName + @"'" + "," + @"'label' : '"
                            + lstDoctorFunc[j].DoctorShortName + @"'" + "},";
                        lstDoctorFunc.RemoveAt(j);
                        j--;
                        hasItem = true;
                    }
                }

                if (hasItem)
                    result = result.Substring(0, result.Length - 1);
                result += @"]},";
            }
            if (result.Length > 1)
                result = result.Substring(0, result.Length - 1);

            result = @"[{ 'result': 'true', 'message': '', 'data':[" + result + @"] }]";
            goto StepResult;
        }
        catch (Exception ex)
        {
            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            goto StepResult;
        }
    StepResult:
        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetStaffs(string FuncId, string StartTime, string EndTime, string StartDate, string EndDate)
    {
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data:  [{
         *          key: name of roles,
         *          label: name of roles,
         *          }, {}, {}]
         *  }]
         */
        string result = string.Empty;

        try
        {
            // Get start time and end time
            int intStartHour = Convert.ToInt32(StartTime.Split(':')[0]);
            int intStartMinute = Convert.ToInt32(StartTime.Split(':')[1]);
            int intEndHour = Convert.ToInt32(EndTime.Split(':')[0]);
            int intEndMinute = Convert.ToInt32(EndTime.Split(':')[1]);

            // Get start date and end date
            DateTime dtStart = Convert.ToDateTime(StartDate);
            DateTime dtEnd = Convert.ToDateTime(EndDate);

            dtStart = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, intStartHour, intStartMinute, 0);
            dtEnd = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, intEndHour, intEndMinute, 0);
            
            string[] arr = FuncId.Split(chrSeperateStaff);

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetAvailableStaffsForAppointment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@FuncId", Convert.ToInt64(arr[0])));
            cmd.Parameters.Add(new SqlParameter("@StartTime", dtStart));
            cmd.Parameters.Add(new SqlParameter("@EndTime", dtEnd));
            cmd.CommandTimeout = 0;

            ds = DataRepository.Provider.ExecuteDataSet(cmd);
            DataTable objTable = ds.Tables[0];

            if (objTable.Rows.Count == 0)
            {
                result = @"[{ 'result': 'true', 'message': 'There is no doctor. Please contact Administrator.', 'data': [{'key': '-1', 'label': 'No Doctor'] }]";
                goto StepResult;
            }

            foreach (DataRow dr in objTable.Rows)
            {
                result += @"{'key' : '" + dr["DoctorUserName"].ToString() + @"'" + "," + @"'label' : '" + dr["DoctorShortName"].ToString();

                if (dr["IsDisabled"].ToString() == "True")
                {
                    result += strSeperateStaff + @"Not Available', ";
                    result += @"'property' : 'disabled=""disabled""'";
                }
                else
                {
                    result += strSeperateStaff + @"Available', ";
                    result += @"'property' : ''";
                }

                result += @"},";
            }
            
            if (result.Length > 1)
                result = result.Substring(0, result.Length - 1);

            result = @"[{ 'result': 'true', 'message': '', 'data':[" + result + @"] }]";
            goto StepResult;
        }
        catch (Exception ex)
        {
            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            goto StepResult;
        }
    StepResult:
        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetContents()
    {
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data:  [{
         *          key: name of roles,
         *          label: name of roles,
         *          }, {}, {}]
         *  }]
         */
        string result = string.Empty;

        try
        {
            TList<Content> lstObj = DataRepository.ContentProvider.GetByIsDisabled(false);

            if (lstObj.Count == 0)
            {
                result = @"[{ 'result': 'true', 'message': 'There is no content. Please contact Administrator.', 'data': [] }]";
                goto StepResult;
            }

            lstObj.Sort("FuncTitle ASC, Title ASC");
            foreach (Content item in lstObj)
            {
                result += @"{'key' : '" + item.FuncId + chrSeperateStaff.ToString() + item.Id.ToString() + @"'" + "," + @"'label' : '" 
                    + item.FuncTitle + strSeperateStaff + item.Title + @"'},";
            }

            if (result.Length > 1)
                result = result.Substring(0, result.Length - 1);

            result = @"[{ 'result': 'true', 'message': '', 'data':[" + result + @"] }]";
            goto StepResult;
        }
        catch (Exception ex)
        {
            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            goto StepResult;
        }
    StepResult:
        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetRooms(string DoctorUserName, string FuncId)
    {
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data:  [{
         *          key: name of roles,
         *          label: name of roles,
         *          }, {}, {}]
         *  }]
         */
        string result = string.Empty;

        try
        {
            string[] arr = FuncId.Split(chrSeperateStaff);

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetRoomByDoctorUserNameByIsDisabledWithOrderPriority";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DoctorUserName", DoctorUserName));
            cmd.Parameters.Add(new SqlParameter("@FuncId", Convert.ToInt64(arr[0])));
            cmd.Parameters.Add(new SqlParameter("@IsDisabled", false));
            cmd.CommandTimeout = 0;

            ds = DataRepository.Provider.ExecuteDataSet(cmd);
            DataTable objTable = ds.Tables[0];

            if (objTable.Rows.Count == 0)
            {
                result = @"[{ 'result': 'true', 'message': 'There is no room. Please contact Administrator.', 'data': [{'key': '-1', 'label': 'No Room'] }]";
                goto StepResult;
            }

            foreach (DataRow dr in objTable.Rows)
            {
                result += @"{'key' : '" + dr["Id"].ToString() + @"'" + "," + @"'label' : '" + dr["Title"].ToString() + @"'" + "},";
            }

            if (result.Length > 1)
                result = result.Substring(0, result.Length - 1);

            result = @"[{ 'result': 'true', 'message': '', 'data':[" + result + @"] }]";
            goto StepResult;
        }
        catch (Exception ex)
        {
            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            goto StepResult;
        }
    StepResult:
        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetCurrentContents(string DoctorUserName)
    {
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data: string content id
         *  }
         */
        string result = string.Empty;

        try
        {
            TList<DoctorFunc> lstDrFu = DataRepository.DoctorFuncProvider.GetByDoctorUserNameIsDisabled(DoctorUserName, false);
            TList<Content> lstObj = DataRepository.ContentProvider.GetByIsDisabled(false);

            if (lstObj.Count == 0)
            {
                result = @"[{ 'result': 'true', 'message': 'There is no content. Please contact Administrator.', 'data': [] }]";
                goto StepResult;
            }

            // Lay dong dau tien
            lstObj.Sort("FuncTitle ASC, Title ASC");
            foreach (Content item in lstObj)
            {
                foreach (DoctorFunc itemDr in lstDrFu)
                {
                    if (item.FuncId == itemDr.FuncId)
                    {
                        result = item.FuncId + chrSeperateStaff.ToString() + item.Id.ToString();
                        goto StepString;
                    }
                }
            }

        StepString:
            result = @"[{ 'result': 'true', 'message': '', 'data':['" + result + @"'] }]";
            goto StepResult;
        }
        catch (Exception ex)
        {
            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            goto StepResult;
        }
    StepResult:
        return result;
    }
    #endregion
}
