using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AppointmentBusiness.Util
{
    /// <summary>
    /// Provides support methods for logging operations.
    /// </summary>
    /// <history>
    /// oskeola@gmail.com - 20121009 - Add
    /// </history>	
    public static partial class GlobalUtilities
    {
        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods


        public static DataSet ReadLog(object showObject, ExcuteDataSet excuteDataSetMethod)
        {
            var tableColumnsProperty = showObject.GetType().GetProperty("TableColumns");
            var tableNameProperty = showObject.GetType().GetProperty("TableName");
            var tableName = (string)tableNameProperty.GetValue(showObject, null);
            var columns = (string[])tableColumnsProperty.GetValue(showObject, null);
            var sql_dataset = string.Format("SELECT * FROM {0}_Log WHERE ", tableName);
            foreach (var column in columns)
            {
                var mi = showObject.GetType().GetMember(column);
                if (mi.Length > 0)
                {
                    var myAt =
                        (DataObjectFieldAttribute)
                        Attribute.GetCustomAttribute(mi[0], typeof(DataObjectFieldAttribute));
                    if (myAt != null && myAt.PrimaryKey)
                    {
                        PropertyInfo propertyInfo = showObject.GetType().GetProperty(column);
                        var value = propertyInfo.GetValue(showObject, null);
                        sql_dataset += string.Format("[{0}]=", column);
                        if (IsNumeric(propertyInfo.PropertyType))
                            sql_dataset += value + " AND ";
                        else
                            sql_dataset += "'" + value + "' AND ";
                    }
                }
            }
            sql_dataset = sql_dataset.Remove(sql_dataset.Length - 5);
            sql_dataset += " ORDER BY UpdateDate DESC";
            var dataSet = excuteDataSetMethod(CommandType.Text, sql_dataset);
            return dataSet;
        }

        /// <summary>
        /// Write log changes between two version of an oject before and after modify.
        /// </summary>
        /// <param name="originalObject">The original object to compare.</param>
        /// <param name="modifiedObject">The modified object to compare. If null => create new</param>
        /// <param name="user">The user name was made changes.</param>
        /// <param name="executeDataSetMethods">The methods to execute Dataset </param>
        public static void WriteLog(object originalObject, object modifiedObject, string user, ExcuteDataSet executeDataSetMethods)
        {
            var stringBuilder = new StringBuilder();

            // If create object
            if (modifiedObject == null)
            {
                stringBuilder.AppendLine(String.Format("Create new"));
            }
            else
            {
                // if not changes between two version
                if (ReferenceEquals(originalObject, modifiedObject))
                {
                    // do nothing
                    return;
                }

                // get all infos of versions
                var originalInfos = originalObject.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => Attribute.GetCustomAttribute(p, typeof(DescriptionAttribute)) != null)
                    .ToList();
                var modifiedInfos = modifiedObject.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => Attribute.GetCustomAttribute(p, typeof(DescriptionAttribute)) != null)
                    .ToList();

                originalInfos.Sort((info, propertyInfo) => String.CompareOrdinal(info.Name, propertyInfo.Name));
                modifiedInfos.Sort((info, propertyInfo) => String.CompareOrdinal(info.Name, propertyInfo.Name));

                // with each property
                for (var i = 0; i < originalInfos.Count; i++)
                {
                    // get value of property
                    var originalValue = originalInfos[i].GetValue(originalObject, null);
                    var modifiedValue = modifiedInfos[i].GetValue(modifiedObject, null);

                    // if current property is not changes
                    if (AreValuesEqual(originalValue, modifiedValue))
                    {
                        // go next property
                        continue;
                    }
                    // append change to log
                    stringBuilder.AppendLine(String.Format("Change {0} from \"{1}\" to \"{2}\"",
                                                           originalInfos[i].Name,
                                                           originalValue,
                                                           modifiedValue));
                }
            }

            //Create log table if not exist
            if (!string.IsNullOrEmpty(stringBuilder.ToString()))
            {
                var tableColumnsProperty = originalObject.GetType().GetProperty("TableColumns");
                var tableNameProperty = originalObject.GetType().GetProperty("TableName");
                var tableName = (string)tableNameProperty.GetValue(originalObject, null);
                var columns = (string[])tableColumnsProperty.GetValue(originalObject, null);
                var sql = string.Format("IF OBJECT_ID('[dbo].[{0}]','U') IS NULL ", tableName + "_Log");
                sql += string.Format("CREATE TABLE [dbo].[{0}]([LogId] [bigint] IDENTITY(1,1) NOT NULL, ",
                                     tableName + "_Log");
                //Insert into log table
                var insertSQL = string.Format("INSERT INTO [dbo].[{0}](", tableName + "_Log");
                var valueSQL = "";
                foreach (var column in columns)
                {
                    var mi = originalObject.GetType().GetMember(column);
                    if (mi.Length > 0)
                    {
                        var myAt =
                            (DataObjectFieldAttribute)
                            Attribute.GetCustomAttribute(mi[0], typeof(DataObjectFieldAttribute));
                        if (myAt!= null && myAt.PrimaryKey)
                        {
                            var sql_dataset =
                                string.Format(
                                    "SELECT '[' +COLUMN_NAME+'] ['+  DATA_TYPE +  '] ('+ CAST(ISNULL(CHARACTER_MAXIMUM_LENGTH,'')  AS NVARCHAR)+' ) ' + CASE IS_NULLABLE WHEN 'YES' THEN 'NULL' ELSE 'NOT NULL, ' END  FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME='{0}'  AND TABLE_NAME='{1}'",
                                    column, tableName);
                            var dataSet = executeDataSetMethods(CommandType.Text, sql_dataset);
                            sql += dataSet.Tables[0].Rows[0][0].ToString();
                            insertSQL += "[" + column + "],";
                            PropertyInfo propertyInfo = originalObject.GetType().GetProperty(column);
                            var value = propertyInfo.GetValue(originalObject, null);
                            if (IsNumeric(propertyInfo.PropertyType))
                                valueSQL += value + ",";
                            else
                                valueSQL += "'" + value + "',";
                        }
                    }
                }
                insertSQL += "[UpdateUser],[UpdateDate],[LogMessage]) VALUES(";
                sql +=
                    string.Format(
                        "[UpdateUser] [nvarchar](50) NULL,[UpdateDate] [datetime] NOT NULL,[LogMessage] [nvarchar](250) NULL, CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ",
                        tableName + "_Log");
                sql +=
                    "([LogId] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY] ";
                valueSQL += string.Format("'{0}','{1}','{2}')", user, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                                          stringBuilder);
                executeDataSetMethods(CommandType.Text, sql);
                executeDataSetMethods(CommandType.Text, insertSQL + valueSQL);
            }
            //// Write log to text file
            //Stream str = new FileStream("\\" + "logger.txt",
            //                            FileMode.Append,
            //                            FileAccess.Write,
            //                            FileShare.ReadWrite);
            //var data = Encoding.UTF8.GetBytes(stringBuilder.ToString());
            //str.Write(data, 0, data.Length);
            //str.Close();
        }

        public static bool IsNumeric(Type type)
        {
            if (type == null)
                return false;

            TypeCode typeCode = Type.GetTypeCode(type);

            switch (typeCode)
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
            }
            return false;
        }

        public delegate DataSet ExcuteDataSet(CommandType type, string sql);

        #endregion

        //**************************************************
        //
        // Private methods
        //
        //**************************************************

        #region Private methods

        /// <summary>
        /// Compares two values and returns if they are the same.
        /// </summary>
        /// <param name="valueA">The first value to compare.</param>
        /// <param name="valueB">The second value to compare.</param>
        /// <returns><c>true</c> if both values match, otherwise <c>false</c>.</returns>
        private static bool AreValuesEqual(object valueA, object valueB)
        {
            var selfValueComparer = valueA as IComparable;

            if (valueA == null && valueB != null)
            {
                if (string.IsNullOrEmpty(valueB.ToString()))
                    return true;
                return false;
            }
            if (valueA != null && valueB == null)
            {
                if (string.IsNullOrEmpty(valueA.ToString()))
                    return true;
                return false;
            }// one of the values is null
            if (selfValueComparer != null && selfValueComparer.CompareTo(valueB) != 0)
                return false; // the comparison using IComparable failed
            if (!Equals(valueA, valueB))
                return false; // the comparison using Equals failed
            return true; // match

        }

        #endregion
    }
}
