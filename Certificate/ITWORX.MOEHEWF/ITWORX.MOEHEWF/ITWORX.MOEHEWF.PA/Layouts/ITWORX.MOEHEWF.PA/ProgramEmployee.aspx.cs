using System;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace ITWORX.MOEHEWF.PA.Layouts.ITWORX.MOEHEWF.PA
{
    public partial class ProgramEmployee : LayoutsPageBase
    {
        private const string BASE_PATH = "~/_controltemplates/15/ITWORX.MOEHEWF.PA/";

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
                if (Session["ActionTaken"].ToString() == "AddNewStatement")
                    LastLoadedControl = BASE_PATH + "PAAdd_NewStatement.ascx";

                else if (Session["ActionTaken"].ToString() == "AddNewBook")
                    LastLoadedControl = BASE_PATH + "PAAdd_NewBook.ascx";

                else if (Session["ActionTaken"].ToString() == "AddNewClarification")
                    LastLoadedControl = BASE_PATH + "PAAdd_Clarification.ascx"; 
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
