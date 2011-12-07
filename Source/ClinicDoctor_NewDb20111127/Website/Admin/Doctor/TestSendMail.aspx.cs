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
        DateTime startTime = DateTime.Now.AddHours(1);
        DateTime endTime = startTime.AddHours(1);

        SmtpClient sc = new SmtpClient("smtp.gmail.com");
        sc.Port = 587;
        sc.Credentials = new System.Net.NetworkCredential("votienphat", "@@@chilaquakhu");
        sc.EnableSsl = true;

        MailMessage msg = new MailMessage();
        msg.From = new MailAddress("votienphat@gmail.com", "Đại bàng Dép Đứt");
        msg.To.Add(new MailAddress("quangvy@gmail.com@gmail.com", "Chim én Heroes :))"));
        msg.Subject = "Thử nghiệm gửi appointment nà";
        msg.Body = "Ê, thấy tao gửi zậy được chưa ku tèo";

        iCalendar iCal = new iCalendar();

        iCal.Method = "PUBLISH";

        // Create the event
        Event evt = iCal.Create<Event>();

        evt.Summary = "Tóm tắt về Appointment nè. Tiêu đề ở đây.";
        evt.Start = new iCalDateTime(startTime.Year, startTime.Month, startTime.Day, startTime.Hour, startTime.Minute, startTime.Second);
        evt.End = evt.Start.AddMinutes(30);
        evt.Description = "Chi tiết appointment";
        evt.Location = "Cái location chắc ko cần";

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

        msg.Attachments.Add(attachment);

        sc.Send(msg);
    }
}