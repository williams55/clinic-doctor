using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using System.Data;
using System.Xml;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Bridge class between connectors and database adapters. Class is responsible for parsing client request and processing basic data operations (sorting, filtering, selecting etc.)
    /// </summary>
    public partial class DataRequest
    {
        private Dictionary<CustomSQLType, string> _CustomSQLs = new Dictionary<CustomSQLType, string>();
        
        /// <summary>
        /// Gets reference to collection of sql templates for database operations(INSERT, UPDATE, DELETE) used instead of standard ones.
        /// </summary>
        public Dictionary<CustomSQLType, string> CustomSQLs
        {
            get
            {
                return this._CustomSQLs;
            }
        }


        private IdhtmlxConnector _Connector;

        /// <summary>
        /// Gets reference to dhtmlxConnector which is responsible for rendering responses in format specific to particular dhtmlx control
        /// </summary>
        public IdhtmlxConnector Connector
        {
            get
            {
                return this._Connector;
            }
        }

        private dhtmlxDatabaseAdapterType _AdapterType = dhtmlxDatabaseAdapterType.Unknown;

        /// <summary>
        /// Gets adapter type used for communication with database engine chosen
        /// </summary>
        public dhtmlxDatabaseAdapterType AdapterType
        {
            get
            {
                return this._AdapterType;
            }
        }

        /// <summary>
        /// Initializes own properties
        /// </summary>
        /// <param name="SelectQuery">Query to be parsed</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name</param>
        /// <param name="ParentIDColumnName">ForeignKey column name</param>
        private void Initialize(string SelectQuery, string PrimaryKeyColumnName, string ParentIDColumnName)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Initialize: primary and foreign key columns (" + PrimaryKeyColumnName + ", " + ParentIDColumnName + ")");
#endif
            #endregion
            InitializeKeyFields(PrimaryKeyColumnName, ParentIDColumnName);
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Initialize: parsing sql " + SelectQuery);
#endif
            #endregion
            this.InitializeFromSql(SelectQuery, PrimaryKeyColumnName);
            this.AddMissingQueryFields();
        }

        /// <summary>
        /// Initializes own properties
        /// </summary>
        /// <param name="TableName">TableName to run queries against</param>
        /// <param name="Columns">Comma delimited collection of columns to select</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name</param>
        /// <param name="ParentIDColumnName">ForeignKey column name</param>
        private void Initialize(string TableName, string Columns, string PrimaryKeyColumnName, string ParentIDColumnName)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Initialize: primary and foreign key columns (" + PrimaryKeyColumnName + ", " + ParentIDColumnName + ")");
#endif
            #endregion
            InitializeKeyFields(PrimaryKeyColumnName, ParentIDColumnName);
            this.TableName = TableName;
            //Parse fields assuming that they're comma-delimited
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Initialize: parsing columns " + Columns);
#endif
            #endregion
            foreach (string ColumnExpression in Columns.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                this.RequestedFields.Add(this.Adapter.ParseField(ColumnExpression));
            this.AddMissingQueryFields();
        }

        /// <summary>
        /// Adds columns into RequestedFields collection that supposed to be retrieven from server, but was not mentioned in RequestedFields collection
        /// </summary>
        private void AddMissingQueryFields()
        {
            //If PrimaryKeyField is set, but not mentioned in RequestedFields - add it
            if (this.PrimaryKeyField != null && !this.RequestedFields.Contains(this.PrimaryKeyField))
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "PrimaeyKeyField (" + this.PrimaryKeyField.ExternalName + ") is not included into RequestedFields collection. Adding...");
#endif
                #endregion
                this.RequestedFields.Add(this.PrimaryKeyField.Clone());
            }
            //If ParentRecordIDField is set, but not mentioned in RequestedFields - add it
            if (this.ParentRecordIDField != null && !this.RequestedFields.Contains(this.ParentRecordIDField))
                this.RequestedFields.Add(this.ParentRecordIDField.Clone());
        }

        /// <summary>
        /// Creates instance of DataRequest
        /// </summary>
        /// <param name="Connector">Connector to use for results output</param>
        private DataRequest(IdhtmlxConnector Connector)
        {
            this._Connector = Connector;
        }

        /// <summary>
        /// Creates instance of DataRequest
        /// </summary>
        /// <param name="Connector">Connector to use for results output</param>
        /// <param name="AdapterType">Type of adapter to use for communication with database engine</param>
        /// <param name="ConnectionString">Connection string to use for DatabaseAdapter initialization</param>
        private DataRequest(IdhtmlxConnector Connector, dhtmlxDatabaseAdapterType AdapterType, string ConnectionString)
            :this(Connector)
        {
            this._AdapterType = AdapterType;
            this.Adapter.ConnectionString = ConnectionString;
        }

        /// <summary>
        /// Creates instance of DataRequest
        /// </summary>
        /// <param name="Connector">Connector to use for results output</param>
        /// <param name="Adapter">DatabaseAdapter to use for communication with database engine</param>
        private DataRequest(IdhtmlxConnector Connector, IdhtmlxDatabaseAdapter Adapter)
            : this(Connector)
        {
            this._Adapter = Adapter;
        }

        /// <summary>
        /// Creates new instance of DataRequest
        /// </summary>
        /// <param name="Connector">Connector to use for results output</param>
        /// <param name="SelectQuery">Select query to use for data requests</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name. Used to identify single row</param>
        /// <param name="ParentIDColumnName">ForeignKey column name. Used to link child rows to parents</param>
        /// <param name="AdapterType">Type of adapter to use for communication with database engine</param>
        /// <param name="ConnectionString">DatabaseAdapter connection string</param>
        public DataRequest(IdhtmlxConnector Connector, string SelectQuery, string PrimaryKeyColumnName, string ParentIDColumnName, dhtmlxDatabaseAdapterType AdapterType, string ConnectionString)
            : this(Connector, AdapterType, ConnectionString)
        {
            this.Initialize(SelectQuery, PrimaryKeyColumnName, ParentIDColumnName);
        }

        /// <summary>
        /// Creates new instance of DataRequest
        /// </summary>
        /// <param name="Connector">Connector to use for results output</param>
        /// <param name="SelectQuery">Select query to use for data requests</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name. Used to identify single row</param>
        /// <param name="ParentIDColumnName">ForeignKey column name. Used to link child rows to parents</param>
        /// <param name="Adapter">Adapter to use for communication with database engine</param>
        public DataRequest(IdhtmlxConnector Connector, string SelectQuery, string PrimaryKeyColumnName, string ParentIDColumnName, IdhtmlxDatabaseAdapter Adapter)
            : this(Connector, Adapter)
        {
            this.Initialize(SelectQuery, PrimaryKeyColumnName, ParentIDColumnName);
        }

        /// <summary>
        /// Creates new instance of DataRequest
        /// </summary>
        /// <param name="Connector">Connector to use for results output</param>
        /// <param name="TableName">Table name to run queries against to</param>
        /// <param name="Columns">Columns to include into result</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name. Used to identify single row</param>
        /// <param name="ParentIDColumnName">ForeignKey column name. Used to link child rows to parents</param>
        /// <param name="Adapter">Adapter to use for communication with database engine</param>
        public DataRequest(IdhtmlxConnector Connector, string TableName, string Columns, string PrimaryKeyColumnName, string ParentIDColumnName, IdhtmlxDatabaseAdapter Adapter)
            : this(Connector, Adapter)
        {
            this.Initialize(TableName, Columns, PrimaryKeyColumnName, ParentIDColumnName);
        }

        /// <summary>
        /// Creates new instance of DataRequest
        /// </summary>
        /// <param name="Connector">Connector to use for results output</param>
        /// <param name="TableName">Table name to run queries against to</param>
        /// <param name="Columns">Columns to include into result</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name. Used to identify single row</param>
        /// <param name="ParentIDColumnName">ForeignKey column name. Used to link child rows to parents</param>
        /// <param name="AdapterType">Type of adapter to use for communication with database engine</param>
        /// <param name="ConnectionString">DatabaseAdapter connection string</param>
        public DataRequest(IdhtmlxConnector Connector, string TableName, string Columns, string PrimaryKeyColumnName, string ParentIDColumnName, dhtmlxDatabaseAdapterType AdapterType, string ConnectionString)
            : this(Connector, AdapterType, ConnectionString)
        {
            this.Initialize(TableName, Columns, PrimaryKeyColumnName, ParentIDColumnName);
        }

        /// <summary>
        /// Initializes primary and foreign key fields
        /// </summary>
        /// <param name="PrimaryKeyColumnName">Name of column to be used and PrimaryKey field</param>
        /// <param name="ParentIDColumnName">Name of column to be used as ForeignKey field</param>
        private void InitializeKeyFields(string PrimaryKeyColumnName, string ParentIDColumnName)
        {
            if (!string.IsNullOrEmpty(PrimaryKeyColumnName))
                this.PrimaryKeyField = this.Adapter.ParseField(PrimaryKeyColumnName);
            if (!string.IsNullOrEmpty(ParentIDColumnName))
                this.ParentRecordIDField = this.Adapter.ParseField(ParentIDColumnName);
        }

        private IdhtmlxDatabaseAdapter _Adapter = null;
        /// <summary>
        /// Gets IdhtmlxDatabaseAdapter instance that is responsible for all database-level operations such as execution queries, parsing and generating database-specific sql, etc.
        /// </summary>
        public virtual IdhtmlxDatabaseAdapter Adapter
        {
            get
            {
                if (this._Adapter == null)
                {
                    if (this.AdapterType == dhtmlxDatabaseAdapterType.Unknown)
                        throw new ApplicationException("Cannot find default dhtmlxDatabaseAdapter for adapter type " + dhtmlxDatabaseAdapterType.Unknown.ToString());
                    switch (this.AdapterType)
                    {
                        case dhtmlxDatabaseAdapterType.SqlServer2005:
                            this._Adapter = new MSSQLAdapter();
                            break;
                        default:
                            throw new NotImplementedException("dhtmlxDatabaseAdapter for type " + this.AdapterType.ToString() + " has not been implemented yet!");
                    }
                }
                return this._Adapter;
            }
        }

        private dhtmlxFieldsCollection _RequestedFields = new dhtmlxFieldsCollection();
        /// <summary>
        /// Gets collection of fields to be requested from table
        /// </summary>
        public dhtmlxFieldsCollection RequestedFields
        {
            get
            {
                return this._RequestedFields;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("RequestedFields");
                this._RequestedFields = value;
            }
        }

        private string _TableName;
        /// <summary>
        /// Gets or Sets table name for data to be requested from (e.g. Customers)
        /// </summary>
        public string TableName
        {
            get
            {
                return this._TableName;
            }
            set
            {
                this._TableName = value;
            }
        }

        private Field _IDField = null;
        /// <summary>
        /// Gets or Sets field to use as identifier for items/rows selected
        /// </summary>
        public Field PrimaryKeyField
        {
            get
            {
                return this._IDField;
            }
            set
            {
                this._IDField = value;
            }
        }

        private Field _ParentRecordIDField = null;
        /// <summary>
        /// Gets or Sets field that stores id of parent item/row 
        /// </summary>
        public Field ParentRecordIDField
        {
            get
            {
                return this._ParentRecordIDField;
            }
            set
            {
                this._ParentRecordIDField = value;
            }
        }

        private List<Rule> _Rules = new List<Rule>();
        /// <summary>
        /// Gets or Sets collection of rules to be applied on resurned data (e.g. all statements coming after WHERE clause will come to this collection: SELECT * FROM )
        /// </summary>
        public List<Rule> Rules
        {
            get
            {
                return this._Rules;
            }
        }

        private List<OrderByStatement> _OrderBy = new List<OrderByStatement>();
        /// <summary>
        /// Gets reference to collection of Order By statements for current query
        /// </summary>
        public List<OrderByStatement> OrderBy
        {
            get
            {
                return this._OrderBy;
            }
        }

        private int _StartIndex;
        /// <summary>
        /// Gets or Sets row index to start select from
        /// </summary>
        public int StartIndex
        {
            get
            {
                return this._StartIndex;
            }
            set
            {
                this._StartIndex = value;
            }
        }

        private int _Count;
        /// <summary>
        /// Gets or Sets total rows number to be returned
        /// </summary>
        public int Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                this._Count = value;
            }
        }

        /// <summary>
        /// Initializes TableName, RequestedFields, OrderBy, Rules, PrimaryKeyField from SQL query and PrimaryKey column name
        /// </summary>
        /// <param name="SqlQuery">SQL Query to be used for initialization</param>
        /// <param name="PrimaryKey">Column name to be used for PrimaryKeyField initialization</param>
        private void InitializeFromSql(string SqlQuery, string PrimaryKey)
        {
            IEnumerable<Field> parsedFields = null;
            this.Adapter.ParseSqlQuery(SqlQuery, out this._TableName, out parsedFields, out this._Rules, out this._OrderBy, out this._StartIndex, out this._Count);
            this._RequestedFields = parsedFields.ToFieldsCollection();
            if (!string.IsNullOrEmpty(PrimaryKey))
                this.PrimaryKeyField = this.Adapter.ParseField(PrimaryKey);
        }

        private AccessRights _AllowedAccess = AccessRights.All;
        
        /// <summary>
        /// Gets or Sets operations flags allowed for execution for current DataRequest
        /// </summary>
        public AccessRights AllowedAccess
        {
            get
            {
                return this._AllowedAccess;
            }
            set
            {
                this._AllowedAccess = value;
            }
        }

        private TransactionMode _TransactionMode = TransactionMode.None;

        /// <summary>
        /// Gets or Sets transactions mode for database operations made by this DataRequest
        /// </summary>
        public TransactionMode TransactionMode
        {
            get
            {
                return this._TransactionMode;
            }
            set
            {
                this._TransactionMode = value;
            }
        }

        private DataRequestType _RequestType = DataRequestType.Select;
        /// <summary>
        /// Gets current operation to be executed by DataRequest
        /// </summary>
        public DataRequestType RequestType
        {
            get
            {
                return this._RequestType;
            }
        }

        private List<DataAction> _DataActions = new List<DataAction>();
        /// <summary>
        /// Gets reference to the collection that contains DataActions to be executed during requrest (except for select)
        /// </summary>
        public List<DataAction> DataActions
        {
            get
            {
                return this._DataActions;
            }
        }
    }

    /// <summary>
    /// Specifies number of operations can be executed by DataRequest
    /// </summary>
    public enum DataRequestType
    {
        /// <summary>
        /// Indicates that DataRequest is going to select data
        /// </summary>
        Select,
        /// <summary>
        /// Indicates that DataRequest is going to modify one or more records
        /// </summary>
        Edit,
    }

    /// <summary>
    /// Collection of access rights allowed for DataRequest
    /// </summary>
    [Flags]
    public enum AccessRights: uint
    {
        /// <summary>
        /// Permission for Select operation
        /// </summary>
        Select  = 0x00000001,
        /// <summary>
        /// Permission for Update operation
        /// </summary>
        Update  = 0x00000010,
        /// <summary>
        /// Permission for Insert operation
        /// </summary>
        Insert  = 0x00000100,
        /// <summary>
        /// Permission for Delete operation
        /// </summary>
        Delete  = 0x00001000,
        /// <summary>
        /// All operations are allowed
        /// </summary>
        All     = Select | Update | Insert | Delete,
        /// <summary>
        /// No operations are allowed
        /// </summary>
        None = 0x00000000
    }

    /// <summary>
    /// Transaction modes supported by DataRequest
    /// </summary>
    public enum TransactionMode
    {
        /// <summary>
        /// No transactions will be used
        /// </summary>
        None,
        /// <summary>
        /// One common transaction will be used for all DataAction run during request
        /// </summary>
        PerRequest,
        /// <summary>
        /// Separate transactions will be used for every DataAction
        /// </summary>
        PerRecord
    }

    /// <summary>
    /// Types of sql templates that can be set by user
    /// </summary>
    public enum CustomSQLType
    {
        /// <summary>
        /// Custom sql template for Insert operation
        /// </summary>
        Insert,
        /// <summary>
        /// Custom sql template for Delete statement
        /// </summary>
        Delete,
        /// <summary>
        /// Custom sql template for Update statement
        /// </summary>
        Update
    }
}