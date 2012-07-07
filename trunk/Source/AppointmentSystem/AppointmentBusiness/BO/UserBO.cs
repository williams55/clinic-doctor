using System;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using Common.Util;
using Log.Controller;

namespace AppointmentBusiness.BO
{
    public class UserBO : IUserBO
    {
        public bool ValidateCurrentUser(string username, out Users user, out string message)
        {
            bool blResult = false;
            message = string.Empty;
            user = null;
            try
            {
                // Get user's info
                user = DataRepository.UsersProvider.GetByUsername(username);

                // If there is no user
                if (user == null)
                {
                    message = "User is not exist.";
                    goto StepResult;
                }

                // If user is disabled
                if (user.IsDisabled)
                {
                    message = "User is not active.";
                    goto StepResult;
                }

                blResult = true;
            }
            catch(Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
        StepResult:
            return blResult;
        }
    }
}
