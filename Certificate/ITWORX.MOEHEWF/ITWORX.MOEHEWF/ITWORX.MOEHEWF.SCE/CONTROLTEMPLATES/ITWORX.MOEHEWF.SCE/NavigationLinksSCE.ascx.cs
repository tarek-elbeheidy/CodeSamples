using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.Common.Utilities;

namespace ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE
{
    public partial class NavigationLinksSCE : UserControlBase
    {
        private int RequestId { get { return Convert.ToInt32(Request.QueryString["RequestId"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            BindRepeater();
        }


        public void BindRepeater()
        {

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {

                    Logging.GetInstance().Debug("Enter NavigationLinksSCE.BindRepeater");
                    using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                    {
                        SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == RequestId).FirstOrDefault();
                        List<SCENavigationLinksListFieldsContentType> navLinks = ctx.SCENavigationLinksList.ToList();
                        List<string> knownGrps = loggedInUserGroups();
                        List<SCENavigationLinksListFieldsContentType> navLst = new List<SCENavigationLinksListFieldsContentType>();
                        if (knownGrps.Count() != 0)
                        {
                            for (int i = 0; i < knownGrps.Count; i++)
                            {
                                navLst.AddRange(navLinks.Where(n => (n.PageType == PageType.Main || n.PageType == PageType.Parent) && n.Active == Active.Yes && n.SPGroupName.ToLower().Contains(',' + knownGrps[i].ToLower() + ',')).ToList());
                            }
                            var grps = navLst.GroupBy(n => n.PageTitleEn);
                            foreach (var grp in grps)
                            {
                                if (grp.Count() > 1)
                                {
                                    if (grp.Where(g => g.RequestStatus.Contains(request.RequestStatus)).ToList().Count > 0)
                                    {
                                        navLst.Remove(grp.Where(g => !g.RequestStatus.Contains(request.RequestStatus)).FirstOrDefault());
                                    }
                                    else
                                    {
                                        if (grp.Where(g => g.RequestStatus.Count == 0).Count() == 2) //same item no request status in both
                                            navLst.Remove(grp.Where(g => g.RequestStatus.Count == 0).FirstOrDefault());
                                        else // removing the one containing request status
                                            navLst.Remove(grp.Where(g => g.RequestStatus.Count == 1).FirstOrDefault());

                                    }
                                }

                            }
                            rep_Main.DataSource = navLst.OrderBy(p => p.PageOrder).ToList();
                            rep_Main.DataBind();
                            //Actions
                            if (!HelperMethods.InGroup(Utilities.Constants.Applicants))
                            {
                                navLst = new List<SCENavigationLinksListFieldsContentType>();
                                for (int i = 0; i < knownGrps.Count; i++)
                                {
                                    navLst.AddRange(navLinks.Where(n => n.PageType == PageType.Action && n.Active == Active.Yes && n.SPGroupName.ToLower().Contains(',' + knownGrps[i].ToLower() + ',')).ToList());
                                }
                                var grpsAction = navLst.GroupBy(n => n.PageTitleEn);
                                foreach (var grp in grpsAction)
                                {
                                    if (grp.Count() > 1)
                                    {
                                        if (grp.Where(g => g.RequestStatus.Contains(request.RequestStatus)).ToList().Count > 0)
                                        {
                                            navLst.Remove(grp.Where(g => !g.RequestStatus.Contains(request.RequestStatus)).FirstOrDefault());
                                        }
                                        else
                                        {
                                            if (grp.Where(g => g.RequestStatus.Count == 0).Count() == 2) //same item no request status in both
                                                navLst.Remove(grp.Where(g => g.RequestStatus.Count == 0).FirstOrDefault());
                                            else // removing the one containing request status
                                                navLst.Remove(grp.Where(g => g.RequestStatus.Count == 1).FirstOrDefault());
                                        }
                                    }

                                }
                                rep_Action.DataSource = navLst.OrderBy(p => p.PageOrder).ToList();
                                rep_Action.DataBind();

                                //View
                                navLst = new List<SCENavigationLinksListFieldsContentType>();
                                for (int i = 0; i < knownGrps.Count; i++)
                                {
                                    navLst.AddRange(navLinks.Where(n => n.PageType == PageType.View && n.Active == Active.Yes && n.SPGroupName.ToLower().Contains(',' + knownGrps[i].ToLower() + ',')).ToList());
                                }
                                var grpsView = navLst.GroupBy(n => n.PageTitleEn);
                                foreach (var grp in grpsView)
                                {
                                    if (grp.Count() > 1)
                                    {
                                        if (grp.Where(g => g.RequestStatus.Contains(request.RequestStatus)).ToList().Count > 0)
                                        {
                                            navLst.Remove(grp.Where(g => !g.RequestStatus.Contains(request.RequestStatus)).FirstOrDefault());
                                        }
                                        else
                                        {
                                            if (grp.Where(g => g.RequestStatus.Count == 0).Count() == 2) //same item no request status in both
                                                navLst.Remove(grp.Where(g => g.RequestStatus.Count == 0).FirstOrDefault());
                                            else // removing the one containing request status
                                                navLst.Remove(grp.Where(g => g.RequestStatus.Count == 1).FirstOrDefault());
                                        }
                                    }

                                }
                                rep_View.DataSource = navLst.OrderBy(p => p.PageOrder).ToList();
                                rep_View.DataBind();
                            }
                            //List<SCENavigationLinksListFieldsContentType> actionItems = navLinks.Where(n => n.PageType == PageType.Action && n.Active == Active.Yes && n.SPGroupName.ToLower().Contains(',' + knownGrps[0].ToLower() + ',')).OrderBy(n => n.PageOrder).ToList();
                            //if (actionItems.Count != 0)
                            //{
                            //    rep_Action.DataSource = actionItems;
                            //    rep_Action.DataBind();
                            //}
                            //else
                            //{
                            //    rep_Action.Visible = false;
                            //}

                            //List<SCENavigationLinksListFieldsContentType> viewItems = navLinks.Where(n => n.PageType == PageType.View && n.Active == Active.Yes && n.SPGroupName.ToLower().Contains(',' + knownGrps[0].ToLower() + ',')).OrderBy(n => n.PageOrder).ToList();
                            //if (viewItems.Count != 0)
                            //{
                            //    rep_View.DataSource = viewItems;
                            //    rep_View.DataBind();
                            //}
                            //else
                            //{
                            //    rep_View.Visible = false;
                            //}
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
                Logging.GetInstance().Debug("Exit NavigationLinksSCE.BindRepeater");
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
        protected void rep_Main_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            try
            {
                Logging.GetInstance().Debug("Entering NavigationLinksSCE.rep_Main_ItemDataBound");


                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    HyperLink lnk_Main = (HyperLink)e.Item.FindControl("lnk_Main");
                    SCENavigationLinksListFieldsContentType navItem = ((SCENavigationLinksListFieldsContentType)e.Item.DataItem);

                    if (LCID == (int)Language.English)
                    {
                        lnk_Main.Text = navItem.PageTitleEn;
                        if (navItem.PageType == PageType.Parent)
                        {
                            lnk_Main.NavigateUrl = SPContext.Current.Web.Url + navItem.PageLinkEn;
                        }
                        else
                        {
                            lnk_Main.NavigateUrl = SPContext.Current.Web.Url + navItem.PageLinkEn + "?RequestId=" + RequestId;
                        }
                    }
                    else
                    {
                        lnk_Main.Text = navItem.PageTitleAr;
                        if (navItem.PageType == PageType.Parent)
                        {
                            lnk_Main.NavigateUrl = SPContext.Current.Web.Url + navItem.PageLinkAr;
                        }
                        else
                        {
                            lnk_Main.NavigateUrl = SPContext.Current.Web.Url + navItem.PageLinkAr + "?RequestId=" + RequestId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NavigationLinksSCE.rep_Main_ItemDataBound");
            }

        }

        protected void rep_Action_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            try
            {
                Logging.GetInstance().Debug("Entering NavigationLinksSCE.rep_Action_ItemDataBound");


                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    HyperLink lnk_Main = (HyperLink)e.Item.FindControl("lnk_Action");

                    if (LCID == (int)Language.English)
                    {
                        lnk_Main.Text = ((SCENavigationLinksListFieldsContentType)e.Item.DataItem).PageTitleEn;
                        lnk_Main.NavigateUrl = SPContext.Current.Web.Url + ((SCENavigationLinksListFieldsContentType)e.Item.DataItem).PageLinkEn + "?RequestId=" + RequestId;
                    }
                    else
                    {
                        lnk_Main.Text = ((SCENavigationLinksListFieldsContentType)e.Item.DataItem).PageTitleAr;
                        lnk_Main.NavigateUrl = SPContext.Current.Web.Url + ((SCENavigationLinksListFieldsContentType)e.Item.DataItem).PageLinkAr + "?RequestId=" + RequestId;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NavigationLinksSCE.rep_Action_ItemDataBound");
            }
        }

        protected void rep_View_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering NavigationLinksSCE.rep_View_ItemDataBound");


                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    HyperLink lnk_Main = (HyperLink)e.Item.FindControl("lnk_View");

                    if (LCID == (int)Language.English)
                    {
                        lnk_Main.Text = ((SCENavigationLinksListFieldsContentType)e.Item.DataItem).PageTitleEn;
                        lnk_Main.NavigateUrl = SPContext.Current.Web.Url + ((SCENavigationLinksListFieldsContentType)e.Item.DataItem).PageLinkEn + "?RequestId=" + RequestId;
                    }
                    else
                    {
                        lnk_Main.Text = ((SCENavigationLinksListFieldsContentType)e.Item.DataItem).PageTitleAr;
                        lnk_Main.NavigateUrl = SPContext.Current.Web.Url + ((SCENavigationLinksListFieldsContentType)e.Item.DataItem).PageLinkAr + "?RequestId=" + RequestId;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NavigationLinksSCE.rep_View_ItemDataBound");
            }
        }
    }
}
