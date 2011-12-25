
using System;

namespace ClinicDoctor.Settings.BusinessLayer
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
                if (!ServiceFacade.SettingsService.TryGetSetting<int>("MINUTE_STEP", out result))
                {
                    throw new ApplicationException("Setting MINUTE_STEP was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting<int>("MINUTE_STEP", value);
            }
        }

        // Max Minute
        public int MaxMinute
        {
            get
            {
                int result;
                if (!ServiceFacade.SettingsService.TryGetSetting<int>("MAX_MINUTE", out result))
                {
                    throw new ApplicationException("Setting MAX_MINUTE was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting<int>("MAX_MINUTE", value);
            }
        }

        // Max Hour
        public int MaxHour
        {
            get
            {
                int result;
                if (!ServiceFacade.SettingsService.TryGetSetting<int>("MAX_HOUR", out result))
                {
                    throw new ApplicationException("Setting MAX_HOUR was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting<int>("MAX_HOUR", value);
            }
        }
        #endregion

        #region "Status, Color"
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
        public string RosterPrefix
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting<string>("ROSTER_PREFIX", out result))
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

        public string AppointmentPrefix
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting<string>("APPOINTMENT_PREFIX", out result))
                {
                    throw new ApplicationException("Setting APPOINTMENT_PREFIX was not found.");
                }

                return result;
            }
            set { ServiceFacade.SettingsService.SaveSetting<string>("APPOINTMENT_PREFIX", value); }
        }

        public string CustomerPrefix
        {
            get
            {
                string result;
                if (!ServiceFacade.SettingsService.TryGetSetting<string>("CUSTOMER_PREFIX", out result))
                {
                    throw new ApplicationException("Setting CUSTOMER_PREFIX was not found.");
                }

                return result;
            }
            set { ServiceFacade.SettingsService.SaveSetting<string>("CUSTOMER_PREFIX", value); }
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

        // Time [Minute] left to remind appointment
        public double TimeLeftRemindAppointment
        {
            get
            {
                int result;
                if (!ServiceFacade.SettingsService.TryGetSetting<int>("TIME_LEFT_RMIND_APPOINTMENT", out result))
                {
                    throw new ApplicationException("Setting TIME_LEFT_RMIND_APPOINTMENT was not found.");
                }

                return result;
            }
            set
            {
                ServiceFacade.SettingsService.SaveSetting<double>("TIME_LEFT_RMIND_APPOINTMENT", value);
            }
        }
    }
}