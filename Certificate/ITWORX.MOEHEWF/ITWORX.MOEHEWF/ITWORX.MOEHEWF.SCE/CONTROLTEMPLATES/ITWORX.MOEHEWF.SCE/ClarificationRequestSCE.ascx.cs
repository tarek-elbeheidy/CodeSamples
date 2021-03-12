using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHEWF.SCE.Entities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint.Linq;
using System.Linq;
using Microsoft.SharePoint.Utilities;
using System.Web;

namespace ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE
{
    public partial class ClarificationRequestSCE : UserControlBase
    {
        private int RequestId { get { return Convert.ToInt32(Request.QueryString["RequestId"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter ClarificationRequestSCE.Page_Load");
                if (!Page.IsPostBack)
                {
                    BindGrid(RequestId);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ClarificationRequestSCE.Page_Load");
            }
        }
        private void BindGrid(int SCEReqID)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {

                    Logging.GetInstance().Debug("Enter ClarificationRequestSCE.BindGrid");
                    using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                    {
                        SCERequestsListFieldsContentType req = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == SCEReqID).FirstOrDefault();
                        if (req != null)
                        {
                            List<SCEClarificationsRequestsListFieldsContentType> ClarRequests = ctx.SCEClarificationsRequestsList.ScopeToFolder("", true).Where(c => c.RequestIDId.Value == SCEReqID).ToList();

                            HelperMethods.BindGridView(grd_SCEClarRequests, ClarRequests);
                            if (ClarRequests.Count > 0)
                            {
                                lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NoOfRequests", (uint)LCID) + ClarRequests.Count;
                                lbl_NoOfRequests.Visible = true;
                            }
                            else
                            {
                                lbl_NoOfRequests.Visible = false;
                            }
                            string loginName = SPContext.Current.Web.CurrentUser.LoginName;
                            if (req.RequestStatus.Id != RequestStatus.SCEDraft.ToInt() && req.RequestStatus.Id != RequestStatus.SCEEmployeeNeedsClarification.ToInt() && req.EmployeeAssignedTo.ToString() == loginName.ToString() && (HelperMethods.InGroup(Utilities.Constants.SCEEquivalenceEmployees)|| HelperMethods.InGroup(Utilities.Constants.SCEEquivalenceEmployeeReassign)))
                            {
                                btn_NewRequest.Visible = true;
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ClarificationRequestSCE.BindGrid");
            }
        }

        protected void lnk_View_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter ClarificationRequestSCE.lnk_View_Click");
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_ClarRequestId = (HiddenField)gvr.FindControl("hdn_ID");

                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEClarificationRequestDetails.aspx?ClarId=" + hdn_ClarRequestId.Value + "&RequestId=" + RequestId, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ClarificationRequestSCE.lnk_View_Click");
            }
        }

        protected void btn_NewRequest_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter ClarificationRequestSCE.btn_NewRequest_Click");
                int reqId = Convert.ToInt32(Request.QueryString["RequestId"]);
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCENewClarificationReq.aspx?RequestId=" + reqId, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ClarificationRequestSCE.btn_NewRequest_Click");
            }
        }

        protected void lnk_ReplytoClarification_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter ClarificationRequestSCE.lnk_ReplytoClarification_Click");
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_ClarRequestId = (HiddenField)gvr.FindControl("hdn_ID");            
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEClarificationRequestDetails.aspx?ClarId=" + hdn_ClarRequestId.Value + "&RequestId=" + RequestId, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ClarificationRequestSCE.lnk_ReplytoClarification_Click");
            }
        }

        protected void grd_SCEClarRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter ClarificationRequestSCE.grd_Requests_RowDataBound");
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbl_ClarReply = (Label)e.Row.FindControl("lbl_ClarReply");
                    LinkButton lnk_View = (LinkButton)e.Row.FindControl("lnk_View");
                    LinkButton lnk_ReplytoClar = (LinkButton)e.Row.FindControl("lnk_ReplytoClarification");
                    int requestId = Convert.ToInt32(Request.QueryString["RequestId"]);
                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        bool isExist = (HelperMethods.InGroup(Common.Utilities.Constants.ApplicanstGroupName));
                        using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                        {
                            SCERequestsListFieldsContentType req = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == requestId).FirstOrDefault();

                            if (HelperMethods.InGroup(Common.Utilities.Constants.ApplicanstGroupName))
                            {
                                if (string.IsNullOrEmpty(lbl_ClarReply.Text))
                                {
                                    lnk_ReplytoClar.Visible = true;
                                    lnk_View.Visible = false;
                                }
                                else
                                {
                                    lnk_ReplytoClar.Visible = false;
                                    lnk_View.Visible = true;
                                }
                                if (LCID == (int)Language.English)
                                    e.Row.Cells[2].Text = ((ClarificationReasonsLookupsCT)DataBinder.Eval(e.Row.DataItem, "ClarificationReason")).Title;
                                else
                                    e.Row.Cells[2].Text = ((ClarificationReasonsLookupsCT)DataBinder.Eval(e.Row.DataItem, "ClarificationReason")).TitleAr;
                            }
                            //Employee created request
                            else if (req.EmployeeAssignedTo.ToLower() == SPContext.Current.Web.CurrentUser.LoginName.ToLower() && req.IsEmployee == IsEmployee.Yes)
                            {
                                if (string.IsNullOrEmpty(lbl_ClarReply.Text))
                                {
                                    lnk_ReplytoClar.Visible = true;
                                    lnk_View.Visible = false;
                                }
                                else
                                {
                                    lnk_ReplytoClar.Visible = false;
                                    lnk_View.Visible = true;
                                }
                                e.Row.Cells[2].Visible = false;
                            }
                            //applicant created request
                            else if (HelperMethods.InGroup(Common.Utilities.Constants.SCEEquivalenceEmployeesGroupName) &&(req.ReturnedBy.ToLower() == SPContext.Current.Web.CurrentUser.LoginName.ToLower() || (req.EmployeeAssignedTo.ToLower() == SPContext.Current.Web.CurrentUser.LoginName.ToLower() && req.RequestStatus.Id == (int)RequestStatus.SCEEmployeeClarificationReply)))
                            {
                               
                                    lnk_ReplytoClar.Visible = false;
                                    lnk_View.Visible = true;
                                
                                e.Row.Cells[2].Visible = false;

                            }
                            else
                            {
                                lnk_ReplytoClar.Visible = false;
                                lnk_View.Visible = true;
                                e.Row.Cells[2].Visible = false;
                            }
                        }


                    });





                }
                else if (e.Row.RowType == DataControlRowType.Header)
                {
                    bool isExist = (HelperMethods.InGroup(Common.Utilities.Constants.ApplicanstGroupName));
                    if (isExist)
                    {
                        e.Row.Cells[0].Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Date", (uint)LCID);
                        e.Row.Cells[4].Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Reply", (uint)LCID);
                    }
                    else
                    {

                        e.Row.Cells[2].Visible = false;//hiding request reason in case of not applicant
                        e.Row.Cells[0].Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ClarRequestedDate", (uint)LCID);


                        e.Row.Cells[4].Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ApplicantReply", (uint)LCID);
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ClarificationRequestSCE.grd_Requests_RowDataBound");
            }
        }
    }
}
