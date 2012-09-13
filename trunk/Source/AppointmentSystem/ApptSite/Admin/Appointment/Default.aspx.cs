using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using AppointmentBusiness.BO;
using AppointmentBusiness.Util;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using Appt.Common.Constants;
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
            if (!CheckReading(out _message))
            {
                WebCommon.ShowDialog(this, _message, WebCommon.GetHomepageUrl(this));
            }
            else
            {
                // Show script
                ClientScript.RegisterStartupScript(GetType(), "MainScript", @"<script src=""GRNEditt.js"" type=""text/javascript""></script>");
            }

            // Validate current user have any right to operate this action to display button
            changeUser.Visible = CheckCreating(out _message) || CheckUpdating(out _message);
            createUser.Visible = CheckCreating(out _message) || CheckUpdating(out _message);

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
                                     Id = "unit_" + x.Id.ToString(),
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
            var lst = DataRepository.StatusProvider.GetAll();
            lst.Sort((x1, x2) => x1.PriorityIndex.CompareTo(x2.PriorityIndex));

            cboStatus.DataSource = lst;
            cboStatus.DataTextField = "Title";
            cboStatus.DataValueField = "Id";
            cboStatus.DataBind();

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
    /// Get patient by patient code
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetPatient(string id)
    {
        try
        {
            // Validate current user have any right to create new patient
            if (!CheckCreating(out _message) && !CheckUpdating(out _message))
            {
                return WebCommon.BuildFailedResult("You have no right to get patient's information.");
            }

            // Get patient by patient code
            var patient = DataRepository.PatientProvider.GetByPatientCode(id);

            if (patient == null || patient.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Cannot find patient");
            }

            return WebCommon.BuildSuccessfulResult(new List<object>
                                                       {
                                                           new
                                                               {
                                                                   patient.PatientCode,
                                                                   patient.FirstName,
                                                                   patient.LastName,
                                                                   patient.HomePhone,
                                                                   patient.WorkPhone,
                                                                   patient.CellPhone,
                                                                   Birthdate = patient.Birthdate == null
                                                                   ? string.Empty
                                                                   : ((DateTime) patient.Birthdate).ToString(
                                                                       "dd-MM-yyyy"),
                                                                   patient.Title,
                                                                   patient.IsFemale,
                                                                   patient.Note,
                                                                   propertyToSearch = ParsePatientInfo(patient),
                                                                   PatientInfo = ParsePatientName(patient)
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
    /// Search patient by keyword
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static string SearchPatient()
    {
        // Validate current user have any right to create new patient
        if (!CheckCreating(out _message) && !CheckUpdating(out _message))
        {
            return WebCommon.BuildFailedResult("You have no right to search patient.");
        }

        var keyword = HttpContext.Current.Request["q"];
        var lstTk = new List<object>();
        try
        {
            int count;
            var lst = DataRepository.PatientProvider.GetPaged(String.Format("FirstName LIKE '%{0}%' OR LastName LIKE '%{0}%'" +
                                                                             " OR HomePhone LIKE '%{0}%'" +
                                                                             " OR WorkPhone LIKE '%{0}%' OR CellPhone LIKE '%{0}%'" +
                                                                             " OR Birthdate LIKE '%{0}%'", keyword)
                , string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);

            var lstResult = lst.Select(x => new
            {
                id = x.PatientCode,
                x.FirstName,
                x.LastName,
                x.HomePhone,
                x.WorkPhone,
                x.CellPhone,
                Birthdate = x.Birthdate == null ? string.Empty : ((DateTime)x.Birthdate).ToString("dd-MM-yyyy"),
                x.Title,
                x.IsFemale,
                x.Note,
                propertyToSearch = ParsePatientInfo(x),
                PatientInfo = ParsePatientName(x)
            });
            return JsonConvert.SerializeObject(lstResult);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }

        return JsonConvert.SerializeObject(lstTk);
    }

    /// <summary>
    /// Create patient with simple information
    /// </summary>
    /// <param name="firstname"></param>
    /// <param name="lastname"></param>
    /// <param name="cellPhone"></param>
    /// <param name="isFemale"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string CreateSimplePatient(string firstname, string lastname, string cellPhone, string isFemale)
    {
        try
        {
            // Validate current user have any right to create new patient
            if (!CheckCreating(out _message) && !CheckUpdating(out _message))
            {
                return WebCommon.BuildFailedResult("You have no right to create new patient.");
            }

            // Validate patient's fields
            if (!ValidatePatient(firstname, lastname, isFemale, ref _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            #region "Insert"
            var patient = new Patient();
            string perfix = ServiceFacade.SettingsHelper.PatientPrefix;
            int count;
            TList<Patient> objPo = DataRepository.PatientProvider.GetPaged("Id like '" + perfix + "' + '%'", "Id desc", 0, 1, out count);
            if (count == 0)
                patient.PatientCode = perfix + "001";
            else
            {
                patient.PatientCode = perfix + String.Format("{0:000}", int.Parse(objPo[0].PatientCode.Substring(objPo[0].PatientCode.Length - 3)) + 1);
            }

            patient.FirstName = firstname;
            patient.LastName = lastname;
            patient.CellPhone = cellPhone;
            patient.IsFemale = Convert.ToBoolean(isFemale);
            patient.CreateUser = Username;
            patient.UpdateUser = Username;

            DataRepository.PatientProvider.Insert(patient);

            #endregion

            return WebCommon.BuildSuccessfulResult(new List<object> {  new
                              {
                                  patient.FirstName,
                                  patient.LastName,
                                  patient.PatientCode,
                                  PatientInfo = ParsePatientName(patient)
                              } });
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult("Cannot create new patient. Please try again.");
        }
    }

    /// <summary>
    /// Update patient with simple information
    /// </summary>
    /// <param name="id"> </param>
    /// <param name="firstname"></param>
    /// <param name="lastname"></param>
    /// <param name="cellPhone"></param>
    /// <param name="isFemale"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string UpdateSimplePatient(string id, string firstname, string lastname, string cellPhone, string isFemale)
    {
        try
        {
            // Validate current user have any right to update patien's info
            if (!CheckCreating(out _message) && !CheckUpdating(out _message))
            {
                return WebCommon.BuildFailedResult("You have no right to update patient's information.");
            }

            // Validate patient's fields
            if (!ValidatePatient(firstname, lastname, isFemale, ref _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            var patient = DataRepository.PatientProvider.GetByPatientCode(id);
            if (patient == null || patient.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Cannot find patient");
            }

            #region Update
            patient.FirstName = firstname;
            patient.LastName = lastname;
            patient.CellPhone = cellPhone;
            patient.IsFemale = Convert.ToBoolean(isFemale);
            patient.CreateUser = Username;
            patient.UpdateUser = Username;

            DataRepository.PatientProvider.Save(patient);
            #endregion

            return WebCommon.BuildSuccessfulResult(new List<object> { new
            {
                patient.FirstName,
                patient.LastName,
                patient.PatientCode,
                PatientInfo = ParsePatientName(patient)
            } });
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult("Cannot update patient. Please try again.");
        }
    }

    /// <summary>
    /// Validate patient's fields
    /// </summary>
    /// <param name="firstname"></param>
    /// <param name="lastname"></param>
    /// <param name="isFemale"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    private static bool ValidatePatient(string firstname, string lastname, string isFemale, ref string message)
    {
        try
        {
            #region Validate
            // Validate empty value for Firstname field
            if (string.IsNullOrEmpty(firstname))
            {
                message = "Please input Firstname.";
                return false;
            }

            // Validate empty value for Lastname field
            if (string.IsNullOrEmpty(lastname))
            {
                message = "Please input Lastname.";
                return false;
            }

            // Validate value of IsFemale
            bool blIsFemale;
            if (!Boolean.TryParse(isFemale, out blIsFemale))
            {
                message = "Value of Sex field is invalid.";
                return false;
            }
            #endregion

            return true;
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            message = ex.Message;
            return false;
        }
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
            strResult = String.Format("{0}{1} {2}{3}{4}{5}{6}"
                                      ,
                                      string.IsNullOrEmpty(obj.Title) ? string.Empty : String.Format("{0}. ", obj.Title)
                                      , string.IsNullOrEmpty(obj.FirstName) ? string.Empty : obj.FirstName
                                      , string.IsNullOrEmpty(obj.LastName) ? string.Empty : obj.LastName
                                      ,
                                      obj.Birthdate == null
                                          ? string.Empty
                                          : String.Format(" - Birthday: {0}",
                                                          ((DateTime)obj.Birthdate).ToString("dd-MM-yyyy"))
                                      ,
                                      string.IsNullOrEmpty(obj.HomePhone)
                                          ? string.Empty
                                          : String.Format(" - Home Phone: {0}", obj.HomePhone)
                                      ,
                                      string.IsNullOrEmpty(obj.CellPhone)
                                          ? string.Empty
                                          : String.Format(" - Cell Phone: {0}", obj.CellPhone)
                                      ,
                                      string.IsNullOrEmpty(obj.WorkPhone)
                                          ? string.Empty
                                          : String.Format(" - Work Phone: {0}", obj.WorkPhone)
                );
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
        return strResult;
    }

    /// <summary>
    /// Parse data to string for display of token input
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static string ParsePatientName(Patient obj)
    {
        string strResult = string.Empty;
        try
        {
            strResult = String.Format("{0}{1}{2}"
                                      ,
                                      string.IsNullOrEmpty(obj.Title) ? string.Empty : String.Format("{0}. ", obj.Title)
                                      , string.IsNullOrEmpty(obj.FirstName) ? string.Empty : String.Format("{0} ", obj.FirstName)
                                      , string.IsNullOrEmpty(obj.LastName) ? string.Empty : obj.LastName
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
            // Validate current user have any right to get doctor list
            if (!CheckCreating(out _message) && !CheckUpdating(out _message))
            {
                return WebCommon.BuildFailedResult("You have no right to get doctor list");
            }

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
            var lstDoctorRoster = lstRoster.Select(x => x.UsernameSource).Distinct().ToList();

            // Lay danh sach bac si co appointment trong cung thoi diem
            var lstAppointment =
                DataRepository.AppointmentProvider.GetPaged(
                    String.Format("((N'{0}' < StartTime AND N'{1}' > StartTime) OR (N'{0}' < EndTime AND N'{1}' > EndTime))"
                                  + " AND IsDisabled = 'False' AND Id <> {2}"
                                  , dtStart.ToString("yyyy-MM-dd HH:mm:ss.000"),
                                  dtEnd.ToString("yyyy-MM-dd HH:mm:ss.000"), appointmentId), string.Empty, 0,
                    ServiceFacade.SettingsHelper.GetPagedLength, out count);

            // Lay danh sach bac si bang keyword voi nhung bac si khong bi trung va co serviceId
            var lstDoctor =
                lstDoctorRoster.FindAll(
                    x => ((!string.IsNullOrEmpty(x.DisplayName) && x.DisplayName.ToLower().Contains(keyword))
                          || (!string.IsNullOrEmpty(x.Firstname) && x.Firstname.ToLower().Contains(keyword))
                          || (!string.IsNullOrEmpty(x.Lastname) && x.Lastname.ToLower().Contains(keyword))
                          || string.IsNullOrEmpty(keyword))
                         && !lstAppointment.Exists(y => y.Username == x.Username)
                    );

            return JsonConvert.SerializeObject(lstDoctor.Select(x => new
                                                                       {
                                                                           id = x.Username,
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

    /// <summary>
    /// Get doctors in service
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="currentDateView"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetSections(string mode, string currentDateView)
    {
        try
        {
            // Validate current user have any right to get room list
            if (!CheckReading(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Validate service
            int serviceId;
            if (!Int32.TryParse(mode, out serviceId))
            {
                return WebCommon.BuildFailedResult("Service is invalid");
            }

            // Get datetime to filter
            DateTime fromDate = Convert.ToDateTime(currentDateView);
            fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 0, 0, 0);
            var toDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 23, 59, 59);
            int count;

            // Get doctor list of service
            var lstDoctor =
                DataRepository.UsersProvider.GetPaged(String.Format("ServicesId = {0} AND IsDisabled = 'False'", serviceId)
                                                      , string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);

            // Get roster list
            var lstRoster =
                DataRepository.RosterProvider.GetPaged(
                    String.Format(
                        "IsDisabled = 'False' AND (StartTime BETWEEN N'{0}' AND N'{1}' OR EndTime BETWEEN N'{0}' AND N'{1}')"
                        , fromDate.ToString("yyyy-MM-dd HH:mm:ss.000"),
                        toDate.ToString("yyyy-MM-dd HH:mm:ss.000")),
                    string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength,
                    out count);
            // Neu thoi gian bat dau thuoc ngay nho hon ngay dang xet => Set lai thoi gian bat dau 
            // Neu thoi gian ket thuc thuoc ngay lon hon ngay dang xet => Set lai thoi gian ket thuc
            foreach (var roster in lstRoster)
            {
                roster.StartTime = roster.StartTime < fromDate ? fromDate : roster.StartTime;
                roster.EndTime = roster.EndTime > toDate ? toDate : roster.EndTime;
            }
            DataRepository.RosterProvider.DeepLoad(lstRoster);

            // Get available doctor 
            var lstAvaiDoctor = lstDoctor.FindAll(x => lstRoster.Exists(y => y.Username == x.Username));

            // Get available doctor 
            var lstResultDoctor = new TList<Users>();

            // Xet tung bac si, lay danh sach roster, sap xem theo thoi gian bat dau tang dan
            // Dat bien time chay tu 0 => het ngay
            var blockTimes = new List<BlockTime>();
            var avaiTimes = new List<AvaiTime>();
            lstRoster.Sort(("StartTime ASC"));
            foreach (var userse in lstAvaiDoctor)
            {
                // Lay danh sach roster cua doctor
                Users userse1 = userse;
                var lstTmpRoster = lstRoster.FindAll(x => x.Username == userse1.Username);
                var blockTime = new BlockTime
                    {
                        Username = userse1.Username,
                        BlockList = new List<int>()
                    };

                // Khoi tao bien
                int timeIndex = 0;
                int maxTimeIndex = ServiceFacade.SettingsHelper.MinutePerHour * ServiceFacade.SettingsHelper.MaxHour;

                // Khoi tao doctor vao list
                foreach (var roster in lstTmpRoster)
                {
                    // Add roster available
                    avaiTimes.Add(new AvaiTime
                    {
                        StartTime = roster.StartTime.Hour * ServiceFacade.SettingsHelper.MinutePerHour + roster.StartTime.Minute,
                        EndTime = roster.EndTime.Hour * ServiceFacade.SettingsHelper.MinutePerHour + roster.EndTime.Minute,
                        Color = roster.RosterTypeIdSource.ColorCode,
                        IsBlocked = false,
                        Username = roster.Username
                    });

                    // Neu thoi gian bat dau lon hon timeIndex thi tao mot khoang thoi gian block
                    if ((roster.StartTime.Hour * ServiceFacade.SettingsHelper.MinutePerHour + roster.StartTime.Minute) > timeIndex)
                    {
                        avaiTimes.Add(new AvaiTime
                        {
                            StartTime = timeIndex,
                            EndTime = roster.StartTime.Hour * ServiceFacade.SettingsHelper.MinutePerHour + roster.StartTime.Minute,
                            Color = ServiceFacade.SettingsHelper.NotAvailableColor,
                            IsBlocked = true,
                            Username = roster.Username
                        });
                        blockTime.BlockList.Add(timeIndex);
                        blockTime.BlockList.Add(roster.StartTime.Hour * ServiceFacade.SettingsHelper.MinutePerHour + roster.StartTime.Minute);
                    }

                    // Gan timeIndex moi bang thoi gian ket thuc cua roster
                    timeIndex = roster.EndTime.Hour * ServiceFacade.SettingsHelper.MinutePerHour + roster.EndTime.Minute;
                }

                // Neu timeIndex van bang 0 tuc la doctor hien tai khong co roster trong ngay
                if (timeIndex == 0)
                {
                    continue;
                }

                // Neu timeIndex van chua di het mot ngay thi tao them 1 block time
                if (timeIndex < maxTimeIndex - 1)
                {
                    avaiTimes.Add(new AvaiTime
                    {
                        StartTime = timeIndex,
                        EndTime = maxTimeIndex - 1,
                        Color = ServiceFacade.SettingsHelper.NotAvailableColor,
                        IsBlocked = true,
                        Username = userse.Username
                    });
                    blockTime.BlockList.Add(timeIndex);
                    blockTime.BlockList.Add(maxTimeIndex - 1);
                }
                blockTimes.Add(blockTime);
                lstResultDoctor.Add(userse1);
            }
            var lstAppointments = GetSectionAppointments(mode, currentDateView);

            return
                WebCommon.BuildSuccessfulResult(
                    lstResultDoctor.Select(
                        x => new
                        {
                            key = x.Username,
                            label = x.DisplayName,
                            roster = avaiTimes.Where(list => list.Username == x.Username).ToList(),
                            blockTime = blockTimes.Where(list => list.Username == x.Username).Select(list => list.BlockList).ToArray(),
                            appointment = BuildListAppointment(lstAppointments.FindAll(y => x.Username == y.Username))
                        }).ToList());
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }
    #endregion

    #region Room
    /// <summary>
    /// Search room by condition
    /// </summary>
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
            // Validate current user have any right to get room list
            if (!CheckCreating(out _message) && !CheckUpdating(out _message))
            {
                return WebCommon.BuildFailedResult("You have no right to get room");
            }

            // Get service base on doctorId
            var doctorService = DataRepository.UsersProvider.GetByUsername(doctorId);
            // Validate available doctor
            if (doctorService == null || doctorService.ServicesId == null)
            {
                return WebCommon.BuildFailedResult("Doctor is not available to get list of available room.");
            }

            // Set value for service Id
            var iServiceId = (int)doctorService.ServicesId;

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
                        "((N'{0}' > StartTime AND N'{0}' < EndTime) OR (N'{1}' > StartTime AND N'{1}' < EndTime) OR (N'{0}' <= StartTime AND N'{1}' >= EndTime))"
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
                                                     firstOrDefault.Username ==
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

            if (obj.StartTime != null && obj.EndTime != null)
                return new
                           {
                               id = obj.Id,
                               start_date = obj.StartTime.Value.ToString("dd-MM-yyyy HH:mm"),
                               end_date = obj.EndTime.Value.ToString("dd-MM-yyyy HH:mm"),
                               section_id = obj.Username,
                               text =
                                   String.Format("Patient: {0}<br />Note: {1}",
                                                 FullNameOfPatient(obj.PatientCodeSource),
                                                 obj.Note),
                               DoctorDisplayname = obj.UsernameSource.DisplayName,
                               obj.PatientCode,
                               PatientName = obj.PatientCodeSource.FirstName,
                               PatientInfo = ParsePatientName(obj.PatientCodeSource),
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
        }
        return null;
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

            lstEvent.AddRange(from appointment in lst where appointment != null && appointment.StartTime != null && appointment.EndTime != null select BuildAppointment(appointment));
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

    #region Appointment
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string NewAppointment(string patientCode, string note, string startTime, string endTime, string startDate, string endDate
        , string doctorId, string roomId, string status)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            // Validate current user have any right to operate this action
            // Validate user right for creating
            if (!CheckCreating(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Validate all fields
            var dtStart = new DateTime();
            var dtEnd = new DateTime();
            int serviceId = 0;
            string rosterId = string.Empty;
            if (!ValidateAppointmentFields(0, ref patientCode, ref note, ref startTime, ref endTime
                , ref startDate, ref endDate, ref doctorId, ref roomId, ref status
                , ref dtStart, ref dtEnd, ref _message, ref serviceId, ref rosterId))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            tm.BeginTransaction();

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

            newObj.PatientCode = patientCode;
            newObj.Username = doctorId;
            newObj.RoomId = Convert.ToInt32(roomId);
            newObj.ServicesId = serviceId;
            newObj.Note = note;
            newObj.StatusId = status;
            newObj.StartTime = dtStart;
            newObj.EndTime = dtEnd;
            newObj.RosterId = rosterId;
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
                                              string.IsNullOrEmpty(newObj.PatientCodeSource.Title)
                                                  ? string.Empty
                                                  : newObj.PatientCodeSource.Title + ". ",
                                              string.IsNullOrEmpty(newObj.PatientCodeSource.FirstName)
                                                  ? string.Empty
                                                  : newObj.PatientCodeSource.FirstName + " ",
                                              string.IsNullOrEmpty(newObj.PatientCodeSource.LastName)
                                                  ? string.Empty
                                                  : newObj.PatientCodeSource.LastName);
            string strBody = String.Format("You have a new appointment");
            string strSummary = string.Empty;
            string strDescription = string.Empty;
            string strLocation = String.Format("Room {0}", string.IsNullOrEmpty(newObj.RoomIdSource.Title)
                                                  ? string.Empty
                                                  : newObj.RoomIdSource.Title);
            string strAlarmSummary = String.Format("{0} minutes left for appointment with {1}. {2} {3}",
                ServiceFacade.SettingsHelper.TimeLeftRemindAppointment, string.IsNullOrEmpty(newObj.PatientCodeSource.Title)
                                                  ? string.Empty
                                                  : newObj.PatientCodeSource.Title + ". ",
                                              string.IsNullOrEmpty(newObj.PatientCodeSource.FirstName)
                                                  ? string.Empty
                                                  : newObj.PatientCodeSource.FirstName + " ",
                                              string.IsNullOrEmpty(newObj.PatientCodeSource.LastName)
                                                  ? string.Empty
                                                  : newObj.PatientCodeSource.LastName);
            var objMail = new MailAppointment(strSubject, strBody, strSummary, strDescription, strLocation, strAlarmSummary,
                Convert.ToDateTime(newObj.StartTime), Convert.ToDateTime(newObj.EndTime));
            objMail.AddMailAddress(newObj.UsernameSource.Email, newObj.UsernameSource.DisplayName);
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

    /// <summary>
    /// Save data after move appointment
    /// </summary>
    /// <param name="id"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="doctorId"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string MoveAppointment(string id, string startTime, string endTime, string doctorId)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            // Validate current user have any right to operate this action
            // Validate user right for reading
            if (!CheckUpdating(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Get appointment by Id
            Appointment appointment = DataRepository.AppointmentProvider.GetById(id);
            var dtStart = new DateTime();
            var dtEnd = new DateTime();
            string rosterId = string.Empty;
            if (!ValidateAppointmentFields(doctorId, startTime, endTime, appointment, ref dtStart, ref dtEnd, ref _message, ref rosterId))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            tm.BeginTransaction();
            #region "Update Appointment"
            appointment.Username = doctorId;
            appointment.StartTime = dtStart;
            appointment.EndTime = dtEnd;
            appointment.UpdateUser = Username;
            appointment.UpdateDate = DateTime.Now;
            DataRepository.AppointmentProvider.Save(tm, appointment);
            #endregion

            #region "Send Appointment"
            string strSubject = String.Format("Change Appointment: {0} - {1}. {2} {3}", appointment.ServicesIdSource.Title
                , appointment.PatientCodeSource.Title, appointment.PatientCodeSource.FirstName, appointment.PatientCodeSource.LastName);
            string strBody = String.Format("You have a new change for appointment");
            string strSummary = string.Empty;
            string strDescription = string.Empty;
            string strLocation = String.Format("Room {0}", appointment.RoomIdSource.Title);
            string strAlarmSummary = String.Format("{0} minutes left for appointment with {1}. {2} {3}",
                ServiceFacade.SettingsHelper.TimeLeftRemindAppointment, appointment.PatientCodeSource.Title
                , appointment.PatientCodeSource.FirstName, appointment.PatientCodeSource.LastName);
            var objMail = new MailAppointment(strSubject, strBody, strSummary, strDescription, strLocation, strAlarmSummary,
                Convert.ToDateTime(appointment.StartTime), Convert.ToDateTime(appointment.EndTime));
            objMail.AddMailAddress(appointment.UsernameSource.Email, appointment.UsernameSource.DisplayName);
            if (!objMail.SendMail())
            {

            }
            #endregion

            tm.Commit();
            return WebCommon.BuildSuccessfulResult(BuildListAppointment(appointment));
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
    /// <param name="id"> </param>
    /// <param name="patientCode"></param>
    /// <param name="note"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="doctorId"></param>
    /// <param name="roomId"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string UpdateAppointment(string id, string patientCode, string note, string startTime, string endTime, string startDate, string endDate
        , string doctorId, string roomId, string status)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            // Validate current user have any right to operate this action
            // Validate user right for creating
            if (!CheckUpdating(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Get appointment by Id
            Appointment appointment = DataRepository.AppointmentProvider.GetById(id);
            // Check available appointment
            if (appointment == null || appointment.IsComplete || appointment.IsDisabled)
            {
                return WebCommon.BuildFailedResult("There is no appointment to change or appointment is expired.");
            }

            var dtStart = new DateTime();
            var dtEnd = new DateTime();
            int serviceId = 0;
            string rosterId = string.Empty;
            if (!ValidateAppointmentFields(1, ref patientCode, ref note, ref startTime, ref endTime
                , ref startDate, ref endDate, ref doctorId, ref roomId, ref status
                , ref dtStart, ref dtEnd, ref _message, ref serviceId, ref rosterId))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            tm.BeginTransaction();

            #region "Update Appointment"
            appointment.PatientCode = patientCode;
            appointment.Username = doctorId;
            appointment.RoomId = Convert.ToInt32(roomId);
            appointment.ServicesId = serviceId;
            appointment.Note = note;
            appointment.StatusId = status;
            appointment.RosterId = rosterId;
            appointment.StartTime = dtStart;
            appointment.EndTime = dtEnd;
            appointment.UpdateUser = Username;
            appointment.UpdateDate = DateTime.Now;

            DataRepository.AppointmentProvider.Save(tm, appointment);
            #endregion

            #region "Send Appointment"
            DataRepository.AppointmentProvider.DeepLoad(appointment);
            string strSubject = String.Format("Change Appointment: {0} - {1}. {2} {3}", appointment.ServicesIdSource.Title
                , appointment.PatientCodeSource.Title, appointment.PatientCodeSource.FirstName, appointment.PatientCodeSource.LastName);
            string strBody = String.Format("You have a new change for appointment");
            string strSummary = string.Empty;
            string strDescription = string.Empty;
            string strLocation = String.Format("Room {0}", appointment.RoomIdSource.Title);
            string strAlarmSummary = String.Format("{0} minutes left for appointment with {1}. {2} {3}",
                ServiceFacade.SettingsHelper.TimeLeftRemindAppointment, appointment.PatientCodeSource.Title
                , appointment.PatientCodeSource.FirstName, appointment.PatientCodeSource.LastName);
            var objMail = new MailAppointment(strSubject, strBody, strSummary, strDescription, strLocation, strAlarmSummary,
                Convert.ToDateTime(appointment.StartTime), Convert.ToDateTime(appointment.EndTime));
            objMail.AddMailAddress(appointment.UsernameSource.Email, appointment.UsernameSource.DisplayName);
            if (!objMail.SendMail())
            {

            }
            #endregion

            tm.Commit();
            return WebCommon.BuildSuccessfulResult(BuildListAppointment(appointment));
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            tm.Rollback();
            return WebCommon.BuildFailedResult(ex.Message);
        }
    }

    /// <summary>
    /// Get list of appointment in day
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="currentDateView">Date</param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetAppointments(string mode, string currentDateView)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            // Check active user
            if (!CheckReading(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            #region "Load appointment"
            // Get datetime to filter
            DateTime fromDate = Convert.ToDateTime(currentDateView);
            fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 0, 0, 0);
            var toDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 23, 59, 59);
            int count;

            // Get appointment
            TList<Appointment> lstAppt =
                DataRepository.AppointmentProvider.GetPaged(String.Format("IsDisabled = 'False' AND StartTime BETWEEN N'{0}' AND N'{1}'"
                                                                          , fromDate.ToString("yyyy-MM-dd HH:mm:ss.000"),
                                                                          toDate.ToString("yyyy-MM-dd HH:mm:ss.000")),
                                                            string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength,
                                                            out count);

            tm.BeginTransaction();
            // Set out of date for passed appointment
            lstAppt.ForEach(x =>
                                {
                                    x.StatusId = x.StartTime <= DateTime.Now &&
                                                 x.StatusId != BoFactory.StatusBO.Complete &&
                                                 x.StatusId != BoFactory.StatusBO.Cancel
                                                     ? BoFactory.StatusBO.Complete
                                                     : x.StatusId;
                                });
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

    private static TList<Appointment> GetSectionAppointments(string mode, string currentDateView)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            #region "Load appointment"
            // Get datetime to filter
            DateTime fromDate = Convert.ToDateTime(currentDateView);
            fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 0, 0, 0);
            var toDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 23, 59, 59);
            int count;

            // Get appointment
            TList<Appointment> lstAppt =
                DataRepository.AppointmentProvider.GetPaged(String.Format("IsDisabled = 'False' AND StartTime BETWEEN N'{0}' AND N'{1}'"
                                                                          , fromDate.ToString("yyyy-MM-dd HH:mm:ss.000"),
                                                                          toDate.ToString("yyyy-MM-dd HH:mm:ss.000")),
                                                            string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength,
                                                            out count);

            tm.BeginTransaction();
            // Set out of date for passed appointment
            lstAppt.ForEach(x =>
            {
                x.StatusId = x.StartTime <= DateTime.Now &&
                             x.StatusId != BoFactory.StatusBO.Complete &&
                             x.StatusId != BoFactory.StatusBO.Cancel
                                 ? BoFactory.StatusBO.Complete
                                 : x.StatusId;
            });
            DataRepository.AppointmentProvider.Save(lstAppt);
            #endregion

            tm.Commit();
            return lstAppt;
        }
        catch (Exception ex)
        {
            tm.Rollback();
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return new TList<Appointment>();
        }
    }

    /// <summary>
    /// Delete appointment
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string DeleteAppointment(string id)
    {
        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            // Validate current user have any right to operate this action
            // Validate user right for reading
            if (!CheckDeleting(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            Appointment appointment = DataRepository.AppointmentProvider.GetById(id);
            if (appointment == null || appointment.IsComplete || appointment.IsDisabled)
            {
                return WebCommon.BuildFailedResult("There is no appointment to delete or appointment is expired.");
            }

            tm.BeginTransaction();

            appointment.IsDisabled = true;
            appointment.UpdateUser = Username;
            appointment.UpdateDate = DateTime.Now;

            DataRepository.AppointmentProvider.Save(tm, appointment);

            #region "Send Appointment"
            DataRepository.AppointmentProvider.DeepLoad(appointment);
            string strSubject = String.Format("Delete Appointment: {0} - {1}. {2} {3}", appointment.ServicesIdSource.Title
                , appointment.PatientCodeSource.Title, appointment.PatientCodeSource.FirstName, appointment.PatientCodeSource.LastName);
            string strBody = String.Format("Appointment has been deleted.");
            string strSummary = string.Empty;
            string strDescription = string.Empty;
            string strLocation = string.Empty;
            string strAlarmSummary = string.Empty;
            var objMail = new MailAppointment(strSubject, strBody, strSummary, strDescription, strLocation, strAlarmSummary,
                Convert.ToDateTime(appointment.StartTime), Convert.ToDateTime(appointment.EndTime));
            objMail.AddMailAddress(appointment.UsernameSource.Email, appointment.UsernameSource.DisplayName);
            if (!objMail.SendMail())
            {

            }
            #endregion

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
            // Validate current user have any right to operate this action
            // Validate user right for reading
            if (!CheckReading(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // If there is no appointment, return
            if (string.IsNullOrEmpty(appointmentId))
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
            Patient objPatient = DataRepository.PatientProvider.GetByPatientCode(objAppt.PatientCode);
            if (objPatient == null || objPatient.IsDisabled)
            {
                return WebCommon.BuildFailedResult("Patient is not exist.");
            }

            var lstPatient = new List<object>
                                 {
                                     new
                                         {
                                             Id = objPatient.PatientCode,
                                             FirstName = objPatient.FirstName ?? "&nbsp;",
                                             LastName = objPatient.LastName ?? "&nbsp;",
                                             HomePhone = objPatient.HomePhone ?? "&nbsp;",
                                             WorkPhone = objPatient.WorkPhone ?? "&nbsp;",
                                             CellPhone = objPatient.CellPhone ?? "&nbsp;",
                                             Birthdate =
                                         objPatient.Birthdate == null
                                             ? "&nbsp;"
                                             : ((DateTime) objPatient.Birthdate).ToString("MM-dd-yyyy"),
                                             objPatient.IsFemale,
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
            // Validate current user have any right to operate this action
            // Validate user right for updating
            if (!CheckReading(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Validate serviceId
            int iServiceId;
            if (!Int32.TryParse(serviceId, out iServiceId))
            {
                return WebCommon.BuildFailedResult("Service is invalid.");
            }

            var lstDoctor = DataRepository.UsersProvider.GetByServicesId(iServiceId).FindAll(x => !x.IsDisabled);

            // Return a list with condition is Doctor and Service must be available
            return WebCommon.BuildSuccessfulResult(lstDoctor.Select(x => new { key = x.Username, label = x.DisplayName }));
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult("Cannot load doctor list. Please try again.");
        }
    }
    #endregion

    #region Private Function
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

    #region Validating

    /// <summary>
    /// Check conflict of doctor, room, patient
    /// </summary>
    /// <param name="doctorId"> </param>
    /// <param name="endTime"> </param>
    /// <param name="appointment"></param>
    /// <param name="dtEnd"> </param>
    /// <param name="message"> </param>
    /// <param name="dtStart"> </param>
    /// <param name="startTime"> </param>
    /// <returns></returns>
    private static bool ValidateAppointmentFields(string doctorId, string startTime, string endTime, Appointment appointment
        , ref DateTime dtStart, ref DateTime dtEnd, ref string message, ref string rosterId)
    {
        try
        {
            // Check available appointment
            if (appointment == null || appointment.IsComplete || appointment.IsDisabled)
            {
                message = "There is no appointment to change or appointment is expired.";
                return false;
            }

            // Validate startTime and endTime
            if (!DateTime.TryParse(startTime, out dtStart))
            {
                message = "Start time is invalid.";
                return false;
            }
            if (!DateTime.TryParse(endTime, out dtEnd))
            {
                message = "End time is invalid.";
                return false;
            }

            DataRepository.AppointmentProvider.DeepLoad(appointment);

            // Validate all fields
            string patientCode = appointment.PatientCode;
            string note = appointment.Note;
            string startDate = dtStart.ToString("MM/dd/yyyy");
            string endDate = dtEnd.ToString("MM/dd/yyyy");
            string tmpStartTime = dtStart.ToString("HH:mm':00'");
            string tmpEndTime = dtEnd.ToString("HH:mm':00'");
            string roomId = Convert.ToString(appointment.RoomId);
            string status = Convert.ToString(appointment.StatusId);
            int serviceId = 0;
            if (!ValidateAppointmentFields(1, ref patientCode, ref note, ref tmpStartTime, ref tmpEndTime
            , ref startDate, ref endDate, ref doctorId, ref roomId, ref status
            , ref dtStart, ref dtEnd, ref message, ref serviceId, ref rosterId))
            {
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

    /// <summary>
    /// Validate all of field: Doctor, room, patient, status, datetime
    /// </summary>
    /// <param name="compareValue"> </param>
    /// <param name="patientCode"></param>
    /// <param name="note"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="doctorId"></param>
    /// <param name="roomId"></param>
    /// <param name="status"></param>
    /// <param name="dtEnd"> </param>
    /// <param name="message"> </param>
    /// <param name="dtStart"> </param>
    /// <param name="serviceId"> </param>
    /// <returns></returns>
    private static bool ValidateAppointmentFields(int compareValue, ref string patientCode, ref string note, ref string startTime, ref string endTime
        , ref string startDate, ref string endDate, ref string doctorId, ref string roomId, ref string status
        , ref DateTime dtStart, ref DateTime dtEnd, ref string message, ref int serviceId, ref string rosterId)
    {
        try
        {
            patientCode = patientCode.Trim();
            note = note.Trim();
            startTime = startTime.Trim();
            endTime = endTime.Trim();
            startDate = startDate.Trim();
            endDate = endDate.Trim();
            doctorId = doctorId.Trim();
            roomId = roomId.Trim();
            status = status.Trim();

            #region Validate
            // Check Status
            if (string.IsNullOrEmpty(status))
            {
                message = "You must choose status.";
                return false;
            }

            // Check Doctor
            if (string.IsNullOrEmpty(doctorId))
            {
                message = "You must choose doctor.";
                return false;
            }

            // Check Patient
            if (string.IsNullOrEmpty(patientCode))
            {
                message = "You must choose patient.";
                return false;
            }

            // Get start time and end time
            int intStartHour = Convert.ToInt32(startTime.Split(':')[0]);
            int intStartMinute = Convert.ToInt32(startTime.Split(':')[1]);
            int intEndHour = Convert.ToInt32(endTime.Split(':')[0]);
            int intEndMinute = Convert.ToInt32(endTime.Split(':')[1]);

            // Get start date and end date
            dtStart = Convert.ToDateTime(startDate);
            dtEnd = Convert.ToDateTime(endDate);

            dtStart = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, intStartHour, intStartMinute, 0);
            dtEnd = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, intEndHour, intEndMinute, 0);

            // If appointment From Date >= To Date
            if (dtStart >= dtEnd)
            {
                message = "From date, time must be less than to date, time.";
                return false;
            }

            // If appointment is created in a passed or current day
            DateTime dtNow = DateTime.Now;
            if (dtNow >= dtStart)
            {
                message = "You can not change roster to passed or current date.";
                return false;
            }

            // Check Room
            int iRoomId;
            if (!Int32.TryParse(roomId, out iRoomId))
            {
                message = "You must choose room.";
                return false;
            }
            #endregion

            #region Check information
            // Check exists Patient
            Patient objPatient = DataRepository.PatientProvider.GetByPatientCode(patientCode);
            if (objPatient == null || objPatient.IsDisabled)
            {
                message = "Patient is not exist.";
                return false;
            }

            // Check exists Doctor
            Users objDoctor = DataRepository.UsersProvider.GetByUsername(doctorId);
            if (objDoctor == null || objDoctor.IsDisabled || objDoctor.ServicesId == null)
            {
                message = "Doctor is not exist.";
                return false;
            }

            // Check exists Room
            Room objRoom = DataRepository.RoomProvider.GetById(iRoomId);
            if (objRoom == null || objRoom.IsDisabled)
            {
                message = "Room is not exist.";
                return false;
            }

            // Check exists Room
            Status objStatus = DataRepository.StatusProvider.GetById(status);
            if (objStatus == null)
            {
                message = "Status is not exist.";
                return false;
            }

            // Set value for service Id
            serviceId = (int)objDoctor.ServicesId;
            #endregion

            #region Kiem tra xem doctor duoc tao appointment co dang ky Roster hay khong
            int count;
            // Get roster list
            var lstRoster =
                DataRepository.RosterProvider.GetPaged(
                    String.Format("Username = '{0}' AND StartTime <= '{1}' AND EndTime >= '{2}' AND IsDisabled = 'False'",
                                  doctorId, dtStart.ToString("yyyy-MM-dd HH:mm:ss"),
                                  dtEnd.ToString("yyyy-MM-dd HH:mm:ss")), string.Empty, 0,
                    ServiceFacade.SettingsHelper.GetPagedLength, out count);

            // If there is no roster, return error
            if (!lstRoster.Any())
            {
                message = String.Format("Cannot create appointment because Dr. {0} has no roster from {1} {2} to {3} {4}."
                    , objDoctor.DisplayName, dtStart.DayOfWeek.ToString(), dtStart.ToString("dd MMM yyyy HH:mm")
                    , dtEnd.DayOfWeek.ToString(), dtEnd.ToString("dd MMM yyyy HH:mm"));
                return false;
            }

            // If there has some rosters but not is booked, return error
            DataRepository.RosterProvider.DeepLoad(lstRoster);
            var lstRosterType = lstRoster.Select(x => x.RosterTypeIdSource.IsBooked);
            if (!lstRosterType.Any())
            {
                message = String.Format("Cannot create appointment because Dr. {0} has no roster from {1} {2} to {3} {4}."
                    , objDoctor.DisplayName, dtStart.DayOfWeek.ToString(), dtStart.ToString("dd MMM yyyy HH:mm")
                    , dtEnd.DayOfWeek.ToString(), dtEnd.ToString("dd MMM yyyy HH:mm"));
                return false;
            }
            #endregion

            #region Check Conflict
            // Check conflict Patient
            if (!CheckConflict("PatientCode", patientCode, string.IsNullOrEmpty(objPatient.Title) ? "patient" : objPatient.Title + ".",
                String.Format("{0} {1}", objPatient.FirstName, objPatient.LastName), dtStart, dtEnd, compareValue, ref message))
            {
                message = String.Format("Cannot create appointment because {0} has another appointment from {1} {2} to {3} {4}."
                    , FullNameOfPatient(objPatient), dtStart.DayOfWeek.ToString(), dtStart.ToString("dd MMM yyyy HH:mm")
                    , dtEnd.DayOfWeek.ToString(), dtEnd.ToString("dd MMM yyyy HH:mm"));
                return false;
            }

            // Check conflict Doctor
            if (!CheckConflict("Username", doctorId, string.Empty, objDoctor.DisplayName, dtStart, dtEnd, compareValue, ref message))
            {
                return false;
            }

            // Check conflict Room
            if (!CheckConflict("RoomId", roomId, "Room", objRoom.Title, dtStart, dtEnd, compareValue, ref message))
            {
                return false;
            }
            #endregion

            rosterId = lstRoster[0].Id;
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

    #region Build string
    /// <summary>
    /// Build fullname with title for patient
    /// </summary>
    /// <param name="patient"></param>
    /// <returns></returns>
    private static string FullNameOfPatient(Patient patient)
    {
        return String.Format("{2}{0}{1}",
                             string.IsNullOrEmpty(patient.FirstName)
                                 ? string.Empty
                                 : patient.FirstName + " ",
                             string.IsNullOrEmpty(patient.LastName)
                                 ? string.Empty
                                 : patient.LastName,
                             string.IsNullOrEmpty(patient.Title)
                                 ? string.Empty
                                 : patient.Title + ". ");
    }
    #endregion
}

public class AvaiTime
{
    public string Username { get; set; }
    public int StartTime { get; set; }
    public int EndTime { get; set; }
    public string Color { get; set; }
    public bool IsBlocked { get; set; }
}

public class BlockTime
{
    public string Username { get; set; }
    public List<int> BlockList { get; set; }
}