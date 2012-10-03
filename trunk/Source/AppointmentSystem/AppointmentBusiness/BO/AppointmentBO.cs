using System;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using Common.Util;
using Log.Controller;

namespace AppointmentBusiness.BO
{
    public class AppointmentBO : IAppointmentBO
    {
        public void Insert(Appointment appointment, string username, string fromStatus, string toStatus)
        {
            try
            {
                var patient = DataRepository.PatientProvider.GetByPatientCode(appointment.PatientCode);

                var history = new AppointmentHistory
                    {
                        Guid = Guid.NewGuid(),
                        AppointmentId = appointment.Id,
                        Note = String.Format("User {0} change appointment {1} from status {2} to {3}, patient {4}, user {5}"
                        , username, appointment.Id, fromStatus, toStatus, patient.LastName, appointment.Username),
                        CreateUser = username,
                        CreateDate = DateTime.Now
                    };
                DataRepository.AppointmentHistoryProvider.Insert(history);
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
        }
    }
}
