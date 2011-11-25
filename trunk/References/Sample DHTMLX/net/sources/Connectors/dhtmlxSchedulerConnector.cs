using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Serves dhtmlxConnector requests
    /// </summary>
    public class dhtmlxSchedulerConnector: dhtmlxConnector<dhtmlxSchedulerDataItem>
    {
        /// <summary>
        /// Gets or Sets field to take events' StartDate from
        /// </summary>
        public Field StartDateField
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets field to take events' FinishDate from
        /// </summary>
        public Field FinishDateField
        {
            get;
            set;
        }

        private dhtmlxFieldsCollection _DetailsFields = new dhtmlxFieldsCollection();
        /// <summary>
        /// Gets or Sets reference to collection of details fields that describe event
        /// </summary>
        public dhtmlxFieldsCollection DetailsFields
        {
            get
            {
                return this._DetailsFields;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("DetailsFields");
                this._DetailsFields = value;
            }
        }

        /// <summary>
        /// Initializes own fields
        /// </summary>
        /// <param name="DetailsColumnNames">Comma-delimited list of columns that holds additional information about event to select and include into response (nullable)</param>
        /// <param name="StartDateColumnName">StartDate column name(nullable)</param>
        /// <param name="FinishDateColumnName">FinishDate column name(nullable)</param>
        protected void Initialize(string DetailsColumnNames, string StartDateColumnName, string FinishDateColumnName)
        {
            //details fields parse
            if (!string.IsNullOrEmpty(DetailsColumnNames))
                foreach (string fieldName in DetailsColumnNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    this.DetailsFields.Add(this.Request.Adapter.ParseField(fieldName));
            //start date
            if (!string.IsNullOrEmpty(StartDateColumnName))
                this.StartDateField = this.Request.Adapter.ParseField(StartDateColumnName);
            //finish date
            if (!string.IsNullOrEmpty(FinishDateColumnName))
                this.FinishDateField = this.Request.Adapter.ParseField(FinishDateColumnName);
            this.Request.BeforeSelect += new EventHandler(Request_BeforeSelect);
        }

        /// <summary>
        /// Modifies DataRequest.Select method to reflect Scheduler specific parameters
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event args</param>
        void Request_BeforeSelect(object sender, EventArgs e)
        {
            //include start date if it wasn't included yet
            if (!this.Request.RequestedFields.Contains(this.StartDateField))
                this.Request.RequestedFields.Add(this.StartDateField);
            //include finish date if it wasn't included yet
            if (!this.Request.RequestedFields.Contains(this.FinishDateField))
                this.Request.RequestedFields.Add(this.FinishDateField);
            //merge requested fields with details fields
            this.Request.RequestedFields = this.Request.RequestedFields.Union(this.DetailsFields).Distinct().ToFieldsCollection();

            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["to"]) && this.FinishDateField != null)
                this.Request.Rules.Add(new FieldRule(this.FinishDateField, Operator.Lower, Convert.ToDateTime(HttpContext.Current.Request.QueryString["to"]).ToString(Tools.DateFormat)));

            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["from"]) && this.StartDateField != null)
                this.Request.Rules.Add(new FieldRule(this.StartDateField, Operator.Greater, Convert.ToDateTime(HttpContext.Current.Request.QueryString["from"]).ToString(Tools.DateFormat)));
        }

        /// <summary>
        /// Creates new instance of dhtmlxSchedulerConnector
        /// </summary>
        /// <param name="SelectSource">Select query or Table name to use for data retrieval</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="AdapterType">Type of adapter to use for communication with database engine</param>
        /// <param name="ConnectionString">ConnectionString for connection to database engine</param>
        /// <param name="StartDateColumnName">Field name to take events' start date from</param>
        /// <param name="FinishDateColumnName">Field name to take events' finish date from</param>
        /// <param name="DetailsColumnNames">Additional comma-delimited column names that describe event to select and include into response</param>
        public dhtmlxSchedulerConnector(string SelectSource, string PrimaryKeyColumnName, dhtmlxDatabaseAdapterType AdapterType, string ConnectionString, string StartDateColumnName, string FinishDateColumnName, string DetailsColumnNames)
        {
            if (SelectSource.Trim().StartsWith("SELECT", StringComparison.InvariantCultureIgnoreCase))
                this._Request = new DataRequest(this, SelectSource, PrimaryKeyColumnName, "", AdapterType, ConnectionString);
            else
                this._Request = new DataRequest(this, SelectSource,
                    string.Join(",", new string[] { PrimaryKeyColumnName, DetailsColumnNames, StartDateColumnName, FinishDateColumnName }.Where(col => !string.IsNullOrEmpty(col)).ToArray()),
                    PrimaryKeyColumnName, "", AdapterType, ConnectionString);
            ;
            this.Initialize(DetailsColumnNames, StartDateColumnName, FinishDateColumnName);
        }

        /// <summary>
        /// Creates new instance of dhtmlxSchedulerConnector
        /// </summary>
        /// <param name="SelectSource">Select query or Table name to use for data retrieval</param>
        /// <param name="PrimaryKeyColumnName">PrimaryKey column name (nullable)</param>
        /// <param name="Adapter">Adapter to use for communication with database engine</param>
        /// <param name="StartDateColumnName">Field name to take events' start date from</param>
        /// <param name="FinishDateColumnName">Field name to take events' finish date from</param>
        /// <param name="DetailsColumnNames">Additional comma-delimited column names that describe event to select and include into response</param>
        public dhtmlxSchedulerConnector(string SelectSource, string PrimaryKeyColumnName, IdhtmlxDatabaseAdapter Adapter, string StartDateColumnName, string FinishDateColumnName, string DetailsColumnNames)
        {
            if (SelectSource.Trim().StartsWith("SELECT", StringComparison.InvariantCultureIgnoreCase))
                this._Request = new DataRequest(this, SelectSource, PrimaryKeyColumnName, "", Adapter);
            else
                this._Request = new DataRequest(this, SelectSource,
                    string.Join(",", new string[] { PrimaryKeyColumnName, DetailsColumnNames, StartDateColumnName, FinishDateColumnName }.Where(col => !string.IsNullOrEmpty(col)).ToArray()),
                    PrimaryKeyColumnName, "", Adapter);
            ;
            this.Initialize(DetailsColumnNames, StartDateColumnName, FinishDateColumnName);
        }

        /// <summary>
        /// Writes begin tags of response header
        /// </summary>
        /// <param name="xWriter">XmlWriter to render content to</param>
        /// <param name="RowsToRender">Data to render</param>
        /// <param name="TotalRowsCount">Total amount of rows available</param>
        protected override void BeginRenderContent(System.Xml.XmlWriter xWriter, System.Data.DataTable RowsToRender, int TotalRowsCount)
        {
            xWriter.WriteStartElement("data");
        }


        /// <summary>
        /// Creates collection of dhtmlxSchedulerDataItem objects from DataTable provided
        /// </summary>
        /// <param name="Rows">Table to create dhtmlxSchedulerDataItem objects from</param>
        /// <returns>Collection of dhtmlxSchedulerDataItem objects</returns>
        protected override List<dhtmlxSchedulerDataItem> CreateDataItems(System.Data.DataTable Rows)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Converting DataRow object to connector specific data items");
#endif
            #endregion
            List<dhtmlxSchedulerDataItem> dataItems = new List<dhtmlxSchedulerDataItem>();
            for (int i = 0; i < Rows.Rows.Count; i++)
            {
                DataRow row = Rows.Rows[i];
                dhtmlxSchedulerDataItem dataItem = new dhtmlxSchedulerDataItem();
                if (this.Request.PrimaryKeyField != null)
                    dataItem.ID = Convert.ToString(row[this.Request.PrimaryKeyField.ExternalName]);
                
                string[] excludeColls = new string[]{
                    (this.Request.PrimaryKeyField == null ? "" : this.Request.PrimaryKeyField.ExternalName), 
                    (this.StartDateField == null ? "" : this.StartDateField.ExternalName), 
                    (this.FinishDateField == null ? "" : this.FinishDateField.ExternalName) 
                };
                foreach (DataColumn col in Rows.Columns.Cast<DataColumn>().Where(c => !excludeColls.Contains(c.ColumnName)))
                    dataItem.DataFields.Add(col.ColumnName, Tools.ConvertToString(row[col]));
                
                if (this.StartDateField != null)
                {
                    DateTime startDate;
                    if (DateTime.TryParse(Convert.ToString(row[this.StartDateField.ExternalName]), out startDate))
                        dataItem.StartDate = startDate;
                }
                if (this.FinishDateField != null)
                {
                    DateTime finishDate;
                    if (DateTime.TryParse(Convert.ToString(row[this.FinishDateField.ExternalName]), out finishDate))
                        dataItem.FinishDate = finishDate;
                }
                dataItem.Index = i;

                dataItems.Add(dataItem);
            }
            return dataItems;
        }

        //TODO: Check how tree update works with default parent id = null

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
            switch(encField)
            {
                case "start_date":
                    return this.StartDateField;
                case "end_date":
                    return this.FinishDateField;
                case "id":
                    return this.Request.PrimaryKeyField;
                default:
                    if (this.DetailsFields.Contains(encField))
                        return this.DetailsFields[encField];
                    else
                        return null;
            }
        }

    }
}
