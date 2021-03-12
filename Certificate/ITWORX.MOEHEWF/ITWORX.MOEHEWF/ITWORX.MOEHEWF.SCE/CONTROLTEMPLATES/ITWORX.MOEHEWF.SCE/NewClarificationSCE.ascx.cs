using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHEWF.SCE.BL;
using ITWORX.MOEHEWF.SCE.Entities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Linq;
using Microsoft.SharePoint.Utilities;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWorx.MOEHEWF.Nintex.Actions;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE
{
    
    public partial class NewClarificationSCE : UserControlBase
    {
        private int RequestId { get { return Convert.ToInt32(Request.QueryString["RequestId"]); } }
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DDLWithTXTWithNoPostback ddlClarificationReason;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Exiting method NewClarificationSCE.Page_Load");
                if (!Page.IsPostBack)
                {
                    BindData();
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method NewClarificationSCE.Page_Load");
            }
        }
        private void BindData()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method NewClarificationSCE.BindData");
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    txt_RequestDate.Text = ExtensionMethods.QatarFormatedDate( DateTime.Now)+ExtensionMethods.QatarFormatedDateReturnTime(DateTime.Now);
                    txt_Requester.Text = SPContext.Current.Web.CurrentUser.Name.ToString();
                   
                    using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                    {
                        List<ClarificationReasonsLookupsCT> reasons = ctx.ClarificationReasonsList.ScopeToFolder("", true).Where(c => c.Status == Status.Active).ToList();
                        //HelperMethods.BindDropDownList(ref ddl_Reasons, reasons, "ID", "TitleAr", "Title", LCID);
                        //ddl_Reasons.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ChooseValue", (uint)LCID), string.Empty));

                        //ddl_Reasons.AppendDataBoundItems = true;



                        ddlClarificationReason.IsRequired = true;
                        ddlClarificationReason.DataSource = reasons;
                        ddlClarificationReason.DataValueField = "ID";
                        ddlClarificationReason.DataENTextField = "Title";
                        ddlClarificationReason.DataARTextField = "TitleAr";
                        ddlClarificationReason.ValidationGroup = "NewClar";
                        ddlClarificationReason.Title = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "RequestReason", (uint)LCID);
                        ddlClarificationReason.ValidationMSG = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Required", (uint)LCID);
                        ddlClarificationReason.BingDDL();
                    }
                    
                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method NewClarificationSCE.BindData");
            }
        }

        protected void btn_Send_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method NewClarificationSCE.btn_Send_Click");
              //  int requestId = Convert.ToInt32(Request.QueryString["RequestId"]);
                string neededData = txt_NeededData.Text.ToString();
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                              Logging.GetInstance().Debug("Enter method NewClarificationSCE.btn_Send_Click");
                                web.AllowUnsafeUpdates = true;
                                string folderUrl = DateTime.Now.ToString("yyyy/MM/dd");
                                SPList list = web.Lists[Utilities.Constants.SCEClarificationsRequests];
                                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                                {
                                    SCEClarificationsRequestsListFieldsContentType clarRequest = new SCEClarificationsRequestsListFieldsContentType()
                                    {
                                        ClarificationDate = Convert.ToDateTime(txt_RequestDate.Text.ToString()),
                                        RequiredClarification = neededData,
                                        SenderId = SPContext.Current.Web.CurrentUser.ID,
                                        RequestIDId = RequestId,
                                    };
                                if (ddlClarificationReason.SelectedValue == "-2")
                                {
                                    clarRequest.OtherClarificationReason = ddlClarificationReason.OtherValue;
                                }
                                else
                                {
                                    EntityList<Item> reasonsList = ctx.GetList<Item>(Utilities.Constants.ClarificationReasons);
                                    var reason = (from r in reasonsList.ToList()
                                                  where r.Id == Convert.ToInt32(ddlClarificationReason.SelectedValue)
                                                  select new { r }).SingleOrDefault();
                                    clarRequest.ClarificationReason = (Item)reason.r;
                                }
                                  

                                    SPFolder folder = Common.Utilities.BusinessHelper.CreateFolderInternal(list, list.RootFolder, folderUrl);
                                    clarRequest.Path = folder.Url;
                                    ctx.SCEClarificationsRequests.InsertOnSubmit(clarRequest);

                                    SCERequestsListFieldsContentType currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(a => a.Id == RequestId).FirstOrDefault();
                                    if (currentRequest != null)
                                    { 
                                        currentRequest.ReturnedBy = SPContext.Current.Web.CurrentUser.LoginName; 
                                    }


                                ctx.SubmitChanges();

                                NintexHelper.ContinueTask(ITWorx.MOEHEWF.Nintex.Utilities.Constants.SCEEmployeeNeedsClarification, neededData, Utilities.Constants.SCERequests, RequestId.ToString());

                                Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.SCERequests, Utilities.Constants.SCERequestHistory, LCID, (int)Common.Utilities.RequestStatus.SCEEmployeeNeedsClarification, SPContext.Current.Web.CurrentUser.Name, string.Empty, RequestId.ToString(), "Yes");
                                //Send SMS
                                SendSMS((int)Common.Utilities.RequestStatus.SCEEmployeeNeedsClarification);
                            }

                                int reqId = Convert.ToInt32(Request.QueryString["RequestId"]);
                                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEEquivalenceEmployeesDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);           
                        }

                    }

                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method NewClarificationSCE.btn_Send_Click");
            }
        }

        protected void btn_Back_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter NewClarificationSCE.btn_Back_Click");
                int reqId = Convert.ToInt32(Request.QueryString["RequestId"]);
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ViewRequest.aspx?RequestId=" + reqId, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewClarificationSCE.btn_Back_Click");
            }
        }


        private void SendSMS(int status)
        {

            try
            {
                Logging.GetInstance().Debug("Entering method NewClarificationSCE.SendSMS");
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {

                    SCERequestsListFieldsContentType currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(a => a.Id == RequestId).SingleOrDefault();

                    SCENotificationsListFieldsContentType notfications = ctx.SCENotificationsList.ScopeToFolder("", true).Where(y => y.RequestStatusIdId == status && y.Type == NotifcationType.SMS).SingleOrDefault();


                    if (currentRequest != null && notfications != null)
                    {
                        string smsBody = string.Format(notfications.Body, currentRequest.RequestNumber, currentRequest.RequestNumber);


                        SCENotifcations.SendSMS(currentRequest.MobileNumber, smsBody);

                    }

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("exit method NewClarificationSCE.SendSMS");
            }



        }
    }
}
