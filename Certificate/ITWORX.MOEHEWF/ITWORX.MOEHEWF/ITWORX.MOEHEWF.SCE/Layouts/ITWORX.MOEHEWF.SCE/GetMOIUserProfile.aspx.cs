using System;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Newtonsoft.Json;

namespace ITWORX.MOEHEWF.SCE.Layouts.ITWORX.MOEHEWF.SCE
{
    public partial class GetMOIUserProfile : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering NewRequestUserControl.Page_Load");

                if (Request.QueryString["QatarId"] != null)
                {
                    MOIUserProfile moIuserProfile = CallingIdentityApiHelper.GetMOIUserProfile(Request.QueryString["QatarId"].ToString());

                    Response.Write(JsonConvert.SerializeObject(moIuserProfile));
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit GetMOIUserProfile.Page_Load");
            }
        }
    }
}
