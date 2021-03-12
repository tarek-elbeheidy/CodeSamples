using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class MOE_APPL_SUPPORT_DOCS_Repository
    {
        public static DBOperationResult Insert(MOE_APPL_SUPPORT_DOCS_Model_Not_Serializable supportDocsDataModel)
        {
            DBOperationResult ReturnValue = new DBOperationResult();
            try
            {
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {

                    //need to handle if data exists
                    MOE_APPL_SUPPORT_DOCS_Model mOE_APPL_SUPPORT_DOCS_Model = GetBy(supportDocsDataModel.MOE_APPL_NBR, supportDocsDataModel.NATIONAL_ID, supportDocsDataModel.MOE_DOCUMENT_TYPE_ID);

                    bool isForDeactivate = false;

                    try { isForDeactivate = (bool)supportDocsDataModel.DeactiveOtherApplications; }
                    catch { }
                    if (isForDeactivate)
                    {
                        List<MOE_APPL_SUPPORT_DOCS> sUPPORT_DOCS_Update = DB.MOE_APPL_SUPPORT_DOCS.Where(D => (D.MOE_APPL_NBR != supportDocsDataModel.MOE_APPL_NBR && D.NATIONAL_ID == supportDocsDataModel.NATIONAL_ID)).Select(D => D).ToList();
                        foreach (var item in sUPPORT_DOCS_Update)
                        {
                            MOE_APPL_SUPPORT_DOCS sUPPORT_DOCS_UpdateDeactivate = DB.MOE_APPL_SUPPORT_DOCS.Where(D => (D.MOE_APPL_NBR == item.MOE_APPL_NBR && D.MOE_DOCUMENT_TYPE_ID == item.MOE_DOCUMENT_TYPE_ID)).Select(D => D).FirstOrDefault();
                            sUPPORT_DOCS_UpdateDeactivate.IsActive = false;
                            DB.SaveChanges();

                        }
                    }
                    else
                    {
                        if (mOE_APPL_SUPPORT_DOCS_Model != null && mOE_APPL_SUPPORT_DOCS_Model.MOE_APPL_NBR != null)
                        {



                            //update if exist
                            MOE_APPL_SUPPORT_DOCS sUPPORT_DOCS_Update = DB.MOE_APPL_SUPPORT_DOCS.Where(D => (D.MOE_APPL_NBR == supportDocsDataModel.MOE_APPL_NBR && D.MOE_DOCUMENT_TYPE_ID == supportDocsDataModel.MOE_DOCUMENT_TYPE_ID)).Select(D => D).FirstOrDefault();
                            sUPPORT_DOCS_Update.MOE_DOCUMENT_LOCATION = supportDocsDataModel.MOE_DOCUMENT_LOCATION;
                            sUPPORT_DOCS_Update.IsActive = supportDocsDataModel.IsActive;
                            DB.SaveChanges();
                            ReturnValue.insertedStringID = sUPPORT_DOCS_Update.MOE_APPL_NBR;
                            ReturnValue.EnglishResult = PSPESConstants.InsertionDone;


                        }
                        else
                        {
                            //insert if not exist
                            DB.Database.Connection.Open();
                            MOE_APPL_SUPPORT_DOCS supportDocs_DATA = new MOE_APPL_SUPPORT_DOCS
                            {
                                NATIONAL_ID = supportDocsDataModel.NATIONAL_ID,
                                MOE_APPL_NBR = supportDocsDataModel.MOE_APPL_NBR,
                                MOE_DOCUMENT_LOCATION = supportDocsDataModel.MOE_DOCUMENT_LOCATION,
                                MOE_DOCUMENT_TYPE_ID = supportDocsDataModel.MOE_DOCUMENT_TYPE_ID,
                                MOE_TRANSACTION_DTTM = DateTime.Now,
                                MOE_USERID = supportDocsDataModel.MOE_USERID
                            };
                            DB.MOE_APPL_SUPPORT_DOCS.Add(supportDocs_DATA);
                            DB.SaveChanges();
                            ReturnValue.insertedStringID = supportDocs_DATA.MOE_APPL_NBR;
                            ReturnValue.EnglishResult = PSPESConstants.InsertionDone;
                        }
                    }
                }

            }
            catch (Exception ex)
            { TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Insert Application Documents", Description = "Insert Application Documents Exception, ex : " + ex.Message + ",inner exception= " + ex.InnerException.ToString() }); }
            return ReturnValue;
        }

        public static MOE_APPL_SUPPORT_DOCS_Model GetBy(string ApplicantReference, decimal QID, string MOE_DOCUMENT_TYPE_ID)
        {




            MOE_APPL_SUPPORT_DOCS_Model sUPPORT_DOCS_Model = new MOE_APPL_SUPPORT_DOCS_Model();
            try
            {
                //decimal QIDDesc = decimal.Parse(QID);
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    sUPPORT_DOCS_Model = DB.MOE_APPL_SUPPORT_DOCS.Where(D => (D.MOE_APPL_NBR == ApplicantReference && D.MOE_DOCUMENT_TYPE_ID == MOE_DOCUMENT_TYPE_ID && D.NATIONAL_ID == QID)).Select(D => new MOE_APPL_SUPPORT_DOCS_Model
                    {
                        MOE_APPL_NBR = D.MOE_APPL_NBR,
                        MOE_DOCUMENT_LOCATION = D.MOE_DOCUMENT_LOCATION,
                        MOE_DOCUMENT_TYPE_ID = D.MOE_DOCUMENT_TYPE_ID,
                        NATIONAL_ID = D.NATIONAL_ID,
                        MOE_TRANSACTION_DTTM = D.MOE_TRANSACTION_DTTM,
                        MOE_USERID = D.MOE_USERID

                    }).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {

                string s = ex.Message;

            }




            return sUPPORT_DOCS_Model;



        }



        public static List<MOE_APPL_SUPPORT_DOCS_Model> GetBy(string ApplicantReference, string QID)
        {




            List<MOE_APPL_SUPPORT_DOCS_Model> sUPPORT_DOCS_Model = new List<MOE_APPL_SUPPORT_DOCS_Model>();
            try
            {
                decimal QIDDesc = decimal.Parse(QID);
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    sUPPORT_DOCS_Model = DB.MOE_APPL_SUPPORT_DOCS.Where(D => (D.MOE_APPL_NBR == ApplicantReference && D.NATIONAL_ID == QIDDesc)).Select(D => new MOE_APPL_SUPPORT_DOCS_Model
                    {
                        MOE_APPL_NBR = D.MOE_APPL_NBR,
                        MOE_DOCUMENT_LOCATION = D.MOE_DOCUMENT_LOCATION,
                        MOE_DOCUMENT_TYPE_ID = D.MOE_DOCUMENT_TYPE_ID,
                        NATIONAL_ID = D.NATIONAL_ID,
                        MOE_TRANSACTION_DTTM = D.MOE_TRANSACTION_DTTM,
                        MOE_USERID = D.MOE_USERID

                    }).ToList();
                }

            }
            catch (Exception ex)
            {

                string s = ex.Message;

            }




            return sUPPORT_DOCS_Model;



        }


    }
}