using ITWorx.MOEHEWF.Nintex.Actions;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.SCE.WebParts.AddSCEStatement
{
    public partial class AddSCEStatementUserControl : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindDDL();
                txt_Date.Text = ExtensionMethods.QatarFormatedDate(DateTime.Now);
                txt_Sender.Text = SPContext.Current.Web.CurrentUser.Name;
            }
        }

        void bindDDL()
        {
            using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
            {
                var agancyList = ctx.StatementAgencyList;
                HelperMethods.BindDropDownList(ref ddl_Agency, agancyList, "GroupName", "TitleAr", "Title", LCID);
                ddl_Agency.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), "-1"));
            }
        }

        protected void btn_Back_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter AddSCEStatement.btn_Back_Click");
                int reqId = Convert.ToInt32(Request.QueryString["RequestId"]);
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEStatementListing.aspx?RequestId=" + reqId, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit AddSCEStatement.btn_Back_Click");
            }
        }

        protected void btn_Send_Click(object sender, EventArgs e)
        { 
            try
            {
                Logging.GetInstance().Debug("Enter AddSCEStatement.btn_Send_Click");
                if (Page.IsValid)
                {
                    if (Request.QueryString["RequestId"] != null)
                    {
                        int requestID = Convert.ToInt32(Request.QueryString["RequestId"]);
                        using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                        {
                            string folderUrl = DateTime.Now.ToString("yyyy/MM/dd");
                            SPList statementList = SPContext.Current.Site.RootWeb.Lists.TryGetList("SCEStatementsRequests");
                            SPFolder statementFolder = ITWORX.MOEHEWF.Common.Utilities.BusinessHelper.CreateFolderInternal(statementList, statementList.RootFolder, folderUrl);

                            ctx.SCEStatementsRequests.InsertOnSubmit(new SCEStatementsRequestsListFieldsContentType() {
                                StatementDate=DateTime.Now,
                                SenderId = SPContext.Current.Web.CurrentUser.ID,
                                StatementSubject=txt_Topic.Text,
                                RequiredStatement=txt_RequiredStatment.Text,
                                RequestID= ctx.SCERequestsList.ScopeToFolder("", true).Where(x => x.Id == requestID).FirstOrDefault(),
                                StatementAgency=ctx.StatementAgencyList.Where(x=>x.GroupName==ddl_Agency.SelectedValue).FirstOrDefault(),
                                Path= statementFolder.Url
                            });

                            var requetsItem = ctx.SCERequestsList.ScopeToFolder("", true).Where(x => x.Id == requestID).FirstOrDefault();
                            requetsItem.EmployeeForCultural = SPContext.Current.Web.CurrentUser.LoginName;
                           
                           
                        ctx.SubmitChanges();
                            NintexHelper.ContinueTask(ITWorx.MOEHEWF.Nintex.Utilities.Constants.SCECulturalMissionNeedsStatement, txt_Topic.Text, Utilities.Constants.SCERequests, requestID.ToString(),ddl_Agency.SelectedValue);
                            Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.SCERequests, Utilities.Constants.SCERequestHistory, LCID, (int)Common.Utilities.RequestStatus.SCECulturalMissionNeedsStatement, SPContext.Current.Web.CurrentUser.Name, string.Empty, requestID.ToString(), "Yes");
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
                Logging.GetInstance().Debug("Exit AddSCEStatement.btn_Send_Click");
                SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/SCEEquivalenceEmployeesDashboard.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
        }
    }
}
