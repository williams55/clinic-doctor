using System;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web;
using ClinicDoctor.Data;
using ClinicDoctor.Entities;
using ClinicDoctor.Settings.BusinessLayer;
using System.Globalization;

public partial class Admin_Doctor_RosterIframe : System.Web.UI.Page
{
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetRosterType()
    {
        TList<RosterType> lst = DataRepository.RosterTypeProvider.GetByIsDisabled(false);

        string result = "[]";
        if (lst.Count > 0)
        {
            result = string.Empty;
            foreach (RosterType item in lst)
            {
                result += @"{'key' : '" + item.Id + @"'" + "," + @"'label' : '" + ((item.Note == null) ? "" : item.Note) + @"'" + "},";
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
    public static string SaveEvent(string RosterType, string StartTime, string EndTime, string Note, string RepeatRoster, string Weekday, string Month)
    {
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data:  [{
         *              Id: id of roster,
         *              DoctorId: Doctor Id,
         *              RosterTypeId: Roster Type Id,
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
                return result;
            }

            // Repeat roster
            if (RepeatRoster == "true")
            {
                // Get RoosterType
                RosterType refObj = DataRepository.RosterTypeProvider.GetByIdIsDisabled(tm, Convert.ToInt64(RosterType), false);

                // Convert Month to right format
                //DateTime month = Convert.ToDateTime(Month, new CultureInfo("vi-VN"));
                DateTime month = Convert.ToDateTime(Month);
                DateTime item = new DateTime(month.Year, month.Month, 1);

                // Get start time and end time
                //DateTime dtStart = Convert.ToDateTime(StartTime, new CultureInfo("vi-VN"));
                //DateTime dtEnd = Convert.ToDateTime(EndTime, new CultureInfo("vi-VN"));
                DateTime dtStart = Convert.ToDateTime(StartTime);
                DateTime dtEnd = Convert.ToDateTime(EndTime);

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

                        newObj.DoctorId = obj.Id;
                        newObj.RosterTypeId = Convert.ToInt64(RosterType);
                        newObj.StartTime = Convert.ToDateTime(item.ToString("MM/dd/yyyy ") + dtStart.ToString("HH:mm:00"));
                        newObj.EndTime = Convert.ToDateTime(item.ToString("MM/dd/yyyy ") + dtEnd.ToString("HH:mm:00"));
                        newObj.Note = Note;
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
                        if (Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(newObj.StartTime.ToString("MM/dd/yyyy")))
                        {
                            tm.Rollback();
                            result = @"[{ 'result': 'false', 'message': 'You can not change roster to passed or current date.', 'data': [] }]";
                            goto StepResult;
                        }

                        // Check roster before insert new roster
                        string query = string.Format("DoctorId = '{0}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'", obj.Id.ToString(), newObj.EndTime, newObj.StartTime);
                        TList<DoctorRoster> lstRoster = DataRepository.DoctorRosterProvider.GetPaged(tm, query, "Id desc", 0, 1, out Count);
                        // If there is no roster -> insert new roster
                        if (Count > 1)
                        {
                            errorMessage += newObj.StartTime.DayOfWeek.ToString() + " " + newObj.StartTime.ToString("dd MMM yyyy") + ", ";
                        }

                        result += @"{id: '" + newObj.Id + @"',"
                            + @"start_date: '" + newObj.StartTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                            + @"end_date: '" + newObj.EndTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                            + @"text: '" + refObj.Note + @"<br />";

                        if (!string.IsNullOrEmpty(newObj.Note))
                            result += newObj.Note;

                        result += @"',DoctorId: '" + newObj.DoctorId + @"',"
                           + @"RosterTypeId: '" + newObj.RosterTypeId + @"',"
                           + @"note: '" + newObj.Note + @"',"
                           + @"color: '" + ServiceFacade.SettingsHelper.UncompleteColor + @"',"
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
                RosterType refObj = DataRepository.RosterTypeProvider.GetByIdIsDisabled(tm, Convert.ToInt64(RosterType), false);
                DoctorRoster newObj = new DoctorRoster();

                string Perfix = "R" + ServiceFacade.SettingsHelper.RosterPrefix + DateTime.Now.ToString("yyMMdd");
                int Count = 0;
                TList<DoctorRoster> objPo = DataRepository.DoctorRosterProvider.GetPaged(tm, "Id like '" + Perfix + "' + '%'", "Id desc", 0, 1, out Count);
                if (Count == 0)
                    newObj.Id = Perfix + "001";
                else
                {
                    newObj.Id = Perfix + String.Format("{0:000}", int.Parse(objPo[0].Id.Substring(objPo[0].Id.Length - 3)) + 1);
                }

                newObj.DoctorId = obj.Id;
                newObj.RosterTypeId = Convert.ToInt64(RosterType);
                newObj.StartTime = Convert.ToDateTime(StartTime);
                newObj.EndTime = Convert.ToDateTime(EndTime);
                //newObj.StartTime = Convert.ToDateTime(StartTime, new CultureInfo("vi-VN"));
                //newObj.EndTime = Convert.ToDateTime(EndTime, new CultureInfo("vi-VN"));
                newObj.Note = Note;
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
                if (Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd")) >= Convert.ToDateTime(newObj.StartTime.ToString("yyyy/MM/dd")))
                {
                    tm.Rollback();
                    result = @"[{ 'result': 'false', 'message': 'You can not change roster to passed or current date.', 'data': [] }]";
                    goto StepResult;
                }

                // Check roster before insert new roster
                string query = string.Format("DoctorId = '{0}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'", obj.Id.ToString(), newObj.EndTime, newObj.StartTime);
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
                    + @"text: '" + refObj.Note + @"<br />";

                if (!string.IsNullOrEmpty(newObj.Note))
                    result += newObj.Note;

                result += @"',DoctorId: '" + newObj.DoctorId + @"',"
                   + @"RosterTypeId: '" + newObj.RosterTypeId + @"',"
                   + @"note: '" + newObj.Note + @"',"
                   + @"color: '" + ServiceFacade.SettingsHelper.UncompleteColor + @"',"
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
            return result;
        }

    StepResult:
        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string UpdateEvent(string Id, string RosterType, string StartTime, string EndTime, string Note)
    {
        string result = string.Empty;
        int Count = 0;
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            tm.BeginTransaction();

            string username = EntitiesUtilities.GetAuthName();
            Staff obj = DataRepository.StaffProvider.GetByUserNameIsDisabled(username, false);
            if (obj == null)
            {
                result = @"[{ 'result': 'false', 'message': 'Doctor is not exist.', 'data': [] }]";
                return result;
            }

            RosterType refObj = DataRepository.RosterTypeProvider.GetByIdIsDisabled(tm, Convert.ToInt64(RosterType), false);
            DoctorRoster dr = DataRepository.DoctorRosterProvider.GetByIdIsCompleteIsDisabled(Id, false, false);
            if (dr == null)
            {
                result = @"[{ 'result': 'false', 'message': 'There is no roster to update or the roster is expired.', 'data': [] }]";
                return result;
            }

            dr.RosterTypeId = Convert.ToInt64(RosterType);
            dr.StartTime = Convert.ToDateTime(StartTime);
            dr.EndTime = Convert.ToDateTime(EndTime);
            dr.Note = Note;
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
            string query = string.Format("DoctorId = '{0}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'", obj.Id.ToString(), dr.EndTime, dr.StartTime);
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
                + @"text: '" + refObj.Note + @"<br />";

            if (!string.IsNullOrEmpty(dr.Note))
                result += dr.Note;

            result += @"',DoctorId: '" + dr.DoctorId + @"',"
               + @"RosterTypeId: '" + dr.RosterTypeId + @"',"
               + @"note: '" + dr.Note + @"',"
               + @"color: '" + ServiceFacade.SettingsHelper.UncompleteColor + @"',"
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string LoadRoster(string Mode, string CurrentDateView)
    {
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data:  [{
         *              Id: id of roster,
         *              DoctorId: Doctor Id,
         *              RosterTypeId: Roster Type Id,
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
        try
        {
            string username = EntitiesUtilities.GetAuthName();
            Staff obj = DataRepository.StaffProvider.GetByUserNameIsDisabled(username, false);
            if (obj == null)
            {
                result = @"[{ 'result': 'false', 'message': 'Doctor is not exist.', 'data': '[]' }]";
                return result;
            }

            TList<RosterType> lstRefObj = DataRepository.RosterTypeProvider.GetByIsDisabled(false);
            // Cho nay se ve sua lai database, them index cho GetByDoctorId va Disabled
            // Lam them cai neu Note ma rong thi ko hien thi
            TList<DoctorRoster> lstObj = DataRepository.DoctorRosterProvider.GetByIsDisabled(false);
            ServiceFacade.SettingsHelper.UncompleteColor = "#A8D4FF";
            ServiceFacade.SettingsHelper.CompleteColor = "#BBBBBB";

            string color = string.Empty;
            foreach (DoctorRoster item in lstObj)
            {
                RosterType itemRef = lstRefObj.Find("Id", item.RosterTypeId);
                if (itemRef != null)
                {
                    result += @"{id: '" + item.Id + @"',"
                        + @"start_date: '" + item.StartTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                        + @"end_date: '" + item.EndTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                        + @"text: '" + itemRef.Note + @"<br />";

                    if (!string.IsNullOrEmpty(item.Note))
                        result += item.Note;

                    result += @"',DoctorId: '" + item.DoctorId + @"',"
                     + @"RosterTypeId: '" + item.RosterTypeId + @"',"
                     + @"note: '" + item.Note + @"',";

                    if (item.StartTime <= DateTime.Now)
                    {
                        result += @"color: '" + ServiceFacade.SettingsHelper.CompleteColor + @"',";
                        result += @"readonly: true,";
                    }
                    else
                    {
                        result += @"color: '" + ServiceFacade.SettingsHelper.UncompleteColor + @"',";
                    }
                    result += @"isnew: 'false'},";
                }
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
        }
        catch (Exception ex)
        {
            //Write log cho nay

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': '[]' }]";
            return result;
        }

        return result;
    }

    #region "Function"
    #endregion
}