using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
    public partial class TransactionsLog
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
