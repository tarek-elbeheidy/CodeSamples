using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.SCE.Utilities;
using Microsoft.SharePoint;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.SCE.WebParts.ViewContact
{
    public partial class ViewContactUserControl : UserControlBase
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDisplayRequest();
        }

        void BindDisplayRequest()
        {
            HttpContext backupContext = HttpContext.Current;
            try
            {
              Logging.GetInstance().Debug("Enter ViewContactUserControl.BindDisplayRequest");


                string loginName = SPContext.Current.Web.CurrentUser.LoginName; // if you need it in your code
                string rootWebUrl = SPContext.Current.Site.RootWeb.Url;
                HttpContext.Current = null;

                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (SCEContextDataContext ctx = new SCEContextDataContext(rootWebUrl))// the url of the web is outside the Datcontext
                    {

                        int requestID = 0;
                        if (Page.Request.QueryString["RequestId"] != null)
                        {
                            requestID = Convert.ToInt32(Page.Request.QueryString["RequestId"]);
                        }
                     
                        if (requestID != 0)
                        {
                            SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == requestID /*&& r.LoginName == loginName*/).FirstOrDefault();
                            if (request != null)
                            {

                                lblApplicantNameValue.Text = request.ApplicantOfficialName;
                                lblEmailValue.Text = request.Email;
                                lblMobileNumberValue.Text = request.MobileNumber;

                            }
                        }
                    }
                });
             //   HttpContext.Current = backupContext;

            }
            catch (Exception ex)
                {
                     Logging.GetInstance().LogException(ex);
                }
                finally
                {
                // resetting the SPContext
                if (HttpContext.Current == null)
                {
                    HttpContext.Current = backupContext;
                }
                Logging.GetInstance().Debug("Exit ViewContactUserControl.BindDisplayRequest");
                }

            }
 }
}
