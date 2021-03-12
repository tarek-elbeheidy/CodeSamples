using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Web;

namespace ITWORX.MOEHEWF.Common.WebParts.HomeRegister
{
    public partial class HomeRegisterUserControl : UserControlBase
    {
        public string EmployeeLoginUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["EmployeeLoginUrl"];
            }
        }

        public string PublicUserLoginUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PublicUserLoginUrl"];
            }
        }

        public string Upn
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["UPN"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SPContext.Current.Web.CurrentUser != null)
            {
                
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/HomeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

            }
        }

        protected void newUserBtn_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.Common/Register.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
        }

        protected void brnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect(PublicUserLoginUrl);
        }

        protected void btnEmployeeLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect(EmployeeLoginUrl);
        }

        protected void btnForgetPassword_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.Common/ForgetPassword.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
        }
    }
}