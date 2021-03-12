using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
    class MOE_Enrollment_Exception_Types
    {
        public int ID { get; set; }
        public string ExceptionTypeName { get; set; }
        public string ExceptionTypeNameAR { get; set; }
        public bool AttachmentRequired { get; set; }
    }
}
