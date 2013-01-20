using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppointmentBusiness.BO
{
    public static class BoFactory
    {
        public static IUserBO UserBO { get { return new UserBO(); } }
        public static IStatusBO StatusBO { get { return new StatusBO(); } }
        public static IRosterBO RosterBO { get { return new RosterBO(); } }
        public static IAppointmentBO AppointmentBO { get { return new AppointmentBO(); } }
        public static IIdBO IdBO { get { return new IdBO(); } }
        public static IPatientBO PatientBO { get { return new PatientBO(); } }
        public static IMessageConfigBO MessageConfigBO { get { return new MessageConfigBO(); } }
    }
}
