using Microsoft.SharePoint;
using MOEHE.PSPES.Repository;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MOEHE.PSPES.Webparts.MultiLanguageSwitcher.ChangeLanguage
{
    public partial class ChangeLanguageUserControl : UserControl
    {
        private static string strKeyName = "LangSwitcher_Setting";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (SPContext.Current.Site.RootWeb.CurrentUser != null)
                //{



                if (Request.Cookies[strKeyName] != null)
                {
                    if (Request.Cookies[strKeyName].Value == "en-US")
                    {
                        EnglishHyperLink.Visible = false;
                    }
                    else
                    {
                        ArabicHyperLink.Visible = false;
                    }
                }

                else
                {
                    string strLanguage = "ar-SA";
                    HttpCookie acookie = new HttpCookie(strKeyName);
                    acookie.Value = strLanguage;
                    acookie.Expires = DateTime.MaxValue;
                    Response.Cookies.Add(acookie);
                    EnglishHyperLink.Visible = false;
                    Response.Redirect(Request.RawUrl);
                }
                if (SPContext.Current.Site.RootWeb.CurrentUser != null)
                {
                    SignInLiteral.Visible = false;
                    SignOutLiteral.Visible = true;
                    SignLnk.HRef = "/_layouts/15/PS_SignOut.aspx";
                    UserDetailsPanel.Visible = true;
                    ltusername.Text = SPContext.Current.Site.RootWeb.CurrentUser.Name;
                }
                else
                {
                    SignInLiteral.Visible = true;
                    UserDetailsPanel.Visible = false;
                    SignOutLiteral.Visible = false;
                    SignLnk.HRef = "/_layouts/15/Authenticate.aspx";
                }
                //UserNameLiteral.Text = userData.DisplayName;
                //}

            }
            catch { }

        }

        //private UserData CurrentUserName()
        //{
        //    UserData userData = new UserData();
        //    SPContext currentContext;
        //    try
        //    {
        //        //Getting the current context                
        //        currentContext = SPContext.Current;
        //    }
        //    catch (InvalidOperationException)
        //    {
        //        currentContext = null;
        //    }
        //    if (currentContext != null && currentContext.Web.CurrentUser != null)
        //    {
        //        userData.DisplayName = SPContext.Current.Web.CurrentUser.Name;

        //    }

        //    //string Username;
        //    SPClaimProviderManager mgr = SPClaimProviderManager.Local;
        //    if (mgr != null && SPClaimProviderManager.IsEncodedClaim(SPContext.Current.Web.CurrentUser.LoginName))
        //    {
        //        userData.LoginName = mgr.DecodeClaim(SPContext.Current.Web.CurrentUser.LoginName).Value;
        //    }
        //    else
        //    {
        //        userData.LoginName = SPContext.Current.Web.CurrentUser.LoginName;
        //    }

        //    return userData;
        //}

        protected void EnglishHyperLink_Click(object sender, EventArgs e)
        {
            string strLanguage = "en-US";

            //if (Request.Cookies[strKeyName] != null)
            //{
            //    Request.Cookies[strKeyName].Value = strLanguage;
            //   // Request.Cookies[strKeyName].Expires = DateTime.MaxValue;

            //}

            //else
            //{
            // Set the Cookies.
            HttpCookie acookie = new HttpCookie(strKeyName);
            acookie.Value = strLanguage;
            acookie.Expires = DateTime.MaxValue;
            Response.Cookies.Add(acookie);
            //}
            Response.Redirect(Request.RawUrl);
        }

        protected void ArabicHyperLink_Click(object sender, EventArgs e)
        {
            string strLanguage = "ar-SA";

            //if (Request.Cookies[strKeyName] != null)
            //{
            //    Request.Cookies[strKeyName].Value = strLanguage;
            //   // Request.Cookies[strKeyName].Expires = DateTime.MaxValue;
            //}

            //else
            //{
            // Set the Cookies.
            HttpCookie acookie = new HttpCookie(strKeyName);
            acookie.Value = strLanguage;
            acookie.Expires = DateTime.MaxValue;
            Response.Cookies.Add(acookie);
            //}

            Response.Redirect(Request.RawUrl);
        }

        protected void SignLnkBtn_Click(object sender, EventArgs e)
        {


            if (SPContext.Current.Site.RootWeb.CurrentUser != null)
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.User = null;
                Context.Response.Redirect("/_layouts/15/PS_SignOut.aspx");
            }
            else
            {
                Context.Response.Redirect("/_layouts/15/Authenticate.aspx");
            }
              
        }
    }

}
