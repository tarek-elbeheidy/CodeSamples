using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHEWF.SCE.Entities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.SCE.BL
{
   public class ClarificationRequest
    {
        public static List<SCEClarificationRequest> GetClarificationRequestbyReqID(string reqID)
        {
            List<SCEClarificationRequest> ClarReq = new List<SCEClarificationRequest>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method SCEClarificationRequests.GetClarificationRequestbyReqID");
                            SPList list = web.Lists[Utilities.Constants.SCEClarificationRequests];
                            SPQuery q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                reqID + "</Value></Eq></Where><OrderBy><FieldRef Name='ClarificationDate' Ascending='False' /></OrderBy>");

                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                    ClarReq.Add(new SCEClarificationRequest()
                                    {
                                        RequestID = RequestID.LookupValue,
                                        RequestClarificationDate = (item["ClarificationDate"] != null) ? DateTime.Parse(item["ClarificationDate"].ToString()) : DateTime.MinValue,
                                       // ReplyDate = (item["ReplyDate"] != null) ? DateTime.Parse(item["ReplyDate"].ToString()) : DateTime.MinValue,
                                        ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty,
                                        RequiredClarification = (item["RequiredClarification"] != null) ? item["RequiredClarification"].ToString() : string.Empty,
                                       // AssignedTo = (item["AssignedTo"] != null) ? item["AssignedTo"].ToString() : string.Empty,
                                     //   RequestSender = (item["RequestSender"] != null) ? item["RequestSender"].ToString() : string.Empty,
                                        ClarificationReply = (item["ClarificationReply"] != null) ? item["ClarificationReply"].ToString() : string.Empty,

                                    });
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
                            Logging.GetInstance().Debug("Exiting method SCEClarificationRequests.GetClarificationRequestbyReqID");
                        }
                    }
                }
            });
            return ClarReq;
        }

        public static void AddClarificationRequest(SCEClarificationRequest request)
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
                            string folderUrl = DateTime.Now.ToString("yyyy/MM/dd");
                            SPList list = web.Lists[Utilities.Constants.SCEClarificationRequests];


                           // SPFieldUserValue user = new SPFieldUserValue(web, SPContext.Current.Web.CurrentUser.LoginName);
                            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                            {
                                SCEClarificationsRequestsListFieldsContentType clarRequest = new SCEClarificationsRequestsListFieldsContentType()
                                {
                                    ClarificationDate = request.RequestClarificationDate,
                                    Sender = "",// user.ToString(),
                                    RequiredClarification = request.RequiredClarification,
                                    RequestID = Convert.ToInt32(request.RequestID),
                                    ClarificationReasonId = Convert.ToInt32(request.ClarificationReason)
                                   
                                                               
                                };
                              //  SPList sPList = SPContext.Current.Site.RootWeb.Lists.TryGetList("SCERequests");
                                SPFolder folder = ITWORX.MOEHEWF.Common.Utilities.BusinessHelper.CreateFolderInternal(list, list.RootFolder, folderUrl);
                                clarRequest.Path = folder.Url;
                                ctx.SCEClarificationsRequests.InsertOnSubmit(clarRequest);


                                ctx.SubmitChanges();

                             //   requestID = request.Id;

                            }

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
