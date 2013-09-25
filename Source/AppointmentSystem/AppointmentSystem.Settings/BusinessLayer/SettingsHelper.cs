
using System;

namespace AppointmentSystem.Settings.BusinessLayer
{
    public sealed class SettingsHelper
    {
        #region singletone
        private static readonly object singletonSync = new object();
        private static SettingsHelper current;
        public static SettingsHelper Current
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                if (SettingsHelper.current == null)
                {
                    lock (SettingsHelper.singletonSync)
                    {
                        if (SettingsHelper.current == null)
                        {
                            SettingsHelper.current = new SettingsHelper();
                        }
                    }
                }
                return SettingsHelper.current;
            }
        }
        private SettingsHelper()
        {
        }
        #endregion singletone

        public DateTime InitialDate
        {
            get
            {
                DateTime result;
                if (!ServiceFacade.SettingsService.TryGetSetting<DateTime>("INITIAL_DATE", out result))
                {
                    throw new ApplicationException("Setting INITIAL_DATE was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting<DateTime>("INITIAL_DATE", value);
            }
        }

        #region "Setting time"
        // Minute Step
        public int MinuteStep
        {
            get
            {
                int result;
                if (!ServiceFacade.SettingsService.TryGetSetting("MINUTE_STEP", out result))
                {
                    throw new ApplicationException("Setting MINUTE_STEP was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting("MINUTE_STEP", value);
            }
        }

        // Minute Step
        public int RosterMinuteStep
        {
            get
            {
                int result;
                if (!ServiceFacade.SettingsService.TryGetSetting("ROSTER_MINUTE_STEP", out result))
                {
                    throw new ApplicationException("Setting ROSTER_MINUTE_STEP was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting("ROSTER_MINUTE_STEP", value);
            }
        }

        // Max Minute
        public int MaxMinute
        {
            get
            {
                int result;
                if (!ServiceFacade.SettingsService.TryGetSetting("MAX_MINUTE", out result))
                {
                    throw new ApplicationException("Setting MAX_MINUTE was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting("MAX_MINUTE", value);
            }
        }

        // Max Hour
        public int MaxHour
        {
            get
            {
                int result;
                if (!ServiceFacade.SettingsService.TryGetSetting("MAX_HOUR", out result))
                {
                    throw new ApplicationException("Setting MAX_HOUR was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting("MAX_HOUR", value);
            }
        }

        // Max Minute
        public int MinutePerHour
        {
            get
            {
                int result;
                if (!ServiceFacade.SettingsService.TryGetSetting("MINUTE_PER_HOUR", out result))
                {
                    throw new ApplicationException("Setting MINUTE_PER_HOUR was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting("MINUTE_PER_HOUR", value);
            }
        }

        // Time [Minute] left to remind appointment
        public double TimeLeftRemindAppointment
        {
            get
            {
                int result;
                if (!ServiceFacade.SettingsService.TryGetSetting("TIME_LEFT_RMIND_APPOINTMENT", out result))
                {
                    throw new ApplicationException("Setting TIME_LEFT_RMIND_APPOINTMENT was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting("TIME_LEFT_RMIND_APPOINTMENT", value);
            }
        }
        #endregion

        #region "Status, Color"
        // Bien nay dung de tao lop mau phu len cho nao trong appointment ma ko the tao
        public string NotAvailableColor
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting<string>("NOT_AVAILABLE_COLOR", out result))
                {
                    throw new ApplicationException("Setting NOT_AVAILABLE_COLOR was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting<string>("NOT_AVAILABLE_COLOR", value);
            }
        }
        public string CompleteColor
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting<string>("COMPLETE_COLOR", out result))
                {
                    throw new ApplicationException("Setting COMPLETE_COLOR was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting<string>("COMPLETE_COLOR", value);
            }
        }

        public string UncompleteColor
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting<string>("UNCOMPLETE_COLOR", out result))
                {
                    throw new ApplicationException("Setting UNCOMPLETE_COLOR was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting<string>("UNCOMPLETE_COLOR", value);
            }
        }

        public string RosterInAppointmentColor
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting<string>("ROSTER_IN_APPOINTMENT_COLOR", out result))
                {
                    throw new ApplicationException("Setting ROSTER_IN_APPOINTMENT_COLOR was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting<string>("ROSTER_IN_APPOINTMENT_COLOR", value);
            }
        }
        #endregion

        #region "Prefix"
        /// <summary>
        /// Prefix for Roster Id
        /// </summary>
        public string RosterPrefix
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting("ROSTER_PREFIX", out result))
                {
                    throw new ApplicationException("Setting ROSTER_PREFIX was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting<string>("ROSTER_PREFIX", value);
            }
        }

        /// <summary>
        /// Prefix for Appointment Id
        /// </summary>
        public string AppointmentPrefix
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting("APPOINTMENT_PREFIX", out result))
                {
                    throw new ApplicationException("Setting APPOINTMENT_PREFIX was not found.");
                }

                return result;
            }
            set { ServiceFacade.SettingsService.SaveSetting("APPOINTMENT_PREFIX", value); }
        }

        /// <summary>
        /// Prefix for Patient Id
        /// </summary>
        public string PatientPrefix
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting("PATIENT_PREFIX", out result))
                {
                    throw new ApplicationException("Setting PATIENT_PREFIX was not found.");
                }

                return result;
            }
            set { ServiceFacade.SettingsService.SaveSetting("PATIENT_PREFIX", value); }
        }

        /// <summary>
        /// Prefix for User Id
        /// </summary>
        public string UserPrefix
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting("USER_PREFIX", out result))
                {
                    throw new ApplicationException("Setting USER_PREFIX was not found.");
                }

                return result;
            }
            set { ServiceFacade.SettingsService.SaveSetting("USER_PREFIX", value); }
        }
        #endregion

        #region "Roles"
        // Roles: Admin;Manager;Doctor;Receptionist;Nurse
        public string Roles
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting<string>("ROLES", out result))
                {
                    throw new ApplicationException("Setting ROLES was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting<string>("ROLES", value);
            }
        }
        #endregion

        #region Location
        /// <summary>
        /// Location code for a clinic
        /// </summary>
        public string LocationCode
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting("LOCATION_CODE", out result))
                {
                    throw new ApplicationException("Setting LOCATION_CODE was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting("LOCATION_CODE", value);
            }
        }
        #endregion

        #region Number setting
        // Max number can get all when using GetPaged
        public int GetPagedLength
        {
            get
            {
                int result;
                if (!ServiceFacade.SettingsService.TryGetSetting("GET_PAGED_LENGTH", out result))
                {
                    throw new ApplicationException("Setting GET_PAGED_LENGTH was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting("GET_PAGED_LENGTH", value);
            }
        }

        // Page size
        public int PageSize
        {
            get
            {
                int result;
                if (!ServiceFacade.SettingsService.TryGetSetting("PAGE_SIZE", out result))
                {
                    throw new ApplicationException("Setting PAGE_SIZE was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting("PAGE_SIZE", value);
            }
        }

        // Max priority index
        public int MaxPriorityIndex
        {
            get
            {
                int result;
                if (!ServiceFacade.SettingsService.TryGetSetting("MAX_PRIORITY_INDEX", out result))
                {
                    throw new ApplicationException("Setting MAX_PRIORITY_INDEX was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting("MAX_PRIORITY_INDEX", value);
            }
        }
        #endregion

        #region Default value
        public int DefaultRosterType
        {
            get
            {
                int result;
                if (!ServiceFacade.SettingsService.TryGetSetting<int>("DEFAULT_ROSTER_TYPE", out result))
                {
                    throw new ApplicationException("Setting DEFAULT_ROSTER_TYPE was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting<int>("DEFAULT_ROSTER_TYPE", value);
            }
        }
        #endregion

        #region Sms
        /// <summary>
        /// Template cho SMS của appointment.
        /// {0}: FirstName của patient
        /// {1}: Thời gian hẹn
        /// {2}: Tên bác sĩ
        /// {3}: Phòng
        /// {4}: Dịch vụ
        /// </summary>
        public string ApptSmsTemplate
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting("ApptSmsTemplate", out result))
                {
                    throw new ApplicationException("Setting ApptSmsTemplate was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting("ApptSmsTemplate", value);
            }
        }

        /// <summary>
        /// Template cho SMS của appointment khi chạy tự động.
        /// {0}: FirstName của patient
        /// {1}: Thời gian hẹn
        /// {2}: Tên bác sĩ
        /// {3}: Phòng
        /// {4}: Dịch vụ
        /// </summary>
        public string ApptAutoSmsTemplate
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting("ApptAutoSmsTemplate", out result))
                {
                    throw new ApplicationException("Setting ApptAutoSmsTemplate was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting("ApptAutoSmsTemplate", value);
            }
        }

        /// <summary>
        /// Tên người gửi SMS
        /// </summary>
        public string SmsSender
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting("SmsSender", out result))
                {
                    throw new ApplicationException("Setting SmsSender was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting("SmsSender", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SmsUsername
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting("SmsUsername", out result))
                {
                    throw new ApplicationException("Setting SmsUsername was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting("SmsUsername", value);
            }
        }

        /// <summary>
        /// Tên người gửi SMS
        /// </summary>
        public string SmsPassword
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting("SmsPassword", out result))
                {
                    throw new ApplicationException("Setting SmsPassword was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting("SmsPassword", value);
            }
        }

        /// <summary>
        /// Cấu hình thời gian sms được gửi khi tạo mới appt.
        /// Đơn vị là phút
        /// </summary>
        public long ApptSmsHour
        {
            get
            {
                long result;
                if (!ServiceFacade.SettingsService.TryGetSetting("ApptSmsHour", out result))
                {
                    throw new ApplicationException("Setting ApptSmsHour was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting("ApptSmsHour", value);
            }
        }

        /// <summary>
        /// Cấu hình thời gian sms tự động gửi khi nằm trong khoảng yêu cầu
        /// Đơn vị là phút
        /// </summary>
        public long ApptAutoSmsHour
        {
            get
            {
                long result;
                if (!ServiceFacade.SettingsService.TryGetSetting("ApptAutoSmsHour", out result))
                {
                    throw new ApplicationException("Setting ApptAutoSmsHour was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting("ApptAutoSmsHour", value);
            }
        }
        #endregion
    }
}