using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHEWF.SCE.BL;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.SCE.WebParts.SCEStatementListing
{
    public partial class SCEStatementListingUserControl : UserControlBase
    {
        bool isCulture
        {
            get
            {
                if (ViewState["isCulture"] != null)
                {
                    return Convert.ToBoolean(Session["isCulture"]);
                }
                else
                {
                    return is_Culture();
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        void BindGrid()
        {
            Logging.GetInstance().Debug("Entering method SCEStatementListingUserControl.BindGrid");
            try
            {
                if (Request.QueryString["RequestId"] != null)
                {
                    int requestID = Convert.ToInt32(Request.QueryString["RequestId"]);

                    var Requests = StatementRequestBL.GetStatementRequestsList(requestID, LCID);
                    grd_StatementRequests.DataSource = Requests;
                    grd_StatementRequests.DataBind();
                    if (Requests.Count > 0)
                    {
                        lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "NoOfRequests", (uint)LCID) + " " + Requests.Count;
                        lbl_NoOfRequests.Visible = true;
                    }
                    else
                        lbl_NoOfRequests.Visible = false;

                    using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                    {
                        var currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(x => x.Id == requestID).FirstOrDefault();
                        if (currentRequest != null)
                        {
                            int currentStatus = (int)currentRequest.RequestStatus.Id;
                            string assignTo = currentRequest.EmployeeAssignedTo;

                            if ((currentStatus == 71 || currentStatus == 69 || currentStatus == 75 || currentStatus == 73 || currentStatus == 81 || currentStatus == 85 || currentStatus== 74) && currentRequest.EmployeeAssignedTo.ToLower() == SPContext.Current.Web.CurrentUser.LoginName.ToLower())
                            {
                                btn_AddSCEStatement.Visible = true;
                            }
                            else
                                btn_AddSCEStatement.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SCEStatementListingUserControl.BindGrid");
            }
        }

        protected void lnk_ReplytoStatement_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_StatementId = (HiddenField)gvr.FindControl("hdn_ID");


                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEStatementReply.aspx?StatementId=" + hdn_StatementId.Value, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SCEStatementListingUserControl.lnk_ReplytoStatement_Click");
            }
        }

        protected void lnk_View_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter SCEStatementListingUserControl.lnk_View_Click");
            try
            {

                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_StatementId = (HiddenField)gvr.FindControl("hdn_ID");


                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEViewStatement.aspx?StatementId=" + hdn_StatementId.Value, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SCEStatementListingUserControl.lnk_View_Click");
            }
        }
        protected void AddSCEStatement_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["RequestId"] != null)
            {
                int requestID = Convert.ToInt32(Request.QueryString["RequestId"]);
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/AddSCEStatement.aspx?RequestId=" + requestID, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
        }

        protected void grd_SCEStatRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter SCEStatementListingUserControl.grd_SCEStatRequests_RowDataBound");
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl_ReplyDate = (Label)e.Row.FindControl("lbl_ReplyDate");
                    LinkButton lnk_View = (LinkButton)e.Row.FindControl("lnk_View");
                    LinkButton lnk_ReplytoStatement = (LinkButton)e.Row.FindControl("lnk_ReplytoStatement");

                    if (isCulture)
                    {
                        if (e.Row.RowIndex==0)
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
                Logging.GetInstance().Debug("Exit SCEStatementListingUserControl.grd_SCEStatRequests_RowDataBound");
            }
        }

        bool is_Culture()
        {
            try
            {
                Logging.GetInstance().Debug("Enter SCEStatementListingUserControl.isCulture");
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    int requestId = Convert.ToInt32(Request.QueryString["RequestId"]);
                    var req = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == requestId).FirstOrDefault();

                    var cultures = ctx.StatementAgencyList;

                    foreach (var item in cultures)
                    {
                        if (HelperMethods.InGroup(item.GroupName.ToLower()) && req.EmployeeAssignedTo.ToLower() == item.GroupName.ToLower()&&req.RequestStatus.Id == 74)
                        {
                            ViewState["isCulture"] = true;
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SCEStatementListingUserControl.isCulture");
            }
            ViewState["isCulture"] = false;
            return false;
        }
    }
}
