using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dhtmlxConnectors;
using System.Web.Services;
using System.Configuration;

namespace dhtmlxConnector.Net_Samples.dhtmlxTree
{
    /// <summary>
    /// Connector body
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SQLFoldersTree : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            dhtmlxTreeConnector connector = new dhtmlxTreeConnector(
                "SELECT * FROM Folders",
                "item_id",
                "item_parent_id",
                dhtmlxDatabaseAdapterType.SqlServer2005,
                ConfigurationManager.ConnectionStrings["SamplesDatabase"].ConnectionString,
                "item_nm"
            );
            connector.RootItemRelationIDValue = "0"; //Set ParentID value for root items
            return connector;
        }
    }
}
