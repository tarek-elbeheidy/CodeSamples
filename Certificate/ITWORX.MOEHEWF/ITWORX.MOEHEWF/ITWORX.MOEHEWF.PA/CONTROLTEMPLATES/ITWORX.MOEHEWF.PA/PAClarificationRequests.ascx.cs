using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Entities;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class PAClarificationRequests : UserControlBase
    {
        #region Public Properties

        public string SPGroupName { get; set; }

        #endregion Public Properties

        protected void Page_Load(object sender, EventArgs e)
        {
            var hdnCtl = Page.Request.Form.Get("__EventTriggerControlIdPAClar");
            if (string.IsNullOrEmpty(hdnCtl))
            {
                lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NoOfRequests", (uint)LCID) + " 0";
                grd_ClarRequests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "DashboardPageSize"));
                BindGrid();
            }
        }

        private void BindGrid()
        {
            try
            {
                Logging.GetInstance().Debug("Enter LateRequests.BindGrid");
                List<ClarificationReqs> clarifications = BL.PAClarificationRequests.GetAllPAClarificationRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.PAClarificationRequests.GetClarQueryPerRole(SPGroupName), "And", true)
                    + "<OrderBy><FieldRef Name='ClarificationDate' Ascending='False' /></OrderBy>", LCID).ToList();

                clarifications = clarifications.Where(x => (x.RequestSender.ToLower().Equals(SPContext.Current.Web.CurrentUser.Name.ToLower()) || x.RequestSender.ToLower() == SPGroupName.ToLower())).ToList();
                HelperMethods.BindGridView(grd_ClarRequests, clarifications);
                if (clarifications.Count > 0)
                {
                    lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NoOfRequests", (uint)LCID) + " " + clarifications.Count;
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
            Logging.GetInstance().Debug("Enter PAClarificationRequests.grd_Requests_PageIndexChanging");
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
                Logging.GetInstance().Debug("Exit PAClarificationRequests.grd_Requests_PageIndexChanging");
            }
        }
        protected void grd_PAClarificationRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter PANewRequests.grd_PANewRequests_RowDataBound");
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton IsClosed = (LinkButton)e.Row.FindControl("lnk_View"); 
                    HiddenField StatusID = (HiddenField)e.Row.FindControl("hdn_RequestStatusId"); 
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PANewRequests.grd_PANewRequests_RowDataBound");
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
                Page.Session["PADisplayRequestId"] = hdn_ReqID.Value;

                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAClarificationReqDetails.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

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
    }
}