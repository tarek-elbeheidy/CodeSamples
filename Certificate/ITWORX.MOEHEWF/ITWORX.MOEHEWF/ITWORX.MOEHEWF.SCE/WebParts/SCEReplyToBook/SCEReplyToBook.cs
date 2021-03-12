using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.SCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace ITWORX.MOEHEWF.SCE.WebParts.SCEReplyToBook
{
    [ToolboxItemAttribute(false)]
    public class SCEReplyToBook : WebPartBase
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/ITWORX.MOEHEWF.SCE.WebParts/SCEReplyToBook/SCEReplyToBookUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
