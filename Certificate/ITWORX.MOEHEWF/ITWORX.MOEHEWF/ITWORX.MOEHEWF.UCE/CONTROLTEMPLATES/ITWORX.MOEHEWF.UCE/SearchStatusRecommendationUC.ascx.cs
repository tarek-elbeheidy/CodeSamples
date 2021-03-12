using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using ITWORX.MOEHEWF.UCE.Entities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nintex.Workflow.HumanApproval;
using common = ITWORX.MOEHEWF.Common;
using Microsoft.SharePoint.Utilities;
using System.Threading;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class SearchStatusRecommendationUC : UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload StatusRecommendAttachements;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload ViewStatusRecommendAttachements;

        #region Protected Variables
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadOrgAttach;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload OrgAttach;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCalculatedDetails;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCertificates;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload Certificates;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadDelegates;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload Delegates;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadNotWorking;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadGeneralSec;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload GeneralSecondary;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadSequenceCert;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload CertificateSequence;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadDiploma;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileDiploma;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadInterDiploma;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileInterDiploma;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadCertificateEquivalent;
        //protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadGrades;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileEquivalent;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileGrades;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadHavePA;
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload fileUploadNoHavePA;


        /// <summary>
        /// CommityDecision control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.CommityDecision CommityDecisionCtr;


        #endregion
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
                _bindDecisionTemplate();
                Bindrdbtn();
                BindDropDowns();
                CommityDecisionCtr.LoadData(int.Parse(Page.Session["DisplayRequestId"].ToString()), SPContext.Current.Site.Url);
                GetSavedProcedures();

            }
            BindAttachements();
            BindViewAttachements();

            DisplayRequestData();
            BindAttachments();
        }

        private void BindDropDowns()
        {
            //bind sir drp
            drp_Sir.Items.Add(new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Sir_M", (uint)LCID), "1"));
            drp_Sir.Items.Add(new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Sir_F", (uint)LCID), "2"));


            //bind drp_recpected drp
            drp_recpected.Items.Add(new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Recpected_M", (uint)LCID), "1"));
            drp_recpected.Items.Add(new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Recpected_F", (uint)LCID), "2"));

        }

        private void _bindDecisionTemplate()
        {
            try
            {
                if (Page.Session["DisplayRequestId"] != null)
                {
                    SPListItem request = common.BL.Request.GetRequestItemByID(int.Parse(Page.Session["DisplayRequestId"].ToString()));
                    {
                        string EntityNeedsEquivalency = string.Empty;
                        string ApplicantName = string.Empty;
                        string PersonalID = string.Empty;


                        if (request["EntityNeedsEquivalencyAr"] != null)
                        {
                            SPFieldLookupValue EntityNeedsEquivalencylkp = new SPFieldLookupValue(request["EntityNeedsEquivalencyAr"].ToString());
                            EntityNeedsEquivalency = EntityNeedsEquivalencylkp.LookupValue;
                        }
                        else if (request["OtherEntityNeedsEquivalency"] != null)
                        {
                            EntityNeedsEquivalency = request["OtherEntityNeedsEquivalency"].ToString();
                        }
                        else if (request["EntityWorkingForAr"] != null)
                        {
                            SPFieldLookupValue EntityWorkingForlkp = new SPFieldLookupValue(request["EntityWorkingForAr"].ToString());
                            EntityNeedsEquivalency = EntityWorkingForlkp.LookupValue;
                        }
                        else if (request["OtherEntityWorkingFor"] != null)
                        {
                            EntityNeedsEquivalency = request["OtherEntityWorkingFor"].ToString();
                        }

                        if (request["Applicants_PersonalID"] != null)
                        {
                            SPFieldLookupValue Applicants_PersonalID = new SPFieldLookupValue(request["Applicants_PersonalID"].ToString());
                            PersonalID = Applicants_PersonalID.LookupValue;
                        }
                        if (request["Applicants_ApplicantAraicName"] != null)
                        {
                            SPFieldLookupValue Applicants_ApplicantName = new SPFieldLookupValue(request["Applicants_ApplicantAraicName"].ToString());
                            ApplicantName = Applicants_ApplicantName.LookupValue;
                        }

                        lbl_headManagerName.Text = lbl_headManagerNameView.Text = HelperMethods.GetConfigurationValue(SPContext.Current.Site.RootWeb.Url, Utilities.Constants.ConfigurationList, "HeadManagerName");
                        string applicantGender = common.BL.Applicants.IsFemale(PersonalID) ? HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "studentF", (uint)LCID) : HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "studentM", (uint)LCID);


                        lbl_EntityNeedsEquivalency.Text = lbl_EntityNeedsEquivalencyView.Text = EntityNeedsEquivalency;
                        lbl_RemainingDecicionBoby.Text = string.Format(lbl_RemainingDecicionBoby.Text, applicantGender, ApplicantName, PersonalID);
                        lbl_decicionBobyView.Text = string.Format(lbl_decicionBobyView.Text, "{0}", "{1}", applicantGender, ApplicantName, PersonalID);

                        lblExceptionFrom.Visible = false;
                        txtExceptionFrom.Visible = false;
                        txtExceptionFrom.Enabled = false;
                        RequiredFieldExceptionFrom.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
        }

        private void GetSavedProcedures()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method SearchStatusRecommendationUC.GetProcedures");
                if (Page.Session["DisplayRequestId"] != null)
                {
                    using (SPWeb web = new SPSite(SPContext.Current.Site.Url).OpenWeb())
                    {
                        SimilarRequest Request = new SimilarRequest();
                        SPList reqs = web.Lists[Utilities.Constants.Requests];
                        SPListItem item = reqs.GetItemById(Convert.ToInt32(Page.Session["DisplayRequestId"].ToString()));
                        SPFieldLookupValue StatusId = new SPFieldLookupValue((item["RequestStatusId"] != null) ? item["RequestStatusId"].ToString() : string.Empty);
                        Request.RequestStatusId = StatusId.LookupId;
                        hdn_RequestNumber.Value = item["RequestNumber"].ToString();
                        Request.AssignedTo = Convert.ToString(item["EmployeeAssignedTo"]);
                        List<Entities.Procedures> RecommendProc = BL.AllProcedures.GetSavedRecommendationStatusProc(Page.Session["DisplayRequestId"].ToString(), SPContext.Current.Web.CurrentUser.Name.ToLower());
                        if (RecommendProc.Count > 0)
                        {
                            if (RecommendProc[0].RecommendationStatus == "Save" || Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEProgramEmployeeMissingInformation)
                            {
                                rdbtn_EmpRecommendation.Items.FindByText(RecommendProc[0].Procedure).Selected = true;
                                hdn_ID.Value = RecommendProc[0].ID;
                                txt_EmpOpinion.Text = RecommendProc[0].Opinion;
                                txt_DecisiontxtForPrinting.Text = SPHttpUtility.HtmlDecode(RecommendProc[0].DecisionForPrint);
                                txtOccupationName.Text = RecommendProc[0].OccupationName;
                                lbl_decicionBobyView.Text = string.Format(lbl_decicionBobyView.Text, RecommendProc[0].BookNum, ExtensionMethods.QatarFormatedDate(Convert.ToDateTime(RecommendProc[0].BookDate)));
                                txtGainedHours.Text = RecommendProc[0].NumberOfHoursGained;
                                txtOnlineHours.Text = RecommendProc[0].NumberOfOnlineHours;
                                txtOnlinePercentage.Text = RecommendProc[0].PercentageOfOnlineHours;
                                if (!string.IsNullOrEmpty(RecommendProc[0].OrdinaryOrOwners))
                                    rblHonoraryDegree.Items.FindByText(RecommendProc[0].OrdinaryOrOwners).Selected = true;
                                //if (RecommendProc[0].HavePA == "True")
                                //    ckbHavePA.Checked = true;
                                //if (!string.IsNullOrEmpty(RecommendProc[0].TypeUniversity))
                                //    rblUniversity.Items.FindByText(RecommendProc[0].TypeUniversity).Selected = true;
                                viewControls.Visible = false;


                                txt_bookNum.Text = RecommendProc[0].BookNum;
                                if (RecommendProc[0].BookDate.ToLower().Contains("z"))
                                {
                                    txt_bookDate.Text = SPUtility.CreateDateTimeFromISO8601DateTimeString(RecommendProc[0].BookDate).ToString("dd/MM/yyyy");
                                }
                                else
                                {
                                    var bookDate = RecommendProc[0].BookDate;
                                    DateTime parsedDate = DateTime.Parse(bookDate);
                                    txt_bookDate.Text = parsedDate.ToShortDateString();
                                }
                                //yassin gethas exception
                                if (RecommendProc[0].HasException == "True")
                                {
                                    ckbHaveException.Checked = true;
                                    HasExceptionOptionEnabled();
                                }
                                else
                                {
                                    ckbHaveException.Checked = false;
                                    HasExceptionOptionDisabled();
                                }
                                if (!string.IsNullOrEmpty(RecommendProc[0].SirValue))
                                    drp_Sir.SelectedItem.Text = RecommendProc[0].SirValue;

                                if (!string.IsNullOrEmpty(RecommendProc[0].RespectedValue))
                                    drp_recpected.SelectedItem.Text = RecommendProc[0].RespectedValue;

                                txtExceptionFrom.Text = RecommendProc[0].ExceptionFrom;
                                //BindAttachements();

                            }
                            else if (RecommendProc[0].RecommendationStatus == "Approved")
                            {
                                if (Request != null && !(Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEProgramEmployeeMissingInformation
                                    || Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEProgramEmployeeReview
                                    || Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCProgramEmployeeClarificationReply
                                    || Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEAsianAndEuropianEmployeeMissingInformation
                                    || Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCECulturalMissionStatementReply
                                    || Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEHigherEduInstitutesStatementReply

                                    ))
                                {
                                    pnl_EmpRecommendation.Visible = false;
                                    viewControls.Visible = true;
                                    btn_ApproveProcedures.Visible = false;
                                    lbl_NotificationMsg.Visible = false;
                                    lbl_RecommendationVal.Text = RecommendProc[0].Procedure;
                                    lbl_OccupationName.Text = RecommendProc[0].OccupationName;
                                    lbl_decicionBobyView.Text = string.Format(lbl_decicionBobyView.Text, RecommendProc[0].BookNum, ExtensionMethods.QatarFormatedDate(Convert.ToDateTime(RecommendProc[0].BookDate)));
                                    hdn_ID.Value = RecommendProc[0].ID;
                                    lbl_EmpOpinionVal.Text = RecommendProc[0].Opinion;
                                    txt_DecisiontxtForPrinting.Text = SPHttpUtility.HtmlDecode(RecommendProc[0].DecisionForPrint);
                                    txtOccupationName.Text = RecommendProc[0].OccupationName;
                                    if (!string.IsNullOrEmpty(RecommendProc[0].NumberOfHoursGained))
                                        lbltxtGainedHours.Text = RecommendProc[0].NumberOfHoursGained;
                                    if (!string.IsNullOrEmpty(RecommendProc[0].NumberOfOnlineHours))
                                        lbltxtOnlineHours.Text = RecommendProc[0].NumberOfOnlineHours;
                                    if (!string.IsNullOrEmpty(RecommendProc[0].PercentageOfOnlineHours))
                                        lbltxtOnlinePercentage.Text = RecommendProc[0].PercentageOfOnlineHours;
                                    if (!string.IsNullOrEmpty(RecommendProc[0].OrdinaryOrOwners))
                                        lblrblHonoraryDegree.Text = RecommendProc[0].OrdinaryOrOwners;
                                    if (RecommendProc[0].HavePA == "True")
                                        valckbHavePA.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Yes", (uint)LCID);
                                    else
                                        valckbHavePA.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "No", (uint)LCID);
                                    if (!string.IsNullOrEmpty(RecommendProc[0].TypeUniversity))
                                        lblrblUniversity.Text = RecommendProc[0].TypeUniversity;

                                    txt_bookNum.Text = RecommendProc[0].BookNum;
                                    if (RecommendProc[0].BookDate.ToLower().Contains("z"))
                                    {
                                        txt_bookDate.Text = SPUtility.CreateDateTimeFromISO8601DateTimeString(RecommendProc[0].BookDate).ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        txt_bookDate.Text = RecommendProc[0].BookDate;
                                    }
                                    //BindViewAttachements();
                                }
                            }
                        }
                        else
                        {
                            if (Request != null && !(Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEProgramEmployeeMissingInformation
                                    || Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEProgramEmployeeReview
                                    || Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCProgramEmployeeClarificationReply
                                    || Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEAsianAndEuropianEmployeeMissingInformation
                                    || Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEAsianAndEuropianEmployeeReview
                                    || Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCECulturalMissionStatementReply
                                    || Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEHigherEduInstitutesStatementReply

                                    ))
                            {

                                pnl_EmpRecommendation.Visible = false;
                                viewControls.Visible = true;
                                btn_ApproveProcedures.Visible = false;
                                lbl_NotificationMsg.Visible = false;

                            }
                            else if (!SPContext.Current.Web.CurrentUser.LoginName.ToLower().Equals(Request.AssignedTo.ToLower()))
                            {
                                pnl_EmpRecommendation.Visible = false;
                                viewControls.Visible = true;
                                btn_ApproveProcedures.Visible = false;
                                lbl_NotificationMsg.Visible = false;

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
                Logging.GetInstance().Debug("Exiting method SearchStatusRecommendationUC.GetProcedures");
            }
        }
        protected void ReviewDecisionClick(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method SearchStatusRecommendationUC.btn_ReviewDecisionClick");
                if (Page.Session["DisplayRequestId"] != null)
                {
                    int dec = 0;
                    if (rdbtn_EmpRecommendation.Text == "1")
                    {
                        dec = 1;
                    }
                    Common.BL.FinalDecisionPrint.ReviewUCEPDF(hdn_RequestNumber.Value, txt_DecisiontxtForPrinting.Text, txtOccupationName.Text, txt_bookDate.Text, txt_bookNum.Text, lbl_headManagerName.Text, "PDF", drp_Sir.SelectedItem.Text, drp_recpected.SelectedItem.Text, LCID, dec);
                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SearchStatusRecommendationUC.btn_ReviewDecisionClick");
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method SearchStatusRecommendationUC.btn_Save_Click");
                //if (!IsRefresh)
                //{
                if (hdn_ID.Value != "")
                    BL.AllProcedures.UpdateRecommendationStatusProc(hdn_ID.Value.ToString(), "Save", txt_EmpOpinion.Text,
                         rdbtn_EmpRecommendation.SelectedIndex != -1 ? rdbtn_EmpRecommendation.SelectedItem.Text : string.Empty, txt_DecisiontxtForPrinting.Text, txtOccupationName.Text, txtGainedHours.Text, txtOnlineHours.Text, txtOnlinePercentage.Text, rblHonoraryDegree.SelectedIndex != -1 ? rblHonoraryDegree.SelectedItem.Text : string.Empty, "0", string.Empty, txt_bookNum.Text, txt_bookDate.Text, lbl_headManagerName.Text, ckbHaveException.Checked, txtExceptionFrom.Text, drp_Sir.SelectedItem.Text, drp_recpected.SelectedItem.Text);
                else
                {
                    if (Page.Session["DisplayRequestId"] != null)
                        BL.AllProcedures.AddRecommendationStatus(txt_EmpOpinion.Text, Page.Session["DisplayRequestId"].ToString(),
                           rdbtn_EmpRecommendation.SelectedIndex != -1 ? rdbtn_EmpRecommendation.SelectedItem.Text : string.Empty,
                           "Save", txt_DecisiontxtForPrinting.Text, txtOccupationName.Text, txtGainedHours.Text, txtOnlineHours.Text, txtOnlinePercentage.Text, rblHonoraryDegree.SelectedIndex != -1 ? rblHonoraryDegree.SelectedItem.Text : string.Empty, "0", string.Empty, txt_bookNum.Text, txt_bookDate.Text, lbl_headManagerName.Text, ckbHaveException.Checked, txtExceptionFrom.Text, drp_Sir.SelectedItem.Text, drp_recpected.SelectedItem.Text);
                    GetSavedProcedures();
                }
                CommityDecisionCtr.Savedata(int.Parse(Page.Session["DisplayRequestId"].ToString()), SPContext.Current.Site.Url);
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
                Logging.GetInstance().Debug("Exiting method SearchStatusRecommendationUC.btn_Save_Click");
            }
        }
        protected void btn_ApproveProcedures_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method SearchStatusRecommendationUC.btn_ApproveProcedures_Click");
                //if (!IsRefresh)
                {
                    if (hdn_ID.Value != "")
                    {
                        BL.AllProcedures.UpdateRecommendationStatusProc(hdn_ID.Value.ToString(), "Approved", txt_EmpOpinion.Text, rdbtn_EmpRecommendation.SelectedItem.Text, txt_DecisiontxtForPrinting.Text, txtOccupationName.Text, txtGainedHours.Text, txtOnlineHours.Text, txtOnlinePercentage.Text, rblHonoraryDegree.SelectedIndex != -1 ? rblHonoraryDegree.SelectedItem.Text : string.Empty, "0", string.Empty, txt_bookNum.Text, txt_bookDate.Text, lbl_headManagerName.Text, ckbHaveException.Checked, txtExceptionFrom.Text, drp_Sir.SelectedItem.Text, drp_recpected.SelectedItem.Text);
                    }
                    else
                    {
                        if (Page.Session["DisplayRequestId"] != null)
                        {
                            BL.AllProcedures.AddRecommendationStatus(txt_EmpOpinion.Text, Page.Session["DisplayRequestId"].ToString(), rdbtn_EmpRecommendation.SelectedItem.Text, "Approved", txt_DecisiontxtForPrinting.Text, txtOccupationName.Text, txtGainedHours.Text, txtOnlineHours.Text, txtOnlinePercentage.Text, rblHonoraryDegree.SelectedIndex != -1 ? rblHonoraryDegree.SelectedItem.Text : string.Empty, "0", string.Empty, txt_bookNum.Text, txt_bookDate.Text, lbl_headManagerName.Text, ckbHaveException.Checked, txtExceptionFrom.Text, drp_Sir.SelectedItem.Text, drp_recpected.SelectedItem.Text);
                        }
                    }
                    //NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCEProgramManagerReviewRecommendation, txt_EmpOpinion.Text, "Requests", Page.Session["DisplayRequestId"].ToString());
                    Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.Requests, Utilities.Constants.RequestHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.UCEProgramManagerReviewRecommendation, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["DisplayRequestId"].ToString(), "No");

                    //Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.UCEProgramManagerReviewRecommendation, Page.Session["DisplayRequestId"].ToString(), Utilities.Constants.ProgramManagerGroupName.ToLower());
                    StatusRecommendAttachements.SaveAttachments();

                    BL.Request.UpdateRejection(new Entities.Request()
                    {
                        ID = Convert.ToInt32(Page.Session["DisplayRequestId"]),
                        RejectedFrom = SPContext.Current.Web.CurrentUser.Name,
                        RejectionReason = "ReturnFromProgramEmployee",
                        RejectionDate = DateTime.Now
                    });
                    //Thread.Sleep(3000);
                    CommityDecisionCtr.Savedata(int.Parse(Page.Session["DisplayRequestId"].ToString()), SPContext.Current.Site.Url);

                    lbl_Success.Visible = true;
                    List<Entities.Procedures> RecommendProc = BL.AllProcedures.GetSavedRecommendationStatusProc(Page.Session["DisplayRequestId"].ToString(), SPContext.Current.Web.CurrentUser.Name.ToLower());
                    if (RecommendProc.Count > 0)
                    {
                        if (RecommendProc[0].RecommendationStatus == "Approved")
                        {
                            pnl_EmpRecommendation.Visible = false;
                            viewControls.Visible = true;
                            btn_ApproveProcedures.Visible = false;
                            lbl_NotificationMsg.Visible = false;
                            lbl_RecommendationVal.Text = RecommendProc[0].Procedure;
                            lbl_OccupationName.Text = RecommendProc[0].OccupationName;
                            lbl_decicionBobyView.Text = string.Format(lbl_decicionBobyView.Text, RecommendProc[0].BookNum, ExtensionMethods.QatarFormatedDate(Convert.ToDateTime(RecommendProc[0].BookDate)));
                            hdn_ID.Value = RecommendProc[0].ID;
                            lbl_EmpOpinionVal.Text = RecommendProc[0].Opinion;

                            if (RecommendProc[0].Procedure == "Approved" || RecommendProc[0].Procedure == "بالموافقة")
                            {
                                lbl_DecisiontxtVal.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "approveDecisionText", (uint)LCID);
                            }
                            else
                            {
                                lbl_DecisiontxtVal.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "rejectDecisionText", (uint)LCID);
                            }

                            lbl_DecisiontxtVal.Text = lbl_DecisiontxtVal.Text + " " + SPHttpUtility.HtmlDecode(RecommendProc[0].DecisionForPrint);

                            if (!string.IsNullOrEmpty(RecommendProc[0].NumberOfHoursGained))
                                lbltxtGainedHours.Text = RecommendProc[0].NumberOfHoursGained;
                            if (!string.IsNullOrEmpty(RecommendProc[0].NumberOfOnlineHours))
                                lbltxtOnlineHours.Text = RecommendProc[0].NumberOfOnlineHours;
                            if (!string.IsNullOrEmpty(RecommendProc[0].PercentageOfOnlineHours))
                                lbltxtOnlinePercentage.Text = RecommendProc[0].PercentageOfOnlineHours;
                            if (!string.IsNullOrEmpty(RecommendProc[0].OrdinaryOrOwners))
                                lblrblHonoraryDegree.Text = RecommendProc[0].OrdinaryOrOwners;
                            if (RecommendProc[0].HavePA == "True")
                                valckbHavePA.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Yes", (uint)LCID);
                            else
                                valckbHavePA.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "No", (uint)LCID);

                            if (!string.IsNullOrEmpty(RecommendProc[0].TypeUniversity))
                                lblrblUniversity.Text = RecommendProc[0].TypeUniversity;
                            BindViewAttachements();
                        }
                    }
                    NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCEProgramManagerReviewRecommendation, txt_EmpOpinion.Text, "Requests", Page.Session["DisplayRequestId"].ToString());

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
                Logging.GetInstance().Debug("Exiting method SearchStatusRecommendationUC.btn_ApproveProcedures_Click");
            }
        }


        private void BindViewAttachements()
        {
            Logging.GetInstance().Debug("Entering method SearchStatusRecommendationUC.BindViewAttachements");
            try
            {

                #region Prerequiestes
                /// add colum "RequestID" lookup from Requests
                /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
                /// add column Group, single line of text
                #endregion
                #region Display Mode

                ViewStatusRecommendAttachements.DocumentLibraryName = Utilities.Constants.ProgEmpStatusandRecomAttachements;
                ViewStatusRecommendAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
                ViewStatusRecommendAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NotesPrepared", (uint)LCID);
                ViewStatusRecommendAttachements.Group = "StatusRecommendAttachements";
                ViewStatusRecommendAttachements.RequestID = Convert.ToInt32(Page.Session["DisplayRequestId"].ToString());
                ViewStatusRecommendAttachements.Enabled = false;
                ViewStatusRecommendAttachements.Bind();

                #endregion
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method SearchStatusRecommendationUC.BindViewAttachements");
            }
        }

        private void BindAttachements()
        {
            Logging.GetInstance().Debug("Entering method SearchStatusRecommendationUC.BindAttachements");
            try
            {
                #region Prerequiestes
                /// add colum "RequestID" lookup from Requests
                /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
                /// add column Group, single line of text
                #endregion
                #region Edit Mode

                StatusRecommendAttachements.DocumentLibraryName = Utilities.Constants.ProgEmpStatusandRecomAttachements;
                StatusRecommendAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
                StatusRecommendAttachements.MaxSize = 7168000;//7MB
                StatusRecommendAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NotesPrepared", (uint)LCID);
                StatusRecommendAttachements.Group = "StatusRecommendAttachements";// field name for example, shouldn't be used for more than one field per each control.
                StatusRecommendAttachements.RequestID = Convert.ToInt32(Page.Session["DisplayRequestId"].ToString());
                StatusRecommendAttachements.SupportedExtensions = "PNG,PDF,JPG";
                StatusRecommendAttachements.IsRequired = false;
                StatusRecommendAttachements.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
                StatusRecommendAttachements.Enabled = true;

                StatusRecommendAttachements.Bind();

                #endregion


            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method SearchStatusRecommendationUC.BindAttachements");
            }

        }
        private void Bindrdbtn()
        {
            rdbtn_EmpRecommendation.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Approved", (uint)LCID), "0"));
            rdbtn_EmpRecommendation.Items.Insert(1, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Rejected", (uint)LCID), "1"));
            rdbtn_EmpRecommendation.Items.Insert(2, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "TransferToComitee", (uint)LCID), "2"));


        }



        private void DisplayRequestData()
        {
            Logging.GetInstance().Debug("Entering method DisplayRequestDetails.DisplayRequestData");
            try
            {
                int requestId = int.Parse(Convert.ToString(Page.Session["DisplayRequestId"]));
                string yesValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Yes", (uint)LCID);
                string noValue = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "No", (uint)LCID);
                Request requestItem = BL.Request.Reviewer_GetRequestByNumber(requestId, LCID);


                if (requestItem != null)
                {
                    Common.Entities.Applicants applicant = Common.BL.Applicants.GetApplicantByID(requestItem.ApplicantID.ID, LCID);
                    if (applicant != null)
                    {
                        lblNameValue.Text = LCID == (int)Language.English ? applicant.EnglishName : applicant.ArabicName;
                        lblPersonalIDValue.Text = applicant.PersonalID;
                    }
                    //divformControls.Visible = true;
                    //lblNoRequest.Visible = false;

                    //lblRequestNumberValue.Text = requestItem.RequestNumber;
                    //lblRequestCreationDateValue.Text = ExtensionMethods.QatarFormatedDate(requestItem.RequestCreationDate);
                    //lblRequestSubmitDateValue.Text = requestItem.SubmitDate != DateTime.MinValue ? ExtensionMethods.QatarFormatedDate(requestItem.SubmitDate) : string.Empty;
                    lblAcademicDegreeForEquivalenceValue.Text = requestItem.AcademicDegree != null ? requestItem.AcademicDegree.SelectedTitle : string.Empty;
                    lblCerticateAcademicDegreeValue.Text = requestItem.AcademicDegreeForEquivalence != null ? requestItem.AcademicDegreeForEquivalence.SelectedTitle : string.Empty;
                    //hdnCertificateAcademic.Value = requestItem.AcademicDegreeForEquivalence != null ? requestItem.AcademicDegreeForEquivalence.SelectedID : string.Empty;
                    //hdnCertificateAcademicTxt.Value = requestItem.AcademicDegreeForEquivalence != null ? requestItem.AcademicDegreeForEquivalence.SelectedTitle : string.Empty;
                    //lblCertificateThroughScholarshipValue.Text = requestItem.CertificateThroughScholarship ? yesValue : noValue;

                    //if (!string.IsNullOrEmpty(requestItem.EntityProvidingStudy))
                    //{
                    //    lblEntityProvidingStudy.Visible = true;
                    //    lblEntityProvidingStudyValue.Text = requestItem.EntityProvidingStudy;

                    //}

                    //lblCampusStudyValue.Text = requestItem.CampusStudy ? yesValue : noValue;


                    //lblCampusExamValue.Text = requestItem.CampusExam ? yesValue : noValue;

                    // have PA values
                    lblHavePAValue.Text = requestItem.HavePAOrNot ? yesValue : noValue;
                    if (requestItem.HavePAOrNot == true)
                    {
                        fileUploadNoHavePA.Visible = false;
                        lblHaveNoPA.Visible = false;
                    }
                    else
                    {
                        fileUploadHavePA.Visible = false;
                        lblHavePAAttachment.Visible = false;
                    }



                    lblCountriesValue.Text = requestItem.CountryOfStudy.SelectedTitle;
                    if (string.IsNullOrEmpty(requestItem.CountryOfStudy.SelectedID))
                    {
                        lblNewCoutriesAdded.Visible = true;
                    }
                    if (requestItem.University != null && !string.IsNullOrEmpty(requestItem.University.SelectedID))
                    {

                        lblUniversityValue.Text = requestItem.University.SelectedTitle;
                        //if (Common.BL.University.IsUniversityCHED(int.Parse(requestItem.University.SelectedID)))
                        //{

                        //    lblUniversityCHED.Text = string.Format(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CHEDMessage", (uint)LCID), requestItem.University.SelectedTitle);
                        //    lblUniversityHEDD.Text = string.Empty;
                        //}
                        if (Common.BL.University.IsUniversityHEDD(int.Parse(requestItem.University.SelectedID)))
                            lblUniversityHEDDValue.Text = yesValue;
                        else lblUniversityHEDDValue.Text = noValue;
                    }
                    else if (!string.IsNullOrEmpty(requestItem.UniversityNotFoundInList))
                    {
                        lblUniversityNotFoundValue.Visible = true;
                        lblNewUniversitiesAdded.Visible = true;
                        lblUniversityNotFoundValue.Text = requestItem.UniversityNotFoundInList;
                    }

                    //get universty type 
                    if (requestItem.UniversityType != null)
                        lblUniversityTypeValue.Text = requestItem.UniversityType.ToLower() == "government" ? HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "universityGovernment", (uint)LCID) : HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "universityPrivate", (uint)LCID);
                    else
                        lblUniversityTypeValue.Text = "NA";


                    if (requestItem.UniversityList != null)
                    {
                        lblUniversityList.Visible = true;
                        lblUniversityListValue.Visible = true;
                        lblUniversityListValue.Text = requestItem.UniversityList;
                    }
                    if (requestItem.Specialization != null)
                    {

                        lblSpecializationValue.Text = requestItem.Specialization.SelectedTitle;
                    }
                    else if (!string.IsNullOrEmpty(requestItem.SpecializationNotFoundInList))
                    {
                        lblSpecializationNotFoundValue.Visible = true;
                        lblSpecializationNotFoundValue.Text = requestItem.SpecializationNotFoundInList;
                    }
                    if (requestItem.Faculty != null)
                    {

                        lblFacultyValue.Text = requestItem.Faculty;
                    }
                    else if (!string.IsNullOrEmpty(requestItem.FacultyNotFoundInList))
                    {
                        lblFacultyNotFoundValue.Visible = true;
                        lblFacultyNotFoundValue.Text = requestItem.FacultyNotFoundInList;
                    }
                    //lblStudyingLanguageValue.Text = requestItem.StudyLanguage.SelectedTitle;
                    //if (requestItem.StudySystem != null)
                    //{
                    //    lblStudyingSystemValue.Visible = true;
                    //    lblStudyingSystemValue.Text = requestItem.StudySystem.SelectedTitle;
                    //}
                    lblStartDateValue.Text = ExtensionMethods.QatarFormatedDate(requestItem.StudyStartDate);
                    if (requestItem.StudyGraduationDate != DateTime.MinValue)
                    {

                        lblGraduationDateValue.Text = ExtensionMethods.QatarFormatedDate(requestItem.StudyGraduationDate);
                    }

                    //if (requestItem.UniversityMainHeadquarter != null)
                    //{

                    //    lblUniversityMainHeadQuarterValue.Text = requestItem.UniversityMainHeadquarter.SelectedTitle;
                    //}
                    //else if (!string.IsNullOrEmpty(requestItem.NewUniversityHeadquarter))
                    //{
                    //    lblUniversityMainAdded.Visible = true;
                    //    lblNewUniversityMainHeadQuarterValue.Visible = true;
                    //    lblNewUniversityMainHeadQuarterValue.Text = requestItem.NewUniversityHeadquarter;
                    //}

                    //if (!string.IsNullOrEmpty(requestItem.UniversityAddress))
                    //{

                    //    lblAddressValue.Text = requestItem.UniversityAddress;
                    //}


                    //if (!string.IsNullOrEmpty(requestItem.UniversityEmail))
                    //{
                    //    lblUniversityEmailValue.Text = requestItem.UniversityEmail;
                    //}

                    //lblWorkingOrNotValue.Text = requestItem.WorkingOrNot ? yesValue : noValue;

                    //if (requestItem.WorkingOrNot == true)
                    //{
                    //    // set drop of occupation and entity working for

                    //    if (requestItem.EntityWorkingFor != null)
                    //    {
                    //        WorkingFor.Visible = true;
                    //        lblEntityWorkingFor.Visible = true;
                    //        lblEntityWorkingForValue.Visible = true;
                    //        lblEntityWorkingForValue.Text = requestItem.EntityWorkingFor.SelectedTitle;
                    //    }
                    //    else if (!string.IsNullOrEmpty(requestItem.OtherEntityWorkingFor))
                    //    {
                    //        WorkingFor.Visible = true;
                    //        lblEntityWorkingFor.Visible = true;
                    //        lblOtherEntityWorkingForValue.Visible = true;
                    //        lblOtherEntityWorkingForValue.Text = requestItem.OtherEntityWorkingFor;
                    //    }

                    //    fileUploadNotWorking.Visible = false;

                    //    //Ahmed
                    //    if (requestItem.EntityNeedsEquivalency != null)
                    //    {
                    //        workEntityNeedsEquivalency.Visible = true;
                    //        lblEntityNeedsEquivalency.Visible = true;
                    //        lblEntityNeedsEquivalencyValue.Visible = true;
                    //        lblEntityNeedsEquivalencyValue.Text = requestItem.EntityNeedsEquivalency.SelectedTitle;
                    //    }
                    //    else if (!string.IsNullOrEmpty(requestItem.OtherEntityNeedsEquivalency))
                    //    {
                    //        workEntityNeedsEquivalency.Visible = true;
                    //        lblEntityNeedsEquivalency.Visible = true;
                    //        lblOtherEntityNeedsEquivalencyValue.Visible = true;
                    //        lblOtherEntityNeedsEquivalencyValue.Text = requestItem.OtherEntityNeedsEquivalency;
                    //    }

                    //}
                    //else
                    //{
                    //    if (requestItem.EntityNeedsEquivalency != null)
                    //    {
                    //        workEntityNeedsEquivalency.Visible = true;
                    //        lblEntityNeedsEquivalency.Visible = true;
                    //        lblEntityNeedsEquivalencyValue.Visible = true;
                    //        lblEntityNeedsEquivalencyValue.Text = requestItem.EntityNeedsEquivalency.SelectedTitle;
                    //    }
                    //    else if (!string.IsNullOrEmpty(requestItem.OtherEntityNeedsEquivalency))
                    //    {
                    //        workEntityNeedsEquivalency.Visible = true;
                    //        lblEntityNeedsEquivalency.Visible = true;
                    //        lblOtherEntityNeedsEquivalencyValue.Visible = true;
                    //        lblOtherEntityNeedsEquivalencyValue.Text = requestItem.OtherEntityNeedsEquivalency;
                    //    }
                    //    fileUploadOrgAttach.Visible = false;
                    //}

                    //List<CalculatedDetailsForCertificate> calculatedDetailsData = BL.CalculatedDetailsForCertificate.GetCalculatedDetailsForCertificate(requestId, LCID);
                    //if (calculatedDetailsData != null && calculatedDetailsData.Count > 0)
                    //{

                    //    repCalculatedDetailsForCertificate.DataSource = calculatedDetailsData;
                    //    repCalculatedDetailsForCertificate.DataBind();
                    //}
                }

                //else
                //{
                //    divformControls.Visible = false;
                //    lblNoRequest.Visible = true;
                //}
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method DisplayRequestDetails.DisplayRequestData");
            }

        }
        #region attachments yassin
        private void BindAttachments()
        {
            Logging.GetInstance().Debug("Entering method DisplayRequestDetails.BindAttachments");
            try
            {
                int requestId = int.Parse(Convert.ToString(Page.Session["DisplayRequestId"]));
                Request requestItem = BL.Request.Reviewer_GetRequestByNumber(requestId, LCID);


                hdnCertificateAcademic.Value = requestItem.AcademicDegreeForEquivalence != null ? requestItem.AcademicDegreeForEquivalence.SelectedID : string.Empty;
                hdnCertificateAcademicTxt.Value = requestItem.AcademicDegreeForEquivalence != null ? requestItem.AcademicDegreeForEquivalence.SelectedTitle : string.Empty;

                fileEquivalent.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                fileEquivalent.DocLibWebUrl = SPContext.Current.Site.Url;
                fileEquivalent.Group = "CertificateEquivalentAttachment";
                fileEquivalent.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileEquivalent.Enabled = false;
                fileEquivalent.Bind();

                fileGrades.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                fileGrades.DocLibWebUrl = SPContext.Current.Site.Url;
                fileGrades.Group = "GardesAttachment";
                fileGrades.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileGrades.Enabled = false;
                fileGrades.Bind();

                CertificateSequence.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                CertificateSequence.DocLibWebUrl = SPContext.Current.Site.Url;
                CertificateSequence.Group = "CertificateSequence" + hdnCertificateAcademic.Value;
                CertificateSequence.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                CertificateSequence.Enabled = false;
                CertificateSequence.Bind();

                Certificates.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                Certificates.DocLibWebUrl = SPContext.Current.Site.Url;
                Certificates.Group = hdnCertificateAcademic.Value;
                Certificates.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                Certificates.Enabled = false;
                Certificates.Bind();


                bool WorkingFor = true;
                if (requestItem.EntityWorkingFor == null || requestItem.OtherEntityWorkingFor == null)
                    WorkingFor = false;


                //if (WorkingFor)
                //{
                //    fileUploadOrgAttach.DocumentLibraryName = Utilities.Constants.OrganizationalLettersAttachments;
                //    fileUploadOrgAttach.DocLibWebUrl = SPContext.Current.Site.Url;
                //    fileUploadOrgAttach.Group = "CopyOfOrganizationlLetter";
                //    fileUploadOrgAttach.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                //    fileUploadOrgAttach.Enabled = false;
                //    fileUploadOrgAttach.Bind();
                //}
                //else
                //{

                //    fileUploadNotWorking.DocumentLibraryName = Utilities.Constants.OrganizationalLettersAttachments;
                //    fileUploadNotWorking.DocLibWebUrl = SPContext.Current.Site.Url;
                //    fileUploadNotWorking.Group = "NotWorkingCopyOfOrganizationlLetter";
                //    fileUploadNotWorking.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                //    fileUploadNotWorking.Enabled = false;
                //    fileUploadNotWorking.Bind();
                //}

                if (WorkingFor)
                {
                    OrgAttach.DocumentLibraryName = Utilities.Constants.OrganizationalLettersAttachments;
                    OrgAttach.DocLibWebUrl = SPContext.Current.Site.Url;
                    OrgAttach.Group = "CopyOfOrganizationlLetter";
                    OrgAttach.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                    OrgAttach.Enabled = false;
                    OrgAttach.Bind();
                }
                else
                {
                    OrgAttach.DocumentLibraryName = Utilities.Constants.OrganizationalLettersAttachments;
                    OrgAttach.DocLibWebUrl = SPContext.Current.Site.Url;
                    OrgAttach.Group = "NotWorkingCopyOfOrganizationlLetter";
                    OrgAttach.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                    OrgAttach.Enabled = false;
                    OrgAttach.Bind();
                }

                //fileUploadCertificates.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                //fileUploadCertificates.DocLibWebUrl = SPContext.Current.Site.Url;
                //fileUploadCertificates.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "PreviousCertificates", (uint)LCID);
                //fileUploadCertificates.Group = hdnCertificateAcademic.Value;
                //fileUploadCertificates.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                //fileUploadCertificates.Enabled = false;
                //fileUploadCertificates.Bind();


                //fileUploadSequenceCert.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                //fileUploadSequenceCert.DocLibWebUrl = SPContext.Current.Site.Url;
                //fileUploadSequenceCert.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CertificateSequence", (uint)LCID);
                //fileUploadSequenceCert.Group = "CertificateSequence" + hdnCertificateAcademic.Value;
                //fileUploadSequenceCert.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                //fileUploadSequenceCert.Enabled = false;
                //fileUploadSequenceCert.Bind();



                Certificates.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                Certificates.DocLibWebUrl = SPContext.Current.Site.Url;
                Certificates.Group = hdnCertificateAcademic.Value;
                Certificates.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                Certificates.Enabled = false;
                Certificates.Bind();


                //fileUploadCertificateEquivalent.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                //fileUploadCertificateEquivalent.DocLibWebUrl = SPContext.Current.Site.Url;
                //fileUploadCertificateEquivalent.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "CertificateToBeEquivalent", (uint)LCID);
                //fileUploadCertificateEquivalent.Group = "CertificateEquivalentAttachment";
                //fileUploadCertificateEquivalent.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                //fileUploadCertificateEquivalent.Enabled = false;
                //fileUploadCertificateEquivalent.Bind();


                fileEquivalent.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                fileEquivalent.DocLibWebUrl = SPContext.Current.Site.Url;
                fileEquivalent.Group = "CertificateEquivalentAttachment";
                fileEquivalent.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileEquivalent.Enabled = false;
                fileEquivalent.Bind();


                //fileUploadGrades.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                //fileUploadGrades.DocLibWebUrl = SPContext.Current.Site.Url;
                //fileUploadGrades.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Grades", (uint)LCID);
                //fileUploadGrades.Group = "GardesAttachment";
                //fileUploadGrades.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                //fileUploadGrades.Enabled = false;
                //fileUploadGrades.Bind();

                fileGrades.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                fileGrades.DocLibWebUrl = SPContext.Current.Site.Url;
                fileGrades.Group = "GardesAttachment";
                fileGrades.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileGrades.Enabled = false;
                fileGrades.Bind();

                fileUploadHavePA.DocumentLibraryName = Utilities.Constants.HavePAAttachments;
                fileUploadHavePA.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadHavePA.Group = "YesHavePAAttachments";
                fileUploadHavePA.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileUploadHavePA.Enabled = false;
                fileUploadHavePA.Bind();

                fileUploadNoHavePA.DocumentLibraryName = Utilities.Constants.HavePAAttachments;
                fileUploadNoHavePA.DocLibWebUrl = SPContext.Current.Site.Url;
                fileUploadNoHavePA.Group = "NoHavePAAttachments";
                fileUploadNoHavePA.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                fileUploadNoHavePA.Enabled = false;
                fileUploadNoHavePA.Bind();

                string bachelor = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Bachelor", (uint)LCID);
                if (hdnCertificateAcademicTxt.Value.Equals(bachelor))
                {
                    //fileUploadDiploma.Visible = true;
                    //fileUploadDiploma.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                    //fileUploadDiploma.DocLibWebUrl = SPContext.Current.Site.Url;
                    //fileUploadDiploma.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Diploma", (uint)LCID);
                    //fileUploadDiploma.Group = "Diploma" + hdnCertificateAcademic.Value;
                    //fileUploadDiploma.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                    //fileUploadDiploma.Enabled = false;
                    //fileUploadDiploma.Bind();

                    //fileUploadInterDiploma.Visible = true;
                    //fileUploadInterDiploma.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                    //fileUploadInterDiploma.DocLibWebUrl = SPContext.Current.Site.Url;
                    //fileUploadInterDiploma.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "IntermediateDiploma", (uint)LCID);
                    //fileUploadInterDiploma.Group = "InterMediateDiploma" + hdnCertificateAcademic.Value;
                    //fileUploadInterDiploma.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                    //fileUploadInterDiploma.Enabled = false;
                    //fileUploadInterDiploma.Bind();

                    diploma.Visible = true;
                    fileDiploma.Visible = true;
                    fileDiploma.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                    fileDiploma.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileDiploma.Group = "Diploma" + hdnCertificateAcademic.Value;
                    fileDiploma.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                    fileDiploma.Enabled = false;
                    fileDiploma.Bind();

                    interDiploma.Visible = true;
                    fileInterDiploma.Visible = true;
                    fileInterDiploma.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                    fileInterDiploma.DocLibWebUrl = SPContext.Current.Site.Url;
                    fileInterDiploma.Group = "InterMediateDiploma" + hdnCertificateAcademic.Value;
                    fileInterDiploma.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                    fileInterDiploma.Enabled = false;
                    fileInterDiploma.Bind();


                }

                CertificateSequence.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
                CertificateSequence.DocLibWebUrl = SPContext.Current.Site.Url;
                CertificateSequence.Group = "CertificateSequence" + hdnCertificateAcademic.Value;
                CertificateSequence.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                CertificateSequence.Enabled = false;
                CertificateSequence.Bind();


                //fileUploadDelegates.DocumentLibraryName = Utilities.Constants.DelegationDocuments;
                //fileUploadDelegates.DocLibWebUrl = SPContext.Current.Site.Url;
                //fileUploadDelegates.Group = "DelegationTemplates";
                //fileUploadDelegates.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                //fileUploadDelegates.Enabled = false;
                //fileUploadDelegates.Bind();

                Delegates.DocumentLibraryName = Utilities.Constants.DelegationDocuments;
                Delegates.DocLibWebUrl = SPContext.Current.Site.Url;
                Delegates.Group = "DelegationTemplates";
                Delegates.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                Delegates.Enabled = false;
                Delegates.Bind();

                //if (ViewState["DisplayCalculatedDetails"] != null)
                //{
                //    fileUploadCalculatedDetails.DocumentLibraryName = Utilities.Constants.CalculatedDetailsForCertificateAttachments;
                //    fileUploadCalculatedDetails.DocLibWebUrl = SPContext.Current.Site.Url;
                //    fileUploadCalculatedDetails.Group = ViewState["DisplayCalculatedDetails"].ToString();
                //    fileUploadCalculatedDetails.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
                //    fileUploadCalculatedDetails.Enabled = false;
                //    fileUploadCalculatedDetails.Bind();

                //}
            }

            catch (Exception ex)
            {

                Logging.GetInstance().LogException(ex);
                throw;
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method DisplayRequestDetails.BindAttachments");
            }

        }



        #endregion


        //private void BindAttachments()
        //{
        //    Logging.GetInstance().Debug("Entering method DisplayRequestDetails.BindAttachments");
        //    try
        //    {
        //        int requestId=int.Parse(Page.Session["DisplayRequestId"].ToString());
        //        Request requestItem = BL.Request.Reviewer_GetRequestByNumber(requestId, LCID);


        //        hdnCertificateAcademic.Value = requestItem.AcademicDegreeForEquivalence != null ? requestItem.AcademicDegreeForEquivalence.SelectedID : string.Empty;
        //        hdnCertificateAcademicTxt.Value = requestItem.AcademicDegreeForEquivalence != null ? requestItem.AcademicDegreeForEquivalence.SelectedTitle : string.Empty;

        //        fileEquivalent.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
        //        fileEquivalent.DocLibWebUrl = SPContext.Current.Site.Url;
        //        fileEquivalent.Group = "CertificateEquivalentAttachment";
        //        fileEquivalent.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
        //        fileEquivalent.Enabled = false;
        //        fileEquivalent.Bind();

        //        fileGrades.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
        //        fileGrades.DocLibWebUrl = SPContext.Current.Site.Url;
        //        fileGrades.Group = "GardesAttachment";
        //        fileGrades.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
        //        fileGrades.Enabled = false;
        //        fileGrades.Bind();

        //        Certificates.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
        //        Certificates.DocLibWebUrl = SPContext.Current.Site.Url;
        //        Certificates.Group = hdnCertificateAcademic.Value;
        //        Certificates.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
        //        Certificates.Enabled = false;
        //        Certificates.Bind();


        //        string bachelor = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Bachelor", (uint)LCID);
        //        if (hdnCertificateAcademicTxt.Value.Equals(bachelor))
        //        {
        //            fileUploadDiploma.Visible = true;
        //            fileUploadDiploma.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
        //            fileUploadDiploma.DocLibWebUrl = SPContext.Current.Site.Url;
        //            fileUploadDiploma.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Diploma", (uint)LCID);
        //            fileUploadDiploma.Group = "Diploma" + hdnCertificateAcademic.Value;
        //            fileUploadDiploma.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
        //            fileUploadDiploma.Enabled = false;
        //            fileUploadDiploma.Bind();

        //            fileUploadInterDiploma.Visible = true;
        //            fileUploadInterDiploma.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
        //            fileUploadInterDiploma.DocLibWebUrl = SPContext.Current.Site.Url;
        //            fileUploadInterDiploma.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "IntermediateDiploma", (uint)LCID);
        //            fileUploadInterDiploma.Group = "InterMediateDiploma" + hdnCertificateAcademic.Value;
        //            fileUploadInterDiploma.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
        //            fileUploadInterDiploma.Enabled = false;
        //            fileUploadInterDiploma.Bind();

        //            diploma.Visible = true;
        //            fileDiploma.Visible = true;
        //            fileDiploma.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
        //            fileDiploma.DocLibWebUrl = SPContext.Current.Site.Url;
        //            fileDiploma.Group = "Diploma" + hdnCertificateAcademic.Value;
        //            fileDiploma.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
        //            fileDiploma.Enabled = false;
        //            fileDiploma.Bind();

        //            interDiploma.Visible = true;
        //            fileInterDiploma.Visible = true;
        //            fileInterDiploma.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
        //            fileInterDiploma.DocLibWebUrl = SPContext.Current.Site.Url;
        //            fileInterDiploma.Group = "InterMediateDiploma" + hdnCertificateAcademic.Value;
        //            fileInterDiploma.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
        //            fileInterDiploma.Enabled = false;
        //            fileInterDiploma.Bind();


        //        }

        //        CertificateSequence.DocumentLibraryName = Utilities.Constants.CertificatesAttachments;
        //        CertificateSequence.DocLibWebUrl = SPContext.Current.Site.Url;
        //        CertificateSequence.Group = "CertificateSequence" + hdnCertificateAcademic.Value;
        //        CertificateSequence.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
        //        CertificateSequence.Enabled = false;
        //        CertificateSequence.Bind();




        //        Delegates.DocumentLibraryName = Utilities.Constants.DelegationDocuments;
        //        Delegates.DocLibWebUrl = SPContext.Current.Site.Url;
        //        Delegates.Group = "DelegationTemplates";
        //        Delegates.RequestID = int.Parse(Page.Session["DisplayRequestId"].ToString());
        //        Delegates.Enabled = false;
        //        Delegates.Bind();


        //    }

        //    catch (Exception ex)
        //    {
        //        Logging.GetInstance().LogException(ex);
        //    }
        //    finally
        //    {

        //        Logging.GetInstance().Debug("Exiting method DisplayRequestDetails.BindAttachments");
        //    }

        //}


        protected void ckbHaveException_CheckedChanged(object sender, EventArgs e)
        {
            var ckbHaveException = sender as CheckBox;
            if (ckbHaveException.Checked)
            {
                HasExceptionOptionEnabled();
            }
            else
            {
                HasExceptionOptionDisabled();
            }

        }

        private void HasExceptionOptionDisabled()
        {
            lblExceptionFrom.Visible = false;
            txtExceptionFrom.Visible = false;
            txtExceptionFrom.Enabled = false;
            RequiredFieldExceptionFrom.Enabled = false;
        }

        private void HasExceptionOptionEnabled()
        {
            lblExceptionFrom.Visible = true;
            txtExceptionFrom.Visible = true;
            txtExceptionFrom.Enabled = true;
            RequiredFieldExceptionFrom.Enabled = true;
        }
    }
}
