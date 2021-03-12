using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class DepartmentManagerDecision : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Session["DisplayRequestId"] != null)
            {
                List<Entities.Decisions> decision = decision = BL.Decisions.GetDecisionbyReqID(Page.Session["DisplayRequestId"].ToString());
                bool AssignedTo = Utilities.BusinessHelper.GetAssignee(Page.Session["DisplayRequestId"].ToString());

                if ((AssignedTo && decision.Count != 0) || (decision.Count != 0 && !AssignedTo))
                {
                    lnk_AddNewDecisionPopUp.Visible = false;
                    BindLabelsData(decision);
                }
                else if (AssignedTo && decision.Count == 0)
                {
                    HideControls();
                } 
                else if(!AssignedTo)
                {
                    lnk_AddNewDecisionPopUp.Visible = false;
                    lblPreventAddingDecision.Visible = true;
                    divContainer.Visible = false;
                }
            }
            else
            {
                divContainer.Visible = false;
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
                //divContainer.Visible = true;
                lbl_Comments.Visible = true;
                lbl_CommentsVal.Visible = true;
                lbl_Decision.Visible = true;
                lbl_FinalDecisionVal.Visible = true;
            }
        }
        private void HideControls()
        {
            divContainer.Visible = false;
            lbl_RejectionReason.Visible = false;
            lbl_Decision.Visible = false;
            lbl_Comments.Visible = false;
        }
        protected void lnk_AddNewDecisionPopUp_Click(object sender, EventArgs e)
        {
            Session["ActionTaken"] = "AddNewHeadManagerDecision";
            Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.UCE/DepartmentManager.aspx");
        }
    }
}
