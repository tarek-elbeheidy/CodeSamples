using ITWORX.MOEHEWF.Common.Utilities;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHE.Utilities;
using CommonUtilities = ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using ITWORX.MOEHEWF.Common.Entities;
using System.Timers;
using System.Threading; 
using System.Collections.Generic;
using ITWORX.MOEHE.Integration.SMS;

namespace ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common
{
    public partial class UserVerification : UserControl
    {
        private int lCID = 0;
        public int LCID
        {
            get => lCID;
            private set => lCID = value;
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            LCID = System.Threading.Thread.CurrentThread.CurrentUICulture.LCID;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //public int TimeLeft;
        //public System.Timers.Timer aTimer;
        BlockVerificationResponse BverificationResponse;

        public void ShowWrongVerificationMsg(bool show)
        {
            if (show)
            {
                BlockVerificationResponse verificationResponse = Session["verificationResponse"] as BlockVerificationResponse;
                if (verificationResponse.IsCodeExpired)
                {
                    lblVerificationStatus.Text = string.Format(HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "VerificationCodeExpired", (uint)LCID));
                    tbVerificationCode.Enabled = true;
                }
                else
                {
                    if (verificationResponse.IsBlockValid)
                    {

                        lblVerificationStatus.Text = (LCID == (int)Language.English) ? verificationResponse.MessageEn : verificationResponse.MessageAr;
                        tbVerificationCode.Enabled = false;
                    }
                    else
                    {
                        lblVerificationStatus.Text = string.Format(HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "WrongVerificationCode", (uint)LCID));
                        tbVerificationCode.Enabled = true;
                    }
                }


                lblVerificationStatus.Visible = true;
                pnlValidation.Visible = true;

            }
            else
            {
                lblVerificationStatus.Visible = false;
                pnlValidation.Visible = false;
                tbVerificationCode.Enabled = true;
            }
        }
        public void VerifyCode()
        {

            Session["verificationResponse"] = BverificationResponse =
                 CallingIdentityApiHelper.BlockVerification(new UserVerificationCode()
                 { MobileNumber = Session["userMobileNumber"].ToString(), VerificationCode = tbVerificationCode.Text,CreationDate= Session["CreationDate"].ToDate() });

           
            if (BverificationResponse.IsBlockValid)
            {
                Session["BlockingStartTime"] = DateTime.Now;

                StartBlockTimer(BverificationResponse.BlockTimeInSeconds);
             

                //aTimer = new System.Timers.Timer();
                //aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                //aTimer.Interval = 1000;
                //aTimer.Enabled = true;
                //TimeLeft = BverificationResponse.BlockTimeInSeconds;
                //aTimer.Start();
            }
            
        }

        public void StartBlockTimer(double blockTimeInSeconds)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "StartBlockTime",
              "StartBlockTime(" + "'" + blockTimeInSeconds + "'" + ");", true);
        }

        protected void BtnSendCode_Click(object sender, EventArgs e)
        {


        }

        //public void OnTimedEvent(object sender, ElapsedEventArgs e)
        //{
        //    if (TimeLeft < 0)
        //    {
        //        aTimer.Close();
        //        lblBlockingTime.Text = " ";
        //        tbVerificationCode.Enabled = true;

        //    }
        //    else
        //    {
        //        tbVerificationCode.Enabled = false;

        //        lblBlockingTime.Text = TimeLeft + "Seconds";
        //        TimeLeft = TimeLeft - 1;

        //    }

        //}

      


        protected void lBtnResendCode_Click(object sender, EventArgs e)
        {
            tbVerificationCode.Text = string.Empty;
            var ReSendCodeResponse =CallingIdentityApiHelper.ResendVerificationCode(Session["userMobileNumber"].ToString());

            if (ReSendCodeResponse != null)
            {
                SendEmail(ReSendCodeResponse);
                SendSMS(ReSendCodeResponse, ReSendCodeResponse);
            }
        }

        private void SendSMS(PRResponse response, PRResponse ReSendCodeResponse)
        {
            if (ReSendCodeResponse.IsValid)
            {
                var smsEnabledConfig = HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "SMS:Enabled");
                var smsEnabled = true;



                if (!string.IsNullOrEmpty(smsEnabledConfig))
                {
                    bool.TryParse(smsEnabledConfig, out smsEnabled);
                }

                if (smsEnabled == false)
                {
                    ReSendCodeResponse.MessageEn = "Verification Code is sent Successfully to " + Session["userMobileNumber"].ToString();
                    ReSendCodeResponse.MessageAr = " تم ارسال الكود التفعيلى بنجاح لرقم " + Session["userMobileNumber"].ToString();
                    return;
                }

                string statusCode = null;

                try
                {
                    //statusCode = MOEHE.Integration.SMS.Texting.SendSMS(Session["userMobileNumber"].ToString(), ReSendCodeResponse.MessageEn);
                    Entities.Notifications smsNotifications = BL.Notifications.GetSubmittedNotification((int)Utilities.NotificationType.SMS, (int)CommonUtilities.RequestStatus.RegistrationVerificationCode);
                    if (smsNotifications != null)
                    {
                        //Send SMS here
                        statusCode = Texting.SendSMS(Session["userMobileNumber"].ToString(), string.Format(smsNotifications.Body, ReSendCodeResponse.MessageEn, ReSendCodeResponse.MessageEn));
                    }
                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }

                if (statusCode == "1000")
                {
                    ReSendCodeResponse.MessageEn = "Verification Code is sent Successfully to " + Session["userMobileNumber"];
                    ReSendCodeResponse.MessageAr = " تم ارسال الكود التفعيلى بنجاح لرقم " + Session["userMobileNumber"];
                }
                else
                {
                    ReSendCodeResponse.IsValid = false;
                    ReSendCodeResponse.MessageEn = "Failed to send Verification Code to " + Session["userMobileNumber"];
                    ReSendCodeResponse.MessageAr = "فشل فى إرسال الكود التفعيلى إلى " + Session["userMobileNumber"];

                    if (statusCode != null)
                    {
                        var statusMessage = "";
                        if (MOEHE.Integration.SMS.Texting.StatusCode.ContainsKey(statusCode))
                        {
                            statusMessage = MOEHE.Integration.SMS.Texting.StatusCode[statusCode];
                        }
                        Logging.GetInstance().LogException(new Exception(
                            string.Format("Failed to send verification to [{0}]. {1}: {2}.", Session["userMobileNumber"].ToString(), statusCode, statusMessage)));
                    }
                }
            }
        }


        private void SendEmail(PRResponse sendCodeResponse)
        {
            try
            {
                if (sendCodeResponse.IsValid)
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
                              emaiNotifications.Subject, SMTPFromAddress, SMTPFromDisplayName, Session["Email"].ToString(), SMTPServer, SMTPServerPort, SMTPUserName, SMTPPassword, true, new List<System.Net.Mail.Attachment>());
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
        }

        internal void UpdateblockVerificationMsg(double remainingTimeInSeconds)
        {

            if (remainingTimeInSeconds == 0)
            {
                tbVerificationCode.Enabled = true;
                pnlValidation.Visible = false;
               // lblVerificationStatus.Visible = false;

            }
            else
            {
               // lblVerificationStatus.Visible = true;
                pnlValidation.Visible = true;
                lblVerificationStatus.Text = string.Format(HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "RemainingblockTime", (uint)LCID)) + remainingTimeInSeconds;
                tbVerificationCode.Enabled = false;
            }
           
        }
    }
}

