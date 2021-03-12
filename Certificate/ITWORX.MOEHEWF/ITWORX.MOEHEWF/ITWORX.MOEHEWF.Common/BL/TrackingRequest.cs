using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ITWORX.MOEHEWF.Common.BL
{
    public class TrackingRequest
    {
        public static List<Entities.TrackingRequest> GetAllRequestsTracking(string requestNumber, DateTime submitDateFrom, DateTime submitDateTo, string requestStatus, string requestType, string finalDecision, int LCID)
        {


            Logging.GetInstance().Debug("Entering method TrackingRequest.GetAllRequestsTracking");
            List<Entities.TrackingRequest> trackingRequests = new List<Entities.TrackingRequest>();
            try
            {


                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList requestsList = web.Lists[Common.Utilities.Constants.Requests];

                            #region UCERequest
                            if (requestsList != null)
                                trackingRequests.AddRange(GetTrackingRequests(requestNumber.Trim(), submitDateFrom, submitDateTo, requestStatus, requestType, finalDecision, LCID, requestsList, string.Empty));
                            #endregion


                            SPList paRequestsList = web.Lists[Common.Utilities.Constants.PARequests];

                            #region PARequest
                            if (paRequestsList != null)
                                trackingRequests.AddRange(GetTrackingRequests(requestNumber, submitDateFrom, submitDateTo, requestStatus, requestType, finalDecision, LCID, paRequestsList, Common.Utilities.Constants.PARequests));
                            #endregion
                            #region SCERequest
                            SPList sceRequestsList = web.Lists[Common.Utilities.Constants.SCERequests];
                            if (sceRequestsList != null)
                                trackingRequests.AddRange(GetTrackingRequests(requestNumber, submitDateFrom, submitDateTo, requestStatus, requestType, finalDecision, LCID, sceRequestsList, Common.Utilities.Constants.SCERequests));
                            #endregion


                            trackingRequests = trackingRequests.OrderByDescending(_trRqst => _trRqst.SubmitDate).ToList();
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

                Logging.GetInstance().Debug("Exiting method TrackingRequest.GetAllRequestsTracking");
            }
            return trackingRequests;
        }

        public static List<Entities.TrackingRequest> GetAllPARequestsTracking(string requestNumber, DateTime submitDateFrom, DateTime submitDateTo, string requestStatus, string requestType, string finalDecision, int LCID)
        {


            Logging.GetInstance().Debug("Entering method TrackingRequest.GetAllRequestsTracking");
            List<Entities.TrackingRequest> trackingRequests = new List<Entities.TrackingRequest>();
            try
            {


                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {

                            SPList paRequestsList = web.Lists[Common.Utilities.Constants.PARequests];

                            #region PARequest
                            if (paRequestsList != null)
                                trackingRequests.AddRange(GetTrackingRequests(requestNumber, submitDateFrom, submitDateTo, requestStatus, requestType, finalDecision, LCID, paRequestsList, Common.Utilities.Constants.PARequests));
                            #endregion

                            trackingRequests = trackingRequests.OrderByDescending(_trRqst => _trRqst.SubmitDate).ToList();
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

                Logging.GetInstance().Debug("Exiting method TrackingRequest.GetAllRequestsTracking");
            }
            return trackingRequests;
        }
        private static List<Entities.TrackingRequest> GetTrackingRequests(string requestNumber, DateTime submitDateFrom, DateTime submitDateTo, string requestStatus, string requestType, string finalDecision, int LCID, SPList requestsList,string requestListName)
        {
            List<Entities.TrackingRequest> trackingRequests = new List<Entities.TrackingRequest>();
            string view = string.Empty;

            SPListItemCollection requestsItemsCollection = null;
            string userName = SPContext.Current.Web.CurrentUser.LoginName.ToLower();
            if (string.IsNullOrEmpty(requestNumber) && submitDateFrom == DateTime.MinValue && submitDateTo == DateTime.MinValue && string.IsNullOrEmpty(requestStatus) && string.IsNullOrEmpty(requestType) && string.IsNullOrEmpty(finalDecision))
            {
                SPQuery spQuery = null;
                spQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='LoginName' /><Value Type='Text'>" +
                    userName + "</Value></Eq></Where><OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>");
                
                if (requestListName.Equals(Utilities.Constants.SCERequests))
                {
                    spQuery= Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='LoginName' /><Value Type='Text'>"+ userName
                        +"</Value></Eq><Eq><FieldRef Name='IsEmployee' /><Value Type='Choice'>No</Value></Eq></And></Where>"+
                        "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>");
                }
                requestsItemsCollection = requestsList.GetItems(spQuery);
            }
            else if (!string.IsNullOrEmpty(requestStatus))
            {
                int requestStatusID = int.Parse(requestStatus);
                switch (requestStatusID)
                {
                    case (int)Utilities.RequestStatus.UCEReceptionistReviewInformation:
                        view = RequestViews.ApplicantInProgress.ToString();
                        break;
                    case (int)Utilities.RequestStatus.UCEHeadManagerAccepted:
                        view = RequestViews.ApplicantAccepted.ToString();
                        break;
                    case (int)Utilities.RequestStatus.UCEHeadManagerRejected:
                        view = RequestViews.ApplicantRejected.ToString();
                        break;
                    case (int)Utilities.RequestStatus.UCEReceptionistNeedsClarification:
                        view = RequestViews.ApplicantNeedsClarification.ToString();
                        break;
                    case (int)Utilities.RequestStatus.UCESubmitted:
                        view = RequestViews.ApplicantCurrentRequest.ToString();
                        break;
                    case (int)Utilities.RequestStatus.UCEDraft:
                        view = RequestViews.ApplicantDraft.ToString();
                        break;
                    case (int)Utilities.RequestStatus.UCECulturalMissionNeedsStatement:
                        view = RequestViews.ApplicantPendingForReview.ToString();
                        break;
                    case (int)Utilities.RequestStatus.UCEClosedByAcceptance:
                        view = RequestViews.ApplicantClosedByAccepted.ToString();
                        break;
                    case (int)Utilities.RequestStatus.UCEClosedByRejection:
                        view = RequestViews.ApplicantClosedByRejected.ToString();
                        break;
                    default:
                        view = RequestViews.AllItems.ToString();
                        break;

                }

                if (view.Equals(RequestViews.AllItems.ToString()))
                {
                    StringBuilder queryString = new StringBuilder();
                    string lastStringValue = string.Empty;
                    //if (!string.IsNullOrEmpty(view) && view== "All Items" &&( (string.IsNullOrEmpty(requestNumber) || submitDateFrom == DateTime.MinValue || submitDateTo == DateTime.MinValue || string.IsNullOrEmpty(requestType) || string.IsNullOrEmpty(finalDecision))))
                    //{
                    //    queryString.Append("<Where><Eq><FieldRef Name='LoginName' /><Value Type='Text'>" + userName + "</Value></Eq>");
                    //}
                    //else
                    //{
                    queryString.Append("<Where><And><Eq><FieldRef Name='LoginName' /><Value Type='Text'>" + userName + "</Value></Eq>");
                    lastStringValue += "</And>";
                    if (!string.IsNullOrEmpty(requestNumber))
                    {
                        if (submitDateFrom != DateTime.MinValue || submitDateTo != DateTime.MinValue || !string.IsNullOrEmpty(requestStatus) || !string.IsNullOrEmpty(requestType) || !string.IsNullOrEmpty(finalDecision))
                        {
                            queryString.Append("<And>");
                            lastStringValue += "</And>";
                        }
                       queryString.Append("<Contains><FieldRef Name = 'RequestNumber' /><Value Type = 'Text'>" + requestNumber + "</Value></Contains>");

                        //queryString.Append("<Eq><FieldRef Name = 'RequestNumber' /><Value Type = 'Text'>" + requestNumber + "</Value></Eq>");

                    }
                    if (submitDateFrom != DateTime.MinValue)
                    {
                        if (submitDateTo != DateTime.MinValue || !string.IsNullOrEmpty(requestStatus) || !string.IsNullOrEmpty(requestType) || !string.IsNullOrEmpty(finalDecision))
                        {
                            queryString.Append("<And>");
                            lastStringValue += "</And>";
                        }


                        queryString.Append("<Geq><FieldRef Name='SubmitDate' /><Value IncludeTimeValue='FALSE' Type='DateTime'>" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(submitDateFrom) + "</Value></Geq>");

                    }
                    if (submitDateTo != DateTime.MinValue)
                    {
                        if (!string.IsNullOrEmpty(requestStatus) || !string.IsNullOrEmpty(requestType) || !string.IsNullOrEmpty(finalDecision))
                        {
                            queryString.Append("<And>");
                            lastStringValue += "</And>";
                        }

                        queryString.Append("<Leq><FieldRef Name='SubmitDate' /><Value IncludeTimeValue='FALSE' Type='DateTime'>" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(submitDateTo) + "</Value></Leq>");

                    }
                    if (!string.IsNullOrEmpty(requestStatus))
                    {
                        if (!string.IsNullOrEmpty(requestType) || !string.IsNullOrEmpty(finalDecision))
                        {

                            queryString.Append("<And>");
                            lastStringValue += "</And>";
                        }
                        queryString.Append("<Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + requestStatus + "</Value></Eq>");

                    }
                    if (!string.IsNullOrEmpty(requestType))
                    {
                        //Add it when final decision dropdown is populated
                        if (!string.IsNullOrEmpty(finalDecision))
                        {

                            queryString.Append("<And>");
                            lastStringValue += "</And>";
                        }
                        queryString.Append("<Eq><FieldRef Name='RequestType' LookupId='TRUE' /><Value Type='Lookup'>" + requestType + "</Value></Eq>");
                    }

                    if (!string.IsNullOrEmpty(finalDecision))
                    {
                         if (requestListName.Equals(Utilities.Constants.PARequests)
                            && Utilities.Constants.PAValues.ContainsKey(int.Parse(finalDecision)))
                        {
                            
                                int finalDecisionRequestId = Utilities.Constants.PAValues[int.Parse(finalDecision)];
                                queryString.Append("<Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + finalDecisionRequestId + "</Value></Eq>");
                            
                        }
                        else if (requestListName.Equals(Utilities.Constants.SCERequests)
                            && Utilities.Constants.SCEValues.ContainsKey(int.Parse(finalDecision)))
                        {
                            int finalDecisionRequestId = Utilities.Constants.SCEValues[int.Parse(finalDecision)];
                            queryString.Append("<Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + finalDecisionRequestId + "</Value></Eq>");
                        }
                        else
                        {
                            queryString.Append("<Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + finalDecision + "</Value></Eq>");
                        }
                    }

                    queryString.Append(lastStringValue + "</Where><OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>");
                    SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject(queryString.ToString());
                    requestsItemsCollection = requestsList.GetItems(spQuery);
                }
                else
                {
                    SPView spView = requestsList.Views[view];
                    requestsItemsCollection = requestsList.GetItems(spView);
                }
            }
            else
            {
                StringBuilder queryString = new StringBuilder();
                string lastStringValue = string.Empty;

                queryString.Append("<Where><And><Eq><FieldRef Name='LoginName' /><Value Type='Text'>" + userName + "</Value></Eq>");
                lastStringValue += "</And>";
                if (!string.IsNullOrEmpty(requestNumber))
                {
                    if (submitDateFrom != DateTime.MinValue || submitDateTo != DateTime.MinValue || !string.IsNullOrEmpty(requestType) || !string.IsNullOrEmpty(finalDecision))
                    {
                        queryString.Append("<And>");
                        lastStringValue += "</And>";
                    }

                    //queryString.Append("<Eq><FieldRef Name = 'RequestNumber' /><Value Type = 'Text'>" + requestNumber + "</Value></Eq>");
                    queryString.Append("<Contains><FieldRef Name = 'RequestNumber' /><Value Type = 'Text'>" + requestNumber + "</Value></Contains>");

                }
                if (submitDateFrom != DateTime.MinValue)
                {
                    if (submitDateTo != DateTime.MinValue || !string.IsNullOrEmpty(requestType) || !string.IsNullOrEmpty(finalDecision))
                    {
                        queryString.Append("<And>");
                        lastStringValue += "</And>";
                    }


                    queryString.Append("<Geq><FieldRef Name='SubmitDate' /><Value IncludeTimeValue='FALSE' Type='DateTime'>" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(submitDateFrom) + "</Value></Geq>");

                }
                if (submitDateTo != DateTime.MinValue)
                {
                    if (!string.IsNullOrEmpty(requestType) || !string.IsNullOrEmpty(finalDecision))
                    {
                        queryString.Append("<And>");
                        lastStringValue += "</And>";
                    }

                    queryString.Append("<Leq><FieldRef Name='SubmitDate' /><Value IncludeTimeValue='FALSE' Type='DateTime'>" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(submitDateTo) + "</Value></Leq>");

                }

                if (!string.IsNullOrEmpty(requestType))
                {
                    //Add it when final decision dropdown is populated
                    if (!string.IsNullOrEmpty(finalDecision))
                    {

                        queryString.Append("<And>");
                        lastStringValue += "</And>";
                    }
                    queryString.Append("<Eq><FieldRef Name='RequestType' LookupId='TRUE' /><Value Type='Lookup'>" + requestType + "</Value></Eq>");
                }

                //Final decision will be changed to requeststatusID in case final  decision is selected ?? 
                if (!string.IsNullOrEmpty(finalDecision)  )
                {



                    if (requestListName.Equals(Utilities.Constants.PARequests)
                        &&   Utilities.Constants.PAValues.ContainsKey(int.Parse(finalDecision)))
                    {
                        int finalDecisionRequestId = Utilities.Constants.PAValues[int.Parse(finalDecision)];
                        queryString.Append("<Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + finalDecisionRequestId + "</Value></Eq>");
                    }
                    else if (requestListName.Equals(Utilities.Constants.SCERequests)
                         && Utilities.Constants.SCEValues.ContainsKey(int.Parse(finalDecision)))
                    {
                        int finalDecisionRequestId = Utilities.Constants.SCEValues[int.Parse(finalDecision)];
                        queryString.Append("<Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + finalDecisionRequestId + "</Value></Eq>");
                    }
                    else
                    {
                        queryString.Append("<Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + finalDecision + "</Value></Eq>");
                    }
                }
                // }

                queryString.Append(lastStringValue + "</Where><OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>");
                SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject(queryString.ToString());
                requestsItemsCollection = requestsList.GetItems(spQuery);

            }

            Entities.RequestStatus requestStatusItem = new Entities.RequestStatus();
            if (requestsItemsCollection != null && requestsItemsCollection.Count > 0)
            {
                foreach (SPListItem item in requestsItemsCollection)
                {
                    if (item["RequestStatusId"] != null)
                    {
                        requestStatusItem = RequestStatus.GetRequestStatusById(new SPFieldLookupValue(item["RequestStatusId"].ToString()).LookupId);

                        Entities.TrackingRequest trackingRequest = new Entities.TrackingRequest();

                        trackingRequest.RequestId = item.ID;
                        trackingRequest.RequestType = item["RequestType"] != null ? new Entities.RequestTypes() { SelectedID = new SPFieldLookupValue(item["RequestType"].ToString()).LookupId.ToString(), SelectedTitle = LCID == 1033 ? new SPFieldLookupValue(item["RequestType"].ToString()).LookupValue : new SPFieldLookupValue(item["RequestTypeAr"].ToString()).LookupValue } : new Entities.RequestTypes() { SelectedID = string.Empty, SelectedTitle = string.Empty };
                        trackingRequest.RequestNumber = item["RequestNumber"] != null ? Convert.ToString(item["RequestNumber"]) : string.Empty;
                        trackingRequest.SubmitDate = Convert.ToDateTime(item["SubmitDate"]) != DateTime.MinValue ? Convert.ToDateTime(item["SubmitDate"]) : DateTime.MinValue;
                        trackingRequest.RequestStatus = new Entities.RequestStatus() { SelectedID = new SPFieldLookupValue(item["RequestStatus"].ToString()).LookupId.ToString(), SelectedTitle = LCID == 1033 ? new SPFieldLookupValue(item["RequestStatus"].ToString()).LookupValue : new SPFieldLookupValue(item["RequestStatusAr"].ToString()).LookupValue, ApplicantRequestPhaseEn = requestStatusItem.ApplicantRequestPhaseEn/* Convert.ToString(item["RequestStatus_ApplicantRequestPhaseEn"])*/, ApplicantRequestPhaseAr = requestStatusItem.ApplicantRequestPhaseAr /* Convert.ToString(item["RequestStatus_ApplicantRequestPhaseAr"])*/ , FinalDecisionAr = requestStatusItem.FinalDecisionAr, FinalDecisionEn = requestStatusItem.FinalDecisionEn, TargetPageUrl = requestStatusItem.TargetPageUrl };
                        trackingRequest.Code = requestStatusItem.Code != null ? requestStatusItem.Code :string.Empty;
                        if (requestListName.Equals(Utilities.Constants.SCERequests)
                            && item["RegisteredScholasticLevel"] !=null)
                        {
                            trackingRequest.CurrentGrade = new SPFieldLookupValue(item["RegisteredScholasticLevel"].ToString()).LookupId.ToString();  
                        }
                        else
                        {
                            trackingRequest.CurrentGrade = string.Empty;
                        }

                        if (requestListName.Equals(Utilities.Constants.SCERequests))
                        {
                            trackingRequest.AcadDegree = item["CertificateType"] != null ? ((LCID == (int)Language.English) ? new SPFieldLookupValue(item["CertificateType"].ToString()).LookupValue : new SPFieldLookupValue(item["CertificateTypeAr"].ToString()).LookupValue) : string.Empty;

                        }
                        else
                        {
                            trackingRequest.AcadDegree = item["AcademicDegreeForEquivalence"] != null ? ((LCID == (int)Language.English) ? new SPFieldLookupValue(item["AcademicDegreeForEquivalence"].ToString()).LookupValue : new SPFieldLookupValue(item["AcademicDegreeForEquivalenceAr"].ToString()).LookupValue) : string.Empty;

                        }
                        trackingRequest.AttachmentURL = item.Attachments.Count != 0 ? SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, item.Attachments[0]) : string.Empty;
                        trackingRequest.FileName = item.Attachments.Count != 0 ? item.Attachments[0] : string.Empty;
                        trackingRequest.LoginName = item["LoginName"] != null ? Convert.ToString(item["LoginName"]) : string.Empty;

                        if (requestListName.Equals(Utilities.Constants.PARequests))
                        {
                            trackingRequest.RequestTypeEnumVaue = RequestType.PriorApproval.ToString();
                        }
                        else if (requestListName.Equals(Utilities.Constants.SCERequests))
                        {
                            trackingRequest.RequestTypeEnumVaue = RequestType.Schooling.ToString();
                        }
                        else
                        {
                            trackingRequest.RequestTypeEnumVaue = RequestType.CertificateEquivalency.ToString();
                        }

                        trackingRequests.Add(trackingRequest);
                    }
                }
                if (!string.IsNullOrEmpty(view) && !view.Equals(RequestViews.AllItems.ToString()))
                {
                    trackingRequests = trackingRequests.Where(a => a.LoginName.Equals(userName)).ToList();

                    if (!string.IsNullOrEmpty(requestNumber))
                    {
                        trackingRequests = trackingRequests.FindAll(t => t.RequestNumber.Contains(requestNumber));
                    }
                    if (!string.IsNullOrEmpty(requestType))
                    {
                        trackingRequests = trackingRequests.FindAll(t => t.RequestType.SelectedID.Equals(requestType));
                    }
                    if (submitDateFrom != DateTime.MinValue)
                    {

                        trackingRequests = trackingRequests.FindAll(t => t.SubmitDate != DateTime.MinValue && t.SubmitDate.Date >= Convert.ToDateTime(submitDateFrom).Date);
                    }
                    if (submitDateTo != DateTime.MinValue)
                    {
                        trackingRequests = trackingRequests.FindAll(t => t.SubmitDate != DateTime.MinValue && t.SubmitDate.Date <= Convert.ToDateTime(submitDateTo).Date);
                    }
                    //add search by final decision, this needs to be tested after generating the scenario

                    if (!string.IsNullOrEmpty(finalDecision))
                    {
                        if (requestListName.Equals(Utilities.Constants.PARequests)
                             && Utilities.Constants.PAValues.ContainsKey(int.Parse(finalDecision)))
                        {
                            int finalDecisionRequestId = Utilities.Constants.PAValues[int.Parse(finalDecision)];
                            trackingRequests = trackingRequests.FindAll(t =>int.Parse( t.RequestStatus.SelectedID) == finalDecisionRequestId);
                        }
                        else if (requestListName.Equals(Utilities.Constants.SCERequests)
                             && Utilities.Constants.SCEValues.ContainsKey(int.Parse(finalDecision)))
                        {
                            int finalDecisionRequestId = Utilities.Constants.SCEValues[int.Parse(finalDecision)];
                            trackingRequests = trackingRequests.FindAll(t => int.Parse(t.RequestStatus.SelectedID) == finalDecisionRequestId);
                        }
                        else
                        {
                            trackingRequests = trackingRequests.FindAll(t => t.RequestStatus.SelectedID.Equals(finalDecision));
                        }
                    }
                }

            }
            return trackingRequests;
        }

        public static List<Entities.TrackingRequest> GetAllUCERequestsTracking(string requestNumber, DateTime submitDateFrom, DateTime submitDateTo, string requestStatus, string requestType, string finalDecision, int LCID)
        {


            Logging.GetInstance().Debug("Entering method TrackingRequest.GetAllRequestsTracking");
            List<Entities.TrackingRequest> trackingRequests = new List<Entities.TrackingRequest>();
            try
            {


                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {

                           
                            SPList requestsList = web.Lists[Common.Utilities.Constants.Requests];

                            #region UCERequest
                            if (requestsList != null)
                                trackingRequests.AddRange(GetTrackingRequests(requestNumber.Trim(), submitDateFrom, submitDateTo, requestStatus, requestType, finalDecision, LCID, requestsList, string.Empty));
                            #endregion

                            trackingRequests = trackingRequests.OrderByDescending(_trRqst => _trRqst.SubmitDate).ToList();
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

                Logging.GetInstance().Debug("Exiting method TrackingRequest.GetAllRequestsTracking");
            }
            return trackingRequests;
        }

        public static int GetSubmittedRequestsNo()
        {
            Logging.GetInstance().Debug("Entering method TrackingRequest.GetSubmittedRequestsNo");
            int submittedRequestsNo = 0;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList requestsLists = web.Lists[Common.Utilities.Constants.Requests];
                            if (requestsLists == null)
                                throw new Exception();

                            SPQuery sPQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='LoginName' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName.ToLower() + @"</Value></Eq>
                            <Eq><FieldRef Name = 'RequestStatusId' /><Value Type = 'Lookup' >" + (int)Utilities.RequestStatus.UCESubmitted + "</Value ></Eq ></And ></Where>");
                            SPListItemCollection requestsItems = requestsLists.GetItems(sPQuery);

                            if (requestsItems != null && requestsItems.Count > 0)
                            {
                                submittedRequestsNo = requestsItems.Count;
                            }

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

                Logging.GetInstance().Debug("Exiting method TrackingRequest.GetSubmittedRequestsNo");
            }
            return submittedRequestsNo;
        }

        public static int GetClosedRequestsNo()
        {
            Logging.GetInstance().Debug("Entering method TrackingRequest.GetClosedRequestsNo");
            int closedRequestsNo = 0;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList requestsLists = web.Lists[Common.Utilities.Constants.Requests];
                            if (requestsLists == null)
                                throw new Exception();

                            SPQuery sPQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='LoginName' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName.ToLower() + @"</Value></Eq>
<Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Utilities.RequestStatus.UCEClosedByAcceptance + @"</Value></Eq>
<Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Utilities.RequestStatus.UCEClosedByRejection + "</Value></Eq></Or></And></Where>");
                            SPListItemCollection requestsItems = requestsLists.GetItems(sPQuery);

                            if (requestsItems != null && requestsItems.Count > 0)
                            {
                                closedRequestsNo = requestsItems.Count;
                            }

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

                Logging.GetInstance().Debug("Exiting method TrackingRequest.GetClosedRequestsNo");
            }
            return closedRequestsNo;
        }


        public static int GetNeedsClarificationRequestsNo()
        {
            Logging.GetInstance().Debug("Entering method TrackingRequest.GetNeedsClarificationRequestsNo");
            int needsClarificationRequestsNo = 0;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList requestsLists = web.Lists[Common.Utilities.Constants.Requests];
                            if (requestsLists == null)
                                throw new Exception();

                            SPQuery sPQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='LoginName' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName.ToLower() + @"</Value></Eq>
                            <Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Utilities.RequestStatus.UCEReceptionistNeedsClarification + @"</Value></Eq>
                            <Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Utilities.RequestStatus.UCEProgramEmployeeNeedsClarification + "</Value></Eq></Or></And></Where>");
                            SPListItemCollection requestsItems = requestsLists.GetItems(sPQuery);

                            if (requestsItems != null && requestsItems.Count > 0)
                            {
                                needsClarificationRequestsNo = requestsItems.Count;
                            }

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

                Logging.GetInstance().Debug("Exiting method TrackingRequest.GetNeedsClarificationRequestsNo");
            }
            return needsClarificationRequestsNo;
        }

        public static int GetUnderProgressRequestsNo()
        {
            Logging.GetInstance().Debug("Entering method TrackingRequest.GetUnderProgressRequestsNo");
            int underProgressRequestsNo = 0;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList requestsLists = web.Lists[Common.Utilities.Constants.Requests];
                            if (requestsLists == null)
                                throw new Exception();



                            SPView sPView = requestsLists.Views[RequestViews.ApplicantInProgress.ToString()];
                            SPListItemCollection requestsItems = requestsLists.GetItems(sPView);
                            List<Entities.TrackingRequest> trackingRequests = new List<Entities.TrackingRequest>();
                            if (requestsItems != null || requestsItems.Count > 0)
                            {
                                foreach (SPListItem item in requestsItems)
                                {

                                    trackingRequests.Add(new Entities.TrackingRequest
                                    {
                                        RequestId = item.ID,
                                        RequestNumber = Convert.ToString(item["RequestNumber"]),
                                        LoginName = Convert.ToString(item["LoginName"])

                                    });


                                }
                                trackingRequests = trackingRequests.FindAll(t => t.LoginName.Equals(SPContext.Current.Web.CurrentUser.LoginName.ToLower()));
                                underProgressRequestsNo = trackingRequests.Count;

                            }
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

                Logging.GetInstance().Debug("Exiting method TrackingRequest.GetUnderProgressRequestsNo");
            }
            return underProgressRequestsNo;
        }

        public static List<object> CreatePieChartList(uint LCID)
        {
            Logging.GetInstance().Debug("Entering method TrackingRequest.CreatePieChartList");
            List<object> pieChartList = new List<object>();
            try
            {

                int submittedRequestsNo = GetSubmittedRequestsNo();
                int closedRequestsNo = GetClosedRequestsNo();
                int needsClarificationRequestNo = GetNeedsClarificationRequestsNo();
                int underProgressRequestNo = GetUnderProgressRequestsNo();


                if (submittedRequestsNo > 0)
                {
                    pieChartList.Add(new Entities.RequestsChart()
                    {
                        RequestCount = submittedRequestsNo,
                        RequestType = HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "SubmittedRequestsNo", LCID)
                    });
                }

                if (closedRequestsNo > 0)
                {
                    pieChartList.Add(new Entities.RequestsChart()
                    {
                        RequestCount = closedRequestsNo,
                        RequestType = HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "ClosedRequestsNo", LCID)
                    });
                }

                if (needsClarificationRequestNo > 0)
                {
                    pieChartList.Add(new Entities.RequestsChart()
                    {
                        RequestCount = needsClarificationRequestNo,
                        RequestType = HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "NeedsClarificationRequestsNo", LCID)
                    });
                }
                if (underProgressRequestNo > 0)
                {
                    pieChartList.Add(new Entities.RequestsChart()
                    {
                        RequestCount = underProgressRequestNo,
                        RequestType = HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "UnderProgressRequestsNo", LCID)
                    });
                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method TrackingRequest.CreatePieChartList                                                                                                                                                                                                                                                                                                                                  ");
            }
            return pieChartList;
        }



    }
}