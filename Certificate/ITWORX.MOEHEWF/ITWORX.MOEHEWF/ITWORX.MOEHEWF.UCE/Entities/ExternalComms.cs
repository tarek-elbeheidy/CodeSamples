using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.Entities
{
    public class ExternalComms
    {
        public string ID { get; set; }
        public string RequestID { get; set; }
        public string OrgReplyAddress { get; set; }
        public string OrgReplyBookSubject { get; set; }
        public string OrgReply { get; set; }
        public string OrgReplyBookNo { get; set; }
        public string OrgReplyDate { get; set; }

        public string BookText { get; set; }
        public string BookAuthor { get; set; }
        public string BookDirectedTo { get; set; }
        public string BookID { get; set; }
        public string BookDate { get; set; }
        public string BookSubject { get; set; }
        public string OrgEmail { get; set; }
        public string OrgTitleEN { get; set; }
        public string OrgTitleAr { get; set; }
        public string TemplateEN { get; set; }
        public string TemplateAr { get; set; }
    }
}
