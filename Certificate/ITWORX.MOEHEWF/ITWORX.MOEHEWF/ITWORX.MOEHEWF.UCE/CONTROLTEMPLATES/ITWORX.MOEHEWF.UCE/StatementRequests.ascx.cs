using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class StatementRequests : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((HelperMethods.GetGroupUsers(Common.Utilities.Constants.CulturalMissionBritainGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName)) ||
                    (HelperMethods.GetGroupUsers(Common.Utilities.Constants.CulturalMissionAustraliaGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName)) ||
                    (HelperMethods.GetGroupUsers(Common.Utilities.Constants.CulturalMissionFranceGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName)) ||
                    (HelperMethods.GetGroupUsers(Common.Utilities.Constants.CulturalMissionUSAGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName)) ||
                    (HelperMethods.GetGroupUsers(Common.Utilities.Constants.CulturalMissionCanadaGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName)) ||
                    (HelperMethods.GetGroupUsers(Common.Utilities.Constants.CulturalMissionJordanGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName)) ||
                   (HelperMethods.GetGroupUsers(Common.Utilities.Constants.HigherEducationalInstitutionsGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName)))
                    grd_StatementRequests.Columns[6].HeaderText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Actions", (uint)LCID);
                else
                    grd_StatementRequests.Columns[6].HeaderText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "View", (uint)LCID);

                grd_StatementRequests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "StatementReqsPageSize"));

                if (Page.Session["DisplayRequestId"] != null)
                {
                    int requestId = int.Parse(Convert.ToString(Page.Session["DisplayRequestId"]));

                    SPListItem req = BL.Request.Reviewer_GetRequestItemByID(requestId);
                    string assignedTo = Convert.ToString(req["EmployeeAssignedTo"]);
                    SPFieldLookupValue StatusId = new SPFieldLookupValue((req["RequestStatusId"] != null) ? req["RequestStatusId"].ToString() : string.Empty);
                    if (((StatusId.LookupId == (int)Common.Utilities.RequestStatus.UCECulturalMissionStatementReply) ||
                        (StatusId.LookupId == (int)Common.Utilities.RequestStatus.UCEHigherEduInstitutesStatementReply) ||
                        (StatusId.LookupId == (int)Common.Utilities.RequestStatus.UCEProgramEmployeeReview) ||
                        (StatusId.LookupId == (int)Common.Utilities.RequestStatus.UCEAsianAndEuropianEmployeeReview) ||
                        (StatusId.LookupId == (int)Common.Utilities.RequestStatus.UCProgramEmployeeClarificationReply) ||
                        (StatusId.LookupId == (int)Common.Utilities.RequestStatus.UCEAsianAndEuropianEmployeeMissingInformation) ||
                        (StatusId.LookupId == (int)Common.Utilities.RequestStatus.UCEProgramEmployeeMissingInformation) ||
                        (StatusId.LookupId == (int)Common.Utilities.RequestStatus.UCEHeadManagerAccepted) ||
                        (StatusId.LookupId == (int)Common.Utilities.RequestStatus.UCEHeadManagerRejected) ||
                        (StatusId.LookupId == (int)Common.Utilities.RequestStatus.UCEAssistantUndersecretaryAccepted) ||
                        (StatusId.LookupId == (int)Common.Utilities.RequestStatus.UCEAssistantUndersecretaryRejected) ||
                        (StatusId.LookupId == (int)Common.Utilities.RequestStatus.UCECulturalMissionStatementReply) ||
                        (StatusId.LookupId == (int)Common.Utilities.RequestStatus.UCEHigherEduInstitutesStatementReply)
                        )
                        &&  (HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName))
                        && SPContext.Current.Web.CurrentUser.LoginName.Equals(assignedTo))
                    {
                        lnk_AddNewStatementPopUp.Visible = true;
                    }
                    else
                        lnk_AddNewStatementPopUp.Visible = false;

                    BindGrid(Page.Session["DisplayRequestId"].ToString());
                }
            }
        }

        private void BindGrid(string ReqID)
        {
            try
            {
                Logging.GetInstance().Debug("Enter StatementRequests.BindGrid");
                List<Entities.RequestsForStatement> StatementReqs = BL.RequestsForStatements.GetRequestsForStatementbyReqID(ReqID);
                HelperMethods.BindGridView(grd_StatementRequests, StatementReqs);
                if (StatementReqs.Count > 0)
                {
                    lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NoOfRequests", (uint)LCID) + StatementReqs.Count;
                    lbl_NoOfRequests.Visible = true;
                }
                else
                    lbl_NoOfRequests.Visible = false;
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit StatementRequests.BindGrid");
            }
        }

        protected void grd_StatementRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter StatementRequests.grd_StatementRequests_PageIndexChanging");
            try
            {
                grd_StatementRequests.PageIndex = e.NewPageIndex;
                bool isExist = (HelperMethods.InGroup(Common.Utilities.Constants.ApplicanstGroupName));
                if (isExist & Page.Session["EditRequestId"] != null)
                    BindGrid(Page.Session["EditRequestId"].ToString());
                else if (Page.Session["DisplayRequestId"] != null)
                    BindGrid(Page.Session["DisplayRequestId"].ToString());
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit StatementRequests.grd_StatementRequests_PageIndexChanging");
            }
        }

        protected void lnk_ViewDetails_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter StatementRequests.lnk_ViewDetails_Click");
            try
            {
                if (!IsRefresh)
                {
                    LinkButton lnkButton = (LinkButton)sender;
                    GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                    HiddenField hdn_RequestNumber = (HiddenField)gvr.FindControl("hdn_RequestID");
                    Page.Session["DisplayRequestId"] = hdn_RequestNumber.Value;
                    HiddenField hdn_StatementReqId = (HiddenField)gvr.FindControl("hdn_ID");
                    Page.Session["hdn_StatementReqId"] = hdn_StatementReqId.Value;
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/StatementRequestDetails.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit StatementRequests.lnk_ViewDetails_Click");
            }
        }

        protected void lnk_ReplytoStatement_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsRefresh)
                {
                    Logging.GetInstance().Debug("Enter StatementRequests.lnk_ReplytoStatement_Click");
                    LinkButton lnkButton = (LinkButton)sender;
                    GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                    HiddenField hdn_StatementReqId = (HiddenField)gvr.FindControl("hdn_ID");
                    Page.Session["hdn_StatementReqId"] = hdn_StatementReqId.Value;
                    Session["ActionTaken"] = "AddNewStatement";
                    Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.UCE/ProgramEmployee.aspx");
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit StatementRequests.lnk_ReplytoStatement_Click");
            }
        }

        protected void grd_StatementRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter StatementRequests.grd_StatementRequests_RowDataBound");
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lnk_View = (LinkButton)e.Row.FindControl("lnk_ViewDetails");
                    LinkButton lnk_ReplytoStatement = (LinkButton)e.Row.FindControl("lnk_ReplytoStatement");
                    Label lbl_ReplyDate = (Label)e.Row.FindControl("lbl_ReplyDate");
                    //bool isExist = (HelperMethods.GetGroupUsers(Common.Utilities.Constants.CulturalMissionGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName));
                    //bool isExistHigherEducational = (HelperMethods.GetGroupUsers(Common.Utilities.Constants.HigherEducationalInstitutionsGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName));

                    if ((
                    (HelperMethods.GetGroupUsers(Common.Utilities.Constants.CulturalMissionBritainGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName)) ||
                    (HelperMethods.GetGroupUsers(Common.Utilities.Constants.CulturalMissionAustraliaGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName)) ||
                    (HelperMethods.GetGroupUsers(Common.Utilities.Constants.CulturalMissionFranceGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName)) ||
                    (HelperMethods.GetGroupUsers(Common.Utilities.Constants.CulturalMissionUSAGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName)) ||
                    (HelperMethods.GetGroupUsers(Common.Utilities.Constants.CulturalMissionCanadaGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName)) ||
                    (HelperMethods.GetGroupUsers(Common.Utilities.Constants.CulturalMissionJordanGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName)) ||
                    (HelperMethods.GetGroupUsers(Common.Utilities.Constants.HigherEducationalInstitutionsGroupName).Exists(i => i.LoginName == SPContext.Current.Web.CurrentUser.LoginName))
                    ) && lbl_ReplyDate.Text == string.Empty)
                    {
                        //grd_StatementRequests.Columns[6].HeaderText = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Actions", (uint)LCID);
                        lnk_ReplytoStatement.Visible = true;
                        lnk_View.Visible = false;
                    }
                    else
                    {
                        //grd_StatementRequests.HeaderRow.Cells[6].Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "View", (uint)LCID);
                        lnk_ReplytoStatement.Visible = false;
                        lnk_View.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit StatementRequests.grd_StatementRequests_RowDataBound");
            }
        }

        protected void lnk_AddNewStatementPopUp_Click(object sender, EventArgs e)
        {
            Session["ActionTaken"] = "AddNewStatement";
            Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.UCE/ProgramEmployee.aspx");
        }
    }
}