using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.Entities
{
   public class TrackingRequest
    {
        public RequestTypes RequestType { get; set; }
        public RequestPhase RequestPhase { get; set; }
        public int RequestNumber { get; set; }
        public DateTime SubmitDate { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string AttachmentURL { get; set; }
        public string FileName { get; set; }
        public AcademicDegree AcademicDegree { get; set; }
    }
}
