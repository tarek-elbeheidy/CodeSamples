using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
    class MOE_APPL_SUPPORT_DOCS_Model
    {
    
        public decimal? NATIONAL_ID { get; set; }
        public string MOE_APPL_NBR { get; set; }
        public string MOE_DOCUMENT_TYPE_ID { get; set; }
        public string MOE_DOCUMENT_LOCATION { get; set; }
        public System.DateTime? MOE_TRANSACTION_DTTM { get; set; }
        public string MOE_USERID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> DeactiveOtherApplications { get; set; }

    }
}
