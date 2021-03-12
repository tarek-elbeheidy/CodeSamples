using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;
using System.Web;
using ITWORX.MOEHEWF.PA.Entities;
using System.Collections.Generic;
using ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA;
using Microsoft.SharePoint.WebControls;
using ITWORX.MOEHEWF.Common.Entities;
using System.Text;
using ITWORX.MOEHE.Integration.SMS;

namespace ITWORX.MOEHEWF.PA.WebParts.PANewRequest
{
    public partial class PANewRequestUserControl : UserControlBase
    {


        #region Protected Variables
        protected string StudyYear = string.Empty;
        protected global::ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.PARequestDetails PARequestDetails;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.TermsAndConditions termsAndConditions;
        protected global::ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.ApplicantDetails applicantDetails;
        protected global::ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.CheckUniversity checkUniversity;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering PANewRequestUserControl.Page_Load");
            try
            {
                StudyYear = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "StudyYear");
                PARequestDetails.HideResubmit = true;
                if (!Page.IsPostBack)
                {
                   
                       
                    
                  

                    if (Page.Session["PAEditRequestId"] != null)
                    {
                        List<PARequest> Request = BL.Request.GetRequestActiveStep(int.Parse(Page.Session["PAEditRequestId"].ToString()));
                        if (Request[0].WizardActiveStep != string.Empty)
                        {
                            WizardStep wizardStep = GetWizardStepByTitle(wizardPANewRequest, Request[0].WizardActiveStep);
                            wizardPANewRequest.ActiveStepIndex = wizardPANewRequest.WizardSteps.IndexOf(wizardStep);
                        }
                        termsAndConditions.CheckVisibility = false;
                        //termsAndConditions.LabelVisibility = true;
                    }
                      else
                    {

                        //termsAndConditions.LabelVisibility = false;
                        termsAndConditions.CheckVisibility = true;
                    }
                    
                    termsAndConditions.isVisible = false;



                    if (HelperMethods.InGroup(Common.Utilities.Constants.ApplicanstGroupName))
                    {


                        if (/*Page.Session["Save"]==null &&*/  !(SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditPARequestPage)) && !(SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage.ToLower())))

                        {
                            Page.Session.Remove("PANewRequestId");
                            Page.Session.Remove("applicantId");
                            Page.Session.Remove("PAEditRequestId");
            
                        }
                         
                        termsAndConditions.isVisible = false;

                        if (SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditPARequestPage) && Page.Session["PAEditRequestId"] == null)
                        {
                            lblNoRequest.Visible = true;
                            pnlForm.Visible = false;
                        } 
                    }
                    else
                    {
                        Response.Redirect(@"~/_layouts/15/AccessDenied.aspx");
                    } 

                }
                 
                 
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit PANewRequestUserControl.Page_Load");
            }
        }
        private WizardStep GetWizardStepByTitle(Wizard radWizard, string title)
        {
            foreach (WizardStep step in radWizard.WizardSteps)
            {
                if (step.ID == title)
                {
                    return step;
                }
            }
            return null;
        }
        protected void wizardPANewRequest_PreRender(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering PANewRequestUserControl.wizardPANewRequest_PreRender");
            try
            {

                Repeater SideBarList = wizardPANewRequest.FindControl("HeaderContainer").FindControl("SideBarList") as Repeater;
                SideBarList.DataSource = wizardPANewRequest.WizardSteps;
                SideBarList.DataBind();


            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit PANewRequestUserControl.wizardPANewRequest_PreRender");
            }

        }

        public string GetClassForWizardStep(object wizardStep)
        {
            Logging.GetInstance().Debug("Entering PANewRequestUserControl.GetClassForWizardStep");
            string stepText = string.Empty;
            try
            {
                WizardStep step = wizardStep as WizardStep;

                if (step == null)
                {
                    stepText = "";
                }
                int stepIndex = wizardPANewRequest.WizardSteps.IndexOf(step);

                if (stepIndex < wizardPANewRequest.ActiveStepIndex)
                {
                    stepText = "prevStep";
                }
                else if (stepIndex > wizardPANewRequest.ActiveStepIndex)
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

                Logging.GetInstance().Debug("Exit PANewRequestUserControl.GetClassForWizardStep");
            }
            return stepText;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PANewRequestUserControl.btnSave_Click");

            try
            {

                if (!IsRefresh)
                {

                    if (Page.Session["PANewRequestId"] == null && Page.Session["PAEditRequestId"] == null)

                    {
                        lblErrorMessage.Text = lblErrorMessage.Text + " " + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                        modalPopupError.Show();
                        return;
                    }


                    Applicants applicant = Common.BL.Applicants.GetApplicantProfilefromADByLoginName(SPContext.Current.Web.CurrentUser.LoginName);
                    long qatarID = Common.BL.Applicants.GetApplicantQatarIDfromADByLoginName(SPContext.Current.Web.CurrentUser.LoginName);
                    // Check if applicant exists
                    int applicantId = Common.BL.Applicants.GetApplicantItemByPersonalID(qatarID.ToString());
                    //If applicantId=0 , add row
                    if (applicantId == 0)
                    { 
                        if (applicant != null)
                        {
                            applicantId = Common.BL.Applicants.AddApplicant(applicant, applicantId);
                        }
                        else
                        {
                            Logging.GetInstance().Debug("Applicant LoginName " + SPContext.Current.Web.CurrentUser.LoginName + " doesn't exist AD");
                        }
                    }
                    else
                    {
                        applicant.ID = applicantId;
                    }
                    //add check university data and applicant id
                    Entities.PARequest checkUniversityObject = checkUniversity.CreateCheckUniversityDataObject(applicantId);
                    int checkRequestId = BL.Request.AddCheckUniversityDetails(checkUniversityObject);
                    applicantDetails.CreateUserDataObject(applicant);


                    Entities.PARequest request = PARequestDetails.CreateSavedRequestDataObject();
                    bool updatedRequest = BL.Request.AddUpdateRequest(request, wizardPANewRequest.ActiveStep.ID, request.ID, (int)Common.Utilities.RequestStatus.PADraft);
                    bool savedCalculated = PARequestDetails.GetCalculatedDetailsDataFromRepeater(request.ID);
                    PARequestDetails.SaveAttachments();

                    if (updatedRequest)
                    {

                        string requestNumber = request.RequestNumber;

                       
                        lblSaveSuccess.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "SaveSucceed", (uint)LCID), requestNumber);

                        modalSavePopup.Show();
                    }

                    Page.Session["PAEditRequestId"] = request.ID;
                    Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests, Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PADraft, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["PAEditRequestId"].ToString(), "Yes");

                    //GetCalculatedDetailsDataFromGrid( requestId);
                    //For saving of attachments grids
 
                    Page.Session.Remove("PANewRequestId"); 
                    Page.Session.Remove("CalcDetailsSelectedValue");
                    Page.Session.Remove("UploadCertificateSelectedValue");
 
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method PANewRequestUserControl.btnSave_Click");
            }
        }


        protected void wizardPANewRequest_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            Logging.GetInstance().Debug("Enter NewRequestUserControl.wizardNewRequest_NextButtonClick");
            try
            {
                if (e.CurrentStepIndex != 0)
                {
                    if (Page.Session["PANewRequestId"] == null && Page.Session["PAEditRequestId"] == null)

                    {
                        lblErrorMessage.Text = lblErrorMessage.Text + " " + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                        modalPopupError.Show();
                        return;
                    }
                }

                Applicants applicant = Common.BL.Applicants.GetApplicantProfilefromADByLoginName(SPContext.Current.Web.CurrentUser.LoginName);
                if (e.CurrentStepIndex == 0)
                {
                    checkUniversity.ValidateOthersUni();
                }
             
                if (Page.IsValid || e.CurrentStepIndex == 1)
                {

                    if (e.NextStepIndex == 1 && !SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditPARequestPage))
                    {
                        ((Button)wizardPANewRequest.FindControl("StepNavigationTemplateContainerID$StepNextButton")).Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Agree", (uint)LCID);
                        ((Button)wizardPANewRequest.FindControl("StepNavigationTemplateContainerID$StepNextButton")).Enabled = false;
                    }
                    else
                    {
                        ((Button)wizardPANewRequest.FindControl("StepNavigationTemplateContainerID$StepNextButton")).Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Next", (uint)LCID);

                    }

                    if (e.CurrentStepIndex == 0 /*&& Session["requestId"]==null*/)
                    {
                        long qatarID = Common.BL.Applicants.GetApplicantQatarIDfromADByLoginName(SPContext.Current.Web.CurrentUser.LoginName);
                        // Check if applicant exists
                        int applicantId = Common.BL.Applicants.GetApplicantItemByPersonalID(qatarID.ToString());
                      
                        if (applicantId == 0)
                        {
                            
                            applicant = Common.BL.Applicants.GetApplicantProfilefromADByLoginName(SPContext.Current.Web.CurrentUser.LoginName);
                            applicantId = Common.BL.Applicants.AddApplicant(applicant, applicantId);
                        }
                        //add check university data and applicant id
                        Entities.PARequest checkUniversityObject = checkUniversity.CreateCheckUniversityDataObject(applicantId);
                        int requestId = BL.Request.AddCheckUniversityDetails(checkUniversityObject);

                        if (SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditPARequestPage))
                        {
                            Page.Session["PAEditRequestId"] = requestId;
                        }
                        else
                        {
                            Page.Session["PANewRequestId"] = requestId;
                        }
                        Page.Session["applicantId"] = applicantId;
                    }

                  

                    // add requestId in view state to update request in finish btn
                    //Check if applicant controls are not empty then update the applicant list
                    if (e.CurrentStepIndex == 2 && Page.Session["applicantId"] != null)
                    {
                        ((Button)wizardPANewRequest.FindControl("StepNavigationTemplateContainerID$StepNextButton")).CausesValidation = true;
                       
                        applicant.ID = int.Parse(Page.Session["applicantId"].ToString());
                        applicantDetails.CreateUserDataObject(applicant);
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit NewRequestUserControl.wizardNewRequest_NextButtonClick");
            }
        }

        protected void wizardPANewRequest_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {
            Logging.GetInstance().Debug("Enter PANewRequestUserControl.wizardPANewRequest_PreviousButtonClick");
            try
            {
                wizardPANewRequest.ActiveStepIndex = wizardPANewRequest.ActiveStepIndex - 1;
                // populate values in each screen 
                //show check university data 
                if (e.NextStepIndex == 1 && Page.Session["PAEditRequestId"] == null)
                {
                    ((Button)wizardPANewRequest.FindControl("StepNavigationTemplateContainerID$StepNextButton")).Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Agree", (uint)LCID);

                }
                else
                {
                    ((Button)wizardPANewRequest.FindControl("StepNavigationTemplateContainerID$StepNextButton")).Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Next", (uint)LCID);
                }

                if (e.NextStepIndex == 0)
                {
                    checkUniversity.SetPanelVisibility();
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit PANewRequestUserControl.wizardPANewRequest_PreviousButtonClick");
            }
        }

        protected void wizardPANewRequest_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PANewRequestUserControl.wizardPANewRequest_FinishButtonClick");

            try
            {
                if (!IsRefresh)
                {
                    if (Page.Session["PANewRequestId"] == null && Page.Session["PAEditRequestId"] == null)

                    {
                        lblErrorMessage.Text = lblErrorMessage.Text + " " + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                        modalPopupError.Show();
                        return;
                    }

                    Entities.PARequest request = PARequestDetails.CreateSubmittedRequestDataObject();
                    PARequestDetails.Validate();
                    Page.Validate();
                    if (Page.IsValid)
                    {


                       
                        if (request == null)
                        {
                            return;
                        }

                        bool updatedRequest = BL.Request.AddUpdateRequest(request,wizardPANewRequest.ActiveStep.ID, request.ID, (int)Common.Utilities.RequestStatus.PASubmitted);
                        PARequestDetails.SaveAttachments();
                        applicantDetails.SaveAttachments();


                        if (updatedRequest)
                        {
                            string requestNumber = request.RequestNumber;

                            Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests, Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PASubmitted, SPContext.Current.Web.CurrentUser.Name, string.Empty, request.ID.ToString(), "Yes");

                            lblSuccess.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "SubmitSucceed", (uint)LCID), requestNumber);
                            bool savedCalculated = PARequestDetails.GetCalculatedDetailsDataFromRepeater(request.ID);
                            modalPopUpConfirmation.Show();

                            //send email 
                            string SMTPServer = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPServer");
                            string SMTPServerPort = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPServerPort");
                            string SMTPFromAddress = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPFromAddress");
                            string SMTPFromDisplayName = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPPAFromDisplayName");


                            if (Page.Session["applicantId"] != null)
                            {
                                Applicants applicant = Common.BL.Applicants.GetApplicantByID(int.Parse(Page.Session["applicantId"].ToString()), LCID);
                                Notifications emaiNotifications = Common.BL.Notifications.GetSubmittedNotification((int)Common.Utilities.NotificationType.Email, (int)Common.Utilities.RequestStatus.PASubmitted);
                                if (emaiNotifications != null)
                                {
                                    HelperMethods.SendNotificationEmail(string.Format(emaiNotifications.Body, request.RequestNumber, "<br/>", request.RequestNumber), emaiNotifications.Subject, SMTPFromAddress, SMTPFromDisplayName, applicant.ApplicantEmail, SMTPServer, SMTPServerPort, "", "", true, new List<System.Net.Mail.Attachment>());

                                }

                                //Send SMS here
                                string smsEnabledConfig = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMSPAEnabled");
                                var SMSPAEnabled = false;

                                if (!string.IsNullOrEmpty(smsEnabledConfig))
                                {
                                    bool.TryParse(smsEnabledConfig, out SMSPAEnabled);
                                }

                                if (SMSPAEnabled)
                                {
                                    try
                                    {
                                        Notifications smsNotifications = Common.BL.Notifications.GetSubmittedNotification((int)Common.Utilities.NotificationType.SMS, (int)Common.Utilities.RequestStatus.PASubmitted);
                                        if (smsNotifications != null)
                                        {

                                            Texting.SendSMS(applicant.MobileNumber, string.Format(smsNotifications.Body, request.RequestNumber, "Equivalency", request.RequestNumber, "معادلة الشهادات"));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Logging.GetInstance().LogException(ex);
                                    }
                                }
                            }


                        }
                         
                        Page.Session.Remove("PANewRequestId");
                        Page.Session.Remove("applicantId");
                        Page.Session.Remove("PAEditRequestId");
                        Page.Session.Remove("CalcDetailsSelectedValue");
                        Page.Session.Remove("UploadCertificateSelectedValue");
 
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method PANewRequestUserControl.wizardPANewRequest_FinishButtonClick");
            }
        }


        protected void btnModalOK_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PANewRequestUserControl.btnModalOK_Click");

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

                Logging.GetInstance().Debug("Exiting method PANewRequestUserControl.btnModalOK_Click");
            }
        }

        protected void btnDashboard_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/TrackRequest.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
        }

        protected void btnSaveOk_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PANewRequestUserControl.btnSaveOk_Click");

            try
            {
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ManagePARequest.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PANewRequestUserControl.btnSaveOk_Click");
            }

        }

        protected void btnErrorOk_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method PANewRequestUserControl.btnModalOK_Click");

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
                Logging.GetInstance().Debug("Exiting method PANewRequestUserControl.btnModalOK_Click");
            }
        }
    }
}
