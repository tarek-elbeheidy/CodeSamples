using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Nintex.Workflow.HumanApproval;
using ITWORX.MOEHEWF.UCE.Utilities;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class Add_ProcedureProgramManager : Utilities.UserControlBase
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
                BinddrpEmployees();
                Binddrp_Procedure();
            }
        }

        private void BinddrpEmployees()
        {
            List<SPUser> Araibcusers = HelperMethods.GetGroupUsers(Common.Utilities.Constants.ArabicProgEmployeeGroupName);
            List<SPUser> Europeancusers = HelperMethods.GetGroupUsers(Common.Utilities.Constants.EuropeanProgEmployeeGroupName);
            foreach (SPUser user in Araibcusers)
            {
                drp_Employees.Items.Add(user.Name);
            }
            foreach (SPUser user in Europeancusers)
            {
                drp_Employees.Items.Add(user.Name);
            }
            drp_Employees.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));
        }

        protected void drp_Procedure_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drp_Procedure.SelectedValue == "8")
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
                    if (Page.Session["PADisplayRequestId"] != null)
                    {
                        if (drp_Employees.SelectedIndex != 0)
                            BL.AllProcedures.AddProgramManagerProcedure(txt_Comments.Text, Page.Session["PADisplayRequestId"].ToString(), drp_Procedure.SelectedItem.Text, drp_Employees.SelectedItem.Text);
                        else
                            BL.AllProcedures.AddProgramManagerProcedure(txt_Comments.Text, Page.Session["PADisplayRequestId"].ToString(), drp_Procedure.SelectedItem.Text, string.Empty);

                        switch (drp_Procedure.SelectedItem.Value)
                        {
                            case "1":
                                NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAProgramManagerAcceptance, txt_Comments.Text, "PARequests", Page.Session["PADisplayRequestId"].ToString());
                                Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests, Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PAProgramManagerAcceptance, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["PADisplayRequestId"].ToString(), "No");
                                break;
                            case "2":
                                NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAProgramManagerReject, txt_Comments.Text, "PARequests", Page.Session["PADisplayRequestId"].ToString());
                                Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests, Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PAProgramManagerReject, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["PADisplayRequestId"].ToString(), "No");
                                break;
                            case "3":
                                NintexHelper.ContinueTask(CurrentContext, Utilities.Constants.PAProgramEmployeeMissingInformation, txt_Comments.Text, "PARequests", Page.Session["PADisplayRequestId"].ToString());
                                Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.PARequests, Utilities.Constants.RequestPAHistoricalRecords, LCID, (int)Common.Utilities.RequestStatus.PAProgramEmployeeMissingInformation, SPContext.Current.Web.CurrentUser.Name, string.Empty, Page.Session["PADisplayRequestId"].ToString(), "No");
                                break;
                            default: break; 
                        }

                        //Response.Redirect((LCID == 1033 ? "/en" : "/ar") + "/Pages/PAProgramManagerProcedureListing.aspx");
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAProgramManagerProcedureListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
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
                drp_Procedure.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1")); 
                drp_Procedure.Items.Insert(1, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Approved", (uint)LCID), "1"));
                drp_Procedure.Items.Insert(2, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "Rejected", (uint)LCID), "2"));
                drp_Procedure.Items.Insert(3, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "returnToEmployee", (uint)LCID), "3")); 
                //List<Entities.PAEmployeeProcedures> PAEmployeeProcedures =

                //    BL.PAEmployeeProcedures.GetAllByGroupName(Common.Utilities.Constants.ProgramManagerGroupName);

                //HelperMethods.BindDropDownList(ref drp_Procedure, PAEmployeeProcedures, "ID", "ArabicTitle", "EnglishTitle", LCID);

                //drp_Procedure.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), string.Empty));

                //drp_Procedure.AppendDataBoundItems = true;
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