using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    [Serializable]
    public class MOE_SCHOOL_Model
    {
        public string MOE_SCHOOL_CODE { get; set; }
        public string MOE_SCHOOL_NAME_ENG { get; set; }
        public string MOE_SCHOOL_NAME_ARA { get; set; }
        public string MOE_SCHOOL_CURRICULUM { get; set; }
        public string MOE_SCHOOL_CURRICULUM_ID { get; set; }
        public int MOE_SCHOOL_CURRENT_CAPACITY { get; set; }
        public int MOE_SCHOOL_CURRENT_ENROLLMENTS { get; set; }
        public System.DateTime MOE_EFFECTIVE_DATE { get; set; }
        public string MOE_EFF_STATUS { get; set; }
        public string MOE_SCHOOL_LOCATION { get; set; }
        public string MOE_SCHOOL_ADDRESS1 { get; set; }
        public string MOE_SCHOOL_ADDRESS2 { get; set; }
        public string MOE_CITY { get; set; }
        public string MOE_SCHOOL_STAGES { get; set; }
        public string MOE_SCHOOL_GENDER { get; set; }
        public decimal MOE_SCHOOL_OWN_NID { get; set; }
        public string MOE_CONTACT_NBR { get; set; }
        public string MOE_EMAIL { get; set; }
        public string MOE_PRN_CONTACT_NBR { get; set; }
        public string MOE_PRN_EMAIL { get; set; }
        public string MOE_SCHOOL_WEBSITE { get; set; }
        public System.DateTime MOE_TRANSACTION_DTTM { get; set; }
        public string MOE_USERID { get; set; }

        //Added for close data and share with NSIS modified by Veer on 16 July 2018
        public Nullable<System.DateTime> MOE_CLOSE_DATE { get; set; }

        public List<schoolGrade> schoolGrades = new List<schoolGrade>();
        
      
        
    }
    [Serializable]
    public class schoolGrade
    {
        public string Grade { get; set; }
        public int CurrentEnrollments { get; set; }
        public int CurrentCapacity { get; set; }

    }

}