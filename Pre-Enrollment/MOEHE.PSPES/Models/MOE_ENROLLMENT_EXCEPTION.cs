using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
    class MOE_ENROLLMENT_EXCEPTION
    {
        public int ID { get; set; }
        public Nullable<decimal> National_ID { get; set; }
        public Nullable<int> MOE_TERM { get; set; }
        public string MOE_SCHOOL_CODE { get; set; }
        public string MOE_GRADE { get; set; }
        public Nullable<System.DateTime> MOE_EXCEPTION_EXPIRY { get; set; }
        public Nullable<bool> AGE_EXCEPTION { get; set; }
        public Nullable<bool> REPEAT_YEAR_EXCEPTION { get; set; }
        public Nullable<bool> NATIONALITY_EXCEPTION { get; set; }
        public Nullable<bool> CURRICULUM_EXCEPTION { get; set; }
        public Nullable<bool> GENDER_EXCEPTION { get; set; }
        public Nullable<bool> ENROLLMENT_WHILE_CLOSED_EXCEPTION { get; set; }
        public Nullable<System.DateTime> MOE_CREATED_DATE { get; set; }
        public Nullable<int> EXCEPTION_AUDIT_ID { get; set; }

    }
}
