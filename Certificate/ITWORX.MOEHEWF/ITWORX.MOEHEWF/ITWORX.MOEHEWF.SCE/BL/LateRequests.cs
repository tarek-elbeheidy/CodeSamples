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
    public class LateRequests
    {
        public static IEnumerable<Entities.LateRequests> GetAllLateRequests(string strQuery, int LCID)
        {
            List<Entities.LateRequests> Requests = new List<Entities.LateRequests>();
            using (SPSite oSite = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb oWeb = oSite.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method LateRequests.GetAllLateRequests");
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
                                      "<FieldRef  Name='StdNationality' RefType='Id'/>" +
                                      "<FieldRef List='Nationality' Name='ID'/>" +
                                      "</Eq>" +
                                      "</Join>";

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

                                                 "<Field Name='ScholasticLevel_Title' Type='Lookup' " +
                                                "List='ScholasticLevel' ShowField='Title'/>" +

                                                "<Field Name='ScholasticLevel_TitleAr' Type='Lookup' " +
                                                "List='ScholasticLevel' ShowField='TitleAr'/>";

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
                        Logging.GetInstance().Debug("Exiting method LateRequests.GetAllLateRequests");
                    }
                    return Requests;
                }
            }
        }

        public static List<string> GetLateQueryPerRole()
        {
            List<string> objColumns = new List<string>();
            int delayedDays = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SCEDelayedDays"));
            objColumns.Add("DelayedDays;Number;Gt;" + delayedDays.ToString());
            return objColumns;
        }

        public static string GetLateRequestsQuery(string GroupName, int delayedDays)
        {
            Logging.GetInstance().Debug("Enter LateRequests.GetLateRequestsQuery");
            string lateQuery = string.Empty;
            try
            {

                //lateQuery = @"<Where><And><Gt><FieldRef Name='DelayedDays' /><Value Type='Number'>" + delayedDays +
                //    "</Value></Gt><Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName +
                //    "</Value></Eq><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim() +
                //    "</Value></Eq></Or></And></Where>" +
                //    "<OrderBy><FieldRef Name = 'RecievedDate' Ascending = 'False' /></OrderBy >";


                lateQuery  = @"<Where><And><And><Eq><FieldRef Name='IsEmployee' /><Value Type='Choice'>No</Value></Eq><Gt><FieldRef Name='DelayedDays' /><Value Type='Number'>"+delayedDays+
                    "</Value></Gt></And><Or><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>"+ SPContext.Current.Web.CurrentUser.LoginName+
                    "</Value></Eq><Eq><FieldRef Name='EmployeeAssignedTo' /><Value Type='Text'>" + GroupName.Trim() +
                    "</Value></Eq></Or></And></Where><OrderBy><FieldRef Name='RecievedDate' Ascending='False' /></OrderBy>";
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit LateRequests.GetLateRequestsQuery");
            }
            return lateQuery;
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
                +"<FieldRef Name='MobileNumber'/>";


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
                   + "<FieldRef Name='MobileNumber'/>";
            }
            return strViewFields;
        }
        public static List<Entities.LateRequests> GetRequestsListing(SPQuery query, int LCID)
        {
            List<Entities.LateRequests> Requests = new List<Entities.LateRequests>();
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method LateRequests.GetRequestsListing");
                        SPList requestList = web.Lists[Utilities.Constants.SCERequests];
                        SPListItemCollection items = requestList.GetItems(query);
                        Common.Entities.RequestStatus requestStatusItem = new Common.Entities.RequestStatus();
                        foreach (SPListItem item in items)
                        {
                            requestStatusItem = Common.BL.RequestStatus.GetRequestStatusById(new SPFieldLookupValue(item["RequestStatusId"].ToString()).LookupId);
                            Entities.LateRequests Request = new Entities.LateRequests();
                            SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue((item["Applicants_QatarID"] != null) ? item["Applicants_QatarID"].ToString() : string.Empty);
                            Request.QatariID = ApplicantsQatarID.LookupValue;

                            //SPFieldLookupValue ApplicantsMobileNumber = new SPFieldLookupValue((item["Applicants_MobileNumber"] != null) ? item["Applicants_MobileNumber"].ToString() : string.Empty);
                          //  Request.ApplicantMobileNumber = ApplicantsMobileNumber.LookupValue;
                            Request.ApplicantMobileNumber = item["MobileNumber"] != null ? Convert.ToString(item["MobileNumber"]) : string.Empty;


                            Request.DelayedDays = (item["DelayedDays"] != null) ? item["DelayedDays"].ToString() : string.Empty;
                            Request.AssignedTo = (item["EmployeeAssignedTo"] != null) ? item["EmployeeAssignedTo"].ToString() : string.Empty;
                            Request.ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;
                            Request.RequestNumber = (item["RequestNumber"] != null) ? item["RequestNumber"].ToString() : string.Empty;

                           /* if (requestStatusItem.Code == Common.Utilities.RequestStatus.PAEmployeeClarificationReplay.ToString())
                            {

                                if (LCID == (int)Language.English)
                                {
                                    SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                                    Request.RejectedFrom = ApplicantsEnglishName.LookupValue;
                                }
                                else
                                {
                                    SPFieldLookupValue ApplicantsArabicName = new SPFieldLookupValue((item["Applicants_ArabicName"] != null) ? item["Applicants_ArabicName"].ToString() : string.Empty);
                                    Request.RejectedFrom = ApplicantsArabicName.LookupValue;
                                }
                            }
                            else
                            {
                                Request.RejectedFrom = (item["RejectedFrom"] != null) ? item["RejectedFrom"].ToString() : string.Empty;
                            }
                            */

                           // Request.RejectionReason = (item["RejectionReason"] != null) ? item["RejectionReason"].ToString() : string.Empty;
                            //Request.RejectionDate = (item["RejectionDate"] != null) ? DateTime.Parse(item["RejectionDate"].ToString()) : DateTime.MinValue;
                            Request.RecievedDate = (item["RecievedDate"] != null) ? DateTime.Parse(item["RecievedDate"].ToString()) : DateTime.MinValue;

                            SPFieldLookupValue StatusId = new SPFieldLookupValue((item["RequestStatusId"] != null) ? item["RequestStatusId"].ToString() : string.Empty);
                            Request.RequestStatusId = StatusId.LookupId;
                            if ((Request.RequestStatusId == (int)Common.Utilities.RequestStatus.SCESectionManagerAccepted) || (Request.RequestStatusId == (int)Common.Utilities.RequestStatus.SCESectionManagerRejected))
                                Request.IsRequestClosed = true;
                            else
                                Request.IsRequestClosed = false;

                            SPFieldLookupValue ApplicantsID = new SPFieldLookupValue((item["Applicants_QID"] != null) ? item["Applicants_QID"].ToString() : string.Empty);
                            Request.QID = ApplicantsID.LookupValue;

                           
                            Request.StudentNameAccToCert = item["StdPrintedName"] != null ? Convert.ToString(item["StdPrintedName"]) : string.Empty;

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
                        Logging.GetInstance().Debug("Exit LateRequests.GetRequestsListing");
                    }
                    return Requests;
                }

            }
        }
    }

}
