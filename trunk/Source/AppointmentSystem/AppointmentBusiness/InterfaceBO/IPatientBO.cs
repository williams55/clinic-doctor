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
        /// Insert mot patient
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="message"></param>
        bool Insert(VcsPatient patient, ref string message);

        /// <summary>
        /// Update mot patient
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="message"></param>
        bool Update(ref VcsPatient patient, ref string message);

        /// <summary>
        /// Update appointment remark cua patient
        /// </summary>
        /// <param name="patientCode"></param>
        /// <param name="apptRemark"></param>
        /// <param name="updateUser"></param>
        /// <param name="message"></param>
        bool UpdateRemark(string patientCode, string apptRemark, string updateUser, ref string message);
    }
}
