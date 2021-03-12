using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class CheckUniversity : UserControlBase
    {
        #region Protected Variables

        protected string StudyYear = string.Empty;
        protected string UniversityListNotFoundText = string.Empty;
        protected string Select = string.Empty;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlCalcSectionCountry;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlCalcSectionUniversity;
        #endregion Protected Variables

        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering CheckUniversity.Page_Load");
            try
            {
                StudyYear = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "StudyYear");
                UniversityListNotFoundText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "UniversityListNotFound", (uint)LCID);
                Select = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID);
               // ValidateOthers();
                ddlCalcSectionCountry.DropWithNewOption.SelectedIndexChanged += new EventHandler(ddlCalcSectionCountry_SelectedIndexChanged);
                if (!Page.IsPostBack)
                {
                    List<Common.Entities.TrackingRequest> allPARequests = Common.BL.TrackingRequest.GetAllPARequestsTracking(string.Empty, DateTime.MinValue, DateTime.MinValue, string.Empty, string.Empty, string.Empty, LCID);
                    allPARequests = allPARequests.Where(x => x.Code == Common.Utilities.RequestStatus.PASubmitted.ToString()
                    || x.Code == Common.Utilities.RequestStatus.PAEmployeeNeedsClarification.ToString()
                    || x.Code == Common.Utilities.RequestStatus.PAEmployeeClarificationReplay.ToString()
                    || x.Code == Common.Utilities.RequestStatus.PAProgramManagerReviewRecommendation.ToString()
                    || x.Code == Common.Utilities.RequestStatus.PAProgramEmployeeMissingInformation.ToString()
                    || x.Code == Common.Utilities.RequestStatus.PAHeadManagerReviewRecommendation.ToString()
                    || x.Code == Common.Utilities.RequestStatus.PAHeadEmployeeMissingInformation.ToString()
                    || x.Code == Common.Utilities.RequestStatus.PAEmployeeReviewInformation.ToString()
                    || x.Code == Common.Utilities.RequestStatus.PACulturalMissionNeedsStatement.ToString()
                    || x.Code == Common.Utilities.RequestStatus.PACulturalMissionStatementReply.ToString()
                    || x.Code == Common.Utilities.RequestStatus.PAHigherEduInstitutesNeedsStatement.ToString()
                    || x.Code == Common.Utilities.RequestStatus.PAHigherEduInstitutesStatementReply.ToString()
                    || x.Code == Common.Utilities.RequestStatus.PAProgramManagerReject.ToString()
                    || x.Code == Common.Utilities.RequestStatus.PAProgramManagerAcceptance.ToString()).ToList();
                    if (allPARequests != null && allPARequests.Count > 0)
                    {
                        lblSubmissionMsg.Text = HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "OpenRequestPA", (uint)LCID);
                        modalPopUpConfirmation.Show();
                    }
                    FillDropdownsWithNewOption();
                    //This will be removed when session is used
                    if (SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditPARequestPage) && Page.Session["PAEditRequestId"] != null) /*Page.Request.QueryString["reqNo"] != null*/
                    {
                        GetCheckUniversityDataForEditMode();
                    }
                  
                }

                if (Request["__EVENTTARGET"] == ddlCalcSectionCountry.DropWithNewOption.UniqueID)
                {
                    ddlCalcSectionCountry_SelectedIndexChanged(ddlCalcSectionCountry.DropWithNewOption, null);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit CheckUniversity.Page_Load");
            }
        }

        
        public void FillDropdownsWithNewOption()
        {
            Logging.GetInstance().Debug("Enter CheckUniversity.FillDropdownsWithNewOption");
            try
            {

                List<CountryOfStudy> countryItems = Common.BL.CountryOfStudy.GetAllByViewPA(LCID);

                ddlCalcSectionCountry.DropListItems = countryItems;
                ddlCalcSectionCountry.DataValueField = "ID";
                ddlCalcSectionCountry.DataTextEnField = "EnglishTitle";
                ddlCalcSectionCountry.DataTextArField = "ArabicTitle";
                ddlCalcSectionCountry.BindDataSource();
                ddlCalcSectionCountry.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)LCID);
                ddlCalcSectionCountry.RequiredDropText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredState", (uint)LCID);
                ddlCalcSectionCountry.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredNewCountry", (uint)LCID);
                ddlCalcSectionCountry.ValidationGroup = "Submit";
                ddlCalcSectionCountry.DropWithNewOption.AutoPostBack = true;
               // ddlCalcSectionCountry.IsRequired = true;
                ddlCalcSectionCountry.ReqErrorAstrik = true;
                ddlCalcSectionCountry.LblText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CountryOfStudy", (uint)LCID);
                
                ddlCalcSectionUniversity.BindDataSource();
                ddlCalcSectionUniversity.ValidationGroup = "Submit";
                ddlCalcSectionUniversity.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)LCID);
               // ddlCalcSectionUniversity.IsRequired = true;
                ddlCalcSectionUniversity.RequiredDropText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredUniversity", (uint)LCID);
                ddlCalcSectionUniversity.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredNewUniversity", (uint)LCID);
                ddlCalcSectionUniversity.ReqErrorAstrik = true;
                ddlCalcSectionUniversity.LblText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "University", (uint)LCID);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit CheckUniversity.FillDropdownsWithNewOption");
            }
        }

        protected void btnModalOK_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method CheckUniversity.btnModalOK_Click");

            try
            {

                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/TrackRequest.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method CheckUniversity.btnModalOK_Click");
            }
        }
        private void GetCheckUniversityDataForEditMode()
        {
            Logging.GetInstance().Debug("Entering CheckUniversity.GetCheckUniversityDataForEditMode");
            try
            {
                int requestId = int.Parse(Convert.ToString(Page.Session["PAEditRequestId"]));
                Entities.PARequest requestItem = BL.Request.GetRequestByNumber(requestId, LCID);

                if (requestItem != null)
                {
                    txtYear.Text = requestItem.Year.ToString();
                    int year = int.Parse(txtYear.Text);
                    if (year >= int.Parse(StudyYear))
                    {
                        if (!string.IsNullOrEmpty(requestItem.ProgramCountry.SelectedID))
                        {
                            ddlCalcSectionCountry.DropWithNewOption.SelectedValue = requestItem.ProgramCountry.SelectedID;

                            if (!string.IsNullOrEmpty(requestItem.ProgramUniversity.SelectedID))
                            {
                                bindUniversitiesByCountry(requestItem.ProgramCountry.SelectedID);
                                ddlCalcSectionUniversity.DropWithNewOption.SelectedValue = requestItem.ProgramUniversity.SelectedID;
                            }
                            else
                            {
                                ddlCalcSectionUniversity.DropWithNewOption.SelectedValue = "New";
                                ddlCalcSectionUniversity.NewOptionText = requestItem.ProgramUniversity.SelectedTitle;
                            }
                        }
                        else
                        {



                            ddlCalcSectionCountry.DropWithNewOption.SelectedValue = "New";
                            bindUniversitiesByCountry("New");
                            ddlCalcSectionUniversity.DropWithNewOption.SelectedValue = "New";

                            ddlCalcSectionCountry.NewOptionText = requestItem.ProgramCountry.SelectedTitle;
                            ddlCalcSectionUniversity.NewOptionText = requestItem.ProgramUniversity.SelectedTitle;
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
                Logging.GetInstance().Debug("Exit CheckUniversity.GetCheckUniversityDataForEditMode");
            }
        }

        public Entities.PARequest CreateCheckUniversityDataObject(int applicantId)
        {
            Logging.GetInstance().Debug("Entering CheckUniversity.CreateCheckUniversityDataObject");
            Entities.PARequest checkUniversity = new Entities.PARequest();
            try
            {
                int year = int.Parse(txtYear.Text);
                if (year >= int.Parse(StudyYear) && ddlCalcSectionCountry.DropWithNewOption.SelectedIndex > 0)
                {
                    checkUniversity.ProgramCountry = new CountryOfStudy()
                    {
                        SelectedID = ddlCalcSectionCountry.DropWithNewOption.SelectedValue.ToLower() == "new" ? string.Empty : ddlCalcSectionCountry.DropWithNewOption.SelectedValue,
                        SelectedTitle = ddlCalcSectionCountry.DropWithNewOption.SelectedValue.ToLower() == "new" ? ddlCalcSectionCountry.NewOptionText : ddlCalcSectionCountry.DropWithNewOption.SelectedItem.Text
                    };
                    
                }
                else
                {
                    checkUniversity.ProgramCountry = new CountryOfStudy() { SelectedID = string.Empty, SelectedTitle = string.Empty };
                }

                if (year >= int.Parse(StudyYear) && ddlCalcSectionUniversity.DropWithNewOption.SelectedIndex > 0)
                {
                    checkUniversity.ProgramUniversity = new University()
                    {
                        SelectedID = ddlCalcSectionUniversity.DropWithNewOption.SelectedValue.ToLower() == "new" ? string.Empty : ddlCalcSectionUniversity.DropWithNewOption.SelectedValue,
                        SelectedTitle = ddlCalcSectionUniversity.DropWithNewOption.SelectedValue.ToLower() == "new" ? ddlCalcSectionUniversity.NewOptionText : ddlCalcSectionUniversity.DropWithNewOption.SelectedItem.Text
                    };
                    checkUniversity.UniversityList = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "UniversityList", (uint)LCID), year, year);
                }
                else
                {
                    checkUniversity.ProgramCountry = new CountryOfStudy() { SelectedID = string.Empty, SelectedTitle = string.Empty };
                    
                    checkUniversity.ProgramUniversity = new University() { SelectedID = string.Empty, SelectedTitle = string.Empty };
                    checkUniversity.UniversityList = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "UniversityListNotFound", (uint)LCID);
                }

                checkUniversity.ApplicantID = new Applicants() { ID = applicantId };
                checkUniversity.LoginName = SPContext.Current.Web.CurrentUser.LoginName.ToLower();
                checkUniversity.Year = int.Parse(txtYear.Text);
                if (SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditPARequestPage) && Page.Session["PAEditRequestId"] != null)
                {
                    checkUniversity.ID = int.Parse(Convert.ToString(Page.Session["PAEditRequestId"]));
                }
                else if (Page.Session["PANewRequestId"] != null)
                {
                    checkUniversity.ID = int.Parse(Convert.ToString(Page.Session["PANewRequestId"]));
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit CheckUniversity.CreateCheckUniversityDataObject");
            }
            return checkUniversity;
        }

        public void SetPanelVisibility()
        {
            Logging.GetInstance().Debug("Entering CheckUniversity.SetPanelVisibility");
            try
            {
                int yearValue = int.Parse(txtYear.Text);
                //if (yearValue < int.Parse(StudyYear))
                //{
                //    pnlUniversities.Attributes.Add("style", "display:none");
                //}
                //else
                //{
                pnlUniversities.Attributes.Add("style", "display:block");
                int selectedCountry = int.Parse(ddlCalcSectionCountry.DropWithNewOption.SelectedValue);
                //get values and bind to drop and lbl
                //Entities.University university=  BL.University.GetUniversityByYearAndCountry(yearValue, selectedCountry);

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit CheckUniversity.SetPanelVisibility");
            }
        }

        public void ddlCalcSectionCountry_SelectedIndexChanged(Object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter RequestDetails.ddlCalcSectionCountry_SelectedIndexChanged");
            try
            {
                DropDownList dropDownList = sender as DropDownList;

                bindUniversitiesByCountry(dropDownList.SelectedValue);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit RequestDetails.ddlCalcSectionCountry_SelectedIndexChanged");
            }
        }

        private void bindUniversitiesByCountry(string SelectedValue)
        {

            Logging.GetInstance().Debug("Enter RequestDetails.getUniversitybyCountry");
            try
            {
                if (string.IsNullOrEmpty(SelectedValue) || SelectedValue == "New")
                {
                    ddlCalcSectionUniversity.DropWithNewOption.Items.Clear();
                    if (string.IsNullOrEmpty(SelectedValue))
                    {
                        ddlCalcSectionUniversity.DropListItems = null;
                    }

                    else
                    {
                        ddlCalcSectionUniversity.DropListItems = "New";
                    }
                    ddlCalcSectionUniversity.BindDataSource();

                }
                else
                {
                    //changed in order to run the cycle currently but it must be without university list
                    List<University> university = Common.BL.University.GetDistinctUniversityByYearAndCountry(int.Parse(txtYear.Text), int.Parse(SelectedValue), LCID);//Common.BL.UniversityLists.GetUniversityWithEmptyUniversityListsAndCountryId(int.Parse(dropDownList.SelectedValue));
                    ddlCalcSectionUniversity.DropWithNewOption.Items.Clear();
                    if (university != null && university.Count > 0)
                    {

                        ddlCalcSectionUniversity.DropListItems = university;
                        ddlCalcSectionUniversity.DataValueField = "ID";
                        ddlCalcSectionUniversity.DataTextEnField = "EnglishTitle";
                        ddlCalcSectionUniversity.DataTextArField = "ArabicTitle";
                        ddlCalcSectionUniversity.BindDataSource();
                        ddlCalcSectionUniversity.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)LCID);
                        ddlCalcSectionUniversity.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredState", (uint)LCID);

                    }
                    else
                    {
                        ddlCalcSectionUniversity.DropListItems = "New";
                        //ddlCalcSectionUniversity.DropListItems = null;
                        ddlCalcSectionUniversity.BindDataSource();
                    }

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit RequestDetails.getUniversitybyCountry");
            }
        }


        public void ValidateOthersUni()
        {
            Logging.GetInstance().Debug("Enter RequestDetails.ValidateOthers");
            // bool notValid = true;
            try
            {
                int yearValue = int.Parse(txtYear.Text);
                if (yearValue >= int.Parse(StudyYear))

                {

                    if (ddlCalcSectionCountry.SelectedValue == "New")
                    {
                        ddlCalcSectionCountry.ReqNewOptionText.Enabled = true;
                        ddlCalcSectionCountry.ReqDropWithNewOption.Enabled = false;

                    }
                    else
                    {
                        ddlCalcSectionCountry.ReqNewOptionText.Enabled = false;
                        ddlCalcSectionCountry.ReqDropWithNewOption.Enabled = true;

                    }
                    if (ddlCalcSectionUniversity.SelectedValue == "New")
                    {
                        ddlCalcSectionUniversity.ReqNewOptionText.Enabled = true;
                        ddlCalcSectionUniversity.ReqDropWithNewOption.Enabled = false;

                    }
                    else
                    {
                        ddlCalcSectionUniversity.ReqNewOptionText.Enabled = false;
                        ddlCalcSectionUniversity.ReqDropWithNewOption.Enabled = true;

                    }

                }
                else
                {
                    ddlCalcSectionUniversity.ReqNewOptionText.Enabled = false;
                    ddlCalcSectionUniversity.ReqDropWithNewOption.Enabled = false;
                    ddlCalcSectionCountry.ReqNewOptionText.Enabled = false;
                    ddlCalcSectionCountry.ReqDropWithNewOption.Enabled = false;
                }
                ddlCalcSectionCountry.ReqNewOptionText.Validate();
                ddlCalcSectionUniversity.ReqNewOptionText.Validate();
                ddlCalcSectionUniversity.ReqDropWithNewOption.Validate();
                ddlCalcSectionCountry.ReqDropWithNewOption.Validate();
                //if (!ddlCalcSectionCountry.ReqNewOptionText.IsValid || !ddlCalcSectionUniversity.ReqNewOptionText.IsValid
                //    || !ddlCalcSectionUniversity.ReqDropWithNewOption.IsValid || !ddlCalcSectionCountry.ReqDropWithNewOption.IsValid)
                //{
                //    notValid=false;
                //}

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit RequestDetails.ValidateOthers");
            }
            //ddlCalcSectionCountry.ReqNewOptionText.Enabled = false;
            //ddlCalcSectionUniversity.ReqNewOptionText.Enabled = false;

            //  return notValid;
        }
        internal void ValidateOthers()
        {
            if (ddlCalcSectionUniversity.SelectedValue.ToLower() == "new")
            {
                ddlCalcSectionUniversity.ReqNewOptionText.Enabled = true;
            }
            if (ddlCalcSectionCountry.SelectedValue.ToLower() == "new")
            {
                ddlCalcSectionCountry.ReqNewOptionText.Enabled = true;
            }
        }

        protected void txtYear_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(txtYear.Text) >= int.Parse(StudyYear))
            {
                ddlCalcSectionCountry.NewItemText = string.Empty;
                ddlCalcSectionCountry.DropWithNewOption.ClearSelection();
             

                ddlCalcSectionUniversity.NewItemText = string.Empty;
                ddlCalcSectionUniversity.DropWithNewOption.ClearSelection();
               
            }

        }
    }
}