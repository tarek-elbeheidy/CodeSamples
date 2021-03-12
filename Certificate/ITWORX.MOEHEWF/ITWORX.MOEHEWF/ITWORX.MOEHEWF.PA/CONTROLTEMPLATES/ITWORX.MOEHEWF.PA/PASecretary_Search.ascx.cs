using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class Secretary_Search : UserControlBase
    {
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                grd_Requests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "DashboardPageSize"));

                BindDropDowns();
                BindGridOnSearch();
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            BindGridOnSearch();
            SrchControls.Visible = true;
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearControls();
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

        protected void lnk_View_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter SearchRequests.lnk_View_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_RequestID = (HiddenField)gvr.FindControl("hdn_ID");
                Page.Session["PADisplayRequestId"] = hdn_RequestID.Value;
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdn_RequestStatusId");
                Common.Entities.RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                string viewLink = string.Empty;
                if (requestStatus.CanReviewerEditRequest)
                    viewLink = requestStatus.ReviewerTargetPageURL;

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

        #endregion Events

        #region Methods

        private void BindGridOnSearch()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method SearchRequests.BindGridOnSearch");
                            List<string> objColumns = new List<string>();

                            if (drp_RequestStatus.SelectedIndex != 0 & drp_RequestStatus.SelectedIndex != -1)
                            {
                                if (LCID == (int)Language.English)
                                    objColumns.Add("RequestStatusId;Lookup;Eq;" + drp_RequestStatus.SelectedValue.ToString());
                                else
                                    objColumns.Add("RequestStatusId;Lookup;Eq;" + drp_RequestStatus.SelectedValue.ToString());
                            }
                            if (drp_EntityNeedsEquivalency.SelectedIndex != 0 & drp_EntityNeedsEquivalency.SelectedIndex != -1)
                            {
                                if (LCID == (int)Language.English)
                                    objColumns.Add("EntityNeedsEquivalency;Lookup;Eq;" + drp_EntityNeedsEquivalency.SelectedItem.Text.ToString());
                                else
                                    objColumns.Add("EntityNeedsEquivalencyAr;Lookup;Eq;" + drp_EntityNeedsEquivalency.SelectedItem.Text.ToString());
                            }
                            if (drp_Certificate.SelectedIndex != 0 & drp_Certificate.SelectedIndex != -1)
                            {
                                if (LCID == (int)Language.English)
                                    objColumns.Add("HighestCertificate;Lookup;Eq;" + drp_Certificate.SelectedItem.Text.ToString());
                                else
                                    objColumns.Add("HighestCertificateAr;Lookup;Eq;" + drp_Certificate.SelectedItem.Text.ToString());

                            }
                            if (drp_TheEquationSent.SelectedIndex != 0 & drp_TheEquationSent.SelectedIndex != -1)
                            {
                                if (LCID == (int)Language.English)
                                    objColumns.Add("OrgBookReply;Boolean;Eq;" + drp_Certificate.SelectedItem.Text.ToString());
                                else
                                    objColumns.Add("OrgBookReply;Boolean;Eq;" + drp_Certificate.SelectedItem.Text.ToString());
                            }
                            if (txt_RequestID.Text != "")
                            {
                                if (LCID == (int)Language.English)
                                    objColumns.Add("RequestNumber;Text;Eq;" + txt_RequestID.Text);
                                else
                                    objColumns.Add("RequestNumber;Text;Eq;" + txt_RequestID.Text);

                            }
                            if (txt_NationalID.Text != "")
                            {
                                if (LCID == (int)Language.English)
                                    objColumns.Add("Applicants_QatarID;Text;Eq;" + txt_NationalID.Text);
                                else
                                    objColumns.Add("Applicants_QatarID;Text;Eq;" + txt_NationalID.Text);

                            }
                            if (txt_ApplicantName.Text != "")
                            {
                                if (LCID == (int)Language.English)
                                    objColumns.Add("Applicants_ApplicantName;Text;Eq;" + txt_ApplicantName.Text.ToLower());
                                else
                                    objColumns.Add("Applicants_ApplicantName;Text;Eq;" + txt_ApplicantName.Text.ToLower());

                            }
                            List<Entities.SimilarRequest> srchRequests;

                            if (objColumns.Count > 0)
                                srchRequests = BL.PASearchSimilarRequests.GetAllRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(objColumns, "And", true) + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();
                            else
                                srchRequests = BL.PASearchSimilarRequests.GetAllRequests("<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();

                            HelperMethods.BindGridView(grd_Requests, srchRequests);
                            if (srchRequests.Count > 0)
                            {
                                SrchControls.Visible = true;
                                lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NoOfRequests", (uint)LCID) + srchRequests.Count;
                                lbl_NoOfRequests.Visible = true;

                            }
                            else
                            {
                                SrchControls.Visible = false;
                                lbl_NoOfRequests.Visible = false;

                            }

                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method SimilarRequests.BindGridOnSearch");
                        }
                    }
                }
            });
        }

        private void BindDropDowns()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method SearchRequests.BindDropDowns");

                //Bind Request Status
                List<Common.Entities.RequestStatus> requestStatusItems = Common.BL.RequestStatus.GetDistinctReviewerStatus(LCID);
                if (requestStatusItems != null && requestStatusItems.Count > 0)
                {
                    HelperMethods.BindDropDownList(ref drp_RequestStatus, requestStatusItems, "ID", "ApplicantDescriptionAr", "ApplicantDescriptionEn", LCID);
                    drp_RequestStatus.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));
                }

                //Bind Certificates
                HelperMethods.BindDropDownList(ref drp_Certificate, BusinessHelper.GetLookupData(Utilities.Constants.Certificates), "ID", "TitleAr", "Title", LCID);
                drp_Certificate.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));

                // Bind Entity Needs Equivalency
                HelperMethods.BindDropDownList(ref drp_EntityNeedsEquivalency, BusinessHelper.GetLookupData(Utilities.Constants.EntityNeedsEquivalency), "ID", "TitleAr", "Title", LCID);
                drp_EntityNeedsEquivalency.Items.Insert(0, new ListItem(HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ChooseValue", (uint)LCID), "-1"));
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method SearchRequests.BindDropDowns");
            }
        }

        private void ClearControls()
        {
            drp_Certificate.ClearSelection();

            drp_EntityNeedsEquivalency.ClearSelection();

            drp_RequestStatus.ClearSelection();

            txt_NationalID.Text = string.Empty;
            txt_RequestID.Text = string.Empty;
            txt_ApplicantName.Text = string.Empty;

            BindGridOnSearch();
        }


        #endregion Methods

    }
}