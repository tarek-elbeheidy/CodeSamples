using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.PA.WebParts.TestWebPart
{
    public partial class TestWebPartUserControl : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //    UserControl uc = (UserControl)Page.LoadControl("~/_controltemplates/15/ITWORX.MOEHEWF.PA/PANewRequests.ascx");
            //    ViewState["controlname"] = "PANewRequests.ascx";
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
        //        Logging.GetInstance().Debug("Enter PAMyRequests.BindbtnsCount");
        //        lnkbtn_PANewRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "PANewRequests", (uint)LCID) + "(" + BL.PASearchSimilarRequests.GetAllRequestsbyStatus(LCID, "PANewRequest").Count.ToString() + ")";
        //        lnkbtn_ClarRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "PAClarificationRequests", (uint)LCID) + "(" + BL.PASearchSimilarRequests.GetAllRequestsbyStatus(LCID, "ClarRequest").Count.ToString() + ")";
        //        lnkbtn_RejectedRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "RejectedRequests", (uint)LCID) + "(" + BL.PASearchSimilarRequests.GetAllRequestsbyStatus(LCID, "RejectedRequest").Count.ToString() + ")";
        //        lnkbtn_LateRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "LateRequests", (uint)LCID) + "(" + BL.PASearchSimilarRequests.GetAllRequestsbyStatus(LCID, "LateRequests").Count.ToString() + ")";
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.GetInstance().LogException(ex);
        //    }
        //    finally
        //    {

        //        Logging.GetInstance().Debug("Exit PAMyRequests.BindbtnsCount");
        //    }
        //}

        //protected void lnkbtn_PANewRequests_Click(object sender, EventArgs e)
        //{
        //    UserControl uc = (UserControl)Page.LoadControl("~/_controltemplates/15/ITWORX.MOEHEWF.PA/PANewRequests.ascx");
        //    ViewState["controlname"] = "PANewRequests.ascx";
        //    this.PlaceHolder1.Controls.Clear();
        //    this.PlaceHolder1.Controls.Add(uc);
        //}

        //protected void lnkbtn_ClarRequests_Click(object sender, EventArgs e)
        //{
        //    UserControl uc = (UserControl)Page.LoadControl("~/_controltemplates/15/ITWORX.MOEHEWF.PA/PAClarificationRequests.ascx");
        //    ViewState["controlname"] = "PAClarificationRequests.ascx";
        //    this.PlaceHolder1.Controls.Clear();
        //    this.PlaceHolder1.Controls.Add(uc);
        //}

        //protected void lnkbtn_RejectedRequests_Click(object sender, EventArgs e)
        //{
        //    UserControl uc = (UserControl)Page.LoadControl("~/_controltemplates/15/ITWORX.MOEHEWF.PA/RejectedRequests.ascx");
        //    ViewState["controlname"] = "RejectedRequests.ascx";
        //    this.PlaceHolder1.Controls.Clear();
        //    this.PlaceHolder1.Controls.Add(uc);
        //}

        //protected void lnkbtn_LateRequests_Click(object sender, EventArgs e)
        //{
        //    UserControl uc = (UserControl)Page.LoadControl("~/_controltemplates/15/ITWORX.MOEHEWF.PA/LateRequests.ascx");
        //    ViewState["controlname"] = "LateRequests.ascx";
        //    this.PlaceHolder1.Controls.Clear();
        //    this.PlaceHolder1.Controls.Add(uc);
        //}
    }
}
