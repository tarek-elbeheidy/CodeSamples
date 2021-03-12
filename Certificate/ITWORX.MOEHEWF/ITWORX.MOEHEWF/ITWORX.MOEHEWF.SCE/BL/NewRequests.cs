using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.SCE.BL
{
   public class NewRequests
    {
        public static IEnumerable<Entities.NewRequests> GetAllNewRequests(string strQuery, int LCID)
        {
            List<Entities.NewRequests> Requests = new List<Entities.NewRequests>();
            using (SPSite oSite = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb oWeb = oSite.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method NewRequests.GetAllNewRequests");
                        oWeb.AllowUnsafeUpdates = true;
                        string strViewFields = GetstrViewFields(LCID);

                        string joins = "<Join Type='LEFT' ListAlias='Applicants'>" +
                                      "<Eq>" +
                                      "<FieldRef Name='ApplicantID' RefType='Id'/>" +
                                      "<FieldRef List='Applicants' Name='ID'/>" +
                                      "</Eq>" +
                                      "</Join>" +

                                      "<Join Type='LEFT' ListAlias='CountryOfStudy'>" +
                                      "<Eq>" +
                                        "<FieldRef  Name='CertificateResource' RefType='Id' />" +
                                        "<FieldRef List='CountryOfStudy' Name='ID' />" +
                                      "</Eq></Join>" +
                                        "<Join Type='LEFT' ListAlias='ScholasticLevel'>" +
                                      "<Eq>" +
                                        "<FieldRef  Name='LastScholasticLevel' RefType='Id' />" +
                                        "<FieldRef List='ScholasticLevel' Name='ID' />" +
                                      "</Eq></Join>" +
                                      "<Join Type='LEFT' ListAlias='Nationality'>" +
                                      "<Eq>" +
                                      "<FieldRef Name='StdNationality' RefType='Id'/>" +
                                      "<FieldRef List='Nationality' Name='ID'/>" +
                                      "</Eq>" +
                                      "</Join>";
                                      
                        string projFields  = "<Field Name='Applicants_QatarID' Type='Lookup' " +
                                                "List='Applicants' ShowField='PersonalID'/>" +
                                                  "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ApplicantName'/>" +
                                                 "<Field Name='Applicants_ArabicName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ArabicName'/>" +

                                                "<Field Name='Applicants_EnglishName' Type='Lookup' " +
                                                "List='Applicants' ShowField='EnglishName'/>" +
                                                "<Field Name='Applicants_QID' Type='Lookup' " +
                                                "List='Applicants' ShowField='ID'/>" +

                                                "<Field Name='Applicants_MobileNumber' Type='Lookup' " +
                                                "List='Applicants' ShowField='MobileNumber'/>" +

                                                "<Field Name='Nationality_Title' Type='Lookup' " +
                                                "List='Nationality' ShowField='Title'/>" +
                                                "<Field Name='Nationality_TitleAr' Type='Lookup' " +
                                                "List='Nationality' ShowField='TitleAr'/>"+ 

                                                "<Field Name='CountryOfStudy_Title' Type='Lookup' " +
                                                "List='CountryOfStudy' ShowField='Title'/>"+

                                                "<Field Name='CountryOfStudy_TitleAr' Type='Lookup' " +
                                                "List='CountryOfStudy' ShowField='TitleAr'/>"+

                                                 "<Field Name='ScholasticLevel_Title' Type='Lookup' " +
                                                "List='ScholasticLevel' ShowField='Title'/>" +

                                                "<Field Name='ScholasticLevel_TitleAr' Type='Lookup' " +
                                                "List='ScholasticLevel' ShowField='TitleAr'/>";


                        //if (strQuery.Contains("&"))
                        //{
                        //    var queries = strQuery.Split('&');
                        //    SPQuery reqQueryNewReq = Common.Utilities.BusinessHelper.GetQueryObject(queries[0], joins, projFields, strViewFields);

                        //    Requests.AddRange(GetRequestsListing(reqQueryNewReq, LCID));
                        //    SPQuery reqQueryCultural = Common.Utilities.BusinessHelper.GetQueryObject(queries[1], joins, projFields, strViewFields);

                        //    Requests.AddRange(GetRequestsListing(reqQueryCultural, LCID));

                        //}
                        //else
                        //{
                            SPQuery reqQuery = Common.Utilities.BusinessHelper.GetQueryObject(strQuery, joins, projFields, strViewFields);


                            Requests = GetRequestsListing(reqQuery, LCID);
                        //}
                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(ex);
                    }
                    finally
                    {
                        oWeb.AllowUnsafeUpdates = false;
                        Logging.GetInstance().Debug("Exiting method NewRequests.GetAllNewRequests");
                    }
                }
            }
            return Requests;
        }

        public static string GetNewRequestsQuery (string GroupName , int delayedDays)
        {
            Logging.GetInstance().Debug("Enter NewRequests.GetNewRequestsQuery");
            string newQuery = string.Empty;
            try
            {

                //Department Manager Query
                if (Common.Utilities.Constants.SCEDepartmentManagersGroupName.ToLower() == GroupName.ToLower())
                {
                    newQuery = @"<Where><And><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+ (int)Common.Utilities.RequestStatus.SCERejectedRecommendationDepartmentManager + @"</Value></Eq>
                                 <Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>"+ SPContext.Current.Web.CurrentUser.LoginName + @"</Value></Eq>
                                  <And><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>"+ GroupName.Trim() + @"</Value></Eq>
                                  <Or><IsNull><FieldRef Name='DelayedDays' /></IsNull><Lt><FieldRef Name='DelayedDays' /><Value Type='Number'>"+delayedDays+ @"</Value></Lt></Or></And></Or></And></Where>
                                 <OrderBy><FieldRef Name = 'RecievedDate' Ascending = 'False' /></OrderBy >";
                }
                //  Equivalence Employees Query
                else if (Common.Utilities.Constants.SCEEquivalenceEmployeesGroupName.ToLower() == GroupName.ToLower())//|| Common.Utilities.Constants.EuropeanProgEmployeeGroupName.ToLower() == SPGroupName.ToLower())
                {

                    newQuery = @"<Where><And><And><And><Eq><FieldRef Name='IsEmployee' /><Value Type='Choice'>No</Value></Eq>" +
                    "<Or><IsNull><FieldRef Name='DelayedDays' /></IsNull>" +
                    "<Lt><FieldRef Name='DelayedDays' /><Value Type='Number'>" + delayedDays + "</Value></Lt>" +
                    "</Or></And><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCESubmitted +
                    "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEEquivalenceEmployeeReassign +
                    "</Value></Eq><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCECulturalMissionNeedsStatement +
                    "</Value></Eq></Or></Or></And><Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName +
                    "</Value></Eq><Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim() +
                    "</Value></Eq><Eq><FieldRef Name='EmployeeForCultural' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName + "</Value></Eq></Or></Or></And></Where>" +
                     "<OrderBy><FieldRef Name='RecievedDate' Ascending='False' /></OrderBy>";



                    /*
                    newQuery = @"<Where><And><Eq><FieldRef Name='IsEmployee' /><Value Type='Choice'>No</Value></Eq>
                              <Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCESubmitted +
                              "</Value></Eq><And><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEEquivalenceEmployeeReassign +
                              "</Value></Eq><Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName +
                              "</Value></Eq><And><Neq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim() +
                              "</Value></Neq><Or><IsNull><FieldRef Name='DelayedDays' /></IsNull>" +
                              "<Lt><FieldRef Name='DelayedDays' /><Value Type='Number'>" + delayedDays + "</Value></Lt></Or></And></Or></And></Or></And></Where>" +
                              "<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";




                    newQuery = @"<Where><And><And><And><Eq><FieldRef Name='IsEmployee' /><Value Type='Choice'>No</Value></Eq>"+
                            "<Or><IsNull><FieldRef Name='DelayedDays' /></IsNull>"+
                           "<Lt><FieldRef Name='DelayedDays' /><Value Type='Number'>" + delayedDays + "</Value></Lt>"+
                           "</Or></And><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCESubmitted +
                           "</Value></Eq><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEEquivalenceEmployeeReassign +
                           "</Value></Eq></Or></And><Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName +
                           "</Value></Eq><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim() +
                           "</Value></Eq></Or></And></Where><OrderBy><FieldRef Name='RecievedDate' Ascending='False' /></OrderBy>&"+

                     @"<Where><And><And><And><Eq><FieldRef Name='IsEmployee' /><Value Type='Choice'>No</Value></Eq>"+
                      "<Or><IsNull><FieldRef Name='DelayedDays' /></IsNull>"+
                     "<Lt><FieldRef Name='DelayedDays' /><Value Type='Number'>"+delayedDays+
                     "</Value></Lt></Or></And><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+(int)Common.Utilities.RequestStatus.SCECulturalMissionNeedsStatement +
                     "</Value></Eq></And><Eq><FieldRef Name='ReturnedBy' /><Value Type='Text'>"+SPContext.Current.Web.CurrentUser.Name+"</Value></Eq></And></Where>" +
                     "<OrderBy><FieldRef Name='RecievedDate' Ascending='False' /></OrderBy>";

    */





                }
                //Section Manager Query
                else if (Common.Utilities.Constants.SCESectionManagers.ToLower() == GroupName.ToLower())
                {

                   // newQuery = @"<Where><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCESectionManagerAccepted +
                   //@"</Value></Eq><And><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCESectionManagerRejected +
                   //@"</Value></Eq><Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName +
                   //@"</Value></Eq><And><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim() +
                   //@"</Value></Eq><Or><IsNull><FieldRef Name='DelayedDays' /></IsNull><Lt><FieldRef Name='DelayedDays' /><Value Type='Number'>" + delayedDays + "</Value></Lt></Or></And></Or></And></Or></Where>" +
                   //@"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";


                    //newQuery = @"<Where><And><And><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+ (int)Common.Utilities.RequestStatus.SCEAcceptedRecommendation +
                    //    "</Value></Eq><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+ (int)Common.Utilities.RequestStatus.SCERejectedRecommendationSectionManager + 
                    //    "</Value></Eq></Or><Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName +
                    //    "</Value></Eq><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>"+GroupName.Trim()+
                    //    "</Value></Eq></Or></And><Or><IsNull><FieldRef Name='DelayedDays' /></IsNull>" +
                    //    "<Eq><FieldRef Name='DelayedDays' /><Value Type='Number'>"+delayedDays+
                    //    "</Value></Eq></Or></And></Where><OrderBy><FieldRef Name='RecievedDate' Ascending='False' /></OrderBy>";

                    newQuery = @"<Where><And><And><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+(int)Common.Utilities.RequestStatus.SCEAcceptedRecommendation +"</Value></Eq>"+
                                 "<Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+ (int)Common.Utilities.RequestStatus.SCERejectedRecommendationSectionManager+"</Value></Eq>" +
                                 "<Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+(int)Common.Utilities.RequestStatus.SCEDepartmentManagerRejectDecision + "</Value></Eq>" +
                                 "<Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" +(int)Common.Utilities.RequestStatus.SCEDepartmentManagerAcceptDecision 
                                 + "</Value></Eq></Or></Or></Or><Or>" +
                                 "<Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>"+ SPContext.Current.Web.CurrentUser.LoginName+
                                 "</Value></Eq><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>"+ GroupName.Trim() + 
                                 "</Value></Eq></Or></And><Or><IsNull><FieldRef Name='DelayedDays' /></IsNull><Lt>" +
                                 "<FieldRef Name='DelayedDays' /><Value Type='Number'>"+delayedDays+"</Value></Lt></Or></And></Where>"+
                                 "<OrderBy><FieldRef Name='RecievedDate' Ascending='False' /></OrderBy>"; 


                }
                //Cultural Mission Query
                else if (Common.Utilities.Constants.SCECulturalMissionBritainGroupName.ToLower() == GroupName.ToLower() ||
                     Common.Utilities.Constants.SCECulturalMissionAustraliaGroupName.ToLower() == GroupName.ToLower() ||
                     Common.Utilities.Constants.SCECulturalMissionFranceGroupName.ToLower() == GroupName.ToLower() ||
                     Common.Utilities.Constants.SCECulturalMissionUSAGroupName.ToLower() == GroupName.ToLower() ||
                     Common.Utilities.Constants.SCECulturalMissionCanadaGroupName.ToLower() == GroupName.ToLower() ||
                     Common.Utilities.Constants.SCECulturalMissionJordanGroupName.ToLower() == GroupName.ToLower())
                {
                    //newQuery = @"<Where><And><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCECulturalMissionNeedsStatement + @"</Value></Eq>
                    //             <Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName + @"</Value></Eq>
                    //              <And><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim() + @"</Value></Eq>
                    //              <Or><IsNull><FieldRef Name='DelayedDays' /></IsNull><Lt><FieldRef Name='DelayedDays' /><Value Type='Number'>" + delayedDays + @"</Value></Lt></Or></And></Or></And></Where>
                    //             <OrderBy><FieldRef Name = 'Created' Ascending = 'False' /></OrderBy >";

                   newQuery= @"<Where><And><And><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+ (int)Common.Utilities.RequestStatus.SCECulturalMissionNeedsStatement +
                        "</Value></Eq><Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>"+ SPContext.Current.Web.CurrentUser.LoginName + 
                        "</Value></Eq><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>"+GroupName.Trim()
                        +"</Value></Eq></Or></And><Or><IsNull><FieldRef Name='DelayedDays' /></IsNull><Lt>" +
                        "<FieldRef Name='DelayedDays' /><Value Type='Number'>"+delayedDays+"</Value></Lt>" +
                        "</Or></And></Where><OrderBy><FieldRef Name='RecievedDate' Ascending='False' /></OrderBy>";
                }
                //Higher Educational Institutions Query
                else if (Common.Utilities.Constants.SCEHigherEducationalInstitutionsGroupName.ToLower() == GroupName.ToLower())
                {
                    //newQuery = @"<Where><And><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEExternalCommunicationAddBook + @"</Value></Eq>
                    //             <Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName + @"</Value></Eq>
                    //              <And><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim() + @"</Value></Eq>
                    //              <Or><IsNull><FieldRef Name='DelayedDays' /></IsNull><Lt><FieldRef Name='DelayedDays' /><Value Type='Number'>" + delayedDays + @"</Value></Lt></Or></And></Or></And></Where>
                    //             <OrderBy><FieldRef Name = 'Created' Ascending = 'False' /></OrderBy >";


                    newQuery = @"<Where><And><And><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEExternalCommunicationAddBook +
                       "</Value></Eq><Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName +
                       "</Value></Eq><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim()
                       + "</Value></Eq></Or></And><Or><IsNull><FieldRef Name='DelayedDays' /></IsNull><Lt>" +
                       "<FieldRef Name='DelayedDays' /><Value Type='Number'>" + delayedDays + "</Value></Lt>" +
                       "</Or></And></Where><OrderBy><FieldRef Name='RecievedDate' Ascending='False' /></OrderBy>";
                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewRequests.GetNewRequestsQuery");
            }
            return newQuery;
        }
        public static void CheckOutRequest(string ReqID)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        Logging.GetInstance().Debug("Enter NewRequests.CheckOutRequest");
                        try
                        {
                            SPList list = web.Lists[Utilities.Constants.SCERequests];
                            SPListItem newitem = list.Items.GetItemById(Convert.ToInt32(ReqID));
                            SPFieldLookupValue value = new SPFieldLookupValue((int)Common.Utilities.RequestStatus.PAReceptionistReviewInformation, ((int)Common.Utilities.RequestStatus.PAReceptionistReviewInformation).ToString());
                            newitem["RequestStatus"] = Common.Utilities.BusinessHelper.GetLookupFieldFromValue(((int)Common.Utilities.RequestStatus.PAReceptionistReviewInformation).ToString(), "ID", Utilities.Constants.RequestStatus);
                            newitem["DelayedDays"] = "0";
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
                            Logging.GetInstance().Debug("Exit NewRequests.CheckOutRequest");
                        }
                    }
                }
            });
        }

        public static void AssignTo(string id, string AssignTo)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            List<string> reqIDstr = new List<string>();
                            Logging.GetInstance().Debug("Entering method NewRequests.AssignTo");
                            SPList list = web.Lists[Utilities.Constants.SCERequests];
                            SPListItem newitem = list.Items.GetItemById(Convert.ToInt32(id));
                            newitem["EmployeeAssignedTo"] = AssignTo;
                            //newitem["RequestStatus"] = new SPFieldLookupValue((int)Common.Utilities.RequestStatus.PAProgramEmployeeReview, ((int)Common.Utilities.RequestStatus.PAProgramEmployeeReview).ToString());
                            newitem["DelayedDays"] = "0";
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
                            Logging.GetInstance().Debug("Exiting method NewRequests.AssignTo");
                        }
                    }
                }
            });
        }

        public static string GetstrViewFields(int LCID)
        {
            string strViewFields = string.Empty;
            if (LCID == (int)Language.English)
            {
                strViewFields = "<FieldRef Name='RequestNumber'/>"
                      + "<FieldRef Name='Applicants_QatarID'/>"
                       + "<FieldRef Name='Applicants_QID'/>"
                      + "<FieldRef Name='Applicants_ApplicantName'/>"
                      + "<FieldRef Name='Applicants_EnglishName'/>"
                      + "<FieldRef Name='Applicants_MobileNumber'/>"
                      + "<FieldRef Name='RecievedDate'/>"
                      + "<FieldRef Name='Nationality_Title'/>"
                     
                      + "<FieldRef Name='OtherCertificateResource'/>"
                        + "<FieldRef Name='StdPrintedName'/>"
                         + "<FieldRef Name='StdQatarID'/>"
                          + "<FieldRef Name='PrevSchool'/>"

                      + "<FieldRef Name='ID'/>"

                      + "<FieldRef Name='RequestStatus'/>"

                      + "<FieldRef Name='RequestStatusId'/>"

                     + "<FieldRef Name='EmployeeAssignedTo'/>"
                     + "<FieldRef Name='DelayedDays'/>"
                      + "<FieldRef Name='CountryOfStudy_Title'/>"
                      + "<FieldRef Name='ScholasticLevel_Title'/>"
                +"<FieldRef Name='MobileNumber'/>"
                 + "<FieldRef Name='EmployeeForCultural'/>";
                

            }
            else
            {
                strViewFields = "<FieldRef Name='RequestNumber'/>"
                     + "<FieldRef Name='Applicants_QatarID'/>"
                      + "<FieldRef Name='Applicants_QID'/>"
                     + "<FieldRef Name='Applicants_ApplicantName'/>"
                      + "<FieldRef Name='Applicants_ArabicName'/>"
                      + "<FieldRef Name='Applicants_MobileNumber'/>"
                     + "<FieldRef Name='RecievedDate'/>"
                     + "<FieldRef Name='Nationality_TitleAr'/>"
                  
                     + "<FieldRef Name='OtherCertificateResource'/>"
                     + "<FieldRef Name='StdPrintedName'/>"
                      + "<FieldRef Name='StdQatarID'/>"
                          + "<FieldRef Name='PrevSchool'/>"
                    
                     + "<FieldRef Name='ID'/>"
                     
                     + "<FieldRef Name='RequestStatusAr'/>"
                     + "<FieldRef Name='EmployeeAssignedTo'/>"
                     
                     + "<FieldRef Name='RequestStatusId'/>"
                    
                +"<FieldRef Name='DelayedDays'/>"
                +"<FieldRef Name='CountryOfStudy_TitleAr'/>"
                 + "<FieldRef Name='ScholasticLevel_TitleAr'/>"
                    + "<FieldRef Name='MobileNumber'/>"
                     + "<FieldRef Name='EmployeeForCultural'/>";

            }
            return strViewFields;
        }

        public static List<Entities.NewRequests> GetRequestsListing(SPQuery query, int LCID)
        {
            List<Entities.NewRequests> Requests = new List<Entities.NewRequests>();
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method NewRequests.GetRequestsListing");
                        SPList requestsList = web.Lists[Utilities.Constants.SCERequests];
                        SPListItemCollection items = requestsList.GetItems(query);
                        Common.Entities.RequestStatus requestStatusItem = new Common.Entities.RequestStatus();
                        foreach (SPListItem item in items)
                        {
                            requestStatusItem = Common.BL.RequestStatus.GetRequestStatusById(new SPFieldLookupValue(item["RequestStatusId"].ToString()).LookupId);
                            Entities.NewRequests Request = new Entities.NewRequests();
                            SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue((item["Applicants_QatarID"] != null) ? item["Applicants_QatarID"].ToString() : string.Empty);
                            Request.QatariID = ApplicantsQatarID.LookupValue;

                            //SPFieldLookupValue ApplicantsMobileNumber = new SPFieldLookupValue((item["Applicants_MobileNumber"] != null) ? item["Applicants_MobileNumber"].ToString() : string.Empty);
                            //Request.ApplicantMobileNumber = ApplicantsMobileNumber.LookupValue;

                            Request.ApplicantMobileNumber = item["MobileNumber"] != null ? Convert.ToString(item["MobileNumber"]) : string.Empty;


                            Request.DelayedDays = (item["DelayedDays"] != null) ? item["DelayedDays"].ToString() : string.Empty;
                            Request.AssignedTo = (item["EmployeeAssignedTo"] != null) ? item["EmployeeAssignedTo"].ToString() : string.Empty;
                            Request.ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;
                            Request.RequestNumber = (item["RequestNumber"] != null) ? item["RequestNumber"].ToString() : string.Empty;
                          
                            Request.RecievedDate = (item["RecievedDate"] != null) ? DateTime.Parse(item["RecievedDate"].ToString()) : DateTime.MinValue;

                            SPFieldLookupValue StatusId = new SPFieldLookupValue((item["RequestStatusId"] != null) ? item["RequestStatusId"].ToString() : string.Empty);
                            Request.RequestStatusId = StatusId.LookupId;
                            if ((Request.RequestStatusId == (int)Common.Utilities.RequestStatus.SCESectionManagerAccepted) || (Request.RequestStatusId == (int)Common.Utilities.RequestStatus.SCESectionManagerRejected))
                                Request.IsRequestClosed = true;
                            else
                                Request.IsRequestClosed = false;

                            SPFieldLookupValue ApplicantsID = new SPFieldLookupValue((item["Applicants_QID"] != null) ? item["Applicants_QID"].ToString() : string.Empty);
                            Request.QID = ApplicantsID.LookupValue;
                           
                            Request.StudentNameAccToCert = item["StdPrintedName"] != null ?  Convert.ToString(item["StdPrintedName"]):string.Empty;
                           
                            Request.CertificateHolderQatarID = item["StdQatarID"] != null ? Convert.ToString(item["StdQatarID"]) : string.Empty;
                            
                            if (LCID == (int)Language.English)
                            {
                                SPFieldLookupValue NationalityTitle = new SPFieldLookupValue((item["Nationality_Title"] != null) ? item["Nationality_Title"].ToString() : string.Empty);
                                Request.Nationality = NationalityTitle.LookupValue;
                                SPFieldLookupValue RequestStatus = new SPFieldLookupValue((item["RequestStatus"] != null) ? item["RequestStatus"].ToString() : string.Empty);
                                Request.RequestStatus = RequestStatus.LookupValue;

                                Request.RequestStatus = requestStatusItem.ReviewerDescriptionEn;
                                SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                                Request.ApplicantName = ApplicantsEnglishName.LookupValue;

                              
                                if (item["CountryOfStudy_Title"] != null)
                                {
                                    SPFieldLookupValue CertificateResource = new SPFieldLookupValue(item["CountryOfStudy_Title"].ToString());
                                    Request.CertificateResource = CertificateResource.LookupValue;
                                }

                               else if (item["OtherCertificateResource"] != null)
                                {
                                    Request.CertificateResource =Convert.ToString(item["OtherCertificateResource"]);
                                
                                }

                                if (item["ScholasticLevel_Title"] != null)
                                {
                                    SPFieldLookupValue SchoolLastGrade = new SPFieldLookupValue(item["ScholasticLevel_Title"].ToString());
                                    Request.SchoolLastGrade = SchoolLastGrade.LookupValue;
                                }
                            }
                            else
                            {
                                SPFieldLookupValue NationalityTitleAr = new SPFieldLookupValue((item["Nationality_TitleAr"] != null) ? item["Nationality_TitleAr"].ToString() : string.Empty);
                                Request.Nationality = NationalityTitleAr.LookupValue;

                               
                                SPFieldLookupValue RequestStatus = new SPFieldLookupValue((item["RequestStatusAr"] != null) ? item["RequestStatusAr"].ToString() : string.Empty);
                                Request.RequestStatus = RequestStatus.LookupValue;

                                Request.RequestStatus = requestStatusItem.ReviewerDescriptionAr;


                                SPFieldLookupValue ApplicantsArabicName = new SPFieldLookupValue((item["Applicants_ArabicName"] != null) ? item["Applicants_ArabicName"].ToString() : string.Empty);
                                Request.ApplicantName = ApplicantsArabicName.LookupValue;
                                
                                if (item["CountryOfStudy_TitleAr"] != null)
                                {
                                    SPFieldLookupValue CertificateResource = new SPFieldLookupValue(item["CountryOfStudy_TitleAr"].ToString());
                                    Request.CertificateResource = CertificateResource.LookupValue;
                                }
                                else if (item["OtherCertificateResource"] != null)
                                {
                                    Request.CertificateResource = Convert.ToString(item["OtherCertificateResource"]);

                                }

                                if (item["ScholasticLevel_TitleAr"] != null)
                                {
                                    SPFieldLookupValue SchoolLastGrade = new SPFieldLookupValue(item["ScholasticLevel_TitleAr"].ToString());
                                    Request.SchoolLastGrade = SchoolLastGrade.LookupValue;
                                }

                            }
                            Requests.Add(Request);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(ex);
                    }
                    finally
                    {
                        web.AllowUnsafeUpdates = false;
                        Logging.GetInstance().Debug("Exit NewRequests.GetRequestsListing");
                    }
                    return Requests;
                }
            }
        }
    }
}
