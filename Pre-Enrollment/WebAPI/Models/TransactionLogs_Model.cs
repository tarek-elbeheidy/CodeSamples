using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    public class TransactionLogs_Model
    {
        public int ID { get; set; }
        public Nullable<int> ActionCode { get; set; }
        public string TableName { get; set; }
        public string UserID { get; set; }
        public Nullable<long> ItemID { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}