using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHE.Utilities;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using ITWORX.MOEHE.Utilities.Logging;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using ITWORX.MOEHEWF.Common.Utilities; 

namespace ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common
{
    public partial class SendVerification : UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           
           Session["userMobileNumber"] = tbMobileNumber.Text;
         
        }

        public string MobileNumber
        {
            get
            {
                return tbMobileNumber.Text;
            }
        }

        PRResponse SendCodeResponse;

        public void SendVerificationCode()
        {
            if (SendCodeResponse == null)
            {
                SendCodeResponse = SendVerificationCode(tbMobileNumber.Text);
            }

            if (SendCodeResponse != null)
            {
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

        private void SendSMS(PRResponse response)
        {
            if (SendCodeResponse.IsValid)
            {
                var smsEnabledConfig = HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "SMS:Enabled");
                var smsEnabled = true;
                if(!string.IsNullOrEmpty(smsEnabledConfig))
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
                    statusCode = MOEHE.Integration.SMS.Texting.SendSMS(tbMobileNumber.Text, SendCodeResponse.MessageEn);
                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }

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
       
        public static PRResponse SendVerificationCode(string MobileNumber)
        {
            PRResponse objInsightResponse = null;
            try
            {

                Logging.GetInstance().Debug("Entering Method SendVerification.SendVerificationCode");
                using (var client = new System.Net.Http.HttpClient())
                {
                    Uri address = new Uri(HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                         "IdentityServiceURL")+ "/" + HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                         "SendVerificationCode"));
                    client.BaseAddress = address;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add
                        (new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                        address);
                    request.Headers.Authorization =
   new AuthenticationHeaderValue(
   "Basic",
   Convert.ToBase64String(
   System.Text.ASCIIEncoding.ASCII.GetBytes(
   string.Format("{0}:{1}", HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "UserName"),
   HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "Password")))));
                    string jsonString = JsonConvert.SerializeObject(MobileNumber,
                        Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
                        { NullValueHandling = NullValueHandling.Ignore });
                    request.Content = new StringContent(jsonString, Encoding.UTF8,
                        "application/json");
                    var response = client.SendAsync(request).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;

                        objInsightResponse = JsonConvert.DeserializeObject<PRResponse>(jsonResponse);
                        //if (objInsightResponse != null&& objInsightResponse.IsValid)
                        //{
                        //        return true;

                        //}


                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit Method SendVerification.SendVerificationCode");
            }
            return objInsightResponse;
        }
    }
}
