using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DDay.iCal;
using DDay.iCal.Serialization.iCalendar;
using System.Net.Mail;
using System.Collections.Generic;
using ClinicDoctor.Settings.BusinessLayer;
using LogUtil;

public class MailAddressObject
{
    public string Email { get; set; }
    public string Name { get; set; }

    public MailAddressObject(string _email, string _name)
    {
        Email = _email;
        Name = _name;
    }
}

/// <summary>
/// Summary description for MailAppointment
/// </summary>
public class MailAppointment
{
    #region "Variables"
    public string strSubject { get; set; }
    public string strBody { get; set; }
    public string strSummary { get; set; }
    public string strDescription { get; set; }
    public string strLocation { get; set; }
    public string strAlarmSummary { get; set; }
    public DateTime dtStartTime { get; set; }
    public DateTime dtEndTime { get; set; }
    public List<MailAddressObject> lstMail;
    #endregion

    #region "Constructor"
    public MailAppointment()
    {
        try
        {
            strSubject = string.Empty;
            strBody = string.Empty;
            strSummary = string.Empty;
            strDescription = string.Empty;
            strLocation = string.Empty;
            strAlarmSummary = string.Empty;
            dtStartTime = DateTime.Now;
            dtEndTime = DateTime.Now;
            lstMail = new List<MailAddressObject>();
        }
        catch
        {

        }
    }

    public MailAppointment(string _subject, string _body, string _summary, string _description, string _location, string _alarmSummary,
        DateTime _startTime, DateTime _endTime)
    {
        try
        {
            strSubject = _subject;
            strBody = _body;
            strSummary = _summary;
            strDescription = _description;
            strLocation = _location;
            strAlarmSummary = _alarmSummary;
            dtStartTime = _startTime;
            dtEndTime = _endTime;
            lstMail = new List<MailAddressObject>();
        }
        catch
        {

        }
    }
    #endregion

    #region "Function"
    public bool SendMail()
    {
        try
        {
            SmtpClient sc = new SmtpClient("smtp.gmail.com");
            sc.Port = 587;
            sc.Credentials = new System.Net.NetworkCredential("username", "password");
            sc.EnableSsl = true;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("votienphat@gmail.com", "System");
            foreach (MailAddressObject obj in lstMail)
            {
                msg.To.Add(new MailAddress(obj.Email, obj.Name));
            }
            msg.Subject = strSubject;
            msg.Body = strBody;

            iCalendar iCal = new iCalendar();

            iCal.Method = "REQUEST";

            // Create the event
            Event evt = iCal.Create<Event>();

            evt.Summary = strSummary;
            evt.Start = new iCalDateTime(dtStartTime.Year, dtStartTime.Month, dtStartTime.Day, dtStartTime.Hour, dtStartTime.Minute, dtStartTime.Second);
            evt.End = new iCalDateTime(dtEndTime.Year, dtEndTime.Month, dtEndTime.Day, dtEndTime.Hour, dtEndTime.Minute, dtEndTime.Second);
            evt.Description = strDescription;
            evt.Location = strLocation;

            //evt.Organizer = new Organizer("Phats");
            Alarm alarm = new Alarm();
            alarm.Action = AlarmAction.Display;
            alarm.Summary = strAlarmSummary;
            alarm.Trigger = new Trigger(TimeSpan.FromMinutes(0 - ServiceFacade.SettingsHelper.TimeLeftRemindAppointment));
            evt.Alarms.Add(alarm);

            iCalendarSerializer serializer = new iCalendarSerializer(iCal);
            string icalData = serializer.SerializeToString();

            AlternateView loTextView = null;
            AlternateView loHTMLView = null;
            AlternateView loCalendarView = null;

            // Set up the different mime types contained in the message
            System.Net.Mime.ContentType loTextType = new System.Net.Mime.ContentType("text/plain");
            System.Net.Mime.ContentType loHTMLType = new System.Net.Mime.ContentType("text/html");
            System.Net.Mime.ContentType loCalendarType = new System.Net.Mime.ContentType("text/calendar");

            // Add parameters to the calendar header
            loCalendarType.Parameters.Add("method", "REQUEST");
            loCalendarType.Parameters.Add("name", "meeting.ics");

            loTextView = AlternateView.CreateAlternateViewFromString(icalData, loTextType);
            msg.AlternateViews.Add(loTextView);

            loHTMLView = AlternateView.CreateAlternateViewFromString(icalData, loHTMLType);
            msg.AlternateViews.Add(loHTMLView);

            loCalendarView = AlternateView.CreateAlternateViewFromString(icalData, loCalendarType);
            loCalendarView.TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit;
            msg.AlternateViews.Add(loCalendarView);

            sc.Send(msg);
            return true;
        }
        catch (Exception ex)
        {
            SingletonLogger.Instance.Error("MailAppointment.SendMail", ex);
            return false;
        }
    }

    public void AddMailAddress(string _email, string _name)
    {
        try
        {
            lstMail.Add(new MailAddressObject(_email, _name));
        }
        catch (Exception ex)
        {
            SingletonLogger.Instance.Error("MailAppointment.AddMailAddress", ex);
        }
    }
    #endregion
}
