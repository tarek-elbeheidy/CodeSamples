using MOEHE.PSPES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Repository
{
    class MOI_COUNTRY_MAP_Repository
    {
        public static async Task<List<MOI_COUNTRY_MAP>> GetCountries()
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<MOI_COUNTRY_MAP> countries = new List<MOI_COUNTRY_MAP>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync("api/GetMOICountries");

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        countries = await res.Content.ReadAsAsync<List<MOI_COUNTRY_MAP>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return countries;
            }
        }
    }
}
