using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.Entities
{
   public  class ClarificationReqs
    {
        public string ID { get; set; }
        public string RequestID { get; set; }
        public string RequestSender { get; set; }
        public string AssignedTo { get; set; }
        public string RequestedClarification { get; set; }
        public string ClarificationReply { get; set; }
        public DateTime ReplyDate { get; set; }
        public DateTime RequestClarificationDate { get; set; }
        public string ApplicantName { get; set; }
        public string RequestNumber { get; set; }
        public string QatariID { get; set; }
        public string Nationality { get; set; }
        public string AcademicDegree { get; set; }
        public string EntityNeedsEquivalency { get; set; }
        public string Country { get; set; }
        public string University { get; set; }
        public string Faculty { get; set; }

    }
}
