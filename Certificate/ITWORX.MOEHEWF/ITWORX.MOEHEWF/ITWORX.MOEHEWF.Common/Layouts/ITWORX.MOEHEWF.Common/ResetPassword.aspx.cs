using System;
using System.IdentityModel.Tokens; 
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.IdentityModel;
using Microsoft.SharePoint.WebControls;

namespace ITWORX.MOEHEWF.Common.Layouts.ITWORX.MOEHEWF.Common
{
    public partial class ResetPassword : UnsecuredLayoutsPageBase
    {
        protected override bool AllowAnonymousAccess { get { return true; } }

        public string LoginUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["LoginUrl"];
            }
        }
        public string QatarId { set { tbQatarId.Text = value; } get { return tbQatarId.Text; } }
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            SPSite TestWeb = SPControl.GetContextSite(Context);
            string strUrl = TestWeb.ServerRelativeUrl + "/_catalogs/masterpage/Home1.master";

            this.MasterPageFile = strUrl;
        }
        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);
            LCID = System.Threading.Thread.CurrentThread.CurrentUICulture.LCID;
 
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public string ADMembershipProviderName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ADMembershipProviderName"];
            }
        }
        protected void BtnChangePassword_Click(object sender, EventArgs e)
        {
           
            ChangePassword();
        }
        public void ChangePassword()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method ResetPassword.ChangePassword");
                UserCredentials userCredentials = new UserCredentials();//"Administrator",
                userCredentials.UserName = MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "UserName");
                userCredentials.Password = MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "Password");
                userCredentials.LDAPPath = MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "LDAPPath");// DC =MOEHE,DC=EDU,DC=QA";
                userCredentials.DomainController = MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "DomainController");
                ////CreateUserRequest createUserRequest = new CreateUserRequest { UserCredentials = userCredentials, ADUser = new ADUser() { UserName = QatarId, QatarId = QatarId, Password = tbOldPassword.Text } };
                ////PRResponse pRResponse = CallingIdentityApiHelper.IsLoginValid(createUserRequest);
                ////if (pRResponse == null || !pRResponse.IsValid)
                ////{
                ////    lblMessage.ForeColor = System.Drawing.Color.Red;
                ////    lblMessage.Text = string.Format(HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "CurrentPasswordErrorMessage", (uint)LCID));
                ////}
               
                ////else
                ////{
                  //  ADPasswordPattern aDPasswordPattern = CallingIdentityApiHelper.GetADPasswordPattern(userCredentials);

                    FPResponse fPResponse = CallingIdentityApiHelper.ChangePassword(new PasswordResetRequest()
                    {
                        UserCredentials = userCredentials,
                        PasswordReset = new PasswordReset()
                        {
                            NewPassword = tbNewPassword.Text,
                            OldPassword = tbOldPassword.Text,
                            UserName = QatarId
                        }
                    });

                if (fPResponse.ValidationCode ==  ResetPasswordValidationCode.PasswordchangedSuccessfully)
                {
                    //var loginUrl = MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "LoginUrl");
                    //if (String.IsNullOrEmpty(loginUrl))
                    //{
                    //    loginUrl = SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.Common/MOEHELogIn.aspx";
                    //}
                    pnlForm.Visible = false;
                    pnlSuccess.Visible = true;
                    lblCreatedUserName.Text = QatarId;
                    lnkNavigateToLogin.NavigateUrl = LoginUrl;

                }
                else if (fPResponse.ValidationCode ==  ResetPasswordValidationCode.InvalidQatarId)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = string.Format(MOEHE.Utilities.HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "InvalidQatarId", (uint)LCID));
                }
                else if (fPResponse.ValidationCode ==  ResetPasswordValidationCode.UnexpectedError)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = fPResponse.MessageEn;
                }
                else if (fPResponse.ValidationCode ==  ResetPasswordValidationCode.ErrorInpasswordPolicy)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = string.Format(MOEHE.Utilities.HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "PasswordPolicyError", (uint)LCID));

                }
                else if (fPResponse.ValidationCode ==  ResetPasswordValidationCode.NotMatchedPassword)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = string.Format(MOEHE.Utilities.HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "CurrentPasswordErrorMessage", (uint)LCID));
                }

            }
            catch (System.Threading.ThreadAbortException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method ForgetPassword.ForgotPassword");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            pnlForm.Visible = true;
            pnlSuccess.Visible = false;
        }
    }
}
