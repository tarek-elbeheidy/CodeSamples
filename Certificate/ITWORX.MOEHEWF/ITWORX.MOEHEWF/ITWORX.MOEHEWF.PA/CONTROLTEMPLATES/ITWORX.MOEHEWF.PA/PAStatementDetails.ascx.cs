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
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class StatementDetails : UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload StatementReplyAttachements;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.Session["hdn_StatementReqId"] != null)
                {
                    BindStatementRequest(Page.Session["hdn_StatementReqId"].ToString());
                    

                }
            }
            BindAttachements();
        }

        private void BindStatementRequest(string StatementReqID)
        {
            try
            {
                Logging.GetInstance().Debug("Enter StatementDetails.BindStatementRequest");
                Entities.RequestsForStatement StatementRequest = BL.RequestsForStatements.GetReqStatementbyID(StatementReqID);
                if (StatementRequest != null)
                {
                    lbl_StatDetailsCreatedbyVal.Text = StatementRequest.StatementCreatedby;
                    lbl_StatDetailsDateVal.Text = StatementRequest.StatementDate.ToString();
                    lbl_StatDetailsDirectedToVal.Text = StatementRequest.DirectedTo;
                    lbl_StatDetailsRequestedVal.Text = StatementRequest.StatementRequested;
                    lbl_StatDetailsSubjectVal.Text = StatementRequest.StatementSubject;
                    lbl_RequestIDVal.Text = StatementRequest.RequestID;

                    if (!string.IsNullOrEmpty(StatementRequest.StatementReply))
                    {
                        lbl_StatDetailsReplybyVal.Text = StatementRequest.StatementReplyby;
                        lbl_StatDetailsReplyDateVal.Text = StatementRequest.StatementReplyDate.ToString();
                        lbl_StatDetailsReplyVal.Text = StatementRequest.StatementReply;

                        ReplyControlsdiv.Visible = true;
                    }
                    else
                        ReplyControlsdiv.Visible=false;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit StatementDetails.BindStatementRequest");
            }
        }
        
        protected void btn_Close_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsRefresh)
                {
                    Logging.GetInstance().Debug("Enter StatementDetails.btn_Close_Click");

                    Page.Session["hdn_StatementReqId"] = null;
                   // Response.Redirect((LCID == 1033 ? "/en" : "/ar") + "/Pages/PAStatementRequestsListing.aspx");
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAStatementRequestsListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
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
        private void BindAttachements()
        {
            #region Prerequiestes
            /// add colum "RequestID" lookup from Requests
            /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
            /// add column Group, single line of text
            #endregion
            #region Display Mode
            int requestId = 0;
            if (Page.Session["PADisplayRequestId"] != null && Page.Session["hdn_StatementReqId"] != null)
            {
                requestId = int.Parse(Convert.ToString(Page.Session["PADisplayRequestId"]));
            }
            StatementReplyAttachements.DocumentLibraryName = Utilities.Constants.PAStatementReqReplyAttachements;
            StatementReplyAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
            StatementReplyAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NotesPrepared", (uint)LCID);
            StatementReplyAttachements.Group = "StatementReplyAttachements";
            StatementReplyAttachements.RequestID = requestId;
            StatementReplyAttachements.Enabled = false;
            StatementReplyAttachements.Bind();
            #endregion
            

        }
    }
}