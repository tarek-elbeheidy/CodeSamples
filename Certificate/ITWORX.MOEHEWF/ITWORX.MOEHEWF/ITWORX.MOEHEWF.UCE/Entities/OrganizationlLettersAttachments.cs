using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.Entities
{
  
    public class OrganizationlLettersAttachments
    {
        public int ID { get; set; }
        public Request Request { get; set; }
        public SPFile OrganizationDocuments { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LoginName { get; set; }
        public string Group { get; set; }
        public AttachmentStatus AttachmentStatus { set; get; }


    }
}
