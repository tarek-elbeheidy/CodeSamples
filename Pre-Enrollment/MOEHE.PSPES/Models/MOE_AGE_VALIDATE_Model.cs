using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
    public class MOE_AGE_VALIDATE_Model
    {
        public int ID { get; set; }
        public string MOE_TERM { get; set; }
        public string MOE_SCHOOL_CODE { get; set; }
        public string MOE_SCHOOL_TYPE { get; set; }
        public string MOE_CURRICULUM_ID { get; set; }
        public string MOE_GRADE { get; set; }
        public string MOE_CALCULATE_AGE_ASOF_DT { get; set; }
        public Nullable<int> MOE_MIN_AGE { get; set; }
        public Nullable<int> MOE_MAX_AGE { get; set; }
        public Nullable<System.DateTime> MOE_TRANSACTION_DTTM { get; set; }
        public string MOE_USERID { get; set; }
        public string MOE_CURRICULUM_DESC { get; set; }
        public string MOE_GRADE_DESC_ENG { get; set; }
        public string MOE_GRADE_DESC_ARA { get; set; }
        public Nullable<int> Weight { get; set; }
        public string MOE_MIN_AGE_STRING { get; set; }
        public string MOE_MAX_AGE_STRING { get; set; }
        public string Comments { get; set; }

    }
}
