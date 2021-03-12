using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.SCE.WebParts.SCEReplyToBook
{
    public partial class SCEReplyToBookUserControl : Utilities.UserControlBase
    {

        public int? BookId
        {
            get
            {
                if (Request.QueryString["BookId"] != null)
                {
                    ViewState["BookId"] = Request.QueryString["BookId"];
                    return Convert.ToInt32(Request.QueryString["BookId"]);
                }
                else if (ViewState["BookId"] != null)
                {
                    return Convert.ToInt32(ViewState["BookId"]);
                }
                return null;
            }
            set
            {
                ViewState["BookId"] = value;
            }
        }
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
        public int? BookStatus
        {
            get
            {
                if (Request.QueryString["ReqSt"] != null)
                {
                    ViewState["ReqSt"] = Request.QueryString["ReqSt"];
                    return Convert.ToInt32(Request.QueryString["ReqSt"]);
                }
                else if (ViewState["ReqSt"] != null)
                {
                    return Convert.ToInt32(ViewState["ReqSt"]);
                }
                return null;
            }
            set
            {
                ViewState["ReqSt"] = value;
            }
        }
        /// <summary>
        /// FileUp1 control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        /// 
        protected ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ClientSideFileUpload FileUp1;

        /// <summary>
        /// fileUpload2 control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ClientSideFileUpload fileUpload2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadBookRequest();
                LoadDocuments();
                txtReplyDate.Text = DateTime.Now.ToShortDateString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter function SCEBookReplyUserControl.btnSave_Click");
                Page.Validate("BookReply");
                if(Page.IsValid)
                { 
                SaveBookRequest();
                SPUtility.Redirect(string.Format("{0}/Pages/SCEBookRequests.aspx?RequestId={1}", SPContext.Current.Web.Url, RequestId), SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }

            }
            catch (Exception ex)
            {

                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit function SCEBookReplyUserControl.btnSave_Click");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect(string.Format("{0}/Pages/SCEBookRequests.aspx?RequestId={1}", SPContext.Current.Web.Url, RequestId), SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

        }
        private void LoadDocuments()
        {
            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
            {
                AttachmentsLookupListFieldsContentType attachmentInfo = ctx.AttachmentsLookupList.Where(a => a.Group == Utilities.Constants.SCEBookAttachments).FirstOrDefault();
                if (attachmentInfo != null)
                {
                    FileUp1.Group = "SendBook";
                    FileUp1.HasOptions = false;
                    FileUp1.DocumentLibraryName = Utilities.Constants.SCEBookAttachments;
                    FileUp1.IsRequired = (bool)attachmentInfo.IsRequired;
                    FileUp1.MaxFileNumber = (int)attachmentInfo.MaxFileNumber;
                    FileUp1.MaxSize = (int)attachmentInfo.MaxSize;
                    FileUp1.SupportedExtensions = attachmentInfo.SupportedExtensions;
                    FileUp1.DocLibWebUrl = attachmentInfo.DocLibWebUrl;
                    FileUp1.Title = LCID == (int)Language.English ? attachmentInfo.Title : attachmentInfo.TitleAr;
                    FileUp1.LookupFieldName = attachmentInfo.LookupFieldName;
                    FileUp1.LookupFieldValue = BookId ?? 0;
                    FileUp1.FileExtensionValidation = HelperMethods.LocalizedText("SCEBookfileSupportedFormat");
                    FileUp1.FileSizeValidationMsg = string.Format(HelperMethods.LocalizedText("SCEBookFileMaxSize"), 7);
                    FileUp1.FileNumbersValidationMsg = string.Format(HelperMethods.LocalizedText("SCEBookMaxFilesNumber"), FileUp1.MaxFileNumber);
                    FileUp1.FileExistsValidationMsg = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileExistsValidationMsg", (uint)LCID);
                    FileUp1.DisplayMode = false;


                    fileUpload2.Group = "BookReply";
                    fileUpload2.HasOptions = false;
                    fileUpload2.DocumentLibraryName = Utilities.Constants.SCEBookAttachments;
                    fileUpload2.IsRequired = (bool)attachmentInfo.IsRequired;
                    fileUpload2.MaxFileNumber = (int)attachmentInfo.MaxFileNumber;
                    fileUpload2.MaxSize = (int)attachmentInfo.MaxSize;
                    fileUpload2.SupportedExtensions = attachmentInfo.SupportedExtensions;
                    fileUpload2.DocLibWebUrl = attachmentInfo.DocLibWebUrl;
                    fileUpload2.Title = LCID == (int)Language.English ? attachmentInfo.Title : attachmentInfo.TitleAr;
                    fileUpload2.LookupFieldName = attachmentInfo.LookupFieldName;
                    fileUpload2.LookupFieldValue = BookId ?? 0;
                    fileUpload2.FileExtensionValidation = HelperMethods.LocalizedText("SCEBookfileSupportedFormat");
                    fileUpload2.FileSizeValidationMsg = string.Format(HelperMethods.LocalizedText("SCEBookFileMaxSize"), 7);
                    fileUpload2.FileNumbersValidationMsg = string.Format(HelperMethods.LocalizedText("SCEBookMaxFilesNumber"), FileUp1.MaxFileNumber);
                    fileUpload2.FileExistsValidationMsg = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileExistsValidationMsg", (uint)LCID);
                    fileUpload2.DisplayMode = true;
                    if (!HelperMethods.InGroup(Common.Utilities.Constants.SCEEquivalenceEmployeesGroupName))
                    {
                        fileUpload2.DisplayMode = false;
                        btnSave.Visible = false;
                        txtReplySummary.Enabled = false;
                        txtReplyNumber.Enabled = false;
                    }
                    else
                    {
                        fileUpload2.DisplayMode = true;
                    }

                }
            }

        }
        private void LoadBookRequest()
        {
            HttpContext backupContext = HttpContext.Current;

            try
            {
                Logging.GetInstance().Debug("Exit NewSCEBooksRequestUserControl.BindBookRequest");
                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    string loginName = SPContext.Current.Web.CurrentUser.LoginName; // if you need it in your code
                    string rootWebUrl = SPContext.Current.Site.RootWeb.Url;
                    HttpContext.Current = null;

                    using (SCEContextDataContext ctx = new SCEContextDataContext(rootWebUrl))
                    {
                        SCEBooksRequestsListFieldsContentType bookRequest = ctx.SCEBooksRequestsList.ScopeToFolder("", true).Where(c => c.Id == BookId).FirstOrDefault();
                        if (bookRequest != null)
                        {
                            var bookag= ctx.ExCommAgencyList.Where(c => c.Id == bookRequest.BookEntityId).FirstOrDefault();

                            txtBookNumber.Text = bookRequest.BookNumber ?? string.Empty;
                            txtBookSender.Text = bookRequest.PreparedBy ?? string.Empty;
                            txtBookDate.Text = bookRequest.BookDate ?? string.Empty;
                            txtBookSubject.Text = bookRequest.BookSubject ?? string.Empty;
                            txtAuthorityAddress.Text = bookRequest.EntityAddress ?? string.Empty;
                            txtAuthorityEmail.Text = bookRequest.EntityMail ?? string.Empty;
                            txtBookBody.Text = bookRequest.BookBody ?? string.Empty;
                            if(bookag!=null)
                                txtAuthority.Text = LCID == (int)Language.English ?  bookag.Title: bookag.TitleAr;

                            txtReplyDate.Text = bookRequest.ReplyDate ?? DateTime.Now.ToShortDateString();
                            txtReplyNumber.Text = bookRequest.ReplyNumber ?? string.Empty;
                            txtReplySummary.Text = bookRequest.EntityReply ?? string.Empty;
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
                if (HttpContext.Current == null)
                {
                    HttpContext.Current = backupContext;
                }

                Logging.GetInstance().Debug("Exit NewSCEBooksRequestUserControl.BindBookRequest");
            }
        }

        private void SaveBookRequest()
        {
            HttpContext backupContext = HttpContext.Current;
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string loginName = SPContext.Current.Web.CurrentUser.LoginName; // if you need it in your code
                string rootWebUrl = SPContext.Current.Site.RootWeb.Url;
                HttpContext.Current = null;

                using (SPSite site = new SPSite(rootWebUrl))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Enter method NewSCEBooksRequestUserControl.AddBookRequest");


                            using (SCEContextDataContext ctx = new SCEContextDataContext(rootWebUrl))
                            {
                                try
                                {

                                    SCEBooksRequestsListFieldsContentType bookRequest = null;
                                    if (BookId != null)
                                        bookRequest = ctx.SCEBooksRequestsList.ScopeToFolder("", true).Where(c => c.Id == BookId).FirstOrDefault();

                                    if (bookRequest != null)
                                    {
                                        bookRequest.ReplyNumber = txtReplyNumber.Text;
                                        bookRequest.ReplyDate = DateTime.Now.ToShortDateString();
                                        bookRequest.EntityReply = txtReplySummary.Text;
                                        fileUpload2.SaveAttachments();

                                    }

                                }
                                catch (Exception ex)
                                {

                                    Logging.GetInstance().LogException(ex);


                                }

                                ctx.SubmitChanges();

                            }


                        }
                        catch (Exception funEx)
                        {
                            Logging.GetInstance().LogException(funEx);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exit method NewSCEBooksRequestUserControl.AddBookRequest");
                            // resetting the SPContext
                            if (HttpContext.Current == null)
                            {
                                HttpContext.Current = backupContext;
                            }

                        }

                    }
                }

            });
        }

    }
}
