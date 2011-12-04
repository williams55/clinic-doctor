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

        if (!IsPostBack)
        {

            string authUserName;
            authUserName = Context.User.Identity.Name.Split('\\')[1];
            //lbl_User.Text = authUserName;
            Staff objUser = DataRepository.StaffProvider.GetByUserName(authUserName);
            if (objUser == null)
            {
                lbl_Name.Text = authUserName;
                if (Session["Redirect"] == null)
                    Response.Redirect("AccessDenied.aspx", true);
                else
                    Session["Redirect"] = null;
            }
            else
            {
                lbl_Name.Text = objUser.FirstName;
                Session["UserName"] = authUserName;
                
            }

        }
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
