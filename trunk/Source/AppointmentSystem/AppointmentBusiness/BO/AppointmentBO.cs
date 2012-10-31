using System;
using AppointmentBusiness.Util;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
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

        public TList<Appointment> GetByDateMode(DateTime? date, string mode, out string message)
        {
            message = string.Empty;
            try
            {
                // Get datetime to filter
                DateTime fromDate, toDate;
                if (!CommonBO.GetDateTimeByMode(date, mode, out fromDate, out toDate))
                {
                    message = "Current date is invalid.";
                    goto StepResult;
                }

                // Get appointment
                int count;
                TList<Appointment> lstAppt =
                    DataRepository.AppointmentProvider.GetPaged(String.Format("IsDisabled = 'False' AND StartTime BETWEEN N'{0}' AND N'{1}'"
                                                                              , fromDate.ToString("yyyy-MM-dd HH:mm:ss.000"),
                                                                              toDate.ToString("yyyy-MM-dd HH:mm:ss.000")),
                                                                string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength,
                                                                out count);

                // Set out of date for passed appointment
                //lstAppt.ForEach(x =>
                //{
                //    x.StatusId = x.StartTime <= DateTime.Now &&
                //                 x.StatusId != BoFactory.StatusBO.Complete &&
                //                 x.StatusId != BoFactory.StatusBO.Cancel
                //                     ? BoFactory.StatusBO.Complete
                //                     : x.StatusId;
                //});
                //DataRepository.AppointmentProvider.Save(lstAppt);

                return lstAppt;
            }
            catch (Exception ex)
            {
                message = "System error. Please contact Administratos.";
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
        StepResult:
            return new TList<Appointment>();
        }
    }
}
