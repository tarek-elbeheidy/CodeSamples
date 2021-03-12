using ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using ITWORX.MOEHEWF.SCE.Utilities;
using ITWORX.MOEHE.Utilities;
using System.Collections.Generic;
using System.Web.Script.Serialization;
//using ITWORX.MOEHEWF.SCE.Entities;
using Newtonsoft.Json;
using ITWORX.MOEHEWF.Common.BL;
using Microsoft.SharePoint.Linq;
using ITWORX.MOEHEWF.SCE.Entities;
using ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE;
using System.Collections.Specialized;
using System.Web;
using System.Globalization;
using ITWORX.MOEHEWF.SCE.BL;
using System.Resources;
using System.Reflection;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint.Utilities;

namespace ITWORX.MOEHEWF.SCE.WebParts.NewRequest
{
    public partial class NewRequestUserControl : UserControlBase
    {
        protected DDLWithTXTWithNoPostback certificateResource;
        protected DDLWithTXTWithNoPostback schooleType;
        protected DDLWithTXTWithNoPostback certificateType;
        protected DDLWithTXTWithNoPostback dropFileUpload;
        protected DDLWithTXTWithNoPostback ddlSchoolingSystem;

        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ClientSideFileUpload FileUp1;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ClientSideFileUpload FileUploadDisplay;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.MOEHE_TC MOEHE_TC;


        public string CerTypeClientID { get { return certificateType.Client_ID; } }
        //public string AssApplicantGroupName { get { return Common.Utilities.Constants.EmployeeAsApplicant; } }
        public bool isEmpAsApplicant { get { return HelperMethods.InGroup(Common.Utilities.Constants.EmployeeAsApplicant); } }

        //public bool isEmpAsApplicant { get { return Applicants.inApplicantGroup(AssApplicantGroupName); } }

        public string LoginName { get { return SPContext.Current.Web.CurrentUser.LoginName; } }

        public string requestNumber
        {
            get
            {
                if (ViewState["requestNumber"] != null)
                {
                    return ViewState["requestNumber"].ToString();
                }
                return null;
            }
            set
            {
                ViewState["requestNumber"] = value;
            }
        }

        public int? requestID
        {
            get
            {
                if (Request.QueryString["RequestId"] != null)
                {
                    return Convert.ToInt32(Request.QueryString["RequestId"]);
                }
                else if (ViewState["RequestId"] != null)
                {
                    return Convert.ToInt32(ViewState["RequestId"]);
                }
                return null;
            }
            set
            {
                ViewState["RequestId"] = value;
            }
        }

        public string filesCount
        {
            get
            {
                if (ViewState["filesCount"] != null)
                {
                    return ViewState["filesCount"].ToString();
                }
                return null;
            }
            set
            {
                ViewState["filesCount"] = value;
            }
        }
        
        public string dearStudent
        {
            get
            {
                if (isEmpAsApplicant)
                {
                    return string.Format(LocalizedText2("ITWORX_MOEHEWF_SCE", "TermsAttachmentNote", (uint)LCID), " ");
                }
                else
                {
                    if (LCID == (int)Language.English)
                    {
                        return string.Format(LocalizedText2("ITWORX_MOEHEWF_SCE", "TermsAttachmentNote", (uint)LCID), "Dear Student, ");
                    }
                    else
                    {
                        return string.Format(LocalizedText2("ITWORX_MOEHEWF_SCE", "TermsAttachmentNote", (uint)LCID), "عزيزي الطالب ، ");
                    }
                }
                
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering NewRequestUserControl.Page_Load");
            try
            {
                MOEHE_TC.RequestType = (int)Common.Utilities.RequestType.Schooling;
                if (!IsPostBack)
                {
                    
                        if (isEmpAsApplicant)
                    {
                        ((Literal)NewRequest_Wizard.FindControlRecursive("litTermsAttachNote")).Text= string.Format(LocalizedText2("ITWORX_MOEHEWF_SCE", "TermsAttachmentNote", (uint)LCID), " ");

                        Label10.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "TempQatarID", (uint)LCID);
                    }

                    else
                    {
                        Label10.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "QatarID", (uint)LCID);
                        if (LCID == (int)Language.English)
                        {
                            ((Literal)NewRequest_Wizard.FindControlRecursive("litTermsAttachNote")).Text = string.Format(LocalizedText2("ITWORX_MOEHEWF_SCE", "TermsAttachmentNote", (uint)LCID), "Dear Student, ");
                        }
                        else
                        {
                            ((Literal)NewRequest_Wizard.FindControlRecursive("litTermsAttachNote")).Text = string.Format(LocalizedText2("ITWORX_MOEHEWF_SCE", "TermsAttachmentNote", (uint)LCID), "عزيزي الطالب ، ");
                        }
                    }

                    if (requestID != null)
                    {
                        requestHeaderDiv.Visible = true;
                        using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                        {
                            SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == requestID && r.LoginName == LoginName).FirstOrDefault();
                            if (request != null)
                            {
                                if (request.WizardActiveStep != null && request.WizardActiveStep != string.Empty)
                                {
                                    WizardStep wizardStep = GetWizardStepByTitle(NewRequest_Wizard, request.WizardActiveStep);
                                    NewRequest_Wizard.ActiveStepIndex = NewRequest_Wizard.WizardSteps.IndexOf(wizardStep);

                                  

                                }
                            }
                        }
                    }
                    if (NewRequest_Wizard.ActiveStepIndex == 0 && Request.QueryString["RequestId"] == null)
                    {
                        ((Button)NewRequest_Wizard.FindControl("StartNavigationTemplateContainerID$StartNextButton")).Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Agree", (uint)LCID);
                    }
                    if (NewRequest_Wizard.ActiveStepIndex > 0 && NewRequest_Wizard.ActiveStepIndex < 5)
                    {
                        divSaveButton.Visible = true;
                    }
                    bindDDLS();
                    BindApplicant();
                    MOIAddress_hdf.Value = new Uri(HelperMethods.GetWebAppConfigValue(SPContext.Current.Site.Url,
                             "IdentityServiceURLAjaxCall") + "?QatarId=").ToString();

                    if (requestID != null)
                    {
                        BindRequest();



                    }
                    else
                    {
                        if (!isEmpAsApplicant)
                        {
                            string moiServiceEnabled = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SCEMOIServiceEnabled");
                            var moiEnabled = false;
                            if (!string.IsNullOrEmpty(moiServiceEnabled))
                            {
                                bool.TryParse(moiServiceEnabled, out moiEnabled);
                            }
                            if (!moiEnabled)
                            {
                                txt_QatarID.Text = "10101010101";
                                txt_Name.Text = "Static Name";
                                txt_birthDate.Text = "01/01/2001";
                                ddl_Nationality.SelectedValue = "37";
                                ddl_Gender.SelectedValue = "M";
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
        private void BindAttahcmentsCategory(SCEContextDataContext ctx)
        {
            dropFileUpload.OtherText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "OtherDocuments", (uint)LCID);
            dropFileUpload.LblOtherTextBoxVisibility = true;
            dropFileUpload.LblOtherTextBoxText= HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileDescription", (uint)LCID);
            if (ddl_GoingToClass.SelectedValue == "14"  &&  certificateType.SelectedValue != "-1")
            //ddl_ScholasticLevel.SelectedValue == "13")
            {
                var selectedCountry = ctx.CountryOfStudyList.Where(c => c.Id == int.Parse(certificateResource.SelectedValue)).FirstOrDefault();
                //var fileNames = ctx.FileNameList.Where(f => f.CertificateTypeId.Contains(Convert.ToInt32(certificateType.SelectedValue)));
                var fileNames = ctx.FileNameList.Where(f => f.CertificateTypeId.Contains(Convert.ToInt32(certificateType.SelectedValue)) && f.MOEHECountryType.Contains(selectedCountry.MOEHECountryType.ToString())).ToList();
                var selectedCertType = ctx.CertificateType.Where(c => c.Id == int.Parse(certificateType.SelectedValue)).FirstOrDefault();

                string groupName = string.Empty;
                //check  if certificate type is high school, country is arab and school type is not private 
                //in order to remove the certificate from country option
                
                if (certificateType.SelectedText.Equals(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "HighSchool", (uint)LCID))
                   && selectedCountry.MOEHECountryType.ToString().Equals("Arab"))
                 
                {
                    if (schooleType.SelectedValue != "-2" && schooleType.SelectedText.Equals(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Private", (uint)LCID)))
                    {
                        groupName = selectedCertType.Title + selectedCountry.MOEHECountryType.ToString() + "Private";
                    }
                    else
                    {
                        // remove here
                        groupName = selectedCertType.Title + selectedCountry.MOEHECountryType.ToString();
                        string certText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "CertificateCopyFromCountry", (uint)LCID);
                        var file = fileNames.Find(f => f.Title.Equals(certText) || f.TitleAr.Equals(certText));
                        fileNames.Remove(file);
                    }
                      
                 

                }
                else
                {
                    groupName = selectedCertType.Title + selectedCountry.MOEHECountryType.ToString();
                }

                dropFileUpload.DataSource = fileNames;
                dropFileUpload.DataValueField = "ID";
                dropFileUpload.DataENTextField = "Title";
                dropFileUpload.DataARTextField = "TitleAr";
                dropFileUpload.ValidationGroup = "Submit";
                dropFileUpload.BingDDL();
               
                BindAttachments(fileNames.Count(),groupName );
                ViewState["filesCount"] = fileNames.Count().ToString();
            }
            else
            {
                var fileNames = ctx.FileNameList.Where(f => f.NationalityCategoryId.Contains(Convert.ToInt32(ddl_NatCat.SelectedValue)));
                //HelperMethods.BindDropDownList(ref dropFileUpload, fileNames, "ID", "TitleAr", "Title", LCID);
                //dropFileUpload.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                //dropFileUpload.IsRequired = true;
                dropFileUpload.DataSource = fileNames;
                dropFileUpload.DataValueField = "ID";
                dropFileUpload.DataENTextField = "Title";
                dropFileUpload.DataARTextField = "TitleAr";
                dropFileUpload.ValidationGroup = "Submit";
                //dropFileUpload.Title = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SchoolTypeTitle", (uint)LCID);
                //dropFileUpload.ValidationMSG = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SchoolTypeValidation", (uint)LCID);
                dropFileUpload.BingDDL();
                //if (ddl_NatCat.SelectedValue == "6" && ctx.FileNameList.Where(f => f.NationalityCategoryId.Contains(Convert.ToInt32(ddl_NatCat.SelectedValue)) && f.ScholasticLevelId.Contains(Convert.ToInt32(ddl_GoingToClass.SelectedValue))).Count() == 0)
                //{
                //    //dropFileUpload.Items.RemoveAt(dropFileUpload.Items.Count - 1);
                //    DropDownList myddl = (DropDownList)dropFileUpload.FindControl("dropWithNewOption");
                //    myddl.Items.RemoveAt(myddl.Items.Count - 2);
                //    BindAttachments(fileNames.Count());
                //    ViewState["filesCount"] = (fileNames.Count() - 1).ToString();
                //}
                //else
                //{
                var nationalityCat = ctx.NationalityCategory.Where(f => f.Id == int.Parse(ddl_NatCat.SelectedValue)).FirstOrDefault();
                BindAttachments(fileNames.Count(), nationalityCat.Title);// + 1);
                ViewState["filesCount"] = fileNames.Count().ToString();
                //}
            }
        }
        protected void wizardNewRequest_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            Logging.GetInstance().Debug("Entering NewRequestUserControl.wizardNewRequest_NextButtonClick");
            try
            {
                Page.Validate();
                if (Page.IsValid)
                {

                    if (e.NextStepIndex == 1)
                    {
                        divSaveButton.Visible = true;
                        requestHeaderDiv.Visible = true;
                        if (requestID == null)
                        {
                            AddRequest(1);
                        }
                        else
                        {
                            updateRequest(1, (int)requestID);
                        }
                        using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                        {
                            ApplicantsListFieldsContentType applicantData = ctx.ApplicantsList.Where(r => r.ApplicantName == SPContext.Current.Web.CurrentUser.Name).FirstOrDefault();
                            SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == requestID && r.LoginName == LoginName).FirstOrDefault();
                            if (request.ApplicantOfficialName != null)
                            {
                                txtApplicantName.Text = request.ApplicantOfficialName;
                                txtEmail.Text = request.Email;
                                txtMobileNumber.Text = request.MobileNumber;
                            }
                            else if (applicantData != null)
                            {
                                txtApplicantName.Text = applicantData.ApplicantName;
                                txtEmail.Text = applicantData.ApplicantEmail;
                                txtMobileNumber.Text = applicantData.MobileNumber;
                            }
                        }
                        using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                        {
                            SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == requestID && r.LoginName == LoginName).FirstOrDefault();
                            if (request != null)
                            {
                                txt_requestNum.Text = request.RequestNumber;
                                //CultureInfo english = CultureInfo.GetCultureInfo("en-US");
                                //txt_creationDate.Text = DateTime.Parse(request.CreateDate.ToString()).ToString("dd/MM/yyyy").ToString(english);
                                //txt_creationDate.Text = ExtensionMethods.QatarFormatedDate(DateTime.Parse(request.CreateDate.ToString())) + ExtensionMethods.QatarFormatedDateReturnTime(DateTime.Parse(request.CreateDate.ToString()));
                                if (LCID == (int)Language.English)
                                {
                                    txt_creationDate.Text = ConvertDateCalendar(DateTime.Parse(request.CreateDate.ToString()), "Gregorian", "en-US") + ExtensionMethods.QatarFormatedDateReturnTime(DateTime.Parse(request.CreateDate.ToString()));
                                }
                                else
                                {
                                    txt_creationDate.Text = ToArabicDigits(ConvertDateCalendar(DateTime.Parse(request.CreateDate.ToString()), "Gregorian", "en-US")) + ToArabicDigits(ExtensionMethods.QatarFormatedDateReturnTime(DateTime.Parse(request.CreateDate.ToString()))).Replace("م", "مساء").Replace("ص", "صباحا");
                                }
                                if (request.RequestStatus != ctx.RequestStatus.Where(x => x.Id == Convert.ToInt32(ITWORX.MOEHEWF.Common.Utilities.RequestStatus.SCESubmitted)).FirstOrDefault())
                                {
                                    txt_submitionDate.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "NotSubmittedYet", (uint)LCID);
                                }
                            }
                        }
                    }
                    if (e.NextStepIndex == 2)
                    {
                        requestHeaderDiv.Visible = true;
                        if (requestID == null)
                        {
                            AddRequest(2);
                        }
                        else
                        {
                            updateRequest(2, (int)requestID);
                        }

                    }
                    else if (e.NextStepIndex == 3)
                    {
                        requestHeaderDiv.Visible = true;
                        if (requestID == null)
                        {
                            AddRequest(3);
                        }
                        else
                        {
                            updateRequest(3, (int)requestID);
                        }
                    }
                    else if (e.NextStepIndex == 4)
                    {
                        requestHeaderDiv.Visible = true;
                        if (requestID == null)
                        {
                            AddRequest(4);
                        }
                        else
                        {
                            updateRequest(4, (int)requestID);
                        }
                        using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                        {
                            BindAttahcmentsCategory(ctx);

                        }
                    }
                    else if (e.NextStepIndex == 5)
                    {
                        //BindAttachments(int.Parse(filesCount));
                        //To update the wizard step
                        updateRequest(5, (int)requestID);
                        requestHeaderDiv.Visible = true;
                        FileUp1.SaveAttachments();
                        BindDisplayRequest();
                        divSaveButton.Visible = false;
                        using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                        {
                            AttachmentsLookupListFieldsContentType attachmentInfo = ctx.AttachmentsLookupList.Where(a => a.Group == ITWORX.MOEHEWF.SCE.Utilities.Constants.NewSCEAttachments).FirstOrDefault();
                            if (attachmentInfo != null)
                            {
                                FileUploadDisplay.DisplayMode = false;
                                FileUploadDisplay.Group = FileUp1.Group;//attachmentInfo.Group;
                                FileUploadDisplay.HasOptions = false;
                                FileUploadDisplay.DocumentLibraryName = attachmentInfo.DocumentLibraryName;
                                // FileUploadDisplay.IsRequired = (bool)attachmentInfo.IsRequired;
                                // FileUploadDisplay.MaxFileNumber = (int)attachmentInfo.MaxFileNumber;
                                // FileUploadDisplay.MaxSize = (int)attachmentInfo.MaxSize;
                                // FileUploadDisplay.SupportedExtensions = attachmentInfo.SupportedExtensions;
                                FileUploadDisplay.DocLibWebUrl = attachmentInfo.DocLibWebUrl;
                                // FileUploadDisplay.Title = LCID == (int)Language.English ? attachmentInfo.Title : attachmentInfo.TitleAr;
                                // FileUploadDisplay.RequiredValidationMessage = LCID == (int)Language.Arabic ? attachmentInfo.RequiredValidationMessage : attachmentInfo.RequiredValidationMessageAr;
                                FileUploadDisplay.LookupFieldName = attachmentInfo.LookupFieldName;
                                FileUploadDisplay.LookupFieldValue = Convert.ToInt32(requestID);
                                FileUploadDisplay.LookupDocumentLibraryName = Utilities.Constants.FileName;
                                // FileUploadDisplay.FileExtensionValidation = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileExtensionValidation", (uint)LCID); //"Supported file extensions are jpg,pdf,png";
                                // FileUploadDisplay.FileSizeValidationMsg = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileMaxValidationMsg", (uint)LCID), FileUp1.MaxSize); //"File size must not be greater than " + FileUp1.MaxSize + " MB";
                                // FileUploadDisplay.FileNumbersValidationMsg = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileNumbersValidationMsg", (uint)LCID), FileUp1.MaxFileNumber); //"You can't upload more than " + FileUp1.MaxFileNumber + " files";
                                // FileUploadDisplay.FileExistsValidationMsg = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileExistsValidationMsg", (uint)LCID); //"File exists with the same name";
                                //FileUploadDisplay.DropClientID = dropFileUpload.ClientID;
                                // FileUploadDisplay.ReqDropClientID = lblRequiredDrop.ClientID;
                                //FileUploadDisplay.ValidqationGroup = "Submit";
                                //FileUploadDisplay.LabelRequiredDrop = lbldropFileUpload.ClientID;
                            }
                        }
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

        protected void wizardNewRequest_PreRender(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering NewRequestUserControl.wizardNewRequest_PreRender");
            try
            {
                Repeater SideBarList = NewRequest_Wizard.FindControl("HeaderContainer").FindControl("SideBarList") as Repeater;
                SideBarList.DataSource = NewRequest_Wizard.WizardSteps;
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
                int stepIndex = NewRequest_Wizard.WizardSteps.IndexOf(step);
                if (stepIndex < NewRequest_Wizard.ActiveStepIndex)
                {
                    stepText = "prevStep";
                }
                else if (stepIndex > NewRequest_Wizard.ActiveStepIndex)
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

        protected void wizardNewRequest_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {
            /*  if (e.CurrentStepIndex == 5)
              {
                  BindAttachments(int.Parse(filesCount));
              }*/
            NewRequest_Wizard.ActiveStepIndex = NewRequest_Wizard.ActiveStepIndex - 1;
            divSaveButton.Visible = true;
            if (NewRequest_Wizard.ActiveStepIndex == 0 || NewRequest_Wizard.ActiveStepIndex == 5)
            {
                divSaveButton.Visible = false;
            }
            if (e.CurrentStepIndex == 1)
            {
                requestHeaderDiv.Visible = false;
                divSaveButton.Visible = false;

            }
            else if (e.CurrentStepIndex == 3)
            {
                Logging.GetInstance().Debug("Entering NewRequestUserControl.wizardNewRequest_PreviousButtonClick");
                try
                {
                    if (!isEmpAsApplicant)
                    {
                        using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                        {
                            SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == requestID && r.LoginName == LoginName).FirstOrDefault();
                            if (request != null)
                            {
                                txt_QatarID.Text = request.StdQatarID;
                                txt_PrintedName.Text = request.StdPrintedName;
                                ddl_Nationality.SelectedValue = request.StdNationality != null ? request.StdNationality.Id.ToString() : "-1";
                                ddl_NatCat.SelectedValue = request.StdNationalityCatId.ToString();
                                txtEmail.Text = request.Email;
                                txtMobileNumber.Text = request.MobileNumber;
                                txt_QatarID.Text = request.StdQatarID;
                                txt_birthDate.Text = request.StdBirthDate;
                                txt_Name.Text = request.StdName;
                                ddl_Gender.SelectedValue = request.StdGender != null ? request.StdGender : "-1";
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
                    Logging.GetInstance().Debug("Exit NewRequestUserControl.wizardNewRequest_PreviousButtonClick");
                }
            }
        }

        void updateRequest(int nextStep, int requestID)
        {
            Logging.GetInstance().Debug("Entering NewRequestUserControl.updateRequest");
            try
            {
                string qatarID = Applicants.GetApplicantQatarIDfromADByLoginName(LoginName).ToString();
                int id = Applicants.GetApplicantItemByPersonalID(qatarID);
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == requestID && r.LoginName == LoginName).FirstOrDefault();
                    if (request != null)
                    {
                        bool isEmp = isEmpAsApplicant;
                        request.WizardActiveStep = NewRequest_Wizard.ActiveStep.ID;
                        if (nextStep == 2)
                        {
                            request.MobileNumber = txtMobileNumber.Text;
                            request.Email = txtEmail.Text;
                            request.ApplicantOfficialName = txtApplicantName.Text;
                        }
                        else if (nextStep == 3)
                        {
                            if (isEmp)
                            {
                                request.StdQatarID = txtTempQatarID.Text;
                                request.StdPassportNum = txt_PassPort.Text;
                                request.StdBirthDate = txt_birthDate.Text;
                                request.StdName = txt_Name.Text;
                                request.StdNationality = ctx.NationalityList.Where(n => n.Id == int.Parse(ddl_Nationality.SelectedValue)).FirstOrDefault();
                                request.StdNationalityCatId = Convert.ToInt32(ddl_NatCat.SelectedValue);
                                request.StdGender = ddl_Gender.SelectedValue;
                                request.StdPrintedName = txt_PrintedName.Text;
                            }
                            else
                            {
                                request.ApplicantID = ctx.Applicants.Where(a => a.Id == id).FirstOrDefault();
                                request.StdQatarID = txt_QatarID.Text;
                                request.StdPassportNum = txt_PassPort.Text;
                                //if (request.StdBirthDate == null)
                                request.StdBirthDate = !string.IsNullOrEmpty(hdnBirthDate.Value) ? hdnBirthDate.Value : txt_birthDate.Text;
                                //if (request.StdName == null)
                                request.StdName = !string.IsNullOrEmpty(hdnStudentName.Value) ? hdnStudentName.Value : txt_Name.Text;
                                //if (request.StdNationality == null)
                                //{
                                //NationalityItem nationalityItem = NationalityList.GetNationalityByISOCode(stdNationality_hf.Value);
                                //request.StdNationality = nationalityItem;
                                request.StdNationality = !string.IsNullOrEmpty(stdNationality_hf.Value) ? ctx.NationalityList.Where(n => n.ISOCode == stdNationality_hf.Value).FirstOrDefault() :
                                    ctx.NationalityList.Where(n => n.Id == int.Parse(ddl_Nationality.SelectedValue)).FirstOrDefault();
                                //}
                                request.StdNationalityCatId = Convert.ToInt32(ddl_NatCat.SelectedValue);
                                //if (request.StdGender == null)
                                request.StdGender = !string.IsNullOrEmpty(stdGender_hf.Value) ? stdGender_hf.Value : ddl_Gender.SelectedValue;
                                request.StdPrintedName = txt_PrintedName.Text;
                            }

                        }
                        else if (nextStep == 4)
                        {
                            request.CertificateResourceId = (certificateResource.SelectedValue == "-2") ? null : (int?)Convert.ToInt32((certificateResource.SelectedValue));
                            request.OtherCertificateResource = (certificateResource.SelectedValue == "-2") ? certificateResource.OtherValue : string.Empty;
                            request.SchoolType = (schooleType.SelectedValue == "-2") ? null : ctx.SchoolType.Where(n => n.Id == Convert.ToInt32(schooleType.SelectedValue)).FirstOrDefault();
                            request.OtherSchoolType = (schooleType.SelectedValue == "-2") ? schooleType.OtherValue : string.Empty;
                            request.PrevSchool = txt_PrevSchool.Text;
                            request.LastScholasticLevel = (ddl_ScholasticLevel.SelectedValue == "-1") ? null : ctx.ScholasticLevel.Where(n => n.Id == Convert.ToInt32(ddl_ScholasticLevel.SelectedValue)).FirstOrDefault();
                            request.LastAcademicYear = ddl_LastAcademicYear.SelectedValue != "-1" ? ddl_LastAcademicYear.SelectedValue : string.Empty;
                            request.RegisteredScholasticLevel = (ddl_GoingToClass.SelectedValue == "-1") ? null : ctx.ScholasticLevel.Where(n => n.Id == Convert.ToInt32(ddl_GoingToClass.SelectedValue)).FirstOrDefault();
                            request.CertificateType = (certificateType.SelectedValue == "-2") ? null : ctx.CertificateType.Where(n => n.Id == Convert.ToInt32(certificateType.SelectedValue)).FirstOrDefault();
                            request.OtherCertificateType = (certificateType.SelectedValue == "-2") ? certificateType.OtherValue : string.Empty;
                            request.SchoolSystem = (ddlSchoolingSystem.SelectedValue == "-1") ? null : ctx.SchoolSystem.Where(n => n.Id == Convert.ToInt32(ddlSchoolingSystem.SelectedValue)).FirstOrDefault();
                            request.SchoolSystemAr = (ddlSchoolingSystem.SelectedValue == "-1") ? null : ctx.SchoolSystem.Where(n => n.Id == Convert.ToInt32(ddlSchoolingSystem.SelectedValue)).FirstOrDefault();
                            request.OtherSchoolSystem = ddlSchoolingSystem.SelectedValue == "-2" ? ddlSchoolingSystem.OtherValue : string.Empty;
                            request.EquivalencyPurpose = (ddlEquiPurpose.SelectedValue == "-1") ? null : ctx.EquivalencyPurpose.Where(n => n.Id == Convert.ToInt32(ddlEquiPurpose.SelectedValue)).FirstOrDefault();
                            request.EquivalencyPurposeAr = (ddlEquiPurpose.SelectedValue == "-1") ? null : ctx.EquivalencyPurpose.Where(n => n.Id == Convert.ToInt32(ddlEquiPurpose.SelectedValue)).FirstOrDefault();
                            request.TotalPassedYears = txtPassedYears.Text;
                            string folderUrl = DateTime.Now.ToString("yyyy/MM/dd");
                            if (certificateType.SelectedValue == "1")
                            {
                                var oldSCEIGCSE = ctx.SCEIGCSEList.ScopeToFolder("", true).ToList().Where(x => x.RequestID == ctx.SCERequests.ScopeToFolder("", true).Where(r => r.Id == requestID).FirstOrDefault()).ToList();
                                if (oldSCEIGCSE.Count > 0)
                                {
                                    ctx.SCEIGCSE.DeleteAllOnSubmit(oldSCEIGCSE);
                                }

                                var oldSCEIB = ctx.SCEIBList.ScopeToFolder("", true).ToList().Where(ib => ib.RequestID == ctx.SCERequests.ScopeToFolder("", true).Where(r => r.Id == requestID).FirstOrDefault()).ToList();
                                if (oldSCEIB.Count > 0)
                                {
                                    ctx.SCEIB.DeleteAllOnSubmit(oldSCEIB);
                                }
                                SPList sPSCEIGCSEList = SPContext.Current.Site.RootWeb.Lists.TryGetList("SCEIGCSE");
                                SPFolder SCEIGCSEFolder = ITWORX.MOEHEWF.Common.Utilities.BusinessHelper.CreateFolderInternal(sPSCEIGCSEList, sPSCEIGCSEList.RootFolder, folderUrl);
                                List<SCEIGCSE> AlevelListList = JsonConvert.DeserializeObject<List<SCEIGCSE>>(ALevel_HF.Value);
                                List<SCEIGCSE> OlevelListList = JsonConvert.DeserializeObject<List<SCEIGCSE>>(OLevel_HF.Value);
                                foreach (var item in AlevelListList)
                                {
                                    ctx.SCEIGCSE.InsertOnSubmit(new SCEIGCSEListFieldsContentType()
                                    {
                                        RequestID = ctx.SCERequests.ScopeToFolder("", true).Where(r => r.Id == requestID).FirstOrDefault(),
                                        Code = item.Code,
                                        Title = item.Title,
                                        Average = item.Avrage,
                                        Type = Type.ALevel,
                                        Path = SCEIGCSEFolder.Url
                                    });
                                }
                                foreach (var item in OlevelListList)
                                {
                                    ctx.SCEIGCSE.InsertOnSubmit(new SCEIGCSEListFieldsContentType()
                                    {
                                        RequestID = ctx.SCERequests.ScopeToFolder("", true).Where(r => r.Id == requestID).FirstOrDefault(),
                                        Code = item.Code,
                                        Title = item.Title,
                                        Average = item.Avrage,
                                        Type = Type.Olevel,
                                        Path = SCEIGCSEFolder.Url
                                    });
                                }
                            }
                            else if (certificateType.SelectedValue == "2")
                            {
                                var oldSCEIB = ctx.SCEIBList.ScopeToFolder("", true).ToList().Where(ib => ib.RequestID == ctx.SCERequests.ScopeToFolder("", true).Where(r => r.Id == requestID).FirstOrDefault()).ToList();
                                if (oldSCEIB.Count > 0)
                                {
                                    ctx.SCEIB.DeleteAllOnSubmit(oldSCEIB);
                                }
                                var oldSCEIGCSE = ctx.SCEIGCSEList.ScopeToFolder("", true).ToList().Where(x => x.RequestID == ctx.SCERequests.ScopeToFolder("", true).Where(r => r.Id == requestID).FirstOrDefault()).ToList();
                                if (oldSCEIGCSE.Count > 0)
                                {
                                    ctx.SCEIGCSE.DeleteAllOnSubmit(oldSCEIGCSE);
                                }
                                SPList sPIbList = SPContext.Current.Site.RootWeb.Lists.TryGetList("SCEIB");
                                SPFolder ibFolder = ITWORX.MOEHEWF.Common.Utilities.BusinessHelper.CreateFolderInternal(sPIbList, sPIbList.RootFolder, folderUrl);
                                List<SCEIB> ibList = JsonConvert.DeserializeObject<List<SCEIB>>(IBList_HF.Value);
                                foreach (var ib in ibList)
                                {
                                    ctx.SCEIB.InsertOnSubmit(new SCEIBListFieldsContentType()
                                    {
                                        RequestID = ctx.SCERequests.ScopeToFolder("", true).Where(r => r.Id == requestID).FirstOrDefault(),
                                        Code = ib.Code,
                                        Title = ib.Title,
                                        Level = ctx.Levels.Where(n => n.Title == ib.Level).FirstOrDefault(),
                                        PointCount = ib.Points,
                                        Path = ibFolder.Url
                                    });
                                }
                            }
                            else
                            {
                                var oldSCEIB = ctx.SCEIBList.ScopeToFolder("", true).ToList().Where(ib => ib.RequestID == ctx.SCERequests.ScopeToFolder("", true).Where(r => r.Id == requestID).FirstOrDefault()).ToList();
                                if (oldSCEIB.Count > 0)
                                {
                                    ctx.SCEIB.DeleteAllOnSubmit(oldSCEIB);
                                }
                                var oldSCEIGCSE = ctx.SCEIGCSEList.ScopeToFolder("", true).ToList().Where(x => x.RequestID == ctx.SCERequests.ScopeToFolder("", true).Where(r => r.Id == requestID).FirstOrDefault()).ToList();
                                if (oldSCEIGCSE.Count > 0)
                                {
                                    ctx.SCEIGCSE.DeleteAllOnSubmit(oldSCEIGCSE);
                                }
                            }
                        }
                        ctx.SubmitChanges();
                    }
                    ViewState["RequestId"] = requestID;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewRequestUserControl.updateRequest");
            }
        }

        void AddRequest(int nextStep)
        {
            Logging.GetInstance().Debug("Entering NewRequestUserControl.AddRequest");
            try
            {
                if (nextStep == 2)
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        string folderUrl = DateTime.Now.ToString("yyyy/MM/dd");
                        string EmployeeAsApplicant = Common.Utilities.Constants.EmployeeAsApplicant;
                        bool isEmp = isEmpAsApplicant;
                        string qatarID = Applicants.GetApplicantQatarIDfromADByLoginName(LoginName).ToString();
                        int id = Applicants.GetApplicantItemByPersonalID(qatarID);
                        using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                        {
                            SCERequestsListFieldsContentType request = new SCERequestsListFieldsContentType()
                            {
                                ApplicantID = ctx.Applicants.Where(a => a.Id == id).FirstOrDefault(),
                                MobileNumber = txtMobileNumber.Text,
                                Email = txtEmail.Text,
                                ApplicantOfficialName = txtApplicantName.Text,
                                IsEmployee = isEmpAsApplicant ? IsEmployee.Yes : IsEmployee.No,
                                RequestNumber = requestID.ToString(),
                                LoginName = LoginName,
                                RequestStatus = ctx.RequestStatus.Where(x => x.Id == Convert.ToInt32(ITWORX.MOEHEWF.Common.Utilities.RequestStatus.SCEDraft)).FirstOrDefault(),
                                RequestTypeId = Convert.ToInt32(ITWORX.MOEHEWF.Common.Utilities.RequestType.Schooling),
                                
                            };
                            SPList sPList = SPContext.Current.Site.RootWeb.Lists.TryGetList("SCERequests");
                            SPFolder folder = ITWORX.MOEHEWF.Common.Utilities.BusinessHelper.CreateFolderInternal(sPList, sPList.RootFolder, folderUrl);
                            request.Path = folder.Url;
                            ctx.SCERequests.InsertOnSubmit(request);
                            ctx.SubmitChanges();
                            requestID = request.Id;
                            ViewState["RequestId"] = requestID;

                            long requestIDlong = (long)requestID;
                            string RequestNoPrefix = HelperMethods.GetConfigurationValue(SPContext.Current.Site.RootWeb.Url, Utilities.Constants.ConfigurationList, Utilities.Constants.RequestNoPrefix);
                            string PaddingTotalRequest = HelperMethods.GetConfigurationValue(SPContext.Current.Site.RootWeb.Url, Utilities.Constants.ConfigurationList, Utilities.Constants.PaddingTotalRequest);
                            ViewState["requestNumber"] = RequestNoPrefix + requestIDlong.ToString(PaddingTotalRequest);
                            request.RequestNumber = ViewState["requestNumber"].ToString();
                            //creation date
                            CultureInfo english = CultureInfo.GetCultureInfo("en-US");
                            request.CreateDate = DateTime.Parse(DateTime.Now.ToString(english), CultureInfo.InvariantCulture);
                            ctx.SubmitChanges();
                        }
                    });
                }
                else if (nextStep == 1)
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        string folderUrl = DateTime.Now.ToString("yyyy/MM/dd");
                        string EmployeeAsApplicant = Common.Utilities.Constants.EmployeeAsApplicant;
                        bool isEmp = isEmpAsApplicant;
                        string qatarID = Applicants.GetApplicantQatarIDfromADByLoginName(LoginName).ToString();
                        int id = Applicants.GetApplicantItemByPersonalID(qatarID);
                        using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                        {
                            SCERequestsListFieldsContentType request = new SCERequestsListFieldsContentType()
                            {
                                ApplicantID = ctx.Applicants.Where(a => a.Id == id).FirstOrDefault(),
                                IsEmployee = isEmpAsApplicant ? IsEmployee.Yes : IsEmployee.No,
                                RequestNumber = requestID.ToString(),
                                LoginName = LoginName,
                                RequestStatus = ctx.RequestStatus.Where(x => x.Id == Convert.ToInt32(ITWORX.MOEHEWF.Common.Utilities.RequestStatus.SCEDraft)).FirstOrDefault(),
                                RequestTypeId = Convert.ToInt32(ITWORX.MOEHEWF.Common.Utilities.RequestType.Schooling),
                                EmployeeAssignedTo = isEmpAsApplicant ? SPContext.Current.Web.CurrentUser.LoginName : string.Empty

                            };
                            SPList sPList = SPContext.Current.Site.RootWeb.Lists.TryGetList("SCERequests");
                            SPFolder folder = ITWORX.MOEHEWF.Common.Utilities.BusinessHelper.CreateFolderInternal(sPList, sPList.RootFolder, folderUrl);
                            request.Path = folder.Url;
                            ctx.SCERequests.InsertOnSubmit(request);
                            ctx.SubmitChanges();
                            requestID = request.Id;
                            ViewState["RequestId"] = requestID;

                            long requestIDlong = (long)requestID;
                            string RequestNoPrefix = HelperMethods.GetConfigurationValue(SPContext.Current.Site.RootWeb.Url, Utilities.Constants.ConfigurationList, Utilities.Constants.RequestNoPrefix);
                            string PaddingTotalRequest = HelperMethods.GetConfigurationValue(SPContext.Current.Site.RootWeb.Url, Utilities.Constants.ConfigurationList, Utilities.Constants.PaddingTotalRequest);
                            ViewState["requestNumber"] = RequestNoPrefix + requestIDlong.ToString(PaddingTotalRequest);
                            request.RequestNumber = ViewState["requestNumber"].ToString();
                            //creation date
                            CultureInfo english = CultureInfo.GetCultureInfo("en-US");
                            request.CreateDate = DateTime.Parse(DateTime.Now.ToString(english), CultureInfo.InvariantCulture);
                            ctx.SubmitChanges();
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewRequestUserControl.AddRequest");
            }

        }

        void BindRequest()
        {
            Logging.GetInstance().Debug("Entering NewRequestUserControl.BindRequest");
            try
            {
                MOEHE_TC.CheckVisibility = false;
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == requestID && r.LoginName == LoginName).FirstOrDefault();
                    if (request != null)
                    {
                        if (isEmpAsApplicant)
                        {
                            txtTempQatarID.Text = request.StdQatarID;
                            txt_PassPort.Text = request.StdPassportNum;
                            DateTime dateBirthEmp;
                            bool isBirthEmpSuccess = DateTime.TryParse(request.StdBirthDate, out dateBirthEmp);
                            txt_birthDate.Text = isBirthEmpSuccess ? ExtensionMethods.QatarFormatedDate(dateBirthEmp) : string.Empty;
                            txt_Name.Text = request.StdName;
                          
                        }
                     
                        ViewState["requestNumber"] = request.RequestNumber;
                        txt_requestNum.Text = request.RequestNumber;
                        if (request.RequestStatus != ctx.RequestStatus.Where(x => x.Id == Convert.ToInt32(ITWORX.MOEHEWF.Common.Utilities.RequestStatus.SCESubmitted)).FirstOrDefault())
                        {
                            txt_submitionDate.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "NotSubmittedYet", (uint)LCID);
                        }
                        if (LCID == (int)Language.English)
                        {
                            txt_creationDate.Text = ConvertDateCalendar(DateTime.Parse(request.CreateDate.ToString()), "Gregorian", "en-US") + ExtensionMethods.QatarFormatedDateReturnTime(DateTime.Parse(request.CreateDate.ToString()));
                        }
                        else
                        {
                            txt_creationDate.Text = ToArabicDigits(ConvertDateCalendar(DateTime.Parse(request.CreateDate.ToString()), "Gregorian", "en-US")) + ToArabicDigits(ExtensionMethods.QatarFormatedDateReturnTime(DateTime.Parse(request.CreateDate.ToString()))).Replace("م", "مساء").Replace("ص", "صباحا");
                        }

                        string moiServiceEnabled = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SCEMOIServiceEnabled");
                        var moiEnabled = false;
                        if (!string.IsNullOrEmpty(moiServiceEnabled))
                        {
                            bool.TryParse(moiServiceEnabled, out moiEnabled);
                        }
                        if (moiEnabled)
                        {

                            txt_QatarID.Text = request.StdQatarID;
                            txt_Name.Text = request.StdName;
                            txt_birthDate.Text = request.StdBirthDate;
                            ddl_Nationality.SelectedValue = request.StdNationality != null ? request.StdNationality.Id.ToString() : "-1";
                            ddl_Gender.SelectedValue = request.StdGender != null ? request.StdGender : "-1";
                        }
                        else
                        {
                            if (!isEmpAsApplicant)
                            {
                                if (!string.IsNullOrEmpty(request.StdQatarID))
                                {
                                    txt_QatarID.Text = request.StdQatarID;
                                    txt_Name.Text = request.StdName;
                                    txt_birthDate.Text = request.StdBirthDate;
                                    ddl_Nationality.SelectedValue = request.StdNationality != null ? request.StdNationality.Id.ToString() : "-1";
                                    ddl_Gender.SelectedValue = request.StdGender != null ? request.StdGender : "-1";
                                }
                                else
                                {
                                    txt_QatarID.Text = "10101010101";
                                    txt_Name.Text = "Static Name";
                                    txt_birthDate.Text = "01/01/2001";
                                    ddl_Nationality.SelectedValue = "37";
                                    ddl_Gender.SelectedValue = "M";
                                }
                            }
                            else
                            {
                                txt_QatarID.Text = request.StdQatarID;
                                txt_Name.Text = request.StdName;
                                txt_birthDate.Text = request.StdBirthDate;
                                ddl_Nationality.SelectedValue = request.StdNationality != null ? request.StdNationality.Id.ToString() : "-1";
                                ddl_Gender.SelectedValue = request.StdGender != null ? request.StdGender : "-1";
                            }

                        }

                        //DateTime dateCreated;
                        //bool isCreatSuccess = DateTime.TryParse(request.CreateDate.Value.ToString(), out dateCreated);
                        //txt_creationDate.Text = isCreatSuccess ? ExtensionMethods.QatarFormatedDate(dateCreated) + ExtensionMethods.QatarFormatedDateReturnTime(dateCreated) : string.Empty;
                        //txt_creationDate.Text = isCreatSuccess ? ConvertDateCalendar(dateCreated, "Gregorian", "en-US") : string.Empty;
                        //ConvertDateCalendar(DateTime.Parse(request.CreateDate.ToString()), "Gregorian", "en-US");
                        txt_PrintedName.Text = request.StdPrintedName;
                        ddl_NatCat.SelectedValue = request.StdNationalityCatId.ToString();
                        txtApplicantName.Text = request.ApplicantOfficialName;
                        txtEmail.Text = request.Email;
                        txtMobileNumber.Text = request.MobileNumber;
                        //DateTime dateBirth;
                        //bool isSuccess = DateTime.TryParse(request.StdBirthDate, out dateBirth);
                        if (request.CertificateResourceId == null && string.IsNullOrEmpty(request.OtherCertificateResource))
                        {
                            certificateResource.SelectedValue = "-1";
                        }
                        else if (request.CertificateResourceId == null && !string.IsNullOrEmpty(request.OtherCertificateResource))
                        {
                            certificateResource.SelectedValue = "-2";
                            certificateResource.OtherValue = request.OtherCertificateResource;
                        }
                        else if (request.CertificateResourceId != null)
                        {
                            certificateResource.SelectedValue = request.CertificateResourceId.ToString();
                        }

                        if (request.SchoolType == null && string.IsNullOrEmpty(request.OtherSchoolType))
                        {
                            schooleType.SelectedValue = "-1";
                        }
                        else if (request.SchoolType == null && !string.IsNullOrEmpty(request.OtherSchoolType))
                        {
                            schooleType.SelectedValue = "-2";
                            schooleType.OtherValue = request.OtherSchoolType;
                        }
                        else if (request.SchoolType != null)
                        {
                            schooleType.SelectedValue = request.SchoolType.Id.ToString();
                        }
                        if (request.SchoolSystem == null && string.IsNullOrEmpty(request.OtherSchoolSystem))
                        {
                            ddlSchoolingSystem.SelectedValue = "-1";
                        }
                        else if (request.SchoolSystem == null && !string.IsNullOrEmpty(request.OtherSchoolSystem))
                        {
                            ddlSchoolingSystem.SelectedValue = "-2";
                            ddlSchoolingSystem.OtherValue = request.OtherSchoolSystem;
                        }
                        else if (request.SchoolSystem != null)
                        {
                            ddlSchoolingSystem.SelectedValue = request.SchoolSystem.Id.ToString();
                        }
                        //if (request.SchoolSystem != null)
                        //{
                        //    ddlSchoolingSystem.SelectedValue = request.SchoolSystem.Id.ToString();
                        //}
                        if (request.EquivalencyPurpose != null)
                        {
                            ddlEquiPurpose.SelectedValue = request.EquivalencyPurpose.Id.ToString();
                        }
                        txt_PrevSchool.Text = request.PrevSchool;
                        ddl_ScholasticLevel.SelectedValue = request.LastScholasticLevel != null ? request.LastScholasticLevel.Id.ToString() : "-1";
                        ddl_LastAcademicYear.SelectedValue = string.IsNullOrEmpty(request.LastAcademicYear) ? "-1" : request.LastAcademicYear;
                        ddl_GoingToClass.SelectedValue = request.RegisteredScholasticLevel != null ? request.RegisteredScholasticLevel.Id.ToString() : "-1";
                        txtPassedYears.Text = request.TotalPassedYears;
                        if (request.CertificateType == null && string.IsNullOrEmpty(request.OtherCertificateType))
                        {
                            certificateType.SelectedValue = "-1";
                        }
                        else if (request.CertificateType == null && !string.IsNullOrEmpty(request.OtherCertificateType))
                        {
                            certificateType.SelectedValue = "-2";
                            certificateType.OtherValue = request.OtherCertificateType;
                        }
                        else if (request.CertificateType != null)
                        {
                            certificateType.SelectedValue = request.CertificateType.Id.ToString();
                            if (request.CertificateType.Id == 1)
                            {
                                var oLevels = ctx.SCEIGCSEList.ScopeToFolder("", true).ToList().Where(x => x.RequestID == ctx.SCERequests.ScopeToFolder("", true).Where(r => r.Id == requestID).FirstOrDefault() && x.Type == Type.Olevel).ToList();
                                List<SCEIGCSE> OlevelListList = new List<SCEIGCSE>();
                                int counter = 1;
                                foreach (var item in oLevels)
                                {
                                    OlevelListList.Add(new SCEIGCSE()
                                    {
                                        //ID = (int)item.Id,
                                        ID = counter,
                                        Avrage = item.Average,
                                        Code = item.Code,
                                        Title = item.Title
                                    });
                                    counter++;
                                }
                                OLevel_HF.Value = JsonConvert.SerializeObject(OlevelListList);
                                var aLevels = ctx.SCEIGCSEList.ScopeToFolder("", true).Where(x => x.RequestID.Id == requestID && x.Type == Type.ALevel);
                                List<SCEIGCSE> AlevelListList = new List<SCEIGCSE>();
                                counter = 1;
                                foreach (var item in aLevels)
                                {
                                    AlevelListList.Add(new SCEIGCSE()
                                    {
                                        //ID = (int)item.Id,
                                        ID = counter,
                                        Avrage = item.Average,
                                        Code = item.Code,
                                        Title = item.Title
                                    });
                                    counter++;
                                }
                                ALevel_HF.Value = JsonConvert.SerializeObject(AlevelListList);
                            }
                            else if (request.CertificateType.Id == 2)
                            {
                                var SCEIBS = ctx.SCEIBList.ScopeToFolder("", true).Where(x => x.RequestID.Id == requestID);
                                List<SCEIB> SCEIBList = new List<SCEIB>();
                                foreach (var item in SCEIBS)
                                {
                                    SCEIBList.Add(new SCEIB()
                                    {
                                        ID = (int)item.Id,
                                        Title = item.Title,
                                        Code = item.Code,
                                        Level = item.Level.Title,
                                        //LevelTitle = item.Level.Title,
                                        Points = item.PointCount
                                    });
                                }
                                IBList_HF.Value = JsonConvert.SerializeObject(SCEIBList);
                            }
                        }

                        //Check if current step is 4 (attachment step)

                        if (NewRequest_Wizard.ActiveStepIndex == 4)
                        {
                            BindAttahcmentsCategory(ctx);


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
                Logging.GetInstance().Debug("Exit NewRequestUserControl.BindRequest");
            }
        }

        void bindDDLS()
        {
            Logging.GetInstance().Debug("Entering NewRequestUserControl.bindDDLS");
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    certificateResource.IsRequired = true;
                    var CountriesOfStudy = ITWORX.MOEHEWF.Common.BL.CountryOfStudy.GetAll(LCID);
                    string SCECountryOfStudy = HelperMethods.GetConfigurationValue(SPContext.Current.Site.RootWeb.Url, Utilities.Constants.ConfigurationList, Utilities.Constants.SCECountryOfStudy);
                    if (!string.IsNullOrEmpty(SCECountryOfStudy) && SCECountryOfStudy.ToLower() == "qatar")
                    {
                        certificateResource.DataSource = CountriesOfStudy.Where(x => x.EnglishTitle.ToLower() == "qatar");
                        certificateResource.HideOtherOption = true;
                    }
                    else
                    {
                        certificateResource.DataSource = CountriesOfStudy;
                        certificateResource.HideOtherOption = false;

                    }
                    certificateResource.DataValueField = "ID";
                    certificateResource.DataENTextField = "EnglishTitle";
                    certificateResource.DataARTextField = "ArabicTitle";
                    certificateResource.ValidationGroup = "Submit";
                    
                    certificateResource.Title = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "CertificateResourceTitle", (uint)LCID);
                    certificateResource.ValidationMSG = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "CertificateResourceValidation", (uint)LCID);
                    certificateResource.BingDDL();

                    schooleType.IsRequired = true;
                    schooleType.DataSource = BL.SchoolType.GetAll();
                    schooleType.DataValueField = "ID";
                    schooleType.DataENTextField = "Title";
                    schooleType.DataARTextField = "TitleAr";
                    schooleType.ValidationGroup = "Submit";
                    schooleType.Title = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SchoolTypeTitle", (uint)LCID);
                    schooleType.ValidationMSG = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SchoolTypeValidation", (uint)LCID);
                    schooleType.BingDDL();

                    using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                    {
                        var schooSystem = ctx.SchoolSystemList;
                        //HelperMethods.BindDropDownList(ref ddlSchoolingSystem, schooSystem, "ID", "TitleAr", "Title", LCID);
                        //ddlSchoolingSystem.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));

                        ddlSchoolingSystem.IsRequired = false;
                        ddlSchoolingSystem.DataSource = schooSystem;
                        ddlSchoolingSystem.DataValueField = "ID";
                        ddlSchoolingSystem.DataENTextField = "Title";
                        ddlSchoolingSystem.DataARTextField = "TitleAr";
                        ddlSchoolingSystem.ValidationGroup = "Submit";
                        ddlSchoolingSystem.Title = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SchoolingSystem", (uint)LCID);
                        //ddlSchoolingSystem.ValidationMSG = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SchoolTypeValidation", (uint)LCID);
                        ddlSchoolingSystem.BingDDL();

                        var equiPurpose = ctx.EquivalencyPurposeList;
                        HelperMethods.BindDropDownList(ref ddlEquiPurpose, equiPurpose, "ID", "TitleAr", "Title", LCID);
                        ddlEquiPurpose.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));

                        var igDegree = ctx.IGDegreeList;
                        HelperMethods.BindDropDownList(ref ddlOlevelAverage, igDegree, "ID", "TitleAr", "Title", LCID);
                        ddlOlevelAverage.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                        HelperMethods.BindDropDownList(ref ddlAlevelAverage, igDegree, "ID", "TitleAr", "Title", LCID);
                        ddlAlevelAverage.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));

                        var lastScholasticLevel = ctx.ScholasticLevelList.ToList();
                        string notAvailableLevel = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "NotAvailable", (uint)LCID);
                        var level = lastScholasticLevel.Find(f => f.Title.Equals(notAvailableLevel) || f.TitleAr.Equals(notAvailableLevel));
                        lastScholasticLevel.Remove(level);

                        HelperMethods.BindDropDownList(ref ddl_ScholasticLevel, lastScholasticLevel, "ID", "TitleAr", "Title", LCID);
                        ddl_ScholasticLevel.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));


                    }

                   
                    var IBLevel = BL.Levels.GetAll();
                    HelperMethods.BindDropDownList(ref ddl_IBLevel, IBLevel, "ID", "TitleAr", "Title", LCID);
                    ddl_IBLevel.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));

                    var scholasticLevel = BL.ScholasticLevel.GetAll();
                    HelperMethods.BindDropDownList(ref ddl_GoingToClass, scholasticLevel, "ID", "TitleAr", "Title", LCID);
                    ddl_GoingToClass.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                    ddl_GoingToClass.Items.RemoveAt(1);

                    certificateType.DataSource = BL.CertificateType.GetEquivalenceCertificateType();
                    certificateType.DataValueField = "ID";
                    certificateType.DataENTextField = "Title";
                    certificateType.DataARTextField = "TitleAr";
                    certificateType.IsRequired = false;
                  //  certificateType.ValidationGroup = "Submit";
                    certificateType.Title = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "CertificateTypeTitle", (uint)LCID);
                    certificateType.BingDDL();

                    ddl_NatCat.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                    ddl_NatCat.AppendDataBoundItems = true;
                    var nationalCategories = NationalityCategory.GetAll().Where(x => x.ID == 1 || x.ID == 6).ToList();
                    HelperMethods.BindDropDownList(ref ddl_NatCat, nationalCategories, "ID", "ArabicTitle", "EnglishTitle", (int)SPContext.Current.Web.Language);

                    ddl_Nationality.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                    ddl_Nationality.AppendDataBoundItems = true;
                    if (LCID == (int)Language.English)
                    {
                        HelperMethods.BindDropDownList(ref ddl_Nationality, Nationality.GetAll().OrderBy(n => n.EnglishTitle), "ID", "ArabicTitle", "EnglishTitle", (int)SPContext.Current.Web.Language);
                    }
                    else
                    {
                        HelperMethods.BindDropDownList(ref ddl_Nationality, Nationality.GetAll().OrderBy(n => n.ArabicTitle), "ID", "ArabicTitle", "EnglishTitle", (int)SPContext.Current.Web.Language);
                    }

                    ddl_Gender.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                    ddl_Gender.Items.Insert(1, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Male", (uint)LCID), "M"));
                    ddl_Gender.Items.Insert(2, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Female", (uint)LCID), "F"));

                    ddl_LastAcademicYear.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                    ddl_LastAcademicYear.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref ddl_LastAcademicYear, getCertificateYearsReverse(), "Key", "Value", "Value", (int)SPContext.Current.Web.Language);

                    //using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                    //{
                    //    var fileNames = ctx.FileNameList.Where(f => f.NationalityCategoryId.Contains(Convert.ToInt32(ddl_NatCat.SelectedValue)));
                    //    HelperMethods.BindDropDownList(ref dropFileUpload, fileNames, "ID", "TitleAr", "Title", LCID);
                    //    dropFileUpload.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                    //}
                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewRequestUserControl.bindDDLS");
            }
        }

        void BindDisplayRequest()
        {
            Logging.GetInstance().Debug("Entering NewRequestUserControl.BindDisplayRequest");
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                    {
                        requestID = Convert.ToInt32(ViewState["RequestId"]);
                        if (requestID != null)
                        {
                            SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == requestID && r.LoginName == LoginName).FirstOrDefault();
                            if (request != null)
                            {
                                if (isEmpAsApplicant)
                                {
                                    divPassportContainer.Style.Add("display", "block");
                                    lblPassPortDisplay.Visible = true;
                                    lblPassPortVal.Visible = true;
                                    lblPassPortVal.Text = request.StdPassportNum;
                                }
                                lblApplicantOfficialNameVal.Text = request.ApplicantOfficialName;
                                lblMobileNumberVal.Text = request.MobileNumber;
                                lblEmailVal.Text = request.Email;
                                lblQatarIDVal.Text = request.StdQatarID;
                                lblbirthDateVal.Text = request.StdBirthDate;
                                lblNameVal.Text = request.StdName;
                                if (request.StdNationality != null)
                                {
                                    NationalityItem nationalityItem = NationalityList.GetNationalityById(Convert.ToInt32(request.StdNationality.Id));
                                    lblNationalityVal.Text = LCID == (int)Language.English ? nationalityItem.Title : nationalityItem.TitleAr;
                                }
                                if (request.StdNationalityCatId != null)
                                {
                                    //NationalityCategoryItem nationalityCategoryItem = NationalityCategoryList.GetNationalityCategoryById(Convert.ToInt32(request.StdNationalityCatId));
                                    //lblNatCatVal.Text = LCID == (int)Language.English ? nationalityCategoryItem.Title : nationalityCategoryItem.TitleAr;
                                    NationalityCategoryListFieldsContentType natCatItem = ctx.NationalityCategoryList.Where(n => n.Id == request.StdNationalityCatId).FirstOrDefault();
                                    lblNatCatVal.Text = LCID == (int)Language.English ? natCatItem.Title : natCatItem.TitleAr;
                                }
                                if (request.StdGender != null)
                                {
                                    lblGenderVal.Text = request.StdGender == "M" ? LCID == (int)Language.English ? "Male" : "ذكر" : LCID == (int)Language.English ? "Female" : "انثى";
                                }
                                lblPrintedNameDisplay.Text = request.StdPrintedName;
                                if (string.IsNullOrEmpty(request.OtherCertificateResource))
                                {
                                    //CertificateResourceItem certificateResourceItem = CertificateResourceList.GetCertificateResourceById(Convert.ToInt32(request.CertificateResourceId));
                                    //lblcertificateResource.Text = LCID == (int)Language.English ? certificateResourceItem.Title : certificateResourceItem.TitleAr;
                                    CountryOfStudyListFieldsContentType certResource = ctx.CountryOfStudyList.Where(c => c.Id == request.CertificateResourceId).FirstOrDefault();
                                    lblcertificateResource.Text = LCID == (int)Language.English ? certResource.Title : certResource.TitleAr;
                                }
                                else
                                {
                                    lblcertificateResource.Text = request.OtherCertificateResource;
                                }
                                if (request.SchoolType == null)
                                {
                                    lblSchoolTypeVal.Text = request.OtherSchoolType;
                                }
                                else
                                {
                                    lblSchoolTypeVal.Text = LCID == (int)Language.English ? request.SchoolType.Title : ctx.SchoolTypeList.Where(s => s.Id == request.SchoolType.Id).FirstOrDefault().TitleAr;
                                }
                                lblPrevSchool.Text = request.PrevSchool;
                                if (string.IsNullOrEmpty(request.OtherSchoolSystem))
                                {
                                    lblSchoolSystemVal.Text = request.SchoolSystem != null ? LCID == (int)Language.English ? request.SchoolSystem.Title
                                    : ctx.SchoolSystemList.Where(s => s.Id == request.SchoolSystem.Id).FirstOrDefault().TitleAr : "";
                                }
                                else
                                {
                                    lblSchoolSystemVal.Text = request.OtherSchoolSystem;
                                }
                                lblScholasticLevel.Text = request.LastScholasticLevel != null ? LCID == (int)Language.English ? request.LastScholasticLevel.Title : ctx.ScholasticLevelList.Where(s => s.Id == request.LastScholasticLevel.Id).FirstOrDefault().TitleAr : "";
                                lblLastAcademicYear.Text = string.IsNullOrEmpty(request.LastAcademicYear) ? "" : request.LastAcademicYear;
                                lblEquiPurposeVal.Text = request.EquivalencyPurpose != null ? LCID == (int)Language.English ? request.EquivalencyPurpose.Title
                                    : ctx.EquivalencyPurposeList.Where(e => e.Id == request.EquivalencyPurposeAr.Id).FirstOrDefault().TitleAr : "";
                                lblGoingToClass.Text = request.RegisteredScholasticLevel != null ? LCID == (int)Language.English ? request.RegisteredScholasticLevel.Title : ctx.ScholasticLevelList.Where(g => g.Id == request.RegisteredScholasticLevel.Id).FirstOrDefault().TitleAr : "";
                                lblTotalPassedYears.Text = request.TotalPassedYears;
                                if (request.CertificateType == null)
                                {
                                    lblCertificateTypeVal.Text = request.OtherCertificateType;
                                }
                                //else if (request.CertificateType == null && !string.IsNullOrEmpty(request.OtherCertificateType))
                                //{
                                //    certificateType.SelectedValue = "-2";
                                //    certificateType.OtherValue = request.OtherCertificateType;
                                //}
                                else if (request.CertificateType != null)
                                {
                                    lblCertificateTypeVal.Text = LCID == (int)Language.English ? request.CertificateType.Title : ctx.CertificateTypeCT.Where(c => c.Id == request.CertificateType.Id).FirstOrDefault().TitleAr;
                                    if (request.CertificateType.Id == 1)
                                    {
                                        var oLevels = ctx.SCEIGCSEList.ScopeToFolder("", true).Where(x => x.RequestID.Id == requestID && x.Type == Type.Olevel);
                                        List<SCEIGCSE> OlevelListList = new List<SCEIGCSE>();
                                        foreach (var item in oLevels)
                                        {
                                            OlevelListList.Add(new SCEIGCSE()
                                            {
                                                ID = (int)item.Id,
                                                Avrage = item.Average,
                                                Code = item.Code,
                                                Title = item.Title
                                            });
                                        }
                                        OLevel_HF.Value = JsonConvert.SerializeObject(OlevelListList);
                                        var aLevels = ctx.SCEIGCSEList.ScopeToFolder("", true).Where(x => x.RequestID.Id == requestID && x.Type == Type.ALevel);
                                        List<SCEIGCSE> AlevelListList = new List<SCEIGCSE>();
                                        foreach (var item in aLevels)
                                        {
                                            AlevelListList.Add(new SCEIGCSE()
                                            {
                                                ID = (int)item.Id,
                                                Avrage = item.Average,
                                                Code = item.Code,
                                                Title = item.Title
                                            });
                                        }
                                        ALevel_HF.Value = JsonConvert.SerializeObject(AlevelListList);
                                    }
                                    else if (request.CertificateType.Id == 2)
                                    {
                                        var SCEIBS = ctx.SCEIBList.ScopeToFolder("", true).Where(x => x.RequestID.Id == requestID);
                                        List<SCEIB> SCEIBList = new List<SCEIB>();
                                        foreach (var item in SCEIBS)
                                        {
                                            SCEIBList.Add(new SCEIB()
                                            {
                                                ID = (int)item.Id,
                                                Title = item.Title,
                                                Code = item.Code,
                                                Level = item.Level.Title,
                                                //LevelTitle = item.Level.Title,
                                                Points = item.PointCount
                                            });
                                        }
                                        IBList_HF.Value = JsonConvert.SerializeObject(SCEIBList);
                                    }
                                }
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewRequestUserControl.BindDisplayRequest");
            }
        }

        void BindAttachments(int maxFiles,string groupName)
        {
            Logging.GetInstance().Debug("Entering NewRequestUserControl.BindAttachments");
            try
            {
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    AttachmentsLookupListFieldsContentType attachmentInfo = ctx.AttachmentsLookupList.Where(a => a.Group == ITWORX.MOEHEWF.SCE.Utilities.Constants.NewSCEAttachments).FirstOrDefault();
                    if (attachmentInfo != null)
                    {
                        FileUp1.Group = groupName;// attachmentInfo.Group;
                        FileUp1.HasOptions = true;
                        FileUp1.DocumentLibraryName = attachmentInfo.DocumentLibraryName;
                        FileUp1.IsRequired = (bool)attachmentInfo.IsRequired;
                        //FileUp1.MaxFileNumber = (int)attachmentInfo.MaxFileNumber;
                        FileUp1.MaxFileNumber = maxFiles;
                        FileUp1.MaxSize = (int)attachmentInfo.MaxSize;
                        FileUp1.SupportedExtensions = attachmentInfo.SupportedExtensions;
                        FileUp1.DocLibWebUrl = attachmentInfo.DocLibWebUrl;
                        //FileUp1.Title = LCID == (int)Language.English ? attachmentInfo.Title : attachmentInfo.TitleAr;
                        FileUp1.RequiredValidationMessage = LCID == (int)Language.English ? attachmentInfo.RequiredValidationMessage : attachmentInfo.RequiredValidationMessageAr;
                        FileUp1.LookupFieldName = attachmentInfo.LookupFieldName;
                        FileUp1.LookupFieldValue = Convert.ToInt32(requestID);
                        FileUp1.FileExtensionValidation = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileExtensionValidation", (uint)LCID); //"Supported file extensions are jpg,pdf,png";
                        FileUp1.FileNumbersValidationMsg = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileNumbersValidationMsg", (uint)LCID), FileUp1.MaxFileNumber); //"You can't upload more than " + FileUp1.MaxFileNumber + " files";
                        FileUp1.FileExistsValidationMsg = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileExistsValidationMsg", (uint)LCID); //"File exists with the same name";
                        FileUp1.DropClientID = dropFileUpload.Client_ID;
                        FileUp1.TextBoxClientID = dropFileUpload.TextBoxClient_ID;
                        FileUp1.LblTextBoxClientID = dropFileUpload.LblTxtNewOption_ID;
                        FileUp1.ReqDropClientID = lblRequiredDrop.ClientID;
                        FileUp1.ValidqationGroup = "Submit";
                        FileUp1.LabelRequiredDrop = lbldropFileUpload.ClientID;
                        FileUp1.DisplayMode = true;
                        FileUp1.LookupDocumentLibraryName = Utilities.Constants.FileName;
                        
                        FileUp1.DeleteAttachments();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewRequestUserControl.BindAttachments");
            }
        }

        void BindApplicant()
        {
            if (isEmpAsApplicant)
            {
                lbl_QatarID.Visible = false;
                txt_QatarID.Visible = false;
                QatarIDValidator.Enabled = false;
                QatarIDValidator.Visible = false;
                //lbl_QatarIDValidat.Visible = false;
                lblTempQatarID.Visible = true;
                txtTempQatarID.Visible = true;
                lbl_PassPort.Visible = true;
                txt_PassPort.Visible = true;
                valPassport.Visible = true;
                valPassport.Enabled = true;
                ddl_Nationality.Enabled = true;
                ddl_Gender.Enabled = true;
                txtApplicantName.Enabled = true;
                txt_birthDate.Enabled = true;
                valBirthDate.Enabled = true;
                valBirthDate.Visible = true;
                valGender.Enabled = true;
                valGender.Visible = true;
                valName.Enabled = true;
                valName.Visible = true;
                txt_Name.Enabled = true;

            }
        }

        protected void valCertificateType_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (certificateType.SelectedValue == "-1" && ddl_ScholasticLevel.SelectedValue == "13")
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void serverValidateAlevel(object sender, ServerValidateEventArgs e)
        {
            string crtTypeVal = certificateType.SelectedValue;
            List<SCEIB> ibList = JsonConvert.DeserializeObject<List<SCEIB>>(IBList_HF.Value);
            List<SCEIGCSE> AlevelListList = JsonConvert.DeserializeObject<List<SCEIGCSE>>(ALevel_HF.Value);
            if (crtTypeVal == "1" && (AlevelListList == null || AlevelListList.Count < 2))
            {
                e.IsValid = false;
            }
        }

        protected void serverValidateOlevel(object sender, ServerValidateEventArgs e)
        {
            string crtTypeVal = certificateType.SelectedValue;
            List<SCEIGCSE> OlevelListList = JsonConvert.DeserializeObject<List<SCEIGCSE>>(OLevel_HF.Value);
            if (crtTypeVal == "1" && (OlevelListList == null || OlevelListList.Count < 5))
            {
                e.IsValid = false;
            }
        }

        protected void serverValidateIB(object sender, ServerValidateEventArgs e)
        {
            string crtTypeVal = certificateType.SelectedValue;
            if (crtTypeVal == "2")
            {
                int points = 0;
                List<SCEIB> IBList = JsonConvert.DeserializeObject<List<SCEIB>>(IBList_HF.Value);
                foreach (var item in IBList)
                {
                    points = points + Convert.ToInt32(item.Points);
                }
                if (points < 24)
                    e.IsValid = false;
            }
        }

        protected void QatarIDValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(txt_QatarID.Text) || (string.IsNullOrEmpty(hdnBirthDate.Value)&&string.IsNullOrEmpty(txt_birthDate.Text)) || (string.IsNullOrEmpty(hdnStudentName.Value) && string.IsNullOrEmpty(txt_Name.Text)) || (string.IsNullOrEmpty(stdNationality_hf.Value) && ddl_Nationality.SelectedValue=="-1") || (string.IsNullOrEmpty(stdGender_hf.Value) && ddl_Gender.SelectedValue=="-1"))
            {
                args.IsValid = false;
            }
        }

        private Dictionary<string, string> getCertificateYears()
        {
            int StartYear = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SchoolingAcademicStudyStart"));
            int EndYear = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SchoolingAcademicStudyEnd"));
            int nextYear = StartYear + 1;
            Dictionary<string, string> yearsList = new Dictionary<string, string>();
            for (int i = StartYear; i < EndYear; i++)
            {
                nextYear = i + 1;
                yearsList.Add(i + " - " + nextYear, i + " - " + nextYear);
            }
            return yearsList;
        }

        private Dictionary<string, string> getCertificateYearsReverse()
        {
            string sYear = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SchoolingAcademicStudyStart");
            string eYear = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SchoolingAcademicStudyEnd");
            Dictionary<string, string> yearsList = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(sYear) && !string.IsNullOrEmpty(eYear))
            {
                int StartYear = int.Parse(sYear);
                int EndYear = int.Parse(eYear);
                int prevYear = EndYear - 1;
                for (int i = EndYear; i > StartYear; i--)
                {
                    prevYear = i - 1;
                    yearsList.Add(i + " - " + prevYear, i + " - " + prevYear);
                }
            }
            return yearsList;
        }

        protected void NewRequest_Wizard_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter NewRequestUserControl.FinishButton");
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == requestID && r.LoginName == LoginName).FirstOrDefault();
                    request.RequestStatus = ctx.RequestStatus.Where(x => x.Id == Convert.ToInt32(ITWORX.MOEHEWF.Common.Utilities.RequestStatus.SCESubmitted)).FirstOrDefault();
                    CultureInfo english = CultureInfo.GetCultureInfo("en-US");
                    request.SubmitDate = DateTime.Parse(DateTime.Now.ToString(english), CultureInfo.InvariantCulture);
                    request.RecievedDate = DateTime.Parse(DateTime.Now.ToString(english), CultureInfo.InvariantCulture);
                    request.WizardActiveStep = NewRequest_Wizard.ActiveStep.ID;
                    if (isEmpAsApplicant)
                    {
                        request.EmployeeAssignedTo = LoginName;
                        int currentEmpId = SPContext.Current.Web.CurrentUser.ID;
                        SCERequestsAssignmentsListFieldsContentType requestAssign = ctx.SCERequestsAssignmentsList.Where(ra => ra.EmployeeId == currentEmpId).FirstOrDefault();
                        if (requestAssign != null)
                        {
                            if (requestAssign.TasksCount != null)
                            {
                                requestAssign.TasksCount += 1;
                            }
                            else
                            {
                                requestAssign.TasksCount = 1;
                            }
                        }
                    }
                    else
                    {
                        SCERequestsAssignmentsListFieldsContentType requestAssign = ctx.SCERequestsAssignmentsList.Where(ra => ra.IsVacation == false).OrderBy(ra => ra.TasksCount).FirstOrDefault();
                        request.EmployeeAssignedTo = SPContext.Current.Web.SiteUsers.GetByID((int)requestAssign.EmployeeId).LoginName;
                        if (requestAssign != null)
                        {
                            if (requestAssign.TasksCount != null)
                            {
                                requestAssign.TasksCount += 1;
                            }
                            else
                            {
                                requestAssign.TasksCount = 1;
                            }
                        }
                    }
                    ctx.SubmitChanges();
                    Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.SCERequests, Utilities.Constants.SCERequestHistory, LCID, (int)Common.Utilities.RequestStatus.SCESubmitted, SPContext.Current.Web.CurrentUser.Name, string.Empty,requestID.ToString(), "Yes");
                    lblSuccess.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SubmitSucceed", (uint)LCID), requestNumber);
                    modalPopUpConfirmation.Show();
                    SendSMSForSubmition((int)Common.Utilities.RequestStatus.SCESubmitted, SPContext.Current.Site.RootWeb.Url,(int)request.Id);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewRequestUserControl.FinishButton");
            }
        }

        private void SendSMSForSubmition(int status,string RootWebURL,int RequestID)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method PMDecisionSCEUserControl.SendSMS");
                using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                {

                    SCERequestsListFieldsContentType currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(a => a.Id == RequestID).SingleOrDefault();

                    SCENotificationsListFieldsContentType notfications = ctx.SCENotificationsList.ScopeToFolder("", true).Where(y => y.RequestStatusIdId == status && y.Type == NotifcationType.SMS).SingleOrDefault();


                    if (currentRequest != null && notfications != null)
                    {
                        string smsBody = string.Format(notfications.Body, currentRequest.CertificateResourceTitle, currentRequest.RequestNumber, currentRequest.RequestNumber);


                        SCENotifcations.SendSMS(currentRequest.MobileNumber, smsBody);

                    }

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("exit method PMDecisionSCEUserControl.SendSMS");
            }

        }

        protected void btnSaveRequest_Click(object sender, EventArgs e)
        {
            int activeStep = NewRequest_Wizard.ActiveStepIndex;
            if (activeStep == 1)
            {
                if (requestID == null)
                {
                    AddRequest(2);
                }
                else
                {
                    updateRequest(2, (int)requestID);
                }
            }
            else if (activeStep == 2)
            {
                if (requestID == null)
                {
                    AddRequest(3);
                }
                else
                {
                    updateRequest(3, (int)requestID);
                }
            }
            else if (activeStep == 3)
            {
                if (requestID == null)
                {
                    AddRequest(4);
                }
                else
                {
                    updateRequest(4, (int)requestID);
                }
            }
            else if (activeStep == 4)
            {
                updateRequest(5, (int)requestID);
                FileUp1.SaveAttachments();
            }
            lblSaveSuccess.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SaveSucceed", (uint)LCID), requestNumber);
            modalSavePopup.Show();
        }

        protected void btnModalOK_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method SCENewRequestUserControl.btnModalOK_Click");
            try
            {
                if (isEmpAsApplicant)
                {
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEEquivalenceEmployeesDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
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
                Logging.GetInstance().Debug("Exiting method SCENewRequestUserControl.btnModalOK_Click");
            }
        }

        protected void btn_goToMenu_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method SCENewRequestUserControl.btn_goToMenu_Click");
            try
            {
                if (isEmpAsApplicant)
                {
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEEquivalenceEmployeesDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
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
                Logging.GetInstance().Debug("Exiting method SCENewRequestUserControl.btn_goToMenu_Click");
            }
        }

        protected void btnSaveOk_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method SCENewRequestUserControl.btnSaveOk_Click");
            try
            {
                modalSavePopup.Hide();
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/NewSCERequest.aspx?RequestId=" + requestID, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SCENewRequestUserControl.btnSaveOk_Click");
            }
        }

        //protected void serverValidateQatarID(object sender, ServerValidateEventArgs e)
        //{
        //    if ( string.IsNullOrEmpty(lbl_birthDateVal.Text) || string.IsNullOrEmpty(lbl_NameVal.Text) || string.IsNullOrEmpty(lbl_NationalityVal.Text) || string.IsNullOrEmpty(lbl_GenderVal.Text))
        //{
        //        e.IsValid = false;
        //    }
        //}

        public string ConvertDateCalendar(DateTime DateConv, string Calendar, string DateLangCulture)
        {
            System.Globalization.DateTimeFormatInfo DTFormat;
            DateLangCulture = DateLangCulture.ToLower();
            /// We can't have the hijri date writen in English. We will get a runtime error - LAITH - 11/13/2005 1:01:45 PM -
            if (Calendar == "Hijri" && DateLangCulture.StartsWith("en-"))
            {
                DateLangCulture = "ar-sa";
            }
            /// Set the date time format to the given culture - LAITH - 11/13/2005 1:04:22 PM -
            DTFormat = new System.Globalization.CultureInfo(DateLangCulture, false).DateTimeFormat;
            /// Set the calendar property of the date time format to the given calendar - LAITH - 11/13/2005 1:04:52 PM -
            switch (Calendar)
            {
                case "Hijri":
                    DTFormat.Calendar = new System.Globalization.HijriCalendar();
                    break;
                case "Gregorian":
                    DTFormat.Calendar = new System.Globalization.GregorianCalendar();
                    break;
                default:
                    return "";
            }
            /// We format the date structure to whatever we want - LAITH - 11/13/2005 1:05:39 PM -
            DTFormat.ShortDatePattern = "dd/MM/yyyy";
            if (LCID == (int)Language.English)
            {
                return (DateConv.Date.ToString("dd/MM/yyyy", DTFormat));
            }
            else
            {
                return (DateConv.Date.ToString("yyyy/MM/dd", DTFormat));
            }
        }

        public string ToArabicDigits(string input)
        {
            return input.Replace('0', '\u0660')
                    .Replace('1', '\u0661')
                    .Replace('2', '\u0662')
                    .Replace('3', '\u0663')
                    .Replace('4', '\u0664')
                    .Replace('5', '\u0665')
                    .Replace('6', '\u0666')
                    .Replace('7', '\u0667')
                    .Replace('8', '\u0668')
                    .Replace('9', '\u0669');
        }

        public static string LocalizedText2(string resourceName, string resourceKey, uint lcid)
        {
            return SPUtility.GetLocalizedString(string.Format("$Resources:{0},{1}", resourceName, resourceKey), "MOEHE", lcid);
        }

        protected void custIBSubjects_ServerValidate(object source, ServerValidateEventArgs args)
        {

            string crtTypeVal = certificateType.SelectedValue;
            if (crtTypeVal == "2")
            {
                int SLCount = 0;
                int HLCount = 0;
                List<SCEIB> IBList = JsonConvert.DeserializeObject<List<SCEIB>>(IBList_HF.Value);
                foreach (var item in IBList)
                {
                    if (item.Level == "HL")
                        HLCount++;
                    else
                        SLCount++;

                }
                if ((SLCount == 4 && HLCount == 2) || (SLCount == 3 && HLCount == 3))
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
        }

         
            
        
    }
}
