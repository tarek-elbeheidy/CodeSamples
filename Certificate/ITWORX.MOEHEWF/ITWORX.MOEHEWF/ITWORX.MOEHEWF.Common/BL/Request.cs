using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.BL
{
    public class Request
    {
        public static void DeleteRequest(int requestNumber,string requestType)
        {
            Logging.GetInstance().Debug("Entering Request.DeleteRequest");
            SPWeb web = null;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPQuery requestQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" +
                                requestNumber + "</Value></Eq></Where>");
                            SPList requestList = null;
                            if (requestType==RequestType.CertificateEquivalency.ToString())
                            {
                                requestList = web.Lists[Constants.Requests];
                            }
                            else if (requestType == RequestType.PriorApproval.ToString())
                            {
                                requestList = web.Lists[Constants.PARequests];
                            }
                             else if (requestType == RequestType.Schooling.ToString())
                            {
                                requestList = web.Lists[Constants.SCERequests];
                            }

                            
                            SPListItemCollection requestCollection = requestList.GetItems(requestQuery);
                            web.AllowUnsafeUpdates = true;
                            if (requestCollection != null && requestCollection.Count > 0)
                            {
                                requestList.Items.DeleteItemById(requestCollection[0].ID);

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
                web.AllowUnsafeUpdates = false;
                Logging.GetInstance().Debug("Exit Request.DeleteRequest");
            }

        }
        public static int GetRequestsCountByStatus(string requestStatus)
        {
            Logging.GetInstance().Debug("Entering method Request.GetRequestsCountByStatus");
            int requestsCount = 0;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {

                            SPList requestsList = web.Lists[Utilities.Constants.Requests];
                            if (requestsList == null)
                                throw new Exception();
                            SPQuery sPQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestStatus' /><Value Type='Lookup'>" +
                                requestStatus + "</Value></Eq></Where>");

                            SPListItemCollection requestsItems = requestsList.GetItems(sPQuery);
                            if (requestsItems != null && requestsItems.Count > 0)
                            {
                                requestsCount = requestsItems.Count;
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

                Logging.GetInstance().Debug("Exit Request.GetRequestsCountByStatus");
            }
            return requestsCount;
        }
        public static bool updateRequestStatus(int requestID)
        {
            Logging.GetInstance().Debug("Entering method Request.updateRequestStatus");
            string ID = null;
            SPWeb web = null;
            bool result = false;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;
                            SPList requestsList = web.Lists[Utilities.Constants.Requests];
                            SPListItem item = requestsList.GetItemById(requestID);
                            if (item != null)
                            {
                                if (item["RequestStatus"] != null)
                                {
                                    SPFieldLookupValue statusValue = new SPFieldLookupValue(item["RequestStatus"].ToString());
                                    if (statusValue.LookupId == Convert.ToInt32(Utilities.RequestStatus.UCEDraft))
                                    {
                                        item["RequestStatus"] = new SPFieldLookupValue((int)Utilities.RequestStatus.UCESubmitted, ((int)Common.Utilities.RequestStatus.UCESubmitted).ToString());
                                        item["SubmitDate"] = DateTime.Now;
                                        item["ActionDate"] = DateTime.Now;

                                        item.Update();
                                        result = true;
                                    }
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
                web.AllowUnsafeUpdates = false;
                Logging.GetInstance().Debug("Exiting method Request.updateRequestStatus");
            }
            return result;
        }

        public static bool addNewPaymentRecord(string requestID, string req_amount, string signed_date_time, string req_card_type, string req_reference_number, string cardNum, string responseMessage, string statementSubject,string reason_code,string paymentLogs="")
        {
            SPWeb web = null;
            try
            {
                Logging.GetInstance().Debug("Entering method Request.addNewPaymentRecord");

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;
                            SPList paymentList = web.Lists[Constants.PaymentRecords];

                            SPListItem NewItem = paymentList.Items.Add();
                            {
                                web.AllowUnsafeUpdates = true;
                                NewItem["Amount"] = req_amount;
                                NewItem["ReceiptDate"] = signed_date_time;
                                NewItem["ReceiptNumber"] = req_reference_number;
                                NewItem["CardType"] = req_card_type;
                                NewItem["CardNumber"] = cardNum;
                                NewItem["ResponseMessage"] = responseMessage;
                                NewItem["StatementSubject"] = statementSubject;
                                NewItem["ReasonCode"] = reason_code;
                                NewItem["RequestID"] = new SPFieldLookupValue(Convert.ToInt32(requestID), (Convert.ToInt32(requestID)).ToString());
                                NewItem["PaymentLogs"] = paymentLogs;
                                NewItem.Update();
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
                web.AllowUnsafeUpdates = false;
                Logging.GetInstance().Debug("Exiting method Request.GetRequestIDByNumber");
            }
            return false;
        }
        public static SPListItem GetRequestByNumber(string requestNumber)
        {
            Logging.GetInstance().Debug("Entering method Request.GetRequestIDByNumber");
            SPListItem temp = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList requestsList = web.Lists[Utilities.Constants.Requests];
                            if (requestsList == null)
                                throw new Exception();

                            string joins = @"<Join Type='LEFT' ListAlias='Applicants'>
                                               <Eq>
                                                    <FieldRef Name = 'ApplicantID' RefType = 'ID' />
                                                    <FieldRef List = 'Applicants' Name = 'ID' />
                                                </Eq>
                                             </Join>";
                            string projectedFields = "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                                                     "List='Applicants' ShowField='ApplicantName'/>" +
                                                     "<Field Name = 'Applicants_ApplicantAraicName' Type = 'Lookup' " +
                                                     "List='Applicants' ShowField='ArabicName'/>" + 
                                                     "<Field Name='Applicants_PersonalID' Type='Lookup' " +
                                                     "List='Applicants' ShowField='PersonalID'/>";

                            SPQuery reqQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestNumber' /><Value Type='Text'>" +
                                requestNumber + "</Value></Eq></Where>",joins,projectedFields, string.Empty);

                            SPListItemCollection requestItemCollection = requestsList.GetItems(reqQuery);
                            if (requestItemCollection != null && requestItemCollection.Count > 0)
                            {
                                temp = requestItemCollection[0];
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
                Logging.GetInstance().Debug("Exiting method Request.GetRequestIDByNumber");
            }
            return temp;
        }
        public static SPListItem GetRequestItemByID(int requestNumber)
        {
            Logging.GetInstance().Debug("Entering method Request.GetRequestItemByNumber");
            SPListItem item = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        { 
                            SPList requestsList = web.Lists[Utilities.Constants.Requests];
                            if (requestsList == null)
                                throw new Exception();
                             
                            SPQuery reqQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" +
                                requestNumber + "</Value></Eq></Where>");
                            reqQuery.Joins= @"<Join Type='LEFT' ListAlias='Applicants'>
                                               <Eq>
                                                    <FieldRef Name = 'ApplicantID' RefType = 'ID' />
                                                    <FieldRef List = 'Applicants' Name = 'ID' />
                                                </Eq>
                                             </Join>";
                            reqQuery.ProjectedFields= @"<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                                                     "List='Applicants' ShowField='ApplicantName'/>" +
                                                     "<Field Name = 'Applicants_ApplicantAraicName' Type = 'Lookup' " +
                                                     "List='Applicants' ShowField='ArabicName'/>" + 
                                                     "<Field Name='Applicants_PersonalID' Type='Lookup' " +
                                                     "List='Applicants' ShowField='PersonalID'/>";

                            SPListItemCollection requestItemCollection = requestsList.GetItems(reqQuery);
                            if (requestItemCollection != null && requestItemCollection.Count > 0)
                            {
                                item = requestItemCollection[0];
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
                Logging.GetInstance().Debug("Exiting method Request.GetRequestItemByNumber");
            }
            return item;
        }

        public static SPListItem GetPARequestItemByID(int requestNumber)
        {
            Logging.GetInstance().Debug("Entering method Request.GetPARequestItemByID");
            SPListItem item = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList requestsList = web.Lists[Utilities.Constants.PARequests];
                            if (requestsList == null)
                                throw new Exception();


                            SPQuery reqQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" +
                                requestNumber + "</Value></Eq></Where>");

                            SPListItemCollection requestItemCollection = requestsList.GetItems(reqQuery);
                            if (requestItemCollection != null && requestItemCollection.Count > 0)
                            {
                                item = requestItemCollection[0];
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
                Logging.GetInstance().Debug("Exiting method Request.GetPARequestItemByID");
            }
            return item;
        }

        public static SPListItem GetPARequestItemByNumber(string requestNumber)
        {
            Logging.GetInstance().Debug("Entering method Request.GetRequestItemByNumber");
            SPListItem item = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            string userName = SPContext.Current.Web.CurrentUser.LoginName.ToLower();
                            SPList requestsList = web.Lists[Utilities.Constants.PARequests];
                            if (requestsList == null)
                                throw new Exception();

                            string joins = @"<Join Type='LEFT' ListAlias='University'>
                                               <Eq>
                                                    <FieldRef Name = 'University' RefType = 'ID' />
                                                    <FieldRef List = 'University' Name = 'ID' />
                                                </Eq>
                                               </Join>

                                               <Join Type='LEFT' ListAlias='CountryOfStudy'>
                                               <Eq>
                                                    <FieldRef Name = 'ProgramCountry' RefType = 'ID' />
                                                    <FieldRef List = 'CountryOfStudy' Name = 'ID' />
                                                </Eq>
                                               </Join>

                                               <Join Type='LEFT' ListAlias='Applicants'>
                                               <Eq>
                                                    <FieldRef Name = 'ApplicantID' RefType = 'ID' />
                                                    <FieldRef List = 'Applicants' Name = 'ID' />
                                                </Eq>
                                               </Join>

                                               <Join Type='LEFT' ListAlias='Specialization'>
                                               <Eq>
                                                    <FieldRef Name = 'ProgramSpecialization' RefType = 'ID' />
                                                    <FieldRef List = 'Specialization' Name = 'ID' />
                                                </Eq>
                                               </Join>

                                               <Join Type='LEFT' ListAlias='AcademicDegree'>
                                               <Eq>
                                                    <FieldRef Name = 'ProgramType' RefType = 'ID' />
                                                    <FieldRef List = 'AcademicDegree' Name = 'ID' />
                                                </Eq>
                                               </Join>
                                               <Join Type='LEFT' ListAlias='StudySystem'>
                                               <Eq>
                                                    <FieldRef Name = 'StudySystem' RefType = 'ID' />
                                                    <FieldRef List = 'StudySystem' Name = 'ID' />
                                                </Eq>
                                               </Join>";

                            string projectedFields = "<Field Name='University_Title' Type='Lookup' " +
                                                       "List='University' ShowField='Title'/>" +
                                                       "<Field Name='University_TitleAr' Type='Lookup' " +
                                                       "List='University' ShowField='TitleAr'/>" +

                            "<Field Name='CountryOfStudy_Title' Type='Lookup' " +
                            "List='CountryOfStudy' ShowField='Title'/>" +
                            "<Field Name='CountryOfStudy_TitleAr' Type='Lookup' " +
                            "List='CountryOfStudy' ShowField='TitleAr'/>" +

                            "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                            "List='Applicants' ShowField='ApplicantName'/>" +
                            "<Field Name = 'Applicants_ApplicantAraicName' Type = 'Lookup' " +
                            "List='Applicants' ShowField='ArabicName'/>" +
                            "<Field Name = 'Applicants_ApplicantEnglishName' Type = 'Lookup' " +
                            "List='Applicants' ShowField='EnglishName'/>" +
                            "<Field Name='Applicants_PersonalID' Type='Lookup' " +
                            "List='Applicants' ShowField='PersonalID'/>" +

                            "<Field Name='Specialization_Title' Type='Lookup' " +
                            "List='Specialization' ShowField='Title'/>" +
                            "<Field Name='Specialization_TitleAr' Type='Lookup' " +
                            "List='Specialization' ShowField='TitleAr'/>" +

                            "<Field Name='AcademicDegree_Title' Type='Lookup' " +
                            "List='AcademicDegree' ShowField='Title'/>" +
                            "<Field Name='AcademicDegree_TitleAr' Type='Lookup' " +
                            "List='AcademicDegree' ShowField='TitleAr'/>" +

                            "<Field Name='StudySystem_Title' Type='Lookup' " +
                            "List='StudySystem' ShowField='Title'/>" +
                            "<Field Name='StudySystem_TitleAr' Type='Lookup' " +
                            "List='StudySystem' ShowField='TitleAr'/>";

                            SPQuery reqQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestNumber' /><Value Type='Text'>" +
                            requestNumber + "</Value></Eq></Where>", joins, projectedFields, string.Empty);

                            SPListItemCollection requestItemCollection = requestsList.GetItems(reqQuery);
                            if (requestItemCollection != null && requestItemCollection.Count > 0)
                            {
                                item = requestItemCollection[0];
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
                Logging.GetInstance().Debug("Exiting method Request.GetRequestItemByNumber");
            }
            return item;
        }
        public static SPListItem GetPAEmpRecommendation(string reqID)
        {
            SPListItem item = null;
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method AllProcedures.GetSavedPARecommendationstatusProc");
                            SPList list = web.Lists[Utilities.Constants.PAEquationOfficerProcedures];
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                reqID + "</Value></Eq></Where><OrderBy><FieldRef Name='ProcedureDate' Ascending='False' /></OrderBy>");

                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count > 0)
                            {
                                item = collListItems[0];
                            }

                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            //dispose any objects that require dispose.
                            Logging.GetInstance().Debug("Exiting method Request.GetSavedPARecommendationstatusProc");
                        }
                    }
                }
            });
            return item;
        }
        public static SPListItem GetRequestItemByOnlyID(int requestNumber)
        {
            Logging.GetInstance().Debug("Entering method Request.GetRequestItemByNumber");
            SPListItem item = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList requestsList = web.Lists[Utilities.Constants.Requests];
                            if (requestsList == null)
                                throw new Exception();


                            SPQuery reqQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" +
                                requestNumber + "</Value></Eq></Where>");

                            SPListItemCollection requestItemCollection = requestsList.GetItems(reqQuery);
                            if (requestItemCollection != null && requestItemCollection.Count > 0)
                            {
                                item = requestItemCollection[0];
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
                Logging.GetInstance().Debug("Exiting method Request.GetRequestItemByNumber");
            }
            return item;
        }
        public static int GetRequesStaustByRequestNumber(int requestNumber)
        {
            Logging.GetInstance().Debug("Entering method Request.GetRequestByNumber");
            int requestStatusId = 0;
            try
            {

                SPListItem requestItem = GetRequestItemByID(requestNumber);
                if (requestItem != null)
                {
                    requestStatusId = new SPFieldLookupValue(requestItem["RequestStatusId"].ToString()).LookupId;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit Request.GetRequestByNumber");
            }
            return requestStatusId;
        }
        public static int GetPARequesStaustByRequestNumber(int requestNumber)
        {
            Logging.GetInstance().Debug("Entering method Request.GetRequestByNumber");
            int requestStatusId = 0;
            try
            {

                SPListItem requestItem = GetPARequestItemByID(requestNumber);
                if (requestItem != null)
                {
                    requestStatusId = new SPFieldLookupValue(requestItem["RequestStatusId"].ToString()).LookupId;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit Request.GetRequestByNumber");
            }
            return requestStatusId;
        }
        public static int GetRequesStaustByOnlyRequestNumber(int requestNumber)
        {
            Logging.GetInstance().Debug("Entering method Request.GetRequestByNumber");
            int requestStatusId = 0;
            try
            {

                SPListItem requestItem = GetRequestItemByOnlyID(requestNumber);
                if (requestItem != null)
                {
                    requestStatusId = new SPFieldLookupValue(requestItem["RequestStatusId"].ToString()).LookupId;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit Request.GetRequestByNumber");
            }
            return requestStatusId;
        }

        public static void DeletePARequestsAttachments(int requestNumber)
        {
            Logging.GetInstance().Debug("Entering Request.DeletePARequestsAttachments");
            SPWeb web = null;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPQuery reqAttachQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                requestNumber + "</Value></Eq></Where>");

                            SPList requestsAttachments = web.Lists[Utilities.Constants.PARequestsAttachments]; ;
                           
                            SPListItemCollection reqAttachCollection = requestsAttachments.GetItems(reqAttachQuery);
                            web.AllowUnsafeUpdates = true;
                            if (reqAttachCollection != null && reqAttachCollection.Count > 0)
                            {
                                foreach (SPListItem item in reqAttachCollection)
                                {
                                    requestsAttachments.Items.DeleteItemById(item.ID);
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
                web.AllowUnsafeUpdates = false;
                Logging.GetInstance().Debug("Exit Request.DeletePARequestsAttachments");
            }
        }


        public static void DeleteSCERequestsAttachments(int requestNumber)
        {
            Logging.GetInstance().Debug("Entering Request.DeleteSCERequestsAttachments");
            SPWeb web = null;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPQuery reqAttachQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                requestNumber + "</Value></Eq></Where>");

                            SPList requestsAttachments = web.Lists[Utilities.Constants.SCERequestsAttachments]; ;

                            SPListItemCollection reqAttachCollection = requestsAttachments.GetItems(reqAttachQuery);
                            web.AllowUnsafeUpdates = true;
                            if (reqAttachCollection != null && reqAttachCollection.Count > 0)
                            {
                                foreach (SPListItem item in reqAttachCollection)
                                {
                                    requestsAttachments.Items.DeleteItemById(item.ID);
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
                web.AllowUnsafeUpdates = false;
                Logging.GetInstance().Debug("Exit Request.DeleteSCERequestsAttachments");
            }
        }


    }
}