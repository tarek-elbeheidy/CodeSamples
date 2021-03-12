using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
    class EnrollmentControllingModel
    {

        public int ID { get; set; }
        public string SchoolCode { get; set; }
        public string CountryCodes { get; set; }
        public string GradeCodes { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
    }
}
