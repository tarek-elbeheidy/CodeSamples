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
    public partial class Procedure_ReceptionistDetails : UserControlBase
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
                Logging.GetInstance().Debug("Enter StatementDetails.BindProcedure");
                Entities.Procedures Procedure = BL.AllProcedures.GetProcedureReceptionistbyID(ProcID);
                if (Procedure != null)
                {
                  
                    lbl_ProcedureCommentsVal.Text = Procedure.ProcedureComments;
                    lbl_ProcedureCreatedByVal.Text = Procedure.ProcedureCreatedby;
                    lbl_ProcedureDateVal.Text = Procedure.ProcedureDate;
                    lbl_RequestIDVal.Text = Procedure.RequestID;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ProcedureDetails.BindProcedure");
            }
        }

        protected void btn_Close_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsRefresh)
                {
                    Logging.GetInstance().Debug("Enter ProcedureDetails.btn_Close_Click");

                    Page.Session["hdn_ProcedureId"] = null;
                    //Response.Redirect("/en/Pages/ProceduresReceptionistListing.aspx");
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ProceduresReceptionistListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit ProcedureDetails.btn_Close_Click");
            }
        }
    }
}
