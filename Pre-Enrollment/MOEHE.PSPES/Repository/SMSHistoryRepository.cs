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
    class SMSHistoryRepository
    {

        public static async Task<DBOperationResult> Insert( SMSHistoryModel smsHModel)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/SMSHistory/", smsHModel);
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



        public static async Task<List<SMSHistoryModel>> GetSMSHistory(string AppRefNum, string MsgType, string MobileNumber, string QID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<SMSHistoryModel> SMShm = new List<SMSHistoryModel>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetSMSHistory/{0}/{1}/{2}/{3}", AppRefNum, MsgType,MobileNumber,QID));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        SMShm = await res.Content.ReadAsAsync<List<SMSHistoryModel>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return SMShm;
            }
        }
        public static async Task<List<SMSHistoryModel>> GetSMSHistory(string AppRefNum, string MsgType,string MessageTitle, string MobileNumber, string QID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<SMSHistoryModel> SMShm = new List<SMSHistoryModel>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetSMSHistory/{0}/{1}/{2}/{3}/{4}", new object[] { AppRefNum, MsgType, MessageTitle, MobileNumber,QID }));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        SMShm = await res.Content.ReadAsAsync<List<SMSHistoryModel>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return SMShm;
            }
        }

    }
}
