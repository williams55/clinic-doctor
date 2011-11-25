using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Xml;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Represents basic connector functionality 
    /// </summary>
    /// <typeparam name="T">Type of item connector will operate</typeparam>
    public abstract class dhtmlxConnector<T>: IdhtmlxConnector
        where T: dhtmlxDataItem
    {
        /// <summary>
        /// Reference to DataRequest object
        /// </summary>
        protected DataRequest _Request = null;
        
        /// <summary>
        /// Gets reference to DataRequest object that is responsible for requests handling
        /// </summary>
        public virtual DataRequest Request
        {
            get
            {
                return this._Request;
            }
        }

        /// <summary>
        /// Processes client commands token from QueryString and Form collections
        /// </summary>
        /// <param name="QueryString">QueryString collection of client request</param>
        /// <param name="Form">Form collection of client request</param>
        public virtual void ProcessRequest(NameValueCollection QueryString, NameValueCollection Form)
        {
            if (this.Begin != null)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Calling Begin event");
#endif
                #endregion
                this.Begin(this, EventArgs.Empty);
            }
            this.AttachProxyEvents();
            this.Request.ProcessRequest(QueryString, Form);
        }

        /// <summary>
        /// Add event listeners to DataRequest events to make their easy access by own events system
        /// </summary>
        private void AttachProxyEvents()
        {
            this.Request.BeforeDataActionProcessing += delegate(object sender, DataActionProcessingEventArgs e) { if (this.BeforeDataActionProcessing != null) this.BeforeDataActionProcessing(this, e); };
            this.Request.DataActionProcessed += delegate(object sender, DataActionProcessingEventArgs e) { if (this.DataActionProcessed != null) this.DataActionProcessed(this, e); };
            this.Request.BeforeInsert += delegate(object sender, DataActionProcessingEventArgs e) { if (this.BeforeInsert != null) this.BeforeInsert(this, e); };
            this.Request.Inserted += delegate(object sender, DataActionProcessingEventArgs e) { if (this.Inserted != null) this.Inserted(this, e); };
            this.Request.BeforeUpdate += delegate(object sender, DataActionProcessingEventArgs e) { if (this.BeforeUpdate != null) this.BeforeUpdate(this, e); };
            this.Request.Updated += delegate(object sender, DataActionProcessingEventArgs e) { if (this.Updated != null) this.Updated(this, e); };
            this.Request.BeforeDelete += delegate(object sender, DataActionProcessingEventArgs e) { if (this.BeforeDelete != null) this.BeforeDelete(this, e); };
            this.Request.Deleted += delegate(object sender, DataActionProcessingEventArgs e) { if (this.Deleted != null) this.Deleted(this, e); };
            this.Request.BeforeSelect += delegate(object sender, EventArgs e) { if (this.BeforeSelect != null) this.BeforeSelect(this, e); };
            this.Request.Selected += delegate(object sender, DataSelectedEventArgs e) { if (this.Selected != null) this.Selected(this, e); };
        }

        /// <summary>
        /// Renders processing results into current response
        /// </summary>
        /// <param name="response">HttpResponse object where to put results to</param>
        public virtual void RenderResponse(HttpResponse response)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Rendering response");
#endif
            #endregion
            this.RenderResponseHeader(response);
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            XmlWriter xWriter = XmlWriter.Create(sb, settings);
            this.Render(xWriter);
            xWriter.Close();
            response.Write(sb.ToString());
        }

        /// <summary>
        /// Renders processing results into XmlWriter
        /// </summary>
        /// <param name="xWriter">XmlWriter object where to put results to</param>
        public virtual void Render(XmlWriter xWriter)
        {
            this.Request.Render(xWriter);
        }

        /// <summary>
        /// Renders and prepares xml specific headers
        /// </summary>
        /// <param name="response">HttpResponse object to use</param>
        protected virtual void RenderResponseHeader(HttpResponse response)
        {
            response.ContentType = "text/xml";
            response.Write("<?xml version=\"1.0\" encoding=\"" + response.HeaderEncoding.BodyName + "\"?>");
        }

        /// <summary>
        /// Renders DataTable object into XmlWriter using connector-specific format
        /// </summary>
        /// <param name="xWriter">XmlWriter object to use for rendering</param>
        /// <param name="RowsToRender">Data to render</param>
        /// <param name="TotalRowsCount">Total rows count in case if RowsToRender doesn't contain all data</param>
        public void RenderData(XmlWriter xWriter, System.Data.DataTable RowsToRender, int TotalRowsCount)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Rendering 'Select' result");
#endif
            #endregion
            this.BeginRenderContent(xWriter, RowsToRender, TotalRowsCount);
            if (this.BeforeOutput != null)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Calling BeforeOutput event");
#endif
                #endregion
                this.BeforeOutput(this, new RenderEventArgs(xWriter));
            }
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Rendering children");
#endif
            #endregion
            this.RenderChildren(xWriter, this.CreateDataItems(RowsToRender));
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Closing response");
#endif
            #endregion
            this.EndRenderContent(xWriter, RowsToRender, TotalRowsCount);

            if (this.End != null)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Calling End event");
#endif
                #endregion
                this.End(this, EventArgs.Empty);
            }
        }


        /// <summary>
        /// Writes begin tags of response header
        /// </summary>
        /// <param name="xWriter">XmlWriter to render content to</param>
        /// <param name="RowsToRender">Data to render</param>
        /// <param name="TotalRowsCount">Total amount of rows available</param>
        protected abstract void BeginRenderContent(XmlWriter xWriter, DataTable RowsToRender, int TotalRowsCount);

        /// <summary>
        /// Creates collection of connector specific data items from DataTable provided
        /// </summary>
        /// <param name="Rows">DataTable to create items from</param>
        /// <returns>Collection of connector specific data items</returns>
        protected abstract List<T> CreateDataItems(DataTable Rows);
        
        /// <summary>
        /// Renders children (data items) into XmlWriter provided
        /// </summary>
        /// <param name="xWriter">XmlWriter to render response to</param>
        /// <param name="dataItems">Data items to render</param>
        protected virtual void RenderChildren(XmlWriter xWriter, List<T> dataItems)
        {
            foreach (T dataItem in dataItems)
            {
                if (this.ItemPrerender != null)
                    dataItem.Prerender += delegate(object sender, EventArgs e) { this.ItemPrerender(this, new ItemPrerenderEventArgs<T>(sender as T)); };
                dataItem.Render(xWriter);
            }
        }

        /// <summary>
        /// Renders results of data actions into XmlWriter provided
        /// </summary>
        /// <param name="xWriter">XmlWriter to render response to</param>
        /// <param name="ResultsToRender">Collection of DataAction object to render</param>
        public void RenderActions(XmlWriter xWriter, IEnumerable<DataAction> ResultsToRender)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Rendering data actions: [" + Tools.Join(ResultsToRender, ", ") + "]");
#endif
            #endregion
            xWriter.WriteStartElement("data");
            
            if (this.BeforeOutput != null)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Calling BeforeOutput event");
#endif
                #endregion
                this.BeforeOutput(this, new RenderEventArgs(xWriter));
            }

            foreach (DataAction actionResult in ResultsToRender)
                actionResult.Render(xWriter);
            xWriter.Close();

            if (this.End != null)
            {
                #region LOG ENTRY
#if !NO_LOG
                Log.WriteLine(this, "Calling End event");
#endif
                #endregion
                this.End(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Event to be called before item is going to render
        /// </summary>
        public event EventHandler<ItemPrerenderEventArgs<T>> ItemPrerender;
        /// <summary>
        /// Event to be called before items items are going to render. This event is mostly necessary for writing additional xml info into response (e.g. grid header configuration)
        /// </summary>
        public event EventHandler<RenderEventArgs> BeforeOutput;

        /// <summary>
        /// Event to be fired when all connector activity has been done
        /// </summary>
        public event EventHandler End;

        /// <summary>
        /// Event to be fired before any connector activity starts
        /// </summary>
        public event EventHandler Begin;

        #region Proxy events

        /// <summary>
        /// Event to be fired before any DataAction is executed
        /// </summary>
        public event EventHandler<DataActionProcessingEventArgs> BeforeDataActionProcessing;

        /// <summary>
        /// Event to be fired after any DataAction was executed
        /// </summary>
        public event EventHandler<DataActionProcessingEventArgs> DataActionProcessed;

        /// <summary>
        /// Event to be fired before any insert DataAction is executed
        /// </summary>
        public event EventHandler<DataActionProcessingEventArgs> BeforeInsert;

        /// <summary>
        /// Event to be fired after insert DataAction was executed
        /// </summary>
        public event EventHandler<DataActionProcessingEventArgs> Inserted;

        /// <summary>
        /// Event to be fired before any update DataAction is executed
        /// </summary>
        public event EventHandler<DataActionProcessingEventArgs> BeforeUpdate;

        /// <summary>
        /// Event to be fired after Update DataAction was executed
        /// </summary>
        public event EventHandler<DataActionProcessingEventArgs> Updated;

        /// <summary>
        /// Event to be fired before any delete DataAction is executed
        /// </summary>
        public event EventHandler<DataActionProcessingEventArgs> BeforeDelete;

        /// <summary>
        /// Event to be fired after delete DataAction was executed
        /// </summary>
        public event EventHandler<DataActionProcessingEventArgs> Deleted;

        /// <summary>
        /// Event to be fired before data select
        /// </summary>
        public event EventHandler BeforeSelect;

        /// <summary>
        /// Event to be fired after select results were retrieved
        /// </summary>
        public event EventHandler<DataSelectedEventArgs> Selected;
        #endregion

        /// <summary>
        /// Writes end tags of response header
        /// </summary>
        /// <param name="xWriter">XmlWriter to render content to</param>
        /// <param name="RowsToRender">Data to render</param>
        /// <param name="TotalRowsCount">Total amount of rows available</param>
        protected virtual void EndRenderContent(System.Xml.XmlWriter xWriter, System.Data.DataTable RowsToRender, int TotalRowsCount)
        {
            xWriter.WriteEndElement();
        }

        /// <summary>
        /// Decodes field name to Field object
        /// </summary>
        /// <param name="EncodedField">Encoded field name token from QueryString</param>
        /// <returns>Field object that corresponds EncodedField, or null</returns>
        public virtual Field DecodeField(string EncodedField)
        {
            #region LOG ENTRY
#if !NO_LOG
            Log.WriteLine(this, "Encoding query field: " + EncodedField);
#endif
            #endregion

            string encField = EncodedField;// Regex.Replace(EncodedField, "^[^_]*.", "");
            if (encField.StartsWith("c"))
            {
                encField = encField.Substring(1);
                int colIndex = Int32.Parse(encField);
                if (this.Request.RequestedFields.Count > colIndex)
                    return this.Request.RequestedFields[colIndex];
            }
            return null;
        }
    }
}
