using System.Collections.Generic;
using Common;

namespace Appt.Common.Constants
{
    /// <summary>
    /// Trường hợp SMS
    /// </summary>
    public enum SmsCase
    {
        /// <summary>
        /// Khi user tạo appt và gửi SMS
        /// </summary>
        WebApp = 1,

        /// <summary>
        /// Khi service chạy để auto gửi SMS
        /// </summary>
        Auto = 2
    }

    /// <summary>
    /// Loại SMS
    /// </summary>
    public class SmsType
    {
        public static List<ConstantKeyValue> GetAll()
        {
            return new List<ConstantKeyValue>
                       {
                           new ConstantKeyValue("1", "Appointment"),
                           new ConstantKeyValue("2", "Other")
                       };
        }

        public const byte Appointment = 1;
        public const byte Other = 2;
    }

    /// <summary>
    /// Loại SMS
    /// </summary>
    public class SmsUserType
    {
        public static List<ConstantKeyValue> GetAll()
        {
            return new List<ConstantKeyValue>
                       {
                           new ConstantKeyValue("1", "Doctor"),
                           new ConstantKeyValue("2", "Patient")
                       };
        }

        public const byte Doctor = 1;
        public const byte Patient = 2;
    }

    /// <summary>
    /// Loại SMS
    /// </summary>
    public class SmsStatus
    {
        public const int Success = 0;
    }
}
