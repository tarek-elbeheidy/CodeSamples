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
    public class ReassignRequests
    {
        public static IEnumerable<Entities.ReassignRequests> GetAllReassignRequests(string strQuery, int LCID)
        {
            List<Entities.ReassignRequests> Requests = new List<Entities.ReassignRequests>();
            using (SPSite oSite = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb oWeb = oSite.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method ReassignRequests.GetAllReassignRequests");
                        oWeb.AllowUnsafeUpdates = true;
                        string strViewFields = GetstrViewFields(LCID);
                        
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
                                      </Eq></Join>" +
"<Join Type='LEFT' ListAlias='Applicants'>" +
                                      "<Eq>" +
                                        "<FieldRef List='SCERequests' Name='ApplicantID' RefType='Id' />" +
                                        "<FieldRef List='Applicants' Name='Id' />" +
                                      "</Eq>" +
                                    "</Join><Join Type='INNER' ListAlias='Nationality'>" +
                                      "<Eq>" +
                                        "<FieldRef List='SCERequests' Name='StdNationality' RefType='Id' />" +
                                        "<FieldRef List='Nationality' Name='Id' />" +
                                      "</Eq>" +
                                    "</Join>" 
                                ;


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

                        //+"<Field Name='SCERequests_DelayedDays' Type='Lookup' "
                        //+ "List='SCERequests' ShowField='DelayedDays'/>"

                   
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
                                                "List='Nationality' ShowField='TitleAr'/>";
                                                





                        SPQuery reqQuery = Common.Utilities.BusinessHelper.GetQueryObject(strQuery, joins, projFields, strViewFields);


                        Requests = GetRequestsListing(reqQuery, LCID);
                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(ex);
                    }
                    finally
                    {
                        oWeb.AllowUnsafeUpdates = false;
                        Logging.GetInstance().Debug("Exiting method ReassignRequests.GetAllReassignRequests");
                    }
                }
            }
            return Requests;
        }
        public static string GetReassignRequestsQuery( int delayedDays)
        {
            Logging.GetInstance().Debug("Enter ReassignRequests.GetReassignRequestsQuery");
            string reassignQuery = string.Empty;
            try
            {
                //reassignQuery = @"<Where><And><And><Eq><FieldRef Name='RequestStatus_Code' /><Value Type='Lookup'>" + Common.Utilities.RequestStatus.SCEReturnForUnauthorized +
                //     "</Value></Eq><Or><Eq><FieldRef Name='SCERequests_EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName +
                //     "</Value></Eq><Eq><FieldRef Name='SCERequests_EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim()
                //     + "</Value></Eq></Or></And><Or><IsNull><FieldRef Name='SCERequests_DelayedDays' /></IsNull><Lt>" +
                //     "<FieldRef Name='SCERequests_DelayedDays' /><Value Type='Number'>" + delayedDays + "</Value></Lt>" +
                //     "</Or></And></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";

                reassignQuery= @"<Where><And><Eq><FieldRef Name='RequestStatus_Code' />
                           <Value Type='Lookup'>"+ Common.Utilities.RequestStatus.SCEReturnForUnauthorized +
                           "</Value></Eq><Eq><FieldRef Name='SCERequests_EmployeeAssignedTo' /><Value Type='Text'>" + Common.Utilities.Constants.ReassignEmployees + "</Value></Eq></And></Where>"+
                           "<OrderBy><FieldRef Name='SCERequests_RecievedDate' Ascending='False' /></OrderBy>"; 


            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ReassignRequests.GetReassignRequestsQuery");
            }
            return reassignQuery;
        }
        public static string GetstrViewFields(int LCID)
        {
            string strViewFields = string.Empty;
            if (LCID == (int)Language.English)
            {

                strViewFields = "<FieldRef Name='SCERequests_RequestNumber'/>"
            + "<FieldRef Name='CountryOfStudy_Title'/>"
            + "<FieldRef Name='Applicants_ApplicantName'/>"
             + "<FieldRef Name='Applicants_EnglishName'/>"
            + "<FieldRef Name='Applicants_QatarID/>"
            + "<FieldRef Name='Applicants_MobileNumber'/>"
            + "<FieldRef Name='Nationality_Title'/>"

            + "<FieldRef Name='ReturnedReasons'/>"

            + "<FieldRef Name='Employee'/>"


            + "<FieldRef Name='SCERequests_ID'/>"
                + "<FieldRef Name='SCERequests_StdPrintedName'/>"
                 + "<FieldRef Name='SCERequests_StdQatarID'/>"


                    + "<FieldRef Name='SCERequests_RecievedDate'/>"

                     + "<FieldRef Name= 'SCERequests_OtherCertificateResource' />"
                     + "<FieldRef Name= 'SCERequests_EmployeeAssignedTo' />"
                      + "<FieldRef Name= 'SCERequests_DelayedDays' />"

                    + "<FieldRef Name= 'RequestStatus_Code' />" 
                    +"<FieldRef Name= 'RequestStatus_ID' />";
                    


            }
            else
            {

                strViewFields = "<FieldRef Name='SCERequests_RequestNumber'/>"

           + "<FieldRef Name='CountryOfStudy_TitleAr'/>"

            + "<FieldRef Name='Applicants_ApplicantName'/>"
             + "<FieldRef Name='Applicants_ArabicName'/>"
            + "<FieldRef Name='Applicants_QatarID'/>"
            + "<FieldRef Name='Applicants_MobileNumber'/>"
            + "<FieldRef Name='Nationality_TitleAr'/>"

            + "<FieldRef Name='ReturnedReasons'/>"

            + "<FieldRef Name='Employee'/>"

            + "<FieldRef Name='SCERequests_ID'/>"
             + "<FieldRef Name='SCERequests_StdPrintedName'/>"
             + "<FieldRef Name='SCERequests_StdQatarID'/>"
                  + "<FieldRef Name='SCERequests_PrevSchool'/>"
                   + "<FieldRef Name='SCERequests_RecievedDate'/>"
                   + "<FieldRef Name= 'SCERequests_DelayedDays' />"
                     + "<FieldRef Name= 'SCERequests_OtherCertificateResource' />"
                     + "<FieldRef Name= 'SCERequests_EmployeeAssignedTo' />"
                      + "<FieldRef Name= 'RequestStatus_Code' />"
                       + "<FieldRef Name= 'RequestStatus_ID' />";



            }
            return strViewFields;
        }
        public static List<Entities.ReassignRequests> GetRequestsListing(SPQuery query, int LCID)
        {
            List<Entities.ReassignRequests> Requests = new List<Entities.ReassignRequests>();
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method ReassignRequests.GetRequestsListing");
                        SPList requestsList = web.Lists[Utilities.Constants.SCEUnauthorizedReason];
                        SPListItemCollection items = requestsList.GetItems(query);
                       // Common.Entities.RequestStatus requestStatusItem = new Common.Entities.RequestStatus();
                        foreach (SPListItem item in items)
                        {
                            //requestStatusItem = Common.BL.RequestStatus.GetRequestStatusById(new SPFieldLookupValue(item["RequestStatus_ID"].ToString()).LookupId);
                            Entities.ReassignRequests Request = new Entities.ReassignRequests();
                            //SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue((item["Applicants_QatarID"] != null) ? item["Applicants_QatarID"].ToString() : string.Empty);
                            //Request.QatariID = ApplicantsQatarID.LookupValue;

                            //SPFieldLookupValue ApplicantsMobileNumber = new SPFieldLookupValue((item["Applicants_MobileNumber"] != null) ? item["Applicants_MobileNumber"].ToString() : string.Empty);
                            //Request.ApplicantMobileNumber = ApplicantsMobileNumber.LookupValue;

                           
                            //if (item["SCERequests_DelayedDays"] != null)
                            //{
                            //    SPFieldLookupValue DelayedDays = new SPFieldLookupValue(item["SCERequests_DelayedDays"].ToString());
                            //    Request.DelayedDays = DelayedDays.LookupValue;
                            //}
                            if (item["SCERequests_EmployeeAssignedTo"] != null)
                            {
                                SPFieldLookupValue EmployeeAssignedTo = new SPFieldLookupValue(item["SCERequests_EmployeeAssignedTo"].ToString());
                                Request.AssignedTo = EmployeeAssignedTo.LookupValue;
                            }
                            if (item["SCERequests_ID"] != null)
                            {
                                SPFieldLookupValue RequestID = new SPFieldLookupValue(item["SCERequests_ID"].ToString());
                                Request.RequestID = RequestID.LookupValue;
                            }
                            if (item["SCERequests_RequestNumber"] != null)
                            {
                                SPFieldLookupValue RequestNumber = new SPFieldLookupValue(item["SCERequests_RequestNumber"].ToString());
                                Request.RequestNumber = RequestNumber.LookupValue;
                            }
                            SPFieldLookupValue RecievedDate = new SPFieldLookupValue((item["SCERequests_RecievedDate"] != null) ? item["SCERequests_RecievedDate"].ToString() : string.Empty);
                            Request.RecievedDate = DateTime.Parse(RecievedDate.LookupValue);

                            SPFieldLookupValue StatusId = new SPFieldLookupValue((item["RequestStatus_ID"] != null) ? item["RequestStatus_ID"].ToString() : string.Empty);
                            Request.RequestStatusId = StatusId.LookupId;
                            //if ((Request.RequestStatusId == (int)Common.Utilities.RequestStatus.SCESectionManagerAccepted) || (Request.RequestStatusId == (int)Common.Utilities.RequestStatus.SCESectionManagerRejected))
                            //    Request.IsRequestClosed = true;
                            //else
                            //    Request.IsRequestClosed = false;

                            //SPFieldLookupValue ApplicantsID = new SPFieldLookupValue((item["Applicants_QID"] != null) ? item["Applicants_QID"].ToString() : string.Empty);
                            //Request.QID = ApplicantsID.LookupValue;


                            if (item["SCERequests_StdPrintedName"] != null)
                            {
                                SPFieldLookupValue StudentNameAccToCert = new SPFieldLookupValue(item["SCERequests_StdPrintedName"].ToString());
                                Request.StudentNameAccToCert = StudentNameAccToCert.LookupValue;
                            }

                            if (item["SCERequests_StdQatarID"] != null)
                            {
                                SPFieldLookupValue CertificateHolderQatarID = new SPFieldLookupValue(item["SCERequests_StdQatarID"].ToString());
                                Request.CertificateHolderQatarID = CertificateHolderQatarID.LookupValue;
                            }

                            Request.ReturnReason = item["ReturnedReasons"] != null ? Convert.ToString(item["ReturnedReasons"]) : string.Empty;
                            
                            if (item["Employee"] != null)
                            {
                                SPFieldLookupValue ReturnedFrom = new SPFieldLookupValue(item["Employee"].ToString());
                                Request.ReturnedFrom = ReturnedFrom.LookupValue;
                                
                            }

                                if (LCID == (int)Language.English)
                            {
                                SPFieldLookupValue NationalityTitle = new SPFieldLookupValue((item["Nationality_Title"] != null) ? item["Nationality_Title"].ToString() : string.Empty);
                                Request.Nationality = NationalityTitle.LookupValue;
                                /*SPFieldLookupValue RequestStatus = new SPFieldLookupValue((item["RequestStatus"] != null) ? item["RequestStatus"].ToString() : string.Empty);
                                Request.RequestStatus = RequestStatus.LookupValue;

                                Request.RequestStatus = requestStatusItem.ReviewerDescriptionEn;*/
                                SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                                Request.ApplicantName = ApplicantsEnglishName.LookupValue;


                                if (item["CountryOfStudy_Title"] != null)
                                {
                                    SPFieldLookupValue CertificateResource = new SPFieldLookupValue(item["CountryOfStudy_Title"].ToString());
                                    Request.CertificateResource = CertificateResource.LookupValue;
                                }

                                else if (item["SCERequests_OtherCertificateResource"] != null)
                                {

                                    SPFieldLookupValue CertificateResource = new SPFieldLookupValue(item["SCERequests_OtherCertificateResource"].ToString());
                                    Request.CertificateResource = CertificateResource.LookupValue;
                                }
                               

                                

                                //if (item["ScholasticLevel_Title"] != null)
                                //{
                                //    SPFieldLookupValue SchoolLastGrade = new SPFieldLookupValue(item["ScholasticLevel_Title"].ToString());
                                //    Request.SchoolLastGrade = SchoolLastGrade.LookupValue;
                                //}
                            }
                            else
                            {
                                SPFieldLookupValue NationalityTitleAr = new SPFieldLookupValue((item["Nationality_TitleAr"] != null) ? item["Nationality_TitleAr"].ToString() : string.Empty);
                                Request.Nationality = NationalityTitleAr.LookupValue;

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

                                //if (item["ScholasticLevel_TitleAr"] != null)
                                //{
                                //    SPFieldLookupValue SchoolLastGrade = new SPFieldLookupValue(item["ScholasticLevel_TitleAr"].ToString());
                                //    Request.SchoolLastGrade = SchoolLastGrade.LookupValue;
                                //}

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
                        Logging.GetInstance().Debug("Exit ReassignRequests.GetRequestsListing");
                    }
                    return Requests;
                }
            }
        }
            }
}
