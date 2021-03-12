using ITWORX.MOEHE.Integration.SMS;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using commonUtilities = ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Nintex.Workflow.HumanApproval;
using Portiva.CustomTaskForm.ControlTemplates;
using System;
using System.Web;
using System.Web.UI;
using commonBL = ITWORX.MOEHEWF.Common.BL;
using requestBL = ITWORX.MOEHEWF.PA.BL;
using ITWORX.MOEHE.Utilities;
using System.Threading;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class Add_Clarification : Utilities.UserControlBase
    {
        public NintexContext CurrentContext { get; set; }
        public NintexBaseUserControl CurrentControl { get; set; }
        public bool IsTaskComplete { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (Page.Session["PADisplayRequestId"] != null)
                {
                    SPSite site = new SPSite(SPContext.Current.Site.Url);
                    SPWeb web = site.OpenWeb();
                    SPListItem requestItem = web.Lists["PARequests"].GetItemById(int.Parse(Page.Session["PADisplayRequestId"].ToString()));
                    SPList list = web.Lists.TryGetList("Workflow Tasks");
                    Guid taskListId = new Guid(list.ID.ToString());
                    int spTaskItemId = requestItem.Tasks[0].ID;
                    // create Nintext context
                    this.CurrentContext = NintexHelper.ParseRequest(spTaskItemId, taskListId);
                    this.CurrentContext.TaskAuthorized = Nintex.Workflow.HumanApproval.User.CheckCurrentUserMatchesHWUser(SPContext.Current.Web, CurrentContext.Approver);
                    // check if a task has already completed
                    IsTaskComplete = (CurrentContext.Approver.ApprovalOutcome != Outcome.Pending);
                    string contentAction = CurrentContext.TaskItem.ContentType.Name;
                    web.Dispose();
                    site.Dispose();
                }
            }
            catch (Exception ex)
            {
                // show error
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (HelperMethods.InGroup(Common.Utilities.Constants.Applicants))
                {
                  
                    lnkClarRequestDetails.Visible = true;
                }
                //To Reply on Clarification Request
                if (Page.Session["PADisplayRequestId"] != null)
                {
                    int requestStatus = commonBL.Request.GetPARequesStaustByRequestNumber(int.Parse(Page.Session["PADisplayRequestId"].ToString()));
                    if (Page.Session["hdn_ClarRequestId"] != null &&(requestStatus == Convert.ToInt32(commonUtilities.RequestStatus.PAEmployeeNeedsClarification)|| requestStatus == Convert.ToInt32(commonUtilities.RequestStatus.PADraftForClarification)))
                    {
                        GetClarificationRequest_ForReply(Page.Session["hdn_ClarRequestId"].ToString());
                        txt_ClarRequested.Enabled = false;
                        txt_ClarReply.Enabled = true;
                        btn_Reply.Visible = true;
                        btn_Cancel.Visible = true;
                        btn_AddNewClarification.Visible = false;
                    }
                    else
                    {
                        //To Add new Clarification Request
                        txt_ClarRequested.Enabled = true;
                        txt_ClarReply.Visible = false;
                        lbl_ClarReply.Visible = false;
                        btn_Reply.Visible = false;
                        btn_Cancel.Visible = false;
                        btn_AddNewClarification.Visible = true;
                    }
                }
            }
            //Commented till the demo is finished
            //if ((IsTaskComplete) || (!this.CurrentContext.TaskAuthorized))
            //{
            //    // set form disabed
            //}
        }

        private void GetClarificationRequest_ForReply(string ClarRequestID)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_Clarification.GetClarificationRequest_ForReply");
                Entities.ClarificationReqs ClarReqs = BL.PAClarificationRequests.GetClarRequest_ForReply(ClarRequestID);
                if (ClarReqs != null)
                {
                    txt_ClarRequested.Text = ClarReqs.RequestedClarification;
                    hdn_ID.Value = ClarReqs.ID;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_Clarification.GetClarificationRequest_ForReply");
            }
        }

        protected void btn_Reply_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_Clarification.btn_Reply_Click");
                if (!IsRefresh)
                {
                    if (Page.IsValid)
                    {
                        
                        if (Page.Session["PAEditRequestId"] != null)
                        {
                            BL.PAClarificationRequests.AddClarRequestReply(Convert.ToInt32(hdn_ID.Value), txt_ClarReply.Text);
                            BL.Request.UpdateNeedsClarificationRequestStatus(int.Parse(Page.Session["PAEditRequestId"].ToString()));
                            NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAEmployeeClarificationReplay, txt_ClarRequested.Text, "PARequests", Page.Session["PAEditRequestId"].ToString());
                            Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests, Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PAEmployeeClarificationReplay, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["PAEditRequestId"].ToString(), "Yes");
                            requestBL.Request.UpdateRejection(new Entities.PARequest()
                            {
                                ID = Convert.ToInt32(Page.Session["EditRequestId"]),
                                RejectedFrom = SPContext.Current.Web.CurrentUser.Name,
                                RejectionReason = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "clarificationReplay", (uint)LCID),
                                RejectionDate = DateTime.Now
                            });
                        }
                       // Thread.Sleep(5000);
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/trackrequest.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_Clarification.btn_Reply_Click");
            }
        }

        protected void btn_AddNewClarification_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_Clarification.btn_AddNewClarification_Click");
                if (!IsRefresh)
                {
                    BL.PAClarificationRequests.AddRequestClar(txt_ClarRequested.Text, Page.Session["PADisplayRequestId"].ToString());
                    //Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.PAReceptionistNeedsClarification,Page.Session["PADisplayRequestId"].ToString(), "i:0#.w|moehe\\ApplicantA".ToLower());
                    //BL.PAHistoricalRecords.AddHistoricalRecord(LCID,"إرسال طلب توضيح للطالب", SPContext.Current.Web.CurrentUser.Name , "موظف استقبال", string.Empty, Page.Session["PADisplayRequestId"].ToString());

                    NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAReceptionistNeedsClarification, txt_ClarRequested.Text, "PARequests", Page.Session["PADisplayRequestId"].ToString());
                    Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests, Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PAEmployeeNeedsClarification, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["PADisplayRequestId"].ToString(), "Yes");

                    Entities.PARequest requestItem = BL.Request.GetRequestByNumber(Convert.ToInt32(base.Page.Session["PADisplayRequestId"].ToString()), LCID);
                    Applicants applicant = Common.BL.Applicants.GetApplicantByID(Convert.ToInt32(requestItem.ApplicantID.ID), LCID);
                    Notifications smsNotifications = Common.BL.Notifications.GetSubmittedNotification((int)Common.Utilities.NotificationType.SMS, (int)Common.Utilities.RequestStatus.PAEmployeeNeedsClarification);
                    if (smsNotifications != null)
                    {
                        //Send SMS here
                        Texting.SendSMS(applicant.MobileNumber, string.Format(smsNotifications.Body, requestItem.RequestNumber, "Equivalency", requestItem.RequestNumber, "الموافقة المسبقة"));
                    }
                     
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_Clarification.btn_AddNewClarification_Click");
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/paArabicAsianProgramEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
        }

        protected void lnkClarRequestDetails_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter Add_Clarification.lnkClarRequestDetails_Click");
            try
            {
                SPUtility.Redirect(SPContext.Current.Web.Url + "/" + Utilities.Constants.NeedsClarEditRequestPage, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);

            }
            finally
            {
                Logging.GetInstance().Debug("Exit Add_Clarification.lnkClarRequestDetails_Click");
            }
        }

        protected void custReply_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            if (Page.Session["PAEditRequestId"] != null)
            {
                int requestId = int.Parse(Convert.ToString(Page.Session["PAEditRequestId"]));
                Entities.PARequest request = BL.Request.GetRequestByNumber(requestId, LCID);
                if (int.Parse(request.RequestStatus.Code) == (int)Common.Utilities.RequestStatus.PADraftForClarification)
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAClarificationRequestsListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

        }
    }
}