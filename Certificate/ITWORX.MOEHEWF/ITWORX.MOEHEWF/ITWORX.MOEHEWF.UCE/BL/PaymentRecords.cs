using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITWORX.MOEHEWF.UCE.Entities;

namespace ITWORX.MOEHEWF.UCE.BL
{
    public class PaymentRecords
    {
        public static List<Payments> GetPaymentRecords(string reqID)
        {
            List<Payments> Payment = new List<Payments>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method PaymentRecords.GetPaymentRecords");
                            SPList list = web.Lists[Utilities.Constants.PaymentRecords];
                            var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                reqID + "</Value></Eq></Where><OrderBy><FieldRef Name='ReceiptDate' Ascending='False' /></OrderBy>");

                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                    Payment.Add(new Payments()
                                    {
                                        RequestID = RequestID.LookupValue,
                                        Amount = item["Amount"] == null ? string.Empty : item["Amount"].ToString(),
                                        CardNumber = item["CardNumber"] == null ? string.Empty : item["CardNumber"].ToString(),
                                        CardType = item["CardType"] == null ? string.Empty : item["CardType"].ToString(),
                                        ReceiptDate = item["ReceiptDate"] == null ? DateTime.MinValue : DateTime.Parse(item["ReceiptDate"].ToString()),
                                        ReceiptNumber = item["ReceiptNumber"] == null ? string.Empty : item["ReceiptNumber"].ToString(),
                                        Statement = item["StatementSubject"] == null ? string.Empty : item["StatementSubject"].ToString(),
                                        ReasonCode= item["ReasonCode"] == null ? string.Empty : item["ReasonCode"].ToString(),
                                        ResponseMessage= item["ResponseMessage"] == null ? string.Empty : item["ResponseMessage"].ToString(),
                                    });
                                }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method PaymentRecords.GetPaymentRecords");
                        }
                    }
                }
            });
            return Payment;
        }

    }
}