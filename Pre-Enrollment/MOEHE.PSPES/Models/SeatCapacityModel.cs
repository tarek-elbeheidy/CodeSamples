using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.Models
{
    public class SeatCapacityModel
    {
        public int ID { get; set; }
        public int MOE_TERM { get; set; }
        public string MOE_SCHOOL_CODE { get; set; }
        public string MOE_SCHOOL_GRADE { get; set; }
        public int MOE_SEAT_DISTRIBUTION { get; set; }
        public System.DateTime MOE_TRANSACTION_DTMM { get; set; }
        public int MOE_PREENROL_SEATS { get; set; }
        public string MOE_USERID { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public int MOE_CURRENT_ENROLLED { get; set; }

    }
}
