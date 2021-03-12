using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    public class MOE_PREENROLLMENT_DATA_VIEW_Model
    {
        public long ID { get; set; }
        public Nullable<decimal> STUDENT_QID { get; set; }
        public string STUDENT_NAME_EN { get; set; }
        public string STUDENT_NAME_AR { get; set; }
        public Nullable<System.DateTime> APPLICATION_DATE { get; set; }
        public string MOE_SCHOOL_CODE { get; set; }
        public string MOE_APPLIED_GRADE { get; set; }
        public Nullable<bool> MOE_AVAIL_TRANSPORT { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsApplicationFinalized { get; set; }
        public Nullable<System.DateTime> MOE_TRANSACTION_DTTM { get; set; }
        public Nullable<int> CURRENT_YEAR_TERM { get; set; }
        public string CURRENT_YEAR_SCHOOL_CODE { get; set; }
        public string CURRENT_YEAR_GRADE { get; set; }
        public string CURRENT_YEAR_RESULT { get; set; }
        public Nullable<int> PREVIOUS_YEAR_TERM { get; set; }
        public string PREVIOUS_YEAR_SCHOOL_CODE { get; set; }
        public string PREVIOUS_YEAR_GRADE { get; set; }
        public string PREVIOUS_YEAR_RESULT { get; set; }
        public Nullable<decimal> MOE_RELATED_QID { get; set; }
        public string GUARDIAN_NAME_EN { get; set; }
        public string GUARDIAN_NAME_AR { get; set; }
        public string GUARDIAN_RELATION_SHIP_ID { get; set; }
        public string GUARDIAN_EMPLOYER_TYPE_ID { get; set; }
        public string GUARDIAN_EMPLOYER_ID { get; set; }
        public string GUARDIAN_MOBILE_NUMBER { get; set; }
        public string GUARDIAN_HOME_PHONE_NUMBER { get; set; }
        public string GUARDIAN_WORK_PHONE_NUMBER { get; set; }
        public string GUARDIAN_EMAIL { get; set; }
        public string GUARDIAN_FAX_NUMBER { get; set; }
        public string HEALTH_CARD_NUMBER { get; set; }
        public string HEALTH_CENTER_NAME { get; set; }
        public Nullable<bool> DOES_STUDENT_HAVE_HEALTH_PROBLEMS_ { get; set; }
        public string HEALTH_PROBLEMS_DETAILS { get; set; }
        public Nullable<bool> DOES_STUDENT_HAVE_LEARNING_DIFFICULTIES_ { get; set; }
        public Nullable<bool> DOES_STUDENT_HAVE_SPECIAL_NEEDS_ { get; set; }
    }
}