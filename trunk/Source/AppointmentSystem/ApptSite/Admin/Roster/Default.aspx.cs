using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
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
    private const string ScreenCode = "Roster";
    static string _message;
    static readonly string Username = EntitiesUtilities.GetAuthName();

    #region Event
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Validate user right for reading
            if (!RightAccess.CheckUserRight(Username, ScreenCode, OperationConstant.Read.Key, out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
            }
            else
            {
                // Show script
                ClientScript.RegisterStartupScript(GetType(), "MainScript", @"<script src=""GRNEditt.js"" type=""text/javascript""></script>");
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
    public static string NewRoster(string doctorId, string rosterTypeId, string startTime, string endTime,
        string startDate, string endDate, string note, string repeatRoster, string weekday)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            tm.BeginTransaction();

            #region Validation and covert value
            // Validate user right for creating
            if (!RightAccess.CheckUserRight(Username, ScreenCode, OperationConstant.Create.Key, out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Check doctor
            if (string.IsNullOrEmpty(doctorId))
            {
                return WebCommon.BuildFailedResult("You must choose doctor.");
            }

            // Get Roster Type
            int intRosterTypeId;
            // If rosterTypeId is not integer
            if (!Int32.TryParse(rosterTypeId, out intRosterTypeId))
            {
                return WebCommon.BuildFailedResult("Roster Type is not exist.");
            }

            // If roster type is not existed
            RosterType objRt = DataRepository.RosterTypeProvider.GetById(intRosterTypeId);
            if (objRt == null || objRt.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Roster Type is not exist.");
            }

            // Get start time and end time, validate all of them
            int intStartHour, intStartMinute, intEndHour, intEndMinute;
            if (!Int32.TryParse(startTime.Split(':')[0], out intStartHour)
                || !Int32.TryParse(startTime.Split(':')[1], out intStartMinute)
                || !Int32.TryParse(endTime.Split(':')[0], out intEndHour)
                || !Int32.TryParse(endTime.Split(':')[1], out intEndMinute))
            {
                return WebCommon.BuildFailedResult("Time is invalid.");
            }

            // Get start date and end date, validate all of them
            DateTime dtStart, dtEnd;
            if (!DateTime.TryParse(startDate, out dtStart)
                || !DateTime.TryParse(endDate, out dtEnd))
            {
                return WebCommon.BuildFailedResult("Date is invalid.");
            }
            #endregion

            // Declare list of object are returned
            var lstResult = new List<object>();

            // Declare list of roster in case repeat roster
            var lstRoster = new TList<Roster>();

            // Repeat roster
            if (repeatRoster.Trim().ToLower() == "true")
            {
                #region Validate Time
                if (new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, intStartHour, intStartMinute, 0)
                    >= new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, intEndHour, intEndMinute, 0))
                {
                    return WebCommon.BuildFailedResult("To time must be greater than from date.");
                }

                // If roster is created in a passed or current day
                DateTime dtNow = DateTime.Now;
                if (new DateTime(dtNow.Year, dtNow.Month, dtNow.Day) >= new DateTime(dtStart.Year, dtStart.Month, dtStart.Day))
                {
                    tm.Rollback();
                    return WebCommon.BuildFailedResult("You can not change roster to passed or current date.");
                }
                #endregion

                // Get auto increasement id
                string perfix = ServiceFacade.SettingsHelper.RosterPrefix + DateTime.Now.ToString("yyMMdd");
                int count;
                TList<Roster> objPo = DataRepository.RosterProvider.GetPaged(tm, "Id like '" + perfix + "' + '%'", "Id desc", 0, 1, out count);
                string number = count == 0 ? "000" : String.Format("{0:000}", int.Parse(objPo[0].Id.Substring(objPo[0].Id.Length - 3)));

                // Variable for error message if there is conflict roster
                string errorMessage = string.Empty;

                dtStart = dtStart.AddDays(-1);
                while (dtStart <= dtEnd)
                {
                    dtStart = dtStart.AddDays(1);
                    if (weekday.Contains(dtStart.DayOfWeek.ToString()))
                    {
                        var dtTmpStart = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, intStartHour, intStartMinute, 0);
                        var dtTmpEnd = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, intEndHour, intEndMinute, 0);

                        // Check existed rosters
                        string query = string.Format("DoctorId = '{0}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'"
                            , doctorId, dtTmpEnd, dtTmpStart);
                        DataRepository.RosterProvider.GetPaged(tm, query, "Id desc", 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);
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
                                              DoctorId = doctorId,
                                              RosterTypeId = intRosterTypeId,
                                              StartTime = dtTmpStart,
                                              EndTime = dtTmpEnd,
                                              Note = note,
                                              CreateUser = Username,
                                              UpdateUser = Username
                                          });
                    }
                }

                if (errorMessage.Length > 0)
                {
                    tm.Rollback();
                    return WebCommon.BuildFailedResult(String.Format("There are some rosters conflicted: {0}", errorMessage.Substring(0, errorMessage.Length - 1)));
                }
            }
            else
            {
                #region Validate Time
                var dtTmpStart = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, intStartHour, intStartMinute, 0);
                var dtTmpEnd = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, intEndHour, intEndMinute, 0);

                if (dtTmpStart >= dtTmpEnd)
                {
                    return WebCommon.BuildFailedResult("To time must be greater than from date.");
                }
                // If roster is created in a passed or current day
                DateTime dtNow = DateTime.Now;
                if (new DateTime(dtNow.Year, dtNow.Month, dtNow.Day) >= new DateTime(dtStart.Year, dtStart.Month, dtStart.Day))
                {
                    tm.Rollback();
                    return WebCommon.BuildFailedResult("You can not change roster to passed or current date.");
                }
                #endregion

                var newObj = new Roster();

                string perfix = ServiceFacade.SettingsHelper.RosterPrefix + DateTime.Now.ToString("yyMMdd");
                int count;
                TList<Roster> objPo = DataRepository.RosterProvider.GetPaged(tm, "Id like '" + perfix + "' + '%'", "Id desc", 0, 1, out count);
                string id;
                if (count == 0)
                    id = perfix + "001";
                else
                {
                    id = perfix + String.Format("{0:000}", int.Parse(objPo[0].Id.Substring(objPo[0].Id.Length - 3)) + 1);
                }

                // Check existed rosters
                string query = string.Format("DoctorId = '{0}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'"
                    , doctorId, dtTmpEnd, dtTmpStart);
                DataRepository.RosterProvider.GetPaged(tm, query, "Id desc", 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);
                // If there is no roster -> insert new roster
                if (count > 0)
                {
                    return WebCommon.BuildFailedResult(String.Format("There is roster conflicted: From {0} {1} to {2} {3}"
                        , newObj.StartTime.DayOfWeek, newObj.StartTime.ToString("dd MMM yyyy HH:mm")
                        , newObj.EndTime.DayOfWeek, newObj.EndTime.ToString("dd MMM yyyy HH:mm")));
                }

                lstRoster.Add(new Roster
                {
                    Id = id,
                    DoctorId = doctorId,
                    RosterTypeId = intRosterTypeId,
                    StartTime = dtTmpStart,
                    EndTime = dtTmpEnd,
                    Note = note,
                    CreateUser = Username,
                    UpdateUser = Username
                });
            }
            // Insert new rosters
            DataRepository.RosterProvider.Insert(tm, lstRoster);

            // Load relation data
            DataRepository.RosterProvider.DeepLoad(lstRoster);

            // Add all roster to result list
            AddRoster(lstResult, lstRoster);

            tm.Commit();

            return WebCommon.BuildSuccessfulResult(lstResult);
        }
        catch (Exception ex)
        {
            //Write log cho nay
            tm.Rollback();
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }
    #endregion

    #region "Update Roster"
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string MoveRoster(string id, string doctorId, string startTime, string endTime)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            tm.BeginTransaction();

            // Declare list of object are returned
            var lstResult = new List<object>();

            #region Validation
            // Get roster by id
            Roster rosterItem = DataRepository.RosterProvider.GetById(id);

            // Validate if roster's not existed
            // return message, nothing more
            if (rosterItem == null)
            {
                return WebCommon.BuildFailedResult("There is no roster to update or the roster is expired.");
            }

            // Load relation
            DataRepository.RosterProvider.DeepLoad(rosterItem);

            // Validate if roster's expired
            // return message with roster, this will be used to rollback roster to previous location, before moved
            if (rosterItem.IsDisabled)
            {
                AddRoster(lstResult, rosterItem);
                return WebCommon.BuildFailedResult("Roster is expired.", lstResult);
            }

            // Validate current user have any right to operate this action
            // Validate user right for reading
            if (!RightAccess.CheckUserRight(Username, ScreenCode, OperationConstant.Update.Key, out _message))
            {
                AddRoster(lstResult, rosterItem);
                return WebCommon.BuildFailedResult(_message, lstResult);
            }

            // Check StaffId
            if (doctorId == null)
            {
                AddRoster(lstResult, rosterItem);
                return WebCommon.BuildFailedResult("You must choose staff.", lstResult);
            }

            // Get start date and end date
            DateTime dtStart = Convert.ToDateTime(startTime);
            DateTime dtEnd = Convert.ToDateTime(endTime);

            // If roster is created in a passed or current day
            if (DateTime.Now >= dtStart)
            {
                AddRoster(lstResult, rosterItem);
                return WebCommon.BuildFailedResult("You can not change roster to passed or current date.", lstResult);
            }

            if (dtStart >= dtEnd)
            {
                AddRoster(lstResult, rosterItem);
                return WebCommon.BuildFailedResult("To time must be greater than from date.");
            }

            // Check roster before insert new roster
            int count;
            DataRepository.RosterProvider.GetPaged(String.Format("Id <> '{3}' AND DoctorId = '{0}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'"
                , doctorId, dtEnd, dtStart, id), string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);
            if (count > 0)
            {
                AddRoster(lstResult, rosterItem);
                return WebCommon.BuildFailedResult(String.Format("There is roster conflicted: From {0} {1} to {2} {3}"
                    , rosterItem.StartTime.DayOfWeek.ToString(), rosterItem.StartTime.ToString("dd MMM yyyy HH:mm")
                    , rosterItem.EndTime.DayOfWeek.ToString(), rosterItem.EndTime.ToString("dd MMM yyyy HH:mm")), lstResult);
            }
            #endregion

            // Set new value
            rosterItem.DoctorId = doctorId;
            rosterItem.StartTime = dtStart;
            rosterItem.EndTime = dtEnd;
            rosterItem.UpdateUser = Username;
            rosterItem.UpdateDate = DateTime.Now;

            // Save roster
            DataRepository.RosterProvider.Save(tm, rosterItem);
            DataRepository.RosterProvider.DeepLoad(rosterItem);

            AddRoster(lstResult, rosterItem);
            tm.Commit();
            return WebCommon.BuildSuccessfulResult(lstResult);
        }
        catch (Exception ex)
        {
            //Write log cho nay
            tm.Rollback();
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string UpdateRoster(string id, string doctorId, string rosterTypeId, string startTime, string endTime,
        string startDate, string endDate, string note)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            tm.BeginTransaction();

            // Validate user right for updating
            if (!RightAccess.CheckUserRight(Username, ScreenCode, OperationConstant.Update.Key, out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Declare list of object are returned
            var lstResult = new List<object>();

            #region Validation and convert value
            // Get roster by id
            Roster rosterItem = DataRepository.RosterProvider.GetById(id);

            // Validate if roster's not existed
            // return message, nothing more
            if (rosterItem == null)
            {
                return WebCommon.BuildFailedResult("There is no roster to update or the roster is expired.");
            }

            // Validate if roster's expired
            // return message with roster, this will be used to rollback roster to previous location, before moved
            if (rosterItem.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Roster is expired.");
            }

            // Check doctor
            if (string.IsNullOrEmpty(doctorId))
            {
                return WebCommon.BuildFailedResult("You must choose doctor.");
            }

            // Get Roster Type
            int intRosterTypeId;
            // If rosterTypeId is not integer
            if (!Int32.TryParse(rosterTypeId, out intRosterTypeId))
            {
                return WebCommon.BuildFailedResult("Roster Type is not exist.");
            }

            // If roster type is not existed
            RosterType objRt = DataRepository.RosterTypeProvider.GetById(intRosterTypeId);
            if (objRt == null || objRt.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Roster Type is not exist.");
            }

            // Get start time and end time, validate all of them
            int intStartHour, intStartMinute, intEndHour, intEndMinute;
            if (!Int32.TryParse(startTime.Split(':')[0], out intStartHour)
                || !Int32.TryParse(startTime.Split(':')[1], out intStartMinute)
                || !Int32.TryParse(endTime.Split(':')[0], out intEndHour)
                || !Int32.TryParse(endTime.Split(':')[1], out intEndMinute))
            {
                return WebCommon.BuildFailedResult("Time is invalid.");
            }

            // Get start date and end date, validate all of them
            DateTime dtStart, dtEnd;
            if (!DateTime.TryParse(startDate, out dtStart)
                || !DateTime.TryParse(endDate, out dtEnd))
            {
                return WebCommon.BuildFailedResult("Date is invalid.");
            }

            // Get datetime
            dtStart = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, intStartHour, intStartMinute, 0);
            dtEnd = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, intEndHour, intEndMinute, 0);

            // If roster is created in a passed or current day
            if (DateTime.Now >= dtStart)
            {
                AddRoster(lstResult, rosterItem);
                return WebCommon.BuildFailedResult("You can not change roster to passed or current date.", lstResult);
            }

            if (dtStart >= dtEnd)
            {
                return WebCommon.BuildFailedResult("To time must be greater than from date.");
            }

            // Check existed rosters
            int count;
            string query = string.Format("DoctorId = '{0}' AND Id <> '{3}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'"
                , doctorId, dtEnd, dtStart, id);
            DataRepository.RosterProvider.GetPaged(tm, query, "Id desc", 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);
            // If there is no roster -> insert new roster
            if (count > 0)
            {
                return WebCommon.BuildFailedResult(String.Format("There is roster conflicted: From {0} {1} to {2} {3}"
                    , dtStart.DayOfWeek, dtStart.ToString("dd MMM yyyy HH:mm")
                    , dtEnd.DayOfWeek, dtEnd.ToString("dd MMM yyyy HH:mm")));
            }
            #endregion

            rosterItem.DoctorId = doctorId;
            rosterItem.RosterTypeId = intRosterTypeId;
            rosterItem.StartTime = dtStart;
            rosterItem.EndTime = dtEnd;
            rosterItem.Note = note;
            rosterItem.UpdateUser = Username;
            rosterItem.UpdateDate = DateTime.Now;

            DataRepository.RosterProvider.Save(tm, rosterItem);
            DataRepository.RosterProvider.DeepLoad(rosterItem);

            AddRoster(lstResult, rosterItem);
            tm.Commit();
            return WebCommon.BuildSuccessfulResult(lstResult);
        }
        catch (Exception ex)
        {
            //Write log cho nay
            tm.Rollback();
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string LoadRoster(string mode, string currentDateView)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            tm.BeginTransaction();

            // Validate user right for reading
            if (!RightAccess.CheckUserRight(Username, ScreenCode, OperationConstant.Read.Key, out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            int count;
            var lstObj = DataRepository.RosterProvider.GetPaged("IsDisabled = 'False'", string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);
            DataRepository.RosterProvider.DeepLoad(lstObj);

            // Declare list of object are returned
            var lstResult = new List<object>();

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
            tm.Commit();
            return WebCommon.BuildSuccessfulResult(lstResult);
        }
        catch (Exception ex)
        {
            //Write log cho nay
            tm.Rollback();
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string DeleteRoster(string id)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            // Validate user right for deleting
            if (!RightAccess.CheckUserRight(Username, ScreenCode, OperationConstant.Delete.Key, out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            tm.BeginTransaction();

            Roster roster = DataRepository.RosterProvider.GetById(id);
            if (roster == null || roster.IsDisabled)
            {
                return WebCommon.BuildFailedResult("There is no roster to delete or roster is expired.");
            }

            roster.IsDisabled = true;
            roster.UpdateUser = Username;
            roster.UpdateDate = DateTime.Now;

            DataRepository.RosterProvider.Save(tm, roster);
            tm.Commit();

            return WebCommon.BuildSuccessfulResult("Roster is deleted");
        }
        catch (Exception ex)
        {
            tm.Rollback();
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }
    #endregion

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetRosterType()
    {
        try
        {
            // Validate user right for reading
            if (!RightAccess.CheckUserRight(Username, ScreenCode, OperationConstant.Read.Key, out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            int count;
            TList<RosterType> lst = DataRepository.RosterTypeProvider.GetPaged("IsDisabled = 'False'", "Title ASC", 0,
                                                                               ServiceFacade.SettingsHelper.GetPagedLength,
                                                                               out count);
            return WebCommon.BuildSuccessfulResult(lst.Select(x => new { x.Id, x.Title }));
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetDoctorTree()
    {
        try
        {
            // Validate user right for reading
            if (!RightAccess.CheckUserRight(Username, ScreenCode, OperationConstant.Read.Key, out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Get all services
            int count;
            var lstService = DataRepository.ServicesProvider.GetPaged("IsDisabled = 'False'", string.Empty, 0
                , ServiceFacade.SettingsHelper.GetPagedLength, out count);

            // Get all available staffs
            TList<DoctorService> lstDoctorService = DataRepository.DoctorServiceProvider.GetPaged("IsDisabled = 'False'"
                , string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);
            DataRepository.DoctorServiceProvider.DeepLoad(lstDoctorService);

            return WebCommon.BuildSuccessfulResult(lstService.Select(item => new
                                                                                 {
                                                                                     key = item.Id,
                                                                                     label = item.Title,
                                                                                     open = true,
                                                                                     children = lstDoctorService.FindAll(x => x.ServiceId == item.Id).Select(service => new
                                                                                     {
                                                                                         key = service.DoctorId,
                                                                                         label = service.DoctorIdSource.DisplayName
                                                                                     })  
                                                                                 }));
        }
        catch (Exception ex)
        {
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }

    #region Methods
    /// <summary>
    /// Add roster to list
    /// </summary>
    /// <param name="lst"></param>
    /// <param name="item"></param>
    private static void AddRoster(List<object> lst, Roster item)
    {
        lst.Add(new
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

    /// <summary>
    /// Add roster to list
    /// </summary>
    /// <param name="lst"></param>
    /// <param name="lstItem"></param>
    private static void AddRoster(List<object> lst, IEnumerable<Roster> lstItem)
    {
        foreach (var roster in lstItem)
        {
            AddRoster(lst, roster);
        }
    }
    #endregion

    #region Doctor
    /// <summary>
    /// Search doctor by keyword
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetDoctorById(string doctorId)
    {
        try
        {
            // Validate user right for reading
            if (!RightAccess.CheckUserRight(Username, ScreenCode, OperationConstant.Read.Key, out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            var doctor = DataRepository.UsersProvider.GetById(doctorId);
            object result = null;
            if (doctor != null)
            {
                result = new
                             {
                                 DoctorId = doctor.Id,
                                 doctor.DisplayName
                             };
            }
            return WebCommon.BuildSuccessfulResult(result);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }

    /// <summary>
    /// Search doctor by keyword
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static string SearchDoctor()
    {
        try
        {
            // Validate user right for reading
            if (!RightAccess.CheckUserRight(Username, ScreenCode, OperationConstant.Read.Key, out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            return SearchDoctor(HttpContext.Current.Request["q"]);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }

        return string.Empty;
    }

    /// <summary>
    /// Search Doctor with conditions
    /// </summary>
    /// <param name="keyword"> </param>
    /// <returns></returns>
    private static string SearchDoctor(string keyword)
    {
        try
        {
            // Se them cai vu chi tim theo bac si sau
            int count;
            string query = string.Format("(Id LIKE '%{0}%' OR Firstname LIKE '%{0}%' OR Lastname LIKE '%{0}%' OR DisplayName LIKE '%{0}%') AND IsDisabled = 'False'", keyword);
            var lstDoctor = DataRepository.UsersProvider.GetPaged(query, string.Empty, 0
                , 10, out count);

            var lst = (from Users item in lstDoctor
                       select new
                       {
                           id = item.Id,
                           item.DisplayName
                       });

            return JsonConvert.SerializeObject(lst);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return string.Empty;
        }
    }
    #endregion
}
