using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class Procedures_LegalAffairsListing : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.Session["DisplayRequestId"] != null)
                {
                    grd_Procedures.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "Procedures"));

                    if (Utilities.BusinessHelper.GetAssignee(Page.Session["DisplayRequestId"].ToString()))
                        lnk_AddNewProcedurePopUp.Visible = true;
                    BindGrid(Page.Session["DisplayRequestId"].ToString());
                }
            }
        }
        private void BindGrid(string ReqID)
        {
            try
            {
                Logging.GetInstance().Debug("Enter Procedures_LegalAffairsListing.BindGrid");
                List<Entities.Procedures> Procedures = BL.AllProcedures.GetProcedurebyReqID(Utilities.Constants.LegalOfficerProcedures, ReqID);
                HelperMethods.BindGridView(grd_Procedures, Procedures);
                if (Procedures.Count > 0)
                {
                    lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NoOfRequests", (uint)LCID) + Procedures.Count;
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
                Logging.GetInstance().Debug("Exit Procedures_LegalAffairsListing.BindGrid");
            }
        }
        protected void grd_Procedures_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter Procedures_LegalAffairsListing.grd_Procedures_PageIndexChanging");
            try
            {
                grd_Procedures.PageIndex = e.NewPageIndex;
                BindGrid(Page.Session["DisplayRequestId"].ToString());
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit Procedures_LegalAffairsListing.grd_Procedures_PageIndexChanging");
            }
        }
        protected void lnk_View_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter Procedures_LegalAffairsListing.lnk_View_Click");
            try
            {
                if (!IsRefresh)
                {
                    LinkButton lnkButton = (LinkButton)sender;
                    GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                    HiddenField hdn_ProcdId = (HiddenField)gvr.FindControl("hdn_Id");
                    Page.Session["hdn_ProcedureId"] = hdn_ProcdId.Value;
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/Procedure_LegalAffairsDetails.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit Procedures_LegalAffairsListing.lnk_View_Click");
            }
        }

        protected void lnk_AddNewProcedurePopUp_Click(object sender, EventArgs e)
        {
            Session["ActionTaken"] = "AddNewProcedure";
            Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.UCE/LegalAffairs.aspx");
        }
    }
}
