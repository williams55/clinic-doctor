using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppointmentSystem.Entities;

namespace AppointmentBusiness.BO
{
    public interface IIdBO
    {
        /// <summary>
        /// Generate Id cho roster
        /// </summary>
        /// <returns></returns>
        string RosterId();

        /// <summary>
        /// Generate Id cho roster voi so co san
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        string RosterId(object number);
    }
}
