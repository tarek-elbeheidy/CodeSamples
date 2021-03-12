using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.PA.Entities
{
   public class RequestsForStatement
    {
        public string DirectedTo { get; set; }
        public string ID { get; set; }
        public string StatementCreatedby { get; set; }
        public string StatementDate { get; set; }
        public string RequestID { get; set; }
        public string StatementReplyDate { get; set; }
        public string StatementReplyby { get; set; }
        public string StatementRequested { get; set; }
        public string StatementSubject { get; set; }
        public string StatementReply { get; set; }
        
    }
}
