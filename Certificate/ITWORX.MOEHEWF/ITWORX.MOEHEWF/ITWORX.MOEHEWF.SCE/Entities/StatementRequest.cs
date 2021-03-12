using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.SCE.Entities
{
    public class StatementRequest
    {
        public int ID { get; set; } 

        public int RequestID { get; set; }
         
        public string StatementDate { get; set; }

        public string Sender { get; set; }

        public string StatementSubject { get; set; }

        public string RequiredStatement { get; set; }

        public string StatementAgency { get; set; } 

        public string ReplayDate { get; set; }

        public string StatementReplay { get; set; }

        public string ReplaySender { get; set; }

    }
}
