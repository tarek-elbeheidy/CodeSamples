using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Entities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.BL
{
    public class RequestsForStatements
    {
        public static List<RequestsForStatement> GetRequestsForStatementbyReqID(string reqID)
        {
            List<RequestsForStatement> ReqStatement = new List<RequestsForStatement>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method RequestsForStatements.GetRequestsForStatementbyReqID");
                            SPList list = web.Lists[Utilities.Constants.StatementRequests];
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Text'>" +
                                reqID + "</Value></Eq></Where> <OrderBy><FieldRef Name='StatementDate' Ascending='False' /></OrderBy>");

                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                    ReqStatement.Add(new RequestsForStatement()
                                    {
                                        DirectedTo = item["DirectedTo"] == null ? string.Empty : item["DirectedTo"].ToString(),
                                        StatementCreatedby = item["StatementCreatedBy"] == null ? string.Empty : item["StatementCreatedBy"].ToString(),
                                        StatementDate = item["StatementDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["StatementDate"].ToString()).ToShortDateString(),
                                        StatementReplyby = item["Replyby"] == null ? string.Empty : item["Replyby"].ToString(),
                                        StatementReplyDate = item["ReplyDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["ReplyDate"].ToString()).ToShortDateString(),
                                        StatementReply = item["Reply"] == null ? string.Empty : item["Reply"].ToString(),
                                        StatementRequested = item["StatementRequested"] == null ? string.Empty : item["StatementRequested"].ToString(),
                                        StatementSubject = item["StatementSubject"] == null ? string.Empty : item["StatementSubject"].ToString(),
                                        RequestID = RequestID.LookupValue,
                                        ID = item["ID"] == null ? string.Empty : item["ID"].ToString(),
                                    });

                                }

                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method RequestsForStatements.GetRequestsForStatementbyReqID");
                        }
                    }
                }
            });
            return ReqStatement;

        }
        public static void AddRequestsForStatement(string DirectedTo, string StatRequested, string StatSubject, string reqID)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method RequestsForStatements.AddRequestsForStatement");
                            SPList list = web.Lists[Utilities.Constants.StatementRequests];
                            SPListItem newitem = list.Items.Add();
                            newitem["RequestID"] = new SPFieldLookupValue(Convert.ToInt32(reqID), reqID);
                            newitem["StatementCreatedBy"] = SPContext.Current.Web.CurrentUser.Name;
                            newitem["StatementDate"] = DateTime.Now;
                            newitem["DirectedTo"] = DirectedTo;
                            newitem["StatementRequested"] = SPHttpUtility.HtmlEncode(StatRequested);
                            newitem["StatementSubject"] = SPHttpUtility.HtmlEncode(StatSubject);
                            web.AllowUnsafeUpdates = true;
                            newitem.Update();
                            list.Update();
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                            //handle exception as the business require.
                        }
                        finally
                        { //dispose any objects that require dispose.
                            Logging.GetInstance().Debug("Exiting method RequestsForStatements.AddRequestsForStatement");
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                }
            });
        }
        public static RequestsForStatement GetReqStatementbyID(string StatementID)
        {
            RequestsForStatement ReqStatement = new RequestsForStatement();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method RequestsForStatements.GetReqStatementbyID");
                            SPList list = web.Lists[Utilities.Constants.StatementRequests];
                            SPListItem item = list.GetItemById(Convert.ToInt32(StatementID));
                            if (item != null)
                            {
                                SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);

                                ReqStatement.DirectedTo = item["DirectedTo"] == null ? string.Empty : item["DirectedTo"].ToString();
                                ReqStatement.StatementCreatedby = item["StatementCreatedBy"] == null ? string.Empty : item["StatementCreatedBy"].ToString();
                                ReqStatement.StatementDate = item["StatementDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["StatementDate"].ToString()).ToShortDateString();
                                ReqStatement.StatementReplyby = item["Replyby"] == null ? string.Empty : item["Replyby"].ToString();
                                ReqStatement.StatementReplyDate = item["ReplyDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["ReplyDate"].ToString()).ToShortDateString();
                                ReqStatement.StatementReply = item["Reply"] == null ? string.Empty : item["Reply"].ToString();
                                ReqStatement.StatementRequested = item["StatementRequested"] == null ? string.Empty : item["StatementRequested"].ToString();
                                ReqStatement.StatementSubject = item["StatementSubject"] == null ? string.Empty : item["StatementSubject"].ToString();
                                ReqStatement.RequestID = RequestID.LookupValue;
                                ReqStatement.ID = item["ID"] == null ? string.Empty : item["ID"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method RequestsForStatements.GetReqStatementbyID");
                        }
                    }
                }
            });
            return ReqStatement;
        }
        public static void UpdateRequestsForStatementbyReply(string id, string reply)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method RequestsForStatements.UpdateRequestsForStatement");
                            SPList list = web.Lists[Utilities.Constants.StatementRequests];
                            SPListItem itemToUpdate = list.GetItemById(int.Parse(id));
                            itemToUpdate["Reply"] = SPHttpUtility.HtmlEncode(reply);
                            itemToUpdate["ReplyDate"] = DateTime.Now;
                            itemToUpdate["Replyby"] = SPContext.Current.Web.CurrentUser.Name;
                            web.AllowUnsafeUpdates = true;
                            itemToUpdate.Update();
                            list.Update();

                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method RequestsForStatements.UpdateRequestsForStatement");
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                }
            });
        }
    }
}
