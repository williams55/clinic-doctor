using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FCommon.Util;

/// <summary>
/// Summary description for AppointmentStatus
/// </summary>
public class AppointmentStatus : Constants
{
    public static AppointmentStatus Instant()
    {
        return new AppointmentStatus();
    }

    public static ConstantsKeyValue New = new ConstantsKeyValue(0, "New");
    public static ConstantsKeyValue Processing = new ConstantsKeyValue(1, "Processing");
    public static ConstantsKeyValue Cancel = new ConstantsKeyValue(2, "Cancel");
    public static ConstantsKeyValue Complete = new ConstantsKeyValue(3, "Complete");
}

public class AppointmentColor : Constants
{
    public static AppointmentStatus Instant()
    {
        return new AppointmentStatus();
    }

    public static ConstantsKeyValue New = new ConstantsKeyValue(0, "#CCFF66");
    public static ConstantsKeyValue Processing = new ConstantsKeyValue(1, "#CCFF66");
    public static ConstantsKeyValue Cancel = new ConstantsKeyValue(2, "#999966");
    public static ConstantsKeyValue Complete = new ConstantsKeyValue(3, "#3366CC");
}
