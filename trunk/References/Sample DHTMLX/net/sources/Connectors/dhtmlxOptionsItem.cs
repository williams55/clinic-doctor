using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Represents dhtmlxGrid(treeGrid) combobox item
    /// </summary>
    public class dhtmlxOptionsItem: dhtmlxComboItem
    {
        /// <summary>
        /// Gets or Sets value indicating whether to include Value into response
        /// </summary>
        protected bool UseTextOnly
        {
            get;
            set;
        }

        /// <summary>
        /// Creates new instance of dhtmlxOptionsItem
        /// </summary>
        /// <param name="Value">Option's value</param>
        /// <param name="Text">Option's text</param>
        /// <param name="Index">Option's position</param>
        internal protected dhtmlxOptionsItem(string Value, string Text, int Index)
        {
            this.Value = Value;
            this.Text = Text;
            this.Index = Index;
            this.UseTextOnly = false;
        }

        /// <summary>
        /// Creates new instance of dhtmlxOptionsItem
        /// </summary>
        /// <param name="Text">Option's text</param>
        /// <param name="Index">Option's position</param>
        internal protected dhtmlxOptionsItem(string Text, int Index)
        {
            this.Text = Text;
            this.Index = Index;
            this.UseTextOnly = true;
        }

        /// <summary>
        /// Creates new instance of dhtmlxOptionsItem
        /// </summary>
        /// <param name="Value">Option's value</param>
        /// <param name="Text">Option's text</param>
        public dhtmlxOptionsItem(string Value, string Text)
            :this(Value, Text, -1)
        { 
        }

        /// <summary>
        /// Creates new instance of dhtmlxOptionsItem
        /// </summary>
        /// <param name="Text">Option's value</param>
        public dhtmlxOptionsItem(string Text)
            : this(Text, -1)
        {
        }

        /// <summary>
        /// Outputs content into XmlWriter
        /// </summary>
        /// <param name="xWriter">XmlWriter to render</param>
        protected override void RenderContent(System.Xml.XmlWriter xWriter)
        {
            xWriter.WriteStartElement("item");
            if (this.UseTextOnly)
            {
                xWriter.WriteAttributeString("value", this.Text);
            }
            else
            {
                xWriter.WriteAttributeString("value", this.Value);
                xWriter.WriteAttributeString("label", this.Text);
            } 
            xWriter.WriteEndElement();
        }
    }
}
