using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class Procedure_TechCommitteListing : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.Session["PADisplayRequestId"] != null)
                {
                    grd_Procedures.PageSize = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "Procedures"));

                    bool userExists = HelperMethods.InGroup(Common.Utilities.Constants.TechnicalCommitteeGroupName);
                    if (Utilities.BusinessHelper.GetAssignee(Page.Session["PADisplayRequestId"].ToString()) && userExists)
                        lnk_AddNewProcedurePopUp.Visible = true;
                    BindGrid(Page.Session["PADisplayRequestId"].ToString());
                }
            }
        }

        private void BindGrid(string ReqID)
        {
            try
            {
                Logging.GetInstance().Debug("Enter Procedure_TechCommitteListing.BindGrid");
                List<Entities.Procedures> Procedures = BL.AllProcedures.GetProcedurebyReqID(Utilities.Constants.PATechCommitteeProcedures, ReqID);
                HelperMethods.BindGridView(grd_Procedures, Procedures);
                if (Procedures.Count > 0)
                {
                    lbl_NoOfRequests.Text = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "NoOfRequests", (uint)LCID) + Procedures.Count;
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
                Logging.GetInstance().Debug("Exit Procedure_TechCommitteListing.BindGrid");
            }
        }

        protected void grd_Procedures_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Logging.GetInstance().Debug("Enter Procedure_TechCommitteListing.grd_Procedures_PageIndexChanging");
            try
            {
                grd_Procedures.PageIndex = e.NewPageIndex;
                BindGrid(Page.Session["PADisplayRequestId"].ToString());
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit Procedure_TechCommitteListing.grd_Procedures_PageIndexChanging");
            }
        }

        protected void lnk_View_Click(object sender, EventArgs e)
        {
            Logging.GetInstance().Debug("Enter Procedure_TechCommitteListing.lnk_View_Click");
            try
            {
                if (!IsRefresh)
                {
                    LinkButton lnkButton = (LinkButton)sender;
                    GridViewRow gvr = (GridViewRow)lnkButton.NamingContainer;
                    //HiddenField hdn_RequestNumber = (HiddenField)gvr.FindControl("hdn_RequestID");
                    //Page.Session["PADisplayRequestId"] = hdn_RequestNumber.Value;
                    HiddenField hdn_ProcdId = (HiddenField)gvr.FindControl("hdn_Id");
                    Page.Session["hdn_ProcedureId"] = hdn_ProcdId.Value;
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAProcedureTechCommitteeDetails.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit Procedure_TechCommitteListing.lnk_View_Click");
            }
        }

        protected void lnk_AddNewProcedurePopUp_Click(object sender, EventArgs e)
        {
            Session["ActionTaken"] = "AddNewProcedure";
            Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/ITWORX.MOEHEWF.PA/TechnicalCommitte.aspx");
        }
    }
}