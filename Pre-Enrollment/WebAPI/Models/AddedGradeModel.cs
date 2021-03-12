using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    public class AddedGradeModel
    {
        public int ID { get; set; }
        public Nullable<int> MOE_SCHOOL_YEAR { get; set; }
        public string MOE_SCHOOL_CODE { get; set; }
        public Nullable<int> ADDED_NUMBER_OF_GRADES { get; set; }
        public string USERID { get; set; }
        public Nullable<System.DateTime> DTTM { get; set; }
    }
}