using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
    public class MessageBulkModel
    {
        public int ID { get; set; }
        public int DistributionListID { get; set; }
        public int Message_ID { get; set; }
        public long UserID { get; set; }
        public int BulkSetID { get; set; }

        public DateTime CreateDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsBulkSet { get; set; }

    }
    public class MessageDetailsModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long UserID { get; set; } //QID
        public string TextBody { get; set; }
        public string SenderCode { get; set; }
        public string MobileNumber { get; set; }
        public string ContactID { get; set; }
        public int PriorityID { get; set; }
        public string MessageID { get; set; }
        public bool IsBulk { get; set; }
        public bool IsBulkSet { get; set; }
        public string StatusCode { set; get; }
        public int BulkID { set; get; }
        public string DistributionListID { set; get; }
        public bool IsReceived { get; set; }
        public int ContactSourceID { get; set; }

        public int TotalMessageCount { get; set; }
        public int ProcessedMessageCount { get; set; }
    }
}
