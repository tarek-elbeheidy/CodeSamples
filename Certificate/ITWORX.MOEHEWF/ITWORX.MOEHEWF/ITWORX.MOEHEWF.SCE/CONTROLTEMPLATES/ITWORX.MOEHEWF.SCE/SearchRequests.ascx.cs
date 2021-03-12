using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.BL;
using ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common;
using ITWORX.MOEHEWF.SCE.Utilities;
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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE
{
    public partial class SearchRequests : UserControlBase
    {
        protected DDLWithTXTWithNoPostback certificateResource;
        protected DDLWithTXTWithNoPostback schooleType;
        protected DDLWithTXTWithNoPostback certificateType; 


        #region Public Properties
        public string SPGroupName { get; set; }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            grd_Requests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SearchPageSize"));
            BindDropDowns();
            BindEmployees();
        }

        private List<Entities.NewRequests> GetSearchRequests()
        {
            List<Entities.NewRequests> srchRequests = new List<Entities.NewRequests>();
            try
            {
                Logging.GetInstance().Debug("Entering method SCESearchRequests.GetSearchRequests");
                List<string> objColumns = new List<string>();

                 

                if (!string.IsNullOrEmpty(hdn_DateFrom.Value))
                {
                    objColumns.Add("SubmitDate;DateTime;Geq;" + Convert.ToDateTime(hdn_DateFrom.Value).ToString("yyyy-MM-dd"));
                }
                if (!string.IsNullOrEmpty(hdn_DateTo.Value))
                {
                    objColumns.Add("SubmitDate;DateTime;Leq;" + Convert.ToDateTime(hdn_DateTo.Value).ToString("yyyy-MM-dd"));
                } 
                if (drp_Nationality.SelectedValue != "-1")
                {
                   objColumns.Add("StdNationality;Lookup;Eq;" + drp_Nationality.SelectedValue);
                }
                if (!string.IsNullOrEmpty(txt_StudentName.Text))
                {
                    objColumns.Add("StdPrintedName;Text;Contains;" + txt_StudentName.Text);
                } 
                if (!string.IsNullOrEmpty(txt_RequestID.Text))
                {
                    objColumns.Add("RequestNumber;Text;Contains;" + txt_RequestID.Text);
                } 
                if (!string.IsNullOrEmpty(txt_QatarID.Text))
                {
                    objColumns.Add("StdQatarID;Text;Contains;" + txt_QatarID.Text);
                }
                if (!string.IsNullOrEmpty(txt_PhoneNumber.Text))
                {
                    objColumns.Add("MobileNumber;Text;Contains;" + txt_PhoneNumber.Text);
                }
                if (certificateResource.SelectedValue != "-1"&&certificateResource.SelectedValue != "-2")
                {
                    objColumns.Add("CertificateResource;Lookup;Eq;" + certificateResource.SelectedValue);
                }
                else if(certificateResource.SelectedValue == "-2"&&!string.IsNullOrEmpty(certificateResource.OtherValue))
                {
                    objColumns.Add("OtherCertificateResource;Text;Contains;" + certificateResource.OtherValue);
                }
                if (schooleType.SelectedValue != "-1"&& schooleType.SelectedValue != "-2")
                {
                    objColumns.Add("SchoolType;Lookup;Eq;" + schooleType.SelectedValue);
                }
                else if (schooleType.SelectedValue == "-2"&&!string.IsNullOrEmpty(schooleType.OtherValue))
                {
                    objColumns.Add("OtherSchoolType;Text;Contains;" + schooleType.OtherValue); 
                }
                if (certificateType.SelectedValue != "-1"&& certificateType.SelectedValue != "-2")
                {
                    objColumns.Add("CertificateType;Lookup;Eq;" + certificateType.SelectedValue);
                }
                else if (certificateType.SelectedValue == "-2"&&!string.IsNullOrEmpty(certificateType.OtherValue))
                {
                    objColumns.Add("OtherCertificateType;Text;Contains;" + certificateType.OtherValue); 
                }
                if (drp_RequestStatus.SelectedValue != "-1")
                {
                    objColumns.Add("RequestStatus;Lookup;Eq;" + drp_RequestStatus.SelectedValue);
                }
                if (drp_Employees.SelectedValue != "-1")
                {
                    objColumns.Add("EmployeeAssignedTo;Text;Contains;" + drp_Employees.SelectedValue);
                }


                if (objColumns.Count > 0)
                    srchRequests = BL.SearchRequests.GetAllRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(objColumns, "And", true) + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SearchRequests.GetSearchRequests");
            }
            return srchRequests;
        }
        private void BindGridOnSearch()
        {
            try
            {
                List<Entities.NewRequests> srchRequests = GetSearchRequests();
                HelperMethods.BindGridView(grd_Requests, srchRequests);
                if (srchRequests.Count > 0)
                {
                    lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "NoOfRequests", (uint)LCID) + srchRequests.Count;
                    lbl_NoOfRequests.Visible = true;
                    lblNewRequests.Visible = true;
                }
                else
                {
                    lbl_NoOfRequests.Visible = false;
                    lblNewRequests.Visible = false; 
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SearchRequests.BindGridOnSearch");
            }


        }
        private void BindEmployees()
        {
            drp_Employees.Items.Clear();
            List<SPUser> Araibcusers = HelperMethods.GetGroupUsers(Common.Utilities.Constants.SCEEquivalenceEmployeesGroupName);
            foreach (SPUser user in Araibcusers)
            {
                drp_Employees.Items.Add(new ListItem(user.Name, user.LoginName));
            }

            drp_Employees.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ChooseValue", (uint)LCID), "-1"));
        }
        private void BindDropDowns()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method SCESearchRequests.BindDropDowns");
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    drp_Nationality.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ChooseValue", (uint)LCID), "-1"));
                    drp_Nationality.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref drp_Nationality, Nationality.GetAll(), "EnglishTitle", "ArabicTitle", "EnglishTitle", (int)SPContext.Current.Web.Language);
                     
                    certificateResource.DataSource = ctx.CountryOfStudyList;
                    certificateResource.DataValueField = "ID";
                    certificateResource.DataENTextField = "Title";
                    certificateResource.DataARTextField = "TitleAr"; 
                    certificateResource.Title = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "CertificateResourceTitle", (uint)LCID);
                    certificateResource.ValidationMSG = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "CertificateResourceValidation", (uint)LCID);
                    certificateResource.BingDDL();
                     
                    schooleType.DataSource = ctx.SchoolTypeList;
                    schooleType.DataValueField = "Title";
                    schooleType.DataENTextField = "Title";
                    schooleType.DataARTextField = "TitleAr"; 
                    schooleType.Title = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SchoolTypeTitle", (uint)LCID);
                    schooleType.ValidationMSG = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SchoolTypeValidation", (uint)LCID);
                    schooleType.BingDDL();

                     
                    certificateType.DataSource = BL.CertificateType.GetEquivalenceCertificateType();
                    certificateType.DataValueField = "Title";
                    certificateType.DataENTextField = "Title";
                    certificateType.DataARTextField = "TitleAr";
                    certificateType.IsRequired = false; 
                    certificateType.Title = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "CertificateTypeTitle", (uint)LCID);
                    certificateType.BingDDL();


                    var statusList = Common.BL.RequestStatus.GetDistinctStatusToApplicant();
                    HelperMethods.BindDropDownList(ref drp_RequestStatus, statusList, "ApplicantDescriptionEn", "ApplicantDescriptionAr", "ApplicantDescriptionEn", LCID);
                    drp_RequestStatus.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ChooseValue", (uint)LCID), "-1"));


                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method SCESearchRequests.BindDropDowns");
            }
        }

        private void ClearControls()
        {
            txt_RequestID.Text = string.Empty;
            txt_QatarID.Text = string.Empty;
            txt_StudentName.Text = string.Empty;
            txt_PhoneNumber.Text = string.Empty;
            drp_Nationality.SelectedValue = "-1";
            certificateResource.SelectedValue = "-1";
            certificateResource.OtherValue = string.Empty; 
            schooleType.SelectedValue = "-1";
            schooleType.OtherValue = string.Empty; 
            certificateType.SelectedValue = "-1";
            certificateType.OtherValue = string.Empty; 
            drp_RequestStatus.SelectedValue = "-1";
            hdn_DateFrom.Value = string.Empty;
            hdn_DateTo.Value = string.Empty;
            drp_Employees.SelectedValue = "-1";

            BindGridOnSearch();
            grd_Requests.Visible = false;
            searchLimit.Visible = true;

        }
        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_RequestID.Text) && string.IsNullOrEmpty(txt_QatarID.Text) && string.IsNullOrEmpty(txt_StudentName.Text)
                && string.IsNullOrEmpty(txt_PhoneNumber.Text)
               && drp_Nationality.SelectedValue == "-1"  && certificateResource.SelectedValue == "-1" && string.IsNullOrEmpty(certificateResource.OtherValue)
               && schooleType.SelectedValue == "-1" && certificateType.SelectedValue == "-1" && string.IsNullOrEmpty(schooleType.OtherValue)
               && drp_RequestStatus.SelectedValue == "-1" && drp_Employees.SelectedValue == "-1" && string.IsNullOrEmpty(certificateType.OtherValue)
               && string.IsNullOrEmpty(hdn_DateFrom.Value) && string.IsNullOrEmpty(hdn_DateTo.Value))
            {
                grd_Requests.Visible = false;
                searchLimit.Visible = true;
            }
            else
            {
                searchLimit.Visible = false;
                grd_Requests.Visible = true;
            }
            BindGridOnSearch();

        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {

            List<Entities.NewRequests> srchRequests = GetSearchRequests(); 

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
            workSheet.Cells[1, 4].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ExcepApplicantName", (uint)LCID);
            workSheet.Cells[1, 5].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "LastGrade", (uint)LCID);
            workSheet.Cells[1, 6].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "Nationality", (uint)LCID);
            workSheet.Cells[1, 7].Value = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ExcepPhoneNumber", (uint)LCID); 

            int recordIndex = 2;
            foreach (var item in srchRequests)
            { 
                workSheet.Cells[recordIndex, 1].Value = item.RequestNumber;
                workSheet.Cells[recordIndex, 2].Value = item.RecievedDate.ToShortDateString();
                workSheet.Cells[recordIndex, 3].Value = item.QatariID;
                workSheet.Cells[recordIndex, 4].Value = item.StudentNameAccToCert;
                workSheet.Cells[recordIndex, 5].Value = item.SchoolLastGrade;
                workSheet.Cells[recordIndex, 6].Value = item.Nationality;
                workSheet.Cells[recordIndex, 7].Value = item.ApplicantMobileNumber;  
                recordIndex++;
            }
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            string excelName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "searchRequests", (uint)LCID);

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

        protected void grd_Requests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter SearchRequests.grd_Requests_PageIndexChanging");
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

                Logging.GetInstance().Debug("Exit SearchRequests.grd_Requests_PageIndexChanging");
            }
        }

        protected void grd_Requests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter SearchRequests.grd_Requests_RowDataBound");
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hdnRequestStatusId = (HiddenField)e.Row.FindControl("hdn_RequestStatusId");
                    HiddenField IsClosed = (HiddenField)e.Row.FindControl("hdn_IsClosed");
                    HiddenField AssignedTo = (HiddenField)e.Row.FindControl("hdn_AssignedTo");
                    LinkButton btnEdit = (LinkButton)e.Row.FindControl("lnk_Edit");
                    LinkButton btnView = (LinkButton)e.Row.FindControl("lnk_View");
                    if (!Convert.ToBoolean(IsClosed.Value))
                    {
                        if (AssignedTo.Value.ToLower() == SPContext.Current.Web.CurrentUser.LoginName.ToLower() || AssignedTo.Value.ToLower() == SPGroupName.ToLower())
                        {
                            btnEdit.Visible = true;
                            btnView.Visible = false; 
                        }
                        else
                        {
                            btnEdit.Visible = false;
                            btnView.Visible = true;
                        }
                    }
                    else
                    {
                        btnEdit.Visible = false;
                        btnView.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit SearchRequests.grd_Requests_RowDataBound");
            }
        }

        protected void lnk_Edit_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter SearchRequests.lnk_Edit_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField lblRequestID = (HiddenField)gvr.FindControl("hdn_ID");
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdn_RequestStatusId");
                Common.Entities.RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                string editLink = string.Empty;

                if (requestStatus.CanReviewerEditRequest)
                    editLink = requestStatus.ReviewerTargetPageURL + "?RequestId=" + lblRequestID.Value;
                 
                SPUtility.Redirect(SPContext.Current.Web.Url + editLink, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit SearchRequests.lnk_Edit_Click");
            }
        }

        protected void lnk_View_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter SearchRequests.lnk_View_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField lblRequestID = (HiddenField)gvr.FindControl("hdn_ID");
                // Page.Session["SCEDisplayRequestId"] = lblRequestID.Value;
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdn_RequestStatusId");
                Common.Entities.RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                string viewLink = string.Empty;
                if (requestStatus.CanReviewerEditRequest)
                    viewLink = requestStatus.ReviewerTargetPageURL + "?RequestId=" + lblRequestID.Value;

                SPUtility.Redirect(SPContext.Current.Web.Url + viewLink, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SearchRequests.lnk_View_Click");
            }
        }
         
    }
}
