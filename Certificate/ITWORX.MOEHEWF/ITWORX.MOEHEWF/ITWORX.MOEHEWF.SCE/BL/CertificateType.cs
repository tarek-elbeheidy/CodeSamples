using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.SCE.BL
{
    class CertificateType
    {


        public static List<CertificateTypeListFieldsContentType> GetAll()
        {
            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
            { 
                return ctx.CertificateTypeList.ToList();
            }
        }

        public static List<CertificateTypeListFieldsContentType> GetEquivalenceCertificateType()
        {
            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
            {
                return ctx.CertificateTypeList.Where(x => x.SearchCritaria.Id == 1 && x.Status == Status.Active).ToList();
            }
        }
    }
}
