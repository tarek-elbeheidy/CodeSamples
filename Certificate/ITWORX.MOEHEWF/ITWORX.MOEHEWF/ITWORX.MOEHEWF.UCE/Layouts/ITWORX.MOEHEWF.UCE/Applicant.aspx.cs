using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace ITWORX.MOEHEWF.UCE.Layouts.ITWORX.MOEHEWF.UCE
{
    public partial class Applicant : LayoutsPageBase
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            SPSite TestWeb = SPControl.GetContextSite(Context);
            string strUrl = TestWeb.ServerRelativeUrl + "/_catalogs/masterpage/Home1.master";

            this.MasterPageFile = strUrl;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
