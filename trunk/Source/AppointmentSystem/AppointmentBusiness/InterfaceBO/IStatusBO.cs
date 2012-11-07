using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppointmentSystem.Entities;

namespace AppointmentBusiness.BO
{
    public interface IStatusBO
    {
        /// <summary>
        /// Lay mau cua status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        string GetColor(string status);

        /// <summary>
        /// Lay ten cua status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        string GetTitle(string status);
    }
}
