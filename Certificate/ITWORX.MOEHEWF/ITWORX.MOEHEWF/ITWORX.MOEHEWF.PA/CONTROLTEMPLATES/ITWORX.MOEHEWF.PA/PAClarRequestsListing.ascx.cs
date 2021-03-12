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

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class ClarRequestsListing : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SPContext.Current.Web.CurrentUser != null)
                {
                    grd_ClarRequests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "Procedures"));

                    bool isExist = (HelperMethods.InGroup(Common.Utilities.Constants.ApplicanstGroupName));
                    if (isExist)
                    {
                        if (Page.Session["PAEditRequestId"] != null)
                        {
                            if (Utilities.BusinessHelper.GetAssignee(Page.Session["PAEditRequestId"].ToString()))
                                lnk_AddNewClarPopUp.Visible = false;

                            BindGrid(Page.Session["PAEditRequestId"].ToString());
                        }
                        else if (Page.Session["PADisplayRequestId"] != null)
                        {
                            if (Utilities.BusinessHelper.GetAssignee(Page.Session["PADisplayRequestId"].ToString()))
                                lnk_AddNewClarPopUp.Visible = false;

                            BindGrid(Page.Session["PADisplayRequestId"].ToString());
                        }
                    }
                    else
                    {
                        if (Page.Session["PADisplayRequestId"] != null)
                        {
                            if ((Utilities.BusinessHelper.GetAssignee(Page.Session["PADisplayRequestId"].ToString()) &
                                (HelperMethods.InGroup(Common.Utilities.Constants.ReceptionistGroupName))) ||
                                (Utilities.BusinessHelper.GetAssignee(Page.Session["PADisplayRequestId"].ToString()) &
                                (HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName))))
                                lnk_AddNewClarPopUp.Visible = true;

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
                Logging.GetInstance().Debug("Enter ClarRequestsListing.BindGrid");
                List<ClarificationReqs> ClarRequests = BL.PAClarificationRequests.GetClarificationRequestbyReqID(ReqID);
                HelperMethods.BindGridView(grd_ClarRequests, ClarRequests);
                if (ClarRequests.Count > 0)
                {
                    lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NoOfRequests", (uint)LCID) + ClarRequests.Count;
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
                Logging.GetInstance().Debug("Exit ClarRequestsListing.BindGrid");
            }
        }

        protected void grd_ClarRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter PAClarificationRequests.grd_Requests_PageIndexChanging");
            try
            {
                grd_ClarRequests.PageIndex = e.NewPageIndex;

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
                Logging.GetInstance().Debug("Exit PAClarificationRequests.grd_Requests_PageIndexChanging");
            }
        }

        protected void lnk_View_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter ClarRequestsListing.lnk_View_Click");
            try
            {
                if (!IsRefresh)
                {
                    LinkButton lnkButton = (LinkButton)sender;
                    GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                    HiddenField hdn_ClarRequestId = (HiddenField)gvr.FindControl("hdn_ID");
                    Page.Session["hdn_ClarRequestId"] = hdn_ClarRequestId.Value;
                    //Response.Redirect((LCID == 1033 ? "/en" : "/ar") + "/Pages/PAClarificationReqDetails.aspx");
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAClarificationReqDetails.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ClarRequestsListing.lnk_View_Click");
            }
        }

        protected void lnk_ReplytoClarification_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter ClarRequestsListing.lnk_Edit_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_ClarRequestId = (HiddenField)gvr.FindControl("hdn_ID");
                HiddenField hdn_RequestID = (HiddenField)gvr.FindControl("hdn_RequestID");
                Page.Session["hdn_ClarRequestId"] = hdn_ClarRequestId.Value;
                Page.Session["PADisplayRequestId"] = hdn_RequestID.Value;
                Session["ActionTaken"] = "AddNewClarification";
                if (HelperMethods.InGroup(Common.Utilities.Constants.ReceptionistGroupName))
                    Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.PA~/Receptionist.aspx");
                else if (HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName) || HelperMethods.InGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName))
                    Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.PA/ProgramEmployee.aspx");
                else if (HelperMethods.InGroup(Common.Utilities.Constants.ApplicanstGroupName))
                    Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.PA/Applicant.aspx");
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ClarRequestsListing.lnk_Edit_Click");
            }
        }

        protected void grd_ClarRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter ClarRequestsListing.grd_Requests_RowDataBound");
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    
                    Label lbl_ReplyDate = (Label)e.Row.FindControl("lbl_ReplyDate");
                    Label lbl_ClarReply = (Label)e.Row.FindControl("lbl_ClarReply");

                    LinkButton lnk_View = (LinkButton)e.Row.FindControl("lnk_View");
                    LinkButton lnk_ReplytoClar = (LinkButton)e.Row.FindControl("lnk_ReplytoClarification");
                    bool isExist = (HelperMethods.InGroup(Common.Utilities.Constants.ApplicanstGroupName));
                    if (isExist)
                    {
                        if (string.IsNullOrEmpty(lbl_ReplyDate.Text) && string.IsNullOrEmpty(lbl_ClarReply.Text))
                        {
                            lnk_ReplytoClar.Visible = true;
                            lnk_View.Visible = false;
                        }
                        else
                        {
                            lnk_ReplytoClar.Visible = false;
                            lnk_View.Visible = true;
                        }
                     
                    }
                    else
                    {
                        lnk_ReplytoClar.Visible = false;
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
                Logging.GetInstance().Debug("Exit ClarRequestsListing.grd_Requests_RowDataBound");
            }
        }

        protected void lnk_AddNewClarPopUp_Click(object sender, EventArgs e)
        {
            Session["ActionTaken"] = "AddNewClarification";
            //string ReceptionistURL = SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.PA/Receptionist.aspx";

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('"+ ReceptionistURL + "','_blank','status=0,toolbar=0,menubar=0,location=0,scrollbars=0,resizable=0,width=800,height=800');", true);

            if (HelperMethods.InGroup(Common.Utilities.Constants.ReceptionistGroupName))
                Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.PA/Receptionist.aspx");
            else if (HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName) || HelperMethods.InGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName))
                Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.PA/ProgramEmployee.aspx");
        }
    }
}