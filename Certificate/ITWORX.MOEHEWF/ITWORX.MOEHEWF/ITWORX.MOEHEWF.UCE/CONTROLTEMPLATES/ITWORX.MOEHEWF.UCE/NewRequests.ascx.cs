using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Entities;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Nintex.Workflow.HumanApproval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class NewRequests : Utilities.UserControlBase
    {
        #region Public Properties

        public string SPGroupName { get; set; }

        public NintexContext CurrentContext { get; set; }
        public bool IsTaskComplete { get; set; }

        #endregion Public Properties

        protected void Page_Load(object sender, EventArgs e)
        {
            var hdnCtl = Page.Request.Form.Get("__EventTriggerControlIdNew");
            if (string.IsNullOrEmpty(hdnCtl))
            {
                grd_NewRequests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "DashboardPageSize"));
                BindGrid();
                lbl_SuccessMsg.Visible = false;
            }
            //if (!Page.IsPostBack)
            //    BindGrid();
        }

        private void BindProgramEmployees()
        {
            drp_AssignTo.Items.Clear();
            List<SPUser> Araibcusers = HelperMethods.GetGroupUsers(Common.Utilities.Constants.ArabicProgEmployeeGroupName);
            foreach (SPUser user in Araibcusers)
            {
                drp_AssignTo.Items.Add(new ListItem(user.Name, user.LoginName));
            }

            drp_AssignTo.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
            EmpAssignTo.Visible = true;
        }

        private void BindGrid()
        {
            try
            {
                Logging.GetInstance().Debug("Enter NewRequests.BindGrid");
                List<SimilarRequest> Requests = BL.NewRequests.GetAllNewRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.NewRequests.GetNewQueryPerRole(SPGroupName), "Or", true) + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();
                int delayedDays = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "DelayedDays"));

                Requests = Requests.Where(x => (x.AssignedTo.ToLower().Equals(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == SPGroupName.ToLower()) && (string.IsNullOrEmpty(x.DelayedDays) || int.Parse(x.DelayedDays) < delayedDays)).ToList();

                HelperMethods.BindGridView(grd_NewRequests, Requests);
                if (Requests.Count > 0)
                {
                    lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NoOfRequests", (uint)LCID) + Requests.Count;
                    lbl_NoOfRequests.Visible = true;
                }
                else
                    lbl_NoOfRequests.Visible = false;

                if (Common.Utilities.Constants.ProgramManagerGroupName == SPGroupName && Requests.Count > 0)
                    BindProgramEmployees();
                else if (Common.Utilities.Constants.ProgramManagerGroupName != SPGroupName || (Common.Utilities.Constants.ProgramManagerGroupName == SPGroupName && Requests.Count == 0))
                    EmpAssignTo.Visible = false;
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewRequests.BindGrid");
            }
        }

        protected void grd_NewRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bool IsGroupUser = false;
            Logging.GetInstance().Debug("Enter NewRequests.grd_NewRequests_PageIndexChanging");
            try
            {
                if (Common.Utilities.Constants.ProgramManagerGroupName == SPGroupName)
                {
                    IsGroupUser = true;
                    foreach (GridViewRow row in grd_NewRequests.Rows)
                    {
                        var chkBox = row.FindControl("chkbox_Select") as CheckBox;
                        HiddenField reqID = (row.FindControl("hdn_ID") as HiddenField);
                        if (chkBox.Checked)
                        {
                            PersistRowIndex(Convert.ToInt32(reqID.Value));
                        }
                        else
                        {
                            RemoveRowIndex(Convert.ToInt32(reqID.Value));
                        }
                    }
                }
                grd_NewRequests.PageIndex = e.NewPageIndex;
                BindGrid();
                if (IsGroupUser)
                    RePopulateCheckBoxes();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewRequests.grd_NewRequests_PageIndexChanging");
            }
        }

        protected void grd_NewRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter NewRequests.grd_NewRequests_RowDataBound");
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField IsClosed = (HiddenField)e.Row.FindControl("hdn_IsClosed");
                    HiddenField AssignedTo = (HiddenField)e.Row.FindControl("hdn_AssignedTo");
                    HiddenField StatusID = (HiddenField)e.Row.FindControl("hdn_RequestStatusId");
                    CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkbox_Select");
                    LinkButton btnEdit = (LinkButton)e.Row.FindControl("lnk_Edit");
                    LinkButton lnk_Notes = (LinkButton)e.Row.FindControl("lnk_Notes");
                    LinkButton btnCheckOut = (LinkButton)e.Row.FindControl("lnk_CheckOut");
                    Label lbl_HEDD = (Label)e.Row.FindControl("lbl_HEDD");

                    

                    SimilarRequest req =  e.Row.DataItem as SimilarRequest;
                    if (req != null)
                    {
                        bool isHEDD =  BL.UniversityLookup.ReturnIsUniversityHEDD(req.UniversityId);
                        if (isHEDD)
                        {
                            lbl_HEDD.Text = "HEDD";
                            lbl_HEDD.BackColor = System.Drawing.Color.Yellow;
                        }
                        else
                        {
                            lbl_HEDD.Visible = false;
                        }

                        if (lnk_Notes != null)
                            lnk_Notes.Visible = !string.IsNullOrEmpty(req.Note);


                    }
                    if (Common.Utilities.Constants.ProgramManagerGroupName == SPGroupName)
                        grd_NewRequests.Columns[0].Visible = true;
                    else
                        grd_NewRequests.Columns[0].Visible = false;

                    if (!Convert.ToBoolean(IsClosed.Value))
                    {
                        if ((Convert.ToInt32(StatusID.Value)) == (int)Common.Utilities.RequestStatus.UCESubmitted)
                        {
                            btnEdit.Visible = false;
                            btnCheckOut.Visible = true;
                        }
                        else
                        {
                            btnEdit.Visible = true;
                            btnCheckOut.Visible = false;
                        }
                    }
                    else
                    {
                        btnEdit.Visible = false;
                        btnCheckOut.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewRequests.grd_NewRequests_RowDataBound");
            }
        }

        protected void lnk_CheckOut_Click(object sender, EventArgs e)
        {
            LinkButton lnkButton = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
            HiddenField hdnField = (HiddenField)gvr.FindControl("hdn_ID");
            BL.NewRequests.CheckOutRequest(hdnField.Value);
            BindGrid();
        }

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter NewRequests.lnk_Edit_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_RequestID = (HiddenField)gvr.FindControl("hdn_ID");
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdn_RequestStatusId");
                Common.Entities.RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                string editLink = string.Empty;
                if (requestStatus.CanReviewerEditRequest)
                    editLink = requestStatus.ReviewerTargetPageURL;

                Page.Session["DisplayRequestId"] = hdn_RequestID.Value;
                SPUtility.Redirect(SPContext.Current.Web.Url + editLink, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewRequests.lnk_Edit_Click");
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

        private void RePopulateCheckBoxes()
        {
            foreach (GridViewRow row in grd_NewRequests.Rows)
            {
                var chkBox = row.FindControl("chkbox_Select") as CheckBox;
                HiddenField reqID = (row.FindControl("hdn_ID") as HiddenField);

                if (SelectedRequestsIndex != null)
                {
                    if (SelectedRequestsIndex.Exists(i => i == Convert.ToInt32(reqID.Value)))
                    {
                        chkBox.Checked = true;
                    }
                }
            }
        }

        protected void lnk_AssignTo_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> reqIDstr = new List<string>();
                Logging.GetInstance().Debug("Entering method NewRequests.lnk_AssignTo_Click");
                if (!IsRefresh)
                {
                    foreach (GridViewRow row in grd_NewRequests.Rows)
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
                    foreach (var id in reqIDstr)
                    {
                        if (drp_AssignTo.SelectedValue != "-1")
                        {
                            SPSecurity.RunWithElevatedPrivileges(delegate ()
                            {
                                BL.NewRequests.AssignTo(id, drp_AssignTo.SelectedValue);
                                SPSite site = new SPSite(SPContext.Current.Site.Url);
                                SPWeb web = site.OpenWeb();
                                SPListItem requestItem = web.Lists["Requests"].GetItemById(int.Parse(id));
                                SPList list = web.Lists.TryGetList("Workflow Tasks");
                                Guid taskListId = new Guid(list.ID.ToString());
                                int spTaskItemId = requestItem.Tasks[0].ID;
                                // create Nintext context
                                CurrentContext = NintexHelper.ParseRequest(spTaskItemId, taskListId);
                                CurrentContext.TaskAuthorized = Nintex.Workflow.HumanApproval.User.CheckCurrentUserMatchesHWUser(SPContext.Current.Web, CurrentContext.Approver);
                                // check if a task has already completed
                                IsTaskComplete = (CurrentContext.Approver.ApprovalOutcome != Outcome.Pending);
                                web.Dispose();
                                site.Dispose();
                                NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCEProgramEmployeeReview, "", "Requests", id);
                            });
                        }
                    }
                    if (drp_AssignTo.SelectedValue != "-1")
                        lbl_SuccessMsg.Visible = true;
                }
                BindGrid();
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ProgramManagerDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method NewRequests.lnk_AssignTo_Click");
            }
        }
    }
}