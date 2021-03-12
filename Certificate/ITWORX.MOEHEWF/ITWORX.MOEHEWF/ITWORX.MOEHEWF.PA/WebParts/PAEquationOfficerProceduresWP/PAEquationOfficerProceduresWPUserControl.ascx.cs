using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.PA.WebParts.PAEquationOfficerProceduresWP
{
    public partial class PAEquationOfficerProceduresWPUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["_libName"] = "ProceduresDocuments";
                Session["_reqID"] = "4";
                PAStatementRequestsUC.Visible = false;
                SearchSimilarDecisionsUC.Visible = false;
                PASearchStatusRecommendationUC.Visible = false;
            }
        }
    }
}
