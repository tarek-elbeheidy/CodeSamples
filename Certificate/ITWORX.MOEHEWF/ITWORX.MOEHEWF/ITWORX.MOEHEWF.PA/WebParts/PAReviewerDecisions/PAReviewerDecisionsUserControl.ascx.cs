using ITWORX.MOEHE.Utilities.Logging;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHEWF.PA.Utilities;

namespace ITWORX.MOEHEWF.PA.WebParts.PAReviewerDecisions
{
    public partial class PAReviewerDecisionsUserControl : UserControlBase
    {
        private const string BASE_PATH = "~/_controltemplates/15/ITWORX.MOEHEWF.PA/";

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

                if (controlPath.Contains("ViewAssistantUnderSecDec_AllDecision"))
                {
                    ViewAssistantUnderSecDec_AllDecision uc = (ViewAssistantUnderSecDec_AllDecision)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    uc.SPGroupName = Common.Utilities.Constants.AssistUndersecretaryGroupName;
                    PlaceHolder_UserControl.Controls.Add(uc);
                }
               
                else if (controlPath.Contains("ViewTechCommitteeDec_AllDecision"))
                {
                    ViewTechCommitteeDec_AllDecision uc = (ViewTechCommitteeDec_AllDecision)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    uc.SPGroupName = Common.Utilities.Constants.TechnicalCommitteeGroupName;
                    PlaceHolder_UserControl.Controls.Add(uc);
                }
                else if (controlPath.Contains("ViewDeptManagerDec_AllDecision"))
                {
                    ViewDeptManagerDec_AllDecision uc = (ViewDeptManagerDec_AllDecision)LoadControl(controlPath);
                    uc.ID = "UC_ITWorxEducation";
                    uc.SPGroupName = Common.Utilities.Constants.DepartmentManagerGroupName;
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
                    controlPath = BASE_PATH + "ViewAssistantUnderSecDec_AllDecision.ascx";
                    break;
                case "1":
                    controlPath = BASE_PATH + "ViewTechCommitteeDec_AllDecision.ascx";
                    break;
                case "2":
                    controlPath = BASE_PATH + "ViewDeptManagerDec_AllDecision.ascx";
                    break;
                default:
                    controlPath = BASE_PATH + "ViewAssistantUnderSecDec_AllDecision.ascx";
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
                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "PAAssistantUnderSecretaryDecision", (uint)LCID),"0"));
                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "PATechCommitteeDecision", (uint)LCID),"1"));
                Menu_Links.Items.Add(new MenuItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "PAHeadManagerDecision", (uint)LCID),"2"));
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
