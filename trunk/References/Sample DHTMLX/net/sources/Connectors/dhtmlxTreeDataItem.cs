using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Represents node of dhtmlxTree connector
    /// </summary>
    public class dhtmlxTreeDataItem: dhtmlxDataItem
    {
        /// <summary>
        /// Gets or Sets value that identifies this item (is absolutely the same as ID property)
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
        /// Gets or Sets node text
        /// </summary>
        public string Text
        {
            get
            {
                return GetAttribValue("text");
            }
            set
            {
                SetAttribValue("text", value);
            }
        }

        /// <summary>
        /// Gets or Sets image name to be applied if this item has no children
        /// </summary>
        public string LeafImage
        {
            get
            {
                return GetAttribValue("im0");
            }
            set
            {
                SetAttribValue("im0", value);
            }
        }

        /// <summary>
        /// Gets or Sets image name to be applied when this node is collapsed and has children
        /// </summary>
        public string FolderClosedImage
        {
            get
            {
                return GetAttribValue("im2");
            }
            set
            {
                SetAttribValue("im2", value);
            }
        }

        /// <summary>
        /// Gets or Sets image name to be applied when this node is expanded and has children
        /// </summary>
        public string FolderOpenedImage
        {
            get
            {
                return GetAttribValue("im1");
            }
            set
            {
                SetAttribValue("im1", value);
            }
        }

        /// <summary>
        /// Gets or Sets checked state of an item
        /// </summary>
        public bool Checked
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets value indicating whether this item has child items (makes sense in case of EnableDynamicLoading mode enabled in connector)
        /// </summary>
        public bool HasChildren
        {
            get
            {
                return GetAttribValue("child") == "1";
            }
            set
            {
                SetAttribValue("child", value ? "1" : "0");
            }
        }

        private List<dhtmlxTreeDataItem> _ChildNodes = new List<dhtmlxTreeDataItem>();
        
        /// <summary>
        /// Gets reference to collection of child nodes
        /// </summary>
        public List<dhtmlxTreeDataItem> ChildNodes
        {
            get
            {
                return this._ChildNodes;
            }
        }


        /// <summary>
        /// Outputs own content into XmlWriter
        /// </summary>
        /// <param name="xWriter">XmlWriter to render content to</param>
        protected override void RenderContent(System.Xml.XmlWriter xWriter)
        {
            xWriter.WriteStartElement("item");
            xWriter.WriteAttributeString("id", Convert.ToString(this.ID));
            foreach (KeyValuePair<string, string> pair in this.CustomAttribs)
                xWriter.WriteAttributeString(pair.Key, pair.Value);
            if (this.Checked)
                xWriter.WriteAttributeString("check", "1");

            foreach (dhtmlxTreeDataItem childItem in this.ChildNodes)
            {
                if (this.HasPrerender)
                    childItem.Prerender += delegate(object sender, EventArgs e) { this.CallPrerender(childItem, new ItemPrerenderEventArgs<dhtmlxTreeDataItem>(childItem)); };
                childItem.Render(xWriter);
            }
            xWriter.WriteEndElement();
        }
    }
}
