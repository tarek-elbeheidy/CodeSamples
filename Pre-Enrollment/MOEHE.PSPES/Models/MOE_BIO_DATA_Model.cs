using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
    [Serializable]
    public class MOE_BIO_DATA_Model
    {
        public decimal NATIONAL_ID { get; set; }
        public string MOE_ENGLISH_NAME { get; set; }
        public string MOE_ARABIC_NAME { get; set; }
        public string MOE_DOB { get; set; }
        public string MOE_GENDER { get; set; }
        public string MOE_COUNTRY_ENGLISH_NAME { get; set; }
        public string MOE_COUNTRY_ARABIC_NAME { get; set; }
        public int MOE_COUNTRY_CODE { get; set; }
        public string MOE_STATUS { get; set; }
        public System.DateTime MOE_STATUS_DATE { get; set; }
        public System.DateTime MOE_LAST_UPDATED_DATE { get; set; }
        public System.DateTime MOE_TRANSACTION_DTTM { get; set; }
        public string MOE_USERID { get; set; }
        public Nullable<bool> IsActive { get; set; }

    }
}
