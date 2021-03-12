using MOEHE.PSPES.WebAPI.Models;
using MOEHE.PSPES.WebAPI.Repository.NSIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class MOE_APPLICATION_DATA_Repository
    {
        public static int GetCurrentWaitListNumber(int schoolYearID, string schoolCode, string Grade)
        {




            List<MOE_APPLICATION_DATA_Model> applicationModel = new List<MOE_APPLICATION_DATA_Model>();
            try
            {
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    applicationModel = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_YEAR == schoolYearID && D.MOE_SCHOOL_CODE == schoolCode && D.MOE_APPLIED_GRADE == Grade).Select(D => new MOE_APPLICATION_DATA_Model
                    {
                        //NATIONAL_ID = D.NATIONAL_ID,
                        //MOE_ACAD_INFO_ID = D.MOE_ACAD_INFO_ID,
                        //MOE_APPL_REF_NBR = D.MOE_APPL_REF_NBR,
                        //MOE_TERM = D.MOE_TERM,
                        //MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                        //MOE_GRADE = D.MOE_GRADE,
                        //MOE_STU_RSLT = D.MOE_STU_RSLT,
                        //MOE_TRANSACTION_DTTM = D.MOE_TRANSACTION_DTTM,
                        //MOE_USERID = D.MOE_USERID

                    }).ToList();
                }

            }
            catch (Exception ex)
            {

                string s = ex.Message;

            }


            int applicationsCount = 0;
            if (applicationModel !=null && applicationModel.Count>0)
            {
                applicationsCount = applicationModel.Count;
            }

            return applicationsCount;



        }

        public static MOE_APPLICATION_DATA_Model GetBy(string ApplicantReference)
        {




            MOE_APPLICATION_DATA_Model applicationModel = new MOE_APPLICATION_DATA_Model();
            try
            {
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    applicationModel = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_REF_NBR == ApplicantReference).Select(D => new MOE_APPLICATION_DATA_Model
                    {
                        NATIONAL_ID = D.NATIONAL_ID,

                        MOE_APPL_REF_NBR = D.MOE_APPL_REF_NBR,
                        MOE_TERM = D.MOE_TERM,
                        MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                        MOE_APPLIED_GRADE = D.MOE_APPLIED_GRADE,
                        MOE_APPL_DATE = (DateTime)D.MOE_APPL_DATE,
                        MOE_WAITLIST_NUMBER = (int)D.MOE_WAITLIST_NUMBER,


                        MOE_USERID = D.MOE_USERID,
                        MOE_AVAIL_TRANSPORT = D.MOE_AVAIL_TRANSPORT,
                        MOE_RESIDENTIAL_AREA = D.MOE_RESIDENTIAL_AREA,
                        MOE_APPL_INCR_ID = D.MOE_APPL_INCR_ID,
                        MOE_APPL_YEAR = D.MOE_APPL_YEAR,
                        MOE_STUDENT_NAME_ARA = D.MOE_STUDENT_NAME_ARA,
                        MOE_STUDENT_NAME_ENG = D.MOE_STUDENT_NAME_ENG


                    }).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {

                string s = ex.Message;

            }




            return applicationModel;



        }

        public static MOE_APPLICATION_DATA_Model GetByRefAndID(string ApplicantReference,string QID)
        {


            decimal QIDDec = decimal.Parse(QID);

            MOE_APPLICATION_DATA_Model applicationModel = new MOE_APPLICATION_DATA_Model();
            try
            {
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    applicationModel = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_REF_NBR == ApplicantReference && D.NATIONAL_ID==QIDDec).Select(D => new MOE_APPLICATION_DATA_Model
                    {
                        NATIONAL_ID = D.NATIONAL_ID,

                        MOE_APPL_REF_NBR = D.MOE_APPL_REF_NBR,
                        MOE_TERM = D.MOE_TERM,
                        MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                        MOE_APPLIED_GRADE = D.MOE_APPLIED_GRADE,
                        MOE_APPL_DATE = (DateTime)D.MOE_APPL_DATE,
                        MOE_WAITLIST_NUMBER = (int)D.MOE_WAITLIST_NUMBER,


                        MOE_USERID = D.MOE_USERID,
                        MOE_AVAIL_TRANSPORT = D.MOE_AVAIL_TRANSPORT,
                        MOE_RESIDENTIAL_AREA = D.MOE_RESIDENTIAL_AREA,
                        MOE_APPL_INCR_ID = D.MOE_APPL_INCR_ID,
                        MOE_APPL_YEAR = D.MOE_APPL_YEAR,
                        MOE_STUDENT_NAME_ARA = D.MOE_STUDENT_NAME_ARA,
                        MOE_STUDENT_NAME_ENG = D.MOE_STUDENT_NAME_ENG


                    }).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {

                string s = ex.Message;

            }




            return applicationModel;



        }


        public static DBOperationResult Insert(MOE_APPLICATION_DATA_Model applicationDataModel)
        {

            //need to handle if data exists


            MOE_APPLICATION_DATA_Model mOE_APPLICATION_DATA_Model = new MOE_APPLICATION_DATA_Model();

            DBOperationResult ReturnValue = new DBOperationResult();
            try
            {
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    if (applicationDataModel.DeactiveOtherApplications != null && (bool)applicationDataModel.DeactiveOtherApplications)
                    {
                        mOE_APPLICATION_DATA_Model = GetApplicationExistByIDANDRefNumber(applicationDataModel.NATIONAL_ID.ToString(), applicationDataModel.MOE_APPL_REF_NBR);
                    }
                    else
                    {
                        mOE_APPLICATION_DATA_Model = GetApplicationExist(applicationDataModel.MOE_TERM, applicationDataModel.MOE_SCHOOL_CODE, applicationDataModel.MOE_APPLIED_GRADE, applicationDataModel.NATIONAL_ID.ToString());
                    }
                }


                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    bool isForDeactivate = false;


                    if (applicationDataModel.DeactiveOtherApplications == null)
                    {
                        isForDeactivate = false;
                    }
                    else
                    {
                        isForDeactivate = (bool)applicationDataModel.DeactiveOtherApplications;
                    }

                    //try { isForDeactivate = (bool)applicationDataModel.DeactiveOtherApplications; }
                    //catch { }
                    if (isForDeactivate)
                    {
                        MOE_Application_DATA MOE_Application_DATAForUpdateIsFinalized = DB.MOE_Application_DATA.Where(D => D.NATIONAL_ID == applicationDataModel.NATIONAL_ID && D.MOE_APPL_REF_NBR == applicationDataModel.MOE_APPL_REF_NBR).FirstOrDefault();

                        MOE_Application_DATAForUpdateIsFinalized.IsApplicationFinalized = true; DB.SaveChanges();
                        List<MOE_Application_DATA> MOE_Application_DATAForUpdate = DB.MOE_Application_DATA.Where(D => D.NATIONAL_ID == applicationDataModel.NATIONAL_ID && D.MOE_APPL_REF_NBR != applicationDataModel.MOE_APPL_REF_NBR).ToList();
                        foreach (var item in MOE_Application_DATAForUpdate)
                        {
                            MOE_Application_DATA MOE_Application_DATAForDeactive = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_YEAR == item.MOE_TERM && D.MOE_SCHOOL_CODE == item.MOE_SCHOOL_CODE && D.MOE_APPLIED_GRADE == item.MOE_APPLIED_GRADE && D.NATIONAL_ID == item.NATIONAL_ID && D.MOE_APPL_REF_NBR == item.MOE_APPL_REF_NBR).FirstOrDefault();
                            MOE_Application_DATAForDeactive.IsActive = false;
                            DB.SaveChanges();
                        }

                    }
                    else
                    {
                        if (mOE_APPLICATION_DATA_Model != null && mOE_APPLICATION_DATA_Model.MOE_APPL_REF_NBR != null)
                        {


                            MOE_Application_DATA MOE_Application_DATAForUpdate = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_YEAR == applicationDataModel.MOE_TERM && D.MOE_SCHOOL_CODE == applicationDataModel.MOE_SCHOOL_CODE && D.MOE_APPLIED_GRADE == applicationDataModel.MOE_APPLIED_GRADE && D.NATIONAL_ID == applicationDataModel.NATIONAL_ID && D.MOE_APPL_REF_NBR == applicationDataModel.MOE_APPL_REF_NBR).FirstOrDefault();

                            MOE_Application_DATAForUpdate.MOE_TRANSACTION_DTTM = DateTime.Now;
                            MOE_Application_DATAForUpdate.MOE_USERID = applicationDataModel.MOE_USERID;
                            MOE_Application_DATAForUpdate.NATIONAL_ID = applicationDataModel.NATIONAL_ID;
                            MOE_Application_DATAForUpdate.MOE_APPL_DATE = applicationDataModel.MOE_APPL_DATE;
                            MOE_Application_DATAForUpdate.MOE_APPLIED_GRADE = applicationDataModel.MOE_APPLIED_GRADE;
                            MOE_Application_DATAForUpdate.MOE_APPL_YEAR = applicationDataModel.MOE_APPL_YEAR;
                            MOE_Application_DATAForUpdate.MOE_APPL_NBR = applicationDataModel.MOE_APPL_NBR;
                            MOE_Application_DATAForUpdate.MOE_APPL_REF_NBR = applicationDataModel.MOE_APPL_REF_NBR;
                            MOE_Application_DATAForUpdate.MOE_AVAIL_TRANSPORT = applicationDataModel.MOE_AVAIL_TRANSPORT;
                            MOE_Application_DATAForUpdate.MOE_SCHOOL_CODE = applicationDataModel.MOE_SCHOOL_CODE;
                            MOE_Application_DATAForUpdate.MOE_RESIDENTIAL_AREA = applicationDataModel.MOE_RESIDENTIAL_AREA;
                            MOE_Application_DATAForUpdate.MOE_TERM = applicationDataModel.MOE_TERM;
                            MOE_Application_DATAForUpdate.MOE_WAITLIST_NUMBER = applicationDataModel.MOE_WAITLIST_NUMBER;
                            MOE_Application_DATAForUpdate.MOE_APPL_INCR_ID = applicationDataModel.MOE_APPL_INCR_ID;
                            MOE_Application_DATAForUpdate.IsActive = applicationDataModel.IsActive;
                            MOE_Application_DATAForUpdate.MOE_STUDENT_NAME_ARA = applicationDataModel.MOE_STUDENT_NAME_ARA;
                            MOE_Application_DATAForUpdate.MOE_STUDENT_NAME_ENG = applicationDataModel.MOE_STUDENT_NAME_ENG;
                            //MOE_Application_DATAForUpdate.IsApplicationFinalized = applicationDataModel.;


                            DB.SaveChanges();
                            ReturnValue.insertedStringID = MOE_Application_DATAForUpdate.MOE_APPL_REF_NBR;
                            ReturnValue.EnglishResult = PSPESConstants.InsertionDone;

                        }

                        else
                        {


                            DB.Database.Connection.Open();
                            MOE_Application_DATA application_DATA = new MOE_Application_DATA
                            {
                                MOE_TRANSACTION_DTTM = DateTime.Now,
                                MOE_USERID = applicationDataModel.MOE_USERID,
                                NATIONAL_ID = applicationDataModel.NATIONAL_ID,
                                MOE_APPL_DATE = applicationDataModel.MOE_APPL_DATE,
                                MOE_APPLIED_GRADE = applicationDataModel.MOE_APPLIED_GRADE,
                                MOE_APPL_YEAR = applicationDataModel.MOE_APPL_YEAR,
                                MOE_APPL_NBR = applicationDataModel.MOE_APPL_NBR,
                                MOE_APPL_REF_NBR = applicationDataModel.MOE_APPL_REF_NBR,
                                MOE_AVAIL_TRANSPORT = applicationDataModel.MOE_AVAIL_TRANSPORT,
                                MOE_SCHOOL_CODE = applicationDataModel.MOE_SCHOOL_CODE,
                                MOE_RESIDENTIAL_AREA = applicationDataModel.MOE_RESIDENTIAL_AREA,
                                MOE_TERM = applicationDataModel.MOE_TERM,
                                MOE_WAITLIST_NUMBER = applicationDataModel.MOE_WAITLIST_NUMBER,
                                MOE_APPL_INCR_ID = applicationDataModel.MOE_APPL_INCR_ID,
                                IsActive = true,
                                IsApplicationFinalized = false,
                                MOE_STUDENT_NAME_ARA = applicationDataModel.MOE_STUDENT_NAME_ARA,
                                MOE_STUDENT_NAME_ENG = applicationDataModel.MOE_STUDENT_NAME_ENG
                            };
                            DB.MOE_Application_DATA.Add(application_DATA);
                            DB.SaveChanges();
                            ReturnValue.insertedStringID = application_DATA.MOE_APPL_REF_NBR;
                            ReturnValue.EnglishResult = PSPESConstants.InsertionDone;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Insert Application", Description = "Insert Application Data Exception, ex : " + ex.Message + ",inner exception= " + ex.InnerException.ToString() });
            }
            return ReturnValue;
        }

        private static MOE_APPLICATION_DATA_Model GetApplicationExistByIDANDRefNumber(string studentQID, string mOE_APPL_REF_NBR)
        {
            MOE_APPLICATION_DATA_Model applicationModel = new MOE_APPLICATION_DATA_Model();
            decimal studentQIDDec = decimal.Parse(studentQID);
            try
            {

                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    applicationModel = DB.MOE_Application_DATA.Where(D => D.NATIONAL_ID == studentQIDDec && D.MOE_APPL_REF_NBR == mOE_APPL_REF_NBR).Select(D => new MOE_APPLICATION_DATA_Model
                    {
                        NATIONAL_ID = D.NATIONAL_ID,

                        MOE_APPL_REF_NBR = D.MOE_APPL_REF_NBR,
                        MOE_TERM = D.MOE_TERM,
                        MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,

                        MOE_APPLIED_GRADE = D.MOE_APPLIED_GRADE,
                        MOE_APPL_DATE = D.MOE_APPL_DATE,
                        MOE_APPL_INCR_ID = D.MOE_APPL_INCR_ID,
                        MOE_APPL_NBR = D.MOE_APPL_NBR,
                        MOE_APPL_YEAR = D.MOE_APPL_YEAR,
                        MOE_AVAIL_TRANSPORT = D.MOE_AVAIL_TRANSPORT,
                        MOE_RESIDENTIAL_AREA = D.MOE_RESIDENTIAL_AREA,
                        MOE_TRANSACTION_DTTM = DateTime.Now,
                        MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER,
                        MOE_USERID = D.MOE_USERID,
                        IsActive = D.IsActive,
                        MOE_STUDENT_NAME_ARA = D.MOE_STUDENT_NAME_ARA,
                        MOE_STUDENT_NAME_ENG = D.MOE_STUDENT_NAME_ENG

                    }).FirstOrDefault();



                }

            }
            catch (Exception ex)
            { }

            return applicationModel;
        }



        //DO NOT change GetApplicationExist and add in WHERE IsActive =true
        public static MOE_APPLICATION_DATA_Model GetApplicationExist(int? schoolYearID, string schoolCode, string Grade, string studentQID)
        {
            MOE_APPLICATION_DATA_Model applicationModel = new MOE_APPLICATION_DATA_Model();
            decimal studentQIDDec = decimal.Parse(studentQID);
            try
            {

                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    applicationModel = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_YEAR == schoolYearID && D.MOE_SCHOOL_CODE == schoolCode && D.MOE_APPLIED_GRADE == Grade && D.NATIONAL_ID == studentQIDDec).Select(D => new MOE_APPLICATION_DATA_Model
                    {
                        NATIONAL_ID = D.NATIONAL_ID,

                        MOE_APPL_REF_NBR = D.MOE_APPL_REF_NBR,
                        MOE_TERM = D.MOE_TERM,
                        MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,

                        MOE_APPLIED_GRADE = D.MOE_APPLIED_GRADE,
                        MOE_APPL_DATE = D.MOE_APPL_DATE,
                        MOE_APPL_INCR_ID = D.MOE_APPL_INCR_ID,
                        MOE_APPL_NBR = D.MOE_APPL_NBR,
                        MOE_APPL_YEAR = D.MOE_APPL_YEAR,
                        MOE_AVAIL_TRANSPORT = D.MOE_AVAIL_TRANSPORT,
                        MOE_RESIDENTIAL_AREA = D.MOE_RESIDENTIAL_AREA,
                        MOE_TRANSACTION_DTTM = DateTime.Now,
                        MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER,
                        MOE_USERID = D.MOE_USERID,
                        IsActive = D.IsActive,
                        MOE_STUDENT_NAME_ARA = D.MOE_STUDENT_NAME_ARA,
                        MOE_STUDENT_NAME_ENG = D.MOE_STUDENT_NAME_ENG

                    }).FirstOrDefault();



                }

            }
            catch (Exception ex)
            { }

            return applicationModel;
        }

        public static bool CheckApplicationExist(int schoolYearID, string schoolCode, string Grade, string studentQID)
        {
            List<MOE_APPLICATION_DATA_Model> applicationModel = new List<MOE_APPLICATION_DATA_Model>();

            decimal decimalStudentQID = decimal.Parse(studentQID);

            bool ApplicationExist = false;
            try
            {

                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    //tarek el beheidy 1/10/2018    added check is Active application or not, to avail adding application after cancellation
                    applicationModel = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_YEAR == schoolYearID && D.MOE_SCHOOL_CODE == schoolCode && D.MOE_APPLIED_GRADE == Grade && D.NATIONAL_ID == decimalStudentQID && D.IsActive == true).Select(D => new MOE_APPLICATION_DATA_Model
                    {
                        //NATIONAL_ID = D.NATIONAL_ID,
                        //MOE_ACAD_INFO_ID = D.MOE_ACAD_INFO_ID,
                        //MOE_APPL_REF_NBR = D.MOE_APPL_REF_NBR,
                        //MOE_TERM = D.MOE_TERM,
                        //MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                        //MOE_GRADE = D.MOE_GRADE,
                        //MOE_STU_RSLT = D.MOE_STU_RSLT,
                        //MOE_TRANSACTION_DTTM = D.MOE_TRANSACTION_DTTM,
                        //MOE_USERID = D.MOE_USERID

                    }).ToList();

                    if (applicationModel.Count > 0)
                    {
                        ApplicationExist = true;
                    }

                }

            }
            catch (Exception ex)
            { }

            return ApplicationExist;
        }


        public static MOE_SCHOOL_Model GetSchoolDetail(string SchoolCode, string Term)
        {
            MOE_SCHOOL_Model schoolModel = new MOE_SCHOOL_Model();
            try
            {
                schoolModel = NSIS_Helper.GetSchoolData(Term, SchoolCode, "");
            }
            catch { }
            return schoolModel;
        }

        public static string GetStudentName(string QID, bool IsEnglish)
        {
            string studentName = "";
            if (IsEnglish)
            {
                studentName = MOE_BIO_DATA_Repository.Get(QID).MOE_ENGLISH_NAME;

            }
            else
            {
                studentName = MOE_BIO_DATA_Repository.Get(QID).MOE_ARABIC_NAME;

            }
            return studentName;
        }

        public static string GetSchoolName(string Term, string SchoolCode, bool IsEnglish)
        {
            MOE_SCHOOL_Model schoolModel = new MOE_SCHOOL_Model();
            try
            {
                schoolModel = NSIS_Helper.GetSchoolData(Term, SchoolCode, "");
            }
            catch { }
            string SChooltName = "";
            if (IsEnglish)
            {
                SChooltName = schoolModel.MOE_SCHOOL_NAME_ENG;

            }
            else
            {
                SChooltName = schoolModel.MOE_SCHOOL_NAME_ARA;

            }
            return SChooltName;
        }


        public static string GetCurricullumName(string Term, string SchoolCode, bool IsEnglish)
        {
            MOE_SCHOOL_Model schoolModel = new MOE_SCHOOL_Model();
            try
            {
                schoolModel = NSIS_Helper.GetSchoolData(Term, SchoolCode, "");
            }
            catch { }
            string CurricullumName = "";
            if (IsEnglish)
            {
                CurricullumName = schoolModel.MOE_SCHOOL_CURRICULUM;

            }
            else
            {
                CurricullumName = schoolModel.MOE_SCHOOL_CURRICULUM;

            }
            return CurricullumName;
        }
        public static List<ApplicationDataModel> GetBy(string Term, string ApplicantReference, string QID, bool IsEnglish)
        {


            decimal QIDtoDecimal = decimal.Parse(QID);

            List<ApplicationDataModel> applicationModels = new List<ApplicationDataModel>();
            List<ApplicationDataModel> applicationModelsWithNames = new List<ApplicationDataModel>();

            try
            {
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_APPL_REF_NBR == ApplicantReference || D.NATIONAL_ID == QIDtoDecimal) && D.MOE_TERM.ToString() == Term).Select(D => new ApplicationDataModel
                    {
                        QID = D.NATIONAL_ID.ToString(),
                        ApplicationDate = D.MOE_APPL_DATE.ToString(),
                        ApplicationRefNo = D.MOE_APPL_REF_NBR,
                        SchoolName = D.MOE_SCHOOL_CODE,
                        StudentName = D.NATIONAL_ID.ToString(),
                        Grade = D.MOE_SCHOOL_CODE,
                        IsEnglish = IsEnglish,
                        Term = D.MOE_TERM.ToString(),
                        IsCompletedApplication = D.IsApplicationFinalized.Value,
                        MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER,
                        MOE_STUDENT_NAME_ARA = D.MOE_STUDENT_NAME_ARA,
                        MOE_STUDENT_NAME_ENG = D.MOE_STUDENT_NAME_ENG



                    }).ToList();


                }

                applicationModelsWithNames = applicationModels.Select(D => new ApplicationDataModel
                {
                    QID = D.QID,
                    ApplicationDate = D.ApplicationDate,
                    ApplicationRefNo = D.ApplicationRefNo,
                    SchoolName = GetSchoolName(D.Term, D.SchoolName, IsEnglish),
                    StudentName = GetStudentName(D.StudentName, IsEnglish),
                    Grade = GetCurricullumName(D.Term, D.Grade, IsEnglish),
                    IsEnglish = IsEnglish,
                    Term = D.Term,
                    MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER



                }).ToList();


            }
            catch (Exception ex)
            {

                string s = ex.Message;

            }




            return applicationModelsWithNames;



        }


        public static List<ApplicationDataModel> SearchBy(string Term, string ApplicationRefNumber, decimal? QID, string SchoolCode, string Grade, DateTime ApplicationDate, bool IsEnglish)
        {
            decimal? QIDForSearch = 0;
            int? TermForSearch = 0;
            List<ApplicationDataModel> applicationModels = new List<ApplicationDataModel>();
            List<ApplicationDataModel> applicationModelsWithNames = new List<ApplicationDataModel>();
            List<ApplicationDataModel> applicationModelsWithNamesWithoutSeacrh = new List<ApplicationDataModel>();

            string SearchText = "";
            try
            {
                decimal QIDtoDecimal = decimal.Parse(ApplicationRefNumber);
                QIDForSearch = QIDtoDecimal;
            }
            catch { }
            try
            {
                TermForSearch = int.Parse(Term);
            }
            catch (Exception)
            {


            }
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                if (SchoolCode == "")
                {
                    if (TermForSearch == 0)
                    {
                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.NATIONAL_ID.ToString().Contains(SearchText) || D.MOE_APPL_REF_NBR.ToUpper().Contains(SearchText)) || D.MOE_APPLIED_GRADE.ToUpper().Contains(SearchText)).Select(D => new ApplicationDataModel
                        {
                            QID = D.NATIONAL_ID.ToString(),
                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                            SchoolName = D.MOE_SCHOOL_CODE,
                            StudentName = D.NATIONAL_ID.ToString(),
                            Grade = D.MOE_APPLIED_GRADE,
                            IsEnglish = IsEnglish,
                            Term = D.MOE_TERM.ToString(),
                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER,
                            MOE_STUDENT_NAME_ARA = D.MOE_STUDENT_NAME_ARA,
                            MOE_STUDENT_NAME_ENG = D.MOE_STUDENT_NAME_ENG



                        }).ToList();
                    }
                    else
                    {
                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_TERM == TermForSearch && (D.NATIONAL_ID.ToString().Contains(SearchText) || D.MOE_APPL_REF_NBR.ToUpper().Contains(SearchText)) || D.MOE_APPLIED_GRADE.ToUpper().Contains(SearchText))).Select(D => new ApplicationDataModel
                        {
                            QID = D.NATIONAL_ID.ToString(),
                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                            SchoolName = D.MOE_SCHOOL_CODE,
                            StudentName = D.NATIONAL_ID.ToString(),
                            Grade = D.MOE_APPLIED_GRADE,
                            IsEnglish = IsEnglish,
                            Term = D.MOE_TERM.ToString(),
                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER,
                            MOE_STUDENT_NAME_ARA = D.MOE_STUDENT_NAME_ARA,
                            MOE_STUDENT_NAME_ENG = D.MOE_STUDENT_NAME_ENG



                        }).ToList();


                    }
                }
                else
                {



                    if (TermForSearch == 0)
                    {
                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_SCHOOL_CODE == SchoolCode && (D.NATIONAL_ID.ToString().Contains(SearchText) || D.MOE_APPL_REF_NBR.ToUpper().Contains(SearchText)) || D.MOE_APPLIED_GRADE.ToUpper().Contains(SearchText)).Select(D => new ApplicationDataModel
                        {
                            QID = D.NATIONAL_ID.ToString(),
                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                            SchoolName = D.MOE_SCHOOL_CODE,
                            StudentName = D.NATIONAL_ID.ToString(),
                            Grade = D.MOE_APPLIED_GRADE,
                            IsEnglish = IsEnglish,
                            Term = D.MOE_TERM.ToString(),
                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER,
                            MOE_STUDENT_NAME_ARA = D.MOE_STUDENT_NAME_ARA,
                            MOE_STUDENT_NAME_ENG = D.MOE_STUDENT_NAME_ENG



                        }).ToList();
                    }
                    else
                    {
                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_SCHOOL_CODE == SchoolCode && (D.MOE_TERM == TermForSearch && (D.NATIONAL_ID.ToString().Contains(SearchText) || D.MOE_APPL_REF_NBR.ToUpper().Contains(SearchText)) || D.MOE_APPLIED_GRADE.ToUpper().Contains(SearchText))).Select(D => new ApplicationDataModel
                        {
                            QID = D.NATIONAL_ID.ToString(),
                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                            SchoolName = D.MOE_SCHOOL_CODE,
                            StudentName = D.NATIONAL_ID.ToString(),
                            Grade = D.MOE_APPLIED_GRADE,
                            IsEnglish = IsEnglish,
                            Term = D.MOE_TERM.ToString(),
                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER,
                            MOE_STUDENT_NAME_ARA = D.MOE_STUDENT_NAME_ARA,
                            MOE_STUDENT_NAME_ENG = D.MOE_STUDENT_NAME_ENG



                        }).ToList();

                        //if (QIDForSearch==0)
                        //{
                        //    applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_TERM == TermForSearch && D.NATIONAL_ID == QIDForSearch)).Select(D => new ApplicationDataModel
                        //    {
                        //        QID = D.NATIONAL_ID.ToString(),
                        //        ApplicationDate = D.MOE_APPL_DATE.ToString(),
                        //        ApplicationRefNo = D.MOE_APPL_REF_NBR,
                        //        SchoolName = D.MOE_SCHOOL_CODE,
                        //        StudentName = D.NATIONAL_ID.ToString(),
                        //        Curriculum = D.MOE_SCHOOL_CODE,
                        //        IsEnglish = IsEnglish,
                        //        Term = D.MOE_TERM.ToString(),IsCompletedApplication=D.IsApplicationFinalized.Value



                        //    }).ToList();
                        //}
                        //else
                        //{
                        //    applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_TERM == TermForSearch && D.NATIONAL_ID == QIDForSearch)).Select(D => new ApplicationDataModel
                        //    {
                        //        QID = D.NATIONAL_ID.ToString(),
                        //        ApplicationDate = D.MOE_APPL_DATE.ToString(),
                        //        ApplicationRefNo = D.MOE_APPL_REF_NBR,
                        //        SchoolName = D.MOE_SCHOOL_CODE,
                        //        StudentName = D.NATIONAL_ID.ToString(),
                        //        Curriculum = D.MOE_SCHOOL_CODE,
                        //        IsEnglish = IsEnglish,
                        //        Term = D.MOE_TERM.ToString(),IsCompletedApplication=D.IsApplicationFinalized.Value



                        //    }).ToList();
                        //}


                    }


                }


            }

            applicationModelsWithNames = applicationModels.Select(D => new ApplicationDataModel
            {
                QID = D.QID,
                ApplicationDate = D.ApplicationDate,
                ApplicationRefNo = D.ApplicationRefNo,
                SchoolName = GetSchoolName(D.Term, D.SchoolName, IsEnglish),
                StudentName = GetStudentName(D.StudentName, IsEnglish),
                Grade = D.Grade,
                IsEnglish = IsEnglish,
                Term = D.Term,
                ArabicSchoolName = GetSchoolName(D.Term, D.SchoolName, false),
                EnglishSchoolName = GetSchoolName(D.Term, D.SchoolName, true),
                ArabicStudentName = GetStudentName(D.StudentName, false),
                EnglishStudentName = GetStudentName(D.StudentName, true),
                MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER,
                MOE_STUDENT_NAME_ARA = D.MOE_STUDENT_NAME_ARA,
                MOE_STUDENT_NAME_ENG = D.MOE_STUDENT_NAME_ENG



            }).ToList();








            return applicationModelsWithNames;



        }


        public static List<ApplicationDataModel> SearchBy(SearchApplicationPageFilters searchApplicationPageFilters)
        {
            //TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "inside serach by", UserID = "" });
            decimal? QIDForSearch = 0;
            int? TermForSearch = 0;
            List<ApplicationDataModel> applicationModels = new List<ApplicationDataModel>();
            List<ApplicationDataModel> applicationModelsWithNames = new List<ApplicationDataModel>();
            List<ApplicationDataModel> applicationModelsWithNamesWithoutSeacrh = new List<ApplicationDataModel>();

            string SearchText = "";
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                try
                {
                    StringBuilder Log = new StringBuilder();
                    StringBuilder QueryText = new StringBuilder();

                    if (searchApplicationPageFilters.SeacrhByStudentNationID)
                    {
                        QueryText.Append(" && NATIONAL_ID ==" + searchApplicationPageFilters.StudentNationID);
                    }
                    if (searchApplicationPageFilters.SeacrhByApplicationReference)
                    {
                        QueryText.Append(" && MOE_APPL_REF_NBR ==\"" + searchApplicationPageFilters.ApplicationReference + "\"");
                    }
                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    {
                        QueryText.Append(" && MOE_SCHOOL_CODE == \"" + searchApplicationPageFilters.SchoolCode.ToString() + "\"");
                    }
                    if (searchApplicationPageFilters.SeacrhByAppliedDate)
                    {
                        QueryText.Append(" && MOE_APPL_DATE.Value.Year ==" + searchApplicationPageFilters.AppliedDate.Year + "&& D.MOE_APPL_DATE.Value.Month ==" + searchApplicationPageFilters.AppliedDate.Month + " && D.MOE_APPL_DATE.Value.Day ==" + searchApplicationPageFilters.AppliedDate.Day);
                    }

                    if (searchApplicationPageFilters.SeacrhByTerm)
                    {
                        QueryText.Append(" && MOE_TERM ==" + searchApplicationPageFilters.Term);
                    }
                    if (searchApplicationPageFilters.SeacrhByGrade)
                    {
                        QueryText.Append(" && MOE_APPLIED_GRADE ==\"" + searchApplicationPageFilters.Grade + "\"");
                    }
                    if (searchApplicationPageFilters.SeacrhByApplicationStatus)
                    {
                        QueryText.Append(" && IsApplicationFinalized ==" + searchApplicationPageFilters.IsCompletedApplication);
                    }


                    QueryText.Append(" && (IsActive ==True || IsActive == null)");

                    QueryText = QueryText.Remove(0, " && ".Length);

                    //TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Search", Description = "Search Query : " + QueryText.ToString()});

                    //Expression ex = 
                    try
                    {
                        applicationModels = DB.MOE_Application_DATA.Where(QueryText.ToString()).Select(D => new ApplicationDataModel
                        {
                            QID = D.NATIONAL_ID.ToString(),
                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                            SchoolName = D.MOE_SCHOOL_CODE,
                            StudentName = D.NATIONAL_ID.ToString(),
                            Grade = D.MOE_APPLIED_GRADE,
                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                            Term = D.MOE_TERM.ToString(),
                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER,
                            MOE_STUDENT_NAME_ARA = D.MOE_STUDENT_NAME_ARA,
                            MOE_STUDENT_NAME_ENG = D.MOE_STUDENT_NAME_ENG,
                            ArabicSchoolName = D.MOE_SCHOOL.MOE_SCHOOL_NAME_ARA,
                            EnglishSchoolName = D.MOE_SCHOOL.MOE_SCHOOL_NAME_ENG



                        }).ToList();

                        //applicationModels.ForEach()
                    }
                    catch (Exception ex)
                    {
                        TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Search", Description = "Search exception while selecting, ex : " + ex.Message + ",inner exception= " + ex.InnerException.ToString() });
                    }
                    // TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Search", Description = "Search after retrieve data, count : " + applicationModels.Count.ToString() });

                    #region old Search
                    //if (searchApplicationPageFilters.SeacrhByAppliedDate)
                    //{
                    //    //not application date
                    //    if (searchApplicationPageFilters.SeacrhByApplicationReference)
                    //    {
                    //        if (searchApplicationPageFilters.SeacrhByStudentNationID)
                    //        {
                    //            if (searchApplicationPageFilters.SeacrhByTerm)
                    //            {
                    //                if (searchApplicationPageFilters.SeacrhByGrade)
                    //                {
                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = SearchbySchoolCode(searchApplicationPageFilters, DB);

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = SearchByTermApPliedDateNationality(searchApplicationPageFilters, DB);
                    //                    }

                    //                }
                    //                //not search by grade
                    //                else
                    //                {




                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = SearchbySchoolCodeWithoutGrade(searchApplicationPageFilters, DB);

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = SearchByGradeWithoutSchoolCode(searchApplicationPageFilters, DB);
                    //                    }
                    //                }
                    //            }

                    //            //not search by term
                    //            else
                    //            {

                    //                if (searchApplicationPageFilters.SeacrhByGrade)
                    //                {
                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = SearchWithoutTermAndSchooldeCode(searchApplicationPageFilters, DB);

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = SearchWithoutCodeAndTermAndQID(searchApplicationPageFilters, DB);
                    //                    }

                    //                }
                    //                //not search by grade
                    //                else
                    //                {




                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_APPL_DATE.Value.Year == searchApplicationPageFilters.AppliedDate.Year && D.MOE_APPL_DATE.Value.Month == searchApplicationPageFilters.AppliedDate.Month && D.MOE_APPL_DATE.Value.Day == searchApplicationPageFilters.AppliedDate.Day && D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_DATE.Value.Year == searchApplicationPageFilters.AppliedDate.Year && D.MOE_APPL_DATE.Value.Month == searchApplicationPageFilters.AppliedDate.Month && D.MOE_APPL_DATE.Value.Day == searchApplicationPageFilters.AppliedDate.Day && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference && D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }
                    //                }
                    //            }
                    //        }
                    //        //not search by student national id

                    //        else
                    //        {


                    //            if (searchApplicationPageFilters.SeacrhByTerm)
                    //            {
                    //                if (searchApplicationPageFilters.SeacrhByGrade)
                    //                {
                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_APPL_DATE.Value.Year == searchApplicationPageFilters.AppliedDate.Year && D.MOE_APPL_DATE.Value.Month == searchApplicationPageFilters.AppliedDate.Month && D.MOE_APPL_DATE.Value.Day == searchApplicationPageFilters.AppliedDate.Day && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_DATE.Value.Year == searchApplicationPageFilters.AppliedDate.Year && D.MOE_APPL_DATE.Value.Month == searchApplicationPageFilters.AppliedDate.Month && D.MOE_APPL_DATE.Value.Day == searchApplicationPageFilters.AppliedDate.Day && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }

                    //                }
                    //                //not search by grade
                    //                else
                    //                {




                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_APPL_DATE.Value.Year == searchApplicationPageFilters.AppliedDate.Year && D.MOE_APPL_DATE.Value.Month == searchApplicationPageFilters.AppliedDate.Month && D.MOE_APPL_DATE.Value.Day == searchApplicationPageFilters.AppliedDate.Day && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_DATE.Value.Year == searchApplicationPageFilters.AppliedDate.Year && D.MOE_APPL_DATE.Value.Month == searchApplicationPageFilters.AppliedDate.Month && D.MOE_APPL_DATE.Value.Day == searchApplicationPageFilters.AppliedDate.Day && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }
                    //                }
                    //            }

                    //            //not search by term
                    //            else
                    //            {

                    //                if (searchApplicationPageFilters.SeacrhByGrade)
                    //                {
                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_APPL_DATE.Value.Year == searchApplicationPageFilters.AppliedDate.Year && D.MOE_APPL_DATE.Value.Month == searchApplicationPageFilters.AppliedDate.Month && D.MOE_APPL_DATE.Value.Day == searchApplicationPageFilters.AppliedDate.Day && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_DATE.Value.Year == searchApplicationPageFilters.AppliedDate.Year && D.MOE_APPL_DATE.Value.Month == searchApplicationPageFilters.AppliedDate.Month && D.MOE_APPL_DATE.Value.Day == searchApplicationPageFilters.AppliedDate.Day && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }

                    //                }
                    //                //not search by grade
                    //                else
                    //                {




                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_APPL_DATE.Value.Year == searchApplicationPageFilters.AppliedDate.Year && D.MOE_APPL_DATE.Value.Month == searchApplicationPageFilters.AppliedDate.Month && D.MOE_APPL_DATE.Value.Day == searchApplicationPageFilters.AppliedDate.Day && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_DATE.Value.Year == searchApplicationPageFilters.AppliedDate.Year && D.MOE_APPL_DATE.Value.Month == searchApplicationPageFilters.AppliedDate.Month && D.MOE_APPL_DATE.Value.Day == searchApplicationPageFilters.AppliedDate.Day && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //    //Not Appplicaiotn Ref Number

                    //    else
                    //    {
                    //        if (searchApplicationPageFilters.SeacrhByStudentNationID)
                    //        {
                    //            if (searchApplicationPageFilters.SeacrhByTerm)
                    //            {
                    //                if (searchApplicationPageFilters.SeacrhByGrade)
                    //                {
                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }

                    //                }
                    //                //not search by grade
                    //                else
                    //                {




                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }
                    //                }
                    //            }

                    //            //not search by term
                    //            else
                    //            {

                    //                if (searchApplicationPageFilters.SeacrhByGrade)
                    //                {
                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }

                    //                }
                    //                //not search by grade
                    //                else
                    //                {




                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }
                    //                }
                    //            }
                    //        }
                    //        //not search by student national id

                    //        else
                    //        {


                    //            if (searchApplicationPageFilters.SeacrhByTerm)
                    //            {
                    //                if (searchApplicationPageFilters.SeacrhByGrade)
                    //                {
                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }

                    //                }
                    //                //not search by grade
                    //                else
                    //                {




                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_TERM.ToString() == searchApplicationPageFilters.Term).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }
                    //                }
                    //            }

                    //            //not search by term
                    //            else
                    //            {

                    //                if (searchApplicationPageFilters.SeacrhByGrade)
                    //                {
                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }

                    //                }
                    //                //not search by grade
                    //                else
                    //                {




                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_DATE.Value.Year == searchApplicationPageFilters.AppliedDate.Year && D.MOE_APPL_DATE.Value.Month == searchApplicationPageFilters.AppliedDate.Month && D.MOE_APPL_DATE.Value.Day == searchApplicationPageFilters.AppliedDate.Day).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }
                    //                }
                    //            }
                    //        }

                    //    }
                    //}
                    //else
                    //{

                    //    //not application date
                    //    if (searchApplicationPageFilters.SeacrhByApplicationReference)
                    //    {
                    //        if (searchApplicationPageFilters.SeacrhByStudentNationID)
                    //        {
                    //            if (searchApplicationPageFilters.SeacrhByTerm)
                    //            {
                    //                if (searchApplicationPageFilters.SeacrhByGrade)
                    //                {
                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }

                    //                }
                    //                //not search by grade
                    //                else
                    //                {




                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }
                    //                }
                    //            }

                    //            //not search by term
                    //            else
                    //            {

                    //                if (searchApplicationPageFilters.SeacrhByGrade)
                    //                {
                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference && D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }

                    //                }
                    //                //not search by grade
                    //                else
                    //                {




                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference && D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }
                    //                }
                    //            }
                    //        }
                    //        //not search by student national id

                    //        else
                    //        {


                    //            if (searchApplicationPageFilters.SeacrhByTerm)
                    //            {
                    //                if (searchApplicationPageFilters.SeacrhByGrade)
                    //                {
                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }

                    //                }
                    //                //not search by grade
                    //                else
                    //                {




                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }
                    //                }
                    //            }

                    //            //not search by term
                    //            else
                    //            {

                    //                if (searchApplicationPageFilters.SeacrhByGrade)
                    //                {
                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }

                    //                }
                    //                //not search by grade
                    //                else
                    //                {




                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //    //Not Appplicaiotn Ref Number

                    //    else
                    //    {
                    //        if (searchApplicationPageFilters.SeacrhByStudentNationID)
                    //        {
                    //            if (searchApplicationPageFilters.SeacrhByTerm)
                    //            {
                    //                if (searchApplicationPageFilters.SeacrhByGrade)
                    //                {
                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }

                    //                }
                    //                //not search by grade
                    //                else
                    //                {




                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }
                    //                }
                    //            }

                    //            //not search by term
                    //            else
                    //            {

                    //                if (searchApplicationPageFilters.SeacrhByGrade)
                    //                {
                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }

                    //                }
                    //                //not search by grade
                    //                else
                    //                {




                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }
                    //                }
                    //            }
                    //        }
                    //        //not search by student national id

                    //        else
                    //        {


                    //            if (searchApplicationPageFilters.SeacrhByTerm)
                    //            {
                    //                if (searchApplicationPageFilters.SeacrhByGrade)
                    //                {
                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }

                    //                }
                    //                //not search by grade
                    //                else
                    //                {




                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_TERM.ToString() == searchApplicationPageFilters.Term).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }
                    //                }
                    //            }

                    //            //not search by term
                    //            else
                    //            {

                    //                if (searchApplicationPageFilters.SeacrhByGrade)
                    //                {
                    //                    //Handle Search by schoolCode
                    //                    if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade)).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();

                    //                    }
                    //                    //handle search by all
                    //                    else
                    //                    {
                    //                        applicationModels = DB.MOE_Application_DATA.Where(D => D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.NATIONAL_ID.ToString(),
                    //                            ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                            ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                            SchoolName = D.MOE_SCHOOL_CODE,
                    //                            StudentName = D.NATIONAL_ID.ToString(),
                    //                            Grade = D.MOE_APPLIED_GRADE,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.MOE_TERM.ToString(),
                    //                            IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                        }).ToList();
                    //                    }

                    //                }
                    //                //not search by grade

                    //                //handle search by application status
                    //                else
                    //                {
                    //                    if (searchApplicationPageFilters.SeacrhByApplicationStatus)
                    //                    {
                    //                        applicationModelsWithNames = applicationModels.Where(D => D.IsCompletedApplication == searchApplicationPageFilters.IsCompletedApplication).Select(D => new ApplicationDataModel
                    //                        {
                    //                            QID = D.QID,
                    //                            ApplicationDate = D.ApplicationDate,
                    //                            ApplicationRefNo = D.ApplicationRefNo,
                    //                            SchoolName = GetSchoolName(D.Term, D.SchoolName, searchApplicationPageFilters.IsEnglish),
                    //                            StudentName = GetStudentName(D.StudentName, searchApplicationPageFilters.IsEnglish),
                    //                            Grade = D.Grade,
                    //                            IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                            Term = D.Term,
                    //                            ArabicSchoolName = GetSchoolName(D.Term, D.SchoolName, false),
                    //                            EnglishSchoolName = GetSchoolName(D.Term, D.SchoolName, true),
                    //                            ArabicStudentName = GetStudentName(D.StudentName, false),
                    //                            EnglishStudentName = GetStudentName(D.StudentName, true),
                    //                            IsCompletedApplication = D.IsCompletedApplication,
                    //                            MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER



                    //                        }).ToList();
                    //                    }
                    //                    //not search by status
                    //                    else
                    //                    {




                    //                        //Handle Search by schoolCode
                    //                        if (searchApplicationPageFilters.SearchBySchoolCode)
                    //                        {
                    //                            applicationModels = DB.MOE_Application_DATA.Where(D => (D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode)).Select(D => new ApplicationDataModel
                    //                            {
                    //                                QID = D.NATIONAL_ID.ToString(),
                    //                                ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                                ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                                SchoolName = D.MOE_SCHOOL_CODE,
                    //                                StudentName = D.NATIONAL_ID.ToString(),
                    //                                Grade = D.MOE_APPLIED_GRADE,
                    //                                IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                                Term = D.MOE_TERM.ToString(),
                    //                                IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                                MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                            }).ToList();

                    //                        }



                    //                        //handle search by all
                    //                        else
                    //                        {
                    //                            applicationModels = DB.MOE_Application_DATA.Select(D => new ApplicationDataModel
                    //                            {
                    //                                QID = D.NATIONAL_ID.ToString(),
                    //                                ApplicationDate = D.MOE_APPL_DATE.ToString(),
                    //                                ApplicationRefNo = D.MOE_APPL_REF_NBR,
                    //                                SchoolName = D.MOE_SCHOOL_CODE,
                    //                                StudentName = D.NATIONAL_ID.ToString(),
                    //                                Grade = D.MOE_APPLIED_GRADE,
                    //                                IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                                Term = D.MOE_TERM.ToString(),
                    //                                IsCompletedApplication = D.IsApplicationFinalized.Value,
                    //                                MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER
                    //                            }).ToList();
                    //                        }
                    //                    }



                    //                    applicationModelsWithNames = applicationModels.Select(D => new ApplicationDataModel
                    //                    {
                    //                        QID = D.QID,
                    //                        ApplicationDate = D.ApplicationDate,
                    //                        ApplicationRefNo = D.ApplicationRefNo,
                    //                        SchoolName = GetSchoolName(D.Term, D.SchoolName, searchApplicationPageFilters.IsEnglish),
                    //                        StudentName = GetStudentName(D.StudentName, searchApplicationPageFilters.IsEnglish),
                    //                        Grade = D.Grade,
                    //                        IsEnglish = searchApplicationPageFilters.IsEnglish,
                    //                        Term = D.Term,
                    //                        ArabicSchoolName = GetSchoolName(D.Term, D.SchoolName, false),
                    //                        EnglishSchoolName = GetSchoolName(D.Term, D.SchoolName, true),
                    //                        ArabicStudentName = GetStudentName(D.StudentName, false),
                    //                        EnglishStudentName = GetStudentName(D.StudentName, true),
                    //                        IsCompletedApplication = D.IsCompletedApplication,
                    //                        MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER



                    //                    }).ToList();

                    //                }

                    //            }
                    //        }

                    //    }
                    //}

                    #endregion



                    foreach (ApplicationDataModel applicationModel in applicationModels)
                    {

                        if (applicationModel.ArabicStudentName != null && applicationModel.ArabicStudentName != "")
                        {
                            if (searchApplicationPageFilters.IsEnglish)
                            {
                                applicationModel.SchoolName = applicationModel.EnglishSchoolName;
                                applicationModel.StudentName = applicationModel.EnglishStudentName;
                            }
                            else
                            {
                                applicationModel.SchoolName = applicationModel.ArabicSchoolName;
                                applicationModel.StudentName = applicationModel.ArabicStudentName;
                            }
                        }
                        else
                        {
                            MOE_BIO_DATA_Model student_bio = MOE_BIO_DATA_Repository.Get(applicationModel.QID);
                            applicationModel.ArabicStudentName = student_bio.MOE_ARABIC_NAME;
                            applicationModel.EnglishStudentName = student_bio.MOE_ENGLISH_NAME;
                            if (searchApplicationPageFilters.IsEnglish)
                            {
                                applicationModel.SchoolName = applicationModel.EnglishSchoolName;
                                applicationModel.StudentName = applicationModel.EnglishStudentName;
                            }
                            else
                            {
                                applicationModel.SchoolName = applicationModel.ArabicSchoolName;
                                applicationModel.StudentName = applicationModel.ArabicStudentName;
                            }

                        }
                        //    MOE_SCHOOL_Model schoolModel = new MOE_SCHOOL_Model();
                        //    try
                        //    {
                        //        schoolModel = NSIS_Helper.GetSchoolData(applicationModel.Term, applicationModel.SchoolName, "");
                        //    }
                        //    catch { }

                        //    string StudentEnglishName = MOE_BIO_DATA_Repository.Get(applicationModel.QID).MOE_ENGLISH_NAME;
                        //    string StudentArabicName = MOE_BIO_DATA_Repository.Get(applicationModel.QID).MOE_ARABIC_NAME;

                        //    ApplicationDataModel applicationModelWithNames = new ApplicationDataModel();

                        //    if (schoolModel != null && schoolModel.MOE_SCHOOL_CODE != null)
                        //    {
                        //        if (searchApplicationPageFilters.IsEnglish)
                        //        {
                        //            applicationModelWithNames.SchoolName = schoolModel.MOE_SCHOOL_NAME_ENG;
                        //            applicationModelWithNames.StudentName = StudentEnglishName;
                        //        }
                        //        else
                        //        {
                        //            applicationModelWithNames.SchoolName = schoolModel.MOE_SCHOOL_NAME_ARA;
                        //            applicationModelWithNames.StudentName = StudentArabicName;
                        //        }


                        //        applicationModelWithNames.EnglishSchoolName = schoolModel.MOE_SCHOOL_NAME_ENG;
                        //        applicationModelWithNames.ArabicSchoolName = schoolModel.MOE_SCHOOL_NAME_ARA;
                        //    }

                        //    if (!applicationModel.IsCompletedApplication)
                        //    {
                        //        if (searchApplicationPageFilters.IsEnglish)
                        //        {
                        //            applicationModelWithNames.FinalizeButtonText = "Click to Finalize the Application";
                        //        }
                        //        else
                        //        {
                        //            applicationModelWithNames.FinalizeButtonText = "اضغط لإستكمال الطلب";
                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (searchApplicationPageFilters.IsEnglish)
                        //        {
                        //            applicationModelWithNames.FinalizeButtonText = "Click to View the Application";
                        //        }
                        //        else
                        //        {
                        //            applicationModelWithNames.FinalizeButtonText = "اضغط لعرض الطلب";
                        //        }
                        //    }

                        //    applicationModelWithNames.QID = applicationModel.QID;
                        //    applicationModelWithNames.ApplicationDate = applicationModel.ApplicationDate;
                        //    applicationModelWithNames.ApplicationRefNo = applicationModel.ApplicationRefNo;

                        //    applicationModelWithNames.Grade = applicationModel.Grade;
                        //    applicationModelWithNames.IsEnglish = searchApplicationPageFilters.IsEnglish;
                        //    applicationModelWithNames.Term = applicationModel.Term;
                        //    applicationModelWithNames.ArabicStudentName = StudentArabicName;
                        //    applicationModelWithNames.EnglishStudentName = StudentEnglishName;
                        //    applicationModelWithNames.IsCompletedApplication = applicationModel.IsCompletedApplication;
                        //    applicationModelWithNames.MOE_WAITLIST_NUMBER = applicationModel.MOE_WAITLIST_NUMBER;
                        //    applicationModelWithNames.ApplicationDate = Convert.ToDateTime(applicationModel.ApplicationDate).ToString("dd/MM/yyyy");

                        // applicationModelsWithNames.Add(applicationModelWithNames);
                    }
                    // TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Search", Description = "Search after retrieve named  data, count : " + applicationModelsWithNames.Count.ToString() });

                }
                catch (Exception ex)
                {
                    TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Search", Description = "Start Search exception : " + ex.Message + ",inner exception=" + ex.InnerException.ToString() });
                }



            }









            applicationModelsWithNames = applicationModels;
            return applicationModelsWithNames;



        }

        private static List<ApplicationDataModel> SearchWithoutCodeAndTermAndQID(SearchApplicationPageFilters searchApplicationPageFilters, PrivateScoolsPreEnrollmentEntities222 DB)
        {
            return DB.MOE_Application_DATA.Where(D => D.MOE_APPL_DATE.Value.Year == searchApplicationPageFilters.AppliedDate.Year && D.MOE_APPL_DATE.Value.Month == searchApplicationPageFilters.AppliedDate.Month && D.MOE_APPL_DATE.Value.Day == searchApplicationPageFilters.AppliedDate.Day && D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference).Select(D => new ApplicationDataModel
            {
                QID = D.NATIONAL_ID.ToString(),
                ApplicationDate = D.MOE_APPL_DATE.ToString(),
                ApplicationRefNo = D.MOE_APPL_REF_NBR,
                SchoolName = D.MOE_SCHOOL_CODE,
                StudentName = D.NATIONAL_ID.ToString(),
                Grade = D.MOE_APPLIED_GRADE,
                IsEnglish = searchApplicationPageFilters.IsEnglish,
                Term = D.MOE_TERM.ToString(),
                IsCompletedApplication = D.IsApplicationFinalized.Value,
                MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER,
                MOE_STUDENT_NAME_ARA = D.MOE_STUDENT_NAME_ARA,
                MOE_STUDENT_NAME_ENG = D.MOE_STUDENT_NAME_ENG
            }).ToList();
        }

        private static List<ApplicationDataModel> SearchWithoutTermAndSchooldeCode(SearchApplicationPageFilters searchApplicationPageFilters, PrivateScoolsPreEnrollmentEntities222 DB)
        {
            return DB.MOE_Application_DATA.Where(D => (D.MOE_APPL_DATE.Value.Year == searchApplicationPageFilters.AppliedDate.Year && D.MOE_APPL_DATE.Value.Month == searchApplicationPageFilters.AppliedDate.Month && D.MOE_APPL_DATE.Value.Day == searchApplicationPageFilters.AppliedDate.Day && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference && D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade)).Select(D => new ApplicationDataModel
            {
                QID = D.NATIONAL_ID.ToString(),
                ApplicationDate = D.MOE_APPL_DATE.ToString(),
                ApplicationRefNo = D.MOE_APPL_REF_NBR,
                SchoolName = D.MOE_SCHOOL_CODE,
                StudentName = D.NATIONAL_ID.ToString(),
                Grade = D.MOE_APPLIED_GRADE,
                IsEnglish = searchApplicationPageFilters.IsEnglish,
                Term = D.MOE_TERM.ToString(),
                IsCompletedApplication = D.IsApplicationFinalized.Value,
                MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER,
                MOE_STUDENT_NAME_ARA = D.MOE_STUDENT_NAME_ARA,
                MOE_STUDENT_NAME_ENG = D.MOE_STUDENT_NAME_ENG
            }).ToList();
        }

        private static List<ApplicationDataModel> SearchByGradeWithoutSchoolCode(SearchApplicationPageFilters searchApplicationPageFilters, PrivateScoolsPreEnrollmentEntities222 DB)
        {
            return DB.MOE_Application_DATA.Where(D => D.MOE_APPL_DATE.Value.Year == searchApplicationPageFilters.AppliedDate.Year && D.MOE_APPL_DATE.Value.Month == searchApplicationPageFilters.AppliedDate.Month && D.MOE_APPL_DATE.Value.Day == searchApplicationPageFilters.AppliedDate.Day && D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term).Select(D => new ApplicationDataModel
            {
                QID = D.NATIONAL_ID.ToString(),
                ApplicationDate = D.MOE_APPL_DATE.ToString(),
                ApplicationRefNo = D.MOE_APPL_REF_NBR,
                SchoolName = D.MOE_SCHOOL_CODE,
                StudentName = D.NATIONAL_ID.ToString(),
                Grade = D.MOE_APPLIED_GRADE,
                IsEnglish = searchApplicationPageFilters.IsEnglish,
                Term = D.MOE_TERM.ToString(),
                IsCompletedApplication = D.IsApplicationFinalized.Value,
                MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER,
                MOE_STUDENT_NAME_ARA = D.MOE_STUDENT_NAME_ARA,
                MOE_STUDENT_NAME_ENG = D.MOE_STUDENT_NAME_ENG
            }).ToList();
        }

        private static List<ApplicationDataModel> SearchbySchoolCodeWithoutGrade(SearchApplicationPageFilters searchApplicationPageFilters, PrivateScoolsPreEnrollmentEntities222 DB)
        {
            return DB.MOE_Application_DATA.Where(D => (D.MOE_APPL_DATE.Value.Year == searchApplicationPageFilters.AppliedDate.Year && D.MOE_APPL_DATE.Value.Month == searchApplicationPageFilters.AppliedDate.Month && D.MOE_APPL_DATE.Value.Day == searchApplicationPageFilters.AppliedDate.Day && D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode)).Select(D => new ApplicationDataModel
            {
                QID = D.NATIONAL_ID.ToString(),
                ApplicationDate = D.MOE_APPL_DATE.ToString(),
                ApplicationRefNo = D.MOE_APPL_REF_NBR,
                SchoolName = D.MOE_SCHOOL_CODE,
                StudentName = D.NATIONAL_ID.ToString(),
                Grade = D.MOE_APPLIED_GRADE,
                IsEnglish = searchApplicationPageFilters.IsEnglish,
                Term = D.MOE_TERM.ToString(),
                IsCompletedApplication = D.IsApplicationFinalized.Value,
                MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER,
                MOE_STUDENT_NAME_ARA = D.MOE_STUDENT_NAME_ARA,
                MOE_STUDENT_NAME_ENG = D.MOE_STUDENT_NAME_ENG
            }).ToList();
        }

        private static List<ApplicationDataModel> SearchByTermApPliedDateNationality(SearchApplicationPageFilters searchApplicationPageFilters, PrivateScoolsPreEnrollmentEntities222 DB)
        {
            return DB.MOE_Application_DATA.Where(D => D.MOE_APPL_DATE.Value.Year == searchApplicationPageFilters.AppliedDate.Year && D.MOE_APPL_DATE.Value.Month == searchApplicationPageFilters.AppliedDate.Month && D.MOE_APPL_DATE.Value.Day == searchApplicationPageFilters.AppliedDate.Day && D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference).Select(D => new ApplicationDataModel
            {
                QID = D.NATIONAL_ID.ToString(),
                ApplicationDate = D.MOE_APPL_DATE.ToString(),
                ApplicationRefNo = D.MOE_APPL_REF_NBR,
                SchoolName = D.MOE_SCHOOL_CODE,
                StudentName = D.NATIONAL_ID.ToString(),
                Grade = D.MOE_APPLIED_GRADE,
                IsEnglish = searchApplicationPageFilters.IsEnglish,
                Term = D.MOE_TERM.ToString(),
                IsCompletedApplication = D.IsApplicationFinalized.Value,
                MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER,
                MOE_STUDENT_NAME_ARA = D.MOE_STUDENT_NAME_ARA,
                MOE_STUDENT_NAME_ENG = D.MOE_STUDENT_NAME_ENG
            }).ToList();
        }

        private static List<ApplicationDataModel> SearchbySchoolCode(SearchApplicationPageFilters searchApplicationPageFilters, PrivateScoolsPreEnrollmentEntities222 DB)
        {
            return DB.MOE_Application_DATA.Where(D => (D.MOE_APPL_DATE.Value.Year == searchApplicationPageFilters.AppliedDate.Year && D.MOE_APPL_DATE.Value.Month == searchApplicationPageFilters.AppliedDate.Month && D.MOE_APPL_DATE.Value.Day == searchApplicationPageFilters.AppliedDate.Day && D.NATIONAL_ID.ToString() == searchApplicationPageFilters.StudentNationID && D.MOE_TERM.ToString() == searchApplicationPageFilters.Term && D.MOE_SCHOOL_CODE == searchApplicationPageFilters.SchoolCode && D.MOE_APPLIED_GRADE == searchApplicationPageFilters.Grade && D.MOE_APPL_REF_NBR == searchApplicationPageFilters.ApplicationReference)).Select(D => new ApplicationDataModel
            {
                QID = D.NATIONAL_ID.ToString(),
                ApplicationDate = D.MOE_APPL_DATE.ToString(),
                ApplicationRefNo = D.MOE_APPL_REF_NBR,
                SchoolName = D.MOE_SCHOOL_CODE,
                StudentName = D.NATIONAL_ID.ToString(),
                Grade = D.MOE_APPLIED_GRADE,
                IsEnglish = searchApplicationPageFilters.IsEnglish,
                Term = D.MOE_TERM.ToString(),
                IsCompletedApplication = D.IsApplicationFinalized.Value,
                MOE_WAITLIST_NUMBER = D.MOE_WAITLIST_NUMBER,
                MOE_STUDENT_NAME_ARA = D.MOE_STUDENT_NAME_ARA,
                MOE_STUDENT_NAME_ENG = D.MOE_STUDENT_NAME_ENG

            }).ToList();
        }

        #region Is Finalized Application - Withdrawal Applicaiton added by Veer on 30 July
        public static MOE_APPLICATION_DATA_Model FinalizedBySchool(string studentQID, string schoolCode, int year)
        {
            MOE_APPLICATION_DATA_Model applicationModel = new MOE_APPLICATION_DATA_Model();
            decimal decimalStudentQID = decimal.Parse(studentQID);
            try
            {
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    applicationModel = DB.MOE_Application_DATA.Where(D => D.MOE_TERM == year && D.MOE_SCHOOL_CODE == schoolCode && D.NATIONAL_ID == decimalStudentQID && D.IsApplicationFinalized == true).Select(D => new MOE_APPLICATION_DATA_Model {
                        MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                        MOE_APPL_REF_NBR = D.MOE_APPL_REF_NBR,
                        NATIONAL_ID = D.NATIONAL_ID,
                        MOE_TERM = D.MOE_TERM,
                        MOE_APPLIED_GRADE = D.MOE_APPLIED_GRADE,
                        MOE_RESIDENTIAL_AREA = D.MOE_RESIDENTIAL_AREA,
                        MOE_AVAIL_TRANSPORT = D.MOE_AVAIL_TRANSPORT,
                        MOE_APPL_DATE = D.MOE_APPL_DATE,
                        MOE_TRANSACTION_DTTM = (DateTime)D.MOE_TRANSACTION_DTTM,
                        IsActive = D.IsActive,
                        MOE_APPL_YEAR = D.MOE_APPL_YEAR
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            { }
            return applicationModel;
        }
        #endregion
    }
}