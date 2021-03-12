using MOEHE.PSPES.WebAPI.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;


namespace MOEHE.PSPES.WebAPI.Repository.NSIS
{
    public class NSIS_Helper
    {
        public static MOE_APPL_ACAD_INFO_Model GetEnrollmentData(string nationalID, int requestedTerm)
        {
            decimal nationalIDDec = decimal.Parse(nationalID);



            MOE_APPL_ACAD_INFO_Model Acad_Model_service = new MOE_APPL_ACAD_INFO_Model();
            try
            {
                TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "NSIS Helper", Description = "Start GetEnrollmentData: nationalID= " + nationalID + ",requestedTerm=" + requestedTerm.ToString() });
                //var client = new RestClient(ConfigurationManager.AppSettings["NSIS_GetCodes"]);
                //string PortalUserName = "svc_nsis_moehe_cert";

                //string PortalPassword = "pD6~V#9_&v-6#c`;%YL:+.M";
                //string urlAuth = "https://nsis.sec.gov.qa/Authentication_JSON_AppService.axd/Login";

                //string CookieHeader = GetCookieHeader(urlAuth, PortalUserName, PortalPassword);




                //var request = new RestRequest(Method.POST);
                //request.AddHeader("Cookie", CookieHeader);
                //// request.AddHeader("Postman-Token", "f1744e3e-91a8-3747-e15d-5af3a669bb28");
                ////request.AddHeader("Cache-Control", "no-cache");
                //request.AddHeader("Content-Type", "application/json");
                //request.AddHeader("Accept", "application/json");
                //request.AddParameter("undefined", "{\"studentQID\":" + nationalID + ",\n\"schoolYearID\":" + requestedTerm.ToString() + "\n}", ParameterType.RequestBody);
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
                    client.Headers["Content-Type"] = "application/json; charset=utf-8";
                    // Create the one and only "options" parameter object.
                    var dto = "{\"studentQID\":" + nationalID + ",\n\"schoolYearID\":" + requestedTerm.ToString() + "\n}";
                    // Serialise the data we are sending in to JSON
                    string serialisedData = JsonConvert.SerializeObject(dto);
                    // Make the request
                    var response = client.UploadString(ConfigurationManager.AppSettings["NSIS_GetEnrollments"], dto);
                    result = JsonConvert.DeserializeObject(response);
                    //dynamic acadInfo = Newtonsoft.Json.Linq.JObject.Parse(response.ToString());

                    EnrollmentsData acadInfo = EnrollmentsData.FromJson(result.ToString());
                    if (acadInfo != null && acadInfo.GetEnrollmentsResult != null && acadInfo.GetEnrollmentsResult.Count() > 0)
                    {
                        Acad_Model_service.NATIONAL_ID = nationalIDDec;
                        Acad_Model_service.MOE_TERM = int.Parse(acadInfo.GetEnrollmentsResult[0].SchoolYearId.ToString());
                        Acad_Model_service.MOE_SCHOOL_CODE = acadInfo.GetEnrollmentsResult[0].SchoolId;
                        Acad_Model_service.MOE_GRADE = acadInfo.GetEnrollmentsResult[0].GradeLevel;
                        Acad_Model_service.MOE_STU_RSLT = (string)acadInfo.GetEnrollmentsResult[0].PromotionStatus;
                        Acad_Model_service.HasPendingPayment = acadInfo.GetEnrollmentsResult[0].HasPendingPayment;

                        MOE_SCHOOL_Model school_Model = GetSchoolData(Convert.ToString(acadInfo.GetEnrollmentsResult[0].SchoolYearId), Convert.ToString(acadInfo.GetEnrollmentsResult[0].SchoolId), "false");

                        Acad_Model_service.MOE_SCHOOL_NAME = school_Model.MOE_SCHOOL_NAME_ENG;
                        Acad_Model_service.MOE_SCHOOL_ARABIC_NAME = school_Model.MOE_SCHOOL_NAME_ARA;
                        Acad_Model_service.MOE_SCHOOL_CURRICULUM = school_Model.MOE_SCHOOL_CURRICULUM;
                        Acad_Model_service.MOE_SCHOOL_ARABIC_CURRICULUM = school_Model.MOE_SCHOOL_CURRICULUM;//need to pass curriculum id and get the name in arabic
                        Acad_Model_service.MOE_SCHOOL_CURRICULUM_ID = school_Model.MOE_SCHOOL_CURRICULUM_ID;
                        

                        //Acad_Model_service.MOE_TRANSACTION_DTTM = D.MOE_TRANSACTION_DTTM,
                        //Acad_Model_service.MOE_USERID = D.MOE_USERID
                    }
                }

            }
            catch (Exception ex)
            {

                TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "NSIS Helper", Description = "GetEnrollmentData Exception: nationalID= " + nationalID + ",requestedTerm=" + requestedTerm.ToString() + ",Exception=" + ex.Message });
                string s = ex.Message;

            }



            return Acad_Model_service;

        }

        //needs to be updated with school model not just school name
        public static MOE_SCHOOL_Model GetSchoolData(string schoolYearID, string schoolCode, string isPublic)
        {
            MOE_SCHOOL_Model School_Model = new MOE_SCHOOL_Model();
            try
            {
                TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "NSIS Helper", Description = "Start GetSchoolData: schoolYearID= " + schoolYearID + ",schoolCode=" + schoolCode.ToString() });

                // string schoolName = "";
                // var client = new RestClient(ConfigurationManager.AppSettings["NSIS_GetCodes"]);
                // string PortalUserName = "svc_nsis_moehe_cert";

                // string PortalPassword = "pD6~V#9_&v-6#c`;%YL:+.M";
                // string urlAuth = "https://nsis.sec.gov.qa/Authentication_JSON_AppService.axd/Login";

                // string CookieHeader = GetCookieHeader(urlAuth, PortalUserName, PortalPassword);




                // var request = new RestRequest(Method.POST);
                // request.AddHeader("Cookie", CookieHeader);
                //// request.AddHeader("Cache-Control", "no-cache");
                // request.AddHeader("Content-Type", "application/json");
                // request.AddParameter("undefined", "{\"schoolYearID\":" + schoolYearID + ",\n\t\"schoolID\":" + schoolCode + ",\n\t\"isPublic\":" + isPublic + "\n}", ParameterType.RequestBody);
                // IRestResponse response = client.Execute(request);
                string schoolName = "";
                //var client = new RestClient(ConfigurationManager.AppSettings["NSIS_GetCodes"]);
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
                    client.Headers["Content-Type"] = "application/json; charset=utf-8";
                    // Create the one and only "options" parameter object.
                    //var dto = "{\"schoolYearID\":" + schoolYearID + ",\n\t\"shoolID\":" + schoolCode + ",\n\t\"isPublic\":" + isPublic + "\n}";
                    var dto = "{\"schoolYearID\":" + schoolYearID + ",\n\t\"shoolID\":" + schoolCode + "\n}";
                    // Serialise the data we are sending in to JSON
                    string serialisedData = JsonConvert.SerializeObject(dto);
                    // Make the request
                    var response = client.UploadString(ConfigurationManager.AppSettings["NSIS_GetSchools"], dto);


                    result = JsonConvert.DeserializeObject(response);

                    SchoolsData schoolInfo = SchoolsData.FromJson(result.ToString());



                    //dynamic schoolInfo = Newtonsoft.Json.Linq.JObject.Parse(response.ToString());
                    if (schoolInfo != null && schoolInfo.GetSchoolsResult != null && schoolInfo.GetSchoolsResult.Count() > 0)
                    {
                        School_Model.MOE_SCHOOL_NAME_ENG = schoolInfo.GetSchoolsResult[0].EnglishName;
                        School_Model.MOE_SCHOOL_CODE = schoolInfo.GetSchoolsResult[0].Id;

                        School_Model.MOE_SCHOOL_NAME_ARA = schoolInfo.GetSchoolsResult[0].ArabicName;
                        School_Model.MOE_SCHOOL_CURRICULUM = schoolInfo.GetSchoolsResult[0].ProgramTypeDescription;

                        if (schoolInfo.GetSchoolsResult[0].ProgramTypeId.Contains(','))
                        {
                            School_Model.MOE_SCHOOL_CURRICULUM_ID = MOE_DOUBLE_CURRICULUM_MAPPING_Repository.GetCurriculumIDMapping(schoolInfo.GetSchoolsResult[0].ProgramTypeId);
                        }
                        else
                        {
                            School_Model.MOE_SCHOOL_CURRICULUM_ID = schoolInfo.GetSchoolsResult[0].ProgramTypeId;
                        }
                       
                        School_Model.MOE_SCHOOL_GENDER = schoolInfo.GetSchoolsResult[0].StudentGenderCode;
                        List<schoolGrade> schoolGrades = new List<schoolGrade>();
                        int schoolCapacity = 0;
                        int schoolEnrollemnts = 0;
                        if (schoolInfo.GetSchoolsResult[0].GradeLevels.Count() > 0)
                        {
                            foreach (var gradeLevel in schoolInfo.GetSchoolsResult[0].GradeLevels)
                            {
                                schoolGrade Grade = new schoolGrade();

                                int Capacity = 0;
                                int Enrollements = 0;
                                foreach (var homeRoom in gradeLevel.Hommerooms)
                                {
                                    Capacity += Convert.ToInt32(homeRoom.Capacity);
                                    Enrollements += Convert.ToInt32(homeRoom.EnrollmentCounts);
                                    schoolCapacity += Convert.ToInt32(homeRoom.Capacity);
                                    schoolEnrollemnts += Convert.ToInt32(homeRoom.EnrollmentCounts);
                                }
                                Grade.Grade = gradeLevel.Code;
                                Grade.CurrentCapacity = Capacity;
                                Grade.CurrentEnrollments = Enrollements;
                                schoolGrades.Add(Grade);
                            }




                        }

                        School_Model.schoolGrades = schoolGrades;
                        // School_Model.MOE_SCHOOL_CURRENT_CAPACITY = schoolCapacity; commented for building capacity change

                        try
                        {
                            //added for building capacity change ...Saadallah
                            List<BuildingCapacityModel> BldCapModel = new List<BuildingCapacityModel>();
                            BldCapModel = BuildingCapacityRepository.Get(School_Model.MOE_SCHOOL_CODE);
                            if (BldCapModel != null && BldCapModel.Count > 0)
                            {
                                School_Model.MOE_SCHOOL_CURRENT_CAPACITY = int.Parse(BldCapModel[0].BuildingCapacity1);
                            }
                        }
                        catch { }

                        School_Model.MOE_SCHOOL_CURRENT_ENROLLMENTS = schoolEnrollemnts;
                        //School_Model.MOE_SCHOOL_CURRENT_CAPACITY = schoolInfo.GetSchoolsResult[0].
                        //School_Model.MOE_SCHOOL_CURRENT_ENROLLMENTS = schoolInfo.GetSchoolsResult[0].

                        // School_Model.MOE_EFFECTIVE_DATE = schoolInfo.GetSchoolsResult[0].
                        // School_Model.MOE_EFF_STATUS = schoolInfo.GetSchoolsResult[0].
                        // School_Model.MOE_SCHOOL_LOCATION = schoolInfo.GetSchoolsResult[0].
                        //School_Model.MOE_SCHOOL_ADDRESS1 = schoolInfo.GetSchoolsResult[0].
                        //School_Model.MOE_SCHOOL_ADDRESS2 = schoolInfo.GetSchoolsResult[0].
                        //School_Model.MOE_CITY = schoolInfo.GetSchoolsResult[0].
                        //School_Model.MOE_SCHOOL_STAGES = schoolInfo.GetSchoolsResult[0].

                        //            School_Model.MOE_SCHOOL_OWN_NID = schoolInfo.GetSchoolsResult[0].
                        //            School_Model.MOE_CONTACT_NBR = schoolInfo.GetSchoolsResult[0].
                        //            School_Model.MOE_EMAIL = schoolInfo.GetSchoolsResult[0].
                        //            School_Model.MOE_PRN_CONTACT_NBR = schoolInfo.GetSchoolsResult[0].
                        //            School_Model.MOE_PRN_EMAIL = schoolInfo.GetSchoolsResult[0].
                        //            School_Model.MOE_SCHOOL_WEBSITE = schoolInfo.GetSchoolsResult[0].
                        //            School_Model.MOE_TRANSACTION_DTTM = schoolInfo.GetSchoolsResult[0].
                        //            School_Model.MOE_USERID = schoolInfo.GetSchoolsResult[0].
                        //}308

                    }
                }
            }
            catch (Exception ex)
            {
                TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "NSIS Helper", Description = "GetSchoolData Exception: schoolYearID= " + schoolYearID + ",schoolCode=" + schoolCode.ToString() + ",Exception=" + ex.Message });
            }
            return School_Model;
        }

        private static string DecodeFromUtf8(string utf8_String)
        {

            byte[] bytes = Encoding.Default.GetBytes(utf8_String);
            string encoded_String = Encoding.UTF8.GetString(bytes);
            return encoded_String;
        }

        //needs to be updated with school model not just school name
        public static List<MOE_SCHOOL_Model> GetAllSchools(string schoolYearID, string isPublic)
        {
            List<MOE_SCHOOL_Model> Schools = new List<MOE_SCHOOL_Model>();
            try
            {
                TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "NSIS Helper", Description = "Start GetSchools: schoolYearID= " + schoolYearID });
                string schoolName = "";
                //var client = new RestClient(ConfigurationManager.AppSettings["NSIS_GetCodes"]);
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
                    client.Headers["Content-Type"] = "application/json; charset=utf-8";
                    // Create the one and only "options" parameter object.
                    var dto = "{\"schoolYearID\":" + schoolYearID + ",\n\t\"isPublic\":" + isPublic + "\n}";
                    // Serialise the data we are sending in to JSON
                    string serialisedData = JsonConvert.SerializeObject(dto);
                    // Make the request
                    var response = client.UploadString(ConfigurationManager.AppSettings["NSIS_GetSchools"], dto);

                    result = JsonConvert.DeserializeObject(response);

                    SchoolsData schoolInfo = SchoolsData.FromJson(result.ToString());
                    if (schoolInfo != null && schoolInfo.GetSchoolsResult != null && schoolInfo.GetSchoolsResult.Count() > 0)
                    {
                        string schoolcode = "";
                        try
                        {
                            foreach (var school in schoolInfo.GetSchoolsResult)
                            {
                                schoolcode = school.Id;
                                MOE_SCHOOL_Model School_Model = new MOE_SCHOOL_Model();

                                School_Model.MOE_SCHOOL_NAME_ENG = school.EnglishName;
                                School_Model.MOE_SCHOOL_CODE = school.Id;
                                School_Model.MOE_SCHOOL_NAME_ARA = school.ArabicName;
                                School_Model.MOE_SCHOOL_CURRICULUM = school.ProgramTypeDescription;
                                if (school.ProgramTypeId.Contains(','))
                                {
                                    School_Model.MOE_SCHOOL_CURRICULUM_ID = MOE_DOUBLE_CURRICULUM_MAPPING_Repository.GetCurriculumIDMapping(school.ProgramTypeId);
                                }
                                else
                                {
                                    School_Model.MOE_SCHOOL_CURRICULUM_ID = school.ProgramTypeId;
                                }
                               
                                School_Model.MOE_SCHOOL_GENDER = school.StudentGenderCode;
                                List<schoolGrade> schoolGrades = new List<schoolGrade>();
                                int schoolCapacity = 0;
                                int schoolEnrollemnts = 0;
                                if (school.GradeLevels.Count() > 0)
                                {
                                    foreach (var gradeLevel in school.GradeLevels)
                                    {
                                        schoolGrade Grade = new schoolGrade();

                                        int Capacity = 0;
                                        int Enrollements = 0;
                                        foreach (var homeRoom in gradeLevel.Hommerooms)
                                        {
                                            Capacity += Convert.ToInt32(homeRoom.Capacity);
                                            Enrollements += Convert.ToInt32(homeRoom.EnrollmentCounts);
                                            schoolCapacity += Convert.ToInt32(homeRoom.Capacity);
                                            schoolEnrollemnts += Convert.ToInt32(homeRoom.EnrollmentCounts);
                                        }
                                        Grade.Grade = gradeLevel.Code;
                                        Grade.CurrentCapacity = Capacity;
                                        Grade.CurrentEnrollments = Enrollements;
                                        schoolGrades.Add(Grade);

                                    }
                                    School_Model.schoolGrades = schoolGrades;

                                    try
                                    {
                                        //added for building capacity change...Saadallah
                                        List<BuildingCapacityModel> BldCapModel = new List<BuildingCapacityModel>();
                                        BldCapModel = BuildingCapacityRepository.Get(School_Model.MOE_SCHOOL_CODE);
                                        if (BldCapModel != null && BldCapModel.Count > 0)
                                        {
                                            School_Model.MOE_SCHOOL_CURRENT_CAPACITY = int.Parse(BldCapModel[0].BuildingCapacity1);
                                        }
                                        
                                    }

                                    catch { }
                                    // School_Model.MOE_SCHOOL_CURRENT_CAPACITY = schoolCapacity; //commented for buildigcapacity change
                                    School_Model.MOE_SCHOOL_CURRENT_ENROLLMENTS = schoolEnrollemnts;


                                    Schools.Add(School_Model);




                                }


                            }
                        }
                        catch (Exception ex)
                        {
                            TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "NSIS Helper", Description = "Start GetSchoolData: schoolYearID= " + schoolYearID + ",Exception=" + ex.Message });
                            string x = ex.Message + "," + schoolcode;
                        }




                    }
                }
            }

            catch (Exception ex)
            {
                TransactionLogsRepository.Insert(new TransactionsLog
                {
                    CreatedDate = DateTime.Now,
                    ShortDescription = "NSIS Helper",
                    Description = "Start GetSchoolData: schoolYearID= " + schoolYearID +",Exception="+ex.Message
                });
            }


            return Schools;
        }

        //needs to be updated with school model not just school name
        public static List<MOE_SCHOOL_Model> GetQatarSchools(string schoolYearID)
        {
            List<MOE_SCHOOL_Model> Schools = new List<MOE_SCHOOL_Model>();
            try
            {
                TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "NSIS Helper", Description = "Start GetSchools: schoolYearID= " + schoolYearID });
                string schoolName = "";
                //var client = new RestClient(ConfigurationManager.AppSettings["NSIS_GetCodes"]);
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
                    client.Headers["Content-Type"] = "application/json; charset=utf-8";
                    // Create the one and only "options" parameter object.
                    var dto = "{\"schoolYearID\":" + schoolYearID ;
                    // Serialise the data we are sending in to JSON
                    string serialisedData = JsonConvert.SerializeObject(dto);
                    // Make the request
                    var response = client.UploadString(ConfigurationManager.AppSettings["NSIS_GetSchools"], dto);

                    result = JsonConvert.DeserializeObject(response);

                    SchoolsData schoolInfo = SchoolsData.FromJson(result.ToString());
                    if (schoolInfo != null && schoolInfo.GetSchoolsResult != null && schoolInfo.GetSchoolsResult.Count() > 0)
                    {
                        string schoolcode = "";
                        try
                        {
                            foreach (var school in schoolInfo.GetSchoolsResult)
                            {
                                schoolcode = school.Id;
                                MOE_SCHOOL_Model School_Model = new MOE_SCHOOL_Model();

                                School_Model.MOE_SCHOOL_NAME_ENG = school.EnglishName;
                                School_Model.MOE_SCHOOL_CODE = school.Id;
                                School_Model.MOE_SCHOOL_NAME_ARA = school.ArabicName;
                                School_Model.MOE_SCHOOL_CURRICULUM = school.ProgramTypeDescription;
                                if (school.ProgramTypeId.Contains(','))
                                {
                                    School_Model.MOE_SCHOOL_CURRICULUM_ID = MOE_DOUBLE_CURRICULUM_MAPPING_Repository.GetCurriculumIDMapping(school.ProgramTypeId);
                                }
                                else
                                {
                                    School_Model.MOE_SCHOOL_CURRICULUM_ID = school.ProgramTypeId;
                                }
                                School_Model.MOE_SCHOOL_GENDER = school.StudentGenderCode;
                                List<schoolGrade> schoolGrades = new List<schoolGrade>();
                                int schoolCapacity = 0;
                                int schoolEnrollemnts = 0;
                                if (school.GradeLevels.Count() > 0)
                                {
                                    foreach (var gradeLevel in school.GradeLevels)
                                    {
                                        schoolGrade Grade = new schoolGrade();

                                        int Capacity = 0;
                                        int Enrollements = 0;
                                        foreach (var homeRoom in gradeLevel.Hommerooms)
                                        {
                                            Capacity += Convert.ToInt32(homeRoom.Capacity);
                                            Enrollements += Convert.ToInt32(homeRoom.EnrollmentCounts);
                                            schoolCapacity += Convert.ToInt32(homeRoom.Capacity);
                                            schoolEnrollemnts += Convert.ToInt32(homeRoom.EnrollmentCounts);
                                        }
                                        Grade.Grade = gradeLevel.Code;
                                        Grade.CurrentCapacity = Capacity;
                                        Grade.CurrentEnrollments = Enrollements;
                                        schoolGrades.Add(Grade);

                                    }
                                    School_Model.schoolGrades = schoolGrades;

                                    try
                                    {
                                        //added for building capacity change...Saadallah
                                        List<BuildingCapacityModel> BldCapModel = new List<BuildingCapacityModel>();
                                        BldCapModel = BuildingCapacityRepository.Get(School_Model.MOE_SCHOOL_CODE);
                                        if (BldCapModel != null && BldCapModel.Count > 0)
                                        {
                                            School_Model.MOE_SCHOOL_CURRENT_CAPACITY = int.Parse(BldCapModel[0].BuildingCapacity1);
                                        }
                                       
                                    }

                                    catch { }
                                    // School_Model.MOE_SCHOOL_CURRENT_CAPACITY = schoolCapacity; //commented for buildigcapacity change
                                    School_Model.MOE_SCHOOL_CURRENT_ENROLLMENTS = schoolEnrollemnts;


                                    Schools.Add(School_Model);




                                }


                            }
                        }
                        catch (Exception ex)
                        {
                            TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "NSIS Helper", Description = "Start GetSchoolData: schoolYearID= " + schoolYearID + ",Exception=" + ex.Message });
                            string x = ex.Message + "," + schoolcode;
                        }




                    }
                }
            }

            catch (Exception ex)
            {
                TransactionLogsRepository.Insert(new TransactionsLog
                {
                    CreatedDate = DateTime.Now,
                    ShortDescription = "NSIS Helper",
                    Description = "Start GetSchoolData: schoolYearID= " + schoolYearID + ",Exception=" + ex.Message
                });
            }


            return Schools;
        }


        //get list of values from NSIS
        public static List<ListOfValues_Model> GetListOfValues(string codesetID)
        {
            List<ListOfValues_Model> listOfValues_Models = new List<ListOfValues_Model>();
            try
            {
                TransactionLogsRepository.Insert(new TransactionsLog
                {
                    CreatedDate = DateTime.Now,
                    ShortDescription = "NSIS Helper",
                    Description = "Start GetSchoolData: codesetID= " + codesetID
                });

                // var client = new RestClient(ConfigurationManager.AppSettings["NSIS_GetCodes"]);
                string PortalUserName = "svc_nsis_moehe_cert";

                string PortalPassword = "pD6~V#9_&v-6#c`;%YL:+.M";
                string urlAuth = "https://nsis.sec.gov.qa/Authentication_JSON_AppService.axd/Login";

                string CookieHeader = GetCookieHeader(urlAuth, PortalUserName, PortalPassword);
                



                // var request = new RestRequest(Method.POST);
                // request.AddHeader("Cookie", CookieHeader);
                //// request.AddHeader("Cache-Control", "no-cache");
                // request.AddHeader("Content-Type", "application/json");
                // request.AddParameter("undefined", "{\"codesetID\":\"" + codesetID + "\"}", ParameterType.RequestBody);
                // IRestResponse response = client.Execute(request);
                object result = string.Empty;
                using (var client = new WebClient())
                {
                    // Set the header so it knows we are sending JSON.

                    client.Headers["Cookie"] = CookieHeader;
                    client.Headers["Content-Type"] = "application/json; charset=utf-8";
                    // Create the one and only "options" parameter object.
                    var dto = "{\"codesetID\":\"" + codesetID + "\"}";
                    // Serialise the data we are sending in to JSON
                    string serialisedData = JsonConvert.SerializeObject(dto);
                    // Make the request
                    var response = client.UploadString(ConfigurationManager.AppSettings["NSIS_GetCodes"], dto);

                    result = JsonConvert.DeserializeObject(response);
                    ListOfValueData listOfValuesContent = ListOfValueData.FromJson(result.ToString());
                    // dynamic listOfValuesContent = Newtonsoft.Json.Linq.JObject.Parse(response.ToString());


                    if (listOfValuesContent.GetCodesResult.Count() > 0)
                    {
                        switch (codesetID)
                        {
                            case PSPESConstants.RelationshipsCodesetID:
                                {

                                    foreach (var listOfValue in listOfValuesContent.GetCodesResult)
                                    {
                                        ListOfValues_Model lov_model = new ListOfValues_Model();
                                        lov_model.Code = listOfValue.Code;
                                        //lov_model.DescriptionArabic = (listOfValue.DescriptionArabic != null) ? DecodeFromUtf8((string)listOfValue.DescriptionArabic) : "No Arabic Value from NSIS";
                                        lov_model.DescriptionArabic = listOfValue.DescriptionArabic;
                                        lov_model.DescriptionEnglish = listOfValue.DescriptionEnglish;
                                        lov_model.ID = listOfValue.Id;

                                        listOfValues_Models.Add(lov_model);
                                    }



                                    break;
                                }


                            case PSPESConstants.MaritalStatusCodesetID:
                                {

                                    foreach (var listOfValue in listOfValuesContent.GetCodesResult)
                                    {
                                        ListOfValues_Model lov_model = new ListOfValues_Model();
                                        lov_model.Code = listOfValue.Code;
                                        lov_model.DescriptionArabic = listOfValue.DescriptionArabic;// (listOfValue.DescriptionArabic != null) ? DecodeFromUtf8((string)listOfValue.DescriptionArabic) : "No Arabic Value from NSIS";
                                        lov_model.DescriptionEnglish = listOfValue.DescriptionEnglish;
                                        lov_model.ID = listOfValue.Id;

                                        listOfValues_Models.Add(lov_model);
                                    }

                                    break;
                                }

                            case PSPESConstants.EmployerTypesCodesetID:
                                {
                                    foreach (var listOfValue in listOfValuesContent.GetCodesResult)
                                    {
                                        ListOfValues_Model lov_model = new ListOfValues_Model();
                                        lov_model.Code = listOfValue.Code;
                                        lov_model.DescriptionArabic = listOfValue.DescriptionArabic;// (listOfValue.DescriptionArabic != null) ? DecodeFromUtf8((string)listOfValue.DescriptionArabic) : "No Arabic Value from NSIS";
                                        lov_model.DescriptionEnglish = listOfValue.DescriptionEnglish;
                                        lov_model.ID = listOfValue.Id;

                                        if (listOfValue.CustomCodes.Count() > 0)
                                        {
                                            foreach (var Code in listOfValue.CustomCodes)
                                            {
                                                CustomCodes customCode = new CustomCodes();
                                                customCode.Code = Code.Code;
                                                customCode.DescriptionArabic = Code.DescriptionArabic;//(Code.DescriptionArabic != null) ? DecodeFromUtf8((string)Code.DescriptionArabic) : "No Arabic Value from NSIS"; 
                                                customCode.DescriptionEnglish = Code.DescriptionEnglish;
                                                lov_model.customCodes.Add(customCode);
                                            }

                                            listOfValues_Models.Add(lov_model);
                                        }

                                    }

                                    break;
                                }

                            case PSPESConstants.CurriculumsCodesetID:
                                {
                                    foreach (var listOfValue in listOfValuesContent.GetCodesResult)
                                    {
                                        ListOfValues_Model lov_model = new ListOfValues_Model();
                                        lov_model.Code = listOfValue.Code;
                                        lov_model.DescriptionArabic = listOfValue.DescriptionArabic; //(listOfValue.DescriptionArabic !=null)? DecodeFromUtf8((string)listOfValue.DescriptionArabic):"No Arabic Value from NSIS";
                                        lov_model.DescriptionEnglish = listOfValue.DescriptionEnglish;
                                        lov_model.ID = listOfValue.Id;

                                        listOfValues_Models.Add(lov_model);
                                    }
                                    break;

                                };

                            default:
                                break;
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                TransactionLogsRepository.Insert(new TransactionsLog
                {
                    CreatedDate = DateTime.Now,
                    ShortDescription = "NSIS Helper",
                    Description = "Start GetSchoolData: codesetID= " + codesetID + ",Exception=" + ex.Message
                });
            }
            return listOfValues_Models;
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

    }
}