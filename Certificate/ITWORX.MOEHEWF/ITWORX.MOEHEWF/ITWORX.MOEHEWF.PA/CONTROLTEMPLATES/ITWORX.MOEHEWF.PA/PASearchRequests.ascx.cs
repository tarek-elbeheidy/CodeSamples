using ClosedXML.Excel;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.BL;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ITWORX.MOEHEWF.Common.Entities;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class PASearchRequests : UserControlBase
    {
        #region Public Properties
        public string SPGroupName { get; set; }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            grd_Requests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SearchPageSize"));

            BindDropDowns();
            
        }
        private List<Entities.SimilarRequest> GetSearchRequests()
        {
            List<Entities.SimilarRequest> srchRequests = new List<Entities.SimilarRequest>();
            try
            {
                Logging.GetInstance().Debug("Entering method SearchRequests.GetSearchRequests");
                List<string> objColumns = new List<string>();

                if (drp_RequestStatus.SelectedIndex != 0 & drp_RequestStatus.SelectedIndex != -1)
                {
                   
                        objColumns.Add("RequestStatus;Lookup;Eq;" + drp_RequestStatus.SelectedValue);
                   
                }
                if (!string.IsNullOrEmpty(hdf_University.Value)/*drp_University.SelectedIndex != 0 & drp_University.SelectedIndex != -1*/)
                {
                    if (LCID == (int)Language.English)
                        objColumns.Add("ProgramUniversity;Lookup;Eq;" + hdf_University.Value.ToString());
                    else
                        objColumns.Add("ProgramUniversityAr;Lookup;Eq;" + hdf_University.Value.ToString());

                }

                if (!string.IsNullOrEmpty(hdn_DateFrom.Value))
                {
                  objColumns.Add("SubmitDate;DateTime;Geq;" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.ParseExact(hdn_DateFrom.Value, "M/d/yyyy", CultureInfo.CurrentCulture)));
                }
                if (!string.IsNullOrEmpty(hdn_DateTo.Value))
                {
                    objColumns.Add("SubmitDate;DateTime;Leq;" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.ParseExact(hdn_DateTo.Value, "M/d/yyyy", CultureInfo.CurrentCulture)));
                }
                if (drp_Specialization.SelectedIndex != 0 & drp_Specialization.SelectedIndex != -1)
                {
                    if (LCID == (int)Language.English)
                        objColumns.Add("ProgramSpecialization;Lookup;Eq;" + drp_Specialization.SelectedItem.Text);
                    else
                        objColumns.Add("ProgramSpecializationAr;Lookup;Eq;" + drp_Specialization.SelectedItem.Text);
                }
                if (drp_Nationality.SelectedIndex != 0 & drp_Nationality.SelectedIndex != -1)
                {
                    if (LCID == (int)Language.English)
                        objColumns.Add("Nationality_Title;Lookup;Eq;" + drp_Nationality.SelectedItem.Text);
                    else
                        objColumns.Add("Nationality_TitleAr;Lookup;Eq;" + drp_Nationality.SelectedItem.Text);
                }
                if (ddlFaculty.Text != "")
                {
                    if (LCID == (int)Language.English)
                        objColumns.Add("ProgramFaculty;Text;Eq;" + ddlFaculty.Text);
                    else
                        objColumns.Add("ProgramFaculty;Text;Eq;" + ddlFaculty.Text);
                }
                if (drp_EntityNeedsEquivalency.SelectedIndex != 0 & drp_EntityNeedsEquivalency.SelectedIndex != -1)
                {
                    if (LCID == (int)Language.English)
                        objColumns.Add("EntityNeedsEquivalency;Lookup;Eq;" + drp_EntityNeedsEquivalency.SelectedItem.Text);
                    else
                        objColumns.Add("EntityNeedsEquivalencyAr;Lookup;Eq;" + drp_EntityNeedsEquivalency.SelectedItem.Text);
                }
                if (drp_Country.SelectedIndex != 0 & drp_Country.SelectedIndex != -1)
                {
                    if (LCID == (int)Language.English)
                        objColumns.Add("ProgramCountry;Lookup;Eq;" + drp_Country.SelectedItem.Text);
                    else
                        objColumns.Add("ProgramCountryAr;Lookup;Eq;" + drp_Country.SelectedItem.Text);
                }
                if (drp_NationCategory.SelectedIndex != 0 & drp_NationCategory.SelectedIndex != -1)
                {
                    if (LCID == (int)Language.English)
                        objColumns.Add("NationalityCategory_Title;Lookup;Eq;" + drp_NationCategory.SelectedItem.Text);
                    else
                        objColumns.Add("NationalityCategory_TitleAr;Lookup;Eq;" + drp_NationCategory.SelectedItem.Text);
                }
                if (drp_Employees.SelectedIndex != 0 & drp_Employees.SelectedIndex != -1)
                {
                    if (LCID == (int)Language.English)
                        objColumns.Add("EmployeeAssignedTo;Text;Eq;" + drp_Employees.SelectedItem.Value);
                    else
                        objColumns.Add("EmployeeAssignedTo;Text;Eq;" + drp_Employees.SelectedItem.Value);
                }
                if (drp_Certificate.SelectedIndex != 0 & drp_Certificate.SelectedIndex != -1)
                {
                    if (LCID == (int)Language.English)
                        objColumns.Add("AcademicDegreeForEquivalence;Lookup;Eq;" + drp_Certificate.SelectedItem.Text);
                    else
                        objColumns.Add("AcademicDegreeForEquivalenceAr;Lookup;Eq;" + drp_Certificate.SelectedItem.Text);

                }
                if (txt_RequestID.Text != "")
                {
                    if (LCID == (int)Language.English)
                        objColumns.Add("RequestNumber;Text;Contains;" + txt_RequestID.Text);
                    else
                        objColumns.Add("RequestNumber;Text;Contains;" + txt_RequestID.Text);

                }
                if (txt_NationalID.Text != "")
                {
                    if (LCID == (int)Language.English)
                        objColumns.Add("Applicants_QatarID;Text;Eq;" + txt_NationalID.Text);
                    else
                        objColumns.Add("Applicants_QatarID;Text;Eq;" + txt_NationalID.Text);

                }
                if (txt_ApplicantName.Text != "")
                {
                    if (LCID == (int)Language.English)
                        objColumns.Add("Applicants_EnglishName;Text;Contains;" + txt_ApplicantName.Text);


                    else
                        objColumns.Add("Applicants_ArabicName;Text;Contains;" + txt_ApplicantName.Text.ToLower());

                }



                if (objColumns.Count > 0)
                    srchRequests = BL.PASearchSimilarRequests.GetAllRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(objColumns, "And", true) + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();
                else
                    srchRequests = BL.PASearchSimilarRequests.GetAllRequests("<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SearchRequests.GetSearchRequests");
            }
            return srchRequests;
        }
        private void BindGridOnSearch()
        {
            try
            {
                List<Entities.SimilarRequest> srchRequests = GetSearchRequests();
                HelperMethods.BindGridView(grd_Requests, srchRequests);
                if (srchRequests.Count > 0)
                {
                    lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NoOfRequests", (uint)LCID) + srchRequests.Count;
                    lbl_NoOfRequests.Visible = true;
                }
                else
                {
                    lbl_NoOfRequests.Visible = false;
                }

                if (srchRequests.Count >= Common.Utilities.Constants.SearchLimit)
                {
                    searchLimit.Visible = true;
                }
                else
                {
                    searchLimit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SearchRequests.BindGridOnSearch");
            }


        }
        private void BindDropDowns()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method SearchRequests.BindDropDowns");

                List<Entities.Nationality> nationalityItems = BL.Nationality.GetAll();
                List<Common.Entities.CountryOfStudy> countryItems = Common.BL.CountryOfStudy.GetAll(LCID);
                List<Entities.EntityNeedsEquivalency> entityNeedsEquivalencyItems = BL.EntityNeedsEquivalency.GetAll();
                List<Entities.Certificates> certificatesItems = BL.Certificates.GetAll();
                List<Entities.NationalityCategory> nationalityCategoryItems = BL.NationalityCategory.GetAll();
                List<Entities.Specialization> Specializationlist = BL.Specialization.GetAll();

                //Bind Request Status
                //List<Common.Entities.RequestStatus> requestStatusItems = Common.BL.RequestStatus.GetDistinctReviewerStatus(LCID);
                var requestStatusItems = Common.BL.RequestStatus.GetDistinctStatusToApplicant();
                if (requestStatusItems != null && requestStatusItems.Count > 0)
                {
                    //HelperMethods.BindDropDownList(ref drp_RequestStatus, requestStatusItems, "ID", "ApplicantDescriptionAr", "ApplicantDescriptionEn", LCID);
                    HelperMethods.BindDropDownList(ref drp_RequestStatus, requestStatusItems, "ApplicantDescriptionEn", "ApplicantDescriptionAr", "ApplicantDescriptionEn", LCID);
                    drp_RequestStatus.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));
                }
                //Bind Nationality Category
                HelperMethods.BindDropDownList(ref drp_NationCategory, nationalityCategoryItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                drp_NationCategory.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));

                //Bind Certificates
                HelperMethods.BindDropDownList(ref drp_Certificate, certificatesItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                drp_Certificate.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));

                //Bind Nationality
                HelperMethods.BindDropDownList(ref drp_Nationality, nationalityItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                drp_Nationality.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));

                //Bind Country
                HelperMethods.BindDropDownList(ref drp_Country, countryItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                drp_Country.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));

                //Bind Specialization

                drp_Specialization.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));
                if (Specializationlist.Count > 0)
                {
                    drp_Specialization.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref drp_Specialization, Specializationlist, "ID", "ArabicTitle", "EnglishTitle", LCID);
                    drp_Specialization.Visible = true;
                    lbl_Specialization.Visible = true;
                }

                //Bind Employees
                List<SPUser> users = HelperMethods.GetGroupUsers(Common.Utilities.Constants.ArabicProgEmployeeGroupName);

                foreach (SPUser user in users)
                {
                    drp_Employees.Items.Add(new ListItem(user.Name, user.LoginName));
                }
                drp_Employees.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));
                
                // Bind Entity Needs Equivalency
                HelperMethods.BindDropDownList(ref drp_EntityNeedsEquivalency, entityNeedsEquivalencyItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                drp_EntityNeedsEquivalency.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method SearchRequests.BindDropDowns");
            }
        }
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
        private void ClearControls()
        {
            drp_Certificate.ClearSelection();
            drp_Country.ClearSelection();
            //drp_Faculty.Items.Clear();
            ddlFaculty.Text = string.Empty;
            drp_EntityNeedsEquivalency.ClearSelection();
            drp_NationCategory.ClearSelection();
            drp_Nationality.ClearSelection();
            // drp_Specialization.Items.Clear();
            drp_RequestStatus.ClearSelection();
            drp_University.Items.Clear();
            hdn_DateTo.Value = string.Empty;
            hdn_DateFrom.Value = string.Empty;
            txt_NationalID.Text = string.Empty;
            txt_RequestID.Text = string.Empty;
            txt_ApplicantName.Text = string.Empty;
            drp_Employees.ClearSelection();
            hdf_University.Value = string.Empty;
            BindGridOnSearch();
        }
        protected void grd_Requests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter SearchRequests.grd_Requests_PageIndexChanging");
            try
            {
                grd_Requests.PageIndex = e.NewPageIndex;
                BindGridOnSearch();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit SearchRequests.grd_Requests_PageIndexChanging");
            }
        }
        protected void grd_Requests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter SearchRequests.grd_Requests_RowDataBound");
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hdnRequestStatusId = (HiddenField)e.Row.FindControl("hdn_RequestStatusId");
                    HiddenField IsClosed = (HiddenField)e.Row.FindControl("hdn_IsClosed");
                    HiddenField AssignedTo = (HiddenField)e.Row.FindControl("hdn_AssignedTo");
                    LinkButton btnEdit = (LinkButton)e.Row.FindControl("lnk_Edit");
                    LinkButton btnView = (LinkButton)e.Row.FindControl("lnk_View");
                    LinkButton lnk_FinalDecisionFile = (LinkButton)e.Row.FindControl("lnk_FinalDecisionFile");

                    if (!Convert.ToBoolean(IsClosed.Value))
                    {
                        if (AssignedTo.Value.ToLower() == SPContext.Current.Web.CurrentUser.LoginName.ToLower() || AssignedTo.Value.ToLower() == SPGroupName.ToLower())
                        {
                            if (HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName) && (int.Parse(hdnRequestStatusId.Value) == (int)Common.Utilities.RequestStatus.PACulturalMissionNeedsStatement || int.Parse(hdnRequestStatusId.Value) == (int)Common.Utilities.RequestStatus.PAHigherEduInstitutesNeedsStatement))
                            {
                                btnEdit.Visible = false;
                                btnView.Visible = true;
                            }
                            else
                            {
                                btnEdit.Visible = true;
                                btnView.Visible = false;
                            }
                        }
                        else
                        {
                            btnEdit.Visible = false;
                            btnView.Visible = true;
                        }
                        //lnk_FinalDecisionFile.Visible = false;
                    }
                    else
                    {
                        btnEdit.Visible = false;
                        btnView.Visible = true;
                        lnk_FinalDecisionFile.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit SearchRequests.grd_Requests_RowDataBound");
            }
        }
        protected void lnk_View_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter SearchRequests.lnk_View_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField lblRequestID = (HiddenField)gvr.FindControl("hdn_ID");
                Page.Session["PADisplayRequestId"] = lblRequestID.Value;
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdn_RequestStatusId");
                Common.Entities.RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                string viewLink = string.Empty;
                if (requestStatus.CanReviewerEditRequest)
                    viewLink = requestStatus.ReviewerTargetPageURL;
                if (requestStatus.Code == Common.Utilities.RequestStatus.PADraft.ToString())
                    viewLink = requestStatus.ReviewerTargetPageURL;

                SPUtility.Redirect(SPContext.Current.Web.Url + viewLink, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SearchRequests.lnk_View_Click");
            }
        }
        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter SearchRequests.lnk_Edit_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField lblRequestID = (HiddenField)gvr.FindControl("hdn_ID");
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdn_RequestStatusId");
                Common.Entities.RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                string editLink = string.Empty;

                if (requestStatus.CanReviewerEditRequest)
                    editLink = requestStatus.ReviewerTargetPageURL;

                Page.Session["PADisplayRequestId"] = lblRequestID.Value;
                SPUtility.Redirect(SPContext.Current.Web.Url + editLink, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit SearchRequests.lnk_Edit_Click");
            }
        }
        protected void drp_Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter SearchRequests.drp_Country_SelectedIndexChanged");

                //Bind University
                //SPListItemCollection Universitylist = BusinessHelper.GetLookupData(Utilities.Constants.University);
                List<Common.Entities.University> Universitylist = Common.BL.University.GetDistinctUniversityByYearAndCountry(0, int.Parse(drp_Country.SelectedValue), LCID);
                if (Universitylist.Count > 0)
                {
                    HelperMethods.BindDropDownList(ref drp_University, Universitylist, "ID", "ArabicTitle", "EnglishTitle", LCID);
                    drp_University.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));
                    drp_University.Visible = true;
                    lbl_University.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SearchRequests.drp_Country_SelectedIndexChanged");
            }
        }
       
        protected void drp_Faculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter SearchRequests.drp_Faculty_SelectedIndexChanged");

                //Bind Specialization
                List<Entities.Specialization> Specializationlist = BL.Specialization.GetAll();
                // SPListItemCollection Specializationlist = BusinessHelper.GetLookupData(Utilities.Constants.Specialization);
                if (Specializationlist.Count > 0)
                {
                    HelperMethods.BindDropDownList(ref drp_Specialization, Specializationlist, "ID", "ArabicTitle", "EnglishTitle", LCID);
                    drp_Specialization.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));
                    drp_Specialization.Visible = true;
                    lbl_Specialization.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SearchRequests.drp_Faculty_SelectedIndexChanged");
            }
        }
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_RequestID.Text) && string.IsNullOrEmpty(txt_NationalID.Text) && string.IsNullOrEmpty(txt_ApplicantName.Text)
               && drp_Nationality.SelectedIndex <= 0 && drp_NationCategory.SelectedIndex <= 0
               && string.IsNullOrEmpty(hdn_DateFrom.Value) && string.IsNullOrEmpty(hdn_DateTo.Value)
               && drp_Certificate.SelectedIndex <= 0 && drp_Country.SelectedIndex <= 0
               && drp_University.SelectedIndex <= 0 && string.IsNullOrEmpty(ddlFaculty.Text)
               && drp_Specialization.SelectedIndex <= 0 && drp_EntityNeedsEquivalency.SelectedIndex <= 0
               && drp_RequestStatus.SelectedIndex <= 0 && drp_Employees.SelectedIndex <= 0)
            {
                modalSearchFilter.Show();
            }
            else
            {
                BindGridOnSearch();
                if (!string.IsNullOrEmpty(hdf_University.Value))
                {
                    drp_University.Items.FindByText(hdf_University.Value.ToString()).Selected = true;

                }
            }

        }

        #region ExportExcelCommented
        /* Export to excel is removed for now 
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter SearchRequests.btnExportExcel_Click");


                List<Entities.SimilarRequest> srchRequests = GetSearchRequests();

                DataTable similarRequestTable = new DataTable();
                XLWorkbook wb = null;
                string fileName = string.Empty;

                if (LCID == (int)Language.English)
                {

                    similarRequestTable.Columns.Add("S", typeof(string));
                    similarRequestTable.Columns.Add("Request Number", typeof(string));
                    similarRequestTable.Columns.Add("Submit Date", typeof(string));
                    similarRequestTable.Columns.Add("Qatari ID", typeof(string));
                    similarRequestTable.Columns.Add("Applicant Name", typeof(string));
                    similarRequestTable.Columns.Add("Nationality", typeof(string));
                    similarRequestTable.Columns.Add("Country Name", typeof(string));
                    similarRequestTable.Columns.Add("Request Status", typeof(string));
                    similarRequestTable.Columns.Add("Responsible Officer", typeof(string));

                    int i = 1;

                    foreach (var request in srchRequests)
                    {
                        DataRow row = similarRequestTable.NewRow();
                        row["S"] = i;
                        row["Request Number"] = request.RequestNumber;
                        row["Submit Date"] = request.SubmitDate.ToShortDateString();
                        row["Qatari ID"] = request.QatariID;
                        row["Applicant Name"] = request.ApplicantName;
                        row["Nationality"] = request.Nationality;
                        row["Country Name"] = request.Country;
                        row["Request Status"] = request.RequestStatus;
                        row["Responsible Officer"] = request.AssignedTo;


                        similarRequestTable.Rows.Add(row);
                        i++;
                    }
                    wb = new ClosedXML.Excel.XLWorkbook();
                    wb.Worksheets.Add(similarRequestTable, "ApplicantsData");
                    fileName = "PA_EmployeeSearch.xlsx";

                }
                else
                {

                    similarRequestTable.Columns.Add("م", typeof(string));
                    similarRequestTable.Columns.Add("رقم الطلب", typeof(string));
                    similarRequestTable.Columns.Add("تاريخ إرسال الطلب", typeof(string));
                    similarRequestTable.Columns.Add("الرقم الشخصي", typeof(string));
                    similarRequestTable.Columns.Add("اسم الطالب", typeof(string));
                    similarRequestTable.Columns.Add("الجنسية", typeof(string));
                    similarRequestTable.Columns.Add("الدولة", typeof(string));
                    similarRequestTable.Columns.Add("حالة الطلب", typeof(string));
                    similarRequestTable.Columns.Add("الموظف المسئول", typeof(string));



                    int i = 1;
                    foreach (var request in srchRequests)
                    {
                        DataRow row = similarRequestTable.NewRow();
                        row["م"] = i;
                        row["رقم الطلب"] = request.RequestNumber;
                        row["تاريخ إرسال الطلب"] = request.SubmitDate.ToShortDateString();
                        row["الرقم الشخصي"] = request.QatariID;
                        row["اسم الطالب"] = request.ApplicantName;
                        row["الجنسية"] = request.Nationality;
                        row["الدولة"] = request.Country;
                        row["حالة الطلب"] = request.RequestStatus;
                        row["الموظف المسئول"] = request.AssignedTo;

                        similarRequestTable.Rows.Add(row);
                        i++;
                    }
                    wb = new ClosedXML.Excel.XLWorkbook() { RightToLeft = true };
                    wb.Worksheets.Add(similarRequestTable, "بيانات المقدمين");
                    fileName = "الموافقة المسبقة_بحث الموظفين.xlsx";

                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    //wb.SaveAs(fileName);
                    wb.SaveAs(memoryStream);

                    //byte[] bytes = new byte[file.Length];
                    //file.Read(bytes, 0, (int)file.Length);
                    //memoryStream.Write(bytes, 0, (int)file.Length);
                    memoryStream.WriteTo(HttpContext.Current.Response.OutputStream);


                    Response.Buffer = false;
                    // Clear all content output from the buffer stream 
                    Response.Clear();
                    // Add a HTTP header to the output stream that specifies the default filename 
                    // for the browser's download dialog 
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                    // Add a HTTP header to the output stream that contains the 
                    // content length(File Size). This lets the browser know how much data is being transfered 
                    Response.AddHeader("Content-Length", memoryStream.Length.ToString());
                    // Set the HTTP MIME type of the output stream 
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("X-Download-Options", "noopen");

                    // Write the data out to the client. 
                    //Response.BinaryWrite(bytes);
                    Response.BinaryWrite(memoryStream.ToArray());
                    Response.Flush();
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SearchRequests.btnExportExcel_Click");
            }
        }
        */
        #endregion
        protected void btnOk_Click(object sender, EventArgs e)
        {
            BindGridOnSearch();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            List<Entities.SimilarRequest> srchRequests = GetSearchRequests();

            // DataTable similarRequestTable = new DataTable();

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Applicants Data");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            //Header of table
            if (LCID == (int)Language.English)
            {
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cells[1, 1].Value = "S.No";
                workSheet.Cells[1, 2].Value = "Request Number";
                workSheet.Cells[1, 3].Value = "Submit Date";
                workSheet.Cells[1, 4].Value = "Qatari ID";
                workSheet.Cells[1, 5].Value = "Arabic Name";
                workSheet.Cells[1, 6].Value = "English Name";
                workSheet.Cells[1, 7].Value = "Nationality";
                workSheet.Cells[1, 8].Value = "Nation Catgeory";
                workSheet.Cells[1, 9].Value = "Certificate";
                workSheet.Cells[1, 10].Value = "Degree Details";
                workSheet.Cells[1, 11].Value = "Country Name";
                workSheet.Cells[1, 12].Value = "University";
                workSheet.Cells[1, 13].Value = "Faculty";
                workSheet.Cells[1, 14].Value = "Specialization";
                workSheet.Cells[1, 15].Value = "Request Status";
                workSheet.Cells[1, 16].Value = "Responsible Officer";
                //Body of table
                //
                int recordIndex = 2;
                foreach (var item in srchRequests)
                {
                    workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
                    workSheet.Cells[recordIndex, 2].Value = item.RequestNumber;
                    workSheet.Cells[recordIndex, 3].Value = item.SubmitDate.ToShortDateString();
                    workSheet.Cells[recordIndex, 4].Value = item.QatariID;
                    workSheet.Cells[recordIndex, 5].Value = item.ArabicName;
                    workSheet.Cells[recordIndex, 6].Value = item.EnglishName;
                    workSheet.Cells[recordIndex, 7].Value = item.Nationality;
                    workSheet.Cells[recordIndex, 8].Value = item.NationCatgeory;
                    workSheet.Cells[recordIndex, 9].Value = item.AcademicDegree;
                    workSheet.Cells[recordIndex, 10].Value = item.ProgramType;
                    workSheet.Cells[recordIndex, 11].Value = item.ProgramCountry;
                    workSheet.Cells[recordIndex, 12].Value = item.ProgramUniversity;
                    workSheet.Cells[recordIndex, 13].Value = item.Faculty;
                    workSheet.Cells[recordIndex, 14].Value = item.Specialization;
                    workSheet.Cells[recordIndex, 15].Value = item.RequestStatus;
                    //workSheet.Cells[recordIndex, 16].Value = item.;
                    workSheet.Cells[recordIndex, 16].Value = item.AssignedTo;

                    recordIndex++;
                }
                workSheet.Column(1).AutoFit();
                workSheet.Column(2).AutoFit();
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).AutoFit();
                string excelName = "PA_EmployeeSearch";

                using (var memoryStream = new MemoryStream())
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                    excel.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            else
            {

                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cells[1, 1].Value = "م";
                workSheet.Cells[1, 2].Value = "رقم الطلب";
                workSheet.Cells[1, 3].Value = "تاريخ إرسال الطلب";
                workSheet.Cells[1, 4].Value = "الرقم الشخصي";
                workSheet.Cells[1, 5].Value = "اسم الطالب عربي";
                workSheet.Cells[1, 6].Value = "اسم الطالب إنجليزي";
                workSheet.Cells[1, 7].Value = "الجنسية";
                workSheet.Cells[1, 8].Value = "فئة الجنسية";
                workSheet.Cells[1, 9].Value = "الشهادة";
                workSheet.Cells[1, 10].Value = "الدرجة العلمية";
                workSheet.Cells[1, 11].Value = "الدولة";
                workSheet.Cells[1, 12].Value = "الجامعة";
                workSheet.Cells[1, 13].Value = "الكلية";
                workSheet.Cells[1, 14].Value = "التخصص";
                workSheet.Cells[1, 15].Value = "حالة الطلب";
                //workSheet.Cells[1, 16].Value = "مرحلة الطلب";
                workSheet.Cells[1, 16].Value = "الموظف المسئول";
                //Body of table
                //
                int recordIndex = 2;
                foreach (var item in srchRequests)
                {
                    workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
                    workSheet.Cells[recordIndex, 2].Value = item.RequestNumber;
                    workSheet.Cells[recordIndex, 3].Value = item.SubmitDate.ToShortDateString();
                    workSheet.Cells[recordIndex, 4].Value = item.QatariID;
                    workSheet.Cells[recordIndex, 5].Value = item.ArabicName;
                    workSheet.Cells[recordIndex, 6].Value = item.EnglishName;
                    workSheet.Cells[recordIndex, 7].Value = item.Nationality;
                    workSheet.Cells[recordIndex, 8].Value = item.NationCatgeory;
                    workSheet.Cells[recordIndex, 9].Value = item.AcademicDegree;
                    workSheet.Cells[recordIndex, 10].Value = item.ProgramType;
                    workSheet.Cells[recordIndex, 11].Value = item.ProgramCountry;
                    workSheet.Cells[recordIndex, 12].Value = item.ProgramUniversity;
                    workSheet.Cells[recordIndex, 13].Value = item.Faculty;
                    workSheet.Cells[recordIndex, 14].Value = item.Specialization;
                    workSheet.Cells[recordIndex, 15].Value = item.RequestStatus;
                    //workSheet.Cells[recordIndex, 16].Value = item.;
                    workSheet.Cells[recordIndex, 16].Value = item.AssignedTo;
                    recordIndex++;
                }
                workSheet.Column(1).AutoFit();
                workSheet.Column(2).AutoFit();
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).AutoFit();
                string excelName = "PA_EmployeeSearch";

                using (var memoryStream = new MemoryStream())
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                    excel.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }

        protected void lnk_FinalDecisionFile_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter grd_Requests.lnk_Edit_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                Label RequestNo = (Label)gvr.FindControl("lbl_RequestID");
                HiddenField RequestID = (HiddenField)gvr.FindControl("hdn_ID");
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdn_RequestStatusId");
                RequestStatus requestStatus =Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                if (requestStatus.Code == Common.Utilities.RequestStatus.PAHeadManagerAccepted.ToString() || requestStatus.Code == Common.Utilities.RequestStatus.PAHeadManagerRejected.ToString())
                {
                    List<Proc> RecommendProc = Common.BL.Procedure.GetPAApprovedRecommendationStatus(RequestID.Value, "Approved");
                    if (RecommendProc.Count > 0)
                    {
                        Common.BL.FinalDecisionPrint.PAPDFExportation(RequestNo.Text, "PDF", LCID);

                    }
                }
               
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit grd_Requests.lnk_FinalDecisionFile_Click");
            }
        }

        
    }

}
