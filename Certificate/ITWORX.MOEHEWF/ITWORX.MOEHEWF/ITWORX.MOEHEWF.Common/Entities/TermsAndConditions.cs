
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.Entities
{
   public class TermsAndConditions
    {
        public string Description { set; get; }
        public RequestTypes RequestType { set; get; }
        public List<TermsAttachments> TermsAttachmentsList { set; get; }
    }

    public class TermsAttachments
    {
        public string FileName { set; get; }
        public string FileURL { set; get; }
    }
}
