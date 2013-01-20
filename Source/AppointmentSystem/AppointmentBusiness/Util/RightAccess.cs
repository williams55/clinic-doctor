using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppointmentBusiness.BO;
using AppointmentSystem.Data;
using AppointmentSystem.Settings.BusinessLayer;
using Appt.Common.Constants;
using Common.Util;
using Log.Controller;

namespace AppointmentBusiness.Util
{
    public class RightAccess
    {
        /// <summary>
        /// Check right of user
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="screen">What screen need to operation</param>
        /// <param name="operation">Operation [CRUD]</param>
        /// <param name="message">Message is returned</param>
        /// <returns></returns>
        public static bool CheckUserRight(string username, string screen, string operation, out string message)
        {
            message = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    message = BoFactory.MessageConfigBO.GetMessage(MessageCode.AuthCode.SessionTimeOut);
                    return false;
                }

                // Get role list of user
                var lstUserRole = DataRepository.UserRoleProvider.GetByUsername(username);

                int count;
                var roleDetails = DataRepository.RoleDetailProvider.GetPaged(
                    String.Format("IsDisabled = 'False' AND ScreenCode = '{0}'", screen),
                    string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength, out count);
                roleDetails = roleDetails.FindAll(
                    roleDetail =>
                    lstUserRole.Exists(userRole => roleDetail.RoleId == userRole.RoleId) &&
                    roleDetail.Crud.ToLower().Contains(operation.ToLower()));

                // Get list of role
                if(roleDetails.Any())
                    return true;
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }

            // There is no right for user
            message = String.Format("You have no right to {0}", OperationConstant.Instant().GetValueByKey(operation));
            return false;
        }
    }
}
