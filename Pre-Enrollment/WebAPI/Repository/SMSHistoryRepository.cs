using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MOEHE.PSPES.WebAPI.Models;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class SMSHistoryRepository
    {

        public static DBOperationResult Insert(SMSHistoryModel SMSMHistoryModel)
        {
            DBOperationResult ReturnValue = new DBOperationResult();
            //try
            //{
                //// int CurrentSupportingDocID = 0;
                //List<AddedCapacityModel> CheckIfExit = GetADC(AddedCapModel.MOE_SCHOOL_YEAR, AddedCapModel.MOE_SCHOOL_CODE);

                //if (CheckIfExit.Count > 0)
                //{

                //then update
                //using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                //{
                //    DB.Database.Connection.Open();


                //    //foreach (var item in CheckIfExit)
                //    //{


                //        SMSHistory AddedCapacityForUpdate = DB.SMSHistories.Where(D => D.MOE_SCHOOL_YEAR == item.MOE_SCHOOL_YEAR && D.MOE_SCHOOL_CODE == item.MOE_SCHOOL_CODE && D.MOE_ADDED_CAPACITY1 == item.MOE_ADDED_CAPACITY1).FirstOrDefault();

                //        AddedCapacityForUpdate.MOE_ADDED_CAPACITY1 = AddedCapModel.MOE_ADDED_CAPACITY1;
                //        AddedCapacityForUpdate.MOE_SCHOOL_CODE = AddedCapModel.MOE_SCHOOL_CODE;
                //        AddedCapacityForUpdate.MOE_SCHOOL_YEAR = AddedCapModel.MOE_SCHOOL_YEAR;

                //if ((bool)supportingDocumentForUpdate.IsRequiredForPSO)
                //{
                //    //if the document required by PSO it will be froce required also for school
                //    supportingDocumentForUpdate.IsRequiredForPSO = supportingDocsModel.IsRequiredForPSO; 
                //    supportingDocumentForUpdate.IsRequiredForSchool = supportingDocsModel.IsRequiredForPSO;
                //}




                //        DB.SaveChanges();
                //    }
                //}

                //}
                //else
                //{
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    DB.Database.Connection.Open();
                    SMSHistory SMSHist = new SMSHistory
                    {
                        MsgStatus= SMSMHistoryModel.MsgStatus,
                        MsgTime = SMSMHistoryModel.MsgTime,
                        MsgType = SMSMHistoryModel.MsgType,
                        MsgText = SMSMHistoryModel.MsgText,
                        MOE_APPL_REF_NBR = SMSMHistoryModel.MOE_APPL_REF_NBR,
                        MsgTitle = SMSMHistoryModel.MsgTitle,
                         MsgSender = SMSMHistoryModel.MsgSender,
                         MobileNumber = SMSMHistoryModel.MobileNumber,
                         Description = SMSMHistoryModel.Description
              
                        
                     };


                    DB.SMSHistories.Add(SMSHist);  

                    DB.SaveChanges();
                    ReturnValue.InsertedID = SMSHist.ID;

                }

               
            //}

            //catch (Exception ex)
            //{
            //}

            return ReturnValue;

        }


        public static List<SMSHistoryModel> Get(string AppRefNum, string MsgType,string MobileNumber,string QID)
        {

            List<SMSHistoryModel> SMSHistModel = new List<SMSHistoryModel>();

            
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();

                SMSHistModel = DB.SMSHistories.Where(D => (D.MOE_APPL_REF_NBR == AppRefNum && D.MsgType == MsgType  && D.MobileNumber == MobileNumber && D.MsgText.Contains(QID))).Select(D => new SMSHistoryModel
                {
                    MsgStatus = D.MsgStatus,
                    MsgTime = D.MsgTime,
                    MsgType = D.MsgType,
                    MsgText = D.MsgText,
                    MOE_APPL_REF_NBR = D.MOE_APPL_REF_NBR,
                    MsgTitle = D.MsgTitle,
                    MsgSender = D.MsgSender,
                    MobileNumber = D.MobileNumber,
                    Description = D.Description


                }).ToList();

            }
            return SMSHistModel;
        }



        public static List<SMSHistoryModel> Get(string AppRefNum, string MsgType,string MessageTitle, string MobileNumber,string QID)
        {
            List<SMSHistoryModel> SMSHistModel = new List<SMSHistoryModel>();

            if (MsgType == "null")
            {
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    DB.Database.Connection.Open();

                    SMSHistModel = DB.SMSHistories.Where(D => (D.MOE_APPL_REF_NBR == AppRefNum && D.MsgTitle == MessageTitle && D.MobileNumber == MobileNumber && D.MsgText.Contains(QID))).Select(D => new SMSHistoryModel
                    {
                        MsgStatus = D.MsgStatus,
                        MsgTime = D.MsgTime,
                        MsgType = D.MsgType,
                        MsgText = D.MsgText,
                        MOE_APPL_REF_NBR = D.MOE_APPL_REF_NBR,
                        MsgTitle = D.MsgTitle,
                        MsgSender = D.MsgSender,
                        MobileNumber = D.MobileNumber,
                         Description = D.Description


                    }).ToList();

                }
            }
            else
            {




                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    DB.Database.Connection.Open();

                    SMSHistModel = DB.SMSHistories.Where(D => (D.MOE_APPL_REF_NBR == AppRefNum && D.MsgType==MsgType && D.MsgTitle == MessageTitle && D.MobileNumber == MobileNumber && D.MsgText.Contains(QID))).Select(D => new SMSHistoryModel
                    {
                        MsgStatus = D.MsgStatus,
                        MsgTime = D.MsgTime,
                        MsgType = D.MsgType,
                        MsgText = D.MsgText,
                        MOE_APPL_REF_NBR = D.MOE_APPL_REF_NBR,
                        MsgTitle = D.MsgTitle,
                        MsgSender = D.MsgSender,
                        MobileNumber = D.MobileNumber


                    }).ToList();

                }
            }
            return SMSHistModel;
        }









    }
}