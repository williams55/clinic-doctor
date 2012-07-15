using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                // Get role list of user
                var objUserRole = DataRepository.UsersProvider.GetByUsername(username);
                DataRepository.UsersProvider.DeepLoad(objUserRole);
                var lstUserRole = objUserRole.UserRoleCollection;

                int count;
                var abc = lstUserRole.Select(userRole => DataRepository.RoleDetailProvider.GetPaged(
                    String.Format("IsDisabled = 'False' AND RoleId = {0} AND ScreenCode = '{1}'", userRole.RoleId,
                                  screen)
                    , string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength, out count));
                // Get list of role
                if (lstUserRole.Select(userRole => DataRepository.RoleDetailProvider.GetPaged(
                    String.Format("IsDisabled = 'False' AND RoleId = {0} AND ScreenCode = '{1}'", userRole.RoleId, screen)
                    , string.Empty, 0, ServiceFacade.SettingsHelper.GetPagedLength, out count)).Any(lstRoleDetail => lstRoleDetail.Exists(x => x.Crud.ToLower().Contains(operation.ToLower()))))
                {
                    return true;
                }
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
