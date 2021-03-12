using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.SCE.Utilities
{
    public static class BusinessHelper
    {
        public static string GetLocalizedString(string resource, uint lcid)
        {
          return  SPUtility.GetLocalizedString(resource, Constants.SCEResourcesFileName, lcid);
        }

        
    }
}
