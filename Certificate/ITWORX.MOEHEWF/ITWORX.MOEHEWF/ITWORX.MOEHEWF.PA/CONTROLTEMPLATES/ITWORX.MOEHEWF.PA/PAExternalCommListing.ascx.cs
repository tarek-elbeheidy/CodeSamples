using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class ExternalCommListing : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SPContext.Current.Web.CurrentUser != null)
                {
                    grd_Books.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "Procedures"));

                    bool isExist = (HelperMethods.InGroup(Common.Utilities.Constants.ApplicanstGroupName));
                    if (isExist)
                    {
                        if (Page.Session["PAEditRequestId"] != null)
                        {
                            if (Utilities.BusinessHelper.GetAssignee(Page.Session["PAEditRequestId"].ToString()))
                                lnk_AddNewBookPopUp.Visible = false;

                            BindGrid(Page.Session["PAEditRequestId"].ToString());
                        }
                    }
                    else
                    {
                        if (Page.Session["PADisplayRequestId"] != null)
                        {
                            if (Utilities.BusinessHelper.GetAssignee(Page.Session["PADisplayRequestId"].ToString()) &
                                (HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName) || HelperMethods.InGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName)))
                                lnk_AddNewBookPopUp.Visible = true;

                            BindGrid(Page.Session["PADisplayRequestId"].ToString());
                        }
                    }
                }
            }
        }

        private void BindGrid(string ReqID)
        {
            try
            {
                Logging.GetInstance().Debug("Enter ExternalCommListing.BindGrid");
                List<Entities.ExternalComms> StatementReqs = BL.ExternalCommunications.GetBooksbyRequestID(ReqID);
                HelperMethods.BindGridView(grd_Books, StatementReqs);
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
                Logging.GetInstance().Debug("Exit ExternalCommListing.BindGrid");
            }
        }

        protected void grd_Books_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter ExternalCommListing.grd_Books_RowDataBound");
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lnk_View = (LinkButton)e.Row.FindControl("lnk_ViewDetails");
                    LinkButton lnk_OrgReply = (LinkButton)e.Row.FindControl("lnk_OrgReply");
                    Label lbl_OrgReplyBookNo = (Label)e.Row.FindControl("lbl_OrgReplyBookNo");
                    if (lbl_OrgReplyBookNo.Text == "")
                    {
                        lnk_OrgReply.Visible = true;
                        lnk_View.Visible = false;
                    }
                    else
                    {
                        lnk_OrgReply.Visible = false;
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
                Logging.GetInstance().Debug("Exit ExternalCommListing.grd_Books_RowDataBound");
            }
        }

        protected void grd_Books_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter ExternalCommListing.grd_Books_PageIndexChanging");
            try
            {
                grd_Books.PageIndex = e.NewPageIndex;
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
                Logging.GetInstance().Debug("Exit ExternalCommListing.grd_Books_PageIndexChanging");
            }
        }

        protected void lnk_ViewDetails_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter ExternalCommListing.lnk_ViewDetails_Click");
            try
            {
                if (!IsRefresh)
                {
                    LinkButton lnkButton = (LinkButton)sender;
                    GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                    HiddenField hdn_BookReqId = (HiddenField)gvr.FindControl("hdn_ID");
                    Page.Session["hdn_BookReqId"] = hdn_BookReqId.Value;
                    //Response.Redirect("/en/Pages/PAExternalCommDetails.aspx");
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAExternalCommDetails.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ExternalCommListing.lnk_ViewDetails_Click");
            }
        }

        protected void lnk_OrgReply_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter ExternalCommListing.lnk_OrgReply_Click");
            try
            {
                if (!IsRefresh)
                {
                    LinkButton lnkButton = (LinkButton)sender;
                    GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                    HiddenField hdn_BookReqId = (HiddenField)gvr.FindControl("hdn_ID");
                    Page.Session["hdn_BookReqId"] = hdn_BookReqId.Value;
                    Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.PA/ProgramEmployee.aspx");
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ExternalCommListing.lnk_OrgReply_Click");
            }
        }

        protected void lnk_AddNewBookPopUp_Click(object sender, EventArgs e)
        {
            Session["ActionTaken"] = "AddNewBook";
            Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.PA/ProgramEmployee.aspx");
        }
    }
}