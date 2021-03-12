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
    public partial class PAExternalCommDetails : UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload NewBookAttachements;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload NewBookReplyAttachements;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.Session["hdn_BookReqId"] != null)
                {
                    BindExternalCommunication(Page.Session["hdn_BookReqId"].ToString());
                    BindAttachements();
                }
            }
        }

        private void BindExternalCommunication(string BookReqId)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method PAExternalCommDetails.BindExternalCommunication");
                Entities.ExternalComms ExternalCommunication = BL.ExternalCommunications.GetBookDetailsbyID(BookReqId);
                if (ExternalCommunication != null)
                {
                    lbl_BookIDVal.Text = ExternalCommunication.BookID;
                    hdn_ID.Value = ExternalCommunication.ID;
                    lbl_BookDateVal.Text = ExternalCommunication.BookDate;
                    lbl_BookAuthorVal.Text = ExternalCommunication.BookAuthor;
                    lbl_BookSubjectVal.Text = ExternalCommunication.BookSubject;
                    lbl_DirectedToVal.Text = ExternalCommunication.BookDirectedTo;
                    lbl_BookTextVal.Text = ExternalCommunication.BookText;
                    lbl_OrgAddressVal.Text = ExternalCommunication.OrgReplyAddress;
                    lbl_OrgEmailVal.Text = ExternalCommunication.OrgEmail;

                    if (!string.IsNullOrEmpty(ExternalCommunication.OrgReply))
                    {
                        lbl_OrgBookIDVal.Text = ExternalCommunication.OrgReplyBookNo;
                        lbl_OrgBookDateVal.Text = ExternalCommunication.OrgReplyDate;
                        lbl_OrgReplyVal.Text = ExternalCommunication.OrgReply;
                        pnl_OrgReply.Visible = true;
                        BindReplyAttachements();
                    }
                    else
                        pnl_OrgReply.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method PAExternalCommDetails.BindExternalCommunication");
            }
        }

        private void BindAttachements()
        {
            #region Prerequiestes

            /// add colum "RequestID" lookup from Requests
            /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
            /// add column Group, single line of text

            #endregion Prerequiestes

            #region Display Mode

            NewBookAttachements.DocumentLibraryName = Utilities.Constants.PAExternalCommunicationAttachements;
            NewBookAttachements.DocLibWebUrl = SPContext.Current.Site.Url;

            NewBookAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NotesPrepared", (uint)LCID);
            NewBookAttachements.Group = "NewBookAttachements";
            NewBookAttachements.RequestID = Convert.ToInt32(Page.Session["PADisplayRequestId"].ToString());
            NewBookAttachements.Enabled = false;

            NewBookAttachements.Bind();

            #endregion Display Mode
        }

        private void BindReplyAttachements()
        {
            #region Prerequiestes

            /// add colum "RequestID" lookup from Requests
            /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
            /// add column Group, single line of text

            #endregion Prerequiestes

            #region Display Mode

            NewBookReplyAttachements.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
            NewBookReplyAttachements.DocLibWebUrl = SPContext.Current.Site.Url;

            NewBookReplyAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NotesPrepared", (uint)LCID);
            NewBookReplyAttachements.Group = "NewBookReplyAttachements";
            NewBookReplyAttachements.RequestID = Convert.ToInt32(Page.Session["PADisplayRequestId"].ToString());
            NewBookReplyAttachements.Enabled = false;

            NewBookReplyAttachements.Bind();

            #endregion Display Mode
        }

        protected void btn_Close_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method PAExternalCommDetails.btn_Close_Click");
                if (!IsRefresh)
                {
                    Page.Session["hdn_BookReqId"] = null;
                    //Response.Redirect("/en/Pages/PAExternalCommunicationsListing.aspx");
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAExternalCommunicationsListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method PAExternalCommDetails.btn_Close_Click");
            }
        }
    }
}