using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Web.UI;
using common = ITWORX.MOEHEWF.Common;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class PAPaymentRecords : UserControl
    {

        bool isApplicant
        {
            get { return HelperMethods.InGroup(common.Utilities.Constants.ApplicanstGroupName); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.Session["PADisplayRequestId"] != null)
                    BindFields(Page.Session["PADisplayRequestId"].ToString());
                else
                {
                    lbl_NoPayment.Visible = true;
                    paymentdata.Visible = false;
                }

                if(isApplicant)
                {
                    div_CardType.Visible = false;
                    div_CardNumber.Visible = false;
                }
            }
        }

        private void BindFields(string Reqid)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method PaymentsRecordsUserControl.BindFields");
                List<Entities.Payments> PR = BL.PAPaymentRecords.GetPAPaymentRecords(Reqid);
                if (PR.Count > 0)
                {
                    lbl_AmountValue.Text = PR[0].Amount;
                    lbl_CardNumberValue.Text = PR[0].CardNumber;
                    lbl_CardTypeValue.Text = PR[0].CardType;
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