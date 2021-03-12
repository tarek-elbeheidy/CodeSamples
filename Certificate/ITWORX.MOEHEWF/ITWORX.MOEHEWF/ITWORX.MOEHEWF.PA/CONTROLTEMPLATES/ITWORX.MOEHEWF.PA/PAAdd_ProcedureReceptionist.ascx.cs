using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Nintex.Workflow.HumanApproval;
using ITWORX.MOEHEWF.UCE.Utilities;
using System;
using System.Web;
using System.Web.UI;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class Add_ProcedureReceptionist : Utilities.UserControlBase
    {
        public NintexContext CurrentContext { get; set; }
        public bool IsTaskComplete { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (Page.Session["PADisplayRequestId"] != null)
                {
                    SPSite site = new SPSite(SPContext.Current.Site.Url);
                    SPWeb web = site.OpenWeb();
                    SPListItem requestItem = web.Lists["Requests"].GetItemById(int.Parse(Page.Session["PADisplayRequestId"].ToString()));
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
            if (!Page.IsPostBack)
            {
            }
        }

        protected void btn_ApproveProcedures_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_Procedure.btn_ApproveProcedures_Click");
                if (!IsRefresh)
                {
                    if (Page.Session["PADisplayRequestId"] != null)
                    {
                        BL.AllProcedures.AddProcedureReceptionist(txt_Procedure.Text, Page.Session["PADisplayRequestId"].ToString());
                        NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAProgramManagerReview, txt_Procedure.Text, "PARequests", Page.Session["PADisplayRequestId"].ToString());
                        //Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.PAProgramManagerReview, Page.Session["PADisplayRequestId"].ToString(), "Program Managers".ToLower());
                        //BL.PAHistoricalRecords.AddHistoricalRecord(LCID,"تحويل الطلب إلى مدير برنامج معادلة الشهادات ل مراجعة البيانات والتأكد من صحتها وشموليتها ", SPContext.Current.Web.CurrentUser.Name, "موظف الاستقبال", string.Empty, Page.Session["PADisplayRequestId"].ToString());
                    }
                    //if (LCID==(int)Language.English)
                    //Response.Redirect("/en/Pages/PAProceduresReceptionistListing.aspx");
                    //SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAProceduresReceptionistListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

                    //else
                    //Response.Redirect("/ar/Pages/PAProceduresReceptionistListing.aspx");
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAProceduresReceptionistListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_Procedure.btn_ApproveProcedures_Click");
            }
        }
    }
}