using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Nintex.Workflow.HumanApproval;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class Add_LegalAffairsProcOpinion : UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload legalAffairsAttachements;


        public NintexContext CurrentContext { get; set; }
        public bool IsTaskComplete { get; set; }
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (Page.Session["DisplayRequestId"] != null)
                {
                    SPSite site = new SPSite(SPContext.Current.Site.Url);
                    SPWeb web = site.OpenWeb();
                    SPListItem requestItem = web.Lists["Requests"].GetItemById(int.Parse(Page.Session["DisplayRequestId"].ToString()));
                    SPList list = web.Lists.TryGetList("Workflow Tasks");
                    Guid taskListId = new Guid(list.ID.ToString());
                    int spTaskItemId = requestItem.Tasks[0].ID;
                    // create Nintext context
                    this.CurrentContext = NintexHelper.ParseRequest(spTaskItemId, taskListId);
                    this.CurrentContext.TaskAuthorized = Nintex.Workflow.HumanApproval.User.CheckCurrentUserMatchesHWUser(SPContext.Current.Web, CurrentContext.Approver);
                    // check if a task has already completed
                    IsTaskComplete = (CurrentContext.Approver.ApprovalOutcome != Outcome.Pending);
                    string controlUrl = string.Empty;
                    string contentAction = CurrentContext.TaskItem.ContentType.Name;
                    web.Dispose();
                    site.Dispose();
                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
                BindAttachements();
        }

        protected void btn_ApproveProc_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_LegalAffairsProcUC.btn_ApproveProc_Click");
                if (!IsRefresh)
                {
                    if (Page.Session["DisplayRequestId"] != null)
                    {
                        BL.AllProcedures.AddLegalAffairsProcedureOpinion(txt_InitialOpinion.Text, Page.Session["DisplayRequestId"].ToString());
                        legalAffairsAttachements.SaveAttachments();
                        NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCELegalEmployeeSendNotes, txt_InitialOpinion.Text, "Requests", Page.Session["DisplayRequestId"].ToString());
                        //Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.UCELegalEmployeeSendNotes, Page.Session["DisplayRequestId"].ToString(), "Head Managers".ToLower());

                        //Response.Redirect((LCID == 1033 ? "/en" : "/ar") + "/Pages/ProceduresLegalAffairsOpinion.aspx");
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ProceduresLegalAffairsOpinion.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
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
                Logging.GetInstance().Debug("Exiting method Add_LegalAffairsProcUC.btn_ApproveProc_Click");
            }
        }
        private void BindAttachements()
        {
            #region Prerequiestes
            /// add colum "RequestID" lookup from Requests
            /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
            /// add column Group, single line of text
            #endregion
            #region Edit Mode
            legalAffairsAttachements.DocumentLibraryName = Utilities.Constants.LegalAffairsAttachements;
            legalAffairsAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
            legalAffairsAttachements.MaxSize = 2048000;//2MB
            legalAffairsAttachements.LabelDisplayName = "المذكرات التي تم إعدادها";
            legalAffairsAttachements.Group = "المذكرات التي تم إعدادها";// field name for example, shouldn't be used for more than one field per each control.
            legalAffairsAttachements.RequestID =Convert.ToInt32(Page.Session["DisplayRequestId"].ToString());
            legalAffairsAttachements.SupportedExtensions = "PNG,PDF,JPG";
            legalAffairsAttachements.IsRequired = false;
            legalAffairsAttachements.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
            legalAffairsAttachements.Enabled = true;

            legalAffairsAttachements.Bind();
            #endregion
        }
    }
}
