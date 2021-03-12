using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.Common.WebParts.TrackingRequest
{
    public partial class TrackingRequestUserControl : UserControlBase
    {
        public string chartCountData { get; set; }
        protected string DeleteConfirmation = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter TrackingRequestUserControl.Page_Load");
            try
            {
                Session.Remove("EditRequestId");
                Session.Remove("DisplayRequestId");
                Session.Remove("PAEditRequestId");
                Session.Remove("PADisplayRequestId");
                DeleteConfirmation = HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "DeleteConfirmation", (uint)LCID);
                if (!Page.IsPostBack)
                {
                    string PageSize = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Utilities.Constants.HEWebUrl, Utilities.Constants.Configuration, "DashboardPageSize");
                    if (!string.IsNullOrEmpty(PageSize))
                    {
                        gridRequests.PageSize = int.Parse(PageSize);
                    }
                    else
                    {
                        gridRequests.PageSize = 10;
                       
                    }

                    PopulateDropdowns();
                    List<Entities.TrackingRequest> allTrackingRequests = BL.TrackingRequest.GetAllRequestsTracking(txtRequestNumber.Text.Trim(), !string.IsNullOrEmpty(dtSubmitDateFrom.Value) ? Convert.ToDateTime(DateTime.ParseExact(dtSubmitDateFrom.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) : DateTime.MinValue, !string.IsNullOrEmpty(dtSubmitDateTo.Value) ? Convert.ToDateTime(DateTime.ParseExact(dtSubmitDateTo.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) : DateTime.MinValue, dropRequestStatus.SelectedIndex > 0 ? dropRequestStatus.SelectedValue : string.Empty, dropRequestTypeSearch.SelectedIndex > 0 ? dropRequestTypeSearch.SelectedValue : string.Empty, dropFinalDecision.SelectedIndex > 0 ? dropFinalDecision.SelectedValue : string.Empty, LCID);
                   /* if (allTrackingRequests.Count == 0)
                    {
                        divSearch.Visible = false;
                        lblNoRequests.Visible = true;
                    }
                    else
                    {
                        divSearch.Visible = true;
                        lblNoRequests.Visible = false;*/
                        HelperMethods.BindGridView(gridRequests, allTrackingRequests);
                        lblNoOfTrackingRequestsValue.Text = allTrackingRequests.Count.ToString();
                  //  }
                    if (allTrackingRequests.Count >= Utilities.Constants.SearchLimit)
                    {
                        searchLimit.Visible = true;
                    }
                    else
                    {
                        searchLimit.Visible = false;
                    }
                    //RenderChart();
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit TrackingRequestUserControl.Page_Load");
            }
        }

        public void PopulateDropdowns()
        {
            Logging.GetInstance().Debug("Enter TrackingRequestUserControl.PopulateDropdowns");
            try
            {
                List<RequestTypes> requestTypesItems = BL.RequestTypes.GetAll();
                List<Entities.RequestStatus> requestStatusItems = BL.RequestStatus.GetDistinctStatusToApplicant();
                List<Entities.RequestStatus> requestFinalDecision = BL.RequestStatus.GetStatusFinalDecisionToApplicant();

                dropRequestType.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "selectService", (uint)LCID), string.Empty));
                dropRequestTypeSearch.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), string.Empty));
                if (requestTypesItems != null && requestTypesItems.Count > 0)
                {
                    dropRequestType.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref dropRequestType, requestTypesItems, "ID", "ArabicTitle", "EnglishTitle", LCID);

                    dropRequestTypeSearch.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref dropRequestTypeSearch, requestTypesItems, "ID", "ArabicTitle", "EnglishTitle", LCID);
                }
                dropRequestStatus.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), string.Empty));
                dropFinalDecision.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ChooseValue", (uint)LCID), string.Empty));
                if (requestStatusItems != null && requestStatusItems.Count > 0)
                {
                    dropRequestStatus.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref dropRequestStatus, requestStatusItems, "ID", "ApplicantDescriptionAr", "ApplicantDescriptionEn", LCID);

                    dropFinalDecision.AppendDataBoundItems = true;
                    HelperMethods.BindDropDownList(ref dropFinalDecision, requestFinalDecision, "ID", "FinalDecisionAr", "FinalDecisionEn", LCID);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit TrackingRequestUserControl.PopulateDropdowns");
            }
        }

        public void LoadRequestsData()
        {
            Logging.GetInstance().Debug("Enter TrackingRequestUserControl.LoadRequestsData");
            try
            {
                List<Entities.TrackingRequest> allTrackingRequests = BL.TrackingRequest.GetAllRequestsTracking(txtRequestNumber.Text.Trim(), !string.IsNullOrEmpty(dtSubmitDateFrom.Value) ? Convert.ToDateTime(DateTime.ParseExact(dtSubmitDateFrom.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) : DateTime.MinValue, !string.IsNullOrEmpty(dtSubmitDateTo.Value) ? Convert.ToDateTime(DateTime.ParseExact(dtSubmitDateTo.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) : DateTime.MinValue, dropRequestStatus.SelectedIndex > 0 ? dropRequestStatus.SelectedValue : string.Empty, dropRequestTypeSearch.SelectedIndex > 0 ? dropRequestTypeSearch.SelectedValue : string.Empty, dropFinalDecision.SelectedIndex > 0 ? dropFinalDecision.SelectedValue : string.Empty, LCID);
                HelperMethods.BindGridView(gridRequests, allTrackingRequests);
                lblNoOfTrackingRequestsValue.Text = allTrackingRequests.Count.ToString();
                if (allTrackingRequests.Count >= Utilities.Constants.SearchLimit)
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
                Logging.GetInstance().Debug("Exit TrackingRequestUserControl.LoadRequestsData");
            }
        }

        protected void btnChangeSearchCriteria_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter TrackingRequestUserControl.btnChangeSearchCriteria_Click");
            try
            {
                txtRequestNumber.Text = string.Empty;
                dtSubmitDateFrom.Value = string.Empty;
                dtSubmitDateTo.Value = string.Empty;
                dropRequestTypeSearch.ClearSelection();
                dropRequestStatus.ClearSelection();
                dropRequestType.ClearSelection();
                dropFinalDecision.ClearSelection();
                ViewState["Search"] = null;
                LoadRequestsData();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit TrackingRequestUserControl.btnChangeSearchCriteria_Click");
            }
        }

        protected void gridRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter TrackingRequestUserControl.gridRequests_PageIndexChanging");
            try
            {
                gridRequests.PageIndex = e.NewPageIndex;
                if (ViewState["Search"] == null)
                {
                    txtRequestNumber.Text = string.Empty;
                    dtSubmitDateFrom.Value = string.Empty;
                    dtSubmitDateTo.Value = string.Empty;
                    dropRequestTypeSearch.ClearSelection();
                    dropRequestStatus.ClearSelection();
                    dropRequestType.ClearSelection();
                    dropFinalDecision.ClearSelection();
                }
                LoadRequestsData();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit TrackingRequestUserControl.gridRequests_PageIndexChanging");
            }
        }

        protected void gridRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Logging.GetInstance().Debug("Enter TrackingRequestUserControl.gridRequests_RowDataBound");
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton deleteLink = (LinkButton)e.Row.FindControl("lnkDelete");
                    LinkButton editLink = (LinkButton)e.Row.FindControl("lnkEdit");
                    LinkButton displayLink = (LinkButton)e.Row.FindControl("lnkDisplay");
                    LinkButton lnk_FinalDecisionFile = (LinkButton)e.Row.FindControl("lnk_FinalDecisionFile");
                    Label lblRequestStatus = (Label)e.Row.FindControl("lblRequestStatus");
                    HiddenField hdnRequestStatusId = (HiddenField)e.Row.FindControl("hdnRequestStatusId");
                    HiddenField hdnRegisteredSchool = (HiddenField)e.Row.FindControl("hdnRegisteredSchool");

                    Entities.RequestStatus requestStatus = BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));

                    if (requestStatus.Code == Common.Utilities.RequestStatus.PAHeadManagerAccepted.ToString()
                        || requestStatus.Code == Common.Utilities.RequestStatus.UCEClosedByAcceptance.ToString()
                        || requestStatus.Code == Common.Utilities.RequestStatus.UCEClosedByRejection.ToString()
                        || requestStatus.Code==Common.Utilities.RequestStatus.SCESectionManagerAccepted.ToString()
                        || (!string.IsNullOrEmpty(hdnRegisteredSchool.Value) && int.Parse(hdnRegisteredSchool.Value) == 14
                        && requestStatus.Code == Common.Utilities.RequestStatus.SCESectionManagerRejected.ToString()))
                   
                    {
                        lnk_FinalDecisionFile.Visible = true;
                    }

                  
                       
                    if (lblRequestStatus.Text == "Draft" || lblRequestStatus.Text == "مسودة")
                    {
                        deleteLink.Visible = true;
                    }
                    else if (requestStatus.CanDeleteRequest)
                    {
                        deleteLink.Visible = true;
                    }
                    else
                    {
                        deleteLink.Visible = false;
                    }

                    if (requestStatus.CanApplicantEditRequest)
                    {
                        editLink.Visible = true;
                        displayLink.Visible = false;
                    }
                    else
                    {
                        editLink.Visible = false;
                        displayLink.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit TrackingRequestUserControl.gridRequests_RowDataBound");
            }
        }

        protected void lnk_Request_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter ClarRequestsListing.lnk_View_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdnRequestId = (HiddenField)gvr.FindControl("hdnRequestId");
                Page.Session["DisplayRequestId"] = hdnRequestId.Value;
                Page.Session["PADisplayRequestId"] = hdnRequestId.Value;
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdnRequestStatusId");
                Entities.RequestStatus requestStatus = BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                string displayLink = string.Empty;
                if (!requestStatus.CanApplicantEditRequest)
                {
                    displayLink = requestStatus.TargetPageUrl;
                }
                else if (requestStatus.CanApplicantEditRequest)
                    displayLink = requestStatus.TargetPageUrl;

                HiddenField hdnRequestTypeEnum = (HiddenField)gvr.FindControl("hdnRequestTypeEnum");
                if (RequestType.Schooling.ToString().Equals(hdnRequestTypeEnum.Value))
                {
                    SPUtility.Redirect(SPContext.Current.Web.Url + displayLink + "?RequestId=" + hdnRequestId.Value, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
                else
                {
                    SPUtility.Redirect(SPContext.Current.Web.Url + displayLink, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ClarRequestsListing.lnk_View_Click");
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter TrackingRequestUserControl.lnkEdit_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdnRequestId = (HiddenField)gvr.FindControl("hdnRequestId");
                Label lblRequestStatus = (Label)gvr.FindControl("lblRequestStatus");

                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdnRequestStatusId");
                Entities.RequestStatus requestStatus = BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));

                string editLink = string.Empty;
                if (requestStatus.CanApplicantEditRequest)
                {
                    editLink = requestStatus.TargetPageUrl;
                }

                Page.Session["EditRequestId"] = hdnRequestId.Value;
                Page.Session["PAEditRequestId"] = hdnRequestId.Value;

                HiddenField hdnRequestTypeEnum = (HiddenField)gvr.FindControl("hdnRequestTypeEnum");

                if (RequestType.Schooling.ToString().Equals(hdnRequestTypeEnum.Value))
                {
                    SPUtility.Redirect(SPContext.Current.Web.Url + editLink + "?RequestId=" + hdnRequestId.Value, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
                else
                {
                    SPUtility.Redirect(SPContext.Current.Web.Url + editLink, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }

               
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit TrackingRequestUserControl.lnkEdit_Click");
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter TrackingRequestUserControl.lnkDelete_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdnRequestId = (HiddenField)gvr.FindControl("hdnRequestId");
                int requestId = int.Parse(hdnRequestId.Value);
                HiddenField hdnRequestTypeEnum = (HiddenField)gvr.FindControl("hdnRequestTypeEnum");
                if (!string.IsNullOrEmpty(hdnRequestTypeEnum.Value))
                {
                    if (RequestType.CertificateEquivalency.ToString().Equals(hdnRequestTypeEnum.Value))
                    {
                        BL.DelegationDocuments.DeleteDelegationDocuments(requestId, RequestType.CertificateEquivalency.ToString());
                        BL.OrganizationlLettersAttachments.DeleteOrganizationalLetter(requestId);
                        BL.CalculatedDetailsForCertificateAttachments.DeleteCalculatedDetailsForCertificateAttachments(requestId, RequestType.CertificateEquivalency.ToString());
                        BL.CalculatedDetailsForCertificate.DeleteRequestCalculatedDetailsForCertificate(requestId, RequestType.CertificateEquivalency.ToString());
                        BL.CertificatesAttachments.DeleteCertificates(requestId);
                        BL.Request.DeleteRequest(requestId, RequestType.CertificateEquivalency.ToString());
                    }
                    else if (RequestType.PriorApproval.ToString().Equals(hdnRequestTypeEnum.Value))
                    {
                        BL.DelegationDocuments.DeleteDelegationDocuments(requestId, RequestType.PriorApproval.ToString());
                        BL.CalculatedDetailsForCertificateAttachments.DeleteCalculatedDetailsForCertificateAttachments(requestId, RequestType.PriorApproval.ToString());
                        BL.CalculatedDetailsForCertificate.DeleteRequestCalculatedDetailsForCertificate(requestId, RequestType.PriorApproval.ToString());
                        BL.Request.DeletePARequestsAttachments(requestId);
                        BL.Request.DeleteRequest(requestId, Utilities.RequestType.PriorApproval.ToString());
                    }
                    else if (RequestType.Schooling.ToString().Equals(hdnRequestTypeEnum.Value))
                    {

                        BL.Request.DeleteSCERequestsAttachments(requestId);
                        BL.Request.DeleteRequest(requestId, Utilities.RequestType.Schooling.ToString());
                    }
                }

                LoadRequestsData();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit TrackingRequestUserControl.lnkDelete_Click");
            }
        }

        protected void lnkDisplay_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter TrackingRequestUserControl.lnkDisplay_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdnRequestId = (HiddenField)gvr.FindControl("hdnRequestId");
                Page.Session["DisplayRequestId"] = hdnRequestId.Value;
                Page.Session["PADisplayRequestId"] = hdnRequestId.Value;
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdnRequestStatusId");
                Entities.RequestStatus requestStatus = BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                string displayLink = string.Empty;
                if (!requestStatus.CanApplicantEditRequest)
                {
                    displayLink = requestStatus.TargetPageUrl;
                }

               
                HiddenField hdnRequestTypeEnum = (HiddenField)gvr.FindControl("hdnRequestTypeEnum");

                if (RequestType.Schooling.ToString().Equals(hdnRequestTypeEnum.Value))
                {
                    SPUtility.Redirect(SPContext.Current.Web.Url + displayLink + "?RequestId=" + hdnRequestId.Value, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
                else
                {
                    SPUtility.Redirect(SPContext.Current.Web.Url + displayLink, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit TrackingRequestUserControl.lnkDisplay_Click");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter TrackingRequestUserControl.btnSearch_Click");
            try
            {
                ViewState["Search"] = true;
                LoadRequestsData();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit TrackingRequestUserControl.btnSearch_Click");
            }
        }

        #region PieChart

        /*
        public void RenderChart()
        {
            Logging.GetInstance().Debug("Enter TrackingRequestUserControl.RenderChart");
            try
            {
                List<object> chartData = BL.TrackingRequest.CreatePieChartList((uint)LCID);
                piechartLiteral.Text = BL.RequestsChart.ToPieChartSeries(chartData);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit TrackingRequestUserControl.RenderChart");
            }
        }
        */

        #endregion PieChart

        protected void lnk_FinalDecisionFile_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter RejectedRequests.lnk_Edit_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                LinkButton RequestNo = (LinkButton)gvr.FindControl("lblRequestNumber");
                HiddenField RequestID = (HiddenField)gvr.FindControl("hdnRequestId");
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdnRequestStatusId");
                HiddenField hdnRegisteredSchool = (HiddenField)gvr.FindControl("hdnRegisteredSchool");

                Entities.RequestStatus requestStatus = BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                if (requestStatus.Code == Common.Utilities.RequestStatus.SCESectionManagerAccepted.ToString()
                    || (!string.IsNullOrEmpty(hdnRegisteredSchool.Value) && int.Parse(hdnRegisteredSchool.Value) == 14
                        && requestStatus.Code == Common.Utilities.RequestStatus.SCESectionManagerRejected.ToString()))
                {
                   BL.FinalDecisionPrint.ViewAttachments(Utilities.Constants.SCERequests, false, int.Parse(RequestID.Value), Response);
                }

              

                else  if (requestStatus.Code == Common.Utilities.RequestStatus.PAHeadManagerAccepted.ToString() || requestStatus.Code == Common.Utilities.RequestStatus.PAHeadManagerRejected.ToString())
                {
                    List<Entities.Proc> RecommendProc = BL.Procedure.GetPAApprovedRecommendationStatus(RequestID.Value, "Approved");
                    if (RecommendProc.Count > 0)
                    {
                        BL.FinalDecisionPrint.PAPDFExportation(RequestNo.Text, "PDF", LCID);
                    }
                }
                else
                {
                    List<Entities.Proc> RecommendProc = BL.Procedure.GetApprovedRecommendationStatus(RequestID.Value, "Approved");
                    if (RecommendProc.Count > 0)
                    {
                        BL.FinalDecisionPrint.PDFExportation(RequestNo.Text, SPHttpUtility.HtmlDecode(RecommendProc[0].DecisionForPrint), RecommendProc[0].OccupationName.ToString(), ExtensionMethods.QatarFormatedDate(Convert.ToDateTime(RecommendProc[0].BookDate)), RecommendProc[0].BookNum, RecommendProc[0].HeadManagerName, RecommendProc[0].SirValue, RecommendProc[0].RespectedValue, "PDF", LCID);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit TrackingRequestUserControl.lnk_FinalDecisionFile_Click");
            }
        }

        protected void btnRequestType_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter TrackingRequestUserControl.btnRequestType_Click");
            try
            {
                if (int.Parse(dropRequestType.SelectedValue) == (int)RequestType.PriorApproval)
                {
                    //chech if current applicant is qatari or children of qatari if no hide pnlForm and display msg
                    var natinalityCategory = BL.RequestTypes.GetRequestTypeNationalityCategory(dropRequestType.SelectedValue);
                    //check for current user and if he is not allowed, send to the request to display msg
                    if (natinalityCategory != null && natinalityCategory.Count > 0)
                    {
                        Applicants applicant = BL.Applicants.GetApplicantProfilefromADByLoginName(SPContext.Current.Web.CurrentUser.LoginName);
                        List<Entities.TrackingRequest> allPARequests = BL.TrackingRequest.GetAllPARequestsTracking(string.Empty, DateTime.MinValue, DateTime.MinValue, string.Empty, string.Empty, string.Empty, LCID);
                        allPARequests = allPARequests.Where(x => x.Code == Common.Utilities.RequestStatus.PASubmitted.ToString()
                        || x.Code == Common.Utilities.RequestStatus.PAEmployeeNeedsClarification.ToString()
                        || x.Code == Common.Utilities.RequestStatus.PAEmployeeClarificationReplay.ToString()
                        || x.Code == Common.Utilities.RequestStatus.PAProgramManagerReviewRecommendation.ToString()
                        || x.Code == Common.Utilities.RequestStatus.PAProgramEmployeeMissingInformation.ToString()
                        || x.Code == Common.Utilities.RequestStatus.PAHeadManagerReviewRecommendation.ToString()
                        || x.Code == Common.Utilities.RequestStatus.PAHeadEmployeeMissingInformation.ToString()
                        || x.Code == Common.Utilities.RequestStatus.PAEmployeeReviewInformation.ToString()
                        || x.Code == Common.Utilities.RequestStatus.PACulturalMissionNeedsStatement.ToString()
                        || x.Code == Common.Utilities.RequestStatus.PACulturalMissionStatementReply.ToString()
                        || x.Code == Common.Utilities.RequestStatus.PAHigherEduInstitutesNeedsStatement.ToString()
                        || x.Code == Common.Utilities.RequestStatus.PAHigherEduInstitutesStatementReply.ToString()
                        || x.Code == Common.Utilities.RequestStatus.PAProgramManagerReject.ToString()
                        || x.Code == Common.Utilities.RequestStatus.PAProgramManagerAcceptance.ToString()).ToList();
                       
                        bool exist = false;
                        if (applicant != null)
                        {
                            exist = natinalityCategory.Exists(n => n.ID == int.Parse(applicant.NationalityCategory.SelectedID));
                        }
                        if (!exist)
                        {
                            lblSubmissionMsg.Text = HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "QatariPASubmissionMsg", (uint)LCID);
                            modalPopUpConfirmation.Show();
                        }
                        else if (allPARequests != null && allPARequests.Count > 0)
                        {
                            lblSubmissionMsg.Text = HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "OpenRequestPA", (uint)LCID);
                            modalPopUpConfirmation.Show();
                        }
                        else
                        {
                            SPUtility.Redirect(SPContext.Current.Web.Url + BL.RequestTypes.GetPageLink(dropRequestType.SelectedValue), SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                        }
                    }
                }
                else if (int.Parse(dropRequestType.SelectedValue) == (int)RequestType.CertificateEquivalency)
                {
                    List<Entities.TrackingRequest> allUCERequests = BL.TrackingRequest.GetAllUCERequestsTracking(string.Empty, DateTime.MinValue, DateTime.MinValue, string.Empty, string.Empty, string.Empty, LCID);
                    if (allUCERequests != null && allUCERequests.Count > 0)
                    {
                        allUCERequests = allUCERequests.Where(x => x.Code == Common.Utilities.RequestStatus.UCESubmitted.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEProgramManagerReview.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEProgramEmployeeReview.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEAsianAndEuropianEmployeeReview.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEHeadManagerReviewRecommendation.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEAsianAndEuropianEmployeeMissingInformation.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCProgramEmployeeClarificationReply.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEProgramEmployeeNeedsClarification.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEProgramManagerReviewRecommendation.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEProgramManagerMissingRecommendationFromHeadManager.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEHeadManagerAccepted.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEHeadManagerRejected.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEProgramEmployeeMissingInformation.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCECulturalMissionNeedsStatement.ToString()||
                        x.Code == Common.Utilities.RequestStatus.UCECulturalMissionStatementReply.ToString() ||
                        x.Code == Common.Utilities.RequestStatus.UCEHigherEduInstitutesNeedsStatement.ToString()||
                        x.Code == Common.Utilities.RequestStatus.UCEHigherEduInstitutesStatementReply.ToString()).ToList();
                        if (allUCERequests != null && allUCERequests.Count > 0)
                        {
                            lblSubmissionMsg.Text = HelperMethods.LocalizedText("ITWORX.MOEHEWF.Common", "OpenRequestUCE", (uint)LCID);
                            modalPopUpConfirmation.Show();
                        }
                        
                        else
                        {
                            SPUtility.Redirect(SPContext.Current.Web.Url + BL.RequestTypes.GetPageLink(dropRequestType.SelectedValue), SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                        }
                    }
                    else
                    {
                        SPUtility.Redirect(SPContext.Current.Web.Url + BL.RequestTypes.GetPageLink(dropRequestType.SelectedValue), SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                    }
                }
                else
                {
                    SPUtility.Redirect(SPContext.Current.Web.Url + BL.RequestTypes.GetPageLink(dropRequestType.SelectedValue), SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit TrackingRequestUserControl.btnRequestType_Click");
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