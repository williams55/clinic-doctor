using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appt.Common.Constants;
using Common.Extension;
using Common.Util;
using Logger.Controller;

namespace AppointmentBusiness.Util
{
    public class CommonBO
    {
        /// <summary>
        /// Define value of none or all value
        /// </summary>
        public const int NonValue = -1;

        /// <summary>
        /// Ham tach ngay truyen vao ra thanh ngay bat dau va ngay ket thuc tuy vao mode xem la gi
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="mode"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static bool GetDateTimeByMode(DateTime? dateTime, string mode, out DateTime fromDate, out DateTime toDate)
        {
            fromDate = toDate = new DateTime();
            try
            {
                if (dateTime == null) return false;

                fromDate = Convert.ToDateTime(dateTime);
                fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 0, 0, 0);
                toDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 23, 59, 59);
                switch (mode)
                {
                    case ModeView.Month:
                        // Neu loai thoi gian la thang thi lay ngay dau thang va cuoi thang
                        toDate = fromDate.AddMonths(1).LastDayOfMonth();
                        toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day, 23, 59, 59);
                        fromDate = (new DateTime(fromDate.Year, fromDate.Month, 1, 0, 0, 0)).AddMonths(-1);
                        break;
                    case ModeView.Week:
                        // Neu loai thoi gian la tuan thi lay ngay dau tuan va cuoi tuan
                        toDate = fromDate.LastDateOfWeek();
                        toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day, 23, 59, 59);
                        fromDate = fromDate.FirstDateOfWeek();
                        fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 0, 0, 0);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                LoggerController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return false;
        }

        /// <summary>
        /// Convert phone number to phone number with region code. 
        /// Ex: 0909123456 => 84909123456
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static string SetPhoneRegionCode(string phone)
        {
            if (!string.IsNullOrEmpty(phone) && phone.StartsWith("0") && phone.Length < 15)
            {
                return "84" + phone.Substring(1);
            }
            return phone;
        }

        /// <summary>
        /// Convert phone numberwith region code to general phone number. 
        /// Ex: 84909123456 => 0909123456
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static string RemovePhoneRegionCode(string phone)
        {
            if (phone.StartsWith("84") && phone.Length < 15)
            {
                return "0" + phone.Substring(2);
            }
            return phone;
        }
    }
}
