using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Provides base functionality for handling dhtmlx components requests
    /// </summary>
    public abstract class dhtmlxRequestHandler : IHttpHandler
    {
        /// <summary>
        /// Gets value indicating whether current service can be used. dhtmlxRequestHandler cannot be reused.
        /// </summary>
        public bool IsReusable
        {
            get 
            {
                return false;
            }
        }

        private IdhtmlxConnector _DataConnector = null;
        /// <summary>
        /// Gets reference to dhtmlxConnector serving current request
        /// </summary>
        public IdhtmlxConnector Connector
        {
            get
            {
                if (this._DataConnector == null)
                    this._DataConnector = this.CreateConnector(HttpContext.Current);
                return this._DataConnector;
            }
        }
        
        /// <summary>
        /// When overriden in derived class, creates dhtmlxConnector to serve current request. This is the only approved way of dhtmlxConnector intialization
        /// </summary>
        /// <param name="context">Current HttpContext</param>
        /// <returns>Instance dhtmlxConnector class to serve current request</returns>
        public abstract IdhtmlxConnector CreateConnector(HttpContext context);

        /// <summary>
        /// Processes client request. All connectors functionality happens here
        /// </summary>
        /// <param name="context">Current HttpContext</param>
        public virtual void ProcessRequest(HttpContext context)
        {
            this.Connector.ProcessRequest(context.Request.QueryString, context.Request.Form);
            this.Connector.RenderResponse(context.Response);
        }
    }
}
