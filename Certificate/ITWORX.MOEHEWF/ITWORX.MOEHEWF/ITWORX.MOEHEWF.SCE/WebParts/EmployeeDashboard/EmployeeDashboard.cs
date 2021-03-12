using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.SCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace ITWORX.MOEHEWF.SCE.WebParts.EmployeeDashboard
{
    [ToolboxItemAttribute(false)]
    public class EmployeeDashboard : WebPartBase
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/ITWORX.MOEHEWF.SCE.WebParts/EmployeeDashboard/EmployeeDashboardUserControl.ascx";
        #region Properties

        [Category("MOEHE"),
         Personalizable(PersonalizationScope.Shared),
         WebBrowsable(true),
         WebDisplayName("SharePoint Group Name")
        ]
        public string SPGroupName
        {
            get;
            set;
        }
        #endregion
        protected override void CreateChildControls()
        {
            EmployeeDashboardUserControl control = (EmployeeDashboardUserControl)Page.LoadControl(_ascxPath);
            control.SPGroupName = SPGroupName;
            Controls.Add(control);
        }
    }
}
