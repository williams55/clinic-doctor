using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppointmentSystem.Entities;

namespace AppointmentBusiness.BO
{
    public interface IAppointmentBO
    {
        /// <summary>
        /// Them History log cua appointment
        /// </summary>
        /// <param name="username"> </param>
        /// <param name="fromStatus"></param>
        /// <param name="toStatus"></param>
        /// <param name="appointment"> </param>
        void Insert(Appointment appointment, string username, string fromStatus, string toStatus);

        /// <summary>
        /// Lay danh sach Appointment theo ngay, mode
        /// </summary>
        /// <param name="date"></param>
        /// <param name="mode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        TList<Appointment> GetByDateMode(DateTime? date, string mode, out string message);
    }
}
