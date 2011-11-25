using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using dhtmlxConnectors;
using System.Configuration;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace dhtmlxConnector.Net_Samples.common
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class LoggableTree : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            dhtmlxTreeConnector connector = new dhtmlxTreeConnector(
                "Folders",
                "item_id",
                "item_parent_id",
                dhtmlxDatabaseAdapterType.SqlServer2005,
                ConfigurationManager.ConnectionStrings["SamplesDatabase"].ConnectionString,
                "item_nm"
            );
            connector.RootItemRelationIDValue = "0"; //Set ParentID value for root items
            connector.Begin += new EventHandler(connector_Begin);
            connector.End += new EventHandler(connector_End);
            return connector;
        }

        private StringBuilder _LogContent = new StringBuilder();

        void connector_End(object sender, EventArgs e)
        {
            Log.Enabled = false;//stop logging
        }

        void connector_Begin(object sender, EventArgs e)
        {
            //enable logging and add listener to it
            Log.Enabled = true;
            Log.Listeners.Add(new TextWriterTraceListener(new StringWriter(this._LogContent)));
        }
    }
}
