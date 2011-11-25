using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dhtmlxConnectors;
using System.Configuration;

namespace dhtmlxConnector.Net_Samples.common
{
    public partial class formBasedConnector : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Create and configure connector
            dhtmlxTreeConnector connector = new dhtmlxTreeConnector(
                "Folders",
                "item_id",
                "item_parent_id",
                dhtmlxDatabaseAdapterType.SqlServer2005,
                ConfigurationManager.ConnectionStrings["SamplesDatabase"].ConnectionString,
                "item_nm"
            );
            connector.RootItemRelationIDValue = "0"; //Set ParentID value for root items
            connector.EnableDynamicLoading = true;

            //Process client request
            connector.ProcessRequest(this.Request.QueryString, this.Request.Form);

            //Render response
            this.Response.Clear();
            connector.RenderResponse(this.Response);
            this.Response.End();
        }
    }
}
