using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    public class MOE_ENROLLMENT_EXCEPTION_Model
    {
        public int ID { get; set; }
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
        public DateTime MOE_CREATED_DATE { get; set; }
        public string MOE_User_Name { get; set; }
    }
}