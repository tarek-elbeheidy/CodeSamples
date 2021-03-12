 
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace ITWORX.MOEHEWF.Common.Utilities
{
    [Serializable]
    public class MOIValidationInfo
    {
        public string QatarId { set; get; }
        public DateTime DateOfBirth { set; get; }
    }
    [Serializable]
    public class PRResponse
    {
        public bool IsValid { get; set; }
        public string MessageAr { get; set; }
        public string MessageEn { get; set; }
    }
    public class MOIUserProfile
    {
        public string Nationality { set; get; }
        public string Gender { set; get; }
        public string QatarID { set; get; }
        public string MobileNumber { set; get; }
        public string DisplayName { get; set; }

        public string ArabicName { get; set; }
        public string EnglishName { get; set; }

        public string BirthDate { get; set; }

    }

    public class UserCredentials
    {
        private string _domainController;
        //public UserCredentials(string UserName, string Password, string LDAPPath)
        //{
        //    this.UserName = UserName;
        //    this.Password = Password;
        //    this.LDAPPath = LDAPPath;
        //}
        //public UserCredentials()
        //{ }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string LDAPPath { set; get; }
        public string DomainController
        {
            get
            {
                return String.IsNullOrEmpty(_domainController) ? null : _domainController;
            }
            set
            {
                _domainController = value;
            }
        }
    }

    public class ADPasswordPattern
    {
        public int MinPwdLength { set; get; }
        public int PwdProperties { set; get; }
        public long lockoutDuration { set; get; }
        public long lockoutObservationWindow { set; get; }
        public int lockoutThreshold { set; get; }
        public long minPwdAge
        {
            set
            {
                MinPwdAge = (value == -1) ? TimeSpan.MaxValue : (TimeSpan.FromTicks(-value));

            }
        }
        public long maxPwdAge
        {
            set
            {
                MaxPwdAge = (value == -1) ? TimeSpan.MaxValue : (TimeSpan.FromTicks(-value));

            }
        }

        public TimeSpan MaxPwdAge { set; get; }
        public int pwdHistoryLength { set; get; }
        public TimeSpan MinPwdAge { get; set; }
        public int Properties { get { return (PwdProperties & 0x00000001) == 0x00000001 ? 2 : -1; } }
    }
    public class UserProfile
    {
        public string UserName { set; get; }
        public string QatarId { set; get; }
        public string EmailAddress { set; get; }
        public string NewPassword { set; get; }
        public string MobileNumber { set; get; }
    }

    public class ADUser
    {
        public string QatarId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { set; get; }
        public string MobileNumber { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string OU { set; get; }
        public string GroupName { set; get; }

        public string Nationality { set; get; }
        public string Gender { set; get; }
        public string NationalityCategory { set; get; }
        public string Birthdate { get; set; }

        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
    }
    public class CreateUserRequest
    {
        public ADUser ADUser { set; get; }
        public UserCredentials UserCredentials { set; get; }
    }
    public enum ValidationCode
    {
        UnexpectedError = 1,
        InvalidQatarId = 2,
        InvalidData = 3,
        AccessDenied = 4,
        ExistingUser = 5,
        UserCreated = 6
    }
    public class LoginResponseData
    {
        public string UserName { get; set; }
        public string MessageAr { get; set; }
        public string MessageEn { get; set; }
        public string Password { get; set; }
        public ValidationCode ValidationCode { get; set; }
    }
    public class ForgetPasswordValidationRequest
    {
        public UserProfile UserProfile { set; get; }
        public UserCredentials UserCredentials { set; get; }

    }
    public enum ResetPasswordValidationCode
    {
        InvalidQatarId = 1,
        NotMatchedPassword = 2,
        LessThanMinPasswordLength = 3,
        expiredPassword = 4,
        PasswordDidntExceedMinAge = 5,
        PasswordchangedSuccessfully = 6,
        ErrorInpasswordPolicy = 7,
        UnexpectedError = 8
    }

    public enum ForgotPasswordValidationCode
    {
        NotMatchedQatarId = 1,
        NotMatchedEmailAddress = 2,
        UnexpectedError = 3,
        PasswordchangedSuccessfully = 4,
        InvalidQatarId = 5,
        NotMatchedPassword = 6
    }
    public class FPResponse
    {
        public ResetPasswordValidationCode ValidationCode { set; get; }
        public ForgotPasswordValidationCode ForgotPasswordValidationCode { set; get; }
        public string MessageAr { set; get; }

        public string MessageEn { set; get; }

        public string EmailAddress { set; get; }

        public string MobileNumber { set; get; }

    }

    public class PasswordReset
    {
        public string UserName { set; get; }
        public string OldPassword { set; get; }
        public string NewPassword { set; get; }
    }
    public class PasswordResetRequest
    {
        public PasswordReset PasswordReset { set; get; }
        public UserCredentials UserCredentials { set; get; }
    }

    public partial class UserVerificationCode
    {
        public string MobileNumber { get; set; }
        public string VerificationCode { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
    }

    [Serializable]
    public class BlockVerificationResponse
    {
        public bool IsBlockValid { get; set; }
        public string MessageAr { get; set; }
        public string MessageEn { get; set; }
        public bool CodeVerified { get; set; }
        public bool IsCodeExpired { get; set; }
        public double BlockTimeInSeconds { set; get; }

    }


    public class CallingIdentityApiHelper
    {
        public static PRResponse GetMOIUserValidation(MOIValidationInfo mOIValidationInfo)
        {
            string errorCode = "0";
            PRResponse objInsightResponse = null;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                try
                {
                    Logging.GetInstance().Debug("Entering Method CallingIdentityApiHelper.GetMOIUserValidation");
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        errorCode = "1";
                        Uri address = new Uri(HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                            "IdentityServiceURL") + "/" + HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url
                            , "MOIValidation"));
                        errorCode = "2";
                        client.BaseAddress = address;
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add
                            (new MediaTypeWithQualityHeaderValue("application/json"));
                        errorCode = "3";
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                           address);
                        errorCode = "4";
                        request.Headers.Authorization =
       new AuthenticationHeaderValue(
       "Basic",
       Convert.ToBase64String(
       System.Text.ASCIIEncoding.ASCII.GetBytes(
       string.Format("{0}:{1}", HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "UserName"),
       HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "Password")))));
                        errorCode = "5";
                        string jsonString = JsonConvert.SerializeObject(mOIValidationInfo,
                            Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
                            { NullValueHandling = NullValueHandling.Ignore });
                        errorCode = "6"+jsonString;
                        request.Content = new StringContent(jsonString, Encoding.UTF8,
                            "application/json");
                        errorCode = "7";
                        
                        //var response = await client.SendAsync(request);
                        //client.Timeout = TimeSpan.FromSeconds(90);
                        errorCode = "7.1"; ;
                        var response = client.SendAsync(request).Result;
                        errorCode = "8";
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            errorCode = "9";
                            var jsonResponse = response.Content.ReadAsStringAsync().Result;
                            errorCode = "10";
                            objInsightResponse = JsonConvert.DeserializeObject<PRResponse>(jsonResponse);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(new Exception("Error Code: " + errorCode ));
                    Logging.GetInstance().LogException(ex);
                }
                finally
                {
                    Logging.GetInstance().Debug("Exit Method  CallingIdentityApiHelper.GetMOIUserValidation");
                }
            });
            return objInsightResponse;
        }
        public static MOIUserProfile GetMOIUserProfile(string QatarId)
        {
            string errorCode = "0";   
            MOIUserProfile objInsightResponse = null;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                try
                {
                    Logging.GetInstance().Debug("Entering Method CallingIdentityApiHelper.GetMOIUserProfile");
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        errorCode = "1";
                        Uri address = null;
                        address = new Uri(HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                                 "IdentityServiceURL") +
                            "/" + HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                                 "GetMOIUserProfile") + "/?QatarId=" + QatarId);
                        client.BaseAddress = address;
                        errorCode = "2";
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add
                            (new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, address);
                        request.Headers.Authorization =
       new AuthenticationHeaderValue(
       "Basic",
       Convert.ToBase64String(
       System.Text.ASCIIEncoding.ASCII.GetBytes(
       string.Format("{0}:{1}", HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "UserName"),
       HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "Password")))));
                        errorCode = "3";
                        var response = client.SendAsync(request).Result;
                        errorCode = "4";
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            errorCode = "5";
                            var jsonResponse = response.Content.ReadAsStringAsync().Result;
                            objInsightResponse = JsonConvert.DeserializeObject<MOIUserProfile>(jsonResponse);

                            //return objInsightResponse;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(new Exception("Error Code: " + errorCode));
                    Logging.GetInstance().LogException(ex);
                }
                finally
                {
                    Logging.GetInstance().Debug("Exit Method CallingIdentityApiHelper.GetMOIUserProfile");
                }
            });
            return objInsightResponse;
        }
        public static ADPasswordPattern GetADPasswordPattern(UserCredentials userCredentials)
        {
            ADPasswordPattern objInsightResponse = null;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                try
                {
                    Logging.GetInstance().Debug("Entering Method CallingIdentityApiHelper.GetADPasswordPattern");
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        Uri address = new Uri(HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                             "IdentityServiceURL") +
                            "/" + HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url
                               , "GetADPasswordPattern"));
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
                        string jsonString = JsonConvert.SerializeObject(userCredentials,
                            Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
                            { NullValueHandling = NullValueHandling.Ignore });
                        request.Content = new StringContent(jsonString, Encoding.UTF8,
                            "application/json");
                        var response = client.SendAsync(request).Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var jsonResponse = response.Content.ReadAsStringAsync().Result;
                            objInsightResponse = JsonConvert.DeserializeObject<ADPasswordPattern>(jsonResponse);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }
                finally
                {
                    Logging.GetInstance().Debug("Exit Method CallingIdentityApiHelper.GetADPasswordPattern");
                }
            });
            return objInsightResponse;
        }
        public static string GeneratePassword(ADPasswordPattern aDPasswordPattern)
        {
            string objInsightResponse = null;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                try
                {
                    Logging.GetInstance().Debug("Entering Method CallingIdentityApiHelper.GeneratePassword");
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        Uri address = new Uri(HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                            "IdentityServiceURL") +
                            "/" + HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                                "GenerateNewPassword"));
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
                        string jsonString = JsonConvert.SerializeObject(aDPasswordPattern,
                            Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
                            { NullValueHandling = NullValueHandling.Ignore });
                        request.Content = new StringContent(jsonString, Encoding.UTF8,
                            "application/json");
                        var response = client.SendAsync(request).Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var jsonResponse = response.Content.ReadAsStringAsync().Result;
                            objInsightResponse = JsonConvert.DeserializeObject<string>(jsonResponse);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }
                finally
                {
                    Logging.GetInstance().Debug("Exit Method CallingIdentityApiHelper.GeneratePassword");
                }
            });
            return objInsightResponse;
        }
        public static LoginResponseData CreateADUser(CreateUserRequest createUserRequest)
        {
            LoginResponseData objInsightResponse = null;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {

                try
                {
                    Logging.GetInstance().Debug("Entering Method CallingIdentityApiHelper.CreateADUser");
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        Uri address = new Uri(HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                             "IdentityServiceURL") +
                            "/" + HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                                 "CreateUser"));
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
                        string jsonString = JsonConvert.SerializeObject(createUserRequest,
                            Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
                            { NullValueHandling = NullValueHandling.Ignore });
                        request.Content = new StringContent(jsonString, Encoding.UTF8,
                            "application/json");
                        var response = client.SendAsync(request).Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var jsonResponse = response.Content.ReadAsStringAsync().Result;
                            objInsightResponse = JsonConvert.DeserializeObject<LoginResponseData>(jsonResponse);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }
                finally
                {
                    Logging.GetInstance().Debug("Exit Method CallingIdentityApiHelper.CreateADUser");
                }
            });
            return objInsightResponse;
        }
        public static FPResponse ForgotPassword(ForgetPasswordValidationRequest forgetPasswordRequest)
        {
            FPResponse objInsightResponse = null;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {

                try
                {
                    Logging.GetInstance().Debug("Entering Method CallingIdentityApiHelper.ForgotPassword");
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        Uri address = new Uri(MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                             "IdentityServiceURL") +
                            "/" + MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                                 "ForgotPassword"));
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
                        string.Format("{0}:{1}", MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "UserName"),
                        MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "Password")))));
                        string jsonString = JsonConvert.SerializeObject(forgetPasswordRequest,
                            Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
                            { NullValueHandling = NullValueHandling.Ignore });
                        request.Content = new StringContent(jsonString, Encoding.UTF8,
                            "application/json");
                        var response = client.SendAsync(request).Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var jsonResponse = response.Content.ReadAsStringAsync().Result;
                            objInsightResponse = JsonConvert.DeserializeObject<FPResponse>(jsonResponse);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }
                finally
                {
                    Logging.GetInstance().Debug("Exit Method CallingIdentityApiHelper.ForgotPassword");
                }
            });
            return objInsightResponse;
        }
        public static FPResponse ChangePassword(PasswordResetRequest passwordResetRequest)
        {
            FPResponse objInsightResponse = null;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {

                try
                {
                    Logging.GetInstance().Debug("Entering Method CallingIdentityApiHelper.ChangePassword");
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        Uri address = new Uri(MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                             "IdentityServiceURL") +
                            "/" + MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                                 "ChangePassword"));
                        client.BaseAddress = address;

                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add
                            (new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                           address);
                        request.Headers.Authorization =
                        new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "UserName"),
                        MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "Password")))));
                        string jsonString = JsonConvert.SerializeObject(passwordResetRequest,
                            Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
                            { NullValueHandling = NullValueHandling.Ignore });
                        request.Content = new StringContent(jsonString, Encoding.UTF8,
                            "application/json");
                        var response = client.SendAsync(request).Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var jsonResponse = response.Content.ReadAsStringAsync().Result;
                            objInsightResponse = JsonConvert.DeserializeObject<FPResponse>(jsonResponse);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }
                finally
                {
                    Logging.GetInstance().Debug("Exit Method CallingIdentityApiHelper.ChangePassword");
                }
            });
            return objInsightResponse;
        }
        public static PRResponse FindUserByQatarId(CreateUserRequest userInfo)
        {
            PRResponse objInsightResponse = null;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {

                try
                {
                    Logging.GetInstance().Debug("Entering Method CallingIdentityApiHelper.FindUserByQatarId");
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        Uri address = new Uri(MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                             "IdentityServiceURL") +
                            "/" + MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                                 "FindUserByQatarId"));
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
                        string.Format("{0}:{1}", MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "UserName"),
                        MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "Password")))));
                        string jsonString = JsonConvert.SerializeObject(userInfo,
                            Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
                            { NullValueHandling = NullValueHandling.Ignore });
                        request.Content = new StringContent(jsonString, Encoding.UTF8,
                            "application/json");
                        var response = client.SendAsync(request).Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var jsonResponse = response.Content.ReadAsStringAsync().Result;
                            objInsightResponse = JsonConvert.DeserializeObject<PRResponse>(jsonResponse);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }
                finally
                {
                    Logging.GetInstance().Debug("Exit Method CallingIdentityApiHelper.FindUserByQatarId");
                }
            });
            return objInsightResponse;

        }
        public static PRResponse IsLoginValid(CreateUserRequest userInfo)
        {
            PRResponse objInsightResponse = null;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {

                try
                {
                    Logging.GetInstance().Debug("Entering Method CallingIdentityApiHelper.IsLoginValid");
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        Uri address = new Uri(MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                             "IdentityServiceURL") +
                            "/" + MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                                 "IsLoginValid"));
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
                        string.Format("{0}:{1}", MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "UserName"),
                        MOEHE.Utilities.HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "Password")))));
                        string jsonString = JsonConvert.SerializeObject(userInfo,
                            Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
                            { NullValueHandling = NullValueHandling.Ignore });
                        request.Content = new StringContent(jsonString, Encoding.UTF8,
                            "application/json");
                        var response = client.SendAsync(request).Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var jsonResponse = response.Content.ReadAsStringAsync().Result;
                            objInsightResponse = JsonConvert.DeserializeObject<PRResponse>(jsonResponse);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }
                finally
                {
                    Logging.GetInstance().Debug("Exit Method CallingIdentityApiHelper.IsLoginValid");
                }
            });
            return objInsightResponse;

        }
        public static PRResponse DeleteUser(CreateUserRequest userInfo)
        {
            PRResponse objInsightResponse = null;
            SPSecurity.RunWithElevatedPrivileges(() =>
            {

                try
                {
                    Logging.GetInstance().Debug("Entering Method CallingIdentityApiHelper.DeleteUser");
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        Uri address = new Uri(HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                            "IdentityServiceURL") +
                            "/" + HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                                "DeleteUser"));
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
                        string jsonString = JsonConvert.SerializeObject(userInfo,
                            Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
                            { NullValueHandling = NullValueHandling.Ignore });
                        request.Content = new StringContent(jsonString, Encoding.UTF8,
                            "application/json");
                        var response = client.SendAsync(request).Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var jsonResponse = response.Content.ReadAsStringAsync().Result;
                            objInsightResponse = JsonConvert.DeserializeObject<PRResponse>(jsonResponse);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }
                finally
                {
                    Logging.GetInstance().Debug("Exit Method CallingIdentityApiHelper.DeleteUser");
                }
            });
            return objInsightResponse;

        }
        public static PRResponse SendVerificationCode(string MobileNumber)
        {
            PRResponse objInsightResponse = null;
            try
            {

                Logging.GetInstance().Debug("Entering Method CallingIdentityApiHelper.SendVerificationCode");
                using (var client = new System.Net.Http.HttpClient())
                {
                    Uri address = new Uri(HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                         "IdentityServiceURL") + "/" + HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
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
                Logging.GetInstance().Debug("Exit Method CallingIdentityApiHelper.SendVerificationCode");
            }
            return objInsightResponse;
        }
        public static BlockVerificationResponse BlockVerification(UserVerificationCode userVerificationCode)
        {
            BlockVerificationResponse objInsightResponse = null;
            try
            {

                Logging.GetInstance().Debug("Entering Method CallingIdentityApiHelper.BlockVerification");
                using (var client = new System.Net.Http.HttpClient())
                {
                    Uri address = new Uri(HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                         "IdentityServiceURL") + "/" + HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                         "BlockUserVerification"));
                    client.BaseAddress = address;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add
                        (new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, address
                       );
                    request.Headers.Authorization =
   new AuthenticationHeaderValue(
   "Basic",
   Convert.ToBase64String(
   System.Text.ASCIIEncoding.ASCII.GetBytes(
   string.Format("{0}:{1}", HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "UserName"),
   HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "Password")))));
                    string jsonString = JsonConvert.SerializeObject(userVerificationCode,
                        Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
                        { NullValueHandling = NullValueHandling.Ignore });
                    request.Content = new StringContent(jsonString, Encoding.UTF8,
                        "application/json");
                    var response = client.SendAsync(request).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;

                        objInsightResponse = JsonConvert.DeserializeObject<BlockVerificationResponse>(jsonResponse);
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
                Logging.GetInstance().Debug("Exit Method CallingIdentityApiHelper.BlockVerification");
            }
            return objInsightResponse;
        }
        public static PRResponse ResendVerificationCode(string MobileNumber)
        {
            PRResponse objInsightResponse = null;
            try
            {

                Logging.GetInstance().Debug("Entering Method CallingIdentityApiHelper.ResendVerificationCode");
                using (var client = new System.Net.Http.HttpClient())
                {
                    Uri address = new Uri(HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                         "IdentityServiceURL") + "/" + HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "SendVerificationCode"));
                    client.BaseAddress = address;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, address);
                    request.Headers.Authorization =
                        new AuthenticationHeaderValue("Basic",
                            Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "UserName"),
                                HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url, "Password")))));

                    string jsonString = JsonConvert.SerializeObject(MobileNumber, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    var response = client.SendAsync(request).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var jsonResponse = response.Content.ReadAsStringAsync().Result;
                        objInsightResponse = JsonConvert.DeserializeObject<PRResponse>(jsonResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit Method CallingIdentityApiHelper.ResendVerificationCode");
            }
            return objInsightResponse;
        }

    }
}
