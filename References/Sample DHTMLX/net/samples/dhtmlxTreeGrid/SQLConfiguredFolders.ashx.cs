using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using dhtmlxConnectors;
using System.Configuration;

namespace dhtmlxConnector.Net_Samples.dhtmlxTreeGrid
{
    /// <summary>
    /// Connector body
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SQLConfiguredFolders : dhtmlxRequestHandler
    {

        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            //select only folders that has children + their children themselves
            dhtmlxTreeGridConnector connector = new dhtmlxTreeGridConnector(
                "SELECT item_nm, item_order, item_desc from Folders WHERE is_hidden = 0", 
                "item_id", 
                "item_parent_id",
                dhtmlxDatabaseAdapterType.SqlServer2005,
                ConfigurationManager.ConnectionStrings["SamplesDatabase"].ConnectionString
                );
            connector.RootItemRelationIDValue = "0";//Set ParentID value for root items
            return connector;
        }
    }
}
