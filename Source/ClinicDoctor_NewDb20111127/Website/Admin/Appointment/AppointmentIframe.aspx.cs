using System;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web;
using ClinicDoctor.Data;
using ClinicDoctor.Entities;
using ClinicDoctor.Settings.BusinessLayer;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using LogUtil;

public partial class Admin_Appointment_AppointmentIframe : System.Web.UI.Page
{
    static string strSeperateStaff = " | ";
    static char chrSeperateStaff = '|';

    #region "Roster"
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string SaveEvent(string PatientId, string ContentId, string Note,
        string StartTime, string EndTime, string StartDate, string EndDate, string DoctorUsername, string RoomId)
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
            if (string.IsNullOrEmpty(PatientId) || PatientId == "-1")
            {
                result = @"[{ 'result': 'false', 'message': 'You must choose patient.', 'data': [] }]";
                return result;
            }

            // Check Content
            if (string.IsNullOrEmpty(ContentId) || ContentId == "-1")
            {
                result = @"[{ 'result': 'false', 'message': 'You must choose content.', 'data': [] }]";
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
            if (new DateTime(dtNow.Year, dtNow.Month, dtNow.Day) >= new DateTime(dtStart.Year, dtStart.Month, dtStart.Day))
            {
                result = @"[{ 'result': 'false', 'message': 'You can not change roster to passed or current date.', 'data': [] }]";
                goto StepResult;
            }

            // Check Doctor
            if (string.IsNullOrEmpty(DoctorUsername) || DoctorUsername == "-1")
            {
                result = @"[{ 'result': 'false', 'message': 'You must choose doctor.', 'data': [] }]";
                goto StepResult;
            }

            // Check Room
            if (string.IsNullOrEmpty(RoomId) || RoomId == "-1")
            {
                result = @"[{ 'result': 'false', 'message': 'You must choose room.', 'data': [] }]";
                goto StepResult;
            }
            #endregion

            string strUserName = EntitiesUtilities.GetAuthName();
            string strPatientId = PatientId;
            string[] arr = ContentId.Split(chrSeperateStaff);
            long lContentId = Convert.ToInt64(arr[1]);
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

            // Check exists Content
            Content objContent = DataRepository.ContentProvider.GetByIdIsDisabled(tm, lContentId, false);
            if (objPatient == null)
            {
                tm.Rollback();
                result = @"[{ 'result': 'false', 'message': 'Content is not exist.', 'data': '[]' }]";
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
            if (objDoctor == null)
            {
                tm.Rollback();
                result = @"[{ 'result': 'false', 'message': 'Room is not exist.', 'data': '[]' }]";
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
            newObj.ContentId = lContentId;
            newObj.ContentTitle = objContent.Title;
            newObj.DoctorEmail = objDoctor.Email;
            newObj.DoctorUsername = DoctorUsername;
            newObj.DoctorShortName = objDoctor.ShortName;
            newObj.RoomId = lRoomId;
            newObj.RoomTitle = objRoom.Title;
            //newObj.NurseUsername = string.Empty;
            //newObj.NurseShortName = string.Empty;
            newObj.Note = Note;
            newObj.StartTime = dtStart;
            newObj.EndTime = dtEnd;
            newObj.ColorCode = string.Empty; // Cho nay se them 1 field chon mau
            newObj.CreateUser = strUserName;
            newObj.UpdateUser = strUserName;

            DataRepository.AppointmentProvider.Insert(tm, newObj);
            #endregion

            #region "Return string"
            result = @"[{ 'result': 'true', 'message': '', 'data': ["
                + @"{id: '" + newObj.Id + @"',"
                + @"start_date: '" + newObj.StartTime.Value.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                + @"end_date: '" + newObj.EndTime.Value.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                + @"section_id: '" + newObj.DoctorUsername + @"',"
                + @"text: '" + newObj.ContentTitle + @"<br />Doctor: " + newObj.DoctorUsername + @"<br />";

            if (!string.IsNullOrEmpty(newObj.Note))
                result += newObj.Note;

            result += @"',DoctorUserName: '" + newObj.DoctorUsername + @"',"
                + @"DoctorShortName: '" + newObj.DoctorShortName + @"',"
                + @"CustomerId: '" + newObj.CustomerId + @"',"
                + @"CustomerName: '" + newObj.CustomerName + @"',"
                + @"ContentId: '" + objContent.FuncId + chrSeperateStaff.ToString() + newObj.ContentId + @"',"
                + @"ContentTitle: '" + newObj.ContentTitle + @"',"
                + @"RoomId: '" + newObj.RoomId + @"',"
                + @"RoomTitle: '" + newObj.RoomTitle + @"',"
                + @"NurseUsername: '" + newObj.NurseUsername + @"',"
                + @"NurseShortName: '" + newObj.NurseShortName + @"',"
                + @"note: '" + newObj.Note + @"',"
                + @"color: '" + newObj.ColorCode + @"',"
                + @"isnew: 'false'"
                + @"}"
               + @"] }]";
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

            tm.Commit();

        }
        catch (Exception ex)
        {
            SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.SaveEvent", ex);

            tm.Rollback();

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            goto StepResult;
        }

    StepResult:
        return result;
    }

    #region "Update Roster"
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string UpdateEventSave(string Id, string PatientId, string ContentId, string Note,
        string StartTime, string EndTime, string StartDate, string EndDate, string DoctorUsername, string RoomId)
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

            return UpdateEvent(Id, PatientId, ContentId, Note, dtStart, dtEnd, DoctorUsername, RoomId);
        }
        catch (Exception ex)
        {
            SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.UpdateEventSave", ex);

            return @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string UpdateEventMove(string Id, string DoctorUsername, string StartTime, string EndTime)
    {
        string result = string.Empty;
        try
        {
            // Get start date and end date
            DateTime dtStart = Convert.ToDateTime(StartTime);
            DateTime dtEnd = Convert.ToDateTime(EndTime);

            Appointment obj = DataRepository.AppointmentProvider.GetByIdIsDisabled(Id, false);
            if (obj == null || obj.IsComplete == true)
            {
                result = @"[{ 'result': 'false', 'message': 'There is no appointment to change or appointment is expired.', 'data': [] }]";
                return result;
            }

            // Check if user drag to another function
            TList<DoctorFunc> lstObj = DataRepository.DoctorFuncProvider.GetByDoctorUserNameIsDisabled(DoctorUsername, false);
            Content objContent = DataRepository.ContentProvider.GetByIdIsDisabled(obj.ContentId, false);
            if (lstObj.Count == 0 || objContent == null)
            {
                #region "Return string"
                result = @"{id: '" + obj.Id + @"',"
                    + @"start_date: '" + obj.StartTime.Value.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"end_date: '" + obj.EndTime.Value.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"section_id: '" + obj.DoctorUsername + @"',"
                    + @"text: '" + obj.ContentTitle + @"<br />Doctor: " + obj.DoctorUsername + @"<br />";

                if (!string.IsNullOrEmpty(obj.Note))
                    result += obj.Note;

                result += @"',DoctorUserName: '" + obj.DoctorUsername + @"',"
                    + @"DoctorShortName: '" + obj.DoctorShortName + @"',"
                    + @"CustomerId: '" + obj.CustomerId + @"',"
                    + @"CustomerName: '" + obj.CustomerName + @"',"
                    + @"ContentId: '" + obj.ContentId + @"',"
                    + @"ContentTitle: '" + obj.ContentTitle + @"',"
                    + @"RoomId: '" + obj.RoomId + @"',"
                    + @"RoomTitle: '" + obj.RoomTitle + @"',"
                    + @"NurseUsername: '" + obj.NurseUsername + @"',"
                    + @"NurseShortName: '" + obj.NurseShortName + @"',"
                    + @"note: '" + obj.Note + @"',"
                    + @"color: '" + obj.ColorCode + @"',"
                    + @"isnew: 'false'"
                    + @"}";
                #endregion
                result = @"[{ 'result': 'false', 'message': 'You cannot change content appointment to another functionality by dragging.', 'data': [" + result + "] }]";
                return result;
            }
            else
            {
                if (lstObj[0].FuncId != objContent.FuncId)
                {
                    #region "Return string"
                    result = @"{id: '" + obj.Id + @"',"
                        + @"start_date: '" + obj.StartTime.Value.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                        + @"end_date: '" + obj.EndTime.Value.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                        + @"section_id: '" + obj.DoctorUsername + @"',"
                        + @"text: '" + obj.ContentTitle + @"<br />Doctor: " + obj.DoctorUsername + @"<br />";

                    if (!string.IsNullOrEmpty(obj.Note))
                        result += obj.Note;

                    result += @"',DoctorUserName: '" + obj.DoctorUsername + @"',"
                        + @"DoctorShortName: '" + obj.DoctorShortName + @"',"
                        + @"CustomerId: '" + obj.CustomerId + @"',"
                        + @"CustomerName: '" + obj.CustomerName + @"',"
                        + @"ContentId: '" + obj.ContentId + @"',"
                        + @"ContentTitle: '" + obj.ContentTitle + @"',"
                        + @"RoomId: '" + obj.RoomId + @"',"
                        + @"RoomTitle: '" + obj.RoomTitle + @"',"
                        + @"NurseUsername: '" + obj.NurseUsername + @"',"
                        + @"NurseShortName: '" + obj.NurseShortName + @"',"
                        + @"note: '" + obj.Note + @"',"
                        + @"color: '" + obj.ColorCode + @"',"
                        + @"isnew: 'false'"
                        + @"}";
                    #endregion
                    result = @"[{ 'result': 'false', 'message': 'You cannot change content appointment to another functionality by dragging.', 'data': [" + result + "] }]";
                    return result;
                }
            }

            return UpdateEvent(Id, obj.CustomerId, chrSeperateStaff.ToString() + obj.ContentId.ToString(), obj.Note, dtStart,
                dtEnd, DoctorUsername, obj.RoomId.ToString());
        }
        catch (Exception ex)
        {
            SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.UpdateEventMove", ex);

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            return result;
        }
    }

    // Update roster
    private static string UpdateEvent(string Id, string PatientId, string ContentId, string Note, DateTime StartTime, DateTime EndTime,
        string DoctorUsername, string RoomId)
    {
        string result = string.Empty;

        TransactionManager tm = DataRepository.Provider.CreateTransaction();
        try
        {
            #region "Validate"
            // Check Patient
            if (string.IsNullOrEmpty(PatientId) || PatientId == "-1")
            {
                result = @"[{ 'result': 'false', 'message': 'You must choose patient.', 'data': [] }]";
                return result;
            }

            // Check Content
            if (string.IsNullOrEmpty(ContentId) || ContentId == "-1")
            {
                result = @"[{ 'result': 'false', 'message': 'You must choose content.', 'data': [] }]";
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
            if (new DateTime(dtNow.Year, dtNow.Month, dtNow.Day) >= new DateTime(dtStart.Year, dtStart.Month, dtStart.Day))
            {
                result = @"[{ 'result': 'false', 'message': 'You can not change roster to passed or current date.', 'data': [] }]";
                goto StepResult;
            }

            // Check Doctor
            if (string.IsNullOrEmpty(DoctorUsername) || DoctorUsername == "-1")
            {
                result = @"[{ 'result': 'false', 'message': 'You must choose doctor.', 'data': [] }]";
                goto StepResult;
            }

            // Check Room
            if (string.IsNullOrEmpty(RoomId) || RoomId == "-1")
            {
                result = @"[{ 'result': 'false', 'message': 'You must choose room.', 'data': [] }]";
                goto StepResult;
            }
            #endregion

            string strUserName = EntitiesUtilities.GetAuthName();
            string strPatientId = PatientId;
            string[] arr = ContentId.Split(chrSeperateStaff);
            long lContentId = Convert.ToInt64(arr[1]);
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

            // Check exists Content
            Content objContent = DataRepository.ContentProvider.GetByIdIsDisabled(tm, lContentId, false);
            if (objPatient == null)
            {
                tm.Rollback();
                result = @"[{ 'result': 'false', 'message': 'Content is not exist.', 'data': '[]' }]";
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
            if (objDoctor == null)
            {
                tm.Rollback();
                result = @"[{ 'result': 'false', 'message': 'Room is not exist.', 'data': '[]' }]";
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
            obj.ContentId = lContentId;
            obj.ContentTitle = objContent.Title;
            obj.DoctorEmail = objDoctor.Email;
            obj.DoctorUsername = DoctorUsername;
            obj.DoctorShortName = objDoctor.ShortName;
            obj.RoomId = lRoomId;
            obj.RoomTitle = objRoom.Title;
            //obj.NurseUsername = string.Empty;
            //obj.NurseShortName = string.Empty;
            obj.Note = Note;
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

            #region "Return String"
            result = @"[{ 'result': 'true', 'message': '', 'data': ["
                + @"{id: '" + obj.Id + @"',"
                + @"start_date: '" + obj.StartTime.Value.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                + @"end_date: '" + obj.EndTime.Value.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                + @"section_id: '" + obj.DoctorUsername + @"',"
                + @"text: '" + obj.ContentTitle + @"<br />Doctor: " + obj.DoctorUsername + @"<br />";

            if (!string.IsNullOrEmpty(obj.Note))
                result += obj.Note;

            result += @"',DoctorUserName: '" + obj.DoctorUsername + @"',"
                + @"DoctorShortName: '" + obj.DoctorShortName + @"',"
                + @"CustomerId: '" + obj.CustomerId + @"',"
                + @"CustomerName: '" + obj.CustomerName + @"',"
                + @"ContentId: '" + objContent.FuncId + chrSeperateStaff.ToString() + obj.ContentId + @"',"
                + @"ContentTitle: '" + obj.ContentTitle + @"',"
                + @"RoomId: '" + obj.RoomId + @"',"
                + @"RoomTitle: '" + obj.RoomTitle + @"',"
                + @"NurseUsername: '" + obj.NurseUsername + @"',"
                + @"NurseShortName: '" + obj.NurseShortName + @"',"
                + @"note: '" + obj.Note + @"',"
                + @"color: '" + obj.ColorCode + @"',"
                + @"isnew: 'false'"
                + @"}"
               + @"] }]";
            #endregion

            tm.Commit();
        }
        catch (Exception ex)
        {
            SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.UpdateEvent", ex);

            tm.Rollback();

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            return result;
        }

    StepResult:
        return result;
    }
    #endregion

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string LoadRoster(string Mode, string CurrentDateView)
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

            string username = EntitiesUtilities.GetAuthName();
            Staff obj = DataRepository.StaffProvider.GetByUserNameIsDisabled(username, false);
            if (obj == null)
            {
                result = @"[{ 'result': 'false', 'message': 'Doctor is not exist.', 'data': '[]' }]";
                goto StepResult;
            }
            /*
            #region "Load roster in appointment"
            TList<DoctorRoster> lstObj = DataRepository.DoctorRosterProvider.GetByIsDisabled(tm, false);

            string color = string.Empty;
            foreach (DoctorRoster item in lstObj)
            {
                result += @"{id: '" + item.Id + @"',"
                    + @"start_date: '" + item.StartTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"end_date: '" + item.EndTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"section_id: '" + item.DoctorUserName + @"',"
                    + @"text: '" + item.RosterTypeTitle + @"<br />";

                if (!string.IsNullOrEmpty(item.Note))
                    result += item.Note;

                result += @"',DoctorUserName: '" + item.DoctorUserName + @"',"
                    + @"DoctorShortName: '" + item.DoctorShortName + @"',"
                    + @"RosterTypeId: '" + item.RosterTypeId + @"',"
                    + @"RosterTypeTitle: '" + item.RosterTypeTitle + @"',"
                    + @"RosterTypeId: '" + item.RosterTypeId + @"',"
                    + @"note: '" + item.Note + @"',";

                // If roster is passed, set color code and is completed
                if (item.StartTime <= DateTime.Now)
                {
                    item.ColorCode = ServiceFacade.SettingsHelper.CompleteColor;
                    item.IsComplete = true;
                    DataRepository.DoctorRosterProvider.Save(tm, item);

                    result += @"readonly: true,";
                }
                // Set color to recognize that's is roster
                result += @"color: '" + ServiceFacade.SettingsHelper.RosterInAppointmentColor + @"',";
                result += @"isnew: 'false'},";
            }
            #endregion
            */
            #region "Load appointment"
            TList<Appointment> lstApt = DataRepository.AppointmentProvider.GetByIsDisabled(tm, false);

            foreach (Appointment item in lstApt)
            {
                Content objContent = DataRepository.ContentProvider.GetByIdIsDisabled(item.ContentId, false);
                if (objContent == null)
                {
                    tm.Rollback();
                    result = @"[{ 'result': 'false', 'message': 'Content " + item.ContentTitle
                        + " has no function or function is disabled. Please contact Administrator.', 'data': '[]' }]";
                    goto StepResult;
                }

                result += @"{id: '" + item.Id + @"',"
                    + @"start_date: '" + item.StartTime.Value.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"end_date: '" + item.EndTime.Value.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"section_id: '" + item.DoctorUsername + @"',"
                    + @"text: '" + item.ContentTitle + @"<br />Doctor: " + item.DoctorUsername + @"<br />";

                if (!string.IsNullOrEmpty(item.Note))
                    result += item.Note;

                result += @"',DoctorUserName: '" + item.DoctorUsername + @"',"
                    + @"DoctorShortName: '" + item.DoctorShortName + @"',"
                    + @"CustomerId: '" + item.CustomerId + @"',"
                    + @"CustomerName: '" + item.CustomerName + @"',"
                    + @"ContentId: '" + objContent.FuncId + chrSeperateStaff.ToString() + item.ContentId + @"',"
                    + @"ContentTitle: '" + item.ContentTitle + @"',"
                    + @"RoomId: '" + item.RoomId + @"',"
                    + @"RoomTitle: '" + item.RoomTitle + @"',"
                    + @"NurseUsername: '" + item.NurseUsername + @"',"
                    + @"NurseShortName: '" + item.NurseShortName + @"',"
                    + @"note: '" + item.Note + @"',";

                // If roster is passed, set color code and is completed
                if (item.StartTime <= DateTime.Now)
                {
                    item.ColorCode = ServiceFacade.SettingsHelper.CompleteColor;
                    item.IsComplete = true;
                    DataRepository.AppointmentProvider.Save(tm, item);

                    result += @"readonly: true,";
                }
                if (string.IsNullOrEmpty(item.ColorCode))
                {
                    item.ColorCode = ServiceFacade.SettingsHelper.UncompleteColor;
                    DataRepository.AppointmentProvider.Save(tm, item);
                }
                // Set color to recognize that's is roster
                result += @"color: '" + item.ColorCode + @"',";
                result += @"isnew: 'false'},";
            }
            #endregion

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

            tm.Commit();
        }
        catch (Exception ex)
        {
            SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.LoadRoster", ex);

            tm.Rollback();

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': '[]' }]";
            goto StepResult;
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
            goto StepResult;
        }
        catch (Exception ex)
        {
            SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.DeleteEvent", ex);

            tm.Rollback();

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            goto StepResult;
        }

    StepResult:
        return result;
    }
    #endregion

    #region "Function"
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetPatient()
    {
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data:  [{
         *          key: name of roles,
         *          label: name of roles,
         *          open: true, [priority for Doctor]
         *          children:  [{
         *                      key: Staff Id,
         *                      label: Staff Name,
         *                      }, {}, {}]
         *          }, {}, {}]
         *  }]
         */
        string result = string.Empty;

        try
        {

            TList<Customer> lst = DataRepository.CustomerProvider.GetByIsDisabled(false);

            result = @"{'key': '-1', 'label': 'Select Patient'}";
            if (lst.Count == 0)
            {
                result = @"[{ 'result': 'true', 'message': 'There is no patient.', 'data': [" + result + "] }]";
                goto StepResult;
            }

            lst.Sort("FirstName ASC");
            foreach (Customer item in lst)
            {
                result += @",{'key' : '" + item.Id + @"'" + "," + @"'label' : '" +
                    String.Format("{3} |{2} {0} {1}", item.FirstName, item.LastName, string.IsNullOrEmpty(item.Title) ? string.Empty : item.Title + ". ",
                    item.Id) + @"'" + "}";
            }

            result = @"[{ 'result': 'true', 'message': '', 'data':[" + result + @"] }]";
            goto StepResult;
        }
        catch (Exception ex)
        {
            SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.GetPatient", ex);

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            goto StepResult;
        }
    StepResult:
        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetTime()
    {
        string result = string.Empty;
        try
        {
            int step = ServiceFacade.SettingsHelper.MinuteStep;
            int minute = ServiceFacade.SettingsHelper.MaxMinute;
            int hour = ServiceFacade.SettingsHelper.MaxHour;

            TList<RosterType> lst = DataRepository.RosterTypeProvider.GetByIsDisabled(false);

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
        catch (Exception ex)
        {
            SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.GetTime", ex);

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            goto StepResult;
        }
    StepResult:
        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetDoctorTree()
    {
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data:  [{
         *          key: name of roles,
         *          label: name of roles,
         *          open: true, [priority for Doctor]
         *          children:  [{
         *                      key: Staff Id,
         *                      label: Staff Name
         *                      }, {}, {}]
         *          }, {}, {}]
         *  }]
         */
        string result = string.Empty;

        try
        {
            // Get all functionalities
            TList<Functionality> lstFunc = DataRepository.FunctionalityProvider.GetByIsDisabled(false);

            // If there is no functionality, return empty data
            if (lstFunc.Count == 0)
            {
                result = @"[{ 'result': 'false', 'message': 'There is no group. Please contact Administrator.', 'data': [] }]";
                goto StepResult;
            }

            // Get all available staffs
            TList<DoctorFunc> lstDoctorFunc = DataRepository.DoctorFuncProvider.GetByIsDisabled(false);
            bool hasItem = false;
            lstDoctorFunc.Sort("DoctorShortName ASC");

            foreach (Functionality objFunc in lstFunc)
            {
                result += @"{'key' : '" + objFunc.Id + @"'" + "," + @"'label' : '" + objFunc.Title + @"', open: false, children: [";
                hasItem = false;

                // Get staff by functionality then remove it from list
                for (int j = 0; j < lstDoctorFunc.Count; j++)
                {
                    if (lstDoctorFunc[j].FuncId == objFunc.Id)
                    {
                        result += @"{'key' : '" + lstDoctorFunc[j].DoctorUserName + @"'" + "," + @"'label' : '"
                            + lstDoctorFunc[j].DoctorShortName + @"'" + "},";
                        lstDoctorFunc.RemoveAt(j);
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
            goto StepResult;
        }
        catch (Exception ex)
        {
            SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.GetDoctorTree", ex);

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            goto StepResult;
        }
    StepResult:
        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetStaffs(string AppointmentId, string FuncId, string StartTime, string EndTime, string StartDate, string EndDate)
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
            int intStartHour = Convert.ToInt32(StartTime.Split(':')[0]);
            int intStartMinute = Convert.ToInt32(StartTime.Split(':')[1]);
            int intEndHour = Convert.ToInt32(EndTime.Split(':')[0]);
            int intEndMinute = Convert.ToInt32(EndTime.Split(':')[1]);

            // Get start date and end date
            DateTime dtStart = Convert.ToDateTime(StartDate);
            DateTime dtEnd = Convert.ToDateTime(EndDate);

            dtStart = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, intStartHour, intStartMinute, 0);
            dtEnd = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, intEndHour, intEndMinute, 0);

            string[] arr = FuncId.Split(chrSeperateStaff);

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetAvailableStaffsForAppointment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@AppointmentId", AppointmentId));
            cmd.Parameters.Add(new SqlParameter("@FuncId", Convert.ToInt64(arr[0])));
            cmd.Parameters.Add(new SqlParameter("@StartTime", dtStart));
            cmd.Parameters.Add(new SqlParameter("@EndTime", dtEnd));
            cmd.CommandTimeout = 0;

            ds = DataRepository.Provider.ExecuteDataSet(cmd);
            DataTable objTable = ds.Tables[0];

            result = @"{'key': '-1', 'label': 'Select Doctor'}";
            if (objTable.Rows.Count == 0)
            {
                result = @"[{ 'result': 'true', 'message': 'There is no doctor. Please contact Administrator.', 'data': [" + result + "] }]";
                goto StepResult;
            }

            int intStatus = 0;
            string strStatus = string.Empty;
            foreach (DataRow dr in objTable.Rows)
            {
                intStatus = Convert.ToInt32(dr["Status"]);
                DoctorAppointmentStatus.Instance().TryGetValue(dr["Status"].ToString(), out strStatus);

                result += @", {'key' : '" + dr["DoctorUserName"].ToString() + @"'" + "," + @"'label' : '" + dr["DoctorShortName"].ToString();
                result += strSeperateStaff + strStatus + @"', ";

                if (intStatus != 0)
                {
                    result += @"'property' : 'disabled=""disabled""'";
                }
                else
                {
                    result += @"'property' : ''";
                }

                result += @"}";
            }

            result = @"[{ 'result': 'true', 'message': '', 'data':[" + result + @"] }]";
            goto StepResult;
        }
        catch (Exception ex)
        {
            SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.GetStaffs", ex);

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            goto StepResult;
        }
    StepResult:
        return result;
    }

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
                result += @", {'key' : '" + item.FuncId + chrSeperateStaff.ToString() + item.Id.ToString() + @"'" + "," + @"'label' : '"
                    + item.FuncTitle + strSeperateStaff + item.Title + @"'}";
            }

            result = @"[{ 'result': 'true', 'message': '', 'data':[" + result + @"] }]";
            goto StepResult;
        }
        catch (Exception ex)
        {
            SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.GetContents", ex);

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': [] }]";
            goto StepResult;
        }
    StepResult:
        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetRooms(string AppointmentId, string DoctorUserName, string FuncId, string StartTime, string EndTime, string StartDate, string EndDate)
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
            int intStartHour = Convert.ToInt32(StartTime.Split(':')[0]);
            int intStartMinute = Convert.ToInt32(StartTime.Split(':')[1]);
            int intEndHour = Convert.ToInt32(EndTime.Split(':')[0]);
            int intEndMinute = Convert.ToInt32(EndTime.Split(':')[1]);

            // Get start date and end date
            DateTime dtStart = Convert.ToDateTime(StartDate);
            DateTime dtEnd = Convert.ToDateTime(EndDate);

            dtStart = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, intStartHour, intStartMinute, 0);
            dtEnd = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, intEndHour, intEndMinute, 0);

            string[] arr = FuncId.Split(chrSeperateStaff);

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GetAvailableRoomsForAppointment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@AppointmentId", AppointmentId));
            cmd.Parameters.Add(new SqlParameter("@DoctorUserName", DoctorUserName));
            cmd.Parameters.Add(new SqlParameter("@FuncId", Convert.ToInt64(arr[0])));
            cmd.Parameters.Add(new SqlParameter("@StartTime", dtStart));
            cmd.Parameters.Add(new SqlParameter("@EndTime", dtEnd));
            cmd.CommandTimeout = 0;

            ds = DataRepository.Provider.ExecuteDataSet(cmd);
            DataTable objTable = ds.Tables[0];

            result = @"{'key': '-1', 'label': 'Select Room'}";
            if (objTable.Rows.Count == 0)
            {
                result = @"[{ 'result': 'true', 'message': 'There is no room. Please contact Administrator.', 'data': [" + result + "] }]";
                goto StepResult;
            }

            int intStatus = 0;
            string strStatus = string.Empty;
            foreach (DataRow dr in objTable.Rows)
            {
                intStatus = Convert.ToInt32(dr["Status"]);
                RoomAppointmentStatus.Instance().TryGetValue(dr["Status"].ToString(), out strStatus);

                result += @", {'key' : '" + dr["RoomId"].ToString() + @"'" + "," + @"'label' : '" + dr["RoomTitle"].ToString();
                result += strSeperateStaff + strStatus + @"', ";

                if (intStatus != 0)
                {
                    result += @"'property' : 'disabled=""disabled""'";
                }
                else
                {
                    result += @"'property' : ''";
                }
                result += @"}";
            }

            result = @"[{ 'result': 'true', 'message': '', 'data':[" + result + @"] }]";
            goto StepResult;
        }
        catch (Exception ex)
        {
            SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.GetRooms", ex);

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
                        result = item.FuncId.ToString() + chrSeperateStaff.ToString() + item.Id.ToString();
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
            SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.GetCurrentContents", ex);

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
            result = newObj.Id;

            #endregion

            result = @"[{ 'result': 'true', 'message': '', 'data':'" + result + @"' }]";
            goto StepResult;
        }
        catch (Exception ex)
        {
            SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.CreateSimplePatient", ex);

            result = @"[{ ""result"": ""false"", ""message"": ""Cannot create new patient. Please try again."", ""data"": """" }]";
            goto StepResult;
        }
    StepResult:
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
            SingletonLogger.Instance.Error("Admin_Appointment_AppointmentIframe.CheckConflict", ex);

            blResult = false;
            goto StepResult;
        }
    StepResult:
        return blResult;
    }
    #endregion
}
