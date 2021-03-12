using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public static class PARecommendations
    {
        public static void AddRecommendation(string Recommendation, string Comments, string ReqID)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.AddRecommendation");
                            SPList list = web.Lists[Utilities.Constants.PAProgramManagerRecommendation];
                            SPListItem newitem = list.Items.Add();
                            newitem["RecommendationCreatedBy"] = SPContext.Current.Web.CurrentUser.Name;
                            newitem["RecommendationComments"] = SPHttpUtility.HtmlEncode(Comments);
                            newitem["RequestID"] = new SPFieldLookupValue(Convert.ToInt32(ReqID), ReqID);
                            newitem["RecommendationDate"] = DateTime.Now;
                            newitem["Recommendation"] = SPHttpUtility.HtmlEncode(Recommendation);
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
                            Logging.GetInstance().Debug("Exiting method AllProcedures.AddRecommendation");
                        }
                    }
                }
            });
        }

        public static List<Entities.Recomendations> GetRecommendationbyReqID(string ReqID)
        {
            List<Entities.Recomendations> Proc = new List<Entities.Recomendations>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method PARecommendations.GetRecommendationbyReqID");
                            SPList list = web.Lists[Utilities.Constants.PAProgramManagerRecommendation];
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" + ReqID + "</Value></Eq></Where><OrderBy><FieldRef Name='RecommendationDate' Ascending='False' /></OrderBy>");

                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    Proc.Add(new Entities.Recomendations()
                                    {
                                        RecommendationDate = item["RecommendationDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["RecommendationDate"].ToString()).ToShortDateString(),
                                        RecommendationCreatedBy = item["RecommendationCreatedBy"] == null ? string.Empty : item["RecommendationCreatedBy"].ToString(),
                                        RecommendationComments = item["RecommendationComments"] == null ? string.Empty : item["RecommendationComments"].ToString(),
                                        Recommendation = item["Recommendation"] == null ? string.Empty : item["Recommendation"].ToString(),
                                    });
                                }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method PARecommendations.GetRecommendationbyReqID");
                        }
                    }
                }
            });
            return Proc;
        }
    }
}