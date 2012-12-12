using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using AppointmentBusiness.Util;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using Appt.Common.Constants;
using Common.Util;
using Log.Controller;

namespace AppointmentBusiness.BO
{
    public class AppointmentBO : IAppointmentBO
    {
        public bool Insert(Appointment appointment, ref string message)
        {
            try
            {
                if (!ValidateAppointment(appointment, false, ref message)) return false;
                DataRepository.AppointmentProvider.Insert(appointment);
                GlobalUtilities.WriteLog(BuildApptHistory(appointment), null, appointment.UpdateUser, DataRepository.Provider.ExecuteDataSet);
                SendEmail(appointment, false);
                return true;
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return false;
        }

        public bool Update(ref Appointment appointment, ref string message)
        {
            try
            {
                // Goi ham kiem tra appointment
                if (!ValidateAppointment(appointment, true, ref message)) return false;

                // Get old roster by id
                var oldAppt = DataRepository.AppointmentProvider.GetById(appointment.Id);

                // Validate if roster's not existed
                // return message, nothing more
                if (oldAppt == null || oldAppt.IsDisabled)
                {
                    message = "There is no appointment to update.";
                    return false;
                }

                var comparedAppt = oldAppt.Copy();

                // Gan gia tri moi
                // Ghi log moi khi thay doi gia tri
                oldAppt.PatientCode = appointment.PatientCode;
                GlobalUtilities.WriteLog(BuildApptHistory(comparedAppt), BuildApptHistory(oldAppt), appointment.UpdateUser, DataRepository.Provider.ExecuteDataSet);
                comparedAppt.PatientCode = appointment.PatientCode;

                oldAppt.Username = appointment.Username;
                GlobalUtilities.WriteLog(BuildApptHistory(comparedAppt), BuildApptHistory(oldAppt), appointment.UpdateUser, DataRepository.Provider.ExecuteDataSet);
                comparedAppt.Username = appointment.Username;

                oldAppt.RoomId = appointment.RoomId;
                GlobalUtilities.WriteLog(BuildApptHistory(comparedAppt), BuildApptHistory(oldAppt), appointment.UpdateUser, DataRepository.Provider.ExecuteDataSet);
                comparedAppt.RoomId = appointment.RoomId;

                oldAppt.StatusId = appointment.StatusId;
                GlobalUtilities.WriteLog(BuildApptHistory(comparedAppt), BuildApptHistory(oldAppt), appointment.UpdateUser, DataRepository.Provider.ExecuteDataSet);
                comparedAppt.StatusId = appointment.StatusId;

                oldAppt.StartTime = appointment.StartTime;
                GlobalUtilities.WriteLog(BuildApptHistory(comparedAppt), BuildApptHistory(oldAppt), appointment.UpdateUser, DataRepository.Provider.ExecuteDataSet);
                comparedAppt.StartTime = appointment.StartTime;

                oldAppt.EndTime = appointment.EndTime;
                GlobalUtilities.WriteLog(BuildApptHistory(comparedAppt), BuildApptHistory(oldAppt), appointment.UpdateUser, DataRepository.Provider.ExecuteDataSet);
                comparedAppt.EndTime = appointment.EndTime;

                oldAppt.Note = appointment.Note;
                GlobalUtilities.WriteLog(BuildApptHistory(comparedAppt), BuildApptHistory(oldAppt), appointment.UpdateUser, DataRepository.Provider.ExecuteDataSet);
                comparedAppt.Note = appointment.Note;

                oldAppt.UpdateUser = appointment.UpdateUser;

                DataRepository.AppointmentProvider.Update(oldAppt);

                // Gan lai doi tuong appointment
                appointment = oldAppt;
                SendEmail(appointment, true);
                return true;
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return false;
        }

        public TList<Appointment> GetByDateMode(DateTime? date, string mode, out string message)
        {
            message = string.Empty;
            try
            {
                // Get datetime to filter
                DateTime fromDate, toDate;
                if (!CommonBO.GetDateTimeByMode(date, mode, out fromDate, out toDate))
                {
                    message = "Current date is invalid.";
                    goto StepResult;
                }

                // Get appointment
                int count;
                TList<Appointment> lstAppt =
                    DataRepository.AppointmentProvider.GetPaged(
                        String.Format("IsDisabled = 'False' AND (StartTime BETWEEN N'{0}' AND N'{1}'"
                                      + " OR EndTime BETWEEN N'{0}' AND N'{1}'"
                                      + " OR (StartTime <= N'{0}' AND EndTime >= N'{1}'))"
                                      , fromDate.ToString("yyyy-MM-dd HH:mm:ss.000"),
                                      toDate.ToString("yyyy-MM-dd HH:mm:ss.000")),
                        string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength,
                        out count);

                // Set out of date for passed appointment
                //lstAppt.ForEach(x =>
                //{
                //    x.StatusId = x.StartTime <= DateTime.Now &&
                //                 x.StatusId != BoFactory.StatusBO.Complete &&
                //                 x.StatusId != BoFactory.StatusBO.Cancel
                //                     ? BoFactory.StatusBO.Complete
                //                     : x.StatusId;
                //});
                //DataRepository.AppointmentProvider.Save(lstAppt);

                return lstAppt;
            }
            catch (Exception ex)
            {
                message = "System error. Please contact Administratos.";
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
        StepResult:
            return new TList<Appointment>();
        }

        public object BuildApptHistory(Appointment appt)
        {
            try
            {
                //var patient = DataRepository.VcsPatientProvider.GetByPatientCode(appt.PatientCode);
                var room = DataRepository.RoomProvider.GetById(Convert.ToInt32(appt.RoomId));
                var service = DataRepository.ServicesProvider.GetById(Convert.ToInt32(appt.ServicesId));

                if (room != null && service != null)
                    return new AppointmentLog
                    {
                        Id = appt.Id,
                        PatientCode = appt.PatientCode,
                        Username = appt.Username,
                        Room = room.Title,
                        Service = service.Title,
                        Status = appt.StatusId,
                        StartTime = Convert.ToDateTime(appt.StartTime),
                        EndTime = Convert.ToDateTime(appt.EndTime),
                        Note = appt.Note,
                    };
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return null;
        }

        public void InsertHistory(Appointment appointment, string username, string fromStatus, string toStatus)
        {
            try
            {
                var patient = DataRepository.PatientProvider.GetByPatientCode(appointment.PatientCode);

                var history = new AppointmentHistory
                    {
                        Guid = Guid.NewGuid(),
                        AppointmentId = appointment.Id,
                        Note = String.Format("User {0} change appointment {1} from status {2} to {3}, patient {4}, user {5}"
                        , username, appointment.Id, fromStatus, toStatus, patient.LastName, appointment.Username),
                        CreateUser = username,
                        CreateDate = DateTime.Now
                    };
                DataRepository.AppointmentHistoryProvider.Insert(history);
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
        }

        #region Private methods
        /// <summary>
        /// Kiem tra appointment
        /// </summary>
        /// <param name="oldAppt"></param>
        /// <param name="isUpdate"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool ValidateAppointment(Appointment oldAppt, bool isUpdate, ref string message)
        {
            try
            {
                #region Validate exsited
                // Kiem tra status co ton tai khong
                var objStatus = DataRepository.StatusProvider.GetById(oldAppt.StatusId);
                if (objStatus == null)
                {
                    message = "Status is not exist.";
                    return false;
                }

                // Kiem tra doctor co ton tai khong
                var objUser = DataRepository.UsersProvider.GetByUsername(oldAppt.Username);
                if (objUser == null || objUser.IsDisabled)
                {
                    message = "Doctor is not exist.";
                    return false;
                }

                // Kiem tra patient co ton tai khong
                var objPatients = DataRepository.VcsPatientProvider.GetByPatientCode(oldAppt.PatientCode);
                if (objPatients == null || !objPatients.Any())
                {
                    message = "Patient is not exist.";
                    return false;
                }
                var objPatient = objPatients[0];

                // Kiem tra room co ton tai khong
                Room objRoom = null;
                if (oldAppt.RoomId != null)
                {
                    objRoom = DataRepository.RoomProvider.GetById(Convert.ToInt32(oldAppt.RoomId));
                    if (objRoom == null || objRoom.IsDisabled)
                    {
                        message = "Room is not exist.";
                        return false;
                    }

                    // Cho nay can kiem tra phong co dang su dung ko
                    // Hien tai thi chua
                }
                #endregion

                #region Validate date time
                // Kiem tra ngay bat dau, ngay ket thuc
                if (oldAppt.StartTime >= oldAppt.EndTime)
                {
                    message = "End time must be greater than start time.";
                    return false;
                }

                // If oldAppt is created in a passed or current day
                if (DateTime.Now >= oldAppt.StartTime)
                {
                    message = "You can not change appointment to passed or current date.";
                    return false;
                }
                #endregion

                #region Kiem tra xem doctor duoc tao appointment co dang ky Roster hay khong
                int count;
                // Get roster list
                var lstRoster =
                    DataRepository.RosterProvider.GetPaged(
                        String.Format("Username = '{0}' AND StartTime <= '{1}' AND EndTime >= '{2}' AND IsDisabled = 'False'",
                                      oldAppt.Username, String.Format("{0:yyyy-MM-dd HH:mm:ss}", oldAppt.StartTime),
                                      String.Format("{0:yyyy-MM-dd HH:mm:ss}", oldAppt.EndTime)), string.Empty, 0,
                        ServiceFacade.SettingsHelper.GetPagedLength, out count);

                // If there is no roster, return error
                if (!lstRoster.Any())
                {
                    message = String.Format("Cannot create appointment because {0} has no roster from {1} to {2}."
                        , objUser.DisplayName, String.Format("{0:yyyy-MM-dd HH:mm}", oldAppt.StartTime)
                        , String.Format("{0:yyyy-MM-dd HH:mm}", oldAppt.EndTime));
                    return false;
                }

                // If there has some rosters but not is booked, return error
                DataRepository.RosterProvider.DeepLoad(lstRoster);
                //var lstRosterType = lstRoster.Select(x => x.RosterTypeIdSource.IsBooked);
                //if (!lstRosterType.Any())
                //{
                //    message = String.Format("Cannot create appointment because roster {0} is not booked roster."
                //        , lstRoster[0].RosterTypeIdSource.Title);
                //    return false;
                //}
                #endregion

                #region Check Conflict
                // Neu cac trang thai khong phai la cancel, done, block [may cai nay co the thay doi] thi kiem tra conflict
                if (oldAppt.StatusId != ApptStatus.Cancel && oldAppt.StatusId != ApptStatus.Done && oldAppt.StatusId != ApptStatus.Blocked)
                {
                    // Check conflict Patient
                    if (!CheckConflict("PatientCode", oldAppt.PatientCode, string.Empty,
                        String.Format("{0} {1}", objPatient.FirstName, objPatient.LastName), Convert.ToDateTime(oldAppt.StartTime)
                            , Convert.ToDateTime(oldAppt.EndTime), oldAppt.Id, ref message))
                    {
                        message = String.Format("Cannot create appointment because {0} has another appointment from {1} to {2}."
                            , objPatient.FirstName + " " + objPatient.LastName
                            , String.Format("{0:yyyy-MM-dd HH:mm}", oldAppt.StartTime)
                            , String.Format("{0:yyyy-MM-dd HH:mm}", oldAppt.EndTime));
                        return false;
                    }

                    if (!CheckConflict("Username", objUser.Username, string.Empty, objUser.DisplayName
                        , Convert.ToDateTime(oldAppt.StartTime), Convert.ToDateTime(oldAppt.EndTime), oldAppt.Id, ref message))
                    {
                        return false;
                    }

                    // Check conflict Room
                    if (oldAppt.RoomId != null && objRoom != null)
                    {
                        if (!CheckConflict("RoomId", oldAppt.RoomId.ToString(), "Room"
                            , objRoom.Title, Convert.ToDateTime(oldAppt.StartTime), Convert.ToDateTime(oldAppt.EndTime), oldAppt.Id, ref message))
                        {
                            return false;
                        }
                    }
                }
                #endregion

                // Tao Id moi bang cach truyen Id cu vao
                // Neu la update thi khong can RosterId moi
                oldAppt.Id = isUpdate ? oldAppt.Id : BoFactory.IdBO.AppointmentId(oldAppt.Id);
                oldAppt.ServicesId = objUser.ServicesId;
                oldAppt.RosterId = lstRoster[0].Id;

                return true;
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return false;
        }
        /// <summary>
        /// Check conflict for appointment follow column
        /// </summary>
        /// <param name="column">Cot se duoc kiem tra conflict</param>
        /// <param name="compareValue">Gia tri cua cot can duoc kiem tra</param>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <param name="id">Truyen vao Id cua Appointment</param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static bool CheckConflict(string column, string compareValue, string name, string title,
            DateTime dtStart, DateTime dtEnd, string id, ref string message)
        {
            try
            {
                int count;
                string query =
                    String.Format("{3} = '{0}' AND IsDisabled = 'False' AND StartTime < '{1}' AND EndTime > '{2}'" +
                                  " AND StatusId NOT IN ('{4}', '{5}', '{6}') AND Id <> '{7}'",
                                  compareValue, dtEnd.ToString("yyyy-MM-dd HH:mm:ss"),
                                  dtStart.ToString("yyyy-MM-dd HH:mm:ss"), column,
                                  ApptStatus.Cancel, ApptStatus.Done, ApptStatus.Blocked,
                                  string.IsNullOrEmpty(id) ? string.Empty : id);
                DataRepository.AppointmentProvider.GetPaged(query, "Id desc", 0,
                                                                      ServiceFacade.SettingsHelper.GetPagedLength,
                                                                      out count);

                if (count > 0)
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

        /// <summary>
        /// Gui email cho doctor sau khi insert/update
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="isUpdate"></param>
        /// <returns></returns>
        private bool SendEmail(Appointment appointment, bool isUpdate)
        {
            try
            {
                var objPatient = DataRepository.VcsPatientProvider.GetByPatientCode(appointment.PatientCode)[0];

                DataRepository.AppointmentProvider.DeepLoad(appointment);
                string strSubject = String.Format("{3} Appointment: {0}{1}{2}", appointment.ServicesIdSource.Title,
                                                  objPatient.FirstName, objPatient.LastName, isUpdate ? "Change" : "New");
                string strBody = isUpdate ? "You have a new change for appointment" : "You have a new appointment";
                string strSummary = string.Empty;
                string strDescription = string.Empty;
                string strLocation = appointment.RoomId == null ? " - " :
                    String.Format("Room {0}", appointment.RoomIdSource.Title);
                string strAlarmSummary = String.Format("{0} minutes left for appointment with {1} {2}",
                    ServiceFacade.SettingsHelper.TimeLeftRemindAppointment
                    , objPatient.FirstName, objPatient.LastName);
                var objMail = new MailAppointment(strSubject, strBody, strSummary, strDescription, strLocation, strAlarmSummary,
                    Convert.ToDateTime(appointment.StartTime), Convert.ToDateTime(appointment.EndTime));
                objMail.AddMailAddress(appointment.UsernameSource.Email, appointment.UsernameSource.DisplayName);
                objMail.SendMail();

                return true;
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return false;
        }
        #endregion
    }

    class AppointmentLog
    {
        [BrowsableAttribute(false), XmlIgnore()]
        public string TableName
        {
            get { return "Appointment"; }
        }

        [BrowsableAttribute(false), XmlIgnore()]
        public string[] TableColumns
        {
            get
            {
                return new[] { "Id", "PatientCode", "Username", "Room", "Service", "Status", "Note", "StartTime", "EndTime" };
            }
        }

        [Description("Id"), DataObjectFieldAttribute(true, true, false)]
        public string Id { get; set; }
        [Description("Patient Code")]
        public string PatientCode { get; set; }
        [Description("Doctor")]
        public string Username { get; set; }
        [Description("Room")]
        public string Room { get; set; }
        [Description("Service")]
        public string Service { get; set; }
        [Description("Status")]
        public string Status { get; set; }
        [Description("Start Time")]
        public DateTime StartTime { get; set; }
        [Description("End Time")]
        public DateTime EndTime { get; set; }
        [Description("Note")]
        public string Note { get; set; }
    }
}
