using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
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
    public partial class ApplicantRequestsClosed : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.Session["PADisplayRequestId"] != null && Page.Session["PAQID"] != null)
                {
                    Page.Session["CurrentPADisplayRequestId"] = Page.Session["PADisplayRequestId"];
                    grd_ApplicantRequestsClosed.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SimilarRequestsPageSize"));
                    BindGrid(Page.Session["PAQID"].ToString());
                    lbl_SuccessMsg.Visible = false;
                }
            }
        }

        private void BindGrid(string userName)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method ApplicantRequestsClosed.BindGrid");

                            List<Entities.SimilarRequest> srchRequests = BL.PASearchSimilarRequests.GetAllRequests("<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy><Where><Eq><FieldRef Name='ApplicantID' /><Value Type='Lookup'>" +
                         userName + "</Value></Eq></Where>", LCID).ToList();
                            srchRequests = srchRequests.Where(x => x.RequestStatusId == (int)Common.Utilities.RequestStatus.PAHeadManagerAccepted || x.RequestStatusId == (int)Common.Utilities.RequestStatus.PAHeadManagerRejected).ToList();
                            HelperMethods.BindGridView(grd_ApplicantRequestsClosed, srchRequests);
                            if (srchRequests.Count > 0)
                            {
                                lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NoOfRequests", (uint)LCID) + srchRequests.Count;
                                lbl_NoOfRequests.Visible = true;
                            }
                            else
                            {
                                lbl_NoOfRequests.Visible = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method ApplicantRequestsClosed.BindGrid");
                        }
                    }
                }
            });
        }
        protected void lnkDisplay_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter ApplicantRequestsClosed.lnkDisplay_Click");
            try
            {
                LinkButton lnkButton = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                HiddenField hdn_RequestID = (HiddenField)gvr.FindControl("hdn_ID");
                Page.Session["OldPADisplayRequestId"] = hdn_RequestID.Value;
                Page.Session["PADisplayRequestId"] = Page.Session["OldPADisplayRequestId"];
                HiddenField hdnRequestStatusId = (HiddenField)gvr.FindControl("hdn_RequestStatusId");
               
                Common.Entities.RequestStatus requestStatus = Common.BL.RequestStatus.GetRequestStatusById(int.Parse(hdnRequestStatusId.Value));
                string displayLink = string.Empty;
                if (!requestStatus.CanApplicantEditRequest)
                    displayLink = requestStatus.ReviewerTargetPageURL;

                SPUtility.Redirect(SPContext.Current.Web.Url + displayLink, SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ApplicantRequestsClosed.lnkDisplay_Click");
            }
        }
        protected void grd_ApplicantRequestsClosed_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter ApplicantRequestsClosed.grd_ApplicantRequestsClosed_PageIndexChanging");
            try
            {
                foreach (GridViewRow row in grd_ApplicantRequestsClosed.Rows)
                {
                    HiddenField reqID = (row.FindControl("hdn_ID") as HiddenField);

                    RemoveRowIndex(Convert.ToInt32(reqID.Value));// (container.DataItemIndex);
                }
                //grd_SimilarRequests.PageIndex = e.NewPageIndex;
                //BindGridOnSearch();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ApplicantRequestsClosed.grd_ApplicantRequestsClosed_PageIndexChanging");
            }
        }

        private List<Int32> SelectedRequestsIndex
        {
            get
            {
                if (ViewState["SELECTED_REQUESTS_INDEX"] == null)
                {
                    ViewState["SELECTED_REQUESTS_INDEX"] = new List<Int32>();
                }

                return (List<Int32>)ViewState["SELECTED_REQUESTS_INDEX"];
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
    }
}