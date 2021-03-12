using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHEWF.PA.Entities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class PASearchSimilarRequests : UserControlBase
    {
        public static IEnumerable<SimilarRequest> GetAllRequests(string strQuery, int LCID)
        {
            List<Entities.SimilarRequest> Requests = new List<Entities.SimilarRequest>();
            try
            {
                Logging.GetInstance().Debug("Entering method PASearchSimilarRequests.GetAllRequests");
                using (SPWeb web = new SPSite(SPContext.Current.Site.Url).OpenWeb())
                {
                    string strViewFields = string.Empty;

                    if (LCID == (int)Language.English)
                    {
                        strViewFields = "<FieldRef Name='RequestNumber'/>"
                              + "<FieldRef Name='Applicants_QatarID'/>"
                              + "<FieldRef Name='Applicants_QID'/>"
                              + "<FieldRef Name='Applicants_ApplicantName'/>"
                              + "<FieldRef Name='Applicants_EnglishName'/>"
                              + "<FieldRef Name='Applicants_ArabicName'/>"
                              + "<FieldRef Name='SubmitDate'/>"
                              + "<FieldRef Name='Nationality_Title'/>"
                              + "<FieldRef Name='NationalityCategory_Title'/>"
                              + "<FieldRef Name='AcademicDegree'/>"
                              + "<FieldRef Name='CountryOfStudy'/>"
                              + "<FieldRef Name='University'/>"
                              + "<FieldRef Name='ID'/>"
                              + "<FieldRef Name='Faculty'/>"
                              + "<FieldRef Name='ProgramSpecialization'/>"
                              + "<FieldRef Name='RequestStatus'/>"
                              + "<FieldRef Name='EntityNeedsEquivalency'/>"
                              + "<FieldRef Name='RequestStatusId'/>"
                              + "<FieldRef Name='EmployeeAssignedTo'/>"
                              + "<FieldRef Name='AcademicDegreeForEquivalence'/>"
                              + "<FieldRef Name= 'Attachments' />"
                              + "<FieldRef Name= 'ProgramType' />"
                              + "<FieldRef Name= 'ProgramCountry' />"
                              + "<FieldRef Name= 'ProgramFaculty' />"
                              + "<FieldRef Name= 'ProgramUniversity' />"
                              + "<FieldRef Name= 'OtherPAUniversityOfStudy' />"
                              + "<FieldRef Name= 'StudySystem' />";
                    }
                    else 
                    {
                        strViewFields = "<FieldRef Name='RequestNumber'/>"
                             + "<FieldRef Name='Applicants_QatarID'/>"
                             + "<FieldRef Name='Applicants_QID'/>"
                             + "<FieldRef Name='Applicants_ApplicantName'/>"
                             + "<FieldRef Name='Applicants_EnglishName'/>"
                             + "<FieldRef Name='Applicants_ArabicName'/>"
                             + "<FieldRef Name='SubmitDate'/>"
                             + "<FieldRef Name='Nationality_TitleAr'/>"
                             + "<FieldRef Name='NationalityCategory_TitleAr'/>"
                             + "<FieldRef Name='AcademicDegreeAr'/>"
                             + "<FieldRef Name='CountryOfStudyAr'/>"
                             + "<FieldRef Name='UniversityAr'/>"
                             + "<FieldRef Name='ID'/>"
                             + "<FieldRef Name='FacultyAr'/>"
                             + "<FieldRef Name='ProgramSpecializationAr'/>"
                             + "<FieldRef Name='RequestStatusAr'/>"
                             + "<FieldRef Name='EntityNeedsEquivalencyAr'/>"
                             + "<FieldRef Name='RequestStatusId'/>"
                             + "<FieldRef Name='EmployeeAssignedTo'/>"
                             + "<FieldRef Name='AcademicDegreeForEquivalenceAr'/>"
                             + "<FieldRef Name= 'Attachments' />"
                             + "<FieldRef Name= 'ProgramTypeAr' />"
                             + "<FieldRef Name= 'ProgramCountryAr' />"
                             + "<FieldRef Name= 'ProgramUniversityAr' />"
                             + "<FieldRef Name= 'ProgramFaculty' />"
                             + "<FieldRef Name= 'OtherPAUniversityOfStudy' />"
                             + "<FieldRef Name= 'StudySystemAr' />";
                    }
                    SPQuery query = new SPQuery
                    {
                        Joins = "<Join Type='INNER' ListAlias='Applicants'>" +
                                  "<Eq>" +
                                  "<FieldRef Name='ApplicantID' RefType='Id'/>" +
                                  "<FieldRef List='Applicants' Name='ID'/>" +
                                  "</Eq>" +
                                  "</Join>" +
                                  "<Join Type='INNER' ListAlias='Nationality'>" +
                                  "<Eq>" +
                                  "<FieldRef List='Applicants' Name='Nationality' RefType='Id'/>" +
                                  "<FieldRef List='Nationality' Name='ID'/>" +
                                  "</Eq>" +
                                  "</Join>" +
                                  "<Join Type='INNER' ListAlias='NationalityCategory'>" +
                                  "<Eq>" +
                                  "<FieldRef List='Applicants' Name='NationalityCategory' RefType='Id'/>" +
                                  "<FieldRef List='NationalityCategory' Name='ID'/>" +
                                  "</Eq>" +
                                  "</Join>",

                        ProjectedFields = "<Field Name='Applicants_QatarID' Type='Lookup' " +
                                            "List='Applicants' ShowField='PersonalID'/>" +

                                              "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ApplicantName'/>" +
                                                 "<Field Name='Applicants_ArabicName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ArabicName'/>" +
                                                "<Field Name='Applicants_EnglishName' Type='Lookup' " +
                                                "List='Applicants' ShowField='EnglishName'/>" +
                                                "<Field Name='Applicants_QID' Type='Lookup' " +
                                                "List='Applicants' ShowField='ID'/>" +
                                            "<Field Name='Nationality_Title' Type='Lookup' " +
                                            "List='Nationality' ShowField='Title'/>" +
                                            "<Field Name='Nationality_TitleAr' Type='Lookup' " +
                                            "List='Nationality' ShowField='TitleAr'/>" +
                                            "<Field Name='NationalityCategory_Title' Type='Lookup' " +
                                            "List='NationalityCategory' ShowField='Title'/>" +
                                            "<Field Name='NationalityCategory_TitleAr' Type='Lookup' " +
                                            "List='NationalityCategory' ShowField='TitleAr'/>",

                        ViewFields = strViewFields,
                        Query = strQuery,
                    };

                    SPList customerList = web.Lists[Utilities.Constants.PARequests];
                   
                    SPListItemCollection items = customerList.GetItems(query);
                    Common.Entities.RequestStatus requestStatusItem = new Common.Entities.RequestStatus();
                    foreach (SPListItem item in items)
                    {
                        if (item["RequestStatusId"] != null)
                        {
                            requestStatusItem = Common.BL.RequestStatus.GetRequestStatusById(new SPFieldLookupValue(item["RequestStatusId"].ToString()).LookupId);
                            SimilarRequest Request = new SimilarRequest();
                            SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue((item["Applicants_QatarID"] != null) ? item["Applicants_QatarID"].ToString() : string.Empty);
                            Request.QatariID = ApplicantsQatarID.LookupValue;

                            Request.ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;
                            Request.RowAssignedTo = (item["EmployeeAssignedTo"] != null) ? item["EmployeeAssignedTo"].ToString() : string.Empty;
                            Request.RequestNumber = (item["RequestNumber"] != null) ? item["RequestNumber"].ToString() : string.Empty;
                            Request.SubmitDate = (item["SubmitDate"] != null) ? DateTime.Parse(item["SubmitDate"].ToString()) : DateTime.MinValue;
                            SPFieldLookupValue StatusId = new SPFieldLookupValue((item["RequestStatusId"] != null) ? item["RequestStatusId"].ToString() : string.Empty);
                            Request.RequestStatusId = StatusId.LookupId;
                            if ((Request.RequestStatusId == (int)RequestStatus.PAHeadManagerAccepted) /*|| (Request.RequestStatusId == (int)RequestStatus.PAHeadManagerRejected)*/)
                                Request.IsRequestClosed = true;
                            else
                                Request.IsRequestClosed = false;


                            if (item["EmployeeAssignedTo"] != null)
                            {
                                Request.AssignedTo = item["EmployeeAssignedTo"].ToString();
                                if (Request.AssignedTo.Contains("\\") || Request.AssignedTo.Contains("|"))
                                {
                                    SPUser user = web.EnsureUser(Request.AssignedTo);
                                    Request.AssignedTo = user.Name;
                                }
                            }

                            Request.AttachmentURL = item.Attachments.Count != 0 ? SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, item.Attachments[0]) : string.Empty;
                            Request.FileName = item.Attachments.Count != 0 ? item.Attachments[0] : string.Empty;

                            SPFieldLookupValue ApplicantsID = new SPFieldLookupValue((item["Applicants_QID"] != null) ? item["Applicants_QID"].ToString() : string.Empty);
                            Request.QID = ApplicantsID.LookupValue;

                            if (item["ProgramFaculty"] != null)
                            {
                                Request.ProgramFaculty = item["ProgramFaculty"].ToString();
                            }

                            if (LCID == (int)Language.English)
                            {
                                SPFieldLookupValue AcademicDegree = new SPFieldLookupValue((item["AcademicDegreeForEquivalence"] != null) ? item["AcademicDegreeForEquivalence"].ToString() : string.Empty);
                                Request.AcademicDegree = AcademicDegree.LookupValue;

                                SPFieldLookupValue ProgramType = new SPFieldLookupValue((item["ProgramType"] != null) ? item["ProgramType"].ToString() : string.Empty);
                                Request.ProgramType = ProgramType.LookupValue;

                                if (item["Nationality_Title"] != null)
                                {
                                    SPFieldLookupValue NationalityTitle = new SPFieldLookupValue(item["Nationality_Title"].ToString());
                                    Request.Nationality = NationalityTitle.LookupValue;
                                }
                                if (item["NationalityCategory_Title"] != null)
                                {
                                    SPFieldLookupValue NationCatgeory = new SPFieldLookupValue(item["NationalityCategory_Title"].ToString());
                                    Request.NationCatgeory = NationCatgeory.LookupValue;
                                }
                                if (item["RequestStatus"] != null)
                                {
                                    SPFieldLookupValue RequestStatus = new SPFieldLookupValue(item["RequestStatus"].ToString());
                                    Request.RequestStatus = RequestStatus.LookupValue;
                                }
                                if (item["CountryOfStudy"] != null)
                                {
                                    SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue(item["CountryOfStudy"].ToString());
                                    Request.Country = CountryOfStudy.LookupValue;
                                }
                                if (item["University"] != null)
                                {
                                    SPFieldLookupValue University = new SPFieldLookupValue(item["University"].ToString());
                                    Request.University = University.LookupValue;
                                }
                                if (item["Faculty"] != null)
                                {
                                    Request.Faculty = item["Faculty"].ToString();
                                }
                                SPFieldLookupValue Specialization = new SPFieldLookupValue((item["ProgramSpecialization"] != null) ? item["ProgramSpecialization"].ToString() : string.Empty);
                                Request.Specialization = Specialization.LookupValue;

                                SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                                Request.ApplicantName =Request.EnglishName = ApplicantsEnglishName.LookupValue;


                                SPFieldLookupValue ApplicantsArabicName = new SPFieldLookupValue((item["Applicants_ArabicName"] != null) ? item["Applicants_ArabicName"].ToString() : string.Empty);
                                Request.ArabicName = ApplicantsArabicName.LookupValue;

                                SPFieldLookupValue StudySystem = new SPFieldLookupValue((item["StudySystem"] != null) ? item["StudySystem"].ToString() : string.Empty);
                                Request.StudySystem = StudySystem.LookupValue;

                                SPFieldLookupValue ProgramUniversity = new SPFieldLookupValue((item["ProgramUniversity"] != null) ? item["ProgramUniversity"].ToString() : string.Empty);
                                if (ProgramUniversity.LookupValue != null)
                                    Request.ProgramUniversity = ProgramUniversity.LookupValue;
                                else
                                    Request.ProgramUniversity = (item["OtherPAUniversityOfStudy"] != null) ? item["OtherPAUniversityOfStudy"].ToString() : string.Empty;

                                SPFieldLookupValue ProgramCountry = new SPFieldLookupValue((item["ProgramCountry"] != null) ? item["ProgramCountry"].ToString() : string.Empty);
                                Request.ProgramCountry = ProgramCountry.LookupValue;
                            }
                            else
                            {
                                SPFieldLookupValue AcademicDegree = new SPFieldLookupValue((item["AcademicDegreeForEquivalenceAr"] != null) ? item["AcademicDegreeForEquivalenceAr"].ToString() : string.Empty);
                                Request.AcademicDegree = AcademicDegree.LookupValue;

                                SPFieldLookupValue ProgramType = new SPFieldLookupValue((item["ProgramTypeAr"] != null) ? item["ProgramTypeAr"].ToString() : string.Empty);
                                Request.ProgramType = ProgramType.LookupValue;

                                if (item["Nationality_TitleAr"] != null)
                                {
                                    SPFieldLookupValue NationalityTitleAr = new SPFieldLookupValue(item["Nationality_TitleAr"].ToString());
                                    Request.Nationality = NationalityTitleAr.LookupValue;
                                }
                                if (item["NationalityCategory_TitleAr"] != null)
                                {
                                    SPFieldLookupValue NationCatgeory = new SPFieldLookupValue(item["NationalityCategory_TitleAr"].ToString());
                                    Request.NationCatgeory = NationCatgeory.LookupValue;
                                }
                                if (item["RequestStatusAr"] != null)
                                {
                                    SPFieldLookupValue RequestStatus = new SPFieldLookupValue(item["RequestStatusAr"].ToString());
                                    Request.RequestStatus = RequestStatus.LookupValue;
                                }
                                if (item["CountryOfStudyAr"] != null)
                                {
                                    SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue(item["CountryOfStudyAr"].ToString());
                                    Request.Country = CountryOfStudy.LookupValue;
                                }
                                if (item["UniversityAr"] != null)
                                {
                                    SPFieldLookupValue University = new SPFieldLookupValue(item["UniversityAr"].ToString());
                                    Request.University = University.LookupValue;
                                }
                                if (item["FacultyAr"] != null)
                                {
                                    Request.Faculty = item["FacultyAr"].ToString();
                                }
                                SPFieldLookupValue Specialization = new SPFieldLookupValue((item["ProgramSpecializationAr"] != null) ? item["ProgramSpecializationAr"].ToString() : string.Empty);
                                Request.Specialization = Specialization.LookupValue;

                                SPFieldLookupValue ApplicantsArabicName = new SPFieldLookupValue((item["Applicants_ArabicName"] != null) ? item["Applicants_ArabicName"].ToString() : string.Empty);
                                Request.ApplicantName =Request.ArabicName= ApplicantsArabicName.LookupValue;

                                SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                                Request.EnglishName = ApplicantsEnglishName.LookupValue;

                                SPFieldLookupValue StudySystem = new SPFieldLookupValue((item["StudySystemAr"] != null) ? item["StudySystemAr"].ToString() : string.Empty);
                                Request.StudySystem = StudySystem.LookupValue;

                                SPFieldLookupValue ProgramUniversity = new SPFieldLookupValue((item["ProgramUniversityAr"] != null) ? item["ProgramUniversityAr"].ToString() : string.Empty);
                                if (ProgramUniversity.LookupValue != null)
                                    Request.ProgramUniversity = ProgramUniversity.LookupValue;
                                else
                                    Request.ProgramUniversity = (item["OtherPAUniversityOfStudy"] != null) ? item["OtherPAUniversityOfStudy"].ToString() : string.Empty;

                                SPFieldLookupValue ProgramCountry = new SPFieldLookupValue((item["ProgramCountryAr"] != null) ? item["ProgramCountryAr"].ToString() : string.Empty);
                                Request.ProgramCountry = ProgramCountry.LookupValue;
                            }
                            Requests.Add(Request);
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
                Logging.GetInstance().Debug("Exiting method PASearchSimilarRequests.GetAllRequests");
            }
            return Requests;
        }

        public static List<int> GetCheckedRequests(int reqID)
        {
            List<int> requestsID = new List<int>();
            try
            {
                Logging.GetInstance().Debug("Entering method PASearchSimilarRequests.GetCheckedRequests");
                SPSite site = new SPSite(SPContext.Current.Site.Url);
                SPWeb web = site.OpenWeb();

                SPList requestsList = web.Lists[Utilities.Constants.PARequests];
                if (requestsList == null)
                    throw new Exception();
                SPListItem item = requestsList.GetItemById(reqID);
                SPFieldLookupValueCollection values = new SPFieldLookupValueCollection(item["RelatedRequests"].ToString());
                if (values != null)
                {
                    foreach (SPFieldLookupValue value in values)

                    {
                        requestsID.Add(value.LookupId);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PASearchSimilarRequests.GetCheckedRequests");
            }
            return requestsID;
        }

        public static bool GetRequestStatusbyReqID(string reqID)
        {
            bool IsRequestClosed = false;
            try
            {
                SimilarRequest Request = new SimilarRequest();

                Logging.GetInstance().Debug("Entering method PASearchSimilarRequests.GetAllRequests");
                using (SPWeb web = new SPSite(SPContext.Current.Site.Url).OpenWeb())
                {
                    SPList reqs = web.Lists[Utilities.Constants.PARequests];
                    SPListItem item = reqs.GetItemById(Convert.ToInt32(reqID));
                    SPFieldLookupValue StatusId = new SPFieldLookupValue((item["RequestStatusId"] != null) ? item["RequestStatusId"].ToString() : string.Empty);
                    Request.RequestStatusId = StatusId.LookupId;
                    if ((Request.RequestStatusId == (int)RequestStatus.PAClosedByAcceptance) || (Request.RequestStatusId == (int)RequestStatus.PAClosedByRejection))
                        IsRequestClosed = true;
                    else
                        IsRequestClosed = false;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PASearchSimilarRequests.GetAllRequests");
            }
            return IsRequestClosed;
        }

        public static List<SimilarRequest> GetAttachments(SPListItem spListItem)
        {
            List<SimilarRequest> attachments = new List<SimilarRequest>();
            try
            {
                var attachs = spListItem.Attachments;
                foreach (String attachmentName in attachs)
                {
                    string attachmentAbsoluteURL = spListItem.Attachments.UrlPrefix + attachmentName;
                    SPFile attachmentFile = spListItem.Web.GetFile(attachmentAbsoluteURL);
                    SimilarRequest attachment = new SimilarRequest();
                    attachment.FileName = attachmentName;
                    attachment.AttachmentURL = attachmentAbsoluteURL;
                    attachments.Add(attachment);
                }
            }
            catch
            {
            }
            return attachments;
        }

        public static IEnumerable<SimilarRequest> GetSimilarRequstsbyReqId(List<int> requests, int LCID)
        {
            List<Entities.SimilarRequest> Requests = new List<Entities.SimilarRequest>();
            try
            {
                Logging.GetInstance().Debug("Entering method PASearchSimilarRequests.GetAllRequests");
                using (SPWeb web = new SPSite(SPContext.Current.Site.Url).OpenWeb())
                {
                    foreach (int id in requests)
                    {
                        SimilarRequest Request = new SimilarRequest();
                        string strViewFields = string.Empty;

                        if (LCID == (int)Language.English)
                        {
                            strViewFields = "<FieldRef Name='RequestNumber'/>"
                                  + "<FieldRef Name='Applicants_QatarID'/>"
                                  + "<FieldRef Name='Applicants_ApplicantName'/>"
                                  + "<FieldRef Name='SubmitDate'/>"
                                  + "<FieldRef Name='Nationality_Title'/>"
                                  + "<FieldRef Name='AcademicDegree'/>"
                                  + "<FieldRef Name='CountryOfStudy'/>"
                                  + "<FieldRef Name='University'/>"
                                  + "<FieldRef Name='ID'/>"
                                  + "<FieldRef Name='Faculty'/>"
                                  + "<FieldRef Name='Specialization'/>"
                                  + "<FieldRef Name='RequestStatus'/>"
                                  + "<FieldRef Name='EntityNeedsEquivalency'/>"
                                  + "<FieldRef Name='RequestStatusId'/>"
                                  + "<FieldRef Name='EmployeeAssignedTo'/>"
                                  + "<FieldRef Name='HighestCertificate'/>"
                                  + "<FieldRef Name= 'Attachments' />"
                                  + "<FieldRef Name= 'OrgBookReply' />";
                        }
                        else
                        {
                            strViewFields = "<FieldRef Name='RequestNumber'/>"
                                 + "<FieldRef Name='Applicants_QatarID'/>"
                                 + "<FieldRef Name='Applicants_ApplicantName'/>"
                                 + "<FieldRef Name='SubmitDate'/>"
                                 + "<FieldRef Name='Nationality_TitleAr'/>"
                                 + "<FieldRef Name='AcademicDegreeAr'/>"
                                 + "<FieldRef Name='CountryOfStudyAr'/>"
                                 + "<FieldRef Name='UniversityAr'/>"
                                 + "<FieldRef Name='ID'/>"
                                 + "<FieldRef Name='FacultyAr'/>"
                                 + "<FieldRef Name='RequestStatusAr'/>"
                                 + "<FieldRef Name='EmployeeAssignedTo'/>"
                                 + "<FieldRef Name='SpecializationAr'/>"
                                 + "<FieldRef Name='RequestStatusId'/>"
                                 + "<FieldRef Name='EntityNeedsEquivalencyAr'/>"
                                 + "<FieldRef Name='HighestCertificateAr'/>"
                                 + "<FieldRef Name= 'Attachments' />"
                                 + "<FieldRef Name= 'OrgBookReply' />";
                        }
                        SPQuery query = new SPQuery
                        {
                            Joins = "<Join Type='INNER' ListAlias='Applicants'>" +
                                      "<Eq>" +
                                      "<FieldRef Name='ApplicantID' RefType='Id'/>" +
                                      "<FieldRef List='Applicants' Name='ID'/>" +
                                      "</Eq>" +
                                      "</Join>" +
                                      "<Join Type='INNER' ListAlias='Nationality'>" +
                                      "<Eq>" +
                                      "<FieldRef List='Applicants' Name='Nationality' RefType='Id'/>" +
                                      "<FieldRef List='Nationality' Name='ID'/>" +
                                      "</Eq>" +
                                      "</Join>",

                            ProjectedFields = "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ApplicantName'/>" +
                                                "<Field Name='Applicants_QatarID' Type='Lookup' " +
                                                "List='Applicants' ShowField='PersonalID'/>" +
                                                "<Field Name='Nationality_Title' Type='Lookup' " +
                                                "List='Nationality' ShowField='Title'/>" +
                                                "<Field Name='Nationality_TitleAr' Type='Lookup' " +
                                                "List='Nationality' ShowField='TitleAr'/>",

                            ViewFields = strViewFields,
                            Query = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + id + "</Value></Eq></Where>",
                        };

                        SPList reqsList = web.Lists[Utilities.Constants.PARequests];
                        SPListItemCollection items = reqsList.GetItems(query);
                        foreach (SPListItem item in items)
                        {
                            Common.Entities.RequestStatus requestStatusItem = new Common.Entities.RequestStatus();
                            requestStatusItem = Common.BL.RequestStatus.GetRequestStatusById(new SPFieldLookupValue(item["RequestStatusId"].ToString()).LookupId);
                            SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue((item["Applicants_QatarID"] != null) ? item["Applicants_QatarID"].ToString() : string.Empty);
                            Request.QatariID = ApplicantsQatarID.LookupValue;
                            SPFieldLookupValue ApplicantsApplicantName = new SPFieldLookupValue((item["Applicants_ApplicantName"] != null) ? item["Applicants_ApplicantName"].ToString() : string.Empty);
                            Request.ApplicantName = ApplicantsApplicantName.LookupValue;
                            Request.ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;
                            Request.AssignedTo = (item["EmployeeAssignedTo"] != null) ? item["EmployeeAssignedTo"].ToString() : string.Empty;
                            Request.RequestNumber = (item["RequestNumber"] != null) ? item["RequestNumber"].ToString() : string.Empty;
                            Request.SubmitDate = (item["SubmitDate"] != null) ? DateTime.Parse(item["SubmitDate"].ToString()) : DateTime.MinValue;
                            SPFieldLookupValue StatusId = new SPFieldLookupValue((item["RequestStatusId"] != null) ? item["RequestStatusId"].ToString() : string.Empty);
                            Request.RequestStatusId = StatusId.LookupId;
                            if ((Request.RequestStatusId == (int)RequestStatus.PAClosedByAcceptance) || (Request.RequestStatusId == (int)RequestStatus.PAClosedByRejection))
                                Request.IsRequestClosed = true;
                            else
                                Request.IsRequestClosed = false;

                            if (item["EmployeeAssignedTo"] != null)
                            {
                                Request.AssignedTo = item["EmployeeAssignedTo"].ToString();
                                if (Request.AssignedTo.Contains("\\") || Request.AssignedTo.Contains("|"))
                                {
                                    SPUser user = web.EnsureUser(Request.AssignedTo);
                                    Request.AssignedTo = user.Name;
                                }
                            }

                            Request.AttachmentURL = item.Attachments.Count != 0 ? SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, item.Attachments[0]) : string.Empty;
                            Request.FileName = item.Attachments.Count != 0 ? item.Attachments[0] : string.Empty;

                            Request.OrgBookReply = (item["OrgBookReply"] != null) ? item["OrgBookReply"].ToString() : string.Empty;

                            if (LCID == (int)Language.English)
                            {
                                SPFieldLookupValue NationalityTitle = new SPFieldLookupValue((item["Nationality_Title"] != null) ? item["Nationality_Title"].ToString() : string.Empty);
                                Request.Nationality = NationalityTitle.LookupValue;
                                SPFieldLookupValue RequestStatus = new SPFieldLookupValue((item["RequestStatus"] != null) ? item["RequestStatus"].ToString() : string.Empty);
                                Request.RequestStatus = RequestStatus.LookupValue;
                                SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue((item["CountryOfStudy"] != null) ? item["CountryOfStudy"].ToString() : string.Empty);
                                Request.Country = CountryOfStudy.LookupValue;
                                SPFieldLookupValue University = new SPFieldLookupValue((item["University"] != null) ? item["University"].ToString() : string.Empty);
                                Request.University = University.LookupValue;
                                SPFieldLookupValue Faculty = new SPFieldLookupValue((item["Faculty"] != null) ? item["Faculty"].ToString() : string.Empty);
                                Request.Faculty = Faculty.LookupValue;
                                SPFieldLookupValue Specialization = new SPFieldLookupValue((item["Specialization"] != null) ? item["Specialization"].ToString() : string.Empty);
                                Request.Specialization = Specialization.LookupValue;
                                SPFieldLookupValue AcademicDegree = new SPFieldLookupValue((item["HighestCertificate"] != null) ? item["HighestCertificate"].ToString() : string.Empty);
                                Request.AcademicDegree = AcademicDegree.LookupValue;
                                Request.RequestStatus = requestStatusItem.ReviewerDescriptionEn;
                            }
                            else
                            {
                                SPFieldLookupValue NationalityTitleAr = new SPFieldLookupValue((item["Nationality_TitleAr"] != null) ? item["Nationality_TitleAr"].ToString() : string.Empty);
                                Request.Nationality = NationalityTitleAr.LookupValue;
                                SPFieldLookupValue RequestStatus = new SPFieldLookupValue((item["RequestStatusAr"] != null) ? item["RequestStatusAr"].ToString() : string.Empty);
                                Request.RequestStatus = RequestStatus.LookupValue;
                                SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue((item["CountryOfStudyAr"] != null) ? item["CountryOfStudyAr"].ToString() : string.Empty);
                                Request.Country = CountryOfStudy.LookupValue;
                                SPFieldLookupValue University = new SPFieldLookupValue((item["UniversityAr"] != null) ? item["UniversityAr"].ToString() : string.Empty);
                                Request.University = University.LookupValue;
                                SPFieldLookupValue Faculty = new SPFieldLookupValue((item["FacultyAr"] != null) ? item["FacultyAr"].ToString() : string.Empty);
                                Request.Faculty = Faculty.LookupValue;
                                SPFieldLookupValue Specialization = new SPFieldLookupValue((item["SpecializationAr"] != null) ? item["SpecializationAr"].ToString() : string.Empty);
                                Request.Specialization = Specialization.LookupValue;
                                SPFieldLookupValue AcademicDegree = new SPFieldLookupValue((item["HighestCertificateAr"] != null) ? item["HighestCertificateAr"].ToString() : string.Empty);
                                Request.AcademicDegree = AcademicDegree.LookupValue;
                                Request.RequestStatus = requestStatusItem.ReviewerDescriptionAr;
                            }
                            Requests.Add(Request);
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
                Logging.GetInstance().Debug("Exiting method PASearchSimilarRequests.GetAllRequests");
            }
            return Requests;
        }
    }
}