using System;
using System.Web;
using ITWORX.MOEHEWF.SCE.WebParts.EditRequest;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;

namespace ITWORX.MOEHEWF.SCE.Layouts.ITWORX.MOEHEWF.SCE
{
    public partial class Timeout : LayoutsPageBase
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

      

        protected void btnBackHome_Click(object sender, EventArgs e)
        {

            SPUtility.Redirect(SPContext.Current.Site.Url, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
        }
    }
}
