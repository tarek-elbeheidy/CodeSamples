using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.SCE.Entities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.SCE.BL
{
    public class CertificateResourceList
    {
        public static CertificateResourceItem GetCertificateResourceById(int id)
        {
            CertificateResourceItem certificateResourceItem = new CertificateResourceItem();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        Logging.GetInstance().Debug("Enter CertificateResourceList.GetCertificateResourceById");
                        try
                        {
                            SPList list = web.Lists[Utilities.Constants.CertificateResource];
                            SPListItem item = list != null ? list.GetItemById(id) : null;
                            if (item != null)
                            {
                                certificateResourceItem.Id = item.ID;
                                certificateResourceItem.Title = item.Title;
                                certificateResourceItem.TitleAr = item["TitleAr"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exit CertificateResourceList.GetCertificateResourceById");
                        }
                    }
                }
            });
            return certificateResourceItem;
        }
    }
}
