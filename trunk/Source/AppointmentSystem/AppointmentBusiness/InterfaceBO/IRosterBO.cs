using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppointmentSystem.Entities;

namespace AppointmentBusiness.BO
{
    public interface IRosterBO
    {
        /// <summary>
        /// Insert mot roster
        /// </summary>
        /// <param name="roster"></param>
        /// <param name="message"></param>
        bool Insert(Roster roster, ref string message);

        /// <summary>
        /// Insert repeat roster
        /// </summary>
        /// <param name="roster"></param>
        /// <param name="lstRoster"></param>
        /// <param name="weekday"></param>
        /// <param name="message"></param>
        bool InsertRepeat(Roster roster, string weekday, TList<Roster> lstRoster, ref string message);

        /// <summary>
        /// Update mot roster
        /// </summary>
        /// <param name="roster"></param>
        /// <param name="message"></param>
        bool Update(ref Roster roster, ref string message);

        /// <summary>
        /// Lay danh sach Roster theo ngay, mode
        /// </summary>
        /// <param name="date"></param>
        /// <param name="mode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        TList<Roster> GetByDateMode(DateTime? date, string mode, out string message);
    }
}
