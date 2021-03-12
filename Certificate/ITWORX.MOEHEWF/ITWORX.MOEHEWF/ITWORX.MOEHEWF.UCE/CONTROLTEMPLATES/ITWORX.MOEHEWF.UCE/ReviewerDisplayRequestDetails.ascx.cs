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
    public partial class ReviewerDisplayRequestDetails : UserControlBase
    {
        private int rowIndex = 0;
        #region Protected Variables
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadOrgAttach;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload OrgAttach;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCalculatedDetails;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCertificates;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload Certificates;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadDelegates;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload Delegates;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadNotWorking;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadGeneralSec;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload GeneralSecondary;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadSequenceCert;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload CertificateSequence;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadDiploma;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileDiploma;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadInterDiploma;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileInterDiploma;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCertificateEquivalent;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadGrades;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileEquivalent;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileGrades;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadHavePA;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadNoHavePA;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method DisplayRequestDetails.Page_Load");
            try
            {

                //This will be removed when session is used
                if (Page.Session["DisplayRequestId"] != null)
                {
                    DisplayRequestData();
                    BindAttachments();

                    //bool userInGroup = HelperMethods.InGroup(Common.Utilities.Constants.SecretaryGroupName);

                    //if (userInGroup)
                    //{ 

                    //    btn_Print.Visible = true;
                    //}
                    //else
                    //    btn_Print.Visible = false;

                    if (!Page.IsPostBack)
                    {
                        rowIndex = 0;


                    }

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
                int requestId = int.Parse(Convert.ToString(Page.Session["DisplayRequestId"]));
                string yesValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Yes", (uint)LCID);
                string noValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "No", (uint)LCID);
                Request requestItem = BL.Request.Reviewer_GetRequestByNumber(requestId, LCID);


                if (requestItem != null)
                {
                    Common.Entities.Applicants applicant = Common.BL.Applicants.GetApplicantByID(requestItem.ApplicantID.ID, LCID);
                    if (applicant != null)
                    {
                        lblNameValue.Text = LCID == (int)Language.English ? applicant.EnglishName : applicant.ArabicName;
                        lblPersonalIDValue.Text = applicant.PersonalID;
                    }
                    divformControls.Visible = true;
                    lblNoRequest.Visible = false;

                    lblRequestNumberValue.Text = requestItem.RequestNumber;
                    lblRequestCreationDateValue.Text = ExtensionMethods.QatarFormatedDate(requestItem.RequestCreationDate);
                    lblRequestSubmitDateValue.Text = requestItem.SubmitDate != DateTime.MinValue ? ExtensionMethods.QatarFormatedDate(requestItem.SubmitDate) : string.Empty;
                    lblAcademicDegreeForEquivalenceValue.Text = requestItem.AcademicDegree != null ? requestItem.AcademicDegree.SelectedTitle : string.Empty;
                    lblCerticateAcademicDegreeValue.Text = requestItem.AcademicDegreeForEquivalence != null ? requestItem.AcademicDegreeForEquivalence.SelectedTitle : string.Empty;
                    hdnCertificateAcademic.Value = requestItem.AcademicDegreeForEquivalence != null ? requestItem.AcademicDegreeForEquivalence.SelectedID : string.Empty;
                    hdnCertificateAcademicTxt.Value = requestItem.AcademicDegreeForEquivalence != null ? requestItem.AcademicDegreeForEquivalence.SelectedTitle : string.Empty;
                    lblCertificateThroughScholarshipValue.Text = requestItem.CertificateThroughScholarship ? yesValue : noValue;

                    if (!string.IsNullOrEmpty(requestItem.EntityProvidingStudy))
                    {
                        lblEntityProvidingStudy.Visible = true;
                        lblEntityProvidingStudyValue.Text = requestItem.EntityProvidingStudy;

                    }

                    lblCampusStudyValue.Text = requestItem.CampusStudy ? yesValue : noValue;


                    lblCampusExamValue.Text = requestItem.CampusExam ? yesValue : noValue;


                    lblCountriesValue.Text = requestItem.CountryOfStudy.SelectedTitle;
                    if (string.IsNullOrEmpty(requestItem.CountryOfStudy.SelectedID))
                    {
                        lblNewCoutriesAdded.Visible = true;
                    }
                    if (requestItem.University != null && !string.IsNullOrEmpty(requestItem.University.SelectedID))
                    {

                        lblUniversityValue.Text = requestItem.University.SelectedTitle;
                        if (Common.BL.University.IsUniversityCHED(int.Parse(requestItem.University.SelectedID)))
                        {

                            lblUniversityCHED.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CHEDMessage", (uint)LCID), requestItem.University.SelectedTitle);
                            lblUniversityHEDD.Text = string.Empty;
                        }
                        if (Common.BL.University.IsUniversityHEDD(int.Parse(requestItem.University.SelectedID)))
                        {
                            lblUniversityHEDD.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "HEDDMessage", (uint)LCID), requestItem.University.SelectedTitle);
                            lblUniversityCHED.Text = string.Empty;

                        }
                    }
                    else if (!string.IsNullOrEmpty(requestItem.UniversityNotFoundInList))
                    {
                        lblUniversityNotFoundValue.Visible = true;
                        lblNewUniversitiesAdded.Visible = true;
                        lblUniversityNotFoundValue.Text = requestItem.UniversityNotFoundInList;
                    }
                    if (requestItem.UniversityList != null)
                    {
                        lblUniversityList.Visible = true;
                        lblUniversityListValue.Visible = true;
                        lblUniversityListValue.Text = requestItem.UniversityList;
                    }
                    if (requestItem.Specialization != null)
                    {

                        lblSpecializationValue.Text = requestItem.Specialization.SelectedTitle;
                    }
                    else if (!string.IsNullOrEmpty(requestItem.SpecializationNotFoundInList))
                    {
                        lblSpecializationNotFoundValue.Visible = true;
                        lblSpecializationNotFoundValue.Text = requestItem.SpecializationNotFoundInList;
                    }
                    if (requestItem.Faculty != null)
                    {

                        lblFacultyValue.Text = requestItem.Faculty;
                    }
                    else if (!string.IsNullOrEmpty(requestItem.FacultyNotFoundInList))
                    {
                        lblFacultyNotFoundValue.Visible = true;
                        lblFacultyNotFoundValue.Text = requestItem.FacultyNotFoundInList;
                    }
                    lblStudyingLanguageValue.Text = requestItem.StudyLanguage.SelectedTitle;
                    if (requestItem.StudySystem != null)
                    {
                        lblStudyingSystemValue.Visible = true;
                        lblStudyingSystemValue.Text = requestItem.StudySystem.SelectedTitle;
                    }
                    lblStartDateValue.Text = ExtensionMethods.QatarFormatedDate(requestItem.StudyStartDate);
                    if (requestItem.StudyGraduationDate != DateTime.MinValue)
                    {

                        lblGraduationDateValue.Text = ExtensionMethods.QatarFormatedDate(requestItem.StudyGraduationDate);
                    }

                    if (requestItem.UniversityMainHeadquarter != null)
                    {

                        lblUniversityMainHeadQuarterValue.Text = requestItem.UniversityMainHeadquarter.SelectedTitle;
                    }
                    else if (!string.IsNullOrEmpty(requestItem.NewUniversityHeadquarter))
                    {
                        lblUniversityMainAdded.Visible = true;
                        lblNewUniversityMainHeadQuarterValue.Visible = true;
                        lblNewUniversityMainHeadQuarterValue.Text = requestItem.NewUniversityHeadquarter;
                    }

                    if (!string.IsNullOrEmpty(requestItem.UniversityAddress))
                    {

                        lblAddressValue.Text = requestItem.UniversityAddress;
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

                    if (!string.IsNullOrEmpty(requestItem.UniversityEmail))
                    {
                        lblUniversityEmailValue.Text = requestItem.UniversityEmail;
                    }

                    lblWorkingOrNotValue.Text = requestItem.WorkingOrNot ? yesValue : noValue;

                    lblHavePAValue.Text = requestItem.HavePAOrNot ? yesValue : noValue;

                    if (requestItem.HavePAOrNot ==true)
                    {
                        fileUploadNoHavePA.Visible = false;
                    }
                    else
                    {
                        fileUploadHavePA.Visible = false;
                    }
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
                            lblOtherEntityWorkingForValue.Visible = true;
                            lblOtherEntityWorkingForValue.Text = requestItem.OtherEntityWorkingFor;
                        }
                        //if (!string.IsNullOrEmpty(requestItem.Occupation))
                        //{
                        //    divOccupation.Visible = true;
                        //    lblOccupation.Visible = true;
                        //    lblOccupationValue.Visible = true;
                        //    lblOccupationValue.Text = requestItem.Occupation;
                        //}
                        fileUploadNotWorking.Visible = false;

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
                            lblOtherEntityNeedsEquivalencyValue.Visible = true;
                            lblOtherEntityNeedsEquivalencyValue.Text = requestItem.OtherEntityNeedsEquivalency;
                        }
                        fileUploadOrgAttach.Visible = false;
                    }

                    List<CalculatedDetailsForCertificate> calculatedDetailsData = BL.CalculatedDetailsForCertificate.GetCalculatedDetailsForCertificate(requestId, LCID);
                    if (calculatedDetailsData != null && calculatedDetailsData.Count > 0)
                    {

                        repCalculatedDetailsForCertificate.DataSource = calculatedDetailsData;
                        repCalculatedDetailsForCertificate.DataBind();
                    }
                }

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
        private void BindAttachments()
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

                if (WorkingFor.Visible)
                {
                    OrgAttach.DocumentLibraryName = Utilities.Constants.OrganizationalLettersAttachments;
                    OrgAttach.DocLibWebUrl = SPContext.Current.Site.Url;
                    OrgAttach.Group = "CopyOfOrganizationlLetter";
                    OrgAttach.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                    OrgAttach.Enabled = false;
                    OrgAttach.Bind();
                }
                else
                {
                    OrgAttach.DocumentLibraryName = Utilities.Constants.OrganizationalLettersAttachments;
                    OrgAttach.DocLibWebUrl = SPContext.Current.Site.Url;
                    OrgAttach.Group = "NotWorkingCopyOfOrganizationlLetter";
                    OrgAttach.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                    OrgAttach.Enabled = false;
                    OrgAttach.Bind();
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
                //fileUploadGeneralSec.Group = "GeneralSecondary" + hdnCertificateAcademic.Value;
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



                Certificates.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                Certificates.DocLibWebUrl = SPContext.Current.Site.Url;
                Certificates.Group = hdnCertificateAcademic.Value;
                Certificates.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                Certificates.Enabled = false;
                Certificates.Bind();


                fileUploadCertificateEquivalent.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                fileUploadCertificateEquivalent.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadCertificateEquivalent.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CertificateToBeEquivalent", (uint)LCID);
                fileUploadCertificateEquivalent.Group = "CertificateEquivalentAttachment";
                fileUploadCertificateEquivalent.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileUploadCertificateEquivalent.Enabled = false;
                fileUploadCertificateEquivalent.Bind();


                fileEquivalent.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                fileEquivalent.DocLibWebUrl = SPContext.Current.Site.Url;
                fileEquivalent.Group = "CertificateEquivalentAttachment";
                fileEquivalent.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileEquivalent.Enabled = false;
                fileEquivalent.Bind();


                fileUploadGrades.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                fileUploadGrades.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadGrades.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Grades", (uint)LCID);
                fileUploadGrades.Group = "GardesAttachment";
                fileUploadGrades.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileUploadGrades.Enabled = false;
                fileUploadGrades.Bind();

                fileGrades.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                fileGrades.DocLibWebUrl = SPContext.Current.Site.Url;
                fileGrades.Group = "GardesAttachment";
                fileGrades.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileGrades.Enabled = false;
                fileGrades.Bind();


                //GeneralSecondary.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                //GeneralSecondary.DocLibWebUrl = SPContext.Current.Site.Url;
                //GeneralSecondary.Group = "GeneralSecondary" + hdnCertificateAcademic.Value;
                //GeneralSecondary.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                //GeneralSecondary.Enabled = false;
                //GeneralSecondary.Bind();

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

                    diploma.Visible = true;
                    fileDiploma.Visible = true;
                    fileDiploma.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                    fileDiploma.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileDiploma.Group = "Diploma" + hdnCertificateAcademic.Value;
                    fileDiploma.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                    fileDiploma.Enabled = false;
                    fileDiploma.Bind();

                    interDiploma.Visible = true;
                    fileInterDiploma.Visible = true;
                    fileInterDiploma.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                    fileInterDiploma.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileInterDiploma.Group = "InterMediateDiploma" + hdnCertificateAcademic.Value;
                    fileInterDiploma.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                    fileInterDiploma.Enabled = false;
                    fileInterDiploma.Bind();


                }

                CertificateSequence.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                CertificateSequence.DocLibWebUrl = SPContext.Current.Site.Url;
                CertificateSequence.Group = "CertificateSequence" + hdnCertificateAcademic.Value;
                CertificateSequence.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                CertificateSequence.Enabled = false;
                CertificateSequence.Bind();


                fileUploadDelegates.DocumentLibraryName = Utilities.Constants.DelegationDocuments;
                fileUploadDelegates.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadDelegates.Group = "DelegationTemplates";
                fileUploadDelegates.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileUploadDelegates.Enabled = false;
                fileUploadDelegates.Bind();

                Delegates.DocumentLibraryName = Utilities.Constants.DelegationDocuments;
                Delegates.DocLibWebUrl = SPContext.Current.Site.Url;
                Delegates.Group = "DelegationTemplates";
                Delegates.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                Delegates.Enabled = false;
                Delegates.Bind();

                if (ViewState["DisplayCalculatedDetails"] != null)
                {
                    fileUploadCalculatedDetails.DocumentLibraryName = Utilities.Constants.CalculatedDetailsForCertificateAttachments;
                    fileUploadCalculatedDetails.DocLibWebUrl = SPContext.Current.Site.Url;
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

                fileUploadCalculatedDetails.Visible = true;
                fileUploadCalculatedDetails.DocumentLibraryName = Utilities.Constants.CalculatedDetailsForCertificateAttachments;
                fileUploadCalculatedDetails.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadCalculatedDetails.Group = repeaterItem.ItemIndex.ToString();
                fileUploadCalculatedDetails.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileUploadCalculatedDetails.Enabled = false;
                fileUploadCalculatedDetails.Bind();



                ViewState["DisplayCalculatedDetails"] = repeaterItem.ItemIndex.ToString();


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

    }
}
