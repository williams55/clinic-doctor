
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dhtmlxConnectors;
using System.Web.Services;
using System.Configuration;

namespace dhtmlxConnector.Net_Samples.Combo
{
    /// <summary>
    /// Connector body
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class _1_basic_connector : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {

            dhtmlxComboConnector connector = new dhtmlxComboConnector(
                "Country",
                "UID",
                dhtmlxDatabaseAdapterType.SqlServer2005, 
                ConfigurationManager.ConnectionStrings["SamplesDatabase"].ConnectionString,
                "PrintableName" 
            );
            if (context.Request.QueryString["dynamic"] != null)
                connector.SetDynamicLoading(2);
            return connector;
        }
    }
}