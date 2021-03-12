using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHE.Utilities;
using Microsoft.SharePoint;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class PAReviewerDisplayApplicantDetails : UserControlBase
    {
        #region ProtectedVariables
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadNationalID;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadPassport;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadNationalService;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method DisplayApplicantDetails.Page_Load");
            try
            {

                //This will be removed when session is used
                if (/*Page.Request.QueryString["reqNo"] != null*/ Page.Session["PADisplayRequestId"] != null)
                {
                    int requestId = int.Parse(Convert.ToString(Page.Session["PADisplayRequestId"]));

                    Entities.PARequest requestItem = BL.Request.GetApplicantFromRequest(requestId, LCID);

                    string yesValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Yes", (uint)LCID);
                    string noValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "No", (uint)LCID);


                    if (requestItem != null)
                    {
                        Common.Entities.Applicants applicant = Common.BL.Applicants.GetApplicantByID(requestItem.ApplicantID.ID, LCID);
                        if (applicant != null)
                        {
                            DisplayApplicantData(applicant);
                            BindAttachments(requestItem.ApplicantID.ID);
                        }
                    }


                  //bool userInGroup =   HelperMethods.InGroup(Common.Utilities.Constants.SecretaryGroupName);
                
                //  if(userInGroup)
                //    {

                //        btn_Print.Visible = true;
                //    }
                //    else
                //        btn_Print.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method DisplayApplicantDetails.PADisplayRequestData");
            }
        }

        private void DisplayApplicantData(Common.Entities.Applicants applicant)
        {
            Logging.GetInstance().Debug("Entering method DisplayApplicantDetails.DisplayApplicantData");
            try
            {
                //if (Page.Session["RequestNumber"] !=null)
                //{

                //int requestNumber =int.Parse(Page.Request.QueryString["reqNo"]);

                        lblNameValue.Text = LCID == (int)Language.English ? applicant.EnglishName : applicant.ArabicName;
                        lblPersonalIDValue.Text = applicant.PersonalID;
                        lblEmailValue.Text = applicant.ApplicantEmail;
                        lblMobileNumberValue.Text = applicant.MobileNumber;
                        lblBirthDateValue.Text = (applicant.BirthDate == DateTime.MinValue) ? "" : applicant.BirthDate.QatarFormatedDate();
                        lblNationalityValue.Text = applicant.Nationality.SelectedTitle;
                        lblNationalityCategoryValue.Text = applicant.NationalityCategory.SelectedTitle;
                        lblRegionNoValue.Text = applicant.Region > 0 ? applicant.Region.ToString() : string.Empty;
                        lblStreetNoValue.Text = applicant.Street > 0 ? applicant.Street.ToString() : string.Empty;
                        lblPostalNumberValue.Text = applicant.PostalNumber > 0 ? applicant.PostalNumber.ToString() : string.Empty;
                        lblBuildingNoValue.Text = applicant.BuildingNo > 0 ? applicant.BuildingNo.ToString() : string.Empty;
                        lblApartmentNoValue.Text = applicant.ApartmentNo > 0 ? applicant.ApartmentNo.ToString() : string.Empty;
                        lblDetailedAddressValue.Text = applicant.DetailedAddress.ToString();
                 
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method DisplayApplicantDetails.DisplayApplicantData");
            }
        }

        private void BindAttachments(int requestId)
        {
            Logging.GetInstance().Debug("Entering method ReviewerDisplayApplicantDetails.BindAttachments");
            try
            {
                //int requestId = int.Parse(Convert.ToString(Page.Session["DisplayRequestId"]));
                fileUploadNationalID.DocumentLibraryName = Common.Utilities.Constants.ApplicantsAttachments;
                fileUploadNationalID.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadNationalID.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NationalIDCopy", (uint)LCID);
                fileUploadNationalID.Group = "NationalIDCopy";
                fileUploadNationalID.RequestID = requestId;
                fileUploadNationalID.Enabled = false;
                fileUploadNationalID.LookupFieldName = "ApplicantID";
                fileUploadNationalID.Bind();

                fileUploadPassport.DocumentLibraryName = Common.Utilities.Constants.ApplicantsAttachments;
                fileUploadPassport.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadPassport.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "PassportCopy", (uint)LCID);
                fileUploadPassport.Group = "PassportCopy";
                fileUploadPassport.RequestID = requestId;
                fileUploadPassport.Enabled = false;
                fileUploadPassport.LookupFieldName = "ApplicantID";
                fileUploadPassport.Bind();

                fileUploadNationalService.DocumentLibraryName = Common.Utilities.Constants.ApplicantsAttachments;
                fileUploadNationalService.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadNationalService.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NationalServiceIDCopy", (uint)LCID);
                fileUploadNationalService.Group = "NationalServiceIDCopy";
                fileUploadNationalService.RequestID = requestId;
                fileUploadNationalService.Enabled = false;
                fileUploadNationalService.LookupFieldName = "ApplicantID";
                fileUploadNationalService.Bind();
            }


            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }

            finally
            {

                Logging.GetInstance().Debug("Exiting method ReviewerDisplayApplicantDetails.BindAttachments");
            }

        }
    }
}
