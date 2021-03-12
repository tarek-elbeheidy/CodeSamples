using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Nintex.Workflow.HumanApproval;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class ViewDeptManagerDec_AllDecision : Utilities.UserControlBase
    {
        #region Public Properties
        public string SPGroupName { get; set; }
        public NintexContext CurrentContext { get; set; }
        public bool IsTaskComplete { get; set; }
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (Page.Session["PADisplayRequestId"] != null)
                {
                    SPSite site = new SPSite(SPContext.Current.Site.Url);
                    SPWeb web = site.OpenWeb();
                    SPListItem requestItem = web.Lists["Requests"].GetItemById(int.Parse(Page.Session["PADisplayRequestId"].ToString()));
                    SPList list = web.Lists.TryGetList("Workflow Tasks");
                    Guid taskListId = new Guid(list.ID.ToString());
                    int spTaskItemId = requestItem.Tasks[0].ID;
                    // create Nintext context
                    this.CurrentContext = NintexHelper.ParseRequest(spTaskItemId, taskListId);
                    this.CurrentContext.TaskAuthorized = Nintex.Workflow.HumanApproval.User.CheckCurrentUserMatchesHWUser(SPContext.Current.Web, CurrentContext.Approver);
                    // check if a task has already completed
                    IsTaskComplete = (CurrentContext.Approver.ApprovalOutcome != Outcome.Pending);
                    string controlUrl = string.Empty;
                    string contentAction = CurrentContext.TaskItem.ContentType.Name;
                    web.Dispose();
                    site.Dispose();
                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Session["PADisplayRequestId"] != null)
            {
                List<Entities.Decisions> decision = new List<Entities.Decisions>();
                if (!string.IsNullOrEmpty(SPGroupName))
                {
                    decision = BL.Decisions.GetDecisionPerRolebyReqID(Page.Session["PADisplayRequestId"].ToString(), SPGroupName);
                    bool AssignedTo = Utilities.BusinessHelper.GetAssignee(Page.Session["PADisplayRequestId"].ToString());

                    if ((AssignedTo && decision.Count != 0) || (decision.Count != 0 && !AssignedTo))
                        BindLabelsData(decision);

                    else if (AssignedTo && decision.Count == 0 && HelperMethods.InGroup(SPGroupName))
                    {
                        HideControls();
                        lbl_NoResults.Visible = true;
                    }
                    else
                        lbl_NoResults.Visible = true;

                }
            }
        }
        
        private void BindLabelsData(List<Entities.Decisions> decision)
        {

            if (decision != null)
            {
                if (!BL.PASearchSimilarRequests.GetRequestStatusbyReqID(Page.Session["PADisplayRequestId"].ToString()))
                {
                    if ((decision[0].Decision == "Approved" || decision[0].Decision == "بالموافقة") &&
                        (HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName) || HelperMethods.InGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName)))
                        btn_Approve.Visible = true;
                    else if (decision[0].Decision == "Rejected" || decision[0].Decision == "بالرفض" &&
                        (HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName) || HelperMethods.InGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName)))
                        btn_Reject.Visible = false;
                }
                else
                    btn_PrintFinalDecision.Visible = true;

                lbl_CommentsVal.Text = decision[0].DecisionComments;
                lbl_FinalDecisionVal.Text = decision[0].Decision;
                if (!string.IsNullOrEmpty(decision[0].RejectionReason))
                {
                    lbl_RejectionReasonVal.Text = decision[0].RejectionReason;
                    lbl_RejectionReasonVal.Visible = true;
                    lbl_RejectionReason.Visible = true;
                }
                else
                {
                    lbl_RejectionReasonVal.Visible = false;
                    lbl_RejectionReason.Visible = false;
                }
                lbl_Comments.Visible = true;
                lbl_CommentsVal.Visible = true;
                lbl_Decision.Visible = true;
                lbl_FinalDecisionVal.Visible = true;
                lbl_NoResults.Visible = false;

            }
        }
        private void HideControls()
        {
            lbl_RejectionReason.Visible = false;
            lbl_Decision.Visible = false;
            lbl_Comments.Visible = false;
        }
        protected void lnk_AddNewDecisionPopUp_Click(object sender, EventArgs e)
        {
            Session["ActionTaken"] = "AddNewPAHeadManagerDecision";
            Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.PA/DepartmentManager.aspx");
        }

        protected void btn_Approve_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_Clarification.btn_Reply_Click");
                if (!IsRefresh)
                {
                    if (Page.Session["PADisplayRequestId"] != null)
                    {
                        //NintexHelper.ContinueTask(CurrentContext, "", "Approved");
                        Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.PAClosedByAcceptance, Page.Session["PADisplayRequestId"].ToString(), string.Empty);
                        lbl_SuccessMsg.Visible = true;
                        btn_PrintFinalDecision.Visible = true;
                        btn_Approve.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_Clarification.btn_Reply_Click");
            }
        }

        protected void btn_Reject_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_Clarification.btn_Reply_Click");
                if (!IsRefresh)
                {
                    if (Page.Session["PADisplayRequestId"] != null)
                    {
                        //NintexHelper.ContinueTask(CurrentContext, "", "Rejected");
                        Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.PAClosedByRejection, Page.Session["PADisplayRequestId"].ToString(), string.Empty);
                        lbl_SuccessMsg.Visible = true;
                        btn_PrintFinalDecision.Visible = true;
                        btn_Reject.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_Clarification.btn_Reply_Click");
            }
        }

        protected void btn_PrintFinalDecision_Click(object sender, EventArgs e)
        {
            //Response.Redirect((LCID == 1033 ? "/en" : "/ar") + "/Pages/PAViewFinalDecision.aspx");
            SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAViewFinalDecision.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
        }
    }
}
