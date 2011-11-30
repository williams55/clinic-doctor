
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
    }
}