using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.UCE.Entities;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;
using System.Xml;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class RequestDetails : UserControlBase
    {
        private int rowIndex = 0;
          
        #region ProtectedVariables
        protected int StudyYear = 0;
        protected string yesValue = "1";
        protected string noValue = "0";
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlUniversity;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlCountry;
        
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlSpecialization;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlMainCountry;
        
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlEntityWorkingFor;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlEntityNeedsEquivalency;


        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlCalcSectionCountry;
         protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlAttachmentCat;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox ddlCalcSectionUniversity;
       
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadWorking;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadNotWorking;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCertificates;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCalcDetails;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadDelegates;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCertificateEquivalent;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadGrades;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadSequenceCert;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadDiploma;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadInterDiploma;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadHavePA;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadNoHavePA;
        #endregion
        public bool HideResubmit { set; get; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering RequestDetails.Page_Load");
            try
            {
                //Bind Delegation Template to grid 
                rowIndex = 0;

                StudyYear = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "StudyYear"));

                ddlCalcSectionCountry.DropWithNewOption.SelectedIndexChanged += new EventHandler(ddlCalcSectionCountry_SelectedIndexChanged);
                ddlCountry.DropWithNewOption.SelectedIndexChanged += new EventHandler(ddlCountry_SelectedIndexChanged);

                 
                if (HideResubmit == true)
                {
                    btnResubmit.Visible = false;
                }
                if (dropCertificateAcademicDegree.SelectedIndex == 0)
                {
                    lblPreviousCertificates.Visible = true;
                }
                else
                    lblPreviousCertificates.Visible = false;

                if (!Page.IsPostBack)
                {
                   
                    List<DelegationTemplate> delegationTemplates = BL.DelegationTemplate.GetDelegationTemplatesData(LCID);
                    if (delegationTemplates != null && delegationTemplates.Count != 0)
                    {
                        repDelegatesTemplates.DataSource = delegationTemplates;
                        repDelegatesTemplates.DataBind();

                    }
                  
                    PopulateDropdowns();
                    FillDropdownsWithNewOption();
                     
                    rdbCertificateThroughScholarship.Items.Add(new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Yes", (uint)LCID), yesValue));
                    rdbCertificateThroughScholarship.Items.Add(new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "No", (uint)LCID), noValue));
                    rdbWorkingOrNot.Items.Add(new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Yes", (uint)LCID), yesValue));
                    rdbWorkingOrNot.Items.Add(new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "No", (uint)LCID), noValue));

                    radioBtnHavePA.Items.Add(new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Yes", (uint)LCID), yesValue));
                    radioBtnHavePA.Items.Add(new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "No", (uint)LCID), noValue));

                    radiobtnlstUniversity.Items.Add(new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Government", (uint)LCID), "Government"));
                    radiobtnlstUniversity.Items.Add(new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Private", (uint)LCID), "Private"));

                    //This will be removed when session is used
                    if ((SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage) || SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage))
                        && Page.Session["EditRequestId"] != null)  
                    {
                        BindRequestDataForEditMode();
                    }
                     

                }

                GetUniversityData();

                BindAttachments();
 
               dropCertificateAcademicDegree_SelectedIndexChanged(sender, e);
               ddlUniversity_SelectedIndexChanged(sender, e);

            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit RequestDetails.Page_Load");
            }

        } 

        #region RetrieveData
        //To get the data of country, university and university list
        // in case year is >2015
        private void GetUniversityData()
        {

            Logging.GetInstance().Debug("Entering RequestDetails.GetUniversityData");
            Entities.Request requestItem = null;
            try
            {

                if (Page.Session["NewRequestId"] != null || Page.Session["EditRequestId"]!=null )
                {
                    int requestId = 0;
                    if (Page.Session["NewRequestId"] != null)
                    {
                         requestId = int.Parse(Convert.ToString(Page.Session["NewRequestId"]));
                    }
                    else if (Page.Session["EditRequestId"] != null)
                    {
                         requestId = int.Parse(Convert.ToString(Page.Session["EditRequestId"]));
                    }
               
                   
                         

                    requestItem = BL.Request.GetUniversityRequestData(requestId, LCID);
                    ViewState["Year"] = requestItem.Year;
                    if (requestItem != null)
                    {
                        lblRequestNumberValue.Text = requestItem.RequestNumber;
                        lblRequestCreationDateValue.Text = requestItem.RequestCreationDate != DateTime.MinValue ? requestItem.RequestCreationDate.ToShortDateString() : string.Empty;

                        if (requestItem.Year >= StudyYear)
                        {
                            //Set dropdown by visible of user control and country 
                            //Check if year in first screen >2015 
                            //set lbl by values 

                            ddlCountry.DropWithNewOption.ClearSelection();
                            ddlMainCountry.DropWithNewOption.ClearSelection();
                            ddlCountry.DropWithNewOption.Visible = false;
                           
                            //lblCountries.Visible = false;
                            //spanCountry.Visible = false;
                            if (requestItem.CountryOfStudy != null && !string.IsNullOrEmpty(requestItem.CountryOfStudy.SelectedTitle))
                            {
                                lblCountriesValue.Visible = true;
                                txtCountriesValue.Visible = true;
                                txtCountriesValue.Text = requestItem.CountryOfStudy.SelectedTitle;
                                ddlUniversityDiv.Visible = false;                               
                                mainCountry.Visible = false;
                                lblMainCountry.Visible = true;
                                txtMainCountry.Visible = true;
                                txtMainCountry.Text = requestItem.UniversityMainHeadquarter!=null? requestItem.UniversityMainHeadquarter.SelectedTitle:string.Empty;                             
                               //ddlUniversity.SetVisibility(false);
                                lblUniversity.Visible = true;
                                txtUniversityValue.Visible = true;
                                txtUniversityValue.Text = requestItem.University.SelectedTitle;
                                /* Uni List must noit appear
                                uniList.Visible = true;
                                txtUniversityListValue.Visible = true;
                                txtUniversityListValue.Text = requestItem.UniversityList; */
                                if (Common.BL.University.IsUniversityHEDD(int.Parse(requestItem.University.SelectedID)))
                                {
                                    lblUniversityHEDD.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "HEDDMessage", (uint)LCID), requestItem.University.SelectedTitle);
                                    lblUniversityCHED.Text = string.Empty;

                                }
                                if (Common.BL.University.IsUniversityCHED(int.Parse(requestItem.University.SelectedID)))
                                {
                                    lblUniversityCHED.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CHEDMessage", (uint)LCID), requestItem.University.SelectedTitle);
                                    lblUniversityHEDD.Text = string.Empty;
                                }
                            }

                        }
                        else
                        {
                            ddlCountry.DropWithNewOption.Visible = true;
                            lblUniversityCHED.Text = string.Empty;
                            txtCountriesValue.Text = string.Empty;
                            txtCountriesValue.Visible = false;
                            txtUniversityValue.Text = string.Empty;
                            txtUniversityValue.Visible = false;
                            txtUniversityListValue.Text = string.Empty;
                            ddlUniversityDiv.Visible = true;
                            mainCountry.Visible = true;      
                            lblMainCountry.Visible = false;
                            txtMainCountry.Visible = false;
                            lblUniversity.Visible = false;
                            uniList.Visible = false;
                            txtUniversityListValue.Visible = false;
                            lblCountriesValue.Visible = false;
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

                Logging.GetInstance().Debug("Exit RequestDetails.GetUniversityData");
            }

        }


        private void BindRequestDataForEditMode()
        {
            //if (Page.Session["RequestNumber"] == null)
            //{
            Logging.GetInstance().Debug("Entering RequestDetails.GetRequestDataForEditMode");
            try
            {

                int requestId = int.Parse(Convert.ToString(Page.Session["EditRequestId"]));
                Entities.Request requestItem = BL.Request.GetRequestByNumber(requestId, LCID);
                if (requestItem != null)
                {
                    // pnlformControls.Visible = true;
                    // lblNoRequest.Visible = false;

                    if (!string.IsNullOrEmpty(requestItem.RequestNumber))
                    {
                        lblRequestNumberValue.Text = requestItem.RequestNumber.ToString();
                    }
                    lblRequestCreationDateValue.Text = requestItem.RequestCreationDate != DateTime.MinValue ? requestItem.RequestCreationDate.ToShortDateString() : string.Empty;
                    if (requestItem.AcademicDegree != null && !string.IsNullOrEmpty(requestItem.AcademicDegree.SelectedID))
                    {
                        dropAcademicDegreeForEquivalence.SelectedValue = requestItem.AcademicDegree.SelectedID;
                    }
                    if (requestItem.AcademicDegreeForEquivalence != null && !string.IsNullOrEmpty(requestItem.AcademicDegreeForEquivalence.SelectedID))
                    {
                        dropCertificateAcademicDegree.SelectedValue = requestItem.AcademicDegreeForEquivalence.SelectedID;

                    }
                   
                    rdbCertificateThroughScholarship.Items.FindByValue(requestItem.CertificateThroughScholarship ? yesValue : noValue).Selected = true;

                    //handle this section in edit mode
                    //check here 

                    if (!string.IsNullOrEmpty(requestItem.EntityProvidingStudy))
                    {
                        txtEntityProvidingStudy.Text = requestItem.EntityProvidingStudy; 
                    } 
                    if (requestItem.Year >= StudyYear)
                    {
                        //Set university list, university and country here in drop 
                        ddlCountry.DropWithNewOption.Visible = false;
                       // lblCountries.Visible = false;
                       // spanCountry.Visible = false; 
                        ddlUniversityDiv.Visible = false;
                        mainCountry.Visible = false;
                        //  ddlUniversity.SetVisibility(false);
                        if (requestItem.CountryOfStudy != null && !string.IsNullOrEmpty(requestItem.CountryOfStudy.SelectedTitle))
                        {
                            txtCountriesValue.Visible = true;
                            txtCountriesValue.Text = requestItem.CountryOfStudy.SelectedTitle;
                        }
                        if (requestItem.University != null && !string.IsNullOrEmpty(requestItem.University.SelectedTitle))
                        {
                            lblUniversity.Visible = true;
                            txtUniversityValue.Visible = true;
                            txtUniversityValue.Text = requestItem.University.SelectedTitle;
                        }

                        if (requestItem.UniversityMainHeadquarter != null && !string.IsNullOrEmpty(requestItem.UniversityMainHeadquarter.SelectedID))
                        {
                            lblMainCountry.Visible = true;
                            txtMainCountry.Visible = true;
                            txtMainCountry.Text = requestItem.UniversityMainHeadquarter.SelectedID;
                        }
                    }
                    else
                    {
                        //Set university list, university and country here in lbl 
                        ddlCountry.DropWithNewOption.Visible = true; 
                        ddlUniversityDiv.Visible = true;
                        mainCountry.Visible = true;
                        

                        // ddlUniversity.SetVisibility(true);
                        if (requestItem.CountryOfStudy != null  )
                        {
                            if (!string.IsNullOrEmpty(requestItem.CountryOfStudy.SelectedID))
                            {
                                ddlCountry.DropWithNewOption.SelectedValue = requestItem.CountryOfStudy.SelectedID;

                                List<University> university = Common.BL.University.GetDistinctUniversityByYearAndCountry(0, int.Parse(requestItem.CountryOfStudy.SelectedID), LCID);
                                ddlUniversity.DropWithNewOption.Items.Clear();
                                ddlUniversity.DropListItems = university;
                                ddlUniversity.DataValueField = "ID";
                                ddlUniversity.DataTextEnField = "EnglishTitle";
                                ddlUniversity.DataTextArField = "ArabicTitle";
                                ddlUniversity.BindDataSource();
                                if (requestItem.University != null && !string.IsNullOrEmpty(requestItem.University.SelectedID))
                                {

                                    ddlUniversity.SelectedValue = requestItem.University.SelectedID;
                                    if (Common.BL.University.IsUniversityHEDD(int.Parse(requestItem.University.SelectedID)))
                                    {
                                        lblUniversityHEDD.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "HEDDMessage", (uint)LCID), requestItem.University.SelectedTitle);
                                        lblUniversityCHED.Text = string.Empty;
                                    }
                                    if (Common.BL.University.IsUniversityCHED(int.Parse(requestItem.University.SelectedID)))
                                    {
                                        lblUniversityCHED.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CHEDMessage", (uint)LCID), requestItem.University.SelectedTitle);
                                        lblUniversityHEDD.Text = string.Empty;
                                    }

                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(requestItem.UniversityNotFoundInList))
                                    {

                                        ddlUniversity.SelectedValue = "New";
                                        ddlUniversity.NewOptionText = requestItem.UniversityNotFoundInList;
                                    }
                                }

                            }
                            else
                            {
                                ddlCountry.SelectedValue = "New";
                                ddlCountry.NewOptionText = requestItem.CountryOfStudy.SelectedTitle;
                                ddlUniversity.DropWithNewOption.Items.Clear();
                                ddlUniversity.DropListItems = "New";
                                ddlUniversity.BindDataSource();

                                if (!string.IsNullOrEmpty(requestItem.UniversityNotFoundInList))
                                {

                                    ddlUniversity.SelectedValue = "New";
                                    ddlUniversity.NewOptionText = requestItem.UniversityNotFoundInList;
                                }
                            }

                            
                           

                        }
                       
                        
                        else
                        {
                            ddlUniversity.DropWithNewOption.Items.Clear();
                            ddlUniversity.DropListItems = null;
                            ddlUniversity.BindDataSource();
                        }
                        if (!string.IsNullOrEmpty(requestItem.UniversityList))
                        {
                            /* Uni List must not appear
                            uniList.Visible = true;
                            txtUniversityListValue.Visible = true;
                            txtUniversityListValue.Text = requestItem.UniversityList;*/

                        }


                        if (requestItem.UniversityMainHeadquarter != null && !string.IsNullOrEmpty(requestItem.UniversityMainHeadquarter.SelectedID))
                        {

                            ddlMainCountry.SelectedValue = requestItem.UniversityMainHeadquarter.SelectedID;
                        }
                        else if (!string.IsNullOrEmpty(requestItem.NewUniversityHeadquarter))
                        {
                            ddlMainCountry.SelectedValue = "New";
                            ddlMainCountry.NewOptionText = requestItem.NewUniversityHeadquarter;
                        }
                    }
                   
                    if (!string.IsNullOrEmpty(requestItem.FacultyNotFoundInList) || !string.IsNullOrEmpty(requestItem.UniversityNotFoundInList))
                    {

                        //ddlFaculty.DropWithNewOption.Items.Clear();

                        //ddlFaculty.DropListItems = "New";
                        //ddlFaculty.BindDataSource();

                        //if (!string.IsNullOrEmpty(requestItem.FacultyNotFoundInList))
                        //{
                        //    ddlFaculty.SelectedValue = "New";
                        //    ddlFaculty.NewOptionText = requestItem.FacultyNotFoundInList;
                        //}
                    }
                    else if (requestItem.University != null && !string.IsNullOrEmpty(requestItem.University.SelectedID))
                    {
                        //List<Faculty> faculty = BL.Faculty.GetAll();//.GetFacultyByUniversityId(int.Parse(requestItem.University.SelectedID));
                        //ddlFaculty.DropWithNewOption.Items.Clear();
                        //ddlFaculty.DropListItems = faculty;
                        //ddlFaculty.DataValueField = "ID";
                        //ddlFaculty.DataTextEnField = "EnglishTitle";
                        //ddlFaculty.DataTextArField = "ArabicTitle";
                        //ddlFaculty.BindDataSource();
                        //if (requestItem.Faculty != null && !string.IsNullOrEmpty(requestItem.Faculty.SelectedID))
                        //{
                        //    ddlFaculty.SelectedValue = requestItem.Faculty.SelectedID;
                        //}
                    }
                    else
                    {
                        //ddlFaculty.DropWithNewOption.Items.Clear();
                        //ddlFaculty.DropListItems = null;
                        //ddlFaculty.BindDataSource();
                    }



                    //if (requestItem.Faculty != null)
                    //{
                        if (!string.IsNullOrEmpty(requestItem.SpecializationNotFoundInList) /*|| !string.IsNullOrEmpty(requestItem.UniversityNotFoundInList)*/)
                    {
                        ddlSpecialization.DropWithNewOption.Items.Clear();
                        ddlSpecialization.DropListItems = "New";
                        ddlSpecialization.BindDataSource();

                        if (!string.IsNullOrEmpty(requestItem.SpecializationNotFoundInList))
                        {
                            ddlSpecialization.SelectedValue = "New";
                            ddlSpecialization.NewOptionText = requestItem.SpecializationNotFoundInList;
                        }
                    }
                    else/* if (requestItem.Faculty != null )*/
                    {
                        List<Specialization> specialization = BL.Specialization.GetAll();//.GetSpecializationByFacultyId(int.Parse(requestItem.Faculty.SelectedID));
                        ddlSpecialization.DropWithNewOption.Items.Clear();
                        ddlSpecialization.DropListItems = specialization;
                        ddlSpecialization.DataValueField = "ID";
                        ddlSpecialization.DataTextEnField = "EnglishTitle";
                        ddlSpecialization.DataTextArField = "ArabicTitle";
                        ddlSpecialization.BindDataSource();
                        if (requestItem.Specialization != null && !string.IsNullOrEmpty(requestItem.Specialization.SelectedID))
                        {
                            ddlSpecialization.SelectedValue = requestItem.Specialization.SelectedID;
                        }
                    }
                    /*else
                    {

                        ddlSpecialization.DropWithNewOption.Items.Clear();
                        ddlSpecialization.DropListItems = null;
                        ddlSpecialization.BindDataSource();
                    }*/
                    //}
                    if (!string.IsNullOrEmpty(requestItem.Faculty))
                    {
                        ddlFaculty.Text = requestItem.Faculty;
                    }
                    if (requestItem.StudyLanguage != null && !string.IsNullOrEmpty(requestItem.StudyLanguage.SelectedID))
                    {
                        dropStudyingLanguage.SelectedValue = requestItem.StudyLanguage.SelectedID;
                    }
                   // dropStudyingType.SelectedValue = requestItem.StudyType.SelectedID;
                    if (requestItem.StudySystem != null)
                    {
                        dropStudyingSystem.SelectedValue = requestItem.StudySystem.SelectedID;
                    }
                    if (requestItem.StudyStartDate != DateTime.MinValue)
                    {
                        txtStartDate.Text = ExtensionMethods.QatarFormatedDate(requestItem.StudyStartDate);
                    }
                    if (requestItem.StudyGraduationDate != DateTime.MinValue)
                    {
                        txtGraduationDate.Text = ExtensionMethods.QatarFormatedDate(requestItem.StudyGraduationDate);
                    }
                    //txtStudyPeriod.Text = requestItem.AcademicProgramPeriod.ToString();
                    //txtActualStudy.Text = requestItem.ActualStudingPeriod.ToString();
                    //if (requestItem.NumberOfHoursGained != 0)
                    //{
                    //    txtGainedHours.Text = requestItem.NumberOfHoursGained.ToString();
                    //}
                    //txtGPA.Text = requestItem.GPA;
                    //if (requestItem.NumberOfOnlineHours != 0)
                    //{
                    //    txtOnlineHours.Text = requestItem.NumberOfOnlineHours.ToString();
                    //}
                    //txtOnlinePercentage.Text = requestItem.PercentageOfOnlineHours;
                    //chkCompExam.Checked = requestItem.IsThereComprehensiveExam;
                    //chkAcceptExam.Checked = requestItem.IsThereAcceptanceExam;
                 
                    if (!string.IsNullOrEmpty(requestItem.UniversityAddress))
                    {
                        txtAddress.Text = requestItem.UniversityAddress;
                    }
                    if (!string.IsNullOrEmpty(requestItem.UniversityEmail))
                    {
                        txtUniversityEmail.Text = requestItem.UniversityEmail;
                    }
                    rdbWorkingOrNot.Items.FindByValue(requestItem.WorkingOrNot ? yesValue : noValue).Selected = true;
                    radioBtnHavePA.Items.FindByValue(requestItem.HavePAOrNot ? yesValue : noValue).Selected = true;
                    radiobtnlstUniversity.Items.FindByValue(requestItem.UniversityType).Selected = true;
                    if (requestItem.WorkingOrNot == true)
                    {
                        // set drop of occupation and entity working for
                        //lblWorkPhone.Visible = true;
                        //txtWorkPhone.Visible = true;
                        //txtWorkPhone.Text = requestItem.OccupationPhone;
                        //lblHiringDate.Visible = true;
                        //// dtHiringDate.Visible = true;
                        //if (requestItem.HiringDate != DateTime.MinValue)
                        //{
                        //    dtHiringDate.Value = ExtensionMethods.QatarFormatedDate(requestItem.HiringDate);
                        //}

                        if (requestItem.EntityWorkingFor != null && !string.IsNullOrEmpty(requestItem.EntityWorkingFor.SelectedID))
                        {

                            ddlEntityWorkingFor.SelectedValue = requestItem.EntityWorkingFor.SelectedID;
                        }
                        else if (!string.IsNullOrEmpty(requestItem.OtherEntityWorkingFor))
                        {
                            ddlEntityWorkingFor.SelectedValue = "New";
                            ddlEntityWorkingFor.NewOptionText = requestItem.OtherEntityWorkingFor;
                        }
                        //if (!string.IsNullOrEmpty(requestItem.Occupation))
                        //{
                        //    txtOccupation.Text = requestItem.Occupation;
                        //    //ddlOccupation.SelectedValue = requestItem.Occupation.SelectedID;
                        //}
                        //else if (!string.IsNullOrEmpty(requestItem.OtherOccupation))
                        //{
                        //    ddlOccupation.SelectedValue = "New";
                        //    ddlOccupation.NewOptionText = requestItem.OtherOccupation;
                        //}
                    }

                    //Ahmed
                    //else
                    //{
                        if (requestItem.EntityNeedsEquivalency != null && !string.IsNullOrEmpty(requestItem.EntityNeedsEquivalency.SelectedID))
                        {

                        ddlEntityNeedsEquivalency.SelectedValue = requestItem.EntityNeedsEquivalency.SelectedID;
                        }
                        //Ahmed
                        else if (!string.IsNullOrEmpty(requestItem.OtherEntityNeedsEquivalency))
                        {
                        ddlEntityNeedsEquivalency.SelectedValue = "New";
                        ddlEntityNeedsEquivalency.NewOptionText = requestItem.OtherEntityNeedsEquivalency;
                        }
                    //else if (!string.IsNullOrEmpty(requestItem.OtherEntityNeedsEquivalency))
                    //{
                    //    ddlEntityNeedsEquivalency.SelectedValue = "New";
                    //    ddlEntityNeedsEquivalency.NewOptionText = requestItem.OtherEntityNeedsEquivalency;
                    //}
                    // }
                    //if (requestItem.BookDate != DateTime.MinValue)
                    //{
                    //    dtDate.Value = ExtensionMethods.QatarFormatedDate(requestItem.BookDate);

                    //}
                    //if (requestItem.IncomingNumber != 0)
                    //{
                    //    txtIncomingNumber.Text = requestItem.IncomingNumber.ToString();
                    //}



                    //txtBarCode.Text = requestItem.BarCode;

                    //Bind calculated certificate repeater
                    List<CalculatedDetailsForCertificate> calculatedDetailsData = BL.CalculatedDetailsForCertificate.GetCalculatedDetailsForCertificate(requestId, LCID);
                    if (calculatedDetailsData != null && calculatedDetailsData.Count > 0)
                    {


                        repCalculatedDetailsForCertificate.DataSource = calculatedDetailsData;
                        repCalculatedDetailsForCertificate.DataBind();
                    }
                    //List<OrganizationlLettersAttachments> organizationItems = BL.OrganizationlLettersAttachments.GetOrganizationLetterData(requestItem.ID);
                    //HelperMethods.BindGridView(gridCopyOfOrganizationLetter, organizationItems);

                }
                //else
                //{
                //    pnlformControls.Visible = false;
                //    lblNoRequest.Visible = true;
                //}
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit RequestDetails.GetRequestDataForEditMod");
            }
        }

        #endregion

        #region CommonFunction
   
        #endregion
        #region Drop
        public void FillDropdownsWithNewOption()
        {
            Logging.GetInstance().Debug("Enter RequestDetails.FillDropdownsWithNewOption");
            try
            { 
                List<Faculty> facultyItems = BL.Faculty.GetAll();
                List<Specialization> specializationItems = BL.Specialization.GetAll();
                List<UniversityMainCountry> mainCountryItems = BL.UniversityMainCountry.GetAll(); 
                List<EntityWorkingFor> entityWorkingForItems = BL.EntityWorkingFor.GetAll();
                List<Occupation> occupationItems = BL.Occupation.GetAll();
                List<EntityProvidingStudy> entityProvidingStudyItems = BL.EntityProvidingStudy.GetAll();
                List<CountryOfStudy> countryItems = Common.BL.CountryOfStudy.GetAll(LCID);
                List<Specialization> specialization = BL.Specialization.GetAll();

                //Ahmed
                List<EntityNeedsEquivalency> entityNeedsEquivalencyItems = BL.EntityNeedsEquivalency.GetAll();
                ddlAttachmentCat.LblText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CertificateName", (uint)LCID);
                ddlAttachmentCat.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)LCID);
                 
                ddlAttachmentCat.ReqErrorAstrik = true;
                ddlAttachmentCat.RequiredDropText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredCertificateName", (uint)LCID);
                ddlAttachmentCat.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredOtherCertificate", (uint)LCID);
                ddlAttachmentCat.ValidationGroup = "Upload";



                ddlUniversity.DropListItems = null;
                ddlUniversity.BindDataSource();
                ddlUniversity.LblText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "University", (uint)LCID);
                ddlUniversity.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)LCID);
                ddlUniversity.ReqErrorAstrik = true; 
                ddlUniversity.RequiredDropText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredUniversity", (uint)LCID);
                ddlUniversity.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredNewUniversity", (uint)LCID);
                ddlUniversity.ValidationGroup = "Submit";
                ddlUniversity.DropWithNewOption.AutoPostBack = true;

                ddlCountry.LblText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CountryOfStudy", (uint)LCID);
                ddlCountry.ReqErrorAstrik = true;
                ddlCountry.DropListItems = countryItems;
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataTextEnField = "EnglishTitle";
                ddlCountry.DataTextArField = "ArabicTitle";
                ddlCountry.BindDataSource();
                ddlCountry.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)LCID);
                ddlCountry.RequiredDropText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredState", (uint)LCID);
                ddlCountry.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredNewCountry", (uint)LCID);
                ddlCountry.ValidationGroup = "Submit";
                ddlCountry.DropWithNewOption.AutoPostBack = true;
               
              

                ddlSpecialization.DropListItems = specialization;
                ddlSpecialization.DataValueField = "ID";
                ddlSpecialization.DataTextEnField = "EnglishTitle";
                ddlSpecialization.DataTextArField = "ArabicTitle";
                ddlSpecialization.BindDataSource();
                ddlSpecialization.LblText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Specialization", (uint)LCID);
                
                ddlSpecialization.ReqErrorAstrik = true;
                ddlSpecialization.RequiredDropText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredSpecialization", (uint)LCID);
                ddlSpecialization.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)LCID);
                ddlSpecialization.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredNewSpecialization", (uint)LCID);
                ddlSpecialization.ValidationGroup = "Submit";
 

                ddlMainCountry.DropListItems = mainCountryItems;
                ddlMainCountry.DataValueField = "ID";
                ddlMainCountry.DataTextEnField = "EnglishTitle";
                ddlMainCountry.DataTextArField = "ArabicTitle";
                ddlMainCountry.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)LCID);
                ddlMainCountry.BindDataSource(); 
                ddlMainCountry.ReqErrorAstrik = true;
                ddlMainCountry.LblText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "UniversityMainHeadquarter", (uint)LCID);
                ddlMainCountry.RequiredDropText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredUniversityMainHeadquarter", (uint)LCID);
                ddlMainCountry.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "OtherRequiredUniversityMainHeadquarter", (uint)LCID);
                ddlMainCountry.ValidationGroup = "Submit";
 

                ddlEntityWorkingFor.DropListItems = entityWorkingForItems;
                ddlEntityWorkingFor.DataValueField = "ID";
                ddlEntityWorkingFor.DataTextEnField = "EnglishTitle";
                ddlEntityWorkingFor.DataTextArField = "ArabicTitle";
                ddlEntityWorkingFor.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)LCID);
                ddlEntityWorkingFor.BindDataSource(); 
                ddlEntityWorkingFor.ReqErrorAstrik = true;
                ddlEntityWorkingFor.LblText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "EntityWorkingFor", (uint)LCID);
                ddlEntityWorkingFor.RequiredDropText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredEntityWorkingFor", (uint)LCID);
                ddlEntityWorkingFor.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredOtherEntityWorkingFor", (uint)LCID);
                ddlEntityWorkingFor.ValidationGroup = "Submit";

                //Ahmed                
                ddlEntityNeedsEquivalency.DropListItems = entityNeedsEquivalencyItems;
                ddlEntityNeedsEquivalency.DataValueField = "ID";
                ddlEntityNeedsEquivalency.DataTextEnField = "EnglishTitle";
                ddlEntityNeedsEquivalency.DataTextArField = "ArabicTitle";
                ddlEntityNeedsEquivalency.NewItemText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Other", (uint)LCID);
                ddlEntityNeedsEquivalency.BindDataSource();
                ddlEntityNeedsEquivalency.ReqErrorAstrik = true;
                ddlEntityNeedsEquivalency.LblText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "EntityNeedsEquivalency", (uint)LCID);
                ddlEntityNeedsEquivalency.RequiredDropText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredEntityNeedsEquivalency", (uint)LCID);
                ddlEntityNeedsEquivalency.RequiredTextboxText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredOtherEntityNeedsEquivalency", (uint)LCID);
                ddlEntityNeedsEquivalency.ValidationGroup = "Submit";

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
 
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit RequestDetails.FillDropdownsWithNewOption");
            }


        }
        internal void Validate()
        {
            Logging.GetInstance().Debug("Enter RequestDetails.Validate");
            try
            {
                //Ahmed

                if (ddlEntityNeedsEquivalency.SelectedValue == "New")
                {
                    ddlEntityNeedsEquivalency.ReqNewOptionText.Enabled = true;
                    ddlEntityNeedsEquivalency.ReqDropWithNewOption.Enabled = false;
                }
                else
                {
                    ddlEntityNeedsEquivalency.ReqDropWithNewOption.Enabled = true;
                    ddlEntityNeedsEquivalency.ReqNewOptionText.Enabled = false;
                }




                if (radioBtnHavePA.SelectedValue == "0")
                {
                    fileUploadHavePA.Visible = false;
                }
                else if (radioBtnHavePA.SelectedValue == "1")
                {
                    fileUploadNoHavePA.Visible = false;
                }
                if (rdbWorkingOrNot.SelectedValue == "0")
                {
                    //Ahmed
                    //reqEntityNeedsEquivalency.Enabled = true;
                    //reqOccupation.Enabled = false;
                    ddlEntityWorkingFor.ReqDropWithNewOption.Enabled = false;
                    ddlEntityWorkingFor.ReqNewOptionText.Enabled = false;
                   fileUploadWorking.Visible = false;
 

                }
                else if (rdbWorkingOrNot.SelectedValue == "1")
                {
                    // reqOccupation.Enabled = true;
                    //Ahmed
                    // reqEntityNeedsEquivalency.Enabled = false;
                   


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
                if (rdbCertificateThroughScholarship.SelectedValue == "1")
                {
                    reqEntityProvidingStudy.Enabled = true;
                }
                else if (rdbCertificateThroughScholarship.SelectedValue == "0")
                {
                    reqEntityProvidingStudy.Enabled = false;
                }
                if (ddlUniversity.SelectedValue == "New")
                {
                    ddlUniversity.ReqNewOptionText.Enabled = true;
                    ddlUniversity.ReqDropWithNewOption.Enabled = false;

                }
                else
                {
                    ddlUniversity.ReqNewOptionText.Enabled = false;
                    ddlUniversity.ReqDropWithNewOption.Enabled = true;

                }
                if (ddlSpecialization.SelectedValue == "New")
                {
                    ddlSpecialization.ReqNewOptionText.Enabled = true;
                    ddlSpecialization.ReqDropWithNewOption.Enabled = false;

                }
                else
                {
                    ddlSpecialization.ReqNewOptionText.Enabled = false;
                    ddlSpecialization.ReqDropWithNewOption.Enabled = true;

                }
                if (ddlMainCountry.SelectedValue == "New")
                {
                    ddlMainCountry.ReqNewOptionText.Enabled = true;
                    ddlMainCountry.ReqDropWithNewOption.Enabled = false;

                }
                else
                {
                    ddlMainCountry.ReqNewOptionText.Enabled = false;
                    ddlMainCountry.ReqDropWithNewOption.Enabled = true;

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

        private void PopulateDropdowns()
        {
            Logging.GetInstance().Debug("Enter RequestDetails.PopulateDropdowns");
            try
            {
                List<Entities.AcademicDegree> academicDegreeItems = BL.AcademicDegree.GetAll();
                List<CountryOfStudy> countryItems = Common.BL.CountryOfStudy.GetAll(LCID);
                List<StudyLanguage> studyLanguageItems = BL.StudyLanguage.GetAll();
                List<StudyType> studyTypeItems = BL.StudyType.GetAll();
                List<StudySystem> studySystemItems = BL.StudySystem.GetAll();
                List<EntityNeedsEquivalency> entityNeedsEquivalencyItems = BL.EntityNeedsEquivalency.GetAll();
                List<StudyAttachmentTypes> studyAttachTypes = BL.StudyAttachmentTypes.GetAll();

                
                dropAcademicDegreeForEquivalence.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), string.Empty));
                dropCertificateAcademicDegree.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), string.Empty));
                if (academicDegreeItems != null && academicDegreeItems.Count > 0)
                {

                    dropAcademicDegreeForEquivalence.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref dropAcademicDegreeForEquivalence, academicDegreeItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                    dropCertificateAcademicDegree.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref dropCertificateAcademicDegree, academicDegreeItems, "ID", "ArabicTitle", "EnglishTitle", LCID);

                } 

                dropStudyingLanguage.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), string.Empty));

                if (studyLanguageItems != null && studyLanguageItems.Count > 0)
                {
                    dropStudyingLanguage.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref dropStudyingLanguage, studyLanguageItems, "ID", "ArabicTitle", "EnglishTitle", LCID);

                }
                 
                dropStudyingSystem.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), string.Empty));

                dropCalcSectionStudyingSystem.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), string.Empty));
                if (studySystemItems != null && studySystemItems.Count > 0)
                {

                    dropStudyingSystem.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref dropStudyingSystem, studySystemItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                    dropCalcSectionStudyingSystem.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref dropCalcSectionStudyingSystem, studySystemItems, "ID", "ArabicTitle", "EnglishTitle", LCID);


                }

                //Ahmed
               // ddlEntityNeedsEquivalency.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), string.Empty));

                //if (entityNeedsEquivalencyItems != null && entityNeedsEquivalencyItems.Count > 0)
                //{
                //    ddlEntityNeedsEquivalency.AppendDataBoundItems = true;
                //    HelperMethods.BindDropDownList(ref ddlEntityNeedsEquivalency, entityNeedsEquivalencyItems, "ID", "ArabicTitle", "EnglishTitle", LCID);

                //}
                //////////
                divCalcAttachmentCategory.Visible = true;
                ddlCalcAttachmentCategory.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), string.Empty));
                if (studyAttachTypes != null && studyAttachTypes.Count > 0)
                {

                    ddlCalcAttachmentCategory.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref ddlCalcAttachmentCategory, studyAttachTypes, "ID", "ArabicTitle", "EnglishTitle", LCID);

                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit RequestDetails.PopulateDropdowns");
            }


        }
        #endregion

  
        protected void lnkDownloadCopy_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method RequestDetails.lnkDownloadCopy_Click");
            try
            {
                int itemId = int.Parse((sender as LinkButton).CommandArgument);
                HelperMethods.DisplayFile(SPContext.Current.Site.Url, itemId, Utilities.Constants.OrganizationalLettersAttachments, "", true);
       
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method RequestDetails.lnkDownloadCopy_Click");
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

        protected void lnkAddCalcDetails_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method RequestDetails.lnkAddCalcDetails_Click");
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
                         dropCalcSectionStudyingSystem.SelectedIndex > 0 || !string.IsNullOrEmpty(ddlCalcSectionFaculty.Text))
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
                            calcItem.Faculty = ddlCalcSectionFaculty.Text != null ? ddlCalcSectionFaculty.Text : string.Empty;
 
                            if (dropCalcSectionStudyingSystem.SelectedIndex > 0)
                            {
                                calcItem.StudySystem = new StudySystem()
                                {
                                    SelectedID = dropCalcSectionStudyingSystem.SelectedValue,
                                    SelectedTitle = dropCalcSectionStudyingSystem.SelectedItem.Text
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
                            calcList.Add(calcItem);


                            repCalculatedDetailsForCertificate.DataSource = calcList;
                            repCalculatedDetailsForCertificate.DataBind();

                            ddlCalcSectionCountry.DropWithNewOption.ClearSelection();
                            ddlCalcSectionUniversity.DropWithNewOption.ClearSelection();
                            ddlCalcSectionUniversity.DropWithNewOption.Items.Clear();
                            ddlCalcSectionUniversity.DropListItems = null;
                            ddlCalcSectionUniversity.BindDataSource(); 
                            ddlCalcSectionFaculty.Text = string.Empty; 
                            dropCalcSectionStudyingSystem.ClearSelection(); 
                            fileUploadCalcDetails.Group = (int.Parse(fileUploadCalcDetails.Group) + 1).ToString();
                            ViewState["FileUploadGroup"] = fileUploadCalcDetails.Group; 
                            fileUploadCalcDetails.Bind();
 
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {


                Logging.GetInstance().Debug("Exiting method RequestDetails.lnkAddCalcDetails_Click");
            }
        }


        #region SavingData
        public Entities.Request CreateSubmittedRequestDataObject()
        {
            Logging.GetInstance().Debug("Entering RequestDetails.CreateSubmittedRequestDataObject");
            Entities.Request request = new Entities.Request();
            try
            {



                request.SubmitDate = DateTime.Now;
                request.ActionDate = DateTime.Now;
                request.AcademicDegree = new Entities.AcademicDegree()
                {

                    SelectedID = dropAcademicDegreeForEquivalence.SelectedValue,
                    SelectedTitle = dropAcademicDegreeForEquivalence.SelectedItem.Text
                };

                request.AcademicDegreeForEquivalence = new Entities.AcademicDegree()
                {
                    SelectedID = dropCertificateAcademicDegree.SelectedValue,
                    SelectedTitle = dropCertificateAcademicDegree.SelectedItem.Text
                };

                request.CertificateThroughScholarship = rdbCertificateThroughScholarship.SelectedValue == yesValue ? true : false;

                //User control

                if (!string.IsNullOrEmpty(txtEntityProvidingStudy.Text))
                {
                    request.EntityProvidingStudy = txtEntityProvidingStudy.Text;
                } 
                request.StudyLanguage = new StudyLanguage()
                {
                    SelectedID = dropStudyingLanguage.SelectedValue,
                    SelectedTitle = dropStudyingLanguage.SelectedItem.Text
                }; 


                if (dropStudyingSystem.SelectedIndex != 0)
                {
                    request.StudySystem = new StudySystem()
                    {
                        SelectedID = dropStudyingSystem.SelectedValue,
                        SelectedTitle = dropStudyingSystem.SelectedItem.Text
                    };

                }
                if (!string.IsNullOrEmpty(txtStartDate.Text))
                {
                    request.StudyStartDate = Convert.ToDateTime(DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrEmpty(txtGraduationDate.Text))
                {
                    request.StudyGraduationDate = Convert.ToDateTime(DateTime.ParseExact(txtGraduationDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

                }
                 

                if (!string.IsNullOrEmpty(ddlCountry.DropWithNewOption.SelectedValue))
                {
                    if (ddlCountry.SelectedValue == "New")
                    { 
                        request.CountryOfStudy = new CountryOfStudy()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = ddlCountry.NewOptionText
                        };
                    }
                    else
                    {
                        request.CountryOfStudy = new CountryOfStudy()
                        {
                            SelectedID = ddlCountry.DropWithNewOption.SelectedValue,
                            SelectedTitle = ddlCountry.DropWithNewOption.SelectedItem.Text
                        };
                    }
                }

                //User Control

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
                            SelectedTitle = ddlSpecialization.SelectedText
                        };
                        request.SpecializationNotFoundInList = string.Empty;
                    }
                }


                if (!string.IsNullOrEmpty(ddlMainCountry.SelectedValue))
                {
                    if (ddlMainCountry.SelectedValue == "New")
                    {
                        request.NewUniversityHeadquarter = ddlMainCountry.NewOptionText;
                        request.UniversityMainHeadquarter = new UniversityMainCountry()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = string.Empty
                        };
                    }
                    else
                    {
                        request.UniversityMainHeadquarter = new UniversityMainCountry()
                        {
                            SelectedID = ddlMainCountry.SelectedValue,
                            SelectedTitle = ddlMainCountry.SelectedText
                        };
                        request.NewUniversityHeadquarter = string.Empty;
                    }
                }



                if (!string.IsNullOrEmpty(txtAddress.Text))
                {
                    request.UniversityAddress = txtAddress.Text;
                }
                if (!string.IsNullOrEmpty(txtUniversityEmail.Text))
                {
                    request.UniversityEmail = txtUniversityEmail.Text;
                }
                request.WorkingOrNot = rdbWorkingOrNot.SelectedValue == yesValue ? true : false;
                request.HavePAOrNot = radioBtnHavePA.SelectedValue == yesValue ? true : false;
                request.UniversityType = radiobtnlstUniversity.SelectedValue;
                checkHavePA();
                if (rdbWorkingOrNot.SelectedValue == yesValue)
                {   //User Control



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

                    //if (!string.IsNullOrEmpty(txtOccupation.Text))
                    //{
                    //    request.Occupation = txtOccupation.Text;
                    //}
 

                //Ahmed
                    //request.EntityNeedsEquivalency = new EntityNeedsEquivalency()
                    //{
                    //    SelectedID = string.Empty,
                    //    SelectedTitle = string.Empty
                    //};
                    //request.OtherEntityNeedsEquivalency = string.Empty;
                }

                else
                {             //User Control

                    //Ahmed
                    //if (!string.IsNullOrEmpty(ddlEntityNeedsEquivalency.SelectedValue))
                    //{

                    //    request.EntityNeedsEquivalency = new EntityNeedsEquivalency()
                    //    {
                    //        SelectedID = ddlEntityNeedsEquivalency.SelectedValue,
                    //        SelectedTitle = ddlEntityNeedsEquivalency.SelectedItem.Text
                    //    };
                    //}
 

                    request.EntityWorkingFor = new EntityWorkingFor()
                    {
                        SelectedID = string.Empty,
                        SelectedTitle = string.Empty
                    };
                    request.OtherEntityWorkingFor = string.Empty;
                  //  request.Occupation = string.Empty;
 
                }
                // Ahmed
                if (!string.IsNullOrEmpty(ddlEntityNeedsEquivalency.SelectedValue))
                {
                    if (ddlEntityNeedsEquivalency.SelectedValue == "New")
                    {
                        request.OtherEntityNeedsEquivalency = ddlEntityNeedsEquivalency.NewOptionText;
                        request.EntityNeedsEquivalency = new EntityNeedsEquivalency()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = string.Empty
                        };
                    }
                    else
                    {
                        request.EntityNeedsEquivalency = new EntityNeedsEquivalency()
                        {
                            SelectedID = ddlEntityNeedsEquivalency.SelectedValue,
                            SelectedTitle = ddlEntityNeedsEquivalency.SelectedText
                        };
                        request.OtherEntityNeedsEquivalency = string.Empty;
                    }
                }

                request.LoginName = SPContext.Current.Web.CurrentUser.LoginName.ToLower();


                string requestId = string.Empty; 
                if ((SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage) || SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage))
                    && Page.Session["EditRequestId"] != null /* Page.Request.QueryString["reqNo"] != null*/)
                {

                    request.ID = int.Parse(Convert.ToString(Page.Session["EditRequestId"]));
 
                }
                else
                {
                    request.ID = int.Parse(Page.Session["NewRequestId"].ToString());
                }
 

                request.RequestNumber = lblRequestNumberValue.Text;

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit RequestDetails.CreateSubmittedRequestDataObject");
            }
            return request;
        }


        public Entities.Request CreateSavedRequestDataObject()
        {
            Logging.GetInstance().Debug("Enter RequestDetails.CreateSavedRequestDataObject");
            Entities.Request request = new Entities.Request();
            try
            {

                if (dropAcademicDegreeForEquivalence.SelectedIndex != 0)
                {

                    request.AcademicDegree = new Entities.AcademicDegree()
                    {

                        SelectedID = dropAcademicDegreeForEquivalence.SelectedValue,
                        SelectedTitle = dropAcademicDegreeForEquivalence.SelectedItem.Text
                    };
                }
                if (dropCertificateAcademicDegree.SelectedIndex != 0)
                {
                    request.AcademicDegreeForEquivalence = new Entities.AcademicDegree()
                    {
                        SelectedID = dropCertificateAcademicDegree.SelectedValue,
                        SelectedTitle = dropCertificateAcademicDegree.SelectedItem.Text
                    };
                }

                request.CertificateThroughScholarship = rdbCertificateThroughScholarship.SelectedValue == yesValue ? true : false;
                if (rdbCertificateThroughScholarship.SelectedValue == yesValue)
                {
                    if (!string.IsNullOrEmpty(txtEntityProvidingStudy.Text))
                    {
                        request.EntityProvidingStudy = txtEntityProvidingStudy.Text;
                    }
                }

                if (dropStudyingLanguage.SelectedIndex != 0)
                {
                    request.StudyLanguage = new StudyLanguage()
                    {
                        SelectedID = dropStudyingLanguage.SelectedValue,
                        SelectedTitle = dropStudyingLanguage.SelectedItem.Text
                    };
                }


                if (dropStudyingSystem.SelectedIndex != 0)
                {
                    request.StudySystem = new StudySystem()
                    {
                        SelectedID = dropStudyingSystem.SelectedValue,
                        SelectedTitle = dropStudyingSystem.SelectedItem.Text
                    };

                }
                 else if(Page.Session["EditRequestId"] != null && dropStudyingSystem.SelectedIndex == 0)
                {
                    request.StudySystem = null;
                }
                if (!string.IsNullOrEmpty(txtStartDate.Text))
                {
 
                    request.StudyStartDate = Convert.ToDateTime(DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
 

                }
                if (!string.IsNullOrEmpty(txtGraduationDate.Text))
                {
                    request.StudyGraduationDate = Convert.ToDateTime(DateTime.ParseExact(txtGraduationDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                }
                else if (Page.Session["EditRequestId"] != null && string.IsNullOrEmpty(txtGraduationDate.Text))
                {
                    DateTime? dt = null;

                    request.StudyGraduationDate = Convert.ToDateTime(dt);

                }

                    if (!string.IsNullOrEmpty(ddlCountry.DropWithNewOption.SelectedValue))
                {
                    if (ddlCountry.SelectedValue == "New")
                    {
                        request.CountryOfStudy = new CountryOfStudy()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = ddlCountry.NewOptionText
                        };
                    }
                    else
                    {
                        request.CountryOfStudy = new CountryOfStudy()
                        {
                            SelectedID = ddlCountry.DropWithNewOption.SelectedValue,
                            SelectedTitle = ddlCountry.DropWithNewOption.SelectedItem.Text
                        };
                    }
                }

                //User Control

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


                if (!string.IsNullOrEmpty(ddlMainCountry.SelectedValue))
                {
                    if (ddlMainCountry.SelectedValue == "New")
                    {
                        request.NewUniversityHeadquarter = ddlMainCountry.NewOptionText;
                        request.UniversityMainHeadquarter = new UniversityMainCountry()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = string.Empty
                        };
                    }
                    else
                    {
                        request.UniversityMainHeadquarter = new UniversityMainCountry()
                        {
                            SelectedID = ddlMainCountry.SelectedValue,
                            SelectedTitle = ddlMainCountry.SelectedText
                        };
                        request.NewUniversityHeadquarter = string.Empty;
                    }
                }



                if (!string.IsNullOrEmpty(txtAddress.Text))
                {
                    request.UniversityAddress = txtAddress.Text;
                }
                if (!string.IsNullOrEmpty(txtUniversityEmail.Text))
                {

                    request.UniversityEmail = txtUniversityEmail.Text;
                }
                request.WorkingOrNot = rdbWorkingOrNot.SelectedValue == yesValue ? true : false;
                request.HavePAOrNot = radioBtnHavePA.SelectedValue == yesValue ? true : false;
                request.UniversityType = radiobtnlstUniversity.SelectedValue;
                checkHavePA();
                if (rdbWorkingOrNot.SelectedValue == yesValue)
                {   //User Control



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
                    //if (!string.IsNullOrEmpty(txtOccupation.Text))
                    //{
                    //    request.Occupation = txtOccupation.Text;
                    //}
 
                //Ahmed
                    //request.EntityNeedsEquivalency = new EntityNeedsEquivalency()
                    //{
                    //    SelectedID = string.Empty,
                    //    SelectedTitle = string.Empty
                    //};
                    //request.OtherEntityNeedsEquivalency = string.Empty;
                }

                else
                {             //User Control

                    //Ahmed
                    //if (!string.IsNullOrEmpty(ddlEntityNeedsEquivalency.SelectedValue))
                    //{

                    //    request.EntityNeedsEquivalency = new EntityNeedsEquivalency()
                    //    {
                    //        SelectedID = ddlEntityNeedsEquivalency.SelectedValue,
                    //        SelectedTitle = ddlEntityNeedsEquivalency.SelectedItem.Text
                    //    };
                    //}
 

                    request.EntityWorkingFor = new EntityWorkingFor()
                    {
                        SelectedID = string.Empty,
                        SelectedTitle = string.Empty
                    };
                    request.OtherEntityWorkingFor = string.Empty;
                    //request.Occupation = string.Empty;
 
                }

                //Ahmed
                if (!string.IsNullOrEmpty(ddlEntityNeedsEquivalency.SelectedValue))
                {
                    if (ddlEntityNeedsEquivalency.SelectedValue == "New")
                    {
                        request.OtherEntityNeedsEquivalency = ddlEntityNeedsEquivalency.NewOptionText;
                        request.EntityNeedsEquivalency = new EntityNeedsEquivalency()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = string.Empty
                        };
                    }
                    else
                    {
                        request.EntityNeedsEquivalency = new EntityNeedsEquivalency()
                        {
                            SelectedID = ddlEntityNeedsEquivalency.SelectedValue,
                            SelectedTitle = ddlEntityNeedsEquivalency.SelectedText
                        };
                        request.OtherEntityNeedsEquivalency = string.Empty;
                    }
                }

                request.LoginName = SPContext.Current.Web.CurrentUser.LoginName.ToLower();
 
                if ((SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage) || SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage))
                    && Page.Session["EditRequestId"] != null /* Page.Request.QueryString["reqNo"] != null*/)
                {

                    request.ID = int.Parse(Convert.ToString(Page.Session["EditRequestId"]));
 
                }
 
                else
                {
                    request.ID = int.Parse(Page.Session["NewRequestId"].ToString());
                }
                request.RequestNumber = lblRequestNumberValue.Text;
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit RequestDetails.CreateSavedRequestDataObject");
            }
            return request;
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
        #endregion

        protected void dropCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter RequestDetails.dropCountries_SelectedIndexChanged");
            try
            {
                if (string.IsNullOrEmpty(ddlCountry.DropWithNewOption.SelectedValue))
                {


                    ddlUniversity.DropWithNewOption.Items.Clear();
                    ddlUniversity.DropListItems = null;
                    ddlUniversity.BindDataSource(); 
                    ddlSpecialization.DropWithNewOption.Items.Clear();
                    ddlSpecialization.DropListItems = null;
                    ddlSpecialization.BindDataSource();
                    txtUniversityListValue.Text = string.Empty;
                    lblUniversityHEDD.Text = string.Empty;
                    lblUniversityCHED.Text = string.Empty;
                }
                else
                { 
                    lblUniversityHEDD.Text = string.Empty;
                    lblUniversityCHED.Text = string.Empty;
                    txtUniversityListValue.Text = string.Empty;
                    List<University> university = Common.BL.University.GetDistinctUniversityByYearAndCountry(0, int.Parse(ddlCountry.DropWithNewOption.SelectedValue),LCID);
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
                        //ddlUniversity.DropListItems = "New";
                       ddlUniversity.DropListItems = null;

                    }
                    ddlUniversity.BindDataSource();
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit RequestDetails.dropCountries_SelectedIndexChanged");
            }


        }
        public void ddlUniversity_SelectedIndexChanged(Object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter RequestDetails.ddlUniversity_SelectedIndexChanged");
            try
            {
                // DropDownList dropDownList = sender as DropDownList;
                if (ddlUniversityDiv.Visible)
                {
                    if (string.IsNullOrEmpty(ddlUniversity.SelectedValue) || ddlUniversity.SelectedValue == "New")
                    {


                        txtUniversityListValue.Text = string.Empty;
                        lblUniversityHEDD.Text = string.Empty;
                        lblUniversityCHED.Text = string.Empty;
                    }
                    else
                    {


                        string universityList = string.Empty;


                        if (Common.BL.University.IsUniversityHEDD(int.Parse(ddlUniversity.SelectedValue)))
                        {
                            lblUniversityHEDD.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "HEDDMessage", (uint)LCID), ddlUniversity.DropWithNewOption.SelectedItem.ToString());

                        }
                        else
                        {
                            lblUniversityHEDD.Text = string.Empty;
                        }
                        if (Common.BL.University.IsUniversityCHED(int.Parse(ddlUniversity.SelectedValue)))
                        {
                            lblUniversityCHED.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CHEDMessage", (uint)LCID), ddlUniversity.DropWithNewOption.SelectedItem.ToString());

                        }
                        else
                        {
                            lblUniversityCHED.Text = string.Empty;
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

                Logging.GetInstance().Debug("Exit RequestDetails.ddlUniversity_SelectedIndexChanged");
            }

        }
        public void ddlFaculty_SelectedIndexChanged(Object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter RequestDetails.ddlFaculty_SelectedIndexChanged");
            try
            {
                DropDownList dropDownList = sender as DropDownList;
                if (string.IsNullOrEmpty(dropDownList.SelectedValue) || dropDownList.SelectedValue == "New")
                {

                    ddlSpecialization.DropWithNewOption.Items.Clear();

                    if (string.IsNullOrEmpty(dropDownList.SelectedValue))
                    {

                        ddlSpecialization.DropListItems = null;
                    }
                    else
                    {

                        ddlSpecialization.DropListItems = "New";
                    }

                    ddlSpecialization.BindDataSource();
                }
                else
                {
 

                    List<Specialization> specialization = BL.Specialization.GetAll(); 
 
                    ddlSpecialization.DropWithNewOption.Items.Clear();
                    if (specialization != null && specialization.Count > 0)
                    {
                        ddlSpecialization.DropListItems = specialization;
                        ddlSpecialization.DataValueField = "ID";
                        ddlSpecialization.DataTextEnField = "EnglishTitle";
                        ddlSpecialization.DataTextArField = "ArabicTitle";

                    }
                    else
                    {
                        ddlSpecialization.DropListItems = null;
                    }

                    ddlSpecialization.BindDataSource();

                } 
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit RequestDetails.ddlFaculty_SelectedIndexChanged");
            }
        }
        public void ddlCountry_SelectedIndexChanged(Object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter RequestDetails.ddlCountry_SelectedIndexChanged");
            try
            {
                DropDownList dropDownList = sender as DropDownList;
                if (string.IsNullOrEmpty(dropDownList.SelectedValue) || dropDownList.SelectedValue == "New")
                {
                    ddlUniversity.DropWithNewOption.Items.Clear(); 
                    if (string.IsNullOrEmpty(dropDownList.SelectedValue))
                    {
                        ddlUniversity.DropListItems = null; 
                    }

                    else
                    {
                        ddlUniversity.DropListItems = "New"; 
                    }
                    ddlUniversity.BindDataSource(); 
                }
                else
                {
                    //changed in order to run the cycle currently but it must be without university list
                    List<University> university = Common.BL.University.GetDistinctUniversityByYearAndCountry(0, int.Parse(dropDownList.SelectedValue), LCID);//Common.BL.UniversityLists.GetUniversityWithEmptyUniversityListsAndCountryId(int.Parse(dropDownList.SelectedValue));
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
                        ddlUniversity.DropListItems = "New";
                        //ddlUniversity.DropListItems = null;

                    }
                    ddlUniversity.BindDataSource();

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit RequestDetails.ddlCountry_SelectedIndexChanged");
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
                    List<University> university = Common.BL.University.GetDistinctUniversityByYearAndCountry(0, int.Parse(dropDownList.SelectedValue),LCID);//Common.BL.UniversityLists.GetUniversityWithEmptyUniversityListsAndCountryId(int.Parse(dropDownList.SelectedValue));
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
                        ddlCalcSectionUniversity.DropListItems = "New";
                        //ddlCalcSectionUniversity.DropListItems = null;

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
 

        protected void lnkCalcSectionDelete_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method RequestDetails.lnkCalcSectionDelete_Click");
        
            try
            {
                List<CalculatedDetailsForCertificate> calcList = GetCalculatedRepeaterBoundData();
                RepeaterItem repeaterItem = (sender as LinkButton).NamingContainer as RepeaterItem;
                HiddenField hiddenId = (sender as LinkButton).NamingContainer.FindControl("hdnId") as HiddenField;

                if (ViewState["FileUploadGroup"] !=null && int.Parse(ViewState["FileUploadGroup"].ToString())== repeaterItem.ItemIndex)
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

 
                RepeaterItem repeaterItem = (sender as LinkButton).NamingContainer as RepeaterItem;
                        HiddenField hdnCalcCountry = repeaterItem.FindControl("hdnCalcSectionCountry") as HiddenField;
                        Label lblOtherCountry = repeaterItem.FindControl("lblCalcSectionOtherCountry") as Label;
                        HiddenField hdnCalcUniversity = repeaterItem.FindControl("hdnCalcSectionUniversity") as HiddenField;
                        Label lblOtherUni = repeaterItem.FindControl("lblCalcSectionOtherUniversity") as Label; 
                        HiddenField hdnStudySystem = repeaterItem.FindControl("hdnCalcSectionStudySystem") as HiddenField; 
                        Label lblCalcSectionFaculty = repeaterItem.FindControl("lblCalcSectionFaculty") as Label;

                        CalculatedDetailsForCertificate calculatedDetails = new CalculatedDetailsForCertificate()
                        {
                            Country = new CountryOfStudy() { SelectedID = hdnCalcCountry.Value },
                            OtherCountry = lblOtherCountry.Text,
                            Univesrity = new University() { SelectedID = hdnCalcUniversity.Value },
                            OtherUniversity = lblOtherUni.Text,
                            Faculty = lblCalcSectionFaculty.Text, 
                            StudySystem = new StudySystem() { SelectedID = hdnStudySystem.Value }
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
                                    }
                                }

                            }
  
                            if (!string.IsNullOrEmpty(calculatedDetails.Faculty))
                            {
                                ddlCalcSectionFaculty.Text = calculatedDetails.Faculty;
                            }
                            if (!string.IsNullOrEmpty(calculatedDetails.StudySystem.SelectedID))
                            {
                                dropCalcSectionStudyingSystem.SelectedValue = calculatedDetails.StudySystem.SelectedID;
                            }
 
                            hdnCalcSectionItem.Value = repeaterItem.ItemIndex.ToString();
                            fileUploadCalcDetails.Group = repeaterItem.ItemIndex.ToString();
                            ViewState["FileUploadGroup"] = fileUploadCalcDetails.Group; 
                            fileUploadCalcDetails.Bind();
                  
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

                        if ( dropCalcSectionStudyingSystem.SelectedIndex > 0||
 
                        ddlCalcSectionCountry.DropWithNewOption.SelectedIndex > 0 ||
                        ddlCalcSectionUniversity.DropWithNewOption.SelectedIndex > 0  
                        ||!string.IsNullOrEmpty(ddlCalcSectionFaculty.Text))
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

  
                            calcItem.Faculty = ddlCalcSectionFaculty.Text != null ? ddlCalcSectionFaculty.Text :string.Empty;
                            if (dropCalcSectionStudyingSystem.SelectedIndex > 0)
                            {
                                calcItem.StudySystem = new StudySystem()
                                {
                                    SelectedID = dropCalcSectionStudyingSystem.SelectedValue,
                                    SelectedTitle = dropCalcSectionStudyingSystem.SelectedItem.Text
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
 
                    ddlCalcSectionFaculty.Text = string.Empty; 
                    dropCalcSectionStudyingSystem.ClearSelection(); 
                    fileUploadCalcDetails.Group = (calcList.Count).ToString();
                    ViewState["FileUploadGroup"] = fileUploadCalcDetails.Group; 
                    fileUploadCalcDetails.Bind();
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

        public void BindWorkingAttachments()
        {
            Logging.GetInstance().Debug("Entering method RequestDetails.BindWorkingAttachments");
            try
            {

                if (Page.Session["NewRequestId"] != null || Page.Session["EditRequestId"] != null)
                {
                    int requestId = 0;
                    if ((SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage) || SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage))
                        && Page.Session["EditRequestId"] != null)
                    {
                        requestId = int.Parse(Page.Session["EditRequestId"].ToString());
                    }
                    else
                    {
                        requestId = int.Parse(Page.Session["NewRequestId"].ToString());
                    }

                    fileUploadWorking.DocumentLibraryName = Utilities.Constants.OrganizationalLettersAttachments;
                    fileUploadWorking.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadWorking.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CopyOfOrganizationlLetter", (uint)LCID);
                    fileUploadWorking.MaxFileNumber = 1;
                    fileUploadWorking.MaxSize = 7168000;
                    fileUploadWorking.Group = "CopyOfOrganizationlLetter";
                    fileUploadWorking.RequestID = requestId;
                    fileUploadWorking.SupportedExtensions = "PNG,PDF,JPG";
                    fileUploadWorking.IsRequired = true;
                    fileUploadWorking.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredCopyOfOrganizationlLetter", (uint)LCID);
                    fileUploadWorking.ValidationGroup = "Submit";
                    fileUploadWorking.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadWorking.Enabled = true;
                    fileUploadWorking.Bind();


                    fileUploadNotWorking.DocumentLibraryName = Utilities.Constants.OrganizationalLettersAttachments;
                    fileUploadNotWorking.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadNotWorking.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CopyOfOrganizationlLetter", (uint)LCID);
                    fileUploadNotWorking.MaxFileNumber = 1;
                    fileUploadNotWorking.MaxSize = 7168000;
                    fileUploadNotWorking.Group = "NotWorkingCopyOfOrganizationlLetter";
                    fileUploadNotWorking.RequestID = requestId;
                    fileUploadNotWorking.SupportedExtensions = "PNG,PDF,JPG";
                    fileUploadNotWorking.IsRequired = false;
                    fileUploadNotWorking.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadNotWorking.Enabled = true;
                    fileUploadNotWorking.Bind();

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method RequestDetails.BindWorkingAttachments");
            }

        }

        public void BindHavePAAttachments()
        {
            Logging.GetInstance().Debug("Entering method RequestDetails.BindHavePAAttachments");
            try
            {

                if (Page.Session["NewRequestId"] != null || Page.Session["EditRequestId"] != null)
                {
                    int requestId = 0;
                    if ((SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage) || SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage))
                        && Page.Session["EditRequestId"] != null)
                    {
                        requestId = int.Parse(Page.Session["EditRequestId"].ToString());
                    }
                    else
                    {
                        requestId = int.Parse(Page.Session["NewRequestId"].ToString());
                    }

                    fileUploadHavePA.DocumentLibraryName = Utilities.Constants.HavePAAttachments;
                    fileUploadHavePA.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadHavePA.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "DecisionPriorApproval", (uint)LCID);
                    fileUploadHavePA.MaxFileNumber = 1;
                    fileUploadHavePA.MaxSize = 7168000;
                    fileUploadHavePA.Group = "YesHavePAAttachments";
                    fileUploadHavePA.RequestID = requestId;
                    fileUploadHavePA.SupportedExtensions = "PNG,PDF,JPG";
                    fileUploadHavePA.IsRequired = true;
                    fileUploadHavePA.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredDecisionPriorApproval", (uint)LCID);
                    fileUploadHavePA.ValidationGroup = "Submit";
                    fileUploadHavePA.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadHavePA.Enabled = true;
                    fileUploadHavePA.Bind();


                    fileUploadNoHavePA.DocumentLibraryName = Utilities.Constants.HavePAAttachments;
                    fileUploadNoHavePA.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadNoHavePA.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ApprovalNoPriorApproval", (uint)LCID);
                    fileUploadNoHavePA.MaxFileNumber = 1;
                    fileUploadNoHavePA.MaxSize = 7168000;
                    fileUploadNoHavePA.Group = "NoHavePAAttachments";
                    fileUploadNoHavePA.RequestID = requestId;
                    fileUploadNoHavePA.SupportedExtensions = "PNG,PDF,JPG";
                    fileUploadNoHavePA.IsRequired = true;
                    fileUploadNoHavePA.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredApprovalNoPriorApproval", (uint)LCID);
                    fileUploadNoHavePA.ValidationGroup = "Submit";
                    fileUploadNoHavePA.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadNoHavePA.Enabled = true;
                    fileUploadNoHavePA.Bind();

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method RequestDetails.BindHavePAAttachments");
            }

        }
        public void BindAttachments()
        {
            Logging.GetInstance().Debug("Entering method RequestDetails.BindAttachments");
            try
            {

                if (Page.Session["NewRequestId"] != null || Page.Session["EditRequestId"] != null)
                {
                    int requestId = 0;
                    if ((SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage) || SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage))
                        && Page.Session["EditRequestId"] != null)
                    {
                        requestId = int.Parse(Page.Session["EditRequestId"].ToString());
                    }
                    else
                    {
                        requestId = int.Parse(Page.Session["NewRequestId"].ToString());
                    }

                      BindWorkingAttachments();
                      BindHavePAAttachments();


                    if (ddlCalcAttachmentCategory.SelectedIndex > 0)
                    {
                        fileUploadCalcDetails.AttachmentCategorySelectedText = ddlCalcAttachmentCategory.SelectedItem.Text;
                    }
                    ddlCalcAttachmentCategory.ClearSelection(); 
 

                    fileUploadCalcDetails.DocumentLibraryName = Utilities.Constants.CalculatedDetailsForCertificateAttachments;
                    fileUploadCalcDetails.DocLibWebUrl = SPContext.Current.Site.Url;

                    fileUploadCalcDetails.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CalculatedDetailsAttachText", (uint)LCID);
                    fileUploadCalcDetails.MaxFileNumber = BL.StudyAttachmentTypes.GetAll().Count;
                    fileUploadCalcDetails.MaxSize = 7168000;

                    if ((SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage) || SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage))
                            && Page.Session["EditRequestId"] != null)
                    {
                        if (ViewState["FileUploadGroup"]!=null)
                        {
                            fileUploadCalcDetails.Group = ViewState["FileUploadGroup"].ToString();
                        }
                        else
                        {
                            fileUploadCalcDetails.Group = repCalculatedDetailsForCertificate.Items.Count.ToString();
                        }
                       
                    }
                    else
                    {
                        if (ViewState["FileUploadGroup"] == null)
                        {
                            fileUploadCalcDetails.Group = "0";
                        }
                        else
                        {
                            fileUploadCalcDetails.Group = ViewState["FileUploadGroup"].ToString();
                        }
                    }
 
                    fileUploadCalcDetails.RequestID = requestId;
                    fileUploadCalcDetails.SupportedExtensions = "PNG,PDF,JPG";
                    fileUploadCalcDetails.IsRequired = false;
                    fileUploadCalcDetails.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadCalcDetails.Enabled = true;
                  
                    fileUploadCalcDetails.Bind();
 


                    fileUploadDelegates.DocumentLibraryName = Utilities.Constants.DelegationDocuments;
                    fileUploadDelegates.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadDelegates.AfterDawnLoad = true;
                    fileUploadDelegates.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "DelegationTemplates", (uint)LCID);
                     
                    fileUploadDelegates.MaxSize = 7168000;
                    fileUploadDelegates.Group = "DelegationTemplates";
                    fileUploadDelegates.RequestID = requestId;
                    fileUploadDelegates.SupportedExtensions = "DOC,DOCX,PDF,PNG,JPG";
                    fileUploadDelegates.IsRequired = true;
                    fileUploadDelegates.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredDelegates", (uint)LCID);
                    fileUploadDelegates.ValidationGroup = "Submit";
                    fileUploadDelegates.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadDelegates.Enabled = true;
                    fileUploadDelegates.Bind();
                    fileUploadDelegates.DocumentLibraryName = Utilities.Constants.DelegationDocuments;
                    fileUploadDelegates.DocLibWebUrl = SPContext.Current.Site.Url;


                    fileUploadCertificateEquivalent.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                    fileUploadCertificateEquivalent.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadCertificateEquivalent.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CertificateToBeEquivalent", (uint)LCID);
                    fileUploadCertificateEquivalent.MaxSize = 7168000;
                    fileUploadCertificateEquivalent.Group = "CertificateEquivalentAttachment";
                    fileUploadCertificateEquivalent.RequestID = requestId;
                    fileUploadCertificateEquivalent.SupportedExtensions = "PNG,PDF,JPG";
                    fileUploadCertificateEquivalent.IsRequired = true;
                    fileUploadCertificateEquivalent.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredCertificateToBeEquivalent", (uint)LCID);
                    fileUploadCertificateEquivalent.ValidationGroup = "Submit";
                    fileUploadCertificateEquivalent.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadCertificateEquivalent.Enabled = true;
                    fileUploadCertificateEquivalent.Bind();



                    fileUploadGrades.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                    fileUploadGrades.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadGrades.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Grades", (uint)LCID);
                    fileUploadGrades.MaxSize = 7168000;
                    fileUploadGrades.Group = "GardesAttachment";
                    fileUploadGrades.RequestID = requestId;
                    fileUploadGrades.SupportedExtensions = "PNG,PDF,JPG";
                    fileUploadGrades.IsRequired = true;
                    fileUploadGrades.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredGrades", (uint)LCID);
                    fileUploadGrades.ValidationGroup = "Submit";
                    fileUploadGrades.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadGrades.Enabled = true;
                    fileUploadGrades.Bind();

                    if (!string.IsNullOrEmpty(ddlAttachmentCat.SelectedValue))
                    {
                        if (ddlAttachmentCat.SelectedValue == "New")
                        {
                            fileUploadCertificates.AttachmentCategorySelectedText = ddlAttachmentCat.NewOptionText;
                        }
                        else
                        {
                            fileUploadCertificates.AttachmentCategorySelectedText = ddlAttachmentCat.SelectedText;
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

                Logging.GetInstance().Debug("Exiting method RequestDetails.BindAttachments");
            }

        }
        public bool SaveAttachments()
        {
            Logging.GetInstance().Debug("Entering method RequestDetails.SaveAttachments");
            bool fileWorking = false;
            bool fileNotWorking = false;
            bool fileHavePa = false;
            bool fileNotHavePa = false;
            bool saved = false;
            try
            {
                if (rdbWorkingOrNot.SelectedValue == "0")
                 {
                    fileNotWorking= fileUploadNotWorking.SaveAttachments();

                 }
                else
                {
                    fileWorking = fileUploadWorking.SaveAttachments();

                }

                if (radioBtnHavePA.SelectedValue == "0")
                {
                    fileHavePa = fileUploadNoHavePA.SaveAttachments();

                }
                else
                {
                    fileNotHavePa = fileUploadHavePA.SaveAttachments();

                }


                BL.CertificatesAttachments.DeleteCertificatesAttachmentsByGroupAndRequestID(fileUploadCertificates.RequestID, fileUploadCertificates.Group, fileUploadSequenceCert.Group,fileUploadDiploma.Group,fileUploadInterDiploma.Group,fileUploadGrades.Group,fileUploadCertificateEquivalent.Group ,dropCertificateAcademicDegree.SelectedItem.Text);
                bool fileCertificates = fileUploadCertificates.SaveAttachments();
                bool fileCertificateEquivalent = fileUploadCertificateEquivalent.SaveAttachments();
                bool fileGrades = fileUploadGrades.SaveAttachments();
                bool fileDiploma = fileUploadDiploma.SaveAttachments();
                bool fileInterDiploma = fileUploadInterDiploma.SaveAttachments();
                bool fileCertSequence = fileUploadSequenceCert.SaveAttachments();
                bool fileDelegates = fileUploadDelegates.SaveAttachments();
                
                bool fileCalcDet = false;
                foreach (RepeaterItem item in repCalculatedDetailsForCertificate.Items)
                {
                    fileUploadCalcDetails.Group = item.ItemIndex.ToString();
                    fileUploadCalcDetails.GetAttachments();

                    fileCalcDet = fileUploadCalcDetails.SaveAttachments();

                }


                if (fileDiploma && fileInterDiploma && fileWorking && fileCertificateEquivalent && fileCertificates && fileCalcDet && fileDelegates && fileNotWorking && fileWorking  && fileCertSequence && fileGrades && fileHavePa && fileNotHavePa)
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

                Logging.GetInstance().Debug("Exiting method RequestDetails.SaveAttachments");
            }
            return saved;
        }

        protected void dropCertificateAcademicDegree_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method RequestDetails.dropCertificateAcademicDegree_SelectedIndexChanged");
            try
            {

                if (!string.IsNullOrEmpty(dropCertificateAcademicDegree.SelectedValue))
                {
                    fileUploadCertificates.Visible = true;
                   // fileUploadGeneralSec.Visible = true;
                    fileUploadSequenceCert.Visible = true;
                    lnkSequences.Visible = true;
                    lblSequence.Visible = true;
                    HETemplates templates =Common.BL.HETemplates.GetAttachmentByType(Common.Utilities.TemplateType.Certificates.ToString());
                    if (templates != null)
                    {
                        lnkSequences.Text = templates.FileName;
                        lnkSequences.CommandArgument = templates.ID.ToString();

                    }

                    int requestId = 0;
                    if ((SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage) || SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage))
                        && Page.Session["EditRequestId"] != null)
                    {
                        requestId = int.Parse(Page.Session["EditRequestId"].ToString());
                    }
                    else if (Page.Session["NewRequestId"]!=null)
                    {
                        requestId = int.Parse(Page.Session["NewRequestId"].ToString());
                    }
 
                   

                    divAttachmentCategory.Visible = true;
                    List<Certificates> attachmentCategoryList = BL.Certificates.GetCertificatesByAcademicDegree(int.Parse(dropCertificateAcademicDegree.SelectedValue), LCID);
                    string bachelor = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Bachelor", (uint)LCID);
                    string diploma = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Diploma", (uint)LCID);
                    string intermediateDiploma = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "IntermediateDiploma", (uint)LCID);
                    attachmentCategoryList.RemoveAll(c => (c.EnglishTitle.Equals(diploma) || c.ArabicTitle.Equals(diploma)) || (c.EnglishTitle.Equals(intermediateDiploma) || c.ArabicTitle.Equals(intermediateDiploma)));
                    ddlAttachmentCat.DropWithNewOption.Items.Clear();
                    ddlAttachmentCat.DropListItems = attachmentCategoryList;
                    ddlAttachmentCat.DataValueField = "ID";
                    ddlAttachmentCat.DataTextEnField = "EnglishTitle";
                    ddlAttachmentCat.DataTextArField = "ArabicTitle";
                    ddlAttachmentCat.BindDataSource();
 
                    fileUploadCertificates.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                    fileUploadCertificates.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadCertificates.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "PreviousCertificates", (uint)LCID);
                    //fileUploadCertificates.MaxFileNumber =1;
                    fileUploadCertificates.MaxSize = 7168000;
                    fileUploadCertificates.Group = dropCertificateAcademicDegree.SelectedItem.Value;//"PreviousCertificates";// field name for example, shouldn't be used for more than one field per each control.
                    fileUploadCertificates.RequestID = requestId;
                    fileUploadCertificates.SupportedExtensions = "PNG,PDF,JPG";
                    fileUploadCertificates.IsRequired = true;
                    fileUploadCertificates.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredCertificateFile", (uint)LCID);
                    fileUploadCertificates.ValidationGroup = "Submit";
                    fileUploadCertificates.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadCertificates.Enabled = true;
                    fileUploadCertificates.Bind();


                    //fileUploadGeneralSec.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                    //fileUploadGeneralSec.DocLibWebUrl = SPContext.Current.Site.Url;
                    //fileUploadGeneralSec.LabelDisplayName = generalSecondary;
                    //fileUploadGeneralSec.MaxFileNumber = 1;
                    //fileUploadGeneralSec.MaxSize = 7168000;
                    //fileUploadGeneralSec.Group = "GeneralSecondary"+ dropCertificateAcademicDegree.SelectedItem.Value; //"PreviousCertificates";// field name for example, shouldn't be used for more than one field per each control.
                    //fileUploadGeneralSec.RequestID = requestId;
                    //fileUploadGeneralSec.SupportedExtensions = "PNG,PDF,JPG";
                    //fileUploadGeneralSec.IsRequired = true;
                    //fileUploadGeneralSec.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredGeneralSecondaryCert", (uint)LCID);
                    //fileUploadGeneralSec.ValidationGroup = "Submit";
                    //fileUploadGeneralSec.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    //fileUploadGeneralSec.Enabled = true;
                    //fileUploadGeneralSec.Bind();
                   
                    fileUploadSequenceCert.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                    fileUploadSequenceCert.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadSequenceCert.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CertificateSequence", (uint)LCID); ; 
                    fileUploadSequenceCert.MaxFileNumber = 1;
                    fileUploadSequenceCert.MaxSize = 7168000;
                    fileUploadSequenceCert.Group = "CertificateSequence" + dropCertificateAcademicDegree.SelectedItem.Value; //"PreviousCertificates";// field name for example, shouldn't be used for more than one field per each control.
                    fileUploadSequenceCert.RequestID = requestId;
                    fileUploadSequenceCert.SupportedExtensions = "PNG,PDF,JPG";
                    fileUploadSequenceCert.IsRequired = true;
                    fileUploadSequenceCert.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredCertificateSequence", (uint)LCID);
                    fileUploadSequenceCert.ValidationGroup = "Submit";
                    fileUploadSequenceCert.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadSequenceCert.Enabled = true;
                    fileUploadSequenceCert.Bind();

                    if (dropCertificateAcademicDegree.SelectedItem.Text.Equals(bachelor))
                    {
                        fileUploadDiploma.Visible = true;
                        fileUploadDiploma.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                        fileUploadDiploma.DocLibWebUrl = SPContext.Current.Site.Url;
                        fileUploadDiploma.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Diploma", (uint)LCID); ;
                        fileUploadDiploma.MaxFileNumber = 1;
                        fileUploadDiploma.MaxSize = 7168000;
                        fileUploadDiploma.Group = "Diploma" + dropCertificateAcademicDegree.SelectedItem.Value; //"PreviousCertificates";// field name for example, shouldn't be used for more than one field per each control.
                        fileUploadDiploma.RequestID = requestId;
                        fileUploadDiploma.SupportedExtensions = "PNG,PDF,JPG";
                        fileUploadDiploma.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                        fileUploadDiploma.Enabled = true;
                        fileUploadDiploma.Bind();


                        fileUploadInterDiploma.Visible = true;
                        fileUploadInterDiploma.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                        fileUploadInterDiploma.DocLibWebUrl = SPContext.Current.Site.Url;
                        fileUploadInterDiploma.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "IntermediateDiploma", (uint)LCID); ;
                        fileUploadInterDiploma.MaxFileNumber = 1;
                        fileUploadInterDiploma.MaxSize = 7168000;
                        fileUploadInterDiploma.Group = "InterMediateDiploma" + dropCertificateAcademicDegree.SelectedItem.Value; //"PreviousCertificates";// field name for example, shouldn't be used for more than one field per each control.
                        fileUploadInterDiploma.RequestID = requestId;
                        fileUploadInterDiploma.SupportedExtensions = "PNG,PDF,JPG";
                        fileUploadInterDiploma.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                        fileUploadInterDiploma.Enabled = true;
                        fileUploadInterDiploma.Bind();
                    }
                    else
                    {
                        fileUploadDiploma.Visible = false;
                        fileUploadInterDiploma.Visible = false;
                    }



                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method RequestDetails.dropCertificateAcademicDegree_SelectedIndexChanged");
            }
        }

        protected void btnResubmit_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method RequestDetails.btnResubmit_Click");
            try
            {

                if (!IsRefresh)
                {

                    Validate();
                    Page.Validate();
                    if (Page.IsValid)

                    {
                        if (Page.Session["NewRequestId"] == null && Page.Session["EditRequestId"] == null)
                        {
                            lblErrorMessage.Text = lblErrorMessage.Text + " " + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                            modalPopupError.Show();
                            return;
                        }

                        if (SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage) && Page.Session["EditRequestId"] != null)
                        {
                            string requestId = Page.Session["EditRequestId"].ToString();
                            Entities.Request request = CreateSavedRequestDataObject();
                            
                            bool requestUpdated = BL.Request.AddUpdateRequest(request, string.Empty, request.ID, (int)Common.Utilities.RequestStatus.UCEDraftForClarification);
                            bool savedCalculated = GetCalculatedDetailsDataFromRepeater(int.Parse(requestId));
                            SaveAttachments();
 
                            ClarificationReqs calrificationReq = BL.ClarificationRequests.GetClarificationReplybyReqID(requestId);

                            if ( calrificationReq != null )
                            {


                                lblSuccess.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ClarificationReplyRequired", (uint)LCID);

                                modalPopUpConfirmation.Show();
     
                            }
                            else
                            {
                                lblSuccess.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ReSubmitSucceed", (uint)LCID), requestId);

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

                Logging.GetInstance().Debug("Exiting method RequestDetails.btnResubmit_Click");
            }
        }

        protected void btnModalOK_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method RequestDetails.btnModalOK_Click");

            try
            {
                string requestId = Page.Session["EditRequestId"].ToString();
                ClarificationReqs clarificationReq = BL.ClarificationRequests.GetClarificationReplybyReqID(requestId);

                if (clarificationReq != null)
                { 
                    Page.Session["hdn_ClarRequestId"] = clarificationReq.ID;
                    Page.Session["DisplayRequestId"] = Page.Session["EditRequestId"];
                    Session["ActionTaken"] = "AddNewClarification";
                    Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.UCE/Applicant.aspx");

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

                Logging.GetInstance().Debug("Exiting method RequestDetails.btnModalOK_Click");
            }
        }

        protected void repCalculatedDetailsForCertificate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            Logging.GetInstance().Debug("Entering method RequestDetails.repCalculatedDetailsForCertificate_ItemDataBound");

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

                Logging.GetInstance().Debug("Exiting method RequestDetails.repCalculatedDetailsForCertificate_ItemDataBound");
            }
        }



        protected void lnkDelegateDisplay_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method RequestDetails.lnkDelegateDisplay_Click");

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
                    DownloadableName = lnkButton.Text+ hdnFileExtension.Value,
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

                Logging.GetInstance().Debug("Exiting method RequestDetails.lnkDelegateDisplay_Click");
            }
        }

        protected void lnkSequences_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method RequestDetails.lnkSequences_Click");

            try
            {
                Common.Utilities.DisplayedFile displayFile = new Common.Utilities.DisplayedFile()
                {
                    DocLibWebUrl = SPContext.Current.Site.Url,
                    ItemID = int.Parse(lnkSequences.CommandArgument),
                    DocumentLibraryName = Common.Utilities.Constants.HETemplates,
                    DownloadableName = lnkSequences.Text ,
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

                Logging.GetInstance().Debug("Exiting method RequestDetails.lnkSequences_Click");
            }
        }

        protected void custValidateFileUploadCertificates_ServerValidate(object source, ServerValidateEventArgs args)
        {

            if (fileUploadCertificates.AttachmentsCount > 0 && fileUploadCertificates.AttachmentsCount < ddlAttachmentCat.DropWithNewOption.Items.Count - 2)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void btnErrorOk_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method RequestDetails.btnErrorOk_Click");
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
                Logging.GetInstance().Debug("Exiting method RequestDetails.btnErrorOk_Click");
            }
        }

        private void checkHavePA()
        {
            bool havePA= radioBtnHavePA.SelectedValue == yesValue ? true : false;
            int requestId = 0;
            if (Page.Session["NewRequestId"] != null)
            {
                requestId = int.Parse(Convert.ToString(Page.Session["NewRequestId"]));
            }
            else if (Page.Session["EditRequestId"] != null)
            {
                requestId = int.Parse(Convert.ToString(Page.Session["EditRequestId"]));
            }
            if (havePA)
            {
               
                if (requestId != 0 && !string.IsNullOrEmpty(fileUploadNoHavePA.Group))
                    BL.RequestsAttachments.DeleteRequestsAttachmentsByGroupAndRequestID(requestId, fileUploadNoHavePA.Group);
            }
            else
            {
                if (requestId != 0 && !string.IsNullOrEmpty(fileUploadHavePA.Group))
                    BL.RequestsAttachments.DeleteRequestsAttachmentsByGroupAndRequestID(requestId, fileUploadHavePA.Group);
            }
        }
    }
}