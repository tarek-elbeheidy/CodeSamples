using Microsoft.SharePoint;
using MOEHE.PSPES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Repository
{
    class MOE_HEALTH_DATA_Repository
    {
        public static async Task<MOE_HEALTH_DATA_Model> GetHealthInfoByQID(string QID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Health Data", Description = "Start GetHealthInfoByQID", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                MOE_HEALTH_DATA_Model health_Model = new MOE_HEALTH_DATA_Model();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetHealthInfo/{0}", QID));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        health_Model = await res.Content.ReadAsAsync<MOE_HEALTH_DATA_Model>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                    ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Health Data", Description = "GetHealthInfoByQID Exception: "+ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }

                return health_Model;
            }
        }

        public static async Task<MOE_HEALTH_DATA_Model> GetHealthInfoByRefernceNumber(string ReferenceNumber)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Health Data", Description = "Start GetHealthInfoByRef", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                MOE_HEALTH_DATA_Model health_Model = new MOE_HEALTH_DATA_Model();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetHealthInfoByRef/{0}", ReferenceNumber));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        health_Model = await res.Content.ReadAsAsync<MOE_HEALTH_DATA_Model>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                    ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Health Data", Description = "GetHealthInfoByQID Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }

                return health_Model;
            }
        }

        public static async Task<DBOperationResult> Insert(MOE_HEALTH_DATA_Model healthDataModel)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Health Data", Description = "Start Insert", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/MOE_HEALTH_DATA/", healthDataModel);
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        ReturnedResult = await res.Content.ReadAsAsync<DBOperationResult>();
                    }
                }
                catch(Exception ex)
                {
                    //this mean service down (Server may be changed)
                    //MessageID will be Error
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Health Data", Description = "Insert Exception: "+ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;

                }
                return ReturnedResult;
            }
        }
    }
}
