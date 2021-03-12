using ITWorx.MOEHEWF.Nintex.Actions;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.SCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE
{
   
    public partial class ReassignRequests : UserControlBase
    {
        #region Public Properties
        public string SPGroupName { get; set; }
       // public NintexContext CurrentContext { get; set; }
        //public bool IsTaskComplete { get; set; }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //var hdnCtl = Page.Request.Form.Get("__EventTriggerControlIdReassign");
            //if (string.IsNullOrEmpty(hdnCtl))
            //{
            grd_ReassignRequests.PageSize =int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "DashboardPageSize"));
                lbl_SuccessMsg.Visible = false;
                BindEquivalenceEmployees();
                BindGrid();
           // }
        }
        private void BindEquivalenceEmployees()
        {
            try
            {
                Logging.GetInstance().Debug("Enter ReassignRequests.BindEquivalenceEmployees");

                drp_AssignTo.Items.Clear();
            List<SPUser> ReassignUsers = HelperMethods.GetGroupUsers(Common.Utilities.Constants.SCEEquivalenceEmployeesGroupName);
            foreach (SPUser user in ReassignUsers)
            {
                drp_AssignTo.Items.Add(new ListItem(user.Name, user.LoginName));
            }

            drp_AssignTo.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Choose", (uint)LCID), string.Empty));
            EmpAssignTo.Visible = true;
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit ReassignRequests.BindEquivalenceEmployees");
            }
        }

        private void BindGrid()
        {
            try
            {
                Logging.GetInstance().Debug("Enter ReassignRequests.BindGrid");

                int delayedDays = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SCEDelayedDays"));
                List<Entities.ReassignRequests> Requests = BL.ReassignRequests.GetAllReassignRequests(BL.ReassignRequests.GetReassignRequestsQuery( delayedDays), LCID).ToList();


                
                HelperMethods.BindGridView(grd_ReassignRequests, Requests);
                if (Requests.Count > 0)
                {
                    lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "NoOfRequests", (uint)LCID) + " " + Requests.Count;
                    lbl_NoOfRequests.Visible = true;
                }
                else
                    lbl_NoOfRequests.Visible = false;
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit ReassignRequests.BindGrid");
            }

        }
        private void PersistRowIndex(int index)
        {
            if (!SelectedRequestsIndex.Exists(i => i == index))
            {
                SelectedRequestsIndex.Add(index);
            }
        }
        private void RemoveRowIndex(int index)
        {
            SelectedRequestsIndex.Remove(index);
        }

        private void RePopulateCheckBoxes()
        {
            foreach (GridViewRow row in grd_ReassignRequests.Rows)
            {
                var chkBox = row.FindControl("chkAssign") as CheckBox;
                HiddenField reqID = (row.FindControl("hdn_ID") as HiddenField);

                if (SelectedRequestsIndex != null)
                {
                    if (SelectedRequestsIndex.Exists(i => i == Convert.ToInt32(reqID.Value)))
                    {
                        chkBox.Checked = true;
                    }
                }
            }
        }
        protected void grd_ReassignRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter ReassignRequests.grd_ReassignRequests_PageIndexChanging");
            try
            {
                foreach (GridViewRow row in grd_ReassignRequests.Rows)
                {
                    var chkBox = row.FindControl("chkAssign") as CheckBox;
                    HiddenField reqID = (row.FindControl("hdn_ID") as HiddenField);
                    if (chkBox.Checked)
                    {
                        PersistRowIndex(Convert.ToInt32(reqID.Value));
                    }
                    else
                    {
                        RemoveRowIndex(Convert.ToInt32(reqID.Value));
                    }
                }
                grd_ReassignRequests.PageIndex = e.NewPageIndex;
                BindGrid();
                RePopulateCheckBoxes(); 
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit ReassignRequests.grd_ReassignRequests_PageIndexChanging");
            }
        }

      
        protected void lnk_View_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter ReassignRequests.lnk_View_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_RequestID = (HiddenField)gvr.FindControl("hdn_ID");
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdn_RequestStatusId");
                Common.Entities.RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
               string viewLink = requestStatus.ReviewerTargetPageURL + "?RequestId=" + hdn_RequestID.Value;

                SPUtility.Redirect(SPContext.Current.Web.Url + viewLink, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit ReassignRequests.lnk_View_Click");
            }

        }
          
        private List<Int32> SelectedRequestsIndex
        {
            get
            {
                if (ViewState["SELECTED_Requests_INDEX"] == null)
                {
                    ViewState["SELECTED_Requests_INDEX"] = new List<Int32>();
                }

                return (List<Int32>)ViewState["SELECTED_Requests_INDEX"];
            }
        }
        protected void lnk_AssignTo_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> reqIDstr = new List<string>();
                Logging.GetInstance().Debug("Entering method ReassignRequests.lnk_AssignTo_Click");
                //if (!IsRefresh)
                //{
                    foreach (GridViewRow row in grd_ReassignRequests.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.FindControl("chkAssign") as CheckBox);
                            if (chkRow.Checked)
                            {
                                HiddenField reqID = (row.FindControl("hdn_ID") as HiddenField);
                                reqIDstr.Add(reqID.Value);
                            }
                        }
                    }
                    foreach (string id in reqIDstr)
                    {
                        if (!SelectedRequestsIndex.Exists(i => i == int.Parse(id)))
                        {
                            SelectedRequestsIndex.Add(int.Parse(id));
                        }
                    }
                    Page.Validate();
                    if (Page.IsValid)
                    {
                        foreach (var id in reqIDstr)
                        {
                            if (!string.IsNullOrEmpty(drp_AssignTo.SelectedValue))
                            {
                                NintexHelper.ContinueTask(ITWorx.MOEHEWF.Nintex.Utilities.Constants.SCEEquivalenceEmployeeReassign, string.Empty, Utilities.Constants.SCERequests, id, drp_AssignTo.SelectedValue);
                            Common.BL.HistoricalRecords.AddHistoricalRecords(Utilities.Constants.SCERequests, Utilities.Constants.SCERequestHistory, LCID, (int)Common.Utilities.RequestStatus.SCEEquivalenceEmployeeReassign, SPContext.Current.Web.CurrentUser.Name, string.Empty, id, "Yes");

                        }
                        }
                        if (!string.IsNullOrEmpty(drp_AssignTo.SelectedValue))
                            lbl_SuccessMsg.Visible = true;
                    }
                //}
                BindGrid(); 
            
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }

            finally
            {
                Logging.GetInstance().Debug("Exiting method ReassignRequests.lnk_AssignTo_Click");
            }
        }

        protected void custAssignCheck_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (SelectedRequestsIndex==null || SelectedRequestsIndex.Count==0)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            int delayedDays = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SCEDelayedDays"));
            List<Entities.ReassignRequests> reassignRequests = BL.ReassignRequests.GetAllReassignRequests(BL.ReassignRequests.GetReassignRequestsQuery(delayedDays), LCID).ToList();


            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Applicants Data");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;


            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;
            workSheet.Cells[1, 1].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "RequestNumber", (uint)LCID);
            workSheet.Cells[1, 2].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "RecieveRequestDate", (uint)LCID);
            workSheet.Cells[1, 3].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "CertificateHolderQatarID", (uint)LCID);
            workSheet.Cells[1, 4].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ApplicantNameAccToCert", (uint)LCID);
            workSheet.Cells[1, 5].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Nationality", (uint)LCID);
            workSheet.Cells[1, 6].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "CertificateSource", (uint)LCID);
            workSheet.Cells[1, 7].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ReturnedFrom", (uint)LCID);
            workSheet.Cells[1, 8].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ReturnReason", (uint)LCID);

            int recordIndex = 2;
            foreach (var item in reassignRequests)
            {
                workSheet.Cells[recordIndex, 1].Value = item.RequestNumber;
                workSheet.Cells[recordIndex, 2].Value = item.RecievedDate.ToShortDateString();
                workSheet.Cells[recordIndex, 3].Value = item.CertificateHolderQatarID;
                workSheet.Cells[recordIndex, 4].Value = item.StudentNameAccToCert;
                workSheet.Cells[recordIndex, 5].Value = item.Nationality;
                workSheet.Cells[recordIndex, 6].Value = item.CertificateResource;
                workSheet.Cells[recordIndex, 7].Value = item.ReturnedFrom;
                workSheet.Cells[recordIndex, 8].Value = item.ReturnReason;
                recordIndex++;
            }
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            string excelName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SCEReassignRequests", (uint)LCID);

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }


        }
    }
}
