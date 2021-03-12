using ITWORX.MOEHE.Utilities.Logging;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.PA.WebParts.PAManageRequestDetails
{
    public partial class PAManageRequestDetailsUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering PAManageRequestDetailsUserControl.Page_Load");
            try
            {
                int requestId = 0;
                if (Page.Session["PAEditRequestId"] != null)
                {
                    requestId = int.Parse(Page.Session["PAEditRequestId"].ToString());
                }
                else if (Page.Session["PADisplayRequestId"] != null)
                {
                    requestId = int.Parse(Page.Session["PADisplayRequestId"].ToString());
                }
                int requestStatusId = Common.BL.Request.GetPARequesStaustByRequestNumber(requestId);
                if (requestStatusId > 0)
                {
                    if (requestStatusId == (int)Common.Utilities.RequestStatus.PAEmployeeNeedsClarification
                        || requestStatusId == (int)Common.Utilities.RequestStatus.PADraftForClarification)
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

                Logging.GetInstance().Debug("Exit PAManageRequestDetailsUserControl.Page_Load");
            }
        }
        }
}
