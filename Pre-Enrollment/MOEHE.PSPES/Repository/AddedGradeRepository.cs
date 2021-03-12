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
    class AddedGradeRepository
    {

        public static async Task<DBOperationResult> Insert(AddedGradeModel AddedgradeModel)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/AddedGrade/", AddedgradeModel);
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



        public static async Task<List<AddedGradeModel>> GetAddedGrades(int Year, string SchoolCode)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<AddedGradeModel> AgM = new List<AddedGradeModel>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetAddedGrades/{0}/{1}", Year, SchoolCode));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        AgM = await res.Content.ReadAsAsync<List<AddedGradeModel>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return AgM;
            }
        }


    }
}
