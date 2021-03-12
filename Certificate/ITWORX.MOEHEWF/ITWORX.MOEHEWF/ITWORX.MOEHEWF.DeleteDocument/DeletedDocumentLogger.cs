using ITWORX.MOEHE.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.DeleteDocument
{
    static class DeletedDocumentLogger
    {
        private const string DefaultLoggerName = "MOEHELogger";

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

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MOEHELogger"]))
                    loggerInstance = new Logger(ConfigurationManager.AppSettings["MOEHELogger"], false);
                else
                    loggerInstance = new Logger(DefaultLoggerName, false);

            }

            return loggerInstance;
        }
    }
}

