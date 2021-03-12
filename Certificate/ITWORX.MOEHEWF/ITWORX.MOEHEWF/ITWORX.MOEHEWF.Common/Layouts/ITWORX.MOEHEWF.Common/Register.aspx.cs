 
using ITWORX.MOEHE.Integration.SMS;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.BL;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration.Claims;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Timers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using CommonUtilities = ITWORX.MOEHEWF.Common.Utilities;
using System.Collections.Generic;
using System.Threading;

namespace ITWORX.MOEHEWF.Common.Layouts.ITWORX.MOEHEWF.Common
{
    public partial class Register : UnsecuredLayoutsPageBase
    {
        // protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.SendVerification sendVerification;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.UserVerification userVerification;
        public string LoginUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["LoginUrl"];
            }
        }
        protected override bool AllowAnonymousAccess { get { return true; } }

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);
            LCID = System.Threading.Thread.CurrentThread.CurrentUICulture.LCID;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
          
            tbPassword.Attributes["type"] = "password";

            if (!Page.IsPostBack)
            {
                BindDropDowns();
            }

        }

        protected void wizardRegisteration_PreRender(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering Register.wizardNewRegisteration_PreRender");
            try
            {
                Repeater SideBarList = wizardRegisteration.FindControl("HeaderContainer").FindControl("SideBarList") as Repeater;
                SideBarList.DataSource = wizardRegisteration.WizardSteps;
                SideBarList.DataBind();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit Register.wizardNewRegisteration_PreRender");
            }
        }
        protected void OKBtn_OnClick(object sender, EventArgs e)
        {

        }

        public string GetClassForWizardStep(object wizardStep)
        {
            Logging.GetInstance().Debug("Entering Register.GetClassForWizardStep");
            string stepText = string.Empty;
            try
            {
                WizardStep step = wizardStep as WizardStep;

                if (step == null)
                {
                    stepText = "";
                }
                int stepIndex = wizardRegisteration.WizardSteps.IndexOf(step);

                if (stepIndex > wizardRegisteration.ActiveStepIndex)
                {
                    stepText = "nextStep";
                }
                else
                {
                    stepText = "currentStep";
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit Register.GetClassForWizardStep");
            }
            return stepText;
        }

        // private WizardNavigationEventArgs wizardNavigationNextBtnEvent;

        protected void wizardRegisteration_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {

                Logging.GetInstance().Debug("Enter Register.wizardRegisteration_NextButtonClick");
                try
                {
                    if (Page.IsValid)
                    {
                        if (e.CurrentStepIndex == 0)
                        {
                            Session["userMobileNumber"] = tbMobileNumber.Text;
                            Session["Email"] = txtEmail.Text;

                            var MOIUserValidation = CallingIdentityApiHelper.GetMOIUserValidation(new MOIValidationInfo()
                            {
                                QatarId = tbQatarId.Text,

                                DateOfBirth = DateTime.ParseExact(dtDateOfBirth.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),

                            });
                            if (MOIUserValidation == null)
                            {
                                pnlMOIValidation.Visible = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                Page.Session["MOIUserValidation"] = MOIUserValidation.IsValid;
                            }
                            //TODO: don't forget to remove this line
                            //Page.Session["MOIUserValidation"] = true;

                            if (Page.Session["MOIUserValidation"] == null || !Convert.ToBoolean(Page.Session["MOIUserValidation"]))
                            {
                                pnlMOIValidation.Visible = true;
                                e.Cancel = true;
                            }
                            else
                            {
                                pnlMOIValidation.Visible = false;
                                UserCredentials userCredentials = new UserCredentials();//"Administrator",
                                userCredentials.UserName = HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "UserName");
                                userCredentials.Password = HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "Password");
                                userCredentials.LDAPPath = HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "LDAPPath");// DC =MOEHE,DC=EDU,DC=QA";
                                userCredentials.DomainController = HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "DomainController");
                                CreateUserRequest createUserRequest = new CreateUserRequest()
                                {
                                    ADUser = new ADUser()
                                    {
                                        QatarId = tbQatarId.Text
                                    },
                                    UserCredentials = userCredentials

                                };
                                PRResponse pr = CallingIdentityApiHelper.FindUserByQatarId(createUserRequest);
                                if (pr.IsValid)
                                {
                                    lblSuccess.Text = string.Format(HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "SameQatarIdExists", (uint)LCID)) + " : " + createUserRequest.ADUser.QatarId;
                                    lblSuccess.ForeColor = System.Drawing.Color.Red;
                                    modalPopUpConfirmation.Show();
                                    e.Cancel = true;
                                }
                                else if (!pr.IsValid)
                                {

                                    MOIUserProfile moIuserProfile = CallingIdentityApiHelper.GetMOIUserProfile(tbQatarId.Text);
                                    if (moIuserProfile == null) return;
                                    bool passwordValid = true;

                                    string displayName = moIuserProfile.DisplayName;
                                    var splittedDisplayName = moIuserProfile.DisplayName.ToLower().Split(' ');
                                   
                                    string lowerPassword = tbPassword.Text.ToLower();
                                    foreach (var splitted in splittedDisplayName)
                                    {
                                        if (lowerPassword.Contains(splitted))
                                        {
                                            litInvalidPass.Text = string.Format(HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "PasswordContainsUsername", (uint)LCID), displayName);
                                            litInvalidPass.Visible = true;
                                            passwordValid = false;
                                            break;
                                          
                                        }
                                        
                                    }

                                    if (!passwordValid)
                                    {
                                         
                                       e.Cancel = true;
                                    }
                                    else
                                    {
                                        SendVerificationCode();
                                        Session["CreationDate"] = DateTime.Now;
                                    }
                                }
                                //if (Page.Session["IsVerificationCodeSent"] == null || !Convert.ToBoolean(Page.Session["IsVerificationCodeSent"]))
                                //{
                                //    e.Cancel = true;
                                //}

                            }
                        }
                        //if (e.CurrentStepIndex == 1)
                        //{

                        //        SendVerificationCode();
                        //        if (Page.Session["IsVerificationCodeSent"] == null || !Convert.ToBoolean(Page.Session["IsVerificationCodeSent"]))
                        //        {
                        //            e.Cancel = true;
                        //        }

                        //}
                        //if (e.CurrentStepIndex == 1)
                        //{
                        //    //((Button)wizardRegisteration.FindControl("StepNavigationTemplateContainerID$StepNextButton")).Text =
                        //    //       HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "SendCode", (uint)LCID);

                        //    //if (blockVerificationResponse.IsValid && !blockVerificationResponse.Verified)
                        //    //{
                        //    //    wizardNavigationNextBtnEvent = e;
                        //    //    userVerification.aTimer.Elapsed += new ElapsedEventHandler(user_BlockedTimeEvent);
                        //    //}

                        //    if (blockVerificationResponse.IsValid && !blockVerificationResponse.Verified)
                        //    {
                        //        userVerification.ShowWrongVerificationMsg(true);
                        //        e.Cancel = true;
                        //    }
                        //    else if (!blockVerificationResponse.IsValid && blockVerificationResponse.Verified)
                        //    {
                        //        userVerification.ShowWrongVerificationMsg(false);

                        //        //MOIUserProfile moIuserProfile = GetMOIUserProfile(tbQatarId.Text);
                        //        //if (moIuserProfile != null)
                        //        //{
                        //        //    //lblVNationality.Text = moIuserProfile.Nationality;
                        //        //    //lblVMobileNumber.Text = moIuserProfile.MobileNumber;
                        //        //    //lblVGender.Text = moIuserProfile.Gender;
                        //        //}
                        //    }
                        //    else if (!blockVerificationResponse.IsValid && !blockVerificationResponse.Verified)
                        //    {
                        //        userVerification.ShowWrongVerificationMsg(true);
                        //        e.Cancel = true;

                        //    }



                        //}

                        //if (e.CurrentStepIndex == 3)
                        //{
                        //    ((Button)wizardRegisteration.FindControl("StepNavigationTemplateContainerID$StepNextButton")).Text =
                        //           HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "RegisterUser", (uint)LCID);
                        //    userVerification.VerifyCode();
                        //    BlockVerificationResponse blockVerificationResponse = Page.Session["verificationResponse"]
                        //        as BlockVerificationResponse;
                        //    if (blockVerificationResponse.IsValid || !blockVerificationResponse.Verified)
                        //    {
                        //        wizardNavigationNextBtnEvent = e;

                        //        userVerification.aTimer.Elapsed += new ElapsedEventHandler(user_BlockedTimeEvent);
                        //    }
                        //}
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }
                finally
                {
                    Logging.GetInstance().Debug("Exit Register.wizardRegisteration_NextButtonClick");
                }
            });
        }

        private void user_BlockedTimeEvent(object arg1, ElapsedEventArgs arg2)
        {
            //if (wizardNavigationNextBtnEvent == null) return;
            //if (userVerification.TimeLeft < 0)
            //{
            //    wizardNavigationNextBtnEvent.Cancel = false;
            //}
            //else
            //{
            //    wizardNavigationNextBtnEvent.Cancel = true;
            //}
        }


        protected void wizardRegisteration_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                try
                {
                    Logging.GetInstance().Debug("Entering method Register.wizardRegisteration_FinishButtonClick");

                    //((Button)wizardRegisteration.FindControl("StepNavigationTemplateContainerID$StepNextButton")).Text =
                    //           HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "RegisterUser", (uint)LCID);
                    BlockVerificationResponse blockVerificationResponse = Page.Session["verificationResponse"] as BlockVerificationResponse;
                    if (blockVerificationResponse != null && blockVerificationResponse.IsBlockValid && !blockVerificationResponse.CodeVerified)
                    {
                        if (Session["BlockingStartTime"] != null)
                        {
                            DateTime BlockingStartTime = Convert.ToDateTime(Session["BlockingStartTime"]);
                            DateTime BlockingEndTime = BlockingStartTime.AddSeconds(blockVerificationResponse.BlockTimeInSeconds);
                            DateTime now = DateTime.Now;
                            if (now.CompareTo(BlockingStartTime) >= 0 && now.CompareTo(BlockingEndTime) <= 0)
                            {
                                double remainingTimeInSeconds = (BlockingEndTime - now).TotalSeconds;
                                userVerification.UpdateblockVerificationMsg(remainingTimeInSeconds);
                                userVerification.StartBlockTimer(remainingTimeInSeconds);
                                if (remainingTimeInSeconds > 0)
                                {
                                    e.Cancel = true;
                                    return;
                                }
                                else if (remainingTimeInSeconds == 0)
                                {
                                    Page.Session["verificationResponse"] = null;
                                    e.Cancel = true;
                                    return;
                                }
                                
                            }
                            else
                            {
                                userVerification.UpdateblockVerificationMsg(0);
                                Page.Session["verificationResponse"] = null;
                                   // e.Cancel = true;
                                    return;
                            }

                        }
                    }

                    userVerification.VerifyCode();
                    blockVerificationResponse = Page.Session["verificationResponse"] as BlockVerificationResponse;
                    if (blockVerificationResponse.IsBlockValid && !blockVerificationResponse.CodeVerified)
                    {

                        userVerification.ShowWrongVerificationMsg(true);

                        e.Cancel = true;
                    }
                    else if (!blockVerificationResponse.IsBlockValid && blockVerificationResponse.CodeVerified)
                    {
                        userVerification.ShowWrongVerificationMsg(false);
                        CreateUser();

                    }
                    else if (!blockVerificationResponse.IsBlockValid && !blockVerificationResponse.CodeVerified)
                    {
                        userVerification.ShowWrongVerificationMsg(true);
                        e.Cancel = true;

                    }

                    Session["CreationDate"] = null;
                    Page.Session.Remove("SendCodeResponse");
                    Page.Session.Remove("MOIUserValidation");
                    //}
                }

                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }
                finally
                {
                    Logging.GetInstance().Debug("Exiting method Register.wizardNewRequest_FinishButtonClick");
                }
            });
        }

        private void CreateUser()
        {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                try
                {
                    Logging.GetInstance().Debug("Entering method Register.CreateUser");
                    UserCredentials userCredentials = new UserCredentials();//"Administrator",
                    userCredentials.UserName = HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "UserName");
                    userCredentials.Password = HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "Password");
                    userCredentials.LDAPPath = HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "LDAPPath");// DC =MOEHE,DC=EDU,DC=QA";
                    userCredentials.DomainController = HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "DomainController");
                   // ADPasswordPattern aDPasswordPattern = CallingIdentityApiHelper.GetADPasswordPattern(userCredentials);
                   //string password = CallingIdentityApiHelper.GeneratePassword(aDPasswordPattern);

                    MOIUserProfile moIuserProfile = CallingIdentityApiHelper.GetMOIUserProfile(tbQatarId.Text);
                     if (moIuserProfile == null) return;

                    SPListItem item = BL.Nationality.GetNationalityISOCode(moIuserProfile.Nationality);
                    CreateUserRequest createUserRequest = new CreateUserRequest()
                    {
                        ADUser = new ADUser()
                        {

                            DisplayName = moIuserProfile.DisplayName,// tbFirstName.Text + " " + tbLastName.Text,
                            //Email = TxtUserName.Text + "@" + HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "Domain") + ".com",
                            Email = txtEmail.Text,
                            QatarId = tbQatarId.Text,
                            GroupName = HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "GroupName"),
                            OU = HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "OU"),
                            MobileNumber = tbMobileNumber.Text,
                            UserName = tbQatarId.Text,
                            Password = tbPassword.Text, //password,
                            Nationality = item["Title"].ToString(),
                            Gender = moIuserProfile.Gender, //rdoGander.SelectedItem.Text.ToString(),
                            NationalityCategory = drp_NationCategory.SelectedItem.Text,
                            Birthdate = dtDateOfBirth.Value
                        },
                        UserCredentials = userCredentials
                    };

                    LoginResponseData loginResponseData = CallingIdentityApiHelper.CreateADUser(createUserRequest);
                    //var loginUrl = HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "LoginUrl");
                    //if (String.IsNullOrEmpty(loginUrl))
                    //{
                    //    loginUrl = SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.Common/MOEHELogIn.aspx";
                    //}
                    if (loginResponseData.ValidationCode ==  ValidationCode.UserCreated)
                    {
                        //modalPopUpConfirmation.Show();
                        var upnSuffix = "@" + HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "UPN");
                        var userName = createUserRequest.ADUser.UserName + upnSuffix;
                        // var userNameWithoutUpn = createUserRequest.ADUser.UserName;
                        CreateSPUser(userName, createUserRequest);

                        try
                        {
                            Applicants.AddApplicant(new Entities.Applicants
                            {
                                PersonalID = createUserRequest.ADUser.QatarId,
                                BirthDate = DateTime.ParseExact(createUserRequest.ADUser.Birthdate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                                ApplicantName = createUserRequest.ADUser.DisplayName,
                                MobileNumber = createUserRequest.ADUser.MobileNumber,
                                ApplicantEmail = createUserRequest.ADUser.Email,
                                ArabicName= moIuserProfile.ArabicName,
                                EnglishName=moIuserProfile.EnglishName,
                                ApplicantGender = !createUserRequest.ADUser.Gender.Equals("M", StringComparison.OrdinalIgnoreCase),
                                Nationality = new Entities.Nationality
                                {
                                    SelectedID = item["ID"].ToString()/*drp_Nationality.SelectedItem.Value*/,
                                    SelectedTitle = LCID == (int)Language.English ? item["Title"].ToString() : item["TitleAr"].ToString() /*drp_Nationality.SelectedItem.Text*/
                                },
                                NationalityCategory = new Entities.NationalityCategory
                                {
                                    SelectedID = drp_NationCategory.SelectedItem.Value,
                                    SelectedTitle = drp_NationCategory.SelectedItem.Text
                                }
                            }, 0);
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }

                        try
                        {
                            Entities.Notifications smsNotifications = Notifications.GetSubmittedNotification((int)Utilities.NotificationType.SMS, (int)CommonUtilities.RequestStatus.RegistrationPassword);
                            if (smsNotifications != null)
                            {
                                //Send SMS here
                                Texting.SendSMS(createUserRequest.ADUser.MobileNumber, string.Format(smsNotifications.Body, userName, userName));
                                //Texting.SendSMS(createUserRequest.ADUser.MobileNumber, string.Format(smsNotifications.Body, createUserRequest.ADUser.Password, "Equivalency", createUserRequest.ADUser.Password, "معادلة الشهادات"));
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        try
                        {
                            string SMTPServer = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPServer");
                            string SMTPServerPort = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPServerPort");
                            string SMTPFromAddress = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPFromAddress");
                            string SMTPFromDisplayName = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPFromDisplayName");
                            string SMTPUserName = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPUserName");
                            string SMTPPassword = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPPassword");

                            Entities.Notifications emaiNotifications = BL.Notifications.GetSubmittedNotification((int)Utilities.NotificationType.Email, (int)Utilities.RequestStatus.RegistrationPassword);
                            if (emaiNotifications != null)
                            {
                                bool isSent = HelperMethods.SendNotificationEmail(string.Format(emaiNotifications.Body, userName, userName),
                                      emaiNotifications.Subject, SMTPFromAddress, SMTPFromDisplayName, createUserRequest.ADUser.Email, SMTPServer, SMTPServerPort, SMTPUserName, SMTPPassword, true, new List<System.Net.Mail.Attachment>());
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }



                        //Response.Redirect(loginUrl);
                        //SPUtility.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.Common/MOEHELogIn.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

                        pnlForm.Visible = false;
                        pnlSuccess.Visible = true;
                        lblCreatedUserName.Text = createUserRequest.ADUser.UserName;
                        lnkNavigateToLogin.NavigateUrl = LoginUrl;
                        //lblCreatedUserPassword.Text = createUserRequest.ADUser.Password;

                    }
                    else if (loginResponseData.ValidationCode ==  ValidationCode.ExistingUser)
                    {
                        lblSuccess.Text = string.Format(HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "UserExists", (uint)LCID));
                        lblSuccess.ForeColor = System.Drawing.Color.Red;
                        modalPopUpConfirmation.Show();
                        // LoginNavigationHyperLink.NavigateUrl = loginUrl;

                    }
                    else if (loginResponseData.ValidationCode == ValidationCode.InvalidQatarId)
                    {
                        lblSuccess.Text = string.Format(HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "SameQatarIdExists", (uint)LCID)) + " : " + createUserRequest.ADUser.QatarId;
                        lblSuccess.ForeColor = System.Drawing.Color.Red;
                        modalPopUpConfirmation.Show();
                        //  LoginNavigationHyperLink.NavigateUrl = loginUrl;
                    }
                    else if (loginResponseData.ValidationCode ==  ValidationCode.InvalidData || loginResponseData.ValidationCode ==  ValidationCode.UnexpectedError)
                    {
                        lblSuccess.Text = string.Format(HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "InvalidRegisterationData", (uint)LCID));
                        lblSuccess.ForeColor = System.Drawing.Color.Red;
                        modalPopUpConfirmation.Show();
                        // LoginNavigationHyperLink.NavigateUrl = loginUrl;
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
                    Logging.GetInstance().Debug("Exiting method Register.CreateUser");
                }
            });
        }

        private void CreateSPUser(string userName, CreateUserRequest userInfo)
        {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                try
                {
                    var adfsIssuerIdentifier = HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "ADFS:IssuerIdentifier");

                    Logging.GetInstance().Debug("Entering method Register.CreateSPUser");
                    Microsoft.SharePoint.SPSecurity.RunWithElevatedPrivileges(delegate
                    {
                        using (SPSite spSite = new SPSite(SPContext.Current.Site.ID))
                        {
                            using (SPWeb spWeb = spSite.OpenWeb())
                            {
                                spWeb.AllowUnsafeUpdates = true;

                                SPUser spUser = null;

                                if (String.IsNullOrEmpty(adfsIssuerIdentifier))
                                {
                                    spUser = spWeb.EnsureUser(userName);
                                }
                                else
                                {
                                    spUser = ResolveAdfsPrincipal(spWeb, userName, adfsIssuerIdentifier);
                                }

                                if (spUser != null)
                                {
                                    SPGroup spGroup = spWeb.SiteGroups[CommonUtilities.Constants.ApplicanstGroupName];
                                    spGroup.AddUser(spUser);
                                }
                                else
                                {
                                    CallingIdentityApiHelper.DeleteUser(userInfo);
                                    throw new Exception(String.Format("Failed to resolve user [{0}] from SharePoint.", userName));
                                }

                                spWeb.AllowUnsafeUpdates = false;
                            }
                        }
                    });
                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                    CallingIdentityApiHelper.DeleteUser(userInfo);
                }
                finally
                {
                    Logging.GetInstance().Debug("Exit Method Register.CreateSPUser");
                }
            });
        }

        private SPUser ResolveAdfsPrincipal(SPWeb web, string email, string issuerIdentifier)
        {
            SPUser user = null;
            if (!SPClaimProviderManager.IsEncodedClaim(email))
            {
                SPClaim claim = SPClaimProviderManager.CreateUserClaim(email, SPOriginalIssuerType.TrustedProvider, issuerIdentifier);
                if (claim != null)
                {
                    string userClaim = claim.ToEncodedString();
                    user = web.EnsureUser(userClaim);
                }
            }
            return user;
        }

        private void BindDropDowns()
        {
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                try
                {
                    Logging.GetInstance().Debug("Entering method Register.BindDropDowns");

                    //Bind Nationality Category
                    var spNationalityCategoryList = BusinessHelper.GetLookupData(Utilities.Constants.NationalityCategory);
                    if (spNationalityCategoryList != null && spNationalityCategoryList.Count > 0)
                    {
                        List<NationalityListItem> list = new List<NationalityListItem>();
                        foreach (SPListItem spItem in spNationalityCategoryList)
                        {
                            list.Add(new NationalityListItem
                            {
                                ID = spItem.ID,
                                Title = spItem.Title,
                                TitleAr = spItem["TitleAr"].ToString()
                            });
                        }
                        HelperMethods.BindDropDownList(ref drp_NationCategory, list, "ID", "TitleAr", "Title", LCID);
                    }
                    drp_NationCategory.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));

                    //Bind Nationality

                    //var spNationalityList = BusinessHelper.GetLookupDataOrdered(Utilities.Constants.Nationality, LCID);//.GetLookupData(Utilities.Constants.Nationality);
                    //if (spNationalityList != null && spNationalityList.Count > 0)
                    //{
                    //    List<NationalityListItem> list = new List<NationalityListItem>();
                    //    foreach (SPListItem spItem in spNationalityList)
                    //    {
                    //        list.Add(new NationalityListItem
                    //        {
                    //            ID = spItem.ID,
                    //            Title = spItem.Title,
                    //            TitleAr = spItem["TitleAr"].ToString()
                    //        });
                    //    }
                    //    HelperMethods.BindDropDownList(ref drp_Nationality, list, "ID", "TitleAr", "Title", LCID); ;
                    //}
                    //drp_Nationality.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }
                finally
                {
                    Logging.GetInstance().Debug("Exiting method Register.BindDropDowns");
                }
            });
        }


        PRResponse SendCodeResponse;

        public void SendVerificationCode()
        {
            if (SendCodeResponse == null)
            {
                SendCodeResponse = CallingIdentityApiHelper.SendVerificationCode(tbMobileNumber.Text);
            }

            if (SendCodeResponse != null)
            {
                SendEmail(SendCodeResponse);
                SendSMS(SendCodeResponse);

                Session["IsVerificationCodeSent"] = SendCodeResponse.IsValid;
                if (Thread.CurrentThread.CurrentCulture.LCID == (int)Language.English)
                {
                    lblVerificationStatus.Text = SendCodeResponse.MessageEn;
                }
                else
                {
                    lblVerificationStatus.Text = SendCodeResponse.MessageAr;
                }
            }
        }

        private void SendEmail(PRResponse sendCodeResponse)
        {
            try
            {
                string SMTPServer = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPServer");
                string SMTPServerPort = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPServerPort");
                string SMTPFromAddress = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPFromAddress");
                string SMTPFromDisplayName = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPFromDisplayName");
                string SMTPUserName = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPUserName");
                string SMTPPassword = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPPassword");

                Entities.Notifications emaiNotifications = BL.Notifications.GetSubmittedNotification((int)Utilities.NotificationType.Email, (int)Utilities.RequestStatus.RegistrationVerificationCode);
                if (emaiNotifications != null)
                {
                    bool isSent = HelperMethods.SendNotificationEmail(string.Format(emaiNotifications.Body, sendCodeResponse.MessageEn, sendCodeResponse.MessageEn),
                          emaiNotifications.Subject, SMTPFromAddress, SMTPFromDisplayName, txtEmail.Text, SMTPServer, SMTPServerPort, SMTPUserName, SMTPPassword, true, new List<System.Net.Mail.Attachment>());
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
        }

        private void SendSMS(PRResponse response)
        {
            if (SendCodeResponse.IsValid)
            {
                var smsEnabledConfig = HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "SMS:Enabled");
                var smsEnabled = true;
                if (!string.IsNullOrEmpty(smsEnabledConfig))
                {
                    bool.TryParse(smsEnabledConfig, out smsEnabled);
                }

                if (smsEnabled == false)
                {
                    SendCodeResponse.MessageEn = "Verification Code is sent Successfully to " + tbMobileNumber.Text;
                    SendCodeResponse.MessageAr = " تم ارسال الكود التفعيلى بنجاح لرقم " + tbMobileNumber.Text;
                    return;
                }

                string statusCode = null;

                try
                {
                    Entities.Notifications smsNotifications = Notifications.GetSubmittedNotification((int)Utilities.NotificationType.SMS, (int)CommonUtilities.RequestStatus.RegistrationVerificationCode);
                    if (smsNotifications != null)
                    {
                        //Send SMS here
                        statusCode = Texting.SendSMS(tbMobileNumber.Text, string.Format(smsNotifications.Body, SendCodeResponse.MessageEn, SendCodeResponse.MessageEn));
                        //Texting.SendSMS(createUserRequest.ADUser.MobileNumber, string.Format(smsNotifications.Body, createUserRequest.ADUser.Password, "Equivalency", createUserRequest.ADUser.Password, "معادلة الشهادات"));
                    }
                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }
                // statusCode = MOEHE.Integration.SMS.Texting.SendSMS(tbMobileNumber.Text, SendCodeResponse.MessageEn);
                if (statusCode == "1000")
                {
                    SendCodeResponse.MessageEn = "Verification Code is sent Successfully to " + tbMobileNumber.Text;
                    SendCodeResponse.MessageAr = " تم ارسال الكود التفعيلى بنجاح لرقم " + tbMobileNumber.Text;
                }
                else
                {
                    SendCodeResponse.IsValid = false;
                    SendCodeResponse.MessageEn = "Failed to send Verification Code to " + tbMobileNumber.Text;
                    SendCodeResponse.MessageAr = "فشل فى إرسال الكود التفعيلى إلى " + tbMobileNumber.Text;

                    if (statusCode != null)
                    {
                        var statusMessage = "";
                        if (MOEHE.Integration.SMS.Texting.StatusCode.ContainsKey(statusCode))
                        {
                            statusMessage = MOEHE.Integration.SMS.Texting.StatusCode[statusCode];
                        }
                        Logging.GetInstance().LogException(new Exception(
                            string.Format("Failed to send verification to [{0}]. {1}: {2}.", tbMobileNumber.Text, statusCode, statusMessage)));
                    }
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //var loginUrl = HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "LoginUrl");
            //if (!String.IsNullOrEmpty(loginUrl))
            //{
              SPUtility.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.Common/Register.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

            //}
            //Response.Redirect(LoginUrl);
        }

        protected void btnCancel_Click(object sender, EventArgs e)

        {

            SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/HomeRegister.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

        }
    }
    

    internal class NationalityListItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }

    }
}