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
    public class baseGridConnector : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            dhtmlxGridConnector connector = new dhtmlxGridConnector(
                "Country",
                "ISO, PrintableName",
                "UID",
                dhtmlxDatabaseAdapterType.SqlServer2005, 
                ConfigurationManager.ConnectionStrings["SamplesDatabase"].ConnectionString
            );
            if (context.Request.QueryString["dynamic"] != null)
                connector.SetDynamicLoading(Convert.ToInt32(context.Request.QueryString["dynamic"]));
            return connector;
        }
    }
}
