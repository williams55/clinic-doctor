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
        sc.Credentials = new System.Net.NetworkCredential("username", "password");
        sc.EnableSsl = true;

        MailMessage msg = new MailMessage();
        msg.From = new MailAddress("votienphat@gmail.com", "Đại bàng Dép Đứt");
        msg.To.Add(new MailAddress(" vy.le@internationalsos.com", "Chim én Heroes :))"));
        msg.Subject = "Sát thủ đầu mưng mủ";
        msg.Body = "Một khi đã máu thì đừng hỏi bố cháu là ai :))";

        iCalendar iCal = new iCalendar();

        iCal.Method = "PUBLISH";

        DateTime startTime = DateTime.Now.AddHours(3);
        DateTime endTime = startTime.AddHours(1);

        // Create the event
        Event evt = iCal.Create<Event>();

        evt.Summary = "Một khi đã quyết chỉ có nản mới cản được anh.";
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