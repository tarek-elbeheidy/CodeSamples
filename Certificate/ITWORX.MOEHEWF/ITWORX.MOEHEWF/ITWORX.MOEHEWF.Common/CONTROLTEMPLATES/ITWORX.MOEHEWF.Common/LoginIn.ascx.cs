using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.IdentityModel;
using Microsoft.SharePoint.Utilities;
using System;
using System.IdentityModel.Tokens;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common
{
    public partial class LoginIn : UserControl
    {
        private string ReturnUrl { get; set; }

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            ReturnUrl = Request.QueryString["ReturnUrl"];
        }


        protected void btnLogout_Click1(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering LogIn.btnLogin_Click");



                if (null != SPContext.Current && null != SPContext.Current.Site)
                {
                    SPIisSettings iisSettings = SPContext.Current.Site.WebApplication.IisSettings[SPUrlZone.Default];
                    if (null != iisSettings && iisSettings.UseWindowsClaimsAuthenticationProvider)
                    {
                        SPAuthenticationProvider provider = iisSettings.WindowsClaimsAuthenticationProvider;
                        Redirect(provider);
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);

            }
            finally
            {
                Logging.GetInstance().Debug("Entering LogIn.btnLogin_Click");
            }
        }

        #endregion

        #region Methods
        private void Redirect(SPAuthenticationProvider provider)
        {
            string comp = HttpContext.Current.Request.Url.GetComponents(UriComponents.Query, UriFormat.SafeUnescaped);
            string url = provider.AuthenticationRedirectionUrl.ToString();
            if (provider is SPWindowsAuthenticationProvider)
            {
                comp = EnsureUrl(comp, true);
            }

            SPUtility.Redirect(url, SPRedirectFlags.Default, this.Context, comp);
        }

        private string EnsureUrl(string url, bool urlIsQueryStringOnly)
        {
            if (!url.Contains("ReturnUrl="))
            {
                if (urlIsQueryStringOnly)
                {
                    url = url + (string.IsNullOrEmpty(url) ? "" : "&");
                }
                else
                {
                    url = url + ((url.IndexOf('?') == -1) ? "?" : "&");
                }
                url = url + "ReturnUrl=";
            }
            return url;
        }

        #endregion

       

        
    }
}
