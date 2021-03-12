using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
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
                        if (Page.Session["EditRequestId"] != null)
                        {
                            if (Utilities.BusinessHelper.GetAssignee(Page.Session["EditRequestId"].ToString()))
                                lnk_AddNewBookPopUp.Visible = false;

                            BindGrid(Page.Session["EditRequestId"].ToString());
                        }
                    }
                    else
                    {
                        if (Page.Session["DisplayRequestId"] != null)
                        {
                            int requestId = int.Parse(Convert.ToString(Page.Session["DisplayRequestId"]));

                            SPListItem req = BL.Request.Reviewer_GetRequestItemByID(requestId);
                            SPFieldLookupValue StatusId = new SPFieldLookupValue((req["RequestStatusId"] != null) ? req["RequestStatusId"].ToString() : string.Empty);

                            if (StatusId.LookupId != 37 && (HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName) || HelperMethods.InGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName)))
                                lnk_AddNewBookPopUp.Visible = true;
                            else
                                lnk_AddNewBookPopUp.Visible = false;

                            BindGrid(Page.Session["DisplayRequestId"].ToString());
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
                    if (gvr.RowType == DataControlRowType.DataRow)
                    {
                        if (grd_Books.Rows.Count > 0)
                        {
                            //if (gvr.RowIndex == 0)
                            //{
                            //    Page.Session["First_Id"] = 0;
                            //}
                            //else
                            //{
                            //    Page.Session["First_Id"] = null;
                            //}
                            List<Entities.ExternalComms> StatementReqs = BL.ExternalCommunications.GetBooksbyRequestID(Page.Session["DisplayRequestId"].ToString()).ToList();
                            var StatementReqID = StatementReqs.FirstOrDefault().ID;
                            HiddenField Index_ID = (gvr.FindControl("hdn_ID") as HiddenField);
                            if (StatementReqID == Index_ID.Value)
                            {
                                Page.Session["First_Id"] = 0;
                            }
                            else
                            {
                                Page.Session["First_Id"] = null;
                            }
                        }
                    }
                    HiddenField hdn_BookReqId = (HiddenField)gvr.FindControl("hdn_ID");
                    Page.Session["hdn_BookReqId"] = hdn_BookReqId.Value;
                    //Response.Redirect("/en/Pages/ExternalCommDetails.aspx");
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ExternalCommDetails.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
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

                    
                    if (gvr.RowType == DataControlRowType.DataRow)
                    {
                        if (grd_Books.Rows.Count > 0)
                        {
                            //if (gvr.RowIndex == 0)
                            //{
                            //    HiddenField Index_ID = (gvr.FindControl("hdn_ID") as HiddenField);
                            //    Page.Session["First_Id"] = Index_ID.Value;
                            //}
                            //else
                            //{
                            //    Page.Session["First_Id"] = null;
                            //}
                            List<Entities.ExternalComms> StatementReqs = BL.ExternalCommunications.GetBooksbyRequestID(Page.Session["DisplayRequestId"].ToString()).ToList();
                            var StatementReqID = StatementReqs.FirstOrDefault().ID;
                            HiddenField Index_ID = (gvr.FindControl("hdn_ID") as HiddenField);
                            if (StatementReqID == Index_ID.Value)
                            {
                                Page.Session["First_Id"] = Index_ID.Value;
                            }
                            else
                            {
                                Page.Session["First_Id"] = null;
                            }
                        }
                    }

                    HiddenField hdn_BookReqId = (HiddenField)gvr.FindControl("hdn_ID");
                    Page.Session["hdn_BookReqId"] = hdn_BookReqId.Value;
                    Session["ActionTaken"] = "AddNewBookReply";
                    Page.Session["hdn_BookId"] = null;
                    // Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.UCE/ProgramEmployee.aspx");
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/AddNewBook.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
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
            Logging.GetInstance().Debug("Entering method NewRequests.lnk_AssignTo_Click");
            if (!IsRefresh)
            {
                if (grd_Books.Rows.Count > 0)
                {

                    List<Entities.ExternalComms> StatementReqs = BL.ExternalCommunications.GetBooksbyRequestID(Page.Session["DisplayRequestId"].ToString()).ToList();
                    var StatementReqID =StatementReqs.LastOrDefault().ID;

                    Page.Session["hdn_BookId"] = StatementReqID;
                    //  GridViewRow row =  grd_Books.Rows[grd_Books.Rows.Count - 1];
                    //if (row.RowType == DataControlRowType.DataRow)
                    //{
                    //    HiddenField Index_ID = (row.FindControl("hdn_ID") as HiddenField);
                    //    Page.Session["hdn_BookId"] = Index_ID.Value;
                    //}
                }
            }
            Session["ActionTaken"] = "AddNewBook";
            //Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.UCE/ProgramEmployee.aspx");
            SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/AddNewBook.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
        }
    }
}