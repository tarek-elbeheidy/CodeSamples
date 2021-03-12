using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using System.Collections.Generic;
using ITWORX.MOEHE.Utilities;
using Microsoft.SharePoint;
using System.Data;
using ITWORX.MOEHEWF.UCE.Entities;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class DisplayRequestDetails : UserControlBase
    {
        private int rowIndex = 0;
        #region Protected Variables
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadOrgAttach;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCalculatedDetails;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCertificates;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadDelegates;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadNotWorking;
       // protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadGeneralSec;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadSequenceCert;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadDiploma;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadInterDiploma;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCertificateEquivalent;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadGrades;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadHavePA;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadNoHavePA;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method DisplayRequestDetails.Page_Load");
            try
            {
               

                //This will be removed when session is used
                if (/*Page.Request.QueryString["reqNo"] != null*/ Page.Session["DisplayRequestId"] != null)
                {
                    DisplayRequestData();
                    BindAttachments();

                    if (HelperMethods.GetGroupUsers(Common.Utilities.Constants.SecretaryGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName))
                    {

                        btn_Print.Visible = true;
                    }
                    else
                        btn_Print.Visible = false;
                    // int requestId = int.Parse(Convert.ToString(Page.Session["DisplayRequestId"]));
                    //  int requestNumber=int.Parse(Convert.ToString(Page.Session["RequestNumber"]));
                    // List<Entities.DelegationDocuments> delegationDocumentsData = BL.DelegationDocuments.GetGelegatesDocuments(requestId, LCID);
                    //HelperMethods.BindGridView(gridDelegationTemplates, delegationDocumentsData);

                    // List<Entities.OrganizationlLettersAttachments> organizationlLettersAttachments = BL.OrganizationlLettersAttachments.GetOrganizationLetterData(requestId);
                    // HelperMethods.BindGridView(gridCopyOfOrganizationLetter, organizationlLettersAttachments);


                    if (!Page.IsPostBack)
                    {
                        rowIndex = 0;
                        

                    }
                  
                    #region BindGrid
                    /*
                    List<Entities.CalculatedDetailsForCertificate> calculatedDetailsData = BL.CalculatedDetailsForCertificate.GetCalculatedDetailsForCertificate(requestNumber);

                    if (calculatedDetailsData != null || calculatedDetailsData.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        for (int i = 0; i < calculatedDetailsData.Count; i++)
                        {
                            dt.Rows.Add(dt.NewRow());
                        }


                        gridUniversitiesNames.DataSource = dt;
                        gridUniversitiesNames.DataBind();

                    }*/
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method DisplayRequestDetails.Page_Load");
            }

        }

        private void DisplayRequestData()
        {
            Logging.GetInstance().Debug("Entering method DisplayRequestDetails.DisplayRequestData");
            try
            {
                //if (Page.Session["RequestNumber"] !=null)
                //{

                //int requestNumber =int.Parse(Page.Request.QueryString["reqNo"]);

                int requestId = int.Parse(Convert.ToString(Page.Session["DisplayRequestId"]));
                string yesValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Yes", (uint)LCID);
               string noValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "No", (uint)LCID);
               Request requestItem = BL.Request.GetRequestByNumber(requestId, LCID);

            


                if (requestItem != null)
                {
                   

                    divformControls.Visible = true;
                    lblNoRequest.Visible = false;

                    lblRequestNumberValue.Text = requestItem.RequestNumber;
                    lblRequestCreationDateValue.Text = ExtensionMethods.QatarFormatedDate(requestItem.RequestCreationDate);
                    lblRequestSubmitDateValue.Text = requestItem.SubmitDate != DateTime.MinValue ? ExtensionMethods.QatarFormatedDate(requestItem.SubmitDate) : string.Empty;
                    lblAcademicDegreeForEquivalenceValue.Text = requestItem.AcademicDegree!=null? requestItem.AcademicDegree.SelectedTitle:string.Empty;
                    lblCerticateAcademicDegreeValue.Text = requestItem.AcademicDegreeForEquivalence!=null? requestItem.AcademicDegreeForEquivalence.SelectedTitle:string.Empty;
                    hdnCertificateAcademic.Value= requestItem.AcademicDegreeForEquivalence != null ? requestItem.AcademicDegreeForEquivalence.SelectedID : string.Empty;
                    hdnCertificateAcademicTxt.Value = requestItem.AcademicDegreeForEquivalence != null ? requestItem.AcademicDegreeForEquivalence.SelectedTitle : string.Empty;
                    lblCertificateThroughScholarshipValue.Text = requestItem.CertificateThroughScholarship ? yesValue : noValue;

                    if (!string.IsNullOrEmpty(requestItem.EntityProvidingStudy ))
                    {
                        lblEntityProvidingStudy.Visible = true;
                        lblEntityProvidingStudyValue.Text = requestItem.EntityProvidingStudy;

                    }
                    //else if (!string.IsNullOrEmpty(requestItem.OtherEntity))
                    //{
                       
                    //    lblEntityProvidingOther.Visible = true;
                    //    lblEntityProvidingOtherValue.Visible = true;
                    //    lblEntityProvidingOtherValue.Text = requestItem.OtherEntity;
                    //}
                    //lblCampusStudyValue.Text = requestItem.CampusStudy ? yesValue : noValue;

                    //if (!string.IsNullOrEmpty(requestItem.PlaceOfStudy))
                    //{
                    //    lblStudyLocation.Visible = true;
                    //    lblStudyLocationValue.Visible = true;
                    //    lblStudyLocationValue.Text = Convert.ToString(requestItem.PlaceOfStudy);
                    //}

                    //lblCampusExamValue.Text = requestItem.CampusExam ? yesValue : noValue;

                    //if (!string.IsNullOrEmpty(requestItem.PlaceOfExam))
                    //{
                    //    lblExamLocation.Visible = true;
                    //    lblExamLocationValue.Visible = true;
                    //    lblExamLocationValue.Text = requestItem.PlaceOfExam;
                    //}

                    lblCountriesValue.Text = requestItem.CountryOfStudy.SelectedTitle;
                    if (requestItem.University != null && !string.IsNullOrEmpty(requestItem.University.SelectedID))
                    {
                   
                        
                        lblUniversityValue.Text = requestItem.University.SelectedTitle;
                   
                        if (Common.BL.University.IsUniversityCHED(int.Parse(requestItem.University.SelectedID)))
                        {
                            lblUniversityCHED.Visible = true;
                            lblUniversityCHED.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CHEDMessage", (uint)LCID), requestItem.University.SelectedTitle);

                        }
                     
                    }
                    else if (!string.IsNullOrEmpty(requestItem.UniversityNotFoundInList))
                    {
                   
                        //lblUniversityNotFound.Visible = true;
                        lblUniversityNotFoundValue.Visible = true;
                        lblUniversityNotFoundValue.Text = requestItem.UniversityNotFoundInList;
                    }
                    //if (requestItem.UniversityList !=null)
                    //{
                    //    lblUniversityList.Visible = true;
                    //    lblUniversityListValue.Visible = true;
                    //    lblUniversityListValue.Text = requestItem.UniversityList;
                    //}
                    if (requestItem.Specialization != null)
                    {
                       
                        lblSpecializationValue.Text = requestItem.Specialization.SelectedTitle;
                    }
                    else if (!string.IsNullOrEmpty(requestItem.SpecializationNotFoundInList))
                    {
                        
                        //lblSpecializationNotFound.Visible = true;
                        lblSpecializationNotFoundValue.Visible = true;
                        lblSpecializationNotFoundValue.Text = requestItem.SpecializationNotFoundInList;
                    }
                    if (requestItem.Faculty != null)
                    {
                       
                        lblFacultyValue.Text = requestItem.Faculty;
                    }
                    else if (!string.IsNullOrEmpty(requestItem.FacultyNotFoundInList))
                    {
                       
                       // lblFacultyNotFound.Visible = true;
                        lblFacultyNotFoundValue.Visible = true;
                        lblFacultyNotFoundValue.Text = requestItem.FacultyNotFoundInList;
                    }
                    lblStudyingLanguageValue.Text = requestItem.StudyLanguage.SelectedTitle;
                   // lblStudyingTypeValue.Text = requestItem.StudyType.SelectedTitle;
                    if (requestItem.StudySystem != null)
                    {
                     
                        lblStudyingSystemValue.Text = requestItem.StudySystem.SelectedTitle;
                    }
                    lblStartDateValue.Text = ExtensionMethods.QatarFormatedDate(requestItem.StudyStartDate);
                    if (requestItem.StudyGraduationDate != DateTime.MinValue)
                    {
                      
                        lblGraduationDateValue.Text = ExtensionMethods.QatarFormatedDate(requestItem.StudyGraduationDate);
                    }


                    //lblStudyPeriodValue.Text = requestItem.AcademicProgramPeriod.ToString();
                    //lblActualStudyValue.Text = requestItem.ActualStudingPeriod.ToString();
                    //if (requestItem.NumberOfHoursGained != 0)
                    //{
                        
                    //    lblGainedHoursValue.Text = requestItem.NumberOfHoursGained.ToString();
                    //}
                    //if (!string.IsNullOrEmpty(requestItem.GPA))
                    //{
                      
                    //    lblGPAValue.Text = requestItem.GPA;
                    //}
                    //if (requestItem.NumberOfOnlineHours != 0)
                    //{
                       
                    //    lblOnlineHoursValue.Text = requestItem.NumberOfOnlineHours.ToString();
                    //}
                    //if (!string.IsNullOrEmpty(requestItem.PercentageOfOnlineHours))
                    //{
                      
                    //    lblOnlinePercentageValue.Text = requestItem.PercentageOfOnlineHours;
                    //}
                    //if (requestItem.IsThereComprehensiveExam == true)
                    //{
                       
                    //    lblCompExamValue.Text = requestItem.IsThereComprehensiveExam ? yesValue : noValue;
                    //}

                    //if (requestItem.IsThereAcceptanceExam == true)
                    //{
                       
                    //    lblAcceptExamValue.Text = requestItem.IsThereAcceptanceExam ? yesValue : noValue;
                    //}
                    if (requestItem.UniversityMainHeadquarter != null)
                    {
                        
                        lblUniversityMainHeadQuarterValue.Text = requestItem.UniversityMainHeadquarter.SelectedTitle;
                    }
                    else if (!string.IsNullOrEmpty(requestItem.NewUniversityHeadquarter))
                    {
                      
                        //lblNewUniversityMainHeadQuarter.Visible = true;
                        lblNewUniversityMainHeadQuarterValue.Visible = true;
                        lblNewUniversityMainHeadQuarterValue.Text = requestItem.NewUniversityHeadquarter;
                    }

                    if (!string.IsNullOrEmpty(requestItem.UniversityAddress))
                    {
                        
                        lblAddressValue.Text = requestItem.UniversityAddress;
                    }
                    if (!string.IsNullOrEmpty(requestItem.UniversityEmail))
                    {
                        lblUniversityEmailValue.Text = requestItem.UniversityEmail;
                    }

                    if (!string.IsNullOrEmpty(requestItem.UniversityType))
                    {
                        if (LCID == (int)Language.English)
                        {
                            lblUniversityTypeValue.Text = requestItem.UniversityType;
                        }
                        else
                        {
                            if (requestItem.UniversityType == "Government")
                            {
                                lblUniversityTypeValue.Text = "حكومية";
                            }
                            else
                                lblUniversityTypeValue.Text = "خاصة";
                        }
                    }
                    else
                        divUniversityType.Visible = false;

                    lblHavePAValue.Text = requestItem.HavePAOrNot ? yesValue : noValue;

                    if (requestItem.HavePAOrNot == true)
                    {
                        fileUploadNoHavePA.Visible = false;
                    }
                    else
                    {
                        fileUploadHavePA.Visible = false;
                    }

                    lblWorkingOrNotValue.Text = requestItem.WorkingOrNot ? yesValue : noValue;

                    if (requestItem.WorkingOrNot == true)
                    {
                        // set drop of occupation and entity working for

                        if (requestItem.EntityWorkingFor != null)
                        {
                            WorkingFor.Visible = true;
                            lblEntityWorkingFor.Visible = true;
                            lblEntityWorkingForValue.Visible = true;
                            lblEntityWorkingForValue.Text = requestItem.EntityWorkingFor.SelectedTitle;
                        }
                        else if (!string.IsNullOrEmpty(requestItem.OtherEntityWorkingFor))
                        {
                            WorkingFor.Visible = true;
                            lblEntityWorkingFor.Visible = true;
                            //lblOtherEntityWorkingFor.Visible = true;
                            lblOtherEntityWorkingForValue.Visible = true;
                            lblOtherEntityWorkingForValue.Text = requestItem.OtherEntityWorkingFor;
                        }
                        fileUploadNotWorking.Visible = false;

                        //if (!string.IsNullOrEmpty(requestItem.Occupation))
                        //{
                        //    divOccupation.Visible = true;
                        //    lblOccupation.Visible = true;
                        //    lblOccupationValue.Visible = true;
                        //    lblOccupationValue.Text = requestItem.Occupation;
                        //}
                        //else if (!string.IsNullOrEmpty(requestItem.OtherOccupation))
                        //{
                        //    lblOccupation.Visible = true;
                        //    //lblOtherOccupation.Visible = true;
                        //    lblOtherOccupationValue.Visible = true;
                        //    lblOtherOccupationValue.Text = requestItem.OtherOccupation;
                        //}
                        //lblHiringDate.Visible = true;
                        //lblHiringDateValue.Visible = true;
                        //lblWorkPhone.Visible = true;
                        //lblWorkPhoneValue.Visible = true;
                        //lblHiringDateValue.Text = ExtensionMethods.QatarFormatedDate(requestItem.HiringDate);
                        //lblWorkPhoneValue.Text = requestItem.OccupationPhone;
                        
                        //Ahmed
                        if (requestItem.EntityNeedsEquivalency != null)
                        {
                            workEntityNeedsEquivalency.Visible = true;
                            lblEntityNeedsEquivalency.Visible = true;
                            lblEntityNeedsEquivalencyValue.Visible = true;
                            lblEntityNeedsEquivalencyValue.Text = requestItem.EntityNeedsEquivalency.SelectedTitle;
                        }
                        else if (!string.IsNullOrEmpty(requestItem.OtherEntityNeedsEquivalency))
                        {
                            workEntityNeedsEquivalency.Visible = true;
                            lblEntityNeedsEquivalency.Visible = true;
                            //lblOtherEntityNeedsEquivalency.Visible = true;
                            lblOtherEntityNeedsEquivalencyValue.Visible = true;
                            lblOtherEntityNeedsEquivalencyValue.Text = requestItem.OtherEntityNeedsEquivalency;
                        }
                    }
                    else
                    {
                        if (requestItem.EntityNeedsEquivalency != null)
                        {
                            workEntityNeedsEquivalency.Visible = true;
                            lblEntityNeedsEquivalency.Visible = true;
                            lblEntityNeedsEquivalencyValue.Visible = true;
                            lblEntityNeedsEquivalencyValue.Text = requestItem.EntityNeedsEquivalency.SelectedTitle;
                        }
                        else if (!string.IsNullOrEmpty(requestItem.OtherEntityNeedsEquivalency))
                        {
                            workEntityNeedsEquivalency.Visible = true;
                            lblEntityNeedsEquivalency.Visible = true;
                            //lblOtherEntityNeedsEquivalency.Visible = true;
                            lblOtherEntityNeedsEquivalencyValue.Visible = true;
                            lblOtherEntityNeedsEquivalencyValue.Text = requestItem.OtherEntityNeedsEquivalency;
                        }
                        fileUploadOrgAttach.Visible = false;
                    }
                    //if (requestItem.IncomingNumber != 0)
                    //{
                     
                    //    lblIncomingNumberValue.Text = requestItem.IncomingNumber.ToString();

                    //}
                    //if (requestItem.BookDate != DateTime.MinValue)
                    //{
                       
                    //    lblDateValue.Text = ExtensionMethods.QatarFormatedDate(requestItem.BookDate);
                    //}
                    //if (!string.IsNullOrEmpty(requestItem.BarCode))
                    //{
                       
                    //    lblBarCodeValue.Text = requestItem.BarCode;

                    //}

                    //Bind calculated certificate repeater
                   
                    List<CalculatedDetailsForCertificate> calculatedDetailsData = BL.CalculatedDetailsForCertificate.GetCalculatedDetailsForCertificate(requestId, LCID);
                    if (calculatedDetailsData != null && calculatedDetailsData.Count > 0)
                    {

                        repCalculatedDetailsForCertificate.DataSource = calculatedDetailsData;
                        repCalculatedDetailsForCertificate.DataBind();
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

                Logging.GetInstance().Debug("Exiting method DisplayRequestDetails.DisplayRequestData");
            }

        }
        private void BindAttachments ()
        {
            Logging.GetInstance().Debug("Entering method DisplayRequestDetails.BindAttachments");
            try
            {
                if (WorkingFor.Visible)
                {
                    fileUploadOrgAttach.DocumentLibraryName = Utilities.Constants.OrganizationalLettersAttachments;
                    fileUploadOrgAttach.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadOrgAttach.Group = "CopyOfOrganizationlLetter";
                    fileUploadOrgAttach.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                    fileUploadOrgAttach.Enabled = false;
                    fileUploadOrgAttach.Bind();
                }
                else
                {

                    fileUploadNotWorking.DocumentLibraryName = Utilities.Constants.OrganizationalLettersAttachments;
                    fileUploadNotWorking.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadNotWorking.Group = "NotWorkingCopyOfOrganizationlLetter";
                    fileUploadNotWorking.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                    fileUploadNotWorking.Enabled = false;
                    fileUploadNotWorking.Bind();
                }

                fileUploadHavePA.DocumentLibraryName = Utilities.Constants.HavePAAttachments;
                fileUploadHavePA.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadHavePA.Group = "YesHavePAAttachments";
                fileUploadHavePA.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "DecisionPriorApproval", (uint)LCID);
                fileUploadHavePA.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileUploadHavePA.Enabled = false;
                fileUploadHavePA.Bind();

                fileUploadNoHavePA.DocumentLibraryName = Utilities.Constants.HavePAAttachments;
                fileUploadNoHavePA.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadNoHavePA.Group = "NoHavePAAttachments";
                fileUploadNoHavePA.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ApprovalNoPriorApproval", (uint)LCID);
                fileUploadNoHavePA.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileUploadNoHavePA.Enabled = false;
                fileUploadNoHavePA.Bind();

                fileUploadCertificates.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                fileUploadCertificates.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadCertificates.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "PreviousCertificates", (uint)LCID);
                fileUploadCertificates.Group = hdnCertificateAcademic.Value;
                fileUploadCertificates.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileUploadCertificates.Enabled = false;
                fileUploadCertificates.Bind();

                //fileUploadGeneralSec.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                //fileUploadGeneralSec.DocLibWebUrl = SPContext.Current.Site.Url;
                //fileUploadGeneralSec.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "SecondarySchoolCertificate", (uint)LCID);
                //fileUploadGeneralSec.Group = "GeneralSecondary"+hdnCertificateAcademic.Value;
                //fileUploadGeneralSec.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                //fileUploadGeneralSec.Enabled = false;
                //fileUploadGeneralSec.Bind();

                fileUploadSequenceCert.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                fileUploadSequenceCert.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadSequenceCert.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CertificateSequence", (uint)LCID);
                fileUploadSequenceCert.Group = "CertificateSequence" + hdnCertificateAcademic.Value;
                fileUploadSequenceCert.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileUploadSequenceCert.Enabled = false;
                fileUploadSequenceCert.Bind();



                fileUploadCertificateEquivalent.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                fileUploadCertificateEquivalent.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadCertificateEquivalent.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CertificateToBeEquivalent", (uint)LCID);
                fileUploadCertificateEquivalent.Group = "CertificateEquivalentAttachment";
                fileUploadCertificateEquivalent.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileUploadCertificateEquivalent.Enabled = false;
                fileUploadCertificateEquivalent.Bind();


                fileUploadGrades.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                fileUploadGrades.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadGrades.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Grades", (uint)LCID);
                fileUploadGrades.Group = "GardesAttachment";
                fileUploadGrades.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileUploadGrades.Enabled = false;
                fileUploadGrades.Bind();


                string bachelor = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Bachelor", (uint)LCID);
                if (hdnCertificateAcademicTxt.Value.Equals(bachelor))
                {
                    fileUploadDiploma.Visible = true;
                    fileUploadDiploma.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                    fileUploadDiploma.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadDiploma.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Diploma", (uint)LCID);
                    fileUploadDiploma.Group = "Diploma" + hdnCertificateAcademic.Value;
                    fileUploadDiploma.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                    fileUploadDiploma.Enabled = false;
                    fileUploadDiploma.Bind();

                    fileUploadInterDiploma.Visible = true;
                    fileUploadInterDiploma.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                    fileUploadInterDiploma.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadInterDiploma.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "IntermediateDiploma", (uint)LCID);
                    fileUploadInterDiploma.Group = "InterMediateDiploma" + hdnCertificateAcademic.Value;
                    fileUploadInterDiploma.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                    fileUploadInterDiploma.Enabled = false;
                    fileUploadInterDiploma.Bind();

                }


                fileUploadDelegates.DocumentLibraryName = Utilities.Constants.DelegationDocuments;
                fileUploadDelegates.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadDelegates.Group = "DelegationTemplates";
                fileUploadDelegates.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileUploadDelegates.Enabled = false;
                fileUploadDelegates.Bind();

                if (ViewState["DisplayCalculatedDetails"] != null)
                {



                    fileUploadCalculatedDetails.DocumentLibraryName = Utilities.Constants.CalculatedDetailsForCertificateAttachments;
                    fileUploadCalculatedDetails.DocLibWebUrl = SPContext.Current.Site.Url;
                    //fileUploadCalculatedDetails.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CalculatedDetailsAttachText", (uint)LCID);
                    fileUploadCalculatedDetails.Group = ViewState["DisplayCalculatedDetails"].ToString();
                    fileUploadCalculatedDetails.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                    fileUploadCalculatedDetails.Enabled = false;
                    fileUploadCalculatedDetails.Bind();
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method DisplayRequestDetails.BindAttachments");
            }

        }
            protected void gridUniversitiesNames_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           Logging.GetInstance().Debug("Entering method DisplayRequestDetails.gridUniversitiesNames_RowDataBound");
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (Page.Session["DisplayRequestId"] != null /*Page.Request.QueryString["reqNo"] != null*/)
                    {
                        int requestId = int.Parse(Convert.ToString(Page.Session["DisplayRequestId"]));
                        //  int requestNumber = int.Parse(Page.Request.QueryString["reqNo"]);
                        //gridUniversitiesNames.Rows[0].Visible = false;
                        Label countryLabel = (Label)e.Row.FindControl("lblUniGridCountry");
                        Label universityLabel = (Label)e.Row.FindControl("lblUniGridUniversity");
                        Label facultyLabel = (Label)e.Row.FindControl("lblUniGridFaculty");
                        Label studyingSystemLabel = (Label)e.Row.FindControl("lblUniGridStudyingSystem");
                        Label studyingTypeLabel = (Label)e.Row.FindControl("lblUniGridStudyingType");
                        Label studyingPeriodLabel = (Label)e.Row.FindControl("lblUniGridStudingPeriod");

                       List<Entities.CalculatedDetailsForCertificate> calculatedDetailsForCertificate = BL.CalculatedDetailsForCertificate.GetCalculatedDetailsForCertificate(requestId, LCID);
                        if ((calculatedDetailsForCertificate != null && calculatedDetailsForCertificate.Count > 0) && rowIndex < calculatedDetailsForCertificate.Count)
                        {
                            Entities.CalculatedDetailsForCertificate calculatedDetailsForCertificateItem = calculatedDetailsForCertificate[rowIndex];

                            if (!string.IsNullOrEmpty(calculatedDetailsForCertificateItem.Country.SelectedID))
                            {
                                countryLabel.Text = calculatedDetailsForCertificateItem.Country.SelectedTitle;
                            }
                            if (!string.IsNullOrEmpty(calculatedDetailsForCertificateItem.Univesrity.SelectedID))
                            {
                                universityLabel.Text = calculatedDetailsForCertificateItem.Univesrity.SelectedTitle;
                            }
                            if (!string.IsNullOrEmpty(calculatedDetailsForCertificateItem.Faculty))
                            {
                                facultyLabel.Text = calculatedDetailsForCertificateItem.Faculty;
                            }
                            if (!string.IsNullOrEmpty(calculatedDetailsForCertificateItem.StudySystem.SelectedID))
                            {
                                studyingSystemLabel.Text = calculatedDetailsForCertificateItem.StudySystem.SelectedTitle;
                            }
                            //if (!string.IsNullOrEmpty(calculatedDetailsForCertificateItem.StudyType.SelectedID))
                            //{
                            //    studyingTypeLabel.Text = calculatedDetailsForCertificateItem.StudyType.SelectedTitle;
                            //}
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

                Logging.GetInstance().Debug("Exiting method DisplayRequestDetails.gridUniversitiesNames_RowDataBound");
            }
        }


        protected void lnkCalcSectionDisplayAttach_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method DisplayRequestDetails.lnkCalcSectionDisplayAttach_Click");
            try
            {
               
                RepeaterItem repeaterItem = (sender as LinkButton).NamingContainer as RepeaterItem;
               // HiddenField hdnCalcId = (HiddenField)repeaterItem.FindControl("hdnCalcSectionID");
                //if (!string.IsNullOrEmpty(hdnCalcId.Value))
                //{
                    fileUploadCalculatedDetails.Visible = true;
                 
                fileUploadCalculatedDetails.DocumentLibraryName = Utilities.Constants.CalculatedDetailsForCertificateAttachments;
                fileUploadCalculatedDetails.DocLibWebUrl = SPContext.Current.Site.Url;
                //fileUploadCalculatedDetails.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CalculatedDetailsAttachText", (uint)LCID);
                fileUploadCalculatedDetails.Group = repeaterItem.ItemIndex.ToString();
                fileUploadCalculatedDetails.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileUploadCalculatedDetails.Enabled = false;
                fileUploadCalculatedDetails.Bind();
                ViewState["DisplayCalculatedDetails"] = repeaterItem.ItemIndex.ToString();
                // }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method DisplayRequestDetails.lnkCalcSectionDisplayAttach_Click");
            }
        }

            //protected void lnkDownloadCopy_Click(object sender, EventArgs e)
            //{
            //    Logging.GetInstance().Debug("Entering method DisplayRequestDetails.lnkDownloadCopy_Click");
            //    try
            //    {
            //        int itemId = int.Parse((sender as LinkButton).CommandArgument);
            //        Utilities.BusinessHelper.DownloadFile(itemId, Utilities.Constants.OrganizationalLettersAttachments);

            //    }
            //    catch (Exception ex)
            //    {
            //        Logging.GetInstance().LogException(ex);
            //    }
            //    finally
            //    {

            //        Logging.GetInstance().Debug("Exiting method DisplayRequestDetails.lnkDownloadCopy_Click");
            //    }

            //}

            //protected void lnkDownload_Click(object sender, EventArgs e)
            //{
            //    Logging.GetInstance().Debug("Entering method DisplayRequestDetails.lnkDownload_Click");
            //    try
            //    {
            //        int itemId = int.Parse((sender as LinkButton).CommandArgument);
            //        Utilities.BusinessHelper.DownloadFile(itemId, Utilities.Constants.DelegationTemplate);
            //    }
            //    catch (Exception ex)
            //    {
            //        Logging.GetInstance().LogException(ex);
            //    }
            //    finally
            //    {

            //        Logging.GetInstance().Debug("Exiting method DisplayRequestDetails.lnkDownload_Click");
            //    }
            //}
        }
        }
