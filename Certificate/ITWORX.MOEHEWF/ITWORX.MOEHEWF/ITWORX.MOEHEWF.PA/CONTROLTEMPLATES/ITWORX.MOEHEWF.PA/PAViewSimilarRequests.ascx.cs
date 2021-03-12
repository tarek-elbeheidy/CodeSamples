using ITWORX.MOEHEWF.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint;
using System.Web;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class PAViewSimilarRequests : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grd_SimilarRequests.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "SimilarRequestsPageSize"));
                if (Page.Session["PADisplayRequestId"] != null)
                {
                    BindGrid();
                }
            }
        }

        private void BindGrid()
        {
            Logging.GetInstance().Debug("Enter PAViewSimilarRequests.lnk_Request_Click");
            try
            {
                List<Entities.SimilarRequest> SimilarRequests = BL.PASearchSimilarRequests.GetSimilarRequstsbyReqId(BL.PASearchSimilarRequests.GetCheckedRequests(int.Parse(Page.Session["PADisplayRequestId"].ToString())), LCID).ToList();
                HelperMethods.BindGridView(grd_SimilarRequests, SimilarRequests);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PAViewSimilarRequests.lnk_Request_Click");
            }
        }

        protected void lnk_Request_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter PAViewSimilarRequests.lnk_Request_Click");
            try
            {

                if (!IsRefresh)
                {
                    LinkButton lnkButton = (LinkButton)sender;
                    GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                    HiddenField hdn_ProcdId = (HiddenField)gvr.FindControl("hdn_Id");
                    Page.Session["hdn_SimilarReqID"] = hdn_ProcdId.Value;
                    if (Page.Session["hdn_SimilarReqID"] != null)
                        //Response.Redirect((LCID == 1033 ? "/en" : "/ar") + "/Pages/PAReviewerDisplaySimilarRequestDetails.aspx");
                        SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAReviewerDisplaySimilarRequestDetails.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PAViewSimilarRequests.lnk_Request_Click");
            }
        }
        protected void grd_SimilarRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter SimilarRequests.grd_SimilarRequests_PageIndexChanging");
            try
            {
                grd_SimilarRequests.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit SimilarRequests.grd_SimilarRequests_PageIndexChanging");
            }
        }
    }
}
