using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEHE.Portal.Tawzeef.Model;

using System.IO;
using System.Web.Script.Serialization;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using RestSharp;
using MOEHE.Portal.Tawzeef.Entity;
using System.Configuration;
using System.Security.Cryptography;


namespace MOEHE.PSPES.WebAPI.Repository.NSIS
{
    public class NSISHelper
    {
        public static StudentPersonalData GetStudentPersonal(string studentID)
        {
            string PortalUserName = "svc_nsis_moehe_cert";

            string PortalPassword = "pD6~V#9_&v-6#c`;%YL:+.M";
            string urlAuth = "https://nsis.sec.gov.qa/Authentication_JSON_AppService.axd/Login";

            string CookieHeader = GetCookieHeader(urlAuth, PortalUserName, PortalPassword);
            

            string url = ConfigurationManager.AppSettings["NSIS_StudentContacts"];
            object result = string.Empty;
            // Uses the System.Net.WebClient and not HttpClient, because .NET 2.0 must be supported.
            using (var client = new WebClient())
            {
                // Set the header so it knows we are sending JSON.
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                //string PortalUserName = System.Web.Configuration.WebConfigurationManager.AppSettings["PortalUserName"].ToString();

                //string PortalPassword = Decrypt(System.Web.Configuration.WebConfigurationManager.AppSettings["PortalPassword"].ToString(), true);

                
               

                client.Headers["Cookie"] = CookieHeader;
                client.Headers["Content-Type"] = "application/json";
                // Create the one and only "options" parameter object.
                var dto = "{\"studentID\":\"" + studentID + "\"}";
                // Serialise the data we are sending in to JSON
                string serialisedData = JsonConvert.SerializeObject(dto);
                // Make the request
                 var response = client.UploadString(url, dto);
                // Deserialise the response into a GUID
                result = JsonConvert.DeserializeObject(response);
                // var myListOfItems = JsonConvert.DeserializeObject<List<ListOfValues_Model>>(response);

            }
            StudentPersonalData allResult = StudentPersonalData.FromJson(result.ToString());
           
            return allResult;

        }


        public static string GetCookieHeader(string Uri, string UserName, string Password)
        {


            using (var client = new WebClient())
            {
                client.Headers["Content-Type"] = "application/json";
                client.Headers["Cookie"] = "Culture=ar-QA; UICulture=ar-QA;";
                var data = "{\"userName\":\"" + UserName + "\",\"password\":\"" + Password + "\",\"createPersistentCookie\":false}";
                var response = client.UploadString(Uri, data);
                if (response != "{\"d\":true}")
                {
                    return null;
                }
                var cookie = client.ResponseHeaders["Set-Cookie"];
                client.Headers["Cookie"] = cookie;
                return cookie;
            }

        }

        public static EnrollmentsData GetEnrollments(string studentID, string schoolYearID)
        {
            string PortalUserName = NSISLinkSettingsRepository.GetServiceLink(TawzeefConstants.NSISUserName);

            string PortalPassword = NSISLinkSettingsRepository.GetServiceLink(TawzeefConstants.NSISUserPAssport);
            string urlAuth = NSISLinkSettingsRepository.GetServiceLink(TawzeefConstants.AuthenticationLink);

            string CookieHeader = GetCookieHeader(urlAuth, PortalUserName, PortalPassword);
            


            string url = NSISLinkSettingsRepository.GetServiceLink(TawzeefConstants.GetEnrollments);
            object result = string.Empty;
            // Uses the System.Net.WebClient and not HttpClient, because .NET 2.0 must be supported.
            using (var client = new WebClient())
            {
                // Set the header so it knows we are sending JSON.
               

                client.Headers["Cookie"] = CookieHeader;
                client.Headers["Content-Type"] = "application/json";

                // Create the one and only "options" parameter object.
                var dto = "{\"studentQID\":" + studentID + ",\n\"schoolYearID\":" + schoolYearID + "\n}";
                // Serialise the data we are sending in to JSON
                string serialisedData = JsonConvert.SerializeObject(dto);
                // Make the request
                var response = client.UploadString(url, dto);
                // Deserialise the response into a GUID
                result = JsonConvert.DeserializeObject(response);
                // var myListOfItems = JsonConvert.DeserializeObject<List<ListOfValues_Model>>(response);

            }
            EnrollmentsData allResult = EnrollmentsData.FromJson(result.ToString());
            return allResult;

        }
        public static SchoolsData GetSchools(string schoolYearID, string schoolID, string isPublic)
        {
            string PortalUserName = NSISLinkSettingsRepository.GetServiceLink(TawzeefConstants.NSISUserName);

            string PortalPassword = NSISLinkSettingsRepository.GetServiceLink(TawzeefConstants.NSISUserPAssport);
            string urlAuth = NSISLinkSettingsRepository.GetServiceLink(TawzeefConstants.AuthenticationLink);

            string CookieHeader = GetCookieHeader(urlAuth, PortalUserName, PortalPassword);
            

            string url = NSISLinkSettingsRepository.GetServiceLink(TawzeefConstants.GetSchools);
            object result = string.Empty;
            // Uses the System.Net.WebClient and not HttpClient, because .NET 2.0 must be supported.
            using (var client = new WebClient())
            {
                // Set the header so it knows we are sending JSON.
                
                client.Headers["Cookie"] = CookieHeader;
                client.Headers["Content-Type"] = "application/json";
                // Create the one and only "options" parameter object.
                var dto = "{\"schoolYearID\":" + schoolYearID + ",\n\"shoolID\":" + schoolID + ",\n\"isPublic\":" + isPublic + "\n}";
                // Serialise the data we are sending in to JSON
                string serialisedData = JsonConvert.SerializeObject(dto);
                // Make the request
                var response = client.UploadString(url, dto);
                // Deserialise the response into a GUID
                result = JsonConvert.DeserializeObject(response);
                // var myListOfItems = JsonConvert.DeserializeObject<List<ListOfValues_Model>>(response);

            }
            SchoolsData allResult = SchoolsData.FromJson(result.ToString());
            return allResult;

        }

        public static MarksData GetMarks(string studentID, string schoolYearID)
        {
            string PortalUserName = NSISLinkSettingsRepository.GetServiceLink(TawzeefConstants.NSISUserName);

            string PortalPassword = NSISLinkSettingsRepository.GetServiceLink(TawzeefConstants.NSISUserPAssport);
            string urlAuth = NSISLinkSettingsRepository.GetServiceLink(TawzeefConstants.AuthenticationLink);

            string CookieHeader = GetCookieHeader(urlAuth, PortalUserName, PortalPassword);
            

            string url = NSISLinkSettingsRepository.GetServiceLink(TawzeefConstants.GetMarks);
            object result = string.Empty;
            // Uses the System.Net.WebClient and not HttpClient, because .NET 2.0 must be supported.
            using (var client = new WebClient())
            {
               
                client.Headers["Cookie"] = CookieHeader;
                client.Headers["Content-Type"] = "application/json";



                // Create the one and only "options" parameter object.
                var dto = "{\"studentID\":" + studentID + ",\n\"schoolYearID\":" + schoolYearID + "\n}";

                // Serialise the data we are sending in to JSON
                string serialisedData = JsonConvert.SerializeObject(dto);
                // Make the request
               var response = client.UploadString(url, dto);
                // Deserialise the response into a GUID
                result = JsonConvert.DeserializeObject(response);
                // var myListOfItems = JsonConvert.DeserializeObject<List<ListOfValues_Model>>(response);

            }
            MarksData allResult = MarksData.FromJson(result.ToString());
            return allResult;

        }

        public static NationalitiyData GetCodes(string codesetID)
        {
            string PortalUserName = NSISLinkSettingsRepository.GetServiceLink(TawzeefConstants.NSISUserName);

            string PortalPassword = NSISLinkSettingsRepository.GetServiceLink(TawzeefConstants.NSISUserPAssport);
            string urlAuth = NSISLinkSettingsRepository.GetServiceLink(TawzeefConstants.AuthenticationLink);

            string CookieHeader = GetCookieHeader(urlAuth, PortalUserName, PortalPassword);
            

            string url = NSISLinkSettingsRepository.GetServiceLink(TawzeefConstants.GetCodes);
            object result = string.Empty;
            // Uses the System.Net.WebClient and not HttpClient, because .NET 2.0 must be supported.
            using (var client = new WebClient())
            {
                // Set the header so it knows we are sending JSON.

                client.Headers["Cookie"] = CookieHeader;
                // Create the one and only "options" parameter object.
                var dto = "{\"codesetID\":\"" + codesetID + "\"}";
                // Serialise the data we are sending in to JSON
                string serialisedData = JsonConvert.SerializeObject(dto);
                // Make the request
                var response = client.UploadString(url, dto);
                // Deserialise the response into a GUID
                result = JsonConvert.DeserializeObject(response);
                // var myListOfItems = JsonConvert.DeserializeObject<List<ListOfValues_Model>>(response);

            }
          NationalitiyData allResult = NationalitiyData.FromJson(result.ToString());
          return allResult;
        }


        //public static string Decrypt(string cipherString, bool useHashing)
        //{
        //    byte[] keyArray;
        //    byte[] toEncryptArray = Convert.FromBase64String(cipherString);
        //    System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
        //    string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));

        //    if (useHashing)
        //    {
        //        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        //        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        //        hashmd5.Clear();
        //    }
        //    else
        //    {
        //        keyArray = UTF8Encoding.UTF8.GetBytes(key);
        //    }

        //    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //    tdes.Key = keyArray;
        //    tdes.Mode = CipherMode.ECB;
        //    tdes.Padding = PaddingMode.PKCS7;

        //    ICryptoTransform cTransform = tdes.CreateDecryptor();
        //    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        //    tdes.Clear();
        //    return UTF8Encoding.UTF8.GetString(resultArray);
        //}











    }
}
