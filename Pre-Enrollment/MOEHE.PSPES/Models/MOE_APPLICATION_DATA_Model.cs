using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
    class MOE_APPLICATION_DATA_Model
    {
        public Nullable<decimal> NATIONAL_ID { get; set; }
        public int? MOE_APPL_NBR { get; set; }
        public System.DateTime? MOE_APPL_DATE { get; set; }
        public string MOE_APPL_REF_NBR { get; set; }
        public int? MOE_APPL_INCR_ID { get; set; }
        public int? MOE_APPL_YEAR { get; set; }
        public string MOE_SCHOOL_CODE { get; set; }
        public int? MOE_TERM { get; set; }
        public string MOE_APPLIED_GRADE { get; set; }
        public string MOE_RESIDENTIAL_AREA { get; set; }
        public bool? MOE_AVAIL_TRANSPORT { get; set; }
        public int? MOE_WAITLIST_NUMBER { get; set; }
        public System.DateTime MOE_TRANSACTION_DTTM { get; set; }
        public string MOE_USERID { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public Nullable<bool> DeactiveOtherApplications { get; set; }
        public string MOE_STUDENT_NAME_ENG { get; set; }
        public string MOE_STUDENT_NAME_ARA { get; set; }


    }
}
