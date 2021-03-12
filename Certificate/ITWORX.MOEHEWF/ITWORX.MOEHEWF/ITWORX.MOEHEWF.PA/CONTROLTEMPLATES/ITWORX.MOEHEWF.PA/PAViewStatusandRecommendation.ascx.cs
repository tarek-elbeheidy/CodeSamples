using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Entities;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Web.UI;
using requestBL = ITWORX.MOEHEWF.PA.BL;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class PAViewStatusAndRecommendation : UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload ViewStatusRecommendAttachements;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Session["PADisplayRequestId"] != null)
            {
                BindLabelsData(Page.Session["PADisplayRequestId"].ToString());
            }
        }

        private void BindLabelsData(string ReqID)
        {
            try
            {
                Logging.GetInstance().Debug("Enter PAViewStatusAndRecommendation.BindLabelsData");
                List<Entities.Procedures> RecommendProc = BL.AllProcedures.GetApprovedPARecommendationstatus(Page.Session["PADisplayRequestId"].ToString(), "Approved");
                if (RecommendProc.Count > 0)
                {
                    lbl_EmpOpinion.Text = RecommendProc[0].Opinion;
                    lbl_EmpRecommendation.Text = RecommendProc[0].Procedure;
                    lbl_DecisiontxtForPrintingVal.Text = RecommendProc[0].DecisionForPrint;
                    lbl_EmpRecommend.Visible = true;
                    lbl_Decisiontxt.Visible = true;
                    lbl_Opinion.Visible = true;
                    lblEarnedHoursV.Text = RecommendProc[0].EarnedHours;
                    lblOnlineHoursV.Text = RecommendProc[0].OnlineHours;
                    lblOnlineHoursPerV.Text = RecommendProc[0].OnlineHoursPer;
                    lblEarnedHours.Visible = true;
                    lblOnlineHours.Visible = true;
                    lblOnlineHoursPer.Visible = true;

                    attachmentsDiv.Visible = true;

                    #region Display Mode

                    ViewStatusRecommendAttachements.DocumentLibraryName = Utilities.Constants.PAProgEmpStatusandRecomAttachements;
                    ViewStatusRecommendAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
                    ViewStatusRecommendAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NotesPrepared", (uint)LCID);
                    ViewStatusRecommendAttachements.Group = "StatusRecommendAttachements";
                    ViewStatusRecommendAttachements.RequestID = Convert.ToInt32(Page.Session["PADisplayRequestId"].ToString());
                    ViewStatusRecommendAttachements.Enabled = false;
                    ViewStatusRecommendAttachements.Bind();

                    #endregion Display Mode

                    var knownGrps = loggedInUserGroups();
                    if (knownGrps.Contains(Common.Utilities.Constants.ProgramManagerGroupName))
                    {
                        btn_ReviewDecision.Visible = true;
                    }
                    else
                        btn_ReviewDecision.Visible = false;
                }
                else
                {
                    lbl_NoResults.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PAViewStatusAndRecommendation.BindLabelsData");
            }
        }

        private List<string> loggedInUserGroups()
        {
            List<string> result = new List<string>();
            SPGroupCollection _SPGroupCollection = SPContext.Current.Web.CurrentUser.Groups;
            foreach (SPGroup group in _SPGroupCollection)
            {
                result.Add(group.Name);
            }
            return result;
        }

        protected void ReviewDecisionClick(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method PAViewStatusAndRecommendation.btn_ReviewDecisionClick");
                if (Page.Session["PADisplayRequestId"] != null)
                {
                    PARequest paRequest = requestBL.Request.GetRequestByNumber(Convert.ToInt32(Page.Session["PADisplayRequestId"]), LCID);
                    hdn_RequestNumber.Value = paRequest.RequestNumber;
                    Common.BL.FinalDecisionPrint.ReviewPAPDF(hdn_RequestNumber.Value, "PDF", LCID);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PAViewStatusAndRecommendation.btn_ReviewDecisionClick");
            }
        }
    }
}