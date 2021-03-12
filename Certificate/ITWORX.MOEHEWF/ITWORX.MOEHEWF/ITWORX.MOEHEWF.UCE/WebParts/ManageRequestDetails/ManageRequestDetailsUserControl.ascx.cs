using ITWORX.MOEHE.Utilities.Logging;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.UCE.WebParts.ManageRequestDetails
{
    public partial class ManageRequestDetailsUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering ManageRequestDetailsUserControl.Page_Load");
            try
            {
                int requestId = 0;
                if (Page.Session["EditRequestId"] != null)
                {
                    requestId = int.Parse(Page.Session["EditRequestId"].ToString());
                }
                else if (Page.Session["DisplayRequestId"] != null)
                {
                    requestId = int.Parse(Page.Session["DisplayRequestId"].ToString());
                }
                int requestStatusId = Common.BL.Request.GetRequesStaustByRequestNumber(requestId);
                if (requestStatusId>0)
                {
                    if (requestStatusId==(int)Common.Utilities.RequestStatus.UCEProgramEmployeeNeedsClarification 
                        || requestStatusId == (int)Common.Utilities.RequestStatus.UCEReceptionistNeedsClarification
                        || requestStatusId == (int)Common.Utilities.RequestStatus.UCEDraftForClarification)
                    {
                        divDisplay.Visible = false;
                        divEdit.Visible = true;
                      
                    }
                    else
                    {
                        divDisplay.Visible = true;
                        divEdit.Visible = false;

                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit ManageRequestDetailsUserControl.Page_Load");
            }
        }
    }
}
