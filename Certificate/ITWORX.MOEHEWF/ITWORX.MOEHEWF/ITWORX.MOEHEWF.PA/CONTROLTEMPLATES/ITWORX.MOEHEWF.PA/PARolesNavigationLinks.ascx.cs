using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class PARolesNavigationLinks : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindRepeater();
            }
        }
        private List<string> loggedInUserGroups()
        {
            List<string> result = new List<string>();
            SPGroupCollection _SPGroupCollection = SPContext.Current.Web.CurrentUser.Groups;
            foreach (SPGroup group in _SPGroupCollection)
            {
                result.Add(group.Name);
            }
            return result;
        }
        private string GetGroup()
        {
            string groupObject=null;
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPUser user = web.CurrentUser;
                        SPGroupCollection groups = user.Groups;
                        foreach (SPGroup group in groups)
                        {
                            groupObject = group.Name;
                        }
                       
                    }
                }
            return groupObject;
        }
        private void BindRepeater()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method PARolesNavigationLinks.BindRepeater");             
                BL.NavigationLinks NL = new BL.NavigationLinks();
                var lst = loggedInUserGroups();
                foreach (var item in lst)
                {
                    var roleNavg = NL.GetNavigationLinks(item, LCID);
                    if (roleNavg.Count > 0)
                    {
                        rpt_Links.DataSource = roleNavg;
                        rpt_Links.DataBind();
                        break;
                    }

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
                Logging.GetInstance().Debug("Exiting method PARolesNavigationLinks.BindRepeater");
            }
        }
    }
}
