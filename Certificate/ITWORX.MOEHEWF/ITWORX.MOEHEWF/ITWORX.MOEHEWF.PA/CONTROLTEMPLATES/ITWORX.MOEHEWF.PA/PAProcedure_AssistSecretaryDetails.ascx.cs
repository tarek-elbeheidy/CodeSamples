using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Web;
using System.Web.UI;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class PAProcedure_AssistSecretaryDetails : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.Session["hdn_ProcedureId"] != null)
                {
                    BindProcedure(Page.Session["hdn_ProcedureId"].ToString());
                }
            }
        }

        private void BindProcedure(string ProcID)
        {
            try
            {
                Logging.GetInstance().Debug("Enter PAProcedure_AssistSecretaryDetails.BindProcedure");
                Entities.Procedures Procedure = BL.AllProcedures.GetProcedurebyID(Utilities.Constants.PAAssistSecretaryProcedures, ProcID);
                if (Procedure != null)
                {
                    lbl_ProcedureCommentsVal.Text = Procedure.ProcedureComments;
                    lbl_ProcedureCreatedByVal.Text = Procedure.ProcedureCreatedby;
                    lbl_ProcedureDateVal.Text = Procedure.ProcedureDate;
                    lbl_RequestIDVal.Text = Procedure.RequestID;
                    lbl_ProcedureVal.Text = Procedure.Procedure;
                    if (!string.IsNullOrEmpty(Procedure.RejectionReason))
                    {
                        lbl_RejectionReasonVal.Text = Procedure.RejectionReason;
                        lbl_RejectionReason.Visible = true;
                        lbl_RejectionReasonVal.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PAProcedure_AssistSecretaryDetails.BindProcedure");
            }
        }

        protected void btn_Close_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsRefresh)
                {
                    Logging.GetInstance().Debug("Enter PAProcedure_AssistSecretaryDetails.btn_Close_Click");

                    Page.Session["hdn_ProcedureId"] = null;
                    // Response.Redirect("/en/Pages/PAProceduresAssistSecretaryListing.aspx");
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/PAProceduresAssistSecretaryListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PAProcedure_AssistSecretaryDetails.btn_Close_Click");
            }
        }
    }
}