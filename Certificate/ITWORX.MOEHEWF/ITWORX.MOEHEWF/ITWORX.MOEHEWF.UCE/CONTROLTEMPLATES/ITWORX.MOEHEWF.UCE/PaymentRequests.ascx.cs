//using ClosedXML.Excel;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class PaymentRequests : UserControlBase
    {
        #region Public Properties

        public string SPGroupName { get; set; }

        #endregion Public Properties

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HelperMethods.InGroup(Common.Utilities.Constants.FinancialManagementGroupName))
            {
                grd_Requests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SearchPageSize"));
                
            }
            else
                divCont.Visible = false;
        }

        private List<Entities.Payments> GetAllPaymentRequestsData()
        {
            List<Entities.Payments> srchRequests = new List<Entities.Payments>();
            try
            {
                Logging.GetInstance().Debug("Entering method SearchRequests.GetAllPaymentRequestsData");
                List<string> objColumns = new List<string>();

                if (!string.IsNullOrEmpty(hdn_DateFrom.Value))
                {
                    objColumns.Add("ReceiptDate;DateTime;Geq;" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.ParseExact(hdn_DateFrom.Value, "M/d/yyyy", CultureInfo.CurrentCulture)));
                }
                if (!string.IsNullOrEmpty(hdn_DateTo.Value))
                {
                    objColumns.Add("ReceiptDate;DateTime;Leq;" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.ParseExact(hdn_DateTo.Value, "M/d/yyyy", CultureInfo.CurrentCulture)));
                }

                if (txt_RequestID.Text != string.Empty)
                {
                   
                        objColumns.Add("Requests_RequestNumber;Text;Contains;" + txt_RequestID.Text.Trim());
                  
                }

                if (objColumns.Count > 0)
                    srchRequests = BL.SearchSimilarRequests.GetAllPaymentRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(objColumns, "And", true) + "<OrderBy><FieldRef Name='ReceiptDate' Ascending='False' /></OrderBy><Where><IsNotNull><FieldRef Name='RequestID' /></IsNotNull></Where>", LCID).ToList();
                else
                    srchRequests = BL.SearchSimilarRequests.GetAllPaymentRequests("<OrderBy><FieldRef Name='ReceiptDate' Ascending='False' /></OrderBy><Where><IsNotNull><FieldRef Name='RequestID' /></IsNotNull></Where>", LCID).ToList();

                srchRequests = srchRequests.Where(a => a.ReasonCode == "100" || a.ResponseMessage== "Request was processed successfully.").ToList();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SearchRequests.GetAllPaymentRequestsData");
            }
            return srchRequests;
        }

        private void BindGridOnSearch()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method PaymentRequests.BindGridOnSearch");
                List<string> objColumns = new List<string>();

                List<Entities.Payments> srchRequests = GetAllPaymentRequestsData();

                HelperMethods.BindGridView(grd_Requests, srchRequests);

                if (srchRequests.Count > 0)
                {
                    //SrchControls.Visible = true;
                    lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NoOfRequests", (uint)LCID) + srchRequests.Count;
                    lbl_NoOfRequests.Visible = true;
                }
                else
                {
                    //SrchControls.Visible = false;
                    lbl_NoOfRequests.Visible = false;
                }

                if (srchRequests.Count >= Common.Utilities.Constants.SearchLimit)
                {
                    searchLimit.Visible = true;
                }
                else
                {
                    searchLimit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PaymentRequests.BindGridOnSearch");
            }
        }

       

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void ClearControls()
        {
            hdn_DateTo.Value = string.Empty;
            hdn_DateFrom.Value = string.Empty;
           
            txt_RequestID.Text = string.Empty;
          

            BindGridOnSearch();
        }

        protected void grd_Requests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter PaymentRequests.grd_Requests_PageIndexChanging");
            try
            {
                grd_Requests.PageIndex = e.NewPageIndex;
                BindGridOnSearch();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PaymentRequests.grd_Requests_PageIndexChanging");
            }
        }

       

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_RequestID.Text)  && string.IsNullOrEmpty(hdn_DateFrom.Value) && string.IsNullOrEmpty(hdn_DateTo.Value))
               
            {
                modalSearchFilter.Show();
            }
            else
            {
                BindGridOnSearch();
            }

            //SrchControls.Visible = true;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            BindGridOnSearch();
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            List<Entities.Payments> srchRequests = GetAllPaymentRequestsData();

            // DataTable similarRequestTable = new DataTable();

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Applicants Data");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            //Header of table
            if (LCID == (int)Language.English)
            {
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cells[1, 1].Value = "S.No";
                workSheet.Cells[1, 2].Value = "Request Number";
                workSheet.Cells[1, 3].Value = "Submit Date";
                workSheet.Cells[1, 4].Value = "Qatari ID";
                //workSheet.Cells[1, 5].Value = "Arabic Name";
                workSheet.Cells[1, 5].Value = "English Name";
                //workSheet.Cells[1, 7].Value = "Nationality";
                //workSheet.Cells[1, 8].Value = "Nation Catgeory";
                //workSheet.Cells[1, 9].Value = "Certificate";
                //workSheet.Cells[1, 10].Value = "Entity Needs Certificate";
                //workSheet.Cells[1, 11].Value = "Country Name";
                //workSheet.Cells[1, 12].Value = "University";
                //workSheet.Cells[1, 13].Value = "Faculty";
                //workSheet.Cells[1, 14].Value = "Specialization";
                workSheet.Cells[1, 6].Value = "Request Status";
                //workSheet.Cells[1, 16].Value = "Responsible Officer";
                workSheet.Cells[1, 7].Value = "Amount";
                workSheet.Cells[1, 8].Value = "Response payment";
                //Body of table
                //
                int recordIndex = 2;
                foreach (var item in srchRequests)
                {
                    workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
                    workSheet.Cells[recordIndex, 2].Value = item.RequestNumber;
                    workSheet.Cells[recordIndex, 3].Value = item.ReceiptDate.ToString();
                    workSheet.Cells[recordIndex, 4].Value = item.QatariID;
                    //workSheet.Cells[recordIndex, 5].Value = item.ArabicName;
                    workSheet.Cells[recordIndex, 5].Value = item.EnglishName;
                    //workSheet.Cells[recordIndex, 7].Value = item.Nationality;
                    //workSheet.Cells[recordIndex, 8].Value = item.NationCatgeory;
                    //workSheet.Cells[recordIndex, 9].Value = item.AcademicDegree;
                    //workSheet.Cells[recordIndex, 10].Value = item.EntityNeedsEquivalency;
                    //workSheet.Cells[recordIndex, 11].Value = item.Country;
                    //workSheet.Cells[recordIndex, 12].Value = item.University;
                    //workSheet.Cells[recordIndex, 13].Value = item.Faculty;
                    //workSheet.Cells[recordIndex, 14].Value = item.Specialization;
                    workSheet.Cells[recordIndex, 6].Value = item.RequestStatus;
                    //workSheet.Cells[recordIndex, 16].Value = item.AssignedTo;
                    workSheet.Cells[recordIndex, 7].Value = item.Amount;
                    workSheet.Cells[recordIndex, 8].Value = item.ResponseMessage;

                    recordIndex++;
                }
                workSheet.Column(1).AutoFit();
                workSheet.Column(2).AutoFit();
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).AutoFit();
                string excelName = "UCE_PaymentEmployeeSearch";

                using (var memoryStream = new MemoryStream())
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                    excel.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            else
            {
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cells[1, 1].Value = "م";
                workSheet.Cells[1, 2].Value = "رقم الطلب";
                workSheet.Cells[1, 3].Value = "تاريخ إرسال الطلب";
                workSheet.Cells[1, 4].Value = "الرقم الشخصي";
                workSheet.Cells[1, 5].Value = "اسم الطالب عربي";
                //workSheet.Cells[1, 6].Value = "اسم الطالب إنجليزي";
                //workSheet.Cells[1, 7].Value = "الجنسية";
                //workSheet.Cells[1, 8].Value = "فئة الجنسية";
                //workSheet.Cells[1, 9].Value = "الشهادة";
                //workSheet.Cells[1, 10].Value = "الجهة الطالبة للشهادة";
                //workSheet.Cells[1, 11].Value = "الدولة";
                //workSheet.Cells[1, 12].Value = "الجامعة";
                //workSheet.Cells[1, 13].Value = "الكلية";
                //workSheet.Cells[1, 14].Value = "التخصص";
                workSheet.Cells[1, 6].Value = "حالة الطلب";

                //workSheet.Cells[1, 16].Value = "الموظف المسئول";
                workSheet.Cells[1, 7].Value = "المبلغ";
                workSheet.Cells[1, 8].Value = "حالة الدفع";
                //Body of table
                //
                int recordIndex = 2;
                foreach (var item in srchRequests)
                {
                    workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
                    workSheet.Cells[recordIndex, 2].Value = item.RequestNumber;
                    workSheet.Cells[recordIndex, 3].Value = item.ReceiptDate.ToString();
                    workSheet.Cells[recordIndex, 4].Value = item.QatariID;
                    workSheet.Cells[recordIndex, 5].Value = item.ArabicName;
                    //workSheet.Cells[recordIndex, 6].Value = item.EnglishName;
                    //workSheet.Cells[recordIndex, 7].Value = item.Nationality;
                    //workSheet.Cells[recordIndex, 8].Value = item.NationCatgeory;
                    //workSheet.Cells[recordIndex, 9].Value = item.AcademicDegree;
                    //workSheet.Cells[recordIndex, 10].Value = item.EntityNeedsEquivalency;
                    //workSheet.Cells[recordIndex, 11].Value = item.Country;
                    //workSheet.Cells[recordIndex, 12].Value = item.University;
                    //workSheet.Cells[recordIndex, 13].Value = item.Faculty;
                    //workSheet.Cells[recordIndex, 14].Value = item.Specialization;
                    workSheet.Cells[recordIndex, 6].Value = item.RequestStatus;

                   // workSheet.Cells[recordIndex, 16].Value = item.AssignedTo;
                    workSheet.Cells[recordIndex, 7].Value = item.Amount;
                    workSheet.Cells[recordIndex, 8].Value = item.ResponseMessage;
                    recordIndex++;
                }
                workSheet.Column(1).AutoFit();
                workSheet.Column(2).AutoFit();
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).AutoFit();
                string excelName = "UCE_PaymentEmployeeSearch";

                using (var memoryStream = new MemoryStream())
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                    excel.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }
    }
}