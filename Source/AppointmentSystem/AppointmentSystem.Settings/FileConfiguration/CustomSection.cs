
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

namespace AppointmentSystem.Settings.FileConfiguration
{
    public class CustomSection : ConfigurationSection
    {
        #region properties
        [ApplicationScopedSetting]
        [ConfigurationProperty("applicationName", DefaultValue = "AppointmentSystem.Settings", IsRequired = true)]
        [StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String ApplicationName
        {
            get { return (String)this["applicationName"]; }
            set { this["applicationName"] = value; }
        }

        [ApplicationScopedSetting]
        [ConfigurationProperty("connectionString", IsRequired = true)]
        public String ConnectionString
        {
            get { return (String)this["connectionString"]; }
            set { this["connectionString"] = value; }
        }

        [ApplicationScopedSetting]
        [ConfigurationProperty("providers", IsDefaultCollection = false, IsRequired = true)]
        [ConfigurationCollection(typeof(System.Configuration.NameValueConfigurationCollection))]
        public NameValueConfigurationCollection Providers
        {
            get { return (NameValueConfigurationCollection)this["providers"]; }
            set { this["providers"] = value; }
        }
        #endregion properties
    }
}