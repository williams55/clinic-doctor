using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Serves client requests came from dhtmlxTreeGrid component
    /// </summary>
    public class dhtmlxTreeGridConnector : dhtmlxGridConnectorBase<dhtmlxTreeGridDataItem>
    {
        /// <summary>
        /// Gets or Sets value indicating either all content will be returned at once or by portions
        /// </summary>
        public bool EnableDynamicLoading
        {
            get
            {
                return this.Request.EnableHierarchicalDynamicLoading;
            }
            set
            {
                this.Request.EnableHierarchicalDynamicLoading = value;
            }
        }

        /// <summary>
        /// Gets or Sets ForeignKey column value for root rows
        /// </summary>
        public string RootItemRelationIDValue
        {
            get
            {
                return this.Request.RootItemRelationIDValue;
            }
            set
            {
                this.Request.RootItemRelationIDValue = value;
            }
        }

        /// <summary>
        /// Creates new instance of dhtmlxTreeGridConnector
        /// </summary>
        /// <param name="SelectQuery">Select query to use for data retrieval</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="ParentIDColumnName">ForeignKey column name (nullable)</param>
        /// <param name="AdapterType">Type of adapter to use for communication with database engine</param>
        /// <param name="ConnectionString">ConnectionString for connection to database engine</param>
        /// <param name="ExtraColumnNames">Columns to be included into select query, but ignored during render event</param>
        public dhtmlxTreeGridConnector(string SelectQuery, string PrimaryKeyColumnName, string ParentIDColumnName, dhtmlxDatabaseAdapterType AdapterType, string ConnectionString, string ExtraColumnNames)
            : base()
        {
            this._Request = new DataRequest(this, SelectQuery, PrimaryKeyColumnName, ParentIDColumnName, AdapterType, ConnectionString);
            this.ParseExtraColumns(ExtraColumnNames);
        }

        /// <summary>
        /// Creates new instance of dhtmlxTreeGridConnector
        /// </summary>
        /// <param name="SelectQuery">Select query to use for data retrieval</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="ParentIDColumnName">ForeignKey column name (nullable)</param>
        /// <param name="AdapterType">Type of adapter to use for communication with database engine</param>
        /// <param name="ConnectionString">ConnectionString for connection to database engine</param>
        public dhtmlxTreeGridConnector(string SelectQuery, string PrimaryKeyColumnName, string ParentIDColumnName, dhtmlxDatabaseAdapterType AdapterType, string ConnectionString)
            : this(SelectQuery, PrimaryKeyColumnName, ParentIDColumnName, AdapterType, ConnectionString, "")
        {
        }

        /// <summary>
        /// Creates new instance of dhtmlxTreeGridConnector
        /// </summary>
        /// <param name="TableName">Table name to run query against</param>
        /// <param name="Columns">Columns to select</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="ParentIDColumnName">ForeignKey column name (nullable)</param>
        /// <param name="AdapterType">Type of adapter to use for communication with database engine</param>
        /// <param name="ConnectionString">ConnectionString for connection to database engine</param>
        /// <param name="ExtraColumnNames">Columns to be included into select query, but ignored during render event</param>
        public dhtmlxTreeGridConnector(string TableName, string Columns, string PrimaryKeyColumnName, string ParentIDColumnName, dhtmlxDatabaseAdapterType AdapterType, string ConnectionString, string ExtraColumnNames)
            : base()
        {
            this._Request = new DataRequest(this, TableName, Columns, PrimaryKeyColumnName, ParentIDColumnName, AdapterType, ConnectionString);
            this.ParseExtraColumns(ExtraColumnNames);

        }

        /// <summary>
        /// Creates new instance of dhtmlxTreeGridConnector
        /// </summary>
        /// <param name="TableName">Table name to run query against</param>
        /// <param name="Columns">Columns to select</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="ParentIDColumnName">ForeignKey column name (nullable)</param>
        /// <param name="AdapterType">Type of adapter to use for communication with database engine</param>
        /// <param name="ConnectionString">ConnectionString for connection to database engine</param>
        public dhtmlxTreeGridConnector(string TableName, string Columns, string PrimaryKeyColumnName, string ParentIDColumnName, dhtmlxDatabaseAdapterType AdapterType, string ConnectionString)
            : this(TableName, Columns, PrimaryKeyColumnName, ParentIDColumnName, AdapterType, ConnectionString, "")
        {
        }

        /// <summary>
        /// Creates new instance of dhtmlxTreeGridConnector
        /// </summary>
        /// <param name="SelectQuery">Select query to use for data retrieval</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="ParentIDColumnName">ForeignKey column name (nullable)</param>
        /// <param name="Adapter">Adapter to use for communication with database engine</param>
        /// <param name="ExtraColumnNames">Columns to be included into select query, but ignored during render event</param>
        public dhtmlxTreeGridConnector(string SelectQuery, string PrimaryKeyColumnName, string ParentIDColumnName, IdhtmlxDatabaseAdapter Adapter, string ExtraColumnNames)
            : base()
        {
            this._Request = new DataRequest(this, SelectQuery, PrimaryKeyColumnName, ParentIDColumnName, Adapter);
            this.ParseExtraColumns(ExtraColumnNames);
        }

        /// <summary>
        /// Creates new instance of dhtmlxTreeGridConnector
        /// </summary>
        /// <param name="SelectQuery">Select query to use for data retrieval</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="ParentIDColumnName">ForeignKey column name (nullable)</param>
        /// <param name="Adapter">Adapter to use for communication with database engine</param>
        public dhtmlxTreeGridConnector(string SelectQuery, string PrimaryKeyColumnName, string ParentIDColumnName, IdhtmlxDatabaseAdapter Adapter)
            : this(SelectQuery, PrimaryKeyColumnName, ParentIDColumnName, Adapter, "")
        {
        }

        /// <summary>
        /// Creates new instance of dhtmlxTreeGridConnector
        /// </summary>
        /// <param name="TableName">Table name to run query against</param>
        /// <param name="Columns">Columns to select</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="ParentIDColumnName">ForeignKey column name (nullable)</param>
        /// <param name="Adapter">Adapter to use for communication with database engine</param>
        /// <param name="ExtraColumnNames">Columns to be included into select query, but ignored during render event</param>
        public dhtmlxTreeGridConnector(string TableName, string Columns, string PrimaryKeyColumnName, string ParentIDColumnName, IdhtmlxDatabaseAdapter Adapter, string ExtraColumnNames)
            : base()
        {
            this._Request = new DataRequest(this, TableName, Columns, PrimaryKeyColumnName, ParentIDColumnName, Adapter);
            this.ParseExtraColumns(ExtraColumnNames);
        }

        /// <summary>
        /// Creates new instance of dhtmlxTreeGridConnector
        /// </summary>
        /// <param name="TableName">Table name to run query against</param>
        /// <param name="Columns">Columns to select</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="ParentIDColumnName">ForeignKey column name (nullable)</param>
        /// <param name="Adapter">Adapter to use for communication with database engine</param>
        public dhtmlxTreeGridConnector(string TableName, string Columns, string PrimaryKeyColumnName, string ParentIDColumnName, IdhtmlxDatabaseAdapter Adapter)
            : this(TableName, Columns, PrimaryKeyColumnName, ParentIDColumnName, Adapter, "")
        {
        }

        /// <summary>
        /// Writes begin tags of response header
        /// </summary>
        /// <param name="xWriter">XmlWriter to render content to</param>
        /// <param name="RowsToRender">Data to render</param>
        /// <param name="TotalRowsCount">Total amount of rows available</param>
        protected override void BeginRenderContent(XmlWriter xWriter, DataTable RowsToRender, int TotalRowsCount)
        {
            xWriter.WriteStartElement("rows");
            if (this.Request.RootItemRelationIDValue == this.Request.RelationIDValue)
                xWriter.WriteAttributeString("parent", "0");
            else
                xWriter.WriteAttributeString("parent", this.Request.RelationIDValue);
        }

        /// <summary>
        /// Creates collection of dhtmlxTreeGridDataItem from DataTable provided
        /// </summary>
        /// <param name="Rows">DataTable to create items from</param>
        /// <returns>Collection of dhtmlxTreeGridDataItem objects</returns>
        protected override List<dhtmlxTreeGridDataItem> CreateDataItems(DataTable Rows)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Converting DataRow object to connector specific data items");
#endif
            #endregion
            FieldRule getChildRule = new FieldRule(this.Request.ParentRecordIDField, Operator.Equals, this.Request.RelationIDValue);
            List<dhtmlxTreeGridDataItem> dataItems = CreateItemsLevel(Rows, getChildRule);
            return dataItems;
        }

        /// <summary>
        /// Creates single level of dhtmlxTreeGridDataItem objects united by the same ForeignKey value
        /// </summary>
        /// <param name="AllItems">Reference to collection that contains all avilable items</param>
        /// <param name="GetChildrenRule">FieldRule to use for appropriate items selection (e.g. "parentID = 15")</param>
        /// <returns>Collection of dhtmlxTreeGridDataItem items located at specified level</returns>
        protected List<dhtmlxTreeGridDataItem> CreateItemsLevel(DataTable AllItems, FieldRule GetChildrenRule)
        {
            List<dhtmlxTreeGridDataItem> dataItems = new List<dhtmlxTreeGridDataItem>();
            List<string> ExtraColumnNames = this.ExtraFields.Select(eField => eField.ExternalName).ToList();
            
            //select this level rows
            DataRow[] RowsToRender = AllItems.Select(GetChildrenRule.ToString());
            for (int i = 0; i < RowsToRender.Length; i++)
            {
                DataRow row = RowsToRender[i];
                dhtmlxTreeGridDataItem dataItem = new dhtmlxTreeGridDataItem();
                //set up dhtmlxTreeGridDataItem properties
                if (this.Request.PrimaryKeyField != null)
                    dataItem.ID = Convert.ToString(row[this.Request.PrimaryKeyField.ExternalName]);
                foreach (DataColumn col in AllItems.Columns)
                    dataItem.DataFields.Add(col.ColumnName, Tools.ConvertToString(row[col]));

                dataItem.Index = i;
                //set column names to be ignored during render
                dataItem.SetExtraColumns(ExtraColumnNames);
                //create rule to select child nodes of this item
                FieldRule getChildRule = new FieldRule(this.Request.ParentRecordIDField, Operator.Equals, Convert.ToString(row[this.Request.PrimaryKeyField.InternalName]));
                int childrenCount = 0;
                if (this.EnableDynamicLoading)//if load on demand is enabled
                {
                    if (this.RequestHasChildren != null)//call event that will check whether this item has children
                    {
                        RequestChildrenEventArgs<dhtmlxTreeGridDataItem> eventArgs = new RequestChildrenEventArgs<dhtmlxTreeGridDataItem>(dataItem);
                        this.RequestHasChildren(this, eventArgs);
                        childrenCount = eventArgs.HasChildren ? 1 : 0;
                    }
                    else//event to check whether this item has children is not set - call default check 
                    {
                        List<Rule> newRules = new List<Rule>();
                        foreach (Rule existingRule in this.Request.Rules)
                            if (!(existingRule is FieldRule) || (existingRule as FieldRule).Field != this.Request.ParentRecordIDField || (existingRule as FieldRule).Operator != Operator.Equals || Convert.ToString((existingRule as FieldRule).Value) != this.Request.RelationIDValue)
                                newRules.Add(existingRule);
                        newRules.Add(getChildRule);
                        childrenCount = this.Request.Adapter.ExecuteGetCountQuery(Request.TableName, newRules);
                    }
                }
                else//load on demand is disabled - just build rows tree
                {
                    DataRow[] childRows = AllItems.Select(getChildRule.ToString());
                    childrenCount = childRows.Length;
                    if (childrenCount > 0)
                        dataItem.ChildRows.AddRange(CreateItemsLevel(AllItems, getChildRule));
                }
                dataItem.HasChildren = childrenCount > 0;
                dataItems.Add(dataItem);
            }
            return dataItems;
        }

        /// <summary>
        /// Event to be called when connector tries to analyze whether particular row has child rows or not. Event will not called if EnableDynamicLoading is false
        /// </summary>
        public event EventHandler<RequestChildrenEventArgs<dhtmlxTreeGridDataItem>> RequestHasChildren;

    }
}
