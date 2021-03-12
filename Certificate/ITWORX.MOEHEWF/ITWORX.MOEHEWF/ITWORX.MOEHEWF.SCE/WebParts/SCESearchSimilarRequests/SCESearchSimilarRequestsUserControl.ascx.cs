using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.SCE.WebParts.SCESearchSimilarRequests
{
    public partial class SCESearchSimilarRequestsUserControl : UserControlBase
    {
        private const string BASE_PATH = "~/_controltemplates/15/ITWORX.MOEHEWF.SCE/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MenuItemsText();
            }
            LoadUserControl();
        }
        private void MenuItemsText()
        {
            try
            {
                Logging.GetInstance().Debug("Enter MyRequests.BindbtnsCount");
                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SearchSimilarReqs", (uint)LCID), "0"));
                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ApprovedSimilarReqs", (uint)LCID), "1"));
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

                if (controlPath.Contains("SearchSimilarRequestsSCE"))
                {
                    SearchSimilarRequestsSCE uc = (SearchSimilarRequestsSCE)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    PlaceHolder_UserControl.Controls.Add(uc);
                }

                else if (controlPath.Contains("ApprovedSimilarRequestsSCE"))
                {
                    ApprovedSimilarRequestsSCE uc = (ApprovedSimilarRequestsSCE)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                  //  uc.viewRecommendation = true;
                    PlaceHolder_UserControl.Controls.Add(uc);
                }

            }
            else if (controlPath == null)
            {

                SearchSimilarRequestsSCE uc = (SearchSimilarRequestsSCE)LoadControl(BASE_PATH + "SearchSimilarRequestsSCE.ascx");
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
                    controlPath = BASE_PATH + "SearchSimilarRequestsSCE.ascx";
                    break;
                case "1":
                    controlPath = BASE_PATH + "ApprovedSimilarRequestsSCE.ascx";
                    break;

                default:
                    controlPath = BASE_PATH + "SearchSimilarRequestsSCE.ascx";
                    break;
            }

            LastLoadedControl = controlPath;
            LoadUserControl();
        }
    }
}
