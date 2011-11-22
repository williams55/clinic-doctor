
using System;
using System.Reflection;

using ClinicDoctor.Settings.Settings;
using ClinicDoctor.Settings.Model;
using System.Globalization;

namespace ClinicDoctor.Settings.BusinessLayer
{
    public class SettingsService
    {
        public bool TryGetSetting<T>(string code, out T value)
        {
            SettingInfo setting = SettingInfo.LoadSetting(code);

            if (typeof(T).GetInterface(typeof(IConvertible).FullName) != null)
            {
                value = (T)Convert.ChangeType(setting.ValueString, typeof(T), CultureInfo.InvariantCulture);
                return true;
            }
            else if (typeof(T).IsClass)
            {
                ConstructorInfo constructor = typeof(T).GetConstructor(Type.EmptyTypes);
                if (constructor == null)
                {
                    throw new ApplicationException(String.Format("Type {0} has no empty constructors", typeof(T).Name));
                }
                value = (T)constructor.Invoke(new object[] { });

                if (setting.ValueBinary != null && value is ICustomBinarySerializable)
                {
                    ICustomBinarySerializable iCustomBinarySerializable = value as ICustomBinarySerializable;
                    iCustomBinarySerializable.Deserialize(setting.ValueBinary);
                    return true;
                }
                else if (!String.IsNullOrEmpty(setting.ValueString) && value is ICustomXmlSerializable)
                {
                    ICustomXmlSerializable iCustomXmlSerializable = value as ICustomXmlSerializable;
                    iCustomXmlSerializable.Deserialize(setting.ValueString);
                    return true;
                }
            }

            value = default(T);
            return false;
        }
        public void SaveSetting<T>(string code, T value)
        {
            SettingInfo setting = SettingInfo.LoadSetting(code);
            if (setting == null)
            {
                setting = new SettingInfo();
                setting.Code = code;
            }

            Type type = typeof(T);

            if (type.GetInterface(typeof(IConvertible).FullName) != null)
            {
                setting.ValueString = Convert.ToString(value, CultureInfo.InvariantCulture);
            }
            else if (type.IsClass)
            {
                if (value is ICustomBinarySerializable)
                {
                    ICustomBinarySerializable iCustomBinarySerializable = value as ICustomBinarySerializable;
                    setting.ValueBinary = iCustomBinarySerializable.Serialize();
                }
                else if (value is ICustomXmlSerializable)
                {
                    ICustomXmlSerializable iCustomXmlSerializable = value as ICustomXmlSerializable;
                    setting.ValueString = iCustomXmlSerializable.Serialize();
                }
            }

            SettingInfo.SaveSetting(setting);
        }
    }
}