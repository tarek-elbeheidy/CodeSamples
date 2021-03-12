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
    public class ReturnedRequests
    {
        public static IEnumerable<Entities.ReturnedRequests> GetAllReturnedRequests(string strQuery, int LCID,string GroupName)
        {
            List<Entities.ReturnedRequests> Requests = new List<Entities.ReturnedRequests>();
            using (SPSite oSite = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb oWeb = oSite.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method ReturnedRequests.GetAllReturnedRequests");
                        string strViewFields = GetstrViewFields(LCID);
                        oWeb.AllowUnsafeUpdates = true;
                        /*
                        string joins = @"<Join Type='INNER' ListAlias='SCERequests'>
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
 "<Join Type='INNER' ListAlias='Applicants'>" +
                                       "<Eq>" +
                                         "<FieldRef List='SCERequests' Name='ApplicantID' RefType='Id' />" +
                                         "<FieldRef List='Applicants' Name='Id' />" +
                                       "</Eq>" +
                                     "</Join><Join Type='INNER' ListAlias='Nationality'>" +
                                       "<Eq>" +
                                         "<FieldRef List='Applicants' Name='Nationality' RefType='Id' />" +
                                         "<FieldRef List='Nationality' Name='Id' />" +
                                       "</Eq>" +
                                     "</Join>";
                       
                        string projFields =
                                             
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

                        + "<Field Name='SCERequests_DelayedDays' Type='Lookup' "
                        + "List='SCERequests' ShowField='DelayedDays'/>"


                         + "<Field Name='SCERequests_StdQatarID' Type='Lookup' "
                        + "List='SCERequests' ShowField='StdQatarID'/>" +
                         "<Field Name='SCERequests_EmployeeAssignedTo' Type='Lookup' "
                        + "List='SCERequests' ShowField='EmployeeAssignedTo'/>"


                         + "<Field Name='RequestStatus_Code' Type='Lookup' "
                        + "List='RequestStatus' ShowField='Code'/>"

                         + "<Field Name='RequestStatus_ID' Type='Lookup' "
                        + "List='RequestStatus' ShowField='ID'/>"

                        + "<Field Name='CountryOfStudy_Title' Type='Lookup' "
                        + "List='CountryOfStudy' ShowField='Title'/>"
                        + "<Field Name='CountryOfStudy_TitleAr' Type='Lookup' "
                        + "List='CountryOfStudy' ShowField='TitleAr'/>" +

                           "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                                                    "List='Applicants' ShowField='ApplicantName'/>"
                        + "<Field Name='Applicants_ArabicName' Type='Lookup' " +
                                                    "List='Applicants' ShowField='ArabicName'/>" +
                                                    "<Field Name='Applicants_EnglishName' Type='Lookup' " +
                                                    "List='Applicants' ShowField='EnglishName'/>"
                        + "<Field Name='Applicants_QatarID' Type='Lookup' "
                        + "List='Applicants' ShowField='PersonalID'/>"
                         + "<Field Name='Applicants_QID' Type='Lookup' "
                         + "List='Applicants' ShowField='ID'/>" +
                          "<Field Name='Applicants_MobileNumber' Type='Lookup' "
                            + "List='Applicants' ShowField='MobileNumber'/>"

                        + "<Field Name='Nationality_Title' Type='Lookup' "
                        + "List='Nationality' ShowField='Title'/>" +
                         "<Field Name='Nationality_TitleAr' Type='Lookup' " +
                                                "List='Nationality' ShowField='TitleAr'/>"

                                                  + "<Field Name='ScholasticLevel_Title' Type='Lookup' "
                        + "List='ScholasticLevel' ShowField='Title'/>" +
                         "<Field Name='ScholasticLevel_TitleAr' Type='Lookup' " +
                                                "List='ScholasticLevel' ShowField='TitleAr'/>";


                        /*

                        */


                        string joins =

                            "<Join Type='LEFT' ListAlias='Applicants'>" +
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
                                       "<Join Type='LEFT' ListAlias='ReturnReasons'>" +
                                      "<Eq>" +
                                      "<FieldRef Name='ManagerReturnReason' RefType='Id'/>" +
                                      "<FieldRef List='ReturnReasons' Name='ID'/>" +
                                      "</Eq>" +
                                      "</Join>"

                         +"<Join Type='INNER' ListAlias='Nationality'>" +
                        "<Eq>" +
                        "<FieldRef  Name='StdNationality' RefType='Id'/>" +
                        "<FieldRef List='Nationality' Name='ID'/>" +
                        "</Eq>" +
                        "</Join>"

                      ;
                        /*
                        + "<Join Type='INNER' ListAlias='NationalityCategory'>" +
                        "<Eq>" +
                        "<FieldRef List='Applicants' Name='NationalityCategory' RefType='Id'/>" +
                        "<FieldRef List='NationalityCategory' Name='ID'/>" +
                        "</Eq>" +
                        "</Join>"*/

                        string projFields = "<Field Name='Applicants_QatarID' Type='Lookup' " +
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
                                                "List='Nationality' ShowField='TitleAr'/>" +
                                                

                                                "<Field Name='CountryOfStudy_Title' Type='Lookup' " +
                                                "List='CountryOfStudy' ShowField='Title'/>" +

                                                "<Field Name='CountryOfStudy_TitleAr' Type='Lookup' " +
                                                "List='CountryOfStudy' ShowField='TitleAr'/>" +

                                                  "<Field Name='ReturnReasons_Title' Type='Lookup' " +
                                                "List='ReturnReasons' ShowField='Title'/>" +

                                                "<Field Name='ReturnReasons_TitleAr' Type='Lookup' " +
                                                "List='ReturnReasons' ShowField='TitleAr'/>"+


                                                 "<Field Name='ScholasticLevel_Title' Type='Lookup' " +
                                                "List='ScholasticLevel' ShowField='Title'/>" +

                                                "<Field Name='ScholasticLevel_TitleAr' Type='Lookup' " +
                                                "List='ScholasticLevel' ShowField='TitleAr'/>";



                        //+"<Field Name='NationalityCategory_Title' Type='Lookup' " +
                        //                       "List='NationalityCategory' ShowField='Title'/>" +
                        //                       "<Field Name='NationalityCategory_TitleAr' Type='Lookup' " +
                        //                       "List='NationalityCategory' ShowField='TitleAr'/>" +


                       


                        SPQuery reqQuery = Common.Utilities.BusinessHelper.GetQueryObject(strQuery, joins, projFields, strViewFields);
                        Requests = GetRequestsListing(reqQuery, LCID,GroupName);

                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(ex);
                    }
                    finally
                    {
                        oWeb.AllowUnsafeUpdates = false;
                        Logging.GetInstance().Debug("Exiting method ReturnedRequests.GetAllReturnedRequests");
                    }
                    return Requests;
                }
            }
        }


        public static string GetReturnedRequestsQuery(string GroupName, int delayedDays)
        {
            Logging.GetInstance().Debug("Enter ReturnedRequests.GetReturnedRequestsQuery");
            string returnedQuery = string.Empty;
            try
            {

                //Department Manager Query
                //if (Common.Utilities.Constants.SCEDepartmentManagersGroupName.ToLower() == GroupName.ToLower())
                //{
                //}
                //  Equivalence Employees Query
                if (Common.Utilities.Constants.SCEEquivalenceEmployeesGroupName.ToLower() == GroupName.ToLower())
                {

                    //SCEEmployeeClarificationReply
                    //SCESectionManagerMissingInformation
                    //SCEDepartmentManagerMissingRecommendation

                    //SCECulturalMissionStatementReply
                    //SCEExternalCommunicationReply

                    //returnedQuery = @"<Where><And><And><Or><Eq><FieldRef Name='RequestStatus_Code' /><Value Type='Lookup'>" + Common.Utilities.RequestStatus.SCEEmployeeClarificationReply + "</Value></Eq>" +
                    //                "<Or><Eq><FieldRef Name='RequestStatus_Code' /><Value Type='Lookup'>" + Common.Utilities.RequestStatus.SCESectionManagerMissingInformation + " </Value></Eq>" +
                    //                "<Or><Eq><FieldRef Name='RequestStatus_Code' /><Value Type='Lookup'>" + Common.Utilities.RequestStatus.SCEDepartmentManagerMissingRecommendation + "</Value></Eq>" +
                    //                "<Or><Eq><FieldRef Name='RequestStatus_Code' /><Value Type='Lookup'>" + Common.Utilities.RequestStatus.SCECulturalMissionStatementReply + "</Value></Eq>" +
                    //                "<Eq><FieldRef Name='RequestStatus_Code' /><Value Type='Lookup'>" + Common.Utilities.RequestStatus.SCEExternalCommunicationReply + "</Value></Eq></Or>" +
                    //                "</Or></Or></Or><Or><Eq><FieldRef Name='SCERequests_EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName + "</Value></Eq>" +
                    //                "<Eq><FieldRef Name='SCERequests_EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim() + "</Value></Eq></Or></And>" +
                    //                "<Or><IsNull><FieldRef Name='SCERequests_DelayedDays' /></IsNull>" +
                    //                "<Lt><FieldRef Name='SCERequests_DelayedDays' /><Value Type='Number'>" + delayedDays + "</Value></Lt></Or></And></Where>" +
                    //                "<OrderBy><FieldRef Name='SCERequests_RecievedDate' Ascending='False' /></OrderBy>";
                    
                    //correct query
                    //returnedQuery = @"<Where><And><And><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEEmployeeClarificationReply + "</Value></Eq>" +
                    //          "<Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCESectionManagerMissingInformation + "</Value></Eq>" +
                    //          "<Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCEDepartmentManagerMissingRecommendation + "</Value></Eq>" +
                    //          "<Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCECulturalMissionStatementReply
                    //          + "</Value></Eq></Or></Or></Or><Or>" +
                    //          "<Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName +
                    //          "</Value></Eq><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim() +
                    //          "</Value></Eq></Or></And><Or><IsNull><FieldRef Name='DelayedDays' /></IsNull><Lt>" +
                    //          "<FieldRef Name='DelayedDays' /><Value Type='Number'>" + delayedDays + "</Value></Lt></Or></And></Where>" +
                    //          "<OrderBy><FieldRef Name='RecievedDate' Ascending='False' /></OrderBy>";



                    returnedQuery = @"<Where><And><And><And><Eq><FieldRef Name='IsEmployee' /><Value Type='Choice'>No</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+ (int)Common.Utilities.RequestStatus.SCEEmployeeClarificationReply + 
                        "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>" + (int)Common.Utilities.RequestStatus.SCESectionManagerMissingInformation +
                        "</Value></Eq><Or><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+ (int)Common.Utilities.RequestStatus.SCEDepartmentManagerMissingRecommendation + 
                        "</Value></Eq><Eq><FieldRef Name='RequestStatusId' /><Value Type='Lookup'>"+ (int)Common.Utilities.RequestStatus.SCECulturalMissionStatementReply +
                        "</Value></Eq></Or></Or></Or></And><Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>"+ SPContext.Current.Web.CurrentUser.LoginName + 
                        "</Value></Eq><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim() +
                        "</Value></Eq></Or></And><Or><IsNull><FieldRef Name='DelayedDays' /></IsNull><Lt><FieldRef Name='DelayedDays' /><Value Type='Number'>"+delayedDays+
                        "</Value></Lt></Or></And></Where><OrderBy><FieldRef Name='RecievedDate' Ascending='False' /></OrderBy>";

                }
                /*
                //Section Manager Query
                else if (Common.Utilities.Constants.SCESectionManagers.ToLower() == GroupName.ToLower())
                {
                    returnedQuery = @"<Where><And><And><Or><Eq><FieldRef Name='RequestStatus_Code' /><Value Type='Lookup'>" + Common.Utilities.RequestStatus.SCEDepartmentManagerAcceptDecision + "</Value></Eq>" +
                                     "<Eq><FieldRef Name='RequestStatus_Code' /><Value Type='Lookup'>" + Common.Utilities.RequestStatus.SCEDepartmentManagerRejectDecision + "</Value></Eq></Or>" +
                                    "<Or><Eq><FieldRef Name='SCERequests_EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName + "</Value></Eq>" +
                                    "<Eq><FieldRef Name='SCERequests_EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim() + "</Value></Eq></Or></And>" +
                                    "<Or><IsNull><FieldRef Name='SCERequests_DelayedDays' /></IsNull>" +
                                    "<Lt><FieldRef Name='SCERequests_DelayedDays' /><Value Type='Number'>" + delayedDays + "</Value></Lt></Or></And></Where>" +
                                    "<OrderBy><FieldRef Name='SCERequests_RecievedDate' Ascending='False' /></OrderBy>";
                    //SCEDepartmentManagerAcceptDecision
                    //SCEDepartmentManagerRejectDecision
                }
                */

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ReturnedRequests.GetReturnedRequestsQuery");
            }
            return returnedQuery;
        }

        public static List<string> GetReturnedQueryPerRole(string SPGroupName)
        {
            List<string> objColumns = new List<string>();
            string strQuery = string.Empty;
            //Program Manager Query
            if (Common.Utilities.Constants.ProgramManagerGroupName == SPGroupName)
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PAHeadEmployeeMissingInformation);
            }

            //Program Employees Query
            else if (Common.Utilities.Constants.ArabicProgEmployeeGroupName == SPGroupName || Common.Utilities.Constants.EuropeanProgEmployeeGroupName == SPGroupName)
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PAProgramEmployeeMissingInformation);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PACulturalMissionStatementReply);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PAHigherEduInstitutesStatementReply);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PAEmployeeClarificationReplay);
            }

            //Legal Affairs Query
            else if (Common.Utilities.Constants.LegalAffairsGroupName == SPGroupName)
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PALegalEmployeeMissingInformation);

            //Receptionist Query
            else if (Common.Utilities.Constants.ReceptionistGroupName == SPGroupName)
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PAReceptionistClarificationReply);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PAReceptionistMissingInformation);
            }

            //Department Manager Query
            else if (Common.Utilities.Constants.DepartmentManagerGroupName == SPGroupName)
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PAHeadManagerMissingInformationTechnicalCommittee);
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PALegalEmployeeSendNotes);
            }

            //Assistant Undersecretary Query
            else if (Common.Utilities.Constants.AssistUndersecretaryGroupName == SPGroupName)
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PAAssistantUndersecretaryMissingInformation);





            return objColumns;
        }

        public static List<Entities.ReturnedRequests> GetRequestsListing(SPQuery query, int LCID,string GroupName)
        {
            List<Entities.ReturnedRequests> Requests = new List<Entities.ReturnedRequests>();
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    try
                    {

                        Logging.GetInstance().Debug("Entering method ReturnedRequests.GetRequestsListing");
                        
                            SPList returnedRequestsList = web.Lists[Utilities.Constants.SCERequests];
                            SPListItemCollection items = returnedRequestsList.GetItems(query);
                            Common.Entities.RequestStatus requestStatusItem = new Common.Entities.RequestStatus();
                            foreach (SPListItem item in items)
                            {
                                requestStatusItem = Common.BL.RequestStatus.GetRequestStatusById(new SPFieldLookupValue(item["RequestStatusId"].ToString()).LookupId);
                                Entities.ReturnedRequests Request = new Entities.ReturnedRequests();
                                SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue((item["Applicants_QatarID"] != null) ? item["Applicants_QatarID"].ToString() : string.Empty);
                                Request.QatariID = ApplicantsQatarID.LookupValue;
                            //SPFieldLookupValue ApplicantsMobileNumber = new SPFieldLookupValue((item["Applicants_MobileNumber"] != null) ? item["Applicants_MobileNumber"].ToString() : string.Empty);
                            // Request.ApplicantMobileNumber = ApplicantsMobileNumber.LookupValue;

                            Request.ApplicantMobileNumber = item["MobileNumber"] != null ? Convert.ToString(item["MobileNumber"]) : string.Empty;


                            Request.DelayedDays = (item["DelayedDays"] != null) ? item["DelayedDays"].ToString() : string.Empty;
                                Request.AssignedTo = (item["EmployeeAssignedTo"] != null) ? item["EmployeeAssignedTo"].ToString() : string.Empty;
                                Request.ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;
                                Request.RequestNumber = (item["RequestNumber"] != null) ? item["RequestNumber"].ToString() : string.Empty;


                                Request.StudentNameAccToCert = item["StdPrintedName"] != null ? Convert.ToString(item["StdPrintedName"]) : string.Empty;

                                Request.CertificateHolderQatarID = item["StdQatarID"] != null ? Convert.ToString(item["StdQatarID"]) : string.Empty;
                            //Request.ReturnedFrom = item["ReturnedFrom"] != null ? Convert.ToString(item["ReturnedFrom"]) : string.Empty;


                            if (item["ReturnReason"]!=null &&( requestStatusItem.Code == Common.Utilities.RequestStatus.SCEEmployeeClarificationReply.ToString()
                                || requestStatusItem.Code ==Common.Utilities.RequestStatus.SCECulturalMissionStatementReply.ToString()))
                            {
                               
                                Request.ReturnReason = Convert.ToString(item["ReturnReason"]);//HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", item["ReturnReason"].ToString(), (uint)LCID);
                            }
                            //else if (requestStatusItem.Code == Common.Utilities.RequestStatus.SCECulturalMissionStatementReply.ToString())
                            //{
                            //    Request.ReturnReason = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ReturnFromCultural", (uint)LCID);
                            //}
                                //if (LCID == (int)Language.English)
                                //{
                                //    SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                                //    Request.ReturnedFrom = ApplicantsEnglishName.LookupValue;
                                //}
                                //else
                                //{
                                //    SPFieldLookupValue ApplicantsArabicName = new SPFieldLookupValue((item["Applicants_ArabicName"] != null) ? item["Applicants_ArabicName"].ToString() : string.Empty);
                                //    Request.ReturnedFrom = ApplicantsArabicName.LookupValue;
                                //}
                            
                            else
                            {
                                if (LCID == (int)Language.English)
                                {
                                    SPFieldLookupValue ReturnReason = new SPFieldLookupValue((item["ReturnReasons_Title"] != null) ? item["ReturnReasons_Title"].ToString() : string.Empty);
                                    Request.ReturnReason = ReturnReason.LookupValue;
                                }
                                else
                                {
                                    SPFieldLookupValue ReturnReason = new SPFieldLookupValue((item["ReturnReasons_TitleAr"] != null) ? item["ReturnReasons_TitleAr"].ToString() : string.Empty);
                                    Request.ReturnReason = ReturnReason.LookupValue;
                                }

                            }

                            Request.ReturnedFrom = item["ReturnedBy"] != null ? item["ReturnedBy"].ToString() : string.Empty;
                           

                            Request.ReturnDate = (item["ReturnDate"] != null) ? DateTime.Parse(item["ReturnDate"].ToString()) : DateTime.MinValue;
                                Request.RecievedDate = (item["RecievedDate"] != null) ? DateTime.Parse(item["RecievedDate"].ToString()) : DateTime.MinValue;

                            Request.RequestStatusId = new SPFieldLookupValue(item["RequestStatusId"].ToString()).LookupId;
                               
                                if ((Request.RequestStatusId == (int)Common.Utilities.RequestStatus.SCESectionManagerAccepted) || (Request.RequestStatusId == (int)Common.Utilities.RequestStatus.SCESectionManagerRejected))
                                    Request.IsRequestClosed = true;
                                else
                                    Request.IsRequestClosed = false;

                                SPFieldLookupValue ApplicantsID = new SPFieldLookupValue((item["Applicants_QID"] != null) ? item["Applicants_QID"].ToString() : string.Empty);
                                Request.QID = ApplicantsID.LookupValue;
                                if (LCID == (int)Language.English)
                                {
                                /*
                                    SPFieldLookupValue RequestStatus = new SPFieldLookupValue((item["RequestStatus_Code"] != null) ? item["RequestStatus"].ToString() : string.Empty);
                                    Request.RequestStatus = RequestStatus.LookupValue;

                                    Request.RequestStatus = requestStatusItem.ReviewerDescriptionEn;*/
                                    SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                                    Request.ApplicantName = ApplicantsEnglishName.LookupValue;

                                    SPFieldLookupValue Nationality = new SPFieldLookupValue((item["Nationality_Title"] != null) ? item["Nationality_Title"].ToString() : string.Empty);
                                    Request.Nationality = Nationality.LookupValue;

                                    if (item["CountryOfStudy_Title"] != null)
                                    {
                                        SPFieldLookupValue CertificateResource = new SPFieldLookupValue(item["CountryOfStudy_Title"].ToString());
                                        Request.CertificateResource = CertificateResource.LookupValue;
                                    }

                                    else if (item["OtherCertificateResource"] != null)
                                    {
                                    SPFieldLookupValue CertificateResource = new SPFieldLookupValue(item["OtherCertificateResource"].ToString());
                                    Request.CertificateResource = CertificateResource.LookupValue;
                                   

                                    }
                                  //  if (item["ClarificationReason"] != null)
                                  //  {
                                  //      SPFieldLookupValue ReturnReasons = new SPFieldLookupValue(item["ClarificationReason"].ToString());
                                  //      Request.ReturnReason = ReturnReasons.LookupValue;
                                  //  }
                                  //else if (item["OtherClarificationReason"] != null)
                                  //{
                                  //  SPFieldLookupValue ReturnReasons = new SPFieldLookupValue(item["OtherClarificationReason"].ToString());
                                  //  Request.ReturnReason = ReturnReasons.LookupValue;
                                  // }
                                if (item["ScholasticLevel_Title"] != null)
                                    {
                                        SPFieldLookupValue SchoolLastGrade = new SPFieldLookupValue(item["ScholasticLevel_Title"].ToString());
                                        Request.SchoolLastGrade = SchoolLastGrade.LookupValue;
                                    }
                                }
                                else
                                {

                                    SPFieldLookupValue Nationality = new SPFieldLookupValue((item["Nationality_TitleAr"] != null) ? item["Nationality_TitleAr"].ToString() : string.Empty);
                                    Request.Nationality = Nationality.LookupValue;
                                /*
                                    SPFieldLookupValue RequestStatus = new SPFieldLookupValue((item["RequestStatusAr"] != null) ? item["RequestStatusAr"].ToString() : string.Empty);
                                    Request.RequestStatus = RequestStatus.LookupValue;

                                    Request.RequestStatus = requestStatusItem.ReviewerDescriptionAr;

    */
                                    SPFieldLookupValue ApplicantsArabicName = new SPFieldLookupValue((item["Applicants_ArabicName"] != null) ? item["Applicants_ArabicName"].ToString() : string.Empty);
                                    Request.ApplicantName = ApplicantsArabicName.LookupValue;

                                    if (item["CountryOfStudy_TitleAr"] != null)
                                    {
                                        SPFieldLookupValue CertificateResource = new SPFieldLookupValue(item["CountryOfStudy_TitleAr"].ToString());
                                        Request.CertificateResource = CertificateResource.LookupValue;
                                    }
                                else if (item["SCERequests_OtherCertificateResource"] != null)
                                {
                                    SPFieldLookupValue CertificateResource = new SPFieldLookupValue(item["SCERequests_OtherCertificateResource"].ToString());
                                    Request.CertificateResource = CertificateResource.LookupValue;


                                }
                                //if (item["ClarificationReasonAr"] != null)
                                //{
                                //    SPFieldLookupValue ReturnReasons = new SPFieldLookupValue(item["ClarificationReasonAr"].ToString());
                                //    Request.ReturnReason = ReturnReasons.LookupValue;
                                //}
                                //else if (item["OtherClarificationReason"] != null)
                                //{
                                //    SPFieldLookupValue ReturnReasons = new SPFieldLookupValue(item["OtherClarificationReason"].ToString());
                                //    Request.ReturnReason = ReturnReasons.LookupValue;
                                //}
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
                        Logging.GetInstance().Debug("Exit ReturnedRequests.GetRequestsListing");
                    }
                    return Requests;
                }

            }
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
                        + "<FieldRef Name= 'ReturnReasons_Title' />"
                             + "<FieldRef Name= 'ReturnReason' />"
                             + "<FieldRef Name= 'ReturnDate' />"
                             + "<FieldRef Name= 'ReturnedBy' />"
                               + "<FieldRef Name='MobileNumber'/>";

                //+ "<FieldRef Name= 'RequestStatus_Code' />"
                //  + "<FieldRef Name= 'RequestStatus_ID' />"
                //               + "<FieldRef Name= 'ScholasticLevel_Title' />"
                //+ "<FieldRef Name= 'ClarificationReason' />"
                // + "<FieldRef Name= 'OtherClarificationReason' />"
                //   + "<FieldRef Name= 'Employee' />"
                //     + "<FieldRef Name= 'ReturnDate' />";
                // ManagerReturnReason







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

                + "<FieldRef Name='DelayedDays'/>"
                + "<FieldRef Name='CountryOfStudy_TitleAr'/>"
                 + "<FieldRef Name='ScholasticLevel_TitleAr'/>"
                + "<FieldRef Name= 'ReturnReasons_TitleAr' />"
                             + "<FieldRef Name= 'ReturnReason' />"
                             + "<FieldRef Name= 'ReturnDate' />"
                             + "<FieldRef Name= 'ReturnedBy' />"
                               + "<FieldRef Name='MobileNumber'/>";
                //       + "<FieldRef Name= 'ClarificationReason' />"
                //+ "<FieldRef Name= 'OtherClarificationReason' />"
                //  + "<FieldRef Name= 'Employee' />"
                //    + "<FieldRef Name= 'ReturnDate' />";
            }
            return strViewFields;
        }

    }
}





