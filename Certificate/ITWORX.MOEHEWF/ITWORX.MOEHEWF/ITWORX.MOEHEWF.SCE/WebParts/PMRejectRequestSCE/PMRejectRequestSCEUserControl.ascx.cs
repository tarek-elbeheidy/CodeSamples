using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;
using ITWORX.MOEHE.Utilities;
using Microsoft.SharePoint.Linq;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using System.Collections.Generic;
using ITWorx.MOEHEWF.Nintex.Actions;
using System.Web;
using Microsoft.SharePoint.Utilities;

namespace ITWORX.MOEHEWF.SCE.WebParts.PMRejectRequestSCE
{
    public partial class PMRejectRequestSCEUserControl : UserControlBase
    {
        public int? RequestID
        {
            get
            {
                if (Request.QueryString["requestID"] != null)
                {
                    return Convert.ToInt32(Request.QueryString["requestID"]);
                }
                else if (ViewState["requestID"] != null)
                {
                    return Convert.ToInt32(ViewState["requestID"]);
                }
                return null;
            }
            set
            {
                ViewState["requestID"] = value;
            }
        }

        public string RequestTitle
        {
            get
            {
               if (ViewState["RequestTitle"] != null)
                {
                   return ViewState["RequestTitle"].ToString();
                }
                return null;
            }
            set
            {
                ViewState["RequestTitle"] = value;
            }
        }

        public string LoginName { get { return SPContext.Current.Web.CurrentUser.LoginName; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDropDown();
                GetRequestDetails();
            }
        }
        private void GetRequestDetails()
        {
            try
            {
                Logging.GetInstance().Debug("Enter method Entering method PMRejectRequestSCEUserControl.GetRequestDetails");

                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == RequestID).FirstOrDefault();

                    if (request != null)
                    {
                        txtRequestDate.Text = ((DateTime)request.SubmitDate).ToString("yyyy/MM/dd");
                        RequestTitle = request.Title;



                        if (!(request.RequestStatus.Id == (int)Common.Utilities.RequestStatus.SCEAcceptedRecommendation
                            || request.RequestStatus.Id == (int)Common.Utilities.RequestStatus.SCERejectedRecommendationSectionManager
                            || request.RequestStatus.Id == (int)Common.Utilities.RequestStatus.SCERejectedRecommendationDepartmentManager 
                            || request.RequestStatus.Id == (int)Common.Utilities.RequestStatus.SCEDepartmentManagerAcceptDecision
                            || request.RequestStatus.Id == (int)Common.Utilities.RequestStatus.SCEDepartmentManagerRejectDecision))
                        {
                            ddl_RejectionReasons.Enabled = false;
                            txtReturnComments.Enabled = false;
                            btnReturnRequets.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
               
                Logging.GetInstance().Debug("Exiting method Entering method PMRejectRequestSCEUserControl.GetRequestDetails");
            }
        }

        private void BindDropDown()
        {
            try
            {
                Logging.GetInstance().Debug("Enter method Entering method PMRejectRequestSCEUserControl.BindDropDown");
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {

                    var reasons = ctx.ReturnReasonsList.ScopeToFolder("", true).Where(c => c.Status == Status.Active).ToList();
                    HelperMethods.BindDropDownList(ref ddl_RejectionReasons, reasons, "ID", "TitleAr", "Title", LCID);
                    ddl_RejectionReasons.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ChooseValue", (uint)LCID), string.Empty));

                    ddl_RejectionReasons.AppendDataBoundItems = true;


                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method Entering method PMRejectRequestSCEUserControl.GetRequestDetails");
            }

        }

        private void ReturnRequest()
        {
            HttpContext backupContext = HttpContext.Current;

            try
            {
               
                SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                       
                        Logging.GetInstance().Debug("Entering method PMRejectRequestSCEUserControl.ReturnRequest");

                        web.AllowUnsafeUpdates = true;
                        string folderUrl = DateTime.Now.ToString("yyyy/MM/dd");
                      

                        if (HelperMethods.InGroup(Common.Utilities.Constants.SCESectionManagers))
                        {

                            SPList list = web.Lists[Utilities.Constants.SCEPMReturnRequests];
                            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                            {
                                //HttpContext.Current = null;

                                try
                                {
                                    SCEPMReturnRequestsListFieldsContentType currentrequest = ctx.SCEPMReturnRequestsList.ScopeToFolder("", true).Where(x => x.RequestIDId == RequestID).FirstOrDefault();
                                    var SMDecision = ctx.SCEPMDecisionList.ScopeToFolder("", true).Where(x => x.RequestIDId == RequestID);
                                    ctx.SCEPMDecisionList.DeleteAllOnSubmit(SMDecision);
                                    if (currentrequest == null)
                                    {
                                        SCEPMReturnRequestsListFieldsContentType returnedRequest = new SCEPMReturnRequestsListFieldsContentType()
                                        {
                                            OtherClarificationReason = txtReturnComments.Text,
                                            Comments = txtReturnComments.Text,
                                            Title = RequestTitle,
                                            RequestIDId = RequestID,
                                            Employee = SPContext.Current.Web.CurrentUser.Name,
                                            EmployeeId = SPContext.Current.Web.CurrentUser.ID,
                                            ReturnDate = DateTime.Now


                                        };
                                        var reasonDetails = ctx.ReturnReasonsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(ddl_RejectionReasons.SelectedItem.Value) && r.Status == Status.Active).SingleOrDefault();

                                        if (reasonDetails != null)
                                        {
                                            returnedRequest.ClarificationReasonId = reasonDetails.Id;
                                            returnedRequest.ClarificationReasonTitle = reasonDetails.TitleAr;
                                        }

                                        SPFolder folder = Common.Utilities.BusinessHelper.CreateFolderInternal(list, list.RootFolder, folderUrl);
                                        returnedRequest.Path = folder.Url;
                                        ctx.SCEPMReturnRequests.InsertOnSubmit(returnedRequest);
                                        ctx.SubmitChanges();

                                    }
                                    else
                                    {
                                        currentrequest.OtherClarificationReason = txtReturnComments.Text;
                                        currentrequest.Comments = txtReturnComments.Text;
                                        currentrequest.Title = RequestTitle;
                                        currentrequest.RequestIDId = RequestID;
                                        currentrequest.Employee = SPContext.Current.Web.CurrentUser.Name;
                                        currentrequest.EmployeeId = SPContext.Current.Web.CurrentUser.ID;
                                        currentrequest.ReturnDate = DateTime.Now;
                                        var reasonDetails = ctx.ReturnReasonsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(ddl_RejectionReasons.SelectedItem.Value) && r.Status == Status.Active).SingleOrDefault();

                                        if (reasonDetails != null)
                                        {
                                            currentrequest.ClarificationReasonId = reasonDetails.Id;
                                            currentrequest.ClarificationReasonTitle = reasonDetails.TitleAr;
                                        }
                                        ctx.SubmitChanges();

                                    }






                                }
                                catch (Exception ex)
                                {

                                    Logging.GetInstance().LogException(ex);


                                }
                               





                            }
                            //SCESectionManagerMissingInformation
                            NintexHelper.ContinueTask(ITWorx.MOEHEWF.Nintex.Utilities.Constants.SCESectionManagerMissingInformation, txtReturnComments.Text, Utilities.Constants.SCERequests, RequestID.ToString());
                            Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.SCERequests, Utilities.Constants.SCERequestHistory, LCID, (int)Common.Utilities.RequestStatus.SCESectionManagerMissingInformation, SPContext.Current.Web.CurrentUser.Name, string.Empty, RequestID.ToString(), "Yes");

                        }
                        else if (HelperMethods.InGroup(Common.Utilities.Constants.SCEDepartmentManagersGroupName))
                        {
                            SPList list = web.Lists[Utilities.Constants.SCEHMReturnRequests];

                            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                            {
                             
                                try
                                {
                                    var HMDecision = ctx.SCEHMDecisionList.ScopeToFolder("", true).Where(x => x.RequestIDId == RequestID);
                                    ctx.SCEHMDecisionList.DeleteAllOnSubmit(HMDecision);
                                    SCEHMReturnRequestsListFieldsContentType currentrequest = ctx.SCEHMReturnRequestsList.ScopeToFolder("", true).Where(x => x.RequestIDId == RequestID).FirstOrDefault();
                                    if (currentrequest == null)
                                    {
                                        SCEHMReturnRequestsListFieldsContentType returnedRequest = new SCEHMReturnRequestsListFieldsContentType()
                                        {
                                            OtherClarificationReason = txtReturnComments.Text,
                                            Comments = txtReturnComments.Text,
                                            Title = RequestTitle,
                                            RequestIDId = RequestID,
                                            Employee = SPContext.Current.Web.CurrentUser.Name,
                                            EmployeeId = SPContext.Current.Web.CurrentUser.ID,
                                            ReturnDate = DateTime.Now

                                        };
                                        var reasonDetails = ctx.ReturnReasonsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(ddl_RejectionReasons.SelectedItem.Value) && r.Status == Status.Active).SingleOrDefault();

                                        if (reasonDetails != null)
                                        {
                                            returnedRequest.ClarificationReasonId = reasonDetails.Id;
                                            returnedRequest.ClarificationReasonTitle = reasonDetails.TitleAr;
                                        }

                                        SPFolder folder = Common.Utilities.BusinessHelper.CreateFolderInternal(list, list.RootFolder, folderUrl);
                                        returnedRequest.Path = folder.Url;
                                        ctx.SCEHMReturnRequests.InsertOnSubmit(returnedRequest);
                                        ctx.SubmitChanges();
                                    }
                                    else
                                    {
                                        currentrequest.OtherClarificationReason = txtReturnComments.Text;
                                        currentrequest.Comments = txtReturnComments.Text;
                                        currentrequest.Title = RequestTitle;
                                        currentrequest.RequestIDId = RequestID;
                                        currentrequest.Employee = SPContext.Current.Web.CurrentUser.Name;
                                        currentrequest.EmployeeId = SPContext.Current.Web.CurrentUser.ID;
                                        currentrequest.ReturnDate = DateTime.Now;
                                        var reasonDetails = ctx.ReturnReasonsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(ddl_RejectionReasons.SelectedItem.Value) && r.Status == Status.Active).SingleOrDefault();

                                        if (reasonDetails != null)
                                        {
                                            currentrequest.ClarificationReasonId = reasonDetails.Id;
                                            currentrequest.ClarificationReasonTitle = reasonDetails.TitleAr;
                                        }
                                        ctx.SubmitChanges();

                                    }






                                }
                                catch (Exception ex)
                                {

                                    Logging.GetInstance().LogException(ex);


                                }
                              


                                //SCEDepartmentManagerMissingRecommendation
                                NintexHelper.ContinueTask(ITWorx.MOEHEWF.Nintex.Utilities.Constants.SCEDepartmentManagerMissingRecommendation, txtReturnComments.Text, Utilities.Constants.SCERequests, RequestID.ToString());
                                Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.SCERequests, Utilities.Constants.SCERequestHistory, LCID, (int)Common.Utilities.RequestStatus.SCEDepartmentManagerMissingRecommendation, SPContext.Current.Web.CurrentUser.Name, string.Empty, RequestID.ToString(), "Yes");

                            }



                        }


                        ///Both will save in request 
                        ///
                        using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                        {
                            SCERequestsListFieldsContentType currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(a => a.Id == RequestID).FirstOrDefault();
                            if (currentRequest != null)
                            {
                                currentRequest.ReturnDate = DateTime.Now;
                                currentRequest.ReturnedBy = SPContext.Current.Web.CurrentUser.Name;

                                currentRequest.ManagerReturnReason= ctx.ReturnReasonsList.ScopeToFolder("", true).Where(r => r.Id == Convert.ToInt32(ddl_RejectionReasons.SelectedItem.Value) && r.Status == Status.Active).SingleOrDefault();
                            }
                            ctx.SubmitChanges();

                        }
                    



                    }
                }
            });


               
              

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                // resetting the SPContext
                if (HttpContext.Current == null)
                {
                    HttpContext.Current = backupContext;
                }
                Logging.GetInstance().Debug("Exiting method Entering method PMRejectRequestSCEUserControl.ReturnRequest");
                if (HelperMethods.InGroup(Common.Utilities.Constants.SCESectionManagers))
                {
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCESectionManagerDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
                else if (HelperMethods.InGroup(Common.Utilities.Constants.SCEDepartmentManagersGroupName))
                {
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEDepartmentManagerDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }




        }

        protected void btnReturnRequets_Click(object sender, EventArgs e)
        {
            ReturnRequest();

        }
    }
}
