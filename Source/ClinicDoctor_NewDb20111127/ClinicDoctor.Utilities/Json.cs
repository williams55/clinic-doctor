using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ClinicDoctor.Utilities
{
    public static class Json
    {
        #region "Convert object to Json"
        // Convert from an object to Json
        public static string ToJson(Object obj)
        {
            // Declare variables
            string result = string.Empty;

            // Get type of object
            // it returns name of class
            Type myType = obj.GetType();

            // Get all properties in object
            PropertyInfo[] infos = myType.GetProperties();

            // Add string
            for (int i = 0; i < infos.Length; i++)
            {
                result += @"""" + infos[i].Name + @""" : """
                    + ((infos[i].GetValue(obj, null) == null) ? "" : infos[i].GetValue(obj, null).ToString().Replace("\\", "\\\\").Replace("\"", "\\\""))
                    + @""",";
            }
            result = "{" + result.Substring(0, result.Length - 1) + "}";

            return result;
        }
        #endregion
    }
}
