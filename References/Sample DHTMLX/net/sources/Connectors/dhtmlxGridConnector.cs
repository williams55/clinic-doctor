using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Serves dhtmlGrid client requests
    /// </summary>
    public class dhtmlxGridConnector : dhtmlxGridConnectorBase<dhtmlxGridDataItem>
    {
        /// <summary>
        /// Creates new instance of dhtmlxGridConnector
        /// </summary>
        /// <param name="SelectQuery">Select query to use for data retrieval</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="AdapterType">Type of adapter to use for communication with database engine</param>
        /// <param name="ConnectionString">ConnectionString for connection to database engine</param>
        /// <param name="ExtraColumnNames">Columns to be included into select query, but ignored during render event</param>
        public dhtmlxGridConnector(string SelectQuery, string PrimaryKeyColumnName, dhtmlxDatabaseAdapterType AdapterType, string ConnectionString, string ExtraColumnNames)
            : base()
        {
            this._Request = new DataRequest(this, SelectQuery, PrimaryKeyColumnName, "", AdapterType, ConnectionString);
            this.ParseExtraColumns(ExtraColumnNames);
        }

        /// <summary>
        /// Creates new instance of dhtmlxGridConnector
        /// </summary>
        /// <param name="SelectQuery">Select query to use for data retrieval</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="AdapterType">Type of adapter to use for communication with database engine</param>
        /// <param name="ConnectionString">ConnectionString for connection to database engine</param>
        public dhtmlxGridConnector(string SelectQuery, string PrimaryKeyColumnName, dhtmlxDatabaseAdapterType AdapterType, string ConnectionString)
            : this(SelectQuery, PrimaryKeyColumnName, AdapterType, ConnectionString, "")
        {
        }

        /// <summary>
        /// Creates new instance of dhtmlxGridConnector
        /// </summary>
        /// <param name="TableName">Table name to run query against</param>
        /// <param name="Columns">Columns to select</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="AdapterType">Type of adapter to use for communication with database engine</param>
        /// <param name="ConnectionString">ConnectionString for connection to database engine</param>
        /// <param name="ExtraColumnNames">Columns to be included into select query, but ignored during render event</param>
        public dhtmlxGridConnector(string TableName, string Columns, string PrimaryKeyColumnName, dhtmlxDatabaseAdapterType AdapterType, string ConnectionString, string ExtraColumnNames)
            : base()
        {
            this._Request = new DataRequest(this, TableName, Columns, PrimaryKeyColumnName, "", AdapterType, ConnectionString);
            this.ParseExtraColumns(ExtraColumnNames);
        }

        /// <summary>
        /// Creates new instance of dhtmlxGridConnector
        /// </summary>
        /// <param name="TableName">Table name to run query against</param>
        /// <param name="Columns">Columns to select</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="AdapterType">Type of adapter to use for communication with database engine</param>
        /// <param name="ConnectionString">ConnectionString for connection to database engine</param>
        public dhtmlxGridConnector(string TableName, string Columns, string PrimaryKeyColumnName, dhtmlxDatabaseAdapterType AdapterType, string ConnectionString)
            : this(TableName, Columns, PrimaryKeyColumnName, AdapterType, ConnectionString, "")
        {
        }

        /// <summary>
        /// Creates new instance of dhtmlxGridConnector
        /// </summary>
        /// <param name="SelectQuery">Select query to use for data retrieval</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="Adapter">Adapter to use for communication with database engine</param>
        /// <param name="ExtraColumnNames">Columns to be included into select query, but ignored during render event</param>
        public dhtmlxGridConnector(string SelectQuery, string PrimaryKeyColumnName, IdhtmlxDatabaseAdapter Adapter, string ExtraColumnNames)
            : base()
        {
            this._Request = new DataRequest(this, SelectQuery, PrimaryKeyColumnName, "", Adapter);
            this.ParseExtraColumns(ExtraColumnNames);
        }

        /// <summary>
        /// Creates new instance of dhtmlxGridConnector
        /// </summary>
        /// <param name="SelectQuery">Select query to use for data retrieval</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="Adapter">Adapter to use for communication with database engine</param>
        public dhtmlxGridConnector(string SelectQuery, string PrimaryKeyColumnName, IdhtmlxDatabaseAdapter Adapter)
            : this(SelectQuery, PrimaryKeyColumnName, Adapter, "")
        {
        }

        /// <summary>
        /// Creates new instance of dhtmlxGridConnector
        /// </summary>
        /// <param name="TableName">Table name to run query against</param>
        /// <param name="Columns">Columns to select</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="Adapter">Adapter to use for communication with database engine</param>
        /// <param name="ExtraColumnNames">Columns to be included into select query, but ignored during render event</param>
        public dhtmlxGridConnector(string TableName, string Columns, string PrimaryKeyColumnName, IdhtmlxDatabaseAdapter Adapter, string ExtraColumnNames)
            : base()
        {
            this._Request = new DataRequest(this, TableName, Columns, PrimaryKeyColumnName, "", Adapter);
            this.ParseExtraColumns(ExtraColumnNames);
        }

        /// <summary>
        /// Creates new instance of dhtmlxGridConnector
        /// </summary>
        /// <param name="TableName">Table name to run query against</param>
        /// <param name="Columns">Columns to select</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="Adapter">Adapter to use for communication with database engine</param>
        public dhtmlxGridConnector(string TableName, string Columns, string PrimaryKeyColumnName, IdhtmlxDatabaseAdapter Adapter)
            : this(TableName, Columns, PrimaryKeyColumnName, Adapter, "")
        { 
        }

        /// <summary>
        /// Sets number of rows returned by default
        /// </summary>
        /// <param name="RowsPerPage">Number of rows to be returned during initial request</param>
        public void SetDynamicLoading(int RowsPerPage)
        {
            this.Request.Count = RowsPerPage;
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
            if (this.Request.StartIndex == 0)
                xWriter.WriteAttributeString("total_count", TotalRowsCount.ToString());
            else
                xWriter.WriteAttributeString("pos", this.Request.StartIndex.ToString());
        }

        /// <summary>
        /// Creates collection of dhtmlxGridDataItem from DataTable provided
        /// </summary>
        /// <param name="Rows">DataTable to create items from</param>
        /// <returns>Collection of dhtmlxGridDataItem objects</returns>
        protected override List<dhtmlxGridDataItem> CreateDataItems(DataTable Rows)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Converting DataRow object to connector specific data items");
#endif
            #endregion
            List<dhtmlxGridDataItem> dataItems = new List<dhtmlxGridDataItem>();
            List<string> ExtraColumnNames = this.ExtraFields.Select(eField => eField.ExternalName).ToList();
            for (int i = 0; i < Rows.Rows.Count; i++)
            {
                DataRow row = Rows.Rows[i];
                dhtmlxGridDataItem dataItem = new dhtmlxGridDataItem();
                if (this.Request.PrimaryKeyField != null)
                    dataItem.ID = Convert.ToString(row[this.Request.PrimaryKeyField.ExternalName]);
                foreach (DataColumn col in Rows.Columns)
                    dataItem.DataFields.Add(col.ColumnName, Tools.ConvertToString(row[col]));
                dataItem.Index = i + this.Request.StartIndex;
                dataItem.SetExtraColumns(ExtraColumnNames);
                dataItems.Add(dataItem);
            }
            return dataItems;
        }
    }
}
