using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.BL
{
   public class RequestStatus
    {
        public static SPListItemCollection GetRequestStatusItems ()
        {
            Logging.GetInstance().Debug("Entering RequestStatus.GetRequestStatusItems");
            SPListItemCollection itemsCollection = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList requestStatusList = web.Lists[Constants.RequestStatus];

                          
                            if (requestStatusList == null)
                                throw new Exception();

                            SPQuery spQuery = new SPQuery();
                            spQuery.ViewFields = "<FieldRef Name='ID' /><FieldRef Name='Code' /><FieldRef Name='StatusEn' /><FieldRef Name='StatusAr' />";
                            spQuery.ViewFieldsOnly = true;
                            itemsCollection = requestStatusList.GetItems(spQuery);

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
                Logging.GetInstance().Debug("Exit RequestStatus.GetRequestStatusItems");
            }
            return itemsCollection;
        }
        public static List<Entities.RequestStatus> GetAll()
        {
            Logging.GetInstance().Debug("Entering method RequestStatus.GetAll");
            List<Entities.RequestStatus> requestStatus = new List<Entities.RequestStatus>();
            try
            {
                SPListItemCollection requestStatusItemsCollection = GetRequestStatusItems();

                if (requestStatusItemsCollection!=null || requestStatusItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in requestStatusItemsCollection)
                    {
                        requestStatus.Add(new Entities.RequestStatus() { ID = item.ID, ArabicStatus = Convert.ToString(item["StatusAr"]), EnglishStatus =Convert.ToString(item["StatusEn"]), Code=  Convert.ToString(item["Code"]) });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method RequestStatus.GetAll");
            }
            return requestStatus;
        }
    }
}
