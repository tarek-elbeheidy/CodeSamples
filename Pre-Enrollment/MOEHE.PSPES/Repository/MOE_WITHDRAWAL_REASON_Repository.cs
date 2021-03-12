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
    public class MOE_WITHDRAWAL_REASON_Repository
    {
        public static async Task<List<MOE_WITHDRAWAL_REASON_Model>> GetWithdrawalReasons()
        {
            //DBOperationResult ResultsLog = new DBOperationResult();
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ResultsLog = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Withdrawal Reasons", Description = "Start GetWithdrawalReasons", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                List<MOE_WITHDRAWAL_REASON_Model> oModel = new List<MOE_WITHDRAWAL_REASON_Model>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync("api/GetWithdrawalReasons");
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        oModel = await res.Content.ReadAsAsync<List<MOE_WITHDRAWAL_REASON_Model>>();
                    }
                }
                catch (Exception ex)
                {
                    ResultsLog = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Withdrawal Reasons", Description = " GetWithdrawalReasons Exception:" + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }
                return oModel;
            }
        }
       
    }
}