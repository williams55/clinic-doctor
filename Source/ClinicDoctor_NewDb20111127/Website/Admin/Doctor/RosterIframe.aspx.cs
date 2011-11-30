using System;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web;
using ClinicDoctor.Data;
using ClinicDoctor.Entities;
using ClinicDoctor.Settings.BusinessLayer;
using System.Globalization;

public partial class Admin_Doctor_RosterIframe : System.Web.UI.Page
{
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetRosterType()
    {
        TList<RosterType> lst = DataRepository.RosterTypeProvider.GetByIsDisabled(false);

        string result = "[]";
        if (lst.Count > 0)
        {
            result = string.Empty;
            foreach (RosterType item in lst)
            {
                result += @"{'key' : '" + item.Id + @"'" + "," + @"'label' : '" + ((item.Note == null) ? "" : item.Note) + @"'" + "},";
            }
            result = "[" + result.Substring(0, result.Length - 1) + "]";
        }

        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetTime()
    {
        int step = ServiceFacade.SettingsHelper.MinuteStep;
        int minute = ServiceFacade.SettingsHelper.MaxMinute;
        int hour = ServiceFacade.SettingsHelper.MaxHour;

        TList<RosterType> lst = DataRepository.RosterTypeProvider.GetByIsDisabled(false);

        string result = string.Empty;
        int m = 0;
        string key = string.Empty;
        string label = string.Empty;

        for (int h = 0; h < hour; h++)
        {
            m = 0;
            while (m < minute)
            {
                key = h.ToString("00") + ":" + m.ToString("00") + ":00";
                label = h.ToString("00") + ":" + m.ToString("00");
                result += @"{'key' : '" + key + @"'" + "," + @"'label' : '" + label + @"'" + "},";
                m += step;
            }
        }
        if (result.Length > 1)
            result = "[" + result.Substring(0, result.Length - 1) + "]";

        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string SaveEvent(string RosterType, string StartTime, string EndTime, string Note, string RepeatRoster, string Weekday, string Month)
    {
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data:  [{
         *              Id: id of roster,
         *              DoctorId: Doctor Id,
         *              RosterTypeId: Roster Type Id,
         *              start_date: start date,
         *              end_date: end date,
         *              note: note,
         *              text: roster type + note,
         *              color: color of event [get from status],
         *              isnew: true
         *          }, {}, {}]
         *  }
         */

        string result = string.Empty;
        try
        {
            string username = EntitiesUtilities.GetAuthName();
            Staff obj = DataRepository.StaffProvider.GetByUserNameIsDisabled(username, false);
            if (obj == null)
            {
                result = @"[{ 'result': 'false', 'message': 'Doctor is not exist.', 'data': '[]' }]";
                return result;
            }

            if (RepeatRoster == "true")
            {
                RosterType refObj = DataRepository.RosterTypeProvider.GetByIdIsDisabled(Convert.ToInt64(RosterType), false);
                
            }
            else
            {
                RosterType refObj = DataRepository.RosterTypeProvider.GetByIdIsDisabled(Convert.ToInt64(RosterType), false);
                DoctorRoster newObj = new DoctorRoster();

                string Perfix = "R" + ServiceFacade.SettingsHelper.RosterPrefix + DateTime.Now.ToString("yyMMdd");
                int Count = 0;
                TList<DoctorRoster> objPo = DataRepository.DoctorRosterProvider.GetPaged("Id like '" + Perfix + "' + '%'", "Id desc", 0, 1, out Count);
                if (Count == 0)
                    newObj.Id = Perfix + "001";
                else
                {
                    newObj.Id = Perfix + String.Format("{0:000}", int.Parse(objPo[0].Id.Substring(objPo[0].Id.Length - 3)) + 1);
                }

                newObj.DoctorId = obj.Id;
                newObj.RosterTypeId = Convert.ToInt64(RosterType);
                newObj.StartTime = Convert.ToDateTime(StartTime, new CultureInfo("vi-VN"));
                newObj.EndTime = Convert.ToDateTime(EndTime, new CultureInfo("vi-VN"));
                newObj.Note = Note;
                newObj.CreateUser = username;
                newObj.UpdateUser = username;
                DataRepository.DoctorRosterProvider.Insert(newObj);

                result = @"[{ 'result': 'true', 'message': '', 'data': ["
                    + @"{id: '" + newObj.Id + @"',"
                    + @"start_date: '" + newObj.StartTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"end_date: '" + newObj.EndTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                    + @"text: 'Title: " + refObj.Note + @"<br />";

                if (!string.IsNullOrEmpty(newObj.Note))
                    result += @"Note: " + newObj.Note;

                result += @"',DoctorId: '" + newObj.DoctorId + @"',"
                   + @"RosterTypeId: '" + newObj.DoctorId + @"',"
                   + @"note: '" + newObj.Note + @"',"
                   + @"color: '" + ServiceFacade.SettingsHelper.UncompleteColor + @"',"
                   + @"isnew: 'true'"
                   + @"}"
                   + @"] }]";
            }

        }
        catch (Exception ex)
        {
            //Write log cho nay

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': '[]' }]";
            return result;
        }

        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string LoadRoster(string Mode, string CurrentDateView)
    {
        /* Return Structure
         * { result: true [success], false [fail],
         *   message: message content [will be showed when fail]
         *   data:  [{
         *              Id: id of roster,
         *              DoctorId: Doctor Id,
         *              RosterTypeId: Roster Type Id,
         *              start_date: start date,
         *              end_date: end date,
         *              note: note,
         *              text: roster type + note,
         *              color: color of event [get from status],
         *              isnew: true
         *          }, {}, {}]
         *  }
         */

        string result = string.Empty;
        try
        {
            string username = EntitiesUtilities.GetAuthName();
            Staff obj = DataRepository.StaffProvider.GetByUserNameIsDisabled(username, false);
            if (obj == null)
            {
                result = @"[{ 'result': 'false', 'message': 'Doctor is not exist.', 'data': '[]' }]";
                return result;
            }

            TList<RosterType> lstRefObj = DataRepository.RosterTypeProvider.GetByIsDisabled(false);
            // Cho nay se ve sua lai database, them index cho GetByDoctorId va Disabled
            // Lam them cai neu Note ma rong thi ko hien thi
            TList<DoctorRoster> lstObj = DataRepository.DoctorRosterProvider.GetByIsDisabled(false);
            ServiceFacade.SettingsHelper.UncompleteColor = "#A8D4FF";
            ServiceFacade.SettingsHelper.CompleteColor = "#BBBBBB";

            string color = string.Empty;
            foreach (DoctorRoster item in lstObj)
            {
                RosterType itemRef = lstRefObj.Find("Id", item.DoctorId);
                if (itemRef != null)
                {
                    result += @"{id: '" + item.Id + @"',"
                        + @"start_date: '" + item.StartTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                        + @"end_date: '" + item.EndTime.ToString("dd/MM/yyyy HH:mm:ss") + @"',"
                        + @"text: 'Title: " + itemRef.Note + @"<br />";

                    if (!string.IsNullOrEmpty(item.Note))
                        result += @"Note: " + item.Note;

                    result += @"',DoctorId: '" + item.DoctorId + @"',"
                     + @"RosterTypeId: '" + item.DoctorId + @"',"
                     + @"note: '" + item.Note + @"',";

                    if (item.StartTime <= DateTime.Now)
                    {
                        result += @"color: '" + ServiceFacade.SettingsHelper.CompleteColor + @"',";
                        result += @"readonly: true,";
                    }
                    else
                    {
                        result += @"color: '" + ServiceFacade.SettingsHelper.UncompleteColor + @"',";
                    }
                    result += @"isnew: 'false'},";
                }
            }
            if (result.Length > 1)
            {
                result = @"[{ 'result': 'true', 'message': '', 'data': "
                    + "[" + result.Substring(0, result.Length - 1) + "]"
                    + @" }]";
            }
            else
            {
                result = @"[{ 'result': 'true', 'message': '' }]";
            }
        }
        catch (Exception ex)
        {
            //Write log cho nay

            result = @"[{ 'result': 'false', 'message': '" + ex.Message + "', 'data': '[]' }]";
            return result;
        }

        return result;
    }
}