using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHEWF.PA.BL;
using Microsoft.SharePoint;
using System.Linq;
using ITWORX.MOEHE.Utilities;
using System.Collections.Generic;
using ITWORX.MOEHEWF.PA.Entities;
using System.Web;
using Microsoft.SharePoint.Utilities;
using ITWORX.MOEHEWF.UCE.Utilities;
using Nintex.Workflow.HumanApproval;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class PANewRequests : Utilities.UserControlBase
    {
        #region Public Properties
        public string SPGroupName { get; set; }

        public NintexContext CurrentContext { get; set; }
        public bool IsTaskComplete { get; set; }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            var hdnCtl = Page.Request.Form.Get("__EventTriggerControlIdPANew");
            if (string.IsNullOrEmpty(hdnCtl))
            {
                grd_PANewRequests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "DashboardPageSize"));
                BindGrid();
            }
        } 
        private void BindGrid()
        {
            try
            {
                Logging.GetInstance().Debug("Enter PANewRequests.BindGrid");
                List<SimilarRequest> Requests = BL.PANewRequests.GetAllPANewRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.PANewRequests.GetNewQueryPerRole(SPGroupName), "Or", true) + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();


                int delayedDays = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "PADelayedDays"));
                Requests = Requests.Where(x => (x.AssignedTo.ToLower().Equals(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == SPGroupName.ToLower())&&(string.IsNullOrEmpty(x.DelayedDays)||int.Parse(x.DelayedDays)<delayedDays)).ToList();

                HelperMethods.BindGridView(grd_PANewRequests, Requests);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit PANewRequests.BindGrid");
            }
        }
        protected void grd_PANewRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter PANewRequests.grd_PANewRequests_PageIndexChanging");
            try
            { 
                grd_PANewRequests.PageIndex = e.NewPageIndex;
                BindGrid(); 
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit PANewRequests.grd_PANewRequests_PageIndexChanging");
            }
        }
        protected void grd_PANewRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter PANewRequests.grd_PANewRequests_RowDataBound");
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField IsClosed = (HiddenField)e.Row.FindControl("hdn_IsClosed");
                    HiddenField AssignedTo = (HiddenField)e.Row.FindControl("hdn_AssignedTo");
                    HiddenField StatusID = (HiddenField)e.Row.FindControl("hdn_RequestStatusId"); 
                    LinkButton btnEdit = (LinkButton)e.Row.FindControl("lnk_Edit");
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
        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter PANewRequests.lnk_Edit_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_RequestID = (HiddenField)gvr.FindControl("hdn_ID");
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdn_RequestStatusId");
                HiddenField hdn_QID = (HiddenField)gvr.FindControl("hdn_QID");
                Common.Entities.RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                string editLink = string.Empty;
                if (requestStatus.CanReviewerEditRequest)
                    editLink = requestStatus.ReviewerTargetPageURL;

                Page.Session["PADisplayRequestId"] = hdn_RequestID.Value;
                Page.Session["PAQID"] = hdn_QID.Value;
                SPUtility.Redirect(SPContext.Current.Web.Url + editLink, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit PANewRequests.lnk_Edit_Click");
            }
        }
        private void PersistRowIndex(int index)
        {
            if (!SelectedRequestsIndex.Exists(i => i == index))
            {
                SelectedRequestsIndex.Add(index);
            }
        }
        private void RemoveRowIndex(int index)
        {
            SelectedRequestsIndex.Remove(index);
        }
        private List<Int32> SelectedRequestsIndex
        {
            get
            {
                if (ViewState["SELECTED_Requests_INDEX"] == null)
                {
                    ViewState["SELECTED_Requests_INDEX"] = new List<Int32>();
                }

                return (List<Int32>)ViewState["SELECTED_Requests_INDEX"];
            }
        }  
    }
}