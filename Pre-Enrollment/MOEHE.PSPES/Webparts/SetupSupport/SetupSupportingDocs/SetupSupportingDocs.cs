using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace MOEHE.PSPES.Webparts.SetupSupport.SetupSupportingDocs
{
    [ToolboxItemAttribute(false)]
    public class SetupSupportingDocs : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/MOEHE.PSPES.Webparts.SetupSupport/SetupSupportingDocs/SetupSupportingDocsUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
