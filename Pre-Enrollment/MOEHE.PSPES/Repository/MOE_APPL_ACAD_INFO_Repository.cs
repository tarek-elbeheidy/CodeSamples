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
    class MOE_APPL_ACAD_INFO_Repository
    {
        public static async Task<MOE_APPL_ACAD_INFO_Model> GetAcadInfoByQIDAndTerm(string QID, string Term)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Academic Information Data", Description = "Start GetAcadInfoByQIDAndTerm", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                MOE_APPL_ACAD_INFO_Model Acad_Model = new MOE_APPL_ACAD_INFO_Model();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetAcadInfo/{0}/{1}", QID, Term));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Acad_Model = await res.Content.ReadAsAsync<MOE_APPL_ACAD_INFO_Model>();
                    }
                }
                catch (Exception ex)
                {
                    ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Academic Information Data", Description = " GetAcadInfoByQIDAndTerm Exception:" + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    //this mean service down (Server may be changed)
                }

                return Acad_Model;
            }
        }

        public static async Task<List<MOE_APPL_ACAD_INFO_Model>> GetAcadInfoRefernceNumber(string ReferenceNumber)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Academic Information Data", Description = "Start GetAcadInfoRefernceNumber", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                List<MOE_APPL_ACAD_INFO_Model> Acad_Model = new List<MOE_APPL_ACAD_INFO_Model>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetAcadInfoByRef/{0}", ReferenceNumber));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Acad_Model = await res.Content.ReadAsAsync<List<MOE_APPL_ACAD_INFO_Model>>();
                    }
                }
                catch (Exception ex)
                {
                    ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Academic Information Data", Description = " GetAcadInfoByQIDAndTerm Exception:" + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    //this mean service down (Server may be changed)
                }

                return Acad_Model;
            }
        }

        public static async Task<DBOperationResult> Insert(MOE_APPL_ACAD_INFO_Model acadInfoModel)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Academic Information Data", Description = "Start Inser", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/MOE_APPL_ACAD_INFO/", acadInfoModel);
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        ReturnedResult = await res.Content.ReadAsAsync<DBOperationResult>();
                    }
                }
                catch(Exception ex)
                {
                    //this mean service down (Server may be changed)
                    //MessageID will be Error
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Academic Information Data", Description = "Insert Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;

                }
                return ReturnedResult;
            }
        }

        #region added new method by Veer on 11 July 2018
        public static async Task<MOE_APPL_ACAD_INFO_Model> GetAcademicInfoByQid(string QID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Academic Information Data", Description = "Start GetAcadInfoByQIDAndTerm", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                MOE_APPL_ACAD_INFO_Model Acad_Model = new MOE_APPL_ACAD_INFO_Model();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetAcadamicInfo/{0}", QID));
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Acad_Model = await res.Content.ReadAsAsync<MOE_APPL_ACAD_INFO_Model>();
                    }
                }
                catch (Exception ex)
                {
                    ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Academic Information Data", Description = " GetAcadInfoByQIDAndTerm Exception:" + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    //this mean service down (Server may be changed)
                }

                return Acad_Model;
            }
        }
        #endregion
    }
}
