using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Web;
using System.Web.UI;

namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class ProgramManagerProcedureDetails : UserControlBase
    {
        protected global::ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload ProcedureProgramManagerAttachements;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.Session["hdn_ProcedureId"] != null)
                {
                    BindProcedure(Page.Session["hdn_ProcedureId"].ToString());
                 
                }
            }
            if (Page.Session["DisplayRequestId"] != null)
                BindAttachments(Convert.ToInt32(Page.Session["DisplayRequestId"]));
        }

        private void BindProcedure(string ProcID)
        {
            try
            {
                Logging.GetInstance().Debug("Enter ProgramManagerProcedureDetails.BindProcedure");
                Entities.Procedures Procedure = BL.AllProcedures.GetProgramManagerProcedurebyID(ProcID);
                if (Procedure != null)
                {
                    lbl_ProcedureVal.Text = Procedure.Procedure;
                    lbl_ProcedureCommentsVal.Text = Procedure.ProcedureComments;
                    lbl_ProcedureCreatedByVal.Text = Procedure.ProcedureCreatedby;
                    lbl_ProcedureDateVal.Text = Procedure.ProcedureDate;
                    lbl_RequestIDVal.Text = Procedure.RequestID;
                    if (!string.IsNullOrEmpty(Procedure.EmpAssignedTo))
                    {
                        lbl_EmpAssignedToVal.Text = Procedure.EmpAssignedTo;
                        lbl_EmpAssignedToVal.Visible = true;
                        lbl_EmpAssignedTo.Visible = true;
                    }
                    else
                    {
                        lbl_EmpAssignedToVal.Visible = false;
                        lbl_EmpAssignedTo.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ProgramManagerProcedureDetails.BindProcedure");
            }
        }

        protected void btn_Close_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsRefresh)
                {
                    Logging.GetInstance().Debug("Enter ProgramManagerProcedureDetails.btn_Close_Click");

                    Page.Session["hdn_ProcedureId"] = null;
                    //Response.Redirect("/en/Pages/ProgramManagerProcedureListing.aspx");
                    SPUtility.Redirect(SPContext.Current.Web.Url + "/Pages/ProgramManagerProcedureListing.aspx", SPRedirectFlags.DoNotEndResponse, HttpContext.Current);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ProgramManagerProcedureDetails.btn_Close_Click");
            }
        }

        private void BindAttachments(int requestId)
        {
            Logging.GetInstance().Debug("Entering method ProgramManagerProcedureDetails.BindAttachments");
            try
            {
                ProcedureProgramManagerAttachements.DocumentLibraryName = Utilities.Constants.ProcedureProgramManagerAttachements;
                ProcedureProgramManagerAttachements.DocLibWebUrl = SPContext.Current.Site.Url;
                ProcedureProgramManagerAttachements.Group = "ProcedureProgramManagerAttachements";
                ProcedureProgramManagerAttachements.LabelDisplayName = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "Attachments", (uint)LCID);
                ProcedureProgramManagerAttachements.RequestID = requestId;
                ProcedureProgramManagerAttachements.Enabled = false;
                //ProcedureProgramManagerAttachements.LookupFieldName = "RequestID";
                ProcedureProgramManagerAttachements.Bind();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method ProgramManagerProcedureDetails.BindAttachments");
            }
        }
    }
    }