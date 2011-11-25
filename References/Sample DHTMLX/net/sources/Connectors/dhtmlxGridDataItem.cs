using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Represents grid row
    /// </summary>
    public class dhtmlxGridDataItem: dhtmlxDataItem
    {
        /// <summary>
        /// Gets or Sets custom css class name to apply to the row
        /// </summary>
        public string CssClass
        {
            get
            {
                return GetAttribValue("class");
            }
            set
            {
                SetAttribValue("class", value);
            }
        }

        /// <summary>
        /// Gets or Sets background color of the row
        /// </summary>
        public string BgColor
        {
            get
            {
                return GetAttribValue("bgColor");
            }
            set
            {
                SetAttribValue("bgColor", value);
            }
        }

        /// <summary>
        /// Gets or Sets custom style attributes
        /// </summary>
        public string Style
        {
            get
            {
                return GetAttribValue("style");
            }
            set
            {
                SetAttribValue("style", value);
            }
        }

        private List<string> _ExtraColumnNames = null;
        /// <summary>
        /// Gets or Sets reference to collection column names to be ignored during render event
        /// </summary>
        protected List<string> ExtraColumnNames
        {
            get
            {
                if (this._ExtraColumnNames == null)
                    this._ExtraColumnNames = new List<string>();
                return this._ExtraColumnNames;
            }
            set
            {
                this._ExtraColumnNames = value;
            }
        }

        /// <summary>
        /// Sets column names to be ignored during render event
        /// </summary>
        /// <param name="ExtraColumnNames">Column names collection</param>
        internal protected void SetExtraColumns(List<string> ExtraColumnNames)
        {
            this.ExtraColumnNames = ExtraColumnNames;
        }

        /// <summary>
        /// Outputs item into response writer
        /// </summary>
        /// <param name="xWriter">XmlWriter to put response to</param>
        protected override void RenderContent(System.Xml.XmlWriter xWriter)
        {
            xWriter.WriteStartElement("row");
            //id
            if (!string.IsNullOrEmpty(this.ID))
                xWriter.WriteAttributeString("id", this.ID);
            //custom attribs
            foreach(KeyValuePair<string,string> entry in this.CustomAttribs)
                xWriter.WriteAttributeString(entry.Key, entry.Value);
            //column cells
            foreach (string colName in this.DataFields.Keys)
                if (!this.ExtraColumnNames.Contains(colName))
                {
                    xWriter.WriteStartElement("cell");
                    xWriter.WriteCData(this.DataFields[colName]);
                    xWriter.WriteEndElement();
                }
            xWriter.WriteEndElement();
        }
    }
}
