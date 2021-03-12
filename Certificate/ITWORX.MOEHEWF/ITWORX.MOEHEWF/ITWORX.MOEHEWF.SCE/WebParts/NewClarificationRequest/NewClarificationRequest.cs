using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.SCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE;

namespace ITWORX.MOEHEWF.SCE.WebParts.NewClarificationRequest
{
    [ToolboxItemAttribute(false)]
    public class NewClarificationRequest : WebPartBase
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/ITWORX.MOEHEWF.SCE/NewClarificationSCE.ascx";
        protected override void CreateChildControls()
        {
            NewClarificationSCE control = (NewClarificationSCE)Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
