
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
    public class gridOptionsConnector : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector  CreateConnector(HttpContext context)
        {
            //configure grid connector
            dhtmlxGridConnector connector = new dhtmlxGridConnector(
                "Country",
                "ISO, PrintableName",
                "UID",
                dhtmlxDatabaseAdapterType.SqlServer2005, 
                ConfigurationManager.ConnectionStrings["SamplesDatabase"].ConnectionString
            );
            connector.SetDynamicLoading(50);
            //configure filter drop-down-list connector
            dhtmlxOptionsConnector filterConnector = new dhtmlxOptionsConnector("Country", connector.Request.Adapter, "ISO");
            //attach filter connector to first column of grid connector
            connector.OptionsConnectors.Add(connector.Request.RequestedFields[0], filterConnector);
            return connector;
        }
    }
}
