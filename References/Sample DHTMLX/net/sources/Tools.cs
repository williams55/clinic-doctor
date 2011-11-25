using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Class container for helper methods
    /// </summary>
    public static class Tools
    {
        private static string _DateFormat = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// Gets or Sets date format used for communication with database engine
        /// </summary>
        public static string DateFormat
        {
            get
            {
                return Tools._DateFormat;
            }
            set
            {
                Tools._DateFormat = value;
            }
        }


        /// <summary>
        /// Joins elements of collection into single string using separator provided
        /// </summary>
        /// <param name="Collection">Collection of elements of any type</param>
        /// <param name="Separator">Elements separator</param>
        /// <returns>String representation of collection elements</returns>
        public static string Join(IEnumerable Collection, string Separator)
        {
            if (Collection == null)
#if !NO_LOG
                return "null";
#else
                throw new ArgumentNullException("Collection");
#endif
                return string.Join(Separator, Collection.Cast<object>().Select(o => o.ToString()).ToArray());
        }

        /// <summary>
        /// Converts into string and escapes special characters in the Value to be included into SQL query
        /// </summary>
        /// <param name="Value">Object to be converted and escaped</param>
        /// <returns>SQL-valid string representation of value provided</returns>
        public static string EscapeQueryValue(object Value)
        {
            return Convert.ToString(Value).Replace("'", "''");
        }

        /// <summary>
        /// Converts row value to format compatible with dhtmlx components
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>String representation of object</returns>
        public static string ConvertToString(object value)
        {
            if (value == null || value is DBNull)
                return "";
            Type valType = value.GetType();
            if (typeof(bool).IsAssignableFrom(valType) || typeof(bool?).IsAssignableFrom(valType))
                return ((bool)value) ? "1" : "0";
            else if (typeof(DateTime).IsAssignableFrom(valType) || typeof(DateTime?).IsAssignableFrom(valType))
                return ((DateTime)value).ToString(Tools.DateFormat);
            return Convert.ToString(value);
        }
    }
}
