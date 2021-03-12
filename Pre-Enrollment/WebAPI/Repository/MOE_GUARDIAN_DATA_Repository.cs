using MOEHE.PSPES.WebAPI.Models;
using MOEHE.PSPES.WebAPI.Repository.NSIS;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class MOE_GUARDIAN_DATA_Repository
    {
        /// <summary>
        /// Used to get Health Data from PHCC service
        /// </summary>
        /// <returns></returns>
        public static List<MOE_GUARDIAN_DATA_Model> Get(string QID)
        {
            List<MOE_GUARDIAN_DATA_Model> studentContacts = new List<MOE_GUARDIAN_DATA_Model>();


            decimal? QIDDec = decimal.Parse(QID);
            try
            {
                List<MOE_GUARDIAN_DATA> studentContactsCopy = new List<MOE_GUARDIAN_DATA>();

                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    studentContactsCopy = DB.MOE_GUARDIAN_DATA.Where(D => D.NATIONAL_ID == QIDDec & D.MOE_ISGUARDIAN == true).Select(D => D).ToList();
                    //studentContactsCopy = DB.MOE_GUARDIAN_DATA.Where(D => D.NATIONAL_ID == QIDDec).Select(D => D).ToList();
                    foreach (var item in studentContactsCopy)
                    {
                        MOE_GUARDIAN_DATA_Model guardian = new MOE_GUARDIAN_DATA_Model();
                        guardian.MOE_ISGUARDIAN = (bool)item.MOE_ISGUARDIAN;
                        guardian.MOE_RELATED_QID = (decimal)item.MOE_RELATED_QID;
                        guardian.MOE_EMAIL = item.MOE_EMAIL;
                        guardian.MOE_EMPLOYER = item.MOE_EMPLOYER;
                        guardian.MOE_EMPLOYER_TYPE_ID = item.MOE_EMPLOYER_TYPE_ID;
                        guardian.MOE_MOBILE_CONTACT_NBR = item.MOE_MOBILE_CONTACT_NBR;
                        guardian.MOE_HOME_CONTACT_NBR = item.MOE_HOME_CONTACT_NBR;
                        guardian.MOE_MARITAL_STATUS_ID = item.MOE_MARITAL_STATUS_ID;
                        guardian.MOE_RELATIONSHIP_TYPE_ID = item.MOE_RELATIONSHIP_TYPE_ID;
                        guardian.MOE_OLD_RELATED_QID = (decimal)item.MOE_RELATED_QID;
                        guardian.MOE_GUARDIAN_NAME_ARA = item.MOE_GUARDIAN_NAME_ARA;
                        guardian.MOE_GUARDIAN_NAME_ENG = item.MOE_GUARDIAN_NAME_ENG;


                        guardian.bio_data = MOE_BIO_DATA_Repository.Get(Convert.ToInt64(guardian.MOE_RELATED_QID).ToString());

                        if (guardian.bio_data != null)
                        {
                            guardian.MOE_ARABIC_NAME = guardian.bio_data.MOE_ARABIC_NAME;
                            guardian.MOE_ENGLISH_NAME = guardian.bio_data.MOE_ENGLISH_NAME;
                            guardian.MOE_COUNTRY_ARABIC_NAME = guardian.bio_data.MOE_COUNTRY_ARABIC_NAME;
                            guardian.MOE_COUNTRY_ENGLISH_NAME = guardian.bio_data.MOE_COUNTRY_ENGLISH_NAME;
                        }


                        //}

                        // guardian.mo
                        studentContacts.Add(guardian);
                    }



                }
                if (studentContacts.Count > 0)
                {
                }
                else
                {

                    //var client = new RestClient(ConfigurationManager.AppSettings["NSIS_StudentContacts"]);
                    //var request = new RestRequest(Method.POST);
                    //request.AddHeader("Cache-Control", "no-cache");
                    //request.AddHeader("Content-Type", "application/json");
                    //request.AddParameter("undefined", "{\"studentID\":"+QID+"}", ParameterType.RequestBody);
                    //IRestResponse response = client.Execute(request);

                    string PortalUserName = "svc_nsis_moehe_cert";

                    string PortalPassword = "pD6~V#9_&v-6#c`;%YL:+.M";
                    string urlAuth = "https://nsis.sec.gov.qa/Authentication_JSON_AppService.axd/Login";

                    string CookieHeader = GetCookieHeader(urlAuth, PortalUserName, PortalPassword);




                    // var request = new RestRequest(Method.POST);
                    // request.AddHeader("Cookie", CookieHeader);
                    //// request.AddHeader("Cache-Control", "no-cache");
                    // request.AddHeader("Content-Type", "application/json");
                    // request.AddParameter("undefined", "{\"schoolYearID\":" + schoolYearID + ",\n\t\"isPublic\":" + isPublic + "\n}", ParameterType.RequestBody);
                    // IRestResponse response = client.Execute(request);
                    object result = string.Empty;
                    using (var client = new WebClient())
                    {
                        // Set the header so it knows we are sending JSON.

                        client.Headers["Cookie"] = CookieHeader;
                        client.Headers["Content-Type"] = "application/json";
                        // Create the one and only "options" parameter object.
                        var dto = "{\"studentID\":" + QID + "}";
                        // Serialise the data we are sending in to JSON
                        string serialisedData = JsonConvert.SerializeObject(dto);
                        // Make the request
                        var response = client.UploadString(ConfigurationManager.AppSettings["NSIS_StudentContacts"], dto);

                        result = JsonConvert.DeserializeObject(response);
                        StudentPersonalData student = StudentPersonalData.FromJson(result.ToString());
                        //dynamic student = Newtonsoft.Json.Linq.JObject.Parse(response.ToString());

                        if (student.GetStudentPersonalResult[0].StudentContacts.Count() > 0)
                        {

                            foreach (var studentContact in student.GetStudentPersonalResult[0].StudentContacts)
                            {
                                MOE_GUARDIAN_DATA_Model guardian = new MOE_GUARDIAN_DATA_Model();
                                guardian.MOE_ISGUARDIAN = studentContact.Guardian;
                                guardian.MOE_RELATED_QID = decimal.Parse(studentContact.Id);
                                guardian.MOE_EMAIL = studentContact.Email;
                                guardian.MOE_EMPLOYER = studentContact.EmployerCode;
                                guardian.MOE_EMPLOYER_TYPE_ID = studentContact.EmployerTypeId;
                                guardian.MOE_MOBILE_CONTACT_NBR = studentContact.Mobile;
                                guardian.MOE_MARITAL_STATUS_ID = studentContact.MaritalstatusId;
                                guardian.MOE_RELATIONSHIP_TYPE_ID = studentContact.RelationshipId;
                              



                                guardian.bio_data = MOE_BIO_DATA_Repository.Get(Convert.ToInt64(guardian.MOE_RELATED_QID).ToString());

                                if (guardian.bio_data != null)
                                {
                                    guardian.MOE_ARABIC_NAME = guardian.bio_data.MOE_ARABIC_NAME;
                                    guardian.MOE_ENGLISH_NAME = guardian.bio_data.MOE_ENGLISH_NAME;
                                    guardian.MOE_COUNTRY_ARABIC_NAME = guardian.bio_data.MOE_COUNTRY_ARABIC_NAME;
                                    guardian.MOE_COUNTRY_ENGLISH_NAME = guardian.bio_data.MOE_COUNTRY_ENGLISH_NAME;
                                    guardian.MOE_GUARDIAN_NAME_ARA = guardian.bio_data.MOE_ARABIC_NAME;
                                    guardian.MOE_GUARDIAN_NAME_ENG = guardian.bio_data.MOE_ENGLISH_NAME;

                                }


                                //}

                                // guardian.mo
                                studentContacts.Add(guardian);
                            }

                        }

                    }




                }


                //Health_Model.MOE_HLTH_CARD_NBR = health.student.hc_number;
                //Health_Model.MOE_HLTH_CTR_NAME = health.student.hc_name;
                //Health_Model.NATIONAL_ID = Convert.ToInt64(health.student.qid_number);
                //Health_Model.MOE_SPL_NEEDS = (health.student.result.ToString() == "4") ? true : false;
                //Health_Model.MOE_FIT_FOR_SCHOOLING = (health.student.result.ToString() == "1" || health.student.result.ToString() == "2" || health.student.result.ToString() == "3") ? true : false;


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return studentContacts;

        }





        public static string GetCookieHeader(string Uri, string UserName, string Password)
        {


            using (var client = new WebClient())
            {
                client.Headers["Content-Type"] = "application/json";
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

        //public static MOE_GUARDIAN_DATA_Model CheckGuardianExist(string National_Id, string Guardian_ID)
        //{
        //    //MOE_GUARDIAN_DATA_Model guardian = null;


        //    //decimal decimalStudentQID = decimal.Parse(National_Id);
        //    //decimal decimalGuardianQID = decimal.Parse(Guardian_ID);

        //    //bool GuardianExist = false;
        //    //try
        //    //{

        //    //    using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
        //    //    {
        //    //        guardian = DB.MOE_GUARDIAN_DATA.Where(D => D.NATIONAL_ID == decimalStudentQID && D.MOE_RELATED_QID == decimalGuardianQID).Select(D => new MOE_GUARDIAN_DATA_Model() { }).FirstOrDefault();
        //    //    }

        //    //}
        //    // if (guardian!=null)
        //    //{
        //    //    GuardianExist = true;
        //    //}

        //    // return GuardianExist
        //}
        public static DBOperationResult Insert(MOE_GUARDIAN_DATA_Model_Not_Serializable guardianDataModel)
        {
            MOE_GUARDIAN_DATA guardian_DATA_Exist = new MOE_GUARDIAN_DATA();
            DBOperationResult ReturnValue = new DBOperationResult();
            //need to handle if data exists
            //tarek.elbeheidy =>01.04.2018 , 6:25 PM
            //enabled check exist only with Application reference number  after muneer asked to save Application reference number with the guardian
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {

                guardian_DATA_Exist = CheckGuardianExist(guardianDataModel.MOE_APPL_NBR,guardianDataModel.NATIONAL_ID.Value);
            }

            if (guardian_DATA_Exist!=null && guardian_DATA_Exist.NATIONAL_ID != null && guardian_DATA_Exist.MOE_MOBILE_CONTACT_NBR != null)
            {
                try
                {
                    using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                    {
                        decimal StudentQIDDec = (decimal)guardianDataModel.NATIONAL_ID;
                        MOE_GUARDIAN_DATA guardianModelForUpdate = DB.MOE_GUARDIAN_DATA.Where(D => D.MOE_RELATED_QID == guardianDataModel.MOE_OLD_RELATED_QID && D.NATIONAL_ID == StudentQIDDec).Select(D => D).FirstOrDefault();

                        guardianModelForUpdate.MOE_TRANSACTION_DTTM = DateTime.Now;
                        guardianModelForUpdate.NATIONAL_ID = guardianDataModel.NATIONAL_ID;
                        guardianModelForUpdate.MOE_EMAIL = guardianDataModel.MOE_EMAIL;
                        guardianModelForUpdate.MOE_EMPLOYER_TYPE_ID = guardianDataModel.MOE_EMPLOYER_TYPE_ID;
                        guardianModelForUpdate.MOE_HOME_CONTACT_NBR = guardianDataModel.MOE_HOME_CONTACT_NBR;
                        guardianModelForUpdate.MOE_ISGUARDIAN = guardianDataModel.MOE_ISGUARDIAN;
                        guardianModelForUpdate.MOE_MARITAL_STATUS_ID = guardianDataModel.MOE_MARITAL_STATUS_ID;
                        guardianModelForUpdate.MOE_MOBILE_CONTACT_NBR = guardianDataModel.MOE_MOBILE_CONTACT_NBR;
                        guardianModelForUpdate.MOE_RELATIONSHIP_TYPE_ID = guardianDataModel.MOE_RELATIONSHIP_TYPE_ID;
                        guardianModelForUpdate.MOE_RELATED_QID = guardianDataModel.MOE_RELATED_QID;
                        guardianModelForUpdate.MOE_USERID = guardianDataModel.MOE_USERID;
                        guardianModelForUpdate.MOE_EMPLOYER = guardianDataModel.MOE_EMPLOYER;
                        guardianModelForUpdate.MOE_GUARDIAN_NAME_ARA = guardianDataModel.MOE_GUARDIAN_NAME_ARA;
                        guardianModelForUpdate.MOE_GUARDIAN_NAME_ENG = guardianDataModel.MOE_GUARDIAN_NAME_ENG;

                        DB.SaveChanges();
                        ReturnValue.EnglishResult = PSPESConstants.InsertionDone;
                    }
                }
                catch (Exception ex)
                { }
            }
            else
            {



                try
                {
                    TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Insert Guardian", Description = "Insert Guardian before connection"});
                    using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                    {
                        DB.Database.Connection.Open();
                        TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Insert Guardian", Description = "Insert Guardian Data MOE_TRANSACTION_DTTM ="+ DateTime.Now+",NATIONAL_ID = "+guardianDataModel.NATIONAL_ID+",MOE_EMAIL ="+ guardianDataModel.MOE_EMAIL+",MOE_EMPLOYER_TYPE_ID = "+guardianDataModel.MOE_EMPLOYER_TYPE_ID+",MOE_HOME_CONTACT_NBR = "+guardianDataModel.MOE_HOME_CONTACT_NBR+",MOE_ISGUARDIAN = "+guardianDataModel.MOE_ISGUARDIAN+",MOE_MARITAL_STATUS_ID = "+guardianDataModel.MOE_MARITAL_STATUS_ID+",MOE_MOBILE_CONTACT_NBR = "+guardianDataModel.MOE_MOBILE_CONTACT_NBR+", MOE_RELATIONSHIP_TYPE_ID ="+ guardianDataModel.MOE_RELATIONSHIP_TYPE_ID+",MOE_RELATED_QID = "+guardianDataModel.MOE_RELATED_QID+", MOE_USERID = "+guardianDataModel.MOE_USERID+",MOE_EMPLOYER = "+guardianDataModel.MOE_EMPLOYER+",MOE_APPL_NBR = "+guardianDataModel.MOE_APPL_NBR+",MOE_GUARDIAN_NAME_ARA ="+ guardianDataModel.MOE_GUARDIAN_NAME_ARA+",MOE_GUARDIAN_NAME_ENG = "+guardianDataModel.MOE_GUARDIAN_NAME_ENG});
                        MOE_GUARDIAN_DATA guardian_DATA = new MOE_GUARDIAN_DATA
                        {
                            MOE_TRANSACTION_DTTM = DateTime.Now,
                            NATIONAL_ID = guardianDataModel.NATIONAL_ID,
                            MOE_EMAIL = guardianDataModel.MOE_EMAIL,
                            MOE_EMPLOYER_TYPE_ID = guardianDataModel.MOE_EMPLOYER_TYPE_ID,
                            MOE_HOME_CONTACT_NBR = guardianDataModel.MOE_HOME_CONTACT_NBR,
                            MOE_ISGUARDIAN = guardianDataModel.MOE_ISGUARDIAN,
                            MOE_MARITAL_STATUS_ID = guardianDataModel.MOE_MARITAL_STATUS_ID,
                            MOE_MOBILE_CONTACT_NBR = guardianDataModel.MOE_MOBILE_CONTACT_NBR,
                            MOE_RELATIONSHIP_TYPE_ID = guardianDataModel.MOE_RELATIONSHIP_TYPE_ID,
                            MOE_RELATED_QID = guardianDataModel.MOE_RELATED_QID,
                            MOE_USERID = guardianDataModel.MOE_USERID,
                            MOE_EMPLOYER = guardianDataModel.MOE_EMPLOYER,
                            MOE_APPL_NBR = guardianDataModel.MOE_APPL_NBR,
                            MOE_GUARDIAN_NAME_ARA = guardianDataModel.MOE_GUARDIAN_NAME_ARA,
                            MOE_GUARDIAN_NAME_ENG = guardianDataModel.MOE_GUARDIAN_NAME_ENG


                    };
                        DB.MOE_GUARDIAN_DATA.Add(guardian_DATA);
                        DB.SaveChanges();
                        //ReturnValue.insertedStringID = guardian_DATA.NATIONAL_ID.ToString();
                        ReturnValue.EnglishResult = PSPESConstants.InsertionDone;
                        ReturnValue.InsertedID = guardian_DATA.ID;
                        TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Insert Guardian", Description = "Inserted ID="+ guardian_DATA.ID });
                    }
                }
                catch (Exception ex)
                { TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Insert Guardian", Description = "Insert Guardian Data Exception, ex : " + ex.Message + ",inner exception= " + ex.InnerException.ToString() }); }
            }
            return ReturnValue;
        }

        private static MOE_GUARDIAN_DATA CheckGuardianExist(string ApplicationRefernceNumber, decimal QID)
        {
            MOE_GUARDIAN_DATA guardianModel = new MOE_GUARDIAN_DATA();
            //decimal decimalStudentQID = decimal.Parse(studentID);
            //decimal decimalGuardianQID = decimal.Parse(GuardianID);

            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                try
                {
                    guardianModel = DB.MOE_GUARDIAN_DATA.Where(D =>  D.MOE_APPL_NBR == ApplicationRefernceNumber && D.NATIONAL_ID == QID).Select(D => D).FirstOrDefault();

                }

                catch(Exception ex)
                {
                    string s = ex.Message;
                }
            }
            return guardianModel;
        }
    }
}