using System;
using ITWORX.MOEHE.Utilities.Logging; 

namespace ITWORX.MOEHEWF.PASuspendedRequests.Utilities
{
    public class Helper
    {
        public static string GetConfigValue(string key)
        {
            string value = string.Empty;
            try
            {
                value = System.Configuration.ConfigurationManager.AppSettings[key];
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            return value;
        }
    }
}
