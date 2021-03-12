using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nintex.Workflow.HumanApproval;
using Portiva.CustomTaskForm.ControlTemplates;
using Microsoft.SharePoint.Utilities;
using System.Web;
using commonUtilities = ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHE.Integration.SMS;
using commonBL = ITWORX.MOEHEWF.Common.BL;
using System.Threading;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class Add_Clarification : UserControlBase
    {
        public NintexContext CurrentContext { get; set; }
        public NintexBaseUserControl CurrentControl { get; set; }
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
                //To Reply on Clarification Request


                if (HelperMethods.InGroup(Common.Utilities.Constants.Applicants))
                {
                    lbl_Warning.Visible = false;
                    lnkClarRequestDetails.Visible = true;
                }
                int requestStatus = commonBL.Request.GetRequesStaustByRequestNumber(int.Parse(Page.Session["DisplayRequestId"].ToString()));

                if (Page.Session["hdn_ClarRequestId"] != null && (requestStatus == Convert.ToInt32(commonUtilities.RequestStatus.UCEProgramEmployeeNeedsClarification) || requestStatus == Convert.ToInt32(commonUtilities.RequestStatus.UCEDraftForClarification)))
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
        private void GetClarificationRequest_ForReply(string ClarRequestID)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_Clarification.GetClarificationRequest_ForReply");
                Entities.ClarificationReqs ClarReqs = BL.ClarificationRequests.GetClarRequest_ForReply(ClarRequestID);
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
                       
                        //Entities.Request req = null;
                        if (Page.Session["EditRequestId"] != null)
                        {
                            BL.ClarificationRequests.AddClarRequestReply(Convert.ToInt32(hdn_ID.Value), txt_ClarReply.Text);
                            BL.Request.UpdateNeedsClarificationRequestStatus(int.Parse(Page.Session["EditRequestId"].ToString()));
                            Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.Requests, Utilities.Constants.RequestHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.UCProgramEmployeeClarificationReply, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["DisplayRequestId"].ToString(), "Yes");
                            //req.ApplicantID = BL.Request.GetRequestByNumber(int.Parse(Page.Session["EditRequestId"].ToString()), LCID).ApplicantID;

                            BL.Request.UpdateRejection(new Entities.Request()
                            {
                                ID = Convert.ToInt32(Page.Session["DisplayRequestId"]),
                                RejectedFrom = SPContext.Current.Web.CurrentUser.Name,
                                RejectionReason = "ReturnFromApplicant",
                                RejectionDate = DateTime.Now//,
                                                            //ApplicantID = req.ApplicantID
                            });
                            NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCProgramEmployeeClarificationReply, txt_ClarRequested.Text, "Requests", Page.Session["EditRequestId"].ToString());
                            SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/trackrequest.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);


                        }
                    }
                    else
                        return ;
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
                    BL.ClarificationRequests.AddRequestClar(txt_ClarRequested.Text, Page.Session["DisplayRequestId"].ToString());

                    NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCEProgramEmployeeNeedsClarification, txt_ClarRequested.Text, "Requests", Page.Session["DisplayRequestId"].ToString());
                    Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.Requests, Utilities.Constants.RequestHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.UCEProgramEmployeeNeedsClarification, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["DisplayRequestId"].ToString(), "Yes");

                    Entities.Request requestItem = BL.Request.GetRequestByNumber(Convert.ToInt32(Page.Session["DisplayRequestId"].ToString()), LCID);
                    try
                    {
                        Common.Entities.Applicants applicant = Common.BL.Applicants.GetApplicantByID(Convert.ToInt32(requestItem.ApplicantID.ID), LCID);
                        Common.Entities.Notifications smsNotifications = Common.BL.Notifications.GetSubmittedNotification((int)Common.Utilities.NotificationType.SMS, (int)Common.Utilities.RequestStatus.UCEReceptionistNeedsClarification);
                        if (smsNotifications != null)
                        {
                            //Send SMS here
                            Texting.SendSMS(applicant.MobileNumber, string.Format(smsNotifications.Body, requestItem.RequestNumber, "Equivalency", requestItem.RequestNumber, "معادلة الشهادات"));
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(ex);
                    }
                   
                    if (HelperMethods.InGroup(Common.Utilities.Constants.ReceptionistGroupName))
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/trackrequest.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

                    else if (HelperMethods.InGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName))// || HelperMethods.InGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName))
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ArabicAsianProgramEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);


                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_Clarification.btn_AddNewClarification_Click");
            }
        }

        protected void custReply_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Page.Session["EditRequestId"] != null)
            {
                int requestId = int.Parse(Convert.ToString(Page.Session["EditRequestId"]));
                Entities.Request request = BL.Request.GetRequestByNumber(requestId, LCID);
                if (int.Parse(request.RequestStatus.Code) == (int)Common.Utilities.RequestStatus.UCEDraftForClarification)
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
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

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ClarificationRequestsListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

        }
    }
}
