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
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
        StepResult:
            return blResult;
        }

        public bool Authentication(string username, string password)
        {
            try
            {
                int count;
                DataRepository.UsersProvider.GetPaged(
                    String.Format("Username = '{0}' AND Password = '{1}' AND IsDisabled = 'False'", username,
                                  Encrypt.EncryptPassword(password)), string.Empty, 0,
                    1, out count);
                return count > 0;
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return false;
        }

        public bool ChangePassword(string username, string password, string newPassword)
        {
            try
            {
                int count;
                var users = DataRepository.UsersProvider.GetPaged(
                    String.Format("Username = '{0}' AND Password = '{1}' AND IsDisabled = 'False'", username,
                                  Encrypt.EncryptPassword(password)), string.Empty, 0,
                    1, out count);
                if (count > 0)
                {
                    var user = users[0];
                    user.Password = Encrypt.EncryptPassword(newPassword);
                    DataRepository.UsersProvider.Save(user);
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogController.WriteLog(System.Runtime.InteropServices.Marshal.GetExceptionCode(), ex, Network.GetIpClient());
            }
            return false;
        }
    }
}
