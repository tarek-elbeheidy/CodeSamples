using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Nintex.Workflow.HumanApproval;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class Add_NewRecommendation : UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload RecommProgramManagerAttachements;

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
                BinddrpRecommendations();
            BindAttachements();
        }

        private void BindAttachements()
        {
            #region Prerequiestes

            /// add colum "RequestID" lookup from Requests
            /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
            /// add column Group, single line of text

            #endregion Prerequiestes

            #region Edit Mode

            RecommProgramManagerAttachements.DocumentLibraryName = Utilities.Constants.ProgramManagerRecommendationAttachements;
            RecommProgramManagerAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
            RecommProgramManagerAttachements.MaxSize = 7168000;//2MB
            RecommProgramManagerAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Attachments", (uint)LCID);
            RecommProgramManagerAttachements.Group = "ProgramManagerRecommendation";// field name for example, shouldn't be used for more than one field per each control.
            RecommProgramManagerAttachements.RequestID = Convert.ToInt32(Page.Session["DisplayRequestId"].ToString());
            RecommProgramManagerAttachements.SupportedExtensions = "PNG,PDF,JPG";
            RecommProgramManagerAttachements.IsRequired = false;
            RecommProgramManagerAttachements.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
            RecommProgramManagerAttachements.Enabled = true;

            RecommProgramManagerAttachements.Bind();

            #endregion Edit Mode
        }

        protected void btn_ApproveRecommendation_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_NewRecommendation.btn_ApproveRecommendation_Click");
                if (!IsRefresh)
                {
                    if (Page.Session["DisplayRequestId"] != null)
                    {
                        BL.Recommendations.AddRecommendation(drp_Recommendations.SelectedItem.Text, txt_Comments.Text, Page.Session["DisplayRequestId"].ToString());
                        RecommProgramManagerAttachements.SaveAttachments();
                        NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCEHeadManagerReviewRecommendation, txt_Comments.Text, "Requests", Page.Session["DisplayRequestId"].ToString());
                        Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.Requests, Utilities.Constants.RequestHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.UCEHeadManagerReviewRecommendation, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["DisplayRequestId"].ToString(), "No");

                        //Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.UCEHeadManagerReviewRecommendation, Page.Session["DisplayRequestId"].ToString(), "Head Managers".ToLower());
                        //Response.Redirect((LCID == 1033 ? "/en" : "/ar") + "/Pages/ProgramManagerRecommendListing.aspx");
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ProgramManagerRecommendListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method Add_NewRecommendation.btn_ApproveRecommendation_Click");
            }
        }
        //temp change for the demo and deployment
        private void BinddrpRecommendations()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_NewRecommendation.BinddrpRecommendations");

                drp_Recommendations.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                //drp_Recommendations.Items.Insert(1, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "TransferToLegalAffairs", (uint)LCID), "0"));
                drp_Recommendations.Items.Insert(1, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Approved", (uint)LCID), "1"));
                drp_Recommendations.Items.Insert(2, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Rejected", (uint)LCID), "2"));
                //drp_Recommendations.Items.Insert(4, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "TransfertoCommittee", (uint)LCID), "3"));
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method Add_NewRecommendation.BinddrpRecommendations");
            }
        }
    }
}
