using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using Appt.Common.UserDefine;
using System.Data;
using System.Data.SqlClient;
using Common.Util;
using Log;
using Log.Controller;
using Newtonsoft.Json;

public partial class Admin_Appointment_Default : System.Web.UI.Page
{
    #region Variables
    private const string StrSeperateStaff = " | ";
    private const char ChrSeperateStaff = '|';

    // Contain list of floor with Json format
    protected string StrFloors = string.Empty;
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        BindTabs();
        BindStatus();
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
            var lst = DataRepository.StatusProvider.GetAll().Where(x => x.IsDisabled == false).ToList();
            lst.Sort((p1, p2) => p1.PriorityIndex.CompareTo(p2.PriorityIndex));

            cboStatus.DataSource = lst;
            cboStatus.DataTextField = "Title";
            cboStatus.DataValueField = "Id";
            cboStatus.DataBind();

            lst.Add(new Status()
            {
                Title = "Out of date",
                ColorCode = ServiceFacade.SettingsHelper.CompleteColor
            });
            rptStatus.DataSource = lst;
            rptStatus.DataBind();
        }
        catch (Exception ex)
        {
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.GetStatus", ex);
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
            var lst = DataRepository.CustomerProvider.GetPaged(String.Format("FirstName LIKE '%{0}%' OR LastName LIKE '%{0}%'" +
                                                                             " OR Address LIKE '%{0}%' OR HomePhone LIKE '%{0}%'" +
                                                                             " OR WorkPhone LIKE '%{0}%' OR CellPhone LIKE '%{0}%'" +
                                                                             " OR Birthdate LIKE '%{0}%'", keyword)
                , string.Empty, 0, 10000, out count);

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
            //SingletonLogger.Instance.Error("home_recharge_Default.aspx.Search", ex);
        }

        return JsonConvert.SerializeObject(lstTk);
    }

    /// <summary>
    /// Parse data to string for property search of token input
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static string ParsePatientInfo(Customer obj)
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
            //SingletonLogger.Instance.Error("home_recharge_Default.aspx.Search", ex);
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
            //SingletonLogger.Instance.Error("home_recharge_Default.aspx.SearchDoctor", ex);
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
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.GetStaffs", ex);
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
            //SingletonLogger.Instance.Error("home_recharge_Default.aspx.SearchRoom", ex);
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
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.GetRoom", ex);
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
        string result = string.Empty;

        try
        {
            Room obj = DataRepository.RoomProvider.GetByIdIsDisabled(Convert.ToInt64(roomId), false);
            if (obj == null)
            {
                result = @"[{ 'result': 'true', 'message': 'Cannot find room.', 'data': '' }]";
                goto StepResult;
            }

            result = JsonConvert.SerializeObject(new JRoomToken()
            {
                id = obj.Id.ToString(),
                propertyToSearch = obj.Title
            });
            result = @"[{ 'result': 'true', 'message': '', 'data':" + result + @" }]";
        }
        catch (Exception ex)
        {
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.GetRoom", ex);
            result = string.Empty;
        }
    StepResult:
        return result;
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
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data:  [{
         *              Id: Id of appointment,
         *              PatientId: Patient Id,
         *              PatientInfo: Patient propertyToSearch,
         *              DoctorUserName: Doctor UserName,
         *              DoctorInfo: Doctor propertyToSearch,
         *              RoomId: Room Id,
         *              RoomInfo: Room propertyToSearch,
         *              start_date: start date,
         *              end_date: end date,
         *              note: note
         *          }, {}, {}]
         *  }
         */
        string result;

        try
        {
            var obj = DataRepository.AppointmentProvider.GetByIdIsDisabled(appointmentId, false);
            if (obj == null)
            {
                result = @"[{ 'result': 'true', 'message': 'Cannot find appointment.', 'data': '' }]";
                goto StepResult;
            }

            // Deep load appointment
            DataRepository.AppointmentProvider.DeepLoad(obj);

            // Get Specialty [Functionality]
            var lstFunc = DataRepository.DoctorFuncProvider.GetByDoctorUserName(obj.DoctorUsername);
            DoctorFunc objFunc = null;
            if (lstFunc.Any()) objFunc = lstFunc.First();

            result = JsonConvert.SerializeObject(new
            {
                Id = obj.Id,
                PatientId = obj.CustomerId,
                PatientInfo = ParsePatientInfo(obj.CustomerIdSource),
                DoctorUserName = obj.DoctorUsername,
                DoctorInfo = String.Format("Dr. {0}. {1}"
                    , obj.DoctorUsernameSource.ShortName, objFunc == null ? string.Empty : String.Format("Specialty: {0}. ", objFunc.FuncTitle)),
                RoomId = obj.RoomId,
                RoomInfo = obj.RoomIdSource.Title,
                note = obj.Note
            });
            result = @"[{ 'result': 'true', 'message': '', 'data':" + result + @" }]";
        }
        catch (Exception ex)
        {
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.GetAppointment", ex);
            result = string.Empty;
        }
    StepResult:
        return result;
    }

    /// <summary>
    /// Build object to string result
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static string BuildResult(Appointment obj)
    {
        var lstEvent = new List<JEvent>();
        try
        {
            if (obj.StartTime != null && obj.EndTime != null)
                lstEvent.Add(
                    new JEvent()
                    {
                        id = obj.Id,
                        start_date = obj.StartTime.Value.ToString("dd-MM-yyyy HH:mm"),
                        end_date = obj.EndTime.Value.ToString("dd-MM-yyyy HH:mm"),
                        section_id = obj.RoomId.ToString(),
                        text =
                            String.Format("Patient: {0}.<br />Note: {1}", obj.CustomerName,
                                          obj.Note),
                        DoctorUserName = obj.DoctorUsername,
                        DoctorShortName = obj.DoctorShortName,
                        CustomerId = obj.CustomerId,
                        CustomerName = obj.CustomerName,
                        ContentId = obj.ContentId.ToString(),
                        ContentTitle = obj.ContentTitle,
                        RoomId = obj.RoomId.ToString(),
                        RoomTitle = obj.RoomTitle,
                        NurseUsername = obj.NurseUsername,
                        NurseShortName = obj.NurseShortName,
                        note = obj.Note,
                        ReadOnly = (obj.StartTime <= DateTime.Now).ToString().ToLower(),
                        color = obj.ColorCode,
                        isnew = "false"
                    }
                    );
        }
        catch (Exception ex)
        {
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.BuildResult", ex);
        }
        return JsonConvert.SerializeObject(lstEvent);
    }
    #endregion

    #region "Roster"
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string SaveEvent(string PatientId, string Note, string StartTime, string EndTime, string StartDate, string EndDate
        , string DoctorUsername, string RoomId, string Status)
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
         *              isnew: false
         *          }, {}, {}]
         *  }
         */

        string result = string.Empty;

        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {

            #region "Validate"
            // Check Patient
            if (string.IsNullOrEmpty(PatientId) || PatientId == "")
            {
                result = @"[{ 'result': 'false', 'message': 'You must choose patient.', 'data': [] }]";
                return result;
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
                result = @"[{ 'result': 'false', 'message': 'From date, time must be less than to date, time.', 'data': [] }]";
                goto StepResult;
            }

            // If appointment is created in a passed or current day
            DateTime dtNow = DateTime.Now;
            if (dtNow >= dtStart)
            {
                result = @"[{ 'result': 'false', 'message': 'You can not change roster to passed or current date.', 'data': [] }]";
                goto StepResult;
            }

            // Check Doctor
            if (string.IsNullOrEmpty(DoctorUsername) || DoctorUsername == "")
            {
                result = @"[{ 'result': 'false', 'message': 'You must choose doctor.', 'data': [] }]";
                goto StepResult;
            }

            // Check Room
            if (string.IsNullOrEmpty(RoomId) || RoomId == "")
            {
                result = @"[{ 'result': 'false', 'message': 'You must choose room.', 'data': [] }]";
                goto StepResult;
            }

            // Check Status
            long lStatus;
            if (!Int64.TryParse(Status, out lStatus))
            {
                result = @"[{ 'result': 'false', 'message': 'You must choose status.', 'data': [] }]";
                goto StepResult;
            }
            #endregion

            string strUserName = EntitiesUtilities.GetAuthName();
            string strPatientId = PatientId;
            long lRoomId = Convert.ToInt64(RoomId);

            tm.BeginTransaction();

            #region "Check infomation"
            // Check exists Patient
            Customer objPatient = DataRepository.CustomerProvider.GetByIdIsDisabled(tm, strPatientId, false);
            if (objPatient == null)
            {
                tm.Rollback();
                result = @"[{ 'result': 'false', 'message': 'Patient is not exist.', 'data': '[]' }]";
                goto StepResult;
            }

            // Check exists Doctor
            Staff objDoctor = DataRepository.StaffProvider.GetByUserNameIsDisabled(tm, DoctorUsername, false);
            if (objDoctor == null)
            {
                tm.Rollback();
                result = @"[{ 'result': 'false', 'message': 'Doctor is not exist.', 'data': '[]' }]";
                goto StepResult;
            }

            // Check exists Room
            Room objRoom = DataRepository.RoomProvider.GetByIdIsDisabled(tm, lRoomId, false);
            if (objRoom == null)
            {
                tm.Rollback();
                result = @"[{ 'result': 'false', 'message': 'Room is not exist.', 'data': '[]' }]";
                goto StepResult;
            }

            // Check exists Room
            Status objStatus = DataRepository.StatusProvider.GetByIdIsDisabled(tm, lStatus, false);
            if (objStatus == null)
            {
                tm.Rollback();
                result = @"[{ 'result': 'false', 'message': 'Status is not exist.', 'data': '[]' }]";
                goto StepResult;
            }
            #endregion

            #region "Check Conflict"
            int intCompareValue = 0;
            // Check conflict Patient
            if (!CheckConflict(tm, "CustomerId", strPatientId, string.IsNullOrEmpty(objPatient.Title) ? "patient" : objPatient.Title + ".",
                String.Format("{0} {1}", objPatient.FirstName, objPatient.LastName), dtStart, dtEnd, intCompareValue, ref result))
            {
                tm.Rollback();
                goto StepResult;
            }

            // Check conflict Doctor
            if (!CheckConflict(tm, "DoctorUserName", DoctorUsername, "Dr.", objDoctor.ShortName, dtStart, dtEnd, intCompareValue, ref result))
            {
                tm.Rollback();
                goto StepResult;
            }

            // Check conflict Room
            if (!CheckConflict(tm, "RoomId", lRoomId.ToString(), "Room", objRoom.Title, dtStart, dtEnd, intCompareValue, ref result))
            {
                tm.Rollback();
                goto StepResult;
            }
            #endregion

            #region "Insert Appointment"
            Appointment newObj = new Appointment();

            string Perfix = "A" + ServiceFacade.SettingsHelper.AppointmentPrefix + DateTime.Now.ToString("yyMMdd");
            int Count = 0;
            TList<Appointment> objPo = DataRepository.AppointmentProvider.GetPaged(tm, "Id like '" + Perfix + "' + '%'", "Id desc", 0, 1, out Count);
            if (Count == 0)
                newObj.Id = Perfix + "001";
            else
            {
                newObj.Id = Perfix + String.Format("{0:000}", int.Parse(objPo[0].Id.Substring(objPo[0].Id.Length - 3)) + 1);
            }

            newObj.CustomerId = strPatientId;
            newObj.CustomerName = String.Format("{0} {1}", objPatient.FirstName, objPatient.LastName);
            newObj.DoctorEmail = objDoctor.Email;
            newObj.DoctorUsername = DoctorUsername;
            newObj.DoctorShortName = objDoctor.ShortName;
            newObj.RoomId = lRoomId;
            newObj.RoomTitle = objRoom.Title;
            newObj.Note = Note;
            newObj.StatusId = lStatus;
            newObj.StartTime = dtStart;
            newObj.EndTime = dtEnd;
            newObj.ColorCode = objStatus.ColorCode;
            newObj.CreateUser = strUserName;
            newObj.UpdateUser = strUserName;

            DataRepository.AppointmentProvider.Insert(tm, newObj);
            #endregion

            #region "Send Appointment"
            string strSubject = String.Format("New Appointment: {0} - {1}", newObj.ContentTitle, newObj.CustomerName);
            string strBody = String.Format("You have a new appointment");
            string strSummary = string.Empty;
            string strDescription = string.Empty;
            string strLocation = String.Format("Room {0}", newObj.RoomTitle);
            string strAlarmSummary = String.Format("{0} minutes left for appointment with {1}",
                ServiceFacade.SettingsHelper.TimeLeftRemindAppointment.ToString(), newObj.CustomerName);
            MailAppointment objMail = new MailAppointment(strSubject, strBody, strSummary, strDescription, strLocation, strAlarmSummary,
                Convert.ToDateTime(newObj.StartTime), Convert.ToDateTime(newObj.EndTime));
            objMail.AddMailAddress(newObj.DoctorEmail, newObj.DoctorShortName);
            objMail.SendMail();
            #endregion

            result = @"[{ 'result': 'true', 'message': '', 'data':" + BuildResult(newObj) + @" }]";

            tm.Commit();

        }
        catch (Exception ex)
        {
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.SaveEvent", ex);

            tm.Rollback();

            result = @"[{ 'result': 'false', 'message': 'Cannot create appointment', 'data': [] }]";
        }

    StepResult:
        return result;
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
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.UpdateEventSave", ex);

            return @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string UpdateEventMove(string Id, string DoctorUsername, string StartTime, string EndTime, string RoomId)
    {
        string result = string.Empty;
        try
        {
            // Get start date and end date
            DateTime dtStart = Convert.ToDateTime(StartTime);
            DateTime dtEnd = Convert.ToDateTime(EndTime);

            Appointment obj = DataRepository.AppointmentProvider.GetByIdIsDisabled(Id, false);
            if (obj == null || obj.IsComplete)
            {
                result = @"[{ 'result': 'false', 'message': 'There is no appointment to change or appointment is expired.', 'data': [] }]";
                return result;
            }

            return UpdateEvent(Id, obj.CustomerId, obj.Note, dtStart,
                dtEnd, DoctorUsername, RoomId, obj.StatusId.ToString());
        }
        catch (Exception ex)
        {
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.UpdateEventMove", ex);

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            return result;
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
        string result = string.Empty;

        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            #region "Validate"
            // Check Patient
            if (string.IsNullOrEmpty(PatientId) || PatientId == "")
            {
                result = @"[{ 'result': 'false', 'message': 'You must choose patient.', 'data': [] }]";
                return result;
            }

            // Get start date and end date
            DateTime dtStart = StartTime;
            DateTime dtEnd = EndTime;

            // If appointment From Date >= To Date
            if (dtStart >= dtEnd)
            {
                result = @"[{ 'result': 'false', 'message': 'From date, time must be less than to date, time.', 'data': [] }]";
                goto StepResult;
            }

            // If appointment is created in a passed or current day
            DateTime dtNow = DateTime.Now;
            if (dtNow >= dtStart)
            {
                result = @"[{ 'result': 'false', 'message': 'You can not change roster to passed or current date.', 'data': [] }]";
                goto StepResult;
            }

            // Check Doctor
            if (string.IsNullOrEmpty(DoctorUsername) || DoctorUsername == "")
            {
                result = @"[{ 'result': 'false', 'message': 'You must choose doctor.', 'data': [] }]";
                goto StepResult;
            }

            // Check Room
            if (string.IsNullOrEmpty(RoomId) || RoomId == "")
            {
                result = @"[{ 'result': 'false', 'message': 'You must choose room.', 'data': [] }]";
                goto StepResult;
            }

            // Check Status
            long lStatus;
            if (!Int64.TryParse(Status, out lStatus))
            {
                result = @"[{ 'result': 'false', 'message': 'You must choose status.', 'data': [] }]";
                goto StepResult;
            }
            #endregion

            string strUserName = EntitiesUtilities.GetAuthName();
            string strPatientId = PatientId;
            long lRoomId = Convert.ToInt64(RoomId);

            tm.BeginTransaction();

            #region "Check infomation"
            // Check exists Patient
            Customer objPatient = DataRepository.CustomerProvider.GetByIdIsDisabled(tm, strPatientId, false);
            if (objPatient == null)
            {
                tm.Rollback();
                result = @"[{ 'result': 'false', 'message': 'Patient is not exist.', 'data': '[]' }]";
                goto StepResult;
            }

            // Check exists Doctor
            Staff objDoctor = DataRepository.StaffProvider.GetByUserNameIsDisabled(tm, DoctorUsername, false);
            if (objDoctor == null)
            {
                tm.Rollback();
                result = @"[{ 'result': 'false', 'message': 'Doctor is not exist.', 'data': '[]' }]";
                goto StepResult;
            }

            // Check exists Room
            Room objRoom = DataRepository.RoomProvider.GetByIdIsDisabled(tm, lRoomId, false);
            if (objRoom == null)
            {
                tm.Rollback();
                result = @"[{ 'result': 'false', 'message': 'Room is not exist.', 'data': '[]' }]";
                goto StepResult;
            }

            // Check exists Room
            Status objStatus = DataRepository.StatusProvider.GetByIdIsDisabled(tm, lStatus, false);
            if (objStatus == null)
            {
                tm.Rollback();
                result = @"[{ 'result': 'false', 'message': 'Status is not exist.', 'data': '[]' }]";
                goto StepResult;
            }
            #endregion

            #region "Check Conflict"
            int intCompareValue = 1;
            // Check conflict Patient
            if (!CheckConflict(tm, "CustomerId", strPatientId, "",
                String.Format("{2}. {0} {1}", objPatient.FirstName, objPatient.LastName, objPatient.Title), dtStart, dtEnd, intCompareValue, ref result))
            {
                tm.Rollback();
                goto StepResult;
            }

            // Check conflict Doctor
            if (!CheckConflict(tm, "DoctorUserName", DoctorUsername, "Doctor", objDoctor.ShortName, dtStart, dtEnd, intCompareValue, ref result))
            {
                tm.Rollback();
                goto StepResult;
            }

            // Check conflict Room
            if (!CheckConflict(tm, "RoomId", lRoomId.ToString(), "Room", objRoom.Title, dtStart, dtEnd, intCompareValue, ref result))
            {
                tm.Rollback();
                goto StepResult;
            }
            #endregion

            #region "Update Appointment"
            Appointment obj = DataRepository.AppointmentProvider.GetByIdIsDisabled(Id, false);
            if (obj == null || obj.IsComplete == true)
            {
                result = @"[{ 'result': 'false', 'message': 'There is no appointment to change or appointment is expired.', 'data': [] }]";
                return result;
            }

            obj.CustomerId = strPatientId;
            obj.CustomerName = String.Format("{0} {1}", objPatient.FirstName, objPatient.LastName);
            obj.DoctorEmail = objDoctor.Email;
            obj.DoctorUsername = DoctorUsername;
            obj.DoctorShortName = objDoctor.ShortName;
            obj.RoomId = lRoomId;
            obj.RoomTitle = objRoom.Title;
            obj.Note = Note;
            obj.StatusId = lStatus;
            obj.ColorCode = objStatus.ColorCode;
            obj.StartTime = dtStart;
            obj.EndTime = dtEnd;
            obj.UpdateUser = strUserName;
            obj.UpdateDate = DateTime.Now;

            DataRepository.AppointmentProvider.Save(tm, obj);
            #endregion

            #region "Send Appointment"
            string strSubject = String.Format("Change Appointment: {0} - {1}", obj.ContentTitle, obj.CustomerName);
            string strBody = String.Format("You have a new change for appointment");
            string strSummary = string.Empty;
            string strDescription = string.Empty;
            string strLocation = String.Format("Room {0}", obj.RoomTitle);
            string strAlarmSummary = String.Format("{0} minutes left for appointment with {1}",
                ServiceFacade.SettingsHelper.TimeLeftRemindAppointment.ToString(), obj.CustomerName);
            MailAppointment objMail = new MailAppointment(strSubject, strBody, strSummary, strDescription, strLocation, strAlarmSummary,
                Convert.ToDateTime(obj.StartTime), Convert.ToDateTime(obj.EndTime));
            objMail.AddMailAddress(obj.DoctorEmail, obj.DoctorShortName);
            if (!objMail.SendMail())
            {

            }
            #endregion

            result = @"[{ 'result': 'true', 'message': '', 'data':" + BuildResult(obj) + @" }]";

            tm.Commit();
        }
        catch (Exception ex)
        {
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.UpdateEvent", ex);

            tm.Rollback();

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            return result;
        }

    StepResult:
        return result;
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

            // Check active user
            string username = EntitiesUtilities.GetAuthName();
            Staff obj = DataRepository.StaffProvider.GetByUserNameIsDisabled(username, false);
            if (obj == null)
            {
                result = @"[{ 'result': 'false', 'message': 'Doctor is not exist.', 'data': '[]' }]";
                goto StepResult;
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
            lstAppt.ForEach(x =>
            {
                x.ColorCode = x.StartTime <= DateTime.Now
                                  ? ServiceFacade.SettingsHelper.CompleteColor
                                  : x.ColorCode;
            });
            DataRepository.AppointmentProvider.Save(lstAppt);

            var lstEvent = from item in lstAppt
                           let startTime = item.StartTime
                           where startTime != null
                           let dateTime = item.EndTime
                           where dateTime != null
                           select new JEvent()
                           {
                               id = item.Id,
                               start_date = startTime.Value.ToString("dd-MM-yyyy HH:mm"),
                               end_date = dateTime.Value.ToString("dd-MM-yyyy HH:mm"),
                               section_id = item.RoomId.ToString(),
                               text = String.Format("Patient: {0}.<br />Note: {1}", item.CustomerName, item.Note),
                               DoctorUserName = item.DoctorUsername,
                               DoctorShortName = item.DoctorShortName,
                               CustomerId = item.CustomerId,
                               CustomerName = item.CustomerName,
                               ContentId = item.ContentId.ToString(),
                               ContentTitle = item.ContentTitle,
                               RoomId = item.RoomId.ToString(),
                               RoomTitle = item.RoomTitle,
                               NurseUsername = item.NurseUsername,
                               NurseShortName = item.NurseShortName,
                               note = item.Note,
                               ReadOnly = (item.StartTime <= DateTime.Now).ToString().ToLower(),
                               color = item.ColorCode,
                               isnew = "false"
                           };
            #endregion

            result = @"[{ 'result': 'true', 'message': '', 'data':" + JsonConvert.SerializeObject(lstEvent) + @" }]";

            tm.Commit();
        }
        catch (Exception ex)
        {
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.GetAppointments", ex);

            tm.Rollback();

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': '[]' }]";
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

            Appointment obj = DataRepository.AppointmentProvider.GetByIdIsDisabled(Id, false);
            if (obj == null || obj.IsComplete == true)
            {
                result = @"[{ 'result': 'false', 'message': 'There is no appointment to delete or appointment is expired.', 'data': [] }]";
                goto StepResult;
            }

            obj.IsDisabled = true;
            obj.UpdateUser = username;
            obj.UpdateDate = DateTime.Now;

            DataRepository.AppointmentProvider.Save(tm, obj);

            result = @"[{ 'result': 'true', 'message': '', 'data': [] }]";
            tm.Commit();
        }
        catch (Exception ex)
        {
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.DeleteEvent", ex);

            tm.Rollback();

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
        }

    StepResult:
        return result;
    }
    #endregion

    #region Status
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetStatus()
    {
        string result = string.Empty;

        try
        {
            var lst = DataRepository.StatusProvider.GetByIsDisabled(false);

            result = @"[{ 'result': 'true', 'message': '', '"
                     + JsonConvert.SerializeObject(lst.Select(x => new
                     {
                         x.Id,
                         x.Title
                     })) + "': [] }]";
        }
        catch (Exception ex)
        {
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.DeleteEvent", ex);

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
        }

    StepResult:
        return result;
    }
    #endregion

    #region "Function"
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

            result = @"{'key': '-1', 'label': 'Select Content'}";
            if (lstObj.Count == 0)
            {
                result = @"[{ 'result': 'true', 'message': 'There is no content. Please contact Administrator.', 'data': [" + result + "] }]";
                goto StepResult;
            }

            lstObj.Sort("FuncTitle ASC, Title ASC");
            foreach (Content item in lstObj)
            {
                result += @", {'key' : '" + item.FuncId + ChrSeperateStaff.ToString() + item.Id.ToString() + @"'" + "," + @"'label' : '"
                    + item.FuncTitle + StrSeperateStaff + item.Title + @"'}";
            }

            result = @"[{ 'result': 'true', 'message': '', 'data':[" + result + @"] }]";
            goto StepResult;
        }
        catch (Exception ex)
        {
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.GetContents", ex);

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
                        result = item.FuncId.ToString() + ChrSeperateStaff.ToString() + item.Id.ToString();
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
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.GetCurrentContents", ex);

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            goto StepResult;
        }
    StepResult:
        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string CreateSimplePatient(string FirstName, string LastName, string CellPhone, string Address, string IsFemale)
    {
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data: string patient id
         *  }
         */
        string result = string.Empty;

        try
        {
            #region "Validate"
            if (string.IsNullOrEmpty(FirstName))
            {
                result = @"[{ 'result': 'true', 'message': 'Please input First name.', 'data': '' }]";
                goto StepResult;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                result = @"[{ 'result': 'true', 'message': 'Please input Last name.', 'data': '' }]";
                goto StepResult;
            }
            #endregion

            #region "Insert"
            string strUserName = EntitiesUtilities.GetAuthName();
            Customer newObj = new Customer();
            bool blIsFemale = Convert.ToBoolean(IsFemale);
            string Perfix = "C" + ServiceFacade.SettingsHelper.CustomerPrefix + DateTime.Now.ToString("yyMMdd");
            int Count = 0;
            TList<Customer> objPo = DataRepository.CustomerProvider.GetPaged("Id like '" + Perfix + "' + '%'", "Id desc", 0, 1, out Count);
            if (Count == 0)
                newObj.Id = Perfix + "001";
            else
            {
                newObj.Id = Perfix + String.Format("{0:000}", int.Parse(objPo[0].Id.Substring(objPo[0].Id.Length - 3)) + 1);
            }

            newObj.FirstName = FirstName;
            newObj.LastName = LastName;
            newObj.CellPhone = CellPhone;
            newObj.Address = Address;
            newObj.IsFemale = blIsFemale;
            newObj.CreateUser = strUserName;
            newObj.UpdateUser = strUserName;

            DataRepository.CustomerProvider.Insert(newObj);

            var patient = new JPatientToken()
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

            result = @"[{ 'result': 'true', 'message': '', 'data':" + JsonConvert.SerializeObject(patient) + @" }]";
        }
        catch (Exception ex)
        {
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.CreateSimplePatient", ex);

            result = @"[{ ""result"": ""false"", ""message"": ""Cannot create new patient. Please try again."", ""data"": """" }]";
        }
    StepResult:
        return result;
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
        string result = string.Empty;

        try
        {
            // If there is no appointment, return
            if (appointmentId == null)
            {
                result = @"[{ 'result': 'true', 'message': '', 'data':'' }]";
                goto StepResult;
            }

            // Get appointment by id
            var objAppt = DataRepository.AppointmentProvider.GetById(appointmentId);

            // If there is no appointment, return
            if (objAppt == null)
            {
                result = @"[{ 'result': 'true', 'message': 'Appointment is not existed', 'data':'' }]";
                goto StepResult;
            }

            // Get patient
            var objPatient = DataRepository.CustomerProvider.GetById(objAppt.CustomerId);

            // If there is no patient, return
            if (objPatient == null)
            {
                result = @"[{ 'result': 'true', 'message': 'Appointment is not existed', 'data':'' }]";
                goto StepResult;
            }

            var lstPatient = new List<JPatient> {new JPatient()
                                                     {
                                                         Id=objPatient.Id,
                                                         FirstName=objPatient.FirstName ?? "&nbsp;",
                                                         LastName = objPatient.LastName ?? "&nbsp;",
                                                         Address = objPatient.Address ?? "&nbsp;",
                                                         HomePhone = objPatient.HomePhone ?? "&nbsp;",
                                                         WorkPhone = objPatient.WorkPhone ?? "&nbsp;",
                                                         CellPhone = objPatient.CellPhone ?? "&nbsp;",
                                                         Birthdate = objPatient.Birthdate==null?"&nbsp;":((DateTime) objPatient.Birthdate).ToString("MM-dd-yyyy"),
                                                         IsFemale = objPatient.IsFemale,
                                                         Title = objPatient.Title ?? "&nbsp;",
                                                         Note = objPatient.Note ?? "&nbsp;"
                                                     }};

            result = @"[{ 'result': 'true', 'message': '', 'data':" + JsonConvert.SerializeObject(lstPatient) + @" }]";
        }
        catch (Exception ex)
        {
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.CreateSimplePatient", ex);

            result = @"[{ ""result"": ""false"", ""message"": ""Cannot load patient's information. Please try again."", ""data"": """" }]";
        }
    StepResult:
        return result;
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
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data: string patient id
         *  }
         */
        string result = string.Empty;

        try
        {
            Floor obj = DataRepository.FloorProvider.GetById(Convert.ToInt32(floorId));
            DataRepository.FloorProvider.DeepLoad(obj);
            var lstRoom = from room in obj.RoomCollection
                          select new JSection()
                          {
                              key = room.Id.ToString(),
                              label = room.Title
                          };
            result = @"[{ 'result': 'true', 'message': '', 'data':" + JsonConvert.SerializeObject(lstRoom) + @" }]";
        }
        catch (Exception ex)
        {
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.CreateSimplePatient", ex);

            result = @"[{ ""result"": ""false"", ""message"": ""Cannot load room list. Please try again."", ""data"": """" }]";
        }
        return result;
    }
    #endregion

    #region "Private Function"
    private static bool CheckConflict(TransactionManager tm, string Column, string CompareValue, string Name, string Title,
        DateTime dtStart, DateTime dtEnd, int ConflictNumber, ref string strResult)
    {
        bool blResult = true;

        try
        {
            int Count = 0;
            string query = String.Format("{3} = '{0}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'",
                CompareValue, dtEnd.ToString("yyyy-MM-dd HH:mm:ss"), dtStart.ToString("yyyy-MM-dd HH:mm:ss"), Column);
            TList<Appointment> lstObj = DataRepository.AppointmentProvider.GetPaged(tm, query, "Id desc", 0, 1, out Count);
            // If there is no roster -> insert new roster
            if (Count > ConflictNumber)
            {
                strResult = @"[{ 'result': 'false', 'message': 'Cannot assign appointment because ";
                strResult += String.Format(@"{0} {1}", Name, Title);
                strResult += @" is not available from ";
                strResult += String.Format(@"{0} {1}", dtStart.DayOfWeek.ToString(), dtStart.ToString("dd MMM yyyy HH:mm"));
                strResult += String.Format(@" to {0} {1}", dtEnd.DayOfWeek.ToString(), dtEnd.ToString("dd MMM yyyy HH:mm"));
                strResult += @".', 'data': [] }]";

                blResult = false;
            }
        }
        catch (Exception ex)
        {
            //SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.CheckConflict", ex);

            blResult = false;
            goto StepResult;
        }
    StepResult:
        return blResult;
    }
    #endregion

    #region Tab and Column in scheduler
    #endregion
}
