using Microsoft.SharePoint;
using System;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHEWF.SCEDelayedDays.Utilities;

namespace ITWORX.MOEHEWF.SCEDelayedDays
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SCEDelayedDaysLogger.GetInstance().Debug("Entering Method Program.Main, Namespace ITWORX.MOEHEWF.SCEDelayedDays");
                using (SPSite site = new SPSite(Helper.GetConfigValue("siteURL")))
                {
                    int ConfiguiredDelayedDays = Convert.ToInt32(HelperMethods.GetConfigurationValue(site.Url, Helper.GetConfigValue("ConfigurationList"), Helper.GetConfigValue("SCEDelayedDays")));

                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList requestsList = web.Lists[Helper.GetConfigValue("SCERequestsList")];
                        if (requestsList != null)
                        {
                            var items = requestsList.GetItems(new SPQuery() {
                                ViewAttributes = "Scope=\"Recursive\""
                            });
                            foreach (SPListItem item in items)
                            {
                                DateTime actionDate = Convert.ToDateTime(item["RecievedDate"]);
                                if (actionDate != DateTime.MinValue)
                                {
                                    int delayedDays = Convert.ToInt32((DateTime.Now - Convert.ToDateTime(item["RecievedDate"])).TotalDays);
                                    if (delayedDays > Convert.ToInt32(ConfiguiredDelayedDays))
                                    {
                                        item["DelayedDays"] = delayedDays.ToString();
                                        item.Update();
                                    }
                                }
                            }
                        }

                        
                    }
                }
            }
            catch (Exception ex)
            {
                SCEDelayedDaysLogger.GetInstance().LogException(ex);
            }
            finally
            {
                SCEDelayedDaysLogger.GetInstance().Debug("Exit Method Program.Main, Namespace ITWORX.MOEHEWF.SCEDelayedDays");
            }
        }

    }
}
