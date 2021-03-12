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

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class Add_AssistSecretaryProc : Utilities.UserControlBase
    {
        public NintexContext CurrentContext { get; set; }
        public bool IsTaskComplete { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Binddrp_Procedures();
                BindRejectionReason();
            }
        }

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

        protected void drp_Procedure_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drp_Procedure.SelectedValue == "11")
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

        protected void btn_ApproveProcedures_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_AssistSecretaryProcUC.btn_ApproveProcedures_Click");
                if (!IsRefresh)
                {
                    if (Page.Session["PADisplayRequestId"] != null)
                    {
                        if (drp_RejectionReason.SelectedIndex != 0)
                            BL.AllProcedures.AddProcedure(Utilities.Constants.PAAssistSecretaryProcedures, txt_Comments.Text, Page.Session["PADisplayRequestId"].ToString(), drp_Procedure.SelectedItem.Text, drp_RejectionReason.SelectedItem.Text);
                        else
                            BL.AllProcedures.AddProcedure(Utilities.Constants.PAAssistSecretaryProcedures, txt_Comments.Text, Page.Session["PADisplayRequestId"].ToString(), drp_Procedure.SelectedItem.Text, string.Empty);

                        switch (drp_Procedure.SelectedItem.Value)
                        {
                            //case "0":
                            //    NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAProgramManagerMissingRecommendationFromAssistantUndersecretary, txt_Comments.Text);
                            //    //Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.PAProgramManagerMissingRecommendationFromAssistantUndersecretary, Page.Session["PADisplayRequestId"].ToString(), "Program Managers".ToLower());
                            //    break;
                            case "10":
                                NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAAssistantUndersecretaryAccepted, txt_Comments.Text, "PARequests", Page.Session["PADisplayRequestId"].ToString());
                                //Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.PAAssistantUndersecretaryAccepted, Page.Session["PADisplayRequestId"].ToString(), "Arabian and Asian Program Employees".ToLower());
                                break;

                            case "11":
                                NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAAssistantUndersecretaryRejected, txt_Comments.Text, "PARequests", Page.Session["PADisplayRequestId"].ToString());
                                //Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.PAAssistantUndersecretaryRejected, Page.Session["PADisplayRequestId"].ToString(), "Arabian and Asian Program Employees".ToLower());
                                break;

                            case "3":
                                NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PATechnicalCommitteeReviewRecommendationAssistantUndersecretary, txt_Comments.Text, "PARequests", Page.Session["PADisplayRequestId"].ToString());
                                //Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.PATechnicalCommitteeReviewRecommendationAssistantUndersecretary, Page.Session["PADisplayRequestId"].ToString(), "Technical Committee Employees".ToLower());
                                break;

                            default: break;
                        }

                        //Response.Redirect((LCID == 1033 ? "/en" : "/ar") + "/Pages/PAProceduresAssistSecretaryListing.aspx");
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAProceduresAssistSecretaryListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_AssistSecretaryProcUC.btn_ApproveProcedures_Click");
            }
        }

        private void Binddrp_Procedures()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_AssistSecretaryProcUC.Binddrp_Procedures");

                //drp_Procedure.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));
                //drp_Procedure.Items.Insert(1, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ReturntoDeptManager", (uint)LCID), "0"));
                //drp_Procedure.Items.Insert(2, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Approved", (uint)LCID), "1"));
                //drp_Procedure.Items.Insert(3, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Rejected", (uint)LCID), "2"));
                //drp_Procedure.Items.Insert(4, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "TransfertoConsultCommittee", (uint)LCID), "3"));

                List<Entities.PAEmployeeProcedures> PAEmployeeProcedures = BL.PAEmployeeProcedures.GetAllByGroupName(Common.Utilities.Constants.AssistUndersecretaryGroupName);

                HelperMethods.BindDropDownList(ref drp_Procedure, PAEmployeeProcedures, "ID", "ArabicTitle", "EnglishTitle", LCID);

                drp_Procedure.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), string.Empty));

                drp_Procedure.AppendDataBoundItems = true;
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_AssistSecretaryProcUC.Binddrp_Procedures");
            }
        }

        private void BindRejectionReason()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_AssistSecretaryProcUC.BindRejectionReason");

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
                Logging.GetInstance().Debug("Exiting method Add_AssistSecretaryProcUC.BindRejectionReason");
            }
        }
    }
}