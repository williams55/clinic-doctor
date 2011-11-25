using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Provides sets of properties and methods supported by particular Database engine
    /// </summary>
    public interface IdhtmlxDatabaseAdapter
    {
        /// <summary>
        /// Gets or Sets connection string
        /// </summary>
        string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// Executes query against table and returns number of records satisfying this query
        /// </summary>
        /// <param name="TableName">Table name to run query against to</param>
        /// <param name="Rules">Collection of query rules</param>
        /// <returns>Number or records that satisfy select query provided</returns>
        int ExecuteGetCountQuery(string TableName, List<Rule> Rules);

        /// <summary>
        /// Executes select table
        /// </summary>
        /// <param name="TableName">Table name to run query against to</param>
        /// <param name="RequestedFields">Fields collection to return</param>
        /// <param name="Rules">Collection of rules to apply to a query</param>
        /// <param name="OrderBy">OrderBy statements</param>
        /// <param name="StartIndex">First row index to include into result</param>
        /// <param name="Count">Total amount of records to include into result (e.g. 0, 1, 2, ... StartIndex, StartIndex + 1, ... StartIndex + Count, ...)</param>
        /// <returns>DataTable object that contains result of a query</returns>
        DataTable ExecuteSelectQuery(string TableName, IEnumerable<Field> RequestedFields, List<Rule> Rules, List<OrderByStatement> OrderBy, int StartIndex, int Count);
        
        /// <summary>
        /// Executes delete query
        /// </summary>
        /// <param name="TableName">Table name to delete records from</param>
        /// <param name="PrimaryKeyField">Column name to delete records by</param>
        /// <param name="PrimaryKeyValue">Target column value to delete records by</param>
        void ExecuteDeleteQuery(string TableName, Field PrimaryKeyField, object PrimaryKeyValue);
        
        /// <summary>
        /// Executes update query
        /// </summary>
        /// <param name="TableName">TableName that contains record to be updated</param>
        /// <param name="NewColumnValues">Collection of values to be updated in target query</param>
        /// <param name="PrimaryKeyField">Primary key field to search target record by</param>
        /// <param name="PrimaryKeyValue">Primary key field value </param>
        void ExecuteUpdateQuery(string TableName, Dictionary<Field, string> NewColumnValues, Field PrimaryKeyField, object PrimaryKeyValue);
        
        /// <summary>
        /// Executed insert query
        /// </summary>
        /// <param name="TableName">Table name to insert record in</param>
        /// <param name="ColumnValues">Column values to insert</param>
        /// <param name="PrimaryKeyField">Primary Key column</param>
        /// <param name="PrimaryKeyValue">Primary key column value</param>
        /// <returns>ID of inserted record</returns>
        object ExecuteInsertQuery(string TableName, Dictionary<Field, string> ColumnValues, Field PrimaryKeyField, object PrimaryKeyValue);
    
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
        void ParseSqlQuery(string sqlQuery, out string TableName, out IEnumerable<Field> RequestedFields, out List<Rule> Rules, out List<OrderByStatement> OrderBy, out int StartIndex, out int Count);
        
        /// <summary>
        /// Converts SQL representation of field into Field object
        /// </summary>
        /// <param name="FieldExpression">SQL representation of field(column)</param>
        /// <returns>Field object representation of expression provided</returns>
        Field ParseField(string FieldExpression);

        /// <summary>
        /// Checkes if field name provided is column name or compound sql expression (subquery or anything else)
        /// </summary>
        /// <param name="FieldName">Expression to check</param>
        /// <returns>True if expression provided is valid column name</returns>
        bool IsFieldName(string FieldName);

        /// <summary>
        /// Gets value indicating whether current database adapter implementation supports transactions
        /// </summary>
        bool SupportsTransactions
        {
            get;
        }
        
        /// <summary>
        /// Executes query against database providing no result
        /// </summary>
        /// <param name="Query">Query to execute</param>
        void ExecuteNonQuery(string Query);

        /// <summary>
        /// Executes query that returns single value
        /// </summary>
        /// <param name="Query">Query to execute</param>
        /// <returns>Value returned by query</returns>
        object ExecuteScalar(string Query);

        /// <summary>
        /// Executes query that returns collection of rows
        /// </summary>
        /// <param name="Query">Query to execute</param>
        /// <returns>DataTable object that contains query result</returns>
        DataTable ExecuteSelectQuery(string Query);

        /// <summary>
        /// Starts transaction
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commits transaction
        /// </summary>
        void CommitTransaction();
        
        /// <summary>
        /// Rollbacks transaction
        /// </summary>
        void RollbackTransaction();
    }
}
