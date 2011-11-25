using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Provides base functionality to all grid-oriented connectors
    /// </summary>
    /// <typeparam name="T">Type of grid element</typeparam>
    public abstract class dhtmlxGridConnectorBase<T> : dhtmlxConnector<T>
        where T: dhtmlxDataItem
    {
        /// <summary>
        /// Creates new instance of dhtmlxGridConnectorBase
        /// </summary>
        public dhtmlxGridConnectorBase()
        {
            this.BeforeSelect += new EventHandler(dhtmlxGridConnectorBase_BeforeSelect);
        }

        /// <summary>
        /// Attach BeforeSelect hook in order to make sure that ExtraFields are also included into request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dhtmlxGridConnectorBase_BeforeSelect(object sender, EventArgs e)
        {
            this.Request.RequestedFields = this.Request.RequestedFields.Union(this.ExtraFields).ToFieldsCollection();
        }

        private Dictionary<Field, dhtmlxOptionsConnector> _OptionsConnectors = new Dictionary<Field, dhtmlxOptionsConnector>();
        
        /// <summary>
        /// Gets rederence to list of dhtmlxOptionsConnector need for rendering comboboxes in grid columns/headers
        /// </summary>
        public Dictionary<Field, dhtmlxOptionsConnector> OptionsConnectors
        {
            get
            {
                return this._OptionsConnectors;
            }
        }

        private dhtmlxFieldOptionsCollection _ExplicitOptions = new dhtmlxFieldOptionsCollection();

        /// <summary>
        /// Gets reference to collection of hardcoded options lists to be rendered as dropdown lists in grid columns/headers
        /// </summary>
        public dhtmlxFieldOptionsCollection ExplicitOptions
        {
            get
            {
                return this._ExplicitOptions;
            }
        }

        //private List<string> _FieldsToRender = null;
        
        ///// <summary>
        ///// Gets or Sets reference to collection of field names for being rendered. By default it includes fields names specified in Request.RequestedFields
        ///// </summary>
        //public List<string> FieldsToRender
        //{
        //    get
        //    {
        //        if (this._FieldsToRender == null)
        //            this._FieldsToRender = this.Request.RequestedFields.Select(f => f.ExternalName).ToList();
        //        return this._FieldsToRender;
        //    }
        //    set
        //    {
        //        if (value == null)
        //            throw new ArgumentNullException("FieldsToRender");
        //        this._FieldsToRender = value;
        //    }
        //}

        ///// <summary>
        ///// Replaces default collection of FieldsToRender with one provided
        ///// </summary>
        ///// <param name="FieldNames">Comma-delimited column names to render</param>
        //public void SetFieldsToRender(string FieldNames)
        //{
        //    if (string.IsNullOrEmpty(FieldNames))
        //        throw new ArgumentException("Argument cannot be null or empty", "FieldNames");
        //    this.FieldsToRender.Clear();
        //    foreach (string fieldName in FieldNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //        this.FieldsToRender.Add(fieldName);
        //}

        /// <summary>
        /// Writes end tags to response header
        /// </summary>
        /// <param name="xWriter">XmlWriter to write response to</param>
        /// <param name="RowsToRender">Rows to render</param>
        /// <param name="TotalRowsCount">Total rows count</param>
        protected override void EndRenderContent(XmlWriter xWriter, DataTable RowsToRender, int TotalRowsCount)
        {
            foreach (Field optionsField in this.OptionsConnectors.Keys)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Adding combo-options into response");
#endif
                #endregion
                int index = this.GetFieldIndex(optionsField);
                if (index != -1)
                {
                    dhtmlxOptionsConnector currentConnector = this.OptionsConnectors[optionsField];
                    currentConnector.ColumnIndex = index;
                    currentConnector.Render(xWriter);
                }
            }
            foreach (Field optionsField in this.ExplicitOptions.Keys)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Adding explicit combo-options into response");
#endif
                #endregion
                if (this.OptionsConnectors.ContainsKey(optionsField))
                    throw new dhtmlxException(optionsField.ExternalName + " column is contained both in OptionsConnectors and ExplicitOptions collections!");
                int index = this.GetFieldIndex(optionsField);
                if (index != -1)
                    dhtmlxOptionsConnector.RenderCustomCollection(xWriter, index, this.ExplicitOptions[optionsField]);
            }

            xWriter.WriteEndElement();
        }

        /// <summary>
        /// Gets field index by object
        /// </summary>
        /// <param name="field">Field to find index for</param>
        /// <returns>Field index or -1 if not found</returns>
        private int GetFieldIndex(Field field)
        {
            for (int i = 0; i < this.Request.RequestedFields.Count; i++)
                if (this.Request.RequestedFields[i].ExternalName == field.ExternalName)
                    return i;
            return -1;
        }


        private dhtmlxFieldsCollection _ExtraFields = new dhtmlxFieldsCollection();

        /// <summary>
        /// Gets reference to collection of Field for being requested but not rendered
        /// </summary>
        public dhtmlxFieldsCollection ExtraFields
        {
            get
            {
                return this._ExtraFields;
            }
        }

        /// <summary>
        /// Parses columns and put them into ExtraFields collection
        /// </summary>
        /// <param name="ExtraColumnNames">SQL string that contains extra column names</param>
        protected void ParseExtraColumns(string ExtraColumnNames)
        {
            if (!string.IsNullOrEmpty(ExtraColumnNames))
                foreach (string ColumnExpression in ExtraColumnNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    this.ExtraFields.Add(this.Request.Adapter.ParseField(ColumnExpression));
        }
    }
}
