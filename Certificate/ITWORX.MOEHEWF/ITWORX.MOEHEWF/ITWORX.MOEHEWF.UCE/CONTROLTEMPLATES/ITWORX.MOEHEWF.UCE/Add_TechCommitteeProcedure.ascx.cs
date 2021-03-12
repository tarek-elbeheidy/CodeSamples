using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Nintex.Workflow.HumanApproval;
using ITWORX.MOEHEWF.UCE.Utilities;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class Add_TechCommitteeProcedure : UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload TechCommitteeAttachements;

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
                //Binddrp_Procedures();
                SPListItem requestItem = BL.Request.GetRequestItemByID(Convert.ToInt32(Page.Session["DisplayRequestId"].ToString()));
                if (Convert.ToInt32(requestItem["RequestStatusId"].ToString()) == (int)Common.Utilities.RequestStatus.UCETechnicalCommitteeReviewRecommendationFromHeadManager
                    || Convert.ToInt32(requestItem["RequestStatusId"].ToString()) == (int)Common.Utilities.RequestStatus.UCETechnicalCommitteeReviewRecommendationAssistantUndersecretary)

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
                    if (Page.Session["DisplayRequestId"] != null)
                    {if (hdn_SenderName.Value == "20")
                        {
                            BL.AllProcedures.AddProcedure(Utilities.Constants.TechCommitteeProcedures, txt_Comments.Text, Page.Session["DisplayRequestId"].ToString(), "ReturntoDepartmentManager", string.Empty);
                            NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCEHeadManagerMissingInformationTechnicalCommittee, txt_Comments.Text, "Requests", Page.Session["DisplayRequestId"].ToString());

                        }
                        else if (hdn_SenderName.Value == "25")
                        {
                            BL.AllProcedures.AddProcedure(Utilities.Constants.TechCommitteeProcedures, txt_Comments.Text, Page.Session["DisplayRequestId"].ToString(), "ReturntoAssistUnderSecretary", string.Empty);
                            NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCEAssistantUndersecretaryMissingInformation, txt_Comments.Text, "Requests", Page.Session["DisplayRequestId"].ToString());
                        }
                        TechCommitteeAttachements.SaveAttachments();
                        //switch (drp_Procedure.SelectedItem.Value)
                        //{
                        //    case "12":
                        //        NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCEHeadManagerMissingInformationTechnicalCommittee, txt_Comments.Text);
                        //        //Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.UCEHeadManagerMissingInformationTechnicalCommittee, Page.Session["DisplayRequestId"].ToString(), Utilities.Constants.DepartmentManagerGroupName.ToLower());
                        //        break;
                        //    case "13":
                        //        NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCEAssistantUndersecretaryMissingInformation, txt_Comments.Text);
                        //        //Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.UCEAssistantUndersecretaryMissingInformation, Page.Session["DisplayRequestId"].ToString(), Utilities.Constants.AssistUndersecretaryGroupName.ToLower());
                        //        break;
                        //    default: break;
                        //}
                                //Response.Redirect((LCID == 1033 ? "/en" : "/ar") + "/Pages/ProceduresTechCommitteListing.aspx");
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ProceduresTechCommitteListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);


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

        //        //drp_Procedure.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX.MOEHEWF.UCE", "ChooseValue", (uint)LCID), "-1"));
        //        //drp_Procedure.Items.Insert(1, new ListItem(HelperMethods.LocalizedText("ITWORX.MOEHEWF.UCE", "ReturntoDepartmentManager", (uint)LCID), "0"));
        //        //drp_Procedure.Items.Insert(2, new ListItem(HelperMethods.LocalizedText("ITWORX.MOEHEWF.UCE", "ReturntoAssistUnderSecretary", (uint)LCID), "1"));

        //        List<Entities.UCEEmployeeProcedures> uCEEmployeeProcedures = BL.UCEEmployeeProcedures.GetAllByGroupName(Utilities.Constants.TechnicalCommitteeGroupName);

        //        //HelperMethods.BindDropDownList(ref drp_Procedure, uCEEmployeeProcedures, "ID", "ArabicTitle", "EnglishTitle", LCID);

        //        //drp_Procedure.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX.MOEHEWF.UCE", "ChooseValue", (uint)LCID), string.Empty));

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
            #endregion
            #region Edit Mode
            TechCommitteeAttachements.DocumentLibraryName = Utilities.Constants.TechCommitteeAttachements;
            TechCommitteeAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
            TechCommitteeAttachements.MaxSize = 7168000;//2MB
            TechCommitteeAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NotesPrepared", (uint)LCID);
            TechCommitteeAttachements.Group = "TechCommitteeAttachements";// field name for example, shouldn't be used for more than one field per each control.
            TechCommitteeAttachements.RequestID = Convert.ToInt32(Page.Session["DisplayRequestId"].ToString());
            TechCommitteeAttachements.SupportedExtensions = "PNG,PDF,JPG";
            TechCommitteeAttachements.IsRequired = false;
            TechCommitteeAttachements.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
            TechCommitteeAttachements.Enabled = true;

            TechCommitteeAttachements.Bind();
            #endregion
        }
    }
}