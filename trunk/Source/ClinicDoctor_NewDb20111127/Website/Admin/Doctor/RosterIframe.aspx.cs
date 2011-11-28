using System;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web;
using ClinicDoctor.Data;
using ClinicDoctor.Entities;

public partial class Admin_Doctor_RosterIframe : System.Web.UI.Page
{
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetData()
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
    public static string SaveEvent(string RosterType, string StartTime, string EndTime, string Note, string RepeatRoster, string Weekday)
    {
        string username = EntitiesUtilities.GetAuthName();

        string result = username + "-" + RosterType + "-" + StartTime + "-" + EndTime + "-" + Note + "-" + RepeatRoster + "-" + Weekday;

        return result;
    }
}