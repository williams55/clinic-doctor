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
    public class gridCustomSQLConnector : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            dhtmlxGridConnector connector = new dhtmlxGridConnector(
                "SELECT ISO, PrintableName FROM Country",
                "UID",
                dhtmlxDatabaseAdapterType.SqlServer2005,
                ConfigurationManager.ConnectionStrings["SamplesDatabase"].ConnectionString
            );
            connector.Request.CustomSQLs.Add(CustomSQLType.Delete, "UPDATE Country SET PrintableName = '-------------' WHERE UID={UID}");
            return connector;
        }
    }
}