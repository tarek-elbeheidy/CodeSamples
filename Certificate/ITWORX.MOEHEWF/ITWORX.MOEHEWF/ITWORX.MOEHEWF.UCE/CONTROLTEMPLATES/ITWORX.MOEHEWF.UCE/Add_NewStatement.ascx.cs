using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Nintex.Workflow.HumanApproval;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class Add_NewStatement : UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload StatementReplyAttachements;

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
            if (!Page.IsPostBack)
            {
                //To Reply on Clarification Request
                if (Page.Session["hdn_StatementReqId"] != null)
                {
                    GetStatementRequestForReply(Page.Session["hdn_StatementReqId"].ToString());
                    ReplyControlsdiv.Visible = true;
                    ReplyControlstxt.Visible = true;
                    btn_SendStatementReq.Visible = false;
                }
                else if (Page.Session["DisplayRequestId"] != null)
                {
                    BindLabelsData();
                    BindDropDown();
                    btn_SendStatementReq.Visible = true;
                    ReplyControlsdiv.Visible = false;
                    ReplyControlstxt.Visible = false;
                }
            }
            BindAttachements();
        }

        private void GetStatementRequestForReply(string StatementReqID)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_NewStatement.GetStatementRequestForReply");
                Entities.RequestsForStatement StatementRequest = BL.RequestsForStatements.GetReqStatementbyID(StatementReqID);
                if (StatementRequest != null)
                {
                    lbl_StatementCreatedbyVal.Text = StatementRequest.StatementCreatedby;
                    lbl_StatementDateVal.Text = StatementRequest.StatementDate.ToString();
                    hdn_ID.Value = StatementRequest.ID;
                    txt_StatementRequested.Text = StatementRequest.StatementRequested;
                    txt_StatementRequested.Enabled = false;
                    txt_StatementSubject.Text = StatementRequest.StatementSubject;
                    txt_StatementSubject.Enabled = false;
                    txt_DirectedTo.Text = StatementRequest.DirectedTo;
                    txt_DirectedTo.Enabled = false;
                    directTo.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_NewStatement.GetStatementRequestForReply");
            }
        }

        private void BindLabelsData()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_NewStatement.BindLabelsData");
                // lbl_ReqIDVal.Text = Page.Session["DisplayRequestId"].ToString();
                lbl_StatementDateVal.Text = DateTime.Now.ToShortDateString();
                lbl_StatementCreatedbyVal.Text = SPContext.Current.Web.CurrentUser.Name;
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_NewStatement.BindLabelsData");
            }
        }

        private void BindDropDown()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_NewStatement.BindDropDown");
                //drp_DirectedTo.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                //drp_DirectedTo.Items.Insert(1, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "HigherEduInstitutes", (uint)LCID), "1"));
                //drp_DirectedTo.Items.Insert(2, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "BritainCulturalAttaché", (uint)LCID), "2"));

                //drp_DirectedTo.Items.Insert(3, new ListItem("الملحقية الثقافية استراليا", "3"));
                //drp_DirectedTo.Items.Insert(4, new ListItem("الملحقية الثقافية فرنسا", "4"));

                //drp_DirectedTo.Items.Insert(5, new ListItem("الملحقية الثقافية أمريكا", "5"));
                //drp_DirectedTo.Items.Insert(6, new ListItem("الملحقية الثقافية كندا", "6"));
                //drp_DirectedTo.Items.Insert(7, new ListItem("الملحقية الثقافية الأردنية", "7"));
                List<Common.Entities.StatementOrganizations> statementOrganizationsItems =Common.BL.StatementOrganizations.GetAll();
                drp_DirectedTo.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                if (statementOrganizationsItems != null && statementOrganizationsItems.Count > 0)
                {
                    drp_DirectedTo.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref drp_DirectedTo, statementOrganizationsItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_NewStatement.btn_ApproveProcedures_Click");
            }
        }

        protected void btn_SendStatementReq_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_NewStatement.btn_SendStatementReq_Click");
                if (!IsRefresh)
                {
                    if (Page.IsValid)
                    {
                        BL.RequestsForStatements.AddRequestsForStatement(drp_DirectedTo.SelectedItem.Text, txt_StatementRequested.Text, txt_StatementSubject.Text, Page.Session["DisplayRequestId"].ToString());// lbl_ReqIDVal.Text);

                        string SMTPServer = MOEHE.Utilities.HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPServer");
                        string SMTPServerPort = MOEHE.Utilities.HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPServerPort");
                        string SMTPFromAddress = MOEHE.Utilities.HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPFromAddress");
                        string SMTPFromDisplayName = MOEHE.Utilities.HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPFromDisplayName");
                        SPListItem  StatementOrganizationItem = Common.BL.StatementOrganizations.GetStatementOrganizationByID(int.Parse(drp_DirectedTo.SelectedItem.Value));
                        var users = MOEHE.Utilities.HelperMethods.GetGroupUsers(StatementOrganizationItem["SPGroupName"].ToString());
                        Entities.Request requestItem = BL.Request.Reviewer_GetRequestByNumber(Convert.ToInt32(Page.Session["DisplayRequestId"].ToString()), LCID);

                        if (StatementOrganizationItem["SPGroupName"].ToString().Trim() == Common.Utilities.Constants.HigherEducationalInstitutionsGroupName.Trim())
                        {


                            Utilities.BusinessHelper.UpdateRequestStatus((int)Common.Utilities.RequestStatus.UCEHigherEduInstitutesNeedsStatement, Page.Session["DisplayRequestId"].ToString(), StatementOrganizationItem["SPGroupName"].ToString());
                            Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.Requests, Utilities.Constants.RequestHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.UCEHigherEduInstitutesNeedsStatement, SPContext.Current.Web.CurrentUser.Name, LCID == (int)Language.English ? StatementOrganizationItem["Title"].ToString() : StatementOrganizationItem["TitleAr"].ToString(), Page.Session["DisplayRequestId"].ToString(), "No");
                            try
                            {
                                // var mails = Common.BL.Notifications.SendMailToGroup(Common.Utilities.Constants.HigherEducationalInstitutionsGroupName);
                                Common.Entities.Notifications emaiNotifications = Common.BL.Notifications.GetSubmittedNotification((int)Common.Utilities.NotificationType.Email, (int)Common.Utilities.RequestStatus.UCEHigherEduInstitutesNeedsStatement);
                                if (emaiNotifications != null)
                                {
                                    if (users.Count > 0)
                                    {
                                        foreach (var item in users)
                                        {
                                            if (!string.IsNullOrEmpty(item.Email))
                                                HelperMethods.SendNotificationEmail(string.Format(emaiNotifications.Body, requestItem.RequestNumber, "<br/>", requestItem.RequestNumber), emaiNotifications.Subject, SMTPFromAddress, SMTPFromDisplayName, item.Email, SMTPServer, SMTPServerPort, "", "", true, new List<System.Net.Mail.Attachment>());
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Logging.GetInstance().LogException(ex);
                            }

                        }
                        else
                        {
                            Utilities.BusinessHelper.UpdateRequestStatus((int)Common.Utilities.RequestStatus.UCECulturalMissionNeedsStatement, Page.Session["DisplayRequestId"].ToString(), StatementOrganizationItem["SPGroupName"].ToString());
                            Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.Requests, Utilities.Constants.RequestHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.UCECulturalMissionNeedsStatement, SPContext.Current.Web.CurrentUser.Name, LCID == (int)Language.English ? StatementOrganizationItem["Title"].ToString() : StatementOrganizationItem["TitleAr"].ToString(), Page.Session["DisplayRequestId"].ToString(), "No");
                            try
                            {
                                Common.Entities.Notifications emaiNotifications = Common.BL.Notifications.GetSubmittedNotification((int)Common.Utilities.NotificationType.Email, (int)Common.Utilities.RequestStatus.UCECulturalMissionNeedsStatement);
                                if (emaiNotifications != null)
                                {
                                    if (users.Count > 0)
                                    {
                                        foreach (var item in users)
                                        {
                                            if (!string.IsNullOrEmpty(item.Email))
                                                HelperMethods.SendNotificationEmail(string.Format(emaiNotifications.Body, requestItem.RequestNumber, "<br/>", requestItem.RequestNumber), emaiNotifications.Subject, SMTPFromAddress, SMTPFromDisplayName, item.Email, SMTPServer, SMTPServerPort, "", "", true, new List<System.Net.Mail.Attachment>());
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Logging.GetInstance().LogException(ex);
                            }

                        }
                    }

                    //NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCEC, txt_StatementRequested.Text);
                    //Response.Redirect("/en/Pages/StatementRequestsListing.aspx");
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ArabicAsianProgramEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
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
                Logging.GetInstance().Debug("Exiting method Add_NewStatement.btn_SendStatementReq_Click");
            }
        }

        protected void btn_Reply_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_NewStatement.btn_Reply_Click");
                if (!IsRefresh)
                {
                    string emailBody = string.Empty;
                    BL.RequestsForStatements.UpdateRequestsForStatementbyReply(hdn_ID.Value, txt_StatDetailsReply.Text);
                    SPListItem item = BL.Request.Reviewer_GetRequestItemByID(int.Parse(Page.Session["DisplayRequestId"].ToString()));
                    SPFieldUser userField = (SPFieldUser)item.Fields.GetField("PrimaryProgramEmployee");
                    SPFieldUserValue userFieldValue = (SPFieldUserValue)userField.GetFieldValue(item["PrimaryProgramEmployee"].ToString());

                    string ProgEmpUser = userFieldValue.User.Email;

                    var assignedTo = userFieldValue.User.LoginName;
                  
                    if (HelperMethods.InGroup(Common.Utilities.Constants.HigherEducationalInstitutionsGroupName))
                    {
                        Utilities.BusinessHelper.UpdateRequestStatus((int)Common.Utilities.RequestStatus.UCEHigherEduInstitutesStatementReply, Page.Session["DisplayRequestId"].ToString(), assignedTo);
                        Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.Requests, Utilities.Constants.RequestHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.UCEHigherEduInstitutesStatementReply, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["DisplayRequestId"].ToString(), "No");

                        emailBody = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "StatementReplyFrom", (uint)LCID), Convert.ToString(item["RequestNumber"]), Convert.ToString(item["RequestNumber"]));

                        //if (LCID == (int)Language.English)
                        //    emailBody = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "StatementReplyFrom", (uint)LCID) + Page.Session["DisplayRequestId"].ToString();
                        //else
                        //    emailBody = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "StatementReplyFrom", (uint)LCID) + Page.Session["DisplayRequestId"].ToString();
                    }
                    else
                    {
                        
                        Utilities.BusinessHelper.UpdateRequestStatus((int)Common.Utilities.RequestStatus.UCECulturalMissionStatementReply, Page.Session["DisplayRequestId"].ToString(), assignedTo);
                        Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.Requests, Utilities.Constants.RequestHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.UCECulturalMissionStatementReply, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["DisplayRequestId"].ToString(), "No");

                        emailBody = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "StatementReplyCultMission", (uint)LCID),  Convert.ToString(item["RequestNumber"]), Convert.ToString(item["RequestNumber"]));

                        //if (LCID == (int)Language.English)
                        //    emailBody = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "StatementReplyCultMission", (uint)LCID) + Page.Session["DisplayRequestId"].ToString();
                        //else
                        //    emailBody = "بيان الرد من الملحقية الثقافية على الطلب رقم:" + Page.Session["DisplayRequestId"].ToString();
                    }
                    StatementReplyAttachements.SaveAttachments();

                    BL.Request.UpdateRejection(new Entities.Request()
                    {
                        ID = Convert.ToInt32(Page.Session["DisplayRequestId"]),
                        RejectedFrom = SPContext.Current.Web.CurrentUser.Name,
                        RejectionReason = "ReturnFromCM_HI",
                        RejectionDate = DateTime.Now
                    });


                 

                    string SMTPServer = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPServer");
                    string SMTPServerPort = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPServerPort");
                    string SMTPFromAddress = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPFromAddress");
                    string SMTPFromDisplayName = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPFromDisplayName");
                    string SMTPUserName = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPUserName");
                    string SMTPPassword = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPPassword");
                    //string BookID = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "BookID", (uint)LCID) + ":" + txt_BookID.Text;
                    string EmailSubject = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "StatementReply", (uint)LCID);

                    HelperMethods.SendNotificationEmail(emailBody, EmailSubject, SMTPFromAddress, SMTPFromDisplayName,ProgEmpUser,
                        SMTPServer, SMTPServerPort, SMTPUserName, SMTPPassword, true, new List<System.Net.Mail.Attachment>());

                    //NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCECulturalMissionStatementReply, txt_StatDetailsReply.Text);
                    Page.Session["hdn_StatementReqId"] = null;
                    //Response.Redirect("/en/Pages/StatementRequestsListing.aspx");
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/StatementRequestsListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_NewStatement.btn_Reply_Click");
            }
        }

        private void BindAttachements()
        {
            #region Prerequiestes

            /// add colum "RequestID" lookup from Requests
            /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
            /// add column Group, single line of text

            #endregion Prerequiestes

            #region Edit Mode

            StatementReplyAttachements.DocumentLibraryName = Utilities.Constants.StatementReqReplyAttachements;
            StatementReplyAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
            StatementReplyAttachements.MaxSize = 7168000;//2MB
            StatementReplyAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NotesPrepared", (uint)LCID);
            StatementReplyAttachements.Group = "StatementReplyAttachements";// field name for example, shouldn't be used for more than one field per each control.
            StatementReplyAttachements.RequestID = Convert.ToInt32(Page.Session["DisplayRequestId"].ToString());
            StatementReplyAttachements.SupportedExtensions = "PNG,PDF,JPG";
            StatementReplyAttachements.IsRequired = false;
            StatementReplyAttachements.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
            StatementReplyAttachements.Enabled = true;

            StatementReplyAttachements.Bind();

            #endregion Edit Mode
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/StatementRequestsListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
        }
    }
}