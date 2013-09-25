using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using AppointmentBusiness.SmsBrand;
using AppointmentBusiness.Util;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using Appt.Common.Constants;
using Common.Util;
using Log;
using Newtonsoft.Json;
using Logger.Controller;

namespace AppointmentBusiness.BO
{
    public class SmsBO : ISmsBO
    {
        public bool SendSms(string mobile, string title, string message)
        {
            bool result = false;
            try
            {
                var client = new SMSSoapClient();
                var status = client.SendSMSBrandName(CommonBO.SetPhoneRegionCode(mobile), message,
                                        ServiceFacade.SettingsHelper.SmsSender, ServiceFacade.SettingsHelper.SmsUsername,
                                        ServiceFacade.SettingsHelper.SmsPassword);
                SingletonLogger.Instance.Debug(
                    String.Format("SendSMS-------Mobile: {0}-------Message: {1}-------Status: {2}", mobile, message,
                                  status));

                switch (status)
                {
                    case "1":
                        result = true;
                        break;
                    case "-2":
                        LoggerController.WriteLog("Wrong login info");
                        break;
                    default:
                        LoggerController.WriteLog("Loi ko xac dinh");
                        break;
                }

            }
            catch (Exception ex)
            {
                SingletonLogger.Instance.Error(String.Format("SendSms"), ex);
            }
            return result;
        }

        public bool SendSmsAppointment(string apptId, SmsCase smsCase, out string messageCode)
        {
            bool result = false;
            messageCode = MessageCode.GeneralCode.SystemError;
            try
            {
                SingletonLogger.Instance.Debug(String.Format("SendSmsAppointment-------ApptId: {0}", apptId));

                #region Validate
                var appt = DataRepository.AppointmentProvider.GetById(apptId);
                if (appt == null || appt.IsDisabled)
                {
                    messageCode = MessageCode.SmsCode.ApptNotExists;
                    return false;
                }

                var patient = DataRepository.VcsPatientProvider.GetByPatientCode(appt.PatientCode).FirstOrDefault();
                if (patient == null || string.IsNullOrEmpty(patient.MobilePhone))
                {
                    messageCode = MessageCode.SmsCode.NoMobile;
                    return false;
                }
                #endregion

                #region Lấy thông tin
                // Lấy đầy đủ thông tin
                var doctor = DataRepository.UsersProvider.GetByUsername(appt.Username);
                var room = DataRepository.RoomProvider.GetById(appt.RoomId.HasValue ? appt.RoomId.Value : 0);
                var service =
                    DataRepository.ServicesProvider.GetById(appt.ServicesId.HasValue ? appt.ServicesId.Value : 0);
                string doctorName = doctor == null ? string.Empty : doctor.Firstname,
                       roomName = room == null ? string.Empty : room.Title,
                       serviceName = service == null ? string.Empty : service.Title;
                #endregion

                #region Lưu Sms, Receiver, Log
                var sms = DataRepository.SmsProvider.GetByAppointmentId(apptId).FirstOrDefault();
                var receivers = new TList<SmsReceiver>();
                if (sms == null)
                {
                    sms = new Sms
                              {
                                  EntityState = EntityState.Added
                              };
                }
                else
                {
                    sms.EntityState = EntityState.Changed;
                    receivers = DataRepository.SmsReceiverProvider.GetBySmsId(sms.Id);
                }

                switch (smsCase)
                {
                    case SmsCase.WebApp:
                        sms.Message = String.Format(ServiceFacade.SettingsHelper.ApptSmsTemplate, patient.FirstName,
                                                    appt.StartTime, doctorName, roomName, serviceName);
                        break;
                    case SmsCase.Auto:
                        sms.Message = String.Format(ServiceFacade.SettingsHelper.ApptAutoSmsTemplate, patient.FirstName,
                                                    appt.StartTime, doctorName, roomName, serviceName);
                        break;
                }
                sms.SmsType = SmsType.Appointment;
                sms.SendTime = DateTime.Now;
                sms.IsSendNow = true;
                sms.IsSent = false;
                sms.SendingTimes += 1;
                sms.AppointmentId = appt.Id;
                sms = DataRepository.SmsProvider.Save(sms);
                if (sms == null)
                {
                    return false;
                }

                var receiver = receivers.FirstOrDefault(x => x.Mobile.Equals(patient.MobilePhone));
                if (receiver == null)
                {
                    receiver = new SmsReceiver
                                   {
                                       Mobile = patient.MobilePhone,
                                       FirstName = patient.FirstName,
                                       LastName = patient.LastName,
                                       UserType = SmsUserType.Patient,
                                       SmsId = sms.Id,
                                       IsSent = false,
                                       SendingTimes = 0,
                                       CreateDate = DateTime.Now
                                   };
                    DataRepository.SmsReceiverProvider.Insert(receiver);
                }

                var smsLog = new SmsLog
                                 {
                                     Id = Guid.NewGuid(),
                                     SmsId = sms.Id,
                                     Message = sms.Message,
                                     Mobile = receiver.Mobile,
                                     SendTime = sms.SendTime,
                                     RealSendTime = DateTime.Now,
                                     IsSent = false
                                 };
                DataRepository.SmsLogProvider.Insert(smsLog);
                #endregion

                #region Gửi sms và update thông tin

                var status = SendSms(smsLog.Mobile, smsLog.Title, smsLog.Message);
                if (status)
                {
                    sms.IsSent = receiver.IsSent = smsLog.IsSent = true;
                    result = true;
                    messageCode = MessageCode.SmsCode.Success;
                }

                DataRepository.SmsLogProvider.Update(smsLog);
                DataRepository.SmsProvider.Update(sms);
                DataRepository.SmsReceiverProvider.Update(receiver);

                #endregion
            }
            catch (Exception ex)
            {
                SingletonLogger.Instance.Error(String.Format("SendSmsAppointment"), ex);
            }
            return result;
        }

        public bool AddSms(Sms sms, string phones, out string messageCode)
        {
            bool result = false;
            messageCode = MessageCode.GeneralCode.SystemError;
            try
            {
                DataRepository.SmsProvider.Insert(sms);

                // Parse phone list
                var lstPhone = phones.Split(new[] { GeneralConstants.Seperate }, StringSplitOptions.None).Distinct().ToList();
                var receivers = new TList<SmsReceiver>();
                var smsLogs = new TList<SmsLog>();
                foreach (var p in lstPhone)
                {
                    var phone = p.Trim();
                    if (string.IsNullOrEmpty(phone))
                    {
                        continue;
                    }

                    receivers.Add(new SmsReceiver
                                      {
                                          Mobile = phone,
                                          UserType = SmsUserType.Patient,
                                          SmsId = sms.Id,
                                          IsSent = sms.IsSendNow, // Set true if send now
                                          SendingTimes = 1,
                                          CreateUser = sms.CreateUser,
                                          CreateDate = DateTime.Now
                                      });

                    if (sms.IsSendNow)
                    {
                        sms.IsSent = true;

                        smsLogs.Add(new SmsLog
                                        {
                                            Id = Guid.NewGuid(),
                                            SmsId = sms.Id,
                                            Message = sms.Message,
                                            Mobile = phone,
                                            SendTime = DateTime.Now,
                                            RealSendTime = DateTime.Now,
                                            IsSent = true,
                                            CreateDate = DateTime.Now,
                                            CreateUser = sms.CreateUser
                                        });
                        SendSms(phone, string.Empty, sms.Message);
                    }
                }

                DataRepository.SmsReceiverProvider.Insert(receivers);
                if (smsLogs.Any())
                {
                    DataRepository.SmsLogProvider.Insert(smsLogs);
                }

                DataRepository.SmsProvider.Update(sms);

                messageCode = MessageCode.SmsCode.Success;
                result = true;
            }
            catch (Exception ex)
            {
                SingletonLogger.Instance.Error(String.Format("AddSms"), ex);
            }
            return result;
        }

        public void CheckAndSendSms()
        {
            try
            {
                #region Send Appointment SMS
                // Lấy thời gian n tiếng tới
                var next24 = DateTime.Now.AddMinutes(ServiceFacade.SettingsHelper.ApptAutoSmsHour);
                var next23 = next24.AddHours(-2);
                int count;

                // Lấy ds appt thỏa điều kiện thời gian và status
                var appts =
                    DataRepository.AppointmentProvider.GetPaged(
                        String.Format(
                            "StartTime <= '{0:yyyy-MM-dd HH:mm:ss}' AND StartTime >= '{1:yyyy-MM-dd HH:mm:ss}' AND StatusId NOT IN ('{2}', '{3}', '{4}') AND IsDisabled = 0 AND IsComplete = 0",
                            next24, next23, ApptStatus.Cancelled, ApptStatus.Completed, ApptStatus.CheckedIn), string.Empty, 0, 99999, out count);

                // Lấy danh sách sms đã gửi dựa vào appt trên
                var smses = !appts.Any() ? new TList<Sms>() :
                    DataRepository.SmsProvider.GetPaged(String.Format("AppointmentId IN ('{0}')",
                                                                  String.Join("','", appts.Select(x => x.Id).ToArray())), string.Empty, 0, 99999, out count);

                // Lấy danh sách Appt chưa được gửi bao giờ
                var sendAppts = appts.FindAll(x => !smses.Exists(s => s.AppointmentId == x.Id));

                SingletonLogger.Instance.Debug(
                    String.Format(
                        "SendSmsAppointment-------Total Appt: {0}-------Total SendAppt: {1}-------Total Sms has been sent: {2}",
                        appts.Count, sendAppts.Count, smses.Count));

                foreach (var appointment in sendAppts)
                {
                    string messageCode;
                    SendSmsAppointment(appointment.Id, SmsCase.Auto, out messageCode);
                }
                #endregion

                #region Send Manual SMS

                smses = DataRepository.SmsProvider
                    .GetPaged(String.Format("IsSent = 0 AND SendTime <= '{0:yyyy-MM-dd HH:mm:ss}'",
                                            DateTime.Now), string.Empty, 0, 99999, out count);

                SingletonLogger.Instance.Debug(
                    String.Format("SendSms-------Total Sms: {0}", smses.Count));
                if (smses.Any())
                {
                    var receivers = DataRepository.SmsReceiverProvider
                        .GetPaged(String.Format("IsSent = 0 AND SmsId IN ({0})",
                                                String.Join(",", smses.Select(x => x.Id.ToString()).ToArray())),
                                  string.Empty, 0, 99999, out count);

                    foreach (var sms in smses)
                    {
                        Sms sms1 = sms;
                        var tmpReceivers = receivers.FindAll(x => x.SmsId == sms1.Id);
                        var smsLogs = new TList<SmsLog>();

                        foreach (var receiver in tmpReceivers)
                        {
                            smsLogs.Add(new SmsLog
                            {
                                Id = Guid.NewGuid(),
                                SmsId = sms.Id,
                                Message = sms.Message,
                                Mobile = receiver.Mobile,
                                SendTime = DateTime.Now,
                                RealSendTime = DateTime.Now,
                                IsSent = true,
                                CreateDate = DateTime.Now,
                                CreateUser = sms.CreateUser
                            }); 
                            SendSms(receiver.Mobile, string.Empty, sms.Message);
                            receiver.IsSent = true;
                            receiver.UpdateDate = DateTime.Now;
                            receiver.UpdateUser = "Auto";
                        }
                        DataRepository.SmsLogProvider.Insert(smsLogs);
                        DataRepository.SmsReceiverProvider.Update(tmpReceivers);

                        sms.IsSent = true;
                        sms.UpdateDate = DateTime.Now;
                        sms.UpdateUser = "Auto";
                        DataRepository.SmsProvider.Update(sms);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                SingletonLogger.Instance.Error(String.Format("CheckAndSendSms"), ex);
            }
        }
    }
}
