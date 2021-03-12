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
    public class MOE_WITHDRAWAL_DATA_Repository
    {
        public static async Task<MOE_WITHDRAWAL_DATA_Model> GetWithdrawalByQID(string QID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                MOE_WITHDRAWAL_DATA_Model oModel = new MOE_WITHDRAWAL_DATA_Model();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetWithdrawalByQId/{0}", QID));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        oModel = await res.Content.ReadAsAsync<MOE_WITHDRAWAL_DATA_Model>();
                    }
                }
                catch (Exception ex)
                {
                    DBOperationResult resultsLog = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Withdrawal Application", Description = "GetWithdrawalByQId - Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }
                return oModel;
            }
        }
        public static async Task<MOE_WITHDRAWAL_DATA_Model> GetWithdrawalByRefId(string RefId)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                MOE_WITHDRAWAL_DATA_Model oModel = new MOE_WITHDRAWAL_DATA_Model();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetWithdrawalByRefId/{0}", RefId));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        oModel = await res.Content.ReadAsAsync<MOE_WITHDRAWAL_DATA_Model>();
                    }
                }
                catch (Exception ex)
                {
                    DBOperationResult resultsLog = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Withdrawal Application", Description = "GetWithdrawalByRefId - Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }
                return oModel;
            }
        }
        public static async Task<DBOperationResult> Insert(MOE_WITHDRAWAL_DATA_Model oModel)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult Results = new DBOperationResult();
                try
                {
                    DBOperationResult resultsLog = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Withdrawal Application", Description = "Start Inser", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/MOE_WITHDRAWAL_DATA/", oModel);
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Results = await res.Content.ReadAsAsync<DBOperationResult>();
                    }
                }
                catch (Exception ex)
                {
                    DBOperationResult resultsLog = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Withdrawal Application", Description = "Insert Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }
                return Results;
            }
        }

        public static async Task<bool> IsItemNotExists(string RefId)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                bool isExists = false;
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetWithdrawalByRefId/{0}", RefId));
                    MOE_WITHDRAWAL_DATA_Model oModel = new MOE_WITHDRAWAL_DATA_Model();

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        oModel = await res.Content.ReadAsAsync<MOE_WITHDRAWAL_DATA_Model>();
                        if (oModel == null)
                            isExists = true;
                    }
                }
                catch (Exception ex)
                {
                    DBOperationResult resultsLog = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Withdrawal Application", Description = "GetWithdrawalByRefId - Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }
                return isExists;
            }
        }
    }
}