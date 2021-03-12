using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.PA.Entities
{
   
    public class DelegationDocuments
    {
        public int ID { get; set; }
        public Entities.PARequest Request { get; set; }
        public DelegationTemplate DelegationTemplate { get; set; }
        public SPFile DelegationDocument { get; set; }
        public string FileName { get; set; }
        public string DelegationTempFileName { get; set; }
        public string Group { get; set; }
        public AttachmentStatus AttachmentStatus { set; get; }
    }
}
