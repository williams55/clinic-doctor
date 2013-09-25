using System;
using AppointmentBusiness.BO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppointmentBusiness.Tests
{
    [TestClass]
    public class CheckAndSendSmsUT
    {
        [TestMethod]
        public void Success()
        {
            BoFactory.SmsBO.CheckAndSendSms();
        }
    }
}
