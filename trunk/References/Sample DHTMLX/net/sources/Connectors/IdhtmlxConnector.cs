using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using System.Xml;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Provides basic set of methods common for all dhtmlx connectors
    /// </summary>
    public interface IdhtmlxConnector
    {
        /// <summary>
        /// Gets reference to instance of object that process current request
        /// </summary>
        DataRequest Request
        {
            get;
        }

        /// <summary>
        /// Process data and commands came from the client
        /// </summary>
        /// <param name="QueryString">QueryString collection of current request</param>
        /// <param name="Form">Form collection of current request</param>
        void ProcessRequest(NameValueCollection QueryString, NameValueCollection Form);

        /// <summary>
        /// Renders commands result into XmlWriter
        /// </summary>
        /// <param name="xWriter">XmlWriter to render connector to</param>
        void Render(XmlWriter xWriter);

        /// <summary>
        /// Renders commands result into HttpResponse
        /// </summary>
        /// <param name="response">HttpResponse instance to render connector to</param>
        void RenderResponse(HttpResponse response);

        /// <summary>
        /// Renders specified DataTable using connector-specific format into XmlWriter
        /// </summary>
        /// <param name="xWriter">XmlWriter to use for rendering</param>
        /// <param name="RowsToRender">DataTable object to render</param>
        /// <param name="TotalRowsCount">Total rows count in case if RowsToRender contains only portion of data available</param>
        void RenderData(XmlWriter xWriter, System.Data.DataTable RowsToRender, int TotalRowsCount);

        /// <summary>
        /// Render executed actions result into response
        /// </summary>
        /// <param name="xWriter">XmlWriter to render result to</param>
        /// <param name="ResultsToRender">Collection of executed actions to render</param>
        void RenderActions(XmlWriter xWriter, IEnumerable<DataAction> ResultsToRender);

        /// <summary>
        /// Decodes field name to Field object
        /// </summary>
        /// <param name="EncodedField">Encoded field name token from QueryString</param>
        /// <returns>Field object that corresponds EncodedField, or null</returns>
        Field DecodeField(string EncodedField);
    }
}
