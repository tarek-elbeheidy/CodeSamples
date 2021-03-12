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
    class MOE_ENROLLMENT_EXCEPTION_Repository
    {
        public static async Task<List<MOE_ENROLLMENT_EXCEPTION_Model>> GetEnrollmentException(string QID, int Term)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                List<MOE_ENROLLMENT_EXCEPTION_Model> studentExceptions = new List<MOE_ENROLLMENT_EXCEPTION_Model>();
                try
                {
                    //DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Guardian Data", Description = "Start GetStudentContactsInfo", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetEnrollmentException/{0}/{1}", QID,Term));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        studentExceptions = await res.Content.ReadAsAsync<List<MOE_ENROLLMENT_EXCEPTION_Model>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                    //DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Guardian Data", Description = "GetStudentContactsInfo Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }

                return studentExceptions;
            }
        }
        public static async Task<List<MOE_ENROLLMENT_EXCEPTION_Model>> GetEnrollmentExceptionBySchool(string QID, int Term,string SchoolCode)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                List<MOE_ENROLLMENT_EXCEPTION_Model> studentExceptions = new List<MOE_ENROLLMENT_EXCEPTION_Model>();
                try
                {
                    //DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Guardian Data", Description = "Start GetStudentContactsInfo", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetEnrollmentExceptionBySchool/{0}/{1}/{2}", QID, Term,SchoolCode));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        studentExceptions = await res.Content.ReadAsAsync<List<MOE_ENROLLMENT_EXCEPTION_Model>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                    //DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Guardian Data", Description = "GetStudentContactsInfo Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }

                return studentExceptions;
            }
        }

        public static async Task<MOE_ENROLLMENT_EXCEPTION_Model> GetEnrollmentExceptionBySchoolAndGrade(string QID, int Term, string SchoolCode, string Grade)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                MOE_ENROLLMENT_EXCEPTION_Model studentException = new MOE_ENROLLMENT_EXCEPTION_Model();
                try
                {
                    //DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Guardian Data", Description = "Start GetStudentContactsInfo", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetEnrollmentExceptionBySchoolAndGrade/{0}/{1}/{2}/{3}", QID, Term, SchoolCode,Grade));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        studentException = await res.Content.ReadAsAsync<MOE_ENROLLMENT_EXCEPTION_Model>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                    //DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Guardian Data", Description = "GetStudentContactsInfo Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }

                return studentException;
            }
        }

        public static async Task<DBOperationResult> Insert(MOE_ENROLLMENT_EXCEPTION_Model student_exception)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Start Application Data Insert", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/MOE_ENROLLMENT_EXCEPTION/", student_exception);
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
