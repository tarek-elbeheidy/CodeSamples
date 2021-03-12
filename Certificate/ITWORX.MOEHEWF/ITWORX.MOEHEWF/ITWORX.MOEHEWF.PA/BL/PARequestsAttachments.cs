using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.PA.BL
{
   public class PARequestsAttachments
    {
        public static void DeleteRequestsAttachmentsByGroupAndRequestID(int requestId, string groupName)
        {
            SPWeb web = null;
            try
            {
                Logging.GetInstance().Debug("Entering method PARequestsAttachments.DeleteRequestsAttachmentsByGroupAndRequestID");
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPList requestsAttachments = web.Lists[Utilities.Constants.PARequestsAttachments];
                            if (requestsAttachments == null)
                                throw new Exception();

                            web.AllowUnsafeUpdates = true;
                            SPQuery sPQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" + requestId + "</Value></Eq><Eq><FieldRef Name='Group' /><Value Type='Text'>" + groupName + "</Value></Eq></And></Where>");
                            SPListItemCollection attachItems = requestsAttachments.GetItems(sPQuery);
                            if (attachItems != null && attachItems.Count > 0)
                            {
                                foreach (SPListItem item in attachItems)
                                {
                                    requestsAttachments.Items.DeleteItemById(item.ID);
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
                Logging.GetInstance().Debug("Exiting method PARequestsAttachments.DeleteRequestsAttachmentsByGroupAndRequestID");
            }
        }
    }
}
