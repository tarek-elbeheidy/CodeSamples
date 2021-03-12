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
    public partial class DisplayApplicantDetails : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method DisplayApplicantDetails.Page_Load");
            try
            {
             
                //This will be removed when session is used
                if (/*Page.Request.QueryString["reqNo"] != null*/ Page.Session["PADisplayRequestId"] != null)
                {
                    DisplayApplicantData();
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

                Logging.GetInstance().Debug("Exiting method DisplayApplicantDetails.PADisplayRequestData");
            }
        }
      
            private void DisplayApplicantData()
            {
                Logging.GetInstance().Debug("Entering method DisplayApplicantDetails.DisplayApplicantData");
                try
                {
                    //if (Page.Session["RequestNumber"] !=null)
                    //{

                    //int requestNumber =int.Parse(Page.Request.QueryString["reqNo"]);
                    int requestId = int.Parse(Convert.ToString(Page.Session["PADisplayRequestId"]));

                    Entities.PARequest requestItem = BL.Request.GetRequestByNumber(requestId, LCID);

                    string yesValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Yes", (uint)LCID);
                    string noValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "No", (uint)LCID);


                    if (requestItem != null)
                    {
                       Common.Entities.Applicants applicant = Common.BL.Applicants.GetApplicantByID(requestItem.ApplicantID.ID, LCID);
                        if (applicant != null)
                        {
                            //lblName.Text = applicant.ApplicantName;
                            //lblPersonalIDValue.Text = applicant.PersonalID;
                            //lblEmailValue.Text = applicant.ApplicantEmail;
                            //lblNationalityValue.Text = applicant.Nationality.SelectedTitle;
                        lblRegionNoValue.Text = applicant.Region > 0 ? applicant.Region.ToString() : string.Empty;
                        lblStreetNoValue.Text = applicant.Street > 0 ? applicant.Street.ToString() : string.Empty;
                        lblBuildingNoValue.Text = applicant.BuildingNo > 0 ? applicant.BuildingNo.ToString() : string.Empty;
                        lblApartmentNoValue.Text = applicant.ApartmentNo > 0 ? applicant.ApartmentNo.ToString() : string.Empty;
                        lblDetailedAddressValue.Text = applicant.DetailedAddress.ToString();


                    }

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
        }
}
