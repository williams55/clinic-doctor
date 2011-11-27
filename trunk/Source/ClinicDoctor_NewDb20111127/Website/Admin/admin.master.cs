using System;
using System.Collections.Specialized;
using System.Web.Security;
using ClinicDoctor.Data;
using ClinicDoctor.Data.SqlClient;
using ClinicDoctor.Entities;
using ClinicDoctor.Web;
public partial class Admin_admin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (!IsPostBack)
        //{

        //    string authUserName;
        //    authUserName = Context.User.Identity.Name.Split('\\')[1];
        //    //lbl_User.Text = authUserName;
        //    WebUser objUser = DataRepository.WebUserProvider.GetByUserId(authUserName);
        //    if (objUser == null)
        //    {
        //        lbl_User.Text = "Welcome, " + authUserName;
        //        if (Session["Redirect"] == null)
        //            Response.Redirect("AccessDenied.aspx", true);
        //        else
        //            Session["Redirect"] = null;
        //    }
        //    else
        //    {
        //        lbl_User.Text = "Welcome, " + objUser.Fullname;
        //    }
        //}
    }
    protected void btnLogOut_Click(object sender, EventArgs eventArgs)
    {
        Response.Cookies["LoginUser"].Expires = DateTime.Now.AddDays(-30);
        FormsAuthentication.RedirectToLoginPage();
    }
    protected override void OnInit(EventArgs e)
    {
        //base.OnInit(e);
        //var httpCookie = Request.Cookies["ConnectionString"];
        //if (httpCookie != null)
        //{
        //    SqlNetTiersProvider provider = GlobalUtilities.CreateProvider(Server.MapPath("~") + "/Config.xml",
        //                                                                  httpCookie.Value);
        //    DataRepository.LoadProvider(provider, true);
        //}
    }
}
