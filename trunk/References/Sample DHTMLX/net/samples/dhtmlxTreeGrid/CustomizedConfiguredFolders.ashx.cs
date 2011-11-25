
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dhtmlxConnectors;
using System.Configuration;
using System.Web.Services;

namespace dhtmlxConnector.Net_Samples.dhtmlxTreeGrid
{
    /// <summary>
    /// Connector body
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CustomizedConfiguredFolders : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            dhtmlxTreeGridConnector connector = new dhtmlxTreeGridConnector(
                "Folders", "item_nm,item_order,item_desc", "item_id", "item_parent_id",
                dhtmlxDatabaseAdapterType.SqlServer2005,
                ConfigurationManager.ConnectionStrings["SamplesDatabase"].ConnectionString
                );
            connector.RootItemRelationIDValue = "0";//Set ParentID value for root items
            connector.ItemPrerender += new EventHandler<ItemPrerenderEventArgs<dhtmlxTreeGridDataItem>>(connector_ItemPrerender);
            return connector;
        }

        void connector_ItemPrerender(object sender, ItemPrerenderEventArgs<dhtmlxTreeGridDataItem> e)
        {
            if (e.DataItem.HasChildren)
                e.DataItem.Image = "folder.gif";
        }
    }
}
