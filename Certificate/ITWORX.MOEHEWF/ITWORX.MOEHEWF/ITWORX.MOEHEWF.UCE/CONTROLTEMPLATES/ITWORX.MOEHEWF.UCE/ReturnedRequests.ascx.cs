using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Entities;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class ReturnedRequests : UserControlBase
    {
        #region Public Properties
        public string SPGroupName { get; set; }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            var hdnCtl = Page.Request.Form.Get("__EventTriggerControlId");
            if (string.IsNullOrEmpty(hdnCtl))
            {
                grd_ReturnedRequests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "DashboardPageSize"));
                BindGrid();
            }
        }
        private void BindGrid()
        {
            try
            {
                Logging.GetInstance().Debug("Enter RejectedRequests.BindGrid");
                List<SimilarRequest> Requests = BL.ReturnedRequests.GetAllReturnedRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.ReturnedRequests.GetReturnedQueryPerRole(SPGroupName), "Or", true) + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();

                int delayedDays = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "DelayedDays"));

                Requests = Requests.Where(x => (x.AssignedTo.ToLower().Equals(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == SPGroupName.ToLower() || x.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEHigherEduInstitutesNeedsStatement) && (string.IsNullOrEmpty(x.DelayedDays) || int.Parse(x.DelayedDays) < delayedDays)).ToList();
                HelperMethods.BindGridView(grd_ReturnedRequests, Requests);
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

                Logging.GetInstance().Debug("Exit RejectedRequests.BindGrid");
            }

        }
        protected void grd_ReturnedRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {
                Logging.GetInstance().Debug("Enter RejectedRequests.grd_RejectedRequests_PageIndexChanging");
                grd_ReturnedRequests.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit RejectedRequests.grd_RejectedRequests_PageIndexChanging");
            }
        }
        protected void grd_ReturnedRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter RejectedRequests.grd_NewRequests_RowDataBound");
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    LinkButton lnk_Notes = (LinkButton)e.Row.FindControl("lnk_Notes");

                    SimilarRequest req = e.Row.DataItem as SimilarRequest;
                    if (req != null && lnk_Notes != null)
                        lnk_Notes.Visible = !string.IsNullOrEmpty(req.Note);

                    HiddenField IsClosed = (HiddenField)e.Row.FindControl("hdn_IsClosed");
                    HiddenField AssignedTo = (HiddenField)e.Row.FindControl("hdn_AssignedTo");
                    HiddenField StatusID = (HiddenField)e.Row.FindControl("hdn_RequestStatusId");
                    CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkbox_Select");
                    LinkButton btnEdit = (LinkButton)e.Row.FindControl("lnk_Edit");
                    LinkButton btnView = (LinkButton)e.Row.FindControl("lnk_View");
                    LinkButton btnCheckOut = (LinkButton)e.Row.FindControl("lnk_CheckOut");
                    btnEdit.Visible = true;
                    btnView.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit RejectedRequests.grd_NewRequests_RowDataBound");
            }
        }
        protected void lnk_View_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter RejectedRequests.lnk_View_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_RequestID = (HiddenField)gvr.FindControl("hdn_ID");
                Page.Session["DisplayRequestId"] = hdn_RequestID.Value;
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdn_RequestStatusId");
                Common.Entities.RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                string viewLink = string.Empty;
                if (!requestStatus.CanReviewerEditRequest)
                    viewLink = requestStatus.ReviewerTargetPageURL;

                SPUtility.Redirect(SPContext.Current.Web.Url + viewLink, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit RejectedRequests.lnk_View_Click");
            }
        }
        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debugger.Break();
            Logging.GetInstance().Debug("Enter RejectedRequests.lnk_Edit_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_RequestID = (HiddenField)gvr.FindControl("hdn_ID");
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdn_RequestStatusId");
                Common.Entities.RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                string editLink = string.Empty;
                if (requestStatus.CanReviewerEditRequest)
                    editLink = requestStatus.ReviewerTargetPageURL;

                Page.Session["DisplayRequestId"] = hdn_RequestID.Value;
                SPUtility.Redirect(SPContext.Current.Web.Url + editLink, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit RejectedRequests.lnk_Edit_Click");
            }
        }


    }
}
