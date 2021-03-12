using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.UCE.Entities;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.UCE.WebParts.DisplayRequest
{
    public partial class DisplayRequestUserControl : UserControlBase
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            //Logging.GetInstance().Debug("Entering method DisplayRequestUserControl.Page_Load");
            //try
            //{
            //    //if (!Page.IsPostBack)
                //{
                //    ((Button)TermsAndConditions.FindControl("btnAgree")).Visible = false;
                //}
                /*
                if (SPContext.Current.Web.CurrentUser != null)
                {
                    lblUserName.Text = SPContext.Current.Web.CurrentUser.Name;
                }

                //This will be removed when session is used
                if (/*Page.Request.QueryString["reqNo"] != null*/ /*Page.Session["RequestNumber"] != null)
                {/*
                    DisplayRequestData();
                    int requestNumber = int.Parse(Convert.ToString(Page.Session["RequestNumber"]));
                    //  int requestNumber=int.Parse(Convert.ToString(Page.Session["RequestNumber"]));
                    List<DelegationDocuments> delegationDocumentsData =BL.DelegationDocuments.GetGelegatesDocuments(requestNumber, LCID);
                    HelperMethods.BindGridView(gridDelegationTemplates, delegationDocumentsData);

                    List<OrganizationlLettersAttachments> organizationlLettersAttachments = BL.OrganizationlLettersAttachments.GetOrganizationLetterData(requestNumber);
                    HelperMethods.BindGridView(gridCopyOfOrganizationLetter, organizationlLettersAttachments);
                  

                    if (!Page.IsPostBack)
                    {
                        rowIndex = 0;
                    }
                    #region BindGrid
                    List<CalculatedDetailsForCertificate> calculatedDetailsData = BL.CalculatedDetailsForCertificate.GetCalculatedDetailsForCertificate(requestNumber);

                    if (calculatedDetailsData!=null || calculatedDetailsData.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        for (int i = 0; i < calculatedDetailsData.Count; i++)
                        {
                            dt.Rows.Add(dt.NewRow());
                        }


                        gridUniversitiesNames.DataSource = dt;
                        gridUniversitiesNames.DataBind();

                    }
                    #endregion
                }*/
            }
            //catch (Exception ex)
            //{
            //    Logging.GetInstance().LogException(ex);
            //}
            //finally
            //{

            //    Logging.GetInstance().Debug("Exiting method DisplayRequestUserControl.Page_Load");
            //}
          

         
        }
    /*
        protected void wizardDisplayRequest_PreRender(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering DisplayRequestUserControl.wizardDisplayRequest_PreRender");
            try
            {

                Repeater SideBarList = wizardDisplayRequest.FindControl("HeaderContainer").FindControl("SideBarList") as Repeater;
                SideBarList.DataSource = wizardDisplayRequest.WizardSteps;
                SideBarList.DataBind();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit DisplayRequestUserControl.wizardDisplayRequest_PreRender");
            }
        }

        public string GetClassForWizardStep(object wizardStep)
        {
            Logging.GetInstance().Debug("Entering DisplayRequestUserControl.GetClassForWizardStep");
            string stepText = string.Empty;
            try
            {
                WizardStep step = wizardStep as WizardStep;

                if (step == null)
                {
                    stepText = "";
                }
                int stepIndex = wizardDisplayRequest.WizardSteps.IndexOf(step);

                if (stepIndex < wizardDisplayRequest.ActiveStepIndex)
                {
                    stepText = "prevStep";
                }
                else if (stepIndex > wizardDisplayRequest.ActiveStepIndex)
                {
                    stepText = "nextStep";
                }
                else
                {
                    stepText = "currentStep";
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit DisplayRequestUserControl.GetClassForWizardStep");
            }
            return stepText;
        }

     */



        /*
        private void DisplayRequestData()
        {
            Logging.GetInstance().Debug("Entering method DisplayRequestUserControl.DisplayRequestData");
            try
            {
                //if (Page.Session["RequestNumber"] !=null)
                //{

                //int requestNumber =int.Parse(Page.Request.QueryString["reqNo"]);
                int requestNumber=int.Parse(Convert.ToString(Page.Session["RequestNumber"]));

                Request requestItem = BL.Request.GetRequestByNumber(requestNumber,LCID);

                string yesValue = HelperMethods.LocalizedText("ITWORX.MOEHEWF.UCE", "Yes", (uint)LCID);
                string noValue = HelperMethods.LocalizedText("ITWORX.MOEHEWF.UCE", "No", (uint)LCID);



                if (requestItem != null)
                {
                    Applicants applicant = Common.BL.Applicants.GetApplicantByID(requestItem.ApplicantID.ID, LCID);
                    if (applicant!=null)
                    {
                        lblName.Text = applicant.ApplicantName;
                        lblPersonalIDValue.Text = applicant.PersonalID;
                        lblEmailValue.Text = applicant.ApplicantEmail;
                        lblNationalityValue.Text = applicant.Nationality.SelectedTitle;
                        lblRegionNoValue.Text = applicant.Region.ToString();
                        lblStreetNoValue.Text = applicant.Street.ToString();
                        lblBuildingNoValue.Text = applicant.BuildingNo.ToString();
                        lblApartmentNoValue.Text = applicant.ApartmentNo.ToString();
                        lblDetailedAddressValue.Text = applicant.DetailedAddress.ToString();

                    }

                    divformControls.Visible = true;
                    lblNoRequest.Visible = false;

                    lblRequestNumberValue.Text = requestItem.ID.ToString();
                    lblRequestSendDateValue.Text = requestItem.SubmitDate != DateTime.MinValue ? requestItem.SubmitDate.ToShortDateString() : string.Empty;
                    lblAcademicDegreeForEquivalenceValue.Text = requestItem.AcademicDegree.SelectedTitle;
                    lblCerticateAcademicDegreeValue.Text = requestItem.AcademicDegreeForEquivalence.SelectedTitle;
                    lblCertificateThroughScholarshipValue.Text = requestItem.CertificateThroughScholarship ? yesValue : noValue;

                    if (requestItem.EntityProvidingStudy !=null)
                    {
                        lblEntityProvidingStudy.Visible = true;
                        lblEntityProvidingStudyValue.Visible = true;
                        lblEntityProvidingStudyValue.Text = requestItem.EntityProvidingStudy.SelectedTitle;

                    }
                    else if (!string.IsNullOrEmpty(requestItem.OtherEntity))
                    {
                        lblEntityProvidingStudy.Visible = true;
                        lblEntityProvidingOther.Visible = true;
                        lblEntityProvidingOtherValue.Visible = true;
                        lblEntityProvidingOtherValue.Text = requestItem.OtherEntity;
                    }
                    lblCampusStudyValue.Text = requestItem.CampusStudy ? yesValue : noValue;

                    if (!string.IsNullOrEmpty(requestItem.PlaceOfStudy))
                    {
                        lblStudyLocation.Visible = true;
                        lblStudyLocationValue.Visible = true;
                        lblStudyLocationValue.Text = Convert.ToString(requestItem.PlaceOfStudy);
                    }

                    lblCampusExamValue.Text = requestItem.CampusExam ? yesValue : noValue;

                    if (!string.IsNullOrEmpty(requestItem.PlaceOfExam))
                    {
                        lblExamLocation.Visible = true;
                        lblExamLocationValue.Visible = true;
                        lblExamLocationValue.Text = requestItem.PlaceOfExam;
                    }

                    lblCountriesValue.Text = requestItem.CountryOfStudy.SelectedTitle;
                    if (requestItem.University!=null)
                    {
                        lblUniversity.Visible = true;
                        lblUniversityValue.Visible = true;
                        lblUniversityValue.Text = requestItem.University.SelectedTitle;
                    }
                    else if (!string.IsNullOrEmpty(requestItem.UniversityNotFoundInList))
                    {
                        lblUniversity.Visible = true;
                        lblUniversityNotFound.Visible = true;
                        lblUniversityNotFoundValue.Visible = true;
                        lblUniversityNotFoundValue.Text = requestItem.UniversityNotFoundInList;
                    }
                   
                    if (requestItem.Specialization != null)
                    {
                        lblSpecialization.Visible = true;
                        lblSpecializationValue.Visible = true;
                        lblSpecializationValue.Text = requestItem.Specialization.SelectedTitle;
                    }
                    else if (!string.IsNullOrEmpty(requestItem.SpecializationNotFoundInList))
                    {
                        lblSpecialization.Visible = true;
                        lblSpecializationNotFound.Visible = true;
                        lblSpecializationNotFoundValue.Visible = true;
                        lblSpecializationNotFoundValue.Text = requestItem.SpecializationNotFoundInList;
                    }
                    if (requestItem.Faculty != null)
                    {
                        lblFaculty.Visible = true;
                        lblFacultyValue.Visible = true;
                        lblFacultyValue.Text = requestItem.Faculty.SelectedTitle;
                    }
                    else if (!string.IsNullOrEmpty(requestItem.FacultyNotFoundInList))
                    {
                        lblFaculty.Visible = true;
                        lblFacultyNotFound.Visible = true;
                        lblFacultyNotFoundValue.Visible = true;
                        lblFacultyNotFoundValue.Text = requestItem.FacultyNotFoundInList;
                    }
                    lblStudyingLanguageValue.Text = requestItem.StudyLanguage.SelectedTitle;
                    lblStudyingTypeValue.Text = requestItem.StudyType.SelectedTitle;
                    if (requestItem.StudySystem!=null)
                    {
                        lblStudyingSystem.Visible = true;
                        lblStudyingSystemValue.Visible = true;
                        lblStudyingSystemValue.Text = requestItem.StudySystem.SelectedTitle;
                    }
                    lblStartDateValue.Text = requestItem.StudyStartDate.ToShortDateString();
                    if (requestItem.StudyGraduationDate != DateTime.MinValue)
                    {
                        lblGraduationDate.Visible = true;
                        lblGraduationDateValue.Visible = true;
                        lblGraduationDateValue.Text = requestItem.StudyGraduationDate.ToShortDateString();
                    }


                    lblStudyPeriodValue.Text = requestItem.AcademicProgramPeriod.ToString();
                    lblActualStudyValue.Text = requestItem.ActualStudingPeriod.ToString();
                    if (requestItem.NumberOfHoursGained != 0)
                    {
                        lblGainedHours.Visible = true;
                        lblGainedHoursValue.Visible = true;
                        lblGainedHoursValue.Text = requestItem.NumberOfHoursGained.ToString();
                    }
                    if (!string.IsNullOrEmpty(requestItem.GPA))
                    {
                        lblGPA.Visible = true;
                        lblGPAValue.Visible = true;
                        lblGPAValue.Text = requestItem.GPA;
                    }
                    if (requestItem.NumberOfOnlineHours != 0)
                    {
                        lblOnlineHours.Visible = true;
                        lblOnlineHoursValue.Visible = true;
                        lblOnlineHoursValue.Text = requestItem.NumberOfOnlineHours.ToString();
                    }
                    if (!string.IsNullOrEmpty(requestItem.PercentageOfOnlineHours))
                    {
                        lblOnlinePercentage.Visible = true;
                        lblOnlinePercentageValue.Visible = true;
                        lblOnlinePercentageValue.Text = requestItem.PercentageOfOnlineHours;
                    }
                    if (requestItem.IsThereComprehensiveExam ==true)
                    {
                        lblCompExam.Visible = true;
                        lblCompExamValue.Visible = true;
                        lblCompExamValue.Text = requestItem.IsThereComprehensiveExam ? yesValue : noValue;
                    }

                    if (requestItem.IsThereAcceptanceExam==true)
                    {
                        lblAcceptExam.Visible = true;
                        lblAcceptExamValue.Visible = true;
                        lblAcceptExamValue.Text = requestItem.IsThereAcceptanceExam ? yesValue : noValue;
                    }
                    if (requestItem.UniversityMainHeadquarter != null)
                    {
                        lblUniversityMainHeadQuarter.Visible = true;
                        lblUniversityMainHeadQuarterValue.Visible = true;
                        lblUniversityMainHeadQuarterValue.Text = requestItem.UniversityMainHeadquarter.SelectedTitle;
                    }
                    else if (!string.IsNullOrEmpty(requestItem.NewUniversityHeadquarter))
                    {
                        lblUniversityMainHeadQuarter.Visible = true;
                        lblNewUniversityMainHeadQuarter.Visible = true;
                        lblNewUniversityMainHeadQuarterValue.Visible = true;
                        lblNewUniversityMainHeadQuarterValue.Text = requestItem.NewUniversityHeadquarter;
                    }

                    if (!string.IsNullOrEmpty(requestItem.UniversityAddress))
                    {
                        lblUniversityAddress.Visible = true;
                        lblAddressValue.Visible = true;
                        lblAddressValue.Text = requestItem.UniversityAddress;
                    }

                    lblUniversityEmailValue.Text = requestItem.UniversityEmail;


                    lblWorkingOrNotValue.Text = requestItem.WorkingOrNot ? yesValue : noValue;

                    if (requestItem.WorkingOrNot == true)
                    {
                        // set drop of occupation and entity working for

                        if (requestItem.EntityWorkingFor != null)
                        {
                            lblEntityWorkingFor.Visible = true;
                            lblEntityWorkingForValue.Visible = true;
                            lblEntityWorkingForValue.Text = requestItem.EntityWorkingFor.SelectedTitle;
                        }
                        else if (!string.IsNullOrEmpty(requestItem.OtherEntityWorkingFor))
                        {
                            lblEntityWorkingFor.Visible = true;
                            lblOtherEntityWorkingFor.Visible = true;
                            lblOtherEntityWorkingForValue.Visible = true;
                            lblOtherEntityWorkingForValue.Text = requestItem.OtherEntityWorkingFor;
                        }
                        if (requestItem.Occupation != null)
                        {
                            lblOccupation.Visible = true;
                            lblOccupationValue.Visible = true;
                            lblOccupationValue.Text = requestItem.Occupation.SelectedTitle;
                        }
                        else if (!string.IsNullOrEmpty(requestItem.OtherOccupation))
                        {
                            lblOccupation.Visible = true;
                            lblOtherOccupation.Visible = true;
                            lblOtherOccupationValue.Visible = true;
                            lblOtherOccupationValue.Text = requestItem.OtherOccupation;
                        }
                        lblHiringDate.Visible = true;
                        lblHiringDateValue.Visible = true;
                        lblWorkPhone.Visible = true;
                        lblWorkPhoneValue.Visible = true;
                        lblHiringDateValue.Text = requestItem.HiringDate.ToShortDateString();
                        lblWorkPhoneValue.Text = requestItem.OccupationPhone;

                    }
                    else
                    {
                        if (requestItem.EntityNeedsEquivalency != null)
                        {
                            lblEntityNeedsEquivalency.Visible = true;
                            lblEntityNeedsEquivalencyValue.Visible = true;
                            lblEntityNeedsEquivalencyValue.Text = requestItem.EntityNeedsEquivalency.SelectedTitle;
                        }
                        else if (!string.IsNullOrEmpty(requestItem.OtherEntityNeedsEquivalency))
                        {
                            lblEntityNeedsEquivalency.Visible = true;
                            lblOtherEntityNeedsEquivalency.Visible = true;
                            lblOtherEntityNeedsEquivalencyValue.Visible = true;
                            lblOtherEntityNeedsEquivalencyValue.Text = requestItem.OtherEntityNeedsEquivalency;
                        }
                    }
                    if (requestItem.IncomingNumber!=0)
                    {
                        lblIncomingNumber.Visible = true;
                        lblIncomingNumberValue.Visible = true;
                        lblIncomingNumberValue.Text = requestItem.IncomingNumber.ToString();

                    }
                    if (requestItem.BookDate != DateTime.MinValue)
                    {
                        lblDate.Visible = true;
                        lblDateValue.Visible = true;
                        lblDateValue.Text = requestItem.BookDate.ToShortDateString();
                    }
                    if (!string.IsNullOrEmpty(requestItem.BarCode))
                    {
                        lblBarCode.Visible = true;
                        lblBarCodeValue.Visible = true;
                        lblBarCodeValue.Text = requestItem.BarCode;
                    
                    }

                }

                // }
                else
                {
                    divformControls.Visible = false;
                    lblNoRequest.Visible = true;
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method DisplayRequestUserControl.DisplayRequestData");
            }

        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method DisplayRequestUserControl.lnkDownload_Click");
            try
            {
                int itemId = int.Parse((sender as LinkButton).CommandArgument);
                BusinessHelper.DownloadFile(itemId, Utilities.Constants.DelegationTemplate);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method DisplayRequestUserControl.lnkDownload_Click");
            }
           
        }

        protected void lnkDownloadCopy_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method DisplayRequestUserControl.lnkDownloadCopy_Click");
            try
            {
                int itemId = int.Parse((sender as LinkButton).CommandArgument);
                BusinessHelper.DownloadFile(itemId, Utilities.Constants.OrganizationalLettersAttachments);

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method DisplayRequestUserControl.lnkDownloadCopy_Click");
            }
           
        }

        protected void gridUniversitiesNames_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Logging.GetInstance().Debug("Entering method DisplayRequestUserControl.gridUniversitiesNames_RowDataBound");
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (Page.Session["RequestNumber"] != null /*Page.Request.QueryString["reqNo"] != null*//*)
                    {
                        int requestNumber=int.Parse(Convert.ToString(Page.Session["RequestNumber"]));
                      //  int requestNumber = int.Parse(Page.Request.QueryString["reqNo"]);
                        //gridUniversitiesNames.Rows[0].Visible = false;
                      /*  Label countryLabel = (Label)e.Row.FindControl("lblUniGridCountry");
                        Label universityLabel = (Label)e.Row.FindControl("lblUniGridUniversity");
                        Label facultyLabel = (Label)e.Row.FindControl("lblUniGridFaculty");
                        Label studyingSystemLabel = (Label)e.Row.FindControl("lblUniGridStudyingSystem");
                        Label studyingTypeLabel = (Label)e.Row.FindControl("lblUniGridStudyingType");
                        Label studyingPeriodLabel = (Label)e.Row.FindControl("lblUniGridStudingPeriod");

                        List<CalculatedDetailsForCertificate> calculatedDetailsForCertificate = BL.CalculatedDetailsForCertificate.GetCalculatedDetailsForCertificate(requestNumber);
                        if ((calculatedDetailsForCertificate!=null && calculatedDetailsForCertificate.Count > 0) && rowIndex < calculatedDetailsForCertificate.Count)
                        {
                            CalculatedDetailsForCertificate calculatedDetailsForCertificateItem = calculatedDetailsForCertificate[rowIndex];
                                      
                            if (!string.IsNullOrEmpty(calculatedDetailsForCertificateItem.Country.SelectedID))
                            {
                                countryLabel.Text = calculatedDetailsForCertificateItem.Country.SelectedTitle;
                            }
                            if (!string.IsNullOrEmpty(calculatedDetailsForCertificateItem.Univesrity.SelectedID))
                            {
                                universityLabel.Text = calculatedDetailsForCertificateItem.Univesrity.SelectedTitle;
                            }
                            if (!string.IsNullOrEmpty(calculatedDetailsForCertificateItem.Faculty.SelectedID))
                            {
                               facultyLabel.Text= calculatedDetailsForCertificateItem.Faculty.SelectedTitle;
                            }
                            if (!string.IsNullOrEmpty(calculatedDetailsForCertificateItem.StudySystem.SelectedID))
                            {
                                studyingSystemLabel.Text = calculatedDetailsForCertificateItem.StudySystem.SelectedTitle;
                            }
                            if (!string.IsNullOrEmpty(calculatedDetailsForCertificateItem.StudyType.SelectedID))
                            {
                               studyingTypeLabel.Text= calculatedDetailsForCertificateItem.StudyType.SelectedTitle;
                            }
                            if (!string.IsNullOrEmpty(calculatedDetailsForCertificateItem.StudyingPeriod))
                            {
                                studyingPeriodLabel.Text = calculatedDetailsForCertificateItem.StudyingPeriod;
                            }
                            rowIndex++;

                        }
                        else
                        {
                            gridUniversitiesNames.Visible = false;
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

                Logging.GetInstance().Debug("Exiting method DisplayRequestUserControl.gridUniversitiesNames_RowDataBound");
            }

        }
        */
   
   // }
}
