using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.PA.Entities;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using commonBL = ITWORX.MOEHEWF.Common.BL;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class PARequestDetails : UserControlBase
    {
        private  int rowIndex = 0;

        #region ProtectedVariables

        protected int StudyYear = 0;
        protected string yesValue = "1";
        protected string noValue = "0";
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlUniversity;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlSpecialization;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlProgramSpecialization;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadWorking;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload schoolDocuments;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload universityDocuments;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadNotWorking;

        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCalcDetails;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadGrades;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadAcceptedHours;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlCalcSectionCountry;

        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlCalcSectionUniversity;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlEntityWorkingFor;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload FUADegreeList;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload FUAcceptedHoursList;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadDelegates;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload FUDequivalentHours;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadAdmissionLetter;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadNationalService;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadStudyPlan;

        #endregion ProtectedVariables

        public bool HideResubmit { set; get; }

        private long personID { get { return commonBL.Applicants.GetApplicantQatarIDfromADByLoginName(SPContext.Current.Web.CurrentUser.LoginName); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering PARequestDetails.Page_Load");
            try
            {
                rowIndex = 0;

                StudyYear = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "StudyYear"));
                ddlUniversity.DropWithNewOption.SelectedIndexChanged += new EventHandler(ddlUniversity_SelectedIndexChanged);
                ddlCalcSectionCountry.DropWithNewOption.SelectedIndexChanged += new EventHandler(ddlCalcSectionCountry_SelectedIndexChanged);
                ddlCalcSectionUniversity.DropWithNewOption.SelectedIndexChanged += new EventHandler(ddlCalcSectionUniversity_SelectedIndexChanged);

                if (!Page.IsPostBack)
                {
                    ddlCertificateYears.DataSource = getCertificateYears();
                    ddlCertificateYears.DataBind();
                    ddlCertificateYears.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));

                    if (HideResubmit == true)
                    {
                        btnResubmit.Visible = false;
                    }
                    List<DelegationTemplate> delegationTemplates = BL.DelegationTemplate.GetDelegationTemplatesData(LCID);
                    if (delegationTemplates != null && delegationTemplates.Count != 0)
                    {
                        repDelegatesTemplates.DataSource = delegationTemplates;
                        repDelegatesTemplates.DataBind();
                    }
                    PopulateDropdowns();
                    FillDropdownsWithNewOption();

                    rdbWorkingOrNot.Items.Add(new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Yes", (uint)LCID), yesValue));
                    rdbWorkingOrNot.Items.Add(new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "No", (uint)LCID), noValue));
                    //rdbJoinedOtherUni.Items.Add(new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Yes", (uint)LCID), yesValue));
                    //rdbJoinedOtherUni.Items.Add(new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "No", (uint)LCID), noValue));

                    ddlHighestCertificate_SelectedIndexChanged(null, null);

                    //This will be removed when session is used
                    if ((SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditPARequestPage) || SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage.ToLower()))
                        && Page.Session["PAEditRequestId"] != null)
                    {
                        if (SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage.ToLower()))
                        {
                            chkConfirmationBox.Visible = false;
                        }
                        BindRequestDataForEditMode();
                        string secondarySchool = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "SecondarySchool", (uint)LCID).ToLower();
                        bool isSecondarySchoolCertified = (ddlHighestCertificate.SelectedItem.Text.ToLower() == secondarySchool);
                        if (isSecondarySchoolCertified)
                            Session["SecondarySchool"] = "1";
                    }
                    else
                    {
                        rdbWorkingOrNot.Items[0].Selected = true;
                        rdbWorkingOrNot.SelectedIndex = 0;
                        rdbWorkingOrNot_SelectedIndexChanged(null, null);

                        //rdbJoinedOtherUni.Items[1].Selected = true;
                        //rdbJoinedOtherUni.SelectedIndex = 1;
                        //rdbJoinedOtherUni_SelectedIndexChanged(null, null);
                    }
                    ddlUniversity_SelectedIndexChanged(sender, e);
                    ddlHighestCertificate_SelectedIndexChanged(sender, e);
                    ddlWantedCertificateDegree_SelectedIndexChanged(sender, e);
                }
                GetUniversityData();
                BindAttachments();

                rdbWorkingOrNot_SelectedIndexChanged(sender, e);
                //rdbJoinedOtherUni_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PARequestDetails.Page_Load");
            }
        }

        #region RetrieveData

        //To get the data of country, university and university list
        // in case year is >2015
        private void GetUniversityData()
        {
            Logging.GetInstance().Debug("Entering RequestDetails.GetUniversityData");
            Entities.PARequest requestItem = null;
            try
            {
                if ((Page.Session["PANewRequestId"] != null || Page.Session["PAEditRequestId"] != null))
                {
                    int requestId = 0;
                    if (Page.Session["PANewRequestId"] != null)
                    {
                        requestId = int.Parse(Convert.ToString(Page.Session["PANewRequestId"]));
                    }
                    else if (Page.Session["PAEditRequestId"] != null)
                    {
                        requestId = int.Parse(Convert.ToString(Page.Session["PAEditRequestId"]));
                    }

                    requestItem = BL.Request.GetUniversityRequestData(requestId, LCID);
                    ViewState["Year"] = requestItem.Year;
                    if (requestItem != null)
                    {
                        lblRequestNumberValue.Text = requestItem.RequestNumber;
                        lblRequestCreationDateValue.Text = requestItem.RequestCreationDate != DateTime.MinValue ? requestItem.RequestCreationDate.ToShortDateString() : string.Empty;

                        txtCountriesValue.Text = requestItem.ProgramCountry.SelectedTitle;
                        txtUniversityValue.Text = requestItem.ProgramUniversity.SelectedTitle;
                        hdnUniversityID.Value = !string.IsNullOrEmpty(requestItem.ProgramUniversity.SelectedID) ? requestItem.ProgramUniversity.SelectedID : "New";
                        BindStudySystem();
                    }

                    ViewState["UniversityLoaded"] = true;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit RequestDetails.GetUniversityData");
            }
        }

        private void BindRequestDataForEditMode()
        {
            Logging.GetInstance().Debug("Entering PARequestDetails.GetRequestDataForEditMode");
            try
            {
                int requestId = int.Parse(Convert.ToString(Page.Session["PAEditRequestId"]));
                Entities.PARequest requestItem = BL.Request.GetRequestByNumber(requestId, LCID);
                if (requestItem != null)
                {
                    lblRequestNumberValue.Text = requestItem.RequestNumber.ToString();

                    lblRequestCreationDateValue.Text = requestItem.RequestCreationDate != DateTime.MinValue ? requestItem.RequestCreationDate.ToShortDateString() : string.Empty;

                    if (requestItem.HighestCertificate != null && !string.IsNullOrEmpty(requestItem.HighestCertificate.SelectedID))
                    {
                        ddlHighestCertificate.SelectedValue = requestItem.HighestCertificate.SelectedID;
                    }

                    dropCountries.Visible = true;
                    reqCountries.Enabled = true;
                    ddlUniversityDiv.Visible = true;
                    if (requestItem.CountryOfStudy != null && !string.IsNullOrEmpty(requestItem.CountryOfStudy.SelectedID))
                    {
                        dropCountries.SelectedValue = requestItem.CountryOfStudy.SelectedID;
                        List<University> university = Common.BL.University.GetDistinctUniversityByYearAndCountry(0, int.Parse(requestItem.CountryOfStudy.SelectedID), this.LCID);
                        ddlUniversity.DropWithNewOption.Items.Clear();
                        ddlUniversity.DropListItems = university;
                        ddlUniversity.DataValueField = "ID";
                        ddlUniversity.DataTextEnField = "EnglishTitle";
                        ddlUniversity.DataTextArField = "ArabicTitle";
                        ddlUniversity.BindDataSource();
                        if (requestItem.University != null)
                        {
                            ddlUniversity.SelectedValue = requestItem.University.SelectedID;
                        }
                        else if (!string.IsNullOrEmpty(requestItem.UniversityNotFoundInList))
                        {
                            ddlUniversity.SelectedValue = "New";
                            ddlUniversity.NewOptionText = requestItem.UniversityNotFoundInList;
                        }
                    }
                    if (requestItem.ProgramType != null && !string.IsNullOrEmpty(requestItem.ProgramType.SelectedID))
                    {
                        ddlWantedCertificateDegree.SelectedValue = requestItem.ProgramType.SelectedID;
                        BindStudySystem();
                    }
                    if (!string.IsNullOrEmpty(requestItem.ProgramFaculty))
                    {
                        ddlCalcSectionFaculty.Text = requestItem.ProgramFaculty;
                    }
                    if (!string.IsNullOrEmpty(requestItem.UniversityList))
                    {
                        txtUniversityListValue.Text = requestItem.UniversityList;
                    }
                    if (requestItem.ProgramStudySystem != null && !string.IsNullOrEmpty(requestItem.ProgramStudySystem.SelectedID))
                    {
                        dropCalcSectionStudyingSystem.SelectedValue = requestItem.ProgramStudySystem.SelectedID;
                    }

                    if (!string.IsNullOrEmpty(requestItem.SpecializationNotFoundInList))
                    {
                        List<Specialization> specialization = BL.Specialization.GetAll();
                        ddlSpecialization.DropWithNewOption.Items.Clear();
                        ddlSpecialization.DropListItems = specialization;
                        ddlSpecialization.BindDataSource();
                        ddlSpecialization.SelectedValue = "New";
                        ddlSpecialization.NewOptionText = requestItem.SpecializationNotFoundInList;
                    }
                    else
                    {
                        List<Specialization> specialization = BL.Specialization.GetAll();
                        ddlSpecialization.DropWithNewOption.Items.Clear();
                        ddlSpecialization.DropListItems = specialization;
                        ddlSpecialization.DataValueField = "ID";
                        ddlSpecialization.DataTextEnField = "EnglishTitle";
                        ddlSpecialization.DataTextArField = "ArabicTitle";
                        ddlSpecialization.BindDataSource();
                        if (requestItem.Specialization != null)
                        {
                            ddlSpecialization.SelectedValue = requestItem.Specialization.SelectedID;
                        }
                    }

                    if (!string.IsNullOrEmpty(requestItem.ProgramSpecializationNotFoundInList))
                    {
                        List<Specialization> programSpecialization = BL.Specialization.GetAll();
                        ddlProgramSpecialization.DropWithNewOption.Items.Clear();
                        ddlProgramSpecialization.DropListItems = programSpecialization;
                        ddlProgramSpecialization.BindDataSource();
                        ddlProgramSpecialization.SelectedValue = "New";
                        ddlProgramSpecialization.NewOptionText = requestItem.ProgramSpecializationNotFoundInList;
                    }
                    else
                    {
                        List<Specialization> programSpecialization = BL.Specialization.GetAll();
                        ddlProgramSpecialization.DropWithNewOption.Items.Clear();
                        ddlProgramSpecialization.DropListItems = programSpecialization;
                        ddlProgramSpecialization.DataValueField = "ID";
                        ddlProgramSpecialization.DataTextEnField = "EnglishTitle";
                        ddlProgramSpecialization.DataTextArField = "ArabicTitle";
                        ddlProgramSpecialization.BindDataSource();
                        if (requestItem.ProgramSpecialization != null)
                        {
                            ddlProgramSpecialization.SelectedValue = requestItem.ProgramSpecialization.SelectedID;
                        }
                    }

                    string secondarySchool = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "SecondarySchool", (uint)LCID).ToLower();
                    bool isSecondarySchoolCertified = (ddlHighestCertificate.SelectedItem.Text.ToLower() == secondarySchool);

                    if (requestItem.AcademicStartDate != DateTime.MinValue)
                    {
                        dtAcademicStartDate.Value = ExtensionMethods.QatarFormatedDate(requestItem.AcademicStartDate);
                    }
                    if (requestItem.AcademicEndDate != DateTime.MinValue)
                    {
                        dtAcademicEndDate.Value = ExtensionMethods.QatarFormatedDate(requestItem.AcademicEndDate);
                    }
                    if (isSecondarySchoolCertified)
                    {
                        ddlCertificateYears.Visible = true;
                        txtCertificateDate.Visible = false;
                        txtCertificateDate.Value = string.Empty;
                        reqCertificateDate.ControlToValidate = "ddlCertificateYears";

                        ddlCertificateYears.ClearSelection();
                        ddlCertificateYears.Items.FindByText(requestItem.CertificateDate).Selected = true;
                    }
                    else
                    {
                        ddlCertificateYears.Visible = false;
                        txtCertificateDate.Visible = true;
                        reqCertificateDate.ControlToValidate = "txtCertificateDate";
                        txtCertificateDate.Value = requestItem.CertificateDate;
                    }
                    if (!string.IsNullOrEmpty(requestItem.Faculty))
                    {
                        ddlFaculty.Text = requestItem.Faculty;
                    }

                    if (requestItem.AcademicNumberOfYears > 0)
                    {
                        txtCalcSectionAcademicNumberOfYears.Text = requestItem.AcademicNumberOfYears.ToString();
                    }
                    //rdbJoinedOtherUni.Items.FindByValue(requestItem.JoinedOtherUniversities ? yesValue : noValue).Selected = true;
                    rdbWorkingOrNot.Items.FindByValue(requestItem.WorkingOrNot ? yesValue : noValue).Selected = true;

                    if (requestItem.WorkingOrNot == true)
                    {
                        //if (requestItem.EntityWorkingFor != null)
                        //{
                        //    ddlEntityWorkingFor.SelectedValue = requestItem.EntityWorkingFor.SelectedID;
                        //}
                        //else if (!string.IsNullOrEmpty(requestItem.OtherEntityWorkingFor))
                        //{
                        //    ddlEntityWorkingFor.SelectedValue = "New";
                        //}
                        if (!string.IsNullOrEmpty(requestItem.OtherEntityWorkingFor))
                        {
                            List<EntityWorkingFor> entityWorkingFor = BL.EntityWorkingFor.GetAll();
                            ddlEntityWorkingFor.DropWithNewOption.Items.Clear();
                            ddlEntityWorkingFor.DropListItems = entityWorkingFor;
                            ddlEntityWorkingFor.BindDataSource();
                            ddlEntityWorkingFor.SelectedValue = "New";
                            ddlEntityWorkingFor.NewOptionText = requestItem.OtherEntityWorkingFor;
                        }
                        else
                        {
                            List<EntityWorkingFor> entityWorkingFor = BL.EntityWorkingFor.GetAll();
                            ddlEntityWorkingFor.DropWithNewOption.Items.Clear();
                            ddlEntityWorkingFor.DropListItems = entityWorkingFor;
                            ddlEntityWorkingFor.DataValueField = "ID";
                            ddlEntityWorkingFor.DataTextEnField = "EnglishTitle";
                            ddlEntityWorkingFor.DataTextArField = "ArabicTitle";
                            ddlEntityWorkingFor.BindDataSource();
                            if (requestItem.EntityWorkingFor != null)
                            {
                                ddlEntityWorkingFor.SelectedValue = requestItem.EntityWorkingFor.SelectedID;
                            }
                        }
                    }
                    List<CalculatedDetailsForCertificate> calculatedDetailsData = BL.CalculatedDetailsForCertificate.GetCalculatedDetailsForCertificate(requestId, LCID);
                    if (calculatedDetailsData != null && calculatedDetailsData.Count > 0)
                    {
                        repCalculatedDetailsForCertificate.DataSource = calculatedDetailsData;
                        repCalculatedDetailsForCertificate.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PARequestDetails.GetRequestDataForEditMod");
            }
        }

        #endregion RetrieveData

        #region Drop

        public void FillDropdownsWithNewOption()
        {
            Logging.GetInstance().Debug("Enter PARequestDetails.FillDropdownsWithNewOption");
            try
            {
                List<CountryOfStudy> countryItems = Common.BL.CountryOfStudy.GetAll(this.LCID);

                ddlUniversity.DropListItems = null;
                ddlUniversity.BindDataSource();
                ddlUniversity.LblText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "University", (uint)LCID);
                ddlUniversity.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Other", (uint)LCID);
                ddlUniversity.RequiredDropText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "RequiredUniversity", (uint)LCID);
                ddlUniversity.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "RequiredNewUniversity", (uint)LCID);
                ddlUniversity.ValidationGroup = "Submit";
                ddlUniversity.ReqErrorAstrik = true;
                ddlUniversity.DropWithNewOption.AutoPostBack = true;

                ddlSpecialization.DropListItems = BL.Specialization.GetAll();
                ddlSpecialization.DataValueField = "ID";
                ddlSpecialization.DataTextEnField = "EnglishTitle";
                ddlSpecialization.DataTextArField = "ArabicTitle";
                ddlSpecialization.BindDataSource();
                ddlSpecialization.LblText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Specialization", (uint)LCID);
                ddlSpecialization.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Other", (uint)LCID);
                ddlSpecialization.RequiredDropText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "RequiredSpecialization", (uint)LCID);
                ddlSpecialization.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "RequiredNewSpecialization", (uint)LCID);
                ddlSpecialization.ValidationGroup = "Submit";
                ddlSpecialization.ReqErrorAstrik = true;

                ddlProgramSpecialization.DropListItems = BL.Specialization.GetAll();
                ddlProgramSpecialization.DataValueField = "ID";
                ddlProgramSpecialization.DataTextEnField = "EnglishTitle";
                ddlProgramSpecialization.DataTextArField = "ArabicTitle";
                ddlProgramSpecialization.BindDataSource();
                ddlProgramSpecialization.LblText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Specialization", (uint)LCID);
                ddlProgramSpecialization.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Other", (uint)LCID);
                ddlProgramSpecialization.RequiredDropText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "RequiredProgramSpecialization", (uint)LCID);
                ddlProgramSpecialization.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "RequiredProgramNewSpecialization", (uint)LCID);
                ddlProgramSpecialization.ValidationGroup = "Submit";
                ddlProgramSpecialization.ReqErrorAstrik = true;

                ddlEntityWorkingFor.DropListItems = BL.EntityWorkingFor.GetAll();
                ddlEntityWorkingFor.DataValueField = "ID";
                ddlEntityWorkingFor.DataTextEnField = "EnglishTitle";
                ddlEntityWorkingFor.DataTextArField = "ArabicTitle";
                ddlEntityWorkingFor.BindDataSource();
                ddlEntityWorkingFor.LblText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "EntityWorkingFor", (uint)LCID);
                ddlEntityWorkingFor.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Other", (uint)LCID);
                ddlEntityWorkingFor.RequiredDropText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "RequiredEntityWorkingFor", (uint)LCID);
                ddlEntityWorkingFor.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "RequiredNewEntityWorkingFor", (uint)LCID);
                ddlEntityWorkingFor.ValidationGroup = "Submit";
                ddlEntityWorkingFor.ReqErrorAstrik = true;

                ddlCalcSectionCountry.DropListItems = countryItems;
                ddlCalcSectionCountry.DataValueField = "ID";
                ddlCalcSectionCountry.DataTextEnField = "EnglishTitle";
                ddlCalcSectionCountry.DataTextArField = "ArabicTitle";
                ddlCalcSectionCountry.BindDataSource();
                ddlCalcSectionCountry.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)LCID);
                ddlCalcSectionCountry.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredNewCountry", (uint)LCID);
                ddlCalcSectionCountry.ValidationGroup = "AddCalc";
                ddlCalcSectionCountry.DropWithNewOption.AutoPostBack = true;

                ddlCalcSectionUniversity.DropListItems = null;
                ddlCalcSectionUniversity.BindDataSource();
                ddlCalcSectionUniversity.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)LCID);
                ddlCalcSectionUniversity.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredNewUniversity", (uint)LCID);
                ddlCalcSectionUniversity.ValidationGroup = "AddCalc";
                ddlCalcSectionUniversity.DropWithNewOption.AutoPostBack = true;
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PARequestDetails.FillDropdownsWithNewOption");
            }
        }

        private void PopulateDropdowns()
        {
            Logging.GetInstance().Debug("Enter PARequestDetails.PopulateDropdowns");
            try
            {
                List<Entities.AcademicDegree> academicDegreeItems = BL.AcademicDegree.GetAll();
                List<CountryOfStudy> countryItems = Common.BL.CountryOfStudy.GetAll(this.LCID);
                List<StudyLanguage> studyLanguageItems = BL.StudyLanguage.GetAll();
                List<StudyType> studyTypeItems = BL.StudyType.GetAll();
                List<StudySystem> studySystemItems = BL.StudySystem.GetAll();
                List<Schools> schoolsItems = BL.Schools.GetAll();

                List<Certificates> certificates = BL.Certificates.GetAll();
                List<StudyAttachmentTypes> studyAttachTypes = BL.StudyAttachmentTypes.GetAll();

                ddlHighestCertificate.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), string.Empty));
                if (certificates != null && certificates.Count > 0)
                {
                    ddlHighestCertificate.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref ddlHighestCertificate, certificates, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }

                ddlWantedCertificateDegree.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), string.Empty));
                if (academicDegreeItems != null && academicDegreeItems.Count > 0)
                {
                    ddlWantedCertificateDegree.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref ddlWantedCertificateDegree, academicDegreeItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }

                dropCountries.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), string.Empty));
                if (countryItems != null && countryItems.Count > 0)
                {
                    dropCountries.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref dropCountries, countryItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }

                dropStudyingSystem.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), string.Empty));
                //if (studySystemItems != null && studySystemItems.Count > 0)
                //{
                //        dropStudyingSystem.AppendDataBoundItems = true;
                //    HelperMethods.BindDropDownList(ref dropStudyingSystem, studySystemItems, "ID", "ArabicTitle", "EnglishTitle", LCID);

                //}

                dropCalcSectionStudyingSystem.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), string.Empty));
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PARequestDetails.PopulateDropdowns");
            }
        }

        #endregion Drop

        protected void lnkDownloadCopy_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PARequestDetails.lnkDownloadCopy_Click");
            try
            {
                int itemId = int.Parse((sender as LinkButton).CommandArgument);
                HelperMethods.DisplayFile(SPContext.Current.Site.Url, itemId, Utilities.Constants.PARequestsAttachments, "", true);
                // BusinessHelper.DownloadFile(itemId, Utilities.Constants.OrganizationalLettersAttachments);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PARequestDetails.lnkDownloadCopy_Click");
            }
        }

        #region SavingData

        public Entities.PARequest CreateSubmittedRequestDataObject()
        {
            Logging.GetInstance().Debug("Entering PARequestDetails.CreateSubmittedRequestDataObject");

            if (!string.IsNullOrEmpty(ddlHighestCertificate.SelectedItem.Text))
            {
                string secondarySchool = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "SecondarySchool", (uint)LCID).ToLower();
                bool isSecondarySchoolCertified = (ddlHighestCertificate.SelectedItem.Text.ToLower() == secondarySchool);
                //For Readability
                bool isUniversity = !isSecondarySchoolCertified;

                if (isUniversity)
                {
                    if (ddlUniversity.SelectedValue == "New")
                    {
                        ddlUniversity.ReqNewOptionText.Enabled = true;
                        ddlUniversity.ReqDropWithNewOption.Enabled = false;
                    }
                    else if (string.IsNullOrEmpty(ddlUniversity.SelectedValue))
                    {
                        ddlUniversity.ReqDropWithNewOption.Enabled = true;
                        ddlUniversity.ReqNewOptionText.Enabled = false;
                    }

                    if (ddlSpecialization.SelectedValue == "New")
                    {
                        ddlSpecialization.ReqNewOptionText.Enabled = true;
                        ddlSpecialization.ReqDropWithNewOption.Enabled = false;
                    }
                    else if (string.IsNullOrEmpty(ddlSpecialization.SelectedValue))
                    {
                        ddlSpecialization.ReqDropWithNewOption.Enabled = true;
                        ddlSpecialization.ReqNewOptionText.Enabled = false;
                    }

                    if (ddlEntityWorkingFor.SelectedValue == "New")
                    {
                        ddlEntityWorkingFor.ReqNewOptionText.Enabled = true;
                        ddlEntityWorkingFor.ReqDropWithNewOption.Enabled = false;
                    }
                    else if (string.IsNullOrEmpty(ddlEntityWorkingFor.SelectedValue))
                    {
                        ddlEntityWorkingFor.ReqDropWithNewOption.Enabled = true;
                        ddlEntityWorkingFor.ReqNewOptionText.Enabled = false;
                    }
                }
                if (ddlProgramSpecialization.SelectedValue == "New")
                {
                    ddlProgramSpecialization.ReqNewOptionText.Enabled = true;
                    ddlProgramSpecialization.ReqDropWithNewOption.Enabled = false;
                }
                else if (string.IsNullOrEmpty(ddlProgramSpecialization.SelectedValue))
                {
                    ddlProgramSpecialization.ReqDropWithNewOption.Enabled = true;
                    ddlProgramSpecialization.ReqNewOptionText.Enabled = false;
                }
            }

            Entities.PARequest request = new Entities.PARequest();
            try
            {
                if ((SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditPARequestPage) || SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage.ToLower()))
                    && Page.Session["PAEditRequestId"] != null)
                {
                    request.ID = int.Parse(Convert.ToString(Page.Session["PAEditRequestId"]));
                }
                else
                {
                    request.ID = int.Parse(Page.Session["PANewRequestId"].ToString());
                }

                request.SubmitDate = DateTime.Now;
                request.ActionDate = DateTime.Now;
                request.RequestCreationDate = DateTime.Now;

                request.RequestNumber = lblRequestNumberValue.Text;

                FillRequestObject(request);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PARequestDetails.CreateSubmittedRequestDataObject");
            }
            return request;
        }

        public Entities.PARequest CreateSavedRequestDataObject()
        {
            Logging.GetInstance().Debug("Enter PARequestDetails.CreateSavedRequestDataObject");
            Entities.PARequest request = new Entities.PARequest();
            try
            {
                if ((SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditPARequestPage) || SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage.ToLower()))
                    && Page.Session["PAEditRequestId"] != null)
                {
                    request.ID = int.Parse(Convert.ToString(Page.Session["PAEditRequestId"]));
                }
                else
                {
                    request.ID = int.Parse(Page.Session["PANewRequestId"].ToString());
                }
                request.RequestNumber = lblRequestNumberValue.Text;
                FillRequestObject(request);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PARequestDetails.CreateSavedRequestDataObject");
            }
            return request;
        }

        private void FillRequestObject(PARequest request)
        {
            Logging.GetInstance().Debug("Enter PARequestDetails.FillRequestObject");

            try
            {
                request.LoginName = SPContext.Current.Web.CurrentUser.LoginName.ToLower();

                #region Highest Degree

                request.HighestCertificate = new Entities.Certificates()
                {
                    SelectedID = ddlHighestCertificate.SelectedValue,
                    SelectedTitle = ddlHighestCertificate.SelectedItem.Text
                };

                if (!string.IsNullOrEmpty(dropCountries.SelectedValue))
                {
                    request.CountryOfStudy = new CountryOfStudy()
                    {
                        SelectedID = dropCountries.SelectedValue,
                        SelectedTitle = dropCountries.SelectedItem.Text
                    };
                }
                string secondarySchool = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "SecondarySchool", (uint)LCID).ToLower();
                bool isSecondarySchoolCertified = (ddlHighestCertificate.SelectedItem.Text.ToLower() == secondarySchool);
                if (isSecondarySchoolCertified)
                {
                    request.CertificateDate = ddlCertificateYears.SelectedItem.ToString();
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtCertificateDate.Value))
                    {
                        request.CertificateDate = ExtensionMethods.QatarFormatedDate(Convert.ToDateTime(txtCertificateDate.Value));
                    }
                }

                if (!string.IsNullOrEmpty(ddlUniversity.SelectedValue))
                {
                    if (ddlUniversity.SelectedValue == "New")
                    {
                        request.UniversityNotFoundInList = ddlUniversity.NewOptionText;
                        request.University = new University()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = string.Empty
                        };
                    }
                    else
                    {
                        request.University = new University()
                        {
                            SelectedID = ddlUniversity.SelectedValue,
                            SelectedTitle = ddlUniversity.SelectedText
                        };
                        request.UniversityNotFoundInList = string.Empty;
                        request.UniversityList = txtUniversityListValue.Text;
                    }
                }

                request.Faculty = ddlFaculty.Text;

                if (!string.IsNullOrEmpty(ddlSpecialization.SelectedValue))
                {
                    if (ddlSpecialization.SelectedValue == "New")
                    {
                        request.SpecializationNotFoundInList = ddlSpecialization.NewOptionText;
                        request.Specialization = new Specialization()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = string.Empty
                        };
                    }
                    else
                    {
                        request.Specialization = new Specialization()
                        {
                            SelectedID = ddlSpecialization.SelectedValue,
                            SelectedTitle = ddlSpecialization.NewOptionText
                        };
                        request.SpecializationNotFoundInList = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(ddlProgramSpecialization.SelectedValue))
                {
                    if (ddlProgramSpecialization.SelectedValue == "New")
                    {
                        request.ProgramSpecializationNotFoundInList = ddlProgramSpecialization.NewOptionText;
                        request.ProgramSpecialization = new Specialization()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = string.Empty
                        };
                    }
                    else
                    {
                        request.ProgramSpecialization = new Specialization()
                        {
                            SelectedID = ddlProgramSpecialization.SelectedValue,
                            SelectedTitle = ddlProgramSpecialization.SelectedText
                        };
                        request.ProgramSpecializationNotFoundInList = string.Empty;
                    }
                }

                #endregion Highest Degree

                request.ProgramType = new Entities.AcademicDegree()
                {
                    SelectedID = ddlWantedCertificateDegree.SelectedValue,
                    SelectedTitle = ddlWantedCertificateDegree.SelectedItem.Text
                };

                if (!string.IsNullOrEmpty(ddlCalcSectionFaculty.Text))
                {
                    request.ProgramFaculty = ddlCalcSectionFaculty.Text;
                }

                if (dropCalcSectionStudyingSystem.SelectedIndex > 0)
                {
                    request.ProgramStudySystem = new StudySystem()
                    {
                        SelectedID = dropCalcSectionStudyingSystem.SelectedValue,
                        SelectedTitle = dropCalcSectionStudyingSystem.SelectedItem.Text
                    };
                }
                else
                {
                    request.ProgramStudySystem = new StudySystem()
                    {
                        SelectedID = string.Empty,
                        SelectedTitle = string.Empty
                    };
                }

                if (!string.IsNullOrEmpty(dtAcademicStartDate.Value))
                {
                    DateTime dt = Convert.ToDateTime(DateTime.ParseExact(dtAcademicStartDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

                    if (dt > DateTime.MinValue)
                    {
                        request.AcademicStartDate = dt;
                    }
                }

                if (!string.IsNullOrEmpty(dtAcademicEndDate.Value))
                {
                    DateTime dt = Convert.ToDateTime(DateTime.ParseExact(dtAcademicEndDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                    if (dt > DateTime.MinValue)
                    {
                        request.AcademicEndDate = dt;
                    }
                }

                if (!string.IsNullOrEmpty(txtCalcSectionAcademicNumberOfYears.Text))
                {
                    request.AcademicNumberOfYears = int.Parse(txtCalcSectionAcademicNumberOfYears.Text);
                }
                //request.JoinedOtherUniversities = rdbJoinedOtherUni.SelectedValue == yesValue ? true : false;

                #region Working Section

                request.WorkingOrNot = rdbWorkingOrNot.SelectedValue == yesValue ? true : false;
                if (rdbWorkingOrNot.SelectedValue == yesValue)
                {   //User Control
                    //if (!string.IsNullOrEmpty(ddlEntityWorkingFor.SelectedValue))
                    //{
                    //    if (ddlEntityWorkingFor.SelectedValue == "New")
                    //    {
                    //        request.EntityWorkingFor = new EntityWorkingFor()
                    //        {
                    //            SelectedID = string.Empty,
                    //            SelectedTitle = string.Empty
                    //        };
                    //    }
                    //    else
                    //    {
                    //        request.EntityWorkingFor = new EntityWorkingFor()
                    //        {
                    //            SelectedID = ddlEntityWorkingFor.SelectedValue,
                    //            SelectedTitle = ddlEntityWorkingFor.Text
                    //        };
                    //    }
                    //}
                    if (!string.IsNullOrEmpty(ddlEntityWorkingFor.SelectedValue))
                    {
                        if (ddlEntityWorkingFor.SelectedValue == "New")
                        {
                            request.OtherEntityWorkingFor = ddlEntityWorkingFor.NewOptionText;
                            request.EntityWorkingFor = new EntityWorkingFor()
                            {
                                SelectedID = string.Empty,
                                SelectedTitle = string.Empty
                            };
                        }
                        else
                        {
                            request.EntityWorkingFor = new EntityWorkingFor()
                            {
                                SelectedID = ddlEntityWorkingFor.SelectedValue,
                                SelectedTitle = ddlEntityWorkingFor.SelectedText
                            };
                            request.OtherEntityWorkingFor = string.Empty;
                        }
                    }
                }
                else
                {
                    request.Occupation = string.Empty;
                    request.HiringDate = new DateTime();
                    request.OccupationPhone = string.Empty;
                }

                #endregion Working Section
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PARequestDetails.FillRequestObject");
            }
        }

        #endregion SavingData

        protected void dropCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter PARequestDetails.dropCountries_SelectedIndexChanged");
            try
            {
                FilterUniversitiesByCountry();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PARequestDetails.dropCountries_SelectedIndexChanged");
            }
        }

        protected void ddlCertificateYears_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter PARequestDetails.ddlCertificateYears_SelectedIndexChanged");
            try
            {
                int MandatoryYear = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "AcademicStudyMandatoryYear"));

                int selectedYear = Convert.ToInt32(ddlCertificateYears.SelectedItem.ToString().Split('-')[0]);
                if (selectedYear >= MandatoryYear)
                {
                    fileUploadNationalService.IsRequired = true;
                }
                else
                {
                    fileUploadNationalService.IsRequired = false;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PARequestDetails.ddlCertificateYears_SelectedIndexChanged");
            }
        }

        private void FilterUniversitiesByCountry()
        {
            if (string.IsNullOrEmpty(dropCountries.SelectedValue))
            {
                ddlUniversity.DropWithNewOption.Items.Clear();
                ddlUniversity.DropListItems = null;
                ddlUniversity.BindDataSource();

                ddlSpecialization.DropWithNewOption.Items.Clear();
                ddlSpecialization.DropListItems = null;
                ddlSpecialization.BindDataSource();

                txtUniversityListValue.Text = string.Empty;
            }
            else
            {
                txtUniversityListValue.Text = string.Empty;
                List<University> university = Common.BL.University.GetDistinctUniversityByYearAndCountry(0, int.Parse(dropCountries.SelectedValue), this.LCID);
                ddlUniversity.DropWithNewOption.Items.Clear();

                if (university != null && university.Count > 0)
                {
                    ddlUniversity.DropListItems = university;
                    ddlUniversity.DataValueField = "ID";
                    ddlUniversity.DataTextEnField = "EnglishTitle";
                    ddlUniversity.DataTextArField = "ArabicTitle";
                }
                else
                {
                    ddlUniversity.DropListItems = null;
                }

                ddlUniversity.BindDataSource();
            }
        }

        public void ddlUniversity_SelectedIndexChanged(Object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter PARequestDetails.ddlUniversity_SelectedIndexChanged");
            try
            {
                //  DropDownList dropDownList = sender as DropDownList;
                if (string.IsNullOrEmpty(ddlUniversity.SelectedValue) || ddlUniversity.SelectedValue == "New")
                {


                    txtUniversityListValue.Text = string.Empty;
                }
                else
                {
                    string universityList = Common.BL.UniversityLists.GetUniversityListByUniversityId(int.Parse(ddlUniversity.SelectedValue), 0);
                    if (!string.IsNullOrEmpty(universityList))
                    {
                        txtUniversityListValue.Text = universityList;
                    }
                    else
                    {
                        txtUniversityListValue.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "UniversityListNotFound", (uint)LCID);
                    }

                    
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PARequestDetails.ddlUniversity_SelectedIndexChanged");
            }
        }

        public void BindAttachments()
        {
            Logging.GetInstance().Debug("Entering method PARequestDetails.BindAttachments");
            try
            {
                if (Page.Session["PANewRequestId"] != null || Page.Session["PAEditRequestId"] != null)
                {
                    int requestId = 0;
                    if ((SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditPARequestPage) || SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage.ToLower()))
                        && Page.Session["PAEditRequestId"] != null)
                    {
                        requestId = int.Parse(Page.Session["PAEditRequestId"].ToString());
                    }
                    else
                    {
                        requestId = int.Parse(Page.Session["PANewRequestId"].ToString());
                    }

                    fileUploadWorking.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                    fileUploadWorking.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadWorking.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "CopyOfOrganizationlLetter", (uint)LCID);
                    fileUploadWorking.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "CopyOfOrganizationlLetterRequired", (uint)LCID);
                    fileUploadWorking.MaxFileNumber = 1;
                    fileUploadWorking.MaxSize = 10240000;
                    fileUploadWorking.Group = "WorkingDocument";
                    fileUploadWorking.RequestID = requestId;
                    fileUploadWorking.SupportedExtensions = "PNG,PDF,JPG";
                    fileUploadWorking.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadWorking.Enabled = true;
                    fileUploadWorking.IsRequired = true;
                    fileUploadWorking.ValidationGroup = "Submit";
                    fileUploadWorking.Bind();

                    fileUploadNotWorking.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                    fileUploadNotWorking.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadNotWorking.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "adlsaLetter", (uint)LCID);
                    fileUploadNotWorking.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "adlsaLetterRequired", (uint)LCID);
                    fileUploadNotWorking.MaxFileNumber = 1;
                    fileUploadNotWorking.MaxSize = 10240000;
                    fileUploadNotWorking.Group = "adlsaLetter";
                    fileUploadNotWorking.RequestID = requestId;
                    fileUploadNotWorking.SupportedExtensions = "PNG,PDF,JPG";
                    fileUploadNotWorking.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadNotWorking.Enabled = true;
                    fileUploadNotWorking.IsRequired = true;
                    fileUploadNotWorking.ValidationGroup = "Submit";
                    fileUploadNotWorking.Bind();

                    schoolDocuments.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                    schoolDocuments.DocLibWebUrl = SPContext.Current.Site.Url;
                    schoolDocuments.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "schoolDocuments", (uint)LCID);
                    schoolDocuments.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "schoolDocumentsRequired", (uint)LCID);
                    schoolDocuments.MaxFileNumber = 1;
                    schoolDocuments.MaxSize = 10240000;
                    schoolDocuments.Group = "schoolDocuments";
                    schoolDocuments.RequestID = requestId;
                    schoolDocuments.SupportedExtensions = "PNG,PDF,JPG";
                    schoolDocuments.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    schoolDocuments.Enabled = true;
                    schoolDocuments.IsRequired = true;
                    schoolDocuments.ValidationGroup = "Submit";
                    schoolDocuments.Bind();

                    fileUploadNationalService.DocumentLibraryName = Common.Utilities.Constants.PARequestsAttachments;
                    fileUploadNationalService.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadNationalService.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NationalServiceIDCopy", (uint)LCID);
                    fileUploadNationalService.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredNationalServiceIDCopy", (uint)LCID);
                    fileUploadNationalService.MaxFileNumber = 1;
                    fileUploadNationalService.MaxSize = 10240000;
                    fileUploadNationalService.Group = "NationalServiceIDCopy";
                    fileUploadNationalService.RequestID = requestId;
                    fileUploadNationalService.SupportedExtensions = "PNG,PDF,JPG";
                    fileUploadNationalService.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadNationalService.Enabled = true;
                    fileUploadNationalService.ValidationGroup = "Submit";
                    fileUploadNationalService.Bind();

                    universityDocuments.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                    universityDocuments.DocLibWebUrl = SPContext.Current.Site.Url;
                    universityDocuments.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "universityDocuments", (uint)LCID);
                    universityDocuments.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "universityDocumentsRequired", (uint)LCID);
                    universityDocuments.MaxFileNumber = 1;
                    universityDocuments.MaxSize = 10240000;
                    universityDocuments.Group = "universityDocuments";
                    universityDocuments.RequestID = requestId;
                    universityDocuments.SupportedExtensions = "PNG,PDF,JPG";
                    universityDocuments.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    universityDocuments.Enabled = true;
                    universityDocuments.IsRequired = true;
                    universityDocuments.ValidationGroup = "Submit";
                    universityDocuments.Bind();

                    //fileUploadCalcDetails.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                    //fileUploadCalcDetails.DocLibWebUrl = SPContext.Current.Site.Url;
                    //fileUploadCalcDetails.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "fileUploadCalcDetails", (uint)LCID);
                    //fileUploadCalcDetails.MaxFileNumber = 1;
                    //fileUploadCalcDetails.MaxSize = 10240000;
                    //fileUploadCalcDetails.Group = "attachments";
                    //fileUploadCalcDetails.RequestID = requestId;
                    //fileUploadCalcDetails.SupportedExtensions = "PNG,PDF,JPG";
                    //fileUploadCalcDetails.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    //fileUploadCalcDetails.Enabled = true;
                    //fileUploadCalcDetails.Bind();

                    fileUploadStudyPlan.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                    fileUploadStudyPlan.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadStudyPlan.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "StudyPlan", (uint)LCID);
                    fileUploadStudyPlan.MaxFileNumber = 1;
                    fileUploadStudyPlan.MaxSize = 10240000;
                    fileUploadStudyPlan.Group = "PAStudyPlan";
                    fileUploadStudyPlan.RequestID = requestId;
                    fileUploadStudyPlan.SupportedExtensions = "PNG,PDF,JPG";
                    fileUploadStudyPlan.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadStudyPlan.Enabled = true;
                    fileUploadStudyPlan.Bind();

                    //fileUploadGrades.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                    //fileUploadGrades.DocLibWebUrl = SPContext.Current.Site.Url;
                    //fileUploadGrades.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Grading", (uint)LCID);
                    //fileUploadGrades.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "RequiredFileUploadGrades", (uint)LCID);
                    //fileUploadGrades.MaxFileNumber = 1;
                    //fileUploadGrades.MaxSize = 10240000;
                    //fileUploadGrades.Group = "Grades";
                    //fileUploadGrades.RequestID = requestId;
                    //fileUploadGrades.SupportedExtensions = "PNG,PDF,JPG";
                    //fileUploadGrades.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    //fileUploadGrades.Enabled = true;
                    //fileUploadGrades.IsRequired = true;
                    //fileUploadGrades.ValidationGroup = "Submit";
                    //fileUploadGrades.Bind();

                    fileUploadAdmissionLetter.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                    fileUploadAdmissionLetter.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadAdmissionLetter.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "UnconditionalAdmissionLetter", (uint)LCID);
                    fileUploadAdmissionLetter.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "RequiredUnconditionalAdmissionLetter", (uint)LCID);
                    fileUploadAdmissionLetter.MaxFileNumber = 1;
                    fileUploadAdmissionLetter.MaxSize = 10240000;
                    fileUploadAdmissionLetter.Group = "UnconditionalAdmissionLetter";
                    fileUploadAdmissionLetter.RequestID = requestId;
                    fileUploadAdmissionLetter.SupportedExtensions = "PNG,PDF,JPG";
                    fileUploadAdmissionLetter.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadAdmissionLetter.Enabled = true;
                    fileUploadAdmissionLetter.IsRequired = true;
                    fileUploadAdmissionLetter.ValidationGroup = "Submit";
                    fileUploadAdmissionLetter.Bind();

                    //fileUploadAcceptedHours.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                    //fileUploadAcceptedHours.DocLibWebUrl = SPContext.Current.Site.Url;
                    //fileUploadAcceptedHours.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "AcceptedHours", (uint)LCID);
                    //fileUploadAcceptedHours.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "RequiredAcceptedHours", (uint)LCID);
                    //fileUploadAcceptedHours.MaxFileNumber = 1;
                    //fileUploadAcceptedHours.MaxSize = 10240000;
                    //fileUploadAcceptedHours.Group = "AcceptedHours";
                    //fileUploadAcceptedHours.RequestID = requestId;
                    //fileUploadAcceptedHours.SupportedExtensions = "PNG,PDF,JPG";
                    //fileUploadAcceptedHours.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    //fileUploadAcceptedHours.Enabled = true;
                    //fileUploadAcceptedHours.IsRequired = true;
                    //fileUploadAcceptedHours.ValidationGroup = "Submit";
                    //fileUploadAcceptedHours.Bind();

                    FUDequivalentHours.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                    FUDequivalentHours.DocLibWebUrl = SPContext.Current.Site.Url;
                    FUDequivalentHours.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "EquivalentHours", (uint)LCID);
                    FUDequivalentHours.MaxFileNumber = 1;
                    FUDequivalentHours.MaxSize = 10240000;
                    FUDequivalentHours.Group = "EquivalentHours";
                    FUDequivalentHours.RequestID = requestId;
                    FUDequivalentHours.SupportedExtensions = "PNG,PDF,JPG";
                    FUDequivalentHours.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    FUDequivalentHours.Enabled = true;
                    FUDequivalentHours.Bind();

                    fileUploadDelegates.DocumentLibraryName = Utilities.Constants.PADelegationDocuments;
                    fileUploadDelegates.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadDelegates.AfterDawnLoad = true;
                    fileUploadDelegates.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "DelegationTemplates", (uint)LCID);
                    fileUploadDelegates.MaxSize = 7168000;
                    fileUploadDelegates.Group = "PADelegationTemplates";
                    fileUploadDelegates.RequestID = requestId;
                    fileUploadDelegates.SupportedExtensions = "DOC,DOCX,PDF,PNG,JPG";
                    fileUploadDelegates.IsRequired = true;
                    fileUploadDelegates.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "RequiredDelegates", (uint)LCID);
                    fileUploadDelegates.ValidationGroup = "Submit";
                    fileUploadDelegates.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadDelegates.Enabled = true;
                    fileUploadDelegates.Bind();

                    FUADegreeList.AttachmentCategorySelectedText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "DegreeList", (uint)LCID);

                    FUADegreeList.DocumentLibraryName = Utilities.Constants.PACalculatedDetailsForCertificateAttachments;
                    FUADegreeList.DocLibWebUrl = SPContext.Current.Site.Url;

                    FUADegreeList.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "DegreeList", (uint)LCID);
                    FUADegreeList.MaxFileNumber = BL.StudyAttachmentTypes.GetAll().Count;
                    FUADegreeList.MaxSize = 10240000;

                    if ((SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage) || SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage))
                            && Page.Session["EditRequestId"] != null)
                    {
                        if (ViewState["FUADegreeList"] != null)
                        {
                            FUADegreeList.Group = ViewState["FUADegreeList"].ToString();
                        }
                        else
                        {
                            FUADegreeList.Group = repCalculatedDetailsForCertificate.Items.Count.ToString();
                        }
                    }
                    else
                    {
                        if (ViewState["FUADegreeList"] == null)
                        {
                            FUADegreeList.Group = "0";
                        }
                        else
                        {
                            FUADegreeList.Group = ViewState["FUADegreeList"].ToString();
                        }
                    }

                    FUADegreeList.RequestID = requestId;
                    FUADegreeList.SupportedExtensions = "PNG,PDF,JPG";
                    //FUADegreeList.IsRequired = true;
                    //FUADegreeList.ValidationGroup = "AddCalc";
                    FUADegreeList.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    FUADegreeList.Enabled = true;

                    FUADegreeList.Bind();

                    FUAcceptedHoursList.AttachmentCategorySelectedText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "AcceptedHoursList", (uint)LCID);

                    FUAcceptedHoursList.DocumentLibraryName = Utilities.Constants.PACalculatedDetailsForCertificateAttachments;
                    FUAcceptedHoursList.DocLibWebUrl = SPContext.Current.Site.Url;

                    FUAcceptedHoursList.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "AcceptedHoursList", (uint)LCID);
                    FUAcceptedHoursList.MaxFileNumber = BL.StudyAttachmentTypes.GetAll().Count;
                    FUAcceptedHoursList.MaxSize = 10240000;

                    if ((SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage) || SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage))
                            && Page.Session["EditRequestId"] != null)
                    {
                        if (ViewState["FUAcceptedHoursList"] != null)
                        {
                            FUAcceptedHoursList.Group = ViewState["FUAcceptedHoursList"].ToString();
                        }
                        else
                        {
                            FUAcceptedHoursList.Group = repCalculatedDetailsForCertificate.Items.Count.ToString() + "0";
                        }
                    }
                    else
                    {
                        if (ViewState["FUAcceptedHoursList"] == null)
                        {
                            FUAcceptedHoursList.Group = "00";
                        }
                        else
                        {
                            FUAcceptedHoursList.Group = ViewState["FUAcceptedHoursList"].ToString() + "0";
                        }
                    }

                    FUAcceptedHoursList.RequestID = requestId;
                    FUAcceptedHoursList.SupportedExtensions = "PNG,PDF,JPG";
                    //FUAcceptedHoursList.IsRequired = true;
                    //FUAcceptedHoursList.ValidationGroup = "AddCalc";
                    FUAcceptedHoursList.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    FUAcceptedHoursList.Enabled = true;

                    FUAcceptedHoursList.Bind();
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PARequestDetails.BindAttachments");
            }
        }

        public bool SaveAttachments()
        {
            Logging.GetInstance().Debug("Entering method PARequestDetails.SaveAttachments");
            bool saved = false;
            bool fileWorking = false;
            bool fileNotWorking = false;
            bool schoolName = false;
            bool NationalService = false;
            bool universotyDocs = false;
            bool fileAcceptedHours = false;
            bool fileGrades = false;
            bool fileADegreeList = false;
            bool fileAcceptedHoursList = false;
            bool fileDelegates = fileUploadDelegates.SaveAttachments();
            bool fileequivalentHours = FUDequivalentHours.SaveAttachments();
            bool fUDAdmissionLetter = fileUploadAdmissionLetter.SaveAttachments();
            bool fUDStudyPlan = fileUploadStudyPlan.SaveAttachments();

            try
            {
                bool working = (rdbWorkingOrNot.SelectedIndex == 0);
                if (working)
                {
                    fileWorking = fileUploadWorking.SaveAttachments();
                }
                else
                {
                    fileNotWorking = fileUploadNotWorking.SaveAttachments();
                }
                string secondarySchool = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "SecondarySchool", (uint)LCID).ToLower();
                bool isSecondarySchoolCertified = (ddlHighestCertificate.SelectedItem.Text.ToLower() == secondarySchool);
                if (isSecondarySchoolCertified)
                {
                    schoolName = schoolDocuments.SaveAttachments();
                    ddlCertificateYears.Visible = true;
                    txtCertificateDate.Visible = false;
                    txtCertificateDate.Value = string.Empty;
                    reqCertificateDate.ControlToValidate = "ddlCertificateYears";
                }
                else
                {
                    ddlCertificateYears.Visible = false;
                    txtCertificateDate.Visible = true;
                    reqCertificateDate.ControlToValidate = "txtCertificateDate";

                    universotyDocs = universityDocuments.SaveAttachments();
                }

                if (commonBL.Applicants.ViewNationalService(personID.ToString()))
                {
                    NationalService = fileUploadNationalService.SaveAttachments();
                }

                //if (rdbJoinedOtherUni.SelectedIndex == 0)
                //{
                //    fileGrades = fileUploadGrades.SaveAttachments();
                //    fileAcceptedHours = fileUploadAcceptedHours.SaveAttachments();
                //}

                //bool wantedcertificate = fileUploadCalcDetails.SaveAttachments();

                foreach (RepeaterItem item in repCalculatedDetailsForCertificate.Items)
                {
                    FUADegreeList.Group = item.ItemIndex.ToString();
                    FUADegreeList.GetAttachments();

                    fileADegreeList = FUADegreeList.SaveAttachments();
                }

                foreach (RepeaterItem item in repCalculatedDetailsForCertificate.Items)
                {
                    FUAcceptedHoursList.Group = item.ItemIndex.ToString() + "0";
                    FUAcceptedHoursList.GetAttachments();

                    fileAcceptedHoursList = FUAcceptedHoursList.SaveAttachments();
                }

                if (fileWorking && fUDAdmissionLetter && fUDStudyPlan && fileDelegates && fileequivalentHours && NationalService && schoolName && universotyDocs && fileAcceptedHoursList && fileADegreeList && fileGrades && fileAcceptedHours)
                {
                    saved = true;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PARequestDetails.SaveAttachments");
            }
            return saved;
        }

        public bool GetCalculatedDetailsDataFromRepeater(int requestId)
        {
            Logging.GetInstance().Debug("Enter RequestDetails.GetCalculatedDetailsDataFromRepeater");
            bool updated = false;
            try
            {
                BL.CalculatedDetailsForCertificate.DeleteRequestCalculatedDetailsForCertificate(requestId);

                CalculatedDetailsForCertificate calculatedDetailsForCertificate = new CalculatedDetailsForCertificate();

                HiddenField hiddenId = null;
                Label lblStudySystem = null;

                HiddenField hdnStudySystem = null;
                Label lblCountry = null;
                HiddenField hdnCountry = null;
                Label lblUniversity = null;
                HiddenField hdnUniversity = null;
                Label lblFaculty = null;
                Label lblOtherCountry = null;
                Label lblOtherUniversity = null;
                Label lblOtherFaculty = null;
                int calculatedDetailsId = 0;

                foreach (RepeaterItem item in repCalculatedDetailsForCertificate.Items)
                {
                    hiddenId = (item.FindControl("hdnId") as HiddenField);
                    hdnCountry = (item.FindControl("hdnCalcSectionCountry") as HiddenField);
                    lblCountry = (item.FindControl("lblCalcSectionCountry") as Label);
                    hdnUniversity = (item.FindControl("hdnCalcSectionUniversity") as HiddenField);
                    lblUniversity = (item.FindControl("lblCalcSectionUniversity") as Label);
                    lblFaculty = (item.FindControl("lblCalcSectionFaculty") as Label);
                    hdnStudySystem = (item.FindControl("hdnCalcSectionStudySystem") as HiddenField);
                    lblStudySystem = (item.FindControl("lblCalcSectionStudySystem") as Label);
                    lblOtherCountry = (item.FindControl("lblCalcSectionOtherCountry") as Label);
                    lblOtherUniversity = (item.FindControl("lblCalcSectionOtherUniversity") as Label);
                    lblOtherFaculty = (item.FindControl("lblCalcSectionOtherFaculty") as Label);
                    Label lblConvertedHours = (item.FindControl("lblConvertedHours") as Label);

                    if (lblCountry != null && !string.IsNullOrEmpty(lblCountry.Text)
                     && hdnCountry != null && !string.IsNullOrEmpty(hdnCountry.Value))
                    {
                        calculatedDetailsForCertificate.Country = new CountryOfStudy() { SelectedID = hdnCountry.Value, SelectedTitle = lblCountry.Text };
                    }
                    else
                    {
                        calculatedDetailsForCertificate.Country = new CountryOfStudy() { SelectedID = string.Empty, SelectedTitle = string.Empty };
                    }
                    if (lblOtherCountry != null && !string.IsNullOrEmpty(lblOtherCountry.Text))
                    {
                        calculatedDetailsForCertificate.OtherCountry = lblOtherCountry.Text;
                    }
                    if (lblUniversity != null && !string.IsNullOrEmpty(lblUniversity.Text)
                     && hdnUniversity != null && !string.IsNullOrEmpty(hdnUniversity.Value))
                    {
                        calculatedDetailsForCertificate.Univesrity = new University() { SelectedID = hdnUniversity.Value, SelectedTitle = lblUniversity.Text };
                    }
                    else
                    {
                        calculatedDetailsForCertificate.Univesrity = new University() { SelectedID = string.Empty, SelectedTitle = string.Empty };
                    }
                    if (lblOtherUniversity != null && !string.IsNullOrEmpty(lblOtherUniversity.Text))
                    {
                        calculatedDetailsForCertificate.OtherUniversity = lblOtherUniversity.Text;
                    }
                    if (lblFaculty != null && !string.IsNullOrEmpty(lblFaculty.Text))
                    {
                        calculatedDetailsForCertificate.Faculty = lblFaculty.Text;
                    }
                    else
                    {
                        calculatedDetailsForCertificate.Faculty = string.Empty;
                    }
                    if (lblOtherFaculty != null && !string.IsNullOrEmpty(lblOtherFaculty.Text))
                    {
                        calculatedDetailsForCertificate.OtherFaculty = lblOtherFaculty.Text;
                    }
                    if (lblStudySystem != null && !string.IsNullOrEmpty(lblStudySystem.Text)
                        && hdnStudySystem != null && !string.IsNullOrEmpty(hdnStudySystem.Value))
                    {
                        calculatedDetailsForCertificate.StudySystem = new StudySystem() { SelectedID = hdnStudySystem.Value, SelectedTitle = lblStudySystem.Text };
                    }
                    else
                    {
                        calculatedDetailsForCertificate.StudySystem = new StudySystem() { SelectedID = string.Empty, SelectedTitle = string.Empty };
                    }
                    calculatedDetailsForCertificate.AcceptedHours = !string.IsNullOrEmpty(lblConvertedHours.Text) ? lblConvertedHours.Text : "0";

                    calculatedDetailsId = BL.CalculatedDetailsForCertificate.SaveCalculatedDetailsForCertificate(calculatedDetailsForCertificate, requestId);

                    if (calculatedDetailsId != 0)
                    {
                        hiddenId.Value = calculatedDetailsId.ToString();
                    }
                }
                updated = true;
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit RequestDetails.GetCalculatedDetailsDataFromRepeater");
            }

            return updated;
        }

        protected void btnResubmit_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PARequestDetails.btnResubmit_Click");
            try
            {
                if (!IsRefresh)
                {
                    Validate();
                    Page.Validate();
                    if (Page.IsValid)
                    {
                        if (Page.Session["PANewRequestId"] == null && Page.Session["PAEditRequestId"] == null)

                        {
                            lblErrorMessage.Text = lblErrorMessage.Text + " " + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                            modalPopupError.Show();
                            return;
                        }
                        if (SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage.ToLower()) && Page.Session["PAEditRequestId"] != null)
                        {
                            string requestId = Page.Session["PAEditRequestId"].ToString();

                            Entities.PARequest request = CreateSavedRequestDataObject();

                            bool requestUpdated = BL.Request.AddUpdateRequest(request, string.Empty, request.ID, (int)Common.Utilities.RequestStatus.PADraftForClarification);
                            bool savedCalculated = GetCalculatedDetailsDataFromRepeater(int.Parse(requestId));

                            SaveAttachments();
                            ClarificationReqs calrificationReq = BL.PAClarificationRequests.GetClarificationReplybyReqID(requestId);

                            if (calrificationReq != null)
                            {
                                lblSuccess.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ClarificationReplyRequired", (uint)LCID);

                                modalPopUpConfirmation.Show();
                            }
                            else
                            {
                                lblSuccess.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ReSubmitSucceed", (uint)LCID), requestId);

                                modalPopUpConfirmation.Show();
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
                Logging.GetInstance().Debug("Exiting method PARequestDetails.btnResubmit_Click");
            }
        }

        protected void lnkAddCalcDetails_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PARequestDetails.lnkAddCalcDetails_Click");
            try
            {
                if (ddlCalcSectionCountry.SelectedValue == "New")
                {
                    ddlCalcSectionCountry.ReqNewOptionText.Enabled = true;
                }
                else
                {
                    ddlCalcSectionCountry.ReqNewOptionText.Enabled = false;
                }
                if (ddlCalcSectionUniversity.SelectedValue == "New")
                {
                    ddlCalcSectionUniversity.ReqNewOptionText.Enabled = true;
                }
                else
                {
                    ddlCalcSectionUniversity.ReqNewOptionText.Enabled = false;
                }
                ddlCalcSectionCountry.ReqNewOptionText.Validate();
                ddlCalcSectionUniversity.ReqNewOptionText.Validate();
                if (!ddlCalcSectionCountry.ReqNewOptionText.IsValid || !ddlCalcSectionUniversity.ReqNewOptionText.IsValid)
                {
                    return;
                }
                ddlCalcSectionCountry.ReqNewOptionText.Enabled = false;
                ddlCalcSectionUniversity.ReqNewOptionText.Enabled = false;

                if (
                         ddlCalcSectionCountry.DropWithNewOption.SelectedIndex > 0 ||
                         ddlCalcSectionUniversity.DropWithNewOption.SelectedIndex > 0 ||
                         dropStudyingSystem.SelectedIndex > 0 || !string.IsNullOrEmpty(txtCalcSectionFaculty.Text))
                {
                    List<CalculatedDetailsForCertificate> calcList = GetCalculatedRepeaterBoundData();

                    CalculatedDetailsForCertificate calcItem = new CalculatedDetailsForCertificate();
                    if (ddlCalcSectionCountry.DropWithNewOption.SelectedIndex > 0)
                    {
                        if (ddlCalcSectionCountry.DropWithNewOption.SelectedValue == "New")
                        {
                            calcItem.OtherCountry = ddlCalcSectionCountry.NewOptionText;
                            calcItem.Country = new CountryOfStudy()
                            {
                                SelectedID = string.Empty,
                                SelectedTitle = string.Empty
                            };
                        }
                        else
                        {
                            calcItem.OtherCountry = string.Empty;
                            calcItem.Country = new CountryOfStudy()
                            {
                                SelectedID = ddlCalcSectionCountry.SelectedValue,
                                SelectedTitle = ddlCalcSectionCountry.SelectedText
                            };
                        }
                    }
                    if (ddlCalcSectionUniversity.DropWithNewOption.SelectedIndex > 0)
                    {
                        if (ddlCalcSectionUniversity.DropWithNewOption.SelectedValue == "New")
                        {
                            calcItem.OtherUniversity = ddlCalcSectionUniversity.NewOptionText;
                            calcItem.Univesrity = new University()
                            {
                                SelectedID = string.Empty,
                                SelectedTitle = string.Empty
                            };
                        }
                        else
                        {
                            calcItem.OtherUniversity = string.Empty;
                            calcItem.Univesrity = new University()
                            {
                                SelectedID = ddlCalcSectionUniversity.SelectedValue,
                                SelectedTitle = ddlCalcSectionUniversity.SelectedText
                            };
                        }
                    }
                    calcItem.Faculty = txtCalcSectionFaculty.Text != null ? txtCalcSectionFaculty.Text : string.Empty;

                    if (dropStudyingSystem.SelectedIndex > 0)
                    {
                        calcItem.StudySystem = new StudySystem()
                        {
                            SelectedID = dropStudyingSystem.SelectedValue,
                            SelectedTitle = dropStudyingSystem.SelectedItem.Text
                        };
                    }
                    else
                    {
                        calcItem.StudySystem = new StudySystem()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = string.Empty
                        };
                    }
                    calcItem.AcceptedHours = !string.IsNullOrEmpty(txt_AcceptedHours.Text) ? txt_AcceptedHours.Text : "0";

                    calcList.Add(calcItem);

                    repCalculatedDetailsForCertificate.DataSource = calcList;
                    repCalculatedDetailsForCertificate.DataBind();

                    ddlCalcSectionCountry.DropWithNewOption.ClearSelection();
                    ddlCalcSectionUniversity.DropWithNewOption.ClearSelection();
                    ddlCalcSectionUniversity.DropWithNewOption.Items.Clear();
                    ddlCalcSectionUniversity.DropListItems = null;
                    ddlCalcSectionUniversity.BindDataSource();
                    txtCalcSectionFaculty.Text = string.Empty;
                    txt_AcceptedHours.Text = string.Empty;
                    dropStudyingSystem.ClearSelection();
                    FUADegreeList.Group = (int.Parse(FUADegreeList.Group) + 1).ToString();
                    ViewState["FUADegreeList"] = FUADegreeList.Group;
                    FUADegreeList.Bind();

                    FUAcceptedHoursList.Group = (int.Parse(FUAcceptedHoursList.Group) + 1).ToString() + "0";
                    ViewState["FUAcceptedHoursList"] = FUAcceptedHoursList.Group;
                    FUAcceptedHoursList.Bind();
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PARequestDetails.lnkAddCalcDetails_Click");
            }
        }

        public void ddlCalcSectionCountry_SelectedIndexChanged(Object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter RequestDetails.ddlCalcSectionCountry_SelectedIndexChanged");
            try
            {
                DropDownList dropDownList = sender as DropDownList;
                if (string.IsNullOrEmpty(dropDownList.SelectedValue) || dropDownList.SelectedValue == "New")
                {
                    ddlCalcSectionUniversity.DropWithNewOption.Items.Clear();
                    if (string.IsNullOrEmpty(dropDownList.SelectedValue))
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
                    List<University> university = Common.BL.University.GetDistinctUniversityByYearAndCountry(0, int.Parse(dropDownList.SelectedValue), LCID);//Common.BL.UniversityLists.GetUniversityWithEmptyUniversityListsAndCountryId(int.Parse(dropDownList.SelectedValue));
                    ddlCalcSectionUniversity.DropWithNewOption.Items.Clear();
                    if (university != null && university.Count > 0)
                    {
                        ddlCalcSectionUniversity.DropListItems = university;
                        ddlCalcSectionUniversity.DataValueField = "ID";
                        ddlCalcSectionUniversity.DataTextEnField = "EnglishTitle";
                        ddlCalcSectionUniversity.DataTextArField = "ArabicTitle";
                    }
                    else
                    {
                        ddlCalcSectionUniversity.DropListItems = null;
                    }
                    ddlCalcSectionUniversity.BindDataSource();
                }
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

        private void BindCalcSectionUniversity(string universityID)
        {
            Logging.GetInstance().Debug("Enter PARequestDetails.BindCalcSectionUniversity");
            try
            {
                List<StudySystem> studySystemItems = BL.StudySystem.GetAll();
                if (studySystemItems != null && studySystemItems.Count > 0)
                {
                    if (!string.IsNullOrEmpty(universityID))
                    {
                        bool online = false;
                        if (universityID != "New")
                        {
                            online = Common.BL.University.IsUniversityOnline(int.Parse(universityID));
                        }

                        if (!online || universityID == "New")
                        {
                            string onlineText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Online", (uint)LCID);
                            studySystemItems.Remove(studySystemItems.Find(s => s.ArabicTitle.Equals(onlineText) || s.EnglishTitle.Equals(onlineText)));
                        }
                    }
                    dropStudyingSystem.Items.Clear();

                    dropStudyingSystem.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), string.Empty));
                    dropStudyingSystem.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref dropStudyingSystem, studySystemItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PARequestDetails.BindCalcSectionUniversity");
            }
        }

        public void ddlCalcSectionUniversity_SelectedIndexChanged(Object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter PARequestDetails.ddlCalcSectionUniversity_SelectedIndexChanged");
            try
            {
                BindCalcSectionUniversity(ddlCalcSectionUniversity.SelectedValue);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PARequestDetails.ddlCalcSectionUniversity_SelectedIndexChanged");
            }
        }

        protected void lnkUpdateCalcDetails_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method RequestDetails.lnkUpdateCalcDetails_Click");
            try
            {
                if (ddlCalcSectionCountry.SelectedValue == "New")
                {
                    ddlCalcSectionCountry.ReqNewOptionText.Enabled = true;
                }
                else
                {
                    ddlCalcSectionCountry.ReqNewOptionText.Enabled = false;
                }
                if (ddlCalcSectionUniversity.SelectedValue == "New")
                {
                    ddlCalcSectionUniversity.ReqNewOptionText.Enabled = true;
                }
                else
                {
                    ddlCalcSectionUniversity.ReqNewOptionText.Enabled = false;
                }
                ddlCalcSectionCountry.ReqNewOptionText.Validate();
                ddlCalcSectionUniversity.ReqNewOptionText.Validate();
                if (!ddlCalcSectionCountry.ReqNewOptionText.IsValid || !ddlCalcSectionUniversity.ReqNewOptionText.IsValid)
                {
                    return;
                }
                ddlCalcSectionCountry.ReqNewOptionText.Enabled = false;
                ddlCalcSectionUniversity.ReqNewOptionText.Enabled = false;

                List<CalculatedDetailsForCertificate> calcList = GetCalculatedRepeaterBoundData();
                if (calcList != null && calcList.Count > 0)
                {
                    CalculatedDetailsForCertificate calcItem = calcList[int.Parse(hdnCalcSectionItem.Value)];
                    if (calcItem != null)
                    {
                        if (dropStudyingSystem.SelectedIndex > 0 ||

                        ddlCalcSectionCountry.DropWithNewOption.SelectedIndex > 0 ||
                        ddlCalcSectionUniversity.DropWithNewOption.SelectedIndex > 0
                        || !string.IsNullOrEmpty(txtCalcSectionFaculty.Text))
                        {
                            if (ddlCalcSectionCountry.DropWithNewOption.SelectedIndex > 0)
                            {
                                if (ddlCalcSectionCountry.DropWithNewOption.SelectedValue == "New")
                                {
                                    calcItem.OtherCountry = ddlCalcSectionCountry.NewOptionText;
                                    calcItem.Country = new CountryOfStudy()
                                    {
                                        SelectedID = string.Empty,
                                        SelectedTitle = string.Empty
                                    };
                                }
                                else
                                {
                                    calcItem.OtherCountry = string.Empty;
                                    calcItem.Country = new CountryOfStudy()
                                    {
                                        SelectedID = ddlCalcSectionCountry.SelectedValue,
                                        SelectedTitle = ddlCalcSectionCountry.SelectedText
                                    };
                                }
                            }
                            if (ddlCalcSectionUniversity.DropWithNewOption.SelectedIndex > 0)
                            {
                                if (ddlCalcSectionUniversity.DropWithNewOption.SelectedValue == "New")
                                {
                                    calcItem.OtherUniversity = ddlCalcSectionUniversity.NewOptionText;
                                    calcItem.Univesrity = new University()
                                    {
                                        SelectedID = string.Empty,
                                        SelectedTitle = string.Empty
                                    };
                                }
                                else
                                {
                                    calcItem.OtherUniversity = string.Empty;
                                    calcItem.Univesrity = new University()
                                    {
                                        SelectedID = ddlCalcSectionUniversity.SelectedValue,
                                        SelectedTitle = ddlCalcSectionUniversity.SelectedText
                                    };
                                }
                            }

                            calcItem.Faculty = txtCalcSectionFaculty.Text != null ? txtCalcSectionFaculty.Text : string.Empty;
                            calcItem.AcceptedHours = !string.IsNullOrEmpty(txt_AcceptedHours.Text) ? txt_AcceptedHours.Text : "0";

                            if (dropStudyingSystem.SelectedIndex > 0)
                            {
                                calcItem.StudySystem = new StudySystem()
                                {
                                    SelectedID = dropStudyingSystem.SelectedValue,
                                    SelectedTitle = dropStudyingSystem.SelectedItem.Text
                                };
                            }
                            else
                            {
                                calcItem.StudySystem = new StudySystem()
                                {
                                    SelectedID = string.Empty,
                                    SelectedTitle = string.Empty
                                };
                            }
                        }
                    }

                    repCalculatedDetailsForCertificate.DataSource = calcList;
                    repCalculatedDetailsForCertificate.DataBind();

                    ddlCalcSectionCountry.DropWithNewOption.ClearSelection();
                    ddlCalcSectionUniversity.DropWithNewOption.ClearSelection();
                    ddlCalcSectionUniversity.DropWithNewOption.Items.Clear();
                    ddlCalcSectionUniversity.DropListItems = null;
                    ddlCalcSectionUniversity.BindDataSource();

                    txtCalcSectionFaculty.Text = string.Empty;
                    txt_AcceptedHours.Text = string.Empty;
                    dropStudyingSystem.ClearSelection();
                    FUADegreeList.Group = (calcList.Count).ToString();
                    ViewState["FUADegreeList"] = FUADegreeList.Group;
                    FUADegreeList.Bind();

                    FUAcceptedHoursList.Group = (calcList.Count).ToString();
                    ViewState["FUAcceptedHoursList"] = FUAcceptedHoursList.Group + "0";
                    FUAcceptedHoursList.Bind();
                    lnkUpdateCalcDetails.Visible = false;
                    lnkAddCalcDetails.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method RequestDetails.lnkUpdateCalcDetails_Click");
            }
        }

        protected void lnkCalcSectionDelete_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method RequestDetails.lnkCalcSectionDelete_Click");

            try
            {
                List<CalculatedDetailsForCertificate> calcList = GetCalculatedRepeaterBoundData();
                RepeaterItem repeaterItem = (sender as LinkButton).NamingContainer as RepeaterItem;
                HiddenField hiddenId = (sender as LinkButton).NamingContainer.FindControl("hdnId") as HiddenField;

                if (ViewState["FileUploadGroup"] != null && int.Parse(ViewState["FileUploadGroup"].ToString()) == repeaterItem.ItemIndex)
                {
                    modalAddCalcSection.Show();
                    return;
                }
                if (!string.IsNullOrEmpty(hiddenId.Value))
                {
                    BL.CalculatedDetailsForCertificate.DeleteCalculatedDetailsForCertificateItem(int.Parse(hiddenId.Value));
                }

                calcList.RemoveAt(repeaterItem.ItemIndex);

                repCalculatedDetailsForCertificate.DataSource = calcList;
                repCalculatedDetailsForCertificate.DataBind();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method RequestDetails.lnkCalcSectionDelete_Click");
            }
        }

        protected void lnkCalcSectionEdit_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method RequestDetails.lnkCalcSectionEdit_Click");

            try
            {
                lnkUpdateCalcDetails.Visible = true;
                lnkAddCalcDetails.Visible = false;
                string uniID = string.Empty;

                RepeaterItem repeaterItem = (sender as LinkButton).NamingContainer as RepeaterItem;
                HiddenField hdnCalcCountry = repeaterItem.FindControl("hdnCalcSectionCountry") as HiddenField;
                Label lblOtherCountry = repeaterItem.FindControl("lblCalcSectionOtherCountry") as Label;
                HiddenField hdnCalcUniversity = repeaterItem.FindControl("hdnCalcSectionUniversity") as HiddenField;
                Label lblOtherUni = repeaterItem.FindControl("lblCalcSectionOtherUniversity") as Label;
                HiddenField hdnStudySystem = repeaterItem.FindControl("hdnCalcSectionStudySystem") as HiddenField;
                Label lblCalcSectionFaculty = repeaterItem.FindControl("lblCalcSectionFaculty") as Label;
                Label lblConvertedHours = repeaterItem.FindControl("lblConvertedHours") as Label;

                CalculatedDetailsForCertificate calculatedDetails = new CalculatedDetailsForCertificate()
                {
                    Country = new CountryOfStudy() { SelectedID = hdnCalcCountry.Value },
                    OtherCountry = lblOtherCountry.Text,
                    Univesrity = new University() { SelectedID = hdnCalcUniversity.Value },
                    OtherUniversity = lblOtherUni.Text,
                    Faculty = lblCalcSectionFaculty.Text,
                    StudySystem = new StudySystem() { SelectedID = hdnStudySystem.Value },
                    AcceptedHours = lblConvertedHours.Text
                };

                if (calculatedDetails != null)
                {
                    List<CountryOfStudy> countryItems = Common.BL.CountryOfStudy.GetAll(LCID);

                    ddlCalcSectionCountry.DropListItems = null;
                    ddlCalcSectionCountry.BindDataSource();
                    ddlCalcSectionCountry.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)LCID);
                    ddlCalcSectionCountry.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredNewCountry", (uint)LCID);
                    ddlCalcSectionCountry.ValidationGroup = "AddCalc";
                    ddlCalcSectionCountry.DropWithNewOption.AutoPostBack = true;
                    ddlCalcSectionCountry.DropWithNewOption.Items.Clear();
                    ddlCalcSectionCountry.DropListItems = countryItems;
                    ddlCalcSectionCountry.DataValueField = "ID";
                    ddlCalcSectionCountry.DataTextEnField = "EnglishTitle";
                    ddlCalcSectionCountry.DataTextArField = "ArabicTitle";

                    ddlCalcSectionCountry.BindDataSource();

                    if (!string.IsNullOrEmpty(calculatedDetails.OtherCountry))
                    {
                        ddlCalcSectionCountry.SelectedValue = "New";
                        ddlCalcSectionCountry.NewOptionText = calculatedDetails.OtherCountry;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(calculatedDetails.Country.SelectedID))
                        {
                            ddlCalcSectionCountry.SelectedValue = calculatedDetails.Country.SelectedID;
                        }
                    }
                    if (!string.IsNullOrEmpty(calculatedDetails.OtherUniversity) || !string.IsNullOrEmpty(calculatedDetails.OtherCountry))
                    {
                        ddlCalcSectionUniversity.DropWithNewOption.Items.Clear();
                        ddlCalcSectionUniversity.DropListItems = "New";
                        ddlCalcSectionUniversity.BindDataSource();
                        if (!string.IsNullOrEmpty(calculatedDetails.OtherUniversity))
                        {
                            ddlCalcSectionUniversity.SelectedValue = "New";
                            ddlCalcSectionUniversity.NewOptionText = calculatedDetails.OtherUniversity;
                        }
                    }
                    else
                    {
                        ddlCalcSectionUniversity.DropListItems = null;
                        ddlCalcSectionUniversity.BindDataSource();
                        ddlCalcSectionUniversity.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)LCID);
                        ddlCalcSectionUniversity.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredNewUniversity", (uint)LCID);
                        ddlCalcSectionUniversity.ValidationGroup = "AddCalc";
                        if (!string.IsNullOrEmpty(calculatedDetails.Country.SelectedID))
                        {
                            List<University> university = Common.BL.University.GetDistinctUniversityByYearAndCountry(0, int.Parse(calculatedDetails.Country.SelectedID), LCID);
                            ddlCalcSectionUniversity.DropWithNewOption.Items.Clear();
                            ddlCalcSectionUniversity.DropListItems = university;
                            ddlCalcSectionUniversity.DataValueField = "ID";
                            ddlCalcSectionUniversity.DataTextEnField = "EnglishTitle";
                            ddlCalcSectionUniversity.DataTextArField = "ArabicTitle";
                            ddlCalcSectionUniversity.BindDataSource();

                            if (!string.IsNullOrEmpty(calculatedDetails.Univesrity.SelectedID))
                            {
                                ddlCalcSectionUniversity.SelectedValue = calculatedDetails.Univesrity.SelectedID;
                                uniID = calculatedDetails.Univesrity.SelectedID;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(calculatedDetails.Faculty))
                    {
                        txtCalcSectionFaculty.Text = calculatedDetails.Faculty;
                    }
                    if (!string.IsNullOrEmpty(calculatedDetails.AcceptedHours))
                    {
                        txt_AcceptedHours.Text = calculatedDetails.AcceptedHours;
                    }

                    if (!string.IsNullOrEmpty(calculatedDetails.StudySystem.SelectedID))
                    {
                        BindCalcSectionUniversity(ddlCalcSectionUniversity.SelectedValue);
                        dropStudyingSystem.SelectedValue = calculatedDetails.StudySystem.SelectedID;
                    }

                    hdnCalcSectionItem.Value = repeaterItem.ItemIndex.ToString();
                    FUADegreeList.Group = repeaterItem.ItemIndex.ToString();
                    ViewState["FUADegreeList"] = FUADegreeList.Group;
                    FUADegreeList.Bind();

                    FUAcceptedHoursList.Group = repeaterItem.ItemIndex.ToString();
                    ViewState["FUAcceptedHoursList"] = FUAcceptedHoursList.Group + "0";
                    FUAcceptedHoursList.Bind();

                    uniID = !string.IsNullOrEmpty(uniID) ? uniID : "New";

                    BindCalcSectionUniversity(uniID);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method RequestDetails.lnkCalcSectionEdit_Click");
            }
        }

        protected void repCalculatedDetailsForCertificate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Logging.GetInstance().Debug("Entering method NewRequestUserControl.repCalculatedDetailsForCertificate_ItemDataBound");

            try
            {
                if (repCalculatedDetailsForCertificate.Items.Count < 1)
                {
                    if (e.Item.ItemType == ListItemType.Footer)
                    {
                        HtmlGenericControl dvNoRec = e.Item.FindControl("dvNoRecords") as HtmlGenericControl;
                        if (dvNoRec != null)
                        {
                            dvNoRec.Visible = true;
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
                Logging.GetInstance().Debug("Exiting method NewRequestUserControl.repCalculatedDetailsForCertificate_ItemDataBound");
            }
        }

        private List<CalculatedDetailsForCertificate> GetCalculatedRepeaterBoundData()
        {
            Logging.GetInstance().Debug("Entering method RequestDetails.GetCalculatedRepeaterBoundData");
            List<CalculatedDetailsForCertificate> calcList = new List<CalculatedDetailsForCertificate>();
            HiddenField hdnStudySystem = null;
            Label lblStudySystem = null;

            Label lblCountry = null;
            HiddenField hdnCountry = null;
            Label lblUniversity = null;
            HiddenField hdnUniversity = null;
            Label lblFaculty = null;
            Label lblConvertedHours = null;
            Label lblOtherCountry = null;
            Label lblOtherUniversity = null;

            try
            {
                foreach (RepeaterItem item in repCalculatedDetailsForCertificate.Items)
                {
                    hdnCountry = (item.FindControl("hdnCalcSectionCountry") as HiddenField);
                    lblCountry = (item.FindControl("lblCalcSectionCountry") as Label);
                    hdnUniversity = (item.FindControl("hdnCalcSectionUniversity") as HiddenField);
                    lblUniversity = (item.FindControl("lblCalcSectionUniversity") as Label);
                    lblFaculty = (item.FindControl("lblCalcSectionFaculty") as Label);
                    lblConvertedHours = (item.FindControl("lblConvertedHours") as Label);
                    hdnStudySystem = (item.FindControl("hdnCalcSectionStudySystem") as HiddenField);
                    lblStudySystem = (item.FindControl("lblCalcSectionStudySystem") as Label);
                    lblOtherCountry = (item.FindControl("lblCalcSectionOtherCountry") as Label);
                    lblOtherUniversity = (item.FindControl("lblCalcSectionOtherUniversity") as Label);
                    calcList.Add(
                                    new CalculatedDetailsForCertificate()
                                    {
                                        Country = new CountryOfStudy() { SelectedID = hdnCountry != null ? hdnCountry.Value : string.Empty, SelectedTitle = lblCountry != null ? lblCountry.Text : string.Empty },
                                        OtherCountry = lblOtherCountry != null ? lblOtherCountry.Text : string.Empty,
                                        Univesrity = new University() { SelectedID = hdnUniversity != null ? hdnUniversity.Value : string.Empty, SelectedTitle = lblUniversity != null ? lblUniversity.Text : string.Empty },
                                        OtherUniversity = lblOtherUniversity != null ? lblOtherUniversity.Text : string.Empty,
                                        Faculty = lblFaculty != null ? lblFaculty.Text : string.Empty,
                                        StudySystem = new StudySystem() { SelectedID = hdnStudySystem != null ? hdnStudySystem.Value : string.Empty, SelectedTitle = lblStudySystem != null ? lblStudySystem.Text : string.Empty },
                                        AcceptedHours = lblConvertedHours != null ? lblConvertedHours.Text : string.Empty
                                    });
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method RequestDetails.GetCalculatedRepeaterBoundData");
            }
            return calcList;
        }

        protected void btnModalOK_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PANewRequestUserControl.btnModalOK_Click");

            try
            {
                string requestId = Page.Session["PAEditRequestId"].ToString();
                ClarificationReqs clarificationReq = BL.PAClarificationRequests.GetClarificationReplybyReqID(requestId);

                if (clarificationReq != null)
                {
                    Page.Session["hdn_ClarRequestId"] = clarificationReq.ID;
                    Page.Session["PADisplayRequestId"] = Page.Session["PAEditRequestId"];
                    Session["ActionTaken"] = "AddNewClarification";
                    Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.PA/Applicant.aspx");
                }
                else
                {
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/TrackRequest.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PANewRequestUserControl.btnModalOK_Click");
            }
        }

        protected void DoYouWorkCheckBoxRequired_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = rdbWorkingOrNot.SelectedIndex > -1;
        }

        protected void rdbWorkingOrNot_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool working = (rdbWorkingOrNot.SelectedIndex == 0);

            fileUploadWorking.Visible = working;

            ShowHideWorkingForAndOccupation.Visible = working;

            fileUploadNotWorking.Visible = !working;

            ShowHideNotWorking.Visible = !working;

            int requestId = 0;
            if (Page.Session["PANewRequestId"] != null)
            {
                requestId = int.Parse(Convert.ToString(Page.Session["PANewRequestId"]));
            }
            else if (Page.Session["PAEditRequestId"] != null)
            {
                requestId = int.Parse(Convert.ToString(Page.Session["PAEditRequestId"]));
            }
            if (!working)
            {
                ddlEntityWorkingFor.DropWithNewOption.ClearSelection();
                if (requestId != 0 && !string.IsNullOrEmpty(fileUploadWorking.Group))
                    BL.PARequestsAttachments.DeleteRequestsAttachmentsByGroupAndRequestID(requestId, fileUploadWorking.Group);
            }
            else
            {
                if (requestId != 0 && !string.IsNullOrEmpty(fileUploadNotWorking.Group))
                    BL.PARequestsAttachments.DeleteRequestsAttachmentsByGroupAndRequestID(requestId, fileUploadNotWorking.Group);
            }
        }

        protected void ddlWantedCertificateDegree_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PANewRequestUserControl.ddlWantedCertificateDegree_SelectedIndexChanged");

            try
            {
                BindStudySystem();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PANewRequestUserControl.ddlWantedCertificateDegree_SelectedIndexChanged");
            }
        }

        private void BindStudySystem()
        {
            Logging.GetInstance().Debug("Entering method PANewRequestUserControl.BindStudySystem");

            try
            {
                string studySystem = string.Empty;
                if (!string.IsNullOrEmpty(dropCalcSectionStudyingSystem.SelectedValue))
                {
                    studySystem = dropCalcSectionStudyingSystem.SelectedValue;
                }
                List<StudySystem> studySystemItems = BL.StudySystem.GetAll();
                if (studySystemItems != null && studySystemItems.Count > 0)
                {
                    if (!(ddlWantedCertificateDegree.SelectedValue == "5" || ddlWantedCertificateDegree.SelectedValue == "6" || ddlWantedCertificateDegree.SelectedValue == "7"))
                    {
                        studySystemItems.RemoveAt(1);
                    }
                    bool online = false;
                    if (!string.IsNullOrEmpty(hdnUniversityID.Value))
                    {
                        if (hdnUniversityID.Value != "New")
                        {
                            online = Common.BL.University.IsUniversityOnline(int.Parse(hdnUniversityID.Value));
                        }

                        if (!online || hdnUniversityID.Value == "New")
                        {
                            string onlineText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Online", (uint)LCID);
                            studySystemItems.Remove(studySystemItems.Find(s => s.ArabicTitle.Equals(onlineText) || s.EnglishTitle.Equals(onlineText)));
                        }
                    }
                    dropCalcSectionStudyingSystem.Items.Clear();

                    dropCalcSectionStudyingSystem.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), string.Empty));
                    dropCalcSectionStudyingSystem.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref dropCalcSectionStudyingSystem, studySystemItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                    if (!string.IsNullOrEmpty(studySystem))
                    {
                        dropCalcSectionStudyingSystem.SelectedValue = studySystem;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PANewRequestUserControl.BindStudySystem");
            }
        }

        protected void ddlHighestCertificate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlHighestCertificate.SelectedValue))
            {
                string secondarySchool = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "SecondarySchool", (uint)LCID).ToLower();
                bool isSecondarySchoolCertified = (ddlHighestCertificate.SelectedItem.Text.ToLower() == secondarySchool);
                //For Readability
                bool isUniversity = !isSecondarySchoolCertified;

                sectionSecondaryCertificate.Visible =

                schoolDocuments.Visible = isSecondarySchoolCertified;

                fileUploadNationalService.Visible = commonBL.Applicants.ViewNationalService(personID.ToString());

                //fileUploadNationalService.IsRequired = false;

                if (isSecondarySchoolCertified)
                {
                    ddlCertificateYears.Visible = true;
                    if (Session["SecondarySchool"] == null)
                        ddlCertificateYears.SelectedIndex = 0;
                    reqCertificateDate.ControlToValidate = "ddlCertificateYears";
                    txtCertificateDate.Visible = false;
                    txtCertificateDate.Value = string.Empty;

                    int MandatoryYear = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "AcademicStudyMandatoryYear"));
                    if (ddlCertificateYears.SelectedIndex > 0)
                    {
                        int selectedYear = Convert.ToInt32(ddlCertificateYears.SelectedItem.ToString().Split('-')[0]);
                        if (selectedYear >= MandatoryYear)
                        {
                            fileUploadNationalService.IsRequired = true;
                        }
                        else
                        {
                            fileUploadNationalService.IsRequired = false;
                        }
                    }
                }
                else
                {
                    txtCertificateDate.Visible = true;
                    reqCertificateDate.ControlToValidate = "txtCertificateDate";
                    ddlCertificateYears.Visible = false;
                }

                divUniversitiesDetails.Visible =
                universityDocuments.Visible = isUniversity;

                int requestId = 0;
                if (Page.Session["PANewRequestId"] != null)
                {
                    requestId = int.Parse(Convert.ToString(Page.Session["PANewRequestId"]));
                }
                else if (Page.Session["PAEditRequestId"] != null)
                {
                    requestId = int.Parse(Convert.ToString(Page.Session["PAEditRequestId"]));
                }
                if (isSecondarySchoolCertified)
                {
                    ddlUniversity.DropWithNewOption.ClearSelection();
                    ddlSpecialization.DropWithNewOption.ClearSelection();
                    ddlFaculty.Text = string.Empty;
                    if (requestId != 0 && !string.IsNullOrEmpty(universityDocuments.Group))
                        BL.PARequestsAttachments.DeleteRequestsAttachmentsByGroupAndRequestID(requestId, universityDocuments.Group);
                }
                else
                {
                    if (requestId != 0 && !string.IsNullOrEmpty(schoolDocuments.Group))
                        BL.PARequestsAttachments.DeleteRequestsAttachmentsByGroupAndRequestID(requestId, schoolDocuments.Group);
                }
                if (!commonBL.Applicants.ViewNationalService(personID.ToString()) && !isSecondarySchoolCertified)
                {
                    if (requestId != 0 && !string.IsNullOrEmpty(fileUploadNationalService.Group))
                        BL.PARequestsAttachments.DeleteRequestsAttachmentsByGroupAndRequestID(requestId, fileUploadNationalService.Group);
                }
            }
            else
            {
                sectionSecondaryCertificate.Visible =
                    fileUploadNationalService.IsRequired =
                    schoolDocuments.IsRequired = false;

                divUniversitiesDetails.Visible =
                universityDocuments.IsRequired = false;
            }
        }

        private List<string> getCertificateYears()
        {
            int StartYear = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "AcademicStudyStart"));
            int EndYear = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "AcademicStudyEnd"));
            int nextYear = StartYear + 1;

            List<string> yearsList = new List<string>();

            for (int i = StartYear; i < EndYear; i++)
            {
                nextYear = i + 1;
                yearsList.Add(i + " - " + nextYear);
            }

            return yearsList;
        }

        //protected void rdbJoinedOtherUni_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Logging.GetInstance().Debug("Entering method PANewRequestUserControl.rdbJoinedOtherUni_SelectedIndexChanged");

        //    try
        //    {
        //        //if (rdbJoinedOtherUni.SelectedIndex == 0)
        //        //{
        //        //    fileUploadGrades.Visible = true;
        //        //    fileUploadAcceptedHours.Visible = true;
        //        //}
        //        else
        //        {
        //            fileUploadGrades.Visible = false;
        //            fileUploadAcceptedHours.Visible = false;
        //            int requestId = 0;
        //            if (Page.Session["PANewRequestId"] != null)
        //            {
        //                requestId = int.Parse(Convert.ToString(Page.Session["PANewRequestId"]));
        //            }
        //            else if (Page.Session["PAEditRequestId"] != null)
        //            {
        //                requestId = int.Parse(Convert.ToString(Page.Session["PAEditRequestId"]));
        //            }
        //            BL.PARequestsAttachments.DeleteRequestsAttachmentsByGroupAndRequestID(requestId, fileUploadGrades.Group);
        //            BL.PARequestsAttachments.DeleteRequestsAttachmentsByGroupAndRequestID(requestId, fileUploadAcceptedHours.Group);
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        Logging.GetInstance().LogException(ex);
        //    }

        //    finally
        //    {
        //        Logging.GetInstance().Debug("Exiting method PANewRequestUserControl.rdbJoinedOtherUni_SelectedIndexChanged");
        //    }
        //}

        //protected void custJoinedOtherUni_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    args.IsValid = rdbJoinedOtherUni.SelectedIndex > -1;
        //}

        protected void lnkDelegateDisplay_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PANewRequestUserControl.lnkDelegateDisplay_Click");

            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                RepeaterItem repItem = (RepeaterItem)lnkButton.NamingContainer;
                HiddenField hdnFileExtension = (HiddenField)repItem.FindControl("hdnFileExtension");
                Common.Utilities.DisplayedFile displayFile = new Common.Utilities.DisplayedFile()
                {
                    DocLibWebUrl = SPContext.Current.Site.Url,
                    ItemID = int.Parse(lnkButton.CommandArgument),
                    DocumentLibraryName = Common.Utilities.Constants.DelegationTemplate,
                    DownloadableName = lnkButton.Text + hdnFileExtension.Value,
                    IsDownloadable = true
                };
                Session["DisplayFile"] = displayFile;

                SPUtility.Redirect("/_layouts/15/ITWORX.MOEHEWF.Common/DisplayFile.aspx", SPRedirectFlags.DoNotEndResponse, System.Web.HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method NewRequestUserControl.lnkDelegateDisplay_Click");
            }
        }

        protected void btnErrorOk_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PARequestDetails.btnModalOK_Click");

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
                Logging.GetInstance().Debug("Exiting method PARequestDetails.btnModalOK_Click");
            }
        }

        internal void Validate()
        {
            Logging.GetInstance().Debug("Enter RequestDetails.Validate");
            try
            {
                if (rdbWorkingOrNot.SelectedValue == "1")
                {
                    // reqOccupation.Enabled = true;
                    //reqEntityNeedsEquivalency.Enabled = false;

                    if (ddlEntityWorkingFor.SelectedValue == "New")
                    {
                        ddlEntityWorkingFor.ReqNewOptionText.Enabled = true;
                        ddlEntityWorkingFor.ReqDropWithNewOption.Enabled = false;
                    }
                    else
                    {
                        ddlEntityWorkingFor.ReqDropWithNewOption.Enabled = true;
                        ddlEntityWorkingFor.ReqNewOptionText.Enabled = false;
                    }
                }
                if (!string.IsNullOrEmpty(ddlHighestCertificate.SelectedItem.Text))
                {
                    string secondarySchool = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "SecondarySchool", (uint)LCID).ToLower();
                    bool isSecondarySchoolCertified = (ddlHighestCertificate.SelectedItem.Text.ToLower() == secondarySchool);
                    //For Readability
                    bool isUniversity = !isSecondarySchoolCertified;

                    if (isUniversity)
                    {
                        if (ddlUniversity.SelectedValue == "New")
                        {
                            ddlUniversity.ReqNewOptionText.Enabled = true;
                            ddlUniversity.ReqDropWithNewOption.Enabled = false;
                        }
                        else if (string.IsNullOrEmpty(ddlUniversity.SelectedValue))
                        {
                            ddlUniversity.ReqDropWithNewOption.Enabled = true;
                            ddlUniversity.ReqNewOptionText.Enabled = false;
                        }

                        if (ddlSpecialization.SelectedValue == "New")
                        {
                            ddlSpecialization.ReqNewOptionText.Enabled = true;
                            ddlSpecialization.ReqDropWithNewOption.Enabled = false;
                        }
                        else if (string.IsNullOrEmpty(ddlSpecialization.SelectedValue))
                        {
                            ddlSpecialization.ReqDropWithNewOption.Enabled = true;
                            ddlSpecialization.ReqNewOptionText.Enabled = false;
                        }
                    }
                    if (ddlProgramSpecialization.SelectedValue == "New")
                    {
                        ddlProgramSpecialization.ReqNewOptionText.Enabled = true;

                        ddlProgramSpecialization.ReqDropWithNewOption.Enabled = false;
                    }
                    else if (string.IsNullOrEmpty(ddlProgramSpecialization.SelectedValue))
                    {
                        ddlProgramSpecialization.ReqDropWithNewOption.Enabled = true;

                        ddlProgramSpecialization.ReqNewOptionText.Enabled = false;
                    }

                    if (isSecondarySchoolCertified)
                    {
                        ddlCertificateYears.Visible = true;
                        txtCertificateDate.Visible = false;
                        txtCertificateDate.Value = string.Empty;
                        reqCertificateDate.ControlToValidate = "ddlCertificateYears";

                        reqtxtCertificateDate.Enabled = false;
                        reqtxtCertificateDate.ValidationGroup = "";

                        int MandatoryYear = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "AcademicStudyMandatoryYear"));

                        int selectedYear = Convert.ToInt32(ddlCertificateYears.SelectedItem.ToString().Split('-')[0]);
                        if (selectedYear >= MandatoryYear)
                        {
                            fileUploadNationalService.IsRequired = true;
                        }
                        else
                        {
                            fileUploadNationalService.IsRequired = false;
                        }
                    }
                    else
                    {
                        ddlCertificateYears.Visible = false;
                        txtCertificateDate.Visible = true;
                        //reqCertificateDate.ControlToValidate = "txtCertificateDate";
                        reqtxtCertificateDate.Enabled = true;
                        reqtxtCertificateDate.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit RequestDetails.Validate");
            }
        }
    }
}