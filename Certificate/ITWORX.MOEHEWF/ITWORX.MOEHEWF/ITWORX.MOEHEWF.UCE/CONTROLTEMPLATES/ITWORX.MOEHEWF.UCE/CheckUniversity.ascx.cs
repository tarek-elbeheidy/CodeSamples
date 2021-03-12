using ITWORX.MOEHEWF.UCE.Utilities;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHE.Utilities;
using System.Collections.Generic;
using Microsoft.SharePoint;
using ITWORX.MOEHEWF.UCE.Entities;
using ITWORX.MOEHEWF.Common.Entities;
using System.Linq;
using Microsoft.SharePoint.Utilities;
using System.Web;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class CheckUniversity : UserControlBase
    {
        #region Protected Variables
        protected string StudyYear = string.Empty;
        protected string UniversityListNotFoundText = string.Empty;
        protected string Select = string.Empty;
        protected string OtherValue = string.Empty;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlCalcSectionCountry;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlCalcSectionUniversity;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            Logging.GetInstance().Debug("Entering CheckUniversity.Page_Load");
            try
            {
                StudyYear = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "StudyYear");
                OtherValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)LCID);
                UniversityListNotFoundText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "UniversityListNotFound", (uint)LCID);
                Select = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID);
                // ValidateOthers();
                ddlCalcSectionCountry.DropWithNewOption.SelectedIndexChanged += new EventHandler(ddlCalcSectionCountry_SelectedIndexChanged);
                if (!Page.IsPostBack)
                {
                    List<Common.Entities.TrackingRequest> allUCERequests = Common.BL.TrackingRequest.GetAllUCERequestsTracking(string.Empty, DateTime.MinValue, DateTime.MinValue, string.Empty, string.Empty, string.Empty, LCID);
                    if (allUCERequests != null && allUCERequests.Count > 0)
                    {
                        allUCERequests = allUCERequests.Where(x => x.Code == Common.Utilities.RequestStatus.UCESubmitted.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEProgramManagerReview.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEProgramEmployeeReview.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEAsianAndEuropianEmployeeReview.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEHeadManagerReviewRecommendation.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEAsianAndEuropianEmployeeMissingInformation.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCProgramEmployeeClarificationReply.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEProgramEmployeeNeedsClarification.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEProgramManagerReviewRecommendation.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEProgramManagerMissingRecommendationFromHeadManager.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEHeadManagerAccepted.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEHeadManagerRejected.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEProgramEmployeeMissingInformation.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCECulturalMissionNeedsStatement.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCECulturalMissionStatementReply.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEHigherEduInstitutesNeedsStatement.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEHigherEduInstitutesStatementReply.ToString()).ToList();
                        if (allUCERequests != null && allUCERequests.Count > 0)
                        {
                            lblSubmissionMsg.Text = HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "OpenRequestUCE", (uint)LCID);
                            modalPopUpConfirmation.Show();
                        }
                        FillDropdownsWithNewOption();
                        //This will be removed when session is used
                        if (SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage) && Page.Session["EditRequestId"] != null) /*Page.Request.QueryString["reqNo"] != null*/
                        {
                            GetCheckUniversityDataForEditMode();
                        }
                    }

                    if (Request["__EVENTTARGET"] == ddlCalcSectionCountry.DropWithNewOption.UniqueID)
                    {
                        ddlCalcSectionCountry_SelectedIndexChanged(ddlCalcSectionCountry.DropWithNewOption, null);
                    }
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
        public void FillDropdownsWithNewOption()
        {
            Logging.GetInstance().Debug("Enter RequestDetails.FillDropdownsWithNewOption");
            try
            { 
                List<CountryOfStudy> countryItems = Common.BL.CountryOfStudy.GetAllByViewUCE(LCID); 

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
                ddlCalcSectionCountry.ReqErrorAstrik = true;
                ddlCalcSectionCountry.LblText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CountryOfStudy", (uint)LCID);
                // ddlCalcSectionCountry.IsRequired = true;


                ddlCalcSectionUniversity.BindDataSource();
                ddlCalcSectionUniversity.ValidationGroup = "Submit";
                ddlCalcSectionUniversity.ReqErrorAstrik = true;
                ddlCalcSectionUniversity.RequiredDropText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredUniversity", (uint)LCID);
                ddlCalcSectionUniversity.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredNewUniversity", (uint)LCID);
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
 


        private void GetCheckUniversityDataForEditMode()
        {
            Logging.GetInstance().Debug("Entering CheckUniversity.GetCheckUniversityDataForEditMode");
            try
            {
                int requestId = int.Parse(Convert.ToString(Page.Session["EditRequestId"]));
                Entities.Request requestItem = BL.Request.GetRequestByNumber(requestId, LCID);
                
                if (requestItem != null)
                {
                    txtYear.Text = requestItem.Year.ToString();
                    int year = int.Parse(txtYear.Text);
                    if (year >= int.Parse(StudyYear))
                    {
                        if (!string.IsNullOrEmpty(requestItem.CountryOfStudy.SelectedID))
                        {
                            ddlCalcSectionCountry.DropWithNewOption.SelectedValue = requestItem.CountryOfStudy.SelectedID;
                            
                            if (!string.IsNullOrEmpty(requestItem.University.SelectedID))
                            {
                                bindUniversitiesByCountry(requestItem.CountryOfStudy.SelectedID);
                                ddlCalcSectionUniversity.DropWithNewOption.SelectedValue = requestItem.University.SelectedID;
                            }
                            else
                            {
                                ddlCalcSectionUniversity.DropWithNewOption.SelectedValue = "New";
                                ddlCalcSectionUniversity.NewOptionText = requestItem.University.SelectedTitle;
                            }
                        }
                        else
                        {
                            ddlCalcSectionCountry.DropWithNewOption.SelectedValue = "New";
                            bindUniversitiesByCountry("New");
                            ddlCalcSectionUniversity.DropWithNewOption.SelectedValue = "New";
                            
                            ddlCalcSectionCountry.NewOptionText = requestItem.CountryOfStudy.SelectedTitle;
                            ddlCalcSectionUniversity.NewOptionText = requestItem.University.SelectedTitle;
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
        public Entities.Request CreateCheckUniversityDataObject(int applicantId)
        {
            Logging.GetInstance().Debug("Entering CheckUniversity.CreateCheckUniversityDataObject");
            Entities.Request checkUniversity = new Entities.Request();
            try
            { 
                int year = int.Parse(txtYear.Text);
                if (year >= int.Parse(StudyYear) && ddlCalcSectionCountry.DropWithNewOption.SelectedIndex > 0 )
                {
                    checkUniversity.CountryOfStudy = new CountryOfStudy() {
                        SelectedID = ddlCalcSectionCountry.DropWithNewOption.SelectedValue.ToLower() == "new"? string.Empty : ddlCalcSectionCountry.DropWithNewOption.SelectedValue,
                        SelectedTitle = ddlCalcSectionCountry.DropWithNewOption.SelectedValue.ToLower() == "new" ? ddlCalcSectionCountry.NewOptionText : ddlCalcSectionCountry.DropWithNewOption.SelectedItem.Text
                    };

                    checkUniversity.UniversityMainHeadquarter = new UniversityMainCountry()
                    {
                        SelectedID = ddlCalcSectionCountry.DropWithNewOption.SelectedValue.ToLower() == "new" ? string.Empty : ddlCalcSectionCountry.DropWithNewOption.SelectedValue,
                        SelectedTitle = ddlCalcSectionCountry.DropWithNewOption.SelectedValue.ToLower() == "new" ? ddlCalcSectionCountry.NewOptionText : ddlCalcSectionCountry.DropWithNewOption.SelectedItem.Text
                    };
                   
                } 
                else
                {
                    checkUniversity.CountryOfStudy = new CountryOfStudy() { SelectedID = string.Empty, SelectedTitle = string.Empty };
                    checkUniversity.UniversityMainHeadquarter = new UniversityMainCountry() { SelectedID = string.Empty, SelectedTitle = string.Empty };
                   
                }

                if (year >= int.Parse(StudyYear) && ddlCalcSectionUniversity.DropWithNewOption.SelectedIndex > 0 /*&& ddlCalcSectionUniversity.DropWithNewOption.SelectedValue.ToLower() != "new"*/)
                {
                    checkUniversity.University = new University()
                    {
                        SelectedID = ddlCalcSectionUniversity.DropWithNewOption.SelectedValue.ToLower() == "new" ? string.Empty : ddlCalcSectionUniversity.DropWithNewOption.SelectedValue,
                        SelectedTitle = ddlCalcSectionUniversity.DropWithNewOption.SelectedValue.ToLower() == "new" ? ddlCalcSectionUniversity.NewOptionText : ddlCalcSectionUniversity.DropWithNewOption.SelectedItem.Text
                    };
                    checkUniversity.UniversityList =string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "UniversityList", (uint)LCID),year,year); 
                }
                else
                {
                    checkUniversity.CountryOfStudy = new CountryOfStudy() { SelectedID = string.Empty, SelectedTitle = string.Empty };
                    checkUniversity.UniversityMainHeadquarter = new UniversityMainCountry() { SelectedID = string.Empty, SelectedTitle = string.Empty };
                    checkUniversity.University = new University() { SelectedID = string.Empty, SelectedTitle = string.Empty };
                    checkUniversity.UniversityList = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "UniversityListNotFound", (uint)LCID); 
                }

                checkUniversity.ApplicantID = new Applicants() { ID = applicantId };
                checkUniversity.LoginName = SPContext.Current.Web.CurrentUser.LoginName.ToLower();
                checkUniversity.Year = year;
                if (SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage) && Page.Session["EditRequestId"] != null)
                {
                    checkUniversity.ID = int.Parse(Convert.ToString(Page.Session["EditRequestId"]));

                }
                else if (Page.Session["NewRequestId"] != null)
                {
                    checkUniversity.ID = int.Parse(Convert.ToString(Page.Session["NewRequestId"]));
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
        public void SetPanelVisibility ()
        {
            Logging.GetInstance().Debug("Entering CheckUniversity.SetPanelVisibility");
            try
            {
                int yearValue = int.Parse(txtYear.Text);
                if (yearValue < int.Parse(StudyYear))
                {
                    pnlUniversities.Attributes.Add("style", "display:none");
                }
                else
                {
                    pnlUniversities.Attributes.Add("style", "display:block");
                    int selectedCountry = int.Parse(ddlCalcSectionCountry.DropWithNewOption.SelectedValue);
                    //get values and bind to drop and lbl 
                    //Entities.University university=  BL.University.GetUniversityByYearAndCountry(yearValue, selectedCountry);
                }
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
                        //ddlCalcSectionUniversity.DropListItems = null;
                        ddlCalcSectionUniversity.DropListItems = "New";
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

         public void ValidateOthers()
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
