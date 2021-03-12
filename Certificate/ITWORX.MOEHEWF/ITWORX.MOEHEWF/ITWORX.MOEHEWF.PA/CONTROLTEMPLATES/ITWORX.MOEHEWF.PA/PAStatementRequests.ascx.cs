using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Entities;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using requestBL = ITWORX.MOEHEWF.PA.BL;
using commonUtilities = ITWORX.MOEHEWF.Common.Utilities;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class PAStatementRequests : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grd_PAStatementRequests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "StatementReqsPageSize"));

                if (Page.Session["PADisplayRequestId"] != null)
                {

                    if (HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName))
                    {
                        PARequest paRequest = requestBL.Request.GetRequestByNumber(Convert.ToInt32(Page.Session["PADisplayRequestId"]), LCID);
                        if (paRequest != null && (paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PAEmployeeClarificationReplay).ToString() || paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PAProgramEmployeeMissingInformation).ToString() || paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PASubmitted).ToString() || paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PACulturalMissionStatementReply).ToString() || paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PAHigherEduInstitutesStatementReply).ToString())
                            &&  SPContext.Current.Web.CurrentUser.LoginName.Equals(paRequest.AssignedTo))
                        {
                            lnk_AddNewStatementPopUp.Visible = true;
                        }
                        else
                        {
                            lnk_AddNewStatementPopUp.Visible = false;
                        }

                    } 
                    BindGrid(Page.Session["PADisplayRequestId"].ToString());
                }
            }
        }

        private void BindGrid(string ReqID)
        {
            try
            {
                Logging.GetInstance().Debug("Enter PAStatementRequests.BindGrid");
                List<Entities.RequestsForStatement> StatementReqs = BL.RequestsForStatements.GetRequestsForStatementbyReqID(ReqID);
                HelperMethods.BindGridView(grd_PAStatementRequests, StatementReqs);
                if (StatementReqs.Count > 0)
                {
                    lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NoOfRequests", (uint)LCID) + StatementReqs.Count;
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
                Logging.GetInstance().Debug("Exit PAStatementRequests.BindGrid");
            }
        }

        protected void grd_PAStatementRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter PAStatementRequests.grd_PAStatementRequests_PageIndexChanging");
            try
            {
                grd_PAStatementRequests.PageIndex = e.NewPageIndex;
                bool isExist = (HelperMethods.InGroup(Common.Utilities.Constants.ApplicanstGroupName));
                if (isExist & Page.Session["PAEditRequestId"] != null)
                    BindGrid(Page.Session["PAEditRequestId"].ToString());
                else if (Page.Session["PADisplayRequestId"] != null)
                    BindGrid(Page.Session["PADisplayRequestId"].ToString());
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PAStatementRequests.grd_PAStatementRequests_PageIndexChanging");
            }
        }

        protected void lnk_ViewDetails_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter PAStatementRequests.lnk_ViewDetails_Click");
            try
            {
                if (!IsRefresh)
                {
                    LinkButton lnkButton = (LinkButton)sender;
                    GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                    HiddenField hdn_RequestNumber = (HiddenField)gvr.FindControl("hdn_RequestID");
                    Page.Session["PADisplayRequestId"] = hdn_RequestNumber.Value;
                    HiddenField hdn_StatementReqId = (HiddenField)gvr.FindControl("hdn_ID");
                    Page.Session["hdn_StatementReqId"] = hdn_StatementReqId.Value;
                    //Response.Redirect("/en/Pages/PAStatementRequestDetails.aspx");
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAStatementRequestDetails.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PAStatementRequests.lnk_ViewDetails_Click");
            }
        }

        protected void lnk_ReplytoStatement_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsRefresh)
                {
                    Logging.GetInstance().Debug("Enter PAStatementRequests.lnk_ReplytoStatement_Click");
                    LinkButton lnkButton = (LinkButton)sender;
                    GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                    HiddenField hdn_StatementReqId = (HiddenField)gvr.FindControl("hdn_ID");
                    Page.Session["hdn_StatementReqId"] = hdn_StatementReqId.Value;
                    Session["ActionTaken"] = "AddNewStatement";
                    Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.PA/ProgramEmployee.aspx");
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PAStatementRequests.lnk_ReplytoStatement_Click");
            }
        }

        protected void grd_PAStatementRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter PAStatementRequests.grd_PAStatementRequests_RowDataBound");
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
                        lnk_ReplytoStatement.Visible = true;
                        lnk_View.Visible = false;
                    }
                    else
                    {
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
                Logging.GetInstance().Debug("Exit PAStatementRequests.grd_PAStatementRequests_RowDataBound");
            }
        }

        protected void lnk_AddNewStatementPopUp_Click(object sender, EventArgs e)
        {
            Session["ActionTaken"] = "AddNewStatement";
            Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.PA/ProgramEmployee.aspx");
        }
    }
}