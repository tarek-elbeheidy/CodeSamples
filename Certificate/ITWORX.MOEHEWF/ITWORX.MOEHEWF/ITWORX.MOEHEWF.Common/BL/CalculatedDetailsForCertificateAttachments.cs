using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ITWORX.MOEHEWF.Common.BL
{
   public  class CalculatedDetailsForCertificateAttachments
    {
        public static void DeleteCalculatedDetailsForCertificateAttachments(int requestNumber,string requestType)
        {
            Logging.GetInstance().Debug("Entering CalculatedDetailsForCertificateAttachments.DeleteCalculatedDetailsForCertificateAttachments");
            SPWeb web = null;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPQuery calcQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                requestNumber + "</Value></Eq></Where>");

                            SPList calctAttachList = null;
                            if (requestType == RequestType.CertificateEquivalency.ToString())
                            {
                                calctAttachList = web.Lists[Utilities.Constants.CalculatedDetailsForCertificateAttachments];
                            }
                            else
                            {
                                calctAttachList = web.Lists[Utilities.Constants.PACalculatedDetailsForCertificateAttachments];
                            }
                            SPListItemCollection calcCollection = calctAttachList.GetItems(calcQuery);
                            web.AllowUnsafeUpdates = true;
                            if (calcCollection != null && calcCollection.Count > 0)
                            {
                                foreach (SPListItem item in calcCollection)
                                {
                                    calctAttachList.Items.DeleteItemById(item.ID);
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
                Logging.GetInstance().Debug("Exit CalculatedDetailsForCertificateAttachments.DeleteCalculatedDetailsForCertificateAttachments");
            }
        }
    }
}
