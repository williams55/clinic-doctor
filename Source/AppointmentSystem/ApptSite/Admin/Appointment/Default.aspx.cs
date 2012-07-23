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
    protected string ListServices = string.Empty;

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
            var lst = DataRepository.ServicesProvider.GetPaged("IsDisabled = 'False'"
                                                            , "PriorityIndex ASC", 0,
                                                            ServiceFacade.SettingsHelper.GetPagedLength, out count)
                .Select(x => new
                                 {
                                     x.Id,
                                     x.Title,
                                     x.ShortTitle
                                 });
            ListServices = JsonConvert.SerializeObject(lst);

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
                , HttpContext.Current.Request["serviceId"]
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
    /// <param name="serviceId"> </param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    private static string SearchDoctor(string keyword, string appointmentId, string serviceId
        , string startTime, string endTime, string startDate, string endDate)
    {
        try
        {
            keyword = keyword.ToLower().Trim();

            #region Validation
            // Validate appointment Id value
            long lAppointmentId;
            if (!Int64.TryParse(appointmentId, out lAppointmentId))
            {
                return string.Empty;
            }

            // Validate service Id value
            int iServiceId;
            if (!Int32.TryParse(serviceId, out iServiceId))
            {
                return string.Empty;
            }
            #endregion

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

            int count;

            // Lay danh sach bac si da duoc dang ky roster co khoang thoi gian trung voi khoan thoi gian cua Appointment
            var lstRoster =
                DataRepository.RosterProvider.GetPaged(
                    String.Format("N'{0}' >= StartTime AND N'{1}' <= EndTime AND IsDisabled = 'False'"
                                  , dtStart.ToString("yyyy-MM-dd HH:mm:ss.000"),
                                  dtEnd.ToString("yyyy-MM-dd HH:mm:ss.000")), string.Empty, 0,
                    ServiceFacade.SettingsHelper.GetPagedLength, out count);
            DataRepository.RosterProvider.DeepLoad(lstRoster);
            var lstDoctorRoster = lstRoster.Select(x => x.DoctorIdSource).Distinct().ToList();

            // Lay danh sach bac si co appointment trong cung thoi diem
            var lstAppointment =
                DataRepository.AppointmentProvider.GetPaged(
                    String.Format("((N'{0}' < StartTime AND N'{1}' > StartTime) OR (N'{0}' < EndTime AND N'{1}' > EndTime))"
                                  + " AND IsDisabled = 'False' AND Id <> {2}"
                                  , dtStart.ToString("yyyy-MM-dd HH:mm:ss.000"),
                                  dtEnd.ToString("yyyy-MM-dd HH:mm:ss.000"), appointmentId), string.Empty, 0,
                    ServiceFacade.SettingsHelper.GetPagedLength, out count);

            // Lay danh sach bac si bang keyword voi nhung bac si khong bi trung va co serviceId
            var lstDoctor = lstDoctorRoster.FindAll(x =>
                                                        {
                                                            var firstOrDefault = x.DoctorServiceCollection.FirstOrDefault();
                                                            return firstOrDefault != null && (((!string.IsNullOrEmpty(x.DisplayName) && x.DisplayName.ToLower().Contains(keyword))
                                                                                                || (!string.IsNullOrEmpty(x.Firstname) && x.Firstname.ToLower().Contains(keyword))
                                                                                                || (!string.IsNullOrEmpty(x.Lastname) && x.Lastname.ToLower().Contains(keyword))
                                                                                                || string.IsNullOrEmpty(keyword))
                                                                                               && firstOrDefault.ServiceId == iServiceId
                                                                                               && !lstAppointment.Exists(y => y.DoctorId == x.Id));
                                                        });

            return JsonConvert.SerializeObject(lstDoctor.Select(x => new
                                                                       {
                                                                           id = x.Id,
                                                                           x.DisplayName,
                                                                           propertyToSearch = String.Format("{0}. {1}", x.Title, x.DisplayName)
                                                                       }));
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return string.Empty;
        }
    }
    #endregion

    #region Room
    /// <summary>
    /// Search room by condition
    /// </summary>
    /// <param name="keyword"> </param>
    /// <param name="appointmentId"></param>
    /// <param name="doctorId"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetRoom(string appointmentId, string doctorId
        , string startTime, string endTime, string startDate, string endDate)
    {
        try
        {
            #region Validation
            // Validate appointment Id value
            long lAppointmentId;
            if (!Int64.TryParse(appointmentId, out lAppointmentId))
            {
                return WebCommon.BuildFailedResult("Appointment is invalid");
            }
            #endregion

            // Get service base on doctorId
            var doctorService = DataRepository.DoctorServiceProvider.GetByDoctorId(doctorId).Find(x => !x.IsDisabled);
            // Validate available doctor
            if (doctorService == null)
            {
                return WebCommon.BuildFailedResult("Doctor or service is not available to get list of available room.");
            }
            // Deepload to get doctor's info
            DataRepository.DoctorServiceProvider.DeepLoad(doctorService);
            // Validate available doctor
            if (doctorService.DoctorIdSource.IsDisabled || doctorService.ServiceIdSource.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Doctor or service is not available to get list of available room.");
            }

            // Set value for service Id
            int iServiceId = doctorService.ServiceId;

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

            int count;

            // Lay danh sach phong co appointment trong cung thoi diem
            var lstAppointment =
                DataRepository.AppointmentProvider.GetPaged(
                    String.Format(
                        "((N'{0}' < StartTime AND N'{1}' > StartTime) OR (N'{0}' < EndTime AND N'{1}' > EndTime))"
                        + " AND IsDisabled = 'False' AND Id <> '{2}'"
                        , dtStart.ToString("yyyy-MM-dd HH:mm:ss.000"),
                        dtEnd.ToString("yyyy-MM-dd HH:mm:ss.000"), appointmentId), string.Empty, 0,
                    ServiceFacade.SettingsHelper.GetPagedLength, out count);

            // Lay danh sach phong
            var lstRoom =
                DataRepository.RoomProvider.GetPaged(
                    String.Format("ServicesId = {0} AND IsDisabled = 'False'", iServiceId), string.Empty, 0,
                    ServiceFacade.SettingsHelper.GetPagedLength, out count);
            DataRepository.RoomProvider.DeepLoad(lstRoom);

            // Lay danh sach phong co the su dung
            // Danh sach nay se tro sang bang DoctorRoom de lay Priority, neu bang nay khong chua du lieu thi lay so lon nhat, cai nay de sort
            var lstAvailableRoom = lstRoom.FindAll(x => !lstAppointment.Exists(y => y.RoomId == x.Id))
                // Phong dang free => Phong khong duoc ton tai trong Appointment
                .Select(x =>
                            {
                                var firstOrDefault = x.DoctorRoomCollection.FirstOrDefault();
                                return new
                                           {
                                               x.Id,
                                               x.Title,
                                               Priority =
                                                   // Lay Priority neu no ton tai voi Doctor, neu khong thi lay mot so lon
                                                   (firstOrDefault != null &&
                                                    (string.IsNullOrEmpty(
                                                        doctorId) ||
                                                     firstOrDefault.DoctorId ==
                                                     doctorId)
                                                        ? firstOrDefault.
                                                              Priority
                                                        : ServiceFacade.
                                                              SettingsHelper.
                                                              GetPagedLength)
                                           };
                            })
                .ToList();

            lstAvailableRoom.Sort((p1, p2) => p1.Priority.CompareTo(p2.Priority));

            return WebCommon.BuildSuccessfulResult(lstAvailableRoom);
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
    public static string NewAppointment(string patientId, string note, string startTime, string endTime, string startDate, string endDate
        , string doctorId, string roomId, string status)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            #region Validate
            patientId = patientId.Trim();
            note = note.Trim();
            startTime = startTime.Trim();
            endTime = endTime.Trim();
            startDate = startDate.Trim();
            endDate = endDate.Trim();
            doctorId = doctorId.Trim();
            roomId = roomId.Trim();
            status = status.Trim();

            // Validate current user have any right to operate this action
            // Validate user right for reading
            if (!RightAccess.CheckUserRight(Username, ScreenCode, OperationConstant.Create.Key, out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Check Status
            int statusId;
            if (!Int32.TryParse(status, out statusId))
            {
                return WebCommon.BuildFailedResult("You must choose status.");
            }

            // Check Doctor
            if (string.IsNullOrEmpty(doctorId))
            {
                return WebCommon.BuildFailedResult("You must choose doctor.");
            }

            // Check Patient
            if (string.IsNullOrEmpty(patientId))
            {
                return WebCommon.BuildFailedResult("You must choose patient.");
            }

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

            // Check Room
            int iRoomId;
            if (!Int32.TryParse(roomId, out iRoomId))
            {
                return WebCommon.BuildFailedResult("You must choose room.");
            }
            #endregion

            tm.BeginTransaction();

            #region Check infomation
            // Check exists Patient
            Patient objPatient = DataRepository.PatientProvider.GetById(patientId);
            if (objPatient == null || objPatient.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Patient is not exist.");
            }

            // Check exists Doctor
            Users objDoctor = DataRepository.UsersProvider.GetById(doctorId);
            if (objDoctor == null || objDoctor.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Doctor is not exist.");
            }

            // Check exists Room
            Room objRoom = DataRepository.RoomProvider.GetById(iRoomId);
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

            // Get service base on doctorId
            var doctorService = DataRepository.DoctorServiceProvider.GetByDoctorId(doctorId).Find(x => !x.IsDisabled);
            // Validate available doctor
            if (doctorService == null)
            {
                return WebCommon.BuildFailedResult("Doctor or service is not available to get list of available room.");
            }
            // Deepload to get doctor's info
            DataRepository.DoctorServiceProvider.DeepLoad(doctorService);
            // Validate available doctor
            if (doctorService.DoctorIdSource.IsDisabled || doctorService.ServiceIdSource.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Doctor or service is not available to get list of available room.");
            }
            #endregion

            #region Check Conflict
            int intCompareValue = 0;
            // Check conflict Patient
            if (!CheckConflict("PatientId", patientId, string.IsNullOrEmpty(objPatient.Title) ? "patient" : objPatient.Title + ".",
                String.Format("{0} {1}", objPatient.FirstName, objPatient.LastName), dtStart, dtEnd, intCompareValue, ref _message))
            {
                tm.Rollback();
                return WebCommon.BuildFailedResult(_message);
            }

            // Check conflict Doctor
            if (!CheckConflict("DoctorId", doctorId, "Dr.", objDoctor.DisplayName, dtStart, dtEnd, intCompareValue, ref _message))
            {
                tm.Rollback();
                return WebCommon.BuildFailedResult(_message);
            }

            // Check conflict Room
            if (!CheckConflict("RoomId", roomId, "Room", objRoom.Title, dtStart, dtEnd, intCompareValue, ref _message))
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

            newObj.PatientId = patientId;
            newObj.DoctorId = objDoctor.Id;
            newObj.RoomId = iRoomId;
            newObj.ServicesId = doctorService.ServiceId;
            newObj.Note = note;
            newObj.StatusId = statusId;
            newObj.StartTime = dtStart;
            newObj.EndTime = dtEnd;
            newObj.CreateUser = Username;
            newObj.UpdateUser = Username;

            DataRepository.AppointmentProvider.Insert(tm, newObj);
            #endregion

            #region "Send Appointment"
            DataRepository.AppointmentProvider.DeepLoad(newObj);
            string strSubject = String.Format("New Appointment: {0}{1}{2}{3}",
                                              string.IsNullOrEmpty(newObj.ServicesIdSource.Title)
                                                  ? string.Empty
                                                  : newObj.ServicesIdSource.Title + " - "
                                              ,
                                              string.IsNullOrEmpty(newObj.PatientIdSource.Title)
                                                  ? string.Empty
                                                  : newObj.PatientIdSource.Title + ". ",
                                              string.IsNullOrEmpty(newObj.PatientIdSource.FirstName)
                                                  ? string.Empty
                                                  : newObj.PatientIdSource.FirstName + " ",
                                              string.IsNullOrEmpty(newObj.PatientIdSource.LastName)
                                                  ? string.Empty
                                                  : newObj.PatientIdSource.LastName);
            string strBody = String.Format("You have a new appointment");
            string strSummary = string.Empty;
            string strDescription = string.Empty;
            string strLocation = String.Format("Room {0}", string.IsNullOrEmpty(newObj.RoomIdSource.Title)
                                                  ? string.Empty
                                                  : newObj.RoomIdSource.Title);
            string strAlarmSummary = String.Format("{0} minutes left for appointment with {1}. {2} {3}",
                ServiceFacade.SettingsHelper.TimeLeftRemindAppointment, string.IsNullOrEmpty(newObj.PatientIdSource.Title)
                                                  ? string.Empty
                                                  : newObj.PatientIdSource.Title + ". ",
                                              string.IsNullOrEmpty(newObj.PatientIdSource.FirstName)
                                                  ? string.Empty
                                                  : newObj.PatientIdSource.FirstName + " ",
                                              string.IsNullOrEmpty(newObj.PatientIdSource.LastName)
                                                  ? string.Empty
                                                  : newObj.PatientIdSource.LastName);
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
            if (!CheckConflict("PatientId", strPatientId, "",
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
    /// <param name="serviceId"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetDoctorList(string serviceId)
    {
        try
        {
            // Validate serviceId
            int iServiceId;
            if (!Int32.TryParse(serviceId, out iServiceId))
            {
                return WebCommon.BuildFailedResult("Service is invalid.");
            }

            var lstDoctorService = DataRepository.DoctorServiceProvider.GetByServiceId(iServiceId).FindAll(x => !x.IsDisabled);
            DataRepository.DoctorServiceProvider.DeepLoad(lstDoctorService);

            // Return a list with condition is Doctor and Service must be available
            return
                WebCommon.BuildSuccessfulResult(
                    lstDoctorService.FindAll(x => !x.DoctorIdSource.IsDisabled && !x.ServiceIdSource.IsDisabled).Select(
                        x => new { key = x.DoctorIdSource.Id, label = x.DoctorIdSource.DisplayName }));
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult("Cannot load doctor list. Please try again.");
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

    #region Patient
    /// <summary>
    /// Create patient with simple information
    /// </summary>
    /// <param name="firstname"></param>
    /// <param name="lastname"></param>
    /// <param name="cellPhone"></param>
    /// <param name="address"></param>
    /// <param name="isFemale"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string CreateSimplePatient(string firstname, string lastname, string cellPhone, string address, string isFemale)
    {
        try
        {
            #region Validate
            // Validate empty value for Firstname field
            if (string.IsNullOrEmpty(firstname))
            {
                return WebCommon.BuildFailedResult("Please input Firstname.");
            }

            // Validate empty value for Lastname field
            if (string.IsNullOrEmpty(lastname))
            {
                return WebCommon.BuildFailedResult("Please input Lastname.");
            }

            // Validate value of IsFemale
            bool blIsFemale;
            if (!Boolean.TryParse(isFemale, out blIsFemale))
            {
                return WebCommon.BuildFailedResult("Value of Sex field is invalid.");
            }
            #endregion

            #region "Insert"
            var newObj = new Patient();
            string perfix = ServiceFacade.SettingsHelper.PatientPrefix + DateTime.Now.ToString("yyMMdd");
            int count;
            TList<Patient> objPo = DataRepository.PatientProvider.GetPaged("Id like '" + perfix + "' + '%'", "Id desc", 0, 1, out count);
            if (count == 0)
                newObj.Id = perfix + "001";
            else
            {
                newObj.Id = perfix + String.Format("{0:000}", int.Parse(objPo[0].Id.Substring(objPo[0].Id.Length - 3)) + 1);
            }

            newObj.FirstName = firstname;
            newObj.LastName = lastname;
            newObj.CellPhone = cellPhone;
            newObj.Address = address;
            newObj.IsFemale = blIsFemale;
            newObj.CreateUser = Username;
            newObj.UpdateUser = Username;

            DataRepository.PatientProvider.Insert(newObj);

            var patient = new
            {
                newObj.FirstName,
                newObj.LastName,
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
    #endregion
}
