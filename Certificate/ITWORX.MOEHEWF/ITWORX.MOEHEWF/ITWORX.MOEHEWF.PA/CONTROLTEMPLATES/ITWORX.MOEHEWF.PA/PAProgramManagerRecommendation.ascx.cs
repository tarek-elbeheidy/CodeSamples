using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHEWF.PA.Entities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Web.UI;
using requestBL = ITWORX.MOEHEWF.PA.BL;
using commonUtilities = ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHEWF.PA.Utilities;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class ProgramManagerRecommendation : UserControlBase
    {
        public bool viewRecommendation { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Session["PADisplayRequestId"] != null)
            { bool AssignedTo = Utilities.BusinessHelper.GetAssignee(Page.Session["PADisplayRequestId"].ToString(), Common.Utilities.Constants.ProgramManagerGroupName);
                if (AssignedTo && HelperMethods.InGroup(Common.Utilities.Constants.ProgramManagerGroupName))
                    lnk_AddNewRecommendation.Visible = true;

                BindLabelsData();
            }
        }
        private void BindLabelsData()
        {
            List<Entities.Recomendations> Recomendation = BL.PARecommendations.GetRecommendationbyReqID(Page.Session["PADisplayRequestId"].ToString());
            if (Recomendation.Count > 0)
            {
                int index = Recomendation.Count - 1;
                lbl_CommentsVal.Text = Recomendation[index].RecommendationComments;
                lbl_FinalDecisionVal.Text = Recomendation[index].Recommendation;
                lbl_CreatedbyVal.Text = Recomendation[index].RecommendationCreatedBy;
                lbl_DateVal.Text = Recomendation[index].RecommendationDate;
                
                PARequest paRequest = requestBL.Request.GetRequestByNumber(Convert.ToInt32(Page.Session["PADisplayRequestId"]), LCID);
                if (paRequest != null && paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PAHeadEmployeeMissingInformation).ToString()|| paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PAProgramManagerReviewRecommendation).ToString())
                {
                    lnk_AddNewRecommendation.Visible = true;
                }
                else
                {
                    lnk_AddNewRecommendation.Visible = false;
                }
            }
            else
            {
                lbl_Comments.Visible = false;
                lbl_Createdby.Visible = false;
                lbl_Date.Visible = false;
                lbl_Decision.Visible = false;
            }
            
        }
        protected void lnk_AddNewRecommendation_Click(object sender, EventArgs e)
        {
            Session["ActionTaken"] = "AddNewRecommendation";
            Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.PA/ProgramManager.aspx");
        }
    }
}
