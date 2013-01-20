using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppointmentSystem.Entities;

namespace AppointmentBusiness.BO
{
    public interface IMessageConfigBO
    {
        /// <summary>
        /// Get message content by message code
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetMessage(string key);
    }
}
