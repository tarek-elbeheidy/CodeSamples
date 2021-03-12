using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
    class InterviewResultModel
    {
        public int ID { get; set; }
        public string NATIONAL_ID { get; set; }
        public string MOE_APPL_NBR { get; set; }
        public Nullable<System.DateTime> DTTM { get; set; }
        public string USERID { get; set; }
        public string InterviewResult1 { get; set; }

    }
}
