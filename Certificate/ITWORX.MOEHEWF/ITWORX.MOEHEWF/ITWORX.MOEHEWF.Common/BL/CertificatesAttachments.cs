using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.BL
{
   public class CertificatesAttachments
    {
        public static void DeleteCertificates(int requestNumber)
        {
            Logging.GetInstance().Debug("Entering CertificatesAttachments.DeleteCertificates");
            SPWeb web = null;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPQuery certQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                requestNumber + "</Value></Eq></Where>");

                            SPList certAttachList = web.Lists[Utilities.Constants.CertificatesAttachments];
                            SPListItemCollection certCollection = certAttachList.GetItems(certQuery);
                            web.AllowUnsafeUpdates = true;
                            if (certCollection != null && certCollection.Count > 0)
                            {
                                foreach (SPListItem item in certCollection)
                                {
                                    certAttachList.Items.DeleteItemById(item.ID);
                                }
                            }

                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                web.AllowUnsafeUpdates = false;
                Logging.GetInstance().Debug("Exit CertificatesAttachments.DeleteCertificates");
            }
        }

    }
}
