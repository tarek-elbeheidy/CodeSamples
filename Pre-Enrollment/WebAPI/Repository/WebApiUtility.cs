using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    class WebApiUtility
    {
        /// <summary>
        /// we use this method to get the connection to the WebAPI service
        /// </summary>
        /// <returns></returns>
        public static HttpClient GetHttpClientConnection(string URI)
        {
            //HttpClient cons = new HttpClient() { BaseAddress = new Uri("http://localhost:64711/") };
            HttpClient cons = new HttpClient() { BaseAddress = new Uri(URI) };


            cons.DefaultRequestHeaders.Accept.Clear();
           // cons.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            cons.DefaultRequestHeaders.Add("API_KEY", "FD1C3E800EE1487D9057920174800B32");


            return cons;
        }
    }
}