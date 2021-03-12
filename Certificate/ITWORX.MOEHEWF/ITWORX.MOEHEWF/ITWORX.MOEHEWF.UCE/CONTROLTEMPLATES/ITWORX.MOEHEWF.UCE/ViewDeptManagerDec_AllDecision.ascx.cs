using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Nintex.Workflow.HumanApproval;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using common = ITWORX.MOEHEWF.Common;



namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class ViewDeptManagerDec_AllDecision : UserControlBase
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
                if (Page.Session["DisplayRequestId"] != null)
                {
                    SPSite site = new SPSite(SPContext.Current.Site.Url);
                    SPWeb web = site.OpenWeb();
                    SPListItem requestItem = web.Lists["Requests"].GetItemById(int.Parse(Page.Session["DisplayRequestId"].ToString()));
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
            if (Page.Session["DisplayRequestId"] != null)
            {
                List<Entities.Decisions> decision = new List<Entities.Decisions>();
                if (!string.IsNullOrEmpty(SPGroupName))
                {
                    decision = BL.Decisions.GetDecisionPerRolebyReqID(Page.Session["DisplayRequestId"].ToString(), SPGroupName);
                    bool AssignedTo = Utilities.BusinessHelper.GetAssignee(Page.Session["DisplayRequestId"].ToString());

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
            try
            {
                if (decision != null)
                {
                    if (Page.Session["DisplayRequestId"] != null)
                    {
                        List<Entities.Procedures> RecommendProc = BL.AllProcedures.GetApprovedRecommendationStatus(Page.Session["DisplayRequestId"].ToString(), "Approved");
                        if (RecommendProc.Count > 0)
                        {
                            lbl_DecisiontxtVal.Text = SPHttpUtility.HtmlDecode(RecommendProc[0].DecisionForPrint);
                            lbl_SirValue.Text = !string.IsNullOrEmpty(RecommendProc[0].SirValue) ? RecommendProc[0].SirValue : HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE", "sir").ToString();
                            lbl_RespectedValue.Text = !string.IsNullOrEmpty(RecommendProc[0].RespectedValue) ? RecommendProc[0].RespectedValue : HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE", "recpected").ToString();
                            lbl_OccupationName.Text = RecommendProc[0].OccupationName;
                            lbl_decicionBodyView.Text = string.Format(lbl_decicionBodyView.Text, RecommendProc[0].BookNum, ExtensionMethods.QatarFormatedDate(Convert.ToDateTime(RecommendProc[0].BookDate)), "{0}", "{1}","{2}");
                            hdBookNum.Value = RecommendProc[0].BookNum;
                            lbl_headManagerName.Text = RecommendProc[0].HeadManagerName;
                            hdBookDate.Value = ExtensionMethods.QatarFormatedDate(Convert.ToDateTime(RecommendProc[0].BookDate));
                            _bindDecisionTemplate();
                            decisionDiv.Visible = true;
                            lbl_PrintDecision.Visible = true;
                        }
                        SPListItem item = BL.Request.Reviewer_GetRequestItemByID(int.Parse(Page.Session["DisplayRequestId"].ToString()));
                        hdn_RequestNumber.Value = item["RequestNumber"].ToString();
                    }
                    if (!BL.SearchSimilarRequests.GetRequestStatusbyReqID(Page.Session["DisplayRequestId"].ToString()))
                    {
                        if ((decision[0].Decision.Contains("Approved") || decision[0].Decision.Contains("بالموافقة")) &&
                            (HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName) || HelperMethods.InGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName)))
                            btn_Approve.Visible = true;
                        else if ((decision[0].Decision.Contains("Rejected") || decision[0].Decision.Contains("بالرفض")) &&
                            (HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName) || HelperMethods.InGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName)))
                            btn_Reject.Visible = true;
                    }
                    else
                        btn_PrintFinalDecision.Visible = true;

                    lbl_CommentsVal.Text = decision[0].DecisionComments;
                    lbl_FinalDecisionVal.Text = decision[0].Decision;
                    lblDecisionMaker.Text = decision[0].DecisionMakerName;
                    lblHeadManager.Visible = true;
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
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
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
            Session["ActionTaken"] = "AddNewHeadManagerDecision";
            Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.UCE/DepartmentManager.aspx");
        }

        protected void btn_Approve_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_Clarification.btn_Reply_Click");
                if (!IsRefresh)
                {
                    if (Page.Session["DisplayRequestId"] != null)
                    { 
                        Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.UCEClosedByAcceptance, Page.Session["DisplayRequestId"].ToString(), string.Empty);
                        Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.Requests, Utilities.Constants.RequestHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.UCEClosedByAcceptance, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["DisplayRequestId"].ToString(), "Yes");
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
                    if (Page.Session["DisplayRequestId"] != null)
                    { 
                        Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.UCEClosedByRejection, Page.Session["DisplayRequestId"].ToString(), string.Empty);
                        Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.Requests,Utilities.Constants.RequestHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.UCEClosedByRejection, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["DisplayRequestId"].ToString(), "Yes");

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
        private void _bindDecisionTemplate()
        {
            try
            {
                if (Page.Session["DisplayRequestId"] != null)
                {
                    SPListItem request = common.BL.Request.GetRequestItemByID(int.Parse(Page.Session["DisplayRequestId"].ToString()));
                    {
                         
                        string ApplicantName = string.Empty;
                        string PersonalID = string.Empty; 

                        if (request["Applicants_PersonalID"] != null)
                        {
                            SPFieldLookupValue Applicants_PersonalID = new SPFieldLookupValue(request["Applicants_PersonalID"].ToString());
                            PersonalID = Applicants_PersonalID.LookupValue;
                        }
                        if (request["Applicants_ApplicantAraicName"] != null)
                        {
                            SPFieldLookupValue Applicants_ApplicantName = new SPFieldLookupValue(request["Applicants_ApplicantAraicName"].ToString());
                            ApplicantName = Applicants_ApplicantName.LookupValue;
                        }

                        string applicantGender = common.BL.Applicants.IsFemale(PersonalID) ? HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "studentF", (uint)LCID) : HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "studentM", (uint)LCID);


                        lbl_decicionBodyView.Text = lbl_decicionBodyView.Text = string.Format(lbl_decicionBodyView.Text, applicantGender, ApplicantName, PersonalID);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
        }
        protected void btn_PrintFinalDecision_Click(object sender, EventArgs e)
        {
  
            Common.BL.FinalDecisionPrint.PDFExportation(hdn_RequestNumber.Value, lbl_DecisiontxtVal.Text, lbl_OccupationName.Text,hdBookDate.Value,hdBookNum.Value, lbl_headManagerName.Text,lbl_SirValue.Text,lbl_RespectedValue.Text, "PDF", LCID);
    }
    }
}
