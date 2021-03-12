using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.PA.Entities
{
    public class CalculatedDetailsForCertificateAttachments
    {
        public int ID { get; set; }
        public PARequest Request { get; set; }
        public CalculatedDetailsForCertificate CalculatedDetails { get; set; }
        public SPFile DelegationDocument { get; set; }
        public string FileName { get; set; }
        public string Group { get; set; }
        public AttachmentStatus AttachmentStatus { set; get; }
    }
}
