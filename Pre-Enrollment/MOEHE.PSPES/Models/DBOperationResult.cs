using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
    public class DBOperationResult
    {
        public string EnglishResult { set; get; }
        public string ArabicResult { set; get; }
        public int InsertedID { set; get; }
        public long InsertedLongID { set; get; }

        public string StatusCode { set; get; }
    }
}
