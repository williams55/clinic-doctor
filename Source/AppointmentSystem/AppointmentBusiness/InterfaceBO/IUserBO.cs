using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppointmentSystem.Entities;

namespace AppointmentBusiness.BO
{
    public interface IUserBO
    {
        /// <summary>
        /// Validate current user is active or not [not disabled or unexisted]
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="user">Users object is returned.</param>
        /// <param name="message">Message is returned</param>
        /// <returns></returns>
        bool ValidateCurrentUser(string username, out Users user, out string message);

        /// <summary>
        /// Kiem tra username va password khi dang nhap co thanh cong khong
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool Authentication(string username, string password);
    }
}
