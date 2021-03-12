using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
    class MOE_APPL_ACAD_INFO_Model
    {
        public int? MOE_ACAD_INFO_ID { get; set; }
        public Nullable<decimal> NATIONAL_ID { get; set; }
        public string MOE_APPL_REF_NBR { get; set; }
        public int? MOE_TERM { get; set; }
        public string MOE_SCHOOL_CODE { get; set; }
        public string MOE_GRADE { get; set; }
        public string MOE_STU_RSLT { get; set; }
        public System.DateTime MOE_TRANSACTION_DTTM { get; set; }
        public string MOE_USERID { get; set; }
        public string MOE_SCHOOL_NAME { get; set; }
        public string MOE_SCHOOL_ARABIC_NAME { get; set; }
        public string MOE_SCHOOL_CURRICULUM { get; set; }
        public string MOE_SCHOOL_ARABIC_CURRICULUM { get; set; }
        public string MOE_SCHOOL_CURRICULUM_ID { get; set; }

        public virtual MOE_BIO_DATA MOE_BIO_DATA { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> DeactiveOtherApplications { get; set; }
        public bool HasPendingPayment { get; set; }


        //public virtual MOE_BIO_DATA MOE_BIO_DATA { get; set; }
    }
    public partial class MOE_BIO_DATA
    {
        public int ID { get; set; }
        public Nullable<decimal> NATIONAL_ID { get; set; }
        public string MOE_ENGLISH_NAME { get; set; }
        public string MOE_ARABIC_NAME { get; set; }
        public Nullable<System.DateTime> MOE_DOB { get; set; }
        public string MOE_GENDER { get; set; }
        public string MOE_COUNTRY_ENGLISH_NAME { get; set; }
        public string MOE_COUNTRY_ARABIC_NAME { get; set; }
        public Nullable<int> MOE_COUNTRY_CODE { get; set; }
        public string MOE_STATUS { get; set; }
        public Nullable<System.DateTime> MOE_STATUS_DATE { get; set; }
        public Nullable<System.DateTime> MOE_LAST_UPDATED_DATE { get; set; }
        public Nullable<System.DateTime> MOE_TRANSACTION_DTTM { get; set; }
        public string MOE_USERID { get; set; }
    }
}
