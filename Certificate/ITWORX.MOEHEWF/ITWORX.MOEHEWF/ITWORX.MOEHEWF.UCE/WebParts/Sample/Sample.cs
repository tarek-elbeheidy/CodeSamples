using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;

namespace ITWORX.MOEHEWF.UCE.WebParts.Sample
{
    [ToolboxItemAttribute(false)]
    public class Sample : WebPartBase
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/ITWORX.MOEHEWF.UCE.WebParts/Sample/SampleUserControl.ascx";

        protected override void CreateChildControls()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Sample.CreateChildControls");
                // function logic goes here
                Control control = Page.LoadControl(_ascxPath);
                Controls.Add(control);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method Sample.CreateChildControls");
            }



           // SPUtility.Redirect("/myurl.aspx", SPRedirectFlags.DoNotEndResponse, System.Web.HttpContext.Current);
            //try
            //{
            //    SPContext.Current.Web.AllowUnsafeUpdates = true;
            //    // State-changing operation occurs here.
            //}
            //catch
            //{
            //    // Handle or re-throw an exception.
            //}
            //finally
            //{
            //    SPContext.Current.Web.AllowUnsafeUpdates = false;
            //}
        }
    }
}
