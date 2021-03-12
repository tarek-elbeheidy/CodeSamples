using ITWORX.MOEHEWF.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.Entities
{
   public class HETemplates
    {
        public int ID { set; get; }
        public TemplateType Type { set; get; }
        public string FileUrl { set; get; }
        public string FileName { set; get; }
    }
}
