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
        //if (Session["adminname"] == null)
        //    Response.Redirect("AdminLogin.aspx", true);
        //ECISaiGon.Entities.User obj = (ECISaiGon.Entities.User)Session["adminname"];
        //obj = DataRepository.UserProvider.GetByUserId(obj.UserId);
        //hpl_EditProfile.Text = obj.DisplayName;
        //hpl_EditProfile.NavigateUrl = "EditUser.aspx?uid=" + obj.UserId;
        //lbl_Name.Text = obj.DisplayName;

        //if (!IsPostBack)
        //{
        //    var httpCookie = Request.Cookies["LoginUser"];
        //    var conCookie = Request.Cookies["ConnectionString"];
        //    if (httpCookie != null && conCookie !=null)
        //    {
        //        WebUser id = DataRepository.WebUserProvider.GetByUserId(httpCookie.Value);
        //        lbl_Server.Text = conCookie.Value;
        //        lbl_Name.Text = id.Name;
        //        hpl_EditProfile.Text = id.Name;
        //    }
        //    else
        //    {
        //        FormsAuthentication.RedirectToLoginPage();
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
