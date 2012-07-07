using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppointmentBusiness.BO
{
    public static class BoFactory
    {
        public static IUserBO UserBO { get { return new UserBO(); } }
    }
}
