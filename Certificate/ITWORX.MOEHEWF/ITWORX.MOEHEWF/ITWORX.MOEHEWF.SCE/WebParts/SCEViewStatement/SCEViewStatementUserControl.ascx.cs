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

namespace ITWORX.MOEHEWF.SCE.WebParts.SCEViewStatement
{
    public partial class SCEViewStatementUserControl : UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ClientSideFileUpload ViewFile;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindControls();
            }
        }

        void bindControls()
        {
            try
            {
                Logging.GetInstance().Debug("Enter SCEViewStatementUserControl.bindControls");

                if (Request.QueryString["StatementId"] != null)
                {
                    int StatementId = Convert.ToInt32(Request.QueryString["StatementId"]);

                    var item = StatementRequestBL.GetStatementByID(StatementId, LCID);

                    if (item != null)
                    {
                        txt_StatementDate.Text = item.StatementDate;
                        txt_statementTopic.Text = item.StatementSubject;
                        txt_StatementAgency.Text = item.StatementAgency;
                        txt_Sender.Text = item.Sender;
                        txt_ReplayDate.Text = item.ReplayDate;
                        txt_SenderReplay.Text = item.ReplaySender;
                        txt_Requiredstatement.Text = item.RequiredStatement;
                        txt_StatementReplay.Text = item.StatementReplay;
                        RequestID_HF.Value = item.RequestID.ToString();

                        using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                        {
                            AttachmentsLookupListFieldsContentType attachmentInfo = ctx.AttachmentsLookupList.Where(a => a.Group == ITWORX.MOEHEWF.SCE.Utilities.Constants.SCEStatementAttachments).FirstOrDefault();
                            ViewFile.Group = attachmentInfo.Group;
                            ViewFile.DocumentLibraryName = attachmentInfo.DocumentLibraryName;
                            ViewFile.DocLibWebUrl = attachmentInfo.DocLibWebUrl;
                            ViewFile.Title = LCID == (int)Language.English ? attachmentInfo.Title : attachmentInfo.TitleAr;
                            ViewFile.LookupFieldName = attachmentInfo.LookupFieldName;
                            ViewFile.LookupFieldValue = StatementId;
                            ViewFile.DisplayMode = false;
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
                Logging.GetInstance().Debug("Exit SCEViewStatementUserControl.bindControls");
            }
        }

        protected void btn_Back_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEStatementListing.aspx?RequestId=" + RequestID_HF.Value, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
        }
    }
}
