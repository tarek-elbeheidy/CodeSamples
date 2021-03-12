using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHEWF.DelayedDays.Utilities;
using Microsoft.SharePoint;
using System; 

namespace ITWORX.MOEHEWF.DelayedDays
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DelayedDaysLogger.GetInstance().Debug("Entering Method Program.Main, Namespace ITWORX.MOEHEWF.DelayedDays");
                using (SPSite site = new SPSite(Helper.GetConfigValue("siteURL")))
                {
                    int ConfiguiredDelayedDays = Convert.ToInt32(HelperMethods.GetConfigurationValue(site.Url, Helper.GetConfigValue("ConfigurationList"), Helper.GetConfigValue("DelayedDays")));
                    int ConfiguiredPADelayedDays = Convert.ToInt32(HelperMethods.GetConfigurationValue(site.Url, Helper.GetConfigValue("ConfigurationList"), Helper.GetConfigValue("PADelayedDays")));

                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList requestsList = web.Lists[Helper.GetConfigValue("RequestsList")];
                        if (requestsList != null)
                        {
                            var items = requestsList.GetItems();
                            foreach (SPListItem item in items)
                            {
                                DateTime actionDate = Convert.ToDateTime(item["ActionDate"]);
                                if (actionDate != DateTime.MinValue)
                                {
                                    int delayedDays = Convert.ToInt32((DateTime.Now - Convert.ToDateTime(item["ActionDate"])).TotalDays);
                                    if (delayedDays > Convert.ToInt32(ConfiguiredDelayedDays))
                                    {
                                        item["DelayedDays"] = delayedDays.ToString();
                                        if (item["TotalDelayedDays"] != null)
                                        {
                                            int result;
                                            int.TryParse(item["TotalDelayedDays"].ToString(), out result);
                                            if (result > 0)
                                                item["TotalDelayedDays"] = result + delayedDays;
                                            else
                                                item["TotalDelayedDays"] = delayedDays;
                                        }
                                        item.Update();

                                    }
                                }
                            }
                        }

                        SPList PARequestsList = web.Lists[Helper.GetConfigValue("PARequestsList")];
                        if (PARequestsList != null)
                        {
                            var items = PARequestsList.GetItems();
                            foreach (SPListItem item in items)
                            {
                                DateTime actionDate = Convert.ToDateTime(item["ActionDate"]);
                                if (actionDate != DateTime.MinValue)
                                {
                                    int delayedDays = Convert.ToInt32((DateTime.Now - Convert.ToDateTime(item["ActionDate"])).TotalDays);
                                    if (delayedDays > Convert.ToInt32(ConfiguiredDelayedDays))
                                    {
                                        item["DelayedDays"] = delayedDays.ToString();

                                        if (item["TotalDelayedDays"] != null)
                                        {
                                            int result;
                                            int.TryParse(item["TotalDelayedDays"].ToString(), out result);
                                            if (result > 0)
                                                item["TotalDelayedDays"] = result + delayedDays;
                                            else
                                                item["TotalDelayedDays"] = delayedDays;
                                        }

                                        item.Update();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                DelayedDaysLogger.GetInstance().LogException(ex);
            }
            finally
            {
                DelayedDaysLogger.GetInstance().Debug("Exit Method Program.Main, Namespace ITWORX.MOEHEWF.DelayedDays");
            }
        }
    }
}
