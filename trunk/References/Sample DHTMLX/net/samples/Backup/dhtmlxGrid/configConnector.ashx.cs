using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using dhtmlxConnectors;
using System.Configuration;

namespace dhtmlxConnector.Net_Samples.dhtmlxGrid
{
    /// <summary>
    /// Connector body
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class configConnector : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            dhtmlxGridConnector connector = new dhtmlxGridConnector(
                "BookStore",
                "sales, title, author, price, instore, shipping, bestseller, pub_date",
                "book_id",
                dhtmlxDatabaseAdapterType.SqlServer2005,
                ConfigurationManager.ConnectionStrings["SamplesDatabase"].ConnectionString
            );
            connector.BeforeOutput += new EventHandler<RenderEventArgs>(connector_BeforeOutput);
            return connector;
        }

        void connector_BeforeOutput(object sender, RenderEventArgs e)
        {
            if (this.Connector.Request.StartIndex == 0)
            {
                e.Writer.WriteStartElement("head");
                {
                    //col 1
                    e.Writer.WriteStartElement("column");
                    {
                        e.Writer.WriteAttributeString("width", "50");
                        e.Writer.WriteAttributeString("type", "ed");
                        e.Writer.WriteAttributeString("align", "right");
                        e.Writer.WriteAttributeString("color", "white");
                        e.Writer.WriteAttributeString("sort", "na");
                        e.Writer.WriteString("Sales");
                    }
                    e.Writer.WriteEndElement();
                    //col 2
                    e.Writer.WriteStartElement("column");
                    {
                        e.Writer.WriteAttributeString("width", "150");
                        e.Writer.WriteAttributeString("type", "ed");
                        e.Writer.WriteAttributeString("align", "left");
                        e.Writer.WriteAttributeString("color", "#d5f1ff");
                        e.Writer.WriteAttributeString("sort", "na");
                        e.Writer.WriteString("Book Title");
                    }
                    e.Writer.WriteEndElement();
                    //col 3
                    e.Writer.WriteStartElement("column");
                    {
                        e.Writer.WriteAttributeString("width", "100");
                        e.Writer.WriteAttributeString("type", "ed");
                        e.Writer.WriteAttributeString("align", "left");
                        e.Writer.WriteAttributeString("color", "#d5f1ff");
                        e.Writer.WriteAttributeString("sort", "na");
                        e.Writer.WriteString("Author");
                    }
                    e.Writer.WriteEndElement();
                }
                e.Writer.WriteEndElement();

                /*
                    <head>
	                    <column width="50" type="ed" align="right" color="white" sort="na">Sales</column>
	                    <column width="150" type="ed" align="left" color="#d5f1ff" sort="na">Book Title</column>
	                    <column width="100" type="ed" align="left" color="#d5f1ff" sort="na">Author</column>
		            </head>
                */
            }
        }
    }
}
