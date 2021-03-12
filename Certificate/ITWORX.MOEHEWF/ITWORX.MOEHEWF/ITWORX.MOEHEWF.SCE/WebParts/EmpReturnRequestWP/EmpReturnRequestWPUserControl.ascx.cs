using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.Common.Utilities;
using System.Web;
using ITWorx.MOEHEWF.Nintex.Actions;
using Microsoft.SharePoint.Utilities;
using ITWORX.MOEHE.Utilities;

namespace ITWORX.MOEHEWF.SCE.WebParts.EmpReturnRequestWP
{
    public partial class EmpReturnRequestWPUserControl : UserControlBase
    {
        public string RootWebURL
        {

            get
            {
                if (ViewState["RootWebURL"] == null)
                {
                    return SPContext.Current.Site.RootWeb.Url;
                }
                return (string)ViewState["RootWebURL"];
            }
            set { ViewState["RootWebURL"] = value; }

        }

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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadControls();
            }
        }
        private void LoadControls()
        {
            try
            {
                Logging.GetInstance().Debug("Enter EmpReturnRequestWPUserControl.LoadControls");


                using (SCEContextDataContext ctx = new SCEContextDataContext(RootWebURL))
                {
                    SCERequestsListFieldsContentType currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(a => a.Id == RequestID).SingleOrDefault();
                    if (currentRequest != null)
                    {
                        if (currentRequest.RequestStatus.Id == (int)RequestStatus.SCESubmitted
                            || currentRequest.RequestStatus.Id == (int)RequestStatus.SCEReturnForUnauthorized || currentRequest.RequestStatus.Id == (int)RequestStatus.SCEEquivalenceEmployeeReassign)
                        {
                            returnResquest.Visible = true;
                            dvRetunNote.Visible = false;
                            if (currentRequest.EmployeeAssignedTo!= SPContext.Current.Web.CurrentUser.LoginName)
                            {
                                txtReturnote.Enabled = false;
                                btnReturn.Enabled = false;
                                Label1.Text= HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "cantReturnRequest", (uint)LCID);
                            }
                            else
                            {
                                Label1.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "EmpReturnPS", (uint)LCID);
                                
                            }
                        }
                        else
                        {
                            returnResquest.Visible = false;
                            dvRetunNote.Visible = true;
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
                // resetting the SPContext

                Logging.GetInstance().Debug("Exit EmpReturnRequestWPUserControl.LoadControls");
            }


        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            HttpContext backupContext = HttpContext.Current;
            try
            {
                Logging.GetInstance().Debug("enter EmpReturnRequestWPUserControl.btnReturn_Click");


                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;
                            string folderUrl = DateTime.Now.ToString("yyyy/MM/dd");
                            SPList list = web.Lists[Utilities.Constants.SCEUnauthorizedReason];


                            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                            {
                                // HttpContext.Current = null;

                                SCEUnauthorizedReasonListFieldsContentType currentrequest = ctx.SCEUnauthorizedReasonList.ScopeToFolder("", true).Where(x => x.RequestIDId == RequestID).FirstOrDefault();
                                if (currentrequest == null)
                                {

                                    SCEUnauthorizedReasonListFieldsContentType returnedRequest = new SCEUnauthorizedReasonListFieldsContentType()
                                    {

                                        ReturnedReasons = txtReturnote.Text,
                                        RequestIDId = RequestID,
                                        Employee = SPContext.Current.Web.CurrentUser.Name,
                                        EmployeeId = SPContext.Current.Web.CurrentUser.ID,
                                        ReturnDate = DateTime.Now

                                    };


                                    SPFolder folder = Common.Utilities.BusinessHelper.CreateFolderInternal(list, list.RootFolder, folderUrl);
                                    returnedRequest.Path = folder.Url;
                                    ctx.SCEUnauthorizedReason.InsertOnSubmit(returnedRequest);
                                    ctx.SubmitChanges();
                                }
                                else
                                {
                                    //currentrequest.RequestIDId = RequestID;
                                    currentrequest.ReturnDate = DateTime.Now;
                                    currentrequest.ReturnedReasons = txtReturnote.Text;
                                    currentrequest.Employee = SPContext.Current.Web.CurrentUser.Name;
                                    currentrequest.EmployeeId = SPContext.Current.Web.CurrentUser.ID;
                                    ctx.SubmitChanges();
                                }



                            }
                        }
                    }
                    // SCEReturnForUnauthorized
                    NintexHelper.ContinueTask(ITWorx.MOEHEWF.Nintex.Utilities.Constants.SCEReturnForUnauthorized, string.Empty, Utilities.Constants.SCERequests, RequestID.ToString());
                    Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.SCERequests, Utilities.Constants.SCERequestHistory, LCID, (int)Common.Utilities.RequestStatus.SCEReturnForUnauthorized, SPContext.Current.Web.CurrentUser.Name, txtReturnote.Text, RequestID.ToString(), "Yes");
                });


            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                // resetting the SPContext
                if (HttpContext.Current == null)
                {
                    HttpContext.Current = backupContext;
                }

                Logging.GetInstance().Debug("Exit EmpReturnRequestWPUserControl.btnReturn_Click");

                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEEquivalenceEmployeesDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

            }


        }
    }
}
