using ITWORX.MOEHE.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.PASuspendedRequests
{
    class PASuspendedRequestsLogger
    {
        /// <summary>
        /// Defines the  name of the default logger 
        /// The default logger should be defined as Logger in log4net configuration file
        /// </summary>
        private const string DefaultLoggerName = "PAMOEHELogger";

        /// <summary>
        /// The logging instance of logger.
        /// </summary>
        private static ITWORX.MOEHE.Utilities.Logging.ILogger loggerInstance = null;

        /// <summary>
        /// Get an instance of the ILogger that is used to log.
        /// </summary>
        /// <returns>An instance of ILogger interface to be used for logging perposes.</returns>
        public static ITWORX.MOEHE.Utilities.Logging.ILogger GetInstance()
        {
            if (loggerInstance == null)
            {
                //get the log instance under elevated permissions so to allow it to write and read from different files.

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["PAMOEHELogger"]))
                    loggerInstance = new Logger(ConfigurationManager.AppSettings["PAMOEHELogger"], false);
                else
                    loggerInstance = new Logger(DefaultLoggerName, false);

            }

            return loggerInstance;
        }
    }
}
