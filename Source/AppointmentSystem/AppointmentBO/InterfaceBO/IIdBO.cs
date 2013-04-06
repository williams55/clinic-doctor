using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppointmentSystem.Entities;

namespace AppointmentBusiness.BO
{
    public interface IIdBO
    {
        #region Roster
        /// <summary>
        /// Generate Id cho roster
        /// </summary>
        /// <returns></returns>
        string RosterId();

        /// <summary>
        /// Generate Id cho roster voi so co san
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        string RosterId(object number);
        #endregion

        #region Appointment
        /// <summary>
        /// Generate Id cho Appointment
        /// </summary>
        /// <returns></returns>
        string AppointmentId();

        /// <summary>
        /// Generate Id cho Appointment voi so co san
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        string AppointmentId(object number);
        #endregion
    }
}
