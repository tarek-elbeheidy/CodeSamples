using MOEHE.PSPES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Repository
{
    class TestingRepository
    {
        /// <summary>
        /// Used to get all Testings
        /// </summary>
        /// <returns></returns>
        //public static async List<Models.TestingModel> Get()
        //{
        //    using (HttpClient cons = Utility.GetHttpClientConnection())
        //    {

        //        List<TestingModel> Testings = new List<TestingModel>();

        //        HttpResponseMessage res = await cons.GetAsync("api/Testing/");

        //        res.EnsureSuccessStatusCode();
        //        if (res.IsSuccessStatusCode)
        //        {
        //            Testings = await res.Content.ReadAsAsync<List<TestingModel>>();
        //        }

        //        return Testings;
        //    }
        //}
    }
}
