
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

        #region "Status"
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
                if (!ServiceFacade.SettingsService.TryGetSetting<string>("AppointmentPrefix", out result))
                {
                    throw new ApplicationException("Setting AppointmentPrefix was not found.");
                }

                return result;
            }
            set { ServiceFacade.SettingsService.SaveSetting<string>("AppointmentPrefix", value); }
        }
        #endregion

    }
}