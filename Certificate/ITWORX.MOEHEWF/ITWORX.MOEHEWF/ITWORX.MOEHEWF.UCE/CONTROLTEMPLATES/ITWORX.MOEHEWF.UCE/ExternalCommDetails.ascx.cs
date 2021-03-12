using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class ExternalCommDetails : UserControlBase
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
                Logging.GetInstance().Debug("Entering method ExternalCommDetails.BindExternalCommunication");
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
                        lbl_OrgBookSubjectVal.Text = ExternalCommunication.OrgReplyBookSubject;
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
                Logging.GetInstance().Debug("Exiting method ExternalCommDetails.BindExternalCommunication");
            }
        }
        private void BindAttachements()
        {
            Logging.GetInstance().Debug("Entering method ExternalCommDetails.BindAttachements");
            try
            {

                #region Prerequiestes
                /// add colum "RequestID" lookup from Requests
                /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
                /// add column Group, single line of text
                #endregion

                #region Display Mode
                int id = 0;
                if (Page.Session["First_Id"] != null)
                {
                    id = 0;
                }
                else
                {
                    id = Convert.ToInt32(Page.Session["hdn_BookReqId"].ToString());
                }
                NewBookAttachements.DocumentLibraryName = Utilities.Constants.ExternalCommunicationAttachements;
            NewBookAttachements.DocLibWebUrl = SPContext.Current.Site.Url;

            NewBookAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NotesPrepared", (uint)LCID);
            NewBookAttachements.Group = "NewBookAttachements" + Convert.ToString(id);
                NewBookAttachements.RequestID = Convert.ToInt32(Page.Session["DisplayRequestId"].ToString());
            NewBookAttachements.Enabled = false;

            NewBookAttachements.Bind();
            #endregion
                     
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method ExternalCommDetails.BindAttachements");
            }
        }
        private void BindReplyAttachements()
        {
            Logging.GetInstance().Debug("Entering method ExternalCommDetails.BindReplyAttachements");
            try
            {
                #region Prerequiestes
                /// add colum "RequestID" lookup from Requests
                /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
                /// add column Group, single line of text
                #endregion

                #region Display Mode
                int id = 0;
                if (Page.Session["hdn_BookReqId"] != null )
                {
                    id = Convert.ToInt32(Page.Session["hdn_BookReqId"].ToString());
                }
                NewBookReplyAttachements.DocumentLibraryName = Utilities.Constants.OrganizationalLettersAttachments;
            NewBookReplyAttachements.DocLibWebUrl = SPContext.Current.Site.Url;

            NewBookReplyAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NotesPrepared", (uint)LCID);
            NewBookReplyAttachements.Group = "NewBookReplyAttachements" + Convert.ToString(id);
                NewBookReplyAttachements.RequestID = Convert.ToInt32(Page.Session["DisplayRequestId"].ToString());
            NewBookReplyAttachements.Enabled = false;

            NewBookReplyAttachements.Bind();
                #endregion
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method ExternalCommDetails.BindReplyAttachements");
            }
        }
        protected void btn_Close_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method ExternalCommDetails.btn_Close_Click");
                if (!IsRefresh)
                {
                    Page.Session["hdn_BookReqId"] = null;
                    //Response.Redirect("/en/Pages/ExternalCommunicationsListing.aspx");
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ExternalCommunicationsListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
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
                Logging.GetInstance().Debug("Exiting method ExternalCommDetails.btn_Close_Click");
            }
        }
    }
}
