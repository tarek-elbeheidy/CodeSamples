using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
   public class SearchApplicationPageFilters
    {
        public bool SearchBySchoolCode { get; set; }
        public bool SeacrhByGrade { get; set; }
        public bool SeacrhByTerm { get; set; }
        public bool SeacrhByApplicationReference { get; set; }
        public bool SeacrhByStudentNationID { get; set; }
        public bool SeacrhByAppliedDate { get; set; }
        public bool SeacrhByApplicationStatus { get; set; }

        public bool IsCompletedApplication { get; set; }


        public string SchoolCode { get; set; }
        public string Grade { get; set; }
        public string Term { get; set; }
        public string ApplicationReference { get; set; }
        public string StudentNationID { get; set; }
        public DateTime AppliedDate { get; set; }
        public bool IsEnglish { get; set; }



    }
}
