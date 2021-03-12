using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.SCE.BL
{
    public class ClarificationReasons
    {
        public static List<ClarificationReasonsListFieldsContentType> GetAllActive()
        {
            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
            {
                  return ctx.ClarificationReasons.ScopeToFolder("",true).Where(c=>c.Status == Status.Active).Cast<ClarificationReasonsListFieldsContentType>().ToList();
            }
        }
    }
}
