using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using Microsoft.SharePoint.Utilities;
using System.Web;
using ITWORX.MOEHEWF.SCE.Utilities;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint.Linq;
using ITWORX.MOEHEWF.Common.Utilities;

namespace ITWORX.MOEHEWF.SCE.WebParts.SCEBooksRequests
{
    public partial class SCEBooksRequestsUserControl : Utilities.UserControlBase
    {

        #region PublicProperties
        public bool ViewOnly { get; set; }
        public int? RequestId
        {
            get
            {
                if (Request.QueryString["RequestId"] != null)
                {
                    ViewState["RequestId"] = Request.QueryString["RequestId"];
                    return Convert.ToInt32(Request.QueryString["RequestId"]);
                }
                else if (ViewState["RequestId"] != null)
                {
                    return Convert.ToInt32(ViewState["RequestId"]);
                }
                return null;
            }
            set
            {
                ViewState["RequestId"] = value;
            }
        }
       
        bool isExist = (HelperMethods.InGroup(Common.Utilities.Constants.SCEEquivalenceEmployeesGroupName));
        #endregion



        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter SCEBooksRequestsUserControl.Page_Load");
                if (!this.IsPostBack)
                {

                    BindGrid((int)RequestId); 
                    using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                    {
                        var currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(x => x.Id == (int)RequestId).FirstOrDefault();
                        if (currentRequest != null)
                        {
                            int currentStatus = (int)currentRequest.RequestStatus.Id;
                            string assignTo = currentRequest.EmployeeAssignedTo;

                            if (((currentStatus == (int)RequestStatus.SCEEquivalenceEmployeeReassign || currentStatus==(int)RequestStatus.SCESubmitted  || currentStatus == (int)RequestStatus.SCECulturalMissionStatementReply || currentStatus == (int)RequestStatus.SCEEmployeeClarificationReply 
                                || currentStatus == (int)RequestStatus.SCESectionManagerMissingInformation || currentStatus == (int)RequestStatus.SCEDepartmentManagerMissingRecommendation ) && currentRequest.EmployeeAssignedTo.ToLower() == SPContext.Current.Web.CurrentUser.LoginName.ToLower())
                                || currentStatus== (int)RequestStatus.SCECulturalMissionNeedsStatement && currentRequest.ReturnedBy.Equals(SPContext.Current.Web.CurrentUser.Name))
                            {
                                lblNewBookRequests.Visible = true;
                                hypNewBookRequest.Visible = true;
                            }
                            else
                            {
                                lblNewBookRequests.Visible = false;
                                hypNewBookRequest.Visible = false;
                            }
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
                Logging.GetInstance().Debug("Exit SCEBooksRequestsUserControl.Page_Load");
            }
        }
        private void BindGrid(int SCEReqID)
        {
            HttpContext backupContext = HttpContext.Current;
            try
            {
                string loginName = SPContext.Current.Web.CurrentUser.LoginName; // if you need it in your code
                string rootWebUrl = SPContext.Current.Site.RootWeb.Url;
                int currentUserId = SPContext.Current.Web.CurrentUser.ID;

                HttpContext.Current = null;

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {

                    Logging.GetInstance().Debug("Enter SCEBooksRequestsUserControl.BindGrid");
                    SCEContextDataContext ctx = new SCEContextDataContext(rootWebUrl);
                    List<SCEBooksRequestsListFieldsContentType> BooksRequests = ctx.SCEBooksRequestsList.ScopeToFolder("", true).Where(c => c.RequestIDId == SCEReqID).ToList();
                    HelperMethods.BindGridView(grd_SCEExternalBookRequests, BooksRequests);
                    if (BooksRequests.Count > 0)
                    {
                        //lastreply = BooksRequests.Last().ClarificationReply;
                        lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NoOfRequests", (uint)LCID) + BooksRequests.Count;
                        lbl_NoOfRequests.Visible = true;
                    }
                    else
                    {
                        lbl_NoOfRequests.Visible = false; 
                    }
                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                // resetting the SPContext
                if (HttpContext.Current == null)
                {
                    HttpContext.Current = backupContext;
                }

                Logging.GetInstance().Debug("Exit SCEBooksRequestsUserControl.BindGrid");
            }
        }

        protected void lnk_View_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter SCEBooksRequestsUserControl.lnk_View_Click");
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_BookRequestId = (HiddenField)gvr.FindControl("hdn_ID");

                SPUtility.Redirect(string.Format("{0}/Pages/SCEBookRequestDetails.aspx?BookId={1}&ReqSt={2}&RequestId={3}",
                    SPContext.Current.Web.Url, hdn_BookRequestId.Value, (int)ExternalBookRequestStatus.View,RequestId), SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SCEBooksRequestsUserControl.lnk_View_Click");
            }
        }

        protected void btn_NewRequest_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter SCEBooksRequestsUserControl.btn_NewRequest_Click");
                int reqId = Convert.ToInt32(Request.QueryString["RequestId"]);
                SPUtility.Redirect(string.Format("{0}/Pages/SCEBookRequestDetails.aspx?RequestId={1}&ReqSt={2}", SPContext.Current.Web.Url, reqId,(int) ExternalBookRequestStatus.New), SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SCEBooksRequestsUserControl.btn_NewRequest_Click");
            }
        }

        protected void lnk_AddReply_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter SCEBooksRequestsUserControl.lnk_ReplytoClarification_Click");
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_BookRequestId = (HiddenField)gvr.FindControl("hdn_ID");
                SPUtility.Redirect(string.Format("{0}/Pages/SCEReplyToBookRequest.aspx?BookId={1}&ReqSt={2}&RequestId={3}", SPContext.Current.Web.Url, hdn_BookRequestId.Value, (int)ExternalBookRequestStatus.Reply,RequestId), SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SCEBooksRequestsUserControl.lnk_ReplytoClarification_Click");
            }
        }
        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter SCEBooksRequestsUserControl.lnk_ReplytoClarification_Click");
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_ClarRequestId = (HiddenField)gvr.FindControl("hdn_ID");
                SPUtility.Redirect(string.Format("{0}/Pages/SCEBookRequestDetails.aspx?BookId={1}&ReqSt={2}&RequestId={3}", SPContext.Current.Web.Url, hdn_ClarRequestId.Value, (int)ExternalBookRequestStatus.Edit,RequestId), SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SCEBooksRequestsUserControl.lnk_ReplytoClarification_Click");
            }
        }

        protected void SCEExternalBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter SCEBooksRequestsUserControl.SCEExternalBook_PageIndexChanging");
            try
            {
                grd_SCEExternalBookRequests.PageIndex = e.NewPageIndex;
                BindGrid((int)RequestId);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SCEBooksRequestsUserControl.SCEExternalBook_PageIndexChanging");
            }
        }
        protected void grd_SCEBookRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter SCEBooksRequestsUserControl.grd_Requests_RowDataBound");
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hdn_BookRequestStatusId = (HiddenField)e.Row.FindControl("hdn_BookRequestStatusId");
                    LinkButton lnk_Edit = (LinkButton)e.Row.FindControl("lnk_Edit");
                    LinkButton lnk_AddReply = (LinkButton)e.Row.FindControl("lnk_AddReply");
                    //bool isExist = (HelperMethods.InGroup(Common.Utilities.Constants.SCEEquivalenceEmployeesGroupName) //||
                        //HelperMethods.InGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName));
                        //= (HelperMethods.InGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName) || HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName));
                        if (isExist)
                    {
                        lnk_Edit.Visible = true;
                        if (!string.IsNullOrEmpty(hdn_BookRequestStatusId.Value) && int.Parse(hdn_BookRequestStatusId.Value) == (int)ExternalBookRequestStatus.Sent)
                        {
                            lnk_AddReply.Visible = true;

                        }
                        else
                        {
                            lnk_AddReply.Visible = false;
                        }
                    }
                    else
                    {
                        lnk_AddReply.Visible = false;
                        lnk_Edit.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SCEBooksRequestsUserControl.grd_Requests_RowDataBound");
            }
        }
    }
}
