using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    public class Enrollment_Exception_Attachment_Model
    {
        public int ID { get; set; }
        public int Enrollment_Exception_ID { get; set; }
        public int Enrollment_Exception_Type_ID { get; set; }
        public string DOCUMENT_LOCATION { get; set; }

       public string ExceptionTypeName { get; set; }
       public bool ExceptionTypeRequired { get; set; }
       public int ExceptionTypeID { get; set; }
    }
}