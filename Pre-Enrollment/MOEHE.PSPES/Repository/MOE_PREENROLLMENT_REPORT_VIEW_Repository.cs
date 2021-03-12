using Microsoft.SharePoint;
using MOEHE.PSPES.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Repository
{
    public class MOE_PREENROLLMENT_REPORT_VIEW_Repository
    {
        public static async Task<List<MOE_PREENROLLMENT_REPORT_VIEW_Model>> GetReportByQID(string QID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<MOE_PREENROLLMENT_REPORT_VIEW_Model> oModel = new List<MOE_PREENROLLMENT_REPORT_VIEW_Model>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetReportByQid/{0}", QID));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        oModel = await res.Content.ReadAsAsync<List<MOE_PREENROLLMENT_REPORT_VIEW_Model>>();
                    }
                }
                catch (Exception ex)
                {
                    DBOperationResult resultsLog = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Report", Description = "Report - Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }
                return oModel;

            }
        }
        public static async Task<DataTable> GetAllData(Dictionary<string, string> dictionary)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                DataTable oData = new DataTable();
                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await cons.PostAsync("api/GetReportData/", content);

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        oData = await res.Content.ReadAsAsync<DataTable>();
                    }
                }
                catch (Exception ex)
                {
                    DBOperationResult resultsLog = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Report", Description = "Report - Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }
                return oData;

            }
        }
    }
}
