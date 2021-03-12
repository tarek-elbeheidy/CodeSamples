using MOEHE.PSPES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Repository
{
    class ENROLLMENT_EXCEPTION_ATTACHMENT_Repository
    {

        public static async Task<List<Enrollment_Exception_Attachment_Model>> Get(int ExceptionID)
        {
            List<Enrollment_Exception_Attachment_Model> Exception_Attachments = new List<Enrollment_Exception_Attachment_Model>();


            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                try
                {
                    //DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Guardian Data", Description = "Start GetStudentContactsInfo", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetExceptionAttachments/{0}", ExceptionID));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Exception_Attachments = await res.Content.ReadAsAsync<List<Enrollment_Exception_Attachment_Model>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                    //DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Guardian Data", Description = "GetStudentContactsInfo Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }

                return Exception_Attachments;
            }
               
        }

        public static async Task<DBOperationResult> Insert(Enrollment_Exception_Attachment_Model Exception_Attachment)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    //DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Start Application Data Insert", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/Enrollment_Exception_Attachment/", Exception_Attachment);
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
                   // DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Application Data Insert Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;

                }
                return ReturnedResult;

            }
        }

    }
}
