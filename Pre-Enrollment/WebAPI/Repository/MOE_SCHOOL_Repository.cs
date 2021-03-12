using MOEHE.PSPES.WebAPI.Models;
using MOEHE.PSPES.WebAPI.Repository.NSIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class MOE_SCHOOL_Repository
    {



        public static List<schoolGrade> GetByGrade(string schoolYearID, string schoolCode, string isPublic)
        {




            List<schoolGrade> schoolGrade = new List<schoolGrade>();
            try
            {
                schoolGrade = NSIS_Helper.GetSchoolData(schoolYearID, schoolCode, isPublic).schoolGrades;

            }
            catch (Exception ex)
            {

                string s = ex.Message;

            }


            return schoolGrade;

            //using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
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
        // <summary>
        /// Used to get school Data 
        /// </summary>
        /// <returns></returns>
        /// 



        public static MOE_SCHOOL_Model Get(string schoolYearID, string schoolCode, string isPublic)
        {
           



            MOE_SCHOOL_Model school_Model = new MOE_SCHOOL_Model();
            try
            {
                school_Model = NSIS_Helper.GetSchoolData(schoolYearID,schoolCode, isPublic);

            }
            catch (Exception ex)
            {

                string s = ex.Message;

            }


            return school_Model;

            //using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
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
        public static List<MOE_SCHOOL_Model> Get(string schoolYearID,  string isPublic)
        {




            List<MOE_SCHOOL_Model> schools= new List<MOE_SCHOOL_Model>();
            try
            {
                schools = NSIS_Helper.GetAllSchools(schoolYearID,  isPublic);

            }
            catch (Exception ex)
            {

                string s = ex.Message;

            }


            return schools;

            //using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
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
        public static List<MOE_SCHOOL_Model> Get(string schoolYearID)
        {




            List<MOE_SCHOOL_Model> schools = new List<MOE_SCHOOL_Model>();
            try
            {
                schools = NSIS_Helper.GetQatarSchools(schoolYearID);

            }
            catch (Exception ex)
            {

                string s = ex.Message;

            }


            return schools;

            //using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
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

        //public static List<MOE_SCHOOL_Model> GetSchoolFromDB()
        //{




        //    List<MOE_SCHOOL_Model> schools = new List<MOE_SCHOOL_Model>();
        //    try
        //    {

        //        using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
        //        {
        //            schools  = DB.MOE_SCHOOL.Select(D => new MOE_SCHOOL_Model() {


        //            }).ToList();
        //        }
        //        //schools = NSIS_Helper.GetQatarSchools(schoolYearID);



        //    }
        //    catch (Exception ex)
        //    {

        //        string s = ex.Message;

        //    }


        //    return schools;



        //}

        #region added code for close and share data to NSIS modified by Veer on 16 July 2018
        public static DBOperationResult Update(MOE_SCHOOL_Model oModel)
        {
            DBOperationResult Results = new DBOperationResult();
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                MOE_SCHOOL oData = DB.MOE_SCHOOL.Where(D => D.MOE_SCHOOL_CODE == oModel.MOE_SCHOOL_CODE).FirstOrDefault();
                oData.MOE_CLOSE_DATE = oModel.MOE_CLOSE_DATE;
                oData.MOE_TRANSACTION_DTTM = DateTime.Now;
                oData.MOE_USERID = oModel.MOE_USERID;
                DB.SaveChanges();
            }
            return Results;
        }
        public static List<MOE_SCHOOL_Model> GetSchoolsListDB()
        {
            List<MOE_SCHOOL_Model> oModel = new List<MOE_SCHOOL_Model>();
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                oModel = DB.MOE_SCHOOL.Select(D => new MOE_SCHOOL_Model {
                    MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                    MOE_SCHOOL_NAME_ENG = D.MOE_SCHOOL_NAME_ENG,
                    MOE_SCHOOL_NAME_ARA = D.MOE_SCHOOL_NAME_ARA,
                    MOE_TRANSACTION_DTTM = (DateTime)D.MOE_TRANSACTION_DTTM,
                    MOE_CLOSE_DATE = D.MOE_CLOSE_DATE,
                    MOE_SCHOOL_GENDER = D.MOE_SCHOOL_GENDER
                }).ToList();
            }
            return oModel;
        }
        public static string GetSchoolCloseDate(string schoolCode)
        {
            string closeDate = string.Empty;
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                MOE_SCHOOL oData = DB.MOE_SCHOOL.Where(W => W.MOE_SCHOOL_CODE == schoolCode).FirstOrDefault();
                closeDate = oData.MOE_CLOSE_DATE.ToString();
            }
            return closeDate;
        }
        #endregion
    }
}