using ITWORX.MOEHE.Utilities;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class ViewTechCommitteeDec_AllDecision : UserControl
    {
        #region Public Properties
        public string SPGroupName { get; set; }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Entities.Decisions> decision = new List<Entities.Decisions>();
            if (!string.IsNullOrEmpty(SPGroupName))
            {
                decision = BL.Decisions.GetDecisionPerRolebyReqID(Page.Session["PADisplayRequestId"].ToString(), SPGroupName);
                bool AssignedTo = Utilities.BusinessHelper.GetAssignee(Page.Session["PADisplayRequestId"].ToString());
                if ((AssignedTo  && decision.Count != 0) ||(decision.Count != 0 && !AssignedTo))
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
                lbl_NoResults.Visible = false;

            }
        }
        private void HideControls()
        {
            lbl_RejectionReason.Visible = false;
            lbl_Decision.Visible = false;
            lbl_Comments.Visible = false;
        }
       
    }
}
