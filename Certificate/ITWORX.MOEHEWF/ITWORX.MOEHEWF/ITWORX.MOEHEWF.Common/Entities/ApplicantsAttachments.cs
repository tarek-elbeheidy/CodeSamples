using ITWORX.MOEHEWF.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.Entities
{
    
    public class ApplicantsAttachments
    {
        public int ID { get; set; }
        public Applicants Applicants { get; set; }
        public string Group { get; set; }
        public AttachmentStatus AttachmentStatus { set; get; }
        public Request Request { get; set; }
    }
}
