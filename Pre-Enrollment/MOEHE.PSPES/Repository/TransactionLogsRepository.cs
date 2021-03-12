
using MOEHE.PSPES.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MOEHE.PSPES.Repository
{
    /// <summary>
    /// We use this class For TransactionLogs CRUD operations
    /// </summary>
    public class TransactionLogsRepository
    {
        ///// <summary>
        ///// We use this method to get all transactions items
        ///// </summary>
        ///// <returns></returns>
        //public static List<TransactionsLog> Get()
        //{
        //     using (HodhodSMSEntitiesOnline DB = new HodhodSMSEntitiesOnline())
        //    {
        //        List<TransactionsLog> All = DB.TransactionsLogs.Select(x => x).ToList(); 
        //        return All;
        //    }
        //}
        ///// <summary>
        ///// we use this method to get all transactions by userID
        ///// </summary>
        ///// <param name="UserID"></param>
        ///// <returns></returns>
        //public static List<TransactionsLog> GetByUserID(int UserID)
        //{
        //     using (HodhodSMSEntitiesOnline DB = new HodhodSMSEntitiesOnline())
        //    {
        //        List<TransactionsLog> All = DB.TransactionsLogs.Where(x => x.UserID == UserID).Select(x => x).ToList();
        //        return All;
        //    }
        //}

        ///// <summary>
        ///// we use this to get by userID and table name
        ///// </summary>
        ///// <param name="UserID"></param>
        ///// <param name="TableName"></param>
        ///// <returns></returns>
        //public static List<TransactionsLog> Get(int UserID, string TableName)
        //{
        //     using (HodhodSMSEntitiesOnline DB = new HodhodSMSEntitiesOnline())
        //    {
        //        List<TransactionsLog> All = DB.TransactionsLogs.Where(x => x.UserID == UserID && x.TableName == TableName).Select(x => x).ToList();

        //        return All;
        //    }
        //}


        /// <summary>
        /// we use this to insert new item
        /// </summary>
        /// <param name="transactionsLog"></param>
        public static async Task<DBOperationResult> Insert(TransactionsLog transactionsLog)
        {
            DBOperationResult ReturnedResult = new DBOperationResult();
            if (ConfigurationManager.AppSettings["LogChecker"] == "ON")
            {
                using (HttpClient cons = Utility.GetHttpClientConnection())
                {
                    
                    try
                    {
                        HttpResponseMessage res = await cons.PostAsJsonAsync("api/TransactionsLog", transactionsLog);
                        res.EnsureSuccessStatusCode();
                        if (res.IsSuccessStatusCode)
                        {
                            ReturnedResult = await res.Content.ReadAsAsync<DBOperationResult>();
                        }
                    }
                    catch(Exception ex)
                    {

                        string s = ex.Message;
                        //this mean service down (Server may be changed)
                        //MessageID will be Error

                    }
                    
                }
            }
            return ReturnedResult;
        }
    }
}
