using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    public class MOE_APPL_ACAD_INFO_Model
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
        public bool HasPendingPayment { get; set; }
        public Nullable<bool> DeactiveOtherApplications { get; set; }

    }
}