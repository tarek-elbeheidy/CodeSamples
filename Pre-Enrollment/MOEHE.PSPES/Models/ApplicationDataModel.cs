using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
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
        bool IsMinistryUser { set; get; }
        public int? MOE_WAITLIST_NUMBER { get; set; }
        public string FinalizeButtonText { get; set; }




    }
}
