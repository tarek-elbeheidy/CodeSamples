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
    class MOE_APPLICATION_DATA_Repository
    {
        public static async Task<List<ApplicationDataModel>> Get(SearchApplicationPageFilters applicationDataModel)
        {

            List<ApplicationDataModel> list = new List<ApplicationDataModel>();
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    string username = SPContext.Current.Site.RootWeb.CurrentUser.LoginName;
                    ReturnedResult = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Start Search, before calling API", UserID = username }).Result;
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/SearchApplications/", applicationDataModel);
                    try
                    {
                        ReturnedResult = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "before EnsureSuccessStatusCode", UserID = username }).Result;
                        res.EnsureSuccessStatusCode();
                        ReturnedResult = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "after EnsureSuccessStatusCode, reult code=" + res.StatusCode + ",content=" + res.Content, UserID = username }).Result;
                        if (res.IsSuccessStatusCode)
                        {
                            list = await res.Content.ReadAsAsync<List<ApplicationDataModel>>();
                        }
                    }
                    catch (Exception ex)
                    {
                        ReturnedResult = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Application Search Exception: " + ex.Message, UserID = username }).Result;//this mean service down (Server may be changed)
                    }



                }
                catch (Exception ex)
                {
                    string username = SPContext.Current.Site.RootWeb.CurrentUser.LoginName;
                    ReturnedResult = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Application Search Exception: " + ex.Message, UserID = username }).Result;//this mean service down (Server may be changed)
                }

                return list;
            }
        }

        public static async Task<int> GetCurrentWaitListNumber(int schoolYearID, string schoolCode, string Grade)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                int currentWaitListNumber = 0;
                try
                {
                    DBOperationResult ReturnedResult = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Start Get Current Waitlist Number", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetCurrentWaitListNumber/{0}/{1}/{2}", schoolYearID, schoolCode, Grade));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        currentWaitListNumber = await res.Content.ReadAsAsync<int>();
                    }
                }
                catch (Exception ex)
                {
                    DBOperationResult ReturnedResult = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Get Current Waitlist Number Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    //this mean service down (Server may be changed)
                }

                return currentWaitListNumber;
            }
        }


        public static async Task<MOE_APPLICATION_DATA_Model> GetBy(string ApplicantRefernceNumber)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Start Get by Ref Number", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                MOE_APPLICATION_DATA_Model aPPLICATION_DATA_Model = new MOE_APPLICATION_DATA_Model();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetAppliactionData/{0}", ApplicantRefernceNumber));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        aPPLICATION_DATA_Model = await res.Content.ReadAsAsync<MOE_APPLICATION_DATA_Model>();
                    }
                }
                catch (Exception ex)
                {
                    ReturnedResult = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Get by Ref Number Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    //this mean service down (Server may be changed)
                }

                return aPPLICATION_DATA_Model;
            }
        }

        public static async Task<MOE_APPLICATION_DATA_Model> GetByRefAndID(string ApplicantRefernceNumber, string QID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Start Get by Ref Number", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                MOE_APPLICATION_DATA_Model aPPLICATION_DATA_Model = new MOE_APPLICATION_DATA_Model();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetAppliactionDataByRefAndID/{0}/{1}", ApplicantRefernceNumber, QID));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        aPPLICATION_DATA_Model = await res.Content.ReadAsAsync<MOE_APPLICATION_DATA_Model>();
                    }
                }
                catch (Exception ex)
                {
                    ReturnedResult = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Get by Ref Number Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    //this mean service down (Server may be changed)
                }

                return aPPLICATION_DATA_Model;
            }
        }


        public static async Task<DBOperationResult> Insert(MOE_APPLICATION_DATA_Model appDataModel)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Start Application Data Insert", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/MOE_APPLICATION_DATA/", appDataModel);
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

        public static async Task<bool> CheckApplicationExist(int schoolYearID, string schoolCode, string Grade, string studentQID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                bool ApplicationExist = false;
                try
                {
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Check Application Exist ", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/CheckApplicationExist/{0}/{1}/{2}/{3}", schoolYearID, schoolCode, Grade, studentQID));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        ApplicationExist = await res.Content.ReadAsAsync<bool>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Check Application Exist Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }

                return ApplicationExist;
            }
        }
        #region To check finalized application added by Veer on 30 July
        public static async Task<MOE_APPLICATION_DATA_Model> FinalizedBySchool(string studentQID, string schoolCode, int year)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                MOE_APPLICATION_DATA_Model oModel = new MOE_APPLICATION_DATA_Model();
                try
                {
                    DBOperationResult log = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Get Finalized Application by School Code: ", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/FinalizedBySchool/{0}/{1}/{2}", studentQID, schoolCode, year));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        oModel = await res.Content.ReadAsAsync<MOE_APPLICATION_DATA_Model>();
                    }
                }
                catch (Exception ex)
                {
                    DBOperationResult log = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Data", Description = "Get Finalized Application by School Code: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }
                return oModel;
            }
        }
        #endregion
    }
}
