using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Entities;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Nintex.Workflow.HumanApproval;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using commonUtilities = ITWORX.MOEHEWF.Common.Utilities;
using requestBL = ITWORX.MOEHEWF.PA.BL;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class PASearchStatusRecommendationUC : Utilities.UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload StatusRecommendAttachements;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload ViewStatusRecommendAttachements;
        public bool ViewAttach = false;
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
                Bindrdbtn();
                GetSavedProcedures();
            }

            BindAttachements();
            if (!ViewAttach)
                BindViewAttachements();
        }

        private void GetSavedProcedures()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method PAPASearchStatusRecommendationUC.GetProcedures");
                if (Page.Session["PADisplayRequestId"] != null)
                {
                    PARequest paRequest = requestBL.Request.GetRequestByNumber(Convert.ToInt32(Page.Session["PADisplayRequestId"]), LCID);
                    hdn_RequestNumber.Value = paRequest.RequestNumber;
                    List<Entities.Procedures> RecommendProc = BL.AllProcedures.GetSavedPARecommendationstatusProc(Page.Session["PADisplayRequestId"].ToString(), SPContext.Current.Web.CurrentUser.Name.ToLower());
                    if (RecommendProc.Count > 0)
                    {
                        if (RecommendProc[0].PARecommendationstatus == "Save" || paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PAProgramEmployeeMissingInformation).ToString())
                        {
                            rdbtn_EmpRecommendation.Items.FindByText(RecommendProc[0].Procedure).Selected = true;
                            hdn_ID.Value = RecommendProc[0].ID;
                            txt_EmpOpinion.Text = RecommendProc[0].Opinion;
                            txt_DecisiontxtForPrinting.Text = RecommendProc[0].DecisionForPrint;

                            txtEarnedHours.Text = RecommendProc[0].EarnedHours;
                            txtOnlineHours.Text = RecommendProc[0].OnlineHours;
                            txtOnlineHoursPer.Text = RecommendProc[0].OnlineHoursPer;
                            viewControls.Visible = false;
                            //BindAttachements();
                        }
                        else
                        {
                            if (paRequest != null && !(paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PAEmployeeClarificationReplay).ToString() || paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PAProgramEmployeeMissingInformation).ToString() || paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PACulturalMissionStatementReply).ToString() || paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PAHigherEduInstitutesStatementReply).ToString()))
                            {
                                pnl_EmpRecommendation.Visible = false;
                                viewControls.Visible = true;
                                btn_ApproveProcedures.Visible = false;
                                lbl_NotificationMsg.Visible = false;
                                lbl_RecommendationVal.Text = RecommendProc[0].Procedure;
                                hdn_ID.Value = RecommendProc[0].ID;
                                lbl_EmpOpinionVal.Text = RecommendProc[0].Opinion;
                                lblSearchStatusVal.Text = RecommendProc[0].DecisionForPrint;
                                lblEarnedHoursV.Text = RecommendProc[0].EarnedHours;
                                lblOnlineHoursV.Text = RecommendProc[0].OnlineHours;
                                lblOnlineHoursPerV.Text = RecommendProc[0].OnlineHoursPer;
                                //BindViewAttachements();
                            }
                        }
                    }
                    else
                    {
                        if (paRequest != null && !(paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PAEmployeeClarificationReplay).ToString() || paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PAProgramEmployeeMissingInformation).ToString() || paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PASubmitted).ToString() || paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PACulturalMissionStatementReply).ToString() || paRequest.RequestStatus.Code == ((int)commonUtilities.RequestStatus.PAHigherEduInstitutesStatementReply).ToString()))
                        {
                            pnl_EmpRecommendation.Visible = false;
                            viewControls.Visible = true;
                            btn_ApproveProcedures.Visible = false;
                            lbl_NotificationMsg.Visible = false;
                        }
                        else if (!SPContext.Current.Web.CurrentUser.LoginName.ToLower().Equals(paRequest.AssignedTo.ToLower()))
                        {
                            pnl_EmpRecommendation.Visible = false;
                            viewControls.Visible = true;
                            btn_ApproveProcedures.Visible = false;
                            lbl_NotificationMsg.Visible = false;
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
                Logging.GetInstance().Debug("Exiting method PASearchStatusRecommendationUC.GetProcedures");
            }
        }

        protected void ReviewDecisionClick(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method PASearchStatusRecommendationUC.btn_ReviewDecisionClick");
                if (Page.Session["PADisplayRequestId"] != null)
                {
                    Common.BL.FinalDecisionPrint.ReviewPAPDF(hdn_RequestNumber.Value, "PDF", LCID);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PASearchStatusRecommendationUC.btn_ReviewDecisionClick");
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method PASearchStatusRecommendationUC.btn_Save_Click");

                //if (!IsRefresh)
                //{
                if (hdn_ID.Value != "")
                    BL.AllProcedures.UpdatePARecommendationstatusProc(hdn_ID.Value.ToString(), "Save", txt_EmpOpinion.Text, rdbtn_EmpRecommendation.SelectedItem.Text, txt_DecisiontxtForPrinting.Text, txtEarnedHours.Text, txtOnlineHours.Text, txtOnlineHoursPer.Text);
                else
                {
                    if (Page.Session["PADisplayRequestId"] != null)
                        BL.AllProcedures.AddPARecommendationstatus(txt_EmpOpinion.Text, Page.Session["PADisplayRequestId"].ToString(), rdbtn_EmpRecommendation.SelectedItem.Text, "Save", txt_DecisiontxtForPrinting.Text, txtEarnedHours.Text, txtOnlineHours.Text, txtOnlineHoursPer.Text);
                    GetSavedProcedures();
                }
                StatusRecommendAttachements.SaveAttachments();
                lbl_SaveSuccess.Visible = true;

                //}
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PASearchStatusRecommendationUC.btn_Save_Click");
            }
        }

        protected void btn_ApproveProcedures_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method PASearchStatusRecommendationUC.btn_ApproveProcedures_Click");
                //if (!IsRefresh)
                //{
                if (hdn_ID.Value != "")
                {
                    BL.AllProcedures.UpdatePARecommendationstatusProc(hdn_ID.Value.ToString(), "Approved", txt_EmpOpinion.Text, rdbtn_EmpRecommendation.SelectedItem.Text, txt_DecisiontxtForPrinting.Text, txtEarnedHours.Text, txtOnlineHours.Text, txtOnlineHoursPer.Text);
                }
                else
                {
                    if (Page.Session["PADisplayRequestId"] != null)
                    {
                        BL.AllProcedures.AddPARecommendationstatus(txt_EmpOpinion.Text, Page.Session["PADisplayRequestId"].ToString(), rdbtn_EmpRecommendation.SelectedItem.Text, "Approved", txt_DecisiontxtForPrinting.Text, txtEarnedHours.Text, txtOnlineHours.Text, txtOnlineHoursPer.Text);
                    }
                }
                BL.PARequestsAttachments.DeleteRequestsAttachmentsByGroupAndRequestID(Convert.ToInt32(Page.Session["PADisplayRequestId"]), StatusRecommendAttachements.Group);
                StatusRecommendAttachements.SaveAttachments();
                NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAProgramManagerReview, txt_EmpOpinion.Text, "PARequests", Page.Session["PADisplayRequestId"].ToString());
                Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests, Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PAEmployeeReviewInformation, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["PADisplayRequestId"].ToString(), "No");

                lbl_Success.Visible = true;
                List<Entities.Procedures> RecommendProc = BL.AllProcedures.GetSavedPARecommendationstatusProc(Page.Session["PADisplayRequestId"].ToString(), SPContext.Current.Web.CurrentUser.Name.ToLower());
                if (RecommendProc.Count > 0)
                {
                    if (RecommendProc[0].PARecommendationstatus == "Approved")
                    {
                        pnl_EmpRecommendation.Visible = false;
                        viewControls.Visible = true;
                        btn_ApproveProcedures.Visible = false;
                        lbl_NotificationMsg.Visible = false;
                        lbl_RecommendationVal.Text = RecommendProc[0].Procedure;
                        hdn_ID.Value = RecommendProc[0].ID;
                        lbl_EmpOpinionVal.Text = RecommendProc[0].Opinion;
                        lblSearchStatusVal.Text = RecommendProc[0].DecisionForPrint;
                        lblEarnedHoursV.Text = RecommendProc[0].EarnedHours;
                        lblOnlineHoursV.Text = RecommendProc[0].OnlineHours;
                        lblOnlineHoursPerV.Text = RecommendProc[0].OnlineHoursPer;
                        ViewAttach = true;
                        if (ViewAttach)
                            BindViewAttachements();
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method PASearchStatusRecommendationUC.btn_ApproveProcedures_Click");
            }
        }

        private void BindViewAttachements()
        {
            Logging.GetInstance().Debug("Entering method PASearchStatusRecommendationUC.BindViewAttachements");
            try
            {
                #region Prerequiestes

                /// add colum "RequestID" lookup from Requests
                /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
                /// add column Group, single line of text

                #endregion Prerequiestes

                #region Display Mode

                ViewStatusRecommendAttachements.DocumentLibraryName = Utilities.Constants.PAProgEmpStatusandRecomAttachements;
                ViewStatusRecommendAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
                ViewStatusRecommendAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NotesPrepared", (uint)LCID);
                ViewStatusRecommendAttachements.Group = "StatusRecommendAttachements";
                ViewStatusRecommendAttachements.RequestID = Convert.ToInt32(Page.Session["PADisplayRequestId"].ToString());
                ViewStatusRecommendAttachements.Enabled = false;
                ViewStatusRecommendAttachements.Bind();

                #endregion Display Mode
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method PASearchStatusRecommendationUC.BindViewAttachements");
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

            StatusRecommendAttachements.DocumentLibraryName = Utilities.Constants.PAProgEmpStatusandRecomAttachements;
            StatusRecommendAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
            StatusRecommendAttachements.MaxSize = 10240000;//10MB
            StatusRecommendAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NotesPrepared", (uint)LCID);
            StatusRecommendAttachements.Group = "StatusRecommendAttachements";// field name for example, shouldn't be used for more than one field per each control.
            StatusRecommendAttachements.RequestID = Convert.ToInt32(Page.Session["PADisplayRequestId"].ToString());
            StatusRecommendAttachements.SupportedExtensions = "PNG,PDF,JPG";
            StatusRecommendAttachements.IsRequired = false;
            StatusRecommendAttachements.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
            StatusRecommendAttachements.Enabled = true;
            StatusRecommendAttachements.MaxFileNumber = 1;

            StatusRecommendAttachements.Bind();

            #endregion Edit Mode
        }

        private void Bindrdbtn()
        {
            rdbtn_EmpRecommendation.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Approved", (uint)LCID), "0"));
            rdbtn_EmpRecommendation.Items.Insert(1, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Rejected", (uint)LCID), "1"));
            //rdbtn_EmpRecommendation.Items.Insert(2, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "TransferToLegalAffairs", (uint)LCID), "2"));
            //rdbtn_EmpRecommendation.Items.Insert(3, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "TransfertoCommittee", (uint)LCID), "3"));
        }
    }
}