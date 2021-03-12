using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.UCE.WebParts.TestWebPart
{
    public partial class TestWebPartUserControl : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //    UserControl uc = (UserControl)Page.LoadControl("~/_controltemplates/15/ITWORX.MOEHEWF.UCE/NewRequests.ascx");
            //    ViewState["controlname"] = "NewRequests.ascx";
            //    this.PlaceHolder1.Controls.Clear();
            //    this.PlaceHolder1.Controls.Add(uc);
            //    BindbtnsCount();
            //}
            //if (IsPostBack)
            //{
            //    string controlName = string.Empty;
            //    if (null != ViewState["controlname"])
            //    {
            //        controlName = ViewState["controlname"].ToString();
            //        UserControl uc = (UserControl)LoadControl(controlName);
            //        this.PlaceHolder1.Controls.Add(uc);
            //    }
            //}
        }
        //private void BindbtnsCount()
        //{
        //    try
        //    {
        //        Logging.GetInstance().Debug("Enter MyRequests.BindbtnsCount");
        //        lnkbtn_NewRequests.Text = HelperMethods.LocalizedText("ITWORX.MOEHEWF.UCE", "NewRequests", (uint)LCID) + "(" + BL.SearchSimilarRequests.GetAllRequestsbyStatus(LCID, "NewRequest").Count.ToString() + ")";
        //        lnkbtn_ClarRequests.Text = HelperMethods.LocalizedText("ITWORX.MOEHEWF.UCE", "ClarificationRequests", (uint)LCID) + "(" + BL.SearchSimilarRequests.GetAllRequestsbyStatus(LCID, "ClarRequest").Count.ToString() + ")";
        //        lnkbtn_RejectedRequests.Text = HelperMethods.LocalizedText("ITWORX.MOEHEWF.UCE", "RejectedRequests", (uint)LCID) + "(" + BL.SearchSimilarRequests.GetAllRequestsbyStatus(LCID, "RejectedRequest").Count.ToString() + ")";
        //        lnkbtn_LateRequests.Text = HelperMethods.LocalizedText("ITWORX.MOEHEWF.UCE", "LateRequests", (uint)LCID) + "(" + BL.SearchSimilarRequests.GetAllRequestsbyStatus(LCID, "LateRequests").Count.ToString() + ")";
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.GetInstance().LogException(ex);
        //    }
        //    finally
        //    {

        //        Logging.GetInstance().Debug("Exit MyRequests.BindbtnsCount");
        //    }
        //}

        //protected void lnkbtn_NewRequests_Click(object sender, EventArgs e)
        //{
        //    UserControl uc = (UserControl)Page.LoadControl("~/_controltemplates/15/ITWORX.MOEHEWF.UCE/NewRequests.ascx");
        //    ViewState["controlname"] = "NewRequests.ascx";
        //    this.PlaceHolder1.Controls.Clear();
        //    this.PlaceHolder1.Controls.Add(uc);
        //}

        //protected void lnkbtn_ClarRequests_Click(object sender, EventArgs e)
        //{
        //    UserControl uc = (UserControl)Page.LoadControl("~/_controltemplates/15/ITWORX.MOEHEWF.UCE/ClarificationRequests.ascx");
        //    ViewState["controlname"] = "ClarificationRequests.ascx";
        //    this.PlaceHolder1.Controls.Clear();
        //    this.PlaceHolder1.Controls.Add(uc);
        //}

        //protected void lnkbtn_RejectedRequests_Click(object sender, EventArgs e)
        //{
        //    UserControl uc = (UserControl)Page.LoadControl("~/_controltemplates/15/ITWORX.MOEHEWF.UCE/RejectedRequests.ascx");
        //    ViewState["controlname"] = "RejectedRequests.ascx";
        //    this.PlaceHolder1.Controls.Clear();
        //    this.PlaceHolder1.Controls.Add(uc);
        //}

        //protected void lnkbtn_LateRequests_Click(object sender, EventArgs e)
        //{
        //    UserControl uc = (UserControl)Page.LoadControl("~/_controltemplates/15/ITWORX.MOEHEWF.UCE/LateRequests.ascx");
        //    ViewState["controlname"] = "LateRequests.ascx";
        //    this.PlaceHolder1.Controls.Clear();
        //    this.PlaceHolder1.Controls.Add(uc);
        //}
    }
}
