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
        /// Insert mot appointment
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="message"></param>
        bool Insert(Appointment appointment, ref string message);

        /// <summary>
        /// Update mot appointment
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="message"></param>
        bool Update(ref Appointment appointment, ref string message);
        
        /// <summary>
        /// Them History log cua appointment
        /// </summary>
        /// <param name="username"> </param>
        /// <param name="fromStatus"></param>
        /// <param name="toStatus"></param>
        /// <param name="appointment"> </param>
        void InsertHistory(Appointment appointment, string username, string fromStatus, string toStatus);

        /// <summary>
        /// Lay danh sach Appointment theo ngay, mode
        /// </summary>
        /// <param name="date"></param>
        /// <param name="mode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        TList<Appointment> GetByDateMode(DateTime? date, string mode, out string message);

        /// <summary>
        /// Build 1 appointment thanh 1 doi tuong de luu history
        /// </summary>
        /// <param name="appt"></param>
        /// <returns></returns>
        object BuildApptHistory(Appointment appt);
    }
}
