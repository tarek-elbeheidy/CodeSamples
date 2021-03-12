using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITWORX.MOEHEWF.Common.Utilities;
namespace ITWORX.MOEHEWF.Common.BL
{
    public class HistoricalRecords
    {

        public static List<HistoricalRecord> GetReqHistoricalRecords(int LCID, string reqID, string listName)
        {
            List<HistoricalRecord> historicalRecords = new List<HistoricalRecord>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method HistoricalRecords.GetReqHistoricalRecords");
                            SPList list = web.Lists[listName];
                            SPListItemCollection collListItems = null;
                            if (HelperMethods.InGroup(Common.Utilities.Constants.ApplicanstGroupName))
                            {
                                var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                    reqID + "</Value></Eq><Eq><FieldRef Name='ApplicantCanView' /><Value Type='Choice'>Yes</Value></Eq></And></Where><OrderBy><FieldRef Name='ActionDate' Ascending='False' /></OrderBy>");

                                collListItems = list.GetItems(q);
                            }
                            else
                            {
                                var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                    reqID + "</Value></Eq></Where><OrderBy><FieldRef Name='ActionDate' Ascending='False' /></OrderBy>");

                                collListItems = list.GetItems(q);
                            }
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    Entities.HistoricalRecord historicalRecord = new Entities.HistoricalRecord();

                                    historicalRecord.Executor = item["Executor"] == null ? string.Empty : item["Executor"].ToString();
                                    SPFieldLookupValue AuthorityTask = new SPFieldLookupValue((item["AuthorityTask"] != null) ? item["AuthorityTask"].ToString() : string.Empty);
                                    SPFieldLookupValue AuthorityTaskAr = new SPFieldLookupValue((item["AuthorityTaskAr"] != null) ? item["AuthorityTaskAr"].ToString() : string.Empty);
                                    historicalRecord.ActionDate = item["ActionDate"] == null ? DateTime.MinValue : DateTime.Parse(item["ActionDate"].ToString());
                                    historicalRecord.Comments = item["HistoryComments"] == null ? string.Empty : item["HistoryComments"].ToString();
                                    historicalRecord.RequestID = item["RequestID"] == null ? string.Empty : item["RequestID"].ToString();
                                    historicalRecord.ApplicantCanView = item["ApplicantCanView"] == null ? string.Empty : item["ApplicantCanView"].ToString();
                                    SPFieldLookupValue ExecutedActionAr = new SPFieldLookupValue((item["ExecutedActionAr"] != null) ? item["ExecutedActionAr"].ToString() : string.Empty);
                                    SPFieldLookupValue ExecutedAction = new SPFieldLookupValue((item["ExecutedAction"] != null) ? item["ExecutedAction"].ToString() : string.Empty);
                                    if (LCID == (int)Language.English)
                                    {
                                        historicalRecord.AuthorityTask = AuthorityTask.LookupValue;
                                        historicalRecord.ExecutedAction = ExecutedAction.LookupValue;
                                    }
                                    else
                                    {
                                        historicalRecord.AuthorityTask = AuthorityTaskAr.LookupValue;
                                        historicalRecord.ExecutedAction = ExecutedActionAr.LookupValue;
                                    }
                                    historicalRecords.Add(historicalRecord);
                                }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method HistoricalRecords.GetReqHistoricalRecords");
                        }
                    }
                }
            });
            return historicalRecords;
        }
        public static void AddHistoricalRecords(string listNameRequest, string listNameHistorical, int LCID, int StatusID, string Executor, string Comments, string requestID, string ApplicantCanView)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPListItem request = web.Lists[listNameRequest].GetItemById(requestID.ToInt());
                        AddHistoricalRecords(listNameHistorical, LCID, StatusID, Executor, Comments, request, ApplicantCanView);
                    }
                }
            });

        }
        public static void AddHistoricalRecords(string listName, int LCID, int StatusID, string Executor, string Comments, SPListItem request, string ApplicantCanView)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            SPListItem listItem = null;

                            SPList list = web.Lists[listName];

                            listItem = list.AddItemWithFolders(request[SPBuiltInFieldId.Created].ToDate().ToString("yyyy/MM/dd"));
                            listItem["ExecutedAction"] = new SPFieldLookupValue(StatusID, Common.BL.RequestStatus.GetRequestStatusById(StatusID).HistoryDescriptionEn);
                            listItem["ExecutedActionAr"] = new SPFieldLookupValue(StatusID, Common.BL.RequestStatus.GetRequestStatusById(StatusID).HistoryDescriptionAr);
                            listItem["Executor"] = Executor;
                            listItem["ActionDate"] = DateTime.Now;
                            listItem["AuthorityTask"] = new SPFieldLookupValue(StatusID, Common.BL.RequestStatus.GetRequestStatusById(StatusID).ActionByEn);
                            listItem["AuthorityTaskAr"] = new SPFieldLookupValue(StatusID, Common.BL.RequestStatus.GetRequestStatusById(StatusID).ActionByAr);
                            listItem["RequestID"] = new SPFieldLookupValue(request.ID, request.ID.ToString());
                            listItem["HistoryComments"] = Comments;
                            listItem["ApplicantCanView"] = "Yes";

                            listItem.Update();
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            web.AllowUnsafeUpdates = false;
                            Logging.GetInstance().Debug("Exiting method HistoricalRecords.AddHistoricalRecords");
                        }
                    }

                }

            });
        }
    }

}
