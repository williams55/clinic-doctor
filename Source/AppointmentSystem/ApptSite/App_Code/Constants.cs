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
