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
    public partial class NewRequests : Utilities.UserControlBase
    {
        #region Public Properties
        public string SPGroupName { get; set; }
       
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            grd_SCENewRequests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "DashboardPageSize"));
            BindGrid();
        }
        private void BindGrid()
        {
            try
            {
                Logging.GetInstance().Debug("Enter NewRequests.BindGrid");
               
               

                int delayedDays = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SCEDelayedDays"));
                List<Entities.NewRequests> Requests = BL.NewRequests.GetAllNewRequests(BL.NewRequests.GetNewRequestsQuery( SPGroupName, delayedDays),LCID).ToList();
              
                HelperMethods.BindGridView(grd_SCENewRequests, Requests);
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

                Logging.GetInstance().Debug("Exit NewRequests.BindGrid");
            }
        }

        protected void grd_SCENewRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter NewRequests.grd_SCENewRequests_PageIndexChanging");
            try
            {
                grd_SCENewRequests.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit NewRequests.grd_SCENewRequests_PageIndexChanging");
            }
        }

        protected void grd_SCENewRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter NewRequests.grd_SCENewRequests_RowDataBound");
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField IsClosed = (HiddenField)e.Row.FindControl("hdn_IsClosed");
                    HiddenField AssignedTo = (HiddenField)e.Row.FindControl("hdn_AssignedTo");
                    HiddenField StatusID = (HiddenField)e.Row.FindControl("hdn_RequestStatusId");
                    LinkButton btnEdit = (LinkButton)e.Row.FindControl("lnk_Edit");
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit NewRequests.grd_SCENewRequests_RowDataBound");
            }
        }

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter NewRequests.lnk_Edit_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_RequestID = (HiddenField)gvr.FindControl("hdn_ID");
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdn_RequestStatusId");
                HiddenField hdn_QID = (HiddenField)gvr.FindControl("hdn_QID");
                Common.Entities.RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                string editLink = string.Empty;
                if (requestStatus.CanReviewerEditRequest)
                    editLink = requestStatus.ReviewerTargetPageURL + "?RequestId=" + hdn_RequestID.Value;

            
                SPUtility.Redirect(SPContext.Current.Web.Url + editLink, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit NewRequests.lnk_Edit_Click");
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            int delayedDays = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SCEDelayedDays"));
            List<Entities.NewRequests> newRequests = BL.NewRequests.GetAllNewRequests(BL.NewRequests.GetNewRequestsQuery(SPGroupName, delayedDays), LCID).ToList();


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
            workSheet.Cells[1, 7].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "LastGrade", (uint)LCID);
            workSheet.Cells[1, 8].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ApplicantMobileNumber", (uint)LCID);

            int recordIndex = 2;
            foreach (var item in newRequests)
            {
                workSheet.Cells[recordIndex, 1].Value = item.RequestNumber;
                workSheet.Cells[recordIndex, 2].Value = item.RecievedDate.ToShortDateString();
                workSheet.Cells[recordIndex, 3].Value = item.QatariID;
                workSheet.Cells[recordIndex, 4].Value = item.StudentNameAccToCert;
                workSheet.Cells[recordIndex, 5].Value = item.Nationality;
                workSheet.Cells[recordIndex, 6].Value = item.CertificateResource;
                workSheet.Cells[recordIndex, 7].Value = item.SchoolLastGrade;
                workSheet.Cells[recordIndex, 8].Value = item.ApplicantMobileNumber;
                recordIndex++;
            }
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            string excelName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SCENewRequests", (uint)LCID);

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

        public string ConvertDateCalendar(DateTime DateConv, string Calendar, string DateLangCulture)
        {
            System.Globalization.DateTimeFormatInfo DTFormat;
            DateLangCulture = DateLangCulture.ToLower();
            /// We can't have the hijri date writen in English. We will get a runtime error - LAITH - 11/13/2005 1:01:45 PM -
            if (Calendar == "Hijri" && DateLangCulture.StartsWith("en-"))
            {
                DateLangCulture = "ar-sa";
            }
            /// Set the date time format to the given culture - LAITH - 11/13/2005 1:04:22 PM -
            DTFormat = new System.Globalization.CultureInfo(DateLangCulture, false).DateTimeFormat;
            /// Set the calendar property of the date time format to the given calendar - LAITH - 11/13/2005 1:04:52 PM -
            switch (Calendar)
            {
                case "Hijri":
                    DTFormat.Calendar = new System.Globalization.HijriCalendar();
                    break;
                case "Gregorian":
                    DTFormat.Calendar = new System.Globalization.GregorianCalendar();
                    break;
                default:
                    return "";
            }
            /// We format the date structure to whatever we want - LAITH - 11/13/2005 1:05:39 PM -
            DTFormat.ShortDatePattern = "dd/MM/yyyy";
            if (LCID == (int)Language.English)
            {
                return (DateConv.Date.ToString("dd/MM/yyyy", DTFormat));
            }
            else
            {
                return (DateConv.Date.ToString("yyyy/MM/dd", DTFormat));
            }
        }

        public string ToArabicDigits(string input)
        {
            return input.Replace('0', '\u0660')
                    .Replace('1', '\u0661')
                    .Replace('2', '\u0662')
                    .Replace('3', '\u0663')
                    .Replace('4', '\u0664')
                    .Replace('5', '\u0665')
                    .Replace('6', '\u0666')
                    .Replace('7', '\u0667')
                    .Replace('8', '\u0668')
                    .Replace('9', '\u0669');
        }
    }
}
