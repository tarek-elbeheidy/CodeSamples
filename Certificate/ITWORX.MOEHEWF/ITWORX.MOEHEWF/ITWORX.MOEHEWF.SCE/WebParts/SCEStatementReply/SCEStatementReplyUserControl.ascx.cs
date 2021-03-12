using ITWorx.MOEHEWF.Nintex.Actions;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHEWF.SCE.BL;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.SCE.WebParts.SCEStatementReply
{
    public partial class SCEStatementReplyUserControl : UserControlBase
    {

        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ClientSideFileUpload AddFile;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindControls();
            }
            init_FileUpload();
        }
        void init_FileUpload()
        {
            if (Request.QueryString["StatementId"] != null)
            {
                int StatementId = Convert.ToInt32(Request.QueryString["StatementId"]);

                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    AttachmentsLookupListFieldsContentType attachmentInfo = ctx.AttachmentsLookupList.Where(a => a.Group == ITWORX.MOEHEWF.SCE.Utilities.Constants.SCEStatementAttachments).FirstOrDefault();

                    AddFile.Group = attachmentInfo.Group;
                    AddFile.HasOptions = false;
                    AddFile.DocumentLibraryName = attachmentInfo.DocumentLibraryName;
                    AddFile.IsRequired = (bool)attachmentInfo.IsRequired;
                    AddFile.MaxFileNumber = (int)attachmentInfo.MaxFileNumber;
                    AddFile.MaxSize = (int)attachmentInfo.MaxSize;
                    AddFile.SupportedExtensions = attachmentInfo.SupportedExtensions;
                    AddFile.DocLibWebUrl = attachmentInfo.DocLibWebUrl;
                    AddFile.Title = LCID == (int)Language.English ? attachmentInfo.Title : attachmentInfo.TitleAr;
                    AddFile.RequiredValidationMessage = LCID == (int)Language.Arabic ? attachmentInfo.RequiredValidationMessage : attachmentInfo.RequiredValidationMessageAr;
                    AddFile.LookupFieldName = attachmentInfo.LookupFieldName;
                    AddFile.LookupFieldValue = StatementId;
                    AddFile.FileExtensionValidation = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileExtensionValidation", (uint)LCID); //"Supported file extensions are jpg,pdf,png";
                    AddFile.FileSizeValidationMsg = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileMaxValidationMsg", (uint)LCID), AddFile.MaxSize); //"File size must not be greater than " + FileUp1.MaxSize + " MB";
                    AddFile.FileNumbersValidationMsg = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileNumbersValidationMsg", (uint)LCID), AddFile.MaxFileNumber); //"You can't upload more than " + FileUp1.MaxFileNumber + " files";
                    AddFile.FileExistsValidationMsg = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "FileExistsValidationMsg", (uint)LCID); //"File exists with the same name";
                    AddFile.ValidqationGroup = "StatementGroup";
                    AddFile.DisplayMode = true;
                }
            }
        }
        void bindControls()
        {
            try
            {
                Logging.GetInstance().Debug("Enter SCEStatementReplyUserControl.bindControls");

                if (Request.QueryString["StatementId"] != null)
                {
                    int StatementId = Convert.ToInt32(Request.QueryString["StatementId"]);

                    var item = StatementRequestBL.GetStatementByID(StatementId, LCID); 

                    if(item!=null)
                    {
                        txt_statementDate.Text = item.StatementDate;
                        txt_statementTopic.Text = item.StatementSubject;
                        txt_statementAgency.Text = item.StatementAgency;
                        txt_Sender.Text = item.Sender;
                        txt_RequiredStatement.Text = item.RequiredStatement;
                        txt_ReplayDate.Text = string.IsNullOrEmpty(item.ReplayDate) ? ExtensionMethods.QatarFormatedDate(DateTime.Now) : item.ReplayDate;
                        txt_ReplayBy.Text = string.IsNullOrEmpty(item.ReplaySender)?SPContext.Current.Web.CurrentUser.Name:item.ReplaySender;
                        txt_Replay.Text = item.StatementReplay;
                        RequestID_HF.Value = item.RequestID.ToString(); 
                    } 
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SCEStatementReplyUserControl.bindControls");
            }
        }

        void updateStatement()
        {
            try
            {
                Logging.GetInstance().Debug("Enter SCEStatementReplyUserControl.updateStatement");

                if (Request.QueryString["StatementId"] != null)
                {
                    using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                    {
                        int StatementId = Convert.ToInt32(Request.QueryString["StatementId"]);
                        var item = ctx.SCEStatementsRequestsList.ScopeToFolder("", true).Where(x => x.Id == StatementId).FirstOrDefault();
                        if (item != null)
                        {
                            item.ReplyDate = DateTime.Now;
                            item.ReplySenderId = SPContext.Current.Web.CurrentUser.ID;
                            item.StatementReply = txt_Replay.Text;
                        }
                        AddFile.SaveAttachments();
                        ctx.SubmitChanges();

                        //Update RequestLists with return date, from and reason
                        if (!string.IsNullOrEmpty(RequestID_HF.Value))
                        {
                            var requetsItem = ctx.SCERequestsList.ScopeToFolder("", true).Where(x => x.Id == int.Parse(RequestID_HF.Value)).FirstOrDefault();
                            requetsItem.ReturnDate = DateTime.Now;
                            requetsItem.ReturnedBy = SPContext.Current.Web.CurrentUser.Name;
                            requetsItem.ReturnReason = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ReturnFromCultural", (uint)LCID);
                            requetsItem.EmployeeForCultural = string.Empty;
                            ctx.SubmitChanges();
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SCEStatementReplyUserControl.updateStatement");
            }
        }

        protected void btn_Back_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEStatementListing.aspx?RequestId=" + RequestID_HF.Value, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                updateStatement();
                NintexHelper.ContinueTask(ITWorx.MOEHEWF.Nintex.Utilities.Constants.SCECulturalMissionStatementReply, txt_Replay.Text, Utilities.Constants.SCERequests, RequestID_HF.Value);
                Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.SCERequests, Utilities.Constants.SCERequestHistory, LCID, (int)Common.Utilities.RequestStatus.SCECulturalMissionStatementReply, SPContext.Current.Web.CurrentUser.Name, string.Empty, RequestID_HF.Value, "Yes");
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCECulturalMissionDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                updateStatement();
            }
        }
    }
}
