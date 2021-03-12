using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class Add_NewBook : UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload NewBookReplyAttachements;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload NewBookAttachements;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.Session["hdn_BookReqId"] != null)
                {
                    lbl_OrgBookDateVal.Text = DateTime.Now.ToShortDateString();
                    GetBookbyIDForReply(Page.Session["hdn_BookReqId"].ToString());
                    ControlsDisplay();
                }
                else if (Page.Session["PADisplayRequestId"] != null)
                {
                    lbl_BookDateVal.Text = DateTime.Now.ToShortDateString();
                    lbl_BookAuthorVal.Text = SPContext.Current.Web.CurrentUser.Name;
                    BindDropDown();
                    btn_SaveBook.Visible = true;
                    btn_SendbyMail.Visible = true;
                    txt_BookDirectedTo.Visible = false;
                    ReplyControlsdiv.Visible = false;
                }
            }
            BindAttachements();
            BindReplyAttachements();
        }

        private void ControlsDisplay()
        {
            ReplyControlsdiv.Visible = true;
            btn_SaveBook.Visible = false;
            btn_SendbyMail.Visible = false;
            txt_BookID.Enabled = false;
            txt_BookSubject.Enabled = false;
            txt_BookDirectedTo.Enabled = false;
            txt_OrgReplyAddress.Enabled = false;
            txt_OrgEmail.Enabled = false;
            txt_BookText.Enabled = false;
            drp_BookDirectedTo.Visible = false;
        }

        private void GetBookbyIDForReply(string BookReqID)
        {
            Entities.ExternalComms BookDetails = BL.ExternalCommunications.GetBookDetailsbyID(BookReqID);
            if (BookDetails != null)
            {
                txt_BookID.Text = BookDetails.BookID;
                lbl_BookDateVal.Text = BookDetails.BookDate.ToString();
                hdn_ID.Value = BookDetails.ID;
                txt_BookSubject.Text = BookDetails.BookSubject;
                lbl_BookAuthorVal.Text = BookDetails.BookAuthor;
                txt_BookDirectedTo.Text = BookDetails.BookDirectedTo;
                txt_OrgReplyAddress.Text = BookDetails.OrgReplyAddress;
                txt_OrgEmail.Text = BookDetails.OrgEmail;
                txt_BookText.Text = BookDetails.BookText;
            }
        }

        private void BindDropDown()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_NewBook.BindDropDown");
                drp_BookDirectedTo.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));
                drp_BookDirectedTo.Items.Insert(1, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "QatarMinistryForeignAffairs", (uint)LCID), "0"));
                drp_BookDirectedTo.Items.Insert(2, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ScienceInternationalEconomicsFaculty", (uint)LCID), "1"));
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_NewBook.BindDropDown");
            }
        }

        protected void btn_SaveBook_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_NewBook.btn_SaveBook_Click");
                if (!IsRefresh)
                {
                    if (Page.Session["PADisplayRequestId"] != null)
                    {
                        BL.ExternalCommunications.AddNewBook(Page.Session["PADisplayRequestId"].ToString(), txt_BookID.Text, txt_BookSubject.Text, drp_BookDirectedTo.SelectedItem.Text, txt_OrgReplyAddress.Text, txt_OrgEmail.Text, txt_BookText.Text);
                        NewBookAttachements.SaveAttachments();
                        //Response.Redirect("/en/Pages/PAExternalCommunicationsListing.aspx");
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAExternalCommunicationsListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                    }
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
                Logging.GetInstance().Debug("Exiting method Add_NewBook.btn_SaveBook_Click");
            }
        }

        protected void btn_SaveOrgReplyBook_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_NewBook.btn_SaveOrgReplyBook_Click");
                if (!IsRefresh)
                {
                    BL.ExternalCommunications.UpdateBookbyOrgReply(hdn_ID.Value, txt_OrgReply.Text, txt_OrgBookID.Text);
                    NewBookReplyAttachements.SaveAttachments();
                    BusinessHelper.UpdateOrgBookReply(Page.Session["PADisplayRequestId"].ToString());
                    Page.Session["hdn_BookReqId"] = null;
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAExternalCommunicationsListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_NewBook.btn_SaveOrgReplyBook_Click");
            }
        }

        protected void btn_SendbyMail_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_NewBook.btn_SendbyMail_Click");
                if (!IsRefresh)
                {
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_NewBook.btn_SendbyMail_Click");
            }
        }

        private void BindReplyAttachements()
        {
            #region Prerequiestes

            /// add colum "RequestID" lookup from Requests
            /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
            /// add column Group, single line of text

            #endregion Prerequiestes

            #region Edit Mode

            NewBookReplyAttachements.DocumentLibraryName = Utilities.Constants.PARequestsAttachments;
            NewBookReplyAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
            NewBookReplyAttachements.MaxSize = 10240000;//10MB
            NewBookReplyAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NotesPrepared", (uint)LCID);
            NewBookReplyAttachements.Group = "NewBookReplyAttachements";// field name for example, shouldn't be used for more than one field per each control.
            NewBookReplyAttachements.RequestID = Convert.ToInt32(Page.Session["PADisplayRequestId"].ToString());
            NewBookReplyAttachements.SupportedExtensions = "PNG,PDF,JPG";
            NewBookReplyAttachements.IsRequired = false;
            NewBookReplyAttachements.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
            NewBookReplyAttachements.Enabled = true;

            NewBookReplyAttachements.Bind();

            #endregion Edit Mode
        }

        private void BindAttachements()
        {
            #region Prerequiestes

            /// add colum "RequestID" lookup from Requests
            /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
            /// add column Group, single line of text

            #endregion Prerequiestes

            #region Edit Mode

            NewBookAttachements.DocumentLibraryName = Utilities.Constants.PAExternalCommunicationAttachements;
            NewBookAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
            NewBookAttachements.MaxSize = 10240000;//10MB
            NewBookAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NotesPrepared", (uint)LCID);
            NewBookAttachements.Group = "NewBookAttachements";// field name for example, shouldn't be used for more than one field per each control.
            NewBookAttachements.RequestID = Convert.ToInt32(Page.Session["PADisplayRequestId"].ToString());
            NewBookAttachements.SupportedExtensions = "PNG,PDF,JPG";
            NewBookAttachements.IsRequired = false;
            NewBookAttachements.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
            NewBookAttachements.Enabled = true;

            NewBookAttachements.Bind();

            #endregion Edit Mode
        }
    }
}