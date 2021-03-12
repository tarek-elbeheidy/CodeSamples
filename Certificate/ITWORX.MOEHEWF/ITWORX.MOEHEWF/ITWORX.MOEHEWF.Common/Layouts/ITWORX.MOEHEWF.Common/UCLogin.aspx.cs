using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using Microsoft.SharePoint.IdentityModel;
using System;
using System.IdentityModel.Tokens;
using System.Web.UI;

namespace ITWORX.MOEHEWF.Common.Layouts.ITWORX.MOEHEWF.Common
{
    public partial class UCLogin : Page
    {
        public string ADMembershipProviderName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ADMembershipProviderName"];
            }
        }

        public string BackUrl
        {
            get
            {
                return string.IsNullOrEmpty(this.Request.QueryString["ReturnUrl"])
                           ? "/"
                           : this.Request.QueryString["ReturnUrl"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtUsername.Focus();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering UCLoginIn.btnLogin_Click");

                SecurityToken securityToken =
                       SPSecurityContext.SecurityTokenForFormsAuthentication(
                           new Uri(SPContext.Current.Web.Url),
                           ADMembershipProviderName,
                           "",
                           this.txtUsername.Text.Trim(),
                           this.txtPassword.Text,
                           SPFormsAuthenticationOption.PersistentSignInRequest);

                if (securityToken != null)
                {
                    // try setting the authentication cookie
                    SPFederationAuthenticationModule farm = SPFederationAuthenticationModule.Current;
                    farm.SetPrincipalAndWriteSessionToken(securityToken);
                    this.Response.Redirect(this.BackUrl, false);
                }
                else
                {
                    //this.lblMessage.Text = (string)this.GetLocalResourceObject("LoginFailure1");
                    this.lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //this.lblMessage.Text = (string)this.GetLocalResourceObject("LoginFailure");
                this.lblMessage.Visible = true;
            }
            finally
            {
                Logging.GetInstance().Debug("Entering UCLoginIn.btnLogin_Click");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
        }
    }
}