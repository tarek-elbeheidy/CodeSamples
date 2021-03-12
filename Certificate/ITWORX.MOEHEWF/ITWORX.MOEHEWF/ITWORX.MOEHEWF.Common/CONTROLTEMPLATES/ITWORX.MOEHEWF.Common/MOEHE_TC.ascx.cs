using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint.Utilities;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common
{

    public partial class MOEHE_TC : UserControlBase
    {
        public int RequestType { set; get; }
        public bool CheckVisibility
        {
            get
            {
                return chkTermsAndConditions.Visible;
            }
            set
            {
                chkTermsAndConditions.Visible = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Entering method  MOEHE_TC.Page_Load");
            try
            {
                if (!Page.IsPostBack)
                {
                    Entities.TermsAndConditions terms = BL.TermsAndConditions.GetTermsConditionsByRequestType(RequestType, (uint)LCID);
                    if (terms != null)
                    {
                        lblTermsAndConditionsText.Text = terms.Description;
                        repTermsAndConditions.DataSource = terms.TermsAttachmentsList;
                        repTermsAndConditions.DataBind();

                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method  MOEHE_TC.Page_Load");
            }
        }



    }
}
