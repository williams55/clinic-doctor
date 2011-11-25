using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Data item for dhtmlxComboConnector
    /// </summary>
    public class dhtmlxComboItem: dhtmlxDataItem
    {
        /// <summary>
        /// Gets or Sets value used for option identification (the same as ID)
        /// </summary>
        public string Value
        {
            get
            {
                return this.ID;
            }
            set
            {
                this.ID = value; ;
            }
        }

        /// <summary>
        /// Gets or Sets item's visible text
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets selected state of the item
        /// </summary>
        public bool Selected
        {
            get;
            set;
        }

        /// <summary>
        /// Renders item into response stream provided
        /// </summary>
        /// <param name="xWriter">XmlWriter to use for output</param>
        protected override void RenderContent(System.Xml.XmlWriter xWriter)
        {
            xWriter.WriteStartElement("option");
            if (!string.IsNullOrEmpty(this.Value))
                xWriter.WriteAttributeString("value", this.Value);
            if (this.Selected)
                xWriter.WriteAttributeString("selected", "true");
            xWriter.WriteCData(this.Text);
            xWriter.WriteEndElement();
        }
    }
}
