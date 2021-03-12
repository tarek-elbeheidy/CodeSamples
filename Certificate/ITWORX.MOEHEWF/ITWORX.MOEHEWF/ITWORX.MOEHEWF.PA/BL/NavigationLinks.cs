using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Entities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class NavigationLinks
    {
        public List<RolesNavLinks> GetNavigationLinks(string GroupName, int LCID)
        {
            List<RolesNavLinks> linkslst = new List<RolesNavLinks>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method NavigationLinks.GetNavigationLinks");
                            SPList list = web.Lists["PARolesNavigationLinks"];
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='SPGroupName' /><Value Type='Text'>" +
                                GroupName + "</Value></Eq><Eq><FieldRef Name='Active' /><Value Type='Choice'>Yes</Value></Eq></And></Where><OrderBy><FieldRef Name='Order' Ascending='True' /></OrderBy>");
                            
                            SPListItemCollection collListItems = list.GetItems(q);

                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    RolesNavLinks links = new RolesNavLinks();
                                    if (LCID == (int)Language.English)
                                    {
                                        links.PageLink = (item["PageLinkEN"] != null) ? item["PageLinkEN"].ToString() : string.Empty;
                                        links.PageTitle = (item["PageTitleEN"] != null) ? item["PageTitleEN"].ToString() : string.Empty;
                                    }
                                    else
                                    {
                                        links.PageLink = (item["PageLinkAR"] != null) ? item["PageLinkAR"].ToString() : string.Empty;
                                        links.PageTitle = (item["PageTitleAR"] != null) ? item["PageTitleAR"].ToString() : string.Empty;
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
                            Logging.GetInstance().Debug("Exiting method NavigationLinks.GetNavigationLinks");
                        }
                    }
                }
            });
            return linkslst;
        }
    }
}