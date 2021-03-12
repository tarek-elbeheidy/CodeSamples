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
    public class ExceptionRequests
    {
        public static IEnumerable<Entities.ExceptionRequests> GetAllExceptionRequests(List<string> strQuery, int LCID)
        {
            List<Entities.ExceptionRequests> Requests = new List<Entities.ExceptionRequests>();
            using (SPSite oSite = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb oWeb = oSite.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method ExceptionRequests.GetAllExceptionRequests");
                        oWeb.AllowUnsafeUpdates = true;
                        string strViewFields = GetstrViewFields(LCID);

                        string joins =
                                        "<Join Type='LEFT' ListAlias='Nationality'>" +
                                                      "<Eq>" +
                                                      "<FieldRef Name='StdNationality' RefType='Id'/>" +
                                                      "<FieldRef List='Nationality' Name='ID'/>" +
                                                      "</Eq>" +
                                                      "</Join>" +
                                        //"<Join Type='LEFT' ListAlias='NationalityCategory'>" +
                                        //              "<Eq>" +
                                        //              "<FieldRef Name='StdNationalityCat' RefType='Id'/>" +
                                        //              "<FieldRef List='NationalityCategory' Name='ID'/>" +
                                        //              "</Eq>" +
                                        //              "</Join>" +

                                        "<Join Type='LEFT' ListAlias='CountryOfStudy'>" +
                                      "<Eq>" +
                                        "<FieldRef  Name='CertificateResource' RefType='Id' />" +
                                        "<FieldRef List='CountryOfStudy' Name='ID' />" +
                                      "</Eq></Join>" +

                                        "<Join Type='LEFT' ListAlias='ScholasticLevel'>" +
                                      "<Eq>" +
                                        "<FieldRef  Name='LastScholasticLevel' RefType='Id' />" +
                                        "<FieldRef List='ScholasticLevel' Name='ID' />" +
                                      "</Eq></Join>";





                        string projFields =
                                                "<Field Name='Nationality_Title' Type='Lookup' " +
                                                "List='Nationality' ShowField='Title'/>" +
                                                "<Field Name='Nationality_TitleAr' Type='Lookup' " +
                                                "List='Nationality' ShowField='TitleAr'/>" +
                                                 //+ "<Field Name='NationalityCategory_Title' Type='Lookup' " +
                                                 //"List='NationalityCategory' ShowField='Title'/>" +
                                                 //"<Field Name='NationalityCategory_TitleAr' Type='Lookup' " +
                                                 //"List='NationalityCategory' ShowField='TitleAr'/>" +

                                                 "<Field Name='CountryOfStudy_Title' Type='Lookup' " +
                                                "List='CountryOfStudy' ShowField='Title'/>" +

                                                "<Field Name='CountryOfStudy_TitleAr' Type='Lookup' " +
                                                "List='CountryOfStudy' ShowField='TitleAr'/>" +



                                                 "<Field Name='ScholasticLevel_Title' Type='Lookup' " +
                                                "List='ScholasticLevel' ShowField='Title'/>" +

                                                "<Field Name='ScholasticLevel_TitleAr' Type='Lookup' " +
                                                "List='ScholasticLevel' ShowField='TitleAr'/>";




                        SPQuery reqQuery = Common.Utilities.BusinessHelper.GetQueryObject(strQuery[0], joins, projFields, strViewFields);
                        Requests.AddRange(GetRequestsListing(reqQuery, LCID));



                        SPQuery draftQuery = Common.Utilities.BusinessHelper.GetQueryObject(strQuery[1], joins, projFields, strViewFields);
                        Requests.AddRange(GetRequestsListing(draftQuery, LCID));



                        string clarificationJoins = @"<Join Type='INNER' ListAlias='SCERequests'>
                                      <Eq>
                                        <FieldRef Name='RequestID' RefType='Id' />
                                        <FieldRef List='SCERequests' Name='Id' />
                                      </Eq>
                                    </Join>
    <Join Type='LEFT' ListAlias='RequestStatus'>
                                      <Eq>
                                        <FieldRef List='SCERequests' Name='RequestStatus' RefType='Id' />
                                        <FieldRef List='RequestStatus' Name='Id' />
                                      </Eq>
                                    </Join>
<Join Type='LEFT' ListAlias='CountryOfStudy'>
                                      <Eq>
                                        <FieldRef List='SCERequests' Name='CertificateResource' RefType='Id' />
                                        <FieldRef List='CountryOfStudy' Name='Id' />
                                      </Eq>
                                    </Join>" +
     "<Join Type='LEFT' ListAlias='ScholasticLevel'>" +
                                          "<Eq>" +
                                            "<FieldRef  List='SCERequests' Name='LastScholasticLevel' RefType='Id' />" +
                                            "<FieldRef List='ScholasticLevel' Name='Id' />" +
                                          "</Eq></Join>" +
                                        //"<Join Type='LEFT' ListAlias='Applicants'>" +
                                        //                                      "<Eq>" +
                                        //                                        "<FieldRef List='SCERequests' Name='ApplicantID' RefType='Id' />" +
                                        //                                        "<FieldRef List='Applicants' Name='Id' />" +
                                        //                                      "</Eq>" +
                                        //                                    "</Join>" +
                                        "<Join Type='LEFT' ListAlias='Nationality'>" +
                                          "<Eq>" +
                                           "<FieldRef List='SCERequests' Name='StdNationality' RefType='Id' />" +
                                            "<FieldRef List='Nationality' Name='Id' />" +
                                          "</Eq>" +
                                        "</Join>";

                        string clarificationProjectedFields =

                                              "<Field Name='SCERequests_RequestNumber' Type='Lookup' "
                        + "List='SCERequests' ShowField='RequestNumber'/>"
                       + "<Field Name='SCERequests_RecievedDate' Type='Lookup' "
                        + "List='SCERequests' ShowField='RecievedDate'/>"
                        + "<Field Name='SCERequests_ID' Type='Lookup' "
                        + "List='SCERequests' ShowField='ID'/>"
                        + "<Field Name='SCERequests_OtherCertificateResource' Type='Lookup' "
                        + "List='SCERequests' ShowField='OtherCertificateResource'/>"


                        + "<Field Name='SCERequests_StdPrintedName' Type='Lookup' "
                        + "List='SCERequests' ShowField='StdPrintedName'/>"
                         + "<Field Name='SCERequests_ApplicantOfficialName' Type='Lookup' "
                        + "List='SCERequests' ShowField='ApplicantOfficialName'/>"


                         + "<Field Name='SCERequests_StdQatarID' Type='Lookup' "
                        + "List='SCERequests' ShowField='StdQatarID'/>" +
                         "<Field Name='SCERequests_EmployeeAssignedTo' Type='Lookup' "
                        + "List='SCERequests' ShowField='EmployeeAssignedTo'/>"
                        //+ "<Field Name='SCERequests_IsEmployee' Type='Lookup' "
                        //+ "List='SCERequests' ShowField='IsEmployee'/>"
                          + "<Field Name='SCERequests_StdName' Type='Lookup' "
                        + "List='SCERequests' ShowField='StdName'/>"

                         + "<Field Name='SCERequests_DelayedDays' Type='Lookup' "
                        + "List='SCERequests' ShowField='DelayedDays'/>"

                       //+ "<Field Name='SCERequests_ReturnedBy' Type='Lookup' "
                       //+ "List='SCERequests' ShowField='ReturnedBy'/>"

                       + "<Field Name='RequestStatus_Code' Type='Lookup' "
                        + "List='RequestStatus' ShowField='Code'/>"

                         + "<Field Name='RequestStatus_ReviewerDescriptionAr' Type='Lookup' "
                        + "List='RequestStatus' ShowField='ReviewerDescriptionAr'/>"
                         + "<Field Name='RequestStatus_ReviewerDescriptionEn' Type='Lookup' "
                        + "List='RequestStatus' ShowField='ReviewerDescriptionEn'/>"

                          + "<Field Name='RequestStatus_ID' Type='Lookup' "
                        + "List='RequestStatus' ShowField='ID'/>"

                        + "<Field Name='CountryOfStudy_Title' Type='Lookup' "
                        + "List='CountryOfStudy' ShowField='Title'/>"
                        + "<Field Name='CountryOfStudy_TitleAr' Type='Lookup' "
                        + "List='CountryOfStudy' ShowField='TitleAr'/>" +

                        //   "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                        //                            "List='Applicants' ShowField='ApplicantName'/>"
                        //+ "<Field Name='Applicants_ArabicName' Type='Lookup' " +
                        //                            "List='Applicants' ShowField='ArabicName'/>" +
                        //                            "<Field Name='Applicants_EnglishName' Type='Lookup' " +
                        //                            "List='Applicants' ShowField='EnglishName'/>"
                        //+ "<Field Name='Applicants_QatarID' Type='Lookup' "
                        //+ "List='Applicants' ShowField='PersonalID'/>"
                        // + "<Field Name='Applicants_QID' Type='Lookup' "
                        // + "List='Applicants' ShowField='ID'/>" +
                        //  "<Field Name='Applicants_MobileNumber' Type='Lookup' "
                        //    + "List='Applicants' ShowField='MobileNumber'/>"

                         "<Field Name='Nationality_Title' Type='Lookup' "
                        + "List='Nationality' ShowField='Title'/>" +
                         "<Field Name='Nationality_TitleAr' Type='Lookup' " +
                                                "List='Nationality' ShowField='TitleAr'/>"

                                                  + "<Field Name='ScholasticLevel_Title' Type='Lookup' "
                        + "List='ScholasticLevel' ShowField='Title'/>" +
                         "<Field Name='ScholasticLevel_TitleAr' Type='Lookup' " +
                                                "List='ScholasticLevel' ShowField='TitleAr'/>";


                        string clarViewFields = GetClarificationsViewFields(LCID);
                        SPQuery reqClarQuery = Common.Utilities.BusinessHelper.GetQueryObject(strQuery[2], clarificationJoins, clarificationProjectedFields, clarViewFields);


                        Requests.AddRange(GetAllClarificationRequests(reqClarQuery, LCID));

                       
                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(ex);
                    }
                    finally
                    {
                        oWeb.AllowUnsafeUpdates = false;
                        Logging.GetInstance().Debug("Exiting method ExceptionRequests.GetAllExceptionRequests");
                    }
                }
            }
            return Requests;
        }

        public static List<string> GetNewQueryPerRole(string SPGroupName)
        {
            List<string> objColumns = new List<string>();
            string strQuery = string.Empty;

            //Department Manager Query
            if (Common.Utilities.Constants.SCEDepartmentManagersGroupName.ToLower() == SPGroupName.ToLower())
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.SCEDepartmentManagerReviewRecommendation);
            }

            //  Equivalence Employees Query
            else if (Common.Utilities.Constants.SCEEquivalenceEmployeesGroupName.ToLower() == SPGroupName.ToLower())//|| Common.Utilities.Constants.EuropeanProgEmployeeGroupName.ToLower() == SPGroupName.ToLower())
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.SCESubmitted);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.SCEEquivalenceEmployeeReassign);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.SCEDraft);

            }
            //Section Manager Query
            else if (Common.Utilities.Constants.SCESectionManagers.ToLower() == SPGroupName.ToLower())
            {
                // objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.SCEEquivalencemployeeReturnRequest);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.SCESectionManagerAccepted);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.SCESectionManagerRejected);
            }

            //Cultural Mission Query
            else if (Common.Utilities.Constants.SCECulturalMissionBritainGroupName.ToLower() == SPGroupName.ToLower() ||
                 Common.Utilities.Constants.SCECulturalMissionAustraliaGroupName.ToLower() == SPGroupName.ToLower() ||
                 Common.Utilities.Constants.SCECulturalMissionFranceGroupName.ToLower() == SPGroupName.ToLower() ||
                 Common.Utilities.Constants.SCECulturalMissionUSAGroupName.ToLower() == SPGroupName.ToLower() ||
                 Common.Utilities.Constants.SCECulturalMissionCanadaGroupName.ToLower() == SPGroupName.ToLower() ||
                 Common.Utilities.Constants.SCECulturalMissionJordanGroupName.ToLower() == SPGroupName.ToLower())
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.SCECulturalMissionNeedsStatement);
            }


            //Higher Educational Institutions Query
            else if (Common.Utilities.Constants.SCEHigherEducationalInstitutionsGroupName.ToLower() == SPGroupName.ToLower())
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.SCEExternalCommunicationAddBook);
            }



            return objColumns;
        }

        public static List<string> GetExceptionRequestsQuery(string GroupName)
        {
            Logging.GetInstance().Debug("Enter ExceptionRequests.GetExceptionRequestsQuery");
            string newQuery = string.Empty;
            string draftQuery = string.Empty;
            string clarificationQuery = string.Empty;
            List<string> queriesStrings = new List<string>();
            try
            {
                //  Equivalence Employees Query 
                if (Common.Utilities.Constants.SCEEquivalenceEmployeesGroupName.ToLower() == GroupName.ToLower())//|| Common.Utilities.Constants.EuropeanProgEmployeeGroupName.ToLower() == SPGroupName.ToLower())
                {

                    //All cases except clarification and draft




                    newQuery = @"<Where><And><And><Eq><FieldRef Name='IsEmployee' /><Value Type='Choice'>Yes</Value></Eq>" +
                        "<Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCESubmitted +
                        "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEEquivalenceEmployeeReassign +
                        "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEEmployeeClarificationReply +
                        "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCESectionManagerMissingInformation +
                        "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEDepartmentManagerMissingRecommendation +
                        "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCECulturalMissionStatementReply +
                        "</Value></Eq><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCECulturalMissionNeedsStatement +
                        "</Value></Eq></Or></Or></Or></Or></Or></Or></And><Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName +
                        "</Value></Eq><Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim() +
                        "</Value></Eq><Eq><FieldRef Name='EmployeeForCultural' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName + "</Value></Eq></Or></Or></And></Where>" +
                        "<OrderBy><FieldRef Name='RecievedDate' Ascending='False' /></OrderBy>";






                    /*  newQuery = @"<Where><And><And><Eq><FieldRef Name='IsEmployee' /><Value Type='Choice'>Yes</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCESubmitted +
                          "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEEquivalenceEmployeeReassign +
                          "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEEmployeeClarificationReply +
                          "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCESectionManagerMissingInformation +
                          "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEDepartmentManagerMissingRecommendation +
                          "</Value></Eq><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCECulturalMissionStatementReply +
                          "</Value></Eq></Or></Or></Or></Or></Or></And><Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName +
                          "</Value></Eq><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim() +
                          "</Value></Eq></Or></And></Where><OrderBy><FieldRef Name='RecievedDate' Ascending='False' /></OrderBy>";
                          */






                    //Drat requests
                    draftQuery = @"<Where><And><And><Eq><FieldRef Name='IsEmployee' /><Value Type='Choice'>Yes</Value></Eq><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+ (int)Common.Utilities.RequestStatus.SCEDraft+
                        "</Value></Eq></And><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>"+ SPContext.Current.Web.CurrentUser.LoginName + "</Value></Eq></And></Where>" +
                        "<OrderBy><FieldRef Name='RecievedDate' Ascending='False' /></OrderBy>";

                    //Clarification requests 

                    SPWeb web = new SPSite(SPContext.Current.Site.Url).RootWeb;
                    SPUser user = web.EnsureUser(SPContext.Current.Web.CurrentUser.LoginName);//This will add user to site if not already added.


                    
                    clarificationQuery = @"<Where><And><And><And><IsNull><FieldRef Name='ClarificationReply' /></IsNull>
            <Eq><FieldRef Name='Sender' LookupId='True' /><Value Type='User'>" + user.ID +
       "</Value></Eq></And><Eq><FieldRef Name='RequestStatus_Code' /><Value Type='Text'>" + Common.Utilities.RequestStatus.SCEEmployeeNeedsClarification +
                   "</Value></Eq></And><Or><Eq><FieldRef Name='SCERequests_EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName +
                   "</Value></Eq><Eq><FieldRef Name='SCERequests_EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim() + "</Value></Eq>" +
                   "</Or></And></Where><OrderBy><FieldRef Name='SCERequests_RecievedDate' Ascending='False' /></OrderBy>";





                    queriesStrings.Add(newQuery);
                    queriesStrings.Add(draftQuery);
                    queriesStrings.Add(clarificationQuery);









                    /*
                    newQuery = @"<Where><And><And><And><Eq><FieldRef Name='IsEmployee' /><Value Type='Choice'>Yes</Value></Eq>
                                 <Or><IsNull><FieldRef Name='DelayedDays' /></IsNull>
                                 <Eq><FieldRef Name='DelayedDays' /><Value Type='Number'>"+delayedDays+
                                 "</Value></Eq></Or></And>"
                                 + "<Or><Or><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCESubmitted +
                                 "</Value></Eq><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEDraft +
                                 "</Value></Eq></Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEEmployeeNeedsClarification +
                                 "</Value></Eq></Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEEmployeeClarificationReply +
                                 "</Value></Eq></Or></And><Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName + 
                                 "</Value></Eq><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>"+GroupName.Trim()+
                                 "</Value></Eq></Or></And></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                          */
                    //newQuery = @"<Where><And><And><And><Eq><FieldRef Name='IsEmployee' /><Value Type='Choice'>Yes</Value></Eq><Or><IsNull><FieldRef Name='DelayedDays' /></IsNull><Lt><FieldRef Name='DelayedDays' /><Value Type='Number'>"+delayedDays+
                    //    "</Value></Lt></Or></And><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+ (int)Common.Utilities.RequestStatus.SCESubmitted +
                    //    "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+ (int)Common.Utilities.RequestStatus.SCEDraft+
                    //    "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+(int)Common.Utilities.RequestStatus.SCEEquivalenceEmployeeReassign + 
                    //    "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+(int) Common.Utilities.RequestStatus.SCEEmployeeNeedsClarification + 
                    //    "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+ (int)Common.Utilities.RequestStatus.SCESectionManagerMissingInformation + 
                    //    "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+ (int)Common.Utilities.RequestStatus.SCEDepartmentManagerMissingRecommendation +
                    //    "</Value></Eq><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+ (int)Common.Utilities.RequestStatus.SCECulturalMissionStatementReply +
                    //    "</Value></Eq></Or></Or></Or></Or></Or></Or></And><Or><Eq>" +
                    //    "<FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>"+ SPContext.Current.Web.CurrentUser.LoginName +
                    //    "</Value></Eq><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>"+GroupName.Trim() +
                    //    "</Value></Eq></Or></And></Where><OrderBy><FieldRef Name='RecievedDate' Ascending='False' /></OrderBy>";

                    //clarificationQuery = @"<Where><And><And><And><Eq><FieldRef Name='SCERequests_IsEmployee' /><Value Type='Choice'>Yes</Value></Eq><Or><IsNull><FieldRef Name='SCERequests_DelayedDays' /></IsNull><Lt><FieldRef Name='SCERequests_DelayedDays' /><Value Type='Number'>" + delayedDays+
                    //    "</Value></Lt></Or></And><Eq><FieldRef Name='RequestStatus_Code' /><Value Type='Lookup'>"+ (int)Common.Utilities.RequestStatus.SCEEmployeeNeedsClarification +
                    //    "</Value></Eq></And><Or><Eq><FieldRef Name='SCERequests_EmployeeAssignedTo' /><Value Type='Text'>"+ SPContext.Current.Web.CurrentUser.LoginName + 
                    //    "</Value></Eq><Eq><FieldRef Name='SCERequests_EmployeeAssignedTo' /><Value Type='Text'>"+ GroupName.Trim() +
                    //    "</Value></Eq></Or></And></Where>" +
                    //    "<OrderBy><FieldRef Name='SCERequests_RecievedDate' Ascending='False' /></OrderBy>";

                    //clarificationQuery = @"<Where><And><And><And><And><Eq><FieldRef Name='SCERequests_IsEmployee' /><Value Type='Choice'>Yes</Value></Eq>" +
                    //  "<IsNull><FieldRef Name='ClarificationReply' /></IsNull></And><Eq><FieldRef Name='Sender' LookupId='True'/><Value Type='User'>" + user.ID +
                    //  "</Value></Eq></And><Eq><FieldRef Name='RequestStatus_Code' /><Value Type='Lookup'>" + Common.Utilities.RequestStatus.SCEEmployeeNeedsClarification +
                    //  "</Value></Eq></And><Or><Eq><FieldRef Name='SCERequests_EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName +
                    //  "</Value></Eq><Eq><FieldRef Name='SCERequests_EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim() +
                    //  "</Value></Eq></Or></And></Where><OrderBy><FieldRef Name='SCERequests_RecievedDate' Ascending='False' /></OrderBy>";

                    //newQuery = @"<Where><And><And><Eq><FieldRef Name='IsEmployee' /><Value Type='Choice'>Yes</Value></Eq>" +
                    //    "<Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCESubmitted +
                    //    "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEDraft +
                    //    "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEEquivalenceEmployeeReassign +
                    //    "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEEmployeeClarificationReply +
                    //    "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCESectionManagerMissingInformation +
                    //    "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEDepartmentManagerMissingRecommendation +
                    //    "</Value></Eq><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCECulturalMissionStatementReply +
                    //    "</Value></Eq></Or></Or></Or></Or></Or></Or></And><Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName +
                    //    "</Value></Eq><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim() +
                    //    "</Value></Eq></Or></And></Where><OrderBy><FieldRef Name='RecievedDate' Ascending='False' /></OrderBy>";



                }


            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ExceptionRequests.GetExceptionRequestsQuery");
            }
            return queriesStrings;
        }

        public static string GetClarificationsViewFields(int LCID)
        {


            string viewFields = string.Empty;
            if (LCID == (int)Language.English)
            {

                viewFields = "<FieldRef Name='SCERequests_RequestNumber'/>"
            + "<FieldRef Name='CountryOfStudy_Title'/>"
            //+ "<FieldRef Name='Applicants_ApplicantName'/>"
            // + "<FieldRef Name='Applicants_EnglishName'/>"
            //+ "<FieldRef Name='Applicants_QatarID/>"
            //+ "<FieldRef Name='Applicants_MobileNumber'/>"
            + "<FieldRef Name='Nationality_Title'/>"

            //+ "<FieldRef Name='ClarificationDate'/>"

            + "<FieldRef Name='Sender'/>"
            //"<FieldRef Name='ClarificationReason'/>"

            + "<FieldRef Name='SCERequests_ID'/>"
                + "<FieldRef Name='SCERequests_StdPrintedName'/>"
                 + "<FieldRef Name='SCERequests_StdQatarID'/>"


                    + "<FieldRef Name='SCERequests_RecievedDate'/>"

                     + "<FieldRef Name= 'SCERequests_OtherCertificateResource' />"
                     + "<FieldRef Name= 'SCERequests_EmployeeAssignedTo' />"
                     //+ "<FieldRef Name= 'SCERequests_ReturnedBy' />"
                     + "<FieldRef Name= 'RequestStatus_Code' />"
                       + "<FieldRef Name= 'ScholasticLevel_Title' />"
                         + "<FieldRef Name= 'SCERequests_ApplicantOfficialName' />"
                //+ "<FieldRef Name= 'SCERequests_IsEmployee' />"
                + "<FieldRef Name= 'SCERequests_StdName' />"
                + "<FieldRef Name= 'RequestStatus_ReviewerDescriptionEn' />"
                + "<FieldRef Name= 'RequestStatus_ID' />"
                + "<FieldRef Name= 'SCERequests_DelayedDays' />";




            }
            else
            {

                viewFields = "<FieldRef Name='SCERequests_RequestNumber'/>"

           + "<FieldRef Name='CountryOfStudy_TitleAr'/>"

            //+ "<FieldRef Name='Applicants_ApplicantName'/>"
            // + "<FieldRef Name='Applicants_ArabicName'/>"
            //+ "<FieldRef Name='Applicants_QatarID'/>"
            //+ "<FieldRef Name='Applicants_MobileNumber'/>"
            + "<FieldRef Name='Nationality_TitleAr'/>"

             //+ "<FieldRef Name='ClarificationDate'/>"
             + "<FieldRef Name='Sender'/>"
            //"<FieldRef Name='ClarificationReasonAr'/>"
            + "<FieldRef Name='SCERequests_ID'/>"
             + "<FieldRef Name='SCERequests_StdPrintedName'/>"
             + "<FieldRef Name='SCERequests_StdQatarID'/>"
                  + "<FieldRef Name='SCERequests_PrevSchool'/>"
                   + "<FieldRef Name='SCERequests_RecievedDate'/>"

                     + "<FieldRef Name= 'SCERequests_OtherCertificateResource' />"
                     + "<FieldRef Name= 'SCERequests_EmployeeAssignedTo' />"
                      //+ "<FieldRef Name= 'SCERequests_ReturnedBy' />"
                      + "<FieldRef Name= 'RequestStatus_Code' />"
                       + "<FieldRef Name= 'ScholasticLevel_TitleAr' />"
                         + "<FieldRef Name= 'SCERequests_ApplicantOfficialName' />"
                            //+ "<FieldRef Name= 'SCERequests_IsEmployee' />"
                            + "<FieldRef Name= 'SCERequests_StdName' />"
                            + "<FieldRef Name= 'RequestStatus_ReviewerDescriptionAr' />"
                            + "<FieldRef Name= 'RequestStatus_ID' />"
                            + "<FieldRef Name= 'SCERequests_DelayedDays' />";

            }
            return viewFields;

        }
        public static string GetstrViewFields(int LCID)
        {
            string strViewFields = string.Empty;
            if (LCID == (int)Language.English)
            {
                strViewFields = "<FieldRef Name='RequestNumber'/>"
                      + "<FieldRef Name='RecievedDate'/>"
                      + "<FieldRef Name='Nationality_Title'/>"
                      + "<FieldRef Name='CertificateResource'/>"
                      + "<FieldRef Name='OtherCertificateResource'/>"
                        + "<FieldRef Name='StdPrintedName'/>"
                         + "<FieldRef Name='StdQatarID'/>"
                          + "<FieldRef Name='PrevSchool'/>"
                      + "<FieldRef Name='ID'/>"

                      + "<FieldRef Name='RequestStatus'/>"

                      + "<FieldRef Name='RequestStatusId'/>"
                     //+ "<FieldRef Name='NationalityCategory_Title'/>"

                     + "<FieldRef Name='EmployeeAssignedTo'/>"
                     + "<FieldRef Name='DelayedDays'/>"
                       + "<FieldRef Name='CountryOfStudy_Title'/>"
                       + "<FieldRef Name='ScholasticLevel_Title'/>"
                       + "<FieldRef Name='ApplicantOfficialName'/>"
                + "<FieldRef Name= 'StdName' />";



            }
            else
            {
                strViewFields = "<FieldRef Name='RequestNumber'/>"
                     + "<FieldRef Name='RecievedDate'/>"
                     + "<FieldRef Name='Nationality_TitleAr'/>"

                     + "<FieldRef Name='CertificateResourceAr'/>"
                     + "<FieldRef Name='OtherCertificateResource'/>"
                     + "<FieldRef Name='StdPrintedName'/>"
                      + "<FieldRef Name='StdQatarID'/>"
                          + "<FieldRef Name='PrevSchool'/>"

                     + "<FieldRef Name='ID'/>"

                     + "<FieldRef Name='RequestStatusAr'/>"
                     + "<FieldRef Name='EmployeeAssignedTo'/>"

                     + "<FieldRef Name='RequestStatusId'/>"
                //+ "<FieldRef Name='NationalityCategory_TitleAr'/>"

                + "<FieldRef Name='DelayedDays'/>"
                  + "<FieldRef Name='CountryOfStudy_TitleAr'/>"
                   + "<FieldRef Name='ScholasticLevel_TitleAr'/>"
                + "<FieldRef Name='ApplicantOfficialName'/>"
                + "<FieldRef Name= 'StdName' />";



            }
            return strViewFields;
        }


        public static IEnumerable<Entities.ExceptionRequests> GetAllClarificationRequests(SPQuery query, int LCID)
        {
            List<Entities.ExceptionRequests> SCERequests = new List<Entities.ExceptionRequests>();
            try
            {
                using (SPWeb web = new SPSite(SPContext.Current.Site.Url).OpenWeb())
                {


                    SPList list = web.Lists[Utilities.Constants.SCEClarificationsRequests];


                    SPListItemCollection items = list.GetItems(query);
                    if (items != null && items.Count > 0)
                    {
                        foreach (SPListItem item in items)
                        {
                            //Common.Entities.RequestStatus requestStatusItem = new Common.Entities.RequestStatus();
                            //requestStatusItem = Common.BL.RequestStatus.GetRequestStatusById(new SPFieldLookupValue(item["RequestStatusId"].ToString()).LookupId);
                            //Entities.ExceptionRequests Request = new Entities.ExceptionRequests();


                            Entities.ExceptionRequests req = new Entities.ExceptionRequests();
                            req.ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;

                            //req.RequestClarificationDate = (item["ClarificationDate"] != null) ? DateTime.Parse(item["ClarificationDate"].ToString()) : DateTime.MinValue;

                            //SPFieldLookupValue ApplicantsMobileNumber = new SPFieldLookupValue((item["Applicants_MobileNumber"] != null) ? item["Applicants_MobileNumber"].ToString() : string.Empty);
                            //req.ApplicantMobileNumber = ApplicantsMobileNumber.LookupValue;


                            SPFieldLookupValue RecievedDate = new SPFieldLookupValue((item["SCERequests_RecievedDate"] != null) ? item["SCERequests_RecievedDate"].ToString() : string.Empty);
                            req.RecievedDate = DateTime.Parse(RecievedDate.LookupValue);

                            SPFieldLookupValue StudentAccToCert = new SPFieldLookupValue((item["SCERequests_StdPrintedName"] != null) ? item["SCERequests_StdPrintedName"].ToString() : string.Empty);
                            req.StudentNameAccToCert = StudentAccToCert.LookupValue;



                            SPFieldLookupValue CertHolderID = new SPFieldLookupValue((item["SCERequests_StdQatarID"] != null) ? item["SCERequests_StdQatarID"].ToString() : string.Empty);
                            req.CertificateHolderQatarID = CertHolderID.LookupValue;


                            if (item["SCERequests_ID"] != null)
                            {
                                SPFieldLookupValue RequestID = new SPFieldLookupValue(item["SCERequests_ID"].ToString());
                                req.RequestID = RequestID.LookupValue;
                            }

                            if (item["SCERequests_DelayedDays"] != null)
                            {
                                SPFieldLookupValue DelayedDays = new SPFieldLookupValue(item["SCERequests_DelayedDays"].ToString());
                                req.DelayedDays = DelayedDays.LookupValue;
                            }




                            if (item["SCERequests_RequestNumber"] != null)
                            {
                                SPFieldLookupValue RequestNumber = new SPFieldLookupValue(item["SCERequests_RequestNumber"].ToString());
                                req.RequestNumber = RequestNumber.LookupValue;
                            }

                            if (item["SCERequests_StdName"] != null)
                            {
                                SPFieldLookupValue ApplicantName = new SPFieldLookupValue(item["SCERequests_StdName"].ToString());
                                req.ApplicantName = ApplicantName.LookupValue;
                            }

                            if (item["RequestStatus_ID"] != null)
                            {
                                SPFieldLookupValue RequestStatusId = new SPFieldLookupValue(item["RequestStatus_ID"].ToString());
                                req.RequestStatusId = int.Parse(RequestStatusId.LookupValue);
                            }



                            if (LCID == (int)Language.English)
                            {

                                //SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                                //req.ApplicantName = ApplicantsEnglishName.LookupValue;
                                if (item["Nationality_Title"] != null)
                                {
                                    SPFieldLookupValue NationalityTitle = new SPFieldLookupValue(item["Nationality_Title"].ToString());
                                    req.Nationality = NationalityTitle.LookupValue;
                                }
                                if (item["RequestStatus_ReviewerDescriptionEn"] != null)
                                {
                                    SPFieldLookupValue RequestStatus = new SPFieldLookupValue(item["RequestStatus_ReviewerDescriptionEn"].ToString());
                                    req.RequestStatus = RequestStatus.LookupValue;
                                }


                                if (item["CountryOfStudy_Title"] != null)
                                {
                                    SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue(item["CountryOfStudy_Title"].ToString());
                                    req.CertificateResource = CountryOfStudy.LookupValue;
                                }
                                else if (item["SCERequests_OtherCertificateResource"] != null)
                                {
                                    SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue(item["SCERequests_OtherCertificateResource"].ToString());
                                    req.CertificateResource = CountryOfStudy.LookupValue;
                                }
                                //if (item["ClarificationReason"] != null)
                                //{
                                //    SPFieldLookupValue ClarificationReason = new SPFieldLookupValue(item["ClarificationReason"].ToString());
                                //    req.ClarificationReason = ClarificationReason.LookupValue;
                                //}
                                if (item["ScholasticLevel_Title"] != null)
                                {
                                    SPFieldLookupValue SchoolLastGrade = new SPFieldLookupValue(item["ScholasticLevel_Title"].ToString());
                                    req.SchoolLastGrade = SchoolLastGrade.LookupValue;
                                }


                            }
                            else
                            {


                                //SPFieldLookupValue ApplicantsArabicName = new SPFieldLookupValue((item["Applicants_ArabicName"] != null) ? item["Applicants_ArabicName"].ToString() : string.Empty);
                                //req.ApplicantName = ApplicantsArabicName.LookupValue;
                                if (item["Nationality_TitleAr"] != null)
                                {
                                    SPFieldLookupValue NationalityTitle = new SPFieldLookupValue(item["Nationality_TitleAr"].ToString());
                                    req.Nationality = NationalityTitle.LookupValue;
                                }
                                if (item["RequestStatus_ReviewerDescriptionAr"] != null)
                                {
                                    SPFieldLookupValue RequestStatus = new SPFieldLookupValue(item["RequestStatus_ReviewerDescriptionAr"].ToString());
                                    req.RequestStatus = RequestStatus.LookupValue;
                                }

                                if (item["CountryOfStudy_TitleAr"] != null)
                                {
                                    SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue(item["CountryOfStudy_TitleAr"].ToString());
                                    req.CertificateResource = CountryOfStudy.LookupValue;
                                }
                                else if (item["SCERequests_OtherCertificateResource"] != null)
                                {
                                    SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue(item["SCERequests_OtherCertificateResource"].ToString());
                                    req.CertificateResource = CountryOfStudy.LookupValue;
                                }
                                //if (item["ClarificationReasonAr"] != null)
                                //{
                                //    SPFieldLookupValue ClarificationReason = new SPFieldLookupValue(item["ClarificationReasonAr"].ToString());
                                //    req.ClarificationReason = ClarificationReason.LookupValue;
                                //}
                                if (item["ScholasticLevel_TitleAr"] != null)
                                {
                                    SPFieldLookupValue SchoolLastGrade = new SPFieldLookupValue(item["ScholasticLevel_TitleAr"].ToString());
                                    req.SchoolLastGrade = SchoolLastGrade.LookupValue;
                                }
                            }





                            SCERequests.Add(req);
                        }


                    }
                }
                //To remove the requests that aren't created by employees 
               
                List<int> deletedId= new List<int>();
                if (SCERequests.Count != 0)
                {
                    using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                    {
                        SCERequestsListFieldsContentType req = null;
                        foreach (var request in SCERequests)
                     
                        {
                            req = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == int.Parse(request.RequestID)).FirstOrDefault();
                            if (req != null && req.IsEmployee == IsEmployee.No)
                            {
                               
                                deletedId.Add(int.Parse(request.RequestID));
                             
                               
                            }
                        }


                        if (deletedId.Count>0)
                        {
                            foreach (int id in deletedId)
                            {
                                SCERequests.Remove(SCERequests.Where(r => int.Parse(r.RequestID) == id).First());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method ClarificationRequests.GetAllClarificationRequests");
            }
            return SCERequests;
        }



        public static List<Entities.ExceptionRequests> GetRequestsListing(SPQuery query, int LCID)
        {
            List<Entities.ExceptionRequests> Requests = new List<Entities.ExceptionRequests>();
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method ExceptionRequests.GetRequestsListing");
                        SPList requestsList = web.Lists[Utilities.Constants.SCERequests];
                        SPListItemCollection items = requestsList.GetItems(query);
                        Common.Entities.RequestStatus requestStatusItem = new Common.Entities.RequestStatus();


                        foreach (SPListItem item in items)
                        {
                            requestStatusItem = Common.BL.RequestStatus.GetRequestStatusById(new SPFieldLookupValue(item["RequestStatusId"].ToString()).LookupId);
                            Entities.ExceptionRequests Request = new Entities.ExceptionRequests();


                            Request.DelayedDays = (item["DelayedDays"] != null) ? item["DelayedDays"].ToString() : string.Empty;
                            Request.AssignedTo = (item["EmployeeAssignedTo"] != null) ? item["EmployeeAssignedTo"].ToString() : string.Empty;
                            Request.RequestID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;
                            Request.RequestNumber = (item["RequestNumber"] != null) ? item["RequestNumber"].ToString() : string.Empty;

                            Request.RecievedDate = (item["RecievedDate"] != null) ? DateTime.Parse(item["RecievedDate"].ToString()) : DateTime.MinValue;

                            SPFieldLookupValue StatusId = new SPFieldLookupValue((item["RequestStatusId"] != null) ? item["RequestStatusId"].ToString() : string.Empty);
                            Request.RequestStatusId = StatusId.LookupId;
                            if ((Request.RequestStatusId == (int)Common.Utilities.RequestStatus.SCESectionManagerAccepted) || (Request.RequestStatusId == (int)Common.Utilities.RequestStatus.SCESectionManagerRejected))
                                Request.IsRequestClosed = true;
                            else
                                Request.IsRequestClosed = false;


                            Request.StudentNameAccToCert = item["StdPrintedName"] != null ? Convert.ToString(item["StdPrintedName"]) : string.Empty;

                            Request.CertificateHolderQatarID = item["StdQatarID"] != null ? Convert.ToString(item["StdQatarID"]) : string.Empty;

                            // Request.ApplicantName= item["ApplicantOfficialName"] != null ? Convert.ToString(item["ApplicantOfficialName"]) : string.Empty;

                            Request.ApplicantName = item["StdName"] != null ? Convert.ToString(item["StdName"]) : string.Empty;


                            if (LCID == (int)Language.English)
                            {
                                SPFieldLookupValue NationalityTitle = new SPFieldLookupValue((item["Nationality_Title"] != null) ? item["Nationality_Title"].ToString() : string.Empty);
                                Request.Nationality = NationalityTitle.LookupValue;
                                SPFieldLookupValue RequestStatus = new SPFieldLookupValue((item["RequestStatus"] != null) ? item["RequestStatus"].ToString() : string.Empty);
                                Request.RequestStatus = RequestStatus.LookupValue;

                                Request.RequestStatus = requestStatusItem.ReviewerDescriptionEn;

                                //SPFieldLookupValue NationalityCategoryTitle = new SPFieldLookupValue((item["NationalityCategory_Title"] != null) ? item["NationalityCategory_Title"].ToString() : string.Empty);
                                //Request.NationalityCategory = NationalityCategoryTitle.LookupValue;

                                if (item["CountryOfStudy_Title"] != null)
                                {
                                    SPFieldLookupValue CertificateResource = new SPFieldLookupValue(item["CountryOfStudy_Title"].ToString());
                                    Request.CertificateResource = CertificateResource.LookupValue;
                                }

                                else if (item["OtherCertificateResource"] != null)
                                {
                                    Request.CertificateResource = Convert.ToString(item["OtherCertificateResource"]);

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

                                //SPFieldLookupValue NationalityCategoryTitle = new SPFieldLookupValue((item["NationalityCategory_TitleAr"] != null) ? item["NationalityCategory_TitleAr"].ToString() : string.Empty);
                                //Request.NationalityCategory = NationalityCategoryTitle.LookupValue;

                                SPFieldLookupValue RequestStatus = new SPFieldLookupValue((item["RequestStatusAr"] != null) ? item["RequestStatusAr"].ToString() : string.Empty);
                                Request.RequestStatus = RequestStatus.LookupValue;

                                Request.RequestStatus = requestStatusItem.ReviewerDescriptionAr;


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
                        Logging.GetInstance().Debug("Exit ExceptionRequests.GetRequestsListing");
                    }
                    return Requests;
                }
            }

        }



    }
}
