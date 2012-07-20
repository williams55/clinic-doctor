using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using AppointmentBusiness.Util;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using Appt.Common.Constants;
using Appt.Common.UserDefine;
using System.Data;
using System.Data.SqlClient;
using Common.Util;
using Log.Controller;
using Newtonsoft.Json;

public partial class Admin_Appointment_Default : System.Web.UI.Page
{
    #region Variables
    // Contain list of floor with Json format
    protected string StrFloors = string.Empty;

    private const string ScreenCode = "Appointment";
    static string _message;
    static readonly string Username = EntitiesUtilities.GetAuthName();
    #endregion

    #region Events
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
            BindTabs();
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
    /// Get floor list
    /// </summary>
    private void BindTabs()
    {
        try
        {
            int count;
            var lst = DataRepository.UnitsProvider.GetPaged("IsDisabled = 'False'"
                                                            , "PriorityIndex ASC", 0,
                                                            ServiceFacade.SettingsHelper.GetPagedLength, out count)
                .Select(x => new
                                 {
                                     x.Id,
                                     x.Title
                                 });
            StrFloors = JsonConvert.SerializeObject(lst);

            rptFloor.DataSource = lst;
            rptFloor.DataBind();
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }

    /// <summary>
    /// Get status list
    /// </summary>
    private void BindStatus()
    {
        try
        {
            int count;
            var lst = DataRepository.StatusProvider.GetPaged("IsDisabled = 'False'"
                                                            , "PriorityIndex ASC", 0,
                                                            ServiceFacade.SettingsHelper.GetPagedLength, out count);

            cboStatus.DataSource = lst;
            cboStatus.DataTextField = "Title";
            cboStatus.DataValueField = "Id";
            cboStatus.DataBind();

            lst.Add(new Status
            {
                Title = "Out of date",
                ColorCode = ServiceFacade.SettingsHelper.CompleteColor
            });
            rptStatus.DataSource = lst;
            rptStatus.DataBind();
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }
    #endregion

    #region Patient
    /// <summary>
    /// Search patient by keyword
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static string SearchPatient()
    {
        var keyword = HttpContext.Current.Request["q"];
        var lstTk = new List<JPatientToken>();
        try
        {
            int count;
            var lst = DataRepository.PatientProvider.GetPaged(String.Format("FirstName LIKE '%{0}%' OR LastName LIKE '%{0}%'" +
                                                                             " OR Address LIKE '%{0}%' OR HomePhone LIKE '%{0}%'" +
                                                                             " OR WorkPhone LIKE '%{0}%' OR CellPhone LIKE '%{0}%'" +
                                                                             " OR Birthdate LIKE '%{0}%'", keyword)
                , string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);

            lstTk = lst.Select(x => new JPatientToken()
            {
                Id = x.Id,
                id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address = x.Address,
                HomePhone = x.HomePhone,
                WorkPhone = x.WorkPhone,
                CellPhone = x.CellPhone,
                Birthdate = x.Birthdate == null ? string.Empty : ((DateTime)x.Birthdate).ToString("dd-MM-yyyy"),
                Title = x.Title,
                IsFemale = x.IsFemale,
                Note = x.Note,
                propertyToSearch = ParsePatientInfo(x)
            }).ToList();
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }

        return JsonConvert.SerializeObject(lstTk);
    }

    /// <summary>
    /// Parse data to string for property search of token input
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static string ParsePatientInfo(Patient obj)
    {
        string strResult = string.Empty;
        try
        {
            strResult = String.Format("{0}{1} {2}. {3}{4}{5}{6}{7}"
                          , string.IsNullOrEmpty(obj.Title) ? string.Empty : String.Format("{0}. ", obj.Title)
                          , obj.FirstName, obj.LastName
                          ,
                          obj.Birthdate == null
                              ? string.Empty
                              : String.Format("Birthday: {0}. ", ((DateTime)obj.Birthdate).ToString("dd-MM-yyyy"))
                          ,
                          string.IsNullOrEmpty(obj.HomePhone)
                              ? string.Empty
                              : String.Format("Home Phone: {0}. ", obj.HomePhone)
                          ,
                          string.IsNullOrEmpty(obj.CellPhone)
                              ? string.Empty
                              : String.Format("Cell Phone: {0}. ", obj.CellPhone)
                          ,
                          string.IsNullOrEmpty(obj.WorkPhone)
                              ? string.Empty
                              : String.Format("Work Phone: {0}. ", obj.WorkPhone)
                          , string.IsNullOrEmpty(obj.Address) ? string.Empty : String.Format("Address: {0}. ", obj.Address)
                );
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
        return strResult;
    }
    #endregion

    #region Doctor
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
            return SearchDoctor(HttpContext.Current.Request["q"], HttpContext.Current.Request["appointmentId"]
                , HttpContext.Current.Request["fromTime"], HttpContext.Current.Request["toTime"]
                , HttpContext.Current.Request["fromDate"], HttpContext.Current.Request["toDate"]);
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
    /// <param name="appointmentId"> </param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    private static string SearchDoctor(string keyword, string appointmentId, string startTime, string endTime, string startDate, string endDate)
    {
        string result = string.Empty;

        try
        {
            // Get start time and end time
            int intStartHour = Convert.ToInt32(startTime.Split(':')[0]);
            int intStartMinute = Convert.ToInt32(startTime.Split(':')[1]);
            int intEndHour = Convert.ToInt32(endTime.Split(':')[0]);
            int intEndMinute = Convert.ToInt32(endTime.Split(':')[1]);

            // Get start date and end date
            DateTime dtStart = Convert.ToDateTime(startDate);
            DateTime dtEnd = Convert.ToDateTime(endDate);

            dtStart = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, intStartHour, intStartMinute, 0);
            dtEnd = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, intEndHour, intEndMinute, 0);

            var cmd = new SqlCommand { CommandText = "GetAvailableStaffsForAppointment", CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add(new SqlParameter("@AppointmentId", appointmentId));
            cmd.Parameters.Add(new SqlParameter("@Keyword", keyword));
            cmd.Parameters.Add(new SqlParameter("@StartTime", dtStart));
            cmd.Parameters.Add(new SqlParameter("@EndTime", dtEnd));
            cmd.CommandTimeout = 0;

            DataSet ds = DataRepository.Provider.ExecuteDataSet(cmd);
            DataTable objTable = ds.Tables[0];

            if (objTable == null) goto StepResult;

            var lst = (from DataRow dr in objTable.Rows
                       select new JDoctorToken()
                       {
                           id = dr["DoctorUserName"].ToString(),
                           ShortName = dr["DoctorShortName"].ToString(),
                           propertyToSearch = String.Format("Dr. {0}. {1}", dr["DoctorShortName"], String.Format("Specialty: {0}. ", dr["FuncTitle"]))
                       }).ToList();

            result = JsonConvert.SerializeObject(lst);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            result = string.Empty;
        }
    StepResult:
        return result;
    }
    #endregion

    #region Room
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static string SearchRoom()
    {
        try
        {
            return SearchRoom(HttpContext.Current.Request["q"], HttpContext.Current.Request["appointmentId"]
                , HttpContext.Current.Request["doctorUserName"]
                , HttpContext.Current.Request["fromTime"], HttpContext.Current.Request["toTime"]
                , HttpContext.Current.Request["fromDate"], HttpContext.Current.Request["toDate"]);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }

        return string.Empty;
    }

    /// <summary>
    /// Search room by condition
    /// </summary>
    /// <param name="keyword"> </param>
    /// <param name="appointmentId"></param>
    /// <param name="doctorUserName"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    private static string SearchRoom(string keyword, string appointmentId, string doctorUserName
        , string startTime, string endTime, string startDate, string endDate)
    {
        string result = string.Empty;

        try
        {
            // Get start time and end time
            int intStartHour = Convert.ToInt32(startTime.Split(':')[0]);
            int intStartMinute = Convert.ToInt32(startTime.Split(':')[1]);
            int intEndHour = Convert.ToInt32(endTime.Split(':')[0]);
            int intEndMinute = Convert.ToInt32(endTime.Split(':')[1]);

            // Get start date and end date
            DateTime dtStart = Convert.ToDateTime(startDate);
            DateTime dtEnd = Convert.ToDateTime(endDate);

            dtStart = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, intStartHour, intStartMinute, 0);
            dtEnd = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, intEndHour, intEndMinute, 0);

            var cmd = new SqlCommand
            {
                CommandText = "GetAvailableRoomsForAppointment",
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@AppointmentId", appointmentId));
            cmd.Parameters.Add(new SqlParameter("@DoctorUserName", doctorUserName));
            cmd.Parameters.Add(new SqlParameter("@StartTime", dtStart));
            cmd.Parameters.Add(new SqlParameter("@EndTime", dtEnd));
            cmd.Parameters.Add(new SqlParameter("@Keyword", keyword));
            cmd.CommandTimeout = 0;

            DataSet ds = DataRepository.Provider.ExecuteDataSet(cmd);
            DataTable objTable = ds.Tables[0];

            if (objTable == null) goto StepResult;

            var lst = (from DataRow dr in objTable.Rows
                       select new JRoomToken()
                       {
                           id = dr["Id"].ToString(),
                           PriorityIndex = Convert.ToInt32(dr["PriorityIndex"].ToString()),
                           propertyToSearch = dr["Title"].ToString()
                       }).ToList();
            lst.Sort((p1, p2) => p2.PriorityIndex.CompareTo(p1.PriorityIndex));

            result = JsonConvert.SerializeObject(lst);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            result = string.Empty;
        }
    StepResult:
        return result;
    }

    /// <summary>
    /// Get a room info
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetRoom(string roomId)
    {
        try
        {
            Room obj = DataRepository.RoomProvider.GetById(Convert.ToInt32(roomId));
            if (obj == null || obj.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Cannot find room.");
            }

            return WebCommon.BuildSuccessfulResult(new List<object>
                                                       {
                                                           new
                                                               {
                                                                   id = obj.Id,
                                                                   propertyToSearch = obj.Title
                                                               }
                                                       }
                );
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }
    #endregion

    #region Appointment
    /// <summary>
    /// Get a room info
    /// </summary>
    /// <param name="appointmentId"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetAppointment(string appointmentId)
    {
        try
        {
            var obj = DataRepository.AppointmentProvider.GetById(appointmentId);
            if (obj == null)
            {
                return WebCommon.BuildFailedResult("Cannot find appointment");
            }

            // Deep load appointment
            DataRepository.AppointmentProvider.DeepLoad(obj);

            // Get Specialty [Functionality]
            var lstDoctorService = DataRepository.DoctorServiceProvider.GetByDoctorId(obj.Id);
            DataRepository.DoctorServiceProvider.DeepLoad(lstDoctorService);
            Services service = null;
            if (lstDoctorService.Any()) service = lstDoctorService.First().ServiceIdSource;

            return WebCommon.BuildSuccessfulResult(new List<object>
                                                       {
                                                           new
                                                               {
                                                                   obj.Id,
                                                                   obj.PatientId,
                                                                   PatientInfo = ParsePatientInfo(obj.PatientIdSource),
                                                                   DoctorUserName = obj.DoctorIdSource.Username,
                                                                   DoctorInfo = String.Format("Dr. {0}. {1}"
                                                                                              ,
                                                                                              obj.DoctorIdSource.
                                                                                                  DisplayName,
                                                                                              service == null
                                                                                                  ? string.Empty
                                                                                                  : String.Format(
                                                                                                      "Specialty: {0}. ",
                                                                                                      service.Title)),
                                                                   obj.RoomId,
                                                                   RoomInfo = obj.RoomIdSource.Title,
                                                                   note = obj.Note
                                                               }
                                                       }
                );
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }

    /// <summary>
    /// Build Appointment to returned object
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static object BuildAppointment(Appointment obj)
    {
        try
        {
            // Load all relational info
            DataRepository.AppointmentProvider.DeepLoad(obj);

            return new
                       {
                           id = obj.Id,
                           start_date = obj.StartTime.Value.ToString("dd-MM-yyyy HH:mm"),
                           end_date = obj.EndTime.Value.ToString("dd-MM-yyyy HH:mm"),
                           section_id = obj.RoomId,
                           text =
                               String.Format("Patient: {3}. {0} {1}.<br />Note: {2}", obj.PatientIdSource.FirstName,
                                             obj.PatientIdSource.LastName,
                                             obj.Note, obj.PatientIdSource.Title),
                           DoctorUsername = obj.DoctorIdSource.Username,
                           DoctorDisplayname = obj.DoctorIdSource.DisplayName,
                           obj.PatientId,
                           PatientName = obj.PatientIdSource.FirstName,
                           obj.ServicesId,
                           ServicesTitle = obj.ServicesIdSource.Title,
                           obj.RoomId,
                           RoomTitle = obj.RoomIdSource.Title,
                           note = obj.Note,
                           ReadOnly = (obj.StartTime <= DateTime.Now),
                           color = obj.StatusIdSource.ColorCode,
                           isnew = false
                       };
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return null;
        }
    }

    /// <summary>
    /// Build object to string result
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static List<object> BuildListAppointment(Appointment obj)
    {
        var lstEvent = new List<object>();
        try
        {
            // If object is null, stop
            if (obj == null)
                return lstEvent;

            // Load all relational info
            DataRepository.AppointmentProvider.DeepLoad(obj);

            if (obj.StartTime != null && obj.EndTime != null)
                lstEvent.Add(BuildAppointment(obj));
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
        return lstEvent;
    }

    /// <summary>
    /// Build object to string result
    /// </summary>
    /// <param name="lst"></param>
    /// <returns></returns>
    private static List<object> BuildListAppointment(TList<Appointment> lst)
    {
        var lstEvent = new List<object>();
        try
        {
            // Load all relational info
            DataRepository.AppointmentProvider.DeepLoad(lst);

            lstEvent.AddRange(from appointment in lst where appointment != null && appointment.StartTime == null && appointment.EndTime == null select BuildAppointment(appointment));
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
        return lstEvent;
    }
    #endregion

    #region Appointment Methods
    /// <summary>
    /// Add appointment to list
    /// </summary>
    /// <param name="lst"></param>
    /// <param name="item"></param>
    private static void AddAppointment(List<object> lst, Appointment item)
    {
        lst.Add(BuildAppointment(item));
    }

    /// <summary>
    /// Add appointment to list
    /// </summary>
    /// <param name="lst"></param>
    /// <param name="lstItem"></param>
    private static void AddAppointment(List<object> lst, IEnumerable<Appointment> lstItem)
    {
        lst.AddRange(lstItem.Select(appointment => BuildAppointment(appointment)));
    }

    #endregion

    #region "Roster"
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string SaveEvent(string PatientId, string Note, string StartTime, string EndTime, string StartDate, string EndDate
        , string DoctorUsername, string RoomId, string Status)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {

            #region "Validate"
            // Check Patient
            if (string.IsNullOrEmpty(PatientId))
            {
                return WebCommon.BuildFailedResult("You must choose patient.");
            }

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

            // If appointment From Date >= To Date
            if (dtStart >= dtEnd)
            {
                return WebCommon.BuildFailedResult("From date, time must be less than to date, time.");
            }

            // If appointment is created in a passed or current day
            DateTime dtNow = DateTime.Now;
            if (dtNow >= dtStart)
            {
                return WebCommon.BuildFailedResult("You can not change roster to passed or current date.");
            }

            // Check Doctor
            if (string.IsNullOrEmpty(DoctorUsername))
            {
                return WebCommon.BuildFailedResult("You must choose doctor.");
            }

            // Check Room
            if (string.IsNullOrEmpty(RoomId))
            {
                return WebCommon.BuildFailedResult("You must choose room.");
            }

            // Check Status
            int statusId;
            if (!Int32.TryParse(Status, out statusId))
            {
                return WebCommon.BuildFailedResult("You must choose status.");
            }
            #endregion

            string strPatientId = PatientId;
            int roomId = Convert.ToInt32(RoomId);

            tm.BeginTransaction();

            #region "Check infomation"
            // Check exists Patient
            Patient objPatient = DataRepository.PatientProvider.GetById(strPatientId);
            if (objPatient == null || objPatient.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Patient is not exist.");
            }

            // Check exists Doctor
            Users objDoctor = DataRepository.UsersProvider.GetByUsername(DoctorUsername);
            if (objDoctor == null || objDoctor.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Doctor is not exist.");
            }

            // Check exists Room
            Room objRoom = DataRepository.RoomProvider.GetById(roomId);
            if (objRoom == null || objRoom.IsDisabled)
            {
                tm.Rollback();
                return WebCommon.BuildFailedResult("Room is not exist.");
            }

            // Check exists Room
            Status objStatus = DataRepository.StatusProvider.GetById(statusId);
            if (objStatus == null || objStatus.IsDisabled)
            {
                tm.Rollback();
                return WebCommon.BuildFailedResult("Status is not exist.");
            }
            #endregion

            #region "Check Conflict"
            int intCompareValue = 0;
            // Check conflict Patient
            if (!CheckConflict("CustomerId", strPatientId, string.IsNullOrEmpty(objPatient.Title) ? "patient" : objPatient.Title + ".",
                String.Format("{0} {1}", objPatient.FirstName, objPatient.LastName), dtStart, dtEnd, intCompareValue, ref _message))
            {
                tm.Rollback();
                return WebCommon.BuildFailedResult(_message);
            }

            // Check conflict Doctor
            if (!CheckConflict("DoctorUserName", DoctorUsername, "Dr.", objDoctor.DisplayName, dtStart, dtEnd, intCompareValue, ref _message))
            {
                tm.Rollback();
                return WebCommon.BuildFailedResult(_message);
            }

            // Check conflict Room
            if (!CheckConflict("RoomId", roomId.ToString(), "Room", objRoom.Title, dtStart, dtEnd, intCompareValue, ref _message))
            {
                tm.Rollback();
                return WebCommon.BuildFailedResult(_message);
            }
            #endregion

            #region "Insert Appointment"
            var newObj = new Appointment();

            string perfix = "A" + ServiceFacade.SettingsHelper.AppointmentPrefix + DateTime.Now.ToString("yyMMdd");
            int count;
            TList<Appointment> objPo = DataRepository.AppointmentProvider.GetPaged(tm, "Id like '" + perfix + "' + '%'", "Id desc", 0, 1, out count);
            if (count == 0)
                newObj.Id = perfix + "001";
            else
            {
                newObj.Id = perfix + String.Format("{0:000}", int.Parse(objPo[0].Id.Substring(objPo[0].Id.Length - 3)) + 1);
            }

            newObj.PatientId = strPatientId;
            newObj.DoctorId = objDoctor.Id;
            newObj.RoomId = roomId;
            newObj.Note = Note;
            newObj.StatusId = statusId;
            newObj.StartTime = dtStart;
            newObj.EndTime = dtEnd;
            newObj.CreateUser = Username;
            newObj.UpdateUser = Username;

            DataRepository.AppointmentProvider.Insert(tm, newObj);
            #endregion

            #region "Send Appointment"
            string strSubject = String.Format("New Appointment: {0} - {1}. {2} {3}", newObj.ServicesIdSource.Title
            , newObj.PatientIdSource.Title, newObj.PatientIdSource.FirstName, newObj.PatientIdSource.LastName);
            string strBody = String.Format("You have a new appointment");
            string strSummary = string.Empty;
            string strDescription = string.Empty;
            string strLocation = String.Format("Room {0}", newObj.RoomIdSource.Title);
            string strAlarmSummary = String.Format("{0} minutes left for appointment with {1}. {2} {3}",
                ServiceFacade.SettingsHelper.TimeLeftRemindAppointment, newObj.PatientIdSource.Title
                , newObj.PatientIdSource.FirstName, newObj.PatientIdSource.LastName);
            var objMail = new MailAppointment(strSubject, strBody, strSummary, strDescription, strLocation, strAlarmSummary,
                Convert.ToDateTime(newObj.StartTime), Convert.ToDateTime(newObj.EndTime));
            objMail.AddMailAddress(newObj.DoctorIdSource.Email, newObj.DoctorIdSource.DisplayName);
            objMail.SendMail();
            #endregion

            tm.Commit();
            return WebCommon.BuildSuccessfulResult(BuildListAppointment(newObj));
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            tm.Rollback();
            return WebCommon.BuildFailedResult("Cannot create appointment");
        }
    }

    #region "Update Roster"
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string UpdateEventSave(string Id, string PatientId, string Note,
        string StartTime, string EndTime, string StartDate, string EndDate, string DoctorUsername, string RoomId, string Status)
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

            return UpdateEvent(Id, PatientId, Note, dtStart, dtEnd, DoctorUsername, RoomId, Status);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string MoveAppointment(string id, string startTime, string endTime, string aGroupId)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            Appointment appointment = DataRepository.AppointmentProvider.GetById(id);
            if (appointment == null || appointment.IsComplete || appointment.IsDisabled)
            {
                return WebCommon.BuildFailedResult("There is no appointment to change or appointment is expired.");
            }
            
            // Load relational info
            DataRepository.AppointmentProvider.DeepLoad(appointment);

            // Declare list of object are returned
            var lstResult = new List<object>();

            // Validate if roster's expired
            // return message with roster, this will be used to rollback roster to previous location, before moved
            if (appointment.IsDisabled)
            {
                AddAppointment(lstResult, appointment);
                return WebCommon.BuildFailedResult("Appointment is expired.", lstResult);
            }

            // Validate current user have any right to operate this action
            // Validate user right for reading
            if (!RightAccess.CheckUserRight(Username, ScreenCode, OperationConstant.Update.Key, out _message))
            {
                AddAppointment(lstResult, appointment);
                return WebCommon.BuildFailedResult(_message, lstResult);
            }

            // Check AppointmentGroupId
            if (string.IsNullOrEmpty(aGroupId))
            {
                AddAppointment(lstResult, appointment);
                return WebCommon.BuildFailedResult("You must choose appointment group.", lstResult);
            }

            // Validate AppointmentGroupId
            int appointmentGroupId;
            if (!Int32.TryParse(aGroupId, out appointmentGroupId))
            {
                AddAppointment(lstResult, appointment);
                return WebCommon.BuildFailedResult("Appointment group is invalid.", lstResult);
            }

            #region "Check infomation"
            // Check exists Patient
            var appointmentGroup = DataRepository.AppointmentGroupProvider.GetById(appointmentGroupId);
            if (appointmentGroup == null || appointmentGroup.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Appointment group is not exist.");
            }
            #endregion

            // Get start date and end date
            DateTime dtStart = Convert.ToDateTime(startTime);
            DateTime dtEnd = Convert.ToDateTime(endTime);

            // If roster is created in a passed or current day
            if (DateTime.Now >= dtStart)
            {
                AddAppointment(lstResult, appointment);
                return WebCommon.BuildFailedResult("You can not change roster to passed or current date.", lstResult);
            }

            if (dtStart >= dtEnd)
            {
                AddAppointment(lstResult, appointment);
                return WebCommon.BuildFailedResult("To time must be greater than from date.");
            }

            #region "Check Conflict"
            int intCompareValue = 1;
            // Check conflict Patient
            if (!CheckConflict("PatientId", appointment.PatientId, "",
                String.Format("{2}. {0} {1}", appointment.PatientIdSource.FirstName, appointment.PatientIdSource.LastName, appointment.PatientIdSource.Title)
                , dtStart, dtEnd, intCompareValue, ref _message))
            {
                tm.Rollback();
                return WebCommon.BuildFailedResult(_message);
            }

            // Check conflict Doctor
            if (!CheckConflict("DoctorId", appointment.DoctorId, "Doctor", appointment.DoctorIdSource.DisplayName, dtStart, dtEnd, intCompareValue, ref _message))
            {
                tm.Rollback();
                return WebCommon.BuildFailedResult(_message);
            }

            // Check conflict Room
            if (!CheckConflict("RoomId", appointment.RoomId.ToString(), "Room", appointment.RoomIdSource.Title, dtStart, dtEnd, intCompareValue, ref _message))
            {
                tm.Rollback();
                return WebCommon.BuildFailedResult(_message);
            }
            #endregion

            #region "Update Appointment"
            Appointment obj = DataRepository.AppointmentProvider.GetById(id);
            if (obj == null || obj.IsComplete || obj.IsDisabled)
            {
                return WebCommon.BuildFailedResult("There is no appointment to change or appointment is expired.");
            }

            obj.AppointmentGroupId = appointmentGroupId;
            obj.StartTime = dtStart;
            obj.EndTime = dtEnd;
            obj.UpdateUser = Username;
            obj.UpdateDate = DateTime.Now;

            DataRepository.AppointmentProvider.Save(tm, obj);
            #endregion

            #region "Send Appointment"
            string strSubject = String.Format("Change Appointment: {0} - {1}. {2} {3}", obj.ServicesIdSource.Title
                , obj.PatientIdSource.Title, obj.PatientIdSource.FirstName, obj.PatientIdSource.LastName);
            string strBody = String.Format("You have a new change for appointment");
            string strSummary = string.Empty;
            string strDescription = string.Empty;
            string strLocation = String.Format("Room {0}", obj.RoomIdSource.Title);
            string strAlarmSummary = String.Format("{0} minutes left for appointment with {1}. {2} {3}",
                ServiceFacade.SettingsHelper.TimeLeftRemindAppointment, obj.PatientIdSource.Title
                , obj.PatientIdSource.FirstName, obj.PatientIdSource.LastName);
            var objMail = new MailAppointment(strSubject, strBody, strSummary, strDescription, strLocation, strAlarmSummary,
                Convert.ToDateTime(obj.StartTime), Convert.ToDateTime(obj.EndTime));
            objMail.AddMailAddress(obj.DoctorIdSource.Email, obj.DoctorIdSource.DisplayName);
            if (!objMail.SendMail())
            {

            }
            #endregion

            tm.Commit();
            return WebCommon.BuildSuccessfulResult(BuildListAppointment(obj));
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            tm.Rollback();
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }

    /// <summary>
    /// Update an appointment
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="PatientId"></param>
    /// <param name="ContentId"></param>
    /// <param name="Note"></param>
    /// <param name="StartTime"></param>
    /// <param name="EndTime"></param>
    /// <param name="DoctorUsername"></param>
    /// <param name="RoomId"></param>
    /// <returns></returns>
    private static string UpdateEvent(string Id, string PatientId, string Note, DateTime StartTime, DateTime EndTime,
        string DoctorUsername, string RoomId, string Status)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            #region "Validate"
            // Check Patient
            if (string.IsNullOrEmpty(PatientId) || PatientId == "")
            {
                return WebCommon.BuildFailedResult("You must choose patient.");
            }

            // Get start date and end date
            DateTime dtStart = StartTime;
            DateTime dtEnd = EndTime;

            // If appointment From Date >= To Date
            if (dtStart >= dtEnd)
            {
                return WebCommon.BuildFailedResult("From date, time must be less than to date, time.");
            }

            // If appointment is created in a passed or current day
            DateTime dtNow = DateTime.Now;
            if (dtNow >= dtStart)
            {
                return WebCommon.BuildFailedResult("You can not change roster to passed or current date.");
            }

            // Check Doctor
            if (string.IsNullOrEmpty(DoctorUsername) || DoctorUsername == "")
            {
                return WebCommon.BuildFailedResult("You must choose doctor.");
            }

            // Check Room
            if (string.IsNullOrEmpty(RoomId) || RoomId == "")
            {
                return WebCommon.BuildFailedResult("You must choose room.");
            }

            // Check Status
            int statusId;
            if (!Int32.TryParse(Status, out statusId))
            {
                return WebCommon.BuildFailedResult("You must choose status.");
            }
            #endregion

            string strPatientId = PatientId;
            int roomId = Convert.ToInt32(RoomId);

            tm.BeginTransaction();

            #region "Check infomation"
            // Check exists Patient
            Patient objPatient = DataRepository.PatientProvider.GetById(strPatientId);
            if (objPatient == null || objPatient.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Patient is not exist.");
            }

            // Check exists Doctor
            Users objDoctor = DataRepository.UsersProvider.GetByUsername(DoctorUsername);
            if (objDoctor == null || objDoctor.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Doctor is not exist.");
            }

            // Check exists Room
            Room objRoom = DataRepository.RoomProvider.GetById(roomId);
            if (objRoom == null || objRoom.IsDisabled)
            {
                tm.Rollback();
                return WebCommon.BuildFailedResult("Room is not exist.");
            }

            // Check exists Room
            Status objStatus = DataRepository.StatusProvider.GetById(statusId);
            if (objStatus == null || objStatus.IsDisabled)
            {
                tm.Rollback();
                return WebCommon.BuildFailedResult("Status is not exist.");
            }
            #endregion

            #region "Check Conflict"
            int intCompareValue = 1;
            // Check conflict Patient
            if (!CheckConflict("CustomerId", strPatientId, "",
                String.Format("{2}. {0} {1}", objPatient.FirstName, objPatient.LastName, objPatient.Title)
                , dtStart, dtEnd, intCompareValue, ref _message))
            {
                tm.Rollback();
                return WebCommon.BuildFailedResult(_message);
            }

            // Check conflict Doctor
            if (!CheckConflict("DoctorId", DoctorUsername, "Doctor", objDoctor.DisplayName, dtStart, dtEnd, intCompareValue, ref _message))
            {
                tm.Rollback();
                return WebCommon.BuildFailedResult(_message);
            }

            // Check conflict Room
            if (!CheckConflict("RoomId", roomId.ToString(), "Room", objRoom.Title, dtStart, dtEnd, intCompareValue, ref _message))
            {
                tm.Rollback();
                return WebCommon.BuildFailedResult(_message);
            }
            #endregion

            #region "Update Appointment"
            Appointment obj = DataRepository.AppointmentProvider.GetById(Id);
            if (obj == null || obj.IsComplete || obj.IsDisabled)
            {
                return WebCommon.BuildFailedResult("There is no appointment to change or appointment is expired.");
            }
            DataRepository.AppointmentProvider.DeepLoad(obj);

            obj.PatientId = strPatientId;
            obj.DoctorId = objDoctor.Id;
            obj.RoomId = roomId;
            obj.Note = Note;
            obj.StatusId = statusId;
            obj.StartTime = dtStart;
            obj.EndTime = dtEnd;
            obj.UpdateUser = Username;
            obj.UpdateDate = DateTime.Now;

            DataRepository.AppointmentProvider.Save(tm, obj);
            #endregion

            #region "Send Appointment"
            string strSubject = String.Format("Change Appointment: {0} - {1}. {2} {3}", obj.ServicesIdSource.Title
                , obj.PatientIdSource.Title, obj.PatientIdSource.FirstName, obj.PatientIdSource.LastName);
            string strBody = String.Format("You have a new change for appointment");
            string strSummary = string.Empty;
            string strDescription = string.Empty;
            string strLocation = String.Format("Room {0}", obj.RoomIdSource.Title);
            string strAlarmSummary = String.Format("{0} minutes left for appointment with {1}. {2} {3}",
                ServiceFacade.SettingsHelper.TimeLeftRemindAppointment, obj.PatientIdSource.Title
                , obj.PatientIdSource.FirstName, obj.PatientIdSource.LastName);
            var objMail = new MailAppointment(strSubject, strBody, strSummary, strDescription, strLocation, strAlarmSummary,
                Convert.ToDateTime(obj.StartTime), Convert.ToDateTime(obj.EndTime));
            objMail.AddMailAddress(obj.DoctorIdSource.Email, obj.DoctorIdSource.DisplayName);
            if (!objMail.SendMail())
            {

            }
            #endregion

            tm.Commit();
            return WebCommon.BuildSuccessfulResult(BuildListAppointment(obj));
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            tm.Rollback();
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }
    #endregion

    /// <summary>
    /// Get list of appointment in day
    /// </summary>
    /// <param name="mode">Floor Id</param>
    /// <param name="currentDateView">Date</param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetAppointments(string mode, string currentDateView)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            tm.BeginTransaction();

            // Check active user
            Users objDoctor = DataRepository.UsersProvider.GetByUsername(Username);
            if (objDoctor == null || objDoctor.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Doctor is not exist.");
            }

            #region "Load appointment"
            // Get datetime to filter
            DateTime fromDate = Convert.ToDateTime(currentDateView);
            fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 0, 0, 0);
            var toDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 23, 59, 59);
            int count;

            // Get appointment
            TList<Appointment> lstAppt = DataRepository.AppointmentProvider.GetPaged(String.Format("StartTime BETWEEN N'{0}' AND N'{1}'"
                , fromDate.ToString("yyyy-MM-dd HH:mm:ss.000"), toDate.ToString("yyyy-MM-dd HH:mm:ss.000")), string.Empty, 0, 10000, out count);
            //TList<Appointment> lstAppt = DataRepository.AppointmentProvider.GetPaged(0, 10000, out count); // Good performance later

            // Set out of date for passed appointment
            //lstAppt.ForEach(x =>
            //{
            //    x.ColorCode = x.StartTime <= DateTime.Now
            //                      ? ServiceFacade.SettingsHelper.CompleteColor
            //                      : x.ColorCode;
            //});
            DataRepository.AppointmentProvider.Save(lstAppt);
            #endregion

            tm.Commit();
            return WebCommon.BuildSuccessfulResult(BuildListAppointment(lstAppt));
        }
        catch (Exception ex)
        {
            tm.Rollback();
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string DeleteEvent(string Id)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            tm.BeginTransaction();

            Appointment obj = DataRepository.AppointmentProvider.GetById(Id);
            if (obj == null || obj.IsComplete || obj.IsDisabled)
            {
                return WebCommon.BuildFailedResult("There is no appointment to change or appointment is expired.");
            }

            obj.IsDisabled = true;
            obj.UpdateUser = Username;
            obj.UpdateDate = DateTime.Now;

            DataRepository.AppointmentProvider.Save(tm, obj);

            tm.Commit();
            return WebCommon.BuildSuccessfulResult();
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            tm.Rollback();
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }
    #endregion

    #region "Function"
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetContents()
    {
        string result = string.Empty;

        //try
    //{
    //    TList<Content> lstObj = DataRepository.ContentProvider.GetByIsDisabled(false);

        //    result = @"{'key': '-1', 'label': 'Select Content'}";
    //    if (lstObj.Count == 0)
    //    {
    //        result = @"[{ 'result': 'true', 'message': 'There is no content. Please contact Administrator.', 'data': [" + result + "] }]";
    //        goto StepResult;
    //    }

        //    lstObj.Sort("FuncTitle ASC, Title ASC");
    //    foreach (Content item in lstObj)
    //    {
    //        result += @", {'key' : '" + item.FuncId + ChrSeperateStaff.ToString() + item.Id.ToString() + @"'" + "," + @"'label' : '"
    //            + item.FuncTitle + StrSeperateStaff + item.Title + @"'}";
    //    }

        //    result = @"[{ 'result': 'true', 'message': '', 'data':[" + result + @"] }]";
    //    goto StepResult;
    //}
    //catch (Exception ex)
    //{
    //    //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.GetContents", ex);

        //    result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
    //    goto StepResult;
    //}
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

        //try
        //{
        //    TList<DoctorFunc> lstDrFu = DataRepository.DoctorFuncProvider.GetByDoctorUserNameIsDisabled(DoctorUserName, false);
        //    TList<Content> lstObj = DataRepository.ContentProvider.GetByIsDisabled(false);

        //    if (lstObj.Count == 0)
        //    {
        //        result = @"[{ 'result': 'true', 'message': 'There is no content. Please contact Administrator.', 'data': [] }]";
        //        goto StepResult;
        //    }

        //    // Lay dong dau tien
        //    lstObj.Sort("FuncTitle ASC, Title ASC");
        //    foreach (Content item in lstObj)
        //    {
        //        foreach (DoctorFunc itemDr in lstDrFu)
        //        {
        //            if (item.FuncId == itemDr.FuncId)
        //            {
        //                result = item.FuncId.ToString() + ChrSeperateStaff.ToString() + item.Id.ToString();
        //                goto StepString;
        //            }
        //        }
        //    }

        //StepString:
        //    result = @"[{ 'result': 'true', 'message': '', 'data':['" + result + @"'] }]";
        //    goto StepResult;
        //}
        //catch (Exception ex)
        //{
        //    //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.GetCurrentContents", ex);

        //    result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
        //    goto StepResult;
        //}
        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string CreateSimplePatient(string FirstName, string LastName, string CellPhone, string Address, string IsFemale)
    {
        try
        {
            #region "Validate"
            if (string.IsNullOrEmpty(FirstName))
            {
                return WebCommon.BuildFailedResult("Please input First name.");
            }
            if (string.IsNullOrEmpty(LastName))
            {
                return WebCommon.BuildFailedResult("Please input Last name.");
            }
            #endregion

            #region "Insert"
            var newObj = new Patient();
            bool blIsFemale = Convert.ToBoolean(IsFemale);
            string perfix = "C" + ServiceFacade.SettingsHelper.PatientPrefix + DateTime.Now.ToString("yyMMdd");
            int count;
            TList<Patient> objPo = DataRepository.PatientProvider.GetPaged("Id like '" + perfix + "' + '%'", "Id desc", 0, 1, out count);
            if (count == 0)
                newObj.Id = perfix + "001";
            else
            {
                newObj.Id = perfix + String.Format("{0:000}", int.Parse(objPo[0].Id.Substring(objPo[0].Id.Length - 3)) + 1);
            }

            newObj.FirstName = FirstName;
            newObj.LastName = LastName;
            newObj.CellPhone = CellPhone;
            newObj.Address = Address;
            newObj.IsFemale = blIsFemale;
            newObj.CreateUser = Username;
            newObj.UpdateUser = Username;

            DataRepository.PatientProvider.Insert(newObj);

            var patient = new
            {
                id = newObj.Id,
                propertyToSearch = String.Format("{0}{1} {2}. {3}{4}{5}{6}{7}"
                                                 ,
                                                 string.IsNullOrEmpty(newObj.Title)
                                                     ? string.Empty
                                                     : String.Format("{0}. ", newObj.Title)
                                                 , newObj.FirstName, newObj.LastName
                                                 ,
                                                 newObj.Birthdate == null
                                                     ? string.Empty
                                                     : String.Format("Birthday: {0}. ",
                                                                     ((DateTime)newObj.Birthdate).ToString
                                                                         ("dd-MM-yyyy"))
                                                 ,
                                                 string.IsNullOrEmpty(newObj.HomePhone)
                                                     ? string.Empty
                                                     : String.Format("Home Phone: {0}. ", newObj.HomePhone)
                                                 ,
                                                 string.IsNullOrEmpty(newObj.CellPhone)
                                                     ? string.Empty
                                                     : String.Format("Cell Phone: {0}. ", newObj.CellPhone)
                                                 ,
                                                 string.IsNullOrEmpty(newObj.WorkPhone)
                                                     ? string.Empty
                                                     : String.Format("Work Phone: {0}. ", newObj.WorkPhone)
                                                 ,
                                                 string.IsNullOrEmpty(newObj.Address)
                                                     ? string.Empty
                                                     : String.Format("Address: {0}. ", newObj.Address)
                    )
            };
            #endregion

            return WebCommon.BuildSuccessfulResult(new List<object> { patient });
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult("Cannot create new patient. Please try again.");
        }
    }

    /// <summary>
    /// Get Patient's info by AppointmentId
    /// </summary>
    /// <param name="appointmentId"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetPatientByAppointmentId(string appointmentId)
    {
        try
        {
            // If there is no appointment, return
            if (appointmentId == null)
            {
                return WebCommon.BuildFailedResult("There is no appointment.");
            }

            // Get appointment by id
            var objAppt = DataRepository.AppointmentProvider.GetById(appointmentId);

            // If there is no appointment, return
            if (objAppt == null)
            {
                return WebCommon.BuildFailedResult("Appointment is not existed.");
            }

            // Check exists Patient
            Patient objPatient = DataRepository.PatientProvider.GetById(objAppt.PatientId);
            if (objPatient == null || objPatient.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Patient is not exist.");
            }

            var lstPatient = new List<object>
                                 {
                                     new
                                         {
                                             Id = objPatient.Id,
                                             FirstName = objPatient.FirstName ?? "&nbsp;",
                                             LastName = objPatient.LastName ?? "&nbsp;",
                                             Address = objPatient.Address ?? "&nbsp;",
                                             HomePhone = objPatient.HomePhone ?? "&nbsp;",
                                             WorkPhone = objPatient.WorkPhone ?? "&nbsp;",
                                             CellPhone = objPatient.CellPhone ?? "&nbsp;",
                                             Birthdate =
                                         objPatient.Birthdate == null
                                             ? "&nbsp;"
                                             : ((DateTime) objPatient.Birthdate).ToString("MM-dd-yyyy"),
                                             IsFemale = objPatient.IsFemale,
                                             Title = objPatient.Title ?? "&nbsp;",
                                             Note = objPatient.Note ?? "&nbsp;"
                                         }
                                 };

            return WebCommon.BuildSuccessfulResult(lstPatient);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult("Cannot load patient's information. Please try again.");
        }
    }

    /// <summary>
    /// Get Room list by floor
    /// </summary>
    /// <param name="floorId"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetRoomByFloor(string floorId)
    {
        try
        {
            var lstRoom = DataRepository.AppointmentGroupProvider.GetByUnitId(Convert.ToInt32(floorId)).FindAll(x => !x.IsDisabled);
            return WebCommon.BuildSuccessfulResult(lstRoom.Select(x => new { key = x.Id, label = x.Title }));
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult("Cannot load room list. Please try again.");
        }
    }
    #endregion

    #region "Private Function"
    /// <summary>
    /// Check conflict for appointment follow column
    /// </summary>
    /// <param name="column">Cot se duoc kiem tra conflict</param>
    /// <param name="compareValue">Gia tri cua cot can duoc kiem tra</param>
    /// <param name="name"></param>
    /// <param name="title"></param>
    /// <param name="dtStart"></param>
    /// <param name="dtEnd"></param>
    /// <param name="conflictNumber">So quy dinh gia tri se cho la conflict, doi voi them moi la 0, doi voi update la 1</param>
    /// <param name="message"></param>
    /// <returns></returns>
    private static bool CheckConflict(string column, string compareValue, string name, string title,
        DateTime dtStart, DateTime dtEnd, int conflictNumber, ref string message)
    {
        try
        {
            int count;
            string query = String.Format("{3} = '{0}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'",
                compareValue, dtEnd.ToString("yyyy-MM-dd HH:mm:ss"), dtStart.ToString("yyyy-MM-dd HH:mm:ss"), column);
            DataRepository.AppointmentProvider.GetPaged(query, "Id desc", 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);

            if (count > conflictNumber)
            {
                message = String.Format("Cannot assign appointment because {0} {1} is not available from {2} {3} to {4} {5}."
                    , name, title, dtStart.DayOfWeek.ToString(), dtStart.ToString("dd MMM yyyy HH:mm")
                    , dtEnd.DayOfWeek.ToString(), dtEnd.ToString("dd MMM yyyy HH:mm"));
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            message = ex.Message;
            return false;
        }
    }
    #endregion

    #region Tab and Column in scheduler
    #endregion
}
