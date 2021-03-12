using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE;
using ITWORX.MOEHEWF.SCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace ITWORX.MOEHEWF.SCE.WebParts.ClarificationRequest
{
    [ToolboxItemAttribute(false)]
    public class ClarificationRequest : WebPartBase
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/ITWORX.MOEHEWF.SCE/ClarificationRequestSCE.ascx";

        protected override void CreateChildControls()
        {
            ClarificationRequestSCE control = (ClarificationRequestSCE)Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
