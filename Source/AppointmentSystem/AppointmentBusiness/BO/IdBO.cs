using System;
using AppointmentSystem.Data;
using AppointmentSystem.Entities;
using AppointmentSystem.Settings.BusinessLayer;
using Common.Util;
using Log.Controller;

namespace AppointmentBusiness.BO
{
    public class IdBO : IIdBO
    {
        public string RosterId()
        {
            string perfix = ServiceFacade.SettingsHelper.RosterPrefix + DateTime.Now.ToString("yyMMdd");
            int count;
            TList<Roster> objPo = DataRepository.RosterProvider.GetPaged("Id like '" + perfix + "' + '%'", "Id desc", 0, 1, out count);

            return String.Format("{0}{1}", perfix,
                                 count == 0? "001"
                                     : String.Format("{0:000}",int.Parse(objPo[0].Id.Substring(objPo[0].Id.Length - 3)) + 1));
        }

        public string RosterId(object number)
        {
            if (number == null) return RosterId();

            string strNumber = number.ToString();
            strNumber = strNumber.Length >= 3
                            ? String.Format("{0:000}", Convert.ToInt32(strNumber.Substring(strNumber.Length - 3)) + 1)
                            : String.Format("{0:000}", Convert.ToInt32(strNumber) + 1);
            return String.Format("{0}{1}{2}", ServiceFacade.SettingsHelper.RosterPrefix,
                                       DateTime.Now.ToString("yyMMdd"), strNumber);
        }
    }
}
