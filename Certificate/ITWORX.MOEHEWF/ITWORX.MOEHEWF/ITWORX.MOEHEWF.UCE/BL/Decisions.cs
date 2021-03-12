using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.BL
{
   public static class Decisions
    {
        public static void AddDecision(string Decision, string RejectionReason, string Comments, string ReqID,string SPGroupName)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.AddDecisions");
                            SPList list = web.Lists[Utilities.Constants.Decisions];
                            SPListItem newitem = list.Items.Add();
                            newitem["SPGroupName"] = SPGroupName;
                            newitem["DecisionMakerName"] = SPContext.Current.Web.CurrentUser.Name;
                            newitem["DecisionComments"] = SPHttpUtility.HtmlEncode(Comments);
                            newitem["RequestID"] = new SPFieldLookupValue(Convert.ToInt32(ReqID), ReqID);
                            newitem["Decision"] = SPHttpUtility.HtmlEncode(Decision);
                            newitem["DecisionDate"] = DateTime.Now;
                            if (!string.IsNullOrEmpty(RejectionReason))
                                newitem["RejectionReason"] = SPHttpUtility.HtmlEncode(RejectionReason);
                            web.AllowUnsafeUpdates = true;
                            newitem.Update();
                            list.Update();
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            web.AllowUnsafeUpdates = false;
                            Logging.GetInstance().Debug("Exiting method AllProcedures.AddDecisions");
                        }

                    }
                }
            });
        }

        public static List<Entities.Decisions> GetDecisionbyReqID(string ReqID)
        {
            List<Entities.Decisions> Decision = new List<Entities.Decisions>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method Decisions.GetDecisionbyReqID");
                            SPList list = web.Lists[Utilities.Constants.Decisions];
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='RequestID' /><Value Type='Text'>" +
                                ReqID + "</Value></Eq><Eq><FieldRef Name='DecisionMaker' /><Value Type='Text'>" +
                                SPContext.Current.Web.CurrentUser.Name + "</Value></Eq></And></Where><OrderBy><FieldRef Name='DecisionDate' Ascending='False' /></OrderBy>");

                            q.RowLimit = 1;
                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    Decision.Add(new Entities.Decisions()
                                    {
                                        DecisionDate = item["DecisionDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["DecisionDate"].ToString()).ToShortDateString(),
                                        DecisionMakerName = item["DecisionMakerName"] == null ? string.Empty : item["DecisionMakerName"].ToString(),
                                        DecisionComments = item["DecisionComments"] == null ? string.Empty : item["DecisionComments"].ToString(),
                                        Decision = item["Decision"] == null ? string.Empty : item["Decision"].ToString(),
                                        RejectionReason = item["RejectionReason"] == null ? string.Empty : item["RejectionReason"].ToString(),
                                    });
                                }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method Decisions.GetDecisionbyReqID");
                        }
                    }
                }
            });
            return Decision;

        }

        public static List<Entities.Decisions> GetDecisionPerRolebyReqID(string ReqID,string SPGroupName)
        {
            List<Entities.Decisions> Decision = new List<Entities.Decisions>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method Decisions.GetDecisionbyReqID");
                            SPList list = web.Lists[Utilities.Constants.Decisions];
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='RequestID' /><Value Type='Text'>" +
                                ReqID + "</Value></Eq><Eq><FieldRef Name='SPGroupName' /><Value Type='Text'>" +
                                SPGroupName + "</Value></Eq></And></Where><OrderBy><FieldRef Name='DecisionDate' Ascending='False' /></OrderBy>");

                            q.RowLimit = 1;
                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    Decision.Add(new Entities.Decisions()
                                    {
                                        DecisionDate = item["DecisionDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["DecisionDate"].ToString()).ToShortDateString(),
                                        DecisionMakerName = item["DecisionMakerName"] == null ? string.Empty : item["DecisionMakerName"].ToString(),
                                        DecisionComments = item["DecisionComments"] == null ? string.Empty : item["DecisionComments"].ToString(),
                                        Decision = item["Decision"] == null ? string.Empty : item["Decision"].ToString(),
                                        RejectionReason = item["RejectionReason"] == null ? string.Empty : item["RejectionReason"].ToString(),
                                    });
                                }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method Decisions.GetDecisionbyReqID");
                        }
                    }
                }
            });
            return Decision;

        }
    }
}
