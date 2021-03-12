using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MOEHE.PSPES.Webparts.BreadCrumb
{
    public partial class BreadCrumbUserControl : UserControl
    {
        const string resourceFile = "MOEHE.PSPES";

        public ResourceManager rm = new ResourceManager("MOEHE.PSPES", Assembly.GetExecutingAssembly());
        protected void Page_Load(object sender, EventArgs e)
        {
            string x = "";
           
            try
            {
                //if (SPContext.Current.Site.RootWeb.CurrentUser != null)
                //{

                    //ltMenu.Visible = true;
                    string currentPageURL = HttpContext.Current.Request.Url.AbsolutePath;

                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        // implementation details omitted
                        //SPContext.Current.Web.url


                        using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                        {
                            using (SPWeb web = site.RootWeb)
                            {
                                SPQuery query = new SPQuery();
                                query.Query = string.Concat(
                                           "<Where>",
                                               "<Eq>",
                                               "<FieldRef Name = 'URL'/>",
                                               "<Value Type = 'Text'>"+currentPageURL+"</Value>",
                                               "</Eq>",
                                           "</Where>");
                                SPList list = web.Lists["TopMenu"]; //web.GetList(listUrl);
                                SPListItemCollection menuItemsquery = list.GetItems(query);
                                IEnumerable<SPListItem> menuItemsEnum = menuItemsquery.Cast<SPListItem>();
                                List<SPListItem> menuItems = menuItemsEnum.ToList();

                                //var parentItems = menuItems.Where(h => h["ParentLink"] == null).OrderBy(q => q["DisplayOrder"]);
                                //List<SPListItem> menuItems = convertToList(list.GetItems(query));

                                //StringBuilder menu = new StringBuilder();

                                //menu.Append("<ul class=\"menuzord-menu\">");
                                //string userLoginName = SPContext.Current.Site.RootWeb.CurrentUser.LoginName;
                                string TitleField = "Title";
                                string HomeTitle = "";
                                if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
                                {
                                    TitleField = "ArabicTitle";
                                }
                                else
                                {
                                    TitleField = "Title";
                                }
                                foreach (SPListItem item in menuItems)
                                {
                                    BC_PageTitle.Text = item[TitleField].ToString();
                                    pageTitle.Text = item[TitleField].ToString();


                                }
                                //get the parentsite URL
                                SPQuery query2 = new SPQuery();
                                query2.Query = string.Concat(
                                           "<Where>",
                                               "<Eq>",
                                               "<FieldRef Name = 'URL'/>",
                                               "<Value Type = 'Text'>" + SPContext.Current.Web.ServerRelativeUrl + "</Value>",
                                               "</Eq>",
                                           "</Where>");
                                SPListItemCollection menuItemsquery2 = list.GetItems(query2);
                                IEnumerable<SPListItem> menuItemsEnum2 = menuItemsquery2.Cast<SPListItem>();
                                List<SPListItem> menuItems2 = menuItemsEnum2.ToList();

                                foreach (SPListItem parent in menuItems2)
                                {
                                    lnkParentSite.PostBackUrl = parent["URL"].ToString();
                                    BC_ParentSiteTitle.Text = parent[TitleField].ToString();


                                }

                                //setting the Home Title AND URL

                                BC_PortalTitle.Text = this.GetGlobalResourceObject("MOEHE.PSPES", "HomeTitle").ToString();
                                lnkPortalHome.PostBackUrl = SPContext.Current.Site.Url;

                            }
                        }
                    });
                //}
                //else
                //{
                //    //ltMenu.Visible = false;
                //}
            }
            catch { }

        }
    }
}
