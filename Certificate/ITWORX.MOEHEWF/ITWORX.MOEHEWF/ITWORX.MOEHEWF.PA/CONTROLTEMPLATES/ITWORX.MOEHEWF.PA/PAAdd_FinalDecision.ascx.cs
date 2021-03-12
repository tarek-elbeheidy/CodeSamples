using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Nintex.Workflow.HumanApproval;
using ITWORX.MOEHEWF.UCE.Utilities;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using requestBL = ITWORX.MOEHEWF.PA.BL;
using BLCommon = ITWORX.MOEHEWF.Common.BL;
using UtilitiesCommon = ITWORX.MOEHEWF.Common.Utilities;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class Add_FinalDecision : Utilities.UserControlBase
    {
        public NintexContext CurrentContext { get; set; }
        public bool IsTaskComplete { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (Page.Session["PADisplayRequestId"] != null)
                {
                    SPSite site = new SPSite(SPContext.Current.Site.Url);
                    SPWeb web = site.OpenWeb();
                    SPListItem requestItem = web.Lists["PARequests"].GetItemById(int.Parse(Page.Session["PADisplayRequestId"].ToString()));
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
            }
        }

        protected void drp_FinalDecision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drp_FinalDecision.SelectedValue == "2" || drp_FinalDecision.SelectedValue == "4" || drp_FinalDecision.SelectedValue == "6")
            {
                drp_RejectionReason.Visible = true;
                lbl_RejectionReason.Visible = true;
            }
            else
            {
                drp_RejectionReason.Visible = false;
                lbl_RejectionReason.Visible = false;
            }
        }

        //temp change for the demo and deployment
        protected void btn_ApproveDecision_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_FinalDecision.btn_ApproveDecision_Click");

                if (Page.Session["PADisplayRequestId"] != null)
                {
                    if (drp_RejectionReason.SelectedIndex != 0)
                        BL.Decisions.AddDecision(drp_FinalDecision.SelectedItem.Text, drp_RejectionReason.SelectedItem.Text, txt_Comments.Text, Page.Session["PADisplayRequestId"].ToString(), Common.Utilities.Constants.DepartmentManagerGroupName);
                    else
                        BL.Decisions.AddDecision(drp_FinalDecision.SelectedItem.Text, string.Empty, txt_Comments.Text, Page.Session["PADisplayRequestId"].ToString(), Common.Utilities.Constants.DepartmentManagerGroupName);


                    int requestStatus = 0;
                    switch (drp_FinalDecision.SelectedItem.Value)
                    {
                        case "1":
                            NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAHeadManagerAccepted, txt_Comments.Text, "PARequests", Page.Session["PADisplayRequestId"].ToString());
                            Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests, Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PAHeadManagerAccepted, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["PADisplayRequestId"].ToString(), "No");
                            requestStatus = (int)Common.Utilities.RequestStatus.PAHeadManagerAccepted;
                            break; 
                        case "2":
                            NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAHeadManagerRejected, txt_Comments.Text, "PARequests", Page.Session["PADisplayRequestId"].ToString());
                            Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests, Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PAHeadManagerRejected, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["PADisplayRequestId"].ToString(), "No");
                            requestStatus = (int)Common.Utilities.RequestStatus.PAHeadManagerRejected;
                            break;
                        case "3":
                            NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAHeadEmployeeMissingInformation, txt_Comments.Text, "PARequests", Page.Session["PADisplayRequestId"].ToString());
                            Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests, Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PAHeadEmployeeMissingInformation, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["PADisplayRequestId"].ToString(), "No");

                            requestBL.Request.UpdateRejection(new Entities.PARequest()
                            {
                                ID = Convert.ToInt32(Page.Session["PADisplayRequestId"]),
                                RejectedFrom = SPContext.Current.Web.CurrentUser.Name,
                                RejectionReason = txt_Comments.Text,
                                RejectionDate = DateTime.Now
                            });
                            break;
                        default: break;
                    }
                    if (requestStatus != 0)
                    {
                        var request = BL.Request.GetUniversityRequestData(Convert.ToInt32(Page.Session["PADisplayRequestId"].ToString()), LCID);
                        string universityID = request.University != null ? request.University.ID.ToString() : string.Empty;


                        string smsBody = string.Empty;
                        string mailBody = string.Empty;
                        string mailSubject = string.Empty; 
                         

                        var mail = BLCommon.Notifications.GetSubmittedNotification((int)UtilitiesCommon.NotificationType.Email, requestStatus);

                        if (mail != null)
                        {
                            mailBody = string.Format(mail.Body, request.RequestNumber, request.RequestNumber);
                            mailSubject = mail.Subject;
                        }

                        var sms = BLCommon.Notifications.GetSubmittedNotification((int)UtilitiesCommon.NotificationType.SMS, requestStatus);

                        if (sms != null)
                            smsBody = string.Format(sms.Body, request.RequestNumber, request.RequestNumber);

                        if (!(string.IsNullOrEmpty(mailSubject) || string.IsNullOrEmpty(mailBody) || string.IsNullOrEmpty(smsBody)))
                            BL.Request.notifyApplicant(request.ApplicantID.ID, universityID, mailBody, smsBody, mailSubject);
                    }
                    
                } 
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_FinalDecision.btn_ApproveDecision_Click");
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/paHeadManagerDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
        }

        protected void ReviewDecisionClick(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_FinalDecision.btn_ReviewDecisionClick");
                if (Page.Session["PADisplayRequestId"] != null)
                {
                    var paRequest = requestBL.Request.GetRequestByNumber(Convert.ToInt32(Page.Session["PADisplayRequestId"]), LCID);
 
                    Common.BL.FinalDecisionPrint.ReviewPAPDF(paRequest.RequestNumber, "PDF", LCID);
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

        private void Binddrp_Decisions()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_FinalDecision.btn_ApproveProcedures_Click");


                List<Entities.PAEmployeeDecisions> PAEmployeeDecisions = BL.PAEmployeeDecisions.GetAllByGroupName(Common.Utilities.Constants.DepartmentManagerGroupName);

                HelperMethods.BindDropDownList(ref drp_FinalDecision, PAEmployeeDecisions, "ID", "ArabicTitle", "EnglishTitle", LCID);

                drp_FinalDecision.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), string.Empty));

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

                drp_RejectionReason.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));
                drp_RejectionReason.Items.Insert(1, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "AttachmentNotCompleted", (uint)LCID), "0"));
                drp_RejectionReason.Items.Insert(2, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NotRecognized", (uint)LCID), "1"));
                drp_RejectionReason.Items.Insert(3, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ForgedCertificate", (uint)LCID), "2"));
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