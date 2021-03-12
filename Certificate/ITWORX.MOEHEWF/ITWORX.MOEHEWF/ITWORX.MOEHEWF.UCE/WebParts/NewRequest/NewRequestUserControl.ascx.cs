using ITWORX.MOEHE.Integration.SMS;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using commonUtilities=ITWORX.MOEHEWF.Common.Utilities;

namespace ITWORX.MOEHEWF.UCE.WebParts.NewRequest
{
    public partial class NewRequestUserControl : UserControlBase
    {
        // string resourceFile = SPContext.Current.Web.Language == 1033 ? "ITWORX.MOEHEWF.UCE.en-US" : "ITWORX.MOEHEWF.UCE.ar-SA";

        #region Protected Variables

        protected string StudyYear = string.Empty;
        protected global::ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.RequestDetails requestDetails;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.TermsAndConditions termsAndConditions;
        protected global::ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.ApplicantDetails applicantDetails;
        protected global::ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.CheckUniversity checkUniversity;

        #endregion Protected Variables

        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering NewRequestUserControl.Page_Load");
            try
            {
                StudyYear = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "StudyYear");
                requestDetails.HideResubmit = true;
               
                if (!Page.IsPostBack)
                {
                    
                    if (Page.Session["EditRequestId"] != null)
                    {
                        List<Entities.Request> Request = BL.Request.GetRequestActiveStep(int.Parse(Page.Session["EditRequestId"].ToString()));
                        if (Request[0].WizardActiveStep != string.Empty)
                        {
                            WizardStep wizardStep = GetWizardStepByTitle(wizardNewRequest, Request[0].WizardActiveStep);
                            wizardNewRequest.ActiveStepIndex = wizardNewRequest.WizardSteps.IndexOf(wizardStep);
                        }
                        termsAndConditions.CheckVisibility = false;
                        //termsAndConditions.LabelVisibility = true;
                    }
                    else
                    {
                        //termsAndConditions.LabelVisibility = false;
                        termsAndConditions.CheckVisibility = true;
                    }
                    if (HelperMethods.InGroup(Common.Utilities.Constants.ApplicanstGroupName))
                    {


                        if (/*Page.Session["Save"]==null &&*/  !(SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage)) && !(SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage)))

                        {
                            Page.Session.Remove("NewRequestId");
                            Page.Session.Remove("applicantId");
                            Page.Session.Remove("EditRequestId");
                            Page.Session.Remove("CalcDetailsSelectedValue");
                            Page.Session.Remove("UploadCertificateSelectedValue");
                        }

                        // UserControl termsandConditions = wizardNewRequest.FindControl("TermsAndConditions") as UserControl;

                        termsAndConditions.isVisible = false;

                        if (SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage) && Page.Session["EditRequestId"] == null)
                        {
                            lblNoRequest.Visible = true;
                            pnlForm.Visible = false;
                        }
                        //wizardNewRequest.PreRender += new EventHandler(wizardNewRequest_PreRender);
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
                Logging.GetInstance().Debug("Exit NewRequestUserControl.Page_Load");
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
        private string createSigniture()
        {
            transaction_uuid.Value = getUUID();
            signed_date_time.Value = getUTCDateTime();
            customer_ip_address.Value = getCustomer_ip_address();
            reference_number.Value = Session["RequestNumber"].ToString();
            merchant_defined_data.Value = ConfigurationManager.AppSettings["merchant_defined_data"];

            amount.Value = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + commonUtilities.Constants.HEWebUrl, commonUtilities.Constants.Configuration, "CertificateEquivalencyFees");
            currency.Value = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + commonUtilities.Constants.HEWebUrl, commonUtilities.Constants.Configuration, "CertificateEquivalencyCurrency");            
            string paymentMessage=HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "PaymentMsg", (uint)LCID);
            lbl_PaymentMsg.Text = string.Format(paymentMessage, amount.Value);

            access_key.Value = ConfigurationManager.AppSettings["access_key"];
            profile_id.Value = ConfigurationManager.AppSettings["profile_id"];
            signed_field_names.Value = ConfigurationManager.AppSettings["signed_field_names"];
            transaction_type.Value = ConfigurationManager.AppSettings["transaction_type"];
            locale.Value = LCID == (int)Language.English ? "en-us" : "ar-xn";

            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("access_key", access_key.Value);
            parameters.Add("profile_id", profile_id.Value);
            parameters.Add("transaction_uuid", transaction_uuid.Value);
            parameters.Add("signed_field_names", signed_field_names.Value);
            parameters.Add("unsigned_field_names", unsigned_field_names.Value);
            parameters.Add("signed_date_time", signed_date_time.Value);
            parameters.Add("locale", locale.Value);
            parameters.Add("customer_ip_address", customer_ip_address.Value);
            parameters.Add("merchant_defined_data1", merchant_defined_data.Value);
            parameters.Add("transaction_type", transaction_type.Value);
            parameters.Add("reference_number", reference_number.Value);
            parameters.Add("amount", amount.Value);
            parameters.Add("currency", currency.Value);
            string signeture = commonUtilities.PaymentHelper.sign(parameters);
            return signeture;
        }
        protected void wizardNewRequest_PreRender(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering NewRequestUserControl.wizardNewRequest_PreRender");
            try
            {
                Repeater SideBarList = wizardNewRequest.FindControl("HeaderContainer").FindControl("SideBarList") as Repeater;
                SideBarList.DataSource = wizardNewRequest.WizardSteps;
                SideBarList.DataBind();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewRequestUserControl.wizardNewRequest_PreRender");
            }
        }

        public string GetClassForWizardStep(object wizardStep)
        {
            Logging.GetInstance().Debug("Entering NewRequestUserControl.GetClassForWizardStep");
            string stepText = string.Empty;
            try
            {
                WizardStep step = wizardStep as WizardStep;

                if (step == null)
                {
                    stepText = "";
                }
                int stepIndex = wizardNewRequest.WizardSteps.IndexOf(step);

                if (stepIndex < wizardNewRequest.ActiveStepIndex)
                {
                    stepText = "prevStep";
                }
                else if (stepIndex > wizardNewRequest.ActiveStepIndex)
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
                Logging.GetInstance().Debug("Exit NewRequestUserControl.GetClassForWizardStep");
            }
            return stepText;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method NewRequestUserControl.btnSave_Click");

            try
            {

                if (!IsRefresh)
                {
                    //if (Page.IsValid)
                    // {


                    if (Page.Session["NewRequestId"] == null && Page.Session["EditRequestId"] == null)
                    
                        {
                           lblErrorMessage.Text = lblErrorMessage.Text +" "+ DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
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
                        //Applicants applicant = new Applicants() { PersonalID = "2663400752", BirthDate = DateTime.Parse("01 / 09 / 1998"), ApplicantName = "applicanta@moehe.gov.qa", Nationality = new Common.Entities.Nationality() { SelectedID = "4", SelectedTitle = "Qatar" }, NationalityCategory = new Common.Entities.NationalityCategory() { SelectedID = "1", SelectedTitle = "Qatari" }, ApplicantEmail = "applicanta@moehe.gov.qa", MobileNumber = "333877600" };

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
                    Entities.Request checkUniversityObject = checkUniversity.CreateCheckUniversityDataObject(applicantId);
                    int checkRequestId = BL.Request.AddCheckUniversityDetails(checkUniversityObject);
                    applicantDetails.CreateUserDataObject(applicant);

                    Entities.Request request = requestDetails.CreateSavedRequestDataObject();
                    bool updatedRequest = BL.Request.AddUpdateRequest(request, wizardNewRequest.ActiveStep.ID, request.ID, (int)Common.Utilities.RequestStatus.UCEDraft);

                    //SPContext.Current.Web.CurrentUser.
                    bool savedCalculated = requestDetails.GetCalculatedDetailsDataFromRepeater(request.ID);
                    applicantDetails.SaveAttachments();
                    requestDetails.SaveAttachments();

                    if (updatedRequest && savedCalculated)
                    {
                        string requestNumber = SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage) ? Page.Session["EditRequestId"] != null ? Page.Session["EditRequestId"].ToString() : string.Empty
                         : Page.Session["NewRequestId"] != null ? Page.Session["NewRequestId"].ToString() : string.Empty;
                        if (!string.IsNullOrEmpty(requestNumber))
                        {
                            lblSuccess_Save.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "SaveSucceed", (uint)LCID), request.RequestNumber);
                            ModalPopupSave.Show();
                        }
                    }
                    Page.Session["EditRequestId"] = request.ID;

                    Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.Requests ,Utilities.Constants.RequestHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.UCEDraft, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["EditRequestId"].ToString(), "Yes");
                    
                    Page.Session.Remove("NewRequestId");
                    Page.Session.Remove("CalcDetailsSelectedValue");
                    Page.Session.Remove("UploadCertificateSelectedValue");
                    Page.Session.Remove("FileCategory");
                    // }

                    // }
                    // }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method NewRequestUserControl.btnSave_Click");
            }
        }

        protected void CancelPayment_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/TrackRequest.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
        }

        protected void wizardNewRequest_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            Logging.GetInstance().Debug("Enter NewRequestUserControl.wizardNewRequest_NextButtonClick");
            try
            {

                if (e.CurrentStepIndex == 0)
                {
                    checkUniversity.ValidateOthers();
                }

                if (e.CurrentStepIndex != 0)
                {
                    if (Page.Session["NewRequestId"] == null && Page.Session["EditRequestId"] == null)
                    {
                        lblErrorMessage.Text = lblErrorMessage.Text + " " + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                        modalPopupError.Show();
                        return;
                    }
                }

                if (e.CurrentStepIndex==3)
                {
                    requestDetails.Validate();
                    Page.Validate();
                }
                    if (Page.IsValid)
                {


                    Applicants applicant = applicant = Common.BL.Applicants.GetApplicantProfilefromADByLoginName(SPContext.Current.Web.CurrentUser.LoginName);
                    if (e.NextStepIndex == 1 && !SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage))
                    {
                        ((Button)wizardNewRequest.FindControl("StepNavigationTemplateContainerID$StepNextButton")).Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Agree", (uint)LCID);
                        ((Button)wizardNewRequest.FindControl("StepNavigationTemplateContainerID$StepNextButton")).Enabled = false;

                    }
                    else
                    {
                        ((Button)wizardNewRequest.FindControl("StepNavigationTemplateContainerID$StepNextButton")).Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Next", (uint)LCID);
                    }

                    if (e.CurrentStepIndex == 0 /*&& Session["requestId"]==null*/)
                    {
                       

                        long qatarID = Common.BL.Applicants.GetApplicantQatarIDfromADByLoginName(SPContext.Current.Web.CurrentUser.LoginName);
                        // Check if applicant exists
                        int applicantId = Common.BL.Applicants.GetApplicantItemByPersonalID(qatarID.ToString());
                        //If applicantId=0 , add row
                        if (applicantId == 0)
                        {
                            applicantId = Common.BL.Applicants.AddApplicant(applicant, applicantId);
                        }
                        //add check university data and applicant id
                        Entities.Request checkUniversityObject = checkUniversity.CreateCheckUniversityDataObject(applicantId);
                        int requestId = BL.Request.AddCheckUniversityDetails(checkUniversityObject);

                        if (SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage))
                        {
                            Page.Session["EditRequestId"] = requestId;
                        }
                        else
                        {
                            Page.Session["NewRequestId"] = requestId;
                        }
                        Page.Session["applicantId"] = applicantId;
                    }
                    // add requestId in view state to update request in finish btn
                    //Check if applicant controls are not empty then update the applicant list
                    

                    if (e.CurrentStepIndex == 2 && Page.Session["applicantId"] != null)
                    {
                        applicant.ID = int.Parse(Page.Session["applicantId"].ToString());
                        applicantDetails.CreateUserDataObject(applicant);
                    }
                    if (e.CurrentStepIndex == 3)
                    {
                        serviceURL.Value = ConfigurationManager.AppSettings["paymentServideURL"];
                        Entities.Request request = requestDetails.CreateSubmittedRequestDataObject();
                        bool updatedRequest = BL.Request.AddUpdateRequest(request, wizardNewRequest.ActiveStep.ID, request.ID, (int)Common.Utilities.RequestStatus.UCEDraft);
                        bool savedCalculated = requestDetails.GetCalculatedDetailsDataFromRepeater(request.ID);

                        applicantDetails.SaveAttachments();
                        requestDetails.SaveAttachments();
                        signature.Value = createSigniture();

                        if (!string.IsNullOrEmpty(request.University.SelectedID))
                        {
                            Page.Session["UniversityObject"] = Common.BL.University.GetUniversityItemById(int.Parse(request.University.SelectedID));
                        }
                        Page.Session.Remove("NewRequestId"); 
                        Page.Session.Remove("EditRequestId");
                        Page.Session.Remove("CalcDetailsSelectedValue");
                        Page.Session.Remove("UploadCertificateSelectedValue");
                        Page.Session.Remove("FileCategory");

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
                Logging.GetInstance().Debug("Exiting method NewRequestUserControl.wizardNewRequest_FinishButtonClick");
            }
        }
         
        protected void wizardNewRequest_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {
            Logging.GetInstance().Debug("Enter NewRequestUserControl.wizardNewRequest_PreviousButtonClick");
            try
            {
                // populate values in each screen
                //show check university data
                wizardNewRequest.ActiveStepIndex = wizardNewRequest.ActiveStepIndex - 1;
                if (e.NextStepIndex == 1 && Page.Session["EditRequestId"] == null)
                {
                    ((Button)wizardNewRequest.FindControl("StepNavigationTemplateContainerID$StepNextButton")).Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Agree", (uint)LCID);
                }
                else
                {
                    ((Button)wizardNewRequest.FindControl("StepNavigationTemplateContainerID$StepNextButton")).Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Next", (uint)LCID);
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
                Logging.GetInstance().Debug("Exit NewRequestUserControl.wizardNewRequest_PreviousButtonClick");
            }
        }

        protected void btnModalOK_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method NewRequestUserControl.btnModalOK_Click");

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
                Logging.GetInstance().Debug("Exiting method NewRequestUserControl.btnModalOK_Click");
            }
        }
        protected void btn_ModalSaveOK_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method NewRequestUserControl.btn_ModalSaveOK_Click");

            try
            {
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ManageRequest.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method NewRequestUserControl.btn_ModalSaveOK_Click");
            }
        }

        protected void btnDashboard_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/TrackRequest.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
        }
        public String getUUID()
        {
            return System.Guid.NewGuid().ToString();
        }

        public String getUTCDateTime()
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            return time.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
        }

        public string getCustomer_ip_address()
        {
            string Str = "";
            Str = System.Net.Dns.GetHostName();
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(Str);
            IPAddress[] addr = ipEntry.AddressList;
            return addr[addr.Length - 1].ToString();
        }

        protected void SaveDraft_OnClick(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method NewRequestUserControl.SaveDraft_OnClick");

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
                Logging.GetInstance().Debug("Exiting method NewRequestUserControl.SaveDraft_OnClick");
            }

        }
        protected void btn_ModalShow_Click(object sender, EventArgs e)
        {
            ModalPopupSave.Show();
        }

        protected void btnErrorOk_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method NewRequestUserControl.btnErrorOk_Click");
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
                Logging.GetInstance().Debug("Exiting method NewRequestUserControl.btnErrorOk_Click");
            }
        }
    }
}
     