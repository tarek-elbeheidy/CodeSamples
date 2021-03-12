using Microsoft.SharePoint; 
using System.Collections.Generic;
using System.Linq; 

namespace ITWORX.MOEHEWF.SCE.BL
{
    public class SchoolType
    {

        public static Microsoft.SharePoint.Linq.EntityList<Item> GetAll()
        { 
            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
            {
                return ctx.SchoolType;
            } 
        }
    }
}
