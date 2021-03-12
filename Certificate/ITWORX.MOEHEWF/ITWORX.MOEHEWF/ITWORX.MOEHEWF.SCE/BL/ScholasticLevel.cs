using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.SCE.BL
{
    class ScholasticLevel
    {

        public static Microsoft.SharePoint.Linq.EntityList<Item> GetAll()
        {
            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
            {
                return ctx.ScholasticLevel;
            }
        }
    }
}
