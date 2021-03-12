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
using ITWORX.MOEHEWF.PA.Entities;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class PADisplayPARequestDetails : UserControlBase
    {
        private int rowIndex = 0;
        #region Protected Variables
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadWorking;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadNotWorking;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload schoolDocuments;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadNationalService;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload universityDocuments;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCalcDetails;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadGrades;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadAcceptedHours;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCalculatedDetails;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCalculatedDetails2;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadDelegates;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload FUDequivalentHours;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadAdmissionLetter;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadStudyPlan;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PADisplayPARequestDetails.Page_Load");
            try
            {
                PADisplayRequestData();
                BindAttachments();

                //This will be removed when session is used
                if (Page.Session["PADisplayRequestId"] != null)
                { 

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

                Logging.GetInstance().Debug("Exiting method PADisplayRequestDetails.Page_Load");
            }
        }

        private void PADisplayRequestData()
        {
            Logging.GetInstance().Debug("Entering method PADisplayPARequestDetails.PADisplayRequestData");
            try
            {
                int requestId = 0;
                //ToDo: Enhance, Somepages use DisplayRequestId, others use  PADisplayRequestId
                if (Page.Session["PADisplayRequestId"] != null)
                {
                    requestId = int.Parse(Convert.ToString(Page.Session["PADisplayRequestId"]));
                } 

                string yesValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Yes", (uint)LCID);
                string noValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "No", (uint)LCID);
                Entities.PARequest requestItem = BL.Request.Reviewer_GetRequestByNumber(requestId, LCID);

                if (requestItem != null)
                {
                    divformControls.Visible = true; 

                    lblRequestNumberValue.Text = requestItem.RequestNumber;
                    lblRequestSubmitDateValue.Text = requestItem.SubmitDate != DateTime.MinValue ? ExtensionMethods.QatarFormatedDate(requestItem.SubmitDate) : "N/A";
                    lblRequestCreationDateValue.Text = requestItem.RequestCreationDate != DateTime.MinValue ? ExtensionMethods.QatarFormatedDate(requestItem.RequestCreationDate) : "N/A";
                    rdbWorkingOrNot.Text = requestItem.WorkingOrNot ? yesValue : noValue;
                    ddlEntityWorkingFor.Text = requestItem.EntityWorkingFor != null ? requestItem.EntityWorkingFor.SelectedTitle : "N/A";
                    if (!string.IsNullOrEmpty(requestItem.OtherEntityWorkingFor))
                    {
                        ddlEntityWorkingFor.Text = requestItem.OtherEntityWorkingFor;
                    }
                    ddlHighestCertificate.Text = requestItem.HighestCertificate != null ? requestItem.HighestCertificate.SelectedTitle : "N/A";
                    lblCountriesValue.Text = requestItem.CountryOfStudy != null ? requestItem.CountryOfStudy.SelectedTitle : "N/A";
                    txtCertificateDate.Text = requestItem.CertificateDate;
                    //Removed from View
                    ddlCalcSectionUniversity.Text = requestItem.ProgramUniversity != null ? requestItem.ProgramUniversity.SelectedTitle : "N/A";
                  
                    ddlFaculty.Text = string.IsNullOrWhiteSpace(requestItem.Faculty) ? "N/A" : requestItem.Faculty;
                    if (!string.IsNullOrEmpty(requestItem.FacultyNotFoundInList))
                    { 
                        ddlFacultyOther.Visible = true;
                        ddlFacultyOther.Text = requestItem.FacultyNotFoundInList;
                    }
                    ddlSpecialization.Text = requestItem.Specialization != null ? requestItem.Specialization.SelectedTitle : "N/A";
                    if (!string.IsNullOrEmpty(requestItem.SpecializationNotFoundInList))
                    {
                        ddlSpecialization.Text = requestItem.SpecializationNotFoundInList;
                    }

                    //universityDocuments file
                    ddlWantedCertificateDegree.Text = requestItem.ProgramType != null ? requestItem.ProgramType.SelectedTitle : "N/A";
                    ddlCalcSectionCountry.Text = requestItem.ProgramCountry != null ? requestItem.ProgramCountry.SelectedTitle : "N/A";
                    ddlUniversity.Text = requestItem.University != null ? requestItem.University.SelectedTitle : "N/A";
                    if (!string.IsNullOrEmpty(requestItem.UniversityNotFoundInList))
                    {
                        ddlUniversity.Text = requestItem.UniversityNotFoundInList;
                    }
                    ddlCalcSectionFaculty.Text = requestItem.ProgramFaculty;
                    lblProgramSpecializationVal.Text = requestItem.ProgramSpecialization != null ? requestItem.ProgramSpecialization.SelectedTitle : "N/A";
                    if (!string.IsNullOrEmpty(requestItem.ProgramSpecializationNotFoundInList))
                    {
                        lblProgramSpecializationVal.Text = requestItem.ProgramSpecializationNotFoundInList;
                    }
                    ddlFaculty.Text = requestItem.Faculty;
                 
                    dropCalcSectionStudyingSystem.Text = requestItem.ProgramStudySystem != null ? requestItem.ProgramStudySystem.SelectedTitle : "N/A";
                    txtCalcSectionStudingPeriod.Text = requestItem.ProgramPeriod;
                    dtAcademicStartDate.Text = requestItem.AcademicStartDate != DateTime.MinValue ? ExtensionMethods.QatarFormatedDate(requestItem.AcademicStartDate) : "N/A";
                    dtAcademicEndDate.Text = requestItem.AcademicEndDate != DateTime.MinValue ? ExtensionMethods.QatarFormatedDate(requestItem.AcademicEndDate) : "N/A";
                    txtAcademicNumberOfYears.Text = Convert.ToString(requestItem.AcademicNumberOfYears);
 

                    //show/hide working section
                    ShowHideWorkingForAndOccupation.Visible = requestItem.WorkingOrNot;
                    ShowHideNotWorking.Visible = !requestItem.WorkingOrNot;

                    string secondarySchool = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "SecondarySchool", (uint)LCID).ToLower();
                    bool isSecondarySchoolCertified = (ddlHighestCertificate.Text.ToLower() == secondarySchool);
                    //For Readability
                    bool isUniversity = !isSecondarySchoolCertified;


                    sectionSecondaryCertificate.Visible = 
                    isSecondarySchoolCertified;

                    divUniversitiesDetails.Visible = 
                    isUniversity;
                    //lblJoinedOtherUniValue.Text = requestItem.JoinedOtherUniversities ? yesValue : noValue;
                    //if(requestItem.JoinedOtherUniversities)
                    //{
                    //    fileUploadAcceptedHours.Visible = true;
                    //    fileUploadGrades.Visible = true;
                    //}
                    //else
                    //{
                    //    fileUploadAcceptedHours.Visible = false;
                    //    fileUploadGrades.Visible = false;
                    //}

                    List<CalculatedDetailsForCertificate> calculatedDetailsData = BL.CalculatedDetailsForCertificate.GetCalculatedDetailsForCertificate(requestId, LCID);
                    if (calculatedDetailsData != null && calculatedDetailsData.Count > 0)
                    {

                        repCalculatedDetailsForCertificate.DataSource = calculatedDetailsData;
                        repCalculatedDetailsForCertificate.DataBind();
                    }
                    else
                    {
                        Div_NoResults.Visible = true;
                    }
                }
                else
                {
                    divformControls.Visible = false; 
                }

            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method PADisplayPARequestDetails.PADisplayRequestData");
            }

        }

        private void BindAttachments()
        {
            Logging.GetInstance().Debug("Entering method PADisplayPARequestDetails.BindAttachments");
            try
            {

                int requestId = 0;
                if (Page.Session["PADisplayRequestId"] != null)
                {
                    requestId = int.Parse(Convert.ToString(Page.Session["PADisplayRequestId"]));
                }
                fileUploadWorking.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                fileUploadWorking.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadWorking.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "CopyOfOrganizationlLetter", (uint)LCID);
                fileUploadWorking.Group = "WorkingDocument";
                fileUploadWorking.RequestID = requestId;
                fileUploadWorking.Enabled = false;
                fileUploadWorking.Bind();

                fileUploadAdmissionLetter.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                fileUploadAdmissionLetter.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadAdmissionLetter.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "UnconditionalAdmissionLetter", (uint)LCID);
                fileUploadAdmissionLetter.Group = "UnconditionalAdmissionLetter";
                fileUploadAdmissionLetter.RequestID = requestId;
                fileUploadAdmissionLetter.Enabled = false;
                fileUploadAdmissionLetter.Bind();

                fileUploadStudyPlan.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                fileUploadStudyPlan.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadStudyPlan.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "StudyPlan", (uint)LCID);
                fileUploadStudyPlan.Group = "PAStudyPlan";
                fileUploadStudyPlan.RequestID = requestId;
                fileUploadStudyPlan.Enabled = false;
                fileUploadStudyPlan.Bind();


                fileUploadNotWorking.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                fileUploadNotWorking.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadNotWorking.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "adlsaLetter", (uint)LCID);
                fileUploadNotWorking.Group = "adlsaLetter";
                fileUploadNotWorking.RequestID = requestId;
                fileUploadNotWorking.Enabled = false;
                fileUploadNotWorking.Bind();

                schoolDocuments.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                schoolDocuments.DocLibWebUrl = SPContext.Current.Site.Url;
                schoolDocuments.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "schoolDocuments", (uint)LCID);
                schoolDocuments.Group = "schoolDocuments";
                schoolDocuments.RequestID = requestId;
                schoolDocuments.Enabled = false;
                schoolDocuments.Bind();

                fileUploadNationalService.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                fileUploadNationalService.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadNationalService.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NationalServiceIDCopy", (uint)LCID);
                fileUploadNationalService.Group = "NationalServiceIDCopy";
                fileUploadNationalService.RequestID = requestId;
                fileUploadNationalService.Enabled = false;
                fileUploadNationalService.Bind();

                
                universityDocuments.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                universityDocuments.DocLibWebUrl = SPContext.Current.Site.Url;
                universityDocuments.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "universityDocuments", (uint)LCID);
                universityDocuments.Group = "universityDocuments";
                universityDocuments.RequestID = requestId;
                universityDocuments.Enabled = false;
                universityDocuments.Bind();
                 
                //fileUploadCalcDetails.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                //fileUploadCalcDetails.DocLibWebUrl = SPContext.Current.Site.Url;
                //fileUploadCalcDetails.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "fileUploadCalcDetails", (uint)LCID);
          
                //fileUploadCalcDetails.Group = "attachments";
                //fileUploadCalcDetails.RequestID = requestId;
                //fileUploadCalcDetails.Enabled = false;
                //fileUploadCalcDetails.Bind();
                

                //fileUploadGrades.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                //fileUploadGrades.DocLibWebUrl = SPContext.Current.Site.Url;
                //fileUploadGrades.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Grading", (uint)LCID);
                //fileUploadGrades.Group = "Grades";
                //fileUploadGrades.RequestID = requestId;
                //fileUploadGrades.Enabled = false;
                //fileUploadGrades.Bind();


                //fileUploadAcceptedHours.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                //fileUploadAcceptedHours.DocLibWebUrl = SPContext.Current.Site.Url;
                //fileUploadAcceptedHours.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "AcceptedHours", (uint)LCID);
                //fileUploadAcceptedHours.Group = "AcceptedHours";
                //fileUploadAcceptedHours.RequestID = requestId;
                //fileUploadAcceptedHours.Enabled = false;
                //fileUploadAcceptedHours.Bind();

                FUDequivalentHours.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                FUDequivalentHours.DocLibWebUrl = SPContext.Current.Site.Url;
                FUDequivalentHours.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "EquivalentHours", (uint)LCID);
                FUDequivalentHours.Group = "EquivalentHours";
                FUDequivalentHours.RequestID = requestId;
                FUDequivalentHours.Enabled = false;
                FUDequivalentHours.Bind();

                fileUploadDelegates.DocumentLibraryName = Utilities.Constants.PADelegationDocuments;
                fileUploadDelegates.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadDelegates.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "DelegationTemplates", (uint)LCID);
                fileUploadDelegates.Group = "PADelegationTemplates";
                fileUploadDelegates.RequestID = requestId;
                fileUploadDelegates.Enabled = false;
                fileUploadDelegates.Bind();

                if (ViewState["DisplayCalculatedDetails"] != null)
                { 
                    fileUploadCalculatedDetails.DocumentLibraryName = Utilities.Constants.PACalculatedDetailsForCertificateAttachments;
                    fileUploadCalculatedDetails.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadCalculatedDetails.Group = ViewState["DisplayCalculatedDetails"].ToString();
                    fileUploadCalculatedDetails.RequestID = int.Parse(Page.Session["PADisplayRequestId"].ToString());
                    fileUploadCalculatedDetails.Enabled = false;
                    fileUploadCalculatedDetails.Bind();

                    fileUploadCalculatedDetails2.DocumentLibraryName = Utilities.Constants.PACalculatedDetailsForCertificateAttachments;
                    fileUploadCalculatedDetails2.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadCalculatedDetails2.Group = ViewState["DisplayCalculatedDetails"].ToString()+"0";
                    fileUploadCalculatedDetails2.RequestID = int.Parse(Page.Session["PADisplayRequestId"].ToString());
                    fileUploadCalculatedDetails2.Enabled = false;
                    fileUploadCalculatedDetails2.Bind();

                }

            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method PADisplayPARequestDetails.BindAttachments");
            }

        }

        protected void gridUniversitiesNames_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PADisplayPARequestDetails.gridUniversitiesNames_RowDataBound");
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (Page.Session["PADisplayRequestId"] != null)
                    {
                        int requestId = int.Parse(Convert.ToString(Page.Session["PADisplayRequestId"])); 
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

                Logging.GetInstance().Debug("Exiting method PADisplayPARequestDetails.gridUniversitiesNames_RowDataBound");
            }
        }

        protected void lnkCalcSectionDisplayAttach_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PADisplayPARequestDetails.lnkCalcSectionDisplayAttach_Click");
            try
            {

                RepeaterItem repeaterItem = (sender as LinkButton).NamingContainer as RepeaterItem;

                fileUploadCalculatedDetails.Visible = true;
                fileUploadCalculatedDetails.DocumentLibraryName = Utilities.Constants.PACalculatedDetailsForCertificateAttachments;
                fileUploadCalculatedDetails.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadCalculatedDetails.Group = repeaterItem.ItemIndex.ToString();
                fileUploadCalculatedDetails.RequestID = int.Parse(Page.Session["PADisplayRequestId"].ToString());
                fileUploadCalculatedDetails.Enabled = false;
                fileUploadCalculatedDetails.Bind();

                fileUploadCalculatedDetails2.Visible = true;
                fileUploadCalculatedDetails2.DocumentLibraryName = Utilities.Constants.PACalculatedDetailsForCertificateAttachments;
                fileUploadCalculatedDetails2.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadCalculatedDetails2.Group = repeaterItem.ItemIndex.ToString()+"0";
                fileUploadCalculatedDetails2.RequestID = int.Parse(Page.Session["PADisplayRequestId"].ToString());
                fileUploadCalculatedDetails2.Enabled = false;
                fileUploadCalculatedDetails2.Bind();

                ViewState["DisplayCalculatedDetails"] = repeaterItem.ItemIndex.ToString();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method PADisplayPARequestDetails.lnkCalcSectionDisplayAttach_Click");
            }
        }

    }
}
