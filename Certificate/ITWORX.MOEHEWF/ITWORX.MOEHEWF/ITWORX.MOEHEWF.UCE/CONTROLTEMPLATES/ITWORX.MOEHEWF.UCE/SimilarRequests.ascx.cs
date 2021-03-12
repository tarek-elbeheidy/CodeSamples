using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class SimilarRequests : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                grd_SimilarRequests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SimilarRequestsPageSize"));

                //BindGridOnSearch();
                BindDropDowns();
                lbl_SuccessMsg.Visible = false;
                btn_ApprovedAttachement.Visible = false;
                //CheckedSimilarRequests();
                if (Page.Session["DisplayRequestId"] != null)
                {
                    var reqStatusNumber = Common.BL.Request.GetRequesStaustByOnlyRequestNumber(int.Parse(Page.Session["DisplayRequestId"].ToString()));

                    if (reqStatusNumber != 0 && reqStatusNumber == (int)Common.Utilities.RequestStatus.UCEClosedByAcceptance)
                    {
                        btn_ApprovedAttachement.Visible = false;
                    }
                }
            }
        }

        private void CheckedSimilarRequests()
        {
            try
            {
                if (Page.Session["DisplayRequestId"] != null)
                {
                    List<int> RequestsList = BL.SearchSimilarRequests.GetCheckedRequests(int.Parse(Page.Session["DisplayRequestId"].ToString()));
                    if (RequestsList.Count > 0)
                    {
                        //SrchControls.Visible = false;
                        btn_ApprovedAttachement.Enabled = false;
                        for (int i = 0; i < grd_SimilarRequests.Rows.Count; i++)
                        {
                            HiddenField hfUser = (HiddenField)grd_SimilarRequests.Rows[i].Cells[1].FindControl("hdn_ID");
                            if (RequestsList.Contains(Convert.ToInt32(hfUser.Value.ToString())))
                            {
                                CheckBox chkSelect = (CheckBox)grd_SimilarRequests.Rows[i].Cells[0].FindControl("chkbox_Select");
                                //chkSelect.Checked = true;
                                chkSelect.Enabled = false;
                                grd_SimilarRequests.Rows[i].BackColor = Color.Gray;
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
                Logging.GetInstance().Debug("Exiting method SimilarRequests.BindDropDowns");
            }
        }

        private void BindDropDowns()
        {
            try
            {
                List<Entities.AcademicDegree> academicDegreeItems = BL.AcademicDegree.GetAll();
                List<CountryOfStudy> countryItems = Common.BL.CountryOfStudy.GetAll(LCID);
                List<Entities.StudyLanguage> studyLanguageItems = BL.StudyLanguage.GetAll();
                List<Entities.StudyType> studyTypeItems = BL.StudyType.GetAll();
                List<Entities.StudySystem> studySystemItems = BL.StudySystem.GetAll();
                List<Entities.EntityNeedsEquivalency> entityNeedsEquivalencyItems = BL.EntityNeedsEquivalency.GetAll();
                List<Entities.StudyAttachmentTypes> studyAttachTypes = BL.StudyAttachmentTypes.GetAll();
                List<Entities.NationalityCategory> nationalityItems = BL.NationalityCategory.GetAll();
                Logging.GetInstance().Debug("Entering method SimilarRequests.BindDropDowns");

                //Bind Academic Degree
                drp_AcademicDegree.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                if (academicDegreeItems != null && academicDegreeItems.Count > 0)
                {
                    drp_AcademicDegree.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref drp_AcademicDegree, academicDegreeItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }
                //Bind Study Type
                drp_StudyType.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                if (studyTypeItems != null && studyTypeItems.Count > 0)
                {
                    drp_StudyType.AppendDataBoundItems = true;
                    //HelperMethods.BindDropDownList(ref drp_StudyType, studyTypeItems, "ID", "TitleAr", "Title", LCID);
                    HelperMethods.BindDropDownList(ref drp_StudyType, studyTypeItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }
                //Bind Study System
                drp_StudySystem.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                if (studySystemItems != null && studySystemItems.Count > 0)
                {
                    drp_StudySystem.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref drp_StudySystem, studySystemItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }
                //Bind Study Language
                drp_StudyLanguage.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                if (studyLanguageItems != null && studyLanguageItems.Count > 0)
                {
                    drp_StudyLanguage.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref drp_StudyLanguage, studyLanguageItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }

                //Bind Country
                drp_Country.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                if (countryItems != null && countryItems.Count > 0)
                {
                    drp_Country.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref drp_Country, countryItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }
                // Bind Entity Needs Equivalency
                drp_EntityNeedsEquivalency.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                if (entityNeedsEquivalencyItems != null && entityNeedsEquivalencyItems.Count > 0)
                {
                    drp_EntityNeedsEquivalency.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref drp_EntityNeedsEquivalency, entityNeedsEquivalencyItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }
                //Bind Nationality
                drp_Nationality.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                if (nationalityItems != null && nationalityItems.Count > 0)
                {
                    drp_Nationality.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref drp_Nationality, nationalityItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
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
                //Bind Final Decisions
                List<Common.Entities.RequestStatus> requestFinalDecision = Common.BL.RequestStatus.GetStatusFinalDecisionToApplicant();
                HelperMethods.BindDropDownList(ref drp_FinalDecision, requestFinalDecision, "ID", "FinalDecisionAr", "FinalDecisionEn", LCID);
                drp_FinalDecision.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SimilarRequests.BindDropDowns");
            }
        }

        private void ClearControls()
        {
            drp_AcademicDegree.ClearSelection();
            drp_Country.ClearSelection();
            drp_EntityNeedsEquivalency.ClearSelection();
            ddlFaculty.Text = string.Empty;
            drp_FinalDecision.ClearSelection();
            drp_Nationality.ClearSelection();
            drp_Specialization.Items.Clear();
            drp_StudyLanguage.ClearSelection();
            drp_StudySystem.ClearSelection();
            drp_StudyType.ClearSelection();
            drp_University.Items.Clear();
            hdn_DateFrom.Value = string.Empty;
            hdn_DateTo.Value = string.Empty;
            BindGridOnSearch();
            CheckedSimilarRequests();
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            BindGridOnSearch();
            CheckedSimilarRequests();
            // SrchControls.Visible = true;
        }

        private void BindGridOnSearch()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method SimilarRequests.BindGridOnSearch");
                            List<string> objColumns = new List<string>();
                            if (drp_University.SelectedIndex != 0 & drp_University.SelectedIndex != -1)
                            {
                                if (LCID == (int)Language.English)
                                    objColumns.Add("University;Lookup;Eq;" + drp_University.SelectedItem.Text.ToString());
                                else
                                    objColumns.Add("UniversityAr;Lookup;Eq;" + drp_University.SelectedItem.Text.ToString());
                            }
                            if (drp_StudyType.SelectedIndex != 0 & drp_StudyType.SelectedIndex != -1)
                            {
                                if (LCID == (int)Language.English)
                                    objColumns.Add("StudyType;Lookup;Eq;" + drp_StudyType.SelectedItem.Text.ToString());
                                else
                                    objColumns.Add("StudyTypeAr;Lookup;Eq;" + drp_StudyType.SelectedItem.Text.ToString());
                            }
                            if (drp_StudySystem.SelectedIndex != 0 & drp_StudySystem.SelectedIndex != -1)
                            {
                                if (LCID == (int)Language.English)
                                    objColumns.Add("StudySystem;Lookup;Eq;" + drp_StudySystem.SelectedItem.Text.ToString());
                                else
                                    objColumns.Add("StudySystemAr; Lookup;Eq;" + drp_StudySystem.SelectedItem.Text.ToString());
                            }
                            if (!string.IsNullOrEmpty(hdn_DateFrom.Value) && !string.IsNullOrEmpty(hdn_DateTo.Value))
                            {
                                objColumns.Add("SubmitDate;DateTime;Geq;" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(Convert.ToDateTime(hdn_DateFrom.Value)));
                                objColumns.Add("SubmitDate;DateTime;Leq;" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(Convert.ToDateTime(hdn_DateTo.Value)));
                            }

                            if (drp_StudyLanguage.SelectedIndex != 0 & drp_StudyLanguage.SelectedIndex != -1)
                            {
                                if (LCID == (int)Language.English)
                                    objColumns.Add("StudyLanguage;Lookup;Eq;" + drp_StudyLanguage.SelectedItem.Text.ToString());
                                else
                                    objColumns.Add("StudyLanguageAr;Lookup;Eq;" + drp_StudyLanguage.SelectedItem.Text.ToString());
                            }
                            if (drp_Specialization.SelectedIndex != 0 & drp_Specialization.SelectedIndex != -1)
                            {
                                if (LCID == (int)Language.English)
                                    objColumns.Add("Specialization;Lookup;Eq;" + drp_Specialization.SelectedItem.Text.ToString());
                                else
                                    objColumns.Add("SpecializationAr;Lookup;Eq;" + drp_Specialization.SelectedItem.Text.ToString());
                            }
                            if (drp_Nationality.SelectedIndex != 0 & drp_Nationality.SelectedIndex != -1)
                            {
                                if (LCID == (int)Language.English)
                                    objColumns.Add("NationalityCategory_Title;Lookup;Eq;" + drp_Nationality.SelectedItem.Text.ToString());
                                else
                                    objColumns.Add("NationalityCategory_TitleAr;Lookup;Eq;" + drp_Nationality.SelectedItem.Text.ToString());
                            }
                            if (drp_FinalDecision.SelectedIndex != 0 & drp_FinalDecision.SelectedIndex != -1)
                            {
                                if (LCID == (int)Language.English)
                                    objColumns.Add("RequestStatusId;Lookup;Eq;" + drp_FinalDecision.SelectedValue.ToString());
                                else
                                    objColumns.Add("RequestStatusId;Lookup;Eq;" + drp_FinalDecision.SelectedValue.ToString());
                            }
                            //if (drp_Faculty.SelectedIndex != 0 & drp_Faculty.SelectedIndex != -1)
                            //{
                            //    if (LCID == (int)Language.English)
                            //        objColumns.Add("Faculty;Lookup;Eq;" + drp_Faculty.SelectedItem.Text.ToString());
                            //    else
                            //        objColumns.Add("FacultyAr;Lookup;Eq;" + drp_Faculty.SelectedItem.Text.ToString());
                            //}
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
                                    objColumns.Add("EntityNeedsEquivalency;Lookup;Eq;" + drp_EntityNeedsEquivalency.SelectedItem.Text.ToString());
                                else
                                    objColumns.Add("EntityNeedsEquivalencyAr;Lookup;Eq;" + drp_EntityNeedsEquivalency.SelectedItem.Text.ToString());
                            }
                            if (drp_Country.SelectedIndex != 0 & drp_Country.SelectedIndex != -1)
                            {
                                if (LCID == (int)Language.English)
                                    objColumns.Add("CountryOfStudy;Lookup;Eq;" + drp_Country.SelectedItem.Text.ToString());
                                else
                                    objColumns.Add("CountryOfStudyAr;Lookup;Eq;" + drp_Country.SelectedItem.Text.ToString());
                            }
                            if (drp_AcademicDegree.SelectedIndex != 0 & drp_AcademicDegree.SelectedIndex != -1)
                            {
                                if (LCID == (int)Language.English)
                                    objColumns.Add("AcademicDegree;Lookup;Eq;" + drp_AcademicDegree.SelectedItem.Text.ToString());
                                else
                                    objColumns.Add("AcademicDegreeAr;Lookup;Eq;" + drp_AcademicDegree.SelectedItem.Text.ToString());
                            }
                            List<Entities.SimilarRequest> srchRequests;

                            if (objColumns.Count > 0)
                                srchRequests = BL.SearchSimilarRequests.GetAllRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(objColumns, "And", true) + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();
                            else
                                srchRequests = BL.SearchSimilarRequests.GetAllRequests("<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();

                            srchRequests = srchRequests.Where(x => x.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEClosedByAcceptance || x.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEClosedByRejection).ToList();
                            HelperMethods.BindGridView(grd_SimilarRequests, srchRequests);
                            if (srchRequests.Count > 0)
                            {
                                // SrchControls.Visible = true;
                                lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NoOfRequests", (uint)LCID) + srchRequests.Count;
                                lbl_NoOfRequests.Visible = true;
                                btn_ApprovedAttachement.Visible = true;
                            }
                            else
                            {
                                // SrchControls.Visible = false;
                                lbl_NoOfRequests.Visible = false;
                                btn_ApprovedAttachement.Visible = false;
                            }
                            RePopulateCheckBoxes();
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
                            Logging.GetInstance().Debug("Exiting method SimilarRequests.BindGridOnSearch");
                        }
                    }
                }
            });
        }

        protected void drp_Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Bind University
            List<University> Universitylist = Common.BL.University.GetUniversityByCountryID(int.Parse(drp_Country.SelectedValue));
            if (Universitylist.Count > 0)
            {
                HelperMethods.BindDropDownList(ref drp_University, Universitylist, "ID", "ArabicTitle", "EnglishTitle", LCID);
                drp_University.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                drp_University.Visible = true;
                lbl_University.Visible = true;
            }
        }

        protected void drp_University_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////Bind Faculty
            //SPListItemCollection Facultylist = BusinessHelper.GetLookupData(Utilities.Constants.Faculty);
            //if (Facultylist.Count > 0)
            //{
            //    HelperMethods.BindDropDownList(ref drp_Faculty, Facultylist, "ID", "TitleAr", "Title", LCID);
            //    drp_Faculty.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
            //    drp_Faculty.Visible = true;
            //    lbl_Faculty.Visible = true;
            //}
        }

        protected void drp_Faculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////Bind Specialization
            //SPListItemCollection Specializationlist = BusinessHelper.GetLookupData(Utilities.Constants.Specialization);
            //if (Specializationlist.Count > 0)
            //{
            //    HelperMethods.BindDropDownList(ref drp_Specialization, Specializationlist, "ID", "TitleAr", "Title", LCID);
            //    drp_Specialization.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
            //    drp_Specialization.Visible = true;
            //    lbl_Specialization.Visible = true;
            //}
        }

        protected void btn_ApprovedAttachement_Click(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method SimilarRequests.btn_ApprovedAttachement_Click");
                            if (!IsRefresh)
                            {
                                if (Page.Session["DisplayRequestId"] != null)
                                {
                                    SPList list = web.Lists[Utilities.Constants.Requests];
                                    SPListItem newitem = list.Items.GetItemById(Convert.ToInt32(Page.Session["DisplayRequestId"].ToString()));
                                    SPFieldLookupValueCollection fieldValues = new SPFieldLookupValueCollection();
                                    foreach (GridViewRow row in grd_SimilarRequests.Rows)
                                    {
                                        if (row.RowType == DataControlRowType.DataRow)
                                        {
                                            CheckBox chkRow = (row.FindControl("chkbox_Select") as CheckBox);
                                            if (chkRow.Checked)
                                            {
                                                HiddenField reqID = (row.FindControl("hdn_ID") as HiddenField);
                                                fieldValues.Add(new SPFieldLookupValue(reqID.Value));
                                            }
                                        }
                                    }

                                    foreach (int id in SelectedRequestsIndex)
                                    {
                                        fieldValues.Add(new SPFieldLookupValue(id.ToString()));
                                    }
                                    if (fieldValues.Count > 0)
                                    {
                                        newitem["RelatedRequests"] = fieldValues;
                                        web.AllowUnsafeUpdates = true;
                                        newitem.Update();
                                        list.Update();
                                        lbl_SuccessMsg.Visible = true;
                                        CheckedSimilarRequests();
                                    }
                                    else
                                        lbl_SuccessMsg.Visible = false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            web.AllowUnsafeUpdates = false;
                            Logging.GetInstance().Debug("Exiting method SimilarRequests.btn_ApprovedAttachement_Click");
                        }
                    }
                }
            });
        }

        protected void grd_SimilarRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter SimilarRequests.grd_SimilarRequests_PageIndexChanging");
            try
            {
                foreach (GridViewRow row in grd_SimilarRequests.Rows)
                {
                    var chkBox = row.FindControl("chkbox_Select") as CheckBox;
                    //IDataItemContainer container = (IDataItemContainer)chkBox.NamingContainer;
                    HiddenField reqID = (row.FindControl("hdn_ID") as HiddenField);
                    if (chkBox.Checked)
                    {
                        PersistRowIndex(Convert.ToInt32(reqID.Value));// (container.DataItemIndex);
                    }
                    else
                    {
                        RemoveRowIndex(Convert.ToInt32(reqID.Value));// (container.DataItemIndex);
                    }
                }
                grd_SimilarRequests.PageIndex = e.NewPageIndex;
                BindGridOnSearch();
                CheckedSimilarRequests();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SimilarRequests.grd_SimilarRequests_PageIndexChanging");
            }
        }

        private void PersistRowIndex(int index)
        {
            if (!SelectedRequestsIndex.Exists(i => i == index))
            {
                SelectedRequestsIndex.Add(index);
            }
        }

        private void RemoveRowIndex(int index)
        {
            SelectedRequestsIndex.Remove(index);
        }

        private List<Int32> SelectedRequestsIndex
        {
            get
            {
                if (ViewState["SELECTED_REQUESTS_INDEX"] == null)
                {
                    ViewState["SELECTED_REQUESTS_INDEX"] = new List<Int32>();
                }

                return (List<Int32>)ViewState["SELECTED_REQUESTS_INDEX"];
            }
        }

        private void RePopulateCheckBoxes()
        {
            foreach (GridViewRow row in grd_SimilarRequests.Rows)
            {
                var chkBox = row.FindControl("chkbox_Select") as CheckBox;

                //IDataItemContainer container = (IDataItemContainer)chkBox.NamingContainer;
                HiddenField reqID = (row.FindControl("hdn_ID") as HiddenField);

                if (SelectedRequestsIndex != null)
                {
                    if (SelectedRequestsIndex.Exists(i => i == Convert.ToInt32(reqID.Value)))// container.DataItemIndex))
                    {
                        chkBox.Checked = true;
                    }
                }
            }
        }

        protected void lnk_Request_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter ClarRequestsListing.lnk_View_Click");
            try
            {
                if (!IsRefresh)
                {
                    LinkButton lnkButton = (LinkButton)sender;
                    GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                    HiddenField hdn_ProcdId = (HiddenField)gvr.FindControl("hdn_Id");
                    Page.Session["hdn_SimilarReqID"] = hdn_ProcdId.Value;
                    if (Page.Session["hdn_SimilarReqID"] != null)
                        //Response.Redirect((LCID == 1033 ? "/en" : "/ar") + "/Pages/ReviewerDisplaySimilarRequestDetails.aspx");
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ReviewerDisplaySimilarRequestDetails.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ClarRequestsListing.lnk_View_Click");
            }
        }
    }
}