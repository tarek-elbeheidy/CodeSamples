using Microsoft.SharePoint;
using MOEHE.PSPES.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MOEHE.PSPES.Webparts.NavigationMenu
{
    public partial class NavigationMenuUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                bool NoUserDetected = true;
                if (SPContext.Current.Site.RootWeb.CurrentUser != null)
                {

                    NoUserDetected = false;
                    ltMenu.Visible = true;

                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    // implementation details omitted



                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.RootWeb)
                        {
                            SPQuery query = new SPQuery();
                            query.Query = string.Concat(
                                       "<Where>",
                                           "<Eq>",
                                           "<FieldRef Name = 'IsActive'/>",
                                           "<Value Type = 'Boolean'>1</Value>",
                                           "</Eq>",
                                       "</Where>");
                            // string listUrl = web.Lists["TopMenu"];
                            SPList list = web.Lists["TopMenu"]; //web.GetList(listUrl);
                            SPListItemCollection menuItemsquery = list.GetItems(query);
                            IEnumerable<SPListItem> menuItemsEnum = menuItemsquery.Cast<SPListItem>();
                            List<SPListItem> menuItems = menuItemsEnum.ToList();

                            var parentItems = menuItems.Where(h => h["ParentLink"] == null).OrderBy(q => q["DisplayOrder"]);
                            //List<SPListItem> menuItems = convertToList(list.GetItems(query));

                            StringBuilder menu = new StringBuilder();

                            menu.Append("<ul class=\"menuzord-menu\">");
                            string userLoginName = SPContext.Current.Site.RootWeb.CurrentUser.LoginName;
                            foreach (SPListItem parent in parentItems)
                            {

                                bool isMember = false;
                                if (parent["AccessGroups"] != null)
                                {


                                    foreach (SPFieldUserValue userValue in parent["AccessGroups"] as SPFieldUserValueCollection)
                                    {
                                        isMember = SharePointUtilityRepository.IsUserInGroup(userValue.LookupValue, userLoginName);
                                        if (isMember)
                                            break;
                                    }


                                    if (isMember)
                                    {
                                        RenderItems(menu, menuItems, parent);
                                    }
                                    else
                                    {
                                        continue;
                                    }

                                }
                                else
                                {
                                    RenderItems(menu, menuItems, parent);
                                }
                            }

                            menu.Append("</ul>");

                            ltMenu.Text = menu.ToString();

                        }
                    }
                });
                }
                else
                {

                    ltMenu.Visible = true;

                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        // implementation details omitted

                        using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                        {
                            using (SPWeb web = site.RootWeb)
                            {
                                SPQuery query = new SPQuery();
                                query.Query = string.Concat(
                                           "<Where>",
                                               "<Eq>",
                                               "<FieldRef Name = 'IsActive'/>",
                                               "<Value Type = 'Boolean'>1</Value>",
                                               "</Eq>",
                                           "</Where>");
                                // string listUrl = web.Lists["TopMenu"];
                                SPList list = web.Lists["TopMenu"]; //web.GetList(listUrl);
                                SPListItemCollection menuItemsquery = list.GetItems(query);
                                IEnumerable<SPListItem> menuItemsEnum = menuItemsquery.Cast<SPListItem>();
                                List<SPListItem> menuItems = menuItemsEnum.ToList();

                                var parentItems = menuItems.Where(h => h["ParentLink"] == null).OrderBy(q => q["DisplayOrder"]);
                                //List<SPListItem> menuItems = convertToList(list.GetItems(query));

                                StringBuilder menu = new StringBuilder();

                                menu.Append("<ul class=\"menuzord-menu\">");
                                
                                string userLoginName = "NoUserThere";
                                foreach (SPListItem parent in parentItems)
                                {

                                    bool isMember = false;
                                    if (parent["AccessGroups"] != null)
                                    {


                                        foreach (SPFieldUserValue userValue in parent["AccessGroups"] as SPFieldUserValueCollection)
                                        {
                                            isMember = SharePointUtilityRepository.IsUserInGroup(userValue.LookupValue, userLoginName);
                                            if (isMember)
                                                break;
                                        }


                                        if (isMember)
                                        {
                                            RenderItems(menu, menuItems, parent);
                                        }
                                        else
                                        {
                                            continue;
                                        }

                                    }
                                    else
                                    {
                                        RenderItems(menu, menuItems, parent);
                                    }
                                }

                                menu.Append("</ul>");

                                ltMenu.Text = menu.ToString();

                            }
                        }

                        //using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                        //{
                        //    using (SPWeb web = site.RootWeb)
                        //    {
                        //        SPQuery query = new SPQuery();
                        //        query.Query = string.Concat(
                        //                   "<Where>",
                        //                        "<And>",
                        //                           "<Eq>",
                        //                                "<FieldRef Name = 'IsActive'/>",
                        //                                "<Value Type = 'Boolean'>1</Value>",
                        //                           "</Eq>",
                        //                            "<IsNull>",
                        //                                "<FieldRef Name='ParentLink' />",
                        //                            "</IsNull>",
                        //                         "</And>",
                        //                   "</Where>");
                        //        // string listUrl = web.Lists["TopMenu"];
                        //        SPList list = web.Lists["TopMenu"]; //web.GetList(listUrl);
                        //        SPListItemCollection menuItemsquery = list.GetItems(query);
                        //        IEnumerable<SPListItem> menuItemsEnum = menuItemsquery.Cast<SPListItem>();
                        //        List<SPListItem> menuItems = menuItemsEnum.ToList();

                        //        //var parentItems = menuItems.Where(h => h["ParentLink"] == null).OrderBy(q => q["DisplayOrder"]);
                        //        //List<SPListItem> menuItems = convertToList(list.GetItems(query));

                        //        StringBuilder menu = new StringBuilder();

                        //        menu.Append("<ul class=\"menuzord-menu\">");

                        //        foreach (SPListItem parent in menuItems)
                        //        {



                        //            RenderItemsAnonymous(menu, parent);

                        //        }

                        //        menu.Append("</ul>");

                        //        ltMenu.Text = menu.ToString();

                        //    }
                        //}

                    });
                }

                if (NoUserDetected)
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.RootWeb)
                        {
                            SPQuery query = new SPQuery();
                            query.Query = string.Concat(
                                       "<Where>",
                                           "<Eq>",
                                           "<FieldRef Name = 'IsActive'/>",
                                           "<Value Type = 'Boolean'>1</Value>",
                                           "</Eq>",
                                       "</Where>");
                            // string listUrl = web.Lists["TopMenu"];
                            SPList list = web.Lists["TopMenu"]; //web.GetList(listUrl);
                            SPListItemCollection menuItemsquery = list.GetItems(query);
                            IEnumerable<SPListItem> menuItemsEnum = menuItemsquery.Cast<SPListItem>();
                            List<SPListItem> menuItems = menuItemsEnum.ToList();

                            var parentItems = menuItems.Where(h => h["ParentLink"] == null).OrderBy(q => q["DisplayOrder"]);
                            //List<SPListItem> menuItems = convertToList(list.GetItems(query));

                            StringBuilder menu = new StringBuilder();

                            menu.Append("<ul class=\"menuzord-menu\">");

                            string userLoginName = "NoUserThere";
                            foreach (SPListItem parent in parentItems)
                            {

                                bool isMember = false;
                                if (parent["AccessGroups"] != null)
                                {


                                    foreach (SPFieldUserValue userValue in parent["AccessGroups"] as SPFieldUserValueCollection)
                                    {
                                        isMember = SharePointUtilityRepository.IsUserInGroup(userValue.LookupValue, userLoginName);
                                        if (isMember)
                                            break;
                                    }


                                    if (isMember)
                                    {
                                        RenderItems(menu, menuItems, parent);
                                    }
                                    else
                                    {
                                        continue;
                                    }

                                }
                                else
                                {
                                    RenderItems(menu, menuItems, parent);
                                }
                            }

                            menu.Append("</ul>");

                            ltMenu.Text = menu.ToString();

                        }
                    }

                }
            }
            catch { }

        }
        private static List<SPListItem> convertToList(SPListItemCollection items)
        {
            List<SPListItem> itemsList = new List<SPListItem>();

            foreach (SPListItem item in items)
            {
                itemsList.Add(item);
            }

            return itemsList;
        }
        private void RenderItems(StringBuilder sb, List<SPListItem> AllItems, SPListItem Node)
        {

            string TitleField = "Title";
            string userLoginName = SPContext.Current.Site.RootWeb.CurrentUser.LoginName;
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            {
                TitleField = "ArabicTitle";
            }
            else
            {
                TitleField = "Title";
            }

            //check if the user have access to this item or not







            sb.AppendLine("<li>");
            sb.AppendLine(@"<a href=""" + Node["URL"] + @""">" + Node[TitleField] + "</a>");

            if (HasChildItems(AllItems, Node))
            {
                var childItems = AllItems.Where(h => h["ParentLink"] != null && h["ParentLink"].ToString().Contains(Node["Title"].ToString())).OrderBy(q => q["DisplayOrder"]);
                if (childItems != null && childItems.Count() > 0)
                {
                    sb.AppendLine("<ul class=\"dropdown\">");
                    foreach (var child in childItems)
                    {
                        //check if the user has access or not

                        bool isMember = false;
                        if (child["AccessGroups"] != null)
                        {

                           // SPFieldUserValueCollection Users = (SPFieldUserValueCollection)child["AccessGroups"];
                            foreach (SPFieldUserValue userValue in child["AccessGroups"] as SPFieldUserValueCollection)
                            {
                                isMember = SharePointUtilityRepository.IsUserInGroup(userValue.LookupValue, userLoginName);
                                if (isMember)
                                    break;
                            }

                            if (isMember)
                            {
                                if (child["URL"].ToString() ==Request.Url.PathAndQuery.ToString())
                                {
                                    sb.AppendLine(@" <li class=""active"" ><a href=""" + child["URL"] + @""">" + child[TitleField] + "</a></li>");
                                }
                                else
                                {
                                    sb.AppendLine(@" <li><a href=""" + child["URL"] + @""">" + child[TitleField] + "</a></li>");
                                }
                                
                            }
                            else
                            {
                                continue;
                            }

                        }
                        else
                        {
                            if (child["URL"].ToString() == Request.Url.PathAndQuery.ToString())
                            {
                                sb.AppendLine(@" <li class=""active"" ><a href=""" + child["URL"] + @""">" + child[TitleField] + "</a></li>");
                            }
                            else
                            {
                                sb.AppendLine(@" <li><a href=""" + child["URL"] + @""">" + child[TitleField] + "</a></li>");
                            }
                          
                        }

                                               
                    }
                    sb.AppendLine("</ul>");
                }
            }




            sb.AppendLine("</li>");
        }
        private void RenderItemsAnonymous(StringBuilder sb,SPListItem Node)
        {

            string TitleField = "Title";
            string userLoginName = SPContext.Current.Site.RootWeb.CurrentUser.LoginName;
            if ((uint)CultureInfo.CurrentUICulture.LCID == 1025)
            {
                TitleField = "ArabicTitle";
            }
            else
            {
                TitleField = "Title";
            }

            //check if the user have access to this item or not







            sb.AppendLine("<li>");
            sb.AppendLine(@"<a href=""" + Node["URL"] + @""">" + Node[TitleField] + "</a>");

          




            sb.AppendLine("</li>");
        }

        public bool HasChildItems(List<SPListItem> AllItems, SPListItem Node)
        {
            bool HasChilds = false;
            var Items = AllItems.Where(h => h["ParentLink"] != null && h["ParentLink"].ToString().Contains(Node["Title"].ToString())).OrderBy(q => q["DisplayOrder"]);
            if (Items != null && Items.Count() > 0)
            {
                HasChilds = true;
            }
            return HasChilds;
        }
    }
}
