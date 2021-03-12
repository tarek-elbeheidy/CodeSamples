using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITWORX.MOEHE.Integration.SMS;
using ITWORX.MOEHE.Utilities;
using Microsoft.SharePoint;
using UtilitiesCommon = ITWORX.MOEHEWF.Common.Utilities;
using EntitiesCommon = ITWORX.MOEHEWF.Common.Entities;
using BLCommon = ITWORX.MOEHEWF.Common.BL;
using System.Net.Mail;

namespace ITWORX.MOEHEWF.SCE.BL
{
   public class SCENotifcations
    {


      

        public static void SendSMS(string mobileNumber, string smsBody)
        {
            string smsEnabledConfig = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + UtilitiesCommon.Constants.HEWebUrl, UtilitiesCommon.Constants.Configuration, "SMSSCEEnabled");
            var SMSSCEEnabled = false;

            if (!string.IsNullOrEmpty(smsEnabledConfig))
            {
                bool.TryParse(smsEnabledConfig, out SMSSCEEnabled);
            }

            if (SMSSCEEnabled)
            {

                if (!string.IsNullOrEmpty(smsBody))
                {
                    Texting.SendSMS(mobileNumber, smsBody);
                }

            }
        }


        



        public static void SendEmail(string emailbody, string subject,string Email, bool isAttachmentIncluded, int requestID)
        {

            //send email 
            string SMTPServer = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPServer");
            string SMTPServerPort = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPServerPort");
            string SMTPFromAddress = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPFromAddress");
            string SMTPFromDisplayName = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPPAFromDisplayName");

            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        if (isAttachmentIncluded)
                        {
                            SPList list = web.Lists[Utilities.Constants.SCERequests];
                            SPListItemCollection collListItems = null;
                            List<System.Net.Mail.Attachment> mailAttachments= new List<System.Net.Mail.Attachment>();
                            var camelQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name ='ID'/><Value Type = 'Number'>" +
                                requestID.ToString() + "</Value></Eq></Where>");

                            collListItems = list.GetItems(camelQuery);
                            if (collListItems.Count > 0)
                            {
                                if (collListItems[0].Attachments[1] != null)
                                {
                                    string url = collListItems[0].Attachments.UrlPrefix + collListItems[0].Attachments[1];
                                    SPFile file = list.ParentWeb.GetFile(url);
                                    mailAttachments.Add(new Attachment(file.OpenBinaryStream(), file.Name));
                                }
                            }

                            HelperMethods.SendNotificationEmail(emailbody, subject, SMTPFromAddress, SMTPFromDisplayName, Email, SMTPServer, SMTPServerPort, "", "", true, mailAttachments);
                        }
                        else
                        {
                            HelperMethods.SendNotificationEmail(emailbody, subject, SMTPFromAddress, SMTPFromDisplayName, Email, SMTPServer, SMTPServerPort, "", "", true, new List<System.Net.Mail.Attachment>());
                        }
                    }
                }
            });
          

        }
    }
}
