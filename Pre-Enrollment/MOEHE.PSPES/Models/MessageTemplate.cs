using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Models
{
    public partial class MessageTemplate
    {
        public int ID { get; set; }
        public string TemplateTitle { get; set; }
        public string ArabicMessage { get; set; }
        public string EnglishMessage { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}
