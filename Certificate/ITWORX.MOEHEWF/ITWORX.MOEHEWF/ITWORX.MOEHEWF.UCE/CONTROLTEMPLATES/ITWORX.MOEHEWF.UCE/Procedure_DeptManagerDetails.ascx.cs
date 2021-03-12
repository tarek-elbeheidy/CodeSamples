using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class Procedure_DeptManagerDetails : UserControlBase
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
                Logging.GetInstance().Debug("Enter Procedure_DeptManagerDetails.BindProcedure");
                Entities.Procedures Procedure = BL.AllProcedures.GetProcedurebyID(Utilities.Constants.DepartmentManagerProcedures, ProcID);
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
                Logging.GetInstance().Debug("Exit Procedure_DeptManagerDetails.BindProcedure");
            }
        }

        protected void btn_Close_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsRefresh)
                {
                    Logging.GetInstance().Debug("Enter Procedure_DeptManagerDetails.btn_Close_Click");

                    Page.Session["hdn_ProcedureId"] = null;
                    //Response.Redirect("/en/Pages/ProceduresDeptManagerListing.aspx");
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ProceduresDeptManagerListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit Procedure_DeptManagerDetails.btn_Close_Click");
            }
        }
    }
}
