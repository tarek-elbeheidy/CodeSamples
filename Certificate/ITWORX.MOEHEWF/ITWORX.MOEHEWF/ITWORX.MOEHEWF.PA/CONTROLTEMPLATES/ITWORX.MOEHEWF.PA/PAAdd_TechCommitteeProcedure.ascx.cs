using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Nintex.Workflow.HumanApproval;
using ITWORX.MOEHEWF.UCE.Utilities;
using System;
using System.Web;
using System.Web.UI;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class Add_TechCommitteeProcedure : Utilities.UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload TechCommitteeAttachements;

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
                    SPListItem requestItem = web.Lists["Requests"].GetItemById(int.Parse(Page.Session["PADisplayRequestId"].ToString()));
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
                //Binddrp_Procedures();
                SPListItem requestItem = BL.Request.GetRequestItemByID(Convert.ToInt32(Page.Session["PADisplayRequestId"].ToString()));
                if (Convert.ToInt32(requestItem["RequestStatusId"].ToString()) == (int)Common.Utilities.RequestStatus.PATechnicalCommitteeReviewRecommendationFromHeadManager
                    || Convert.ToInt32(requestItem["RequestStatusId"].ToString()) == (int)Common.Utilities.RequestStatus.PATechnicalCommitteeReviewRecommendationAssistantUndersecretary)

                    hdn_SenderName.Value = requestItem["RequestStatusId"].ToString();
            }
            BindAttachements();
        }

        protected void btn_ApproveProcedures_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_TechCommitteeProcedure.btn_ApproveProcedures_Click");
                if (!IsRefresh)
                {
                    if (Page.Session["PADisplayRequestId"] != null)
                    {
                        if (hdn_SenderName.Value == "20")
                        {
                            BL.AllProcedures.AddProcedure(Utilities.Constants.PATechCommitteeProcedures, txt_Comments.Text, Page.Session["PADisplayRequestId"].ToString(), "ReturntoDepartmentManager", string.Empty);
                            NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAHeadManagerMissingInformationTechnicalCommittee, txt_Comments.Text, "PARequests", Page.Session["PADisplayRequestId"].ToString());
                        }
                        else if (hdn_SenderName.Value == "25")
                        {
                            BL.AllProcedures.AddProcedure(Utilities.Constants.PATechCommitteeProcedures, txt_Comments.Text, Page.Session["PADisplayRequestId"].ToString(), "ReturntoAssistUnderSecretary", string.Empty);
                            NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAAssistantUndersecretaryMissingInformation, txt_Comments.Text, "PARequests", Page.Session["PADisplayRequestId"].ToString());
                        }
                        TechCommitteeAttachements.SaveAttachments();
                        //switch (drp_Procedure.SelectedItem.Value)
                        //{
                        //    case "12":
                        //        NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAHeadManagerMissingInformationTechnicalCommittee, txt_Comments.Text);
                        //        //Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.PAHeadManagerMissingInformationTechnicalCommittee, Page.Session["PADisplayRequestId"].ToString(), Utilities.Constants.DepartmentManagerGroupName.ToLower());
                        //        break;
                        //    case "13":
                        //        NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAAssistantUndersecretaryMissingInformation, txt_Comments.Text);
                        //        //Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.PAAssistantUndersecretaryMissingInformation, Page.Session["PADisplayRequestId"].ToString(), Utilities.Constants.AssistUndersecretaryGroupName.ToLower());
                        //        break;
                        //    default: break;
                        //}
                        //Response.Redirect((LCID == 1033 ? "/en" : "/ar") + "/Pages/PAProceduresTechCommitteListing.aspx");
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAProceduresTechCommitteListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_TechCommitteeProcedure.btn_ApproveProcedures_Click");
            }
        }

        //private void Binddrp_Procedures()
        //{
        //    try
        //    {
        //        Logging.GetInstance().Debug("Entering method Add_TechCommitteeProcedure.Binddrp_Procedures");

        //        //drp_Procedure.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));
        //        //drp_Procedure.Items.Insert(1, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ReturntoDepartmentManager", (uint)LCID), "0"));
        //        //drp_Procedure.Items.Insert(2, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ReturntoAssistUnderSecretary", (uint)LCID), "1"));

        //        List<Entities.PAEmployeeProcedures> PAEmployeeProcedures = BL.PAEmployeeProcedures.GetAllByGroupName(Utilities.Constants.TechnicalCommitteeGroupName);

        //        //HelperMethods.BindDropDownList(ref drp_Procedure, PAEmployeeProcedures, "ID", "ArabicTitle", "EnglishTitle", LCID);

        //        //drp_Procedure.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), string.Empty));

        //        //drp_Procedure.AppendDataBoundItems = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.GetInstance().LogException(ex);
        //    }
        //    finally
        //    {
        //        Logging.GetInstance().Debug("Exiting method Add_TechCommitteeProcedure.Binddrp_Procedures");
        //    }
        //}

        private void BindAttachements()
        {
            #region Prerequiestes

            /// add colum "RequestID" lookup from Requests
            /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
            /// add column Group, single line of text

            #endregion Prerequiestes

            #region Edit Mode

            TechCommitteeAttachements.DocumentLibraryName = Utilities.Constants.TechCommitteeAttachements;
            TechCommitteeAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
            TechCommitteeAttachements.MaxSize = 10240000;//10MB
            TechCommitteeAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NotesPrepared", (uint)LCID);
            TechCommitteeAttachements.Group = "TechCommitteeAttachements";// field name for example, shouldn't be used for more than one field per each control.
            TechCommitteeAttachements.RequestID = Convert.ToInt32(Page.Session["PADisplayRequestId"].ToString());
            TechCommitteeAttachements.SupportedExtensions = "PNG,PDF,JPG";
            TechCommitteeAttachements.IsRequired = false;
            TechCommitteeAttachements.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
            TechCommitteeAttachements.Enabled = true;

            TechCommitteeAttachements.Bind();

            #endregion Edit Mode
        }
    }
}