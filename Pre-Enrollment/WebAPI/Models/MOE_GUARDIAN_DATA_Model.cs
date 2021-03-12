using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    [Serializable]
    public class MOE_GUARDIAN_DATA_Model
    {
        public Nullable<decimal> NATIONAL_ID { get; set; }
        public string MOE_APPL_NBR { get; set; }
        public decimal MOE_RELATED_QID { get; set; }
        public string MOE_RELATIONSHIP_TYPE_ID { get; set; }
        public bool MOE_ISGUARDIAN { get; set; }
        public string MOE_MARITAL_STATUS_ID { get; set; }
        public string MOE_HOME_CONTACT_NBR { get; set; }
        public string MOE_WORK_CONTACT_NBR { get; set; }
        public string MOE_MOBILE_CONTACT_NBR { get; set; }
        public string MOE_FAX_CONTACT_NBR { get; set; }
        public string MOE_EMAIL { get; set; }
        public string MOE_EMPLOYER_TYPE_ID { get; set; }
        public string MOE_EMPLOYER { get; set; }
        public System.DateTime MOE_TRANSACTION_DTTM { get; set; }
        public string MOE_USERID { get; set; }
        public string MOE_ENGLISH_NAME { get; set; }
        public string MOE_ARABIC_NAME { get; set; }
        public System.DateTime MOE_DOB { get; set; }
        public string MOE_GENDER { get; set; }
        public string MOE_COUNTRY_ENGLISH_NAME { get; set; }
        public string MOE_COUNTRY_ARABIC_NAME { get; set; }
        public decimal MOE_OLD_RELATED_QID { get; set; }
        public string MOE_GUARDIAN_NAME_ENG { get; set; }
        public string MOE_GUARDIAN_NAME_ARA { get; set; }

        public MOE_BIO_DATA_Model bio_data = new MOE_BIO_DATA_Model();

    }
}
