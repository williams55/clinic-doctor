
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Globalization;

namespace AppointmentSystem.Settings.Settings
{
    public class CustomXmlSerializable : CustomSerializable, ICustomXmlSerializable
    {
        #region ICustomXmlSerializable Members
        public void Deserialize(string str)
        {
            using (TextReader textReader = new StringReader(str))
            using (XmlReader xmlReader = new XmlTextReader(textReader))
            {
                xmlReader.ReadStartElement();

                xmlReader.ReadStartElement();
                this.Id = new Guid(xmlReader.ReadContentAsString());
                xmlReader.ReadEndElement();

                xmlReader.ReadStartElement();
                this.Value1 = xmlReader.ReadContentAsInt();
                xmlReader.ReadEndElement();

                xmlReader.ReadStartElement();
                this.Value2 = xmlReader.ReadContentAsDateTime();
                xmlReader.ReadEndElement();

                xmlReader.ReadStartElement();
                this.Value3 = xmlReader.ReadContentAsString();
                xmlReader.ReadEndElement();

                xmlReader.ReadStartElement();
                this.Value4 = xmlReader.ReadContentAsDecimal();
                xmlReader.ReadEndElement();

                xmlReader.ReadEndElement();
            }
        }
        public string Serialize()
        {
            StringBuilder result = new StringBuilder(100);

            using (TextWriter textWriter = new StringWriter(result, CultureInfo.InvariantCulture))
            using (XmlWriter xmlWriter = new XmlTextWriter(textWriter))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("CustomXmlSerializable");

                xmlWriter.WriteStartElement("Id");
                xmlWriter.WriteValue(this.Id.ToString());
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Value1");
                xmlWriter.WriteValue(this.Value1);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Value2");
                xmlWriter.WriteValue(this.Value2);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Value3");
                xmlWriter.WriteValue(this.Value3);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Value4");
                xmlWriter.WriteValue(this.Value4);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            return result.ToString();
        }
        #endregion
    }
}