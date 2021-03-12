using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    public class V_Schools_Grades_Ages_Model
    {
        public long RN { get; set; }
        public Nullable<int> MOE_TERM { get; set; }
        public string MOE_SCHOOL_CODE { get; set; }
        public string MOE_SCHOOL_GRADE { get; set; }
        public Nullable<int> MOE_SEAT_DISTRIBUTION { get; set; }
        public Nullable<System.DateTime> MOE_TRANSACTION_DTMM { get; set; }
        public Nullable<int> MOE_PREENROL_SEATS { get; set; }
        public string MOE_USERID { get; set; }
        public Nullable<int> MOE_CURRENT_ENROLLED { get; set; }
        public string MOE_CURRICULUM_ID { get; set; }
        public string MOE_CURRICULUM_DESC { get; set; }
        public string MOE_GRADE_DESC_ARA { get; set; }
        public string MOE_GRADE_DESC_ENG { get; set; }
        public Nullable<int> MOE_MIN_AGE { get; set; }
        public string MOE_MIN_AGE_STRING { get; set; }
        public Nullable<int> MOE_MAX_AGE { get; set; }
        public string MOE_MAX_AGE_STRING { get; set; }
        public string MOE_CALCULATE_AGE_ASOF_DT { get; set; }
        public Nullable<int> Weight { get; set; }
        public string GradeValue { get; set; }
    }
}