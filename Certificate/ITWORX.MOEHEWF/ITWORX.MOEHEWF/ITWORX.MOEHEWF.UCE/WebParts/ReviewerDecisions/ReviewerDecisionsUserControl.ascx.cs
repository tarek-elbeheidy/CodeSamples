using ITWORX.MOEHE.Utilities.Logging;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHEWF.UCE.Utilities;

namespace ITWORX.MOEHEWF.UCE.WebParts.ReviewerDecisions
{
    public partial class ReviewerDecisionsUserControl : UserControlBase
    {
        private const string BASE_PATH = "~/_controltemplates/15/ITWORX.MOEHEWF.UCE/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MenuItemsText();
            }
            LoadUserControl();
        }
        private string LastLoadedControl
        {
            get
            {
                return ViewState["LastLoaded"] as string;
            }
            set
            {
                ViewState["LastLoaded"] = value;
            }
        }
        private void LoadUserControl()
        {
            string controlPath = LastLoadedControl;
            PlaceHolder_UserControl.Controls.Clear();
 
            ViewDeptManagerDec_AllDecision uc = (ViewDeptManagerDec_AllDecision)LoadControl(BASE_PATH + "ViewDeptManagerDec_AllDecision.ascx");
            uc.ID = "UC_ITWorxEducation";
            uc.SPGroupName = Common.Utilities.Constants.DepartmentManagerGroupName;
            PlaceHolder_UserControl.Controls.Add(uc);

        }
        protected void Menu_Links_MenuItemClick(object sender, MenuEventArgs e)
        {
            MenuItem menu = e.Item;

            string controlPath = string.Empty;

            controlPath = BASE_PATH + "ViewDeptManagerDec_AllDecision.ascx";
            LastLoadedControl = controlPath;
            LoadUserControl();
        }
        private void MenuItemsText()
        {
            try
            {
                Logging.GetInstance().Debug("Enter MyRequests.BindbtnsCount");

                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "HeadManagerDecision", (uint)LCID),"2"));
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit MyRequests.BindbtnsCount");
            }
        }


    }
}
