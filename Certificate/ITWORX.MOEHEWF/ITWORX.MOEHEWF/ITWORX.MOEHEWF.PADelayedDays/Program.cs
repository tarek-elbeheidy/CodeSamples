using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHEWF.PADelayedDays.Utilities;
using Microsoft.SharePoint;
using System; 

namespace ITWORX.MOEHEWF.PADelayedDays
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PADelayedDaysLogger.GetInstance().Debug("Entering Method Program.Main, Namespace ITWORX.MOEHEWF.PADelayedDays");
                using (SPSite site = new SPSite(Helper.GetConfigValue("siteURL")))
                {
                    int ConfiguiredDelayedDays = Convert.ToInt32(HelperMethods.GetConfigurationValue(site.Url, Helper.GetConfigValue("ConfigurationList"), Helper.GetConfigValue("DelayedDays")));
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
                                        item["DelayedDays"] = (delayedDays - ConfiguiredDelayedDays).ToString();
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
                PADelayedDaysLogger.GetInstance().LogException(ex);
            }
            finally
            {
                PADelayedDaysLogger.GetInstance().Debug("Exit Method Program.Main, Namespace ITWORX.MOEHEWF.PADelayedDays");
            }
        }
    }
}
