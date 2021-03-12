using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class MOE_AGE_VALIDATE_Repository
    {
        public static MOE_AGE_VALIDATE_Model Get(string term, string schoolCode, string schoolType, string schoolCurriculumID, string grade, int passedID)
        {



            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {

                MOE_AGE_VALIDATE_Model Age_validate_Model = new MOE_AGE_VALIDATE_Model();
                Age_validate_Model = DB.MOE_AGE_VALIDATE.Where(D => D.MOE_TERM == term && D.MOE_SCHOOL_CODE == schoolCode && D.MOE_SCHOOL_TYPE == schoolType && D.MOE_CURRICULUM_ID == schoolCurriculumID && D.ID == passedID/*D.MOE_GRADE == grade*/).Select(D => new MOE_AGE_VALIDATE_Model
                {
                    MOE_TERM = D.MOE_TERM,
                    MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                    MOE_SCHOOL_TYPE = D.MOE_SCHOOL_TYPE,
                    MOE_CURRICULUM_ID = D.MOE_CURRICULUM_ID,
                    MOE_GRADE = D.MOE_GRADE,
                    MOE_CALCULATE_AGE_ASOF_DT = D.MOE_CALCULATE_AGE_ASOF_DT,
                    MOE_MAX_AGE = D.MOE_MAX_AGE,
                    MOE_MIN_AGE = D.MOE_MIN_AGE,
                    MOE_TRANSACTION_DTTM = D.MOE_TRANSACTION_DTTM,
                    MOE_USERID = D.MOE_USERID,
                    MOE_MAX_AGE_STRING = D.MOE_MAX_AGE_STRING,
                    MOE_MIN_AGE_STRING = D.MOE_MIN_AGE_STRING,
                    Comments = D.Comments

                }).FirstOrDefault();



                //this means the academic data is stored in our DB, then return it
                return Age_validate_Model;

            }


        }

        public static List<MOE_AGE_VALIDATE_Model> GetGradeList(string term, string schoolCode, string schoolType, string schoolCurriculumID)
        {



            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {

                List<MOE_AGE_VALIDATE_Model> Age_validate_Model = new List<MOE_AGE_VALIDATE_Model>();
                try
                {
                    Age_validate_Model = DB.MOE_AGE_VALIDATE.Where(D => D.MOE_TERM == term && D.MOE_SCHOOL_CODE == schoolCode && D.MOE_SCHOOL_TYPE == schoolType && D.MOE_CURRICULUM_ID == schoolCurriculumID).Select(D => new MOE_AGE_VALIDATE_Model
                    {
                        MOE_TERM = D.MOE_TERM,
                        MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                        MOE_SCHOOL_TYPE = D.MOE_SCHOOL_TYPE,
                        MOE_CURRICULUM_ID = D.MOE_CURRICULUM_ID,
                        MOE_GRADE = D.MOE_GRADE,
                        MOE_CALCULATE_AGE_ASOF_DT = D.MOE_CALCULATE_AGE_ASOF_DT,
                        MOE_MAX_AGE = D.MOE_MAX_AGE,
                        MOE_MIN_AGE = D.MOE_MIN_AGE,
                        MOE_TRANSACTION_DTTM = D.MOE_TRANSACTION_DTTM,
                        MOE_USERID = D.MOE_USERID,
                        MOE_CURRICULUM_DESC = D.MOE_CURRICULUM_DESC,
                        MOE_GRADE_DESC_ARA = D.MOE_GRADE_DESC_ARA,
                        MOE_GRADE_DESC_ENG = D.MOE_GRADE_DESC_ENG,
                        Weight = D.Weight,
                        MOE_MAX_AGE_STRING = D.MOE_MAX_AGE_STRING,
                        MOE_MIN_AGE_STRING = D.MOE_MIN_AGE_STRING,
                        Comments = D.Comments

                    }).ToList();


                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                }
                //this means the academic data is stored in our DB, then return it
                return Age_validate_Model;

            }


        }

        public static List<MOE_AGE_VALIDATE_Model> GetGradeListWithCurriculumOnly(string term, string schoolType, string schoolCurriculumID)
        {



            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {

                List<MOE_AGE_VALIDATE_Model> Age_validate_Model = new List<MOE_AGE_VALIDATE_Model>();
                string SchoolCodeofSameCurriculum = null;
                try
                {

                    //select distinct school code of the same curriculum
                    var xx = DB.MOE_AGE_VALIDATE.Where(D => D.MOE_TERM == term && D.MOE_SCHOOL_TYPE == schoolType && D.MOE_CURRICULUM_ID == schoolCurriculumID).Select(D => D.MOE_SCHOOL_CODE).Distinct();
                    if (xx != null)
                    {
                        SchoolCodeofSameCurriculum = xx.FirstOrDefault();

                    }

                    if (SchoolCodeofSameCurriculum != null)
                    {


                        Age_validate_Model = DB.MOE_AGE_VALIDATE.Where(D => D.MOE_TERM == term && D.MOE_SCHOOL_CODE == SchoolCodeofSameCurriculum && D.MOE_SCHOOL_TYPE == schoolType && D.MOE_CURRICULUM_ID == schoolCurriculumID).Select(D => new MOE_AGE_VALIDATE_Model
                        {
                            MOE_TERM = D.MOE_TERM,
                            MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                            MOE_SCHOOL_TYPE = D.MOE_SCHOOL_TYPE,
                            MOE_CURRICULUM_ID = D.MOE_CURRICULUM_ID,
                            MOE_GRADE = D.MOE_GRADE,
                            MOE_CALCULATE_AGE_ASOF_DT = D.MOE_CALCULATE_AGE_ASOF_DT,
                            MOE_MAX_AGE = D.MOE_MAX_AGE,
                            MOE_MIN_AGE = D.MOE_MIN_AGE,
                            MOE_TRANSACTION_DTTM = D.MOE_TRANSACTION_DTTM,
                            MOE_USERID = D.MOE_USERID,
                            MOE_CURRICULUM_DESC = D.MOE_CURRICULUM_DESC,
                            MOE_GRADE_DESC_ARA = D.MOE_GRADE_DESC_ARA,
                            MOE_GRADE_DESC_ENG = D.MOE_GRADE_DESC_ENG,
                            Weight = D.Weight,
                            MOE_MAX_AGE_STRING = D.MOE_MAX_AGE_STRING,
                            MOE_MIN_AGE_STRING = D.MOE_MIN_AGE_STRING,
                            Comments = D.Comments

                        }).ToList();

                    }

                    else
                    {
                        string SpecialCurriculumIDs = ConfigurationManager.AppSettings["SpecialCurriculumIDs"];
                        if (SpecialCurriculumIDs.Contains(schoolCurriculumID))
                        {
                            Age_validate_Model = DB.MOE_AGE_VALIDATE.Where(D => D.MOE_TERM == term && D.MOE_SCHOOL_TYPE == schoolType && D.MOE_CURRICULUM_ID == schoolCurriculumID).Select(D => new MOE_AGE_VALIDATE_Model
                            {
                                MOE_TERM = D.MOE_TERM,
                                MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                                MOE_SCHOOL_TYPE = D.MOE_SCHOOL_TYPE,
                                MOE_CURRICULUM_ID = D.MOE_CURRICULUM_ID,
                                MOE_GRADE = D.MOE_GRADE,
                                MOE_CALCULATE_AGE_ASOF_DT = D.MOE_CALCULATE_AGE_ASOF_DT,
                                MOE_MAX_AGE = D.MOE_MAX_AGE,
                                MOE_MIN_AGE = D.MOE_MIN_AGE,
                                MOE_TRANSACTION_DTTM = D.MOE_TRANSACTION_DTTM,
                                MOE_USERID = D.MOE_USERID,
                                MOE_CURRICULUM_DESC = D.MOE_CURRICULUM_DESC,
                                MOE_GRADE_DESC_ARA = D.MOE_GRADE_DESC_ARA,
                                MOE_GRADE_DESC_ENG = D.MOE_GRADE_DESC_ENG,
                                Weight = D.Weight,
                                MOE_MAX_AGE_STRING = D.MOE_MAX_AGE_STRING,
                                MOE_MIN_AGE_STRING = D.MOE_MIN_AGE_STRING,
                                Comments = D.Comments

                            }).ToList();
                        }
                    }
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                }
                //this means the academic data is stored in our DB, then return it
                return Age_validate_Model;

            }


        }

        public static MOE_AGE_VALIDATE_Model GetGradeAgeRange(string term, string schoolCode, string schoolType, string schoolCurriculumID, string GradeLevel)
        {
            MOE_AGE_VALIDATE_Model Age_validate_Model = new MOE_AGE_VALIDATE_Model();


            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {

                try
                {

                    Age_validate_Model = DB.MOE_AGE_VALIDATE.Where(D => D.MOE_TERM == term && D.MOE_SCHOOL_CODE == schoolCode && D.MOE_SCHOOL_TYPE == schoolType && D.MOE_CURRICULUM_ID == schoolCurriculumID && D.MOE_GRADE == GradeLevel).Select(D => new MOE_AGE_VALIDATE_Model
                    {
                        MOE_TERM = D.MOE_TERM,
                        MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                        MOE_SCHOOL_TYPE = D.MOE_SCHOOL_TYPE,
                        MOE_CURRICULUM_ID = D.MOE_CURRICULUM_ID,
                        MOE_GRADE = D.MOE_GRADE,
                        MOE_CALCULATE_AGE_ASOF_DT = D.MOE_CALCULATE_AGE_ASOF_DT,
                        MOE_MAX_AGE = D.MOE_MAX_AGE,
                        MOE_MIN_AGE = D.MOE_MIN_AGE,
                        MOE_TRANSACTION_DTTM = D.MOE_TRANSACTION_DTTM,
                        MOE_USERID = D.MOE_USERID,
                        MOE_CURRICULUM_DESC = D.MOE_CURRICULUM_DESC,
                        MOE_GRADE_DESC_ARA = D.MOE_GRADE_DESC_ARA,
                        MOE_GRADE_DESC_ENG = D.MOE_GRADE_DESC_ENG,
                        Weight = D.Weight,
                        MOE_MAX_AGE_STRING = D.MOE_MAX_AGE_STRING,
                        MOE_MIN_AGE_STRING = D.MOE_MIN_AGE_STRING,
                        Comments = D.Comments

                    }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                }

            }

            //this means the academic data is stored in our DB, then return it
            return Age_validate_Model;




        }

        public static void InsertAgeValidation(MOE_AGE_VALIDATE_Temp age_validate_temp)
        {
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.MOE_AGE_VALIDATE_Temp.Add(age_validate_temp);
                DB.SaveChanges();

            }
        }

        //Updated for Report to get the curriculum ID based on school - updated by Veer on 24 July 2018
        #region Get Curriculum ID BY School Code
        public static string GetCurriculumIDBySchoolCode(string term, string schoolCode)
        {
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                string CURRICULUM_ID = string.Empty;
                CURRICULUM_ID = DB.MOE_AGE_VALIDATE.Distinct().Where(D => D.MOE_TERM == term && D.MOE_SCHOOL_CODE == schoolCode).Select(D => D.MOE_CURRICULUM_ID).FirstOrDefault();
                return CURRICULUM_ID;
            }
        }
        #endregion
    }
}