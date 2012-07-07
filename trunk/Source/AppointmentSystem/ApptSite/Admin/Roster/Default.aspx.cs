using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentBusiness.BO;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;

public partial class Admin_Roster_Default : System.Web.UI.Page
{
    static string strSeperateStaff = " | ";

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
        string message = string.Empty;

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

            #region Validate current user
            string username = EntitiesUtilities.GetAuthName();
            Users objUser;
            if (!BoFactory.UserBO.ValidateCurrentUser(username, out objUser, out message))
            {
                result = @"[{ 'result': 'false', 'message': '" + message + "', 'data': '[]' }]";
                goto StepResult;
            }
            #endregion

            // Get Roster Type
            RosterType objRt = DataRepository.RosterTypeProvider.GetById(Convert.ToInt32(RosterTypeId));
            if (objRt == null || objRt.IsDisabled)
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
                string Perfix = ServiceFacade.SettingsHelper.RosterPrefix + DateTime.Now.ToString("yyMMdd");
                int Count = 0;
                string strNumber = "000";
                TList<Roster> objPo = DataRepository.RosterProvider.GetPaged(tm, "Id like '" + Perfix + "' + '%'", "Id desc", 0, 1, out Count);
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
                        var newObj = new Roster();
                        strNumber = String.Format("{0:000}", int.Parse(strNumber) + 1);

                        newObj.Id = Perfix + strNumber;

                        newObj.DoctorId = objUser.Id;
                        newObj.RosterTypeId = Convert.ToInt32(RosterTypeId);
                        newObj.StartTime = new DateTime(item.Year, item.Month, item.Day, intStartHour, intStartMinute, 0);
                        newObj.EndTime = new DateTime(item.Year, item.Month, item.Day, intEndHour, intEndMinute, 0);
                        newObj.Note = Note;
                        newObj.CreateUser = username;
                        newObj.UpdateUser = username;

                        DataRepository.RosterProvider.Insert(tm, newObj);

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
                        string query = string.Format("DoctorId = {0} AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'", objUser.Id, newObj.EndTime, newObj.StartTime);
                        TList<Roster> lstRoster = DataRepository.RosterProvider.GetPaged(tm, query, "Id desc", 0, 1, out Count);
                        // If there is no roster -> insert new roster
                        if (Count > 1)
                        {
                            errorMessage += newObj.StartTime.DayOfWeek.ToString() + " " + newObj.StartTime.ToString("dd MMM yyyy") + ", ";
                        }

                        DataRepository.RosterProvider.DeepLoad(newObj);
                        result += @"{id: '" + newObj.Id + @"',"
                            + @"start_date: '" + newObj.StartTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                            + @"end_date: '" + newObj.EndTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                            + @"section_id: '" + newObj.DoctorId + @"',"
                            + @"text: '" + newObj.RosterTypeIdSource.Title + @"<br />Doctor: " + newObj.DoctorIdSource.Username + @"<br />";

                        if (!string.IsNullOrEmpty(newObj.Note))
                            result += newObj.Note;

                        result += @"',DoctorUserName: '" + newObj.DoctorIdSource.Username + @"',"
                           + @"DoctorShortName: '" + newObj.DoctorIdSource.DisplayName + @"',"
                           + @"RosterTypeId: '" + newObj.RosterTypeId + @"',"
                           + @"RosterTypeTitle: '" + newObj.RosterTypeIdSource.Title + @"',"
                           + @"note: '" + newObj.Note + @"',"
                            //+ @"color: '" + newObj.ColorCode + @"',"
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
                Roster newObj = new Roster();

                // Get start date and end date
                DateTime dtStart = Convert.ToDateTime(StartDate);
                DateTime dtEnd = Convert.ToDateTime(EndDate);

                string Perfix = ServiceFacade.SettingsHelper.RosterPrefix + DateTime.Now.ToString("yyMMdd");
                int Count = 0;
                TList<Roster> objPo = DataRepository.RosterProvider.GetPaged(tm, "Id like '" + Perfix + "' + '%'", "Id desc", 0, 1, out Count);
                if (Count == 0)
                    newObj.Id = Perfix + "001";
                else
                {
                    newObj.Id = Perfix + String.Format("{0:000}", int.Parse(objPo[0].Id.Substring(objPo[0].Id.Length - 3)) + 1);
                }

                newObj.DoctorId = objUser.Id;
                newObj.RosterTypeId = Convert.ToInt32(RosterTypeId);
                newObj.StartTime = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, intStartHour, intStartMinute, 0);
                newObj.EndTime = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, intEndHour, intEndMinute, 0);
                newObj.Note = Note;
                newObj.CreateUser = username;
                newObj.UpdateUser = username;

                DataRepository.RosterProvider.Insert(tm, newObj);

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
                string query = string.Format("DoctorId = {0} AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'", objUser.Id, newObj.EndTime, newObj.StartTime);
                TList<Roster> lstRoster = DataRepository.RosterProvider.GetPaged(tm, query, "Id desc", 0, 1, out Count);
                // If there is no roster -> insert new roster
                if (Count > 1)
                {
                    tm.Rollback();
                    result = @"[{ 'result': 'false', 'message': 'There is roster conflicted: From "
                        + newObj.StartTime.DayOfWeek.ToString() + " " + newObj.StartTime.ToString("dd MMM yyyy HH:mm") + " to  "
                        + newObj.EndTime.DayOfWeek.ToString() + " " + newObj.EndTime.ToString("dd MMM yyyy HH:mm") + "', 'data': [] }]";
                    goto StepResult;
                }

                DataRepository.RosterProvider.DeepLoad(newObj);
                result = @"[{ 'result': 'true', 'message': '', 'data': ["
                    + @"{id: '" + newObj.Id + @"',"
                    + @"start_date: '" + newObj.StartTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"end_date: '" + newObj.EndTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"section_id: '" + newObj.DoctorId + @"',"
                    + @"text: '" + newObj.RosterTypeIdSource.Title + @"<br />Doctor: " + newObj.DoctorIdSource.Username + @"<br />";

                if (!string.IsNullOrEmpty(newObj.Note))
                    result += newObj.Note;

                result += @"',DoctorUserName: '" + newObj.DoctorIdSource.Username + @"',"
                    + @"DoctorShortName: '" + newObj.DoctorIdSource.DisplayName + @"',"
                    + @"RosterTypeId: '" + newObj.RosterTypeId + @"',"
                    + @"RosterTypeTitle: '" + newObj.RosterTypeIdSource.Title + @"',"
                    + @"note: '" + newObj.Note + @"',"
                    //+ @"color: '" + newObj.ColorCode + @"',"
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
        string message = string.Empty;

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
            Users objUser;
            if (!BoFactory.UserBO.ValidateCurrentUser(username, out objUser, out message))
            {
                result = @"[{ 'result': 'false', 'message': '" + message + "', 'data': '[]' }]";
                goto StepResult;
            }

            Roster dr = DataRepository.RosterProvider.GetById(Id);
            if (dr == null || dr.IsDisabled)
            {
                result = @"[{ 'result': 'false', 'message': 'There is no roster to update or the roster is expired.', 'data': [] }]";
                goto StepResult;
            }

            // Get Roster Type
            RosterType objRt = DataRepository.RosterTypeProvider.GetByIdIsDisabled(Convert.ToInt32(RosterTypeId), false);
            if (objRt == null)
            {
                result = @"[{ 'result': 'false', 'message': 'Roster Type is not exist.', 'data': '[]' }]";
                goto StepResult;
            }

            dr.DoctorId = objUser.Id;
            dr.RosterTypeId = Convert.ToInt32(RosterTypeId);
            dr.StartTime = StartTime;
            dr.EndTime = EndTime;
            dr.Note = Note;
            dr.UpdateUser = username;
            dr.UpdateDate = DateTime.Now;

            DataRepository.RosterProvider.Save(tm, dr);

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
            string query = string.Format("DoctorId = {0} AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'", objUser.Id, dr.EndTime, dr.StartTime);
            TList<Roster> lstRoster = DataRepository.RosterProvider.GetPaged(tm, query, "Id desc", 0, 1, out Count);
            // If there is no roster -> insert new roster
            if (Count > 1)
            {
                tm.Rollback();
                result = @"[{ 'result': 'false', 'message': 'There is roster conflicted: From "
                    + dr.StartTime.DayOfWeek.ToString() + " " + dr.StartTime.ToString("dd MMM yyyy HH:mm") + " to  "
                    + dr.EndTime.DayOfWeek.ToString() + " " + dr.EndTime.ToString("dd MMM yyyy HH:mm") + "', 'data': [] }]";
                goto StepResult;
            }

            DataRepository.RosterProvider.DeepLoad(dr);
            result = @"[{ 'result': 'true', 'message': '', 'data': ["
                + @"{id: '" + dr.Id + @"',"
                + @"start_date: '" + dr.StartTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                + @"end_date: '" + dr.EndTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                + @"section_id: '" + dr.DoctorId + @"',"
                + @"text: '" + dr.RosterTypeIdSource.Title + @"<br />Doctor: " + dr.DoctorIdSource.Username + @"<br />";

            if (!string.IsNullOrEmpty(dr.Note))
                result += dr.Note;

            result += @"',DoctorUserName: '" + dr.DoctorIdSource.Username + @"',"
                + @"DoctorShortName: '" + dr.DoctorIdSource.DisplayName + @"',"
                + @"RosterTypeId: '" + dr.RosterTypeId + @"',"
                + @"RosterTypeTitle: '" + dr.RosterTypeIdSource.Title + @"',"
                + @"note: '" + dr.Note + @"',"
                //+ @"color: '" + newObj.ColorCode + @"',"
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
        string message = string.Empty;
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            tm.BeginTransaction();

            string username = EntitiesUtilities.GetAuthName();
            Users objUser;
            if (!BoFactory.UserBO.ValidateCurrentUser(username, out objUser, out message))
            {
                result = @"[{ 'result': 'false', 'message': '" + message + "', 'data': '[]' }]";
                goto StepResult;
            }

            int count;
            var lstObj = DataRepository.RosterProvider.GetPaged("IsDisabled = 'False'", string.Empty, 0, 10000, out count);
            DataRepository.RosterProvider.DeepLoad(lstObj);

            string color = string.Empty;
            foreach (Roster item in lstObj)
            {
                result += @"{id: '" + item.Id + @"',"
                    + @"start_date: '" + item.StartTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"end_date: '" + item.EndTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"section_id: '" + item.DoctorId + @"',"
                    + @"text: '" + item.RosterTypeIdSource.Title + @"<br />Doctor: " + item.DoctorIdSource.Username + @"<br />";

                if (!string.IsNullOrEmpty(item.Note))
                    result += item.Note;

                result += @"',DoctorUserName: '" + item.DoctorIdSource.Username + @"',"
                          + @"DoctorShortName: '" + item.DoctorIdSource.DisplayName + @"',"
                          + @"RosterTypeId: '" + item.RosterTypeId + @"',"
                          + @"RosterTypeTitle: '" + item.RosterTypeIdSource.Title + @"',"
                          + @"note: '" + item.Note + @"',";

                // If roster is passed, set color code and is completed
                //if (item.StartTime <= DateTime.Now)
                //{
                //    item.ColorCode = ServiceFacade.SettingsHelper.CompleteColor;
                //    item.IsComplete = true;
                //    DataRepository.DoctorRosterProvider.Save(tm, item);

                //    result += @"readonly: true,";
                //}
                //result += @"color: '" + item.ColorCode + @"',";
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

            Roster dr = DataRepository.RosterProvider.GetById(Id);
            if (dr == null || dr.IsDisabled)
            {
                result = @"[{ 'result': 'false', 'message': 'There is no roster to delete or roster is expired.', 'data': [] }]";
                return result;
            }

            dr.IsDisabled = true;
            dr.UpdateUser = username;
            dr.UpdateDate = DateTime.Now;

            DataRepository.RosterProvider.Save(tm, dr);

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
    public static string GetRosterType()
    {
            int count;
            TList<RosterType> lst = DataRepository.RosterTypeProvider.GetPaged("IsDisabled = 'False'", string.Empty, 0, 10000, out count);

        string result = "[]";
        if (lst.Count > 0)
        {
            result = string.Empty;
            foreach (RosterType item in lst)
            {
                result += @"{'key' : '" + item.Id + @"'" + "," + @"'label' : '" + ((item.Title == null) ? "" : item.Title) + @"'" + "},";
            }
            result = "[" + result.Substring(0, result.Length - 1) + "]";
        }

        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetTime()
    {
        int step = ServiceFacade.SettingsHelper.MinuteStep;
        int minute = ServiceFacade.SettingsHelper.MaxMinute;
        int hour = ServiceFacade.SettingsHelper.MaxHour;

        int count;
        var lst = DataRepository.RosterProvider.GetPaged("IsDisabled = 'False'", string.Empty, 0, 10000, out count);

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
            int count;
            var lstService = DataRepository.ServicesProvider.GetPaged("IsDisabled = 'False'", string.Empty, 0, 10000, out count);

            // If there is no functionality, return empty data
            if (lstService.Count == 0)
            {
                result = @"[{ 'result': 'false', 'message': 'There is no group. Please contact Administrator.', 'data': [] }]";
                goto StepResult;
            }

            // Get all available staffs
            TList<DoctorService> lstDoctorService = DataRepository.DoctorServiceProvider.GetPaged("IsDisabled = 'False'", string.Empty, 0, 10000, out count);
            DataRepository.DoctorServiceProvider.DeepLoad(lstDoctorService);
            bool hasItem = false;
            //lstDoctorService.Sort("DoctorShortName ASC");

            foreach (Services item in lstService)
            {
                result += @"{'key' : '" + item.Id + @"'" + "," + @"'label' : '" + item.Title + @"', open: true, children: [";
                hasItem = false;

                // Get staff by functionality then remove it from list
                for (int j = 0; j < lstDoctorService.Count; j++)
                {
                    if (lstDoctorService[j].ServiceId == item.Id)
                    {
                        result += @"{'key' : '" + lstDoctorService[j].DoctorId + @"'" + "," + @"'label' : '"
                            + lstDoctorService[j].DoctorIdSource.DisplayName + @"'" + "},";
                        lstDoctorService.RemoveAt(j);
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
    public static string GetStaffs()
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
            // Get all available staffs
            int count;
            TList<DoctorService> lstDoctorService = DataRepository.DoctorServiceProvider.GetPaged("IsDisabled = 'False'", string.Empty, 0, 10000, out count);
            DataRepository.DoctorServiceProvider.DeepLoad(lstDoctorService);

            // If there is no roles, return empty data
            if (lstDoctorService.Count == 0)
            {
                result = @"[{ 'result': 'true', 'message': 'There is no group or no doctor. Please contact Administrator.', 'data': [] }]";
                goto StepResult;
            }

            //lstDoctorService.Sort("FuncTitle ASC, DoctorShortName ASC");
            foreach (var item in lstDoctorService)
            {
                result += @"{'key' : '" + item.DoctorId + @"'" + "," + @"'label' : '" + item.ServiceIdSource.Title + strSeperateStaff + item.DoctorIdSource.DisplayName + @"'},";
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
    #endregion
}
