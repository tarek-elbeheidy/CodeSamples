using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    public class MOE_ENROLLMENT_EXCEPTION_AUDIT_Model
    {
        public int ID { get; set; }
        public int ENROLLMENT_EXCEPTION_ID { get; set; }
        public decimal National_ID { get; set; }
        public int MOE_TERM { get; set; }
        public string MOE_SCHOOL_CODE { get; set; }
        public string MOE_GRADE { get; set; }
        public DateTime MOE_EXCEPTION_EXPIRY { get; set; }
        public bool AGE_EXCEPTION { get; set; }
        public bool REPEAT_YEAR_EXCEPTION { get; set; }
        public bool NATIONALITY_EXCEPTION { get; set; }
        public bool CURRICULUM_EXCEPTION { get; set; }
        public bool GENDER_EXCEPTION { get; set; }
        public bool ENROLLMENT_WHILE_CLOSED_EXCEPTION { get; set; }
        public string MOE_UPDATED_BY { get; set; }
        public DateTime MOE_UPDATED_ON { get; set; }

    }
}