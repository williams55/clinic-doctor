
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
    }
}