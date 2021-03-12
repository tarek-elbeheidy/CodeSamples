using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class TestResultRepository
    {

        public static DBOperationResult Insert(TestResultModel TestResultModel)
        {
            DBOperationResult ReturnValue = new DBOperationResult();
            try
            {
                // int CurrentSupportingDocID = 0;
                List<TestResultModel> CheckIfExit = GetBy(TestResultModel.MOE_APPL_NBR,TestResultModel.NATIONAL_ID);
                if (CheckIfExit.Count > 0)
                {

                    //then update
                    using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                    {
                        DB.Database.Connection.Open();


                        foreach (var item in CheckIfExit)
                        {


                            TestResult TestResultForUpdate = DB.TestResults.Where(D => D.ID == item.ID).FirstOrDefault();

                            TestResultForUpdate.ResultDocLocation = TestResultModel.ResultDocLocation;
                            TestResultForUpdate.TestResult1 = TestResultModel.TestResult1;
                            TestResultForUpdate.USERID = TestResultModel.USERID;
                            TestResultForUpdate.DTTM = TestResultModel.DTTM;
                            



                            DB.SaveChanges();
                        }
                    }

                }
                else
                {
                    using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                    {
                        DB.Database.Connection.Open();
                        TestResult TestResult1 = new TestResult
                        {
                            NATIONAL_ID = TestResultModel.NATIONAL_ID,
                            MOE_APPL_NBR = TestResultModel.MOE_APPL_NBR,
                            ResultDocLocation = TestResultModel.ResultDocLocation,
                            DTTM = DateTime.Now,
                            TestResult1 = TestResultModel.TestResult1,
                            USERID = TestResultModel.USERID,

                        };

                        DB.TestResults.Add(TestResult1);
                        DB.SaveChanges();
                        ReturnValue.InsertedID = TestResult1.ID;

                    }

                }



            }

            catch (Exception ex)
            {




            }

            return ReturnValue;



        }





        public static List<TestResultModel> GetBy(string AppRefNum, string QID)
        {
            List<TestResultModel> TestRes = new List<TestResultModel>();
            

            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();
                if (AppRefNum != "")
                {
                    TestRes = DB.TestResults.Where(D => (D.MOE_APPL_NBR == AppRefNum  && D.NATIONAL_ID == QID )).Select(D => new TestResultModel
                    {
                        ID = D.ID,
                        NATIONAL_ID =D.NATIONAL_ID,
                        MOE_APPL_NBR = D.MOE_APPL_NBR,
                        TestResult1 = D.TestResult1,
                        ResultDocLocation = D.ResultDocLocation,
                        USERID = D.USERID,
                        DTTM = D.DTTM,
                        
                    }).ToList();

                }
               
            }

            return TestRes;

        }





    }
}