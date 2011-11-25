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
    public class gridBasicSQLConnector : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            dhtmlxGridConnector connector = new dhtmlxGridConnector(
                "SELECT ISO, PrintableName FROM Country",
                "UID",
                dhtmlxDatabaseAdapterType.SqlServer2005,
                ConfigurationManager.ConnectionStrings["SamplesDatabase"].ConnectionString
            );
            connector.BeforeInsert += new EventHandler<DataActionProcessingEventArgs>(connector_BeforeInsert);
            return connector;
        }

        void connector_BeforeInsert(object sender, DataActionProcessingEventArgs e)
        {
            if (!e.DataAction.Data.ContainsKey((TableField)"Name"))
                e.DataAction.Data.Add("Name", "");//Name field is mandatory in table Country
        }
    }
}
