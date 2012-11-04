using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppointmentSystem.Entities;

namespace AppointmentBusiness.BO
{
    public interface IPatientBO
    {
        /// <summary>
        /// Insert mot appointment
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="message"></param>
        bool Insert(VcsPatient patient, ref string message);

        /// <summary>
        /// Update mot appointment
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="message"></param>
        bool Update(ref VcsPatient patient, ref string message);
    }
}
