using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Nintex.Workflow.HumanApproval;
using ITWORX.MOEHEWF.UCE.Utilities;
using requestBL = ITWORX.MOEHEWF.PA.BL;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class Add_NewRecommendation : Utilities.UserControlBase
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
                BinddrpPARecommendations();
        }

        protected void btn_ApproveRecommendation_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_NewRecommendation.btn_ApproveRecommendation_Click");
                if (!IsRefresh)
                {
                    if (Page.Session["PADisplayRequestId"] != null)
                    {
                        BL.PARecommendations.AddRecommendation(drp_PARecommendations.SelectedItem.Text, txt_Comments.Text, Page.Session["PADisplayRequestId"].ToString());
                        switch (drp_PARecommendations.SelectedItem.Value)
                        {
                            case "1":
                                NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAProgramManagerAcceptance, txt_Comments.Text, "PARequests", Page.Session["PADisplayRequestId"].ToString());
                                Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests, Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PAProgramManagerAcceptance, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["PADisplayRequestId"].ToString(), "No");

                                break;
                            case "2":
                                NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAProgramManagerReject, txt_Comments.Text, "PARequests", Page.Session["PADisplayRequestId"].ToString());
                                Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests, Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PAProgramManagerReject, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["PADisplayRequestId"].ToString(), "No");

                                break;
                            case "3":
                                NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAProgramEmployeeMissingInformation, txt_Comments.Text, "PARequests", Page.Session["PADisplayRequestId"].ToString());
                                Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests, Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PAProgramEmployeeMissingInformation, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["PADisplayRequestId"].ToString(), "No");

                                requestBL.Request.UpdateRejection(new Entities.PARequest() {ID=Convert.ToInt32(Page.Session["PADisplayRequestId"]),
                                                                                            RejectedFrom= SPContext.Current.Web.CurrentUser.Name,
                                                                                            RejectionReason= txt_Comments.Text,
                                                                                            RejectionDate=DateTime.Now});
                                break;
                            default: break;
                        }
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAProgramManagerDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
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
        private void BinddrpPARecommendations()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_NewRecommendation.BinddrpPARecommendations");

                
                drp_PARecommendations.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));
                drp_PARecommendations.Items.Insert(1, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Approved", (uint)LCID), "1"));
                drp_PARecommendations.Items.Insert(2, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Rejected", (uint)LCID), "2"));
                drp_PARecommendations.Items.Insert(3, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "returnToEmployee", (uint)LCID), "3"));
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method Add_NewRecommendation.BinddrpPARecommendations");
            }
        }
    }
}