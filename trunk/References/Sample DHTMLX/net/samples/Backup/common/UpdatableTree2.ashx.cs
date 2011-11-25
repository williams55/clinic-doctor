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
    public class UpdatableTree2 : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            dhtmlxTreeConnector connector = new dhtmlxTreeConnector(
                "Folders",
                "item_id",
                "item_parent_id",
                dhtmlxDatabaseAdapterType.SqlServer2005,
                ConfigurationManager.ConnectionStrings["SamplesDatabase"].ConnectionString,
                "item_nm"
            );
            connector.RootItemRelationIDValue = "0"; //Set ParentID value for root items

            //add rule that will exclude hidden files from result
            connector.Request.Rules.Add(new FieldRule("is_hidden", Operator.NotEquals, true));

            //attach before insert/update events to add some more fields into query
            connector.BeforeInsert += new EventHandler<DataActionProcessingEventArgs>(connector_BeforeInsert);
            connector.BeforeUpdate += new EventHandler<DataActionProcessingEventArgs>(connector_BeforeUpdate);

            return connector;
        }

        void connector_BeforeUpdate(object sender, DataActionProcessingEventArgs e)
        {
            e.DataAction.Data.Add("modify_date", Tools.ConvertToString(DateTime.Now));
        }

        void connector_BeforeInsert(object sender, DataActionProcessingEventArgs e)
        {
            e.DataAction.Data.Add("create_date", Tools.ConvertToString(DateTime.Now));
        }
    }
}
