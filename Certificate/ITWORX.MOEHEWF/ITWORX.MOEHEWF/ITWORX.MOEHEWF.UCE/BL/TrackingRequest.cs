using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.BL
{
    public class TrackingRequest
    {

        
        public static List<Entities.TrackingRequest> GetAllRequestsTracking(string requestNumber , DateTime submitDateFrom , DateTime submitDateTo , string requestStatus, string requestType,/* string finalDecision,*/ int LCID)
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

                            SPList requestsList = web.Lists[Constants.Requests];
                            if (requestsList == null)
                                throw new Exception();
                            SPQuery spQuery = new SPQuery();
                            SPListItemCollection requestsItemsCollection = null;
                            string userName = SPContext.Current.Web.CurrentUser.LoginName.ToLower();
                            if (string.IsNullOrEmpty(requestNumber) && submitDateFrom == DateTime.MinValue && submitDateTo == DateTime.MinValue && string.IsNullOrEmpty(requestStatus) && string.IsNullOrEmpty(requestType) /*&& string.IsNullOrEmpty(finalDecision)*/)
                            {
                                spQuery.Query = @"<Where><Eq><FieldRef Name='LoginName' /><Value Type='Text'>" + userName+ "</Value></Eq></Where><OrderBy><FieldRef Name='RequestSendDate' Ascending='False' /></OrderBy>";
                                requestsItemsCollection = requestsList.GetItems(spQuery);
                            }
                            else
                            {
                                
                                StringBuilder queryString = new StringBuilder();
                                string lastStringValue = string.Empty;
                                queryString.Append("<Where><And><Eq><FieldRef Name='LoginName' /><Value Type='Text'>" + userName+"</Value></Eq>");
                                lastStringValue += "</And>";
                                if (!string.IsNullOrEmpty(requestNumber))
                                {
                                    if (submitDateFrom != DateTime.MinValue || submitDateTo != DateTime.MinValue ||!string.IsNullOrEmpty(requestStatus))
                                    {
                                        queryString.Append("<And>");
                                        lastStringValue += "</And>";
                                    }
                             
                                    queryString.Append("<Eq><FieldRef Name = 'ID' /><Value Type = 'Counter'>"+int.Parse(requestNumber)+"</Value></Eq>");
                                  
                                }
                                if (submitDateFrom != DateTime.MinValue)
                                {
                                    if (submitDateTo != DateTime.MinValue || !string.IsNullOrEmpty(requestStatus))
                                    {
                                        queryString.Append("<And>");
                                        lastStringValue += "</And>";
                                    }
                                
                                    
                                   queryString.Append("<Geq><FieldRef Name='RequestSendDate' /><Value IncludeTimeValue='FALSE' Type='DateTime'>" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(submitDateFrom) +"</Value></Geq>");
                                 
                                }
                                if (submitDateTo != DateTime.MinValue)
                                {
                                    if (!string.IsNullOrEmpty(requestStatus))
                                    {
                                        queryString.Append("<And>");
                                        lastStringValue += "</And>";
                                    }
                                   
                                    queryString.Append("<Leq><FieldRef Name='RequestSendDate' /><Value IncludeTimeValue='FALSE' Type='DateTime'>" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(submitDateTo) +"</Value></Leq>");
                                   
                                }
                                if (!string.IsNullOrEmpty(requestStatus))
                                {
                                    if (!string.IsNullOrEmpty(requestType))
                                    {

                                        queryString.Append("<And>");
                                        lastStringValue += "</And>";
                                    }
                                    queryString.Append("<Eq><FieldRef Name='RequestStatus' /><Value Type='Lookup'>"+requestStatus+"</Value></Eq>");
                                  
                                }
                                if (!string.IsNullOrEmpty(requestType))
                                {
                                    //Add it when final decision dropdown is populated
                                    //if (!string.IsNullOrEmpty(finalDecision))
                                    //{

                                    //    queryString.Append("<And>");
                                    //    lastStringValue += "</And>";
                                    //}
                                    queryString.Append("<Eq><FieldRef Name='RequestType' /><Value Type='Lookup'>" + requestType + "</Value></Eq>");
                                }
                                //if (!string.IsNullOrEmpty(finalDecision))
                                //{
                                //    queryString.Append("<Eq><FieldRef Name='FinalDecision' /><Value Type='Lookup'>" + finalDecision + "</Value></Eq>");
                                //}
                               

                                queryString.Append(lastStringValue+ "</Where><OrderBy><FieldRef Name='RequestSendDate' Ascending='False' /></OrderBy>");
                                spQuery.Query = queryString.ToString();
                                requestsItemsCollection = requestsList.GetItems(spQuery);
                            }
                            if (requestsItemsCollection!=null || requestsItemsCollection.Count > 0)
                            {
                                foreach (SPListItem item in requestsItemsCollection)
                                {
                                  
                                    trackingRequests.Add(new Entities.TrackingRequest
                                    {
                                        RequestType = new Entities.RequestTypes() { SelectedID = new SPFieldLookupValue(item["RequestType"].ToString()).LookupId.ToString(), SelectedTitle =LCID==1033 ? new SPFieldLookupValue(item["RequestType"].ToString()).LookupValue : new SPFieldLookupValue(item["RequestTypeAr"].ToString()).LookupValue },
                                        RequestNumber =item.ID,
                                        SubmitDate = Convert.ToDateTime(item["SubmitDate"]),
                                        RequestStatus = new Entities.RequestStatus() { SelectedID = new SPFieldLookupValue(item["RequestStatus"].ToString()).LookupId.ToString(), SelectedTitle =LCID==1033? new SPFieldLookupValue(item["RequestStatus"].ToString()).LookupValue : new SPFieldLookupValue(item["RequestStatusAr"].ToString()).LookupValue },
                                        RequestPhase = new Entities.RequestPhase() { SelectedID = new SPFieldLookupValue(item["RequestPhase"].ToString()).LookupId.ToString(), SelectedTitle =LCID==1033? new SPFieldLookupValue(item["RequestPhase"].ToString()).LookupValue :new SPFieldLookupValue(item["RequestPhaseAr"].ToString()).LookupValue },
                                        AcademicDegree = new Entities.AcademicDegree() { SelectedID = new SPFieldLookupValue(item["AcademicDegree"].ToString()).LookupId.ToString(), SelectedTitle = LCID == 1033 ? new SPFieldLookupValue(item["AcademicDegree"].ToString()).LookupValue : new SPFieldLookupValue(item["AcademicDegreeAr"].ToString()).LookupValue },
                                        AttachmentURL =item.Attachments.Count!=0?  SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, item.Attachments[0]):string.Empty,
                                        FileName=item.Attachments.Count!=0? item.Attachments[0] :string.Empty
                                    });
                                   

                                }

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

                Logging.GetInstance().Debug("Exiting method TrackingRequest.GetAllRequestsTracking");
            }
            return trackingRequests;
        }
    }
}
