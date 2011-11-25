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
        Staff obj = DataRepository.StaffProvider.GetByUserNameIsDisabled(HttpContext.Current.User.Identity.Name, false);
        if (obj == null)
            return string.Empty;

        //return obj.Address;
        //return @"[" +
        //        @"{ id:""1269366989368"" , text:""New event"" , start_date:""2010-01-04 00:00"" , end_date:""9999-02-01 00:00"" , rec_type:""week_1___2#no""" +
        //        @", event_length:""300"" , event_pid:"""" }" +
        //        @",{ id:""1269366989372"" , text:""New event1"" , start_date:""2010-01-14 00:00"" , end_date:""2010-01-14 00:05"" , rec_type:"""" , " +
        //        @"event_length:""1263247200"" , event_pid:""1269366989368"" }]";
        return "[{ 'key': 'Sun', 'label': 'Sunday' }," +
                "{ 'key': 'Mon', 'label': 'Monday' }," +
                "{ 'key': 'Tue', 'label': 'Tuesday' }," +
                "{ 'key': 'Wed', 'label': 'Wednesday' }," +
                "{ 'key': 'Thu', 'label': 'Thursday' }," +
                "{ 'key': 'Fri', 'label': 'Friday' }," +
                "{ 'key': 'Sat', 'label': 'Saturday' }]";

    }
}