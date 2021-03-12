using ITWORX.MOEHEWF.PA.Entities;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using commonUtilities = ITWORX.MOEHEWF.Common.Utilities;
using requestBL = ITWORX.MOEHEWF.PA.BL;
using System.Web.UI;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class DepartmentManagerDecision : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Session["PADisplayRequestId"] != null)
            {
                List<Entities.Decisions> decision = decision = BL.Decisions.GetDecisionbyReqID(Page.Session["PADisplayRequestId"].ToString());
                bool AssignedTo = Utilities.BusinessHelper.GetAssignee(Page.Session["PADisplayRequestId"].ToString());

                if ((AssignedTo && decision.Count != 0) || (decision.Count != 0 && !AssignedTo))
                {
                    BindLabelsData(decision);
                }
                else if (AssignedTo && decision.Count == 0)
                {
                    HideControls();
                }
                else if (!AssignedTo)
                { 
                    lblPreventAddingDecision.Visible = true;
                    divContainer.Visible = false;

                }
                PARequest paRequest = requestBL.Request.GetRequestByNumber(Convert.ToInt32(Page.Session["PADisplayRequestId"]), LCID);
                if (paRequest != null && !(paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PAProgramManagerAcceptance).ToString() || paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PAProgramManagerReject).ToString()))
                {
                    lnk_AddNewDecisionPopUp.Visible = false; 
                }
                else
                    lnk_AddNewDecisionPopUp.Visible = true;

            }
        }

        private void BindLabelsData(List<Entities.Decisions> decision)
        {
            if (decision != null)
            {
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
            }
        }

        private void HideControls()
        {
            lbl_RejectionReason.Visible = false;
            lbl_Decision.Visible = false;
            lbl_Comments.Visible = false;
            Pnl_HMDecision.Visible = false;
        }

        protected void lnk_AddNewDecisionPopUp_Click(object sender, EventArgs e)
        {
            Session["ActionTaken"] = "AddNewPAHeadManagerDecision";
            Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.PA/DepartmentManager.aspx");
        }
    }
}