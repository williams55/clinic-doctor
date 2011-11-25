using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Serves dhtmlxTree components requests
    /// </summary>
    public class dhtmlxTreeConnector: dhtmlxConnector<dhtmlxTreeDataItem>
    {
        /// <summary>
        /// Gets or Sets field to take nodes text from
        /// </summary>
        public Field NodeTextField
        {
            get;
            set;
        }

        /// <summary>
        /// Enables or Disables load-on-demand functionality when only items level requested is loaded. If disabled, whole tree at once will be rendered
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
        /// Gets or Sets value ForeignKey field of root nodes
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
        /// Initializes properties
        /// </summary>
        /// <param name="NodeTextColumnName">String name of NodeTextField column</param>
        protected void Initialize(string NodeTextColumnName)
        {
            if (!string.IsNullOrEmpty(NodeTextColumnName))
                this.NodeTextField = this.Request.Adapter.ParseField(NodeTextColumnName);
        }

        /// <summary>
        /// Creates new instance of dhtmlxTreeConnector
        /// </summary>
        /// <param name="SelectSource">Select query or Table name to use for data retrieval</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="ParentIDColumnName">ForeignKey column name (nullable)</param>
        /// <param name="AdapterType">Type of adapter to use for communication with database engine</param>
        /// <param name="ConnectionString">ConnectionString for connection to database engine</param>
        /// <param name="NodeTextColumnName">Name of column to take nodes text</param>
        public dhtmlxTreeConnector(string SelectSource, string PrimaryKeyColumnName, string ParentIDColumnName, dhtmlxDatabaseAdapterType AdapterType, string ConnectionString, string NodeTextColumnName)
        {
            if (SelectSource.Trim().StartsWith("SELECT", StringComparison.InvariantCultureIgnoreCase))
                this._Request = new DataRequest(this, SelectSource, PrimaryKeyColumnName, ParentIDColumnName, AdapterType, ConnectionString);//sql
            else
                this._Request = new DataRequest(this, SelectSource, NodeTextColumnName, PrimaryKeyColumnName, ParentIDColumnName, AdapterType, ConnectionString);//table
            Initialize(NodeTextColumnName);
        }

        /// <summary>
        /// Creates new instance of dhtmlxTreeConnector
        /// </summary>
        /// <param name="SelectSource">Select query or Table name to use for data retrieval</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="ParentIDColumnName">ForeignKey column name (nullable)</param>
        /// <param name="Adapter">Adapter to use for communication with database engine</param>
        /// <param name="NodeTextColumnName">Name of column to take nodes text</param>
        public dhtmlxTreeConnector(string SelectSource, string PrimaryKeyColumnName, string ParentIDColumnName, IdhtmlxDatabaseAdapter Adapter, string NodeTextColumnName)
        {
            if (SelectSource.Trim().StartsWith("SELECT", StringComparison.InvariantCultureIgnoreCase))
                this._Request = new DataRequest(this, SelectSource, PrimaryKeyColumnName, ParentIDColumnName, Adapter);//sql
            else
                this._Request = new DataRequest(this, SelectSource, NodeTextColumnName, PrimaryKeyColumnName, ParentIDColumnName, Adapter);//table
            Initialize(NodeTextColumnName);
        }

  

        /// <summary>
        /// Creates collection of dhtmlxTreeDataItem objects from DataTable provided
        /// </summary>
        /// <param name="Rows">Table to create dhtmlxTreeDataItem objects from</param>
        /// <returns>Collection of dhtmlxTreeDataItem objects</returns>
        protected override List<dhtmlxTreeDataItem> CreateDataItems(DataTable Rows)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Converting DataRow object to connector specific data items");
#endif
            #endregion
            FieldRule getChildRule = new FieldRule(this.Request.ParentRecordIDField, Operator.Equals, this.Request.RelationIDValue);
            List<dhtmlxTreeDataItem> dataItems = CreateItemsLevel(Rows, getChildRule);
            return dataItems;
        }

        /// <summary>
        /// Event to be called when connector tries to analyze whether particular node has child nodes or not. Event will not called if EnableDynamicLoading is false
        /// </summary>
        public event EventHandler<RequestChildrenEventArgs<dhtmlxTreeDataItem>> RequestHasChildren;

        /// <summary>
        /// Creates single level of dhtmlxTreeDataItem objects united by the same ForeignKey value
        /// </summary>
        /// <param name="AllItems">Reference to collection that contains all avilable items</param>
        /// <param name="GetChildrenRule">FieldRule to use for appropriate items selection (e.g. "parentID = 15")</param>
        /// <returns>Collection of dhtmlxTreeDataItem items located at specified level</returns>
        protected List<dhtmlxTreeDataItem> CreateItemsLevel(DataTable AllItems, FieldRule GetChildrenRule)
        {
            List<dhtmlxTreeDataItem> dataItems = new List<dhtmlxTreeDataItem>();
            DataRow[] RowsToRender = AllItems.Select(GetChildrenRule.ToString());
            for (int i = 0; i < RowsToRender.Length; i++)
            {
                DataRow row = RowsToRender[i];
                dhtmlxTreeDataItem dataItem = new dhtmlxTreeDataItem();
                if (this.Request.PrimaryKeyField != null)
                    dataItem.ID = Convert.ToString(row[this.Request.PrimaryKeyField.ExternalName]);
                foreach (DataColumn col in AllItems.Columns)
                    dataItem.DataFields.Add(col.ColumnName, Tools.ConvertToString(row[col]));

                if (this.NodeTextField != null)
                    dataItem.Text = Convert.ToString(row[this.NodeTextField.ExternalName]);
                
                dataItem.Index = i;
                FieldRule getChildRule = new FieldRule(this.Request.ParentRecordIDField, Operator.Equals, Convert.ToString(row[this.Request.PrimaryKeyField.InternalName]));
                int childrenCount = 0;
                if (this.EnableDynamicLoading)//dynamic loading is enabled -> check either current item has children or not
                {
                    if (this.RequestHasChildren != null)//call event that will check whether this item has children
                    {
                        RequestChildrenEventArgs<dhtmlxTreeDataItem> eventArgs = new RequestChildrenEventArgs<dhtmlxTreeDataItem>(dataItem);
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
                else//load on demand is disabled - just build tree
                {
                    DataRow[] childRows = AllItems.Select(getChildRule.ToString());
                    childrenCount = childRows.Length;
                    if (childrenCount > 0)
                        dataItem.ChildNodes.AddRange(CreateItemsLevel(AllItems, getChildRule));
                }
                dataItem.HasChildren = childrenCount > 0;
                dataItems.Add(dataItem);
            }
            return dataItems;
        }

        /// <summary>
        /// Writes begin tags of response header
        /// </summary>
        /// <param name="xWriter">XmlWriter to render content to</param>
        /// <param name="RowsToRender">Data to render</param>
        /// <param name="TotalRowsCount">Total amount of rows available</param>
        protected override void BeginRenderContent(XmlWriter xWriter, DataTable RowsToRender, int TotalRowsCount)
        {
            xWriter.WriteStartElement("tree");
            xWriter.WriteAttributeString("id", this.RootItemRelationIDValue == this.Request.RelationIDValue ? "0" : this.Request.RelationIDValue);
        }

        /// <summary>
        /// Decodes field name to Field object
        /// </summary>
        /// <param name="EncodedField">Encoded field name token from QueryString</param>
        /// <returns>Field object that corresponds EncodedField, or null</returns>
        public override Field DecodeField(string EncodedField)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Encoding query field: " + EncodedField);
#endif
            #endregion
            string encField = EncodedField;// Regex.Replace(EncodedField, "^[^_]*.", "");
            switch (encField)
            {
                case "tr_text":
                    return this.NodeTextField;
                case "tr_pid":
                    return this.Request.ParentRecordIDField;
                case "tr_id":
                    return this.Request.PrimaryKeyField;
                default:
                    return null;
            }
        }
    }
}
