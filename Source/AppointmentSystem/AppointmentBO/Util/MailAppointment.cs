using System;
using AppointmentSystem.Settings.BusinessLayer;
using DDay.iCal;
using DDay.iCal.Serialization.iCalendar;
using System.Net.Mail;
using System.Collections.Generic;

namespace AppointmentBusiness.Util
{
    public class MailAddressObject
    {
        public string Email { get; set; }
        public string Name { get; set; }

        public MailAddressObject(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }

    /// <summary>
    /// Summary description for MailAppointment
    /// </summary>
    public class MailAppointment
    {
        #region "Variables"
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string AlarmSummary { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<MailAddressObject> Mail;
        #endregion

        #region "Constructor"
        public MailAppointment()
        {
            try
            {
                Subject = string.Empty;
                Body = string.Empty;
                Summary = string.Empty;
                Description = string.Empty;
                Location = string.Empty;
                AlarmSummary = string.Empty;
                StartTime = DateTime.Now;
                EndTime = DateTime.Now;
                Mail = new List<MailAddressObject>();
            }
            catch
            {

            }
        }

        public MailAppointment(string subject, string body, string summary, string description, string location, string alarmSummary,
                               DateTime startTime, DateTime endTime)
        {
            try
            {
                Subject = subject;
                Body = body;
                Summary = summary;
                Description = description;
                Location = location;
                AlarmSummary = alarmSummary;
                StartTime = startTime;
                EndTime = endTime;
                Mail = new List<MailAddressObject>();
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
                var sc = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new System.Net.NetworkCredential("username", "password"),
                        EnableSsl = true
                    };

                var msg = new MailMessage {From = new MailAddress("votienphat@gmail.com", "System")};
                foreach (MailAddressObject obj in Mail)
                {
                    msg.To.Add(new MailAddress(obj.Email, obj.Name));
                }
                msg.Subject = Subject;
                msg.Body = Body;

                var iCal = new iCalendar {Method = "REQUEST"};

                // Create the event
                var evt = iCal.Create<Event>();

                evt.Summary = Summary;
                evt.Start = new iCalDateTime(StartTime.Year, StartTime.Month, StartTime.Day, StartTime.Hour, StartTime.Minute, StartTime.Second);
                evt.End = new iCalDateTime(EndTime.Year, EndTime.Month, EndTime.Day, EndTime.Hour, EndTime.Minute, EndTime.Second);
                evt.Description = Description;
                evt.Location = Location;

                //evt.Organizer = new Organizer("Phats");
                var alarm = new Alarm
                    {
                        Action = AlarmAction.Display,
                        Summary = AlarmSummary,
                        Trigger =
                            new Trigger(TimeSpan.FromMinutes(0 - ServiceFacade.SettingsHelper.TimeLeftRemindAppointment))
                    };
                evt.Alarms.Add(alarm);

                var serializer = new iCalendarSerializer(iCal);
                string icalData = serializer.SerializeToString(serializer);

                // Set up the different mime types contained in the message
                var loTextType = new System.Net.Mime.ContentType("text/plain");
                var loHtmlType = new System.Net.Mime.ContentType("text/html");
                var loCalendarType = new System.Net.Mime.ContentType("text/calendar");

                // Add parameters to the calendar header
                if (loCalendarType.Parameters != null)
                {
                    loCalendarType.Parameters.Add("method", "REQUEST");
                    loCalendarType.Parameters.Add("name", "meeting.ics");
                }

                AlternateView loTextView = AlternateView.CreateAlternateViewFromString(icalData, loTextType);
                msg.AlternateViews.Add(loTextView);

                AlternateView loHtmlView = AlternateView.CreateAlternateViewFromString(icalData, loHtmlType);
                msg.AlternateViews.Add(loHtmlView);

                AlternateView loCalendarView = AlternateView.CreateAlternateViewFromString(icalData, loCalendarType);
                loCalendarView.TransferEncoding = System.Net.Mime.TransferEncoding.SevenBit;
                msg.AlternateViews.Add(loCalendarView);

                sc.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                //SingletonLogger.Instance.Error("MailAppointment.SendMail", ex);
                return false;
            }
        }

        public void AddMailAddress(string email, string name)
        {
            try
            {
                Mail.Add(new MailAddressObject(email, name));
            }
            catch (Exception)
            {
                //SingletonLogger.Instance.Error("MailAppointment.AddMailAddress", ex);
            }
        }
        #endregion
    }
}