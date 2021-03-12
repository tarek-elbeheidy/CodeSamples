using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace ITWORX.MOEHEWF.UCE.Layouts.ITWORX.MOEHEWF.UCE
{
    public partial class AssistantUndersecretary : LayoutsPageBase
    {
        private const string BASE_PATH = "~/_controltemplates/15/ITWORX.MOEHEWF.UCE/";
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            SPSite TestWeb = SPControl.GetContextSite(Context);
            string strUrl = TestWeb.ServerRelativeUrl + "/_catalogs/masterpage/Home1.master";

            this.MasterPageFile = strUrl;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ActionTaken"] != null)
            {
                if (Session["ActionTaken"].ToString() == "AddNewProcedure")
                    LastLoadedControl = BASE_PATH + "Add_AssistSecretaryProc.ascx";
                else if (Session["ActionTaken"].ToString() == "AddNewAssistantDecision")
                    LastLoadedControl = BASE_PATH + "Add_FinalDecision.ascx";
            }

            LoadUserControl();
        }
        private void LoadUserControl()
        {
            string controlPath = LastLoadedControl;

            if (!string.IsNullOrEmpty(controlPath))
            {
                PlaceHolder_UserControl.Controls.Clear();
                UserControl uc = (UserControl)LoadControl(controlPath);
                uc.ID = "UC_ITWorxEducation";
                PlaceHolder_UserControl.Controls.Add(uc);
            }

        }
    }
}
