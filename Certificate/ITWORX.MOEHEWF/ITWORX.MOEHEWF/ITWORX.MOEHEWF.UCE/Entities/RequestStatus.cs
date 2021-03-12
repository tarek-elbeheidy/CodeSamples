using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.Entities
{
   public class RequestStatus
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string EnglishStatus { get; set; }
        public string ArabicStatus { get; set; }
        public string SelectedID { get; set; }
        public string SelectedTitle { get; set; }
    }
}

