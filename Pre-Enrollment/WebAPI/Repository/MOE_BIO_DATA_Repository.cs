using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;
using System.Configuration;
using Newtonsoft.Json;
using MOEHE.PSPES.WebAPI.Repository.NSIS;
using System.Globalization;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class MOE_BIO_DATA_Repository
    {

        /// <summary>
        /// Used to get Bio Data from MOI service
        /// </summary>
        /// <returns></returns>
        public static MOE_BIO_DATA_Model Get(string QID)
        {

            MOE_BIO_DATA_Model Bio_Model = new MOE_BIO_DATA_Model();
            string serviceURL = ConfigurationManager.AppSettings["MOI_WebAPIURL"];
            string serviceTokenName = ConfigurationManager.AppSettings["MOI_TokenName"];
            string serviceToken = ConfigurationManager.AppSettings["MOI_Token"];

            try
            {

                using (var client = new WebClient())
                {
                    // Set the header so it knows we are sending JSON.

                    //client.Headers["Cookie"] = CookieHeader;
                    client.Headers["Content-Type"] = "application/json; charset=utf-8";
                    client.Headers["Cache-Control"] = "no-cache";
                    client.Headers[serviceTokenName] = serviceToken;
                    Bio_Model.MOE_STATUS += "start";
                    Bio_Model.MOE_STATUS += serviceURL;
                    //var client = new RestClient(serviceURL + QID);
                    //var request = new RestRequest(Method.GET);
                    // request.AddHeader("Postman-Token", "f1744e3e-91a8-3747-e15d-5af3a669bb28");

                    // Make the request
                    var response = client.DownloadString(serviceURL + QID);


                    //IRestResponse response = client.Execute(request);


                    MOIData bio = null;
                    object result = null;
                    try
                    {

                        result = JsonConvert.DeserializeObject(response);
                        bio = MOIData.FromJson(result.ToString());
                        //IRestResponse response = MOE_BIO_DATA_Repository.TestCall();
                        //dynamic bio = Newtonsoft.Json.Linq.JObject.Parse(response.Content.ToString());

                        try
                        {
                            Bio_Model.NATIONAL_ID = decimal.Parse(bio.QatarId);
                        }
                        catch (Exception ex) { Bio_Model.MOE_STATUS = ex.Message; }
                        try
                        {
                            Bio_Model.MOE_ENGLISH_NAME = bio.EnglishName;
                        }
                        catch (Exception ex) { Bio_Model.MOE_STATUS = ex.Message; }
                        try
                        {
                            Bio_Model.MOE_ARABIC_NAME = bio.ArabicName;
                        }
                        catch (Exception ex) { Bio_Model.MOE_STATUS = ex.Message; }
                        try
                        {
                            Bio_Model.MOE_DOB = Convert.ToDateTime(bio.Dob).Date.ToString("dd/MM/yyyy");
                        }
                        catch (Exception ex) { Bio_Model.MOE_STATUS = ex.Message; }
                        try
                        {
                            Bio_Model.MOE_GENDER = bio.Gender;
                        }
                        catch (Exception ex) { Bio_Model.MOE_STATUS = ex.Message; }
                        try
                        {
                            Bio_Model.MOE_COUNTRY_ENGLISH_NAME = bio.CountryNameEn;
                        }
                        catch (Exception ex) { Bio_Model.MOE_STATUS = ex.Message; }
                        try
                        {
                            Bio_Model.MOE_COUNTRY_ARABIC_NAME = bio.CountryNameAr;
                        }
                        catch (Exception ex) { Bio_Model.MOE_STATUS = ex.Message; }
                        try
                        {
                            Bio_Model.MOE_COUNTRY_CODE = int.Parse(bio.CountryCode);
                        }
                        catch (Exception ex) { Bio_Model.MOE_STATUS = ex.Message; }
                        try
                        {
                            Bio_Model.MOE_STATUS = bio.Status;
                        }
                        catch (Exception ex) { Bio_Model.MOE_STATUS = ex.Message; }
                        try
                        {
                            Bio_Model.MOE_STATUS_DATE = Convert.ToDateTime(bio.StatusDate);
                        }
                        catch (Exception ex) { Bio_Model.MOE_STATUS += ex.Message; }
                        try
                        {
                            Bio_Model.MOE_LAST_UPDATED_DATE = Convert.ToDateTime(bio.LastUpdatedDate);
                        }
                        catch (Exception ex) { Bio_Model.MOE_STATUS += ex.Message; }

                    }
                    catch (Exception ex) { Bio_Model.MOE_STATUS = ex.Message; }

                }

            }
            catch (Exception ex)
            {
                Bio_Model.MOE_STATUS += ex.Message;
                string s = ex.Message;
            }
            return Bio_Model;
        }



        public static IRestResponse TestCall()
        {

            //var client = new HttpClient() { BaseAddress = new Uri("http://RFE-AWD1-Tst.sec.gov.qa:6060/") };
            ////var request = new HttpRequest((Method.GET);
            //client.DefaultRequestHeaders.Add("Postman-Token", "f1744e3e-91a8-3747-e15d-5af3a669bb28");
            //client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            //client.DefaultRequestHeaders.Add("API_KEY", "FD1C3E800EE1487D9057920174800B32");
            //HttpResponseMessage response = await client.GetAsync("api/Person/29081808008");
            //return response;

            var client = new RestClient("http://RFE-AWD1-Tst.sec.gov.qa:6060/api/Person/29081808008");
            var request = new RestRequest(Method.GET);
            // request.AddHeader("Postman-Token", "f1744e3e-91a8-3747-e15d-5af3a669bb28");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("API_KEY", "FD1C3E800EE1487D9057920174800B32");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            return response;
        }


        /// <summary>
        /// Used to get Bio Data with National id
        /// </summary>
        /// <returns></returns>
        public static MOE_BIO_DATA CheckExist(decimal QID)
        {
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                //decimal QIDDesc = decimal.Parse(QID);
                MOE_BIO_DATA Bio_Model = DB.MOE_BIO_DATA.Where(D => D.NATIONAL_ID == QID).Select(D => D).FirstOrDefault();



                return Bio_Model;
            }
        }

        /// <summary>
        /// Used to get Insert Bio Data with National id
        /// </summary>
        /// <returns></returns>
        public static DBOperationResult Insert(MOE_BIO_DATA_Model bio_data)
        {

            MOE_BIO_DATA bio_model_to_update = CheckExist(bio_data.NATIONAL_ID);
            DBOperationResult ReturnValue = new DBOperationResult();
            if (bio_model_to_update != null && bio_model_to_update.NATIONAL_ID != null)
            {
                // as advised by Moneer 11.4.2018 10:20 AM , don't update MOI Data entered before, just insert the new data, if the data is there don't update

                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    bio_model_to_update.NATIONAL_ID = bio_data.NATIONAL_ID;
                    bio_model_to_update.MOE_ENGLISH_NAME = bio_data.MOE_ENGLISH_NAME;
                    bio_model_to_update.MOE_ARABIC_NAME = bio_data.MOE_ARABIC_NAME;
                    DateTime outDOB = new DateTime();
                    if (DateTime.TryParseExact(bio_data.MOE_DOB, "dd/MM/yyyy", CultureInfo.GetCultureInfo(2057), DateTimeStyles.None, out outDOB))
                    {
                        bio_model_to_update.MOE_DOB = outDOB;
                    }
                    bio_model_to_update.MOE_GENDER = bio_data.MOE_GENDER;
                    bio_model_to_update.MOE_COUNTRY_ENGLISH_NAME = bio_data.MOE_COUNTRY_ENGLISH_NAME;
                    bio_model_to_update.MOE_COUNTRY_ARABIC_NAME = bio_data.MOE_COUNTRY_ARABIC_NAME;
                    bio_model_to_update.MOE_COUNTRY_CODE = bio_data.MOE_COUNTRY_CODE;
                    // bio_model_to_update.MOE_STATUS = bio_data.MOE_STATUS;
                    //bio_model_to_update.MOE_STATUS_DATE = bio_data.MOE_STATUS_DATE;
                    bio_model_to_update.MOE_LAST_UPDATED_DATE = bio_data.MOE_LAST_UPDATED_DATE;


                    DB.SaveChanges();
                }


            }
            else
            {
              
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    MOE_BIO_DATA Bio_Model = new MOE_BIO_DATA();

                    Bio_Model.NATIONAL_ID = bio_data.NATIONAL_ID;
                    Bio_Model.MOE_ENGLISH_NAME = bio_data.MOE_ENGLISH_NAME;
                    Bio_Model.MOE_ARABIC_NAME = bio_data.MOE_ARABIC_NAME;
                    DateTime outDOB = new DateTime();
                    if (DateTime.TryParseExact(bio_data.MOE_DOB, "dd/MM/yyyy", CultureInfo.GetCultureInfo(2057), DateTimeStyles.None, out outDOB))
                    {
                        Bio_Model.MOE_DOB = outDOB;
                    }
                    Bio_Model.MOE_GENDER = bio_data.MOE_GENDER;
                    Bio_Model.MOE_COUNTRY_ENGLISH_NAME = bio_data.MOE_COUNTRY_ENGLISH_NAME;
                    Bio_Model.MOE_COUNTRY_ARABIC_NAME = bio_data.MOE_COUNTRY_ARABIC_NAME;
                    Bio_Model.MOE_COUNTRY_CODE = bio_data.MOE_COUNTRY_CODE;
                    //Bio_Model.MOE_STATUS = bio_data.MOE_STATUS;
                    //Bio_Model.MOE_STATUS_DATE = bio_data.MOE_STATUS_DATE;
                    Bio_Model.MOE_LAST_UPDATED_DATE = bio_data.MOE_LAST_UPDATED_DATE;

                    DB.MOE_BIO_DATA.Add(Bio_Model);
                    DB.SaveChanges();
                    ReturnValue.insertedStringID = Bio_Model.NATIONAL_ID.ToString();
                    ReturnValue.EnglishResult = PSPESConstants.InsertionDone;




                }
            }
            return ReturnValue;
        }
    }
}