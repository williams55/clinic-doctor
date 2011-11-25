using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using dhtmlxConnectors;
using System.Configuration;

namespace dhtmlxConnector.Net_Samples.dhtmlxTree
{
    /// <summary>
    /// Connector body
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CustomStylesTree : dhtmlxRequestHandler
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
            connector.ItemPrerender += new EventHandler<ItemPrerenderEventArgs<dhtmlxTreeDataItem>>(connector_ItemPrerender);
            return connector;
        }

        void connector_ItemPrerender(object sender, ItemPrerenderEventArgs<dhtmlxTreeDataItem> e)
        {
            if (e.DataItem.DataFields["is_hidden"] == "1")
                e.DataItem.LeafImage = "lock.gif";
        }
    }
}