using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.SCE.Entities
{
    public class SCEClarificationRequest
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public DateTime RequestClarificationDate { get; set; }
        public string RequestSender { get; set; }
        public string RequiredClarification { get; set; }
        public string ClarificationReply { get; set; }
        public string RequestID { get; set; }
        public string ClarificationReason { get; set; }

        public string AssignedTo { get; set; }
        public DateTime ReplyDate { get; set; }
        public string ApplicantName { get; set; }
    }
}
