using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Models
{
    public class SMSHistoryModel
    {
        public int ID { get; set; }
        public string MsgType { get; set; }
        public string MsgText { get; set; }
        public string MsgTime { get; set; }
        public string MsgStatus { get; set; }
        public string MOE_APPL_REF_NBR { get; set; }
        public string MsgTitle { get; set; }
        public string MsgSender { get; set; }
        public string MobileNumber { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Description { get; set; }

    }
}