using System;
using System.Linq;
using AppointmentBusiness.Util;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using Appt.Common.Constants;
using Common.Util;
using Log.Controller;

namespace AppointmentBusiness.BO
{
    public class PatientBO : IPatientBO
    {
        public bool Update(ref VcsPatient patient, ref string message)
        {
            try
            {
                if (!Validate(patient, ref message)) return false;

                // Check exists Patient
                var objPatients = DataRepository.VcsPatientProvider.GetByPatientCode(patient.PatientCode);
                if (objPatients == null || !objPatients.Any() || objPatients[0].IsDisabled)
                {
                    message = "There is no patient to update.";
                    return false;
                }
                var oldPatient = objPatients[0];

                // Gan gia tri moi
                oldPatient.LastName = patient.LastName;
                oldPatient.FirstName = patient.FirstName;
                oldPatient.MiddleName = patient.MiddleName;
                oldPatient.Sex = patient.Sex;
                oldPatient.DateOfBirth = patient.DateOfBirth;
                oldPatient.Nationality = patient.Nationality;
                oldPatient.CompanyCode = patient.CompanyCode;
                oldPatient.MobilePhone = patient.MobilePhone;
                oldPatient.HomePhone = patient.HomePhone;
                oldPatient.UpdateUser = patient.UpdateUser;

                DataRepository.VcsPatientProvider.Update(oldPatient.PatientCode, oldPatient.FirstName, oldPatient.MiddleName, oldPatient.LastName
                , oldPatient.DateOfBirth, oldPatient.Sex, oldPatient.Nationality, oldPatient.CompanyCode, oldPatient.HomePhone, oldPatient.MobilePhone
                , oldPatient.UpdateUser, oldPatient.ApptRemark, oldPatient.IsDisabled);

                return true;
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return false;
        }

        public bool UpdateRemark(string patientCode, string apptRemark, string updateUser, ref string message)
        {
            try
            {
                // Check exists Patient
                var objPatients = DataRepository.VcsPatientProvider.GetByPatientCode(patientCode);
                if (objPatients == null || !objPatients.Any() || objPatients[0].IsDisabled)
                {
                    message = "There is no patient to update.";
                    return false;
                }

                DataRepository.VcsPatientProvider.UpdateRemark(patientCode, updateUser, apptRemark);
                return true;
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return false;
        }

        public bool Insert(VcsPatient patient, ref string message)
        {
            try
            {
                patient.PatientCode = ServiceFacade.SettingsHelper.LocationCode;

                if (!Validate(patient, ref message)) return false;
                var patients = DataRepository.VcsPatientProvider.Insert(patient.PatientCode, patient.FirstName,
                                                                           patient.MiddleName
                                                                           , patient.LastName, patient.DateOfBirth, patient.Sex,
                                                                           patient.Nationality, patient.CompanyCode,
                                                                           patient.HomePhone
                                                                           , patient.MobilePhone, patient.UpdateUser,
                                                                           patient.ApptRemark);
                if (patients == null || !patients.Any())
                {
                    message = "Cannot create new patient. Please contact Administrator.";
                    return false;
                }

                patient.PatientCode = patients[0].PatientCode;

                return true;
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return false;
        }

        #region Private method

        /// <summary>
        /// Validate patient's fields
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static bool Validate(VcsPatient patient, ref string message)
        {
            try
            {
                // Validate empty value for Lastname field
                if (patient.Sex != SexConstant.Male.Key && patient.Sex != SexConstant.Female.Key)
                {
                    message = "Please select sex.";
                    return false;
                }

                // Kiem tra nationality co ton tai khong
                var objNationalities = DataRepository.VcsCountryProvider.Get(
                    String.Format("CitizenName = '{0}' AND IsDisabled = 'False'", patient.Nationality), string.Empty);
                if (objNationalities == null || !objNationalities.Any())
                {
                    message = "Nationality is not exist.";
                    return false;
                }

                // Kiem tra Company co ton tai khong
                if (!string.IsNullOrEmpty(patient.CompanyCode))
                {
                    var objCompanies = DataRepository.VcsCountryProvider.Get(
                        String.Format("CitizenName = '{0}' AND IsDisabled = 'False'", patient.Nationality), string.Empty);
                    if (objCompanies == null || !objCompanies.Any())
                    {
                        message = "Company is not exist.";
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
                message = ex.Message;
                return false;
            }
        }
        #endregion

    }
}
