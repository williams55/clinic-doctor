using System;
using AppointmentBusiness.BO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppointmentBusiness.Tests
{
    [TestClass]
    public class SendSmsUT
    {
        [TestMethod]
        public void Success()
        {
            BoFactory.SmsBO.SendSms("097996894", string.Empty, "Noi dung test thu");
        }
    }
}
