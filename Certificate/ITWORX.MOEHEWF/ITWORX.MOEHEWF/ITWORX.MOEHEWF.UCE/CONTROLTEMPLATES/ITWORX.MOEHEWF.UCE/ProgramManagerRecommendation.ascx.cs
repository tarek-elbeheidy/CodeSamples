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
    public partial class ProgramManagerRecommendation : UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload RecommProgramManagerAttachements;

        public bool viewRecommendation { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Session["DisplayRequestId"] != null)
            {
                bool AssignedTo = Utilities.BusinessHelper.GetAssignee(Page.Session["DisplayRequestId"].ToString(), Common.Utilities.Constants.ProgramManagerGroupName);


                BindLabelsData();
                if (AssignedTo && HelperMethods.InGroup(Common.Utilities.Constants.ProgramManagerGroupName))
                    lnk_AddNewRecommendation.Visible = true;
                else
                    lnk_AddNewRecommendation.Visible = false;
            }
        }
        private void BindLabelsData()
        {
            List<Entities.Recomendations> Recomendation = BL.Recommendations.GetRecommendationbyReqID(Page.Session["DisplayRequestId"].ToString());
            if (Recomendation.Count > 0)
            {
                int index = Recomendation.Count - 1;
                lbl_CommentsVal.Text = Recomendation[index].RecommendationComments;
                lbl_FinalDecisionVal.Text = Recomendation[index].Recommendation;
                lbl_CreatedbyVal.Text = Recomendation[index].RecommendationCreatedBy;
                lbl_DateVal.Text = Recomendation[index].RecommendationDate;
                BindAttachments(Convert.ToInt32(Page.Session["DisplayRequestId"]));
                div_Rec.Visible = true;
            }
            else
            {
                div_Rec.Visible = false;
            }

            lnk_AddNewRecommendation.Visible = !viewRecommendation; 
        }
        protected void lnk_AddNewRecommendation_Click(object sender, EventArgs e)
        {
            Session["ActionTaken"] = "AddNewRecommendation";
            Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.UCE/ProgramManager.aspx");
        }
        private void BindAttachments(int requestId)
        {
            Logging.GetInstance().Debug("Entering method ProgramManagerRecommendation.BindAttachments");
            try
            {
                RecommProgramManagerAttachements.DocumentLibraryName = Utilities.Constants.ProgramManagerRecommendationAttachements;
                RecommProgramManagerAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
                RecommProgramManagerAttachements.Group = "ProgramManagerRecommendation";
                RecommProgramManagerAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Attachments", (uint)LCID);
                RecommProgramManagerAttachements.RequestID = requestId;
                RecommProgramManagerAttachements.Enabled = false;
                RecommProgramManagerAttachements.LookupFieldName = "RequestID";
                RecommProgramManagerAttachements.Bind();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method ProgramManagerRecommendation.BindAttachments");
            }
        }
    }
}
