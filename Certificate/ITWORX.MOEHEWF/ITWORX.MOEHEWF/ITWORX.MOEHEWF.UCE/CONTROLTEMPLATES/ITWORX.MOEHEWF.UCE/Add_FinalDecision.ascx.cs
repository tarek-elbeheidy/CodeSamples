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
using System.Web.UI.WebControls;
using BLCommon = ITWORX.MOEHEWF.Common.BL;
using UtilitiesCommon = ITWORX.MOEHEWF.Common.Utilities;
using common = ITWORX.MOEHEWF.Common;


namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class Add_FinalDecision : UserControlBase
    {
        public NintexContext CurrentContext { get; set; }
        public bool IsTaskComplete { get; set; }
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
            if (!IsPostBack)
            {
                Binddrp_Decisions();
                BindRejectionReason();
                lbl_headManagerName.Text = HelperMethods.GetConfigurationValue(SPContext.Current.Site.RootWeb.Url, Utilities.Constants.ConfigurationList, "HeadManagerName");
            }
        }

        protected void drp_FinalDecision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drp_FinalDecision.SelectedValue == "2")
            {
                drp_RejectionReason.Visible = true;
                lbl_RejectionReason.Visible = true;
                lbl_decisionText.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "rejectDecisionText", (uint)LCID);
            }
            else
            {
                drp_RejectionReason.Visible = false;
                lbl_RejectionReason.Visible = false;
                lbl_decisionText.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "approveDecisionText", (uint)LCID);

            }
        }

        protected void ReviewDecisionClick(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_FinalDecision.btn_ReviewDecisionClick");
                if (Page.Session["DisplayRequestId"] != null)
                {
                    int dec = 0;
                    if (drp_FinalDecision.SelectedValue == "2")
                    {
                        dec = 1;
                    }
                    Common.BL.FinalDecisionPrint.ReviewUCEPDF(hdn_RequestNumber.Value, txt_DecisiontxtForPrinting.Text, txtOccupationName.Text, txt_bookDate.Text, txt_bookNum.Text, lbl_headManagerName.Text, "PDF",lbl_SirValue.Text ,lbl_RespectedValue.Text, LCID, dec);
                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_FinalDecision.btn_ReviewDecisionClick");
            }
        }

        protected void btn_ApproveDecision_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_FinalDecision.btn_ApproveDecision_Click");
                //if (!IsRefresh)
                //{
                    if (Page.Session["DisplayRequestId"] != null)
                    {
                        if (Session["ActionTaken"] != null && Session["ActionTaken"].ToString() == "AddNewTechDecision")
                        {
                            if (drp_RejectionReason.SelectedIndex != 0)
                                BL.Decisions.AddDecision(drp_FinalDecision.SelectedItem.Text, drp_RejectionReason.SelectedItem.Text, txt_Comments.Text, Page.Session["DisplayRequestId"].ToString(), Common.Utilities.Constants.TechnicalCommitteeGroupName);
                            else
                                BL.Decisions.AddDecision(drp_FinalDecision.SelectedItem.Text, string.Empty, txt_Comments.Text, Page.Session["DisplayRequestId"].ToString(), Common.Utilities.Constants.TechnicalCommitteeGroupName);

                            switch (drp_FinalDecision.SelectedItem.Value)
                            {
                                case "1":
                                    NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCEHeadManagereAcceptDecisionTechnicalCommitte, txt_Comments.Text, "Requests", Page.Session["DisplayRequestId"].ToString());
                                    break;
                                case "2":
                                    NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCEHeadManagerRejectDecisionTechnicalCommittee, txt_Comments.Text, "Requests", Page.Session["DisplayRequestId"].ToString());
                                    break;

                                default: break;
                            }
                            SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/TechCommitteDecisions.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                        }

                        else if (Session["ActionTaken"] != null && Session["ActionTaken"].ToString() == "AddNewHeadManagerDecision")
                        {
                            if (drp_RejectionReason.SelectedIndex != 0)
                                BL.Decisions.AddDecision(drp_FinalDecision.SelectedItem.Text, drp_RejectionReason.SelectedItem.Text, txt_Comments.Text, Page.Session["DisplayRequestId"].ToString(), Common.Utilities.Constants.DepartmentManagerGroupName);
                            else
                                BL.Decisions.AddDecision(drp_FinalDecision.SelectedItem.Text, string.Empty, txt_Comments.Text, Page.Session["DisplayRequestId"].ToString(), Common.Utilities.Constants.DepartmentManagerGroupName);

                            BL.AllProcedures.UpdateRecommendationStatusDecision(hdn_ProcID.Value, txt_DecisiontxtForPrinting.Text, txtOccupationName.Text, txt_bookNum.Text, txt_bookDate.Text, lbl_headManagerName.Text);

                            int requestStatus = 0;

                            switch (drp_FinalDecision.SelectedItem.Value)
                            {
                                case "1":
                                    NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCEHeadManagerAccepted, txt_Comments.Text, "Requests", Page.Session["DisplayRequestId"].ToString());
                                    Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.Requests, Utilities.Constants.RequestHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.UCEHeadManagerAccepted, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["DisplayRequestId"].ToString(), "No");
                                    requestStatus = (int)Common.Utilities.RequestStatus.UCEHeadManagerAccepted;
                                    break;
                                case "2":
                                    NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCEHeadManagerRejected, txt_Comments.Text, "Requests", Page.Session["DisplayRequestId"].ToString());
                                    Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.Requests, Utilities.Constants.RequestHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.UCEHeadManagerRejected, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["DisplayRequestId"].ToString(), "No");
                                    requestStatus = (int)Common.Utilities.RequestStatus.UCEHeadManagerRejected;
                                    break;

                                default: break;
                            }
                            BL.Request.UpdateRejection(new Entities.Request()
                            {
                                ID = Convert.ToInt32(Page.Session["DisplayRequestId"]),
                                RejectedFrom = SPContext.Current.Web.CurrentUser.Name,
                                RejectionReason = "ReturnFromHM",
                                RejectionDate = DateTime.Now
                            });
                            if (requestStatus != 0)
                            {
                                var request = BL.Request.GetUniversityRequestData(Convert.ToInt32(Page.Session["DisplayRequestId"].ToString()), LCID);
                                string universityID = request.University != null ? request.University.ID.ToString() : string.Empty;


                                string smsBody = string.Empty;
                                string mailBody = string.Empty;
                                string mailSubject = string.Empty;
                                string arabicAcademicDegree = string.Empty;
                                string englishAcademicDegree = string.Empty;

                                if (request.AcademicDegree != null)
                                {
                                    arabicAcademicDegree = request.AcademicDegree.ArabicTitle;
                                    englishAcademicDegree = request.AcademicDegree.EnglishTitle;
                                }

                                var mail = BLCommon.Notifications.GetSubmittedNotification((int)UtilitiesCommon.NotificationType.Email, requestStatus);

                                if (mail != null)
                                {
                                    mailBody = string.Format(mail.Body, request.RequestNumber, englishAcademicDegree, request.RequestNumber, arabicAcademicDegree);
                                    mailSubject = mail.Subject;
                                }

                                var sms = BLCommon.Notifications.GetSubmittedNotification((int)UtilitiesCommon.NotificationType.SMS, requestStatus);

                                if (sms != null)
                                    smsBody = string.Format(sms.Body, request.RequestNumber, englishAcademicDegree, request.RequestNumber, arabicAcademicDegree);

                                if (!(string.IsNullOrEmpty(mailSubject) || string.IsNullOrEmpty(mailBody) || string.IsNullOrEmpty(smsBody)))
                                    BL.Request.notifyApplicant(request.ApplicantID.ID, universityID, mailBody, smsBody, mailSubject);
                            }
                        }
                    }
                //}
               
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_FinalDecision.btn_ApproveDecision_Click");
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/HeadManagerViewDecision.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
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
                        string EntityNeedsEquivalency = string.Empty;
                        string ApplicantName = string.Empty;
                        string PersonalID = string.Empty; 

                        if (request["EntityNeedsEquivalencyAr"] != null)
                        {
                            SPFieldLookupValue EntityNeedsEquivalencylkp = new SPFieldLookupValue(request["EntityNeedsEquivalencyAr"].ToString());
                            EntityNeedsEquivalency = EntityNeedsEquivalencylkp.LookupValue;
                        }
                        else if (request["OtherEntityNeedsEquivalency"] != null)
                        {
                            EntityNeedsEquivalency = request["OtherEntityNeedsEquivalency"].ToString();
                        }
                        else if (request["EntityWorkingForAr"] != null)
                        {
                            SPFieldLookupValue EntityWorkingForlkp = new SPFieldLookupValue(request["EntityWorkingForAr"].ToString());
                            EntityNeedsEquivalency = EntityWorkingForlkp.LookupValue;
                        }
                        else if (request["OtherEntityWorkingFor"] != null)
                        {
                            EntityNeedsEquivalency = request["OtherEntityWorkingFor"].ToString();
                        }

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
                        
                        lbl_RemainingDecicionBoby.Text = string.Format(lbl_RemainingDecicionBoby.Text, applicantGender, ApplicantName, PersonalID);                    
                        lbl_EntityNeedsEquivalency.Text =  EntityNeedsEquivalency;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
        }
        private void Binddrp_Decisions()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_FinalDecision.btn_ApproveProcedures_Click");

                if (HelperMethods.InGroup(Common.Utilities.Constants.AssistUndersecretaryGroupName))
                {
                    List<Entities.UCEEmployeeDecisions> uCEEmployeeDecisions = BL.UCEEmployeeDecisions.GetAllByGroupName(Common.Utilities.Constants.AssistUndersecretaryGroupName);

                    HelperMethods.BindDropDownList(ref drp_FinalDecision, uCEEmployeeDecisions, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }
                else if (HelperMethods.InGroup(Common.Utilities.Constants.TechnicalCommitteeGroupName))
                {
                    List<Entities.UCEEmployeeDecisions> uCEEmployeeDecisions = BL.UCEEmployeeDecisions.GetAllByGroupName(Common.Utilities.Constants.TechnicalCommitteeGroupName);

                    HelperMethods.BindDropDownList(ref drp_FinalDecision, uCEEmployeeDecisions, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }
                else if (HelperMethods.InGroup(Common.Utilities.Constants.DepartmentManagerGroupName))
                {
                    List<Entities.UCEEmployeeDecisions> uCEEmployeeDecisions = BL.UCEEmployeeDecisions.GetAllByGroupName(Common.Utilities.Constants.DepartmentManagerGroupName);

                    HelperMethods.BindDropDownList(ref drp_FinalDecision, uCEEmployeeDecisions, "ID", "ArabicTitle", "EnglishTitle", LCID);
                    if (Page.Session["DisplayRequestId"] != null)
                    {
                        List<Entities.Procedures> RecommendProc = BL.AllProcedures.GetApprovedRecommendationStatus(Page.Session["DisplayRequestId"].ToString(), "Approved");
                        if (RecommendProc.Count > 0)
                        {
                            txt_DecisiontxtForPrinting.Text = SPHttpUtility.HtmlDecode(RecommendProc[0].DecisionForPrint);
                            txt_bookNum.Text = RecommendProc[0].BookNum;
                            txt_bookDate.Text = ExtensionMethods.QatarFormatedDate(Convert.ToDateTime(RecommendProc[0].BookDate));
                            lbl_SirValue.Text = !string.IsNullOrEmpty(RecommendProc[0].SirValue) ? RecommendProc[0].SirValue : HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE", "sir").ToString();
                            lbl_RespectedValue.Text = !string.IsNullOrEmpty(RecommendProc[0].RespectedValue) ? RecommendProc[0].RespectedValue : HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE", "recpected").ToString();
                            txtOccupationName.Text = RecommendProc[0].OccupationName;
                            hdn_ProcID.Value = RecommendProc[0].ID;
                            div_DecisionforPrint.Visible = true;
                            using (SPWeb web = new SPSite(SPContext.Current.Site.Url).OpenWeb())
                            {
                                SPList reqs = web.Lists[Utilities.Constants.Requests];
                                SPListItem item = reqs.GetItemById(Convert.ToInt32(Page.Session["DisplayRequestId"].ToString()));
                                hdn_RequestNumber.Value = item["RequestNumber"].ToString();
                            }
                            if (RecommendProc[0].Procedure == "Approved" || RecommendProc[0].Procedure == "بالموافقة")
                            {
                                lbl_decisionText.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "approveDecisionText", (uint)LCID);
                            }
                            else
                            {
                                lbl_decisionText.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "rejectDecisionText", (uint)LCID);
                            }
                            

                            _bindDecisionTemplate();
                        }
                    }
                }
                drp_FinalDecision.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), string.Empty));

                drp_FinalDecision.AppendDataBoundItems = true;
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method AssistSecretaryProceduresWPUserControl.btn_ApproveProcedures_Click");
            }
        }

        private void BindRejectionReason()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_FinalDecision.btn_ApproveProcedures_Click");

                drp_RejectionReason.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                drp_RejectionReason.Items.Insert(1, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "AttachmentNotCompleted", (uint)LCID), "0"));
                drp_RejectionReason.Items.Insert(2, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NotRecognized", (uint)LCID), "1"));
                drp_RejectionReason.Items.Insert(3, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ForgedCertificate", (uint)LCID), "2"));
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method AssistSecretaryProceduresWPUserControl.btn_ApproveProcedures_Click");
            }
        }


    }
}