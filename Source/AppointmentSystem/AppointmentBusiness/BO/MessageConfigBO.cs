using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using AppointmentBusiness.Util;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using Appt.Common.Constants;
using Common.Util;
using Log.Controller;

namespace AppointmentBusiness.BO
{
    public class MessageConfigBO : IMessageConfigBO
    {
        #region Implementation of IMessageConfigBO

        public string GetMessage(string key)
        {
            try
            {
                var message = DataRepository.MessageConfigProvider.GetByMessageKey(key);
                if (message != null)
                {
                    return message.MessageValue;
                }
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return string.Empty;
        }

        #endregion
    }

    public class MessageCode
    {
        public class GeneralCode
        {
            public const string SystemError = "GEN0001";
        }

        public class StringCode
        {
            public const string OverMaxLength = "STR0001";
        }

        public class AuthCode
        {
            public const string SessionTimeOut = "ATH0001";
        }
    }
}
