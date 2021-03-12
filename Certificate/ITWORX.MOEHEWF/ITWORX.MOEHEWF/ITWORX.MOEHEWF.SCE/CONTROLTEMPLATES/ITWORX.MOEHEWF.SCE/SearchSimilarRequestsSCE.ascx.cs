using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;
using ITWORX.MOEHEWF.Common.Utilities;
using System.Collections.Generic;
using ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common;
using ITWORX.MOEHEWF.SCE.Entities;
using ITWORX.MOEHEWF.Common;
using ITWORX.MOEHEWF.SCE.BL;
using System.Net;
//using ITWORX.MOEHEWF.Common.BL;

namespace ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE
{
    public partial class SearchSimilarRequestsSCE : UserControlBase
    {
        /// <summary>
        /// ddl_SchoolType control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected DDLWithTXTWithNoPostback ddl_SchoolType;

        private int RequestId { get { return Convert.ToInt32(Request.QueryString["RequestId"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method SearchSimilarRequestsSCE.Page_Load");
                grd_SearchResults.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "DashboardPageSize"));
                var hdnCtl = Page.Request.Form.Get("__EventTriggerSearchControl");
                if (Request.Form["__EVENTTARGET"] ==null ) 
                {
                    BindData();
                }    
                else if(Request.Form["__EVENTTARGET"].ToString().Contains("Menu_Links"))
                {
                    BindData();
                }
                else if(string.IsNullOrEmpty(hdnCtl) && !Request.Form["__EVENTTARGET"].ToString().Contains(grd_SearchResults.ID) )
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
                Logging.GetInstance().Debug("Exiting method SearchSimilarRequestsSCE.Page_Load");
            }
        }

        private void BindData()
        {
            try
            {
                Logging.GetInstance().Debug("Exiting method SearchSimilarRequestsSCE.BindData");
                string rootWebUrl = SPContext.Current.Site.RootWeb.Url;
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SCEContextDataContext ctx = new SCEContextDataContext(rootWebUrl))
                    {

                        HelperMethods.BindDropDownList(ref ddl_Nationality, LCID == (int)Language.English ? Common.BL.Nationality.GetAll().OrderBy(n => n.EnglishTitle) : Common.BL.Nationality.GetAll().OrderBy(n => n.ArabicTitle), "ID", "ArabicTitle", "EnglishTitle", LCID);
                        ddl_Nationality.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ChooseValue", (uint)LCID), string.Empty));
                        ddl_Nationality.AppendDataBoundItems = true;

                        HelperMethods.BindDropDownList(ref ddl_CertificateResource, LCID == (int)Language.English ? ctx.CountryOfStudyList.ToList().OrderBy(c => c.Title) : ctx.CountryOfStudyList.ToList().OrderBy(c => c.TitleAr), "ID", "TitleAr", "Title", LCID);
                        ddl_CertificateResource.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ChooseValue", (uint)LCID), string.Empty));
                        ddl_CertificateResource.AppendDataBoundItems = true;

                        ddl_SchoolType.DataSource = BL.SchoolType.GetAll();
                        ddl_SchoolType.DataValueField = "ID";
                        ddl_SchoolType.DataENTextField = "Title";
                        ddl_SchoolType.DataARTextField = "TitleAr";
                        ddl_SchoolType.ValidationGroup = "Submit";
                        ddl_SchoolType.Title = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SchoolTypeTitle", (uint)LCID);
                        //ddl_SchoolType.ValidationMSG = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "SchoolTypeValidation", (uint)LCID);
                        ddl_SchoolType.BingDDL();

                        HelperMethods.BindDropDownList(ref ddl_LastSchoolLevel, ctx.ScholasticLevelList.Where(s => s.Status == Status.Active).ToList(), "ID", "TitleAr", "Title", LCID);
                        ddl_LastSchoolLevel.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ChooseValue", (uint)LCID), string.Empty));
                        ddl_LastSchoolLevel.AppendDataBoundItems = true;

                        HelperMethods.BindDropDownList(ref ddl_PreferedSchoolLevel, ctx.ScholasticLevelList.Where(s => s.Status == Status.Active).ToList(), "ID", "TitleAr", "Title", LCID);
                        ddl_PreferedSchoolLevel.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ChooseValue", (uint)LCID), string.Empty));
                        ddl_PreferedSchoolLevel.AppendDataBoundItems = true;

                        HelperMethods.BindDropDownList(ref ddl_CertificateType, BL.CertificateType.GetEquivalenceCertificateType(), "ID", "TitleAr", "Title", LCID);
                        ddl_CertificateType.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ChooseValue", (uint)LCID), string.Empty));
                        ddl_CertificateType.AppendDataBoundItems = true;

                        HelperMethods.BindDropDownList(ref ddl_AcademicYear, getCertificateYears(), "Key", "Value", "Value", LCID);
                        ddl_AcademicYear.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "ChooseValue", (uint)LCID), string.Empty));
                        ddl_AcademicYear.AppendDataBoundItems = true;
                    }

                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SearchSimilarRequestsSCE.BindData");
            }
        }

        private Dictionary<string, string> getCertificateYears()
        {
            int StartYear = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SchoolingAcademicStudyStart"));
            int EndYear = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SchoolingAcademicStudyEnd"));
            int nextYear = StartYear + 1;

            Dictionary<string, string> yearsList = new Dictionary<string, string>();

            for (int i = StartYear; i < EndYear; i++)
            {
                nextYear = i + 1;
                yearsList.Add(i + " - " + nextYear, i + " - " + nextYear);
            }
            return yearsList;
        }

        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method SearchSimilarRequestsSCE.btn_Clear_Click");
                ddl_Nationality.ClearSelection();
                ddl_CertificateResource.ClearSelection();
                ddl_SchoolType.SelectedValue = "-1";
                ddl_LastSchoolLevel.ClearSelection();
                ddl_PreferedSchoolLevel.ClearSelection();
                ddl_CertificateType.ClearSelection();
                ddl_AcademicYear.ClearSelection();

                BindGrid();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SearchSimilarRequestsSCE.btn_Clear_Click");
            }

        }

        protected void Btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Exiting method SearchSimilarRequestsSCE.Btn_search_Click");
                BindGrid();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SearchSimilarRequestsSCE.Btn_search_Click");
            }
        }

        private void BindGrid()
        {
            string rootWebUrl = SPContext.Current.Site.RootWeb.Url;
            List<SearchSimilarRequest> similarRequests = new List<SearchSimilarRequest>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SCEContextDataContext ctx = new SCEContextDataContext(rootWebUrl))
                {
                    SCERequestsListFieldsContentType currentRequest = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == RequestId).FirstOrDefault();
                    var currRelatedReqs = currentRequest.RelatedRequests;
                    List<SCERequestsListFieldsContentType> requestsList;
                    List<SCERequestsListFieldsContentType> filteredRequest;
                    if (currRelatedReqs == null)
                    {
                        requestsList = ctx.SCERequestsList.ScopeToFolder("", true).Where(r =>
                        (r.RequestStatus.Id == Common.Utilities.RequestStatus.SCESectionManagerAccepted.ToInt() || r.RequestStatus.Id == Common.Utilities.RequestStatus.SCESectionManagerRejected.ToInt())).ToList();
                    }
                    else
                    {
                        requestsList = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => (!currentRequest.RelatedRequests.Split(';').Contains(r.Id.ToString()))
                        && (r.RequestStatus.Id == Common.Utilities.RequestStatus.SCESectionManagerAccepted.ToInt() || r.RequestStatus.Id == Common.Utilities.RequestStatus.SCESectionManagerRejected.ToInt())).ToList();
                    }

                    if (ddl_SchoolType.SelectedValue == "-2")
                    {
                        filteredRequest = requestsList.Where(r =>
                        (ddl_CertificateResource.SelectedValue != string.Empty ? r.CertificateResourceId == Convert.ToInt32(ddl_CertificateResource.SelectedValue) : true) &&
                        (ddl_Nationality.SelectedValue != string.Empty ? r.StdNationality.Id == Convert.ToInt32(ddl_Nationality.SelectedValue) : true) &&
                        (ddl_CertificateType.SelectedValue != string.Empty ? (r.CertificateType != null && r.CertificateType.Id == Convert.ToInt32(ddl_CertificateType.SelectedValue)) : true) &&
                        (ddl_PreferedSchoolLevel.SelectedValue != string.Empty ? (r.RegisteredScholasticLevel != null && r.RegisteredScholasticLevel.Id == Convert.ToInt32(ddl_PreferedSchoolLevel.SelectedValue)) : true) &&
                         (ddl_CertificateType.SelectedValue != string.Empty ? (r.CertificateType != null && r.CertificateType.Id == Convert.ToInt32(ddl_CertificateType.SelectedValue)) : true) &&
                        (r.OtherSchoolType != null && r.OtherSchoolType.ToLower() == HelperMethods.Normalize( ddl_SchoolType.OtherValue.ToLower())) &&
                        (ddl_LastSchoolLevel.SelectedValue != string.Empty ? (r.LastScholasticLevel !=null && r.LastScholasticLevel.Id == Convert.ToInt32(ddl_LastSchoolLevel.SelectedValue)) : true) &&
                        (ddl_AcademicYear.SelectedValue != string.Empty ? r.LastAcademicYear == ddl_AcademicYear.SelectedItem.Text : true)
                        ).ToList();
                    }
                    else if (ddl_SchoolType.SelectedValue == "-1")
                    {
                        filteredRequest = requestsList.Where(r =>
                        (ddl_CertificateResource.SelectedValue != string.Empty ? r.CertificateResourceId == Convert.ToInt32(ddl_CertificateResource.SelectedValue) : true) &&
                        (ddl_Nationality.SelectedValue != string.Empty ? r.StdNationality.Id == Convert.ToInt32(ddl_Nationality.SelectedValue) : true) &&
                        (ddl_CertificateType.SelectedValue != string.Empty ? (r.CertificateType != null && r.CertificateType.Id == Convert.ToInt32(ddl_CertificateType.SelectedValue)) : true) &&
                        (ddl_PreferedSchoolLevel.SelectedValue != string.Empty ? (r.RegisteredScholasticLevel != null && r.RegisteredScholasticLevel.Id == Convert.ToInt32(ddl_PreferedSchoolLevel.SelectedValue)) : true) &&
                        (ddl_LastSchoolLevel.SelectedValue != string.Empty ? (r.LastScholasticLevel != null && r.LastScholasticLevel.Id == Convert.ToInt32(ddl_LastSchoolLevel.SelectedValue)) : true) &&
                        (ddl_AcademicYear.SelectedValue != string.Empty ? r.LastAcademicYear == ddl_AcademicYear.SelectedItem.Text : true) //&&                                                                                                                        
                        ).ToList();
                    }
                    else
                    {
                        filteredRequest = requestsList.Where(r =>
                        (ddl_CertificateResource.SelectedValue != string.Empty ? r.CertificateResourceId == Convert.ToInt32(ddl_CertificateResource.SelectedValue) : true) &&
                        (ddl_Nationality.SelectedValue != string.Empty ? r.StdNationality.Id == Convert.ToInt32(ddl_Nationality.SelectedValue) : true) &&
                        (ddl_CertificateType.SelectedValue != string.Empty ? (r.CertificateType != null && r.CertificateType.Id == Convert.ToInt32(ddl_CertificateType.SelectedValue)) : true) &&
                        (ddl_PreferedSchoolLevel.SelectedValue != string.Empty ? (r.RegisteredScholasticLevel != null && r.RegisteredScholasticLevel.Id == Convert.ToInt32(ddl_PreferedSchoolLevel.SelectedValue)) : true) &&
                        (r.SchoolType != null && r.SchoolType.Id == Convert.ToInt32(ddl_SchoolType.SelectedValue)) &&
                        (ddl_LastSchoolLevel.SelectedValue != string.Empty ? (r.LastScholasticLevel != null && r.LastScholasticLevel.Id == Convert.ToInt32(ddl_LastSchoolLevel.SelectedValue)) : true) &&
                        (ddl_AcademicYear.SelectedValue != string.Empty ? r.LastAcademicYear == ddl_AcademicYear.SelectedItem.Text : true)).ToList();
                    }

                    foreach (var item in filteredRequest)
                    {
                        SearchSimilarRequest req = new SearchSimilarRequest();
                        req.RequestStatusId = item.RequestStatus.Id.Value;
                        req.RegisteredSchoolId = item.RegisteredScholasticLevel.Id.Value;
                        req.Id = item.Id;
                        req.RequestNumber = item.RequestNumber;
                        req.RequestDate = item.DecisionDate.ToString();
                        req.RequestUrl = SPContext.Current.Web.Url + "/Pages/ViewRequest.aspx?RequestId=" + req.Id;
                        if (item.StdNationality != null)
                        {
                            var natObj = Common.BL.Nationality.GetAll();// ctx.NationalityList.ToList();
                            req.Nationality = LCID == (int)Language.English ? natObj.Where(n => n.ID == item.StdNationality.Id).FirstOrDefault().EnglishTitle : natObj.Where(n => n.ID == item.StdNationality.Id).FirstOrDefault().ArabicTitle;
                        }
                        else
                            req.Nationality = string.Empty;
                        req.ApplicantName = item.StdPrintedName;
                        req.ApplicantNumber = item.StdQatarID;


                        if (string.IsNullOrEmpty(item.OtherCertificateResource))
                        {
                            if (item.CertificateResourceId != null)
                            {
                                var certificateResourceItem = Common.BL.CountryOfStudy.GetAll(LCID);//ctx.CountryOfStudyList.Where(c => c.Id == Convert.ToInt32(item.CertificateResourceId)).FirstOrDefault();
                                req.CertificateOrigin = LCID == (int)Language.English ? certificateResourceItem.Where(c => c.ID == Convert.ToInt32(item.CertificateResourceId)).FirstOrDefault().EnglishTitle : certificateResourceItem.Where(c => c.ID == Convert.ToInt32(item.CertificateResourceId)).FirstOrDefault().ArabicTitle;
                            }
                            else
                                req.CertificateOrigin = "";
                        }
                        else
                        {
                            req.CertificateOrigin = item.OtherCertificateResource;
                        }

                        if (string.IsNullOrEmpty(item.OtherSchoolType))
                        {
                            if (item.SchoolType != null)
                                req.SchoolType = LCID == (int)Language.English ? ((SchoolTypeLookupsCT)item.SchoolType).Title : ((SchoolTypeLookupsCT)item.SchoolType).TitleAr;
                        }
                        else
                        {
                            req.SchoolType = item.OtherSchoolType;
                        }

                        if (item.CertificateType != null)
                            req.CertificateType = LCID == (int)Language.English ? ((CertificateTypeLookupsCT)item.CertificateType).Title : ((CertificateTypeLookupsCT)item.CertificateType).TitleAr;
                        else
                            req.CertificateType = string.Empty;
                        similarRequests.Add(req);

                    }
                    HelperMethods.BindGridView(grd_SearchResults, similarRequests);
                    if (similarRequests.Count > 0)
                    {
                        lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_SCE", "NoOfRequests", (uint)LCID) + similarRequests.Count;
                        lbl_NoOfRequests.Visible = true;
                        btn_Save.Visible = true;
                        btn_Save.OnClientClick = string.Format("LnkClickSearch('{0}');", btn_Save.ID);
                    }
                    else
                    {
                        lbl_NoOfRequests.Visible = false;
                        btn_Save.Visible = false;
                    }

                }

            });
        }

        protected void grd_SearchResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Logging.GetInstance().Debug("Enter SearchSimilarRequestsSCE.grd_SearchResults_RowDataBound");
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton viewLink = (LinkButton)e.Row.FindControl("lnk_View");

                    HiddenField hdnRequestStatus = (HiddenField)e.Row.FindControl("hdnRequestStatus");
                    HiddenField hdnRegisteredLevel= (HiddenField)e.Row.FindControl("hdnRegisteredLevel");

                   
                    int requestStatusId = int.Parse(hdnRequestStatus.Value);
                    if (requestStatusId == (int)RequestStatus.SCESectionManagerAccepted
                        || (!string.IsNullOrEmpty(hdnRegisteredLevel.Value) && int.Parse(hdnRegisteredLevel.Value) == 14
                        && requestStatusId == (int)RequestStatus.SCESectionManagerRejected))

                    {
                        viewLink.Visible = true;
                    }
                    else
                    {
                        viewLink.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SearchSimilarRequestsSCE.grd_SearchResults_RowDataBound");
            }
        }

                protected void lnk_View_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method SearchSimilarRequestsSCE.lnk_View_Click");

                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_RequestId = (HiddenField)gvr.FindControl("hdn_ID");
                SCERequestAttachments.ViewAttachments(Utilities.Constants.SCERequests, false, Convert.ToInt32(hdn_RequestId.Value), Response);

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SearchSimilarRequestsSCE.lnk_View_Click");
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method SearchSimilarRequestsSCE.btn_Save_Click");
                string requestId = Request.QueryString["RequestId"];
                string relatedReqs = "";
                using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                {
                    for (int i = 0; i < grd_SearchResults.Rows.Count; i++)
                    {
                        CheckBox chkStat = grd_SearchResults.Rows[i].FindControl("chk_attach") as CheckBox;

                        if (chkStat.Checked)
                        {
                            HiddenField reqId = grd_SearchResults.Rows[i].FindControl("hdn_ID") as HiddenField;
                            relatedReqs += reqId.Value + ";";

                        }
                    }
                    SCERequestsListFieldsContentType request = ctx.SCERequestsList.ScopeToFolder("", true).Where(c => c.Id.Value == Convert.ToInt32(requestId)).FirstOrDefault();
                    request.RelatedRequests = request.RelatedRequests + relatedReqs;

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
                Logging.GetInstance().Debug("Exiting method SearchSimilarRequestsSCE.btn_Save_Click");
            }
        }
 
        protected void grd_SearchResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering SearchSimilarRequestsSCE.grd_ApprovedResults_PageIndexChanging");
                grd_SearchResults.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit SearchSimilarRequestsSCE.grd_ApprovedResults_PageIndexChanging");
            }
        }
    }
}
