using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Represents dhtmlxTreeGrid row
    /// </summary>
    public class dhtmlxTreeGridDataItem: dhtmlxGridDataItem
    {
        /// <summary>
        /// Ouputs content to response
        /// </summary>
        /// <param name="xWriter">XmlWriter object to render content to</param>
        protected override void RenderContent(System.Xml.XmlWriter xWriter)
        {
            xWriter.WriteStartElement("row");
            if (!string.IsNullOrEmpty(this.ID))
                xWriter.WriteAttributeString("id", this.ID);

            if (this.HasChildren)
                xWriter.WriteAttributeString("xmlkids", "1");
            
            //write custom attribs
            foreach (KeyValuePair<string, string> pair in this.CustomAttribs)
                xWriter.WriteAttributeString(pair.Key, pair.Value);

            int cellIndex = 0;
            foreach (KeyValuePair<string, string> pair in this.DataFields)
                if (!this.ExtraColumnNames.Contains(pair.Key))
                {
                    xWriter.WriteStartElement("cell");
                    if (cellIndex == 0 && !string.IsNullOrEmpty(this.Image))
                        xWriter.WriteAttributeString("image", this.Image);
                    xWriter.WriteCData(pair.Value);
                    xWriter.WriteEndElement();
                    cellIndex++;
                }

            foreach (dhtmlxTreeGridDataItem childItem in this.ChildRows)
                childItem.Render(xWriter);

            xWriter.WriteEndElement();
        }

        /// <summary>
        /// Gets or Sets value indicating either data item has children or not
        /// </summary>
        public bool HasChildren
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets picture name to insert into the first cell of row
        /// </summary>
        public string Image
        {
            get;
            set;
        }

        private List<dhtmlxTreeGridDataItem> _ChildRows = new List<dhtmlxTreeGridDataItem>();
        
        /// <summary>
        /// Gets reference to collection of child items
        /// </summary>
        public List<dhtmlxTreeGridDataItem> ChildRows
        {
            get
            {
                return this._ChildRows;
            }
        }


      
    }
}
