using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using ITWORX.MOEHEWF.PA.BL;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class UploadUC : UserControlBase
    {
        public string libName
        {
            get
            {
                string result = string.Empty;

                if (Session["_libName"] != null)
                {
                    result = (string)Session["_libName"];
                }

                return result;
            }
            set
            {
                Session["_libName"] = value;
            }
        }
        public string reqID
        {
            get
            {
                string result = string.Empty;

                if (Session["_reqID"] != null)
                {
                    result = (string)Session["_reqID"];
                }

                return result;
            }
            set
            {
                Session["_reqID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                lbl_msgNotFound.Visible = false;
            }

        }
        private void BindGrid()
        {
            try
            {
                Logging.GetInstance().Debug("Entering method UploadUC.BindGrid");
                AllProcedures Proc = new AllProcedures();
                //grd_Files.DataSource = Proc.GetProceduresDocuments(libName, int.Parse(reqID));
                if (grd_Files.DataSource != null)
                    grd_Files.DataBind();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method SampleUserControl.Button1_Click");
            }
        }
        protected void btn_Upload_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method UploadUC.btn_Upload_Click");
                if (!IsRefresh)
                {
                    if (!uploadFile.HasFile)
                        lbl_msgNotFound.Visible = true;
                    else
                    {
                        string FileName = uploadFile.FileName;
                        //AllProcedures Proc = new AllProcedures();
                        //Proc.UploadDocument(FileName, reqID);
                        BindGrid();
                        lbl_msgNotFound.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method UploadUC.btn_Upload_Click");
            }
        }
        protected void grd_Files_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method UploadUC.grd_Files_RowCommand");
                if (!IsRefresh)
                {
                    if (e.CommandName == "delete")
                    {
                        int Index = Convert.ToInt32(e.CommandArgument.ToString());
                        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        string FileName = ((HiddenField)row.FindControl("hdn_FNameExt")).Value;
                        //AllProcedures Proc = new AllProcedures();
                        //Proc.DeleteDocument(libName, FileName);
                        BindGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
                //handle exception as the business require.
            }
            finally
            {
                //dispose any objects that require dispose.
                Logging.GetInstance().Debug("Exiting method UploadUC.grd_Files_RowCommand");
            }
        }

    }
}
