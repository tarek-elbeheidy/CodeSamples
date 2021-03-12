using Microsoft.SharePoint;
using MOEHE.PSPES.Models;
using MOEHE.PSPES.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Repository
{
    class TestResultRepository
    {

        public static async Task<DBOperationResult> Insert(TestResultModel smsHModel)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/TestResult/", smsHModel);
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        ReturnedResult = await res.Content.ReadAsAsync<DBOperationResult>();
                    }
                }
                catch
                {
                    //this mean service down (Server may be changed)
                    //MessageID will be Error

                }
                return ReturnedResult;
            }
        }



        public static async Task<List<TestResultModel>> GetTestResult(string AppRefNum,string QID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<TestResultModel> tRes = new List<TestResultModel>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetTestResult/{0}/{1}", AppRefNum,QID));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        tRes = await res.Content.ReadAsAsync<List<TestResultModel>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return tRes;
            }
        }


    }
}
