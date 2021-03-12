using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.Common.BL
{
    public class ServiceDashboard
    {
        public List<Entities.ServiceDashboard> GetServiceLinks(int LCID)
        {
            List<Entities.ServiceDashboard> linkslst = new List<Entities.ServiceDashboard>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method ServiceDashboard.GetServiceLinks");
                            SPList list = web.Lists["ServiceDashBoard"];
                            //var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='SPGroup' /><Value Type='Text'>" +
                            //    GroupName + "</Value></Eq><Eq><FieldRef Name='Active' /><Value Type='Choice'>Yes</Value></Eq></And></Where>");
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='Active' /><Value Type='Boolean'>1</Value></Eq></Where>");

                            SPListItemCollection collListItems = list.GetItems(q);

                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    Entities.ServiceDashboard links = new Entities.ServiceDashboard();
                                    if (LCID == (int)Language.English)
                                    {
                                        links.ID = item.ID;
                                        links.Title = (item["Title"] != null) ? item["Title"].ToString() : string.Empty;
                                        links.SPGroup= (item["SPGroup"] != null) ? item["SPGroup"].ToString() : string.Empty;
                                    }
                                    else
                                    {
                                        links.ID = item.ID;
                                        links.Title = (item["TitleAr"] != null) ? item["TitleAr"].ToString() : string.Empty;
                                        links.SPGroup = (item["SPGroup"] != null) ? item["SPGroup"].ToString() : string.Empty;
                                    }
                                    linkslst.Add(links);
                                }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                            //handle exception as the business require.
                        }
                        finally
                        {

                            //dispose any objects that require dispose.
                            Logging.GetInstance().Debug("Exiting method ServiceDashboard.GetServiceLinks");
                        }
                    }
                }
            });
            return linkslst;
        }

        internal object GetNavigationLinks(object p, int lCID)
        {
            throw new NotImplementedException();
        }
    }
}