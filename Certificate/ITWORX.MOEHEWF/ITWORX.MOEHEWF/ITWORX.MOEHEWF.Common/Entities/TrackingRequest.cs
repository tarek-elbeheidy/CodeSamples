using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.Entities
{
    public class TrackingRequest
    {
        public int RequestId { get; set; }
        public RequestTypes RequestType { get; set; }
      //  public RequestPhase RequestPhase { get; set; }
        public string RequestNumber { get; set; }
        public DateTime SubmitDate { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string AttachmentURL { get; set; }
        public string FileName { get; set; }
        public AcademicDegree AcademicDegree { get; set; }
        public string LoginName { get; set; }
        public string AcadDegree { get; set; }
        public string  Code { get; set; }
        public string RequestTypeEnumVaue { get; set; }

        public string CurrentGrade { get; set; } 
        
    }
}
