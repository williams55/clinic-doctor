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
    public class gridRenderingConnector : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            dhtmlxGridConnector connector = new dhtmlxGridConnector(
                "BookStore", 
                "sales, title, author, price, instore, shipping, bestseller, pub_date", 
                "book_id", 
                dhtmlxDatabaseAdapterType.SqlServer2005,
                ConfigurationManager.ConnectionStrings["SamplesDatabase"].ConnectionString
            );
            connector.ItemPrerender += new EventHandler<ItemPrerenderEventArgs<dhtmlxGridDataItem>>(connector_ItemPrerender);
            return connector;
        }

        void connector_ItemPrerender(object sender, ItemPrerenderEventArgs<dhtmlxGridDataItem> e)
        {
            if (e.DataItem.Index % 2 == 0)
                e.DataItem.BgColor = "gray";
        }
    }
}
