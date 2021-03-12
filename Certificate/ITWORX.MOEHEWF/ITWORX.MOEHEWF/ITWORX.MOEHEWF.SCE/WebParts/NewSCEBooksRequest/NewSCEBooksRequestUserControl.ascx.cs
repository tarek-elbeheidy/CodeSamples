using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.Common;
using Microsoft.SharePoint.Utilities;
using System.Net.Mail;
using System.Web;

namespace ITWORX.MOEHEWF.SCE.WebParts.NewSCEBooksRequest
{
    public partial class NewSCEBooksRequestUserControl : UserControlBase
    {
        #region Public Properties
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
        public bool IsViewOnly
        {
            get
            {
                if (ViewState["IsViewOnly"] != null)
                {
                    bool isViewOnly = bool.Parse(ViewState["IsViewOnly"].ToString());

                    return isViewOnly;
                }
                else
                    return false;
            }
            set
            {
                ViewState["IsViewOnly"] = value;
                txtBookBody.Enabled =
                   txtBooksubject.Enabled =
                   txtBookNumber.Enabled=
                   txtEntityEmail.Enabled =
                   ddlExternalEntities.Enabled =
                   txtEntityAddress.Enabled =
                   btnSave.Visible =
                   btnSendEmail.Visible =
                  FileUp1.DisplayMode = !value;
            }


        }

        public string ExtCommunicationTemp
        {
            get
            {
                return HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ExtCommunicationTemp", (uint)LCID);
            }
        }

        /// <summary>
        /// FileUp1 control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        public ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ClientSideFileUpload FileUp1;

        #endregion
        #region handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter NewSCEBooksRequestUserControl.Page_Load");

                if (!IsPostBack)
                {
                    LoadDocuments(); 
                    BindDropDownLists();
                    if (BookStatus != null)
                    {
                        ExternalBookRequestStatus status = (ExternalBookRequestStatus)BookStatus;
                        if (!HelperMethods.InGroup(Common.Utilities.Constants.SCEEquivalenceEmployeesGroupName))
                        {
                            status = ExternalBookRequestStatus.View;
                        }
                        switch (status)
                        {
                            case ExternalBookRequestStatus.New:
                                BindReadOnlyData();
                                IsViewOnly = false;
                                break;
                            case ExternalBookRequestStatus.Edit:
                                LoadBookRequest();
                                IsViewOnly = false;
                                break;
                            case ExternalBookRequestStatus.View:
                                LoadBookRequest();
                                IsViewOnly = true;
                                break;

                            //case ExternalBookRequestStatus.Saved:
                            //    break;
                            //case ExternalBookRequestStatus.Sent:
                            //    break;
                            default:
                                break;
                        }
                       
                    }
                }



            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                lblExceptionMessage.Visible = true;
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewSCEBooksRequestUserControl.Page_Load");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter NewSCEBooksRequestUserControl.btnSave_Click");
                ValidateForm();
                Page.Validate("AddBook");
                if (Page.IsValid)
                {
                    SaveBookRequest(false);
                    SPUtility.Redirect(string.Format("{0}/Pages/SCEBookRequests.aspx?RequestId={1}", SPContext.Current.Web.Url, RequestId), SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                lblExceptionMessage.Visible = true;
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewSCEBooksRequestUserControl.btnSave_Click");

            }
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter NewSCEBooksRequestUserControl.btnSendEmail_Click");
                ValidateForm();
                Page.Validate("AddBook");
                if (Page.IsValid)
                {
                    SaveBookRequest(true);
                    SPUtility.Redirect(string.Format("{0}/Pages/SCEBookRequests.aspx?RequestId={1}", SPContext.Current.Web.Url, RequestId), SPRedirectFlags.DoNotEndResponse, HttpContext.Current);


                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                lblExceptionMessage.Visible = true;
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewSCEBooksRequestUserControl.btnSendEmail_Click");

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect(string.Format("{0}/Pages/SCEBookRequests.aspx?RequestId={1}", SPContext.Current.Web.Url, RequestId), SPRedirectFlags.DoNotEndResponse, HttpContext.Current);


        }
        #endregion
        #region Private Methods



        private void BindReadOnlyData()
        {
            txtBookEditor.Text = SPContext.Current.Web.CurrentUser.Name;
            txtBookDate.Value = ExtensionMethods.QatarFormatedDate(DateTime.Now) + ExtensionMethods.QatarFormatedDateReturnTime(DateTime.Now);//DateTime.Now.ToShortDateString();
        }
        private void ValidateForm()
        {
            rfvBookBody.IsValid = !string.IsNullOrWhiteSpace(txtBookBody.Text);
            rfvBookSubject.IsValid = !string.IsNullOrWhiteSpace(txtBooksubject.Text);
            rfvBookNumber.IsValid = !string.IsNullOrWhiteSpace(txtBookNumber.Text);
            rfvEntityEmail.IsValid = !string.IsNullOrWhiteSpace(txtEntityEmail.Text);
            rfvExternalEntities.IsValid = !(ddlExternalEntities.SelectedIndex == 0);

        }
        private void BindDropDownLists()
        {
            try
            {
                Logging.GetInstance().Debug("Exit NewSCEBooksRequestUserControl.BindDropDownLists");
                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                    {
                        List<ExCommAgencyListFieldsContentType> Agencies = ctx.ExCommAgencyList.ScopeToFolder("", true).ToList();
                        HelperMethods.BindDropDownList(ref ddlExternalEntities, Agencies, "ID", "TitleAr", "TitleAr", LCID);
                        ddlExternalEntities.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ChooseValue", (uint)LCID), "0"));

                        ddlExternalEntities.AppendDataBoundItems = true;
                    }
                });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewSCEBooksRequestUserControl.BindDropDownLists");
            }

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
                    FileUp1.DocumentLibraryName = attachmentInfo.DocumentLibraryName;
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
                    FileUp1.DisplayMode = true;
                    FileUp1.ValidqationGroup = "Upload";


                }
            }

        }

        private void SaveBookRequest(bool SendMail)
        {
            HttpContext backupContext = HttpContext.Current;
            int userID = SPContext.Current.Web.CurrentUser.ID;
            string rootWebUrl = SPContext.Current.Site.RootWeb.Url;
            HttpContext.Current = null;


            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(rootWebUrl))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Enter method NewSCEBooksRequestUserControl.AddBookRequest");

                            string SMTPServer = HelperMethods.GetConfigurationValue(rootWebUrl + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPServer");
                            string SMTPServerPort = HelperMethods.GetConfigurationValue(rootWebUrl + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPServerPort");
                            string SMTPFromAddress = HelperMethods.GetConfigurationValue(rootWebUrl + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPFromAddress");
                            string SMTPFromDisplayName = HelperMethods.GetConfigurationValue(rootWebUrl + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPFromDisplayName");
                            string SMTPUserName = HelperMethods.GetConfigurationValue(rootWebUrl + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPUserName");
                            string SMTPPassword = HelperMethods.GetConfigurationValue(rootWebUrl + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPPassword");

                            web.AllowUnsafeUpdates = true;
                            string folderUrl = DateTime.Now.ToString("yyyy/MM/dd");
                            SPList list = web.Lists[Utilities.Constants.SCEBooksRequests];
                            List<Attachment> attachments = new List<Attachment>();

                            using (SCEContextDataContext ctx = new SCEContextDataContext(rootWebUrl))
                            {
                                try
                                {


                                    SCEBooksRequestsListFieldsContentType bookRequest = null;
                                    if (BookId != null)
                                        bookRequest = ctx.SCEBooksRequestsList.ScopeToFolder("", true).Where(c => c.Id == BookId).FirstOrDefault();

                                    if (bookRequest != null)
                                    {
                                        bookRequest.BookBody = txtBookBody.Text;
                                        bookRequest.BookSubject = txtBooksubject.Text;
                                        bookRequest.BookNumber = txtBookNumber.Text;
                                        bookRequest.EntityAddress = txtEntityAddress.Text;
                                        bookRequest.EntityMail = txtEntityEmail.Text;
                                        bookRequest.PreparedById = userID;
                                        bookRequest.BookDate = txtBookDate.Value;


                                        attachments = FileUp1.SaveAttachments();
                                        //bookRequest.BookDate
                                        if (ddlExternalEntities.SelectedValue != "0")
                                            bookRequest.BookEntityId = int.Parse(ddlExternalEntities.SelectedValue);
                                        if (SendMail)
                                        {
                                            bookRequest.BookDate = ExtensionMethods.QatarFormatedDate(DateTime.Now) + ExtensionMethods.QatarFormatedDateReturnTime(DateTime.Now);//DateTime.Now.ToShortDateString();
                                            string ManagerMail = HelperMethods.GetConfigurationValue(rootWebUrl + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SCEHeadManagerMail");

                                            bookRequest.BookStatus = ((int)ExternalBookRequestStatus.Sent).ToString();
                                            HelperMethods.SendNotificationEmail(bookRequest.BookBody, bookRequest.BookSubject, SMTPFromAddress, SMTPFromDisplayName,
                                                ManagerMail, SMTPServer, SMTPServerPort, SMTPUserName, SMTPPassword, false, attachments);

                                        }
                                        else
                                        {
                                            bookRequest.BookStatus = ((int)ExternalBookRequestStatus.Saved).ToString();

                                        }
                                        ctx.SubmitChanges();

                                    }
                                    else
                                    {
                                        bookRequest = new SCEBooksRequestsListFieldsContentType();

                                        bookRequest.BookBody = txtBookBody.Text;
                                        bookRequest.BookSubject = txtBooksubject.Text;
                                        bookRequest.BookNumber = txtBookNumber.Text;
                                        bookRequest.EntityAddress = txtEntityAddress.Text;
                                        bookRequest.EntityMail = txtEntityEmail.Text;
                                        bookRequest.BookDate = txtBookDate.Value;
                                        bookRequest.PreparedById = userID;
                                        if (ddlExternalEntities.SelectedValue != "0")
                                            bookRequest.BookEntityId = int.Parse(ddlExternalEntities.SelectedValue);
                                        bookRequest.RequestIDId = RequestId;
                                        SPFolder folder = Common.Utilities.BusinessHelper.CreateFolderInternal(list, list.RootFolder, folderUrl);
                                        bookRequest.Path = folder.Url;
                                        ctx.SCEBooksRequests.InsertOnSubmit(bookRequest);
                                        ctx.SubmitChanges();
                                        string[] IDS = FileUp1.UploadedFilesIDS.Split(',');
                                        List<string> objColumns = new List<string>();


                                        foreach (var id in IDS)
                                        {
                                            if (!string.IsNullOrEmpty(id))
                                                objColumns.Add("ID;Counter;Eq;" + id);
                                        }
                                        attachments = FileUp1.SaveAttachments((int)bookRequest.Id, objColumns);
                                        if (SendMail)
                                        {
                                            bookRequest.BookDate = ExtensionMethods.QatarFormatedDate(DateTime.Now) + ExtensionMethods.QatarFormatedDateReturnTime(DateTime.Now);//DateTime.Now.ToShortDateString();
                                            string ManagerMail = HelperMethods.GetConfigurationValue(rootWebUrl + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SCEHeadManagerMail");

                                            //HelperMethods.SendNotificationEmail()
                                            bookRequest.BookStatus = ((int)ExternalBookRequestStatus.Sent).ToString();
                                            HelperMethods.SendNotificationEmail(bookRequest.BookBody, bookRequest.BookSubject, SMTPFromAddress, SMTPFromDisplayName,
                                              ManagerMail, SMTPServer, SMTPServerPort, SMTPUserName, SMTPPassword, false, attachments);
                                        }
                                        else
                                        {
                                            bookRequest.BookStatus = ((int)ExternalBookRequestStatus.Saved).ToString();

                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logging.GetInstance().LogException(ex);
                                }
                            }
                        }
                        catch (Exception funEx)
                        {
                            Logging.GetInstance().LogException(funEx);
                        }
                        finally
                        {
                            // resetting the SPContext
                            if (HttpContext.Current == null)
                            {
                                HttpContext.Current = backupContext;
                            }

                            Logging.GetInstance().Debug("Exit method NewSCEBooksRequestUserControl.AddBookRequest");

                        }

                    }
                }

            });
        }

        protected void ddlExternalEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlExternalEntities.SelectedIndex!=0)
                txtBookBody.Text = string.Format(ExtCommunicationTemp, ddlExternalEntities.SelectedItem.ToString(), ddlExternalEntities.SelectedItem.ToString());
            else
                txtBookBody.Text=string.Empty;

            BindReadOnlyData();
        }

        private void LoadBookRequest()
        {
            HttpContext backupContext = HttpContext.Current;
            try
            {
                string rootWebUrl = SPContext.Current.Site.RootWeb.Url;
                HttpContext.Current = null;

                Logging.GetInstance().Debug("Exit NewSCEBooksRequestUserControl.BindBookRequest");
                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (SCEContextDataContext ctx = new SCEContextDataContext(rootWebUrl))
                    {
                        SCEBooksRequestsListFieldsContentType bookRequest = ctx.SCEBooksRequestsList.ScopeToFolder("", true).Where(c => c.Id == BookId).FirstOrDefault();
                        if (bookRequest != null)
                        {
                            txtBookNumber.Text = bookRequest.BookNumber ?? string.Empty;
                            txtBookEditor.Text = bookRequest.PreparedBy ?? string.Empty;
                            txtBookDate.Value = bookRequest.BookDate ?? string.Empty;
                            txtBooksubject.Text = bookRequest.BookSubject ?? string.Empty;
                            txtEntityAddress.Text = bookRequest.EntityAddress ?? string.Empty;
                            txtEntityEmail.Text = bookRequest.EntityMail ?? string.Empty;
                            txtBookBody.Text = bookRequest.BookBody ?? string.Empty;
                            ddlExternalEntities.SelectedValue = bookRequest.BookEntityId != null ? bookRequest.BookEntityId.ToString() : "0";
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
                // resetting the SPContext
                if (HttpContext.Current == null)
                {
                    HttpContext.Current = backupContext;
                }

                Logging.GetInstance().Debug("Exit NewSCEBooksRequestUserControl.BindBookRequest");
            }
        }
        #endregion

    }
}
