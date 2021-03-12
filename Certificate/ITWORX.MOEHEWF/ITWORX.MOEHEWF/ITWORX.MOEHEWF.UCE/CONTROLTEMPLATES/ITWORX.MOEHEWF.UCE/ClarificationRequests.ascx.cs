using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Entities;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class ClarificationRequests : UserControlBase
    {
        #region Public Properties
        public string SPGroupName { get; set; }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            var hdnCtl = Page.Request.Form.Get("__EventTriggerControlIdClar");
            if (string.IsNullOrEmpty(hdnCtl))
            {
                grd_ClarRequests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "DashboardPageSize"));
                BindGrid();
            }
        }
        private void BindGrid()
        {
            try
            {
                Logging.GetInstance().Debug("Enter LateRequests.BindGrid");
                List<ClarificationReqs> Requests = BL.ClarificationRequests.GetAllClarificationRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.ClarificationRequests.GetClarQueryPerRole(SPGroupName), "And", true)
                    + "<OrderBy><FieldRef Name='ClarificationDate' Ascending='False' /></OrderBy>", LCID).ToList();

                Requests = Requests.Where(x => (x.RequestSender.ToLower().Equals(SPContext.Current.Web.CurrentUser.Name.ToLower()) || x.RequestSender.ToLower() == SPGroupName.ToLower())).ToList();

                HelperMethods.BindGridView(grd_ClarRequests, Requests);
                if (Requests.Count > 0)
                {
                    lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NoOfRequests", (uint)LCID) + Requests.Count;
                    lbl_NoOfRequests.Visible = true;
                }
                else
                    lbl_NoOfRequests.Visible = false;
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit LateRequests.BindGrid");
            }
        }
        protected void grd_ClarRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter ClarificationRequests.grd_Requests_PageIndexChanging");
            try
            {
                grd_ClarRequests.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit ClarificationRequests.grd_Requests_PageIndexChanging");
            }
        }
        protected void lnk_View_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter ClarRequestsListing.lnk_View_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_ClarRequestId = (HiddenField)gvr.FindControl("hdn_ID");
                HiddenField hdn_ReqID = (HiddenField)gvr.FindControl("hdn_ReqID");
                Page.Session["hdn_ClarRequestId"] = hdn_ClarRequestId.Value;
                Page.Session["DisplayRequestId"] = hdn_ReqID.Value;

                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ClarificationReqDetails.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ClarRequestsListing.lnk_View_Click");
            }
        }

        protected void grd_ClarRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Logging.GetInstance().Debug("Enter NewRequests.grd_NewRequests_RowDataBound");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnk_Notes = (LinkButton)e.Row.FindControl("lnk_Notes");
                SimilarRequest req = e.Row.DataItem as SimilarRequest;
                if (req != null && lnk_Notes!=null)
                {
                    lnk_Notes.Visible = !string.IsNullOrEmpty(req.Note);
                }
            }
        }
    }
}
