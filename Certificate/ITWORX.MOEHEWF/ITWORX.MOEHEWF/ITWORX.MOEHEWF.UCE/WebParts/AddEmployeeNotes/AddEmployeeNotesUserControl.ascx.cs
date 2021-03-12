using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.BL;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ITWORX.MOEHEWF.UCE.Utilities;
using System.Web;
using Microsoft.SharePoint;

namespace ITWORX.MOEHEWF.UCE.WebParts.AddEmployeeNotes
{
    public partial class AddEmployeeNotesUserControl : UserControlBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter AddEmployeeNotesUserControl.Page_Load");
                if (!Page.IsPostBack)
                {

                    //lbl_SuccessMessage.Visible = false;
                    if (Page.Session["DisplayRequestId"] != null)
                    {
                        int requestId = int.Parse(Convert.ToString(Page.Session["DisplayRequestId"]));
                        Entities.Request requestItem = BL.Request.GetRequestByNumber(requestId, LCID);
                        if (requestItem != null)
                        {
                            rftDefaultValue.Text = HttpUtility.HtmlDecode(requestItem.Note);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit AddEmployeeNotesUserControl.Page_Load");
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Enter AddEmployeeNotesUserControl.Page_Load");
                if (Page.Session["DisplayRequestId"] != null)
                {
                    int requestId = int.Parse(Convert.ToString(Page.Session["DisplayRequestId"]));
                    //Entities.Request requestItem = BL.Request.GetRequestByNumber(requestId, LCID);
                    string Note = HttpUtility.HtmlEncode(rftDefaultValue.Text);
                    ITWORX.MOEHEWF.UCE.BL.Request.UpdateRequestNote(requestId, Note);
                    lbl_SuccessMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit AddEmployeeNotesUserControl.Page_Load");
            }
        }
    }
}
