using MOEHE.PSPES.WebAPI.Models;
using MOEHE.PSPES.WebAPI.Repository.NSIS;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class MOE_APPL_ACAD_INFO_Repository
    {
        /// <summary>
        /// Used to get Bio Data with National id
        /// </summary>
        /// <returns></returns>
        public static MOE_APPL_ACAD_INFO_Model Get(string nationalID, int requestedTerm)
        {
            decimal nationalIDDec = decimal.Parse(nationalID);



            MOE_APPL_ACAD_INFO_Model Acad_Model_service = new MOE_APPL_ACAD_INFO_Model();
            try
            {
                Acad_Model_service = NSIS_Helper.GetEnrollmentData(nationalID, requestedTerm);

            }
            catch (Exception ex)
            {

                string s = ex.Message;

            }


            return Acad_Model_service;

            //using (PrivateScoolsPreEnrollmentEntities DB = new PrivateScoolsPreEnrollmentEntities())
            //{

            //    MOE_APPL_ACAD_INFO_Model Acad_Model = new MOE_APPL_ACAD_INFO_Model();
            //    Acad_Model = DB.MOE_APPL_ACAD_INFO.Where(D => D.NATIONAL_ID == nationalIDDec && D.MOE_TERM == requestedTerm).Select(D => new MOE_APPL_ACAD_INFO_Model
            //    {
            //        NATIONAL_ID = D.NATIONAL_ID,
            //        MOE_ACAD_INFO_ID = D.MOE_ACAD_INFO_ID,
            //        MOE_APPL_REF_NBR = D.MOE_APPL_REF_NBR,
            //        MOE_TERM = D.MOE_TERM,
            //        MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
            //        MOE_GRADE = D.MOE_GRADE,
            //        MOE_STU_RSLT = D.MOE_STU_RSLT,
            //        MOE_TRANSACTION_DTTM = D.MOE_TRANSACTION_DTTM,
            //        MOE_USERID = D.MOE_USERID

            //    }).FirstOrDefault();


            //    if (Acad_Model != null)
            //    {
            //        //this means the academic data is stored in our DB, then return it
            //        return Acad_Model;

            //    }
            //}


        }



        public static MOE_APPL_ACAD_INFO_Model GetACADExist( string ApplicationRefernceNumber,int? Term)
        {
          MOE_APPL_ACAD_INFO_Model mOE_APPL_ACAD_INFO_Model = new MOE_APPL_ACAD_INFO_Model();

            try
            {

                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    mOE_APPL_ACAD_INFO_Model = DB.MOE_APPL_ACAD_INFO.Where(D => D.MOE_APPL_REF_NBR == ApplicationRefernceNumber &&  D.MOE_TERM== Term).Select(D => new MOE_APPL_ACAD_INFO_Model
                    {

                        NATIONAL_ID = D.NATIONAL_ID,
                        MOE_TERM = D.MOE_TERM,
                        MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE.ToString(),
                        MOE_GRADE = D.MOE_GRADE.ToString(),
                        MOE_STU_RSLT = D.MOE_STU_RSLT,
                        MOE_APPL_REF_NBR = D.MOE_APPL_REF_NBR,
                        MOE_TRANSACTION_DTTM = DateTime.Now,
                        MOE_USERID = D.MOE_USERID,
                         IsActive=D.IsActive

                    }).FirstOrDefault();



                }

            }
            catch (Exception ex)
            { }

            return mOE_APPL_ACAD_INFO_Model;
        }


        public static List<MOE_APPL_ACAD_INFO_Model> GetACADListForDeactivate(string ApplicationRefernceNumber,decimal? NationalID)
        {
            List<MOE_APPL_ACAD_INFO_Model> mOE_APPL_ACAD_INFO_Model = new List<MOE_APPL_ACAD_INFO_Model>();

            try
            {

                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    mOE_APPL_ACAD_INFO_Model = DB.MOE_APPL_ACAD_INFO.Where(D => D.MOE_APPL_REF_NBR != ApplicationRefernceNumber && D.NATIONAL_ID==NationalID).Select(D => new MOE_APPL_ACAD_INFO_Model
                    {

                        NATIONAL_ID = D.NATIONAL_ID,
                        MOE_TERM = D.MOE_TERM,
                        MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE.ToString(),
                        MOE_GRADE = D.MOE_GRADE.ToString(),
                        MOE_STU_RSLT = D.MOE_STU_RSLT,
                        MOE_APPL_REF_NBR = D.MOE_APPL_REF_NBR,
                        MOE_TRANSACTION_DTTM = DateTime.Now,
                        MOE_USERID = D.MOE_USERID,
                        IsActive = D.IsActive
                        

                    }).ToList();



                }

            }
            catch (Exception ex)
            { }

            return mOE_APPL_ACAD_INFO_Model;
        }


        public static List<MOE_APPL_ACAD_INFO_Model> GetACADInfoByAppRef (string ApplicationRefernceNumber)
        {

            List<MOE_APPL_ACAD_INFO_Model> mOE_APPL_ACAD_INFO_Model = new List<MOE_APPL_ACAD_INFO_Model>();

            try
            {

                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    mOE_APPL_ACAD_INFO_Model = DB.MOE_APPL_ACAD_INFO.Where(D => D.MOE_APPL_REF_NBR == ApplicationRefernceNumber).Select(D => new MOE_APPL_ACAD_INFO_Model
                    {

                        NATIONAL_ID = D.NATIONAL_ID,
                        MOE_TERM = D.MOE_TERM,
                        MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE.ToString(),
                        MOE_GRADE = D.MOE_GRADE.ToString(),
                        MOE_STU_RSLT = D.MOE_STU_RSLT,
                        MOE_APPL_REF_NBR = D.MOE_APPL_REF_NBR,
                        MOE_TRANSACTION_DTTM = DateTime.Now,
                        MOE_USERID = D.MOE_USERID,
                        IsActive = D.IsActive
                        

                    }).ToList();



                }

            }
            catch (Exception ex)
            { }

            return mOE_APPL_ACAD_INFO_Model;

        }


        public static DBOperationResult Insert(MOE_APPL_ACAD_INFO_Model AcadDataModel)
        {


            //need to handle if data exists


            DBOperationResult ReturnValue = new DBOperationResult();
            try
            {
                MOE_APPL_ACAD_INFO_Model mOE_APPL_ACAD_INFO_Model = GetACADExist(AcadDataModel.MOE_APPL_REF_NBR, AcadDataModel.MOE_TERM);


                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    DB.Database.Connection.Open();

                    bool isForDeactivate = false;

                    try { isForDeactivate = (bool)AcadDataModel.DeactiveOtherApplications; }
                    catch { }
                    if (isForDeactivate)
                    {
                        //this mean deactivate all except current
                        List<MOE_APPL_ACAD_INFO_Model> list = new List<MOE_APPL_ACAD_INFO_Model>();
                        list = GetACADListForDeactivate(AcadDataModel.MOE_APPL_REF_NBR, AcadDataModel.NATIONAL_ID);
                        if (list.Count > 0)
                        {
                            foreach (var item in list)
                            {
                                MOE_APPL_ACAD_INFO acad_DATAForUpdate = DB.MOE_APPL_ACAD_INFO.Where(D => D.MOE_APPL_REF_NBR == item.MOE_APPL_REF_NBR && D.MOE_TERM == item.MOE_TERM).FirstOrDefault();

                                acad_DATAForUpdate.IsActive = false;
                                DB.SaveChanges();

                            }
                        }
                    }
                    else
                    {
                        if (mOE_APPL_ACAD_INFO_Model != null && mOE_APPL_ACAD_INFO_Model.MOE_APPL_REF_NBR != null)
                        {

                           
                                // this mean update
                                MOE_APPL_ACAD_INFO acad_DATAForUpdate = DB.MOE_APPL_ACAD_INFO.Where(D => D.MOE_APPL_REF_NBR == AcadDataModel.MOE_APPL_REF_NBR && D.MOE_TERM == AcadDataModel.MOE_TERM).FirstOrDefault();
                                acad_DATAForUpdate.NATIONAL_ID = AcadDataModel.NATIONAL_ID;
                                acad_DATAForUpdate.MOE_TERM = AcadDataModel.MOE_TERM;
                                acad_DATAForUpdate.MOE_SCHOOL_CODE = AcadDataModel.MOE_SCHOOL_CODE.ToString();
                                acad_DATAForUpdate.MOE_GRADE = AcadDataModel.MOE_GRADE.ToString();
                                acad_DATAForUpdate.MOE_STU_RSLT = AcadDataModel.MOE_STU_RSLT;
                            acad_DATAForUpdate.MOE_APPL_REF_NBR = AcadDataModel.MOE_APPL_REF_NBR;
                             acad_DATAForUpdate.MOE_TRANSACTION_DTTM = DateTime.Now;
                                acad_DATAForUpdate.MOE_USERID = AcadDataModel.MOE_USERID;
                                acad_DATAForUpdate.IsActive = AcadDataModel.IsActive;
                                DB.SaveChanges();
                            ReturnValue.insertedStringID = acad_DATAForUpdate.MOE_APPL_REF_NBR;
                            ReturnValue.EnglishResult = PSPESConstants.InsertionDone;



                        }
                        else
                        {



                            MOE_APPL_ACAD_INFO acad_DATA = new MOE_APPL_ACAD_INFO
                            {
                                NATIONAL_ID = AcadDataModel.NATIONAL_ID,
                                MOE_TERM = AcadDataModel.MOE_TERM,
                                MOE_SCHOOL_CODE = AcadDataModel.MOE_SCHOOL_CODE.ToString(),
                                MOE_GRADE = AcadDataModel.MOE_GRADE.ToString(),
                                MOE_STU_RSLT = AcadDataModel.MOE_STU_RSLT,
                                MOE_APPL_REF_NBR = AcadDataModel.MOE_APPL_REF_NBR,
                                MOE_TRANSACTION_DTTM = DateTime.Now,
                                MOE_USERID = AcadDataModel.MOE_USERID,
                                IsActive = true
                            };
                            DB.MOE_APPL_ACAD_INFO.Add(acad_DATA);
                            DB.SaveChanges();
                            ReturnValue.insertedStringID = AcadDataModel.MOE_APPL_REF_NBR;
                            ReturnValue.EnglishResult = PSPESConstants.InsertionDone;
                        }
                    }
                }
            }
            catch (Exception ex)
            { TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Insert Academic Information", Description = "Insert Academic Information Data Exception, ex : " + ex.Message + ",inner exception= " + ex.InnerException.ToString() }); }
            return ReturnValue;
        }

        #region created new method for withdrawal form by Veer on 12 July 2018
        public static MOE_APPL_ACAD_INFO_Model GetData(string nationalID)
        {
            decimal Qid = decimal.Parse(nationalID);
            MOE_APPL_ACAD_INFO_Model Acad_Model = new MOE_APPL_ACAD_INFO_Model();
            try
            {
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    string sCode = DB.MOE_APPL_ACAD_INFO.Where(D => D.NATIONAL_ID == Qid).Select(D => D.MOE_SCHOOL_CODE).FirstOrDefault();
                    MOE_SCHOOL_Model school_Model = NSIS_Helper.GetSchoolData(TermRepository.Get().Where(s => s.ACADEMIC_YEAR_DESC == "CURRENT").Select(d => d).FirstOrDefault().TermCode, sCode, "false");


                    Acad_Model = DB.MOE_APPL_ACAD_INFO.Where(D => D.NATIONAL_ID == Qid && D.MOE_TERM == DateTime.Now.Year).Select(D => new MOE_APPL_ACAD_INFO_Model
                    {
                        NATIONAL_ID = D.NATIONAL_ID,
                        MOE_ACAD_INFO_ID = D.MOE_ACAD_INFO_ID,
                        MOE_APPL_REF_NBR = D.MOE_APPL_REF_NBR,
                        MOE_TERM = D.MOE_TERM,
                        MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                        MOE_GRADE = D.MOE_GRADE,
                        MOE_STU_RSLT = D.MOE_STU_RSLT,
                        MOE_TRANSACTION_DTTM = (DateTime)D.MOE_TRANSACTION_DTTM,
                        MOE_USERID = D.MOE_USERID,
                        MOE_SCHOOL_NAME = school_Model.MOE_SCHOOL_NAME_ENG,
                        MOE_SCHOOL_ARABIC_NAME = school_Model.MOE_SCHOOL_NAME_ARA,
                        MOE_SCHOOL_ARABIC_CURRICULUM = school_Model.MOE_SCHOOL_CURRICULUM,
                        MOE_SCHOOL_CURRICULUM_ID = school_Model.MOE_SCHOOL_CURRICULUM_ID,
                        MOE_SCHOOL_CURRICULUM = school_Model.MOE_SCHOOL_CURRICULUM

                    }).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return Acad_Model;
        }
        #endregion
    }
}