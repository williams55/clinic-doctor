using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.Web;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Serves dhtmlxCombo component requests
    /// </summary>
    public class dhtmlxComboConnector: dhtmlxConnector<dhtmlxComboItem>
    {
        /// <summary>
        /// Gets or Sets reference to Field object from where to take options Text property
        /// </summary>
        public Field ItemTextField
        {
            get;
            set;
        }

        /// <summary>
        /// Initilize properties
        /// </summary>
        /// <param name="Request">DataRequest object to use</param>
        /// <param name="ItemTextColumnName">Column name from where to take Text property of combo items</param>
        protected void Initialize(DataRequest Request, string ItemTextColumnName)
        {
            this._Request = Request;
            if (!string.IsNullOrEmpty(ItemTextColumnName))
                this.ItemTextField = this.Request.Adapter.ParseField(ItemTextColumnName);
            if (this.ItemTextField != null && this.Request.RequestedFields[this.ItemTextField.ExternalName] == null)
                this.Request.RequestedFields.Add(this.ItemTextField);
            this.Request.BeforeSelect += new EventHandler(Request_BeforeSelect);
        }

        /// <summary>
        /// Hook to modify select query by combo specific parameters token from QueryString
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Request_BeforeSelect(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.QueryString["pos"] != null)
                this.Request.StartIndex = Convert.ToInt32(HttpContext.Current.Request.QueryString["pos"]);
            if (HttpContext.Current.Request.QueryString["mask"] != null)
                this.Request.Rules.Add(new FieldRule(this.ItemTextField, Operator.Like, HttpContext.Current.Request.QueryString["mask"] + "%"));
            
        }

        /// <summary>
        /// Creates new instance of dhtmlxComboConnector using adapter type and connection string
        /// </summary>
        /// <param name="SelectSource">Select query or Table name to use for data retrieval</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="AdapterType">Type of adapter to use for communication with database engine</param>
        /// <param name="ConnectionString">ConnectionString for connection to database engine</param>
        /// <param name="ItemTextColumnName">Column name to take option's Text property from</param>
        public dhtmlxComboConnector(string SelectSource, string PrimaryKeyColumnName, dhtmlxDatabaseAdapterType AdapterType, string ConnectionString, string ItemTextColumnName)
        {
            if (SelectSource.Trim().StartsWith("SELECT", StringComparison.InvariantCultureIgnoreCase))
                this.Initialize(
                    new DataRequest(this, SelectSource, PrimaryKeyColumnName, "", AdapterType, ConnectionString),
                    ItemTextColumnName
                    );
            else
                this.Initialize(
                new DataRequest(this, SelectSource, ItemTextColumnName, PrimaryKeyColumnName, "", AdapterType, ConnectionString),
                ItemTextColumnName
                );

        }

        /// <summary>
        /// Creates new instance of dhtmlxComboConnector using ready-to-use database adapter
        /// </summary>
        /// <param name="SelectSource">Select query or Table name to use for data retrieval</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="Adapter">Adapter to use for communication with database engine</param>
        /// <param name="ItemTextColumnName">Column name to take option's Text property from</param>
        public dhtmlxComboConnector(string SelectSource, string PrimaryKeyColumnName, IdhtmlxDatabaseAdapter Adapter, string ItemTextColumnName)
        {
            if (SelectSource.Trim().StartsWith("SELECT", StringComparison.InvariantCultureIgnoreCase))
                this.Initialize(
                    new DataRequest(this, SelectSource, PrimaryKeyColumnName, "", Adapter),
                    ItemTextColumnName
                    );
            else
                this.Initialize(
                new DataRequest(this, SelectSource, ItemTextColumnName, PrimaryKeyColumnName, "", Adapter),
                ItemTextColumnName
                );
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
        /// Creates collection of dhtmlxComboItem from DataTable provided
        /// </summary>
        /// <param name="Rows">DataTable to create items from</param>
        /// <returns>Collection of dhtmlxComboItem objects</returns>
        protected override List<dhtmlxComboItem> CreateDataItems(DataTable Rows)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Converting DataRow object to connector specific data items");
#endif
            #endregion
            List<dhtmlxComboItem> dataItems = new List<dhtmlxComboItem>();
            for (int i = 0; i < Rows.Rows.Count; i++)
            {
                DataRow row = Rows.Rows[i];
                dhtmlxComboItem dataItem = new dhtmlxComboItem();
                if (this.Request.PrimaryKeyField != null)
                    dataItem.ID = Convert.ToString(row[this.Request.PrimaryKeyField.ExternalName]);
                foreach (DataColumn col in Rows.Columns)
                    dataItem.DataFields.Add(col.ColumnName, Tools.ConvertToString(row[col]));
                if (this.ItemTextField != null)
                    dataItem.Text = Convert.ToString(row[this.ItemTextField.ExternalName]);
                dataItem.Index = i + this.Request.StartIndex;
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
            xWriter.WriteStartElement("complete");
            if (this.Request.StartIndex > 0)
                xWriter.WriteAttributeString("add", "true");
        }

    }
}
