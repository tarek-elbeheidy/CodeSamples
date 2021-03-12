using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using common = ITWORX.MOEHEWF.Common;



namespace ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE
{
    public partial class PaymentRecords : UserControl
    {
 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.Session["DisplayRequestId"] != null)
                    BindFields(Page.Session["DisplayRequestId"].ToString());
                else
                {
                    lbl_NoPayment.Visible = true;
                    paymentdata.Visible = false;

                }

 
            }
        }
        private void BindFields(string Reqid)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method PaymentsRecordsUserControl.BindFields");
                List<Entities.Payments> PR = BL.PaymentRecords.GetPaymentRecords(Reqid);
                if (PR.Count > 0)
                {
                    PR = PR.Where(a => a.ReasonCode == "100" || a.ResponseMessage == "Request was processed successfully.").ToList();
                    if (PR.Count > 0)
                    {
                     lbl_AmountValue.Text = PR[0].Amount;
                    lbl_ReceiptDateValue.Text = PR[0].ReceiptDate.ToShortDateString();
                    lbl_ReceiptNumberValue.Text = PR[0].ReceiptNumber;
                    lbl_StatementValue.Text = PR[0].Statement;
                    lbl_NoPayment.Visible = false;
                    paymentdata.Visible = true;
                }
                    else
                    {
                        lbl_NoPayment.Visible = true;
                        paymentdata.Visible = false;
                    }
                }
               
                else
                {
                    lbl_NoPayment.Visible = true;
                    paymentdata.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PaymentsRecordsUserControl.BindFields");
            }
        }
    }
}