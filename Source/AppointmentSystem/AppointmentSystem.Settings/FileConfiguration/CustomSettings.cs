
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace AppointmentSystem.Settings.FileConfiguration
{
    public sealed class CustomSettings
    {
        #region singleton
        private static CustomSettings current;
        private static readonly object singletonSync = new object();
        private static CustomSettings Instance
        {
            get
            {
                if (CustomSettings.current == null)
                {
                    lock (CustomSettings.singletonSync)
                    {
                        if (CustomSettings.current == null)
                        {
                            CustomSettings.current = new CustomSettings();
                        }
                    }
                }
                return CustomSettings.current;
            }
        }
        #endregion singleton

        #region Class Members
        private FileConfigurationSource fileConfigurationSource;
        #endregion Class Members

        #region Constructors
        private CustomSettings()
        {
            string settingsPath = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "Custom.config");
            this.fileConfigurationSource = new FileConfigurationSource(settingsPath);
        }
        #endregion Constructors

        #region Properties
        public static CustomSection Current
        {
            get { return (CustomSection)CustomSettings.Instance.fileConfigurationSource.GetSection("customSettings"); }
        }
        #endregion Properties
    }    
}