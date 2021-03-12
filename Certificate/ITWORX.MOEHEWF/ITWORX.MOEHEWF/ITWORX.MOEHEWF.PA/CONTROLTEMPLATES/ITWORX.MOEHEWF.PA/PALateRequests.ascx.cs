using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Entities;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Nintex.Workflow.HumanApproval;
using ITWORX.MOEHEWF.UCE.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class PALateRequests : Utilities.UserControlBase
    {
        #region Public Properties

        public string SPGroupName { get; set; }
        public NintexContext CurrentContext { get; set; }
        public bool IsTaskComplete { get; set; }

        #endregion Public Properties

        protected void Page_Load(object sender, EventArgs e)
        {
            var hdnCtl = Page.Request.Form.Get("__EventTriggerControlIdPALate");
            if (string.IsNullOrEmpty(hdnCtl))
            {
                grd_LateRequests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "DashboardPageSize"));
                BindGrid();
            }
        }

        private void BindProgramEmployees()
        {
            List<SPUser> Araibcusers = HelperMethods.GetGroupUsers(Common.Utilities.Constants.ArabicProgEmployeeGroupName);
            List<SPUser> Europeancusers = HelperMethods.GetGroupUsers(Common.Utilities.Constants.EuropeanProgEmployeeGroupName);
 
        }

        private void BindGrid()
        {
            try
            {
                Logging.GetInstance().Debug("Enter LateRequests.BindGrid");

                List<SimilarRequest> Requests = BL.LateRequests.GetAllLateRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.LateRequests.GetLateQueryPerRole(), "And", true)
                    + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();
                Requests = Requests.Where(x => x.AssignedTo.ToLower().Equals(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == SPGroupName.ToLower()).ToList();
                HelperMethods.BindGridView(grd_LateRequests, Requests);
                if (Requests.Count > 0)
                {
                    lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NoOfRequests", (uint)LCID) + Requests.Count;
                    lbl_NoOfRequests.Visible = true;
                }
                else
                    lbl_NoOfRequests.Visible = false;
                bool isExist = HelperMethods.InGroup(Common.Utilities.Constants.ProgramManagerGroupName);
                if (isExist && Requests.Count > 0)
                    BindProgramEmployees();
 
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit LateRequests.BindGrid");
            }
        }

        protected void grd_LateRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bool IsGroupUser = false;
            Logging.GetInstance().Debug("Enter LateRequests.grd_LateRequests_PageIndexChanging");
            try
            {
                bool isExist = HelperMethods.InGroup(Common.Utilities.Constants.ProgramManagerGroupName);
                 
                grd_LateRequests.PageIndex = e.NewPageIndex;
                BindGrid(); 
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit LateRequests.grd_LateRequests_PageIndexChanging");
            }
        }

 
 

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter LateRequests.lnk_Edit_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_RequestID = (HiddenField)gvr.FindControl("hdn_ID");
                Page.Session["PADisplayRequestId"] = hdn_RequestID.Value;
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdn_RequestStatusId");
                HiddenField hdn_QID = (HiddenField)gvr.FindControl("hdn_QID");
                Page.Session["PAQID"] = hdn_QID.Value;
                Common.Entities.RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                string editLink = string.Empty;
                if (requestStatus.CanReviewerEditRequest)
                    editLink = requestStatus.ReviewerTargetPageURL;

                SPUtility.Redirect(SPContext.Current.Web.Url + editLink, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit LateRequests.lnk_Edit_Click");
            }
        }

        private void PersistRowIndex(int index)
        {
            if (!SelectedRequestsIndex.Exists(i => i == index))
            {
                SelectedRequestsIndex.Add(index);
            }
        }

        private void RemoveRowIndex(int index)
        {
            SelectedRequestsIndex.Remove(index);
        }

        private List<Int32> SelectedRequestsIndex
        {
            get
            {
                if (ViewState["SELECTED_Requests_INDEX"] == null)
                {
                    ViewState["SELECTED_Requests_INDEX"] = new List<Int32>();
                }

                return (List<Int32>)ViewState["SELECTED_Requests_INDEX"];
            }
        }
         

        protected void lnk_AssignTo_Click(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            List<string> reqIDstr = new List<string>();
                            Logging.GetInstance().Debug("Entering method LateRequests.lnk_AssignTo_Click");
                            if (!IsRefresh)
                            {
                                SPList list = web.Lists[Utilities.Constants.PARequests];
                                foreach (GridViewRow row in grd_LateRequests.Rows)
                                {
                                    if (row.RowType == DataControlRowType.DataRow)
                                    {
                                        CheckBox chkRow = (row.FindControl("chkbox_Select") as CheckBox);
                                        if (chkRow.Checked)
                                        {
                                            HiddenField reqID = (row.FindControl("hdn_ID") as HiddenField);
                                            reqIDstr.Add(reqID.Value);
                                        }
                                    }
                                }
                                foreach (int id in SelectedRequestsIndex)
                                {
                                    reqIDstr.Add(id.ToString());
                                }
                                foreach (var str in reqIDstr)
                                {

                                    SPListItem requestItem = web.Lists["PARequests"].GetItemById(int.Parse(str));
                                    SPList tasksList = web.Lists.TryGetList("Workflow Tasks");
                                    Guid taskListId = new Guid(tasksList.ID.ToString());
                                    int spTaskItemId = requestItem.Tasks[0].ID;
                                    // create Nintext context
                                    CurrentContext = NintexHelper.ParseRequest(spTaskItemId, taskListId);
                                    CurrentContext.TaskAuthorized = Nintex.Workflow.HumanApproval.User.CheckCurrentUserMatchesHWUser(SPContext.Current.Web, CurrentContext.Approver);
                                    // check if a task has already completed
                                    IsTaskComplete = (CurrentContext.Approver.ApprovalOutcome != Outcome.Pending);

                                    NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAProgramEmployeeReview, "", "PARequests", str);

                                }
                                list.Update();
                             }
                            BindGrid();
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            web.AllowUnsafeUpdates = false;
                            Logging.GetInstance().Debug("Exiting method LateRequests.lnk_AssignTo_Click");
                        }
                    }
                }
            });
        }
    }
}