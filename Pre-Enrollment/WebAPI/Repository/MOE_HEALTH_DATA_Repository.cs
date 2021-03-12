using MOEHE.PSPES.WebAPI.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class MOE_HEALTH_DATA_Repository
    {


        public static MOE_HEALTH_DATA_Model GetBy(string ApplicantReference)
        {




            MOE_HEALTH_DATA_Model mOE_HEALTH_DATA_Model = new MOE_HEALTH_DATA_Model();
            try
            {
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    mOE_HEALTH_DATA_Model = DB.MOE_HEALTH_DATA.Where(D => D.MOE_APPL_NBR == ApplicantReference).Select(D => new MOE_HEALTH_DATA_Model
                    {
                        NATIONAL_ID = D.NATIONAL_ID,
                        IsActive = D.IsActive,
                        MOE_APPL_NBR = D.MOE_APPL_NBR,
                        MOE_FIT_FOR_SCHOOLING = D.MOE_FIT_FOR_SCHOOLING,
                        MOE_HEALTH_DATA_ID = D.MOE_HEALTH_DATA_ID,
                        MOE_HLTH_CARD_NBR = D.MOE_HLTH_CARD_NBR,
                        MOE_HLTH_CTR_NAME = D.MOE_HLTH_CTR_NAME,
                        MOE_HLTH_PROBLEMS = D.MOE_HLTH_PROBLEMS,
                        MOE_HLTH_PROBLEMS_DETAILS = D.MOE_HLTH_PROBLEMS_DETAILS,
                        MOE_LEARNING_DIFFICULTIES = D.MOE_LEARNING_DIFFICULTIES,
                        MOE_SPECIAL_NEEDS_DETAILS = D.MOE_SPECIAL_NEEDS_DETAILS,
                        MOE_SPL_NEEDS = D.MOE_SPL_NEEDS,
                        MOE_TRANSACTION_DTTM = DateTime.Now
                      ,
                        MOE_USERID = D.MOE_USERID

                    }).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {

                string s = ex.Message;

            }




            return mOE_HEALTH_DATA_Model;



        }

        /// <summary>
        /// Used to get Health Data from PHCC service
        /// </summary>
        /// <returns></returns>
        public static MOE_HEALTH_DATA_Model Get(string QID)
        {

            MOE_HEALTH_DATA_Model Health_Model = new MOE_HEALTH_DATA_Model();

            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings["PHCC"]);
                var request = new RestRequest(Method.GET);
                request.AddHeader("Cache-Control", "no-cache");
                request.AddParameter("token", ConfigurationManager.AppSettings["PHCC_Token"]);
                request.AddParameter("qid", QID);

                IRestResponse response = client.Execute(request);

                XmlDocument document = new XmlDocument();
                document.LoadXml(response.Content.ToString());


                string jsonText = JsonConvert.SerializeXmlNode(document);

                    dynamic health = Newtonsoft.Json.Linq.JObject.Parse(jsonText);

                    Health_Model.MOE_HLTH_CARD_NBR = health.student.hc_number;
                    Health_Model.MOE_HLTH_CTR_NAME = health.student.hc_name;
                    Health_Model.NATIONAL_ID = Convert.ToInt64(health.student.qid_number);
                    Health_Model.MOE_SPL_NEEDS = (health.student.result.ToString() == "4") ? true : false;
                    Health_Model.MOE_FIT_FOR_SCHOOLING = (health.student.result.ToString() == "1" || health.student.result.ToString() == "2" || health.student.result.ToString() == "3") ? true : false;
              

            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return Health_Model;
        }


        public static DBOperationResult Insert(MOE_HEALTH_DATA_Model healthDataModel)
        {

            //need to handle if data exists


            DBOperationResult ReturnValue = new DBOperationResult();
            try
            {
                MOE_HEALTH_DATA_Model mOE_APPLICATION_DATA_Model = GetBy(healthDataModel.MOE_APPL_NBR);

                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    DB.Database.Connection.Open();
                    bool isForDeactivate = false;

                    try { isForDeactivate = (bool)healthDataModel.DeactiveOtherApplications; }
                    catch { }
                    if (isForDeactivate)
                    {
                        List<MOE_HEALTH_DATA> MOE_HEALTH_DATAListForDeativate = DB.MOE_HEALTH_DATA.Where(D => D.MOE_APPL_NBR != healthDataModel.MOE_APPL_NBR && D.NATIONAL_ID == healthDataModel.NATIONAL_ID).ToList();

                        foreach (var item in MOE_HEALTH_DATAListForDeativate)
                        {
                            MOE_HEALTH_DATA MOE_HEALTH_DATAForUpdate = DB.MOE_HEALTH_DATA.Where(D => D.MOE_APPL_NBR == item.MOE_APPL_NBR).FirstOrDefault();
                            MOE_HEALTH_DATAForUpdate.IsActive = false;
                            DB.SaveChanges();
                        }

                    }
                    else
                    {
                        if (mOE_APPLICATION_DATA_Model!=null && mOE_APPLICATION_DATA_Model.MOE_APPL_NBR != null)
                        {
                            MOE_HEALTH_DATA MOE_HEALTH_DATAForUpdate = DB.MOE_HEALTH_DATA.Where(D => D.MOE_APPL_NBR == healthDataModel.MOE_APPL_NBR).FirstOrDefault();

                            MOE_HEALTH_DATAForUpdate.NATIONAL_ID = healthDataModel.NATIONAL_ID;
                            MOE_HEALTH_DATAForUpdate.MOE_APPL_NBR = healthDataModel.MOE_APPL_NBR;
                            MOE_HEALTH_DATAForUpdate.MOE_FIT_FOR_SCHOOLING = healthDataModel.MOE_FIT_FOR_SCHOOLING;
                            MOE_HEALTH_DATAForUpdate.MOE_HLTH_CARD_NBR = healthDataModel.MOE_HLTH_CARD_NBR;
                            MOE_HEALTH_DATAForUpdate.MOE_HLTH_CTR_NAME = healthDataModel.MOE_HLTH_CTR_NAME;
                            MOE_HEALTH_DATAForUpdate.MOE_HLTH_PROBLEMS = healthDataModel.MOE_HLTH_PROBLEMS;

                            MOE_HEALTH_DATAForUpdate.MOE_HLTH_PROBLEMS_DETAILS = healthDataModel.MOE_HLTH_PROBLEMS_DETAILS;
                            MOE_HEALTH_DATAForUpdate.MOE_LEARNING_DIFFICULTIES = healthDataModel.MOE_LEARNING_DIFFICULTIES;
                            MOE_HEALTH_DATAForUpdate.MOE_SPL_NEEDS = healthDataModel.MOE_SPL_NEEDS;
                            MOE_HEALTH_DATAForUpdate.MOE_TRANSACTION_DTTM = DateTime.Now;
                            MOE_HEALTH_DATAForUpdate.MOE_USERID = healthDataModel.MOE_USERID;
                            MOE_HEALTH_DATAForUpdate.IsActive = healthDataModel.IsActive;



                            DB.SaveChanges();
                            ReturnValue.insertedStringID = MOE_HEALTH_DATAForUpdate.MOE_APPL_NBR;
                            ReturnValue.EnglishResult = PSPESConstants.InsertionDone;

                        }
                        else
                        {


                            MOE_HEALTH_DATA health_DATA = new MOE_HEALTH_DATA
                            {
                                NATIONAL_ID = healthDataModel.NATIONAL_ID,
                                MOE_APPL_NBR = healthDataModel.MOE_APPL_NBR,
                                MOE_FIT_FOR_SCHOOLING = healthDataModel.MOE_FIT_FOR_SCHOOLING,
                                MOE_HLTH_CARD_NBR = healthDataModel.MOE_HLTH_CARD_NBR,
                                MOE_HLTH_CTR_NAME = healthDataModel.MOE_HLTH_CTR_NAME,
                                MOE_HLTH_PROBLEMS = healthDataModel.MOE_HLTH_PROBLEMS,
                                MOE_HLTH_PROBLEMS_DETAILS = healthDataModel.MOE_HLTH_PROBLEMS_DETAILS,
                                MOE_LEARNING_DIFFICULTIES = healthDataModel.MOE_LEARNING_DIFFICULTIES,
                                MOE_SPL_NEEDS = healthDataModel.MOE_SPL_NEEDS,
                                MOE_TRANSACTION_DTTM = DateTime.Now,
                                MOE_USERID = healthDataModel.MOE_USERID,
                                IsActive = true,
                                MOE_HEALTH_DATA_ID = healthDataModel.MOE_HEALTH_DATA_ID,
                                MOE_SPECIAL_NEEDS_DETAILS = healthDataModel.MOE_SPECIAL_NEEDS_DETAILS
                            };
                            DB.MOE_HEALTH_DATA.Add(health_DATA);
                            DB.SaveChanges();
                            ReturnValue.insertedStringID = healthDataModel.MOE_APPL_NBR;
                            ReturnValue.EnglishResult = PSPESConstants.InsertionDone;
                        }
                    }
                }
            }
            catch (Exception ex)
            { TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Insert Health", Description = "Insert Health Data Exception, ex : " + ex.Message + ",inner exception= " + ex.InnerException.ToString() }); }
            return ReturnValue;
        }

    }
}