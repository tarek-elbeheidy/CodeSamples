using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class MOE_PREENROLLMENT_REPORT_VIEW_Repository
    {
        public static DataTable GetAllData(Dictionary<string, string> dictionary)
        {
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                string Query = dictionary["query"];
                string Conditions = dictionary["condition"];
                DataTable result = new DataTable();
                string tableName = "MOE_PREENROLLMENT_REPORT_VIEW";
                string queryStr = string.Empty;
                if (!string.IsNullOrEmpty(Conditions))
                {
                    queryStr = string.Format("SELECT {0} FROM {1} WHERE {2} GROUP BY {0} ORDER BY {3}", Query.ToString().Trim(','), tableName, Conditions.ToString().Trim(','), "MOE_SCHOOL_CODE");
                }
                else
                {
                    queryStr = string.Format("SELECT {0} FROM {1} GROUP BY {0} ORDER BY {2}", Query.ToString().Trim(','), tableName, "MOE_SCHOOL_CODE");
                }
                SqlConnection conn = DB.Database.Connection as SqlConnection;
                using (SqlCommand cmd = new SqlCommand(queryStr, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        result.Load(reader);
                    }
                    return result;
                }
            }
        }
        public static List<MOE_PREENROLLMENT_REPORT_VIEW_Model> GetDataByQID(string QID)
        {
            decimal qid = decimal.Parse(QID);
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                return DB.MOE_PREENROLLMENT_REPORT_VIEW.Where(D => D.STUDENT_QID == qid).Select(D => new MOE_PREENROLLMENT_REPORT_VIEW_Model {
                    ID = D.ID,
                    STUDENT_QID = D.STUDENT_QID,
                    STUDENT_NAME_EN = D.STUDENT_NAME_EN,
                    STUDENT_NAME_AR = D.STUDENT_NAME_AR,
                    APPLICATION_DATE = D.APPLICATION_DATE,
                    MOE_SCHOOL_CODE = D.MOE_SCHOOL_CODE,
                    MOE_APPLIED_GRADE = D.MOE_APPLIED_GRADE,
                    MOE_AVAIL_TRANSPORT = D.MOE_AVAIL_TRANSPORT,
                    IsActive = D.IsActive,
                    IsApplicationFinalized = D.IsApplicationFinalized,
                    MOE_TRANSACTION_DTTM = D.MOE_TRANSACTION_DTTM,
                    CURRENT_YEAR_TERM = D.CURRENT_YEAR_TERM,
                    CURRENT_YEAR_SCHOOL_CODE = D.CURRENT_YEAR_SCHOOL_CODE,
                    CURRENT_YEAR_GRADE = D.CURRENT_YEAR_GRADE,
                    CURRENT_YEAR_RESULT = D.CURRENT_YEAR_RESULT,
                    PREVIOUS_YEAR_TERM = D.PREVIOUS_YEAR_TERM,
                    PREVIOUS_YEAR_SCHOOL_CODE = D.PREVIOUS_YEAR_SCHOOL_CODE,
                    PREVIOUS_YEAR_GRADE = D.PREVIOUS_YEAR_GRADE,
                    PREVIOUS_YEAR_RESULT = D.PREVIOUS_YEAR_RESULT,
                    MOE_RELATED_QID = D.MOE_RELATED_QID,
                    GUARDIAN_NAME_EN = D.GUARDIAN_NAME_EN,
                    GUARDIAN_NAME_AR = D.GUARDIAN_NAME_AR,
                    GUARDIAN_EMPLOYER_TYPE_ID = D.GUARDIAN_EMPLOYER_TYPE_ID,
                    GUARDIAN_EMPLOYER_ID = D.GUARDIAN_EMPLOYER_ID,
                    GUARDIAN_MOBILE_NUMBER = D.GUARDIAN_MOBILE_NUMBER,
                    GUARDIAN_HOME_PHONE_NUMBER = D.GUARDIAN_HOME_PHONE_NUMBER,
                    GUARDIAN_WORK_PHONE_NUMBER = D.GUARDIAN_WORK_PHONE_NUMBER,
                    GUARDIAN_EMAIL = D.GUARDIAN_EMAIL,
                    GUARDIAN_FAX_NUMBER = D.GUARDIAN_FAX_NUMBER,
                    HEALTH_CARD_NUMBER = D.HEALTH_CARD_NUMBER,
                    HEALTH_CENTER_NAME = D.HEALTH_CENTER_NAME,
                    HEALTH_PROBLEMS_DETAILS = D.HEALTH_PROBLEMS_DETAILS
                }).ToList();
            }
        }
    }
}