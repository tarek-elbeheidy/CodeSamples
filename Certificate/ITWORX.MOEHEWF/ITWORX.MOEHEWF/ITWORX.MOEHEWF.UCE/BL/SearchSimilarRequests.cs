using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHEWF.UCE.Entities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.UCE.BL
{
    public class SearchSimilarRequests : UserControlBase
    {
        public static IEnumerable<SimilarRequest> GetAllRequests(string strQuery, int LCID)
        {
            List<Entities.SimilarRequest> Requests = new List<Entities.SimilarRequest>();
            try
            {
                Logging.GetInstance().Debug("Entering method SearchSimilarRequests.GetAllRequests");
                using (SPWeb web = new SPSite(SPContext.Current.Site.Url).OpenWeb())
                {
                    string strViewFields = string.Empty;

                    if (LCID == (int)Language.English)
                    {
                        strViewFields = "<FieldRef Name='RequestNumber'/>"
                              + "<FieldRef Name='Applicants_QatarID'/>"
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
                              + "<FieldRef Name='Specialization'/>"
                              + "<FieldRef Name='RequestStatus'/>"
                              + "<FieldRef Name='EntityNeedsEquivalency'/>"
                              + "<FieldRef Name='OtherEntityNeedsEquivalency'/>"
                              + "<FieldRef Name='RequestStatusId'/>"
                              + "<FieldRef Name='EmployeeAssignedTo'/>"
                              + "<FieldRef Name='AcademicDegreeForEquivalence'/>"
                              + "<FieldRef Name= 'Attachments' />"
                              + "<FieldRef Name='UniversityNotFoundInList'/>"
                              + "<FieldRef Name= 'OrgBookReply' />";
                    }
                    else
                    {
                        strViewFields = "<FieldRef Name='RequestNumber'/>"
                             + "<FieldRef Name='Applicants_QatarID'/>"
                             + "<FieldRef Name='Applicants_ApplicantName'/>"
                              + "<FieldRef Name='Applicants_ArabicName'/>"
                              + "<FieldRef Name='Applicants_EnglishName'/>"
                             + "<FieldRef Name='SubmitDate'/>"
                             + "<FieldRef Name='Nationality_TitleAr'/>"
                             + "<FieldRef Name='NationalityCategory_TitleAr'/>"
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
                             + "<FieldRef Name='OtherEntityNeedsEquivalency'/>"
                             + "<FieldRef Name='AcademicDegreeForEquivalenceAr'/>"
                             + "<FieldRef Name= 'Attachments' />"
                             + "<FieldRef Name='UniversityNotFoundInList'/>"
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

                    SPList customerList = web.Lists[Utilities.Constants.Requests];
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
                            if ((Request.RequestStatusId == (int)RequestStatus.UCEClosedByAcceptance) || (Request.RequestStatusId == (int)RequestStatus.UCEClosedByRejection))
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
                            Common.Entities.RequestStatus ReqStatus = new Common.Entities.RequestStatus();
                            if (LCID == (int)Language.English)
                            {
                                ReqStatus.FinalDecisionEn = Common.BL.RequestStatus.GetRequestStatusById(new SPFieldLookupValue(item["RequestStatusId"].ToString()).LookupId).FinalDecisionEn;
                                SPFieldLookupValue NationalityTitle = new SPFieldLookupValue((item["Nationality_Title"] != null) ? item["Nationality_Title"].ToString() : string.Empty);
                                Request.Nationality = NationalityTitle.LookupValue;
                                SPFieldLookupValue RequestStatus = new SPFieldLookupValue((item["RequestStatus"] != null) ? item["RequestStatus"].ToString() : string.Empty);
                                Request.RequestStatus = RequestStatus.LookupValue;
                                SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue((item["CountryOfStudy"] != null) ? item["CountryOfStudy"].ToString() : string.Empty);
                                Request.Country = CountryOfStudy.LookupValue;
                                SPFieldLookupValue University = new SPFieldLookupValue((item["University"] != null) ? item["University"].ToString() : string.Empty);
                                if (University.LookupValue != null)
                                    Request.University = University.LookupValue;
                                else
                                    Request.University = (item["UniversityNotFoundInList"] != null) ? item["UniversityNotFoundInList"].ToString() : string.Empty;

                                //SPFieldLookupValue Faculty = new SPFieldLookupValue((item["Faculty"] != null) ? item["Faculty"].ToString() : string.Empty);
                                //Request.Faculty = Faculty.LookupValue;
                                SPFieldLookupValue Specialization = new SPFieldLookupValue((item["Specialization"] != null) ? item["Specialization"].ToString() : string.Empty);
                                Request.Specialization = Specialization.LookupValue;
                                SPFieldLookupValue EntityNeedsEquivalency = new SPFieldLookupValue((item["EntityNeedsEquivalency"] != null) ? item["EntityNeedsEquivalency"].ToString() : string.Empty);

                                if (EntityNeedsEquivalency.LookupValue != null)
                                    Request.EntityNeedsEquivalency = EntityNeedsEquivalency.LookupValue;
                                else
                                    Request.EntityNeedsEquivalency = (item["OtherEntityNeedsEquivalency"] != null) ? item["OtherEntityNeedsEquivalency"].ToString() : string.Empty;


                                SPFieldLookupValue AcademicDegree = new SPFieldLookupValue((item["AcademicDegreeForEquivalence"] != null) ? item["AcademicDegreeForEquivalence"].ToString() : string.Empty);
                                Request.AcademicDegree = AcademicDegree.LookupValue;
                                Request.Faculty = (item["Faculty"] != null) ? item["Faculty"].ToString() : string.Empty;
                                Request.FinalDecision = ReqStatus.FinalDecisionEn;
                                SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                                Request.ApplicantName = Request.EnglishName = ApplicantsEnglishName.LookupValue;

                                SPFieldLookupValue ApplicantsArabicName = new SPFieldLookupValue((item["Applicants_ArabicName"] != null) ? item["Applicants_ArabicName"].ToString() : string.Empty);
                                Request.ArabicName = ApplicantsArabicName.LookupValue;

                                if (item["NationalityCategory_Title"] != null)
                                {
                                    SPFieldLookupValue NationCatgeory = new SPFieldLookupValue(item["NationalityCategory_Title"].ToString());
                                    Request.NationCatgeory = NationCatgeory.LookupValue;
                                }
                            }
                            else
                            {
                                ReqStatus.FinalDecisionAr = Common.BL.RequestStatus.GetRequestStatusById(new SPFieldLookupValue(item["RequestStatusId"].ToString()).LookupId).FinalDecisionAr;

                                SPFieldLookupValue NationalityTitleAr = new SPFieldLookupValue((item["Nationality_TitleAr"] != null) ? item["Nationality_TitleAr"].ToString() : string.Empty);
                                Request.Nationality = NationalityTitleAr.LookupValue;
                                SPFieldLookupValue RequestStatus = new SPFieldLookupValue((item["RequestStatusAr"] != null) ? item["RequestStatusAr"].ToString() : string.Empty);
                                Request.RequestStatus = RequestStatus.LookupValue;
                                SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue((item["CountryOfStudyAr"] != null) ? item["CountryOfStudyAr"].ToString() : string.Empty);
                                Request.Country = CountryOfStudy.LookupValue;
                                SPFieldLookupValue University = new SPFieldLookupValue((item["UniversityAr"] != null) ? item["UniversityAr"].ToString() : string.Empty);
                                if (University.LookupValue != null)
                                    Request.University = University.LookupValue;
                                else
                                    Request.University = (item["UniversityNotFoundInList"] != null) ? item["UniversityNotFoundInList"].ToString() : string.Empty;

                                //SPFieldLookupValue Faculty = new SPFieldLookupValue((item["FacultyAr"] != null) ? item["FacultyAr"].ToString() : string.Empty);
                                //Request.Faculty = Faculty.LookupValue;
                                SPFieldLookupValue Specialization = new SPFieldLookupValue((item["SpecializationAr"] != null) ? item["SpecializationAr"].ToString() : string.Empty);
                                Request.Specialization = Specialization.LookupValue;
                                SPFieldLookupValue EntityNeedsEquivalency = new SPFieldLookupValue((item["EntityNeedsEquivalencyAr"] != null) ? item["EntityNeedsEquivalencyAr"].ToString() : string.Empty);
                                if (EntityNeedsEquivalency.LookupValue != null)
                                    Request.EntityNeedsEquivalency = EntityNeedsEquivalency.LookupValue;
                                else
                                    Request.EntityNeedsEquivalency = (item["OtherEntityNeedsEquivalency"] != null) ? item["OtherEntityNeedsEquivalency"].ToString() : string.Empty;

                                SPFieldLookupValue AcademicDegree = new SPFieldLookupValue((item["AcademicDegreeForEquivalenceAr"] != null) ? item["AcademicDegreeForEquivalenceAr"].ToString() : string.Empty);
                                Request.AcademicDegree = AcademicDegree.LookupValue;
                                Request.Faculty = (item["FacultyAr"] != null) ? item["FacultyAr"].ToString() : string.Empty;
                                Request.FinalDecision = ReqStatus.FinalDecisionAr;
                                SPFieldLookupValue ApplicantsArabicName = new SPFieldLookupValue((item["Applicants_ArabicName"] != null) ? item["Applicants_ArabicName"].ToString() : string.Empty);
                                Request.ApplicantName = Request.ArabicName = ApplicantsArabicName.LookupValue;

                                SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                                Request.EnglishName = ApplicantsEnglishName.LookupValue;

                                if (item["NationalityCategory_TitleAr"] != null)
                                {
                                    SPFieldLookupValue NationCatgeory = new SPFieldLookupValue(item["NationalityCategory_TitleAr"].ToString());
                                    Request.NationCatgeory = NationCatgeory.LookupValue;
                                }
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
                Logging.GetInstance().Debug("Exiting method SearchSimilarRequests.GetAllRequests");
            }
            return Requests;
        }

        public static IEnumerable<Payments> GetAllPaymentRequests(string strQuery, int LCID)
        {
            List<Entities.Payments> payments = new List<Entities.Payments>();
            try
            {
                using (SPWeb web = new SPSite(SPContext.Current.Site.Url).OpenWeb())
                {
                    string joins = @"<Join Type='INNER' ListAlias='Requests'>
                                      <Eq>
                                        <FieldRef Name='RequestID' RefType='Id' />
                                        <FieldRef List='Requests' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='LEFT' ListAlias='AcademicDegree'>
                                      <Eq>
                                        <FieldRef List='Requests' Name='AcademicDegreeForEquivalence' RefType='Id' />
                                        <FieldRef List='AcademicDegree' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='LEFT' ListAlias='CountryOfStudy'>
                                      <Eq>
                                        <FieldRef List='Requests' Name='CountryOfStudy' RefType='Id' />
                                        <FieldRef List='CountryOfStudy' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='LEFT' ListAlias='University'>
                                      <Eq>
                                        <FieldRef List='Requests' Name='University' RefType='Id' />
                                        <FieldRef List='University' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='LEFT' ListAlias='EntityNeedsEquivalency'>
                                      <Eq>
                                        <FieldRef List='Requests' Name='EntityNeedsEquivalency' RefType='Id' />
                                        <FieldRef List='EntityNeedsEquivalency' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='INNER' ListAlias='Applicants'>
                                      <Eq>
                                        <FieldRef List='Requests' Name='ApplicantID' RefType='Id' />
                                        <FieldRef List='Applicants' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='INNER' ListAlias='Nationality'>
                                      <Eq>
                                        <FieldRef List='Applicants' Name='Nationality' RefType='Id' />
                                        <FieldRef List='Nationality' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='LEFT' ListAlias='Specialization'>
                                      <Eq>
                                        <FieldRef List='Requests' Name='Specialization' RefType='Id' />
                                        <FieldRef List='Specialization' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='INNER' ListAlias='NationalityCategory'>
                                      <Eq>
                                        <FieldRef List='Applicants' Name='NationalityCategory' RefType='Id' />
                                        <FieldRef List='NationalityCategory' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='LEFT' ListAlias='RequestStatus'>
                                      <Eq>
                                        <FieldRef List='Requests' Name='RequestStatus' RefType='Id' />
                                        <FieldRef List='RequestStatus' Name='Id' />
                                      </Eq>
                                    </Join>";

                    string projectedFields =
                    " <Field Name='Requests_RequestNumber' Type='Lookup' "
                    + "List='Requests' ShowField='RequestNumber'/>"
                    + "<Field Name='Requests_UniversityNotFoundInList' Type='Lookup' "
                    + "List='Requests' ShowField='UniversityNotFoundInList'/>"
                    + "<Field Name='Requests_RequestID' Type='Lookup' "
                    + "List='Requests' ShowField='ID'/>"
                    + "<Field Name='Requests_AssignedTo' Type='Lookup' "
                    + "List='Requests' ShowField='EmployeeAssignedTo'/>"

                    + "<Field Name='AcademicDegree_Title' Type='Lookup' "
                    + "List='AcademicDegree' ShowField='Title'/>"
                    + "<Field Name='AcademicDegree_TitleAr' Type='Lookup' "
                    + "List='AcademicDegree' ShowField='TitleAr'/>"

                    + "<Field Name='CountryOfStudy_Title' Type='Lookup' "
                    + "List='CountryOfStudy' ShowField='Title'/>"
                     + "<Field Name='CountryOfStudy_TitleAr' Type='Lookup' "
                    + "List='CountryOfStudy' ShowField='TitleAr'/>"

                    + "<Field Name='University_Title' Type='Lookup' "
                    + "List='University' ShowField='Title'/>"
                     + "<Field Name='University_TitleAr' Type='Lookup' "
                    + "List='University' ShowField='TitleAr'/>"

                        + "<Field Name='Specialization_Title' Type='Lookup' "
                        + "List='Specialization' ShowField='Title'/>"
                        + "<Field Name='Specialization_TitleAr' Type='Lookup' "
                        + "List='Specialization' ShowField='TitleAr'/>"

                       + "<Field Name='Requests_Faculty' Type='Lookup' "
                    + "List='Requests' ShowField='Faculty'/>"

                    + "<Field Name='Requests_FacultyAr' Type='Lookup' "
                    + "List='Requests' ShowField='FacultyAr'/>"

                    + "<Field Name='EntityNeedsEquivalency_Title' Type='Lookup'"
                    + "List='EntityNeedsEquivalency' ShowField='Title'/>"
                      + "<Field Name='EntityNeedsEquivalency_TitleAr' Type='Lookup'"
                    + "List='EntityNeedsEquivalency' ShowField='TitleAr'/>"

                    + "<Field Name='Requests_SubmitDate' Type='Lookup' "
                    + "List='Requests' ShowField='SubmitDate'/>"
                    + "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ApplicantName'/>"

                    + "<Field Name='Applicants_ArabicName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ArabicName'/>" +
                                                "<Field Name='Applicants_EnglishName' Type='Lookup' " +
                                                "List='Applicants' ShowField='EnglishName'/>"
                    + "<Field Name='Applicants_QatarID' Type='Lookup' "
                    + "List='Applicants' ShowField='PersonalID'/>"
                    + "<Field Name='Nationality_Title' Type='Lookup' "
                    + "List='Nationality' ShowField='Title'/>"
                     + "<Field Name='Nationality_TitleAr' Type='Lookup' "
                    + "List='Nationality' ShowField='TitleAr'/>"

                    + "<Field Name='NationalityCategory_Title' Type='Lookup' "
                    + "List='NationalityCategory' ShowField='Title'/>"
                    + "<Field Name='NationalityCategory_TitleAr' Type='Lookup' "
                    + "List='NationalityCategory' ShowField='TitleAr'/>"

                    + "<Field Name='RequestStatus_Title' Type='Lookup' "
                    + "List='RequestStatus' ShowField='ApplicantDescriptionEn'/>"
                    + "<Field Name='RequestStatus_TitleAr' Type='Lookup' "
                    + "List='RequestStatus' ShowField='ApplicantDescriptionAr'/>";

                    string viewFields = string.Empty;
                    if (LCID == (int)Language.English)
                    {
                        viewFields = "<FieldRef Name='Requests_RequestNumber'/>"
                    + "<FieldRef Name='Requests_UniversityNotFoundInList'/>"
                    + "<FieldRef Name='AcademicDegree_Title'/>"
                    + "<FieldRef Name='CountryOfStudy_Title'/>"
                    + "<FieldRef Name='University_Title'/>"
                      + "<FieldRef Name='Specialization_Title'/>"
                      + "<FieldRef Name='Requests_Faculty'/>"
                    + "<FieldRef Name='Requests_FacultyAr'/>"
                    + "<FieldRef Name='EntityNeedsEquivalency_Title'/>"
                    + "<FieldRef Name='Applicants_ApplicantName'/>"
                     + "<FieldRef Name='Applicants_EnglishName'/>"
                     + "<FieldRef Name='Applicants_ArabicName'/>"
                    + "<FieldRef Name='Applicants_QatarID'/>"
                    + "<FieldRef Name='Nationality_Title'/>"
                    + "<FieldRef Name='Amount'/>"
                    + "<FieldRef Name='RequestID'/>"
                    + "<FieldRef Name='ReasonCode'/>"
                    + "<FieldRef Name='ResponseMessage'/>"
                    + "<FieldRef Name='ReceiptDate'/>"
                     + "<FieldRef Name='ReceiptNumber'/>"
                    + "<FieldRef Name='NationalityCategory_Title'/>"
                    + "<FieldRef Name='Requests_RequestID'/>"
                    + "<FieldRef Name='RequestStatus_Title'/>"
                    + "<FieldRef Name='Requests_AssignedTo'/>"
                    + "<FieldRef Name='Requests_SubmitDate'/>";
                    }
                    else
                    {
                        viewFields = "<FieldRef Name='Requests_RequestNumber'/>"
                 + "<FieldRef Name='Requests_UniversityNotFoundInList'/>"
               + "<FieldRef Name='AcademicDegree_TitleAr'/>"
               + "<FieldRef Name='CountryOfStudy_TitleAr'/>"
               + "<FieldRef Name='University_TitleAr'/>"
                + "<FieldRef Name='Specialization_TitleAr'/>"
               + "<FieldRef Name='Requests_Faculty'/>"
                    + "<FieldRef Name='Requests_FacultyAr'/>"
               + "<FieldRef Name='EntityNeedsEquivalency_TitleAr'/>"
               + "<FieldRef Name='Applicants_ApplicantName'/>"
                + "<FieldRef Name='Applicants_ArabicName'/>"
                + "<FieldRef Name='Applicants_EnglishName'/>"
               + "<FieldRef Name='Applicants_QatarID'/>"
               + "<FieldRef Name='Nationality_TitleAr'/>"
               + "<FieldRef Name='Amount'/>"
               + "<FieldRef Name='ResponseMessage'/>"
               + "<FieldRef Name='ReceiptDate'/>"
                + "<FieldRef Name='RequestID'/>"
                + "<FieldRef Name='ReasonCode'/>"
                 + "<FieldRef Name='ReceiptNumber'/>"
                + "<FieldRef Name='NationalityCategory_TitleAr'/>"
               + "<FieldRef Name='Requests_RequestID'/>"
               + "<FieldRef Name='RequestStatus_TitleAr'/>"
               + "<FieldRef Name='Requests_AssignedTo'/>"
               + "<FieldRef Name='Requests_SubmitDate'/>";
                    }

                    //string query = @"<Where><IsNotNull><FieldRef Name='ResponseMessage' /></IsNotNull></Where>";

                    SPList list = web.Lists[Utilities.Constants.PaymentRecords];

                    SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject(strQuery, joins, projectedFields, viewFields);
                    spQuery.RowLimit = 5000;
                    SPListItemCollection items = list.GetItems(spQuery);
                    if (items != null && items.Count > 0)
                    {
                        foreach (SPListItem item in items)
                        {
                            Payments pay = new Payments();
                            pay.ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;
                            pay.Amount = (item["Amount"] != null) ? item["Amount"].ToString() : string.Empty;
                            pay.ResponseMessage = (item["ResponseMessage"] != null) ? item["ResponseMessage"].ToString() : string.Empty;
                            pay.ReceiptDate = (item["ReceiptDate"] != null) ? DateTime.Parse(item["ReceiptDate"].ToString()) : DateTime.MinValue;
                            pay.ReceiptNumber = (item["ReceiptNumber"] != null) ? item["ReceiptNumber"].ToString() : string.Empty;
                            pay.ReasonCode = (item["ReasonCode"] != null) ? item["ReasonCode"].ToString() : string.Empty;
                            pay.RequestID = (item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty;

                            SPFieldLookupValue SubmitDate = new SPFieldLookupValue((item["Requests_SubmitDate"] != null) ? item["Requests_SubmitDate"].ToString() : string.Empty);

                            pay.SubmitDate = (SubmitDate.LookupValue != null) ? DateTime.Parse(SubmitDate.LookupValue.ToString()) : DateTime.MinValue;

                            //SPFieldLookupValue RequestID = new SPFieldLookupValue((item["Requests_RequestID"] != null) ? item["Requests_RequestID"].ToString() : string.Empty);
                            //pay.RequestID = RequestID.LookupValue;

                            SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue(item["Applicants_QatarID"].ToString());
                            pay.QatariID = ApplicantsQatarID.LookupValue;

                            SPFieldLookupValue ApplicantsArabicName = new SPFieldLookupValue((item["Applicants_ArabicName"] != null) ? item["Applicants_ArabicName"].ToString() : string.Empty);
                            pay.ArabicName = ApplicantsArabicName.LookupValue;

                            SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                            pay.EnglishName = ApplicantsEnglishName.LookupValue;

                            SPFieldLookupValue AssignedTo = new SPFieldLookupValue((item["Requests_AssignedTo"] != null) ? item["Requests_AssignedTo"].ToString() : string.Empty);
                            string assign = AssignedTo.LookupValue;

                            if (!string.IsNullOrEmpty(assign))
                            {
                                pay.AssignedTo = assign;
                                if (pay.AssignedTo.Contains("\\") || pay.AssignedTo.Contains("|"))
                                {
                                    SPUser user = web.EnsureUser(pay.AssignedTo);
                                    pay.AssignedTo = user.Name;
                                }
                            }
                            else
                                pay.AssignedTo = string.Empty;

                            SPFieldLookupValue RequestNumber = new SPFieldLookupValue((item["Requests_RequestNumber"] != null) ? item["Requests_RequestNumber"].ToString() : string.Empty);
                            pay.RequestNumber = RequestNumber.LookupValue;

                            if (LCID == (int)Language.English)
                            {

                                SPFieldLookupValue EnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                                pay.ApplicantName = EnglishName.LookupValue;

                                if (item["Requests_Faculty"] != null)
                                {
                                    SPFieldLookupValue faculty = new SPFieldLookupValue(item["Requests_Faculty"].ToString());
                                    pay.Faculty = faculty.LookupValue;
                                }
                                SPFieldLookupValue RequestStatus = new SPFieldLookupValue(item["RequestStatus_Title"].ToString());
                                pay.RequestStatus = RequestStatus.LookupValue;

                                SPFieldLookupValue NationalityTitle = new SPFieldLookupValue(item["Nationality_Title"].ToString());
                                pay.Nationality = NationalityTitle.LookupValue;

                                SPFieldLookupValue NationCatgeory = new SPFieldLookupValue(item["NationalityCategory_Title"].ToString());
                                pay.NationCatgeory = NationCatgeory.LookupValue;

                                SPFieldLookupValue specialization = new SPFieldLookupValue((item["Specialization_Title"] != null) ? item["Specialization_Title"].ToString() : string.Empty);
                                pay.Specialization = specialization.LookupValue;

                                SPFieldLookupValue EntityNeedsEquivalency = new SPFieldLookupValue((item["EntityNeedsEquivalency_Title"] != null) ? item["EntityNeedsEquivalency_Title"].ToString() : string.Empty);
                                pay.EntityNeedsEquivalency = EntityNeedsEquivalency.LookupValue;

                                SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue((item["CountryOfStudy_Title"] != null) ? item["CountryOfStudy_Title"].ToString() : string.Empty);
                                pay.Country = CountryOfStudy.LookupValue;

                                SPFieldLookupValue AcademicDegree = new SPFieldLookupValue((item["AcademicDegree_Title"] != null) ? item["AcademicDegree_Title"].ToString() : string.Empty);
                                pay.AcademicDegree = AcademicDegree.LookupValue;

                                SPFieldLookupValue University = new SPFieldLookupValue((item["University_Title"] != null) ? item["University_Title"].ToString() : string.Empty);
                                SPFieldLookupValue otherUniversity = new SPFieldLookupValue((item["Requests_UniversityNotFoundInList"] != null) ? item["Requests_UniversityNotFoundInList"].ToString() : string.Empty);
                                if (University.LookupValue != null)
                                    pay.University = University.LookupValue;
                                else
                                    pay.University = otherUniversity.LookupValue;

                            }
                            else
                            {
                                SPFieldLookupValue ArabicName = new SPFieldLookupValue((item["Applicants_ArabicName"] != null) ? item["Applicants_ArabicName"].ToString() : string.Empty);
                                pay.ApplicantName = ArabicName.LookupValue;

                                if (item["Requests_FacultyAr"] != null)
                                {
                                    SPFieldLookupValue faculty = new SPFieldLookupValue(item["Requests_FacultyAr"].ToString());
                                    pay.Faculty = faculty.LookupValue;
                                }
                                SPFieldLookupValue RequestStatus = new SPFieldLookupValue(item["RequestStatus_TitleAr"].ToString());
                                pay.RequestStatus = RequestStatus.LookupValue;

                                SPFieldLookupValue NationalityTitle = new SPFieldLookupValue(item["Nationality_TitleAr"].ToString());
                                pay.Nationality = NationalityTitle.LookupValue;

                                SPFieldLookupValue NationCatgeory = new SPFieldLookupValue(item["NationalityCategory_TitleAr"].ToString());
                                pay.NationCatgeory = NationCatgeory.LookupValue;

                                SPFieldLookupValue specialization = new SPFieldLookupValue((item["Specialization_TitleAr"] != null) ? item["Specialization_TitleAr"].ToString() : string.Empty);
                                pay.Specialization = specialization.LookupValue;

                                SPFieldLookupValue EntityNeedsEquivalency = new SPFieldLookupValue((item["EntityNeedsEquivalency_TitleAr"] != null) ? item["EntityNeedsEquivalency_TitleAr"].ToString() : string.Empty);
                                pay.EntityNeedsEquivalency = EntityNeedsEquivalency.LookupValue;

                                SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue((item["CountryOfStudy_TitleAr"] != null) ? item["CountryOfStudy_TitleAr"].ToString() : string.Empty);
                                pay.Country = CountryOfStudy.LookupValue;

                                SPFieldLookupValue AcademicDegree = new SPFieldLookupValue((item["AcademicDegree_TitleAr"] != null) ? item["AcademicDegree_TitleAr"].ToString() : string.Empty);
                                pay.AcademicDegree = AcademicDegree.LookupValue;

                                SPFieldLookupValue University = new SPFieldLookupValue((item["University_TitleAr"] != null) ? item["University_TitleAr"].ToString() : string.Empty);
                                SPFieldLookupValue otherUniversity = new SPFieldLookupValue((item["Requests_UniversityNotFoundInList"] != null) ? item["Requests_UniversityNotFoundInList"].ToString() : string.Empty);
                                if (University.LookupValue != null)
                                    pay.University = University.LookupValue;
                                else
                                    pay.University = otherUniversity.LookupValue;

                            }


                            payments.Add(pay);
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
                Logging.GetInstance().Debug("Exiting method SearchSimilarRequests.GetAllClarificationRequests");
            }
            return payments;
        }

        public static List<int> GetCheckedRequests(int reqID)
        {
            List<int> requestsID = new List<int>();
            try
            {
                Logging.GetInstance().Debug("Entering method SearchSimilarRequests.GetCheckedRequests");
                SPSite site = new SPSite(SPContext.Current.Site.Url);
                SPWeb web = site.OpenWeb();

                SPList requestsList = web.Lists[Utilities.Constants.Requests];
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
                Logging.GetInstance().Debug("Exiting method SearchSimilarRequests.GetCheckedRequests");
            }
            return requestsID;
        }

        public static bool GetRequestStatusbyReqID(string reqID)
        {
            bool IsRequestClosed = false;
            try
            {
                SimilarRequest Request = new SimilarRequest();

                Logging.GetInstance().Debug("Entering method SearchSimilarRequests.GetAllRequests");
                using (SPWeb web = new SPSite(SPContext.Current.Site.Url).OpenWeb())
                {
                    SPList reqs = web.Lists[Utilities.Constants.Requests];
                    SPListItem item = reqs.GetItemById(Convert.ToInt32(reqID));
                    SPFieldLookupValue StatusId = new SPFieldLookupValue((item["RequestStatusId"] != null) ? item["RequestStatusId"].ToString() : string.Empty);
                    Request.RequestStatusId = StatusId.LookupId;
                    if ((Request.RequestStatusId == (int)RequestStatus.UCEClosedByAcceptance) || (Request.RequestStatusId == (int)RequestStatus.UCEClosedByRejection))
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
                Logging.GetInstance().Debug("Exiting method SearchSimilarRequests.GetAllRequests");
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
                Logging.GetInstance().Debug("Entering method SearchSimilarRequests.GetAllRequests");
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
                                  + "<FieldRef Name='NationalityCategory_Title'/>"
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
                                  + "<FieldRef Name='AcademicDegreeForEquivalence'/>"
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
                                 + "<FieldRef Name='NationalityCategory_TitleAr'/>"
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
                                 + "<FieldRef Name='AcademicDegreeForEquivalenceAr'/>"
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
                                      "</Join>" +
                                      "<Join Type='INNER' ListAlias='NationalityCategory'>" +
                                      "<Eq>" +
                                      "<FieldRef List='Applicants' Name='NationalityCategory' RefType='Id'/>" +
                                      "<FieldRef List='NationalityCategory' Name='ID'/>" +
                                      "</Eq>" +
                                      "</Join>",

                            ProjectedFields = "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ApplicantName'/>" +
                                                "<Field Name='Applicants_QatarID' Type='Lookup' " +
                                                "List='Applicants' ShowField='PersonalID'/>" +
                                                "<Field Name='Nationality_Title' Type='Lookup' " +
                                                "List='Nationality' ShowField='Title'/>" +
                                                "<Field Name='Nationality_TitleAr' Type='Lookup' " +
                                                "List='Nationality' ShowField='TitleAr'/>" +
                                                "<Field Name='NationalityCategory_Title' Type='Lookup' " +
                                                "List='NationalityCategory' ShowField='Title'/>" +
                                                "<Field Name='NationalityCategory_TitleAr' Type='Lookup' " +
                                                "List='NationalityCategory' ShowField='TitleAr'/>",

                            ViewFields = strViewFields,
                            Query = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + id + "</Value></Eq></Where>",
                        };

                        SPList reqsList = web.Lists[Utilities.Constants.Requests];
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
                            if ((Request.RequestStatusId == (int)RequestStatus.UCEClosedByAcceptance) || (Request.RequestStatusId == (int)RequestStatus.UCEClosedByRejection))
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
                                SPFieldLookupValue NationalityTitle = new SPFieldLookupValue((item["Nationality_Title"] != null) ? item["NationalityCategory_Title"].ToString() : string.Empty);
                                Request.Nationality = NationalityTitle.LookupValue;
                                SPFieldLookupValue RequestStatus = new SPFieldLookupValue((item["RequestStatus"] != null) ? item["RequestStatus"].ToString() : string.Empty);
                                Request.RequestStatus = RequestStatus.LookupValue;
                                SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue((item["CountryOfStudy"] != null) ? item["CountryOfStudy"].ToString() : string.Empty);
                                Request.Country = CountryOfStudy.LookupValue;
                                SPFieldLookupValue University = new SPFieldLookupValue((item["University"] != null) ? item["University"].ToString() : string.Empty);
                                Request.University = University.LookupValue;
                                //SPFieldLookupValue Faculty = new SPFieldLookupValue((item["Faculty"] != null) ? item["Faculty"].ToString() : string.Empty);
                                //Request.Faculty = Faculty.LookupValue;
                                SPFieldLookupValue Specialization = new SPFieldLookupValue((item["Specialization"] != null) ? item["Specialization"].ToString() : string.Empty);
                                Request.Specialization = Specialization.LookupValue;
                                SPFieldLookupValue EntityNeedsEquivalency = new SPFieldLookupValue((item["EntityNeedsEquivalency"] != null) ? item["EntityNeedsEquivalency"].ToString() : string.Empty);
                                Request.EntityNeedsEquivalency = EntityNeedsEquivalency.LookupValue;
                                SPFieldLookupValue AcademicDegree = new SPFieldLookupValue((item["AcademicDegreeForEquivalence"] != null) ? item["AcademicDegreeForEquivalence"].ToString() : string.Empty);
                                Request.AcademicDegree = AcademicDegree.LookupValue;
                                Request.RequestStatus = requestStatusItem.ReviewerDescriptionEn;
                                Request.Faculty = (item["Faculty"] != null) ? item["Faculty"].ToString() : string.Empty;
                            }
                            else
                            {
                                SPFieldLookupValue NationalityTitleAr = new SPFieldLookupValue((item["Nationality_TitleAr"] != null) ? item["NationalityCategory_TitleAr"].ToString() : string.Empty);
                                Request.Nationality = NationalityTitleAr.LookupValue;
                                SPFieldLookupValue RequestStatus = new SPFieldLookupValue((item["RequestStatusAr"] != null) ? item["RequestStatusAr"].ToString() : string.Empty);
                                Request.RequestStatus = RequestStatus.LookupValue;
                                SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue((item["CountryOfStudyAr"] != null) ? item["CountryOfStudyAr"].ToString() : string.Empty);
                                Request.Country = CountryOfStudy.LookupValue;
                                SPFieldLookupValue University = new SPFieldLookupValue((item["UniversityAr"] != null) ? item["UniversityAr"].ToString() : string.Empty);
                                Request.University = University.LookupValue;
                                //SPFieldLookupValue Faculty = new SPFieldLookupValue((item["FacultyAr"] != null) ? item["FacultyAr"].ToString() : string.Empty);
                                //Request.Faculty = Faculty.LookupValue;
                                SPFieldLookupValue Specialization = new SPFieldLookupValue((item["SpecializationAr"] != null) ? item["SpecializationAr"].ToString() : string.Empty);
                                Request.Specialization = Specialization.LookupValue;
                                SPFieldLookupValue EntityNeedsEquivalency = new SPFieldLookupValue((item["EntityNeedsEquivalencyAr"] != null) ? item["EntityNeedsEquivalencyAr"].ToString() : string.Empty);
                                Request.EntityNeedsEquivalency = EntityNeedsEquivalency.LookupValue;
                                SPFieldLookupValue AcademicDegree = new SPFieldLookupValue((item["AcademicDegreeForEquivalenceAr"] != null) ? item["AcademicDegreeForEquivalenceAr"].ToString() : string.Empty);
                                Request.AcademicDegree = AcademicDegree.LookupValue;
                                Request.RequestStatus = requestStatusItem.ReviewerDescriptionAr;
                                Request.Faculty = (item["FacultyAr"] != null) ? item["FacultyAr"].ToString() : string.Empty;
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
                Logging.GetInstance().Debug("Exiting method SearchSimilarRequests.GetAllRequests");
            }
            return Requests;
        }
    }
}