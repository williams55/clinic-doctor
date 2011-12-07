using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;
using System.Web.Services;
using DDay.iCal;
using DDay.iCal.Serialization.iCalendar;

public partial class Admin_Doctor_TestSendMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime startTime = DateTime.Now.AddHours(-1);
        DateTime endTime = DateTime.Now;

        SmtpClient sc = new SmtpClient("smtp.gmail.com");
        sc.Port = 587;
        sc.Credentials = new System.Net.NetworkCredential("votienphat", "@@@chilaquakhu");
        sc.EnableSsl = true;

        MailMessage msg = new MailMessage();
        msg.From = new MailAddress("votienphat@gmail.com", "Ahmed Abu Dagga");
        msg.To.Add(new MailAddress("votienphat@gmail.com", "Phat"));
        msg.Subject = "Send Calendar Appointment Email";
        msg.Body = "Here is the Body Content";

        StringBuilder str = new StringBuilder();
        str.AppendLine("BEGIN:VCALENDAR");
        str.AppendLine("PRODID:-//Ahmed Abu Dagga Blog");
        str.AppendLine("VERSION:2.0");
        str.AppendLine("METHOD:REQUEST");
        str.AppendLine("BEGIN:VEVENT");
        str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", startTime));
        str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
        str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", endTime));
        str.AppendLine("LOCATION: Dubai");
        str.AppendLine(string.Format("UID:{0}", Guid.NewGuid()));
        str.AppendLine(string.Format("DESCRIPTION:{0}", msg.Body));
        str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", msg.Body));
        str.AppendLine(string.Format("SUMMARY:{0}", msg.Subject));
        str.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", msg.From.Address));

        str.AppendLine(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", msg.To[0].DisplayName, msg.To[0].Address));

        str.AppendLine("BEGIN:VALARM");
        str.AppendLine("TRIGGER:-PT15M");
        str.AppendLine("ACTION:DISPLAY");
        str.AppendLine("DESCRIPTION:Reminder");
        str.AppendLine("END:VALARM");
        str.AppendLine("END:VEVENT");
        str.AppendLine("END:VCALENDAR");
        System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType("text/calendar");
        ct.Parameters.Add("method", "REQUEST");
        AlternateView avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), ct);
        msg.AlternateViews.Add(avCal);



        iCalendar iCal = new iCalendar();
        iCalendarSerializer serializer = new iCalendarSerializer(iCal);
        string icalData = serializer.SerializeToString();

        System.Net.Mail.Attachment attachment = System.Net.Mail.Attachment.CreateAttachmentFromString(icalData, new System.Net.Mime.ContentType("text/calendar"));
        attachment.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
        attachment.Name = "EventDetails.ics"; //not visible in outlook

        msg.Attachments.Add(attachment);

        sc.Send(msg);
    }

   // [WebMethod(Description =
   //"Send an appointment with much details.")]
   // public void SendAppointment(string from, string to,
   //    string title, string body, DateTime startDate,
   //    double duration, string location, string organizer,
   //    bool updatePreviousEvent, string eventId,
   //    bool allDayEvent,
   //    int recurrenceDaysInterval, int recurrenceCount)
   // {
   //     iCalendar iCal = new iCalendar();

   //     // outlook 2003 needs this property,
   //     //  or we'll get an error (a Lunar error!)
   //     iCal.Method = "PUBLISH";

   //     // Create the event
   //     Event evt = iCal.Create();

   //     evt.Summary = title;

   //     evt.Start = new iCalDateTime(startDate.Year,
   //       startDate.Month, startDate.Day, startDate.Hour,
   //       startDate.Minute, startDate.Second);
   //     evt.Duration = TimeSpan.FromHours(duration);
   //     evt.Description = body;
   //     evt.Location = location;

   //     if (recurrenceDaysInterval > 0)
   //     {
   //         RecurrencePattern rp = new RecurrencePattern();
   //         rp.Frequency = FrequencyType.Daily;
   //         rp.Interval = recurrenceDaysInterval; // interval of days

   //         rp.Count = recurrenceCount;
   //         evt.AddRecurrencePattern(rp);
   //     }
   //     evt.IsAllDay = allDayEvent;

   //     //organizer is mandatory for outlook 2007 - think about
   //     // trowing an exception here.
   //     if (!String.IsNullOrEmpty(organizer))
   //         evt.Organizer = organizer;


   //     if (!String.IsNullOrEmpty(eventId))
   //         evt.UID = eventId;

   //     //"REQUEST" will update an existing event with the same
   //     // UID (Unique ID) and a newer time stamp.
   //     if (updatePreviousEvent)
   //         iCal.Method = "REQUEST";

   //     // Save into calendar file.
   //     iCalendarSerializer serializer = new iCalendarSerializer(iCal);
   //     //serializer.Serialize(@"iCalendar.ics");

   //     string icalData = serializer.SerializeToString();

   //     //send the iCal data. Also sends the subject and body
   //     //on the mail.
   //     //SendAppointmentFromICalWithMailTitle(from, to, icalData, title, body);
   // }


   // private static void sendMailMessage(MailMessage mailMessage)
   // {
   //     string mailHost = "Ask.Someone.com";
   //     SmtpClient smtpClient = new SmtpClient(mailHost, 25);
   //     smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
   //     smtpClient.Send(mailMessage);
   // }
}