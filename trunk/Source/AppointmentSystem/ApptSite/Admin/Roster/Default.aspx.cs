using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppointmentBusiness.BO;
using AppointmentBusiness.Util;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using Appt.Common.Constants;
using Common.Util;
using Log.Controller;
using Newtonsoft.Json;

public partial class Admin_Roster_Default : System.Web.UI.Page
{
    static string strSeperateStaff = " | ";
    private const string ScreenCode = "Roster";
    static string _message;

    #region Event
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Validate user right for reading
            if (!RightAccess.CheckUserRight(EntitiesUtilities.GetAuthName(), ScreenCode, OperationConstant.Read.Key,
                                       out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
            }
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    #endregion

    #region "Roster"
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string SaveEvent(string DoctorId, string RosterTypeId, string RosterTitle, string StartTime, string EndTime,
        string StartDate, string EndDate, string Note, string RepeatRoster, string Weekday, string Month)
    {
        string result = string.Empty;
        string message = string.Empty;
        string username = EntitiesUtilities.GetAuthName();

        // Check StaffId
        if (DoctorId == null)
        {
            return WebCommon.BuildFailedResult("You must choose staff.");
        }

        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            tm.BeginTransaction();

            RightAccess.CheckUserRight(username, WebCommon.GetScreenCode(), OperationConstant.Create.Key, out message);

            #region Validate current user
            Users objUser;
            if (!BoFactory.UserBO.ValidateCurrentUser(username, out objUser, out message))
            {
                return WebCommon.BuildFailedResult(message);
            }
            #endregion

            // Get Roster Type
            RosterType objRt = DataRepository.RosterTypeProvider.GetById(Convert.ToInt32(RosterTypeId));
            if (objRt == null || objRt.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Roster Type is not exist.");
            }

            // Get start time and end time
            int intStartHour = Convert.ToInt32(StartTime.Split(':')[0]);
            int intStartMinute = Convert.ToInt32(StartTime.Split(':')[1]);
            int intEndHour = Convert.ToInt32(EndTime.Split(':')[0]);
            int intEndMinute = Convert.ToInt32(EndTime.Split(':')[1]);

            // Declare list of object are returned
            var lstResult = new List<object>();

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

                // Variable for error message if there is conflict roster
                string errorMessage = string.Empty;

                while (month.Month == item.Month)
                {
                    if (Weekday.Contains(item.DayOfWeek.ToString()))
                    {
                        var newObj = new Roster();
                        strNumber = String.Format("{0:000}", int.Parse(strNumber) + 1);

                        newObj.Id = Perfix + strNumber;

                        newObj.DoctorId = DoctorId;
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
                            return WebCommon.BuildFailedResult("To date must be greater than from date.");
                        }

                        // If roster is created in a passed or current day
                        DateTime dtNow = DateTime.Now;
                        if (new DateTime(dtNow.Year, dtNow.Month, dtNow.Day) >= new DateTime(newObj.StartTime.Year, newObj.StartTime.Month, newObj.StartTime.Day))
                        {
                            tm.Rollback();
                            return WebCommon.BuildFailedResult("You can not change roster to passed or current date.");
                        }

                        // Check roster before insert new roster
                        string query = string.Format("DoctorId = '{0}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'", objUser.Id, newObj.EndTime, newObj.StartTime);
                        TList<Roster> lstRoster = DataRepository.RosterProvider.GetPaged(tm, query, "Id desc", 0, 1, out Count);
                        // If there is no roster -> insert new roster
                        if (Count > 1)
                        {
                            errorMessage += newObj.StartTime.DayOfWeek.ToString() + " " + newObj.StartTime.ToString("dd MMM yyyy") + ", ";
                        }

                        DataRepository.RosterProvider.DeepLoad(newObj);

                        lstResult.Add(new
                                          {
                                              id = newObj.Id,
                                              start_date = newObj.StartTime.ToString("dd/MM/yyyy HH:mm:ss"),
                                              end_date = newObj.EndTime.ToString("dd/MM/yyyy HH:mm:ss"),
                                              section_id = newObj.DoctorId,
                                              text = String.Format("{0}<br />Doctor: {1}<br />{2}"
                                                      , newObj.RosterTypeIdSource.Title
                                                      , newObj.DoctorIdSource.Username
                                                      , newObj.Note),
                                              DoctorUserName = newObj.DoctorIdSource.Username,
                                              DoctorShortName = newObj.DoctorIdSource.DisplayName,
                                              newObj.RosterTypeId,
                                              RosterTypeTitle = newObj.RosterTypeIdSource.Title,
                                              note = newObj.Note,
                                              isnew = false
                                          });
                    }
                    item = item.AddDays(1);
                }

                if (errorMessage.Length > 0)
                {
                    tm.Rollback();
                    return WebCommon.BuildFailedResult(String.Format("There are some rosters conflicted: {0}", errorMessage.Substring(0, errorMessage.Length - 1)));
                }

                result = WebCommon.BuildSuccessfulResult(lstResult);
            }
            else
            {
                var newObj = new Roster();

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

                newObj.DoctorId = DoctorId;
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
                    return WebCommon.BuildFailedResult("To date must be greater than from date.");
                }

                // If roster is created in a passed or current day
                DateTime dtNow = DateTime.Now;
                if (new DateTime(dtNow.Year, dtNow.Month, dtNow.Day) >= new DateTime(newObj.StartTime.Year, newObj.StartTime.Month, newObj.StartTime.Day))
                {
                    tm.Rollback();
                    return WebCommon.BuildFailedResult("You can not change roster to passed or current date.");
                }

                // Check roster before insert new roster
                string query = string.Format("DoctorId = '{0}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'", objUser.Id, newObj.EndTime, newObj.StartTime);
                TList<Roster> lstRoster = DataRepository.RosterProvider.GetPaged(tm, query, "Id desc", 0, 1, out Count);
                // If there is no roster -> insert new roster
                if (Count > 1)
                {
                    tm.Rollback();
                    return WebCommon.BuildFailedResult(String.Format("There is roster conflicted: From {0} {1} to {2} {3}"
                        , newObj.StartTime.DayOfWeek, newObj.StartTime.ToString("dd MMM yyyy HH:mm")
                        , newObj.EndTime.DayOfWeek, newObj.EndTime.ToString("dd MMM yyyy HH:mm")));
                }

                DataRepository.RosterProvider.DeepLoad(newObj);
                lstResult.Add(new
                {
                    id = newObj.Id,
                    start_date = newObj.StartTime.ToString("dd/MM/yyyy HH:mm:ss"),
                    end_date = newObj.EndTime.ToString("dd/MM/yyyy HH:mm:ss"),
                    section_id = newObj.DoctorId,
                    text = String.Format("{0}<br />Doctor: {1}<br />{2}"
                            , newObj.RosterTypeIdSource.Title
                            , newObj.DoctorIdSource.Username
                            , newObj.Note),
                    DoctorUserName = newObj.DoctorIdSource.Username,
                    DoctorShortName = newObj.DoctorIdSource.DisplayName,
                    newObj.RosterTypeId,
                    RosterTypeTitle = newObj.RosterTypeIdSource.Title,
                    note = newObj.Note,
                    isnew = false
                });
            }
            tm.Commit();

            result = WebCommon.BuildSuccessfulResult(lstResult);
        }
        catch (Exception ex)
        {
            //Write log cho nay
            tm.Rollback();
            return WebCommon.BuildFailedResult(ex.Message);
        }

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
    private static string UpdateEvent(string Id, string doctorId, string RosterTypeId, string RosterTitle, DateTime StartTime, DateTime EndTime, string Note)
    {
        string result = string.Empty;
        string message = string.Empty;

        // Check StaffId
        if (doctorId == null)
        {
            return WebCommon.BuildFailedResult("You must choose staff.");
        }

        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            int count;
            tm.BeginTransaction();

            // Validate current user have any right to operate this action
            string username = EntitiesUtilities.GetAuthName();
            Users objUser;
            if (!BoFactory.UserBO.ValidateCurrentUser(username, out objUser, out message))
                return WebCommon.BuildFailedResult(message);

            // Them phan kiem tra doctor Id co hop le hay khong
            // Se lam sau

            // Validate existed or expired roster
            Roster dr = DataRepository.RosterProvider.GetById(Id);
            if (dr == null || dr.IsDisabled)
                return WebCommon.BuildFailedResult("There is no roster to update or the roster is expired.");

            // Get Roster Type
            RosterType objRt = DataRepository.RosterTypeProvider.GetByIdIsDisabled(Convert.ToInt32(RosterTypeId), false);
            if (objRt == null)
                return WebCommon.BuildFailedResult("Roster Type is not exist.");

            dr.DoctorId = doctorId;
            dr.RosterTypeId = Convert.ToInt32(RosterTypeId);
            dr.StartTime = StartTime;
            dr.EndTime = EndTime;
            dr.Note = Note;
            dr.UpdateUser = username;
            dr.UpdateDate = DateTime.Now;

            DataRepository.RosterProvider.Save(tm, dr);

            // Check if start time >= end time
            if (dr.StartTime >= dr.EndTime)
                return WebCommon.BuildFailedResult("To date must be greater than from date.");

            // If roster is created in a passed or current day
            if (Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd")) >= Convert.ToDateTime(dr.StartTime.ToString("yyyy/MM/dd")))
            {
                tm.Rollback();
                return WebCommon.BuildFailedResult("You can not change roster to passed or current date.");
            }

            // Check roster before insert new roster
            string query = string.Format("DoctorId = '{0}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'", objUser.Id, dr.EndTime, dr.StartTime);
            TList<Roster> lstRoster = DataRepository.RosterProvider.GetPaged(tm, query, "Id desc", 0, 1, out count);
            // If there is no roster -> insert new roster
            if (count > 1)
            {
                tm.Rollback();
                return WebCommon.BuildFailedResult(String.Format("There is roster conflicted: From {0} {1} to {2} {3}"
                    , dr.StartTime.DayOfWeek.ToString(), dr.StartTime.ToString("dd MMM yyyy HH:mm")
                    , dr.EndTime.DayOfWeek.ToString(), dr.EndTime.ToString("dd MMM yyyy HH:mm")));
            }

            // Declare list of object are returned
            var lstResult = new List<object>();

            DataRepository.RosterProvider.DeepLoad(dr);

            lstResult.Add(new
            {
                id = dr.Id,
                start_date = dr.StartTime.ToString("dd/MM/yyyy HH:mm:ss"),
                end_date = dr.EndTime.ToString("dd/MM/yyyy HH:mm:ss"),
                section_id = dr.DoctorId,
                text = String.Format("{0}<br />Doctor: {1}<br />{2}"
                        , dr.RosterTypeIdSource.Title
                        , dr.DoctorIdSource.Username
                        , dr.Note),
                DoctorUserName = dr.DoctorIdSource.Username,
                DoctorShortName = dr.DoctorIdSource.DisplayName,
                dr.RosterTypeId,
                RosterTypeTitle = dr.RosterTypeIdSource.Title,
                note = dr.Note,
                isnew = false
            });
            tm.Commit();
            result = WebCommon.BuildSuccessfulResult(lstResult);
        }
        catch (Exception ex)
        {
            //Write log cho nay
            tm.Rollback();
            return WebCommon.BuildFailedResult(ex.Message);
        }

        return result;
    }
    #endregion

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string LoadRoster(string mode, string currentDateView)
    {
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
                return WebCommon.BuildFailedResult(message);
            }

            int count;
            var lstObj = DataRepository.RosterProvider.GetPaged("IsDisabled = 'False'", string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);
            DataRepository.RosterProvider.DeepLoad(lstObj);

            // Declare list of object are returned
            var lstResult = new List<object>();

            string color = string.Empty;
            foreach (Roster item in lstObj)
            {
                lstResult.Add(new
                {
                    id = item.Id,
                    start_date = item.StartTime.ToString("dd/MM/yyyy HH:mm:ss"),
                    end_date = item.EndTime.ToString("dd/MM/yyyy HH:mm:ss"),
                    section_id = item.DoctorId,
                    text = String.Format("{0}<br />Doctor: {1}<br />{2}"
                            , item.RosterTypeIdSource.Title
                            , item.DoctorIdSource.Username
                            , item.Note),
                    DoctorUserName = item.DoctorIdSource.Username,
                    DoctorShortName = item.DoctorIdSource.DisplayName,
                    item.RosterTypeId,
                    RosterTypeTitle = item.RosterTypeIdSource.Title,
                    note = item.Note,
                    isnew = false
                });
            }
            result = WebCommon.BuildSuccessfulResult(lstResult);
            tm.Commit();
        }
        catch (Exception ex)
        {
            //Write log cho nay
            tm.Rollback();
            return WebCommon.BuildFailedResult(ex.Message);
        }

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

    //StepResult:
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
        }
        catch (Exception ex)
        {
            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
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
