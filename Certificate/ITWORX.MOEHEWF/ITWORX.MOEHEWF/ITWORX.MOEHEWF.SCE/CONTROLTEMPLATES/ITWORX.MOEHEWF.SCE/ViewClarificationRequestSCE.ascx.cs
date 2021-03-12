using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;
using Microsoft.SharePoint.Utilities;
using System.Web;
using ITWORX.MOEHE.Utilities;
using ITWorx.MOEHEWF.Nintex.Actions;
using ITWORX.MOEHEWF.Common.Utilities;

namespace ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE
{
    public partial class ViewClarificationRequestSCE : UserControlBase
    {
        private int ClarificationId { get { return Convert.ToInt32(Request.QueryString["ClarId"]); } }
        private int RequestId { get { return Convert.ToInt32(Request.QueryString["RequestId"]); } }

        //1 return from back button
        //2 return from finish button
        //0 first visit
        private int IsEditable { get { return Convert.ToInt32(Request.QueryString["Edit"]); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter ViewClarificationRequestSCE.Page_Load");
                if (!Page.IsPostBack)
                {
                    //  int clarId = Convert.ToInt32(Request.QueryString["ClarId"]);

                    BindData(ClarificationId);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ViewClarificationRequestSCE.Page_Load");
            }
        }

        private void BindData(int clarId)
        {
            try
            {

                Logging.GetInstance().Debug("Enter ViewClarificationRequestSCE.BindData");
                //int currentUserId = SPContext.Current.Web.CurrentUser.ID;
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    SCEClarificationsRequestsListFieldsContentType ClarRequest = ctx.SCEClarificationsRequestsList.ScopeToFolder("", true).Where(c => c.Id.Value == clarId).FirstOrDefault();
                    SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(c => c.Id.Value == RequestId).FirstOrDefault();
                    DateTime dateRequest;
                    bool isrequestDateSuccess = DateTime.TryParse(ClarRequest.ClarificationDate.Value.ToString(), out dateRequest);
                    txt_RequestDate.Text = isrequestDateSuccess ? ExtensionMethods.QatarFormatedDate(dateRequest) + ExtensionMethods.QatarFormatedDateReturnTime(dateRequest) : string.Empty;

                    //   txt_RequestDate.Text = ClarRequest.ClarificationDate.ToString();
                    txt_Requester.Text = ClarRequest.Sender;
                    if (ClarRequest.ClarificationReason==null && !string.IsNullOrEmpty(ClarRequest.OtherClarificationReason))
                    {
                        txt_Reason.Text = ClarRequest.OtherClarificationReason;
                    }
                    else
                    {
                        txt_Reason.Text = LCID == (int)Language.English ? ((ClarificationReasonsLookupsCT)ClarRequest.ClarificationReason).Title : ((ClarificationReasonsLookupsCT)ClarRequest.ClarificationReason).TitleAr;
                    }
                   
                    txt_ReqClarification.Text = ClarRequest.RequiredClarification;
                    hdn_ReqId.Value = ClarRequest.RequestIDId.ToString();
                    if (!string.IsNullOrEmpty(ClarRequest.ClarificationReply))
                    {
                        txt_ApplicantReply.Text = ClarRequest.ClarificationReply;
                        txt_ReplyDate.Text = ClarRequest.ReplyDate.ToString();
                        bool isExist = (HelperMethods.InGroup(Common.Utilities.Constants.ApplicanstGroupName));
                        if (isExist)
                        {
                            dv_Reply.Visible = true;
                            lbl_ReplyDate.Visible = true;
                            txt_ReplyDate.Visible = true;
                            //lbl_ApplicantReply.Visible = false;
                            reqReply.Visible = false;
                            lbl_ReplyToClar.Visible = false;
                            Label2.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ReplyToClarRequest", (uint)LCID);
                            lbl_ApplicantReply.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ReplyToClarRequest", (uint)LCID);
                        }
                        else
                        {
                            lbl_ReplyDate.Visible = false;
                            txt_ReplyDate.Visible = false;
                            lbl_ReplyToClar.Visible = false;
                            Label2.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ViewClarRequest", (uint)LCID);
                            reqReply.Visible = false;
                            lbl_ApplicantReply.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ApplicantReply", (uint)LCID);
                        }
                        dv_firstMsg.Visible = false;
                        dv_secondMsg.Visible = false;
                    }
                    else
                    {
                        bool isExist = (HelperMethods.InGroup(Common.Utilities.Constants.ApplicanstGroupName));
                        if (isExist)
                        {
                            if (IsEditable == 0 || IsEditable == 1)
                            {
                                lbl_ApplicantReply.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ReplyToClarRequest", (uint)LCID);
                                lbl_ReplyToClar.Visible = false;
                                dv_firstMsg.Visible = true;
                                dv_secondMsg.Visible = false;
                                btn_ReturnToReq.Visible = true;
                                btn_Send.Visible = false;
                                dv_replyToClar.Visible = false;
                                dv_Reply.Visible = false;
                            }
                            else
                            {
                                txt_ApplicantReply.ReadOnly = false;
                                btn_Send.Visible = true;
                                rfv_ApplicantReply.Visible = true;
                                dv_replyToClar.Visible = true;
                                dv_firstMsg.Visible = false;
                                dv_secondMsg.Visible = true;
                                btn_ReturnToReq.Visible = false;
                                btn_Send.Visible = true;
                                dv_Reply.Visible = false;
                                lbl_ReplyToClar.Visible = false;
                                lbl_ApplicantReply.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ReplyToClarRequest", (uint)LCID);

                            }
                        }
                        else
                        {
                            if (request.EmployeeAssignedTo.ToLower() == SPContext.Current.Web.CurrentUser.LoginName.ToLower())
                            {
                                dv_firstMsg.Visible = true;
                                btn_ReturnToReq.Visible = true;
                            }
                            else
                            {
                                dv_firstMsg.Visible = false;
                                btn_ReturnToReq.Visible = false;
                            }
                            if (IsEditable == 0 || IsEditable == 1)
                            {
                                
                                dv_secondMsg.Visible = false;
                                btn_Send.Visible = false;
                                dv_replyToClar.Visible = false;
                                dv_Reply.Visible = false;
                            }
                            else
                            {
                                txt_ApplicantReply.ReadOnly = false;
                                btn_Send.Visible = true;
                                rfv_ApplicantReply.Visible = true;
                                dv_replyToClar.Visible = true;
                                dv_firstMsg.Visible = false;
                                dv_secondMsg.Visible = true;
                                btn_ReturnToReq.Visible = false;
                                btn_Send.Visible = true;
                                dv_Reply.Visible = false;
                                lbl_ReplyToClar.Visible = false;
                                lbl_ApplicantReply.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ReplyToClarRequest", (uint)LCID);

                            }
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
                Logging.GetInstance().Debug("Exit ViewClarificationRequestSCE.BindData");
            }
        }

        protected void btn_Back_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter ViewClarificationRequestSCE.btn_Back_Click");
                if (IsEditable == 0 || IsEditable == 1)
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEClarificationsListing.aspx?RequestId=" + RequestId, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                else
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/EditSCERequest.aspx?RequestId=" + RequestId, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ViewClarificationRequestSCE.btn_Back_Click");
            }
        }

        protected void btn_Send_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter ViewClarificationRequestSCE.btn_Send_Click");
                // int clarId = Convert.ToInt32(Request.QueryString["ClarId"]);
                SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url);
                SCEClarificationsRequestsListFieldsContentType ClarRequest = ctx.SCEClarificationsRequestsList.ScopeToFolder("", true).Where(c => c.Id.Value == ClarificationId).FirstOrDefault();
                ClarRequest.ClarificationReply = txt_ApplicantReply.Text;
                ClarRequest.ReplyDate = Convert.ToDateTime(DateTime.Now.ToString());

                SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(c => c.Id.Value == ClarRequest.RequestIDId).FirstOrDefault();
                request.ReturnDate = Convert.ToDateTime(DateTime.Now.ToString());
                request.ReturnedBy = SPContext.Current.Web.CurrentUser.Name;
                request.ReturnReason = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ReturnFromApplicant", (uint)LCID);
                request.RequestStatus = ctx.RequestStatus.Where(x => x.Id == Convert.ToInt32(RequestStatus.SCEEmployeeClarificationReply)).FirstOrDefault();
                
                 ctx.SubmitChanges();

                NintexHelper.ContinueTask(ITWorx.MOEHEWF.Nintex.Utilities.Constants.SCEEmployeeClarificationReply, txt_ApplicantReply.Text, Utilities.Constants.SCERequests, ClarRequest.RequestIDId.ToString());
                Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.SCERequests, Utilities.Constants.SCERequestHistory, LCID, (int)Common.Utilities.RequestStatus.SCEEmployeeClarificationReply, SPContext.Current.Web.CurrentUser.Name, string.Empty, hdn_ReqId.Value, "Yes");

                if (HelperMethods.InGroup(Utilities.Constants.Applicants))
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/TrackRequest.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                else if (HelperMethods.InGroup(Utilities.Constants.SCEEquivalenceEmployees))
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEEquivalenceEmployeesDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);


            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ViewClarificationRequestSCE.btn_Send_Click");
            }
        }
   
        protected void btn_ReturnToReq_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter ViewClarificationRequestSCE.btn_ReturnToReq_Click");
                Session["ClarificationId"] = ClarificationId;
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/EditSCERequest.aspx?RequestId=" + hdn_ReqId.Value, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ViewClarificationRequestSCE.btn_ReturnToReq_Click");
            }
        }
    }
}
