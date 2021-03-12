using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITWORX.MOEHEWF.Common.Entities;


namespace ITWORX.MOEHEWF.UCE.Entities
{
  
    public class Faculty
    {
        public int ID { get; set; }
        public string EnglishTitle { get; set; }
        public string ArabicTitle { get; set; }
        public string SelectedID { get; set; }
        public string SelectedTitle { get ; set ; }
        public University University { get; set; }
    }
}
