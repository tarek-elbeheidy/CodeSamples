using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.BL
{
    public static class CalculatedDetailsForCertificate
    {
        public static void DeleteRequestCalculatedDetailsForCertificate(int requestNumber,string requestType)
        {
            Logging.GetInstance().Debug("Entering CalculatedDetailsForCertificate.DeleteRequestCalculatedDetailsForCertificate");
            SPWeb web = null;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPQuery universitiesQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                requestNumber + "</Value></Eq></Where>");

                          

                            SPList calculatedDataList = null;
                            if (requestType == RequestType.CertificateEquivalency.ToString())
                            {
                                calculatedDataList = web.Lists[Constants.CalculatedDetailsForCertificate];
                            }
                            else
                            {
                                calculatedDataList = web.Lists[Constants.PACalculatedDetailsForCertificate];
                            }
                            SPListItemCollection calculatedCollection = calculatedDataList.GetItems(universitiesQuery);
                            web.AllowUnsafeUpdates = true;
                            if (calculatedCollection != null && calculatedCollection.Count > 0)
                            {
                                foreach (SPListItem item in calculatedCollection)
                                {
                                    calculatedDataList.Items.DeleteItemById(item.ID);
                                }
                            }

                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                web.AllowUnsafeUpdates = false;
                Logging.GetInstance().Debug("Exit CalculatedDetailsForCertificate.DeleteRequestCalculatedDetailsForCertificate");
            }

        }
        public static int GetCountOfAcceptedHours(string requestNumber, int LCID)
        {
            Logging.GetInstance().Debug("Entering CalculatedDetailsForCertificate.GetCalculatedDetailsForCertificate");
            SPListItemCollection calculatedCollection = null;
            int AcceptedHours = 0;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPQuery universitiesQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" + requestNumber + "</Value></Eq></Where>");

                            SPList calculatedDataList = web.Lists[Constants.PACalculatedDetailsForCertificate];
                            calculatedCollection = calculatedDataList.GetItems(universitiesQuery);
                            if (calculatedCollection != null || calculatedCollection.Count > 0)
                            {
                                foreach (SPListItem item in calculatedCollection)
                                {
                                    AcceptedHours = item["AcceptedHours"] != null ? Convert.ToInt32(item["AcceptedHours"]) + AcceptedHours : AcceptedHours;
                                }
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit CalculatedDetailsForCertificate.GetCalculatedDetailsForCertificate");
            }
            return AcceptedHours;
        }

    }
}
