using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class DisplayCheckUniversity : UserControlBase
    {
        #region Protected Variables
        protected string StudyYear = string.Empty;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method DisplayCheckUniversity.Page_Load");
            try
            {
                StudyYear = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "StudyYear");
                //This will be removed when session is used
                if (/*Page.Request.QueryString["reqNo"] != null*/ Page.Session["DisplayRequestId"] != null)
                {
                    DisplayCheckUniversityData();
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method DisplayCheckUniversity.DisplayRequestData");
            }
        }

        private void DisplayCheckUniversityData()
        {
            Logging.GetInstance().Debug("Entering method DisplayCheckUniversity.DisplayCheckUniversityData");
            try
            {
                //if (Page.Session["RequestNumber"] !=null)
                //{

                //int requestNumber =int.Parse(Page.Request.QueryString["reqNo"]);
                int requestId = int.Parse(Convert.ToString(Page.Session["DisplayRequestId"]));

                Entities.Request requestItem = BL.Request.GetRequestByNumber(requestId, LCID);

                string yesValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Yes", (uint)LCID);
                string noValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "No", (uint)LCID);


                if (requestItem != null)
                {
                    lblYearValue.Text = requestItem.Year.ToString();
                    if (requestItem.Year >= int.Parse(StudyYear))
                    {
                        uniValues.Visible = true;
                        lblCountryValue.Text = requestItem.CountryOfStudy.SelectedTitle;
                        lblRequestUniversityValue.Text = requestItem.University.SelectedTitle;
                        lblUniversityListValue.Text = requestItem.UniversityList;
                    }
                    else
                    {
                        uniValues.Visible = false;
                    }
                   
                    
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method DisplayCheckUniversity.DisplayCheckUniversityData");
            }
        }
    }
}
