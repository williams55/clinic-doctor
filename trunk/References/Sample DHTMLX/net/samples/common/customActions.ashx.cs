using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dhtmlxConnectors;
using System.Configuration;
using System.Web.Services;

namespace dhtmlxConnector.Net_Samples.common
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class customActions : dhtmlxRequestHandler
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
            connector.BeforeOutput += new EventHandler<RenderEventArgs>(connector_BeforeOutput);
            return connector;
        }

        void connector_BeforeOutput(object sender, RenderEventArgs e)
        {
            if (this.Connector.Request.RequestType == DataRequestType.Edit)
            {
                //create new action
                DataAction customAction = new DataAction("sayHello", "", null, null, null, null);
                customAction.Details = "Hello, World!";
                //add it to actions collection and send to the client
                this.Connector.Request.DataActions.Add(customAction);
            }
        }

    }
}
