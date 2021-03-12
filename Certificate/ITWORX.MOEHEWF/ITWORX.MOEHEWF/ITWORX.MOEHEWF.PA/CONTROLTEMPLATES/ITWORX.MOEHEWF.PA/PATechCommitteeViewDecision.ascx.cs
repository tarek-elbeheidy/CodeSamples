using ITWORX.MOEHE.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class TechCommitteeViewDecision : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Session["PADisplayRequestId"] != null)
            {
                List<Entities.Decisions> decision = BL.Decisions.GetDecisionbyReqID(Page.Session["PADisplayRequestId"].ToString());
                bool AssignedTo = Utilities.BusinessHelper.GetAssignee(Page.Session["PADisplayRequestId"].ToString());

                if ((AssignedTo && decision.Count != 0) ||(decision.Count != 0 && !AssignedTo))
                {
                    BindLabelsData(decision);
                }
                else if (AssignedTo && decision.Count == 0)
                {
                    HideControls();
                }
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
        }
        protected void lnk_AddNewDecisionPopUp_Click(object sender, EventArgs e)
        {
            Session["ActionTaken"] = "AddNewTechDecision";
            Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.PA/TechnicalCommitte.aspx");
        }
    }
}
