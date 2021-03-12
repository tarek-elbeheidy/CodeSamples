using ITWORX.MOEHE.Utilities;
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
    public partial class Procedure_TechCommitteDetails : UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload TechCommitteeAttachements;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.Session["hdn_ProcedureId"] != null)
                {
                    BindProcedure(Page.Session["hdn_ProcedureId"].ToString());
                    BindAttachements();

                }
            }
        }

        private void BindProcedure(string ProcID)
        {
            try
            {
                Logging.GetInstance().Debug("Enter Procedure_TechCommitteDetails.BindProcedure");
                Entities.Procedures Procedure = BL.AllProcedures.GetProcedurebyID(Utilities.Constants.TechCommitteeProcedures, ProcID);
                if (Procedure != null)
                {

                    lbl_ProcedureCommentsVal.Text = Procedure.ProcedureComments;
                    lbl_ProcedureCreatedByVal.Text = Procedure.ProcedureCreatedby;
                    lbl_ProcedureDateVal.Text = Procedure.ProcedureDate;
                    lbl_RequestIDVal.Text = Procedure.RequestID;
                    lbl_ProcedureVal.Text = Procedure.Procedure;
                 
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit Procedure_TechCommitteDetails.BindProcedure");
            }
        }

        protected void btn_Close_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsRefresh)
                {
                    Logging.GetInstance().Debug("Enter Procedure_TechCommitteDetails.btn_Close_Click");

                    Page.Session["hdn_ProcedureId"] = null;
                    //Response.Redirect("/en/Pages/ProceduresTechCommitteListing.aspx");
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ProceduresTechCommitteListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit Procedure_TechCommitteDetails.btn_Close_Click");
            }
        }
        private void BindAttachements()
        {
            #region Prerequiestes
            /// add colum "RequestID" lookup from Requests
            /// add column "DocumentStatus" choice field, values: Saved,Uploaded,Deleted
            /// add column Group, single line of text
            #endregion

            #region Display Mode
            TechCommitteeAttachements.DocumentLibraryName = Utilities.Constants.TechCommitteeAttachements;
            TechCommitteeAttachements.DocLibWebUrl = SPContext.Current.Site.Url;

            TechCommitteeAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NotesPrepared", (uint)LCID);
            TechCommitteeAttachements.Group = "TechCommitteeAttachements";
            TechCommitteeAttachements.RequestID = Convert.ToInt32(Page.Session["DisplayRequestId"].ToString());
            TechCommitteeAttachements.Enabled = false;

            TechCommitteeAttachements.Bind();
            #endregion
        }

    }
}
