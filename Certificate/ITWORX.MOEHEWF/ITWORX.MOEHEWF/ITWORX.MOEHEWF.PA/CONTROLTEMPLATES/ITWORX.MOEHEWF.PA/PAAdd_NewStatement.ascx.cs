using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Nintex.Workflow.HumanApproval;
using ITWORX.MOEHEWF.UCE.Utilities;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using requestBL = ITWORX.MOEHEWF.PA.BL;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class Add_NewStatement : Utilities.UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload StatementReplyAttachements;

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
                    SPListItem requestItem = web.Lists["PARequests"].GetItemById(int.Parse(Page.Session["PADisplayRequestId"].ToString()));
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
                else if (Page.Session["PADisplayRequestId"] != null)
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
                    lbl_ReqIDVal.Text = Page.Session["PADisplayRequestId"].ToString();
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
                lbl_ReqIDVal.Text = Page.Session["PADisplayRequestId"].ToString();
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
                List<Common.Entities.StatementOrganizations> statementOrganizationsItems = Common.BL.StatementOrganizations.GetAll();
                drp_DirectedTo.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));
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
                    SPListItem StatementOrganizationItem = Common.BL.StatementOrganizations.GetStatementOrganizationByID(int.Parse(drp_DirectedTo.SelectedItem.Value));
                    BL.RequestsForStatements.AddRequestsForStatement(drp_DirectedTo.SelectedItem.Text, txt_StatementRequested.Text, txt_StatementSubject.Text, lbl_ReqIDVal.Text);
                    if (StatementOrganizationItem["SPGroupName"].ToString().Trim() == Common.Utilities.Constants.HigherEducationalInstitutionsGroupName.Trim())
                    {
                        Utilities.BusinessHelper.UpdateRequestStatus((int)Common.Utilities.RequestStatus.PAHigherEduInstitutesNeedsStatement, Page.Session["PADisplayRequestId"].ToString(), StatementOrganizationItem["SPGroupName"].ToString());
                        Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests, Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PAHigherEduInstitutesNeedsStatement, SPContext.Current.Web.CurrentUser.Name, LCID == (int)Language.English ? StatementOrganizationItem["Title"].ToString() : StatementOrganizationItem["TitleAr"].ToString(), Page.Session["PADisplayRequestId"].ToString(), "No");

                    }
                    else
                    {
                        Utilities.BusinessHelper.UpdateRequestStatus((int)Common.Utilities.RequestStatus.PACulturalMissionNeedsStatement, Page.Session["PADisplayRequestId"].ToString(), StatementOrganizationItem["SPGroupName"].ToString());
                        Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests,Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PACulturalMissionNeedsStatement, SPContext.Current.Web.CurrentUser.Name, LCID == (int)Language.English ? StatementOrganizationItem["Title"].ToString() : StatementOrganizationItem["TitleAr"].ToString(), Page.Session["PADisplayRequestId"].ToString(), "No");

                    }

                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/paArabicAsianProgramEmployeeDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
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
                    SPListItem item = BL.Request.Reviewer_GetRequestItemByID(int.Parse(Page.Session["PADisplayRequestId"].ToString()));
                    SPFieldUser userField = (SPFieldUser)item.Fields.GetField("ReceptionEmployee");
                    SPFieldUserValue userFieldValue = (SPFieldUserValue)userField.GetFieldValue(item["ReceptionEmployee"].ToString());

                   

                    var assignedTo = userFieldValue.User.LoginName;
                    string ProgEmpUser = userFieldValue.User.Email;

                    BL.RequestsForStatements.UpdateRequestsForStatementbyReply(hdn_ID.Value, txt_StatDetailsReply.Text);  
                    

                    if (HelperMethods.InGroup(Common.Utilities.Constants.HigherEducationalInstitutionsGroupName))
                    {
                        Utilities.BusinessHelper.UpdateRequestStatus((int)Common.Utilities.RequestStatus.PAHigherEduInstitutesStatementReply, Page.Session["PADisplayRequestId"].ToString(), assignedTo);
                        Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests, Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PACulturalMissionStatementReply, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["PADisplayRequestId"].ToString(), "No");


                        emailBody = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "StatementReplyFrom", (uint)LCID), Convert.ToString(item["RequestNumber"]), Convert.ToString(item["RequestNumber"]));


                    }
                    else
                    {
                        Utilities.BusinessHelper.UpdateRequestStatus((int)Common.Utilities.RequestStatus.PACulturalMissionStatementReply, Page.Session["PADisplayRequestId"].ToString(), assignedTo);
                        Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests, Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PACulturalMissionStatementReply, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["PADisplayRequestId"].ToString(), "No");


                        emailBody = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "StatementReplyCultMission", (uint)LCID), Convert.ToString(item["RequestNumber"]), Convert.ToString(item["RequestNumber"]));

                    }
                    requestBL.Request.UpdateRejection(new Entities.PARequest()
                    {
                        ID = Convert.ToInt32(Page.Session["PADisplayRequestId"]),
                        RejectedFrom = SPContext.Current.Web.CurrentUser.Name,
                        RejectionReason = txt_StatDetailsReply.Text,
                        RejectionDate = DateTime.Now
                    });
                    StatementReplyAttachements.SaveAttachments();

                    string SMTPServer = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPServer");
                    string SMTPServerPort = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPServerPort");
                    string SMTPFromAddress = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPFromAddress");
                    string SMTPFromDisplayName = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPFromDisplayName");
                    string SMTPUserName = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPUserName");
                    string SMTPPassword = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SMTPPassword");
                    //string BookID = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "BookID", (uint)LCID) + ":" + txt_BookID.Text;
                    string EmailSubject = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "StatementReply", (uint)LCID);

                    HelperMethods.SendNotificationEmail(emailBody, EmailSubject, SMTPFromAddress, SMTPFromDisplayName,ProgEmpUser,
                        SMTPServer, SMTPServerPort, SMTPUserName, SMTPPassword, true, new List<System.Net.Mail.Attachment>());


                    Page.Session["hdn_StatementReqId"] = null; 
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAStatementRequestsListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
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

            StatementReplyAttachements.DocumentLibraryName = Utilities.Constants.PAStatementReqReplyAttachements;
            StatementReplyAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
            StatementReplyAttachements.MaxSize = 10240000;//10MB
            StatementReplyAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NotesPrepared", (uint)LCID);
            StatementReplyAttachements.Group = "StatementReplyAttachements";// field name for example, shouldn't be used for more than one field per each control.
            StatementReplyAttachements.RequestID = Convert.ToInt32(Page.Session["PADisplayRequestId"].ToString());
            StatementReplyAttachements.SupportedExtensions = "PNG,PDF,JPG";
            StatementReplyAttachements.IsRequired = false;
            StatementReplyAttachements.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
            StatementReplyAttachements.Enabled = true;

            StatementReplyAttachements.Bind();

            #endregion Edit Mode
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAStatementRequestsListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
        }
    }
}