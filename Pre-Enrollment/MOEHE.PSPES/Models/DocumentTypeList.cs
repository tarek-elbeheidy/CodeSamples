using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
    public class DocumentTypeList
    {
        public int DocumentTypeID { get; set; }
        public string DocumentTypeName { get; set; }
        public string ArabicDocumentTypeName { get; set; }
        public Nullable<bool> IsActive { get; set; }


    }
}
