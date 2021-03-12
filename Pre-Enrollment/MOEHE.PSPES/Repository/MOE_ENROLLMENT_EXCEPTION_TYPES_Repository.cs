
using MOEHE.PSPES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Repository
{
    class MOE_ENROLLMENT_EXCEPTION_TYPES_Repository
    {

        public static async Task<List<MOE_Enrollment_Exception_Types>> Get()
        {
            List<MOE_Enrollment_Exception_Types> Exception_Types = new List<MOE_Enrollment_Exception_Types>();


            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                try
                {
                    //DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Guardian Data", Description = "Start GetStudentContactsInfo", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetExceptionTypes"));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Exception_Types = await res.Content.ReadAsAsync<List<MOE_Enrollment_Exception_Types>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                    //DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Guardian Data", Description = "GetStudentContactsInfo Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }

                return Exception_Types;
            }

        }
    }
}
