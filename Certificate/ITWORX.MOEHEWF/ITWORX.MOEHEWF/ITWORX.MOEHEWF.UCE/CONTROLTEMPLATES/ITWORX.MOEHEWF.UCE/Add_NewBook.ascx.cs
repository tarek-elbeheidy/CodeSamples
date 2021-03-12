using AjaxControlToolkit.HTMLEditor;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class Add_NewBook : UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload NewBookReplyAttachements;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload NewBookAttachements;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.Session["hdn_BookReqId"] != null && Session["ActionTaken"] != null && Session["ActionTaken"].ToString() == "AddNewBookReply")
                {
                    lbl_OrgBookDateVal.Text = DateTime.Now.ToShortDateString();
                    GetBookbyIDForReply(Page.Session["hdn_BookReqId"].ToString());
                    ControlsDisplay();
                   

                }
                else if (Page.Session["DisplayRequestId"] != null && Session["ActionTaken"] != null && Session["ActionTaken"].ToString() == "AddNewBook")
                {
                    lbl_BookDateVal.Text = DateTime.Now.ToShortDateString();
                    lbl_BookAuthorVal.Text = SPContext.Current.Web.CurrentUser.Name;
                    BindDropDown();
                    btn_SaveBook.Visible = true;
                    //btn_SendbyMail.Visible = true;
                    txt_BookDirectedTo.Visible = false;
                    ReplyControlsdiv.Visible = false;
                  

                }
            }

            if (Page.Session["hdn_BookReqId"] != null && Session["ActionTaken"] != null && Session["ActionTaken"].ToString() == "AddNewBookReply")
            {
                BindReplyAttachements();
            }
            else if (Page.Session["DisplayRequestId"] != null && Session["ActionTaken"] != null && Session["ActionTaken"].ToString() == "AddNewBook")
            {
                BindAttachements();
            }
        }
        private void ControlsDisplay()
        {
            ReplyControlsdiv.Visible = true;
            btn_SaveBook.Visible = false;
            //btn_SendbyMail.Visible = false;
            txt_BookID.Enabled = false;
            txt_BookSubject.Enabled = false;
            txt_BookDirectedTo.Enabled = false;
            txt_OrgReplyAddress.Enabled = false;
            txt_OrgEmail.Enabled = false;
            txt_BookText.ActiveMode = ActiveModeType.Preview;
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
                txt_BookText.Content = BookDetails.BookText;
                txt_BookText.Enabled = false;
                reqVal_OrgReply.Enabled = true;
                reqVal_OrgReplyBookSubject.Enabled = true;
                BindAttachementsViewMode();
            }
        }
        private void BindDropDown()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_NewBook.BindDropDown");
                List<Entities.ExternalComms> Organizations = BL.ExternalCommunications.GetAllOrganizations();
                HelperMethods.BindDropDownList(ref drp_BookDirectedTo, Organizations, "ID", "OrgTitleAr", "OrgTitleEN", LCID);
                drp_BookDirectedTo.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
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
                    if (Page.Session["DisplayRequestId"] != null)
                    {
                        BL.ExternalCommunications.AddNewBook(Page.Session["DisplayRequestId"].ToString(), txt_BookID.Text, txt_BookSubject.Text, drp_BookDirectedTo.SelectedItem.Text, txt_OrgReplyAddress.Text, txt_OrgEmail.Text, txt_BookText.Content);
                        //send email 
                        string SMTPServer = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPServer");
                        string SMTPServerPort = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPServerPort");
                        string SMTPFromAddress = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPFromAddress");
                        string SMTPFromDisplayName = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPFromDisplayName");
                        string SMTPUserName = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPUserName");
                        string SMTPPassword = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPPassword");
                        //string BookID = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "BookID", (uint)LCID) + ":" + txt_BookID.Text;
                        string BookText = txt_BookSubject.Text;
                        NewBookAttachements.SaveAttachments();

                        HelperMethods.SendNotificationEmail(txt_BookText.Content, BookText, SMTPFromAddress, SMTPFromDisplayName,txt_OrgEmail.Text, 
                            SMTPServer, SMTPServerPort, SMTPUserName,SMTPPassword, true, new List<System.Net.Mail.Attachment>());
                        //Response.Redirect("/en/Pages/ExternalCommunicationsListing.aspx");
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ExternalCommunicationsListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
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
                    BL.ExternalCommunications.UpdateBookbyOrgReply(hdn_ID.Value, txt_OrgReply.Text, txt_OrgBookID.Text, txt_OrgReplyBookSubjectVal.Text);
                    NewBookReplyAttachements.SaveAttachments();
                    BusinessHelper.UpdateOrgBookReply(Page.Session["DisplayRequestId"].ToString());
                    Page.Session["hdn_BookReqId"] = null;
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ExternalCommunicationsListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
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
        
                Logging.GetInstance().Debug("Entering method Add_NewBook.BindReplyAttachements");
                try
                {
                #region Prerequiestes
                /// add colum "RequestID" lookup from Requests
                /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
                /// add column Group, single line of text
                #endregion
                #region Edit Mode
                int id = 0;
                if (Page.Session["hdn_BookReqId"] != null && Session["ActionTaken"] != null && Session["ActionTaken"].ToString() == "AddNewBookReply")
                {
                    id = Convert.ToInt32(Page.Session["hdn_BookReqId"].ToString());
                }
                NewBookReplyAttachements.DocumentLibraryName = Utilities.Constants.OrganizationalLettersAttachments;
                NewBookReplyAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
                NewBookReplyAttachements.MaxSize = 7168000;//7MB
                NewBookReplyAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NotesPrepared", (uint)LCID);
                NewBookReplyAttachements.Group = "NewBookReplyAttachements" + Convert.ToString(id);// field name for example, shouldn't be used for more than one field per each control.
                NewBookReplyAttachements.RequestID = Convert.ToInt32(Page.Session["DisplayRequestId"].ToString());
                NewBookReplyAttachements.SupportedExtensions = "PNG,PDF,JPG";
                NewBookReplyAttachements.IsRequired = false;
                NewBookReplyAttachements.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                NewBookReplyAttachements.Enabled = true;

                NewBookReplyAttachements.Bind();
                #endregion
            }
            catch (Exception ex)
                {
                    Logging.GetInstance().LogException(ex);
                }
                finally
                {
                    Logging.GetInstance().Debug("Exiting method Add_NewBook.BindReplyAttachements");
                }
            }
        private void BindAttachementsViewMode()
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
                if (Page.Session["hdn_BookId"] != null)
                {
                    id = Convert.ToInt32(Page.Session["hdn_BookId"].ToString()) + 1;

                }
                else if (Page.Session["First_Id"] != null)
                {
                    id = 0;
                }
                else if (Page.Session["hdn_BookReqId"] != null && Session["ActionTaken"] != null && Session["ActionTaken"].ToString() == "AddNewBookReply")
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
        private void BindAttachements()
        {

            Logging.GetInstance().Debug("Entering method Add_NewBook.BindAttachements");
            try
            {
                #region Prerequiestes
                /// add colum "RequestID" lookup from Requests
                /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
                /// add column Group, single line of text
                #endregion
                #region Edit Mode
                int id = 0;
                if (Page.Session["hdn_BookId"] != null)
                {
                    id = Convert.ToInt32(Page.Session["hdn_BookId"].ToString()) + 1;

                }
                else if (Page.Session["First_Id"] != null)
                {
                    id = 0;
                }
                else if(Page.Session["hdn_BookReqId"] != null && Session["ActionTaken"] != null && Session["ActionTaken"].ToString() == "AddNewBookReply")
                {
                    id = Convert.ToInt32(Page.Session["hdn_BookReqId"].ToString());
                }
                NewBookAttachements.DocumentLibraryName = Utilities.Constants.ExternalCommunicationAttachements;
                NewBookAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
                NewBookAttachements.MaxSize = 7168000;//7MB
                NewBookAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NotesPrepared", (uint)LCID);
                NewBookAttachements.Group = "NewBookAttachements"+ Convert.ToString(id);// field name for example, shouldn't be used for more than one field per each control.
                NewBookAttachements.RequestID = Convert.ToInt32(Page.Session["DisplayRequestId"].ToString());
                NewBookAttachements.SupportedExtensions = "PNG,PDF,JPG";
                NewBookAttachements.IsRequired = false;
                NewBookAttachements.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                NewBookAttachements.Enabled = true;

                NewBookAttachements.Bind();
                #endregion
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_NewBook.BindAttachements");
            }
        }
 
    }
}
