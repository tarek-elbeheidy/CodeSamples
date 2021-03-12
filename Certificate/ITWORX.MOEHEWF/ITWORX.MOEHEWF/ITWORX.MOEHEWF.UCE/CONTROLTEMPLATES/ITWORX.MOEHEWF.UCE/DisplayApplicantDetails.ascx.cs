using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHE.Utilities;
using Microsoft.SharePoint;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class DisplayApplicantDetails : UserControlBase
    {

        #region ProtectedVariables
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadNationalID;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadPassport;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method DisplayApplicantDetails.Page_Load");
            try
            {

                //This will be removed when session is used
                if (/*Page.Request.QueryString["reqNo"] != null*/ Page.Session["DisplayRequestId"] != null || Page.Session["EditRequestId"] != null)
                {
                    //int requestId = 0;
                    int applicantId = 0;
                    //if (Page.Session["DisplayRequestId"] != null)
                    //{
                    //    requestId = int.Parse(Page.Session["DisplayRequestId"].ToString());
                    //}
                    //else if (Page.Session["EditRequestId"] != null)
                    //{
                    //    requestId = int.Parse(Page.Session["EditRequestId"].ToString());
                    //}
                    //DisplayApplicantData(requestId);

                    if (Page.Session["applicantId"] != null)
                        applicantId = int.Parse(Page.Session["applicantId"].ToString());
                    else
                    {
                        long qatarID = Common.BL.Applicants.GetApplicantQatarIDfromADByLoginName(SPContext.Current.Web.CurrentUser.LoginName);
                        applicantId = Common.BL.Applicants.GetApplicantItemByPersonalID(qatarID.ToString());
                        if (applicantId != 0)
                        {
                            Page.Session["applicantId"] = applicantId;

                        }
                    }
                        DisplayApplicantData();
                    BindAttachments(applicantId);
                    if (HelperMethods.GetGroupUsers(Common.Utilities.Constants.SecretaryGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName))
                    {

                        btn_Print.Visible = true;
                    }
                    else
                        btn_Print.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method DisplayApplicantDetails.DisplayRequestData");
            }
        }

        //private void DisplayApplicantData(int requestId)
        //{
        //    Logging.GetInstance().Debug("Entering method DisplayApplicantDetails.DisplayApplicantData");
        //    try
        //    {
        //        //if (Page.Session["RequestNumber"] !=null)
        //        //{

        //        //int requestNumber =int.Parse(Page.Request.QueryString["reqNo"]);

        //        Entities.Request requestItem = BL.Request.GetRequestByNumber(requestId, LCID);

        //        string yesValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Yes", (uint)LCID);
        //        string noValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "No", (uint)LCID);


        //        if (requestItem != null)
        //        {
        //            Common.Entities.Applicants applicant = Common.BL.Applicants.GetApplicantByID(requestItem.ApplicantID.ID, LCID);
        //            if (applicant != null)
        //            {
        //                lblNameValue.Text = applicant.ApplicantName;
        //                lblPersonalIDValue.Text = applicant.PersonalID;
        //                lblEmailValue.Text = applicant.ApplicantEmail;
        //                lblMobileNumberValue.Text = applicant.MobileNumber;
        //                lblBirthDateValue.Text = (applicant.BirthDate == DateTime.MinValue) ? "" : applicant.BirthDate.QatarFormatedDate();
        //                lblNationalityValue.Text = applicant.Nationality.SelectedTitle;
        //                lblNationalityCategoryValue.Text = applicant.NationalityCategory.SelectedTitle;
        //                lblRegionNoValue.Text = applicant.Region > 0 ? applicant.Region.ToString() : string.Empty;
        //                lblStreetNoValue.Text = applicant.Street > 0 ? applicant.Street.ToString() : string.Empty;
        //                lblPostalNumberValue.Text = applicant.PostalNumber > 0 ? applicant.PostalNumber.ToString() : string.Empty;
        //                lblBuildingNoValue.Text = applicant.BuildingNo > 0 ? applicant.BuildingNo.ToString() : string.Empty;
        //                lblApartmentNoValue.Text = applicant.ApartmentNo > 0 ? applicant.ApartmentNo.ToString() : string.Empty;
        //                lblDetailedAddressValue.Text = applicant.DetailedAddress.ToString();


        //            }

        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        Logging.GetInstance().LogException(ex);
        //    }
        //    finally
        //    {

        //        Logging.GetInstance().Debug("Exiting method DisplayApplicantDetails.DisplayApplicantData");
        //    }
        //}

        private void DisplayApplicantData()
        {
            Logging.GetInstance().Debug("Entering method DisplayApplicantDetails.DisplayApplicantData");
            try
            {
                string yesValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Yes", (uint)LCID);
                string noValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "No", (uint)LCID);

                int applicantId;

                if (Page.Session["applicantId"] != null)
                    applicantId = Convert.ToInt32(Page.Session["applicantId"]);
                else
                {
                    long qatarID = Common.BL.Applicants.GetApplicantQatarIDfromADByLoginName(SPContext.Current.Web.CurrentUser.LoginName);
                    applicantId = Common.BL.Applicants.GetApplicantItemByPersonalID(qatarID.ToString());
                }

                Common.Entities.Applicants applicant = Common.BL.Applicants.GetApplicantByID(applicantId, LCID);
                if (applicant != null)
                {
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
            Logging.GetInstance().Debug("Entering method DisplayApplicantDetails.BindAttachments");
            try
            {
                fileUploadNationalID.DocumentLibraryName = Common.Utilities.Constants.ApplicantsAttachments;
                fileUploadNationalID.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadNationalID.Group = "NationalIDCopy";
                fileUploadNationalID.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NationalIDCopy", (uint)LCID);
                fileUploadNationalID.RequestID = requestId;
                fileUploadNationalID.Enabled = false;
                fileUploadNationalID.LookupFieldName = "ApplicantID";
                fileUploadNationalID.Bind();

                fileUploadPassport.DocumentLibraryName = Common.Utilities.Constants.ApplicantsAttachments;
                fileUploadPassport.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadPassport.Group = "PassportCopy";
                fileUploadPassport.RequestID = requestId;
                fileUploadPassport.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "PassportCopy", (uint)LCID);
                fileUploadPassport.Enabled = false;
                fileUploadPassport.LookupFieldName = "ApplicantID";
                fileUploadPassport.Bind();
            }


            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }

            finally
            {

                Logging.GetInstance().Debug("Exiting method DisplayApplicantDetails.BindAttachments");
            }

        }
    }
}
