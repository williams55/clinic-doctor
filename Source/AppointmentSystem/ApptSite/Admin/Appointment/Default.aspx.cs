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
using Common.Extension;
using Common.Util;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;
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
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }

    protected void cboRoom_OnCallback(object sender, CallbackEventArgsBase e)
    {
        try
        {
            if (e.Parameter == "Refresh")
            {
                // Validate current user have any right to get room list
                if (!CheckCreating(out _message) && !CheckUpdating(out _message))
                {
                    //return WebCommon.BuildFailedResult("You have no right to get room");
                    return;
                }

                // Get service base on doctorId
                var userName = cboDoctor.Value == null ? string.Empty : cboDoctor.Value.ToString();
                // Set value for service Id
                var serviceId = cboService.Value == null ? 0 : (int)cboService.Value;

                DateTime dtStart = DateTime.Now,
                    dtEnd = DateTime.Now;
                if (WebCommon.ValidateEmpty("Start Time", startTime.Value, out _message)
                    && WebCommon.ValidateEmpty("End Time", endTime.Value, out _message)
                    && WebCommon.ValidateEmpty("Start Date", startDate.Value, out _message)
                    && WebCommon.ValidateEmpty("End Date", endDate.Value, out _message))
                {
                    dtStart = new DateTime(((DateTime)startDate.Value).Year, ((DateTime)startDate.Value).Month, ((DateTime)startDate.Value).Day
                                               , ((DateTime)startTime.Value).Hour, ((DateTime)startTime.Value).Minute, 0);
                    dtEnd = new DateTime(((DateTime)endDate.Value).Year, ((DateTime)endDate.Value).Month, ((DateTime)endDate.Value).Day
                                             , ((DateTime)endTime.Value).Hour, ((DateTime)endTime.Value).Minute, 0);
                }


                int count;
                // Lay danh sach phong co appointment trong cung thoi diem
                var lstAppointment =
                    DataRepository.AppointmentProvider.GetPaged(
                        String.Format(
                            "((N'{0}' > StartTime AND N'{0}' < EndTime) OR (N'{1}' > StartTime AND N'{1}' < EndTime) OR (N'{0}' <= StartTime AND N'{1}' >= EndTime))"
                            + " AND IsDisabled = 'False' AND Id <> '{2}'"
                            , dtStart.ToString("yyyy-MM-dd HH:mm:00.000"),
                            dtEnd.ToString("yyyy-MM-dd HH:mm:00.000"), hdId.Value), string.Empty, 0,
                        ServiceFacade.SettingsHelper.GetPagedLength, out count);

                // Lay danh sach phong
                var lstRoom =
                    DataRepository.RoomProvider.GetPaged(
                        String.Format("IsDisabled = 'False'"), string.Empty, 0,
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
                                    (string.IsNullOrEmpty(userName) || firstOrDefault.Username == userName)
                                     ? firstOrDefault.Priority
                                     : ServiceFacade.SettingsHelper.GetPagedLength),
                            ServiceId = x.ServicesId,
                            ServiceName = x.ServicesIdSource.Title
                        };
                    })
                    .ToList();

                // Tach ra lam 3 list: 
                // - Uu tien
                // - Khong uu tien nhung cung service
                // - Khong uu tien va khac service
                var lstPriority = lstAvailableRoom.FindAll(room => room.Priority != ServiceFacade.SettingsHelper.GetPagedLength);
                var lstNonPriSameService = lstAvailableRoom.FindAll(room => room.Priority == ServiceFacade.SettingsHelper.GetPagedLength
                        && room.ServiceId == serviceId);
                var lstNonPriOthers = lstAvailableRoom.FindAll(room => !lstPriority.Exists(pri => pri.Id == room.Id)
                    && !lstNonPriSameService.Exists(pri => pri.Id == room.Id));

                lstPriority.Sort((p1, p2) => p1.Priority.CompareTo(p2.Priority));
                lstPriority.AddRange(lstNonPriSameService);
                lstPriority.AddRange(lstNonPriOthers);

                cboRoom.DataSource = lstPriority;
                cboRoom.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
    }

    protected void cboDoctor_OnCallback(object sender, CallbackEventArgsBase e)
    {
        try
        {
            if (e.Parameter == "Refresh")
            {
                // Validate current user have any right to get room list
                if (!CheckCreating(out _message) && !CheckUpdating(out _message))
                {
                    //return WebCommon.BuildFailedResult("You have no right to get room");
                    return;
                }

                var serviceId = cboService.Value == null ? 0 : (int)cboService.Value;
                int count;
                cboDoctor.DataSource =
                   DataRepository.UsersProvider.GetPaged(
                       String.Format("IsDisabled = 'False' AND ServicesId = '{0}'", serviceId), string.Empty, 0,
                       ServiceFacade.SettingsHelper.GetPagedLength, out count);
                cboDoctor.DataBind();
            }
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
            var patients = DataRepository.VcsPatientProvider.GetByPatientCode(id);

            if (patients == null || !patients.Any())
            {
                return WebCommon.BuildFailedResult("Cannot find patient");
            }
            var patient = patients[0];

            return WebCommon.BuildSuccessfulResult(new List<object>
                                                       {
                                                           new
                                                               {
                                                                   patient.PatientCode,
                                                                   patient.FirstName,
                                                                   patient.MiddleName,
                                                                   patient.LastName,
                                                                   patient.HomePhone,
                                                                   patient.CompanyPhone,
                                                                   patient.MemberType,
                                                                   patient.Nationality,
                                                                   patient.MobilePhone,
                                                                   DateOfBirth = patient.DateOfBirth.ToString("MM/dd/yyyy"),
                                                                   patient.Sex,
                                                                   patient.Remark,
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
            var lst = DataRepository.VcsPatientProvider.GetPaged(String.Format("FirstName LIKE '%{0}%' OR LastName LIKE '%{0}%'" +
                                                                             " OR HomePhone LIKE '%{0}%'" +
                                                                             " OR CompanyPhone LIKE '%{0}%' OR MobilePhone LIKE '%{0}%'" +
                                                                             " OR CAST(DateOfBirth AS varchar(20)) LIKE '%{0}%'", keyword)
                , string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);

            var lstResult = lst.Select(patient => new
            {
                id = patient.PatientCode,
                patient.FirstName,
                patient.MiddleName,
                patient.LastName,
                patient.HomePhone,
                patient.CompanyPhone,
                patient.MemberType,
                patient.Nationality,
                patient.MobilePhone,
                patient.DateOfBirth,
                patient.Sex,
                patient.Remark,
                propertyToSearch = ParsePatientInfo(patient),
                PatientInfo = ParsePatientName(patient)
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
    /// <param name="firstName"></param>
    /// <param name="middleName"></param>
    /// <param name="lastName"></param>
    /// <param name="memberType"></param>
    /// <param name="dob"></param>
    /// <param name="nationality"></param>
    /// <param name="mobilePhone"></param>
    /// <param name="sex"></param>
    /// <param name="remark"> </param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string CreateSimplePatient(string firstName, string middleName, string lastName
        , string dob, string nationality, string mobilePhone, string sex, string remark)
    {
        try
        {
            // Validate current user have any right to create new patient
            if (!CheckCreating(out _message) && !CheckUpdating(out _message))
            {
                return WebCommon.BuildFailedResult("You have no right to create new patient.");
            }

            // Validate patient's fields
            if (!ValidatePatient(firstName, lastName, nationality, sex, ref _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Validate field
            DateTime dtDOB;
            if (!DateTime.TryParse(dob, out dtDOB))
            {
                return WebCommon.BuildFailedResult("Date of Birth is invalid.");
            }

            #region "Insert"
            var patient = new VcsPatient
            {
                PatientCode = ServiceFacade.SettingsHelper.LocationCode,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Sex = sex,
                DateOfBirth = dtDOB,
                Nationality = nationality,
                MobilePhone = mobilePhone,
                ApptRemark = remark,
                UpdateUser = WebCommon.GetAuthUsername(),
            };

            var patients = DataRepository.VcsPatientProvider.Insert(patient.PatientCode, patient.FirstName, patient.MiddleName
                , patient.LastName, patient.DateOfBirth, patient.Sex, patient.Nationality, patient.CompanyCode, patient.HomePhone
                , patient.MobilePhone, patient.UpdateUser, patient.ApptRemark);

            if (patients == null || !patients.Any())
            {
                return WebCommon.BuildFailedResult("Cannot create new patient. Please contact Administrator.");
            }
            patient = patients[0];

            #endregion

            return WebCommon.BuildSuccessfulResult(new List<object>
                {
                    new
                        {
                            patient.PatientCode,
                            patient.FirstName,
                            patient.MiddleName,
                            patient.LastName,
                            patient.HomePhone,
                            patient.CompanyPhone,
                            patient.MemberType,
                            patient.Nationality,
                            patient.MobilePhone,
                            patient.DateOfBirth,
                            patient.Sex,
                            patient.Remark,
                            PatientInfo = ParsePatientName(patient)
                        }
                });
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult("Cannot create new patient. Please contact Administrator.");
        }
    }

    /// <summary>
    /// Update patient with simple information
    /// </summary>
    /// <param name="id"></param>
    /// <param name="firstName"></param>
    /// <param name="middleName"></param>
    /// <param name="lastName"></param>
    /// <param name="memberType"></param>
    /// <param name="dob"></param>
    /// <param name="nationality"></param>
    /// <param name="mobilePhone"></param>
    /// <param name="sex"></param>
    /// <param name="remark"> </param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string UpdateSimplePatient(string id, string firstName, string middleName, string lastName
        , string dob, string nationality, string mobilePhone, string sex, string remark)
    {
        try
        {
            // Validate current user have any right to update patien's info
            if (!CheckCreating(out _message) && !CheckUpdating(out _message))
            {
                return WebCommon.BuildFailedResult("You have no right to update patient's information.");
            }

            // Validate patient's fields
            if (!ValidatePatient(firstName, lastName, nationality, sex, ref _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Validate field
            DateTime dtDOB;
            if (!DateTime.TryParse(dob, out dtDOB))
            {
                return WebCommon.BuildFailedResult("Date of Birth is invalid.");
            }

            var patients = DataRepository.VcsPatientProvider.GetByPatientCode(id);

            if (patients == null || !patients.Any())
            {
                return WebCommon.BuildFailedResult("Cannot find patient");
            }
            var patient = patients[0];

            #region Update
            patient.FirstName = firstName;
            patient.MiddleName = middleName;
            patient.LastName = lastName;
            patient.Sex = sex;
            patient.DateOfBirth = dtDOB;
            patient.Nationality = nationality;
            patient.MobilePhone = mobilePhone;
            patient.ApptRemark = remark;
            patient.UpdateUser = WebCommon.GetAuthUsername();
            patient.UpdateDate = DateTime.Now;
            DataRepository.VcsPatientProvider.Update(patient.PatientCode, patient.FirstName, patient.MiddleName, patient.LastName
                , patient.DateOfBirth, patient.Sex, patient.Nationality, patient.CompanyCode, patient.HomePhone, patient.MobilePhone
                , patient.UpdateUser, patient.ApptRemark, patient.IsDisabled);
            #endregion

            return WebCommon.BuildSuccessfulResult(new List<object>
                {
                    new
                        {
                            patient.PatientCode,
                            patient.FirstName,
                            patient.MiddleName,
                            patient.LastName,
                            patient.HomePhone,
                            patient.CompanyPhone,
                            patient.MemberType,
                            patient.Nationality,
                            patient.MobilePhone,
                            patient.DateOfBirth,
                            patient.Sex,
                            patient.Remark,
                            PatientInfo = ParsePatientName(patient)
                        }
                });
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
    /// <param name="nationality"> </param>
    /// <param name="sex"></param>
    /// <param name="message"></param>
    /// <param name="memberType"> </param>
    /// <returns></returns>
    private static bool ValidatePatient(string firstname, string lastname, string nationality
        , string sex, ref string message)
    {
        try
        {
            #region Validate
            // Validate empty value for Firstname field
            if (string.IsNullOrEmpty(firstname))
            {
                message = "Please input First Name.";
                return false;
            }

            // Validate empty value for Lastname field
            if (string.IsNullOrEmpty(lastname))
            {
                message = "Please input Last Name.";
                return false;
            }

            // Validate empty value for nationality field
            if (string.IsNullOrEmpty(nationality))
            {
                message = "Please input Nationality.";
                return false;
            }

            // Validate empty value for Lastname field
            if (sex != SexConstant.Male.Key && sex != SexConstant.Female.Key)
            {
                message = "Please select sex.";
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
    private static string ParsePatientInfo(VcsPatient obj)
    {
        string strResult = string.Empty;
        try
        {
            strResult = String.Format("{0} {1}{2}{3}{4}{5}"
                                      , string.IsNullOrEmpty(obj.FirstName) ? string.Empty : obj.FirstName
                                      , string.IsNullOrEmpty(obj.LastName) ? string.Empty : obj.LastName
                                      , String.Format(" - DOB: {0}", obj.DateOfBirth.ToString("dd-MM-yyyy"))
                                      ,
                                      string.IsNullOrEmpty(obj.HomePhone)
                                          ? string.Empty
                                          : String.Format(" - Home Phone: {0}", obj.HomePhone)
                                      ,
                                      string.IsNullOrEmpty(obj.CompanyPhone)
                                          ? string.Empty
                                          : String.Format(" - Company Phone: {0}", obj.CompanyPhone)
                                      ,
                                      string.IsNullOrEmpty(obj.MobilePhone)
                                          ? string.Empty
                                          : String.Format(" - Mobile Phone: {0}", obj.MobilePhone)
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
    private static string ParsePatientName(VcsPatient obj)
    {
        string strResult = string.Empty;
        try
        {
            strResult = String.Format("{0}{1}"
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

    #region Private methods for Appointment
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
            var patients = DataRepository.VcsPatientProvider.GetByPatientCode(obj.PatientCode);
            var patient = new VcsPatient();
            if (patients != null && patients.Any())
            {
                patient = patients[0];
            }

            if (obj.StartTime != null && obj.EndTime != null)
            {
                return new
                {
                    id = obj.Id,
                    start_date = String.Format("{0:dd/MM/yyyy HH:mm:ss}", obj.StartTime),
                    end_date = String.Format("{0:dd/MM/yyyy HH:mm:ss}", obj.EndTime),
                    section_id = obj.Username,
                    text =
                        String.Format("Patient: {0}<br />Note: {1}",
                                      FullNameOfPatient(patient),
                                      obj.Note),
                    DoctorDisplayname = obj.UsernameSource.DisplayName,
                    patient = obj.PatientCode,
                    PatientName = patient.FirstName,
                    PatientInfo = ParsePatientName(patient),
                    obj.ServicesId,
                    ServicesTitle = obj.ServicesIdSource.Title,
                    room = obj.RoomId,
                    RoomTitle = obj.RoomIdSource.Title,
                    note = obj.Note,
                    ReadOnly = (obj.StartTime <= DateTime.Now),
                    color = obj.StatusIdSource.ColorCode,
                    isnew = false
                };
            }
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

    /// <summary>
    /// Build object to string result
    /// </summary>
    /// <param name="lstRosters"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    private static object BuildListSection(TList<Roster> lstRosters, DateTime? date)
    {
        object lst = null;
        try
        {
            DataRepository.RosterProvider.DeepLoad(lstRosters);

            // Lay danh sach doctor theo appointment
            var lstDoctor = new TList<Users>();

            // Lay danh sach service theo appointment
            var lstService = new TList<Services>();

            foreach (var roster in lstRosters)
            {
                Roster roster1 = roster;
                if (!lstDoctor.Exists(doctor => doctor.Username == roster1.Username))
                {
                    lstDoctor.Add(roster.UsernameSource);
                }

                DataRepository.UsersProvider.DeepLoad(roster1.UsernameSource);

                if (!lstService.Exists(service => service.Id == roster1.UsernameSource.ServicesId))
                {
                    lstService.Add(roster1.UsernameSource.ServicesIdSource);
                }
            }

            lst = lstService.Select(service => new
                {
                    service.Id,
                    Title = string.IsNullOrEmpty(service.ShortTitle) ? service.Title : service.ShortTitle,
                    Date = String.Format("{0:yyyy-MM-dd HH:mm:ss}", date),
                    Doctors = lstDoctor.Where(doctor => doctor.ServicesId == service.Id)
                        .Select(doctor => new
                        {
                            key = doctor.Username,
                            label = doctor.DisplayName,
                            Rosters = lstRosters.Where(roster => roster.Username == doctor.Username)
                                .Select(roster => new
                                    {
                                        startTime = roster.StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                        endTime = roster.EndTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                        color = roster.RosterTypeIdSource.ColorCode
                                    })
                        })
                }).ToList();
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
        }
        return lst;
    }
    #endregion

    #region Appointment
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string NewAppointment(string patientCode, string note, DateTime? startTime, DateTime? endTime
        , DateTime? startDate, DateTime? endDate, string doctorId, int? roomId, string status)
    {
        try
        {
            #region Validation and covert value
            // Validate current user have any right to operate this action
            // Validate user right for creating
            if (!CheckCreating(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            if (!WebCommon.ValidateEmpty("Status", status, out _message)
                || !WebCommon.ValidateEmpty("Doctor", doctorId, out _message)
                || !WebCommon.ValidateEmpty("Patient", patientCode, out _message)
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

            var appointment = new Appointment
            {
                PatientCode = patientCode,
                Username = doctorId,
                RoomId = roomId,
                StatusId = status,
                StartTime = dtStart,
                EndTime = dtEnd,
                Note = note,
                CreateUser = Username,
                UpdateUser = Username
            };
            
            if (!BoFactory.AppointmentBO.Insert(appointment, ref _message))
                return WebCommon.BuildFailedResult(_message);

            return WebCommon.BuildSuccessfulResult(BuildListAppointment(appointment), _message);
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult("System error. Please contact Administrator.");
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
    public static string MoveAppointment(string id, DateTime? startTime, DateTime? endTime, string doctorId)
    {
        try
        {
            // Validate current user have any right to operate this action
            // Validate user right for reading
            if (!CheckUpdating(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            if (!WebCommon.ValidateEmpty("Appointment Id", id, out _message)
                || !WebCommon.ValidateEmpty("Doctor", doctorId, out _message)
                || !WebCommon.ValidateEmpty("Start Time", startTime, out _message)
                || !WebCommon.ValidateEmpty("End Time", endTime, out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Get appointment by Id
            Appointment tmpAppt = DataRepository.AppointmentProvider.GetById(id);

            // Validate if roster's not existed
            // return message, nothing more
            if (tmpAppt == null || tmpAppt.IsDisabled)
            {
                return WebCommon.BuildFailedResult("There is no appointment to update.");
            }

            var appointment = (Appointment) tmpAppt.Clone();
            tmpAppt.Dispose();
            appointment.Username = doctorId;
            appointment.StartTime = startTime;
            appointment.EndTime = endTime;
            appointment.UpdateUser = Username;
            appointment.UpdateDate = DateTime.Now;

            if (!BoFactory.AppointmentBO.Update(ref appointment, ref _message))
                return WebCommon.BuildFailedResult(_message);

            return WebCommon.BuildSuccessfulResult(BuildListAppointment(appointment));
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult("System error. Please contact Administrator.");
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
    public static string UpdateAppointment(string id, string patientCode, string note, DateTime? startTime, DateTime? endTime
        , DateTime? startDate, DateTime? endDate, string doctorId, int? roomId, string status)
    {
        try
        {
            #region Validation and covert value
            // Validate current user have any right to operate this action
            // Validate user right for creating
            if (!CheckUpdating(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            if (!WebCommon.ValidateEmpty("Appointment", id, out _message)
                || !WebCommon.ValidateEmpty("Status", status, out _message)
                || !WebCommon.ValidateEmpty("Doctor", doctorId, out _message)
                || !WebCommon.ValidateEmpty("Patient", patientCode, out _message)
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

            var appointment = new Appointment
            {
                Id = id,
                PatientCode = patientCode,
                Username = doctorId,
                RoomId = roomId,
                StatusId = status,
                StartTime = dtStart,
                EndTime = dtEnd,
                Note = note,
                UpdateUser = Username
            };

            if (!BoFactory.AppointmentBO.Update(ref appointment, ref _message))
                return WebCommon.BuildFailedResult(_message);

            return WebCommon.BuildSuccessfulResult(BuildListAppointment(appointment));
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult("System error. Please contact Administrator.");
        }
    }

    /// <summary>
    /// Get list of appointment in day
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="date">Date</param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetAppointments(string mode, DateTime? date)
    {
        try
        {
            // Check active user
            if (!CheckReading(out _message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Goi ham lay danh sach appointment theo ngay, mode
            var lstAppt = BoFactory.AppointmentBO.GetByDateMode(date, mode, out _message);
            if (!string.IsNullOrEmpty(_message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Goi ham lay danh sach roster
            var lstRoster = BoFactory.RosterBO.GetByDateMode(date, mode, out _message);
            if (!string.IsNullOrEmpty(_message))
            {
                return WebCommon.BuildFailedResult(_message);
            }

            // Tra ve danh sach
            return WebCommon.BuildSuccessfulResult(new
                {
                    Events = BuildListAppointment(lstAppt),
                    Sections = BuildListSection(lstRoster, date)
                });
        }
        catch (Exception ex)
        {
            LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            return WebCommon.BuildFailedResult(ex.Message);
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
            var objPatient = DataRepository.VcsPatientProvider.GetByPatientCode(appointment.PatientCode)[0];
            string strSubject = String.Format("Delete Appointment: {0} - {1} {2}", appointment.ServicesIdSource.Title
                , objPatient.FirstName, objPatient.LastName);
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
            var objPatients = DataRepository.VcsPatientProvider.GetByPatientCode(objAppt.PatientCode);
            if (objPatients == null || !objPatients.Any())
            {
                return WebCommon.BuildFailedResult("Patient is not exist.");
            }
            var objPatient = objPatients[0];

            var lstPatient = new List<object>
                                 {
                                     new
                                         {
                                             Id = objPatient.PatientCode,
                                             FirstName = objPatient.FirstName ?? "&nbsp;",
                                             LastName = objPatient.LastName ?? "&nbsp;",
                                             HomePhone = objPatient.HomePhone ?? "&nbsp;",
                                             WorkPhone = objPatient.CompanyPhone ?? "&nbsp;",
                                             CellPhone = objPatient.MobilePhone ?? "&nbsp;",
                                             Birthdate = objPatient.DateOfBirth.ToString("MM-dd-yyyy"),
                                             objPatient.Sex,
                                             Remark = objPatient.Remark ?? "&nbsp;"
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

    #region Build string
    /// <summary>
    /// Build fullname with title for patient
    /// </summary>
    /// <param name="patient"></param>
    /// <returns></returns>
    private static string FullNameOfPatient(VcsPatient patient)
    {
        return String.Format("{0}{1}",
                             string.IsNullOrEmpty(patient.FirstName)
                                 ? string.Empty
                                 : patient.FirstName + " ",
                             string.IsNullOrEmpty(patient.LastName)
                                 ? string.Empty
                                 : patient.LastName);
    }
    #endregion
}
