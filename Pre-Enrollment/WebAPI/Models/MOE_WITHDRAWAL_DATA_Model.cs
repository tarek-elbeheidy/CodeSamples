using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    public class MOE_WITHDRAWAL_DATA_Model
    {
        public int ID { get; set; }
        public string MOE_SCHOOL_CODE { get; set; }
        public string MOE_NATIONAL_ID { get; set; }
        public string MOE_REFERENCE_ID { get; set; }
        public Nullable<System.DateTime> MOE_WITHDRAWAL_REQUESTDATE { get; set; }
        public Nullable<System.DateTime> MOE_WITHDRAWAL_DATE { get; set; }
        public Nullable<int> MOE_WITHDRAWAL_REASONID { get; set; }
        public string MOE_ATTACHMENT { get; set; }
        public Nullable<System.DateTime> MOE_ACTION_DATE { get; set; }
        public string MOE_LOGIN_ID { get; set; }
        public string MOE_COMMENTS { get; set; }
        public int MOE_TERM { get; set; }

        public bool IsActive { get; set; }
        public bool IsFinalized { get; set; }
    }
}