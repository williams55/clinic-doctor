using System;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web;
using ClinicDoctor.Data;
using ClinicDoctor.Entities;
using ClinicDoctor.Settings.BusinessLayer;

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
        result = "[" + result.Substring(0, result.Length - 1) + "]";

        return result;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string SaveEvent(string RosterType, string StartTime, string EndTime, string Note, string RepeatRoster, string Weekday)
    {
        string username = EntitiesUtilities.GetAuthName();

        string result = username + "-" + RosterType + "-" + StartTime + "-" + EndTime + "-" + Note + "-" + RepeatRoster + "-" + Weekday;

        return result;
    }
}