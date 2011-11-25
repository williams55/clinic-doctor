using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Represents scheduler event
    /// </summary>
    public class dhtmlxSchedulerDataItem: dhtmlxDataItem
    {
        /// <summary>
        /// Gets or Sets event start date
        /// </summary>
        public DateTime? StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets event finish date
        /// </summary>
        public DateTime? FinishDate
        {
            get;
            set;
        }

        /// <summary>
        /// Outputs content into XmlWriter
        /// </summary>
        /// <param name="xWriter">XmlWriter to render</param>
        protected override void RenderContent(System.Xml.XmlWriter xWriter)
        {
            xWriter.WriteStartElement("event");
            //id
            if (!string.IsNullOrEmpty(this.ID))
                xWriter.WriteAttributeString("id", this.ID);
            //custom attribs
            foreach (KeyValuePair<string, string> entry in this.CustomAttribs)
                xWriter.WriteAttributeString(entry.Key, entry.Value);
            //start date
            xWriter.WriteStartElement("start_date");
            if (this.StartDate.HasValue)
                xWriter.WriteCData(this.StartDate.Value.ToString(Tools.DateFormat));
            xWriter.WriteEndElement();
            //end_date
            xWriter.WriteStartElement("end_date");
            if (this.FinishDate.HasValue)
                xWriter.WriteCData(this.FinishDate.Value.ToString(Tools.DateFormat));
            xWriter.WriteEndElement();
            
            //details fields
            foreach (string fieldName in this.DataFields.Keys)
            {
                xWriter.WriteStartElement(fieldName);
                xWriter.WriteCData(this.DataFields[fieldName]);
                xWriter.WriteEndElement();
            }

            xWriter.WriteEndElement();
        }
    }
}
