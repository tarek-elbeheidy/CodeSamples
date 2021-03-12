using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.Entities
{
    public class Decisions
    {
        public string DecisionDate { get; set; }
        public string RequestID { get; set; }
        public string DecisionMakerName { get; set; }
        public string DecisionComments { get; set; }
        public string Decision { get; set; }
        public string RejectionReason { get; set; }

    }
}
