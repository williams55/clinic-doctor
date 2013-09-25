using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppointmentSystem.Entities;
using Appt.Common.Constants;

namespace AppointmentBusiness.BO
{
    public interface ISmsBO
    {
        /// <summary>
        /// Gửi sms
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        bool SendSms(string mobile, string title, string message);

        /// <summary>
        /// Gửi SMS cho patient từ appointment
        /// </summary>
        /// <param name="apptId"></param>
        /// <param name="smsCase"></param>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        bool SendSmsAppointment(string apptId, SmsCase smsCase, out string messageCode);

        /// <summary>
        /// Send SMS tự tạo
        /// </summary>
        /// <param name="sms"></param>
        /// <param name="phones"></param>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        bool AddSms(Sms sms, string phones, out string messageCode);

        /// <summary>
        /// Kiểm tra và gửi SMS tự động
        /// </summary>
        void CheckAndSendSms();
    }
}
