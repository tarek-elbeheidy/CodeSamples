using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.UI;
using ITWORX.MOEHE.Integration.SMS;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
//using ITWORX.MOEHEWF.Common.TestingCyberSrcTransactionService;
using ITWORX.MOEHEWF.Common.ProductionCyberSrcTransactionService;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;

namespace ITWORX.MOEHEWF.Common.Layouts.ITWORX.MOEHEWF.Common
{
    public partial class paymentreceipt : UnsecuredLayoutsPageBase
    {
        protected override bool AllowAnonymousAccess { get { return true; } }
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            SPSite TestWeb = SPControl.GetContextSite(Context);
            string strUrl = TestWeb.ServerRelativeUrl + "/_catalogs/masterpage/Home1.master";

            this.MasterPageFile = strUrl;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string paymentLogs = "Entering method paymentreceipt.Page_Load || ";
            Logging.GetInstance().Debug("Entering method paymentreceipt.Page_Load");
            string SMTPPassword = string.Empty;
            try
            {
                string logvalue = "";
                
                if (!Page.IsPostBack)
                {
                    lbl_Msg.Text = string.Empty;
                    string req_amount = Request.Params["req_amount"];
                    string signed_date_time = Request.Params["signed_date_time"];
                    string req_reference_number = Request.Params["req_reference_number"];
                    string req_card_type = Request.Params["req_card_type"];
                    string cardNum = Request.Params["req_card_number"];
                    string responseMessage = Request.Params["message"];
                    SPListItem request = BL.Request.GetRequestByNumber(req_reference_number);
                    string statementSubject = HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "UniversityCertificate", SPContext.Current.Web.Language);
                    string requestID = request.ID.ToString();

                    string transaction_id = Request.Params["transaction_id"];

                    //logging response values
                    logvalue = string.Format("response values: req_amount : {0} , signed_date_time : {1}" +
                        " , req_reference_number : {2} ," +
                        " req_card_type : {3} , cardNum : {4} , " +
                        "responseMessage : {5} , requestID : {6} , transaction_id : {7} , reason_code : {8}  ",
                       req_amount, signed_date_time, req_reference_number, req_card_type, cardNum, responseMessage, requestID, transaction_id, Request.Params["reason_code"]);

                    paymentLogs += logvalue + " || ";
                    Logging.GetInstance().Debug(logvalue);

                    string[] reversalReasonCodesArray = ConfigurationManager.AppSettings["ReversalReasonCodes"].Split(',');//200,230,481

                    //for testing purpose for Auth reversal 
                    //if (Request.Params["reason_code"] == "100" && false)
                    if (Request.Params["reason_code"] == "100")
                    {
                        if (Request.QueryString["PS"] == "1")
                        {
                            if (requestID != null)
                            {
                                bool result = BL.Request.updateRequestStatus(Convert.ToInt32(requestID));
                                if (result)
                                {
                                    BL.Request.addNewPaymentRecord(requestID, req_amount, signed_date_time, req_card_type, req_reference_number, cardNum, responseMessage, statementSubject, Request.Params["reason_code"].ToString(), paymentLogs);
                                    BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.RequestHistoricalRecords, (int)SPContext.Current.Web.Language, (int)Utilities.RequestStatus.UCESubmitted, SPContext.Current.Web.CurrentUser.Name, string.Empty, request, "Yes");
                                    if (Page.Session["applicantId"] != null)
                                    {
                                        Entities.Applicants sessionApplicant = BL.Applicants.GetApplicantByID(int.Parse(Page.Session["applicantId"].ToString()), (int)SPContext.Current.Web.Language);
                                        //send email
                                        string SMTPServer = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPServer");
                                        string SMTPServerPort = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPServerPort");
                                        string SMTPFromAddress = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPFromAddress");
                                        string SMTPFromDisplayName = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPFromDisplayName");
                                        string SMTPUserName = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPUserName");
                                         SMTPPassword = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMTPPassword");

                                        Entities.Notifications emaiNotifications = BL.Notifications.GetSubmittedNotification((int)Utilities.NotificationType.Email, (int)Utilities.RequestStatus.UCESubmitted);
                                        if (emaiNotifications != null)
                                        {
                                            HelperMethods.SendNotificationEmail(string.Format(emaiNotifications.Body, req_reference_number, "Equivalency", "<br/>", req_reference_number, "معادلة الشهادات"), emaiNotifications.Subject, SMTPFromAddress, SMTPFromDisplayName, sessionApplicant.ApplicantEmail, SMTPServer, SMTPServerPort, SMTPUserName, SMTPPassword, true, new List<System.Net.Mail.Attachment>());
                                        }
                                        //Send SMS here

                                        try
                                        {
                                            string smsEnabledConfig = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "SMSUCEEnabled");

                                            var SMSUCEEnabled = false;

                                            if (!string.IsNullOrEmpty(smsEnabledConfig))
                                            {
                                                bool.TryParse(smsEnabledConfig, out SMSUCEEnabled);
                                            }

                                            if (Page.Session["UniversityObject"] != null)
                                            {
                                                Entities.University university = (Entities.University)Page.Session["UniversityObject"];
                                                bool IsUniversityHEDD = BL.University.IsUniversityHEDD(int.Parse(university.SelectedID));
                                                bool isUniversityCHED = BL.University.IsUniversityCHED(int.Parse(university.SelectedID));

                                                if (isUniversityCHED)
                                                {
                                                    Entities.Notifications emailCHEDNotification = BL.Notifications.GetSubmittedNotification((int)Utilities.NotificationType.Email, (int)Utilities.RequestStatus.CHEDMessage);
                                                    if (emailCHEDNotification != null)
                                                    {
                                                        HelperMethods.SendNotificationEmail(string.Format(emailCHEDNotification.Body, university.EnglishTitle, university.ArabicTitle), emaiNotifications.Subject, SMTPFromAddress, SMTPFromDisplayName, sessionApplicant.ApplicantEmail, SMTPServer, SMTPServerPort, SMTPUserName, SMTPPassword, true, new List<System.Net.Mail.Attachment>());
                                                    }
                                                }
                                                if (SMSUCEEnabled)
                                                {
                                                    Entities.Notifications smsNotifications = BL.Notifications.GetSubmittedNotification((int)Utilities.NotificationType.SMS, (int)Utilities.RequestStatus.UCESubmitted);
                                                    if (smsNotifications != null)
                                                    {
                                                        Texting.SendSMS(sessionApplicant.MobileNumber, string.Format(smsNotifications.Body, req_reference_number, "Equivalency", req_reference_number, "معادلة الشهادات"));
                                                    }


                                                    if (isUniversityCHED)
                                                    {


                                                        Entities.Notifications smsNotificationsCHED = BL.Notifications.GetSubmittedNotification((int)Utilities.NotificationType.SMS, (int)Utilities.RequestStatus.CHEDMessage);
                                                        if (smsNotificationsCHED != null)
                                                        {
                                                            //Send SMS here
                                                            Texting.SendSMS(sessionApplicant.MobileNumber, string.Format(smsNotifications.Body, university.EnglishTitle, university.ArabicTitle));
                                                        }
                                                    }
                                                    if (IsUniversityHEDD)
                                                    {
                                                        Entities.Notifications smsNotificationsHEDD = BL.Notifications.GetSubmittedNotification((int)Utilities.NotificationType.SMS, (int)Utilities.RequestStatus.HEDDMessage);
                                                        if (smsNotificationsHEDD != null)
                                                        {
                                                            //Send SMS here
                                                            Texting.SendSMS(sessionApplicant.MobileNumber, string.Format(smsNotifications.Body, sessionApplicant.ApplicantName, sessionApplicant.ApplicantName));
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Logging.GetInstance().LogException(ex);
                                        }
                                    }
                                }
                                Page.Session.Remove("applicantId");
                                Page.Session.Remove("UniversityID");

                                lbl_Msg.Text = HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "paymentSuccess", SPContext.Current.Web.Language);

                               
                            }
                        }
                        else if (Request.QueryString["PS"] == "2")
                        {
                            lbl_Msg.Text = HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "paymentCanceled", SPContext.Current.Web.Language);
                        }
                       
                    }
                    else
                    {                       

                        //Reversal reversal scenario
                        foreach (string reversalReasonCode in reversalReasonCodesArray)
                        {
                            if (Request.Params["reason_code"] == reversalReasonCode)
                            {
                                logvalue = "Start Auth reversal scenario";
                                paymentLogs += logvalue + " || ";
                                Logging.GetInstance().Debug(logvalue);

                                ReplyMessage reverseTransactionReply = ReverseTransaction(transaction_id, req_reference_number, req_card_type, ref paymentLogs);
                                logvalue = string.Format("reverseTransactionReply values reasonCode={0} ,decision={1}", reverseTransactionReply.reasonCode, reverseTransactionReply.decision);
                                paymentLogs += logvalue + " || ";
                                Logging.GetInstance().Debug(logvalue);
                                //authontication reversal succedded 
                                if (reverseTransactionReply.reasonCode == "100")
                                {
                                    logvalue = "Auth reversal has been succedded";
                                    paymentLogs += logvalue + " || ";
                                    Logging.GetInstance().Debug(logvalue);
                                }//authontication reversal failed 
                                else
                                {
                                    logvalue = "Auth reversal has been failed";
                                    paymentLogs += logvalue + " || ";
                                    Logging.GetInstance().Debug(logvalue);
                                }
                                logvalue = "End Auth reversal scenario";
                                paymentLogs += logvalue + " || ";

                                Logging.GetInstance().Debug(logvalue);
                                
                                break;
                            }
                        }

                        BL.Request.addNewPaymentRecord(requestID, req_amount, signed_date_time, req_card_type, req_reference_number, cardNum, responseMessage, statementSubject, Request.Params["reason_code"].ToString(), paymentLogs);
                        lbl_Msg.Text = HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "paymentFailed", SPContext.Current.Web.Language);


                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method paymentreceipt.Page_Load");
                SMTPPassword = string.Empty;
            }
        }

        //http://apps.cybersource.com/library/documentation/dev_guides/CC_Svcs_SO_API/Credit_Cards_SO_API.pdf check page 54
        protected ReplyMessage ReverseTransaction(string transaction_id, string requestNumber,string cardType, ref string paymentLogs)
        {
            try
            {
               string logvalue = "start method ReverseTransaction";
                paymentLogs += logvalue + " || ";

                Logging.GetInstance().Debug(logvalue);

                String MERCHANT_ID = "qnb_moed_qar";
                String TRANSACTION_KEY = ConfigurationManager.AppSettings["CyberSourceKey"]; //"Hw/CT7zgEfTIl3q8AFerFDF32LlRmBs6HaUsj0Zzcd218mqHyFJsuA9LioKW4BtEe5ayCl7SLca6HUFOp4ygxj3OtO3c7NG7eLNmME+C0A29zkOlSCwrkwGsOXT8ed8g+VFiOW83xvl+2LC/h3Q713Lr/9bI9QkvnidfWmm4kAjDxEho9IRmbPPcR+KPkLJbGpW1AzN5IX4fP42JKvJUMIC4rVdUSbFZmizfLrzFem0C6cJVm0oAUuZyZrWu1X31mC23+5hYBCqSOfG19opSGj1WrZ8mDNGiwLFyC+wG/oDD6ay532ZSSVqQayHghWMabsTL0P/wP8D4NFR73wvQ6w==";
                String MerchantReferenceNumber = requestNumber;//equals reference_number at the original request //Session["RequestNumber"].ToString();
                String RequestID = transaction_id;
                String Amount = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "CertificateEquivalencyFees");//"300";


                logvalue = string.Format("reversal Request MERCHANT_ID={0}, MerchantReferenceNumber= {1}, MerchantReferenceNumber= {2}, RequestID= {3}, Amount= {4} ",
                    MERCHANT_ID , MerchantReferenceNumber , MerchantReferenceNumber , RequestID , Amount  );
                paymentLogs += logvalue + " || ";

                Logging.GetInstance().Debug(logvalue);

                RequestMessage request = new RequestMessage();

                
                request.merchantID = MERCHANT_ID;

                //request.purchaseTotals = new PurchaseTotals();
                //request.purchaseTotals.currency = "QAR";
                ////check cardtype is Visa
                //if (cardType == "001")
                //{
                //    request.paymentSolution = "visacheckout";
                //}

                // Before using this example, replace the generic value with your
                // reference number for the current transaction.
                request.merchantReferenceCode = MerchantReferenceNumber;
                request.item = new Item[1];

                Item item = new Item();
                item.id = "0";
                item.unitPrice = Amount;
                request.item[0] = item;


                // To help us troubleshoot any problems that you may encounter,
                // please include the following information about your application.
                request.clientLibrary = "certificate equivalency";
                request.clientLibraryVersion = Environment.Version.ToString();
                request.clientEnvironment =
                 Environment.OSVersion.Platform +
                 Environment.OSVersion.Version.ToString();

                // This section contains a sample transaction request for the   authorization 
                // service with complete billing, payment card, and purchase (two items) information.
                request.ccAuthReversalService = new CCAuthReversalService();
                request.ccAuthReversalService.authRequestID = RequestID;

                request.ccAuthReversalService.run = "true";

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                TransactionProcessorClient proc = new TransactionProcessorClient();

                proc.ChannelFactory.Credentials.UserName.UserName = request.merchantID;
                proc.ChannelFactory.Credentials.UserName.Password = TRANSACTION_KEY;

                ReplyMessage reply = proc.runTransaction(request);

                logvalue = "End  method ReverseTransaction";
                paymentLogs += logvalue + " || ";
                Logging.GetInstance().Debug(logvalue);
                return reply;
                
            }
            catch (Exception ex)
            {
                paymentLogs += "Exception " + ex.Message;
                Logging.GetInstance().LogException(ex);
                throw;
            }
        }

        protected void btn_goDashboard_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method paymentreceipt.btn_goDashboard_Click");

            try
            {
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Applicant/Pages/TrackRequest.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method paymentreceipt.btn_goDashboard_Click");
            }
        }
    }
}
