using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class MOE_ENROLLMENT_EXCEPTION_Repository
    {
        public static List<MOE_ENROLLMENT_EXCEPTION> Get(string QID, int Term)
        {
            List<MOE_ENROLLMENT_EXCEPTION> Exception_Models;
            decimal QIDDesc = decimal.Parse(QID);
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                Exception_Models = DB.MOE_ENROLLMENT_EXCEPTION.Where(D => D.National_ID == QIDDesc && D.MOE_TERM == Term).Select(D => D).ToList();
            }

            return Exception_Models;
        }

        public static List<MOE_ENROLLMENT_EXCEPTION> GetBySchool(string QID, int Term,string SchoolCode)
        {
            List<MOE_ENROLLMENT_EXCEPTION> Exception_Models;
            decimal QIDDesc = decimal.Parse(QID);
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                Exception_Models = DB.MOE_ENROLLMENT_EXCEPTION.Where(D => D.National_ID == QIDDesc && D.MOE_TERM == Term && D.MOE_SCHOOL_CODE == SchoolCode).Select(D => D).ToList();
            }

            return Exception_Models;
        }

        public static MOE_ENROLLMENT_EXCEPTION GetBySchoolAndGrade(string QID, int Term, string SchoolCode,string Grade)
        {
            MOE_ENROLLMENT_EXCEPTION Exception_Models;
            decimal QIDDesc = decimal.Parse(QID);
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                Exception_Models = DB.MOE_ENROLLMENT_EXCEPTION.Where(D => D.National_ID == QIDDesc && D.MOE_TERM == Term && D.MOE_SCHOOL_CODE == SchoolCode &&D.MOE_GRADE == Grade).Select(D => D).FirstOrDefault();
            }

            return Exception_Models;
        }

        public static DBOperationResult Insert (MOE_ENROLLMENT_EXCEPTION_Model student_exception)
        {
            MOE_ENROLLMENT_EXCEPTION exception_model = CheckExist(student_exception.National_ID, student_exception.MOE_SCHOOL_CODE, student_exception.MOE_GRADE);
            DBOperationResult ReturnValue = new DBOperationResult();

            if (exception_model != null && exception_model.National_ID != null)
            {
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {

                    MOE_ENROLLMENT_EXCEPTION exception_model_to_update = DB.MOE_ENROLLMENT_EXCEPTION.Where(D => D.National_ID == student_exception.National_ID && D.MOE_SCHOOL_CODE == student_exception.MOE_SCHOOL_CODE && D.MOE_GRADE == student_exception.MOE_GRADE).Select(D => D).FirstOrDefault();
                    exception_model_to_update.National_ID = student_exception.National_ID;
                    exception_model_to_update.MOE_SCHOOL_CODE = student_exception.MOE_SCHOOL_CODE;
                    exception_model_to_update.MOE_GRADE = student_exception.MOE_GRADE;
                    exception_model_to_update.MOE_EXCEPTION_EXPIRY = student_exception.MOE_EXCEPTION_EXPIRY;
                    exception_model_to_update.MOE_TERM = student_exception.MOE_TERM;
                    exception_model_to_update.AGE_EXCEPTION = student_exception.AGE_EXCEPTION;
                    exception_model_to_update.ENROLLMENT_WHILE_CLOSED_EXCEPTION = student_exception.ENROLLMENT_WHILE_CLOSED_EXCEPTION;
                    exception_model_to_update.GENDER_EXCEPTION = student_exception.GENDER_EXCEPTION;
                    exception_model_to_update.NATIONALITY_EXCEPTION = student_exception.NATIONALITY_EXCEPTION;
                    exception_model_to_update.REPEAT_YEAR_EXCEPTION = student_exception.REPEAT_YEAR_EXCEPTION;
                    DB.SaveChanges();
                    MOE_ENROLLMENT_EXCEPTION_AUDIT exception_model_audit = new MOE_ENROLLMENT_EXCEPTION_AUDIT();
                    exception_model_audit.National_ID = student_exception.National_ID;
                    exception_model_audit.MOE_SCHOOL_CODE = student_exception.MOE_SCHOOL_CODE;
                    exception_model_audit.MOE_GRADE = student_exception.MOE_GRADE;
                    exception_model_audit.MOE_EXCEPTION_EXPIRY = student_exception.MOE_EXCEPTION_EXPIRY;
                    exception_model_audit.MOE_TERM = student_exception.MOE_TERM;
                    exception_model_audit.AGE_EXCEPTION = student_exception.AGE_EXCEPTION;
                    exception_model_audit.ENROLLMENT_WHILE_CLOSED_EXCEPTION = student_exception.ENROLLMENT_WHILE_CLOSED_EXCEPTION;
                    exception_model_audit.GENDER_EXCEPTION = student_exception.GENDER_EXCEPTION;
                    exception_model_audit.NATIONALITY_EXCEPTION = student_exception.NATIONALITY_EXCEPTION;
                    exception_model_audit.REPEAT_YEAR_EXCEPTION = student_exception.REPEAT_YEAR_EXCEPTION;
                    exception_model_audit.MOE_UPDATED_BY = student_exception.MOE_User_Name;
                    exception_model_audit.ENROLLMENT_EXCEPTION_ID = exception_model_to_update.ID;
                    exception_model_audit.MOE_UPDATED_ON = DateTime.Now;
                    DB.MOE_ENROLLMENT_EXCEPTION_AUDIT.Add(exception_model_audit);
                    DB.SaveChanges();
                    ReturnValue.InsertedID = exception_model_to_update.ID;
                    ReturnValue.EnglishResult = PSPESConstants.InsertionDone;

                    exception_model_to_update.EXCEPTION_AUDIT_ID = exception_model_audit.ID;
                    DB.SaveChanges();

                }
            }

            else
            {

                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    MOE_ENROLLMENT_EXCEPTION exception_model_to_insert = new MOE_ENROLLMENT_EXCEPTION();

                    exception_model_to_insert.National_ID = student_exception.National_ID;
                    exception_model_to_insert.MOE_SCHOOL_CODE = student_exception.MOE_SCHOOL_CODE;
                    exception_model_to_insert.MOE_GRADE = student_exception.MOE_GRADE;
                    exception_model_to_insert.MOE_EXCEPTION_EXPIRY = student_exception.MOE_EXCEPTION_EXPIRY;
                    exception_model_to_insert.MOE_TERM = student_exception.MOE_TERM;
                    exception_model_to_insert.AGE_EXCEPTION = student_exception.AGE_EXCEPTION;
                    exception_model_to_insert.ENROLLMENT_WHILE_CLOSED_EXCEPTION = student_exception.ENROLLMENT_WHILE_CLOSED_EXCEPTION;
                    exception_model_to_insert.GENDER_EXCEPTION = student_exception.GENDER_EXCEPTION;
                    exception_model_to_insert.NATIONALITY_EXCEPTION = student_exception.NATIONALITY_EXCEPTION;
                    exception_model_to_insert.REPEAT_YEAR_EXCEPTION = student_exception.REPEAT_YEAR_EXCEPTION;
                    DB.MOE_ENROLLMENT_EXCEPTION.Add(exception_model_to_insert);
                    DB.SaveChanges();

                    MOE_ENROLLMENT_EXCEPTION_AUDIT exception_model_audit = new MOE_ENROLLMENT_EXCEPTION_AUDIT();

                    exception_model_audit.National_ID = student_exception.National_ID;
                    exception_model_audit.MOE_SCHOOL_CODE = student_exception.MOE_SCHOOL_CODE;
                    exception_model_audit.MOE_GRADE = student_exception.MOE_GRADE;
                    exception_model_audit.MOE_EXCEPTION_EXPIRY = student_exception.MOE_EXCEPTION_EXPIRY;
                    exception_model_audit.MOE_TERM = student_exception.MOE_TERM;
                    exception_model_audit.AGE_EXCEPTION = student_exception.AGE_EXCEPTION;
                    exception_model_audit.ENROLLMENT_WHILE_CLOSED_EXCEPTION = student_exception.ENROLLMENT_WHILE_CLOSED_EXCEPTION;
                    exception_model_audit.GENDER_EXCEPTION = student_exception.GENDER_EXCEPTION;
                    exception_model_audit.NATIONALITY_EXCEPTION = student_exception.NATIONALITY_EXCEPTION;
                    exception_model_audit.REPEAT_YEAR_EXCEPTION = student_exception.REPEAT_YEAR_EXCEPTION;
                    exception_model_audit.MOE_UPDATED_BY = student_exception.MOE_User_Name;
                    exception_model_audit.ENROLLMENT_EXCEPTION_ID = exception_model_to_insert.ID;
                    exception_model_audit.MOE_UPDATED_ON = DateTime.Now;
                    DB.MOE_ENROLLMENT_EXCEPTION_AUDIT.Add(exception_model_audit);
                    DB.SaveChanges();
                    ReturnValue.InsertedID = exception_model_to_insert.ID;
                    ReturnValue.EnglishResult = PSPESConstants.InsertionDone;


                    exception_model_to_insert.EXCEPTION_AUDIT_ID = exception_model_audit.ID;
                    DB.SaveChanges();

                }

            }
                    return ReturnValue;
        }

        private static MOE_ENROLLMENT_EXCEPTION CheckExist(decimal national_ID, string SCHOOL_CODE, string GRADE)
        {
            MOE_ENROLLMENT_EXCEPTION existed_student_exception = new MOE_ENROLLMENT_EXCEPTION();
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                existed_student_exception = DB.MOE_ENROLLMENT_EXCEPTION.Where(D => D.National_ID == national_ID && D.MOE_SCHOOL_CODE == SCHOOL_CODE && D.MOE_GRADE == GRADE).Select(D => D).FirstOrDefault();
            }
            return existed_student_exception;
        }
    }
}