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

namespace ITWORX.MOEHEWF.SCE.WebParts.SCENavigationLinks
{
    [ToolboxItemAttribute(false)]
    public class SCENavigationLinks : WebPartBase
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/ITWORX.MOEHEWF.SCE/NavigationLinksSCE.ascx";

        protected override void CreateChildControls()
        {
            NavigationLinksSCE control = (NavigationLinksSCE)Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
