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
    public partial class ExceptionRequests : Utilities.UserControlBase
    {
        #region Public Properties
        public string SPGroupName { get; set; }

        #endregion

        protected string DeleteConfirmation = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LCID == (uint)Language.English)
            {
                hypNewRequest.NavigateUrl = SPContext.Current.Site.Url + "/en/Reviewer/Pages/NewSCERequest.aspx";
            }
            else
            {
                hypNewRequest.NavigateUrl = SPContext.Current.Site.Url + "/ar/Reviewer/Pages/NewSCERequest.aspx";
            }
            grd_SCExceptionRequests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "DashboardPageSize"));
            BindGrid();
            DeleteConfirmation = HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "DeleteConfirmation", (uint)LCID);
        }

        private void BindGrid()
        {
            try
            {
                Logging.GetInstance().Debug("Enter ExceptionRequests.BindGrid");


                List<Entities.ExceptionRequests> Requests = BL.ExceptionRequests.GetAllExceptionRequests(BL.ExceptionRequests.GetExceptionRequestsQuery(SPGroupName), LCID).ToList();// BL.NewRequests.GetAllNewRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.NewRequests.GetNewQueryPerRole(SPGroupName), "Or", true) + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();

                HelperMethods.BindGridView(grd_SCExceptionRequests, Requests);
                if (Requests.Count > 0)
                {
                    lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "NoOfRequests", (uint)LCID) + Requests.Count;
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

                Logging.GetInstance().Debug("Exit ExceptionRequests.BindGrid");
            }
        }



        protected void grd_SCExceptionRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter ExceptionRequests.grd_SCENewRequests_PageIndexChanging");
            try
            {
                grd_SCExceptionRequests.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit ExceptionRequests.grd_SCENewRequests_PageIndexChanging");
            }
        }

        protected void grd_SCExceptionRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter ExceptionRequests.grd_SCENewRequests_RowDataBound");



                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // LinkButton deleteLink = (LinkButton)e.Row.FindControl("lnkDelete");
                    LinkButton editLink = (LinkButton)e.Row.FindControl("lnk_Edit");
                    //LinkButton displayLink = (LinkButton)e.Row.FindControl("lnkDisplay");
                    //LinkButton lnk_FinalDecisionFile = (LinkButton)e.Row.FindControl("lnk_FinalDecisionFile");
                    //Label lblRequestStatus = (Label)e.Row.FindControl("lblRequestStatus");
                    HiddenField hdnRequestStatusId = (HiddenField)e.Row.FindControl("hdn_RequestStatusId");
                    Common.Entities.RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                    HiddenField hdnDelayedDays = (HiddenField)e.Row.FindControl("hdn_DelayedDays");
                    Literal litExc = (Literal)e.Row.FindControl("litExc");
                    LinkButton deleteLink = (LinkButton)e.Row.FindControl("lnkDelete");


                    if (int.Parse(hdnRequestStatusId.Value) == (int)Common.Utilities.RequestStatus.SCEDraft
                        ||requestStatus.CanReviewerEditRequest)
                    {
                        editLink.Visible = true;
                       
                      //  displayLink.Visible = false;
                    }
                    else
                    {
                        editLink.Visible = false;
                        
                        //  displayLink.Visible = true;
                    }
                    if (int.Parse(hdnRequestStatusId.Value) == (int)Common.Utilities.RequestStatus.SCEDraft)
                    {
                        deleteLink.Visible = true;
                    }
                    else
                    {
                        deleteLink.Visible = false;
                    }
                    int delayedDays = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SCEDelayedDays"));
                    if (!string.IsNullOrEmpty(hdnDelayedDays.Value) && int.Parse(hdnDelayedDays.Value) > delayedDays
                        && int.Parse(hdnRequestStatusId.Value) != (int)Common.Utilities.RequestStatus.SCEEmployeeNeedsClarification)
                    {
                        litExc.Visible = true;
                    }
                    else
                    {
                        litExc.Visible = false;
                    }




                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ExceptionRequests.grd_SCENewRequests_RowDataBound");
            }
        }

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {

            Logging.GetInstance().Debug("Enter ExceptionRequests.lnk_Edit_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_RequestID = (HiddenField)gvr.FindControl("hdn_ID");
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdn_RequestStatusId");
                HiddenField hdn_QID = (HiddenField)gvr.FindControl("hdn_QID");
                Common.Entities.RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                string editLink = string.Empty;

                if (requestStatus.CanReviewerEditRequest ||int.Parse(hdnRequestStatusId.Value) == (int)Common.Utilities.RequestStatus.SCEDraft)
                    editLink = requestStatus.TargetPageUrl + "?RequestId=" + hdn_RequestID.Value;


                SPUtility.Redirect(SPContext.Current.Web.Url + editLink, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit ExceptionRequests.lnk_Edit_Click");
            }
        }

        //protected void lnkDisplay_Click(object sender, EventArgs e)
        //{
        //    LinkButton lnkButton = (LinkButton)sender;
        //    GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
        //    HiddenField hdn_RequestID = (HiddenField)gvr.FindControl("hdn_ID");
        //    HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdn_RequestStatusId");
        //    HiddenField hdn_QID = (HiddenField)gvr.FindControl("hdn_QID");
        //    Common.Entities.RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
        //    //string displayLink = string.Empty;
        //    //if (requestStatus.CanReviewerEditRequest)
        //    string displayLink = requestStatus.TargetPageUrl + "?RequestId=" + hdn_RequestID.Value;


        //    SPUtility.Redirect(SPContext.Current.Web.Url + displayLink, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
        //}

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {

            List<Entities.ExceptionRequests> excepRequests = BL.ExceptionRequests.GetAllExceptionRequests(BL.ExceptionRequests.GetExceptionRequestsQuery(SPGroupName), LCID).ToList();



            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Applicants Data");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;


            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;
            workSheet.Cells[1, 1].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "RequestNumber", (uint)LCID);
            workSheet.Cells[1, 2].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "RecieveRequestDate", (uint)LCID);
            workSheet.Cells[1, 3].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ExcepApplicantName", (uint)LCID);
            workSheet.Cells[1, 4].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Nationality", (uint)LCID);
            workSheet.Cells[1, 5].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "CertificateSource", (uint)LCID);
            workSheet.Cells[1, 6].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "LastGrade", (uint)LCID);
            workSheet.Cells[1, 7].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "RequestStatus", (uint)LCID);


            int recordIndex = 2;
            foreach (var item in excepRequests)
            {
                workSheet.Cells[recordIndex, 1].Value = item.RequestNumber;
                workSheet.Cells[recordIndex, 2].Value = item.RecievedDate.ToShortDateString();
                workSheet.Cells[recordIndex, 3].Value = item.ApplicantName;
                workSheet.Cells[recordIndex, 4].Value = item.Nationality;
                workSheet.Cells[recordIndex, 5].Value = item.CertificateResource;
                workSheet.Cells[recordIndex, 6].Value = item.SchoolLastGrade;
                workSheet.Cells[recordIndex, 7].Value = item.RequestStatus;

                recordIndex++;
            }
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            string excelName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SCEExceptionRequests", (uint)LCID);

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

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter ExceptionRequests.lnkDelete_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdnRequestId = (HiddenField)gvr.FindControl("hdn_ID");
                int requestId = int.Parse(hdnRequestId.Value);
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == requestId ).FirstOrDefault();
                    if (request !=null)
                    {
                        ctx.SCERequestsList.DeleteOnSubmit(request);

                    }
                    SCERequestsAttachmentsListFieldsContentType requestAttachment = ctx.SCERequestsAttachmentsList.ScopeToFolder("", true).Where(r => r.RequestIDId == requestId).FirstOrDefault();
                    if (requestAttachment!=null)
                    {
                        ctx.SCERequestsAttachmentsList.DeleteOnSubmit(requestAttachment);
                    }

                     ctx.SubmitChanges();
                }
                BindGrid();


              
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ExceptionRequests.lnkDelete_Click");
            }
        }
    }
}
