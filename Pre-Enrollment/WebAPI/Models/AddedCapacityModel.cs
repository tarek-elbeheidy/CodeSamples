using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    public class AddedCapacityModel
    {
        public int ID { get; set; }
        public int MOE_SCHOOL_YEAR { get; set; }
        public string MOE_SCHOOL_CODE { get; set; }
        public int MOE_ADDED_CAPACITY1 { get; set; }
    }
}