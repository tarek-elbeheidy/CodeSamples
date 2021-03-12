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
    public class MOE_SCHOOL_Repository
    {
        public static async Task<MOE_SCHOOL_Model> GetSchoolInfo(string schoolYearID, string schoolCode, string isPublic)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                MOE_SCHOOL_Model school_Model = new MOE_SCHOOL_Model();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetSchoolInfo/{0}/{1}/{2}", schoolYearID, schoolCode, isPublic));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        school_Model = await res.Content.ReadAsAsync<MOE_SCHOOL_Model>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return school_Model;
            }
        }


        public static async Task<List<schoolGrade>> GetSchoolGrade(string schoolYearID, string schoolCode, string isPublic)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<schoolGrade> schoolGrade = new List<schoolGrade>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetSchoolGrade/{0}/{1}/{2}/{3}", schoolYearID, schoolCode, isPublic,"NullValue"));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        schoolGrade = await res.Content.ReadAsAsync<List<schoolGrade>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return schoolGrade;
            }
        }

        public static async Task<List<MOE_SCHOOL_Model>> GetSchools(string schoolYearID,  string isPublic)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<MOE_SCHOOL_Model> Schools = new List<MOE_SCHOOL_Model>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetAllSchools/{0}/{1}", schoolYearID, isPublic));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Schools = await res.Content.ReadAsAsync<List<MOE_SCHOOL_Model>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return Schools;
            }
        }

        public static async Task<List<MOE_SCHOOL_Model>> GetQatarSchools(string schoolYearID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<MOE_SCHOOL_Model> Schools = new List<MOE_SCHOOL_Model>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetQatarSchools/{0}", schoolYearID));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Schools = await res.Content.ReadAsAsync<List<MOE_SCHOOL_Model>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return Schools;
            }
        }
        #region added code for close and share data to NSIS modified by Veer on 16 July 2018
        public static async Task<List<MOE_SCHOOL_Model>> GetSchoolsDB()
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<MOE_SCHOOL_Model> Schools = new List<MOE_SCHOOL_Model>();
                try
                {
                    DBOperationResult resultsLog = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Close and Share Data", Description = "Insert Exception:", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetSchoolsListDB"));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Schools = await res.Content.ReadAsAsync<List<MOE_SCHOOL_Model>>();
                    }
                }
                catch (Exception ex)
                {
                    DBOperationResult resultsLog = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Close and Share Data", Description = "Insert Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }

                return Schools;
            }
        }
        public static async Task<DBOperationResult> Update(MOE_SCHOOL_Model oModel)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult Results = new DBOperationResult();
                try
                {
                    DBOperationResult resultsLog = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Close and Share Data", Description = "Start Inser", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/MOE_SCHOOL/", oModel);
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Results = await res.Content.ReadAsAsync<DBOperationResult>();
                    }
                }
                catch (Exception ex)
                {
                    DBOperationResult resultsLog = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Close and Share Data", Description = "Insert Exception: " + ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                }
                return Results;
            }
        }
        #endregion
    }
}
