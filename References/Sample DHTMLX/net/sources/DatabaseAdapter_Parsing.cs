using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Microsoft SQL Server 2000-2008 compatible implementation of idhtmlxDatabaseAdapter interface
    /// </summary>
    public partial class MSSQLAdapter
    {
        /// <summary>
        /// Parses SQL query into format used by connectors and DataRequest
        /// </summary>
        /// <param name="sqlQuery">Query to parse</param>
        /// <param name="TableName">TableName extracted from SQL query</param>
        /// <param name="RequestedFields">Column names extracted from SQL query</param>
        /// <param name="Rules">Collection of WHERE statements extracted from SQL query</param>
        /// <param name="OrderBy">Collection of ORDER BY statements extracted from SQL query</param>
        /// <param name="StartIndex">Row index, starting form which query result will be returned</param>
        /// <param name="Count">Number of rows to be returned from query result</param>
        public void ParseSqlQuery(string sqlQuery, out string TableName, out IEnumerable<Field> RequestedFields, out List<Rule> Rules, out List<OrderByStatement> OrderBy, out int StartIndex, out int Count)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Parsing query: " + sqlQuery);
#endif
            #endregion
            //init variables
            TableName = null;
            RequestedFields = new dhtmlxFieldsCollection();
            Rules = new List<Rule>();
            OrderBy = new List<OrderByStatement>();
            StartIndex = 0;
            Count = Int32.MaxValue;

            //regexp expressions for target query elements extraction
            string removeEverythingButFieldsRegex = "(^SELECT)|([ \n]+((TOP)|(LIMIT))[\n ,0-9]*)|((FROM).*$)";
            string removeEverythingButOrderRegex = "^.*(ORDER BY).";
            string removeEverythingButWhereRegex = "((SELECT).*(WHERE))|(((ORDER BY)|(GROUP BY)|(HAVING)).*$)";
            string removeEverythingButTableRegex = "(^.*(FROM))|(((WHERE)|(ORDER BY)|(GROUP BY)|(HAVING)).*$)";

            //parse fields
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Parsing fields by: " + removeEverythingButFieldsRegex);
#endif
            #endregion
            string fieldsOnly = Regex.Replace(sqlQuery, removeEverythingButFieldsRegex, "", RegexOptions.IgnoreCase);
            RequestedFields = this.ParseFields(fieldsOnly);
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Fields are: " + Tools.Join(RequestedFields, ","));
#endif
            #endregion
            //parse table name
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Parsing TableName by: " + removeEverythingButTableRegex);
#endif
            #endregion
            TableName = Regex.Replace(sqlQuery, removeEverythingButTableRegex, "", RegexOptions.IgnoreCase);
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Table name is: " + TableName);
#endif
            #endregion
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Parsing whereStatements by: " + removeEverythingButWhereRegex);
#endif
            #endregion
            string whereStatements = sqlQuery.IndexOf(" WHERE ", StringComparison.OrdinalIgnoreCase) == -1 ? "" : Regex.Replace(sqlQuery, removeEverythingButWhereRegex, "", RegexOptions.IgnoreCase);
            Rules = this.ParseRules(whereStatements);
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Where statements are: " + Tools.Join(Rules, " AND "));
#endif
            #endregion
            
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Parsing OrderBy statements by " + removeEverythingButOrderRegex);
#endif
            #endregion
            string orderByStatements = sqlQuery.IndexOf(" ORDER ", StringComparison.OrdinalIgnoreCase) == -1 ? "" : Regex.Replace(sqlQuery, removeEverythingButOrderRegex, "", RegexOptions.IgnoreCase);
            OrderBy = this.ParseOrders(orderByStatements);
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Order is: " + Tools.Join(OrderBy, ", "));
#endif
            #endregion
        }

        /// <summary>
        /// Converts columns/fields stored in SQL query into Field objects collection
        /// </summary>
        /// <param name="Fields">Database engine specific representation of columns (e.g. "CustomerName, CustomerID, CreatedDate as RegisterDate")</param>
        /// <returns>Collection of Field objects that represent given fields list</returns>
        protected virtual IEnumerable<Field> ParseFields(string Fields)
        {
            string[] FieldsArray = Regex.Split(Fields, ",", RegexOptions.IgnoreCase);
            dhtmlxFieldsCollection ResultFieldsCollection = new dhtmlxFieldsCollection();
            foreach (string Field in FieldsArray)
            {
                if (Field.Trim() != String.Empty)
                    ResultFieldsCollection.Add(this.ParseField(Field));
            }
            return ResultFieldsCollection;
        }

        /// <summary>
        /// Converts SQL representation of field into Field object
        /// </summary>
        /// <param name="FieldExpression">SQL representation of field(column)</param>
        /// <returns>Field object representation of expression provided</returns>
        public virtual Field ParseField(string FieldExpression)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Extracting field from: " + FieldExpression);
#endif
            #endregion
            string[] FieldComponents = Regex.Split(FieldExpression, "[\\s]+as[\\s]+", RegexOptions.IgnoreCase);
            
            //Expression given is plain table column
            if (this.IsFieldName(FieldComponents[0]))
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "It's a table field");
#endif
                #endregion
                if (FieldComponents.Length > 1)
                    return new TableField(FieldComponents[0].Trim(), FieldComponents[FieldComponents.Length - 1].Trim());
                else
                    return new TableField(FieldComponents[0].Trim());
            }
            else
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "It's an expression");
#endif
                #endregion
                // expression given is sql expression to be executed (e.g. "(SELECT 'DUMMY VALUE') AS SomeField")
                if (FieldComponents.Length > 1)
                    return new ExpressionField(FieldComponents[0].Trim(), FieldComponents[FieldComponents.Length - 1].Trim());
                else
                    return new ExpressionField(FieldComponents[0].Trim());
            }
        }

        /// <summary>
        /// Checkes if field name provided is column name or compound sql expression (subquery or anything else)
        /// </summary>
        /// <param name="FieldName">Expression to check</param>
        /// <returns>True if expression provided is valid column name</returns>
        public virtual bool IsFieldName(string FieldName)
        {
            return Regex.IsMatch(FieldName.Trim(), "^[a-zA-Z0-9_\\[\\]]*$");
        }

        /// <summary>
        /// Converts sql representation of WHERE statements into FieldRule objects collection
        /// </summary>
        /// <param name="Rules">T-SQL specific representation of WHERE statements</param>
        /// <returns>Collection of FieldRule objects that represent WHERE statements provided</returns>
        protected virtual List<Rule> ParseRules(string Rules)
        {
            if (!string.IsNullOrEmpty(Rules) && Rules.Trim() != string.Empty)
                return new List<Rule>() { new ExpressionRule(Rules.Trim()) };
            else
                return new List<Rule>();
        }

        /// <summary>
        /// Converts sql representation of ORDER BY statements into OrderByField objects collection
        /// </summary>
        /// <param name="OrderBy">T-SQL specific representation of ORDER BY statements</param>
        /// <returns>Collection of OrderByField objects that represent ORDER BY statements provided</returns>
        protected virtual List<OrderByStatement> ParseOrders(string OrderBy)
        {
            if (!string.IsNullOrEmpty(OrderBy) && OrderBy.Trim() != string.Empty)
                return new List<OrderByStatement>() { new OrderByExpression(OrderBy.Trim()) };
            else
                return new List<OrderByStatement>();
        }
    }
}
