
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using dhtmlxConnectors;
using System.Configuration;
using System.Text.RegularExpressions;

namespace dhtmlxConnector.Net_Samples.dhtmlxGrid
{
    /// <summary>
    /// Connector body
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class connectorWithValidation : dhtmlxRequestHandler
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
            connector.BeforeInsert += new EventHandler<DataActionProcessingEventArgs>(connector_BeforeProcessing);
            connector.BeforeUpdate += new EventHandler<DataActionProcessingEventArgs>(connector_BeforeProcessing);
            return connector;
        }

        void connector_BeforeProcessing(object sender, DataActionProcessingEventArgs e)
        {
            if (string.IsNullOrEmpty(e.DataAction.Data[(TableField)"title"]))
                e.DataAction.SetInvalid("Book title cannot be empty!");

            if (string.IsNullOrEmpty(e.DataAction.Data[(TableField)"author"]))
                e.DataAction.SetInvalid("Book author cannot be empty!");
        }

    }
}
