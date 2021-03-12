using System; 
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using ITWORX.MOEHE.Utilities;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHE.Integration.SMS;
using CommonUtilities = ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHEWF.Common.Entities;
using System.Collections.Generic;
using Microsoft.SharePoint.Utilities;
using System.Web;

namespace ITWORX.MOEHEWF.Common.Layouts.ITWORX.MOEHEWF.Common
{
    public partial class ForgetPassword : UnsecuredLayoutsPageBase
    {
        protected override bool AllowAnonymousAccess { get { return true; } }

        public string LoginUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["LoginUrl"];
            }
        }
        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);
            LCID = System.Threading.Thread.CurrentThread.CurrentUICulture.LCID;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        private void ForgotPassword()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method ForgetPassword.ForgotPassword");
                UserCredentials userCredentials = new UserCredentials();//"Administrator",
                userCredentials.UserName = MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "UserName");
                userCredentials.Password = MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "Password");
                userCredentials.LDAPPath = MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "LDAPPath");// DC =MOEHE,DC=EDU,DC=QA";
                userCredentials.DomainController = MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "DomainController");
                ADPasswordPattern aDPasswordPattern = CallingIdentityApiHelper.GetADPasswordPattern(userCredentials);
                string password = CallingIdentityApiHelper.GeneratePassword(aDPasswordPattern);
                ForgetPasswordValidationRequest forgetPasswordValidation = new ForgetPasswordValidationRequest()
                {
                    UserCredentials = userCredentials,
                    UserProfile = new UserProfile()
                    {
                        //EmailAddress = txtEmail.Text,
                        NewPassword = password,
                        UserName = tbQatarId.Text,
                        QatarId= tbQatarId.Text
                    }
                };
                var upnSuffix = "@" + MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "UPN");
                var userName = forgetPasswordValidation.UserProfile.UserName + upnSuffix;
                FPResponse fPResponse = CallingIdentityApiHelper.ForgotPassword(forgetPasswordValidation);
                
                if (fPResponse.ForgotPasswordValidationCode ==  ForgotPasswordValidationCode.PasswordchangedSuccessfully)
                {
                    try
                    {
                        Entities.Notifications smsNotifications = BL.Notifications.GetSubmittedNotification((int)Utilities.NotificationType.SMS, (int)CommonUtilities.RequestStatus.ForgetPassword);
                        if (smsNotifications != null)
                        {
                            //Send SMS here
                            Texting.SendSMS(fPResponse.MobileNumber, string.Format(smsNotifications.Body, userName, password, userName, password));
                            //Texting.SendSMS(createUserRequest.ADUser.MobileNumber, string.Format(smsNotifications.Body, createUserRequest.ADUser.Password, "Equivalency", createUserRequest.ADUser.Password, "معادلة الشهادات"));
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(ex);
                    }
                    try
                    {
                        SPSecurity.RunWithElevatedPrivileges(() =>
                        {
                            string SMTPServer = MOEHE.Utilities.HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPServer");
                            string SMTPServerPort = MOEHE.Utilities.HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPServerPort");
                            string SMTPFromAddress = MOEHE.Utilities.HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPFromAddress");
                            string SMTPFromDisplayName = MOEHE.Utilities.HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPFromDisplayName");

                            Entities.Notifications emaiNotifications = BL.Notifications.GetSubmittedNotification((int)Utilities.NotificationType.Email, (int)Utilities.RequestStatus.ForgetPassword);
                            if (emaiNotifications != null)
                            {
                                bool isSent = MOEHE.Utilities.HelperMethods.SendNotificationEmail(string.Format(emaiNotifications.Body,  password, password)
                                , emaiNotifications.Subject, SMTPFromAddress, SMTPFromDisplayName, fPResponse.EmailAddress, SMTPServer, SMTPServerPort, "", "", true, new List<System.Net.Mail.Attachment>());
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(ex);
                    }
                    //var loginUrl = MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "LoginUrl");
                    //if (String.IsNullOrEmpty(loginUrl))
                    //{
                    //    loginUrl = SPContext.Current.Web.Url +"/Pages/HomeRegister.aspx";
                    //    lnkNavigateToLogin.NavigateUrl = loginUrl;

                    //}
                    //else
                    //{
                    //    lnkNavigateToLogin.NavigateUrl = SPContext.Current.Web.Url + loginUrl;

                    //}
                    lnkNavigateToLogin.NavigateUrl = LoginUrl;
                    pnlForm.Visible = false;
                    pnlSuccess.Visible = true;
                    lblCreatedUserName.Text = forgetPasswordValidation.UserProfile.UserName;
                   // lblCreatedUserPassword.Text = password;
                }
                else if (fPResponse.ForgotPasswordValidationCode ==  ForgotPasswordValidationCode.InvalidQatarId)
                {
                    lblSuccess.Text = string.Format(MOEHE.Utilities.HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "InvalidQatarId", (uint)LCID));
                    lblSuccess.ForeColor = System.Drawing.Color.Red;
                    modalPopUpConfirmation.Show();
                }
                else if (fPResponse.ForgotPasswordValidationCode ==  ForgotPasswordValidationCode.NotMatchedEmailAddress)
                {
                    lblSuccess.Text = string.Format(MOEHE.Utilities.HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "WrongEmailAddress", (uint)LCID)) + " : " + forgetPasswordValidation.UserProfile.EmailAddress;
                    modalPopUpConfirmation.Show();
                }
                else if (fPResponse.ForgotPasswordValidationCode ==  ForgotPasswordValidationCode.UnexpectedError)
                    modalPopUpConfirmation.Show();
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
            //var loginUrl = MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "LoginUrl");
            //if (String.IsNullOrEmpty(loginUrl))
            //{
            //    loginUrl = SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.Common/MOEHELogIn.aspx";
            //}
            //SPUtility.Redirect(loginUrl, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            Response.Redirect(LoginUrl);
        }
        protected void BtnSendPassword_Click(object sender, EventArgs e)
        {
            ForgotPassword();
        }
        private void Cancel ()
        {
            pnlForm.Visible = true;
            pnlSuccess.Visible = false;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        protected void btnCancelLogin_Click(object sender, EventArgs e)
        {
            Cancel();
        }
    }
}
