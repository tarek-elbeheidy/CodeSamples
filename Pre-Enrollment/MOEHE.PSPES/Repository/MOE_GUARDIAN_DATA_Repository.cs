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
    class MOE_GUARDIAN_DATA_Repository
    {
        public static async Task<List<MOE_GUARDIAN_DATA_Model>> GetStudentContactsInfo(string QID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<MOE_GUARDIAN_DATA_Model> studentContacts = new List<MOE_GUARDIAN_DATA_Model>();
                try
                {
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Guardian Data", Description = "Start GetStudentContactsInfo", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetStudentContacts/{0}", QID));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        studentContacts = await res.Content.ReadAsAsync<List<MOE_GUARDIAN_DATA_Model>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Guardian Data", Description = "GetStudentContactsInfo Exception: "+ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }

                return studentContacts;
            }
        }

        public static async Task<DBOperationResult> Insert(MOE_GUARDIAN_DATA_Model_Not_Serializable guardianhDataModel)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Guardian Data", Description = "Start Insert", UserID = guardianhDataModel.MOE_USERID }).Result;
                    DBOperationResult ReturnedResult4 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Insert Guardian", Description = "Insert Guardian Data Values: MOE_TRANSACTION_DTTM ="+ DateTime.Now+",NATIONAL_ID = "+ guardianhDataModel.NATIONAL_ID+",MOE_EMAIL ="+ guardianhDataModel.MOE_EMAIL+",MOE_EMPLOYER_TYPE_ID = "+guardianhDataModel.MOE_EMPLOYER_TYPE_ID+",MOE_HOME_CONTACT_NBR = "+guardianhDataModel.MOE_HOME_CONTACT_NBR+",MOE_ISGUARDIAN = "+guardianhDataModel.MOE_ISGUARDIAN+",MOE_MARITAL_STATUS_ID = "+guardianhDataModel.MOE_MARITAL_STATUS_ID+",MOE_MOBILE_CONTACT_NBR = "+guardianhDataModel.MOE_MOBILE_CONTACT_NBR+", MOE_RELATIONSHIP_TYPE_ID ="+ guardianhDataModel.MOE_RELATIONSHIP_TYPE_ID+",MOE_RELATED_QID = "+guardianhDataModel.MOE_RELATED_QID+", MOE_USERID = "+guardianhDataModel.MOE_USERID+",MOE_EMPLOYER = "+guardianhDataModel.MOE_EMPLOYER+",MOE_APPL_NBR = "+guardianhDataModel.MOE_APPL_NBR+",MOE_GUARDIAN_NAME_ARA ="+ guardianhDataModel.MOE_GUARDIAN_NAME_ARA+",MOE_GUARDIAN_NAME_ENG = "+guardianhDataModel.MOE_GUARDIAN_NAME_ENG}).Result;
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/MOE_GURADIAN_DATA/", guardianhDataModel);
                    DBOperationResult ReturnedResult3 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Guardian Data", Description = "after calling Post function in Sharepoint ", UserID = guardianhDataModel.MOE_USERID }).Result;
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        ReturnedResult = await res.Content.ReadAsAsync<DBOperationResult>();
                    }
                }
                catch(Exception ex)
                {
                    //this mean service down (Server may be changed)
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Guardian Data", Description = "Insert Exception: "+ex.Message, UserID = guardianhDataModel.MOE_USERID }).Result;
                    //MessageID will be Error

                }
                return ReturnedResult;
            }
        }

        #region Added new function for withdrawal application by Veer 12 July 2018
        public static async Task<MOE_GUARDIAN_DATA_Model> GetStudentInfo(string QID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                MOE_GUARDIAN_DATA_Model results = new MOE_GUARDIAN_DATA_Model();
                try
                {
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Academic Information Data", Description = "Start Inser", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;

                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetStudentContacts/{0}", QID));
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        results = await res.Content.ReadAsAsync<MOE_GUARDIAN_DATA_Model>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Guardian Data", Description = "GetStudentContactsInfo Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }
                return results;
            }
        }
        #endregion
    }
}
