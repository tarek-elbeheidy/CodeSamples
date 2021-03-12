using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.UCE.WebParts.EquationOfficerProceduresWP
{
    public partial class EquationOfficerProceduresWPUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["_libName"] = "ProceduresDocuments";
                Session["_reqID"] = "4";
                StatementRequestsUC.Visible = false;
                SearchSimilarDecisionsUC.Visible = false;
                SearchStatusRecommendationUC.Visible = false;
            }
        }
    }
}
