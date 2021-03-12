using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    public class MOE_HEALTH_DATA_Model
    {
        public int? MOE_HEALTH_DATA_ID { get; set; }
        public Nullable<decimal> NATIONAL_ID { get; set; }
        public string MOE_APPL_NBR { get; set; }
        public string MOE_HLTH_CARD_NBR { get; set; }
        public string MOE_HLTH_CTR_NAME { get; set; }
        public Nullable<bool> MOE_FIT_FOR_SCHOOLING { get; set; }
        public Nullable<bool> MOE_HLTH_PROBLEMS { get; set; }
        public string MOE_HLTH_PROBLEMS_DETAILS { get; set; }
        public Nullable<bool> MOE_SPL_NEEDS { get; set; }
        public Nullable<bool> MOE_LEARNING_DIFFICULTIES { get; set; }
        public string MOE_SPECIAL_NEEDS_DETAILS { get; set; }
        public System.DateTime MOE_TRANSACTION_DTTM { get; set; }
        public string MOE_USERID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> DeactiveOtherApplications { get; set; }




    }
}