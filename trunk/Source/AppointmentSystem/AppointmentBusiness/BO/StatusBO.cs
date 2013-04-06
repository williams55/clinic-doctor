using System;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using Common.Util;
using Logger.Controller;

namespace AppointmentBusiness.BO
{
    public class StatusBO : IStatusBO
    {
        public string GetColor(string status)
        {
            string result = string.Empty;
            try
            {
                var obj = DataRepository.StatusProvider.GetById(status);
                if (obj != null)
                    result = obj.ColorCode;
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return result;
        }

        public string GetTitle(string status)
        {
            string result = string.Empty;
            try
            {
                var obj = DataRepository.StatusProvider.GetById(status);
                if (obj != null)
                    result = obj.Title;
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return result;
        }
    }
}
