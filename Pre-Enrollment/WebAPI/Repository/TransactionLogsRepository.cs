using MOEHE.PSPES.WebAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    /// <summary>
    /// We use this class For TransactionLogs CRUD operations
    /// </summary>
    public class TransactionLogsRepository
    {
        /// <summary>
        /// We use this method to get all transactions items
        /// </summary>
        /// <returns></returns>
        public static List<TransactionsLog> Get()
        {
             using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                List<TransactionsLog> All = DB.TransactionsLogs.Select(x => x).ToList(); 
                return All;
            }
        }
        /// <summary>
        /// we use this method to get all transactions by userID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static List<TransactionsLog> GetByUserID(string UserID)
        {
             using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                List<TransactionsLog> All = DB.TransactionsLogs.Where(x => x.UserID == UserID).Select(x => x).ToList();
                return All;
            }
        }

        /// <summary>
        /// we use this to get by userID and table name
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static List<TransactionsLog> Get(string UserID, string TableName)
        {
             using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                List<TransactionsLog> All = DB.TransactionsLogs.Where(x => x.UserID == UserID && x.TableName == TableName).Select(x => x).ToList();

                return All;
            }
        }
      

        /// <summary>
        /// we use this to insert new item
        /// </summary>
        /// <param name="transactionsLog"></param>
        public static void Insert(TransactionsLog transactionsLog)
        {
            try
            {
                if (ConfigurationManager.AppSettings["LogChecker"] == "ON")
                {

               
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {


                    TransactionsLog log = new TransactionsLog
                    {
                        TableName = transactionsLog.TableName,
                        ItemID = transactionsLog.ItemID,
                        UserID = transactionsLog.UserID,
                        ActionCode = transactionsLog.ActionCode,
                        CreatedDate = DateTime.Now,
                        Description = transactionsLog.Description,
                        ShortDescription = transactionsLog.ShortDescription

                    };

                    DB.TransactionsLogs.Add(log);
                    DB.SaveChanges();

                }
                }
            }
            catch(Exception ex) { string message = ex.Message; }
        }
    }
}
