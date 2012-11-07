using System;
using System.Collections.Generic;
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
using Common.Extension;
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

            BindStatus();
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Get roster type
    /// </summary>
    private void BindStatus()
    {
        try
        {
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
    public static string NewRoster(string doctorId, int? rosterTypeId, DateTime? startTime, DateTime? endTime,
        DateTime? startDate, DateTime? endDate, string note, bool? repeatRoster, string weekday)
    {
        try
        {
            #region Validation and covert value
            // Validate user right for creating
            if (!CheckCreating(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            if (!WebCommon.ValidateEmpty("Doctor", doctorId, out _message)
                || !WebCommon.ValidateEmpty("Roster Type", rosterTypeId, out _message)
                || !WebCommon.ValidateEmpty("Start Time", startTime, out _message)
                || !WebCommon.ValidateEmpty("End Time", endTime, out _message)
                || !WebCommon.ValidateEmpty("Start Date", startDate, out _message)
                || !WebCommon.ValidateEmpty("End Date", endDate, out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Get start date and end date, validate all of them
            var dtStart = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day
                                       , startTime.Value.Hour, startTime.Value.Minute, 0);
            var dtEnd = new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day
                                     , endTime.Value.Hour, endTime.Value.Minute, 0);
            #endregion

            var roster = new Roster
            {
                Username = doctorId,
                RosterTypeId = Convert.ToInt32(rosterTypeId),
                StartTime = dtStart,
                EndTime = dtEnd,
                Note = note,
                CreateUser = Username,
                UpdateUser = Username
            };

            // Declare list of object are returned
            var lstResult = new List<object>();

            // Declare list of roster in case repeat roster
            var lstRoster = new TList<Roster>();

            // Repeat roster
            if (repeatRoster != null && repeatRoster == true)
            {
                if(!BoFactory.RosterBO.InsertRepeat(roster, weekday, lstRoster, ref _message))
                    return WebCommon.BuildFailedResult(_message);
            }
            else
            {
                if (!BoFactory.RosterBO.Insert(roster, ref _message))
                    return WebCommon.BuildFailedResult(_message);
                lstRoster.Add(roster);
            }

            // Load relation data
            DataRepository.RosterProvider.DeepLoad(lstRoster);

            // Add all roster to result list
            AddRoster(lstResult, lstRoster);

            return WebCommon.BuildSuccessfulResult(lstResult);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult("System error. Please contact Administrator.");
        }
    }
    #endregion

    #region "Update Roster"
    /// <summary>
    /// Di chuyen roster
    /// </summary>
    /// <param name="id"></param>
    /// <param name="doctorId"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="rosterTypeId"></param>
    /// <param name="note"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string MoveRoster(string id, string doctorId, int? rosterTypeId, DateTime? startTime, DateTime? endTime, string note)
    {
        try
        {
            #region Validation and covert value
            // Validate current user have any right to operate this action
            // Validate user right for updating roster
            if (!CheckUpdating(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            if (!WebCommon.ValidateEmpty("Roster Id", id, out _message)
                || !WebCommon.ValidateEmpty("Roster Type", rosterTypeId, out _message)
                || !WebCommon.ValidateEmpty("Doctor", doctorId, out _message)
                || !WebCommon.ValidateEmpty("Start Time", startTime, out _message)
                || !WebCommon.ValidateEmpty("End Time", endTime, out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }
            #endregion


            // Declare list of object are returned
            var lstResult = new List<object>();

            var roster = new Roster
            {
                Id = id,
                Username = doctorId,
                RosterTypeId = Convert.ToInt32(rosterTypeId),
                StartTime = Convert.ToDateTime(startTime),
                EndTime = Convert.ToDateTime(endTime),
                Note = note,
                UpdateUser = Username
            };

            if (!BoFactory.RosterBO.Update(ref roster, ref _message))
                return WebCommon.BuildFailedResult(_message);

            AddRoster(lstResult, roster);
            return WebCommon.BuildSuccessfulResult(lstResult);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult("System error. Please contact Administrator.");
        }
    }

    /// <summary>
    /// Cap nhat roster
    /// </summary>
    /// <param name="id"></param>
    /// <param name="doctorId"></param>
    /// <param name="rosterTypeId"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="note"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string UpdateRoster(string id, string doctorId, int? rosterTypeId, DateTime? startTime, DateTime? endTime,
        DateTime? startDate, DateTime? endDate, string note)
    {
        try
        {
            #region Validation and covert value
            // Validate user right for updating
            if (!CheckUpdating(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            if (!WebCommon.ValidateEmpty("Roster Id", id, out _message)
                || !WebCommon.ValidateEmpty("Doctor", doctorId, out _message)
                || !WebCommon.ValidateEmpty("Roster Type", rosterTypeId, out _message)
                || !WebCommon.ValidateEmpty("Start Time", startTime, out _message)
                || !WebCommon.ValidateEmpty("End Time", endTime, out _message)
                || !WebCommon.ValidateEmpty("Start Date", startDate, out _message)
                || !WebCommon.ValidateEmpty("End Date", endDate, out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Get start date and end date, validate all of them
            var dtStart = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day
                                       , startTime.Value.Hour, startTime.Value.Minute, 0);
            var dtEnd = new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day
                                     , endTime.Value.Hour, endTime.Value.Minute, 0);
            #endregion

            // Declare list of object are returned
            var lstResult = new List<object>();

            var roster = new Roster
            {
                Id = id,
                Username = doctorId,
                RosterTypeId = Convert.ToInt32(rosterTypeId),
                StartTime = dtStart,
                EndTime = dtEnd,
                Note = note,
                UpdateUser = Username
            };

            if (!BoFactory.RosterBO.Update(ref roster, ref _message))
                return WebCommon.BuildFailedResult(_message);

            AddRoster(lstResult, roster);
            return WebCommon.BuildSuccessfulResult(lstResult);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult("System error. Please contact Administrator.");
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string LoadRoster(string mode, DateTime? date)
    {
        try
        {
            // Validate user right for reading
            if (!CheckReading(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            var lstRoster = BoFactory.RosterBO.GetByDateMode(date, mode, out _message);
            if (!string.IsNullOrEmpty(_message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Declare list of object are returned
            var lstResult = new List<object>();

            // Neu mode la thang thi select date thoi, de co the tao timespan
            if (mode == "month")
            {
                var lstDate = lstRoster.Select(item => item.StartTime.ToString("MM/dd/yyyy 00:00:00")).Distinct().ToList();
                foreach (string strDate in lstDate)
                {
                    lstResult.Add(new
                    {
                        Date = strDate
                    });
                }
            }
            else
            {
                foreach (Roster item in lstRoster)
                {
                    lstResult.Add(new
                    {
                        id = item.Id,
                        start_date = String.Format("{0:MM-dd-yyyy HH:mm:ss}", item.StartTime),
                        end_date = String.Format("{0:MM-dd-yyyy HH:mm:ss}", item.EndTime),
                        section_id = item.Username,
                        text = String.Format("{0}<br />Doctor: {1}<br />{2}"
                                , item.RosterTypeIdSource.Title
                                , item.Username
                                , item.Note),
                        DoctorUserName = item.Username,
                        DoctorShortName = item.UsernameSource.DisplayName,
                        item.RosterTypeId,
                        RosterTypeTitle = item.RosterTypeIdSource.Title,
                        note = item.Note,
                        isnew = false,
                        color = item.RosterTypeIdSource.ColorCode
                    });
                }
            }
            return WebCommon.BuildSuccessfulResult(lstResult);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }

    /// <summary>
    /// Delete roster
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string DeleteRoster(string id)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            // Validate user right for deleting
            if (!CheckDeleting(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            tm.BeginTransaction();

            Roster roster = DataRepository.RosterProvider.GetById(id);
            if (roster == null || roster.IsDisabled)
            {
                return WebCommon.BuildFailedResult("There is no roster to delete.");
            }
            if (roster.StartTime <= DateTime.Now)
            {
                return WebCommon.BuildFailedResult("Cannot delete roster. Roster is expired.");
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

    #region Methods
    /// <summary>
    /// Add roster to list
    /// </summary>
    /// <param name="lst"></param>
    /// <param name="item"></param>
    private static void AddRoster(List<object> lst, Roster item)
    {
        DataRepository.RosterProvider.DeepLoad(item);
        lst.Add(new
        {
            id = item.Id,
            start_date = String.Format("{0:MM-dd-yyyy HH:mm:ss}", item.StartTime),
            end_date = String.Format("{0:MM-dd-yyyy HH:mm:ss}", item.EndTime),
            section_id = item.Username,
            text = String.Format("{0}<br />Doctor: {1}<br />{2}"
                    , item.RosterTypeIdSource.Title
                    , item.Username
                    , item.Note),
            DoctorUserName = item.Username,
            DoctorShortName = item.UsernameSource.DisplayName,
            item.RosterTypeId,
            RosterTypeTitle = item.RosterTypeIdSource.Title,
            note = item.Note,
            isnew = false,
            color = item.RosterTypeIdSource.ColorCode
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
    /// Lay danh sach doctor
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetDoctorTree()
    {
        try
        {
            // Validate user right for reading
            if (!CheckReading(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Get all services
            int count;
            var lstService = DataRepository.ServicesProvider.GetAll().FindAll(x => !x.IsDisabled);

            // Get all available staffs
            var lstUsers = DataRepository.UsersProvider.GetPaged("IsDisabled = 'False' AND ServicesId IS NOT NULL"
                , string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);

            // Sort user by name
            lstUsers.Sort((x1, x2) => String.CompareOrdinal(x1.DisplayName, x2.DisplayName));

            // Chi lay nhung service co doctor
            lstService = lstService.FindAll(service => lstUsers.FindAll(user => user.ServicesId == service.Id).Count > 0);

            return WebCommon.BuildSuccessfulResult(lstService.Select(item => new
            {
                key = item.Id,
                label = item.Title,
                open = false,
                children = lstUsers.FindAll(x => x.ServicesId == item.Id).Select(user => new
                {
                    key = user.Username,
                    label = user.DisplayName
                })
            }));
        }
        catch (Exception ex)
        {
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }

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
            if (!CheckCreating(out _message) && !CheckUpdating(out _message))
            {
                return WebCommon.BuildFailedResult("You have no right to get doctor's information");
            }

            var doctor = DataRepository.UsersProvider.GetByUsername(doctorId);
            object result = null;
            if (doctor != null)
            {
                result = new
                             {
                                 DoctorId = doctor.Username,
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
            if (!CheckCreating(out _message) && !CheckUpdating(out _message))
            {
                return string.Empty;
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
            string query =
                string.Format(
                    "(DisplayName LIKE '%{0}%') AND IsDisabled = 'False'",
                    keyword);
            var lstDoctor = DataRepository.UsersProvider.GetPaged(query, string.Empty, 0
                , 10, out count);

            var lst = lstDoctor.Select(item => new
                {
                    id = item.Username,
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

    #region Check user right
    /// <summary>
    /// Checking user right for reading
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private static bool CheckReading(out string message)
    {
        return RightAccess.CheckUserRight(Username, ScreenCode, OperationConstant.Read.Key, out message);
    }

    /// <summary>
    /// Checking user right for deleting
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private static bool CheckDeleting(out string message)
    {
        return RightAccess.CheckUserRight(Username, ScreenCode, OperationConstant.Delete.Key, out message);
    }

    /// <summary>
    /// Checking user right for updating
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private static bool CheckUpdating(out string message)
    {
        return RightAccess.CheckUserRight(Username, ScreenCode, OperationConstant.Update.Key, out message);
    }

    /// <summary>
    /// Checking user right for creating
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private static bool CheckCreating(out string message)
    {
        return RightAccess.CheckUserRight(Username, ScreenCode, OperationConstant.Create.Key, out message);
    }
    #endregion

    /// <summary>
    /// Lay khoang thoi gian phu hop voi moi buoc nhay
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="isEndTime"></param>
    /// <returns></returns>
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
}
