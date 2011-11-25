
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
    public class FoldersTreeWithValidation : dhtmlxRequestHandler
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
            connector.BeforeUpdate += new EventHandler<DataActionProcessingEventArgs>(connector_BeforeUpdate);
            return connector;
        }

        void connector_BeforeUpdate(object sender, DataActionProcessingEventArgs e)
        {
            if (e.DataAction.Data[(this.Connector as dhtmlxTreeConnector).NodeTextField] == "")
                e.DataAction.SetInvalid("File name cannot be empty!");
        }
    }
}
