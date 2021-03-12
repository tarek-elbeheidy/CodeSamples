using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.BL;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.UCE.Utilities;
using ITWORX.MOEHE.Utilities;
using System.Collections.Generic;
using System.Web;
using ITWORX.MOEHEWF.UCE.Utilities;
using Nintex.Workflow.HumanApproval;
using System.Threading;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class Add_ProcedureProgramManager : UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload ProcedureProgramManagerAttachements;
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
                BinddrpEmployees();
                Binddrp_Procedure();
            }
            BindAttachements();
        }

        private void BindAttachements()
        {
            #region Prerequiestes

            /// add colum "RequestID" lookup from Requests
            /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
            /// add column Group, single line of text

            #endregion Prerequiestes

            #region Edit Mode

            ProcedureProgramManagerAttachements.DocumentLibraryName = Utilities.Constants.ProcedureProgramManagerAttachements;
            ProcedureProgramManagerAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
            ProcedureProgramManagerAttachements.MaxSize = 7168000;//2MB
            ProcedureProgramManagerAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Attachments", (uint)LCID);
            ProcedureProgramManagerAttachements.Group = "ProcedureProgramManagerAttachements";// field name for example, shouldn't be used for more than one field per each control.
            ProcedureProgramManagerAttachements.RequestID = Convert.ToInt32(Page.Session["DisplayRequestId"].ToString());
            ProcedureProgramManagerAttachements.SupportedExtensions = "PNG,PDF,JPG";
            ProcedureProgramManagerAttachements.IsRequired = false;
            ProcedureProgramManagerAttachements.DeleteImageUrl = SPContext.Current.Site.Url + "/_catalogs/masterpage/MOEHE/common/img/DELETE.png";
            ProcedureProgramManagerAttachements.Enabled = true;

            ProcedureProgramManagerAttachements.Bind();

            #endregion Edit Mode
        }

        private void BinddrpEmployees()
        {
            List<SPUser> Araibcusers = HelperMethods.GetGroupUsers(Common.Utilities.Constants.ArabicProgEmployeeGroupName);
            //List<SPUser> Europeancusers = HelperMethods.GetGroupUsers(Common.Utilities.Constants.EuropeanProgEmployeeGroupName);
            foreach (SPUser user in Araibcusers)
            {
                drp_Employees.Items.Add(new ListItem(user.Name, user.LoginName));
            }
            //foreach (SPUser user in Europeancusers)
            //{
            //    drp_Employees.Items.Add(user.Name);
            //}
            drp_Employees.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
        }
        protected void drp_Procedure_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drp_Procedure.SelectedValue == "9")
            {
                drp_Employees.Visible = true;
                lbl_EmpName.Visible = true;
            }
            else
            {
                drp_Employees.Visible = false;
                lbl_EmpName.Visible = false;
            }
        }
        //temp change for the demo and deployment
        protected void btn_ApproveProc_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_ProgramManagerProc.btn_ApproveProc_Click");
                if (!IsRefresh)
                {
                    if (Page.Session["DisplayRequestId"] != null)
                    {
                        if (drp_Employees.SelectedIndex != 0)
                            BL.AllProcedures.AddProgramManagerProcedure(txt_Comments.Text, Page.Session["DisplayRequestId"].ToString(), drp_Procedure.SelectedItem.Text, drp_Employees.SelectedItem.Text);
                        else
                            BL.AllProcedures.AddProgramManagerProcedure(txt_Comments.Text, Page.Session["DisplayRequestId"].ToString(), drp_Procedure.SelectedItem.Text,string.Empty);

                        ProcedureProgramManagerAttachements.SaveAttachments();

                        switch (drp_Procedure.SelectedValue)
                        {
                            case "9":
                                BL.NewRequests.AssignTo(Page.Session["DisplayRequestId"].ToString(), drp_Employees.SelectedValue);
                                NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCEProgramEmployeeReview, txt_Comments.Text, "Requests", Page.Session["DisplayRequestId"].ToString());
                                Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.Requests, Utilities.Constants.RequestHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.UCEProgramEmployeeReview, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["DisplayRequestId"].ToString(), "No");

                                //Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.UCEProgramEmployeeReview, Page.Session["DisplayRequestId"].ToString(), "Arabian and Asian Program Employees".ToLower());
                                break;
                            case "8":
                                NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCEReceptionistMissingInformation, txt_Comments.Text, "Requests", Page.Session["DisplayRequestId"].ToString());
                                Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.Requests, Utilities.Constants.RequestHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.UCEReceptionistMissingInformation, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["DisplayRequestId"].ToString(), "No");

                                //Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.UCEReceptionistMissingInformation, Page.Session["DisplayRequestId"].ToString(), "Receptionists".ToLower());
                                break;
                            case "7":
                                BL.NewRequests.AssignTo(Page.Session["DisplayRequestId"].ToString(), drp_Employees.SelectedValue);
                                Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.Requests, Utilities.Constants.RequestHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.UCEProgramEmployeeMissingInformation, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["DisplayRequestId"].ToString(), "No");
                                //Thread.Sleep(3000);
                                BL.Request.UpdateRejection(new Entities.Request()
                                {
                                    ID = Convert.ToInt32(Page.Session["DisplayRequestId"]),
                                    RejectedFrom = SPContext.Current.Web.CurrentUser.Name,
                                    RejectionReason = "ReturnFromPM",
                                    RejectionDate = DateTime.Now
                                });
                                NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCEProgramEmployeeMissingInformation, txt_Comments.Text, "Requests", Page.Session["DisplayRequestId"].ToString());
                                //Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.UCEProgramEmployeeReview, Page.Session["DisplayRequestId"].ToString(), "Arabian and Asian Program Employees".ToLower());
                                break;
                        
                            // hidden till the demo and deployment for other roles that will not appear
                            ////case "9":
                            ////    NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.UCELegalEmployeePendingReview, txt_Comments.Text);
                            ////    //Utilities.BusinessHelper. UpdateRequestStatus((int)Common.Utilities.RequestStatus.UCELegalEmployeePendingReview, Page.Session["DisplayRequestId"].ToString(), "Legal Affairs Employees".ToLower());
                            ////    break;
                            //case "4":
                            //Utilities.BusinessHelper.UpdateRequestStatus((int)Common.Utilities.RequestStatus.uc, Page.Session["DisplayRequestId"].ToString(), "".ToLower());
                            //break;
                            default: break;
                        }
                        //if(drp_Procedure.SelectedValue=="7")

                       
                        //Response.Redirect((LCID == 1033 ? "/en" : "/ar") + "/Pages/ProgramManagerProcedureListing.aspx");
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ProgramManagerDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_ProgramManagerProc.btn_ApproveProc_Click");
            }
        }
        private void Binddrp_Procedure()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_ProgramManagerProc.Binddrpbtn");
                //drp_Procedure.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
                //drp_Procedure.Items.Insert(1, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ReturntoLastEmp", (uint)LCID), "0"));
                //drp_Procedure.Items.Insert(2, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ReturntoReceptionist", (uint)LCID), "1"));
                //drp_Procedure.Items.Insert(3, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseAnotherEmp", (uint)LCID), "2"));
                List<Entities.UCEEmployeeProcedures> uCEEmployeeProcedures = UCEEmployeeProcedures.GetAllByGroupName(Common.Utilities.Constants.ProgramManagerGroupName);
                SPListItem req = BL.Request.Reviewer_GetRequestItemByID(Convert.ToInt32(Page.Session["DisplayRequestId"].ToString()));
                SPFieldLookupValue StatusId = new SPFieldLookupValue((req["RequestStatusId"] != null) ? req["RequestStatusId"].ToString() : string.Empty);

                if (StatusId.LookupId == 6)
                    uCEEmployeeProcedures.RemoveAt(0);
                else
                    uCEEmployeeProcedures.RemoveAt(1);

                HelperMethods.BindDropDownList(ref drp_Procedure, uCEEmployeeProcedures, "ID", "ArabicTitle", "EnglishTitle", LCID);
                drp_Procedure.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), string.Empty));
                drp_Procedure.AppendDataBoundItems = true;

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Add_ProgramManagerProc.Binddrpbtn");
            }
        }
    }
}
