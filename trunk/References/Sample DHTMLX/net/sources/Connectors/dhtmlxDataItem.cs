using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Represents single data item of dhtmlxConnector result
    /// </summary>
    public abstract class dhtmlxDataItem
    {
        private int _Index;
        
        /// <summary>
        /// Gets or Sets item index in the list of other items
        /// </summary>
        public int Index
        {
            get
            {
                return this._Index;
            }
            set
            {
                this._Index = value;
            }
        }

        /// <summary>
        /// Gets or Sets value indicating that data item will not be rendered(Prerender event will be caused anyway)
        /// </summary>
        public bool Skip
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets value that identifies this item
        /// </summary>
        public string ID
        {
            get;
            set;
        }

        private Dictionary<string, string> _DataFields = new Dictionary<string,string>();
        
        /// <summary>
        /// Gets reference to collection of Key-Value pairs that represents data fields of this item
        /// </summary>
        public Dictionary<string, string> DataFields
        {
            get
            {
                return this._DataFields;
            }
        }

        /// <summary>
        /// Gets custom attribute value by its name. If attribute doesn't persist - empty string will be returned
        /// </summary>
        /// <param name="AttribName">Attribute name</param>
        /// <returns>Attribute value or empty string</returns>
        protected string GetAttribValue(string AttribName)
        {
            if (this.CustomAttribs.ContainsKey(AttribName))
                return this.CustomAttribs[AttribName];
            return "";
        }

        /// <summary>
        /// Sets attribute value by its name
        /// </summary>
        /// <param name="AttribName">Attribute name</param>
        /// <param name="AttribValue">Attribute value to set</param>
        protected void SetAttribValue(string AttribName, string AttribValue)
        {
            this.CustomAttribs[AttribName] = AttribValue;
        }

        private Dictionary<string, string> _CustomAttribs = new Dictionary<string, string>();

        /// <summary>
        /// Gets reference to collection of custom attributes to be rendered into response
        /// </summary>
        protected Dictionary<string, string> CustomAttribs
        {
            get
            {
                return this._CustomAttribs;
            }
        }

        /// <summary>
        /// Renders data item into XmlWriter
        /// </summary>
        /// <param name="xWriter">XmlWriter to write response to</param>
        public void Render(XmlWriter xWriter)
        {
            if (this.Prerender != null)
                this.Prerender(this, EventArgs.Empty);
            if (this.Skip)
                return;
            this.RenderContent(xWriter);
        }

        /// <summary>
        /// Renders item content into XmlWriter
        /// </summary>
        /// <param name="xWriter">XmlWriter to write response to</param>
        protected abstract void RenderContent(XmlWriter xWriter);

        /// <summary>
        /// Event that is called before item writes its content into response
        /// </summary>
        public event EventHandler Prerender;
        
        /// <summary>
        /// Gets value indicating either this data item has Prerender event attached
        /// </summary>
        protected internal bool HasPrerender
        {
            get
            {
                return this.Prerender != null;
            }
        }

        /// <summary>
        /// Calls prerender event
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event argumens</param>
        protected internal void CallPrerender(object sender, EventArgs e)
        {
            if (this.Prerender != null)
                this.Prerender(sender, e);
        }
    }
}
