using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    public class MOE_Enrollment_Exception_Types
    {
        public int ID { get; set; }
        public string ExceptionTypeName { get; set; }
        public string ExceptionTypeNameAR { get; set; }
        public bool AttachmentRequired { get; set; }
    }
}