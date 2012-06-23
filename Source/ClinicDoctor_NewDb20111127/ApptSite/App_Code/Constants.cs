using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// Summary description for Constants
/// </summary>
public class Constants
{
    // Weekdays
    public const string Weekdays = "[{ 'key': 'Sun', 'label': 'Sunday' }," +
                "{ 'key': 'Mon', 'label': 'Monday' }," +
                "{ 'key': 'Tue', 'label': 'Tuesday' }," +
                "{ 'key': 'Wed', 'label': 'Wednesday' }," +
                "{ 'key': 'Thu', 'label': 'Thursday' }," +
                "{ 'key': 'Fri', 'label': 'Friday' }," +
                "{ 'key': 'Sat', 'label': 'Saturday' }]";
}

public class DoctorAppointmentStatus
{
    private static Dictionary<string, string> _instance;
    protected DoctorAppointmentStatus()
    {

    }

    public static Dictionary<string, string> Instance()
    {
        if (_instance == null)
        {
            _instance = new Dictionary<string, string>();
            _instance.Add("0", "Available");
            _instance.Add("1", "Busy");
            _instance.Add("2", "No Roster");
            _instance.Add("3", "Công Tác");
        }
        return _instance;
    }
}

public class RoomAppointmentStatus
{
    private static Dictionary<string, string> _instance;
    protected RoomAppointmentStatus()
    {

    }

    public static Dictionary<string, string> Instance()
    {
        if (_instance == null)
        {
            _instance = new Dictionary<string, string>();
            _instance.Add("0", "Available");
            _instance.Add("1", "Using");
            _instance.Add("2", "Fixing");
            _instance.Add("3", "Not Available");
        }
        return _instance;
    }
}
