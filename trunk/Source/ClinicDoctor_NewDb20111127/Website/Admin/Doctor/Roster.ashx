<%@ WebHandler Language="C#" Class="Roster" %>

using System;
using System.Web;
using System.Web.Script.Services;
using ClinicDoctor.Data;
using ClinicDoctor.Entities;
using System.Web.Services;

public class Roster : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

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