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
    class MOE_BIO_DATA_Repository
    {
        public static async  Task<MOE_BIO_DATA_Model> GetBioDataByQID(string QID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                MOE_BIO_DATA_Model Bio_Model = new MOE_BIO_DATA_Model();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync("api/GetBioData/"+QID);

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Bio_Model = await res.Content.ReadAsAsync<MOE_BIO_DATA_Model>();
                    }
                }
                catch(Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return Bio_Model;
            }
        }

        public static async Task<DBOperationResult> Insert(MOE_BIO_DATA_Model bio_Model)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    //DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Start Application Data Insert", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/InsertBioData/", bio_Model);
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        ReturnedResult = await res.Content.ReadAsAsync<DBOperationResult>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                    //MessageID will be Error
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Application Data Insert Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;

                }
                return ReturnedResult;
            }
        }
    }
}
