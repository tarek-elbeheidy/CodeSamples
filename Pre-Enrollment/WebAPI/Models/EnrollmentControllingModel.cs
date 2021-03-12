using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    public class EnrollmentControllingModel
    {
        public int ID { get; set; }
        public string SchoolCode { get; set; }
        public string CountryCodes { get; set; }
        public string GradeCodes { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
    }
}