using ITWORX.MOEHE.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.PADelayedDays.Utilities
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
