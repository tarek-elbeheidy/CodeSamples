using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE;

namespace ITWORX.MOEHEWF.UCE.WebParts.ReviewerRecommendations
{
    public partial class ReviewerRecommendationsUserControl : UserControlBase
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
            if (!string.IsNullOrEmpty(controlPath))
            {

                if (controlPath.Contains("ViewStatusandRecommendation"))
                {
                    ViewStatusandRecommendation uc = (ViewStatusandRecommendation)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation"; 
                    PlaceHolder_UserControl.Controls.Add(uc);
                }

                else if (controlPath.Contains("ProgramManagerRecommendation"))
                {
                    ProgramManagerRecommendation uc = (ProgramManagerRecommendation)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    uc.viewRecommendation = true;
                    PlaceHolder_UserControl.Controls.Add(uc);
                }
              
            }
            else if (controlPath == null)
            {

                ViewStatusandRecommendation uc = (ViewStatusandRecommendation)LoadControl(BASE_PATH + "ViewStatusandRecommendation.ascx");
                uc.ID = "UC_ITWorxEducation";
                PlaceHolder_UserControl.Controls.Add(uc);
            }

        }
        protected void Menu_Links_MenuItemClick(object sender, MenuEventArgs e)
        {
            MenuItem menu = e.Item;

            string controlPath = string.Empty;

            switch (menu.Value)
            {
                case "0":
                    controlPath = BASE_PATH + "ViewStatusandRecommendation.ascx";
                    break;
                case "1":
                    controlPath = BASE_PATH + "ProgramManagerRecommendation.ascx";
                    break;

                default:
                    controlPath = BASE_PATH + "ViewStatusandRecommendation.ascx";
                    break;
            }

            LastLoadedControl = controlPath;
            LoadUserControl();
        }
        private void MenuItemsText()
        {
            try
            {
                Logging.GetInstance().Debug("Enter MyRequests.BindbtnsCount");
                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ProgEmployeeRecommendation", (uint)LCID), "0"));
                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ProgManagerRecommend", (uint)LCID),"1"));
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
