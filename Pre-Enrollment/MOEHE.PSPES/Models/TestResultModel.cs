using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.Models
{
    public class TestResultModel
    {
        public int ID { get; set; }
        public string NATIONAL_ID { get; set; }
        public string MOE_APPL_NBR { get; set; }
        public string ResultDocLocation { get; set; }
        public System.DateTime DTTM { get; set; }
        public string USERID { get; set; }
        public string TestResult1 { get; set; }
        public Nullable<bool> IsActive { get; set; }


    }
}