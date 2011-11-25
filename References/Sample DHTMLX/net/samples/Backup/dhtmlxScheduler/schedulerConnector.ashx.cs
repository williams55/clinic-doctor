using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using dhtmlxConnectors;
using System.Configuration;

namespace dhtmlxConnector.Net_Samples.dhtmlxScheduler
{
    /// <summary>
    /// Connector body
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class schedulerConnector : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            return new dhtmlxSchedulerConnector(
                  "Events"
                , "EventID"
                , dhtmlxDatabaseAdapterType.SqlServer2005
                , ConfigurationManager.ConnectionStrings["SamplesDatabase"].ConnectionString
                , "FromDate"
                , "ToDate"
                , "Subject as text, Details as details, Tags"
            );
        }
    }
}