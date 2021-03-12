using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.WebAPI.Models
{
   public class ApplicationDataModel
    {
        public string Term { get; set; }
        public string ApplicationRefNo { get; set; }
        public string ApplicationDate { get; set; }
        public string QID { get; set; }
        public string StudentName { get; set; }
        public string Grade { get; set; }
        public string SchoolName { get; set; }
        public bool IsEnglish { get; set; }
        public string SearchText { get; set; }
        public string EnglishStudentName { get; set; }
        public string ArabicStudentName { get; set; }
        public string ArabicSchoolName { get; set; }
        public string EnglishSchoolName { get; set; }
        public bool IsCompletedApplication { get; set; }
        public int? MOE_WAITLIST_NUMBER { get; set; }
        public string FinalizeButtonText { get; set; }
        public string MOE_STUDENT_NAME_ENG { get; set; }
        public string MOE_STUDENT_NAME_ARA { get; set; }


        bool IsMinistryUser { set; get; }


    }

    public class SearchApplicationPageFilters
    {
        public bool SearchBySchoolCode { get; set; }
        public bool SeacrhByGrade { get; set; }
        public bool SeacrhByTerm { get; set; }
        public bool SeacrhByApplicationReference { get; set; }
        public bool SeacrhByStudentNationID { get; set; }
        public bool SeacrhByAppliedDate { get; set; }
        public bool IsCompletedApplication { get; set; }
        public bool SeacrhByApplicationStatus { get; set; }

        public bool IsEnglish { get; set; }

        public string SchoolCode { get; set; }
        public string Grade { get; set; }
        public string Term { get; set; }
        public string ApplicationReference { get; set; }
        public string StudentNationID { get; set; }
        public DateTime AppliedDate { get; set; }


    }
}
