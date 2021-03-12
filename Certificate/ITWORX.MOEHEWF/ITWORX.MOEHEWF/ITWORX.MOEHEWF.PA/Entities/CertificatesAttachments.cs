using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.PA.Entities
{
   public class CertificatesAttachments
    {
        public int ID { get; set; }
        public Entities.PARequest Request { get; set; }
        public Certificates Certificates { get; set; }
        public SPFile DelegationDocument { get; set; }
        public string FileName { get; set; }
        public string Group { get; set; }
        public AttachmentStatus AttachmentStatus { set; get; }
    }
}
