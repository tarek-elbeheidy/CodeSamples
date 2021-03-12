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
   public class DelegationDocuments
    {
        public static void DeleteDelegationDocuments(int requestNumber,string requestType)
        {
            Logging.GetInstance().Debug("Entering DelegationDocuments.DeleteDelegationDocuments");
            SPWeb web = null;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPQuery delegationQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                requestNumber + "</Value></Eq></Where>");

                            SPList delegationDocumentist = null;
                            if (requestType == RequestType.CertificateEquivalency.ToString())
                            {
                                delegationDocumentist = web.Lists[Utilities.Constants.DelegationDocuments];
                            }
                            else
                            {
                                delegationDocumentist = web.Lists[Utilities.Constants.PADelegationDocuments];
                            }

                           
                            SPListItemCollection delegationCollection = delegationDocumentist.GetItems(delegationQuery);
                            web.AllowUnsafeUpdates = true;
                            if (delegationCollection != null && delegationCollection.Count > 0)
                            {
                                foreach (SPListItem item in delegationCollection)
                                {
                                    delegationDocumentist.Items.DeleteItemById(item.ID);
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
                Logging.GetInstance().Debug("Exit DelegationDocuments.DeleteDelegationDocuments");
            }

        }
    }
}
