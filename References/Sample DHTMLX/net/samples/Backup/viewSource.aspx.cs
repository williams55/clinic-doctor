using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;

namespace dhtmlxConnector.Net_Samples
{
    public partial class viewSource : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["target"]) || string.IsNullOrEmpty(Request.QueryString["folder"]))
                return;

            List<string> viewPages = new List<string>();
            if (Request.QueryString["codeBehind"] == "" || !Convert.ToBoolean(Request.QueryString["codeBehind"]))
                viewPages.Add(Request.QueryString["target"]);
            else
            {
                viewPages.AddRange(this.GetReferencedHandlersURLs(Request.QueryString["target"], Request.QueryString["folder"]));
            }

            StringBuilder sb = new StringBuilder();
            foreach (string pageName in viewPages)
            {
                sb.AppendFormat("******************   {0}   ********************", pageName).Append(Environment.NewLine).Append(Environment.NewLine);
                sb.Append(this.ReadFile(pageName, Request.QueryString["folder"]));
                sb.Append(Environment.NewLine).Append(Environment.NewLine).Append(Environment.NewLine).Append(Environment.NewLine);
            }
            this.Source.Text = sb.ToString();
        }

        protected IEnumerable<string> GetReferencedHandlersURLs(string PageName, string Folder)
        {
            string fileContent = this.ReadFile(PageName, Folder);
            List<string> pages = new List<string>();
            foreach(Match match in Regex.Matches(fileContent, "[^\"]*.ashx"))
                pages.Add(match.Value + ".cs");
            return pages.Distinct();
        }

        protected string ReadFile(string PageName, string Folder)
        {
            string path = Server.MapPath(Folder + "\\" + PageName);
            return System.IO.File.ReadAllText(path);
        }
    }
}
