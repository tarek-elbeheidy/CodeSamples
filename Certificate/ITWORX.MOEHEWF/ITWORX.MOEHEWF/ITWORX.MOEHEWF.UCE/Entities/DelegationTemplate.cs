using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.Entities
{
   
    public  class DelegationTemplate
    {
        public int ID { get; set; }
        public string EnglishTitle { get; set; }
        public string ArabicTitle { get; set; }
        public SPFile DelegationTemplatesDocuments { get; set; }
        public string FileName { get; set; }
        public string FileTitle { get; set; }
        public string Url { get; set; }
        public string FileExtension { get; set; }
        
      
    }
}
