using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Entities;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class DisplaySimilarPARequestDetails : UserControlBase
    {
        private int rowIndex = 0;
        #region Protected Variables
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadOrgAttach;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCalculatedDetails;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCertificates;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadDelegationTemplates;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PADisplayPARequestDetails.Page_Load");
            try
            {
                if (SPContext.Current.Web.CurrentUser != null)
                {
                    lblUserName.Text = SPContext.Current.Web.CurrentUser.Name;
                }

                //This will be removed when session is used
                if (/*Page.Request.QueryString["reqNo"] != null*/ Page.Session["hdn_SimilarReqID"] != null)
                {
                    PADisplayRequestData();
                    BindAttachments();

                    bool userInGroup = HelperMethods.InGroup(Common.Utilities.Constants.SecretaryGroupName);

                    if (userInGroup)
                    {

                        btn_Print.Visible = true;
                    }
                    else
                        btn_Print.Visible = false;

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
                Logging.GetInstance().Debug("Exiting method PADisplayPARequestDetails.Page_Load");
            }

        }

        private void PADisplayRequestData()
        {
            Logging.GetInstance().Debug("Entering method PADisplayPARequestDetails.PADisplayRequestData");
            try
            {
                int requestId = int.Parse(Convert.ToString(Page.Session["hdn_SimilarReqID"]));
                string yesValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Yes", (uint)LCID);
                string noValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "No", (uint)LCID);
                Entities.PARequest requestItem = BL.Request.Reviewer_GetRequestByNumber(requestId, LCID);

                if (requestItem != null)
                {
                    divformControls.Visible = true;
                    lblNoRequest.Visible = false;

                    lblRequestNumberValue.Text = requestItem.RequestNumber;
                    lblRequestSubmitDateValue.Text = requestItem.SubmitDate != DateTime.MinValue ? ExtensionMethods.QatarFormatedDate(requestItem.SubmitDate) : string.Empty;
                    lblCerticateAcademicDegreeValue.Text = requestItem.HighestCertificate.SelectedTitle;

                    lblCountriesValue.Text = requestItem.CountryOfStudy.SelectedTitle;
                    if (requestItem.University != null)
                    {
                        lblUniversityValue.Text = requestItem.University.SelectedTitle;
                    }
                    else if (!string.IsNullOrEmpty(requestItem.UniversityNotFoundInList))
                    {
                        lblUniversityNotFound.Visible = true;
                        lblUniversityNotFoundValue.Visible = true;
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
                        lblSpecializationNotFound.Visible = true;
                        lblSpecializationNotFoundValue.Visible = true;
                        lblSpecializationNotFoundValue.Text = requestItem.SpecializationNotFoundInList;
                    }
                    if (requestItem.Faculty != null)
                    {

                        lblFacultyValue.Text = requestItem.Faculty;
                    }
                    else if (!string.IsNullOrEmpty(requestItem.FacultyNotFoundInList))
                    {
                        lblFacultyNotFound.Visible = true;
                        lblFacultyNotFoundValue.Visible = true;
                        lblFacultyNotFoundValue.Text = requestItem.FacultyNotFoundInList;
                    }

                    lblStartDateValue.Text = ExtensionMethods.QatarFormatedDate(requestItem.AcademicStartDate);
                    if (requestItem.AcademicEndDate != DateTime.MinValue)
                    {

                        lblGraduationDateValue.Text = ExtensionMethods.QatarFormatedDate(requestItem.AcademicEndDate);
                    }


                    lblStudyPeriodValue.Text = requestItem.AcademicNumberOfYears.ToString();


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
                            //lblOccupationValue.Text = requestItem.Occupation.SelectedTitle;
                            lblOccupationValue.Text = requestItem.Occupation;
                        }
                        else if (!string.IsNullOrEmpty(requestItem.OtherOccupation))
                        {
                            lblOccupation.Visible = true;
                            lblOtherOccupation.Visible = true;
                            lblOtherOccupationValue.Visible = true;
                            lblOtherOccupationValue.Text = requestItem.OtherOccupation;
                        }
                        //lblHiringDate.Visible = true;
                        //lblHiringDateValue.Visible = true;
                        //lblWorkPhone.Visible = true;
                        //lblWorkPhoneValue.Visible = true;
                        //lblHiringDateValue.Text = ExtensionMethods.QatarFormatedDate(requestItem.HiringDate);
                        //lblWorkPhoneValue.Text = requestItem.OccupationPhone;
                    }
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

                Logging.GetInstance().Debug("Exiting method PADisplayPARequestDetails.PADisplayRequestData");
            }

        }
        private void BindAttachments()
        {
            Logging.GetInstance().Debug("Entering method PADisplayPARequestDetails.BindAttachments");
            try
            {
                fileUploadOrgAttach.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
                fileUploadOrgAttach.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadOrgAttach.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "CopyOfOrganizationlLetter", (uint)LCID);
                fileUploadOrgAttach.Group = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "CopyOfOrganizationlLetter", (uint)LCID);
                fileUploadOrgAttach.RequestID = int.Parse(Page.Session["hdn_SimilarReqID"].ToString());
                fileUploadOrgAttach.Enabled = false;
                fileUploadOrgAttach.Bind();


                fileUploadCertificates.DocumentLibraryName = Utilities.Constants.PACertificatesAttachments;
                fileUploadCertificates.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadCertificates.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "PreviousCertificates", (uint)LCID);
                fileUploadCertificates.Group = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "PreviousCertificates", (uint)LCID);
                fileUploadCertificates.RequestID = int.Parse(Page.Session["hdn_SimilarReqID"].ToString());
                fileUploadCertificates.Enabled = false;
                fileUploadCertificates.Bind();

                fileUploadDelegationTemplates.DocumentLibraryName = Utilities.Constants.PADelegationDocuments;
                fileUploadDelegationTemplates.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadDelegationTemplates.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "DelegationTemplates", (uint)LCID);
                fileUploadDelegationTemplates.Group = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "DelegationTemplates", (uint)LCID);
                fileUploadDelegationTemplates.RequestID = int.Parse(Page.Session["hdn_SimilarReqID"].ToString());
                fileUploadDelegationTemplates.Enabled = false;
                fileUploadDelegationTemplates.Bind();
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
                    if (Page.Session["hdn_SimilarReqID"] != null)
                    {
                        int requestId = int.Parse(Convert.ToString(Page.Session["hdn_SimilarReqID"]));
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
                            if (!string.IsNullOrEmpty(calculatedDetailsForCertificateItem.StudyType.SelectedID))
                            {
                                studyingTypeLabel.Text = calculatedDetailsForCertificateItem.StudyType.SelectedTitle;
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
                HiddenField hdnCalcId = (HiddenField)repeaterItem.FindControl("hdnCalcSectionID");
                if (!string.IsNullOrEmpty(hdnCalcId.Value))
                {
                    fileUploadCalculatedDetails.Visible = true;
                    fileUploadCalculatedDetails.DocumentLibraryName = Utilities.Constants.PACalculatedDetailsForCertificateAttachments;
                    fileUploadCalculatedDetails.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadCalculatedDetails.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "CalculatedDetailsAttachText", (uint)LCID);
                    fileUploadCalculatedDetails.Group = hdnCalcId.Value;
                    fileUploadCalculatedDetails.RequestID = int.Parse(Page.Session["hdn_SimilarReqID"].ToString());
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

                Logging.GetInstance().Debug("Exiting method PADisplayPARequestDetails.lnkCalcSectionDisplayAttach_Click");
            }
        }
    }
}
