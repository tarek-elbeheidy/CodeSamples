using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class ApplicantDetails : UserControlBase
    {
        #region ProtectedVariables

        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadNationalID;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadPassport;

        #endregion ProtectedVariables

        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering ApplicantDetails.Page_Load");
            try
            {
                if (!Page.IsPostBack)
                {
                    long qatarID = Common.BL.Applicants.GetApplicantQatarIDfromADByLoginName(SPContext.Current.Web.CurrentUser.LoginName);
                    int applicantId = Common.BL.Applicants.GetApplicantItemByPersonalID(qatarID.ToString());
                    if (applicantId != 0)
                    {
                        Page.Session["applicantId"] = applicantId;
                        GetApplicantDataForEditMode();
                    }

                }

                int requestID = 0;
                if (Page.Session["NewRequestId"] != null)
                {
                    requestID = Convert.ToInt32(Page.Session["NewRequestId"]); 
                }
                else if (Page.Session["EditRequestId"] != null)
                {
                    requestID = Convert.ToInt32(Page.Session["EditRequestId"]); 
                }
                if (Page.Session["PANewRequestId"] != null)
                {
                    requestID = Convert.ToInt32(Page.Session["PANewRequestId"]); 
                }
                else if (Page.Session["PAEditRequestId"] != null)
                {
                    requestID = Convert.ToInt32(Page.Session["PAEditRequestId"]); 
                }

                BindApplicantDetails();

                BindAttachments();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ApplicantDetails.Page_Load");
            }
        }

        private void BindApplicantDetails()
        {
            Logging.GetInstance().Debug("Entering ApplicantDetails.BindApplicantDetails");
            Applicants applicant = null;
            try
            {
                if (Session["applicantId"] != null && Convert.ToInt32(Page.Session["applicantId"]) != 0)
                {
                    applicant = Common.BL.Applicants.GetApplicantByID(Convert.ToInt32(Page.Session["applicantId"]), LCID);
                }
                else
                {
                    applicant = Common.BL.Applicants.GetApplicantProfilefromADByLoginName(SPContext.Current.Web.CurrentUser.LoginName);
                }

                if (applicant != null)
                {
                    lblApplicantNameValue.Text = LCID == (int)Language.English ?applicant.EnglishName:applicant.ArabicName;
                    lblPersonalIDValue.Text = applicant.PersonalID;
                    lblBirthDateValue.Text = (applicant.BirthDate == DateTime.MinValue) ? "" : applicant.BirthDate.QatarFormatedDate();
                    lblNationalityValue.Text = applicant.Nationality != null ? applicant.Nationality.SelectedTitle : null;
                    lblNationalityCategoryValue.Text = applicant.NationalityCategory != null ? applicant.NationalityCategory.SelectedTitle : null;
                    lblMobileNumberValue.Text = applicant.MobileNumber;
                    lblEmailValue.Text = applicant.ApplicantEmail;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ApplicantDetails.BindApplicantDetails");
            }
        }

        private void GetApplicantDataForEditMode()
        {
            Logging.GetInstance().Debug("Entering ApplicantDetails.GetApplicantDataForEditMode");
            try
            {
                //int requestId = int.Parse(Convert.ToString(Page.Session["EditRequestId"]));
                //Entities.Request requestItem = BL.Request.GetRequestByNumber(requestId, LCID);
                //Applicants applicant = Common.BL.Applicants.GetApplicantByID(requestItem.ApplicantID.ID, LCID);
                Applicants applicant = Common.BL.Applicants.GetApplicantByID(Convert.ToInt32(Page.Session["applicantId"]), LCID);

                if (applicant != null)
                {
                    txtRegionNo.Text = applicant.Region > 0 ? applicant.Region.ToString() : string.Empty;
                    txtStreetNo.Text = applicant.Street > 0 ? applicant.Street.ToString() : string.Empty;
                    txtPostalNumber.Text = applicant.PostalNumber > 0 ? applicant.PostalNumber.ToString() : string.Empty;
                    txtBuildingNo.Text = applicant.BuildingNo > 0 ? applicant.BuildingNo.ToString() : string.Empty;
                    txtApartmentNo.Text = applicant.ApartmentNo > 0 ? applicant.ApartmentNo.ToString() : string.Empty;
                    txtDetailedAddress.Text = applicant.DetailedAddress.ToString();
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ApplicantDetails.GetApplicantDataForEditMode");
            }
        }

        public int CreateUserDataObject(Applicants applicants)
        {
            Logging.GetInstance().Debug("Enter ApplicantDetails.CreateUserDataObject");
            int userId = 0;
            try
            {
                // UserControl ApplicantDetails = wizardNewRequest.FindControl("ApplicantDetails") as UserControl;

                //applicants.ApplicantName = (ApplicantDetails.FindControl("lblName") as Label).Text;
                //applicants.PersonalID = (ApplicantDetails.FindControl("lblPersonalIDValue") as Label).Text;
                //applicants.MobileNumber = (ApplicantDetails.FindControl("lblMobileNumberValue") as Label).Text;
                //applicants.ApplicantEmail = (ApplicantDetails.FindControl("lblEmailValue") as Label).Text;

                //applicants.Nationality = new Common.Entities.Nationality()
                //{
                //    SelectedID = "1",
                //    SelectedTitle = (ApplicantDetails.FindControl("lblNationalityValue") as Label).Text
                //};

                if (applicants != null)
                {
                    //if (!string.IsNullOrEmpty(txtRegionNo.Text)
                    //&& !string.IsNullOrEmpty(txtStreetNo.Text)
                    //&& !string.IsNullOrEmpty(txtBuildingNo.Text)
                    //&& !string.IsNullOrEmpty(txtApartmentNo.Text))

                    //{
                    applicants.Region = !string.IsNullOrEmpty(txtRegionNo.Text) ? int.Parse(txtRegionNo.Text) : 0;
                    applicants.Street = !string.IsNullOrEmpty(txtStreetNo.Text) ? int.Parse(txtStreetNo.Text) : 0;
                    applicants.BuildingNo = !string.IsNullOrEmpty(txtBuildingNo.Text) ? int.Parse(txtBuildingNo.Text) : 0;
                    applicants.ApartmentNo = !string.IsNullOrEmpty(txtApartmentNo.Text) ? int.Parse(txtApartmentNo.Text) : 0;
                    applicants.PostalNumber = !string.IsNullOrEmpty(txtPostalNumber.Text) ? int.Parse(txtPostalNumber.Text) : 0;
                    applicants.DetailedAddress = !string.IsNullOrEmpty(txtDetailedAddress.Text) ? txtDetailedAddress.Text : string.Empty;
                    //}
                    //else if (!string.IsNullOrEmpty(txtDetailedAddress.Text))
                    //{
                    //applicants.Region = 0;
                    //applicants.Street = 0;
                    //applicants.BuildingNo = 0;
                    //applicants.ApartmentNo = 0;
                    //applicants.DetailedAddress = !string.IsNullOrEmpty(txtDetailedAddress.Text) ? txtDetailedAddress.Text : string.Empty;
                    //}
                    userId = Common.BL.Applicants.AddApplicant(applicants, applicants.ID);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ApplicantDetails.CreateUserDataObject");
            }

            return userId;
        }

        public bool SaveAttachments()
        {
            Logging.GetInstance().Debug("Entering method ApplicantDetails.SaveAttachments");
            bool saved = false;
            try
            {
                bool fileNational = fileUploadNationalID.SaveAttachments();
                bool filePassport = fileUploadPassport.SaveAttachments(); 

                if (fileNational && filePassport)
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
                Logging.GetInstance().Debug("Exiting method ApplicantDetails.SaveAttachments");
            }
            return saved;
        }

        protected void custValidateTextBoxes_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if ((/*!string.IsNullOrEmpty(txtApartmentNo.Text)
                &&*/!string.IsNullOrEmpty(txtBuildingNo.Text)
                && !string.IsNullOrEmpty(txtRegionNo.Text)
                && !string.IsNullOrEmpty(txtStreetNo.Text))
                || !string.IsNullOrEmpty(txtDetailedAddress.Text))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        public void BindAttachments()
        {
            Logging.GetInstance().Debug("Entering method ApplicantDetails.BindAttachments");
            try
            {
                if (Page.Session["NewRequestId"] != null
                    || Page.Session["EditRequestId"] != null
                    || Page.Session["PANewRequestId"] != null
                    || Page.Session["PAEditRequestId"] != null)
                {
                    int requestId = 0;
                    int applicantId = 0;
                    if ((SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditRequestPage) || SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.NeedsClarEditRequestPage))
                        && Page.Session["EditRequestId"] != null)
                    {
                        requestId = int.Parse(Page.Session["EditRequestId"].ToString());
                    }
                    else if (Page.Session["NewRequestId"] != null)
                    {
                        requestId = int.Parse(Page.Session["NewRequestId"].ToString());
                    }
                    else if ((SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.EditPARequestPage) || SPContext.Current.File.Url.ToLower().Equals(Utilities.Constants.PANeedsClarEditRequestPage))
                        && Page.Session["PAEditRequestId"] != null)
                    {
                        requestId = int.Parse(Page.Session["PAEditRequestId"].ToString());
                    }
                    else if (Page.Session["PANewRequestId"] != null)
                    {
                        requestId = int.Parse(Page.Session["PANewRequestId"].ToString());
                    }

                    if (Page.Session["applicantId"] != null)
                        applicantId = int.Parse(Page.Session["applicantId"].ToString());
                    else
                    {
                        long qatarID = Common.BL.Applicants.GetApplicantQatarIDfromADByLoginName(SPContext.Current.Web.CurrentUser.LoginName);
                        applicantId = Common.BL.Applicants.GetApplicantItemByPersonalID(qatarID.ToString());
                    }

                    fileUploadNationalID.DocumentLibraryName = Common.Utilities.Constants.ApplicantsAttachments;
                    fileUploadNationalID.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadNationalID.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NationalIDCopy", (uint)LCID);
                    fileUploadNationalID.MaxFileNumber = 1;
                    fileUploadNationalID.Group = "NationalIDCopy";
                    fileUploadNationalID.RequestID = applicantId;
                    fileUploadNationalID.IsRequired = true;
                    fileUploadNationalID.RequiredValidationMessage = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "RequiredNationalID", (uint)LCID);
                    fileUploadNationalID.ValidationGroup = "Submit";
                    fileUploadNationalID.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadNationalID.Enabled = true;
                    fileUploadNationalID.LookupFieldName = "ApplicantID";
                    fileUploadNationalID.Bind();

                    fileUploadPassport.DocumentLibraryName = Common.Utilities.Constants.ApplicantsAttachments;
                    fileUploadPassport.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileUploadPassport.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "PassportCopy", (uint)LCID);
                    fileUploadPassport.MaxFileNumber = 1;
                    fileUploadPassport.Group = "PassportCopy";
                    fileUploadPassport.RequestID = applicantId;
                    fileUploadPassport.IsRequired = false;
                    fileUploadPassport.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                    fileUploadPassport.Enabled = true;
                    fileUploadPassport.LookupFieldName = "ApplicantID";
                    fileUploadPassport.Bind();
                    if (HttpContext.Current.Request.Url.AbsoluteUri.Contains("NewPARequest"))
                    {
                        fileUploadPassport.SupportedExtensions = "PNG,PDF,JPG";
                        fileUploadPassport.MaxSize = 10240000;
                        fileUploadNationalID.SupportedExtensions = "PNG,PDF,JPG";
                        fileUploadNationalID.MaxSize = 10240000;
                         
                    }
                    else
                    {
                        fileUploadPassport.MaxSize = 7168000;
                        fileUploadPassport.SupportedExtensions = "PNG,PDF,JPG";
                        fileUploadNationalID.SupportedExtensions = "PNG,PDF,JPG";
                        fileUploadNationalID.MaxSize = 7168000; 
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method ApplicantDetails.BindAttachments");
            }
        } 
    }
}