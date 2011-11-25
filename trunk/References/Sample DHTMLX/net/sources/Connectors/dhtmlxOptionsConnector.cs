using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Connector that serves dhtmlxGrid(treeGrid) combobox requests
    /// </summary>
    public class dhtmlxOptionsConnector : dhtmlxComboConnector
    {
        /// <summary>
        /// Gets or Sets column index where combobox will be put
        /// </summary>
        public int ColumnIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Creates new instance of dhtmlxOptionsConnector
        /// </summary>
        /// <param name="SelectSource">Select query or Table name to use for data retrieval</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="ItemTextColumnName">Column name to take option's text from</param>
        /// <param name="AdapterType">Type of adapter to use for communication with database engine</param>
        /// <param name="ConnectionString">ConnectionString for connection to database engine</param>
        public dhtmlxOptionsConnector(string SelectSource, string PrimaryKeyColumnName, dhtmlxDatabaseAdapterType AdapterType, string ConnectionString, string ItemTextColumnName)
            : base(SelectSource, PrimaryKeyColumnName, AdapterType, ConnectionString, ItemTextColumnName)
        {
        }

        /// <summary>
        /// Creates new instance of dhtmlxOptionsConnector
        /// </summary>
        /// <param name="SelectSource">Select query or Table name to use for data retrieval</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="ItemTextColumnName">Column name to take option's text from</param>
        /// <param name="Adapter">Adapter to use for communication with database engine</param>
        public dhtmlxOptionsConnector(string SelectSource, string PrimaryKeyColumnName, IdhtmlxDatabaseAdapter Adapter, string ItemTextColumnName)
            : base(SelectSource, PrimaryKeyColumnName, Adapter, ItemTextColumnName)
        {
        }

        /// <summary>
        /// Creates new instance of dhtmlxOptionsConnector
        /// </summary>
        /// <param name="SelectSource">Select query or Table name to use for data retrieval</param>
        /// <param name="ItemTextColumnName">Column name to take option's text from</param>
        /// <param name="AdapterType">Type of adapter to use for communication with database engine</param>
        /// <param name="ConnectionString">ConnectionString for connection to database engine</param>
        public dhtmlxOptionsConnector(string SelectSource, dhtmlxDatabaseAdapterType AdapterType, string ConnectionString, string ItemTextColumnName)
            : base(SelectSource, "", AdapterType, ConnectionString, ItemTextColumnName)
        {
        }

        /// <summary>
        /// Creates new instance of dhtmlxOptionsConnector
        /// </summary>
        /// <param name="SelectSource">Select query or Table name to use for data retrieval</param>
        /// <param name="ItemTextColumnName">Column name to take option's text from</param>
        /// <param name="Adapter">Adapter to use for communication with database engine</param>
        public dhtmlxOptionsConnector(string SelectSource, IdhtmlxDatabaseAdapter Adapter, string ItemTextColumnName)
            : base(SelectSource, "", Adapter, ItemTextColumnName)
        {
        }

      

        /// <summary>
        /// Creates collection of dhtmlxComboItem objects from DataTable provided
        /// </summary>
        /// <param name="Rows">Table to create dhtmlxComboItem objects from</param>
        /// <returns>Collection of dhtmlxComboItem objects</returns>
        protected override List<dhtmlxComboItem> CreateDataItems(System.Data.DataTable Rows)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Converting DataRow object to connector specific data items");
#endif
            #endregion
            List<dhtmlxComboItem> Items = base.CreateDataItems(Rows);
            List<dhtmlxComboItem> options = new List<dhtmlxComboItem>();
            foreach (dhtmlxComboItem item in Items)
                if (this.Request.PrimaryKeyField == null)
                    options.Add(new dhtmlxOptionsItem(item.Text, item.Index));
                else
                    options.Add(new dhtmlxOptionsItem(item.Value, item.Text, item.Index));
            return options;
        }

        /// <summary>
        /// Writes begin tags of response header
        /// </summary>
        /// <param name="xWriter">XmlWriter to render content to</param>
        /// <param name="RowsToRender">Data to render</param>
        /// <param name="TotalRowsCount">Total amount of rows available</param>
        protected override void BeginRenderContent(System.Xml.XmlWriter xWriter, System.Data.DataTable RowsToRender, int TotalRowsCount)
        {
            xWriter.WriteStartElement("coll_options");
            xWriter.WriteAttributeString("for", this.ColumnIndex.ToString());
        }

        /// <summary>
        /// Renders collection of dhtmlxOptionsItem objects like it was own select result
        /// </summary>
        /// <param name="xWriter">XmlWriter to write collection to</param>
        /// <param name="ColumnIndex">Column index to put items to</param>
        /// <param name="Options">Collection of options to render</param>
        public static void RenderCustomCollection(XmlWriter xWriter, int ColumnIndex, ICollection<dhtmlxOptionsItem> Options)
        {
            xWriter.WriteStartElement("coll_options");
            xWriter.WriteAttributeString("for", ColumnIndex.ToString());

            foreach (var Option in Options)
                Option.Render(xWriter);

            xWriter.WriteEndElement();
        }
    }
}
