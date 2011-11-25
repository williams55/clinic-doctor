using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace dhtmlxConnectors
{
    //TODO: Make possible to specify render columns in grids
    //TODO: Create engine dependent database adapter
    //TODO: Check samples with different datatype columns (float, bools, datetime, escape chars strings etc.)
    //TODO: MySQL Support
    //TODO: Oracle support
    public partial class MSSQLAdapter: IdhtmlxDatabaseAdapter
    {
        /// <summary>
        /// Gets or Sets connection string
        /// </summary>
        public virtual string ConnectionString
        {
            get;
            set;
        }
        /// <summary>
        /// Creates new instance of MSSQLAdapter
        /// </summary>
        public MSSQLAdapter()
        {
        }

        /// <summary>
        /// Creates new instance of MSSQLAdapter
        /// </summary>
        /// <param name="ConnectionString">ConnectionString to initialize adapter by</param>
        public MSSQLAdapter(string ConnectionString)
            : this()
        {
            this.ConnectionString = ConnectionString;
        }

        #region Methods that generate platform-dependent queries for all database operations

        /// <summary>
        /// Creates T-SQL Delete query based on parameters provided
        /// </summary>
        /// <param name="TableName">Table name to delete record from</param>
        /// <param name="PrimaryKeyField">PrimaryKey field</param>
        /// <param name="PrimaryKeyValue">PrimaryKey value</param>
        /// <returns>T-SQL query for deleting record with parameters given</returns>
        protected virtual string CreateDeleteQuery(string TableName, Field PrimaryKeyField, object PrimaryKeyValue)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, string.Format("Creating delete query for TableName: {0}, PrimaryKeyField: {1}, PrimaryKeyValue: {2}", TableName, PrimaryKeyField, PrimaryKeyValue));
#endif
            #endregion
            string result = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", TableName, PrimaryKeyField.InternalName, Tools.EscapeQueryValue(PrimaryKeyValue));
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Delete query: " + result);
#endif
            #endregion
            return result;
        }

        /// <summary>
        /// Creates T-SQL Select query based on parameters provided
        /// </summary>
        /// <param name="TableName">Table name to create query for</param>
        /// <param name="RequestedFields">Fields to include into result</param>
        /// <param name="Rules">Rules to apply to query</param>
        /// <param name="OrderBy">Order statements</param>
        /// <param name="StartIndex">Start index to take result rows from</param>
        /// <param name="Count">Number or rows to return</param>
        /// <returns>Ready-to-execute T-SQL query</returns>
        protected virtual string CreateSelectQuery(string TableName, IEnumerable<Field> RequestedFields, List<Rule> Rules, List<OrderByStatement> OrderBy, int StartIndex, int Count)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, string.Format("Creating SelectQuery from TableName: {0}, Fields: {1}, Rules: {2}, OrderBy: {3}, StartIndex: {4}, Count: {5}", TableName, Tools.Join(RequestedFields, ", "), Tools.Join(Rules, " AND "), Tools.Join(OrderBy, ", "), StartIndex, Count));
#endif
            #endregion
            StringBuilder Query = new StringBuilder();
            Query.Append("SELECT ");
            Query.Append(Tools.Join(RequestedFields, ", "));
            Query.Append(" FROM ").Append(TableName);

            if (Rules != null)
            {
                string WhereClause = Tools.Join(Rules, " AND ");
                if (!string.IsNullOrEmpty(WhereClause))
                    Query.Append(" WHERE ").Append(WhereClause);

            }
            if (OrderBy != null)
            {
                string OrderByStatements = Tools.Join(OrderBy, ", ");
                if (!string.IsNullOrEmpty(OrderByStatements))
                    Query.Append(" ORDER BY ").Append(OrderByStatements);

            }
            string result = "";
            if (Count > 0 && (Count != Int32.MaxValue || StartIndex > 0))
                result = string.Format("declare @handle int,@rows int;exec sp_cursoropen @handle OUT, '{0}',1, 1, @rows OUT;select @handle, @rows;exec sp_cursorfetch @handle,16,{1},{2};exec sp_cursorclose @handle;", Tools.EscapeQueryValue(Query.ToString()), StartIndex + 1, Count);
            else
                result = Query.ToString();
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Select query: " + result);
#endif
            #endregion
            return result;
        }

        /// <summary>
        /// Created T-SQL Update query
        /// </summary>
        /// <param name="TableName">TableName to update records in</param>
        /// <param name="NewColumnValues">New columns values</param>
        /// <param name="PrimaryKeyField">PrimaryKey field</param>
        /// <param name="PrimaryKeyValue">PrimaryKey value</param>
        /// <returns>T-SQL Update query</returns>
        protected virtual string CreateUpdateQuery(string TableName, Dictionary<Field, string> NewColumnValues, Field PrimaryKeyField, object PrimaryKeyValue)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, string.Format("Creating UpdateQuery from TableName: {0}, Fields-Values: {1}, PrimaryKeyField: {2}, PrimaryKeyValue: {3}", TableName, Tools.Join(NewColumnValues.Select(item => item.Key.ToString() + " = " + item.Value), ", "), PrimaryKeyField, PrimaryKeyValue));
#endif
            #endregion
            string updateQuery = string.Format("UPDATE {0} SET {{0}} WHERE {1} = '{2}'", TableName, PrimaryKeyField.InternalName, Tools.EscapeQueryValue(PrimaryKeyValue));
            List<string> updateFieldStatements = new List<string>();
            foreach (Field updateField in NewColumnValues.Keys)
            {
                string updateFieldStatement = string.Format("{0} = '{1}'", updateField.InternalName, Tools.EscapeQueryValue(NewColumnValues[updateField]));
                updateFieldStatements.Add(updateFieldStatement);
            }
            string result = string.Format(updateQuery, string.Join(", ", updateFieldStatements.ToArray()));
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Update query: " + result);
#endif
            #endregion
            return result;
        }

        /// <summary>
        /// Creates T-SQL Insert query
        /// </summary>
        /// <param name="TableName">Table name to insert record into</param>
        /// <param name="ColumnValues">Values to insert</param>
        /// <returns>T-SQL Insert query</returns>
        protected virtual string CreateInsertQuery(string TableName, Dictionary<Field, string> ColumnValues)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, string.Format("Creating insert query from TableName: {0}, Fields-Values: {1}", TableName, Tools.Join(ColumnValues.Select(item => item.Key.ToString() + " = " + item.Value), ", ")));
#endif
            #endregion
            string insertQuery = string.Format("INSERT INTO {0} ({{0}}) VALUES ({{1}}); SELECT SCOPE_IDENTITY();", TableName);
            List<string> insertColumns = new List<string>();
            List<string> insertValues = new List<string>();

            foreach (Field insertField in ColumnValues.Keys)
            {
                insertColumns.Add(insertField.InternalName);
                insertValues.Add("'" + Tools.EscapeQueryValue(ColumnValues[insertField]) + "'");
            }
            string result = string.Format(insertQuery, string.Join(", ", insertColumns.ToArray()), string.Join(", ", insertValues.ToArray()));
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Insert query: " + result);
#endif
            #endregion
            return result;
        } 
        #endregion

        #region Methods that execute dataConnector queries (platform independent)

        /// <summary>
        /// Executes query against table and returns number of records satisfying this query
        /// </summary>
        /// <param name="TableName">Table name to run query against to</param>
        /// <param name="Rules">Collection of query rules</param>
        /// <returns>Number or records that satisfy select query provided</returns>
        public virtual int ExecuteGetCountQuery(string TableName, List<Rule> Rules)
        {
            dhtmlxFieldsCollection newRequestedFields = new dhtmlxFieldsCollection();
            newRequestedFields.Add(new ExpressionField("1", "dummyValue"));
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, string.Format("ExecuteGetCountQuery: TableName: {0}, RequesteFields: {1}, Rules: {2}", TableName, Tools.Join(newRequestedFields, ","), Tools.Join(Rules, ",")));
#endif
            #endregion
            string selectQuery = this.CreateSelectQuery(TableName, newRequestedFields, Rules, null, 0, 0);
            int count = Convert.ToInt32(this.ExecuteScalar("SELECT COUNT(*) FROM (" + selectQuery + ") TBL"));
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Result: " + count.ToString());
#endif
            #endregion
            return count;
        }

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
        public virtual DataTable ExecuteSelectQuery(string TableName, IEnumerable<Field> RequestedFields, List<Rule> Rules, List<OrderByStatement> OrderBy, int StartIndex, int Count)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Entering: ExecuteSelectQuery");
#endif
            #endregion
            string selectQuery = this.CreateSelectQuery(TableName, RequestedFields, Rules, OrderBy, StartIndex, Count);
            var result = this.ExecuteSelectQuery(selectQuery);
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Exiting: ExecuteSelectQuery");
#endif
            #endregion
            return result;
        }

        /// <summary>
        /// Executes delete query
        /// </summary>
        /// <param name="TableName">Table name to delete records from</param>
        /// <param name="PrimaryKeyField">Column name to delete records by</param>
        /// <param name="PrimaryKeyValue">Target column value to delete records by</param>
        public virtual void ExecuteDeleteQuery(string TableName, Field PrimaryKeyField, object PrimaryKeyValue)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Entering: ExecuteDeleteQuery");
#endif
            #endregion
            if (PrimaryKeyField == null)
                throw new dhtmlxException("Data row cannot be deleted because there were no primary key provided");
            try
            {
                this.ExecuteNonQuery(this.CreateDeleteQuery(TableName, PrimaryKeyField, PrimaryKeyValue));
            }
            catch (Exception ex)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Exception cought: " + ex.Message);
#endif
                #endregion
                throw new dhtmlxException("Delete operation failed with the following error: " + ex.Message);
            }
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Leaving: ExecuteDeleteQuery");
#endif
            #endregion
        }

        /// <summary>
        /// Executes update query
        /// </summary>
        /// <param name="TableName">TableName that contains record to be updated</param>
        /// <param name="NewColumnValues">Collection of values to be updated in target query</param>
        /// <param name="PrimaryKeyField">Primary key field to search target record by</param>
        /// <param name="PrimaryKeyValue">Primary key field value </param>
        public virtual void ExecuteUpdateQuery(string TableName, Dictionary<Field, string> NewColumnValues, Field PrimaryKeyField, object PrimaryKeyValue)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Entering: ExecuteUpdateQuery");
#endif
            #endregion
            if (NewColumnValues.Count == 0)
                throw new dhtmlxException("Data row cannot be updated because there were no data specified");

            if (PrimaryKeyField == null)
                throw new dhtmlxException("Data row cannot be updated because there were no primary key provided");
            
            try
            {
                this.ExecuteNonQuery(this.CreateUpdateQuery(TableName, NewColumnValues, PrimaryKeyField, PrimaryKeyValue));
            }
            catch (Exception ex)
            {                
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Exception cought: " + ex.Message);
#endif
                #endregion
                throw new dhtmlxException("Update operation failed with the followind error: " + ex.Message); 
            }
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Leaving: ExecuteUpdateQuery");
#endif
            #endregion
        }

        /// <summary>
        /// Executes insert query
        /// </summary>
        /// <param name="TableName">Table name to insert record in</param>
        /// <param name="ColumnValues">Column values to insert</param>
        /// <param name="PrimaryKeyField">Primary Key column</param>
        /// <param name="PrimaryKeyValue">Primary key column value</param>
        /// <returns>ID of inserted record</returns>
        public object ExecuteInsertQuery(string TableName, Dictionary<Field, string> ColumnValues, Field PrimaryKeyField, object PrimaryKeyValue)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Entering: ExecuteInsertQuery");
#endif
            #endregion
            if (ColumnValues.Count == 0)
                throw new dhtmlxException("Data row cannot be inserted because there were no data specified");

            try
            {
                object result = this.ExecuteScalar(this.CreateInsertQuery(TableName, ColumnValues));
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Result: " + Convert.ToString(result));
#endif
                #endregion
                return result;
            }
            catch (Exception ex)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Exception cought: " + ex.Message);
#endif
                #endregion
                throw new dhtmlxException("Insert operation failed with the followind error: " + ex.Message);
            }
        } 
        #endregion

        #region Phisical platform dependent sqls execution

        /// <summary>
        /// Executes query against database providing no result
        /// </summary>
        /// <param name="Query">Query to execute</param>
        public virtual void ExecuteNonQuery(string Query)
        {
            IDbConnection connection = this.FetchDbConnection();
            if (connection.State != ConnectionState.Open)
                connection.Open();
            
            IDbCommand command = this.CreateCommand(Query, connection);
            command.Transaction = this._ShadowTransaction;
            command.ExecuteNonQuery();
            
            if (this._ShadowTransaction == null)
                connection.Close();
        }

        /// <summary>
        /// Executes query that returns single value
        /// </summary>
        /// <param name="Query">Query to execute</param>
        /// <returns>Value returned by query</returns>
        public virtual object ExecuteScalar(string Query)
        {
            IDbConnection connection = this.FetchDbConnection();
            if (connection.State != ConnectionState.Open)
                connection.Open();
            
            IDbCommand command = this.CreateCommand(Query, connection);
            command.Transaction = this._ShadowTransaction;
            object result = command.ExecuteScalar();
            
            if (this._ShadowTransaction == null)
                connection.Close();
            
            return result;
        }

        /// <summary>
        /// Executes query that returns collection of rows
        /// </summary>
        /// <param name="Query">Query to execute</param>
        /// <returns>DataTable object that contains query result</returns>
        public virtual DataTable ExecuteSelectQuery(string Query)
        {
            IDbConnection connection = this.FetchDbConnection();
            if (connection.State != ConnectionState.Open)
                connection.Open();
            
            IDbCommand command = this.CreateCommand(Query, connection);
            IDbDataAdapter da = this.CreateDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);
                
            DataTable result = ds.Tables[ds.Tables.Count - 1];
            if (result.Columns.Contains("ROWSTAT"))
                result.Columns.Remove("ROWSTAT");
            if (this._ShadowTransaction == null)
                    connection.Close();
            return result;
        }

        /// <summary>
        /// Creates or returns existing IDbConnection object
        /// </summary>
        /// <returns>IDbConnection object</returns>
        private IDbConnection FetchDbConnection()
        {
            if (this._ShadowConnection == null)
                return this.CreateConnection(this.ConnectionString);
            else
                return this._ShadowConnection;
        }

        /// <summary>
        /// Creates initialized opened IDbConnection object
        /// </summary>
        /// <param name="ConnectionString">ConnectionString to initialize connection by</param>
        /// <returns></returns>
        protected IDbConnection CreateConnection(string ConnectionString)
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// Creates IDbCommand object
        /// </summary>
        /// <param name="Query">Query to initialize command with</param>
        /// <param name="Connection">Connection to initialize command with</param>
        /// <returns>IDbCommand object</returns>
        protected IDbCommand CreateCommand(string Query, IDbConnection Connection)
        {
            return new SqlCommand(Query, Connection as SqlConnection);
        }

        /// <summary>
        /// Creates IDbDataAdapter
        /// </summary>
        /// <param name="Command">Command to initialize adapter with</param>
        /// <returns>IDbDataAdapter object</returns>
        protected IDbDataAdapter CreateDataAdapter(IDbCommand Command)
        {
            return new SqlDataAdapter(Command as SqlCommand);
        }

        /// <summary>
        /// Gets value indicating whether current database adapter implementation supports transactions
        /// </summary>
        public bool SupportsTransactions
        {
            get
            {
                return true;
            }
        }

        private IDbConnection _ShadowConnection = null;//cacned connection object
        private IDbTransaction _ShadowTransaction = null;//active transaction

        /// <summary>
        /// Starts transaction
        /// <remarks>When transaction is started, it created shadow connection object which will be active until transaction closed</remarks>
        /// </summary>
        public void BeginTransaction()
        {
            if (this._ShadowTransaction != null)
                throw new dhtmlxException("Transaction is already opened!");
            if (this._ShadowConnection == null)
                this._ShadowConnection = this.CreateConnection(this.ConnectionString);
            if (this._ShadowConnection.State != ConnectionState.Open)
                this._ShadowConnection.Open();
            this._ShadowTransaction = this._ShadowConnection.BeginTransaction();
        }

        /// <summary>
        /// Commits transaction
        /// </summary>
        public void CommitTransaction()
        {
            if (this._ShadowTransaction == null)
                throw new dhtmlxException("There's no active transaction!");
            this._ShadowTransaction.Commit();
        }

        /// <summary>
        /// Rollbacks transaction
        /// </summary>
        public void RollbackTransaction()
        {
            if (this._ShadowTransaction == null)
                throw new dhtmlxException("There's no active transaction!");
            this._ShadowTransaction.Rollback();
        }

        #endregion
    }

    /// <summary>
    /// Supported database adapters type
    /// </summary>
    public enum dhtmlxDatabaseAdapterType
    {
        /// <summary>
        /// Microsoft SQL Server 2000-2008
        /// </summary>
        SqlServer2005,
        ///// <summary>
        ///// MySQL adapter
        ///// </summary>
        //MySql,
        ///// <summary>
        ///// DB2 adapter
        ///// </summary>
        //DB2,
        /// <summary>
        /// User-defined adapter
        /// </summary>
        Unknown
    }
}
