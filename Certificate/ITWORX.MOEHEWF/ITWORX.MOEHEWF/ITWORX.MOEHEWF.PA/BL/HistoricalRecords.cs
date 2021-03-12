using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Entities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class PAHistoricalRecords
    {
        public static List<Entities.SimilarRequest> GetPARequestDetailsbyID(string reqID, int LCID)
        {
            List<Entities.SimilarRequest> PARequestDetails = new List<Entities.SimilarRequest>();
            try
            {
                Logging.GetInstance().Debug("Entering method PAHistoricalRecords.GetPARequestDetailsbyID");
                using (SPWeb web = new SPSite(SPContext.Current.Site.Url).OpenWeb())
                {
                    string strViewFields = string.Empty;
                    if (LCID == (int)Language.English)
                    {
                        strViewFields = "<FieldRef Name='RequestNumber'/>"
                              + "<FieldRef Name='Applicants_QatarID'/>"
                              + "<FieldRef Name='Applicants_ApplicantName'/>"
                              + "<FieldRef Name='SubmitDate'/>"
                              + "<FieldRef Name='ID'/>"
                              + "<FieldRef Name='AcademicDegreeForEquivalence'/>";
                    }
                    else
                    {
                        strViewFields = "<FieldRef Name='RequestNumber'/>"
                             + "<FieldRef Name='Applicants_QatarID'/>"
                             + "<FieldRef Name='Applicants_ApplicantName'/>"
                             + "<FieldRef Name='SubmitDate'/>"
                             + "<FieldRef Name='ID'/>"
                             + "<FieldRef Name='AcademicDegreeForEquivalenceAr'/>";
                    }
                    SPQuery query = new SPQuery
                    {
                        Joins = "<Join Type='INNER' ListAlias='Applicants'>" +
                                  "<Eq>" +
                                  "<FieldRef Name='ApplicantID' RefType='Id'/>" +
                                  "<FieldRef List='Applicants' Name='ID'/>" +
                                  "</Eq>" +
                                  "</Join>",

                        ProjectedFields = "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                                            "List='Applicants' ShowField='ApplicantName'/>" +
                                            "<Field Name='Applicants_QatarID' Type='Lookup' " +
                                            "List='Applicants' ShowField='PersonalID'/>",

                        ViewFields = strViewFields,
                        Query = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Text'>" + reqID + "</Value></Eq></Where><OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>",
                    };
                    SPList customerList = web.Lists[Utilities.Constants.PARequests];
                    SPListItemCollection items = customerList.GetItems(query);
                    Common.Entities.RequestStatus requestStatusItem = new Common.Entities.RequestStatus();
                    foreach (SPListItem item in items)
                    {
                        SimilarRequest Request = new SimilarRequest();
                        SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue((item["Applicants_QatarID"] != null) ? item["Applicants_QatarID"].ToString() : string.Empty);
                        Request.QatariID = ApplicantsQatarID.LookupValue;
                        SPFieldLookupValue ApplicantsApplicantName = new SPFieldLookupValue((item["Applicants_ApplicantName"] != null) ? item["Applicants_ApplicantName"].ToString() : string.Empty);
                        Request.ApplicantName = ApplicantsApplicantName.LookupValue;
                        Request.ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;
                        Request.RequestID = (item["RequestNumber"] != null) ? item["RequestNumber"].ToString() : string.Empty;
                        Request.SubmitDate = (item["SubmitDate"] != null) ? DateTime.Parse(item["SubmitDate"].ToString()) : DateTime.MinValue;

                        if (LCID == (int)Language.English)
                        {
                            SPFieldLookupValue AcademicDegree = new SPFieldLookupValue((item["AcademicDegreeForEquivalence"] != null) ? item["AcademicDegreeForEquivalence"].ToString() : string.Empty);
                            Request.AcademicDegree = AcademicDegree.LookupValue;
                        }
                        else
                        {
                            SPFieldLookupValue AcademicDegree = new SPFieldLookupValue((item["AcademicDegreeForEquivalenceAr"] != null) ? item["AcademicDegreeForEquivalenceAr"].ToString() : string.Empty);
                            Request.AcademicDegree = AcademicDegree.LookupValue;
                        }
                        PARequestDetails.Add(Request);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PAHistoricalRecords.GetPARequestDetailsbyID");
            }
            return PARequestDetails;
        }

        public static List<Entities.HistoricalRecord> GetReqPAHistoricalRecords(int LCID, string reqID)
        {
            List<Entities.HistoricalRecord> PAHistoricalRecords = new List<Entities.HistoricalRecord>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method PAHistoricalRecords.GetReqPAHistoricalRecords");
                            SPList list = web.Lists[Utilities.Constants.RequestPAHistoricalRecords];
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Text'>" +
                                reqID + "</Value></Eq></Where><OrderBy><FieldRef Name='ActionDate' Ascending='False' /></OrderBy>");

                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    HistoricalRecord historicalRecord = new HistoricalRecord();

                                    historicalRecord.Executor = item["Executor"] == null ? string.Empty : item["Executor"].ToString();
                                    historicalRecord.AuthorityTask = item["AuthorityTask"] == null ? string.Empty : item["AuthorityTask"].ToString();
                                    historicalRecord.ActionDate = item["ActionDate"] == null ? DateTime.MinValue : DateTime.Parse(item["ActionDate"].ToString());
                                    historicalRecord.Comments = item["HistoryComments"] == null ? string.Empty : item["HistoryComments"].ToString();
                                    historicalRecord.RequestID = item["RequestID"] == null ? string.Empty : item["RequestID"].ToString();
                                    if (LCID == (int)Language.English)
                                        historicalRecord.ExecutedAction = item["ExecutedAction"] == null ? string.Empty : item["ExecutedAction"].ToString();
                                    else
                                        historicalRecord.ExecutedAction = item["ExecutedActionAr"] == null ? string.Empty : item["ExecutedActionAr"].ToString();

                                    PAHistoricalRecords.Add(historicalRecord);
                                }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method PAHistoricalRecords.GetReqPAHistoricalRecords");
                        }
                    }
                }
            });
            return PAHistoricalRecords;
        }

        public static void AddHistoricalRecord(int LCID, string ExecutedAction, string Executor, string AuthorityTask, string Comments, string ReqID)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method PAHistoricalRecords.AddHistoricalRecord");
                            SPList list = web.Lists[Utilities.Constants.RequestPAHistoricalRecords];
                            web.AllowUnsafeUpdates = true;
                            SPListItem item = list.Items.Add();
                            if (LCID == (int)Language.English)
                                item["ExecutedAction"] = ExecutedAction;
                            else
                                item["ExecutedActionAr"] = ExecutedAction;
                            item["Executor"] = Executor;
                            item["ActionDate"] = DateTime.Now;
                            item["AuthorityTask"] = AuthorityTask;
                            item["RequestID"] = new SPFieldLookupValue(Convert.ToInt32(ReqID), ReqID);
                            item["HistoryComments"] = Comments;
                            item.Update();
                            list.Update();
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            web.AllowUnsafeUpdates = false;
                            Logging.GetInstance().Debug("Exiting method PAHistoricalRecords.AddHistoricalRecord");
                        }
                    }
                }
            });
        }
    }
}