//using ClosedXML.Excel;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class SearchRequests : UserControlBase
    {
        #region Public Properties

        public string SPGroupName { get; set; }

        #endregion Public Properties

        protected void Page_Load(object sender, EventArgs e)
        {
            //var hdnCtl = Page.Request.Form.Get("__EventTriggerControlIdSearch");
            //if (string.IsNullOrEmpty(hdnCtl))
            //{
                grd_Requests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SearchPageSize"));

                BindDropDowns();

            //}
        }

        private List<Entities.SimilarRequest> GetAllSimilarRequestsData()
        {
            List<Entities.SimilarRequest> srchRequests = new List<Entities.SimilarRequest>();
            try
            {
                Logging.GetInstance().Debug("Entering method SearchRequests.GetAllSimilarRequestsData");
                List<string> objColumns = new List<string>();

                if (drp_RequestStatus.SelectedIndex != 0 & drp_RequestStatus.SelectedIndex != -1)
                {
                    
                        objColumns.Add("RequestStatus;Lookup;Eq;" + drp_RequestStatus.SelectedValue);
                   
                }
                if (!string.IsNullOrEmpty(hdf_University.Value) /*drp_University.SelectedIndex != 0 & drp_University.SelectedIndex != -1*/)
                {
                    if (LCID == (int)Language.English)
                        objColumns.Add("University;Lookup;Eq;" + hdf_University.Value.ToString());
                    else
                        objColumns.Add("UniversityAr;Lookup;Eq;" + hdf_University.Value.ToString());
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
                        objColumns.Add("Specialization;Lookup;Eq;" + drp_Specialization.SelectedItem.Text);
                    else
                        objColumns.Add("SpecializationAr;Lookup;Eq;" + drp_Specialization.SelectedItem.Text);
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
                        objColumns.Add("Faculty;Text;Eq;" + ddlFaculty.Text);
                    else
                        objColumns.Add("FacultyAr;Text;Eq;" + ddlFaculty.Text);
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
                        objColumns.Add("CountryOfStudy;Lookup;Eq;" + drp_Country.SelectedItem.Text);
                    else
                        objColumns.Add("CountryOfStudyAr;Lookup;Eq;" + drp_Country.SelectedItem.Text);
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
                        objColumns.Add("RequestNumber;Text;Contains;" + txt_RequestID.Text.Trim());
                    else
                        objColumns.Add("RequestNumber;Text;Contains;" + txt_RequestID.Text.Trim());
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
                        objColumns.Add("Applicants_EnglishName;Text;Contains;" + txt_ApplicantName.Text.ToLower());
                    else
                        objColumns.Add("Applicants_ArabicName;Text;Contains;" + txt_ApplicantName.Text.ToLower());
                }

                if (objColumns.Count > 0)
                    srchRequests = BL.SearchSimilarRequests.GetAllRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(objColumns, "And", true) + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();
                else
                    srchRequests = BL.SearchSimilarRequests.GetAllRequests("<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SearchRequests.GetAllSimilarRequestsData");
            }
            return srchRequests;
        }

        private List<Entities.Payments> GetAllPaymentRequestsData()
        {
            List<Entities.Payments> srchRequests = new List<Entities.Payments>();
            try
            {
                Logging.GetInstance().Debug("Entering method SearchRequests.GetAllPaymentRequestsData");
                srchRequests = BL.SearchSimilarRequests.GetAllPaymentRequests("<OrderBy><FieldRef Name='ReceiptDate' Ascending='False' /></OrderBy>", LCID).ToList();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SearchRequests.GetAllPaymentRequestsData");
            }
            return srchRequests;
        }

        private void BindGridOnSearch()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method SearchRequests.BindGridOnSearch");
                List<string> objColumns = new List<string>();

                List<Entities.SimilarRequest> srchRequests = GetAllSimilarRequestsData();

                HelperMethods.BindGridView(grd_Requests, srchRequests);

                if (srchRequests.Count > 0)
                {
                    //SrchControls.Visible = true;
                    lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NoOfRequests", (uint)LCID) + srchRequests.Count;
                    lbl_NoOfRequests.Visible = true;
                }
                else
                {
                    //SrchControls.Visible = false;
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
                List<CountryOfStudy> countryItems = Common.BL.CountryOfStudy.GetAll(LCID);
                List<Entities.EntityNeedsEquivalency> entityNeedsEquivalencyItems = BL.EntityNeedsEquivalency.GetAll();
                List<Entities.AcademicDegree> academicDegreeItems = BL.AcademicDegree.GetAll();
                List<Entities.NationalityCategory> nationalityCategoryItems = BL.NationalityCategory.GetAll();

                //Bind Request Status
                // List<Common.Entities.RequestStatus> requestStatusItems = Common.BL.RequestStatus.GetDistinctReviewerStatus(LCID);
                var requestStatusItems = Common.BL.RequestStatus.GetDistinctStatusToApplicant();
                if (requestStatusItems != null && requestStatusItems.Count > 0)
                {
                    HelperMethods.BindDropDownList(ref drp_RequestStatus, requestStatusItems, "ApplicantDescriptionEn", "ApplicantDescriptionAr", "ApplicantDescriptionEn", LCID);
                    //HelperMethods.BindDropDownList(ref drp_RequestStatus, requestStatusItems, "ID", "ApplicantDescriptionAr", "ApplicantDescriptionEn", LCID);
                    drp_RequestStatus.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                }
                //Bind Nationality Category
                drp_NationCategory.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                if (nationalityCategoryItems != null && nationalityCategoryItems.Count > 0)
                {
                    drp_NationCategory.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref drp_NationCategory, nationalityCategoryItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }
                //Bind Certificates
                drp_Certificate.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                if (academicDegreeItems != null && academicDegreeItems.Count > 0)
                {
                    drp_Certificate.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref drp_Certificate, academicDegreeItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }
                //Bind Nationality
                drp_Nationality.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                if (nationalityItems != null && nationalityItems.Count > 0)
                {
                    drp_Nationality.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref drp_Nationality, nationalityItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }
                //Bind Country
                drp_Country.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                if (countryItems != null && countryItems.Count > 0)
                {
                    drp_Country.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref drp_Country, countryItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }
                //Bind Specialization
                List<Entities.Specialization> Specializationlist = BL.Specialization.GetAll();
                drp_Specialization.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
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
                //Bind Program Managers
                users = HelperMethods.GetGroupUsers(Common.Utilities.Constants.ProgramManagerGroupName);
                foreach (SPUser user in users)
                {
                    var exist = drp_Employees.Items.FindByValue(user.LoginName);
                    if (exist == null)
                        drp_Employees.Items.Add(new ListItem(user.Name, user.LoginName));
                }
                //Bind Head Managers
                users = HelperMethods.GetGroupUsers(Common.Utilities.Constants.ProgramManagerGroupName);
                foreach (SPUser user in users)
                {
                   var exist = drp_Employees.Items.FindByValue(user.LoginName);
                    if(exist==null)
                    drp_Employees.Items.Add(new ListItem(user.Name, user.LoginName));
                }

                drp_Employees.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));

                // Bind Entity Needs Equivalency
                drp_EntityNeedsEquivalency.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                if (entityNeedsEquivalencyItems != null && entityNeedsEquivalencyItems.Count > 0)
                {
                    drp_EntityNeedsEquivalency.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref drp_EntityNeedsEquivalency, entityNeedsEquivalencyItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }
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
            ddlFaculty.Text = string.Empty;
            drp_EntityNeedsEquivalency.ClearSelection();
            drp_NationCategory.ClearSelection();
            drp_Nationality.ClearSelection();
            //drp_Specialization.Items.Clear();
            drp_RequestStatus.ClearSelection();
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
                            if (HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName) && (int.Parse(hdnRequestStatusId.Value) == (int)Common.Utilities.RequestStatus.UCECulturalMissionNeedsStatement || int.Parse(hdnRequestStatusId.Value) == (int)Common.Utilities.RequestStatus.UCEHigherEduInstitutesNeedsStatement))
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
                Page.Session["DisplayRequestId"] = lblRequestID.Value;
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdn_RequestStatusId");
                Common.Entities.RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                string viewLink = string.Empty;
                if (requestStatus.CanReviewerEditRequest)
                    viewLink = requestStatus.ReviewerTargetPageURL;
                if(requestStatus.Code == Common.Utilities.RequestStatus.UCEDraft.ToString())
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

                Page.Session["DisplayRequestId"] = lblRequestID.Value;
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
                
                List<University> university = Common.BL.University.GetDistinctUniversityByYearAndCountry(0, int.Parse(drp_Country.SelectedValue), LCID);
                if (university.Count > 0)
                {
                    HelperMethods.BindDropDownList(ref drp_University, university, "ID", "ArabicTitle", "EnglishTitle", LCID);
                    drp_University.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
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

        protected void drp_University_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter SearchRequests.drp_University_SelectedIndexChanged");

                //Bind Faculty
                //SPListItemCollection Facultylist = BusinessHelper.GetLookupData(Utilities.Constants.Faculty);
                //if (Facultylist.Count > 0)
                //{
                //    HelperMethods.BindDropDownList(ref drp_Faculty, Facultylist, "ID", "TitleAr", "Title", LCID);
                //    drp_Faculty.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                //    drp_Faculty.Visible = true;
                //    lbl_Faculty.Visible = true;
                //}

                //Bind Specialization
                //SPListItemCollection Specializationlist = BusinessHelper.GetLookupData(Utilities.Constants.Specialization);
                List<Entities.Specialization> Specializationlist = BL.Specialization.GetAll();
                if (Specializationlist.Count > 0)
                {
                    HelperMethods.BindDropDownList(ref drp_Specialization, Specializationlist, "ID", "ArabicTitle", "EnglishTitle", LCID);
                    drp_Specialization.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
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
                Logging.GetInstance().Debug("Exit SearchRequests.drp_University_SelectedIndexChanged");
            }
        }

        protected void drp_Faculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter SearchRequests.drp_Faculty_SelectedIndexChanged");

                //Bind Specialization
                List<Entities.Specialization> Specializationlist = BL.Specialization.GetAll();
                //SPListItemCollection Specializationlist = BusinessHelper.GetLookupData(Utilities.Constants.Specialization);
                if (Specializationlist.Count > 0)
                {
                    HelperMethods.BindDropDownList(ref drp_Specialization, Specializationlist, "ID", "ArabicTitle", "EnglishTitle", LCID);
                    drp_Specialization.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
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

            //SrchControls.Visible = true;
        }

        public DataTable GetData()
        {
            DataTable similarRequestTable = new DataTable();

            similarRequestTable.Columns.Add("S", typeof(string));
            similarRequestTable.Columns.Add("Request Number", typeof(string));
            similarRequestTable.Columns.Add("Submit Date", typeof(string));
            similarRequestTable.Columns.Add("Qatari ID", typeof(string));
            similarRequestTable.Columns.Add("Applicant Name", typeof(string));
            //similarRequestTable.Columns.Add("Nationality", typeof(string));
            //similarRequestTable.Columns.Add("Certificate", typeof(string));
            //similarRequestTable.Columns.Add("Entity Needs Certificate", typeof(string));
            //similarRequestTable.Columns.Add("Country Name", typeof(string));
            //similarRequestTable.Columns.Add("Request Status", typeof(string));
            //similarRequestTable.Columns.Add("Responsible Officer", typeof(string));

            DataRow row = similarRequestTable.NewRow();
            row["S"] = 1;
            row["Request Number"] = "ReqNum";
            row["Submit Date"] = "Submit Date";
            row["Qatari ID"] = "Qatari ID";
            row["Applicant Name"] = "Applicant Name";
            //row["Nationality"] = request.Nationality;
            //row["Certificate"] = request.AcademicDegree;
            //row["Entity Needs Certificate"] = request.EntityNeedsEquivalency;
            //row["Country Name"] = request.Country;
            //row["Request Status"] = request.RequestStatus;
            //row["Responsible Officer"] = request.AssignedTo;

            similarRequestTable.Rows.Add(row);

            //DataSet ds = similarRequestTable.DataSet;

            return similarRequestTable;
        }

        #region ExportExcelCommented

        /* Excel is removed for now
        public  void ExportDataOpenXML()
        {
            string fileName = "SearchResultsOpen.xlsx";
            //MemoryStream documentStream = new MemoryStream();
            ////using (MemoryStream documentStream = new MemoryStream())
            ////{
            //

            MemoryStream SpreadsheetStream = new MemoryStream();
            {
                // Create the spreadsheet on the MemoryStream
                using (SpreadsheetDocument workbook = SpreadsheetDocument.Create(SpreadsheetStream, SpreadsheetDocumentType.Workbook))
                {
                    DataTable table = GetData();

                    var workbookPart = workbook.AddWorkbookPart();

                    workbook.WorkbookPart.Workbook = new Workbook();

                    workbook.WorkbookPart.Workbook.Sheets = new Sheets();

                    // foreach (DataTable table in ds.Tables) {
                    var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
                    var sheetData = new SheetData();
                    sheetPart.Worksheet = new Worksheet(sheetData);

                    Sheets sheets = workbook.WorkbookPart.Workbook.GetFirstChild<Sheets>();
                    string relationshipId = workbook.WorkbookPart.GetIdOfPart(sheetPart);

                    uint sheetId = 1;
                    if (sheets.Elements<Sheet>().Count() > 0)
                    {
                        sheetId =
                            sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                    }

                    Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = "ApplicantsData" };
                    sheets.Append(sheet);

                    Row headerRow = new Row();

                    List<String> columns = new List<string>();
                    foreach (DataColumn column in table.Columns)
                    {
                        columns.Add(column.ColumnName);

                        Cell cell = new Cell();
                        cell.DataType = CellValues.String;
                        cell.CellValue = new CellValue(column.ColumnName);
                        headerRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(headerRow);

                    foreach (DataRow dsrow in table.Rows)
                    {
                        Row newRow = new Row();
                        foreach (String col in columns)
                        {
                            Cell cell = new Cell();
                            cell.DataType = CellValues.String;
                            cell.CellValue = new CellValue(dsrow[col].ToString());
                            newRow.AppendChild(cell);
                        }

                        sheetData.AppendChild(newRow);
                    }

                    //}

                    workbook.Save();
                    SpreadsheetStream.WriteTo(HttpContext.Current.Response.OutputStream);
                    SpreadsheetStream.Seek(0, SeekOrigin.Begin);

                    Response.Buffer = false;
                    // Clear all content output from the buffer stream
                    Response.Clear();
                    // Add a HTTP header to the output stream that specifies the default filename
                    // for the browser's download dialog
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                    // Add a HTTP header to the output stream that contains the
                    // content length(File Size). This lets the browser know how much data is being transfered
                    Response.AddHeader("Content-Length", SpreadsheetStream.ToArray().Length.ToString());
                    // Set the HTTP MIME type of the output stream
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("X-Download-Options", "noopen");
                    Response.BinaryWrite(SpreadsheetStream.ToArray());
                    Response.Flush();
                    // the stream is complete here
                    //AsMemoryStream = new MemoryStream();
                    //documentStream.CopyTo(AsMemoryStream);
                }
            }
        //string destination = "C:\\SearchResultsOpen.xlsx";
        //using (var workbook = SpreadsheetDocument.Create(destination, SpreadsheetDocumentType.Workbook))
        //{
        //    DataTable table = GetData();

        //    var workbookPart = workbook.AddWorkbookPart();

        //    workbook.WorkbookPart.Workbook = new Workbook();

        //    workbook.WorkbookPart.Workbook.Sheets = new Sheets();

        //    // foreach (DataTable table in ds.Tables) {
        //    var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
        //    var sheetData = new SheetData();
        //    sheetPart.Worksheet = new Worksheet(sheetData);

        //    Sheets sheets = workbook.WorkbookPart.Workbook.GetFirstChild<Sheets>();
        //    string relationshipId = workbook.WorkbookPart.GetIdOfPart(sheetPart);

        //    uint sheetId = 1;
        //    if (sheets.Elements<Sheet>().Count() > 0)
        //    {
        //        sheetId =
        //            sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
        //    }

        //    Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = "ApplicantsData" };
        //    sheets.Append(sheet);

        //    Row headerRow = new Row();

        //    List<String> columns = new List<string>();
        //    foreach (DataColumn column in table.Columns)
        //    {
        //        columns.Add(column.ColumnName);

        //        Cell cell = new Cell();
        //        cell.DataType = CellValues.String;
        //        cell.CellValue = new CellValue(column.ColumnName);
        //        headerRow.AppendChild(cell);
        //    }

        //    sheetData.AppendChild(headerRow);

        //    foreach (DataRow dsrow in table.Rows)
        //    {
        //        Row newRow = new Row();
        //        foreach (String col in columns)
        //        {
        //            Cell cell = new Cell();
        //            cell.DataType = CellValues.String;
        //            cell.CellValue = new CellValue(dsrow[col].ToString());
        //            newRow.AppendChild(cell);
        //        }

        //        sheetData.AppendChild(newRow);

        //    }

        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        //wb.SaveAs(fileName);
        //        wb.SaveAs(memoryStream);

        //        //byte[] bytes = new byte[file.Length];
        //        //file.Read(bytes, 0, (int)file.Length);
        //        //memoryStream.Write(bytes, 0, (int)file.Length);
        //        memoryStream.WriteTo(HttpContext.Current.Response.OutputStream);

        //        Response.Buffer = false;
        //        // Clear all content output from the buffer stream
        //        Response.Clear();
        //        // Add a HTTP header to the output stream that specifies the default filename
        //        // for the browser's download dialog
        //        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
        //        // Add a HTTP header to the output stream that contains the
        //        // content length(File Size). This lets the browser know how much data is being transfered
        //        Response.AddHeader("Content-Length", memoryStream.Length.ToString());
        //        // Set the HTTP MIME type of the output stream
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("X-Download-Options", "noopen");

        //        // Write the data out to the client.
        //        //Response.BinaryWrite(bytes);
        //        Response.BinaryWrite(memoryStream.ToArray());
        //        Response.Flush();
        //    }

        //    //}
        //}
    }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter SearchRequests.btnExportExcel_Click");

                //ExportDataOpenXML();

                List<Entities.SimilarRequest> srchRequests = GetAllSimilarRequestsData();
                //using (OfficeOpenXml.ExcelPackage excel = new OfficeOpenXml.ExcelPackage())
                //{
                //    excel.Workbook.Worksheets.Add("Worksheet1");
                //    excel.Workbook.Worksheets.Add("Worksheet2");
                //    excel.Workbook.Worksheets.Add("Worksheet3");

                //    var worksheet = excel.Workbook.Worksheets["Worksheet1"];
                //    worksheet.Cells["A1"].Value = "Hello World!";

                //    FileInfo excelFile = new FileInfo(@"C:\\test.xlsx");
                //    excel.SaveAs(excelFile);
                //}

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
                    similarRequestTable.Columns.Add("Certificate", typeof(string));
                    similarRequestTable.Columns.Add("Entity Needs Certificate", typeof(string));
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
                        row["Certificate"] = request.AcademicDegree;
                        row["Entity Needs Certificate"] = request.EntityNeedsEquivalency;
                        row["Country Name"] = request.Country;
                        row["Request Status"] = request.RequestStatus;
                        row["Responsible Officer"] = request.AssignedTo;

                        similarRequestTable.Rows.Add(row);
                        i++;
                    }
                    wb = new ClosedXML.Excel.XLWorkbook();
                    wb.Worksheets.Add(similarRequestTable, "ApplicantsData");
                    fileName = "UCE_EmployeeSearch.xlsx";
                }
                else
                {
                    similarRequestTable.Columns.Add("م", typeof(string));
                    similarRequestTable.Columns.Add("رقم الطلب", typeof(string));
                    similarRequestTable.Columns.Add("تاريخ إرسال الطلب", typeof(string));
                    similarRequestTable.Columns.Add("الرقم الشخصي", typeof(string));
                    similarRequestTable.Columns.Add("اسم الطالب", typeof(string));
                    similarRequestTable.Columns.Add("الجنسية", typeof(string));
                    similarRequestTable.Columns.Add("الشهادة", typeof(string));
                    similarRequestTable.Columns.Add("الجهة الطالبة للشهادة", typeof(string));
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
                        row["الشهادة"] = request.AcademicDegree;
                        row["الجهة الطالبة للشهادة"] = request.EntityNeedsEquivalency;
                        row["الدولة"] = request.Country;
                        row["حالة الطلب"] = request.RequestStatus;
                        row["الموظف المسئول"] = request.AssignedTo;

                        similarRequestTable.Rows.Add(row);
                        i++;
                    }
                    wb = new ClosedXML.Excel.XLWorkbook() { RightToLeft = true };
                    wb.Worksheets.Add(similarRequestTable, "بيانات المقدمين");

                    fileName = "معادلة الشهادت_بحث الموظفين.xlsx";
                }

                Workbook workbook = new Workbook();
                //Worksheet sheet = workbook.wor;

                //            Worksheet sheet = workbook.Worksheets[0];
                //            sheet.Name = "Test";

                //sheet.Range["A1"].Text = "Text";

                //sheet.Range["A2"].Text = "Number";

                //sheet.Range["B1"].Text = "Hello World";

                //sheet.Range["B2"].NumberValue = 3.1415926;

                //sheet.Range["A7"].Text = "This Excel file is created by Spire.XLS for WPF";

                //workbook.SaveToFile("sample.xlsx", ExcelVersion.Version2013);

                //System.Diagnostics.Process.Start("sample.xlsx");

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

            //var lines = new List<string>();

            //string[] columnNames = similarRequestTable.Columns.Cast<DataColumn>().
            //                                  Select(column => column.ColumnName).
            //                                  ToArray();

            //var header = string.Join(",", columnNames);
            //lines.Add(header);

            //var valueLines = similarRequestTable.AsEnumerable()
            //                   .Select(row => string.Join(",", row.ItemArray));
            //lines.AddRange(valueLines);

            //File.WriteAllLines("C:\\EmployeeSearch.csv", lines);
        }
        */

        #endregion ExportExcelCommented

        protected void btnOk_Click(object sender, EventArgs e)
        {
            BindGridOnSearch();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            List<Entities.SimilarRequest> srchRequests = GetAllSimilarRequestsData();

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
                workSheet.Cells[1, 10].Value = "Entity Needs Certificate";
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
                    workSheet.Cells[recordIndex, 10].Value = item.EntityNeedsEquivalency;
                    workSheet.Cells[recordIndex, 11].Value = item.Country;
                    workSheet.Cells[recordIndex, 12].Value = item.University;
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
                string excelName = "UCE_EmployeeSearch";

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
                workSheet.Cells[1, 10].Value = "الجهة الطالبة للشهادة";
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
                    workSheet.Cells[recordIndex, 10].Value = item.EntityNeedsEquivalency;
                    workSheet.Cells[recordIndex, 11].Value = item.Country;
                    workSheet.Cells[recordIndex, 12].Value = item.University;
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
                string excelName = "UCE_EmployeeSearch";

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
                RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                if (requestStatus.Code == Common.Utilities.RequestStatus.UCEClosedByAcceptance.ToString() || requestStatus.Code == Common.Utilities.RequestStatus.UCEClosedByRejection.ToString())
                {
                    
                    List<Proc> RecommendProc = Common.BL.Procedure.GetApprovedRecommendationStatus(RequestID.Value, "Approved");
                    if (RecommendProc.Count > 0)
                    {
                        Common.BL.FinalDecisionPrint.PDFExportation(RequestNo.Text, SPHttpUtility.HtmlDecode(RecommendProc[0].DecisionForPrint), RecommendProc[0].OccupationName.ToString(), ExtensionMethods.QatarFormatedDate(Convert.ToDateTime(RecommendProc[0].BookDate)), RecommendProc[0].BookNum, RecommendProc[0].HeadManagerName, RecommendProc[0].SirValue, RecommendProc[0].RespectedValue, "PDF", LCID);

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