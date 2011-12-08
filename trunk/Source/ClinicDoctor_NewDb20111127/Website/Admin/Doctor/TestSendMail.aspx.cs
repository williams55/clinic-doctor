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
        SmtpClient sc = new SmtpClient("smtp.gmail.com");
        sc.Port = 587;
        sc.Credentials = new System.Net.NetworkCredential("username", "pass");
        sc.EnableSsl = true;

        MailMessage msg = new MailMessage();
        msg.From = new MailAddress("votienphat@gmail.com", "Đại bàng Dép Đứt");
        msg.To.Add(new MailAddress("phatvt@fpt.net", "Chim én Dép Đứt :))"));
        msg.Subject = "Sát thủ đầu mưng mủ";
        msg.Body = "Một khi đã máu thì đừng hỏi bố cháu là ai :))";

        iCalendar iCal = new iCalendar();

        iCal.Method = "PUBLISH";

        DateTime startTime = DateTime.Now.AddHours(3);
        DateTime endTime = startTime.AddHours(1);

        //Todo evt = iCal.Create<Todo>();

        // Create the event
        Event evt = iCal.Create<Event>();

        evt.Summary = "Một khi đã quyết chỉ có nản mới cản được anh.";
        evt.Start = new iCalDateTime(startTime.Year, startTime.Month, startTime.Day, startTime.Hour, startTime.Minute, startTime.Second);
        //evt.End = evt.Start.AddMinutes(30);
        evt.Description = "Chi tiết appointment";
        evt.Location = "Cái location chắc ko cần";

        //evt.Organizer = new Organizer("Phats");

        Alarm alarm = new Alarm();
        alarm.Action = AlarmAction.Display;
        alarm.Summary = "Nội dung alarm nà";
        alarm.Trigger = new Trigger(TimeSpan.FromMinutes(-58));
        evt.Alarms.Add(alarm);

        iCalendarSerializer serializer = new iCalendarSerializer(iCal);
        string icalData = serializer.SerializeToString();

        System.Net.Mail.Attachment attachment = System.Net.Mail.Attachment.CreateAttachmentFromString(icalData, new System.Net.Mime.ContentType("text/calendar"));
        attachment.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
        attachment.Name = "EventDetails.ics"; //not visible in outlook



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


        
        //msg.Attachments.Add(attachment);

        sc.Send(msg);
    }



//    [WebMethod(Description =
//   "Send an appointment with much details.")]
//public void SendAppointment(string from, string to,
//   string title, string body, DateTime startDate,
//   double duration, string location, string organizer,
//   bool updatePreviousEvent, string eventId,
//   bool allDayEvent,
//   int recurrenceDaysInterval, int recurrenceCount)
//{
//  iCalendar iCal = new iCalendar();

//  // outlook 2003 needs this property,
//  //  or we'll get an error (a Lunar error!)
//  iCal.Method = "PUBLISH";

//  // Create the event
//  Event evt = iCal.Create();

//  evt.Summary = title;

//  evt.Start = new iCalDateTime(startDate.Year,
//    startDate.Month, startDate.Day, startDate.Hour,
//    startDate.Minute, startDate.Second);
//  evt.Duration = TimeSpan.FromHours(duration);
//  evt.Description = body;
//  evt.Location = location;

//  if (recurrenceDaysInterval &gt; 0)
//  {
//    RecurrencePattern rp = new RecurrencePattern();
//    rp.Frequency = FrequencyType.Daily;
//    rp.Interval = recurrenceDaysInterval; // interval of days

//    rp.Count = recurrenceCount;
//    evt.AddRecurrencePattern(rp);
//  }
//  evt.IsAllDay = allDayEvent;

//  //organizer is mandatory for outlook 2007 - think about
//  // trowing an exception here.
//  if (!String.IsNullOrEmpty(organizer))
//    evt.Organizer = organizer;


//  if (!String.IsNullOrEmpty(eventId))
//    evt.UID = eventId;

//  //"REQUEST" will update an existing event with the same
//  // UID (Unique ID) and a newer time stamp.
//  if (updatePreviousEvent)
//    iCal.Method = "REQUEST";

//  // Save into calendar file.
//  iCalendarSerializer serializer = new iCalendarSerializer(iCal);
//  //serializer.Serialize(@"iCalendar.ics");

//  string icalData = serializer.SerializeToString();

//  //send the iCal data. Also sends the subject and body
//  //on the mail.
//  SendAppointmentFromICalWithMailTitle(from, to,
//    icalData, title, body);
//}

//    private static void sendMailMessage(MailMessage mailMessage)
//    {
//        string mailHost = "Ask.Someone.com";
//        SmtpClient smtpClient = new SmtpClient(mailHost, 25);
//        smtpClient.DeliveryMethod =
//           SmtpDeliveryMethod.PickupDirectoryFromIis;
//        smtpClient.Send(mailMessage);
//    }

//    private void SendAppointmentFromICalWithMailTitle()
//    {

//        MailMessage message = initMailMessage();
//    string iCal = initICal(parameters ...);

//    //Add the attachment, specify it is a calendar file.
//System.Net.Mail.Attachment attachment =
//System.Net.Mail.Attachment.CreateAttachmentFromString(
//iCal, new ContentType("text/calendar"));
//attachment.TransferEncoding = TransferEncoding.Base64;
//attachment.Name = "EventDetails.ics"; //not visible in outlook

//message.Attachments.Add(attachment);

//sendMailMessage(message);

//}

}