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
    }
}
