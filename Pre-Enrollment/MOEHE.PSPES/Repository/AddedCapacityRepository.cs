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
    class AddedCapacityRepository
    {
        public static async Task<DBOperationResult> Insert(AddedCapacityModel AddedCapModel)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/AddedCapacity/", AddedCapModel);
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



        public static async Task <List<AddedCapacityModel>> GetAddedCapacity(int Year, string SchoolCode)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

               List< AddedCapacityModel> Adc = new List<AddedCapacityModel>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetAddedCapacity/{0}/{1}", Year, SchoolCode));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Adc = await res.Content.ReadAsAsync<List<AddedCapacityModel>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return Adc;
            }
        }




    }
}
