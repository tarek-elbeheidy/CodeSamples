using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Web;
using System.Web.UI;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class PAClarificationReqDetails : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.Session["hdn_ClarRequestId"] != null)
                {
                    BindClarificationRequest(Page.Session["hdn_ClarRequestId"].ToString());
                }
            }
        }

        private void BindClarificationRequest(string ClarificationID)
        {
            try
            {
                Logging.GetInstance().Debug("Enter PAClarificationReqDetails.BindClarificationRequest");
                Entities.ClarificationReqs ClarificationReq = BL.PAClarificationRequests.GetClarificationbyID(ClarificationID);
                if (ClarificationReq != null)
                {
                    lbl_RequestIDVal.Text = ClarificationReq.RequestID;
                    if (!string.IsNullOrEmpty(ClarificationReq.AssignedTo))
                    {
                        lbl_AssignedToVal.Text = ClarificationReq.AssignedTo;
                        lbl_AssignedTo.Visible = true;
                        lbl_AssignedToVal.Visible = true;
                    }
                    lbl_RequestClarificationDateVal.Text = ClarificationReq.RequestClarificationDate.ToShortDateString();
                    lbl_RequestedClarificationVal.Text = ClarificationReq.RequestedClarification;
                    lbl_RequestSenderVal.Text = ClarificationReq.RequestSender;
                    if (!string.IsNullOrEmpty(ClarificationReq.ClarificationReply))
                    {
                        lbl_ReplyDateVal.Text = ClarificationReq.ReplyDate.ToShortDateString();
                        lbl_ClarificationReplyVal.Text = ClarificationReq.ClarificationReply;
                        lbl_ReplyDateVal.Visible = true;
                        lbl_ReplyDate.Visible = true;
                        lbl_ClarificationReply.Visible = true;
                        lbl_ClarificationReplyVal.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PAClarificationReqDetails.BindClarificationRequest");
            }
        }

        protected void btn_Close_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsRefresh)
                {
                    Logging.GetInstance().Debug("Enter StatementDetails.btn_Close_Click");

                    Page.Session["hdn_ClarRequestId"] = null;
                    if (HelperMethods.InGroup(Common.Utilities.Constants.ApplicanstGroupName))
                        //Response.Redirect((LCID == 1033 ? "/en" : "/ar") + "/Pages/PAClarificationRequestsListing.aspx");
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAClarificationRequestsListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                    else if (HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName) || HelperMethods.InGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName))
                        //Response.Redirect((LCID == 1033 ? "/en" : "/ar") + "/Pages/PAProgramEmployeeClarificationReqs.aspx");
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAProgramEmployeeClarificationReqs.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit StatementDetails.btn_Close_Click");
            }
        }
    }
}