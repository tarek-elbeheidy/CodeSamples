using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA;

namespace ITWORX.MOEHEWF.PA.WebParts.PAReviewerRecommendations
{
    public partial class PAReviewerRecommendationsUserControl : UserControlBase
    {
        private const string BASE_PATH = "~/_controltemplates/15/ITWORX.MOEHEWF.PA/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MenuItemsText();
                LastLoadedControl = BASE_PATH + "PAViewStatusAndRecommendation.ascx";
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
            if (!string.IsNullOrEmpty(controlPath))
            {

                if (controlPath.Contains("PAViewStatusAndRecommendation"))
                {
                    ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.PAViewStatusAndRecommendation uc = (ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.PAViewStatusAndRecommendation)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation"; 
                    PlaceHolder_UserControl.Controls.Add(uc);
                }

                else if (controlPath.Contains("PAProgramManagerRecommendation"))
                {
                    ProgramManagerRecommendation uc = (ProgramManagerRecommendation)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    uc.viewRecommendation = true;
                    PlaceHolder_UserControl.Controls.Add(uc);
                }
              
            }

        }
        protected void Menu_Links_MenuItemClick(object sender, MenuEventArgs e)
        {
            MenuItem menu = e.Item;

            string controlPath = string.Empty;

            switch (menu.Value)
            {
                case "0":
                    controlPath = BASE_PATH + "PAViewStatusAndRecommendation.ascx";
                    break;
                case "1":
                    controlPath = BASE_PATH + "PAProgramManagerRecommendation.ascx";
                    break;

                default:
                    controlPath = BASE_PATH + "PAViewStatusAndRecommendation.ascx";
                    break;
            }

            LastLoadedControl = controlPath;
            LoadUserControl();
        }
        private void MenuItemsText()
        {
            try
            {
                Logging.GetInstance().Debug("Enter PAMyRequests.BindbtnsCount");
                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ProgEmployeeRecommendation", (uint)LCID), "0"));
                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ProgManagerRecommend", (uint)LCID),"1"));
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit PAMyRequests.BindbtnsCount");
            }
        }
    }
}
