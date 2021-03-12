using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class InterviewResultRepository
    {



        public static DBOperationResult Insert(InterviewResultModel InterviewResultModel)
        {
            DBOperationResult ReturnValue = new DBOperationResult();
            try
            {
                // int CurrentSupportingDocID = 0;
                List<InterviewResultModel> CheckIfExit = GetBy(InterviewResultModel.MOE_APPL_NBR, InterviewResultModel.NATIONAL_ID);
                if (CheckIfExit.Count > 0)
                {

                    //then update
                    using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                    {
                        DB.Database.Connection.Open();


                        foreach (var item in CheckIfExit)
                        {


                            InterviewResult InterviewResultForUpdate = DB.InterviewResults.Where(D => D.ID == item.ID).FirstOrDefault();

                         
                            InterviewResultForUpdate.InterviewResult1 = InterviewResultModel.InterviewResult1;
                            InterviewResultForUpdate.USERID = InterviewResultModel.USERID;
                            InterviewResultForUpdate.DTTM = InterviewResultModel.DTTM;




                            DB.SaveChanges();
                        }
                    }

                }
                else
                {
                    using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                    {
                        DB.Database.Connection.Open();
                        InterviewResult InterviewResult1 = new InterviewResult
                        {
                            NATIONAL_ID = InterviewResultModel.NATIONAL_ID,
                            MOE_APPL_NBR = InterviewResultModel.MOE_APPL_NBR,
                            
                            DTTM = DateTime.Now,
                            InterviewResult1 = InterviewResultModel.InterviewResult1,
                            USERID = InterviewResultModel.USERID,

                        };

                        DB.InterviewResults.Add(InterviewResult1);
                        DB.SaveChanges();
                        ReturnValue.InsertedID = InterviewResult1.ID;

                    }

                }



            }

            catch (Exception ex)
            {




            }

            return ReturnValue;



        }





        public static List<InterviewResultModel> GetBy(string AppRefNum, string QID)
        {
            List<InterviewResultModel> InterviewRes = new List<InterviewResultModel>();


            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();
                if (AppRefNum != "")
                {
                    InterviewRes = DB.InterviewResults.Where(D => (D.MOE_APPL_NBR == AppRefNum && D.NATIONAL_ID == QID)).Select(D => new InterviewResultModel
                    {
                        ID = D.ID,
                        NATIONAL_ID = D.NATIONAL_ID,
                        MOE_APPL_NBR = D.MOE_APPL_NBR,
                        InterviewResult1 = D.InterviewResult1,
                        
                        USERID = D.USERID,
                        DTTM = D.DTTM,

                    }).ToList();

                }

            }

            return InterviewRes;

        }





    }
}