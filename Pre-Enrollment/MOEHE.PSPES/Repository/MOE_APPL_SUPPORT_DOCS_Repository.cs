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
    class MOE_APPL_SUPPORT_DOCS_Repository
    {


        public static async Task<List<MOE_APPL_SUPPORT_DOCS_Model>> GetBy(string ApplicantRefernceNumber,string QID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<MOE_APPL_SUPPORT_DOCS_Model> aPPLICATION_DATA_Model = new List<MOE_APPL_SUPPORT_DOCS_Model>();
                try
                {
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Supporting Documents Data", Description = "Start GetBy", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetApplicationSupportingDocs/{0}/{1}", ApplicantRefernceNumber,QID));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        aPPLICATION_DATA_Model = await res.Content.ReadAsAsync<List<MOE_APPL_SUPPORT_DOCS_Model>>();
                    }
                }
                catch (Exception ex)
                {
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Academic Information Data", Description = "GetBy Exception: "+ex.Message , UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    //this mean service down (Server may be changed)
                }

                return aPPLICATION_DATA_Model;
            }
        }


        public static async Task<DBOperationResult> Insert(MOE_APPL_SUPPORT_DOCS_Model_Not_Serializable applSupportDocsModel)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Supporting Documents Data", Description = "Start Insert", UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/MOE_APPL_SUPPORT_DOCS/", applSupportDocsModel);
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
                    DBOperationResult ReturnedResult2 = TransactionLogsRepository.Insert(new TransactionsLog { CreatedDate = DateTime.Now, ShortDescription = "Application Supporting Documents Data", Description = "Insert Eception: "+ex.Message, UserID = SPContext.Current.Site.RootWeb.CurrentUser.LoginName }).Result;

                }
                return ReturnedResult;
            }
        }
    }
}
